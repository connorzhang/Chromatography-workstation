using System;
using System.Net.Sockets;

namespace IBrainChrom2018;

public class ChannelTCPClientState
{
	public TcpClient TcpClient { get; private set; }

	public byte[] Buffer { get; private set; }

	public NetworkStream NetworkStream => TcpClient.GetStream();

	public ChannelTCPClientState(TcpClient tcpClient, byte[] buffer)
	{
		if (tcpClient == null)
		{
			throw new ArgumentNullException("tcpClient");
		}
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		TcpClient = tcpClient;
		Buffer = buffer;
	}

	public void Close()
	{
		TcpClient.Close();
		Buffer = null;
	}
}
