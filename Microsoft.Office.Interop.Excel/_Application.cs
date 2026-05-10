using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel;

[ComImport]
[DefaultMember("_Default")]
[CompilerGenerated]
[Guid("000208D5-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface _Application
{
	Workbooks Workbooks
	{
		[DispId(572)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	bool Visible
	{
		[DispId(558)]
		[LCIDConversion(0)]
		get;
		[DispId(558)]
		[LCIDConversion(0)]
		set;
	}

	void _VtblGap1_45();

	void _VtblGap2_229();
}
