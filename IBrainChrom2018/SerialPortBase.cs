using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace IBrainChrom2018;

public class SerialPortBase
{
	private SerialPort com;

	private Thread _readThread;

	private Thread _writeThread;

	private bool _keepReading;

	private int sendIndex = 0;

	public static int int_10 = 0;

	private byte[] readBuffer = new byte[100];

	public string serialIn;

	public int iCMD = 0;

	public int iState = 0;

	public bool bFire = false;

	public bool bShow = false;

	public bool bHide = true;

	public bool bStart = false;

	public bool bStop = false;

	public bool bSelect = false;

	public bool bStartTemp = false;

	public byte[] Data = new byte[13];

	public byte[] Data2 = new byte[32];

	public byte[] comSend = new byte[32];

	public byte[] Data3 = new byte[32]
	{
		12, 15, 66, 3, 96, 45, 1, 3, 4, 66,
		44, 88, 55, 22, 11, 66, 44, 55, 11, 22,
		33, 11, 77, 88, 78, 98, 96, 6, 9, 36,
		2, 4
	};

	public void openPort()
	{
		Data[0] = 71;
		Data[1] = 67;
		Data[2] = 89;
		Data[3] = 66;
		comSend[0] = 89;
		comSend[1] = 66;
		comSend[2] = 89;
		comSend[3] = 66;
		comSend[6] = 1;
		Data2[6] = 1;
		com = new SerialPort();
		com.BaudRate = 9600;
		com.PortName = "COM2";
		com.DataBits = 8;
		com.Open();
		_keepReading = true;
		_writeThread = new Thread(WritePort);
		_writeThread.IsBackground = true;
		_writeThread.Start();
	}

	public void openPort(string strCom)
	{
		Data[0] = 71;
		Data[1] = 67;
		Data[2] = 89;
		Data[3] = 66;
		comSend[0] = 89;
		comSend[1] = 66;
		comSend[2] = 89;
		comSend[3] = 66;
		comSend[6] = 1;
		Data2[6] = 1;
		com = new SerialPort();
		com.BaudRate = 19200;
		com.PortName = strCom;
		com.DataBits = 8;
		com.Open();
		_keepReading = true;
		_readThread = new Thread(ReadPort);
		_readThread.IsBackground = true;
		_readThread.Start();
	}

	public void openPort(string strCom, int iBaudrate)
	{
		Data[0] = 71;
		Data[1] = 67;
		Data[2] = 89;
		Data[3] = 66;
		comSend[0] = 89;
		comSend[1] = 66;
		comSend[2] = 89;
		comSend[3] = 66;
		comSend[6] = 1;
		Data2[6] = 1;
		com = new SerialPort();
		com.BaudRate = iBaudrate;
		com.PortName = strCom;
		com.DataBits = 8;
		com.Open();
		_keepReading = true;
		_readThread = new Thread(ReadPort);
		_readThread.IsBackground = false;
		_readThread.Start();
	}

	public void closePort()
	{
		com.Close();
	}

	public void sendByte(byte[] bData)
	{
		if (com.IsOpen)
		{
			com.Write(bData, 0, bData.Length);
		}
	}

	public void sendStr(string strCMD)
	{
		if (com != null && com.IsOpen)
		{
			com.Write(strCMD);
		}
	}

	private void ReadPort()
	{
		while (_keepReading)
		{
			if (com.IsOpen)
			{
				try
				{
					if (iCMD == 0)
					{
						if (com.BytesToRead >= 3)
						{
							int count = com.Read(readBuffer, 0, com.BytesToRead);
							serialIn = Encoding.ASCII.GetString(readBuffer, 0, count);
						}
					}
					else if (iCMD == 1)
					{
						if (com.BytesToRead >= 10)
						{
							int count2 = com.Read(readBuffer, 0, com.BytesToRead);
							serialIn = Encoding.ASCII.GetString(readBuffer, 0, count2);
							if (serialIn.Contains("Gas"))
							{
								iState = 1;
							}
						}
					}
					else
					{
						serialIn = com.ReadLine();
					}
				}
				catch (TimeoutException)
				{
				}
			}
			else
			{
				TimeSpan timeout = new TimeSpan(0, 0, 0, 0, 50);
				Thread.Sleep(timeout);
			}
		}
	}

	public void WritePort()
	{
		while (_keepReading)
		{
			if (com.IsOpen)
			{
				try
				{
					Data[Data.Length - 1] = 0;
					for (int i = 4; i < Data.Length - 1; i++)
					{
						Data[Data.Length - 1] += Data[i];
					}
					com.Write(Data, 0, Data.Length);
					Thread.Sleep(1000);
					comSend[Data2.Length - 1] = 0;
					for (int j = 4; j < Data2.Length - 1; j++)
					{
						comSend[j] = (byte)(Data2[j] ^ Data3[j]);
						comSend[comSend.Length - 1] += comSend[j];
					}
					com.Write(comSend, 0, comSend.Length);
					Thread.Sleep(1000);
				}
				catch (TimeoutException)
				{
				}
			}
			else
			{
				TimeSpan timeout = new TimeSpan(0, 0, 0, 0, 50);
				Thread.Sleep(timeout);
			}
		}
	}
}
