using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IBrainChrom2018;

public class ChannelTCPServer : IDisposable
{
	private int _maxClient;

	private int _clientCount;

	private TcpListener _listener;

	public List<object> _clients;

	private bool disposed = false;

	public byte[] dataBuff = new byte[200];

	public bool IsRunning { get; private set; }

	public IPAddress Address { get; private set; }

	public int Port { get; private set; }

	public Encoding Encoding { get; set; }

	public event EventHandler<AsyncEventArgs> ClientConnected;

	public event EventHandler<AsyncEventArgs> ClientDisconnected;

	public event EventHandler<AsyncEventArgs> DataReceived;

	public event EventHandler<AsyncEventArgs> PrepareSend;

	public event EventHandler<AsyncEventArgs> CompletedSend;

	public event EventHandler<AsyncEventArgs> NetError;

	public event EventHandler<AsyncEventArgs> OtherException;

	public ChannelTCPServer(int listenPort)
		: this(IPAddress.Any, listenPort)
	{
	}

	public ChannelTCPServer(IPEndPoint localEP)
		: this(localEP.Address, localEP.Port)
	{
	}

	public ChannelTCPServer(IPAddress localIPAddress, int listenPort)
	{
		Address = localIPAddress;
		Port = listenPort;
		Encoding = Encoding.Default;
		_clients = new List<object>();
		_listener = new TcpListener(Address, Port);
	}

	public void Start()
	{
		if (!IsRunning)
		{
			IsRunning = true;
			_listener.Start();
			_listener.BeginAcceptTcpClient(HandleTcpClientAccepted, _listener);
		}
	}

	public void Start(int backlog)
	{
		if (!IsRunning)
		{
			IsRunning = true;
			_listener.Start(backlog);
			_listener.BeginAcceptTcpClient(HandleTcpClientAccepted, _listener);
		}
	}

	public void Stop()
	{
		if (IsRunning)
		{
			IsRunning = false;
			_listener.Stop();
			lock (_clients)
			{
				CloseAllClient();
			}
		}
	}

	private void HandleTcpClientAccepted(IAsyncResult ar)
	{
		if (IsRunning)
		{
			TcpClient tcpClient = _listener.EndAcceptTcpClient(ar);
			byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
			ChannelTCPClientState channelTCPClientState = new ChannelTCPClientState(tcpClient, buffer);
			lock (_clients)
			{
				_clients.Add(channelTCPClientState);
				RaiseClientConnected(channelTCPClientState);
			}
			NetworkStream networkStream = channelTCPClientState.NetworkStream;
			networkStream.BeginRead(channelTCPClientState.Buffer, 0, channelTCPClientState.Buffer.Length, HandleDataReceived, channelTCPClientState);
			_listener.BeginAcceptTcpClient(HandleTcpClientAccepted, ar.AsyncState);
		}
	}

	private void HandleDataReceived(IAsyncResult ar)
	{
		if (!IsRunning)
		{
			return;
		}
		ChannelTCPClientState channelTCPClientState = (ChannelTCPClientState)ar.AsyncState;
		NetworkStream networkStream = channelTCPClientState.NetworkStream;
		int num = 0;
		try
		{
			num = networkStream.EndRead(ar);
		}
		catch
		{
			num = 0;
		}
		if (num == 0)
		{
			lock (_clients)
			{
				_clients.Remove(channelTCPClientState);
				RaiseClientDisconnected(channelTCPClientState);
				return;
			}
		}
		byte[] dst = new byte[num];
		Buffer.BlockCopy(channelTCPClientState.Buffer, 0, dst, 0, num);
		RaiseDataReceived(channelTCPClientState);
		networkStream.BeginRead(channelTCPClientState.Buffer, 0, channelTCPClientState.Buffer.Length, HandleDataReceived, channelTCPClientState);
		dataBuff[0] = channelTCPClientState.Buffer[0];
		dataBuff[1] = channelTCPClientState.Buffer[1];
		dataBuff[2] = channelTCPClientState.Buffer[2];
		dataBuff[3] = channelTCPClientState.Buffer[3];
	}

	public void Send(ChannelTCPClientState state, byte[] data)
	{
		RaisePrepareSend(state);
		Send(state.TcpClient, data);
	}

	public void Send(TcpClient client, byte[] data)
	{
		if (!IsRunning)
		{
			throw new InvalidProgramException("This TCP Scoket server has not been started.");
		}
		if (client == null)
		{
			throw new ArgumentNullException("client");
		}
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		client.GetStream().BeginWrite(data, 0, data.Length, SendDataEnd, client);
	}

	private void SendDataEnd(IAsyncResult ar)
	{
		((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
		RaiseCompletedSend(null);
	}

	private void RaiseClientConnected(ChannelTCPClientState state)
	{
		if (this.ClientConnected != null)
		{
			this.ClientConnected(this, new AsyncEventArgs(state));
		}
	}

	private void RaiseClientDisconnected(ChannelTCPClientState state)
	{
		if (this.ClientDisconnected != null)
		{
			this.ClientDisconnected(this, new AsyncEventArgs("连接断开"));
		}
	}

	private void RaiseDataReceived(ChannelTCPClientState state)
	{
		if (this.DataReceived != null)
		{
			this.DataReceived(this, new AsyncEventArgs(state));
		}
	}

	private void RaisePrepareSend(ChannelTCPClientState state)
	{
		if (this.PrepareSend != null)
		{
			this.PrepareSend(this, new AsyncEventArgs(state));
		}
	}

	private void RaiseCompletedSend(ChannelTCPClientState state)
	{
		if (this.CompletedSend != null)
		{
			this.CompletedSend(this, new AsyncEventArgs(state));
		}
	}

	private void RaiseNetError(ChannelTCPClientState state)
	{
		if (this.NetError != null)
		{
			this.NetError(this, new AsyncEventArgs(state));
		}
	}

	private void RaiseOtherException(ChannelTCPClientState state, string descrip)
	{
		if (this.OtherException != null)
		{
			this.OtherException(this, new AsyncEventArgs(descrip, state));
		}
	}

	private void RaiseOtherException(ChannelTCPClientState state)
	{
		RaiseOtherException(state, "");
	}

	public void Close(ChannelTCPClientState state)
	{
		if (state != null)
		{
			state.Close();
			_clients.Remove(state);
			_clientCount--;
		}
	}

	public void CloseAllClient()
	{
		foreach (ChannelTCPClientState client in _clients)
		{
			Close(client);
		}
		_clientCount = 0;
		_clients.Clear();
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposed)
		{
			return;
		}
		if (disposing)
		{
			try
			{
				Stop();
				if (_listener != null)
				{
					_listener = null;
				}
			}
			catch (SocketException)
			{
				RaiseOtherException(null);
			}
		}
		disposed = true;
	}
}
