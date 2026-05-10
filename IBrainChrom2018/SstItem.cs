using System;
using System.IO;

namespace IBrainChrom2018;

public class SstItem
{
	public string item = "";

	public float lowerLimit = float.NaN;

	public float mean = float.NaN;

	public SSTResult result;

	public bool rltLower;

	public bool rltRsdPer;

	public bool rltUpper;

	public float rsdPer = float.NaN;

	public float rsdPerLimit = float.NaN;

	public float upperLimit = float.NaN;

	public float[] values = new float[0];

	public void AddValue(Chromatogram[] chroms, string cmpd, SSTCriterion criterion)
	{
		for (int i = 0; i < chroms.Length; i++)
		{
			for (int j = 0; j < chroms[i].RltPeaks.Length; j++)
			{
				Peak peak = chroms[i].RltPeaks[j];
				if (!(peak.name != cmpd))
				{
					float value = getValue(peak, item, criterion);
					if (value >= 0f)
					{
						int num = values.Length;
						Array.Resize(ref values, num + 1);
						values[num] = value;
					}
				}
			}
		}
	}

	public void Calcu()
	{
		if (values.Length == 0)
		{
			return;
		}
		double num = 0.0;
		for (int i = 0; i < values.Length; i++)
		{
			num += (double)values[i];
		}
		mean = Convert.ToSingle(num / (double)values.Length);
		if (values.Length > 1)
		{
			num = 0.0;
			for (int j = 0; j < values.Length; j++)
			{
				num += Math.Pow(values[j] - mean, 2.0);
			}
			double num2 = Math.Sqrt(num / (double)(values.Length - 1));
			rsdPer = 100f * Convert.ToSingle(num2 / (double)mean);
		}
		rltUpper = false;
		if (float.IsNaN(upperLimit))
		{
			rltUpper = true;
		}
		else
		{
			rltUpper = mean <= upperLimit;
		}
		rltLower = false;
		if (float.IsNaN(lowerLimit))
		{
			rltLower = true;
		}
		else
		{
			rltLower = mean >= lowerLimit;
		}
		rltRsdPer = false;
		if (!float.IsNaN(rsdPerLimit) && values.Length != 1)
		{
			rltRsdPer = rsdPer <= rsdPerLimit;
		}
		else
		{
			rltRsdPer = true;
		}
		if (rltUpper && rltLower && rltRsdPer)
		{
			result = SSTResult.Success;
		}
		else
		{
			result = SSTResult.Fail;
		}
	}

	public void ClearValues()
	{
		Array.Resize(ref values, 0);
		mean = (rsdPer = float.NaN);
		result = SSTResult.None;
	}

	public SSTResult extResult(float value)
	{
		if (float.IsNaN(value))
		{
			return SSTResult.None;
		}
		if ((!float.IsNaN(lowerLimit) && value < lowerLimit) || (!float.IsNaN(upperLimit) && value > upperLimit))
		{
			return SSTResult.Fail;
		}
		return SSTResult.Success;
	}

	public static float getValue(Peak peak, string item, SSTCriterion criterion)
	{
		switch (item)
		{
		case "RetenTime":
			return peak.pkRT;
		case "Area":
			return peak.area;
		case "Height":
			return peak.height;
		case "Amount":
			return peak.amount;
		case "AmountPer":
			return peak.amountPer;
		case "WO5":
			return peak.WO5;
		case "Asymmetry":
			return peak.Asymmetry;
		case "SymTail":
			return peak.SymmetryTailing;
		case "Capacity":
			return peak.Capacity;
		case "Efficiency":
			return criterion switch
			{
				SSTCriterion.EP => peak.Efficiency_EP, 
				SSTCriterion.USP => peak.Efficiency_USP, 
				_ => peak.Efficiency_JP, 
			};
		case "Eff_ColL":
			return criterion switch
			{
				SSTCriterion.EP => peak.Eff_Column_EP, 
				SSTCriterion.USP => peak.Eff_Column_USP, 
				_ => peak.Eff_Column_JP, 
			};
		case "HETP":
			return criterion switch
			{
				SSTCriterion.EP => peak.HETP_EP, 
				SSTCriterion.USP => peak.HETP_USP, 
				_ => peak.HETP_JP, 
			};
		case "Resolution":
			if (criterion != SSTCriterion.EP)
			{
				return peak.Resolution_USP;
			}
			return peak.Resolution_EP;
		default:
			return float.NaN;
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		item = binaryReader_0.ReadString();
		lowerLimit = binaryReader_0.ReadSingle();
		upperLimit = binaryReader_0.ReadSingle();
		rsdPerLimit = binaryReader_0.ReadSingle();
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(item);
		binaryWriter_0.Write(lowerLimit);
		binaryWriter_0.Write(upperLimit);
		binaryWriter_0.Write(rsdPerLimit);
	}
}
