using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(ChromDeviceInfo))]
[XmlInclude(typeof(MisMgr))]
[XmlInclude(typeof(IBaseFileMgr))]
public class ChromDevice : IArrayBase
{
	public ChromDeviceInfo info;

	public MisMgr misMgr = new MisMgr();

	public ChromDevice()
	{
		info = new ChromDeviceInfo();
		misMgr = new MisMgr();
	}

	public ChromDevice(ChromDeviceInfo myinfo, int nChannel)
	{
		nChannel = Math.Max(0, nChannel);
		info = myinfo;
		misMgr = new MisMgr();
	}
}
