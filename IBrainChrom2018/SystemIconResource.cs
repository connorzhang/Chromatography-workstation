using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal static class SystemIconResource
{
	private static Icon icon_0;

	private static Icon icon_1;

	private static Icon icon_2;

	private static Icon icon_3;

	private static Icon icon_4;

	private static Icon icon_5;

	private static Icon icon_6;

	private static Icon icon_7;

	private static Icon icon_8;

	private static Icon icon_9;

	private static Icon icon_10;

	private static Icon icon_11;

	private static Icon icon_12;

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

	private static Bitmap bitmap_28;

	private static Bitmap bitmap_29;

	private static Bitmap bitmap_30;

	private static Bitmap bitmap_31;

	private static Bitmap bitmap_32;

	private static Bitmap bitmap_33;

	private static Bitmap bitmap_34;

	private static Bitmap bitmap_35;

	private static Bitmap bitmap_36;

	private static Bitmap bitmap_37;

	private static Bitmap bitmap_38;

	private static Bitmap bitmap_39;

	private static Bitmap bitmap_40;

	private static Bitmap bitmap_41;

	private static Bitmap bitmap_42;

	private static Bitmap bitmap_43;

	private static Bitmap bitmap_44;

	private static Bitmap bitmap_45;

	private static string string_56 = "";

	private static string string_57 = "";

	private static string string_58 = "";

	private static string string_59 = "";

	public static void smethod_0(Graphics graphics_0, Image image_0, Point point_0)
	{
		if (image_0 != null)
		{
			graphics_0.DrawImage(image_0, point_0);
		}
	}

	public static void smethod_1(Graphics graphics_0, Image image_0, Rectangle rectangle_0)
	{
		if (image_0 != null)
		{
			graphics_0.DrawImage(image_0, rectangle_0);
		}
	}

	private static Icon smethod_2(Bitmap bitmap_46)
	{
		if (bitmap_46 == null)
		{
			return null;
		}
		return Icon.FromHandle(bitmap_46.GetHicon());
	}

	public static Bitmap smethod_3(string string_60, bool bool_0)
	{
		string text = string_60;
		if (!File.Exists(text))
		{
			string text2 = string_60 + ".png";
			FileInfo fileInfo = new FileInfo(text2);
			string text3 = string_60 + ".jpg";
			FileInfo fileInfo2 = new FileInfo(text3);
			string text4 = string_60 + ".bmp";
			FileInfo fileInfo3 = new FileInfo(text4);
			if (!fileInfo.Exists)
			{
				if (!fileInfo2.Exists)
				{
					if (!fileInfo3.Exists)
					{
						return null;
					}
					text = text4;
				}
				else
				{
					text = text3;
				}
			}
			else
			{
				text = text2;
			}
		}
		Bitmap bitmap = new Bitmap(text);
		if (bool_0)
		{
			Color pixel = bitmap.GetPixel(0, 0);
			bitmap.MakeTransparent(pixel);
		}
		return bitmap;
	}

	public static void smethod_4(object object_0, Bitmap bitmap_46)
	{
		if (bitmap_46 != null)
		{
			if (object_0 is Button)
			{
				Button button = object_0 as Button;
				button.ImageAlign = ContentAlignment.MiddleCenter;
				button.Image = bitmap_46;
			}
			if (object_0 is ToolStripButton)
			{
				ToolStripButton toolStripButton = object_0 as ToolStripButton;
				toolStripButton.ImageScaling = ToolStripItemImageScaling.None;
				toolStripButton.Image = bitmap_46;
			}
			if (object_0 is ToolStripSplitButton)
			{
				ToolStripSplitButton toolStripSplitButton = object_0 as ToolStripSplitButton;
				toolStripSplitButton.ImageScaling = ToolStripItemImageScaling.None;
				toolStripSplitButton.Image = bitmap_46;
			}
			if (object_0 is ToolStripMenuItem)
			{
				ToolStripMenuItem toolStripMenuItem = object_0 as ToolStripMenuItem;
				toolStripMenuItem.Image = bitmap_46;
			}
		}
	}

	public static Icon smethod_5()
	{
		if (icon_0 == null)
		{
			icon_0 = smethod_2(smethod_45());
		}
		return icon_0;
	}

	public static Icon smethod_6()
	{
		if (icon_1 == null)
		{
			icon_1 = smethod_2(smethod_46());
		}
		return icon_1;
	}

	public static Icon smethod_7()
	{
		if (icon_2 == null)
		{
			icon_2 = smethod_2(smethod_47());
		}
		return icon_2;
	}

	public static Icon smethod_8()
	{
		if (icon_3 == null)
		{
			icon_3 = smethod_2(smethod_48());
		}
		return icon_3;
	}

	public static Icon smethod_9()
	{
		if (icon_4 == null)
		{
			icon_4 = smethod_2(smethod_49());
		}
		return icon_4;
	}

	public static Icon smethod_10()
	{
		if (icon_5 == null)
		{
			icon_5 = smethod_2(smethod_50());
		}
		return icon_5;
	}

	public static Icon smethod_11()
	{
		if (icon_6 == null)
		{
			icon_6 = smethod_2(smethod_51());
		}
		return icon_6;
	}

	public static Icon smethod_12()
	{
		if (icon_7 == null)
		{
			icon_7 = smethod_2(smethod_52());
		}
		return icon_7;
	}

	public static Icon smethod_13()
	{
		if (icon_8 == null)
		{
			icon_8 = smethod_2(smethod_53());
		}
		return icon_8;
	}

	public static Icon smethod_14()
	{
		if (icon_9 == null)
		{
			icon_9 = smethod_2(smethod_54());
		}
		return icon_9;
	}

	public static Icon smethod_15()
	{
		if (icon_10 == null)
		{
			icon_10 = smethod_2(smethod_55());
		}
		return icon_10;
	}

	public static Icon smethod_16()
	{
		if (icon_11 == null)
		{
			icon_11 = smethod_2(smethod_61());
		}
		return icon_11;
	}

	public static Icon smethod_17()
	{
		if (icon_12 == null)
		{
			icon_12 = smethod_2(smethod_62());
		}
		return icon_12;
	}

	public static Bitmap smethod_18()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = smethod_3(smethod_65() + "Delete", bool_0: true);
		}
		return bitmap_0;
	}

	public static Bitmap smethod_19()
	{
		if (bitmap_1 == null)
		{
			bitmap_1 = smethod_3(smethod_65() + "Directory", bool_0: true);
		}
		return bitmap_1;
	}

	public static Bitmap smethod_20()
	{
		if (bitmap_2 == null)
		{
			bitmap_2 = smethod_3(smethod_65() + "Triangle_first", bool_0: true);
		}
		return bitmap_2;
	}

	public static Bitmap smethod_21()
	{
		if (bitmap_3 == null)
		{
			bitmap_3 = smethod_3(smethod_65() + "Folder", bool_0: true);
		}
		return bitmap_3;
	}

	public static Bitmap smethod_22()
	{
		if (bitmap_4 == null)
		{
			bitmap_4 = smethod_3(smethod_65() + "FolderOpen", bool_0: true);
		}
		return bitmap_4;
	}

	public static Bitmap smethod_23()
	{
		if (bitmap_5 == null)
		{
			bitmap_5 = smethod_3(smethod_65() + "Triangle_last", bool_0: true);
		}
		return bitmap_5;
	}

	public static Bitmap smethod_24()
	{
		if (bitmap_6 == null)
		{
			bitmap_6 = smethod_3(smethod_65() + "mk_Cross", bool_0: true);
		}
		return bitmap_6;
	}

	public static Bitmap smethod_25()
	{
		if (bitmap_7 == null)
		{
			bitmap_7 = smethod_3(smethod_65() + "mk_OK", bool_0: true);
		}
		return bitmap_7;
	}

	public static Bitmap smethod_26()
	{
		if (bitmap_8 == null)
		{
			bitmap_8 = smethod_3(smethod_65() + "mk_Question", bool_0: true);
		}
		return bitmap_8;
	}

	public static Bitmap smethod_27()
	{
		if (bitmap_9 == null)
		{
			bitmap_9 = smethod_3(smethod_65() + "New", bool_0: true);
		}
		return bitmap_9;
	}

	public static Bitmap smethod_28()
	{
		if (bitmap_10 == null)
		{
			bitmap_10 = smethod_3(smethod_65() + "Triangle_right", bool_0: true);
		}
		return bitmap_10;
	}

	public static Bitmap smethod_29()
	{
		if (bitmap_11 == null)
		{
			bitmap_11 = smethod_3(smethod_65() + "NudDown", bool_0: true);
		}
		return bitmap_11;
	}

	public static Bitmap smethod_30()
	{
		if (bitmap_12 == null)
		{
			bitmap_12 = smethod_3(smethod_65() + "NudUp", bool_0: true);
		}
		return bitmap_12;
	}

	public static Bitmap smethod_31()
	{
		if (bitmap_13 == null)
		{
			bitmap_13 = smethod_3(smethod_65() + "OpenTo", bool_0: true);
		}
		return bitmap_13;
	}

	public static Bitmap smethod_32()
	{
		if (bitmap_14 == null)
		{
			bitmap_14 = smethod_3(smethod_65() + "OpenTo2", bool_0: true);
		}
		return bitmap_14;
	}

	public static Bitmap smethod_33()
	{
		if (bitmap_15 == null)
		{
			bitmap_15 = smethod_3(smethod_65() + "Preview", bool_0: true);
		}
		return bitmap_15;
	}

	public static Bitmap smethod_34()
	{
		if (bitmap_16 == null)
		{
			bitmap_16 = smethod_3(smethod_65() + "Triangle_left", bool_0: true);
		}
		return bitmap_16;
	}

	public static Bitmap smethod_35()
	{
		if (bitmap_17 == null)
		{
			bitmap_17 = smethod_3(smethod_65() + "Print", bool_0: true);
		}
		return bitmap_17;
	}

	public static Bitmap smethod_36()
	{
		if (bitmap_18 == null)
		{
			bitmap_18 = smethod_3(smethod_65() + "Question", bool_0: true);
		}
		return bitmap_18;
	}

	public static Bitmap smethod_37()
	{
		if (bitmap_19 == null)
		{
			bitmap_19 = smethod_3(smethod_65() + "Save", bool_0: true);
		}
		return bitmap_19;
	}

	public static Bitmap smethod_38()
	{
		if (bitmap_20 == null)
		{
			bitmap_20 = smethod_3(smethod_65() + "Save2", bool_0: true);
		}
		return bitmap_20;
	}

	public static Bitmap smethod_39()
	{
		if (bitmap_21 == null)
		{
			bitmap_21 = smethod_3(smethod_65() + "Save3", bool_0: true);
		}
		return bitmap_21;
	}

	public static Bitmap smethod_40()
	{
		if (bitmap_22 == null)
		{
			bitmap_22 = smethod_3(smethod_65() + "Triangle_right", bool_0: true);
		}
		return bitmap_22;
	}

	public static Bitmap smethod_41()
	{
		if (bitmap_23 == null)
		{
			bitmap_23 = smethod_3(smethod_64() + "Bottom", bool_0: true);
		}
		return bitmap_23;
	}

	public static Bitmap smethod_42()
	{
		if (bitmap_24 == null)
		{
			bitmap_24 = smethod_3(smethod_64() + "Down", bool_0: true);
		}
		return bitmap_24;
	}

	public static Bitmap smethod_43()
	{
		if (bitmap_25 == null)
		{
			bitmap_25 = smethod_3(smethod_64() + "Top", bool_0: true);
		}
		return bitmap_25;
	}

	public static Bitmap smethod_44()
	{
		if (bitmap_26 == null)
		{
			bitmap_26 = smethod_3(smethod_64() + "Up", bool_0: true);
		}
		return bitmap_26;
	}

	public static Bitmap smethod_45()
	{
		if (bitmap_27 == null)
		{
			bitmap_27 = smethod_3(smethod_66() + "AuditTrail", bool_0: true);
		}
		return bitmap_27;
	}

	public static Bitmap smethod_46()
	{
		if (bitmap_28 == null)
		{
			bitmap_28 = smethod_3(smethod_66() + "Calibration", bool_0: true);
		}
		return bitmap_28;
	}

	public static Bitmap smethod_47()
	{
		if (bitmap_29 == null)
		{
			bitmap_29 = smethod_3(smethod_66() + "CalibrationGPC", bool_0: true);
		}
		return bitmap_29;
	}

	public static Bitmap smethod_48()
	{
		if (bitmap_30 == null)
		{
			bitmap_30 = smethod_3(smethod_66() + "Chromatogram", bool_0: true);
		}
		return bitmap_30;
	}

	public static Bitmap smethod_49()
	{
		if (bitmap_31 == null)
		{
			bitmap_31 = smethod_3(smethod_66() + "DataAcquisition", bool_0: true);
		}
		return bitmap_31;
	}

	public static Bitmap smethod_50()
	{
		if (bitmap_32 == null)
		{
			bitmap_32 = smethod_3(smethod_66() + "DeviceMonitor", bool_0: true);
		}
		return bitmap_32;
	}

	public static Bitmap smethod_51()
	{
		if (bitmap_33 == null)
		{
			bitmap_33 = smethod_3(smethod_66() + "Instru1", bool_0: true);
		}
		return bitmap_33;
	}

	public static Bitmap smethod_52()
	{
		if (bitmap_34 == null)
		{
			bitmap_34 = smethod_3(smethod_66() + "Instru2", bool_0: true);
		}
		return bitmap_34;
	}

	public static Bitmap smethod_53()
	{
		if (bitmap_35 == null)
		{
			bitmap_35 = smethod_3(smethod_66() + "Instru3", bool_0: true);
		}
		return bitmap_35;
	}

	public static Bitmap smethod_54()
	{
		if (bitmap_36 == null)
		{
			bitmap_36 = smethod_3(smethod_66() + "Instru4", bool_0: true);
		}
		return bitmap_36;
	}

	public static Bitmap smethod_55()
	{
		if (bitmap_37 == null)
		{
			bitmap_37 = smethod_3(smethod_66() + "MainForm", bool_0: true);
		}
		return bitmap_37;
	}

	public static Bitmap smethod_56()
	{
		if (bitmap_38 == null)
		{
			bitmap_38 = smethod_3(smethod_66() + "NextZoom", bool_0: true);
		}
		return bitmap_38;
	}

	public static Bitmap smethod_57()
	{
		if (bitmap_39 == null)
		{
			bitmap_39 = smethod_3(smethod_66() + "Option", bool_0: true);
		}
		return bitmap_39;
	}

	public static Bitmap smethod_58()
	{
		if (bitmap_40 == null)
		{
			bitmap_40 = smethod_3(smethod_66() + "PreviousZoom", bool_0: true);
		}
		return bitmap_40;
	}

	public static Bitmap smethod_59()
	{
		if (bitmap_41 == null)
		{
			bitmap_41 = smethod_3(smethod_66() + "ReportSetup", bool_0: true);
		}
		return bitmap_41;
	}

	public static Bitmap smethod_60()
	{
		if (bitmap_42 == null)
		{
			bitmap_42 = smethod_3(smethod_66() + "RptLink", bool_0: true);
		}
		return bitmap_42;
	}

	public static Bitmap smethod_61()
	{
		if (bitmap_43 == null)
		{
			bitmap_43 = smethod_3(smethod_66() + "SequenceAnalysis", bool_0: true);
		}
		return bitmap_43;
	}

	public static Bitmap smethod_62()
	{
		if (bitmap_44 == null)
		{
			bitmap_44 = smethod_3(smethod_66() + "SingleAnalysis", bool_0: true);
		}
		return bitmap_44;
	}

	public static Bitmap smethod_63()
	{
		if (bitmap_45 == null)
		{
			bitmap_45 = smethod_3(smethod_66() + "Unzoom", bool_0: true);
		}
		return bitmap_45;
	}

	private static string smethod_64()
	{
		if (string_56 == "")
		{
			string_56 = Class49.GetStartPath() + "Station\\ColumnsSetup\\";
		}
		return string_56;
	}

	private static string smethod_65()
	{
		if (string_57 == "")
		{
			string_57 = Class49.GetStartPath() + "Station\\Common\\";
		}
		return string_57;
	}

	private static string smethod_66()
	{
		if (string_58 == "")
		{
			string_58 = Class49.GetStartPath() + "Station\\General\\";
		}
		return string_58;
	}

	public static string smethod_67()
	{
		if (string_59 == "")
		{
			string_59 = Class49.GetStartPath() + "Station\\InstruPic\\";
		}
		return string_59;
	}
}
