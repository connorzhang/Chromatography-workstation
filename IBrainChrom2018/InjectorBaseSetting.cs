using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class InjectorBaseSetting
{
	public int solClearTimeBeforInject;

	public int sampClearTimeBeforInject;

	public int solClearTimeAfterInject;

	public int pumpTime;

	public float visDelyTime;

	public float delyStartTime;

	public int int_4;

	public float float_2;

	public float float_3;

	public int injectMethod;

	public int injectSpeed;

	public int styletSpeed;

	public int iIvol;

	public int iSINT;

	public int iFSAM;

	public int iTANL;

	public int iREPT;

	public byte[] byte_0 = new byte[8];

	public List<InjectorQuantityRow> injectorQRow = IArrayBase.NewArray<InjectorQuantityRow>(4);

	public bool ReadFromByte(byte[] byte_1)
	{
		int num = 0;
		if (byte_1.Length == 96)
		{
			solClearTimeBeforInject = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			sampClearTimeBeforInject = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			solClearTimeAfterInject = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			pumpTime = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			visDelyTime = IBrainConvert.ByteArray2Float(byte_1, num, 1);
			num += 2;
			delyStartTime = IBrainConvert.ByteArray2Float(byte_1, num, 1);
			num += 2;
			int_4 = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			float_2 = IBrainConvert.ByteArray2Float(byte_1, num, 1);
			num += 2;
			float_3 = IBrainConvert.ByteArray2Float(byte_1, num, 1);
			num += 2;
			injectMethod = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			injectSpeed = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			styletSpeed = IBrainConvert.Byte2ToInt(byte_1, num);
			num += 2;
			Array.Copy(byte_1, num, byte_0, 0, 8);
			num += 8;
			for (int i = 0; i < injectorQRow.Count; i++)
			{
				if (injectorQRow[i] == null)
				{
					injectorQRow[i] = new InjectorQuantityRow();
				}
				injectorQRow[i].startBotNo = IBrainConvert.Byte2ToInt(byte_1, num);
				num += 2;
				injectorQRow[i].endBotNo = IBrainConvert.Byte2ToInt(byte_1, num);
				num += 2;
				injectorQRow[i].fQuantity = IBrainConvert.ByteArray2Float(byte_1, num, 1);
				num += 2;
				injectorQRow[i].iTime = IBrainConvert.Byte2ToInt(byte_1, num);
				num += 2;
				injectorQRow[i].iInterval = IBrainConvert.Byte2ToInt(byte_1, num);
				num += 2;
				Array.Copy(byte_1, num, injectorQRow[i].byte_0, 0, 6);
				num += 6;
			}
		}
		return true;
	}

	public byte[] GetByte()
	{
		byte[] byte_ = new byte[0];
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(int_4));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(solClearTimeBeforInject));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(sampClearTimeBeforInject));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(solClearTimeAfterInject));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(pumpTime));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(visDelyTime, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(float_3, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(delyStartTime, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(injectMethod));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(injectSpeed));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(styletSpeed));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(iIvol));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(iSINT));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(iFSAM));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(iTANL));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Int2Byte3(iREPT));
		return byte_;
	}
}
