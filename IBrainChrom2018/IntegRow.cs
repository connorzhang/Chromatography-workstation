using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct IntegRow
{
	public IntegOprtStyle oprtStyle;

	public char group;

	public float timeA;

	public float timeB;

	public float value;

	public float value2;

	public float value3;

	public float value4;

	public bool success;

	public string ValueUnitStr => LclIntegGridView.ValueUnit(oprtStyle);

	public bool Equals(IntegRow integRow)
	{
		bool result;
		if ((result = oprtStyle == integRow.oprtStyle && group == integRow.group && timeA == integRow.timeA && timeB == integRow.timeB && value == integRow.value) && oprtStyle == IntegOprtStyle.BsTgnt)
		{
			return value2 == integRow.value2 && value3 == integRow.value3 && value4 == integRow.value4;
		}
		return result;
	}

	public void ArrTime()
	{
		if (timeA > timeB)
		{
			float num = timeA;
			timeA = timeB;
			timeB = num;
		}
	}

	public string ExpString(int state)
	{
		string text = "";
		switch (oprtStyle)
		{
		case IntegOprtStyle.DtecDelay:
			text = Lang.PS("信号延迟,平衡多采集卡下不同信号间的时间差", "Signal delay, equilibrium under different signal acquisition card for the time difference between the ");
			break;
		case IntegOprtStyle.PeakWidth:
			text = Lang.PS("判峰基础<峰宽>参数,影响谷点定位及自动合并", "Set the narrowest peak for Global Peak Width");
			break;
		case IntegOprtStyle.Threshold:
			text = Lang.PS("判峰基础<峰高>参数,影响谷点定位及自动合并", "Set the Global Threshold");
			break;
		case IntegOprtStyle.VtVSlope:
			text = Lang.PS("全局的判定峰重叠的'谷点-谷点斜率'边界值,小于等于该值时前面的峰判作单峰,否则与后续峰进行谷点垂切整合", "Determination of peak overlapping global 'Valley valley slope' boundary value, less than or equal to the value in front of the peaks are unimodal, otherwise valley point vertical and subsequent peak cutting integration ");
			break;
		case IntegOprtStyle.ResetDtecNeg:
			text = Lang.PS("对给定的区域按检测负峰模式重新判峰,虚拟基线按始终点信号连线", "Set the detective negtive intervl,");
			break;
		case IntegOprtStyle.ClampNeg:
			text = Lang.PS("将负峰翻转成正峰", "The negative peak turning into Zhengfeng");
			break;
		case IntegOprtStyle.PkWidth:
			text = Lang.PS("最小峰宽参数,小于此峰宽值的峰不被标识", "The minimum peak width parameters, less than the peak width values of the peak is not identified");
			break;
		case IntegOprtStyle.PkThreshold:
			text = Lang.PS("最小峰高参数,小于此峰高值的峰不被标识", "The minimum peak parameters, less than the peak value of peak is not identified ");
			break;
		case IntegOprtStyle.PkAddPosi:
			text = Lang.PS("添加一个正峰,虚拟基线按始终点信号连线", "Add a positive peak, virtual baseline to end signal line");
			break;
		case IntegOprtStyle.PkAddNeg:
			text = Lang.PS("添加一个负峰,虚拟基线按始终点信号连线", "Add a negative peak, virtual baseline to end signal line");
			break;
		case IntegOprtStyle.PkCut:
			text = Lang.PS("删除基本始终点在选定范围内的峰", "Delete the basic always point in the selected range peak ");
			break;
		case IntegOprtStyle.PkHalfWidth:
			text = Lang.PS("最小半峰宽参数,小于此半峰宽值的峰不被标识", "Minimum half peak width parameters, less than the half peak width values of the peak is not identified");
			break;
		case IntegOprtStyle.PkArea:
			text = Lang.PS("最小峰面积参数,小于此面积值的峰不被标识", "Parameter is less than this minimum peak area, peak area values were not identified");
			break;
		case IntegOprtStyle.PkVale:
			if (state != 1)
			{
				return Lang.PS("调整谷点横坐标 [设置该谷点新位置]", "Set the new position of the selected valley");
			}
			return Lang.PS("调整谷点横坐标 [选择要改变的谷点]", "Select the valley would be changed [the nearest choose]");
		case IntegOprtStyle.SolventPeak:
			text = Lang.PS("垂直切割", "Set the solvent peak ");
			break;
		case IntegOprtStyle.FlowMarker:
			text = Lang.PS("设置流速标识", "Set flow identification");
			break;
		case IntegOprtStyle.GroupAdd:
			text = Lang.PS("添加组", "Add Group");
			break;
		case IntegOprtStyle.GroupDelete:
			text = Lang.PS("删除组", "Delete Group");
			break;
		case IntegOprtStyle.BsTgnt:
			text = Lang.PS("肩切参数", "Shoulder cutting parameters");
			break;
		case IntegOprtStyle.BsVtV:
			text = Lang.PS("判定峰是否重叠的'谷点-谷点斜率'边界值,小于等于该值时前面的峰判作单峰,否则与后续峰(组)进行谷点垂切整合", "To determine whether overlapping peaks' Valley valley slope 'boundary value, less than or equal to the value in front of the peaks are unimodal, otherwise it and subsequent peak (Group) of valley point vertical cutting integration");
			break;
		case IntegOprtStyle.BsValley:
			text = Lang.PS("调整峰(组)为单峰", "Adjust peak (Group) is the single peak");
			break;
		case IntegOprtStyle.BsTogether:
			text = Lang.PS("调整峰(组)为重叠峰", "Adjust peak (Group) for overlapping peaks");
			break;
		case IntegOprtStyle.BsForwHorz:
			text = Lang.PS("调整峰(组)基线前向水平", "Adjust peak (Group) to the level before baseline");
			break;
		case IntegOprtStyle.BsBackHorz:
			text = Lang.PS("调整峰(组)基线后向水平", "Adjust peak (Group) to the baseline level after");
			break;
		case IntegOprtStyle.BsFrontTgnt:
			text = Lang.PS("设置选定的峰(组)为后峰的'前肩切峰'", "Sets the selected peak to peak (Group) after the front shoulder cut peak");
			break;
		case IntegOprtStyle.BsTailTgnt:
			text = Lang.PS("设置选定的峰(组)为前峰的'后肩切峰", "After the shoulder set selected peak(Group) before the peak of the guillotine");
			break;
		case IntegOprtStyle.Noise:
			text = Lang.PS("噪音评估", "Noise Evaluation");
			break;
		case IntegOprtStyle.Drift:
			text = Lang.PS("漂移评估");
			break;
		}
		string text2 = "";
		if (state > 0)
		{
			text2 = ((state == 1) ? Lang.PS(" [起始点]", " [the start]") : Lang.PS(" [结束点]", " [the end]"));
		}
		return text + text2;
	}

	public void Parse(string[] strs)
	{
		oprtStyle = method_0(strs[0]);
		group = ((!(strs[1] == "*")) ? char.Parse(strs[1].Trim()) : '\0');
		timeA = float.Parse(strs[2].Trim());
		timeB = float.Parse(strs[3].Trim());
		if (oprtStyle != IntegOprtStyle.BsTgnt)
		{
			value = float.Parse(strs[4].Trim());
			return;
		}
		string[] array = strs[4].Trim().Split(',');
		value = float.Parse(array[0].Trim());
		value2 = float.Parse(array[1].Trim());
		value3 = float.Parse(array[2].Trim());
		value4 = float.Parse(array[3].Trim());
	}

	private IntegOprtStyle method_0(string string_62)
	{
		if (IntegOprtStyle.DtecDelay.ToString() == string_62)
		{
			return IntegOprtStyle.DtecDelay;
		}
		if (IntegOprtStyle.PeakWidth.ToString() == string_62)
		{
			return IntegOprtStyle.PeakWidth;
		}
		if (IntegOprtStyle.Threshold.ToString() == string_62)
		{
			return IntegOprtStyle.Threshold;
		}
		if (IntegOprtStyle.PkSlope.ToString() == string_62)
		{
			return IntegOprtStyle.PkSlope;
		}
		if (IntegOprtStyle.VtVSlope.ToString() == string_62)
		{
			return IntegOprtStyle.VtVSlope;
		}
		if (IntegOprtStyle.ResetDtecNeg.ToString() == string_62)
		{
			return IntegOprtStyle.ResetDtecNeg;
		}
		if (IntegOprtStyle.ClampNeg.ToString() == string_62)
		{
			return IntegOprtStyle.ClampNeg;
		}
		if (IntegOprtStyle.PkWidth.ToString() == string_62)
		{
			return IntegOprtStyle.PkWidth;
		}
		if (IntegOprtStyle.PkThreshold.ToString() == string_62)
		{
			return IntegOprtStyle.PkThreshold;
		}
		if (IntegOprtStyle.PkAddPosi.ToString() == string_62)
		{
			return IntegOprtStyle.PkAddPosi;
		}
		if (IntegOprtStyle.PkAddNeg.ToString() == string_62)
		{
			return IntegOprtStyle.PkAddNeg;
		}
		if (IntegOprtStyle.PkCut.ToString() == string_62)
		{
			return IntegOprtStyle.PkCut;
		}
		if (IntegOprtStyle.PkHalfWidth.ToString() == string_62)
		{
			return IntegOprtStyle.PkHalfWidth;
		}
		if (IntegOprtStyle.PkArea.ToString() == string_62)
		{
			return IntegOprtStyle.PkArea;
		}
		if (IntegOprtStyle.PkVale.ToString() == string_62)
		{
			return IntegOprtStyle.PkVale;
		}
		if (IntegOprtStyle.SolventPeak.ToString() == string_62)
		{
			return IntegOprtStyle.SolventPeak;
		}
		if (IntegOprtStyle.FlowMarker.ToString() == string_62)
		{
			return IntegOprtStyle.FlowMarker;
		}
		if (IntegOprtStyle.GroupAdd.ToString() == string_62)
		{
			return IntegOprtStyle.GroupAdd;
		}
		if (IntegOprtStyle.GroupDelete.ToString() == string_62)
		{
			return IntegOprtStyle.GroupDelete;
		}
		if (IntegOprtStyle.BsTgnt.ToString() == string_62)
		{
			return IntegOprtStyle.BsTgnt;
		}
		if (IntegOprtStyle.BsVtV.ToString() == string_62)
		{
			return IntegOprtStyle.BsVtV;
		}
		if (IntegOprtStyle.BsValley.ToString() == string_62)
		{
			return IntegOprtStyle.BsValley;
		}
		if (IntegOprtStyle.BsTogether.ToString() == string_62)
		{
			return IntegOprtStyle.BsTogether;
		}
		if (IntegOprtStyle.BsForwHorz.ToString() == string_62)
		{
			return IntegOprtStyle.BsForwHorz;
		}
		if (IntegOprtStyle.BsBackHorz.ToString() == string_62)
		{
			return IntegOprtStyle.BsBackHorz;
		}
		if (IntegOprtStyle.BsFrontTgnt.ToString() == string_62)
		{
			return IntegOprtStyle.BsFrontTgnt;
		}
		if (IntegOprtStyle.BsTailTgnt.ToString() == string_62)
		{
			return IntegOprtStyle.BsTailTgnt;
		}
		if (IntegOprtStyle.Noise.ToString() == string_62)
		{
			return IntegOprtStyle.Noise;
		}
		if (IntegOprtStyle.Drift.ToString() != string_62)
		{
			throw new Exception();
		}
		return IntegOprtStyle.Drift;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write((byte)oprtStyle);
		binaryWriter_0.Write(group);
		binaryWriter_0.Write(timeA);
		binaryWriter_0.Write(timeB);
		binaryWriter_0.Write(value);
		if (oprtStyle == IntegOprtStyle.BsTgnt)
		{
			binaryWriter_0.Write(value2);
			binaryWriter_0.Write(value3);
			binaryWriter_0.Write(value4);
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		oprtStyle = (IntegOprtStyle)binaryReader_0.ReadByte();
		group = binaryReader_0.ReadChar();
		timeA = binaryReader_0.ReadSingle();
		timeB = binaryReader_0.ReadSingle();
		value = binaryReader_0.ReadSingle();
		if (oprtStyle == IntegOprtStyle.BsTgnt)
		{
			value2 = binaryReader_0.ReadSingle();
			value3 = binaryReader_0.ReadSingle();
			value4 = binaryReader_0.ReadSingle();
		}
	}
}
