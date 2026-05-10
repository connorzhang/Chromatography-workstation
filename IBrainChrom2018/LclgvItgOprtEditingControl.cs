namespace IBrainChrom2018;

public class LclgvItgOprtEditingControl : LclgvComboBoxEditingControl
{
	private const string string_0 = " - ";

	public const string scnBaseline = "基线";

	public const string scnBsBackHorz = "向后水平";

	public const string scnBsForwHorz = "向前水平";

	public const string scnBsFrontTgnt = "前切";

	public const string scnBsTailTgnt = "尾切";

	public const string scnBsTgnt = "肩切参数";

	public const string scnBsTogether = "整合基线";

	public const string scnBsValley = "经过谷点";

	public const string scnBsVtV = "谷.谷斜率";

	public const string scnClampNeg = "翻转负峰";

	public const string scnDrift = "漂移评估";

	public const string scnDtecDelay = "检测器延迟";

	public const string scnFlowMarker = "流速标识";

	private const string string_1 = "添加组";

	private const string string_2 = "删除组";

	public const string scnItgPeak = "峰";

	public const string scnNoise = "噪声评估";

	public const string scnPeakWidth = "峰宽参数";

	public const string scnPkAddNeg = "添加负峰";

	public const string scnPkAddPosi = "添加正峰";

	public const string scnPkArea = "最小面积";

	public const string scnPkCut = "剔除峰";

	public const string scnPkHalfWidth = "最小半峰宽";

	public const string scnPkSlope = "峰斜率";

	public const string scnPkThreshold = "最小峰高";

	public const string scnPkVale = "谷点";

	public const string scnPkWidth = "最小峰宽";

	public const string scnResetDtecNeg = "重置.检测负峰";

	public const string scnSolventPeak = "溶剂峰";

	public const string scnThreshold = "峰高参数";

	public const string senBaseline = "Baseline";

	public const string senBsBackHorz = "Back. Horzitonal";

	public const string senBsForwHorz = "Forw. Horzitonal";

	public const string senBsFrontTgnt = "Front Tangent";

	public const string senBsTailTgnt = "Tail Tangent";

	public const string senBsTgnt = "Tangent Paras";

	public const string senBsTogether = "Valley Together";

	public const string senBsValley = "Pass Valley";

	public const string senBsVtV = "VtV";

	public const string senClampNeg = "Clamp Neg";

	public const string senDrift = "Drift Evaluation";

	public const string senDtecDelay = "Detector Delay";

	public const string senFlowMarker = "Flow Marker";

	private const string string_3 = "Group Add";

	private const string string_4 = "Group Delete";

	public const string senItgPeak = "Peak";

	public const string senNoise = "Noise Evaluation";

	public const string senPeakWidth = "Peak Width Para";

	public const string senPkAddNeg = "Add Negative";

	public const string senPkAddPosi = "Add Positive";

	public const string senPkArea = "Min Area";

	public const string senPkCut = "Cut Peaks";

	public const string senPkHalfWidth = "Half Width";

	public const string senPkSlope = "Peak Slope";

	public const string senPkThreshold = "Threshold";

	public const string senPkVale = "Valley";

	public const string senPkWidth = "Peak Width";

	public const string senResetDtecNeg = "Reset&Detect Negative";

	public const string senSolventPeak = "Solvent Peak";

	public const string senThreshold = "Peak Height Para";

	protected override string itemString(int itemIndex)
	{
		return ShowString((IntegOprtStyle)base.Items[itemIndex]);
	}

