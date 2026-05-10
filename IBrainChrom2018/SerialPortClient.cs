using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SerialPortClient
{
	public delegate void DataRecivedFromCom(byte[] Data);

	private SerialPort serialPort_0;

	private Thread thread_0;

	private bool bool_0;

	private DataRecivedFromCom dataRecivedFromCom_0;

	public event DataRecivedFromCom OnDataRecivedFromCom
	{
		add
		{
			DataRecivedFromCom dataRecivedFromCom = dataRecivedFromCom_0;
			DataRecivedFromCom dataRecivedFromCom2;
			do
			{
				dataRecivedFromCom2 = dataRecivedFromCom;
				DataRecivedFromCom value2 = (DataRecivedFromCom)Delegate.Combine(dataRecivedFromCom2, value);
				dataRecivedFromCom = Interlocked.CompareExchange(ref dataRecivedFromCom_0, value2, dataRecivedFromCom2);
			}
			while (dataRecivedFromCom != dataRecivedFromCom2);
		}
		remove
		{
			DataRecivedFromCom dataRecivedFromCom = dataRecivedFromCom_0;
			DataRecivedFromCom dataRecivedFromCom2;
			do
			{
				dataRecivedFromCom2 = dataRecivedFromCom;
				DataRecivedFromCom value2 = (DataRecivedFromCom)Delegate.Remove(dataRecivedFromCom2, value);
				dataRecivedFromCom = Interlocked.CompareExchange(ref dataRecivedFromCom_0, value2, dataRecivedFromCom2);
			}
			while (dataRecivedFromCom != dataRecivedFromCom2);
		}
	}

	public void Close()
	{
		thread_0.Abort();
		if (serialPort_0.IsOpen)
		{
			serialPort_0.Close();
		}
	}

	public void Open(string PortNum)
	{
		if (PortNum.Trim() != "")
		{
			try
			{
				serialPort_0 = new SerialPort();
				serialPort_0.BaudRate = 38400;
				serialPort_0.PortName = PortNum.Trim();
				serialPort_0.DataBits = 8;
				serialPort_0.Open();
				bool_0 = true;
				thread_0 = new Thread(method_0);
				thread_0.Start();
			}
			catch
			{
				MessageBox.Show("串口打开失败，请检查！");
			}
		}
	}

	public void SendData(byte[] Data)
	{
		serialPort_0.Write(Data, 0, Data.Length);
	}

	private void method_0()
	{
		while (bool_0)
		{
			if (serialPort_0.IsOpen)
			{
				byte[] array = new byte[serialPort_0.ReadBufferSize + 1];
				try
				{
					int num = serialPort_0.Read(array, 0, serialPort_0.ReadBufferSize);
					byte[] array2 = new byte[num];
					Array.Copy(array, 0, array2, 0, num);
					if (dataRecivedFromCom_0 != null)
					{
						dataRecivedFromCom_0(array2);
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
}
