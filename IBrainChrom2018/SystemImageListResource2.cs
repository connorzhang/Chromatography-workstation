using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal class SystemImageListResource2
{
	private const string string_0 = "AS_Ctrl";

	private const string string_1 = "AS_Sampler";

	private const string string_2 = "Dt_Channel";

	private const string string_3 = "Dt_Ctrl";

	private const string string_4 = "Dt_Spectral";

	private const string string_5 = "GC_Ctrl";

	private const string string_6 = "GC_GCs";

	private const string string_7 = "HasInstru";

	private const string string_8 = "LC_Ctrl";

	private const string string_9 = "LC_Gradient";

	private const string string_10 = "LC_Oven";

	private const string string_11 = "LC_Pump";

	private const string string_12 = "NoInstru";

	private const string string_13 = "None";

	private const string string_14 = "Set";

	public const string string_15 = "SysConfig\\";

	public static int int_0 = -1;

	public static int int_1 = -1;

	public static int int_2 = -1;

	public static int int_3 = -1;

	public static int int_4 = -1;

	public static int int_5 = -1;

	public static int int_6 = -1;

	public static int int_7 = -1;

	public static int int_8 = -1;

	public static int int_9 = -1;

	public static int int_10 = -1;

	public static int int_11 = -1;

	public static int int_12 = -1;

	public static int int_13 = -1;

	public static int int_14 = -1;

	public static int int_15 = -1;

	public static int int_16 = -1;

	private static ImageList imageList_0 = null;

	public static int smethod_0(ref ImageList imageList_1, Image image_0)
	{
		if (image_0 == null)
		{
			return -1;
		}
		imageList_1.Images.Add(image_0);
		return imageList_1.Images.Count - 1;
	}

	public static ImageList smethod_1()
	{
		if (imageList_0 == null)
		{
			imageList_0 = new ImageList();
			string text = Class49.GetStartPath() + "Station\\SysConfig\\";
			Bitmap image_ = SystemIconResource.smethod_3(text + "None", bool_0: true);
			int_15 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_21();
			int_5 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_22();
			int_6 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "HasInstru", bool_0: true);
			int_9 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "NoInstru", bool_0: true);
			int_14 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "AS_Ctrl", bool_0: true);
			int_0 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "LC_Ctrl", bool_0: true);
			int_10 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "GC_Ctrl", bool_0: true);
			int_7 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "Dt_Ctrl", bool_0: true);
			int_3 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "AS_Sampler", bool_0: true);
			int_1 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "LC_Pump", bool_0: true);
			int_13 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "LC_Gradient", bool_0: true);
			int_11 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "LC_Oven", bool_0: true);
			int_12 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "GC_GCs", bool_0: true);
			int_8 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "Dt_Channel", bool_0: true);
			int_2 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "Dt_Spectral", bool_0: true);
			int_4 = smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "Set", bool_0: true);
			int_16 = smethod_0(ref imageList_0, image_);
		}
		return imageList_0;
	}
}
