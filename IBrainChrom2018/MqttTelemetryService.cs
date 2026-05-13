using System;
using System.Collections.Concurrent;
using System.Net.Security;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using IBrainChrom2018.Unit;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace IBrainChrom2018;

public sealed class MqttTelemetryService
{
	private static readonly Lazy<MqttTelemetryService> lazy = new Lazy<MqttTelemetryService>(() => new MqttTelemetryService());

	public static MqttTelemetryService Instance => lazy.Value;

	private readonly object sync = new object();

	private IMqttClient client;

	private CancellationTokenSource cts;

	private Task worker;

	private Timer heartbeatTimer;

	private Timer aggFlushTimer;

	private Timer paramTimer;

	private BlockingCollection<MqttApplicationMessage> queue;

	private JavaScriptSerializer json;

	private bool started;

	private volatile string statusText = "未连接";

	private volatile bool isConnected;

	private int seq;

	private long lastConnLogTicks;

	private readonly System.Collections.Concurrent.ConcurrentDictionary<string, SysAggEntry> sysAgg = new System.Collections.Concurrent.ConcurrentDictionary<string, SysAggEntry>();

	private sealed class SysAggEntry
	{
		public string Severity;
		public string Message;
		public int Count;
		public string FirstTs;
		public string LastTs;
	}

	private MqttTelemetryService()
	{
		json = new JavaScriptSerializer
		{
			MaxJsonLength = int.MaxValue
		};
		queue = new BlockingCollection<MqttApplicationMessage>(new ConcurrentQueue<MqttApplicationMessage>(), 5000);
	}

	public void StartOrReload()
	{
		lock (sync)
		{
			StopInternal();
			StartInternal();
		}
	}

	public void Stop()
	{
		lock (sync)
		{
			StopInternal();
		}
	}

