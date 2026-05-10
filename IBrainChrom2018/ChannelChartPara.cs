using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ChannelChartPara : IArrayBase
{
	public float showLowLimit;

	public float showHighLimit;

	public float fullScreenTime;

	public float stopTime;

	public bool analysisWhenStop;

	public bool bFullScreen;

	public bool bAutoFullY;

	public bool printWhenStop;

	public bool bClearZero;

	public bool bBaselineDeduction;

	public EnumChannelDetectionMethod cnlDetectMethod;

	public EnumChannelBasisQuantity cnlBasisQuantity;

	public ChannelChartPara()
	{
		showLowLimit = 0f;
		showHighLimit = 100f;
		fullScreenTime = 30f;
		stopTime = 30f;
		analysisWhenStop = true;
		bFullScreen = false;
		bAutoFullY = false;
		printWhenStop = true;
		bClearZero = false;
		bBaselineDeduction = false;
		cnlDetectMethod = EnumChannelDetectionMethod.Normal;
		cnlBasisQuantity = EnumChannelBasisQuantity.PeakArea;
	}

	public ChannelChartPara(float showLowLimit, float showHighLimit, float fullScreenTime, float stopTime, bool analysisWhenStop, bool bFullScreen, bool bAutoFullScreen, bool printWhenStop, bool bClearZero, bool bBaselineDeduction, EnumChannelDetectionMethod cnlDetectMethod, EnumChannelBasisQuantity cnlBasisQuantity)
	{
		this.showLowLimit = showLowLimit;
		this.showHighLimit = showHighLimit;
		this.fullScreenTime = fullScreenTime;
		this.stopTime = stopTime;
		this.analysisWhenStop = analysisWhenStop;
		this.bFullScreen = bFullScreen;
		bAutoFullY = false;
		this.printWhenStop = printWhenStop;
		this.bClearZero = bClearZero;
		this.bBaselineDeduction = bBaselineDeduction;
		this.cnlDetectMethod = cnlDetectMethod;
		this.cnlBasisQuantity = cnlBasisQuantity;
	}
}
