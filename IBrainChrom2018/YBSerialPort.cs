using System;
using System.IO.Ports;
using System.Threading;

namespace IBrainChrom2018;

internal class YBSerialPort
{
	private SerialPort MyCom;

	private int CurrentAddr;

	public byte[] bData = new byte[1000];

	public string strErrMsg;

	public bool mRtuFlag = true;

	private bool bCommWell;

	public bool comBusying;

	private byte mReceiveByte;

	private int mReceiveByteCount;

	public YBSerialPort()
	{
		MyCom = new SerialPort();
		mRtuFlag = true;
	}

	public void OpenMyCom(string strPortNo, int iBaudRate, int iDataBits, Parity iParity, StopBits iStopBits)
	{
		try
		{
			if (MyCom.IsOpen)
			{
				MyCom.Close();
			}
			MyCom.BaudRate = iBaudRate;
			MyCom.PortName = strPortNo;
			MyCom.DataBits = iDataBits;
			MyCom.Parity = iParity;
			MyCom.StopBits = iStopBits;
			MyCom.ReceivedBytesThreshold = 1;
			MyCom.DataReceived += MyCom_DataReceived;
			MyCom.Open();
			comBusying = false;
			bCommWell = false;
		}
		catch
		{
		}
		finally
		{
		}
	}

	public void CloseMyCom()
	{
		MyCom.Close();
	}

	private void MyCom_DataReceived(object sender, SerialDataReceivedEventArgs e)
	{
		try
		{
			Thread.Sleep(200);
			while (MyCom.BytesToRead > 0)
			{
				mReceiveByte = (byte)MyCom.ReadByte();
				bData[mReceiveByteCount] = mReceiveByte;
				mReceiveByteCount++;
				if (mReceiveByteCount >= 1000)
				{
					mReceiveByteCount = 0;
					MyCom.DiscardInBuffer();
					return;
				}
			}
			mReceiveByteCount = 0;
			MyCom.DiscardInBuffer();
			MyCom.DiscardInBuffer();
			mReceiveByteCount = 0;
		}
		catch (Exception ex)
		{
			strErrMsg = ex.Message.ToString();
		}
	}
}
