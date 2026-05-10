using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ShortMsg
{
	public bool AutoSendByStopTime;

	public string Mess;

	public bool sound;

	public int soundTimes;
}
