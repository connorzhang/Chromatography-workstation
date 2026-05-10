using System;
using System.IO.Ports;
using System.Threading;
using System.Timers;

namespace IBrainChrom2018;

public class MyModbus
{
	public SerialPort MyCom;

	private int CurrentAddr;

	private byte[] bData = new byte[1024];

	public string strErrMsg;

	public string strUpData;

	public string strDownData;

	private string mTempStr;

	public bool mRtuFlag = true;

	private byte mReceiveByte;

	private int mReceiveByteCount;

	public long iMWordStartAddr;

	public long iMBitStartAddr;

	public int iMWordLen;

	public int iMBitLen;

	public short[,] MWordValue = new short[16, 256];

	public bool[,] IBitValue = new bool[16, 256];

	public bool[,] QBitValue = new bool[16, 256];

	private bool bCommWell;

	public bool[] bCommFlag = new bool[16];

	public bool comBusying;

	private byte ucCRCHi = byte.MaxValue;

	private byte ucCRCLo = byte.MaxValue;

	private System.Timers.Timer tmrTimeOut = new System.Timers.Timer(500.0);

	private static readonly byte[] aucCRCHi = new byte[256]
	{
		0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
		128, 65, 0, 193, 129, 64, 1, 192, 128, 65,
		0, 193, 129, 64, 0, 193, 129, 64, 1, 192,
		128, 65, 1, 192, 128, 65, 0, 193, 129, 64,
		0, 193, 129, 64, 1, 192, 128, 65, 0, 193,
		129, 64, 1, 192, 128, 65, 1, 192, 128, 65,
		0, 193, 129, 64, 1, 192, 128, 65, 0, 193,
		129, 64, 0, 193, 129, 64, 1, 192, 128, 65,
		0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
		128, 65, 0, 193, 129, 64, 0, 193, 129, 64,
		1, 192, 128, 65, 1, 192, 128, 65, 0, 193,
		129, 64, 1, 192, 128, 65, 0, 193, 129, 64,
		0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
		128, 65, 0, 193, 129, 64, 0, 193, 129, 64,
		1, 192, 128, 65, 0, 193, 129, 64, 1, 192,
		128, 65, 1, 192, 128, 65, 0, 193, 129, 64,
		0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
		128, 65, 0, 193, 129, 64, 1, 192, 128, 65,
		0, 193, 129, 64, 0, 193, 129, 64, 1, 192,
		128, 65, 0, 193, 129, 64, 1, 192, 128, 65,
		1, 192, 128, 65, 0, 193, 129, 64, 1, 192,
		128, 65, 0, 193, 129, 64, 0, 193, 129, 64,
		1, 192, 128, 65, 1, 192, 128, 65, 0, 193,
		129, 64, 0, 193, 129, 64, 1, 192, 128, 65,
		0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
		128, 65, 0, 193, 129, 64
	};

	private static readonly byte[] aucCRCLo = new byte[256]
	{
		0, 192, 193, 1, 195, 3, 2, 194, 198, 6,
		7, 199, 5, 197, 196, 4, 204, 12, 13, 205,
		15, 207, 206, 14, 10, 202, 203, 11, 201, 9,
		8, 200, 216, 24, 25, 217, 27, 219, 218, 26,
		30, 222, 223, 31, 221, 29, 28, 220, 20, 212,
		213, 21, 215, 23, 22, 214, 210, 18, 19, 211,
		17, 209, 208, 16, 240, 48, 49, 241, 51, 243,
		242, 50, 54, 246, 247, 55, 245, 53, 52, 244,
		60, 252, 253, 61, 255, 63, 62, 254, 250, 58,
		59, 251, 57, 249, 248, 56, 40, 232, 233, 41,
		235, 43, 42, 234, 238, 46, 47, 239, 45, 237,
		236, 44, 228, 36, 37, 229, 39, 231, 230, 38,
		34, 226, 227, 35, 225, 33, 32, 224, 160, 96,
		97, 161, 99, 163, 162, 98, 102, 166, 167, 103,
		165, 101, 100, 164, 108, 172, 173, 109, 175, 111,
		110, 174, 170, 106, 107, 171, 105, 169, 168, 104,
		120, 184, 185, 121, 187, 123, 122, 186, 190, 126,
		127, 191, 125, 189, 188, 124, 180, 116, 117, 181,
		119, 183, 182, 118, 114, 178, 179, 115, 177, 113,
		112, 176, 80, 144, 145, 81, 147, 83, 82, 146,
		150, 86, 87, 151, 85, 149, 148, 84, 156, 92,
		93, 157, 95, 159, 158, 94, 90, 154, 155, 91,
		153, 89, 88, 152, 136, 72, 73, 137, 75, 139,
		138, 74, 78, 142, 143, 79, 141, 77, 76, 140,
		68, 132, 133, 69, 135, 71, 70, 134, 130, 66,
		67, 131, 65, 129, 128, 64
	};

