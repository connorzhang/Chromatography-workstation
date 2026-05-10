using System;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace IBrainChrom2018;

public class VirtualSignal
{
	public delegate void GetVrData(float value);

	public const string virtualSignalDir = "Station\\VSG\\";

	private const string string_0 = ".t";

	public VirSglArg virSglArg_0 = new VirSglArg();

	private BackgroundWorker backgroundWorker_0 = new BackgroundWorker();

	private GetVrData getVrData_0;

	public event GetVrData OnGetVrData
	{
		add
		{
			GetVrData getVrData = getVrData_0;
			GetVrData getVrData2;
			do
			{
				getVrData2 = getVrData;
				GetVrData value2 = (GetVrData)Delegate.Combine(getVrData2, value);
				getVrData = Interlocked.CompareExchange(ref getVrData_0, value2, getVrData2);
			}
			while (getVrData != getVrData2);
		}
		remove
		{
			GetVrData getVrData = getVrData_0;
			GetVrData getVrData2;
			do
			{
				getVrData2 = getVrData;
				GetVrData value2 = (GetVrData)Delegate.Remove(getVrData2, value);
				getVrData = Interlocked.CompareExchange(ref getVrData_0, value2, getVrData2);
			}
			while (getVrData != getVrData2);
		}
	}

	public VirtualSignal()
	{
		backgroundWorker_0.WorkerReportsProgress = true;
		backgroundWorker_0.WorkerSupportsCancellation = true;
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.ProgressChanged += backgroundWorker_0_ProgressChanged;
	}

	public void BeginThread()
	{
		virSglArg_0.uint_0 = 0u;
		if (!backgroundWorker_0.IsBusy)
		{
			backgroundWorker_0.RunWorkerAsync();
		}
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		while (!backgroundWorker_0.CancellationPending)
		{
			lock (virSglArg_0)
			{
				if (virSglArg_0.sample && virSglArg_0.signals.Length > 100)
				{
					backgroundWorker_0.ReportProgress(0, virSglArg_0.signals[(uint)(UIntPtr)virSglArg_0.uint_0]);
					if ((ulong)virSglArg_0.uint_0 < (ulong)(virSglArg_0.signals.Length - 1))
					{
						virSglArg_0.uint_0++;
					}
				}
				else
				{
					backgroundWorker_0.ReportProgress(0, virSglArg_0.GenerateSignal());
				}
			}
			Thread.Sleep(virSglArg_0.sample_interval);
		}
	}

	private void backgroundWorker_0_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		if (getVrData_0 != null)
		{
			getVrData_0((float)e.UserState);
		}
	}

	public void Detector_Set(bool zero)
	{
		lock (virSglArg_0)
		{
			virSglArg_0.bs_setZero = zero;
		}
	}

	public void LoadVirtualSignals(int int_0)
	{
		string text = int_0 + 1 + ".t";
		text = ResourceImageLoad.ExePath() + "Station\\VSG\\" + text;
		if (!File.Exists(text))
		{
			Array.Resize(ref virSglArg_0.signals, 0);
			return;
		}
		StreamReader streamReader = new FileInfo(text).OpenText();
		try
		{
			string object_ = method_0(streamReader.ReadLine());
			virSglArg_0.sample_interval = Class49.Object2Int(object_, 100);
			int num = 0;
			string string_;
			while ((string_ = streamReader.ReadLine()) != null)
			{
				if (num >= virSglArg_0.signals.Length)
				{
					Array.Resize(ref virSglArg_0.signals, virSglArg_0.signals.Length + 1000);
				}
				float num2 = Class49.String2Float(method_0(string_), 0f);
				virSglArg_0.signals[num++] = virSglArg_0.signal_bs + num2 * virSglArg_0.signal_scale;
			}
			Array.Resize(ref virSglArg_0.signals, num);
		}
		finally
		{
			streamReader.Close();
		}
	}

	private string method_0(string string_1)
	{
		string_1 = string_1.Trim();
		string text = "";
		for (int i = 0; i < string_1.Length; i++)
		{
			if (string_1[i] != '-' && string_1[i] != '.' && ('0' > string_1[i] || string_1[i] > '9'))
			{
				return text;
			}
			text += string_1[i];
		}
		return text;
	}

	public void Stop()
	{
		if (backgroundWorker_0.IsBusy)
		{
			backgroundWorker_0.CancelAsync();
		}
	}
}