	private void StartInternal()
	{
		SystemParam sysParam = SystemParam.Create();
		if (!sysParam.bMqttEnable)
		{
			SetStatus("MQTT未启用", connected: false);
			started = false;
			return;
		}
		if (string.IsNullOrWhiteSpace(sysParam.strMqttHost))
		{
			SetStatus("MQTT未配置", connected: false);
			started = false;
			return;
		}

		started = true;
		SetStatus("待连接 " + sysParam.strMqttHost + ":" + sysParam.iMqttPort, connected: false);
		TryWriteRunLog("MQTT start");
		cts = new CancellationTokenSource();
		var factory = new MqttFactory();
		client = factory.CreateMqttClient();
		worker = Task.Run(() => WorkerLoop(cts.Token), cts.Token);
		int hb = sysParam.iMqttHeartbeatSec;
		if (hb < 5)
		{
			hb = 5;
		}
		heartbeatTimer = new Timer(_ =>
		{
			try
			{
				PublishHeartbeat();
			}
			catch
			{
			}
		}, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(hb));
		aggFlushTimer = new Timer(_ =>
		{
			try
			{
				FlushSystemAgg();
			}
			catch
			{
			}
		}, null, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
		paramTimer = new Timer(_ =>
		{
			try
			{
				PublishParamSnapshot();
			}
			catch
			{
			}
		}, null, TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(10.0));
		PublishParamSnapshot();
	}

	private void StopInternal()
	{
		started = false;
		SetStatus("已停止", connected: false);
		TryWriteRunLog("MQTT stop");
		try
		{
			aggFlushTimer?.Dispose();
		}
		catch
		{
		}
		aggFlushTimer = null;
		try
		{
			paramTimer?.Dispose();
		}
		catch
		{
		}
		paramTimer = null;
		try
		{
			heartbeatTimer?.Dispose();
		}
		catch
		{
		}
		heartbeatTimer = null;

		try
		{
			cts?.Cancel();
		}
		catch
		{
		}

		try
		{
			worker?.Wait(2000);
		}
		catch
		{
		}
		worker = null;

		try
		{
			cts?.Dispose();
		}
		catch
		{
		}
		cts = null;

		try
		{
			client?.Dispose();
		}
		catch
		{
		}
		client = null;

		try
		{
			while (queue != null && queue.TryTake(out _, 0))
			{
			}
		}
		catch
		{
		}
	}

	public void EnqueueAudit(string userName, string deviceId, string action, string detail)
	{
		try
		{
			if (!started)
			{
				return;
			}
			SystemParam sysParam = SystemParam.Create();
			string sid = sysParam.strStationId?.Trim() ?? "";
			string topicRoot = BuildTopicRoot(sysParam, sid);
			var payload = BuildEnvelope("I", userName, new
			{
				dev = deviceId,
				a = action,
				detail = detail
			});
			string jsonText = json.Serialize(payload);
			var msg = new MqttApplicationMessageBuilder()
				.WithTopic(topicRoot + "/events/audit")
				.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
				.WithPayload(jsonText)
				.Build();
			TryEnqueue(msg);
		}
		catch
		{
		}
	}

	public void EnqueueSystem(string severity, string message)
	{
		try
		{
			if (!started)
			{
				return;
			}
			AddSystemAgg(severity, message);
		}
		catch
		{
		}
	}

	private void AddSystemAgg(string severity, string message)
	{
		string sev = string.IsNullOrEmpty(severity) ? "I" : severity;
		string msg = message ?? "";
		string key = sev + "|" + msg;
		string now = DateTime.Now.ToString("yyyyMMddHHmmss");
		SysAggEntry entry = sysAgg.GetOrAdd(key, _ => new SysAggEntry
		{
			Severity = sev,
			Message = msg,
			Count = 0,
			FirstTs = now,
			LastTs = now
		});
		lock (entry)
		{
			if (entry.Count == 0)
			{
				entry.FirstTs = now;
				entry.LastTs = now;
				entry.Count = 1;
				return;
			}
			entry.Count++;
			entry.LastTs = now;
		}
	}

	private void FlushSystemAgg()
	{
		try
		{
			if (!started)
			{
				return;
			}
			SystemParam sysParam = SystemParam.Create();
			string sid = sysParam.strStationId?.Trim() ?? "";
			string topicRoot = BuildTopicRoot(sysParam, sid);
			foreach (var pair in sysAgg)
			{
				SysAggEntry entry = pair.Value;
				int count;
				string first;
				string last;
				string sev;
				string msg;
				lock (entry)
				{
					count = entry.Count;
					first = entry.FirstTs;
					last = entry.LastTs;
					sev = entry.Severity;
					msg = entry.Message;
					entry.Count = 0;
				}
				if (count <= 0)
				{
					sysAgg.TryRemove(pair.Key, out _);
					continue;
				}
				var payload = BuildEnvelope(sev, null, new
				{
					m = msg,
					c = count,
					t0 = first,
					t1 = last
				});
				string jsonText = json.Serialize(payload);
				var mqttMsg = new MqttApplicationMessageBuilder()
					.WithTopic(topicRoot + "/events/system")
					.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
					.WithPayload(jsonText)
					.Build();
				TryEnqueue(mqttMsg);
			}
		}
		catch
		{
		}
	}

	private void PublishParamSnapshot()
	{
		try
		{
			if (!started)
			{
				return;
			}
			SystemParam sysParam = SystemParam.Create();
			string sid = sysParam.strStationId?.Trim() ?? "";
			string topicRoot = BuildTopicRoot(sysParam, sid);
			var payload = BuildEnvelope("I", null, new
			{
				ver = AssemblyInfoCfg.SoftVersion(),
				db = sysParam.iDbConnectType,
				modbus = sysParam.iComModbusType,
				hb = sysParam.iMqttHeartbeatSec,
				clientId = (sysParam.strMqttClientId ?? "").Trim()
			});
			string jsonText = json.Serialize(payload);
			var mqttMsg = new MqttApplicationMessageBuilder()
				.WithTopic(topicRoot + "/status/params")
				.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
				.WithRetainFlag(true)
				.WithPayload(jsonText)
				.Build();
			TryEnqueue(mqttMsg);
		}
		catch
		{
		}
	}

	private void PublishHeartbeat()
	{
		SystemParam sysParam = SystemParam.Create();
		if (!sysParam.bMqttEnable)
		{
			return;
		}
		string sid = sysParam.strStationId?.Trim() ?? "";
		string topicRoot = BuildTopicRoot(sysParam, sid);
		var payload = BuildEnvelope("I", null, new
		{
			ver = AssemblyInfoCfg.SoftVersion(),
			q = queue.Count
		});
		string jsonText = json.Serialize(payload);
		var msg = new MqttApplicationMessageBuilder()
			.WithTopic(topicRoot + "/status")
			.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
			.WithRetainFlag(true)
			.WithPayload(jsonText)
			.Build();
		TryEnqueue(msg);
	}

	private static string BuildTopicRoot(SystemParam sysParam, string stationId)
	{
		string prefix = (sysParam.strMqttTopicPrefix ?? "").Trim();
		if (prefix == "")
		{
			prefix = "chrom/v1/default/default/{stationId}";
		}
		prefix = prefix.Replace("{stationId}", stationId);
		return prefix.TrimEnd('/');
	}

	private void TryEnqueue(MqttApplicationMessage msg)
	{
		if (queue == null)
		{
			return;
		}
		if (!queue.TryAdd(msg))
		{
			queue.TryTake(out _, 0);
			queue.TryAdd(msg);
		}
	}

	private async Task WorkerLoop(CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			try
			{
				await EnsureConnected(token).ConfigureAwait(false);
				MqttApplicationMessage msg = queue.Take(token);
				await client.PublishAsync(msg, token).ConfigureAwait(false);
			}
			catch (OperationCanceledException)
			{
				break;
			}
			catch (Exception ex)
			{
				SetStatus("连接异常: " + ex.Message, connected: false);
				TryWriteRunLogThrottled("MQTT error: " + ex.Message, 30);
				await Task.Delay(1000, token).ConfigureAwait(false);
			}
		}
	}

