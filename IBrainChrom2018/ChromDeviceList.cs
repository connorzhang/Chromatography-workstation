using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(ChromDevice))]
public class ChromDeviceList : IBaseFileMgr
{
	private List<ChromDevice> chromDevList;

	public List<ChromDevice> ChromDevList
	{
		get
		{
			return chromDevList;
		}
		set
		{
			chromDevList = value;
		}
	}

	public int Count
	{
		get
		{
			if (chromDevList == null)
			{
				return 0;
			}
			return chromDevList.Count;
		}
	}

	public ChromDevice this[int index]
	{
		get
		{
			if (chromDevList == null)
			{
				return null;
			}
			return chromDevList[index];
		}
		set
		{
			if (chromDevList != null)
			{
				chromDevList[index] = value;
			}
		}
	}

	public ChromDeviceList()
	{
		m_strExt = "cfg";
		m_strFileTypeName = Lang.PS("系统配置文件");
		chromDevList = new List<ChromDevice>();
	}
}