	public static string ShowString(IntegOprtStyle value)
	{
		string text = "?";
		return Class49.sysLanguage_0 switch
		{
			SysLanguage.CN => value switch
			{
				IntegOprtStyle.None => text, 
				IntegOprtStyle.DtecDelay => "检测器延迟", 
				IntegOprtStyle.PeakWidth => "峰宽参数", 
				IntegOprtStyle.Threshold => "峰高参数", 
				IntegOprtStyle.PkSlope => text, 
				IntegOprtStyle.VtVSlope => "峰斜率", 
				IntegOprtStyle.ResetDtecNeg => "峰 - 重置.检测负峰", 
				IntegOprtStyle.ClampNeg => "峰 - 翻转负峰", 
				IntegOprtStyle.PkWidth => "峰 - 最小峰宽", 
				IntegOprtStyle.PkThreshold => "峰 - 最小峰高", 
				IntegOprtStyle.PkAddPosi => "峰 - 添加正峰", 
				IntegOprtStyle.PkAddNeg => "峰 - 添加负峰", 
				IntegOprtStyle.PkCut => "峰 - 剔除峰", 
				IntegOprtStyle.PkHalfWidth => "峰 - 最小半峰宽", 
				IntegOprtStyle.PkArea => "峰 - 最小面积", 
				IntegOprtStyle.PkVale => "峰 - 谷点", 
				IntegOprtStyle.SolventPeak => "峰 - 垂直切割", 
				IntegOprtStyle.FlowMarker => "峰 - 流速标识", 
				IntegOprtStyle.GroupAdd => "峰 - 添加组", 
				IntegOprtStyle.GroupDelete => "峰 - 删除组", 
				IntegOprtStyle.BsTgnt => "基线 - 肩切参数", 
				IntegOprtStyle.BsVtV => "基线 - 谷.谷斜率", 
				IntegOprtStyle.BsValley => "基线 - 经过谷点", 
				IntegOprtStyle.BsTogether => "基线 - 整合基线", 
				IntegOprtStyle.BsForwHorz => "基线 - 向前水平", 
				IntegOprtStyle.BsBackHorz => "基线 - 向后水平", 
				IntegOprtStyle.BsFrontTgnt => "基线 - 前切", 
				IntegOprtStyle.BsTailTgnt => "基线 - 尾切", 
				IntegOprtStyle.Noise => "噪声评估", 
				IntegOprtStyle.Drift => "漂移评估", 
				_ => text, 
			}, 
			SysLanguage.EN => value switch
			{
				IntegOprtStyle.None => text, 
				IntegOprtStyle.DtecDelay => "Detector Delay", 
				IntegOprtStyle.PeakWidth => "Peak Width Para", 
				IntegOprtStyle.Threshold => "Peak Height Para", 
				IntegOprtStyle.PkSlope => text, 
				IntegOprtStyle.VtVSlope => "Peak Slope", 
				IntegOprtStyle.ResetDtecNeg => "Peak - Reset&Detect Negative", 
				IntegOprtStyle.ClampNeg => "Peak - Clamp Neg", 
				IntegOprtStyle.PkWidth => "Peak - Peak Width", 
				IntegOprtStyle.PkThreshold => "Peak - Threshold", 
				IntegOprtStyle.PkAddPosi => "Peak - Add Positive", 
				IntegOprtStyle.PkAddNeg => "Peak - Add Negative", 
				IntegOprtStyle.PkCut => "Peak - Cut Peaks", 
				IntegOprtStyle.PkHalfWidth => "Peak - Half Width", 
				IntegOprtStyle.PkArea => "Peak - Min Area", 
				IntegOprtStyle.PkVale => "Peak - Valley", 
				IntegOprtStyle.SolventPeak => "Peak - Solvent Peak", 
				IntegOprtStyle.FlowMarker => "Peak - Flow Marker", 
				IntegOprtStyle.GroupAdd => "Peak - Group Add", 
				IntegOprtStyle.GroupDelete => "Peak - Group Delete", 
				IntegOprtStyle.BsTgnt => "Baseline - Tangent Paras", 
				IntegOprtStyle.BsVtV => "Baseline - VtV", 
				IntegOprtStyle.BsValley => "Baseline - Pass Valley", 
				IntegOprtStyle.BsTogether => "Baseline - Valley Together", 
				IntegOprtStyle.BsForwHorz => "Baseline - Forw. Horzitonal", 
				IntegOprtStyle.BsBackHorz => "Baseline - Back. Horzitonal", 
				IntegOprtStyle.BsFrontTgnt => "Baseline - Front Tangent", 
				IntegOprtStyle.BsTailTgnt => "Baseline - Tail Tangent", 
				IntegOprtStyle.Noise => "Noise Evaluation", 
				IntegOprtStyle.Drift => "Drift Evaluation", 
				_ => text, 
			}, 
			_ => text, 
		};
	}
}
