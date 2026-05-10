using System;
using System.IO.Ports;
using System.Threading;
using System.Timers;

namespace IBrainChrom2018;

public class ModbusSlave
{
	private SerialPort MyCom;

	private int CurrentAddr;

	private byte[] bData = new byte[2500];

	public string strErrMsg;

	public bool bReciS = false;

	public bool bReciS16 = false;

	private string mTempStr;

	public bool mRtuFlag = true;

	private byte mReceiveByte;

	private int mReceiveByteCount;

	public long iMWordStartAddr;

	public long iMBitStartAddr;

	public int iMWordLen;

	public int iMBitLen;

	public ushort[,] MWordVaue = new ushort[16, 256];

	public bool[,] MBitVaue = new bool[16, 256];

	private bool bCommWell;

	public bool[] bCommFlag = new bool[16];

	public bool comBusying;

	private byte ucCRCHi = byte.MaxValue;

	private byte ucCRCLo = byte.MaxValue;

	public ushort[] WordVaue = new ushort[10000];

	public string strUpData;

	public string strDownData;

	public ushort DevAdd = 100;

	private ushort Length = 1;

	private ushort Address = 0;

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

	public ModbusSlave()
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
		catch (Exception)
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
			mReceiveByteCount = 0;
			MyCom.DiscardInBuffer();
			if (bData[0] == DevAdd)
			{
				switch (bData[1])
				{
				case 3:
				{
					strUpData = "";
					for (int j = 0; j < 8; j++)
					{
						strUpData = strUpData + " " + bData[j].ToString("X2");
					}
					Address = Convert.ToUInt16(bData[2] * 256 + bData[3]);
					Length = Convert.ToUInt16(bData[4] * 256 + bData[5]);
					Thread.Sleep(30);
					SendData(DevAdd, Address, Length);
					bData[0] = 0;
					bData[1] = 0;
					break;
				}
				case 6:
					Address = Convert.ToUInt16(bData[2] * 256 + bData[3]);
					if (Address < 10000)
					{
						WordVaue[Address] = Convert.ToUInt16(bData[4] * 256 + bData[5]);
					}
					MyCom.Write(bData, 0, 8);
					if (Address == 8)
					{
						bReciS = true;
					}
					break;
				case 16:
				{
					Address = Convert.ToUInt16(bData[2] * 256 + bData[3]);
					Length = Convert.ToUInt16(bData[4] * 256 + bData[5]);
					for (int i = 0; i < Length; i++)
					{
						WordVaue[Address + i] = Convert.ToUInt16(bData[7 + i * 2] * 256 + bData[8 + i * 2]);
					}
					MyCom.Write(bData, 0, 19);
					break;
				}
				}
			}
			MyCom.DiscardInBuffer();
			bData[0] = 0;
			bData[1] = 0;
			mReceiveByteCount = 0;
		}
		catch (Exception ex)
		{
			strErrMsg = ex.Message.ToString();
		}
	}

	private void SendData(ushort iDevAdd, ushort iAddress, ushort iLength)
	{
		ushort num = 0;
		byte[] array = new byte[2 * iLength + 5];
		array[0] = (byte)iDevAdd;
		array[1] = 3;
		array[2] = (byte)(2 * iLength);
		for (int i = 0; i < iLength; i++)
		{
			num = WordVaue[iAddress + i];
			array[3 + i * 2] = (byte)((num - num % 256) / 256);
			array[4 + i * 2] = (byte)(num % 256);
		}
		Crc16(array, 3 + iLength * 2);
		array[iLength * 2 + 3] = ucCRCLo;
		array[iLength * 2 + 4] = ucCRCHi;
		MyCom.Write(array, 0, 2 * iLength + 5);
		strDownData = "";
		for (int j = 0; j < 2 * iLength + 5; j++)
		{
			strDownData = strDownData + " " + array[j].ToString("X2");
		}
	}

	public void sendFData(byte[] SendData, ushort iLength)
	{
		MyCom.Write(SendData, 0, iLength);
	}

	public void MODH_Send06H(ushort _addr, ushort _reg, ushort _value)
	{
		byte[] array = new byte[8]
		{
			(byte)_addr,
			6,
			(byte)(_reg >> 8),
			(byte)_reg,
			(byte)(_value >> 8),
			(byte)_value,
			0,
			0
		};
		Crc16(array, 6);
		array[6] = ucCRCLo;
		array[7] = ucCRCHi;
		MyCom.Write(array, 0, 8);
	}

	public void MODH_Send16H(ushort _addr, ushort _reg, ushort _value, ushort _value2)
	{
		byte[] array = new byte[13]
		{
			(byte)_addr,
			16,
			(byte)(_reg >> 8),
			(byte)_reg,
			0,
			2,
			4,
			(byte)(_value >> 8),
			(byte)_value,
			(byte)(_value2 >> 8),
			(byte)_value2,
			0,
			0
		};
		Crc16(array, 11);
		array[11] = ucCRCLo;
		array[12] = ucCRCHi;
		MyCom.Write(array, 0, 13);
	}

	public void MODH_Send16H(ushort _addr, ushort _reg, ushort[] _value)
	{
		byte[] array = new byte[25]
		{
			(byte)_addr,
			16,
			(byte)(_reg >> 8),
			(byte)_reg,
			0,
			8,
			16,
			(byte)(_value[0] >> 8),
			(byte)_value[0],
			(byte)(_value[1] >> 8),
			(byte)_value[1],
			(byte)(_value[2] >> 8),
			(byte)_value[2],
			(byte)(_value[3] >> 8),
			(byte)_value[3],
			(byte)(_value[4] >> 8),
			(byte)_value[4],
			(byte)(_value[5] >> 8),
			(byte)_value[5],
			(byte)(_value[6] >> 8),
			(byte)_value[6],
			(byte)(_value[7] >> 8),
			(byte)_value[7],
			0,
			0
		};
		Crc16(array, 11);
		array[23] = ucCRCLo;
		array[24] = ucCRCHi;
		MyCom.Write(array, 0, 25);
	}
}
