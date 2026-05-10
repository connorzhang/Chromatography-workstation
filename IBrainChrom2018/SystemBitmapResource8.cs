using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource8
{
	private const string string_0 = "A";

	private const string string_1 = "B";

	private const string string_2 = "C";

	private const string string_3 = "D";

	private const string string_4 = "DeviceMonitor\\";

	private const string string_5 = "Flow";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	private static Bitmap bitmap_3;

	private static Bitmap bitmap_4;

	private static string string_6 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemIconResource.smethod_3(smethod_5() + "A", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = SystemIconResource.smethod_3(smethod_5() + "B", bool_0: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = SystemIconResource.smethod_3(smethod_5() + "C", bool_0: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_3()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = SystemIconResource.smethod_3(smethod_5() + "D", bool_0: true);
		}
		return bitmap_3;
	}

	public static Bitmap smethod_4()
	{
		if (bitmap_4 == null)
		{
			bitmap_4 = SystemIconResource.smethod_3(smethod_5() + "Flow", bool_0: true);
		}
		return bitmap_4;
	}

	private static string smethod_5()
	{
		if (string_6 == "")
		{
			string_6 = Class49.GetStartPath() + "Station\\DeviceMonitor\\";
		}
		return string_6;
	}
}