	private async Task EnsureConnected(CancellationToken token)
	{
		if (client == null)
		{
			throw new InvalidOperationException("MQTT client not initialized");
		}
		if (client.IsConnected)
		{
			return;
		}
		SystemParam sysParam = SystemParam.Create();
		var builder = new MqttClientOptionsBuilder();
		string clientId = (sysParam.strMqttClientId ?? "").Trim();
		if (clientId == "")
		{
			clientId = (sysParam.strStationId ?? "").Trim();
			if (clientId == "")
			{
				clientId = Guid.NewGuid().ToString("N");
			}
		}
		builder.WithClientId(clientId);
		builder.WithTcpServer(sysParam.strMqttHost, sysParam.iMqttPort);
		if (!string.IsNullOrEmpty(sysParam.strMqttUser))
		{
			builder.WithCredentials(sysParam.strMqttUser, sysParam.strMqttPassword);
		}
		if (sysParam.bMqttTls)
		{
			builder.WithTls(new MqttClientOptionsBuilderTlsParameters
			{
				UseTls = true,
				SslProtocol = SslProtocols.Tls12,
				AllowUntrustedCertificates = sysParam.bMqttTlsAllowUntrusted,
				IgnoreCertificateChainErrors = sysParam.bMqttTlsAllowUntrusted,
				IgnoreCertificateRevocationErrors = sysParam.bMqttTlsAllowUntrusted,
				CertificateValidationHandler = args =>
				{
					if (!sysParam.bMqttTlsAllowUntrusted)
					{
						return args.SslPolicyErrors == SslPolicyErrors.None;
					}
					return true;
				}
			});
		}
		builder.WithCleanSession();
		MqttClientOptions options = builder.Build();
		try
		{
			SetStatus("连接中...", connected: false);
			await client.ConnectAsync(options, token).ConfigureAwait(false);
			SetStatus("已连接 " + sysParam.strMqttHost + ":" + sysParam.iMqttPort, connected: true);
			TryWriteRunLog("MQTT connected");
		}
		catch (Exception ex)
		{
			SetStatus("连接失败 " + sysParam.strMqttHost + ":" + sysParam.iMqttPort, connected: false);
			TryWriteRunLogThrottled("MQTT connect failed: " + ex.Message, 30);
			throw;
		}
	}

	private void TryWriteRunLog(string message)
	{
		try
		{
			if (IBrainChrom2018.Unit.LogMgr.Instance == null)
			{
				return;
			}
			IBrainChrom2018.Unit.LogMgr.Instance.Write2RunLog("MQTT " + (message ?? ""));
		}
		catch
		{
		}
	}

	private void TryWriteRunLogThrottled(string message, int seconds)
	{
		try
		{
			long now = DateTime.UtcNow.Ticks;
			long last = Interlocked.Read(ref lastConnLogTicks);
			long min = TimeSpan.FromSeconds(seconds).Ticks;
			if (now - last < min)
			{
				return;
			}
			Interlocked.Exchange(ref lastConnLogTicks, now);
			TryWriteRunLog(message);
		}
		catch
		{
		}
	}

	public string GetStatusText()
	{
		return statusText;
	}

	public bool IsConnected => isConnected;

	private void SetStatus(string text, bool connected)
	{
		statusText = text ?? "";
		isConnected = connected;
	}

	public string PublishTest()
	{
		try
		{
			SystemParam sysParam = SystemParam.Create();
			if (!sysParam.bMqttEnable)
			{
				return "MQTT未启用";
			}
			if (string.IsNullOrWhiteSpace(sysParam.strMqttHost))
			{
				return "MQTT服务器未配置";
			}
			string sid = sysParam.strStationId?.Trim() ?? "";
			string topicRoot = BuildTopicRoot(sysParam, sid);
			var payload = BuildEnvelope("I", null, new
			{
				msg = "test"
			});
			string jsonText = json.Serialize(payload);
			var msg = new MqttApplicationMessageBuilder()
				.WithTopic(topicRoot + "/events/test")
				.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
				.WithPayload(jsonText)
				.Build();
			TryEnqueue(msg);
			return null;
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	private object BuildEnvelope(string severity, string userName, object data)
	{
		var payload = new System.Collections.Generic.Dictionary<string, object>();
		payload["v"] = 1;
		payload["id"] = NextId();
		payload["ts"] = DateTime.Now.ToString("yyyyMMddHHmmss");
		if (!string.IsNullOrEmpty(severity))
		{
			payload["sev"] = severity;
		}
		if (!string.IsNullOrEmpty(userName))
		{
			payload["uid"] = userName;
		}
		if (data != null)
		{
			payload["d"] = data;
		}
		return payload;
	}

	private string NextId()
	{
		int value = Interlocked.Increment(ref seq);
		long ticks = DateTime.UtcNow.Ticks;
		return ToBase36((ulong)ticks) + ToBase36((ulong)value);
	}

	private static string ToBase36(ulong value)
	{
		const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
		if (value == 0)
		{
			return "0";
		}
		char[] buffer = new char[32];
		int i = buffer.Length;
		while (value > 0)
		{
			buffer[--i] = chars[(int)(value % 36)];
			value /= 36;
		}
		return new string(buffer, i, buffer.Length - i);
	}
}

