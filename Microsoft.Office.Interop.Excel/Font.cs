using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel;

[ComImport]
[CompilerGenerated]
[Guid("0002084D-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[TypeIdentifier]
public interface Font
{
	object Bold
	{
		[PreserveSig]
		[DispId(96)]
		[return: MarshalAs(UnmanagedType.Struct)]
		get;
		[PreserveSig]
		[DispId(96)]
		[param: MarshalAs(UnmanagedType.Struct)]
		set;
	}
}
