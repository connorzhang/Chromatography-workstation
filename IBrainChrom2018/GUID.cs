using System.Runtime.InteropServices;

namespace IBrainChrom2018;

public struct GUID
{
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public byte[] Data1;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public byte[] Data2;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public byte[] Data3;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] Data4;
}
