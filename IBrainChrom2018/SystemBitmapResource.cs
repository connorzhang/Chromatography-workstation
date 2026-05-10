using System.Drawing;

namespace IBrainChrom2018;

internal class SystemBitmapResource
{
	private const string string_0 = "Baloon";

	private const string string_1 = "BsBackHorz";

	private const string string_2 = "BsForwHorz";

	private const string string_3 = "BsFrontTgnt";

	private const string string_4 = "BsTailTgnt";

	private const string string_5 = "BsTgnt";

	private const string string_6 = "BsTogether";

	private const string string_7 = "BsValley";

	private const string string_8 = "BsVtV";

	private const string string_9 = "Chromatogram\\";

	private const string string_10 = "ClampNeg";

	private const string string_11 = "DtecDelay";

	private const string string_12 = "FlowMarker";

	private const string string_13 = "Groups";

	private const string string_14 = "noBaloon";

	private const string string_15 = "OverlayMode";

	private const string string_16 = "PeakWidth";

	private const string string_17 = "PkAddNeg";

	private const string string_18 = "PkAddPosi";

	private const string string_19 = "PkArea";

	private const string string_20 = "PkCut";

	private const string string_21 = "PkHalfWidth";

	private const string string_22 = "PkThreshold";

	private const string string_23 = "PkVale";

	private const string string_24 = "PkWidth";

	private const string string_25 = "ResetDtecNeg";

	private const string string_26 = "SolventPeak";

	private const string string_27 = "Threshold";

	private const string string_28 = "VtVSlope";

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

	private static Bitmap bitmap_19;

	private static Bitmap bitmap_20;

	private static Bitmap bitmap_21;

	private static Bitmap bitmap_22;

	private static Bitmap bitmap_23;

	private static Bitmap bitmap_24;

	private static Bitmap bitmap_25;

	private static Bitmap bitmap_26;

	private static Bitmap bitmap_27;

	private static string string_29 = "";

	public static Bitmap smethod_0()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "Baloon", bMakeTransparent: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_1()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsBackHorz", bMakeTransparent: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_2()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsForwHorz", bMakeTransparent: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_3()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsFrontTgnt", bMakeTransparent: true);
		}
		return bitmap_3;
	}

	public static Bitmap smethod_4()
	{
		if (bitmap_4 == null)
		{
			bitmap_4 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsTailTgnt", bMakeTransparent: true);
		}
		return bitmap_4;
	}

	public static Bitmap smethod_5()
	{
		if (bitmap_5 == null)
		{
			bitmap_5 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsTgnt", bMakeTransparent: true);
		}
		return bitmap_5;
	}

	public static Bitmap smethod_6()
	{
		if (bitmap_6 == null)
		{
			bitmap_6 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsTogether", bMakeTransparent: true);
		}
		return bitmap_6;
	}

	public static Bitmap smethod_7()
	{
		if (bitmap_7 == null)
		{
			bitmap_7 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsValley", bMakeTransparent: true);
		}
		return bitmap_7;
	}

	public static Bitmap smethod_8()
	{
		if (bitmap_8 == null)
		{
			bitmap_8 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "BsVtV", bMakeTransparent: true);
		}
		return bitmap_8;
	}

	public static Bitmap smethod_9()
	{
		if (bitmap_9 == null)
		{
			bitmap_9 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "ClampNeg", bMakeTransparent: true);
		}
		return bitmap_9;
	}

	public static Bitmap smethod_10()
	{
		if (bitmap_10 == null)
		{
			bitmap_10 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "DtecDelay", bMakeTransparent: true);
		}
		return bitmap_10;
	}

	public static Bitmap smethod_11()
	{
		if (bitmap_11 == null)
		{
			bitmap_11 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "FlowMarker", bMakeTransparent: true);
		}
		return bitmap_11;
	}

	public static Bitmap smethod_12()
	{
		if (bitmap_12 == null)
		{
			bitmap_12 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "Groups", bMakeTransparent: true);
		}
		return bitmap_12;
	}

	public static Bitmap smethod_13()
	{
		if (bitmap_13 == null)
		{
			bitmap_13 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "noBaloon", bMakeTransparent: true);
		}
		return bitmap_13;
	}

	public static Bitmap smethod_14()
	{
		if (bitmap_14 == null)
		{
			bitmap_14 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "OverlayMode", bMakeTransparent: true);
		}
		return bitmap_14;
	}

	public static Bitmap smethod_15()
	{
		if (bitmap_15 == null)
		{
			bitmap_15 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PeakWidth", bMakeTransparent: true);
		}
		return bitmap_15;
	}

	public static Bitmap smethod_16()
	{
		if (bitmap_16 == null)
		{
			bitmap_16 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkAddNeg", bMakeTransparent: true);
		}
		return bitmap_16;
	}

	public static Bitmap smethod_17()
	{
		if (bitmap_17 == null)
		{
			bitmap_17 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkAddPosi", bMakeTransparent: true);
		}
		return bitmap_17;
	}

	public static Bitmap smethod_18()
	{
		if (bitmap_18 == null)
		{
			bitmap_18 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkArea", bMakeTransparent: true);
		}
		return bitmap_18;
	}

	public static Bitmap smethod_19()
	{
		if (bitmap_19 == null)
		{
			bitmap_19 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkCut", bMakeTransparent: true);
		}
		return bitmap_19;
	}

	public static Bitmap smethod_20()
	{
		if (bitmap_20 == null)
		{
			bitmap_20 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkHalfWidth", bMakeTransparent: true);
		}
		return bitmap_20;
	}

	public static Bitmap smethod_21()
	{
		if (bitmap_21 == null)
		{
			bitmap_21 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkThreshold", bMakeTransparent: true);
		}
		return bitmap_21;
	}

	public static Bitmap smethod_22()
	{
		if (bitmap_22 == null)
		{
			bitmap_22 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkVale", bMakeTransparent: true);
		}
		return bitmap_22;
	}

	public static Bitmap smethod_23()
	{
		if (bitmap_23 == null)
		{
			bitmap_23 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "PkWidth", bMakeTransparent: true);
		}
		return bitmap_23;
	}

	public static Bitmap smethod_24()
	{
		if (bitmap_24 == null)
		{
			bitmap_24 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "ResetDtecNeg", bMakeTransparent: true);
		}
		return bitmap_24;
	}

	public static Bitmap smethod_25()
	{
		if (bitmap_25 == null)
		{
			bitmap_25 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "SolventPeak", bMakeTransparent: true);
		}
		return bitmap_25;
	}

	public static Bitmap smethod_26()
	{
		if (bitmap_26 == null)
		{
			bitmap_26 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "Threshold", bMakeTransparent: true);
		}
		return bitmap_26;
	}

	public static Bitmap smethod_27()
	{
		if (bitmap_27 == null)
		{
			bitmap_27 = ResourceImageLoad.LoadBitmap(PathChromatogram() + "VtVSlope", bMakeTransparent: true);
		}
		return bitmap_27;
	}

	private static string PathChromatogram()
	{
		if (string_29 == "")
		{
			string_29 = ResourceImageLoad.ExePath() + "Station\\Chromatogram\\";
		}
		return string_29;
	}
}
