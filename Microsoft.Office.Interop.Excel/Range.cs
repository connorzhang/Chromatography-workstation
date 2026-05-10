using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel;

[ComImport]
[CompilerGenerated]
[Guid("00020846-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[TypeIdentifier]
public interface Range
{
	object this[[In][MarshalAs(UnmanagedType.Struct)] object RowIndex = null, [In][MarshalAs(UnmanagedType.Struct)] object ColumnIndex = null]
	{
		[PreserveSig]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Struct)]
		get;
		[PreserveSig]
		[DispId(0)]
		[param: MarshalAs(UnmanagedType.Struct)]
		set;
	}

	Font Font
	{
		[PreserveSig]
		[DispId(146)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	Interior Interior
	{
		[PreserveSig]
		[DispId(129)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}
}
