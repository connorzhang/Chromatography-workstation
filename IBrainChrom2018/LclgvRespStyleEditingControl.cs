namespace IBrainChrom2018;

public class LclgvRespStyleEditingControl : LclgvComboBoxEditingControl
{
	protected override string itemString(int itemIndex)
	{
		return ShowString((RespStyle)base.Items[itemIndex]);
	}

	public static string ShowString(RespStyle value)
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			switch (value)
			{
			case RespStyle.Area:
				return "面积";
			case RespStyle.Height:
				return "高度";
			case RespStyle.AreaSquare:
				return "面积平方根";
			case RespStyle.PeakHeightSquare:
				return "高度平方根";
			}
			break;
		case SysLanguage.EN:
			switch (value)
			{
			case RespStyle.Area:
				return "Area";
			case RespStyle.Height:
				return "Height";
			case RespStyle.AreaSquare:
				return "AreaSquare";
			case RespStyle.PeakHeightSquare:
				return "PeakHeightSquare";
			}
			break;
		}
		return "";
	}
}