	public MyModbus()
	{
		MyCom = new SerialPort();
		mRtuFlag = true;
	}

	private string LRC(string strLRC)
	{
		int num = 0;
		string text = "";
		int length = strLRC.Length;
		for (int i = 0; i < length; i += 2)
		{
			string value = strLRC.Substring(i, 2);
			num += Convert.ToByte(value, 16);
		}
		if (num >= 255)
		{
			num %= 256;
		}
		text = Convert.ToInt32(~num + 1).ToString("X");
		if (text.Length > 2)
		{
			text = text.Substring(text.Length - 2, 2);
		}
		return text;
	}

	private void Crc16(byte[] pucFrame, int usLen)
	{
		int num = 0;
		ucCRCHi = byte.MaxValue;
		ucCRCLo = byte.MaxValue;
		ushort num2 = 0;
		while (usLen-- > 0)
		{
			num2 = (ushort)(ucCRCLo ^ pucFrame[num++]);
			ucCRCLo = (byte)(ucCRCHi ^ aucCRCHi[num2]);
			ucCRCHi = aucCRCLo[num2];
		}
	}

	public void OpenMyCom(string strPortNo, int iBaudRate, int iDataBits, Parity iParity, StopBits iStopBits)
	{
		try
		{
			for (int i = 0; i <= 15; i++)
			{
				bCommFlag[i] = false;
			}
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
			tmrTimeOut.Elapsed += commTimeOut;
			tmrTimeOut.AutoReset = true;
			tmrTimeOut.Enabled = false;
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
			while (MyCom.BytesToRead > 0)
			{
				mReceiveByte = (byte)MyCom.ReadByte();
				bData[mReceiveByteCount] = mReceiveByte;
				mReceiveByteCount++;
				if (mReceiveByteCount >= 1024)
				{
					mReceiveByteCount = 0;
					MyCom.DiscardInBuffer();
					return;
				}
			}
			if (!mRtuFlag)
			{
				return;
			}
			if (mReceiveByteCount >= iMWordLen * 2 + 5 && bData[0] == CurrentAddr + 1 && bData[1] == 4)
			{
				mTempStr = "";
				for (int i = 0; i < iMWordLen * 2 + 5; i++)
				{
					mTempStr = mTempStr + " " + bData[i].ToString("X2");
				}
				int num = 0;
				for (int i = 0; i < iMWordLen; i++)
				{
					MWordValue[CurrentAddr, num] = (short)(bData[3 + 2 * i] * 256 + bData[3 + 2 * i + 1]);
					num++;
				}
				strUpData = mTempStr;
				MyCom.DiscardInBuffer();
				bCommFlag[CurrentAddr] = false;
				comBusying = false;
				bCommWell = true;
			}
			if (mReceiveByteCount >= iMWordLen * 2 + 5 && bData[0] == CurrentAddr + 1 && bData[1] == 3)
			{
				mTempStr = "";
				for (int i = 0; i < iMWordLen * 2 + 5; i++)
				{
					mTempStr = mTempStr + " " + bData[i].ToString("X2");
				}
				int num2 = 0;
				for (int i = 0; i < iMWordLen; i++)
				{
					MWordValue[CurrentAddr, num2] = (short)(bData[3 + 2 * i] * 256 + bData[3 + 2 * i + 1]);
					num2++;
				}
				strUpData = mTempStr;
				MyCom.DiscardInBuffer();
				bCommFlag[CurrentAddr] = false;
				comBusying = false;
				bCommWell = true;
			}
			if (mReceiveByteCount >= iMBitLen + 5 && bData[0] == CurrentAddr + 1 && bData[1] == 1)
			{
				mTempStr = "";
				for (int i = 0; i < iMBitLen + 5; i++)
				{
					mTempStr = mTempStr + " " + bData[i].ToString("X2");
				}
				for (int i = 0; i < bData[2]; i++)
				{
					ByteToBArray_DO(bData[3 + i], CurrentAddr, i * 8);
				}
				strUpData = mTempStr;
				MyCom.DiscardInBuffer();
				bCommFlag[CurrentAddr] = false;
				comBusying = false;
				bCommWell = true;
			}
			if (mReceiveByteCount >= iMBitLen + 5 && bData[0] == CurrentAddr + 1 && bData[1] == 2)
			{
				mTempStr = "";
				for (int i = 0; i < iMBitLen + 5; i++)
				{
					mTempStr = mTempStr + " " + bData[i].ToString("X2");
				}
				for (int i = 0; i < bData[2]; i++)
				{
					ByteToBArray_DI(bData[3 + i], CurrentAddr, i * 8);
				}
				strUpData = mTempStr;
				MyCom.DiscardInBuffer();
				bCommFlag[CurrentAddr] = false;
				comBusying = false;
				bCommWell = true;
			}
		}
		catch (Exception ex)
		{
			strErrMsg = ex.Message.ToString();
		}
	}

