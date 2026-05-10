using System;
using System.Text;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ModBusData
{
	public byte[] ModBusBytes = new byte[15000];

	public string PrintFilePath = "";

	public void InitBytes(Chromatogram ModBustestchromatogram)
	{
		ModBusBytes = new byte[9025];
		if (ModBustestchromatogram.chromInfo.cclDescription.Trim() == "")
		{
			ModBustestchromatogram.chromInfo.cclDescription = ModBustestchromatogram.userArchives[0].chromInfo.asChrom;
		}
		byte[] bytes = Encoding.Default.GetBytes(ModBustestchromatogram.chromInfo.cclDescription.Trim());
		bytes.CopyTo(ModBusBytes, 0);
		if (ModBustestchromatogram.RltPeaks != null)
		{
			for (int i = 0; i < ModBustestchromatogram.RltPeaks.Length; i++)
			{
				bytes = Encoding.Default.GetBytes((i + 1).ToString());
				bytes.CopyTo(ModBusBytes, (i + 1) * 100);
				bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[i].pkRT.ToString("0.000"));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 4);
				bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[i].height.ToString("0.000"));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 12);
				bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[i].WO5.ToString("0.000"));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 20);
				bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[i].area.ToString("0.000"));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 25);
				bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[i].name);
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 33);
				bytes = Encoding.Default.GetBytes((ModBustestchromatogram.RltPeaks[i].areaPer * 100f).ToString("F" + Class49.int_8));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 65);
				bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[i].amount.ToString("0.000"));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 72);
				bytes = Encoding.Default.GetBytes((ModBustestchromatogram.RltPeaks[i].amountPer * 100f).ToString("F" + Class49.int_8));
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 80);
				bytes = Encoding.Default.GetBytes("保留保留");
				bytes.CopyTo(ModBusBytes, (i + 1) * 100 + 87);
			}
		}
		bytes = Encoding.Default.GetBytes("YBGC88V3");
		bytes.CopyTo(ModBusBytes, 9000);
		bytes = Encoding.Default.GetBytes("V2020-V2");
		bytes.CopyTo(ModBusBytes, 9016);
		bytes = Encoding.Default.GetBytes("开机正常等待空闲");
		bytes.CopyTo(ModBusBytes, 16);
		bytes = Encoding.Default.GetBytes(ModBustestchromatogram.injAnalysis.dtAcquire.ToString("yyMMddHHmmss"));
		bytes.CopyTo(ModBusBytes, 32);
		bytes = Encoding.Default.GetBytes(ModBustestchromatogram.signal.detectorMark.ToString());
		bytes.CopyTo(ModBusBytes, 44);
		bytes = Encoding.Default.GetBytes(ModBustestchromatogram.injAnalysis.vialNo.ToString());
		bytes.CopyTo(ModBusBytes, 46);
		bytes = Encoding.Default.GetBytes(ModBustestchromatogram.injAnalysis.injNo.ToString());
		bytes.CopyTo(ModBusBytes, 48);
		if (ModBustestchromatogram.RltPeaks != null)
		{
			bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks.Length.ToString());
			bytes.CopyTo(ModBusBytes, 50);
		}
		bytes = Encoding.Default.GetBytes(ModBustestchromatogram.whlHheatVaue.ToString("0.00"));
		bytes.CopyTo(ModBusBytes, 54);
		bytes = Encoding.Default.GetBytes(ModBustestchromatogram.whlLheatVaue.ToString("0.00"));
		bytes.CopyTo(ModBusBytes, 64);
		bytes = Encoding.Default.GetBytes("YBGC88V2");
		bytes.CopyTo(ModBusBytes, 9008);
	}

	public void InitBytesVer1(Chromatogram ModBustestchromatogram)
	{
		try
		{
			ModBusBytes = new byte[10000];
			if (ModBustestchromatogram == null)
			{
				return;
			}
			if (ModBustestchromatogram.chromInfo.cclDescription.Trim() == "")
			{
				ModBustestchromatogram.chromInfo.cclDescription = ModBustestchromatogram.userArchives[0].chromInfo.asChrom;
			}
			byte[] bytes = Encoding.Default.GetBytes(ModBustestchromatogram.chromInfo.cclDescription.Trim());
			bytes.CopyTo(ModBusBytes, 0);
			bytes = Encoding.Default.GetBytes("开机正常等待空闲");
			bytes.CopyTo(ModBusBytes, 16);
			bytes = Encoding.Default.GetBytes(ModBustestchromatogram.injAnalysis.dtAcquire.ToString("yyMMddHHmmss"));
			bytes.CopyTo(ModBusBytes, 32);
			new byte[2]
			{
				0,
				(byte)ModBustestchromatogram.signal.detectorMark
			}.CopyTo(ModBusBytes, 44);
			new byte[2]
			{
				0,
				(byte)ModBustestchromatogram.injAnalysis.injNo
			}.CopyTo(ModBusBytes, 48);
			if (ModBustestchromatogram.RltPeaks != null)
			{
				new byte[2]
				{
					0,
					(byte)ModBustestchromatogram.RltPeaks.Length
				}.CopyTo(ModBusBytes, 50);
			}
			bytes = Class49.Float2Byte(ModBustestchromatogram.whlHheatVaue);
			bytes.CopyTo(ModBusBytes, 52);
			bytes = Class49.Float2Byte(ModBustestchromatogram.whlLheatVaue);
			bytes.CopyTo(ModBusBytes, 56);
			Peak[] peakAllCompound = ModBustestchromatogram.GetPeakAllCompound();
			if (peakAllCompound != null)
			{
				for (int i = 0; i < peakAllCompound.Length; i++)
				{
					for (int j = 0; j < 32; j++)
					{
						ModBusBytes[(i + 2) * 100 + j] = 32;
					}
					Peak peak = new Peak();
					int k;
					for (k = 0; k < ModBustestchromatogram.RltPeaks.Length && !(peakAllCompound[i].name == ModBustestchromatogram.RltPeaks[k].name); k++)
					{
					}
					if (k < ModBustestchromatogram.RltPeaks.Length)
					{
						bytes = Encoding.Default.GetBytes(ModBustestchromatogram.RltPeaks[k].name);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100);
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].pkRT);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 32);
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].height);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 36);
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].area);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 40);
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].areaPer * 100f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 44);
						if (peakAllCompound[i].heightPer <= 0f)
						{
							peakAllCompound[i].heightPer = 0f;
						}
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].heightPer * 100f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 48);
						if (peakAllCompound[i].amount <= 0f)
						{
							peakAllCompound[i].amount = 0f;
						}
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].amount);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 52);
						if (peakAllCompound[i].amountPer <= 0f)
						{
							peakAllCompound[i].amountPer = 0f;
						}
						bytes = Class49.Float2Byte(ModBustestchromatogram.RltPeaks[k].amountPer * 100f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 56);
					}
					else
					{
						bytes = Encoding.Default.GetBytes("");
						bytes.CopyTo(ModBusBytes, (i + 2) * 100);
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 32);
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 36);
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 40);
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 44);
						if (peakAllCompound[i].heightPer <= 0f)
						{
							peakAllCompound[i].heightPer = 0f;
						}
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 48);
						if (peakAllCompound[i].amount <= 0f)
						{
							peakAllCompound[i].amount = 0f;
						}
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 52);
						if (peakAllCompound[i].amountPer <= 0f)
						{
							peakAllCompound[i].amountPer = 0f;
						}
						bytes = Class49.Float2Byte(0f);
						bytes.CopyTo(ModBusBytes, (i + 2) * 100 + 56);
					}
				}
			}
			bool flag = false;
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError($"InitBytesVer1{ex.Message}");
			LogMgr.Instance.LogError($"InitBytesVer1{ex.StackTrace}");
		}
	}

	public byte[] ModBusBigCmdError01(byte[] ReceiveData)
	{
		byte[] array = new byte[9];
		Array.Copy(ReceiveData, 0, array, 0, array.Length);
		array[7] = 131;
		array[8] = 1;
		array[5] = (byte)(array.Length - 6);
		return array;
	}

	public byte[] ModBusAddressError02(byte[] ReceiveData)
	{
		byte[] array = new byte[9];
		Array.Copy(ReceiveData, 0, array, 0, array.Length);
		array[7] = 131;
		array[8] = 2;
		array[5] = (byte)(array.Length - 6);
		return array;
	}

	public byte[] ModBusLengthError03(byte[] ReceiveData)
	{
		byte[] array = new byte[9];
		Array.Copy(ReceiveData, 0, array, 0, array.Length);
		array[7] = 131;
		array[8] = 3;
		array[5] = (byte)(array.Length - 6);
		return array;
	}

	public byte[] ModBusEquipError04(byte[] ReceiveData)
	{
		byte[] array = new byte[9];
		Array.Copy(ReceiveData, 0, array, 0, array.Length);
		array[7] = 131;
		array[8] = 4;
		array[5] = (byte)(array.Length - 6);
		return array;
	}

	public byte[] ModBusEquipError05(byte[] ReceiveData, byte ErrorType)
	{
		Array.Resize(ref ReceiveData, 10);
		ReceiveData[8] = 128;
		ReceiveData[9] = ErrorType;
		ReceiveData[5] = (byte)(ReceiveData.Length - 6);
		return ReceiveData;
	}

	public byte[] ModBusEquipError85(byte[] ReceiveData, byte ErrorType)
	{
		Array.Resize(ref ReceiveData, 10);
		ReceiveData[8] = 133;
		ReceiveData[9] = ErrorType;
		ReceiveData[5] = (byte)(ReceiveData.Length - 6);
		return ReceiveData;
	}

	public byte[] ModBusValue(byte[] ReceiveData, Chromatogram[] ModBustestchromatogram)
	{
		byte[] array = new byte[12];
		Array.Copy(ReceiveData, 0, array, 0, 12);
		int num = array[8] * 256 + array[9];
		int num2 = array[10] * 255 + array[11];
		num2 *= 2;
		byte[] array2 = new byte[0];
		byte b = array[7];
		if (b == 3)
		{
			array = new byte[9 + num2];
			Array.Copy(ReceiveData, 0, array, 0, 8);
			array[5] = (byte)(array.Length - 6);
			array[6] = 100;
			array[8] = (byte)num2;
			if (num * 2 < ModBusBytes.Length)
			{
				Array.Copy(ModBusBytes, num * 2, array, 9, num2);
			}
			return array;
		}
		return ModBusBigCmdError01(ReceiveData);
	}

	public byte[] ModBusValueVer1(byte[] ReceiveData, Chromatogram[] ModBustestchromatogram)
	{
		byte[] array = new byte[12];
		Array.Copy(ReceiveData, 0, array, 0, 12);
		int num = array[8] * 256 + array[9];
		int num2 = array[10] * 255 + array[11];
		num2 *= 2;
		byte b = array[7];
		byte[] result;
		if (b == 3)
		{
			byte[] array2 = new byte[0];
			array2 = new byte[9 + num2];
			Array.Copy(array, 0, array2, 0, 8);
			array2[5] = (byte)(array2.Length - 6);
			array2[8] = (byte)num2;
			if (num < 100)
			{
				Array.Copy(ModBusBytes, num, array2, 9, num2);
			}
			if (num < 10000)
			{
				if (ModBustestchromatogram.Length < 1)
				{
					return ModBusLengthError03(ReceiveData);
				}
				Array.Copy(ModBusBytes, num, array2, 9, num2);
			}
			if (num >= 10000 && num < 20000)
			{
				if (ModBustestchromatogram.Length < 2)
				{
					return ModBusLengthError03(ReceiveData);
				}
				Array.Copy(ModBusBytes, num - 10000, array2, 9, num2);
			}
			if (num >= 20000 && num < 30000)
			{
				if (ModBustestchromatogram.Length < 3)
				{
					return ModBusLengthError03(ReceiveData);
				}
				Array.Copy(ModBusBytes, num - 20000, array2, 9, num2);
			}
			if (num >= 30000 && num < 40000)
			{
				if (ModBustestchromatogram.Length < 4)
				{
					return ModBusLengthError03(ReceiveData);
				}
				Array.Copy(ModBusBytes, num - 30000, array2, 9, num2);
			}
			result = array2;
		}
		else
		{
			result = ModBusBigCmdError01(ReceiveData);
		}
		return result;
	}
}
