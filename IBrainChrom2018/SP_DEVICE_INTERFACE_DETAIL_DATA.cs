using System.Runtime.InteropServices;

namespace IBrainChrom2018;

public struct SP_DEVICE_INTERFACE_DETAIL_DATA
{
	public int cbSize;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
	public byte[] DevicePath;
}
