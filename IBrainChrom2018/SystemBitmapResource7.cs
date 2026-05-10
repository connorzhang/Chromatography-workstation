using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource7
{
	private const string string_0 = "AuditTrail\\";

	private const string string_1 = "Close1";

	private const string string_2 = "Close2";

	private const string string_3 = "Close3";

	private const string string_4 = "Close4";

	private const string string_5 = "Instru1";

	private const string string_6 = "Instru2";

	private const string string_7 = "Instru3";

	private const string string_8 = "Instru4";

	private const string string_9 = "System";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	private static Bitmap bitmap_3;

	private static Bitmap bitmap_4;

	private static Bitmap bitmap_5;

	private static Bitmap bitmap_6;

	private static Bitmap bitmap_7;

	private static Bitmap bitmap_8;

	private static string string_10 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemIconResource.smethod_3(smethod_9() + "Close1", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = SystemIconResource.smethod_3(smethod_9() + "Close2", bool_0: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = SystemIconResource.smethod_3(smethod_9() + "Close3", bool_0: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_3()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = SystemIconResource.smethod_3(smethod_9() + "Close4", bool_0: true);
		}
		return bitmap_3;
	}

	public static Bitmap smethod_4()
	{
		if (bitmap_4 == null)
		{
			bitmap_4 = SystemIconResource.smethod_3(smethod_9() + "Instru1", bool_0: true);
		}
		return bitmap_4;
	}

	public static Bitmap smethod_5()
	{
		if (bitmap_5 == null)
		{
			bitmap_5 = SystemIconResource.smethod_3(smethod_9() + "Instru2", bool_0: true);
		}
		return bitmap_5;
	}

	public static Bitmap smethod_6()
	{
		if (bitmap_6 == null)
		{
			bitmap_6 = SystemIconResource.smethod_3(smethod_9() + "Instru3", bool_0: true);
		}
		return bitmap_6;
	}

	public static Bitmap smethod_7()
	{
		if (bitmap_7 == null)
		{
			bitmap_7 = SystemIconResource.smethod_3(smethod_9() + "Instru4", bool_0: true);
		}
		return bitmap_7;
	}

	public static Bitmap smethod_8()
	{
		if (bitmap_8 == null)
		{
			bitmap_8 = SystemIconResource.smethod_3(smethod_9() + "System", bool_0: true);
		}
		return bitmap_8;
	}

	private static string smethod_9()
	{
		if (string_10 == "")
		{
			string_10 = Class49.GetStartPath() + "Station\\AuditTrail\\";
		}
		return string_10;
	}
}
