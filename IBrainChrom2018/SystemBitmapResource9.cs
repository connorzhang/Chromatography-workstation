using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource9
{
	private const string string_0 = "Abort";

	private const string string_1 = "Check";

	private const string string_2 = "check_CheckOK";

	private const string string_3 = "check_HasError";

	private const string string_4 = "check_NotCheck";

	private const string string_5 = "ContextButton";

	private const string string_6 = "CurMethod";

	private const string string_7 = "InsertLine";

	private const string string_8 = "KAlpha";

	private const string string_9 = "Pause";

	private const string string_10 = "RepeatInj";

	private const string string_11 = "Resume";

	private const string string_12 = "RowsDown";

	private const string string_13 = "RowsUp";

	private const string string_14 = "RunSequence";

	private const string string_15 = "SequenceAnalysis\\";

	private const string string_16 = "SkipVial";

	private const string string_17 = "Snapshot";

	private const string string_18 = "Stop";

	private const string string_19 = "UnContextButton";

	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	private static Bitmap bitmap_3;

	private static Bitmap bitmap_4;

	private static Bitmap bitmap_5;

	private static Bitmap bitmap_6;

	private static Bitmap bitmap_7;

	private static Bitmap bitmap_8;

	private static Bitmap bitmap_9;

	private static Bitmap bitmap_10;

	private static Bitmap bitmap_11;

	private static Bitmap bitmap_12;

	private static Bitmap bitmap_13;

	private static Bitmap bitmap_14;

	private static Bitmap bitmap_15;

	private static Bitmap bitmap_16;

	private static Bitmap bitmap_17;

	private static Bitmap bitmap_18;

	private static string string_20 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = ResourceImageLoad.LoadBitmap(smethod_19() + "Abort", bMakeTransparent: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = ResourceImageLoad.LoadBitmap(smethod_19() + "Check", bMakeTransparent: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = ResourceImageLoad.LoadBitmap(smethod_19() + "check_CheckOK", bMakeTransparent: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_3()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = ResourceImageLoad.LoadBitmap(smethod_19() + "check_HasError", bMakeTransparent: true);
		}
		return bitmap_3;
	}

	public static Bitmap smethod_4()
	{
		if (bitmap_4 == null)
		{
			bitmap_4 = ResourceImageLoad.LoadBitmap(smethod_19() + "check_NotCheck", bMakeTransparent: true);
		}
		return bitmap_4;
	}

	public static Bitmap smethod_5()
	{
		if (bitmap_5 == null)
		{
			bitmap_5 = ResourceImageLoad.LoadBitmap(smethod_19() + "ContextButton", bMakeTransparent: true);
		}
		return bitmap_5;
	}

	public static Bitmap smethod_6()
	{
		if (bitmap_6 == null)
		{
			bitmap_6 = ResourceImageLoad.LoadBitmap(smethod_19() + "CurMethod", bMakeTransparent: true);
		}
		return bitmap_6;
	}

	public static Bitmap smethod_7()
	{
		if (bitmap_7 == null)
		{
			bitmap_7 = ResourceImageLoad.LoadBitmap(smethod_19() + "InsertLine", bMakeTransparent: true);
		}
		return bitmap_7;
	}

	public static Bitmap smethod_8()
	{
		if (bitmap_8 == null)
		{
			bitmap_8 = ResourceImageLoad.LoadBitmap(smethod_19() + "KAlpha", bMakeTransparent: true);
		}
		return bitmap_8;
	}

	public static Bitmap smethod_9()
	{
		if (bitmap_9 == null)
		{
			bitmap_9 = ResourceImageLoad.LoadBitmap(smethod_19() + "Pause", bMakeTransparent: true);
		}
		return bitmap_9;
	}

	public static Bitmap smethod_10()
	{
		if (bitmap_10 == null)
		{
			bitmap_10 = ResourceImageLoad.LoadBitmap(smethod_19() + "RepeatInj", bMakeTransparent: true);
		}
		return bitmap_10;
	}

	public static Bitmap smethod_11()
	{
		if (bitmap_11 == null)
		{
			bitmap_11 = ResourceImageLoad.LoadBitmap(smethod_19() + "Resume", bMakeTransparent: true);
		}
		return bitmap_11;
	}

	public static Bitmap smethod_12()
	{
		if (bitmap_12 == null)
		{
			bitmap_12 = ResourceImageLoad.LoadBitmap(smethod_19() + "RowsDown", bMakeTransparent: true);
		}
		return bitmap_12;
	}

	public static Bitmap smethod_13()
	{
		if (bitmap_13 == null)
		{
			bitmap_13 = ResourceImageLoad.LoadBitmap(smethod_19() + "RowsUp", bMakeTransparent: true);
		}
		return bitmap_13;
	}

	public static Bitmap smethod_14()
	{
		if (bitmap_14 == null)
		{
			bitmap_14 = ResourceImageLoad.LoadBitmap(smethod_19() + "RunSequence", bMakeTransparent: true);
		}
		return bitmap_14;
	}

	public static Bitmap smethod_15()
	{
		if (bitmap_15 == null)
		{
			bitmap_15 = ResourceImageLoad.LoadBitmap(smethod_19() + "SkipVial", bMakeTransparent: true);
		}
		return bitmap_15;
	}

	public static Bitmap smethod_16()
	{
		if (bitmap_16 == null)
		{
			bitmap_16 = ResourceImageLoad.LoadBitmap(smethod_19() + "Snapshot", bMakeTransparent: true);
		}
		return bitmap_16;
	}

	public static Bitmap smethod_17()
	{
		if (bitmap_17 == null)
		{
			bitmap_17 = ResourceImageLoad.LoadBitmap(smethod_19() + "Stop", bMakeTransparent: true);
		}
		return bitmap_17;
	}

	public static Bitmap smethod_18()
	{
		if (bitmap_18 == null)
		{
			bitmap_18 = new Bitmap(16, 16);
		}
		return bitmap_18;
	}

	private static string smethod_19()
	{
		if (string_20 == "")
		{
			string_20 = ResourceImageLoad.ExePath() + "Station\\SequenceAnalysis\\";
		}
		return string_20;
	}
}
