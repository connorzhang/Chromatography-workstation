using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public sealed class AsyncTcpServer : IDisposable
{
	public enum Stype
	{
		GCSever = 1,
		ModBusServer
	}

	private const int int_0 = 500;

	private TcpListener tcpListener_0;

	private TcpListener tcpListener_8000;

	private IPEndPoint ipendPoint_0;

	public List<TcpServerSocket> m_tcpServerList;

	private string strLastServerIP = "";

	public Stype ServerType = Stype.GCSever;

	private ChromFormInterface formMain_0;

	private SystemParam sysParam = SystemParam.Create();

	private EventHandler<TcpServerEventArgs> myAcceptSocketHandler;

	private EventHandler<TcpServerEventArgs> myReceiveDataHandler;

	private EventHandler<TcpServerEventArgs> mySendDataHandler;

	private EventHandler<TcpServerEventArgs> myClientDisconnectedHandler;

	public string LastServerIP => strLastServerIP;

	public bool MainFormIsDisposed2 => UIProxy.Instance.MainForm.IsDisposed2;

	public int ClientCount => m_tcpServerList.Count;

	public int ConnectClientCount
	{
		get
		{
			for (int i = 0; i < m_tcpServerList.Count; i++)
			{
				if (m_tcpServerList[i] == null)
				{
					return i;
				}
			}
			return 0;
		}
	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public List<TcpServerSocket> TcpServerSocketList => m_tcpServerList;

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public TcpServerSocket TcpServerSocketList0
	{
		get
		{
			if (ConnectClientCount == 0)
			{
				return null;
			}
			return m_tcpServerList[0];
		}
	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public TcpListener TcpListener => tcpListener_0;

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public Socket TcpListenerSocket => tcpListener_0.Server;

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public IPEndPoint ServerIPEndPoint => ipendPoint_0;

	public event EventHandler<TcpServerEventArgs> OnAcceptSocket
	{
		add
		{
			EventHandler<TcpServerEventArgs> eventHandler = myAcceptSocketHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref myAcceptSocketHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<TcpServerEventArgs> eventHandler = myAcceptSocketHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref myAcceptSocketHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	public event EventHandler<TcpServerEventArgs> OnReceiveData
	{
		add
		{
			EventHandler<TcpServerEventArgs> eventHandler = myReceiveDataHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref myReceiveDataHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<TcpServerEventArgs> eventHandler = myReceiveDataHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref myReceiveDataHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	public event EventHandler<TcpServerEventArgs> OnSendData
	{
		add
		{
			EventHandler<TcpServerEventArgs> eventHandler = mySendDataHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref mySendDataHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<TcpServerEventArgs> eventHandler = mySendDataHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref mySendDataHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	public event EventHandler<TcpServerEventArgs> OnClientDisconnected
	{
		add
		{
			EventHandler<TcpServerEventArgs> eventHandler = myClientDisconnectedHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref myClientDisconnectedHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<TcpServerEventArgs> eventHandler = myClientDisconnectedHandler;
			EventHandler<TcpServerEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<TcpServerEventArgs> value2 = (EventHandler<TcpServerEventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref myClientDisconnectedHandler, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	public AsyncTcpServer(IPEndPoint ipendPoint_1, ChromFormInterface form)
	{
		InitAsyncTcpServer(ipendPoint_1, form);
	}

	public void InitAsyncTcpServer()
	{
		InitAsyncTcpServer(new IPEndPoint(IPAddress.Any, AsyncTcpServerMgr.iMainPort), null);
	}

	public void InitAsyncTcpServer(IPEndPoint ipendPoint_1, ChromFormInterface form)
	{
		ipendPoint_0 = ipendPoint_1;
		LogMgr.Instance.Write2RunLog("New AsyncTcpServer" + ipendPoint_0.ToString());
		tcpListener_0 = new TcpListener(ipendPoint_0);
		try {
			tcpListener_8000 = new TcpListener(new IPEndPoint(IPAddress.Any, 8000));
		} catch {}

		m_tcpServerList = new List<TcpServerSocket>();
		m_tcpServerList.Capacity = 500;
		for (int i = 0; i < 500; i++)
		{
			m_tcpServerList.Add(null);
		}
		if (form != null)
		{
			formMain_0 = form;
		}
	}

	public void AddComClient()
	{
		TcpServerSocket tcpServerSocket = new TcpServerSocket(null, formMain_0);
		tcpServerSocket.IsComClient = true;
		DtC_Channel[] array = new DtC_Channel[4]
		{
			new DtC_Channel(new SysCfgControl()),
			new DtC_Channel(new SysCfgControl()),
			new DtC_Channel(new SysCfgControl()),
			new DtC_Channel(new SysCfgControl())
		};
		array[0].OnGetNewSignals += method_2;
		array[1].OnGetNewSignals += method_2;
		array[2].OnGetNewSignals += method_2;
		array[3].OnGetNewSignals += method_2;
		tcpServerSocket.LogIn(formMain_0, array);
		tcpServerSocket.OpenCom();
		Add2SocketList(tcpServerSocket);
	}

	public int SendDataToAllConnectNode(byte[] ControlPanelCmdDataBuffer)
	{
		for (int i = 0; i < 500; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket != null)
			{
				try
				{
					tcpServerSocket.SendData(ControlPanelCmdDataBuffer);
				}
				catch (Exception ex)
				{
					LogMgr.Instance.Write2RunLog(ex.Message + "服务发送所有数据到客户端异常，请检查。");
				}
			}
		}
		return ControlPanelCmdDataBuffer.Length;
	}

	public bool Start()
	{
		LogMgr.Instance.Write2RunLog($"tcpListener_0.Start");
		tcpListener_0.Start();
		tcpListener_0.BeginAcceptSocket(BeginAsyncReceiveData, tcpListener_0);

		if (tcpListener_8000 != null)
		{
			try {
				tcpListener_8000.Start();
				tcpListener_8000.BeginAcceptSocket(BeginAsyncReceiveData, tcpListener_8000);
				LogMgr.Instance.Write2RunLog($"tcpListener_8000.Start on port 8000 success");
			} catch (Exception ex) {
				LogMgr.Instance.Write2RunLog($"tcpListener_8000.Start failed: {ex.Message}");
			}
		}

		return true;
	}

	public bool Stop()
	{
		bool flag = true;
		LogMgr.Instance.Write2RunLog("tcpListener_0.Stop");
		CloseTcpServerSocketList();
		try
		{
			tcpListener_0.Stop();
			flag = false;
		}
		catch (Exception arg)
		{
			LogMgr.Instance.Write2RunLog(string.Format("停止AsyncTcpServer时发生异常：", arg));
		}

		if (tcpListener_8000 != null)
		{
			try {
				tcpListener_8000.Stop();
			} catch {}
		}

		return !flag;
	}

	public void CloseSocket(TcpServerSocket socket)
	{
		try
		{
			LogMgr.Instance.Write2RunLog("AsyncTcpServer.CloseSocket:" + socket.ClientIP);
			socket.Socket.Shutdown(SocketShutdown.Both);
			socket.Socket.Close();
		}
		catch (SocketException ex)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.CloseSocket:CloseSocket_SocketException:{ex.StackTrace}");
		}
		catch (ObjectDisposedException ex2)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.CloseSocket:CloseSocket_ObjectDisposedException:{ex2.StackTrace}");
		}
		catch (Exception ex3)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.CloseSocket:CloseSocket_Exception:{ex3.StackTrace}");
		}
		if (myClientDisconnectedHandler != null)
		{
			myClientDisconnectedHandler(this, new TcpServerEventArgs(socket));
		}
	}

	private bool ExistTcpServerSocket(string strIP)
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket != null && tcpServerSocket.ClientIP == strIP)
			{
				return true;
			}
		}
		return false;
	}

	private void BeginAsyncReceiveData(IAsyncResult iasyncResult_0)
	{
		TcpListener tcpListener = iasyncResult_0.AsyncState as TcpListener;
		Socket socket = null;
		if (UIProxy.Instance.MainForm.IsDisposed2)
		{
			return;
		}
		bool flag = true;
		try
		{
			socket = tcpListener.EndAcceptSocket(iasyncResult_0);
			flag = false;
		}
		catch (SocketException ex)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:AcceptSocket_SocketException:{ex.StackTrace}");
		}
		catch (ObjectDisposedException ex2)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:AcceptSocket_ObjectDisposedException:{ex2.StackTrace}");
		}
		catch (Exception ex3)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:AcceptSocket_Exception:{ex3.StackTrace}");
		}
		try
		{
			tcpListener.BeginAcceptSocket(BeginAsyncReceiveData, tcpListener);
		}
		catch (Exception ex4)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:接收新套接字时出错:{ex4.StackTrace}");
		}
		if (flag)
		{
			return;
		}
		TcpServerSocket tcpServerSocket;
		if (ExistTcpServerSocket(((IPEndPoint)socket.RemoteEndPoint).Address.ToString()))
		{
			tcpServerSocket = GetOneInstrumByIP(((IPEndPoint)socket.RemoteEndPoint).Address.ToString());
			tcpServerSocket.StartReceiveTime = DateTime.Now;
			tcpServerSocket.refreshSocket(socket);
			strLastServerIP = tcpServerSocket.ClientIP;
		}
		else
		{
			tcpServerSocket = new TcpServerSocket(socket, formMain_0);
			DtC_Channel[] array = new DtC_Channel[4]
			{
				new DtC_Channel(new SysCfgControl()),
				new DtC_Channel(new SysCfgControl()),
				new DtC_Channel(new SysCfgControl()),
				new DtC_Channel(new SysCfgControl())
			};
			array[0].OnGetNewSignals += method_2;
			array[1].OnGetNewSignals += method_2;
			array[2].OnGetNewSignals += method_2;
			array[3].OnGetNewSignals += method_2;
			tcpServerSocket.LogIn(formMain_0, array);
			Add2SocketList(tcpServerSocket);
			strLastServerIP = tcpServerSocket.ClientIP;
		}
		if (myAcceptSocketHandler != null)
		{
			myAcceptSocketHandler(this, new TcpServerEventArgs(tcpServerSocket));
		}
		flag = true;
		try
		{
			socket.BeginReceive(tcpServerSocket.Buffer, 0, tcpServerSocket.Buffer.Length, SocketFlags.None, EndAsyncReceiveData, tcpServerSocket);
			flag = false;
		}
		catch (SocketException ex5)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:AcceptSocket_BeginReceive_SocketException:{ex5.StackTrace}");
		}
		catch (ObjectDisposedException ex6)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:AcceptSocket_BeginReceive_ObjectDisposedException:{ex6.StackTrace}");
		}
		catch (Exception ex7)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:AcceptSocket_BeginReceive_Exception:{ex7.StackTrace}");
		}
		if (flag)
		{
			try
			{
				socket.Close();
				if (myClientDisconnectedHandler != null)
				{
					myClientDisconnectedHandler(this, new TcpServerEventArgs(socket));
				}
			}
			catch (Exception ex8)
			{
				LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_1:关闭客户端连接出错:{ex8.StackTrace}");
			}
		}
		socket = null;
	}

	private void method_2(int int_1, float[] float_0)
	{
	}

	private int GetValidTcpServerCount()
	{
		int num = 0;
		for (int i = 0; i < m_tcpServerList.Count; i++)
		{
			if (m_tcpServerList[i] == null)
			{
				return num;
			}
			num++;
		}
		return num;
	}

	private void EndAsyncReceiveData(IAsyncResult iasyncResult_0)
	{
		TcpServerSocket tcpServerSocket = iasyncResult_0.AsyncState as TcpServerSocket;
		if (UIProxy.Instance.MainForm.IsDisposed2)
		{
			return;
		}
		SocketError errorCode = SocketError.Success;
		int num = 0;
		bool flag = true;
		bool flag2 = false;
		try
		{
			if (tcpServerSocket.Socket.Connected)
			{
				num = tcpServerSocket.Socket.EndReceive(iasyncResult_0, out errorCode);
			}
			flag = false;
		}
		catch (SocketException ex)
		{
			flag2 = true;
			flag = true;
			LogMgr.Instance.LogError($"AsyncTcpServer.method_3:  EndReceive_SocketException:{ex.StackTrace}");
		}
		catch (ObjectDisposedException ex2)
		{
			flag2 = true;
			flag = true;
			LogMgr.Instance.LogError($"AsyncTcpServer.method_3: EndReceive_ObjectDisposedException:{ex2.StackTrace}");
		}
		catch (Exception ex3)
		{
			flag2 = true;
			flag = true;
			LogMgr.Instance.LogError($"AsyncTcpServer.method_3: EndReceive_Exception:{ex3.StackTrace}");
		}
		if (!flag && num != 0)
		{
			bool flag3 = false;
			try
			{
				if (myReceiveDataHandler != null && num > 0)
				{
					if (ServerType == Stype.GCSever)
					{
						tcpServerSocket.OneDataReceive(tcpServerSocket.Buffer, num);
					}
					myReceiveDataHandler(this, new TcpServerEventArgs(tcpServerSocket, num));
				}
				if (tcpServerSocket.Socket.Connected)
				{
					tcpServerSocket.Socket.BeginReceive(tcpServerSocket.Buffer, 0, tcpServerSocket.Buffer.Length, SocketFlags.None, EndAsyncReceiveData, tcpServerSocket);
				}
				flag = false;
			}
			catch (SocketException ex4)
			{
				flag = false;
				flag3 = true;
				LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_3: BeginReceive_SocketException:{ex4.Message + ex4.StackTrace}");
			}
			catch (ObjectDisposedException ex5)
			{
				flag = false;
				flag3 = true;
				LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_3: BeginReceive_ObjectDisposedException:{ex5.Message + ex5.StackTrace}");
			}
			catch (Exception ex6)
			{
				flag = false;
				flag3 = true;
				LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_3: BeginReceive_Exception:{ex6.Message + ex6.StackTrace}");
			}
			if (flag3)
			{
				myReceiveDataHandler(this, new TcpServerEventArgs(tcpServerSocket, num));
				if (tcpServerSocket.Socket.Connected)
				{
					tcpServerSocket.Socket.BeginReceive(tcpServerSocket.Buffer, 0, tcpServerSocket.Buffer.Length, SocketFlags.None, EndAsyncReceiveData, tcpServerSocket);
				}
			}
			if (flag && myClientDisconnectedHandler != null)
			{
				myClientDisconnectedHandler(this, new TcpServerEventArgs(tcpServerSocket));
			}
			return;
		}
		try
		{
			LogMgr.Instance.Write2RunLog("AsyncTcpServer.method_3:Socket.Close");
			LogMgr.Instance.Write2RunLog(tcpServerSocket.DebugLog);
			tcpServerSocket.Socket.Shutdown(SocketShutdown.Both);
			tcpServerSocket.Socket.Close();
			if (ServerType == Stype.GCSever && sysParam.bAllowAutoRestartListenerWhenCloseSocket && GetValidTcpServerCount() <= 1)
			{
				CheckIpAvalibleAndReStartListener();
			}
		}
		catch (SocketException ex7)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_3: ClientDisconnected_SocketException:{ex7.StackTrace}");
		}
		catch (ObjectDisposedException ex8)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_3: ClientDisconnected_ObjectDisposedException:{ex8.StackTrace}");
		}
		catch (Exception ex9)
		{
			LogMgr.Instance.Write2RunLog($"AsyncTcpServer.method_3: ClientDisconnected_Exception:{ex9.StackTrace}");
		}
		if (myClientDisconnectedHandler != null)
		{
			myClientDisconnectedHandler(this, new TcpServerEventArgs(tcpServerSocket));
		}
	}

	public void CheckIpAvalibleAndReStartListener()
	{
		if (strLastServerIP == "")
		{
			return;
		}
		Ping ping = new Ping();
		PingOptions pingOptions = new PingOptions();
		pingOptions.DontFragment = true;
		string s = "test ping";
		byte[] bytes = Encoding.ASCII.GetBytes(s);
		int timeout = 120;
		PingReply pingReply = ping.Send(strLastServerIP, timeout, bytes, pingOptions);
		if (pingReply.Status == IPStatus.Success)
		{
			LogMgr.Instance.Write2RunLog("AsyncTcpServer.CheckIpAvalibleAndReStartListener:丢失链接后，自动重启监听.");
			Stop();
			try
			{
				InitAsyncTcpServer();
			}
			catch
			{
			}
			Start();
		}
	}

	private void CloseTcpServerSocketList()
	{
		for (int i = 0; i < m_tcpServerList.Count; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket == null)
			{
				continue;
			}
			try
			{
				if (tcpServerSocket.Socket.Connected)
				{
					tcpServerSocket.Socket.Shutdown(SocketShutdown.Both);
				}
				tcpServerSocket.Socket.Close();
			}
			catch (Exception ex)
			{
				LogMgr.Instance.Write2RunLog("关闭Socket出错：{0}", ex.Message);
			}
			if (tcpServerSocket.IsComClient)
			{
				tcpServerSocket.CloseCom();
			}
			m_tcpServerList[i] = null;
		}
	}

	public bool CheckHasDoubleConnect(TcpServerSocket NewT)
	{
		int num = -1;
		int num2 = 0;
		while (num2 < 500)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[num2];
			if (tcpServerSocket != null)
			{
				if (tcpServerSocket.Socket != NewT.Socket)
				{
					num2++;
					continue;
				}
				m_tcpServerList[num2] = null;
				num = num2;
			}
			for (int i = 0; i < 500; i++)
			{
				TcpServerSocket tcpServerSocket2 = m_tcpServerList[i];
				if (tcpServerSocket2 != null && !tcpServerSocket2.IsComClient && tcpServerSocket2.ID == NewT.ID && i != num)
				{
					tcpServerSocket2.Socket.Shutdown(SocketShutdown.Both);
					tcpServerSocket2.Socket.Close();
					tcpServerSocket2.refreshSocket(NewT.Socket);
					m_tcpServerList[num] = null;
				}
			}
			return false;
		}
		return true;
	}

	public void CheckConnectList()
	{
		for (int i = 0; i < 500; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket == null || tcpServerSocket.IsComClient || !(DateTime.Now.Subtract(tcpServerSocket.StartReceiveTime).TotalSeconds > 10.0))
			{
				continue;
			}
			try
			{
				if (myClientDisconnectedHandler != null)
				{
					myClientDisconnectedHandler(this, new TcpServerEventArgs(tcpServerSocket));
				}
			}
			catch (Exception ex)
			{
				LogMgr.Instance.Write2RunLog("关闭Socket出错：{0}", ex.Message);
			}
		}
	}

	private void Add2SocketList(TcpServerSocket tcpServerSocket_0)
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket == null || tcpServerSocket.DisConnect)
			{
				m_tcpServerList[i] = tcpServerSocket_0;
				return;
			}
		}
		CloseSocket(tcpServerSocket_0);
	}

	public bool CanClose()
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket == null)
			{
				return true;
			}
			for (int j = 0; j < tcpServerSocket.sglsSampling.Length; j++)
			{
				if (tcpServerSocket.sglsSampling[j].simple)
				{
					return false;
				}
			}
		}
		return true;
	}

	public TcpServerSocket GetOneInstrumByIP(string IP)
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket != null && IP == tcpServerSocket.ClientIP)
			{
				return tcpServerSocket;
			}
		}
		return null;
	}

	public TcpServerSocket GetOneInstrumByMID(int MID)
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			TcpServerSocket tcpServerSocket = m_tcpServerList[i];
			if (tcpServerSocket != null && MID == tcpServerSocket.DID)
			{
				return tcpServerSocket;
			}
		}
		return null;
	}

	public TcpServerSocket GetOneInstrum(string ID)
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			if (m_tcpServerList[i] != null)
			{
				TcpServerSocket tcpServerSocket = m_tcpServerList[i];
				if (tcpServerSocket != null && ID == tcpServerSocket.ID)
				{
					return tcpServerSocket;
				}
			}
		}
		return null;
	}

	public TcpServerSocket GetOneInstrum(int Index)
	{
		for (int i = 0; i < m_tcpServerList.Count - 1; i++)
		{
			TcpServerSocket result = m_tcpServerList[i];
			if (i == Index)
			{
				return result;
			}
		}
		return null;
	}

	private void Dispose(bool bool_0)
	{
		CloseTcpServerSocketList();
		tcpListener_0.Stop();
		if (tcpListener_8000 != null) {
			try { tcpListener_8000.Stop(); } catch {}
		}
		myAcceptSocketHandler = null;
		myReceiveDataHandler = null;
		mySendDataHandler = null;
		myClientDisconnectedHandler = null;
	}

	~AsyncTcpServer()
	{
		Dispose(bool_0: false);
	}

	public void Dispose()
	{
		Dispose(bool_0: true);
		GC.SuppressFinalize(this);
	}
}
