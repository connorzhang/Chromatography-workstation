using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource5
{
	private const string string_0 = "AddAll";

	private const string string_1 = "AddExists";

	private const string string_2 = "AddGroup";

	private const string string_3 = "AddPeak";

	private const string string_4 = "Calibration\\";

	private const string string_5 = "DeleteCmpd";

	private const string string_6 = "Options";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	private static Bitmap bitmap_3;

	private static Bitmap bitmap_4;

	private static Bitmap bitmap_5;

	private static string string_7 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemIconResource.smethod_3(smethod_6() + "AddAll", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = SystemIconResource.smethod_3(smethod_6() + "AddExists", bool_0: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = SystemIconResource.smethod_3(smethod_6() + "AddGroup", bool_0: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_3()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = SystemIconResource.smethod_3(smethod_6() + "AddPeak", bool_0: true);
		}
		return bitmap_3;
	}

	public static Bitmap smethod_4()
	{
		if (bitmap_4 == null)
		{
			bitmap_4 = SystemIconResource.smethod_3(smethod_6() + "DeleteCmpd", bool_0: true);
		}
		return bitmap_4;
	}

	public static Bitmap smethod_5()
	{
		if (bitmap_5 == null)
		{
			bitmap_5 = SystemIconResource.smethod_3(smethod_6() + "Options", bool_0: true);
		}
		return bitmap_5;
	}

	private static string smethod_6()
	{
		if (string_7 == "")
		{
			string_7 = Class49.GetStartPath() + "Station\\Calibration\\";
		}
		return string_7;
	}
}
