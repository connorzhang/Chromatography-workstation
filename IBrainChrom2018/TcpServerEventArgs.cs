using System;
using System.Net.Sockets;

namespace IBrainChrom2018;

public class TcpServerEventArgs : EventArgs
{
	private TcpServerSocket tcpServerSocket_0;

	private int int_0;

	public TcpServerSocket ServerSocket => tcpServerSocket_0;

	public int DataSize
	{
		get
		{
			return int_0;
		}
		set
		{
			if (value >= 0)
			{
				int_0 = value;
			}
		}
	}

	public TcpServerEventArgs(TcpServerSocket serverSocket, int datasize)
	{
		if (serverSocket == null)
		{
			throw new Exception("TcpServerEventArgs_Constructor_Error:TcpServerSocket实例不能为空");
		}
		if (datasize < 0)
		{
			throw new Exception("TcpServerEventArgs_Constructor_Error:数据大小不能小于0");
		}
		tcpServerSocket_0 = serverSocket;
		int_0 = datasize;
	}

	public TcpServerEventArgs(TcpServerSocket serverSocket)
		: this(serverSocket, 0)
	{
	}

	public TcpServerEventArgs(Socket socket, int datasize)
		: this(new TcpServerSocket(socket, null), datasize)
	{
	}

	public TcpServerEventArgs(Socket socket)
		: this(socket, 0)
	{
	}
}
