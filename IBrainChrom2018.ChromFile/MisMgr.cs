using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace IBrainChrom2018.ChromFile;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(ChannelChartPara))]
[XmlInclude(typeof(ChartParaOpera))]
[XmlInclude(typeof(InsDeviceManager))]
public class MisMgr : IBaseFileMgr
{
	public int nchannel = 0;

	public InsDeviceManager devManager;

	public List<ChannelChartPara> ChannelChartParaS;

	public List<ChartParaOpera> ChartParaOperaS;

	public ChartParaOpera GetChartParaOpera(int idx)
	{
		if (ChartParaOperaS.Count > idx)
		{
			return ChartParaOperaS[idx];
		}
		while (ChartParaOperaS.Count <= idx)
		{
			ChartParaOperaS.Add(new ChartParaOpera());
		}
		return ChartParaOperaS[idx];
	}

	public ChannelChartPara GetChannelChartPara(int idx)
	{
		if (ChannelChartParaS.Count > idx)
		{
			return ChannelChartParaS[idx];
		}
		while (ChannelChartParaS.Count <= idx)
		{
			ChannelChartParaS.Add(new ChannelChartPara());
		}
		return ChannelChartParaS[idx];
	}

	public MisMgr()
	{
		m_strExt = "mis";
		m_strFileTypeName = Lang.PS("仪器设置文件");
		devManager = new InsDeviceManager();
		devManager.epcDevReset();
		ChannelChartParaS = IArrayBase.NewArray<ChannelChartPara>(0);
		ChartParaOperaS = IArrayBase.NewArray<ChartParaOpera>(0);
	}

	public MisMgr(int nChannel)
	{
		m_strExt = "mis";
		m_strFileTypeName = Lang.PS("仪器设置文件");
		devManager = new InsDeviceManager();
		ChannelChartParaS = IArrayBase.NewArray<ChannelChartPara>(nChannel);
		ChartParaOperaS = IArrayBase.NewArray<ChartParaOpera>(nChannel);
	}
}
