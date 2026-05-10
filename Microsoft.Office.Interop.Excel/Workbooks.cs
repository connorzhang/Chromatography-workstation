using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel;

[ComImport]
[DefaultMember("_Default")]
[CompilerGenerated]
[Guid("000208DB-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Workbooks : IEnumerable
{
	void _VtblGap1_3();

	[DispId(181)]
	[LCIDConversion(1)]
	[return: MarshalAs(UnmanagedType.Interface)]
	Workbook Add([In][MarshalAs(UnmanagedType.Struct)] object Template = null);
}
