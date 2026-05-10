using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class InsDeviceManager
{
	public ShortMsg Msg = new ShortMsg();

	public byte sglNumberStart;

	public byte sglNumberEnd;

	public string string_0 = "";

	public bool insDevEnable0;

	public bool insDevEnable1;

	public bool insDevEnable2;

	public bool insDevEnable3;

	public bool insDevEnable4;

	public bool insDevEnable5;

	public bool insDevEnable6;

	public bool insDevEnable7;

	public TempCtrlAreaTable tempCtrlAreaTable = new TempCtrlAreaTable();

	public byte[] multivalveEnable = new byte[4];

	public float[] tempProtectList = new float[6];

	public float[] tempSetedList = new float[6];

	public float[] tempSetedList2 = new float[2];

	public float[] tempProtectList2 = new float[2];

	public List<DetectorSettingRow> detectorSettingList = IArrayBase.NewArray<DetectorSettingRow>(0);

	public string[] strEpcNameListCn = new string[8];

	public string[] strEpcNameListEn = new string[8];

	public List<EpcDeviceSetting> epcDev0 = IArrayBase.NewArray<EpcDeviceSetting>(0);

	public List<EpcDeviceSetting> epcDev1 = new List<EpcDeviceSetting>(0);

	public List<EpcDeviceSetting> epcDev2 = IArrayBase.NewArray<EpcDeviceSetting>(0);

	public byte epcGasType;

	public byte[] injectNumList = new byte[6];

	public byte exeFileNumber;

	public EventTable eventCtrl0 = new EventTable(4);

	public EventTable eventCtrl1 = new EventTable(4);

	public List<NetWorkDeviceRow> netDevList = IArrayBase.NewArray<NetWorkDeviceRow>(6);

	public byte[] insSerial = new byte[20];

	public MyIPAddress[] ipAddressList = IArrayBase.NewArray2<MyIPAddress>(6);

	public float tempHoldTime;

	public List<TemperSettingRow> tempSettingList = IArrayBase.NewArray<TemperSettingRow>(0);

	public int injectType;

	public PrintPara printPara_0 = new PrintPara();

	public float injectInterval;

	public float injectNTimes;

	public float injectSpendTime;

	public float injectLightTime;

	public InjectorBaseSetting injectSet = new InjectorBaseSetting();

	public string injectConnState = "000";

	public string injectWorkState = "000";

	public string injectBotNum = "0";

	public string injectNeedleNum = "0";

	public InsDeviceManager()
	{
		for (int i = 0; i < strEpcNameListCn.Length; i++)
		{
			strEpcNameListCn[i] = (strEpcNameListEn[i] = "");
		}
		if (printPara_0 == null)
		{
			printPara_0 = new PrintPara();
		}
		printPara_0.Init();
		for (int j = 0; j < netDevList.Count; j++)
		{
			netDevList[j] = new NetWorkDeviceRow();
		}
		for (int k = 0; k < ipAddressList.Length; k++)
		{
			ipAddressList[k] = new MyIPAddress("127.0.0.1");
		}
	}

	public void epcDevReset()
	{
		epcDev0.Clear();
		for (int i = 0; i < 6; i++)
		{
			epcDev0.Add(new EpcDeviceSetting());
		}
		epcDev1.Clear();
		for (int j = 0; j < 18; j++)
		{
			epcDev1.Add(new EpcDeviceSetting());
		}
		epcDev2.Clear();
		for (int k = 0; k < 18; k++)
		{
			epcDev2.Add(new EpcDeviceSetting());
		}
		tempSettingList.Clear();
		for (int l = 0; l < 16; l++)
		{
			tempSettingList.Add(new TemperSettingRow());
		}
	}

	public void SetInjectNumList(byte[] byte_7)
	{
		Array.Resize(ref injectNumList, byte_7[0]);
		if (byte_7.Length != injectNumList.Length + 1)
		{
			throw new Exception("气路配置");
		}
		for (int i = 0; i < injectNumList.Length; i++)
		{
			byte b = (injectNumList[i] = byte_7[1 + i]);
			if (b < 30 || b > 37)
			{
				throw new Exception("气路配置");
			}
		}
	}

	public string method_3(byte[] byte_7)
	{
		Array.Copy(byte_7, insSerial, Math.Min(insSerial.Length, byte_7.Length));
		return Encoding.ASCII.GetString(insSerial);
	}

	public void SetInsSerial(string strInsSerial)
	{
		byte[] bytes = Encoding.ASCII.GetBytes(strInsSerial);
		Array.Clear(insSerial, 0, insSerial.Length);
		Array.Copy(bytes, insSerial, Math.Min(insSerial.Length, bytes.Length));
	}

	public byte[] GetInjectInterval()
	{
		return IBrainConvert.Float2Byte(injectInterval, 1);
	}

	public void SetInjectInterval(byte[] byte_7)
	{
		injectInterval = IBrainConvert.ByteArray2Float(byte_7, 0, 1);
	}

	public byte[] GetCurSglNumberStart()
	{
		return new byte[1] { sglNumberStart };
	}

	public void SetCurSglNumberStart(byte[] byte_7)
	{
		if (byte_7.Length != 1)
		{
			throw new Exception("启动指定通道分析应答");
		}
		sglNumberStart = byte_7[0];
	}

	public byte[] GetCurSglNumberEnd()
	{
		return new byte[1] { sglNumberEnd };
	}

	public void SetCurSglNumberEnd(byte[] byte_7)
	{
		if (byte_7.Length != 1)
		{
			throw new Exception("指定通道分析停止应答");
		}
		sglNumberEnd = byte_7[0];
	}

	public byte[] GetInsDevEnable()
	{
		byte b = 0;
		if (insDevEnable5)
		{
			b = IBrainConvert.BitByBitOr(b, 0);
		}
		if (insDevEnable4)
		{
			b = IBrainConvert.BitByBitOr(b, 1);
		}
		if (insDevEnable3)
		{
			b = IBrainConvert.BitByBitOr(b, 2);
		}
		if (insDevEnable2)
		{
			b = IBrainConvert.BitByBitOr(b, 3);
		}
		if (insDevEnable1)
		{
			b = IBrainConvert.BitByBitOr(b, 4);
		}
		if (insDevEnable0)
		{
			b = IBrainConvert.BitByBitOr(b, 5);
		}
		if (insDevEnable6)
		{
			b = IBrainConvert.BitByBitOr(b, 6);
		}
		if (insDevEnable7)
		{
			b = IBrainConvert.BitByBitOr(b, 7);
		}
		return new byte[1] { b };
	}

	public void SetInsDevEnable(byte[] byte_7)
	{
		if (byte_7.Length != 1)
		{
			throw new Exception("控温使能");
		}
		insDevEnable5 = IBrainConvert.Byte2Bool(byte_7[0], 0);
		insDevEnable4 = IBrainConvert.Byte2Bool(byte_7[0], 1);
		insDevEnable3 = IBrainConvert.Byte2Bool(byte_7[0], 2);
		insDevEnable2 = IBrainConvert.Byte2Bool(byte_7[0], 3);
		insDevEnable1 = IBrainConvert.Byte2Bool(byte_7[0], 4);
		insDevEnable0 = IBrainConvert.Byte2Bool(byte_7[0], 5);
		insDevEnable6 = IBrainConvert.Byte2Bool(byte_7[0], 6);
		insDevEnable7 = IBrainConvert.Byte2Bool(byte_7[0], 7);
	}

	public byte[] GetTempSetedList()
	{
		byte[] byte_ = new byte[0];
		for (int i = 0; i < tempSetedList.Length; i++)
		{
			IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempSetedList[i], 1));
		}
		for (int j = 0; j < tempProtectList.Length; j++)
		{
			IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempProtectList[j], 1));
		}
		return byte_;
	}

	public void SetTempSetedList(byte[] byte_7)
	{
		if (byte_7.Length != 24)
		{
			throw new Exception("CtrlTemp");
		}
		for (int i = 0; i < tempSetedList.Length; i++)
		{
			tempSetedList[i] = IBrainConvert.ByteArray2Float(byte_7, i + i, 1);
		}
		for (int j = 0; j < tempProtectList.Length; j++)
		{
			tempProtectList[j] = IBrainConvert.ByteArray2Float(byte_7, 12 + j + j, 1);
		}
	}

	public byte[] GetTempProtectList()
	{
		byte[] byte_ = new byte[0];
		for (int i = 0; i < tempProtectList2.Length; i++)
		{
			IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempProtectList2[i], 1));
		}
		for (int j = 0; j < tempSetedList2.Length; j++)
		{
			IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempSetedList2[j], 1));
		}
		return byte_;
	}

	public void SetTempProtectList(byte[] byte_7)
	{
		for (int i = 0; i < tempProtectList2.Length; i++)
		{
			tempProtectList2[i] = IBrainConvert.ByteArray2Float(byte_7, i + i, 1);
		}
		for (int j = 0; j < tempSetedList2.Length; j++)
		{
			tempSetedList2[j] = IBrainConvert.ByteArray2Float(byte_7, 4 + j + j, 1);
		}
	}

	public byte[] GetDetectorSettingList()
	{
		byte[] array = new byte[1 + 4 * detectorSettingList.Count];
		array[0] = (byte)detectorSettingList.Count;
		for (int i = 0; i < detectorSettingList.Count; i++)
		{
			array[1 + i * 4] = detectorSettingList[i].detectorType;
			array[1 + i * 4 + 1] = detectorSettingList[i].polarity;
			array[1 + i * 4 + 2] = detectorSettingList[i].range;
			array[1 + i * 4 + 3] = detectorSettingList[i].frequency;
		}
		return array;
	}

	public void SetDetectorSettingList(byte[] byte_7)
	{
		List<DetectorSettingRow> list = IArrayBase.NewArray<DetectorSettingRow>(byte_7[0]);
		for (int i = 0; i < list.Count; i++)
		{
			list[i].detectorType = byte_7[1 + i * 4];
			list[i].polarity = byte_7[1 + i * 4 + 1];
			list[i].range = byte_7[1 + i * 4 + 2];
			list[i].frequency = byte_7[1 + i * 4 + 3];
			if (i == 2 && list[1].detectorType == 190)
			{
				list[1].detectorType = list[2].detectorType;
				list[1].polarity = list[2].polarity;
				list[1].range = list[2].range;
				list[1].frequency = list[2].frequency;
			}
		}
		IArrayBase.NewArray(ref detectorSettingList, list.Count);
		for (int j = 0; j < detectorSettingList.Count; j++)
		{
			detectorSettingList[j] = list[j];
		}
	}

	public byte[] GetEpcNameList()
	{
		byte[] byte_ = new byte[0];
		for (int i = 0; i < strEpcNameListCn.Length; i++)
		{
			byte[] array = Encoding.Default.GetBytes(strEpcNameListCn[i]);
			Array.Resize(ref array, 6);
			IBrainConvert.ArrayCopy(ref byte_, array);
		}
		for (int j = 0; j < strEpcNameListEn.Length; j++)
		{
			byte[] array2 = Encoding.ASCII.GetBytes(strEpcNameListEn[j]);
			Array.Resize(ref array2, 6);
			IBrainConvert.ArrayCopy(ref byte_, array2);
		}
		return byte_;
	}

	public void SetEpcNameList(byte[] byte_7)
	{
		if (byte_7.Length != 96)
		{
			throw new Exception("EPC名称");
		}
		int num = 0;
		for (int i = 0; i < strEpcNameListCn.Length; i++)
		{
			strEpcNameListCn[i] = Encoding.Default.GetString(byte_7, num, 6);
			num += 6;
		}
		for (int j = 0; j < strEpcNameListEn.Length; j++)
		{
			strEpcNameListEn[j] = Encoding.ASCII.GetString(byte_7, num, 6);
			num += 6;
		}
	}

	public byte[] GetEPCDevParam(int idx, byte model)
	{
		byte[] byte_ = new byte[1] { model };
		IBrainConvert.ArrayAdd(ref byte_, (byte)(epcDev0[idx].gasType & 0xF));
		IBrainConvert.ArrayAdd(ref byte_, epcDev0[idx].ctrlModel);
		IBrainConvert.ArrayAdd(ref byte_, IBrainConvert.Float2Byte(epcDev0[idx].chromColLenth));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte2(epcDev0[idx].chromColDiameter, 3));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(epcDev0[idx].pressureData, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(epcDev0[idx].initTime, 1));
		for (int i = 0; i < epcDev0[idx].tempSettingTable.Count; i++)
		{
			IBrainConvert.ArrayCopy(ref byte_, epcDev0[idx].tempSettingTable[i].GetByte());
		}
		return byte_;
	}

	public byte[] GetEPCDevParam1(int idx)
	{
		if (epcDev1[idx] == null)
		{
			epcDev1[idx] = new EpcDeviceSetting();
		}
		byte[] byte_ = new byte[1] { epcDev1[idx].splitRatio };
		IBrainConvert.ArrayAdd(ref byte_, (byte)(epcDev1[idx].gasType & 0xF));
		IBrainConvert.ArrayAdd(ref byte_, epcDev1[idx].ctrlModel);
		IBrainConvert.ArrayAdd(ref byte_, IBrainConvert.Float2Byte(epcDev1[idx].chromColLenth));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte2(epcDev1[idx].chromColDiameter, 3));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(epcDev1[idx].pressureData, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(epcDev1[idx].initTime, 1));
		for (int i = 0; i < epcDev1[idx].tempSettingTable.Count; i++)
		{
			IBrainConvert.ArrayCopy(ref byte_, epcDev1[idx].tempSettingTable[i].GetByte());
		}
		return byte_;
	}

	public EpcDeviceSetting GetEPCDevParam(int idx, byte[] byte_7)
	{
		if (byte_7.Length == 35)
		{
			if (epcDev0[idx] == null)
			{
				epcDev0[idx] = new EpcDeviceSetting();
			}
			epcDev0[idx].splitRatio = byte_7[0];
			epcDev0[idx].gasType = byte_7[1];
			epcDev0[idx].ctrlModel = byte_7[2];
			epcDev0[idx].chromColLenth = IBrainConvert.Byte2Float(byte_7[3]);
			epcDev0[idx].chromColDiameter = IBrainConvert.Byte3ToFloat(byte_7, 4);
			epcDev0[idx].pressureData = IBrainConvert.ByteArray2Float(byte_7, 7, 1);
			epcDev0[idx].initTime = IBrainConvert.ByteArray2Float(byte_7, 9, 1);
			for (int i = 0; i < epcDev0[idx].tempSettingTable.Count; i++)
			{
				epcDev0[idx].tempSettingTable[i].ReadByte(IBrainConvert.ArrayCopy(byte_7, 9 + i * 6 + 2, 6));
			}
		}
		if (byte_7.Length == 33)
		{
			if (epcDev0[idx] == null)
			{
				epcDev0[idx] = new EpcDeviceSetting();
			}
			epcDev0[idx].splitRatio = byte_7[0];
			epcDev0[idx].gasType = byte_7[1];
			epcDev0[idx].ctrlModel = byte_7[2];
			epcDev0[idx].pressureData = IBrainConvert.ByteArray2Float(byte_7, 5, 1);
			epcDev0[idx].initTime = IBrainConvert.ByteArray2Float(byte_7, 7, 1);
			for (int j = 0; j < epcDev0[idx].tempSettingTable.Count; j++)
			{
				epcDev0[idx].tempSettingTable[j].ReadByte(IBrainConvert.ArrayCopy(byte_7, 9 + j * 6, 6));
			}
		}
		else if (byte_7.Length == 9)
		{
			if (epcDev0[idx] == null)
			{
				epcDev0[idx] = new EpcDeviceSetting();
			}
			epcDev0[idx].splitRatio = byte_7[0];
			epcDev0[idx].gasType = byte_7[1];
			epcDev0[idx].ctrlModel = byte_7[2];
			epcDev0[idx].chromColLenth = IBrainConvert.Byte2Float(byte_7[3]);
			epcDev0[idx].chromColDiameter = IBrainConvert.Byte3ToFloat(byte_7, 4);
			epcDev0[idx].pressureData = IBrainConvert.ByteArray2Float(byte_7, 7, 1);
		}
		return epcDev0[idx];
	}

	public EpcDeviceSetting GetEPCDevParam1(int idx, byte[] byte_7)
	{
		try
		{
			if (byte_7.Length == 35)
			{
				if (epcDev1[idx] == null)
				{
					epcDev1[idx] = new EpcDeviceSetting();
				}
				epcDev1[idx].splitRatio = byte_7[0];
				epcDev1[idx].gasType = byte_7[1];
				epcDev1[idx].ctrlModel = byte_7[2];
				epcDev1[idx].chromColLenth = IBrainConvert.Byte2Float(byte_7[3]);
				epcDev1[idx].chromColDiameter = IBrainConvert.Byte3ToFloat(byte_7, 4);
				epcDev1[idx].pressureData = IBrainConvert.ByteArray2Float(byte_7, 7, 1);
				epcDev1[idx].initTime = IBrainConvert.ByteArray2Float(byte_7, 9, 1);
				if (idx % 3 != 0)
				{
					Math.Floor((double)(idx / 3));
				}
				for (int i = 0; i < epcDev1[idx].tempSettingTable.Count; i++)
				{
					epcDev1[idx].tempSettingTable[i].ReadByte(IBrainConvert.ArrayCopy(byte_7, 9 + i * 6 + 2, 6));
				}
			}
			if (byte_7.Length == 33)
			{
				if (epcDev1[idx] == null)
				{
					epcDev1[idx] = new EpcDeviceSetting();
				}
				epcDev1[idx].splitRatio = byte_7[0];
				epcDev1[idx].gasType = byte_7[1];
				epcDev1[idx].ctrlModel = byte_7[2];
				epcDev1[idx].pressureData = IBrainConvert.ByteArray2Float(byte_7, 5, 1);
				epcDev1[idx].initTime = IBrainConvert.ByteArray2Float(byte_7, 7, 1);
				for (int j = 0; j < epcDev1[idx].tempSettingTable.Count; j++)
				{
					epcDev1[idx].tempSettingTable[j].ReadByte(IBrainConvert.ArrayCopy(byte_7, 9 + j * 6, 6));
				}
			}
			else if (byte_7.Length == 9)
			{
				if (epcDev1[idx] == null)
				{
					epcDev1[idx] = new EpcDeviceSetting();
				}
				epcDev1[idx].splitRatio = byte_7[0];
				epcDev1[idx].gasType = byte_7[1];
				epcDev1[idx].ctrlModel = byte_7[2];
				epcDev1[idx].chromColLenth = IBrainConvert.Byte2Float(byte_7[3]);
				epcDev1[idx].chromColDiameter = IBrainConvert.Byte3ToFloat(byte_7, 4);
				epcDev1[idx].pressureData = IBrainConvert.ByteArray2Float(byte_7, 7, 1);
			}
			return epcDev1[idx];
		}
		catch (Exception ex)
		{
			Console.WriteLine("EPC参数返回处理错误。" + ex.StackTrace);
		}
		return null;
	}

	public string method_25()
	{
		string text = injectNumList.Length.ToString();
		if (injectNumList.Length != 0)
		{
			text += ": ";
		}
		for (int i = 0; i < injectNumList.Length; i++)
		{
			text = text + injectNumList[i] + ((i != injectNumList.Length - 1) ? ", " : "");
		}
		return text;
	}

	public byte[] GetExeFileNumber()
	{
		return new byte[1] { exeFileNumber };
	}

	public void SetExeFileNumber(byte[] byte_7)
	{
		exeFileNumber = byte_7[0];
	}

	public byte[] GetEventTable0()
	{
		byte[] byte_ = new byte[0];
		int length = eventCtrl0.GetLength(0);
		int length2 = eventCtrl0.GetLength(1);
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < length2; j++)
			{
				IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.ToBcd_3B_new(eventCtrl0[i, j], 2));
			}
		}
		return byte_;
	}

	public void SetEventTable0(byte[] byte_7)
	{
		if (byte_7.Length != 96)
		{
			throw new Exception("外部事件时间程序");
		}
		int length = eventCtrl0.GetLength(0);
		int length2 = eventCtrl0.GetLength(1);
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < length2; j++)
			{
				eventCtrl0[i, j] = IBrainConvert.FromBcd_3B(byte_7, i * (length2 * 3) + j * 3, 2);
			}
		}
	}

	public byte[] GetEventTable1()
	{
		byte[] byte_ = new byte[0];
		int length = eventCtrl1.GetLength(0);
		int length2 = eventCtrl1.GetLength(1);
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < length2; j++)
			{
				IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.ToBcd_3B_new(eventCtrl1[i, j], 2));
			}
		}
		return byte_;
	}

	public void SetEventTable1(byte[] byte_7)
	{
		if (byte_7.Length != 96)
		{
			throw new Exception("外部事件时间程序");
		}
		int length = eventCtrl1.GetLength(0);
		int length2 = eventCtrl1.GetLength(1);
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < length2; j++)
			{
				eventCtrl1[i][j] = IBrainConvert.FromBcd_3B(byte_7, i * (length2 * 3) + j * 3, 2);
			}
		}
	}

	public void SetNetDevList(byte[] byte_7)
	{
		IArrayBase.NewArray(ref netDevList, byte_7[0]);
		for (int i = 0; i < netDevList.Count; i++)
		{
			netDevList[i].SetNetDeivceVersion(byte_7, 1 + i * 16);
		}
	}

	public byte[] GetIpAddressList()
	{
		byte[] byte_ = new byte[0];
		for (int i = 0; i < ipAddressList.Length; i++)
		{
			string[] array = ipAddressList[i].ToString().Split('.');
			IBrainConvert.ArrayAdd(ref byte_, byte.Parse(array[0]));
			IBrainConvert.ArrayAdd(ref byte_, byte.Parse(array[1]));
			IBrainConvert.ArrayAdd(ref byte_, byte.Parse(array[2]));
			IBrainConvert.ArrayAdd(ref byte_, byte.Parse(array[3]));
		}
		return byte_;
	}

	public void SetIpAddressList(byte[] byte_7)
	{
		if (byte_7.Length % 4 != 0)
		{
			throw new Exception("网络参数");
		}
		IArrayBase.NewArray2(ref ipAddressList, byte_7.Length / 4);
		for (int i = 0; i < ipAddressList.Length; i++)
		{
			int num = i * 4;
			string strip = byte_7[num].ToString() + '.' + byte_7[num + 1].ToString() + '.' + byte_7[num + 2].ToString() + '.' + byte_7[num + 3].ToString();
			ipAddressList[i] = new MyIPAddress(strip);
		}
	}

	public byte[] GetTempSettingList()
	{
		byte[] byte_ = new byte[0];
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempHoldTime, 1));
		for (int i = 0; i < tempSettingList.Count; i++)
		{
			IBrainConvert.ArrayCopy(ref byte_, tempSettingList[i].GetByte());
		}
		return byte_;
	}

	public void SetTempSettingList(byte[] byte_7)
	{
		if (byte_7.Length == 50)
		{
			byte[] array = new byte[98];
			Array.Copy(byte_7, array, byte_7.Length);
			byte_7 = array;
		}
		if (byte_7.Length != 98)
		{
			throw new Exception("ProgTemp");
		}
		tempHoldTime = IBrainConvert.ByteArray2Float(byte_7, 0, 1);
		for (int i = 0; i < tempSettingList.Count; i++)
		{
			tempSettingList[i].ReadByte(IBrainConvert.ArrayCopy(byte_7, 2 + i * 6, 6));
		}
	}

	internal void SaveToFile(BinaryWriter binaryWriter_0, string string_7)
	{
		for (int i = 0; i < tempSetedList.Length; i++)
		{
			binaryWriter_0.Write(tempSetedList[i]);
		}
		binaryWriter_0.Write(tempHoldTime);
		binaryWriter_0.Write(insDevEnable0);
		binaryWriter_0.Write(insDevEnable1);
		binaryWriter_0.Write(insDevEnable2);
		binaryWriter_0.Write(insDevEnable3);
		for (int j = 0; j < tempSettingList.Count; j++)
		{
			binaryWriter_0.Write(tempSettingList[j].tempStart);
			binaryWriter_0.Write(tempSettingList[j].tempEnd);
			binaryWriter_0.Write(tempSettingList[j].tempKeep);
		}
		for (int k = 0; k < 4; k++)
		{
			for (int l = 0; l < 8; l++)
			{
				binaryWriter_0.Write(eventCtrl0[k][l]);
			}
		}
		if (string_7 == "VER2.0")
		{
			for (int m = 0; m < 4; m++)
			{
				for (int n = 0; n < 8; n++)
				{
					binaryWriter_0.Write(eventCtrl1[m][n]);
				}
			}
		}
		for (int num = 0; num < epcDev1.Count; num++)
		{
			EpcDeviceSetting epcDeviceSetting = epcDev1[num];
			if (epcDeviceSetting == null)
			{
				epcDeviceSetting = new EpcDeviceSetting();
			}
			binaryWriter_0.Write(epcDeviceSetting.gasType);
			binaryWriter_0.Write(epcDeviceSetting.pressureData);
			binaryWriter_0.Write(epcDeviceSetting.chromColDiameter);
			binaryWriter_0.Write(epcDeviceSetting.initTime);
			binaryWriter_0.Write(epcDeviceSetting.ctrlModel);
			for (int num2 = 0; num2 < epcDeviceSetting.tempSettingTable.Count; num2++)
			{
				binaryWriter_0.Write(epcDeviceSetting.tempSettingTable[num2].tempStart);
				binaryWriter_0.Write(epcDeviceSetting.tempSettingTable[num2].tempEnd);
				binaryWriter_0.Write(epcDeviceSetting.tempSettingTable[num2].tempKeep);
			}
		}
		if (string_7 == "VER2.0")
		{
			binaryWriter_0.Write(multivalveEnable[0]);
			binaryWriter_0.Write(multivalveEnable[1]);
			binaryWriter_0.Write(multivalveEnable[2]);
			binaryWriter_0.Write(multivalveEnable[3]);
		}
		printPara_0.WriteToFile(binaryWriter_0);
	}

	internal void ReadFromFile(BinaryReader binaryReader_0, string string_7)
	{
		for (int i = 0; i < tempSetedList.Length; i++)
		{
			tempSetedList[i] = binaryReader_0.ReadSingle();
		}
		tempHoldTime = binaryReader_0.ReadSingle();
		insDevEnable0 = binaryReader_0.ReadBoolean();
		insDevEnable1 = binaryReader_0.ReadBoolean();
		insDevEnable2 = binaryReader_0.ReadBoolean();
		insDevEnable3 = binaryReader_0.ReadBoolean();
		for (int j = 0; j < tempSettingList.Count; j++)
		{
			tempSettingList[j].tempStart = binaryReader_0.ReadSingle();
			tempSettingList[j].tempEnd = binaryReader_0.ReadSingle();
			tempSettingList[j].tempKeep = binaryReader_0.ReadSingle();
		}
		for (int k = 0; k < 4; k++)
		{
			for (int l = 0; l < 8; l++)
			{
				eventCtrl0[k][l] = binaryReader_0.ReadSingle();
			}
		}
		if (string_7 == "VER2.0")
		{
			for (int m = 0; m < 4; m++)
			{
				for (int n = 0; n < 8; n++)
				{
					eventCtrl1[m][n] = binaryReader_0.ReadSingle();
				}
			}
		}
		for (int num = 0; num < epcDev1.Count; num++)
		{
			if (epcDev1[num] == null)
			{
				epcDev1[num] = new EpcDeviceSetting();
			}
			epcDev1[num].gasType = binaryReader_0.ReadByte();
			epcDev1[num].pressureData = binaryReader_0.ReadSingle();
			epcDev1[num].chromColDiameter = binaryReader_0.ReadSingle();
			epcDev1[num].initTime = binaryReader_0.ReadSingle();
			epcDev1[num].ctrlModel = binaryReader_0.ReadByte();
			for (int num2 = 0; num2 < epcDev1[num].tempSettingTable.Count; num2++)
			{
				epcDev1[num].tempSettingTable[num2].tempStart = binaryReader_0.ReadSingle();
				epcDev1[num].tempSettingTable[num2].tempEnd = binaryReader_0.ReadSingle();
				epcDev1[num].tempSettingTable[num2].tempKeep = binaryReader_0.ReadSingle();
			}
		}
		if (string_7 == "VER2.0")
		{
			multivalveEnable[0] = binaryReader_0.ReadByte();
			multivalveEnable[1] = binaryReader_0.ReadByte();
			multivalveEnable[2] = binaryReader_0.ReadByte();
			multivalveEnable[3] = binaryReader_0.ReadByte();
		}
		try
		{
			if (printPara_0 == null)
			{
				printPara_0 = new PrintPara();
			}
			printPara_0.LoadFromBr(binaryReader_0);
		}
		catch
		{
		}
	}
}