	public void ReadKeepReg(int iDevAdd, long iAddress, int iLength)
	{
		iMWordStartAddr = iAddress - 1;
		iMWordLen = iLength;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 3;
		array[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array[3] = (byte)(iMWordStartAddr % 256);
		array[4] = (byte)((iLength - iLength % 256) / 256);
		array[5] = (byte)(iLength % 256);
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void ReadInputReg(int iDevAdd, long iAddress, int iLength)
	{
		iMWordStartAddr = iAddress - 1;
		iMWordLen = iLength;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 4;
		array[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array[3] = (byte)(iMWordStartAddr % 256);
		array[4] = (byte)((iLength - iLength % 256) / 256);
		array[5] = (byte)(iLength % 256);
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void ReadOutputStatus(int iDevAdd, long iAddress, int iLength)
	{
		if (iLength % 8 == 0)
		{
			iMBitLen = iLength / 8;
		}
		else
		{
			iMBitLen = iLength / 8 + 1;
		}
		iMBitStartAddr = iAddress - 1;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 1;
		array[2] = (byte)((iMBitStartAddr - iMBitStartAddr % 256) / 256);
		array[3] = (byte)(iMBitStartAddr % 256);
		array[4] = (byte)((iLength - iLength % 256) / 256);
		array[5] = (byte)(iLength % 256);
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void ReadInputStatus(int iDevAdd, long iAddress, int iLength)
	{
		if (iLength % 8 == 0)
		{
			iMBitLen = iLength / 8;
		}
		else
		{
			iMBitLen = iLength / 8 + 1;
		}
		iMBitStartAddr = iAddress - 1;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 2;
		array[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array[3] = (byte)(iMWordStartAddr % 256);
		array[4] = (byte)((iLength - iLength % 256) / 256);
		array[5] = (byte)(iLength % 256);
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void ForceOn(int iDevAdd, long iAddress)
	{
		iMWordStartAddr = iAddress - 1;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 5;
		array[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array[3] = (byte)(iMWordStartAddr % 256);
		array[4] = byte.MaxValue;
		array[5] = 0;
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void ForceOff(int iDevAdd, long iAddress)
	{
		iMWordStartAddr = iAddress - 1;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 5;
		array[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array[3] = (byte)(iMWordStartAddr % 256);
		array[4] = 0;
		array[5] = 0;
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void PreSetOutput(int iDevAdd, long iAddress, ushort SetValue)
	{
		iMWordStartAddr = iAddress;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[11];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 15;
		array[2] = 0;
		array[3] = 0;
		array[4] = 0;
		array[5] = 4;
		array[6] = 1;
		array[7] = (byte)(SetValue % 256);
		Crc16(array, 8);
		array[8] = ucCRCLo;
		array[9] = ucCRCHi;
		MyCom.Write(array, 0, 10);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 11; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void PreSetKeepReg(int iDevAdd, long iAddress, ushort SetValue)
	{
		iMWordStartAddr = iAddress - 1;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array = new byte[8];
		CurrentAddr = iDevAdd - 1;
		array[0] = (byte)iDevAdd;
		array[1] = 6;
		array[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array[3] = (byte)(iMWordStartAddr % 256);
		array[4] = (byte)((SetValue - SetValue % 256) / 256);
		array[5] = (byte)(SetValue % 256);
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 8; i++)
		{
			strDownData = strDownData + " " + array[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public void PreSetFloatKeepReg(int iDevAdd, long iAddress, float SetValue)
	{
		byte[] array = new byte[4];
		array = BitConverter.GetBytes(SetValue);
		iMWordStartAddr = iAddress - 1;
		if (comBusying)
		{
			Thread.Sleep(250);
		}
		byte[] array2 = new byte[13];
		CurrentAddr = iDevAdd - 1;
		array2[0] = (byte)iDevAdd;
		array2[1] = 16;
		array2[2] = (byte)((iMWordStartAddr - iMWordStartAddr % 256) / 256);
		array2[3] = (byte)(iMWordStartAddr % 256);
		array2[4] = 0;
		array2[5] = 2;
		array2[6] = 4;
		array2[7] = array[1];
		array2[8] = array[0];
		array2[9] = array[3];
		array2[10] = array[2];
		Crc16(array2, 11);
		array2[11] = ucCRCLo;
		array2[12] = ucCRCHi;
		MyCom.Write(array2, 0, 13);
		mReceiveByteCount = 0;
		strDownData = "";
		for (int i = 0; i < 13; i++)
		{
			strDownData = strDownData + " " + array2[i].ToString("X2");
		}
		tmrTimeOut.Enabled = true;
		comBusying = true;
		bCommWell = false;
	}

	public string ByteToBinary(byte bValue)
	{
		string text = Convert.ToString(bValue, 2);
		if (text.Length < 8)
		{
			int length = text.Length;
			for (int i = 0; i < 8 - length; i++)
			{
				text = "0" + text;
			}
		}
		return text;
	}

	public void ByteToBArray_DO(byte bValue, int iAddr, int pos)
	{
		string text = ByteToBinary(bValue);
		int i;
		for (i = 0; i < 8; i++)
		{
			if (text.Substring(7 - i, 1) == "1")
			{
				QBitValue[iAddr, pos + i] = true;
			}
			else
			{
				QBitValue[iAddr, pos + i] = false;
			}
		}
		i = 10;
	}

	public void ByteToBArray_DI(byte bValue, int iAddr, int pos)
	{
		string text = ByteToBinary(bValue);
		int i;
		for (i = 0; i < 8; i++)
		{
			if (text.Substring(7 - i, 1) == "1")
			{
				IBitValue[iAddr, pos + i] = true;
			}
			else
			{
				IBitValue[iAddr, pos + i] = false;
			}
		}
		i = 10;
	}

	public void commTimeOut(object source, ElapsedEventArgs e)
	{
		if (!bCommWell)
		{
			bCommFlag[CurrentAddr] = true;
			strUpData = "";
		}
		tmrTimeOut.Enabled = false;
		comBusying = false;
	}
}
