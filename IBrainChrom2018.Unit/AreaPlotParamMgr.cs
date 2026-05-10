using System.Collections.Generic;

namespace IBrainChrom2018.Unit;

public class AreaPlotParamMgr
{
	private static AreaPlotParamMgr myparam = null;

	private List<AreaPlotParam> plotList = new List<AreaPlotParam>();

	public static AreaPlotParamMgr Create()
	{
		if (myparam == null)
		{
			myparam = new AreaPlotParamMgr();
		}
		return myparam;
	}

	private AreaPlotParamMgr()
	{
	}

	public AreaPlotParam GetAreaPlotParam(int idx)
	{
		AreaPlotParam areaPlotParam = plotList.Find((AreaPlotParam a) => a.myIdx == idx);
		if (areaPlotParam == null)
		{
			areaPlotParam = new AreaPlotParam(idx);
			plotList.Add(areaPlotParam);
		}
		return areaPlotParam;
	}
}
