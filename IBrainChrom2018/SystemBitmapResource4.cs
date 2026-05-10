using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource4
{
	private const string string_0 = "AutoStop";

	private const string string_1 = "DataAcquisition\\";

	private const string string_2 = "ManuStop";

	private const string string_3 = "RunSingle";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	private static string string_4 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemIconResource.smethod_3(smethod_3() + "AutoStop", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = SystemIconResource.smethod_3(smethod_3() + "ManuStop", bool_0: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = SystemIconResource.smethod_3(smethod_3() + "RunSingle", bool_0: true);
		}
		return bitmap_2;
	}

	public static string smethod_3()
	{
		if (string_4 == "")
		{
			string_4 = Class49.GetStartPath() + "Resource\\";
		}
		return string_4;
	}
}
