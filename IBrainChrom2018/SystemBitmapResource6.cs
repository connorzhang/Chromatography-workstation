using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource6
{
	private const string string_0 = "Down";

	private const string string_1 = "MtdSetup\\";

	private const string string_2 = "Up";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static string string_3 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemIconResource.smethod_3(smethod_2() + "Down", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = SystemIconResource.smethod_3(smethod_2() + "Up", bool_0: true);
		}
		return bitmap_1;
	}

	private static string smethod_2()
	{
		if (string_3 == "")
		{
			string_3 = Class49.GetStartPath() + "Station\\MtdSetup\\";
		}
		return string_3;
	}
}
