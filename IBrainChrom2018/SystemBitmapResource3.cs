using System.Drawing;

namespace IBrainChrom2018;

internal static class SystemBitmapResource3
{
	private const string string_0 = "Configuration";

	private const string string_1 = "Directories";

	private const string string_2 = "Lock";

	private const string string_3 = "Main\\";

	private const string string_4 = "UserAccounts";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	private static Bitmap bitmap_3;

	private static string string_5 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemIconResource.smethod_3(smethod_4() + "Configuration", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = SystemIconResource.smethod_3(smethod_4() + "Directories", bool_0: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = SystemIconResource.smethod_3(smethod_4() + "Lock", bool_0: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_3()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = SystemIconResource.smethod_3(smethod_4() + "UserAccounts", bool_0: true);
		}
		return bitmap_3;
	}

	private static string smethod_4()
	{
		if (string_5 == "")
		{
			string_5 = Class49.GetStartPath() + "Station\\Main\\";
		}
		return string_5;
	}
}
