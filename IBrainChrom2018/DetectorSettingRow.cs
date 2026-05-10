using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class DetectorSettingRow : IArrayBase
{
	public int iDetector = 0;

	public byte detectorType;

	public byte polarity;

	public byte range;

	public byte frequency;

	public static string GetDeviceTypeNameByIdx(int iChn, int iDetector)
	{
		return iChn switch
		{
			0 => "CH1", 
			1 => "CH2", 
			3 => "CH3", 
			_ => iChn.ToString(), 
		};
	}

	public static string GetDeviceTypeNameByIdx(byte byte_4, int iDetector)
	{
		switch (byte_4)
		{
		case 48:
			return "CH1";
		case 49:
			return "CH2";
		default:
			switch (byte_4)
			{
			case 64:
				return iDetector switch
				{
					0 => "FID1", 
					1 => "FID1", 
					2 => "PDD1", 
					_ => "FID1", 
				};
			case 65:
				return iDetector switch
				{
					0 => "FID2", 
					1 => "PDD2", 
					2 => "PDD2", 
					_ => "FID2", 
				};
			case 66:
				return "FID3";
			case 67:
				return "FID4";
			case 68:
				return "FID5";
			case 69:
				return "FID6";
			case 70:
				return "FID7";
			case 80:
				return "TCD1";
			case 81:
				return "TCD2";
			case 82:
				return "TCD3";
			case 83:
				return "TCD4";
			case 84:
				return "TCD5";
			case 85:
				return "TCD6";
			case 96:
				return "FPD1";
			case 97:
				return "FPD2";
			case 98:
				return "FPD3";
			case 99:
				return "FPD4";
			case 100:
				return "FPD5";
			case 101:
				return "FPD6";
			case 102:
				return "FPD7";
			default:
				switch (byte_4)
				{
				case 112:
					return "ECD1";
				case 113:
					return "ECD2";
				case 114:
					return "ECD3";
				case 115:
					return "ECD4";
				case 116:
					return "ECD5";
				case 117:
					return "ECD6";
				case 118:
					return "ECD7";
				case 128:
					return "NPD1";
				case 129:
					return "NPD2";
				case 130:
					return "NPD3";
				case 131:
					return "NPD4";
				case 132:
					return "NPD5";
				case 133:
					return "NPD6";
				case 134:
					return "NPD7";
				case 135:
					return "NPD8";
				case 136:
					return "AUX";
				case 144:
					return "ZrO";
				case 160:
					return "PDD1";
				case 161:
					return "PDD2";
				}
				break;
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 91:
			case 92:
			case 93:
			case 94:
			case 95:
				break;
			}
			return "0x" + BitConverter.ToString(new byte[1] { byte_4 });
		}
	}

	public static string GetDeviceTypeNameByIdx(byte byte_4)
	{
		return byte_4 switch
		{
			48 => "CH1", 
			49 => "CH2", 
			_ => byte_4 switch
			{
				64 => "FID1", 
				65 => "FID2", 
				66 => "FID3", 
				67 => "FID4", 
				68 => "FID5", 
				69 => "FID6", 
				70 => "FID7", 
				80 => "TCD1", 
				81 => "TCD2", 
				82 => "TCD3", 
				83 => "TCD4", 
				84 => "TCD5", 
				85 => "TCD6", 
				96 => "FPD1", 
				97 => "FPD2", 
				98 => "FPD3", 
				99 => "FPD4", 
				100 => "FPD5", 
				101 => "FPD6", 
				102 => "FPD7", 
				112 => "ECD1", 
				113 => "ECD2", 
				114 => "ECD3", 
				115 => "ECD4", 
				116 => "ECD5", 
				117 => "ECD6", 
				118 => "ECD7", 
				128 => "NPD1", 
				129 => "NPD2", 
				130 => "NPD3", 
				131 => "NPD4", 
				132 => "NPD5", 
				133 => "NPD6", 
				134 => "NPD7", 
				135 => "NPD8", 
				136 => "AUX", 
				_ => "0x" + BitConverter.ToString(new byte[1] { byte_4 }), 
			}, 
		};
	}

	public string GetDeviceTypeName()
	{
		return GetDeviceTypeNameByIdx(detectorType, iDetector);
	}

	public void SetDeviceTypeByName(string string_0)
	{
		if (iDetector == 0)
		{
			if (string_0 == "FID1")
			{
				detectorType = 64;
				return;
			}
			if (string_0 == "FID2")
			{
				detectorType = 65;
				return;
			}
		}
		else if (iDetector == 1)
		{
			if (string_0 == "FID1")
			{
				detectorType = 64;
				return;
			}
			if (string_0 == "PDD2")
			{
				detectorType = 65;
				return;
			}
		}
		else if (iDetector == 2)
		{
			if (string_0 == "PDD1")
			{
				detectorType = 64;
				return;
			}
			if (string_0 == "PDD2")
			{
				detectorType = 65;
				return;
			}
		}
		switch (string_0)
		{
		case "FID3":
			detectorType = 66;
			return;
		case "TCD1":
			detectorType = 80;
			return;
		case "TCD2":
			detectorType = 81;
			return;
		case "TCD3":
			detectorType = 82;
			return;
		case "FPD1":
			detectorType = 96;
			return;
		case "FPD2":
			detectorType = 97;
			return;
		case "FPD3":
			detectorType = 98;
			return;
		case "ECD1":
			detectorType = 112;
			return;
		case "ECD2":
			detectorType = 113;
			return;
		case "ECD3":
			detectorType = 114;
			return;
		case "NPD1":
			detectorType = 128;
			return;
		case "NPD2":
			detectorType = 129;
			return;
		case "NPD3":
			detectorType = 130;
			return;
		case "CH1":
			detectorType = 48;
			return;
		case "CH2":
			detectorType = 49;
			return;
		}
		if (string_0 != "NPD2")
		{
			throw new Exception("Dtcr.Mark");
		}
		detectorType = 129;
	}

	public bool GetPolarity()
	{
		return polarity == 0;
	}

	public void SetPolarity(bool bool_0)
	{
		polarity = ((!bool_0) ? ((byte)1) : ((byte)0));
	}

	public bool GetBaselineDeduction()
	{
		return IBrainConvert.Byte2Bool(frequency, 4);
	}

	public void SetBaselineDeduction(bool bool_0)
	{
		if (bool_0)
		{
			frequency = IBrainConvert.BitByBitOr(frequency, 4);
		}
		else
		{
			frequency = IBrainConvert.ByteReverse(frequency, 4);
		}
	}

	public byte GetFreq()
	{
		return (byte)(frequency & 0xF);
	}

	public void SetFreq(byte byte_4)
	{
		bool baselineDeduction = GetBaselineDeduction();
		frequency = byte_4;
		SetBaselineDeduction(baselineDeduction);
	}

	public bool IsVaildDevice()
	{
		if (!(GetDeviceTypeName() == "TCD1") && !(GetDeviceTypeName() == "TCD2") && !(GetDeviceTypeName() == "TCD3"))
		{
			if (!(GetDeviceTypeName() == "ECD1") && !(GetDeviceTypeName() == "ECD2") && !(GetDeviceTypeName() == "ECD3"))
			{
				if (6 <= range && range <= 10)
				{
					return true;
				}
			}
			else if (range >= 1 && range <= 230)
			{
				return true;
			}
		}
		else if (0 <= range && range <= 230)
		{
			return true;
		}
		if (GetDeviceTypeName() == "FPD1" || GetDeviceTypeName() == "FPD2")
		{
			return true;
		}
		if (GetDeviceTypeName() == "FID1" || GetDeviceTypeName() == "FID2")
		{
			return true;
		}
		if (GetDeviceTypeName() == "PDD1" || GetDeviceTypeName() == "PDD2")
		{
			return true;
		}
		return false;
	}
}
