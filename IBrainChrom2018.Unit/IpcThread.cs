using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018.Unit;

public class IpcThread
{
	private static IpcThread myself = null;

	private SystemParam sysParam = SystemParam.Create();

	public static IpcThread CreateIPC()
	{
		if (myself == null)
		{
			myself = new IpcThread();
		}
		return myself;
	}

	private IpcThread()
	{
	}

	public void StartIPC()
	{
		Thread thread = new Thread(IPC);
		thread.Name = "IPCThread";
		thread.IsBackground = true;
		thread.Start();
	}

	private void IPC()
	{
		Thread.Sleep(6000);
		byte[] bytes = Encoding.ASCII.GetBytes("finished0");
		byte[] bytes2 = Encoding.ASCII.GetBytes("finished1");
		TcpListener tcpListener = null;
		try
		{
			int port = 13011;
			IPAddress localaddr = IPAddress.Parse("127.0.0.1");
			tcpListener = new TcpListener(localaddr, port);
			tcpListener.Start();
			byte[] array = new byte[256];
			string text = null;
			while (true)
			{
				TcpClient tcpClient = tcpListener.AcceptTcpClient();
				text = null;
				NetworkStream stream = tcpClient.GetStream();
				try
				{
					int count;
					while ((count = stream.Read(array, 0, array.Length)) != 0)
					{
						text = Encoding.ASCII.GetString(array, 0, count);
						if (UIProxy.Instance.MainForm == null)
						{
							Thread.Sleep(2000);
						}
						if (UIProxy.Instance.MainForm != null && UIProxy.Instance.MainForm.IsDisposed2)
						{
							return;
						}
						if (!(text == "IsAilive"))
						{
							continue;
						}
						string strpos = "IsAilive:";
						if (UIProxy.Instance.MainForm != null)
						{
							UIProxy.Instance.MainForm.Invoke((MethodInvoker)delegate
							{
								strpos = "IsAilive:Yes";
							});
						}
						byte[] bytes3 = Encoding.ASCII.GetBytes(strpos);
						stream.Flush();
						stream.Write(bytes3, 0, bytes3.Length);
					}
				}
				catch (IOException arg)
				{
					Console.WriteLine("IOException: {0}", arg);
				}
				tcpClient.Close();
			}
		}
		catch (SocketException arg2)
		{
			Console.WriteLine("SocketException: {0}", arg2);
		}
		finally
		{
			tcpListener.Stop();
		}
	}

	private void KillProcess()
	{
		try
		{
			Process[] processesByName = Process.GetProcessesByName("KasuSmartVisionLaser");
			Process[] array = processesByName;
			foreach (Process process in array)
			{
				process.Kill();
			}
		}
		catch (Exception innerException)
		{
			throw new Exception("Error Kill", innerException);
		}
	}
}
