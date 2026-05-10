using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal class SystemImageListResource1
{
	private const string string_0 = "Exclude";

	private const string string_1 = "Include";

	public const string string_2 = "RptSetup\\";

	public static int int_0 = -1;

	public static int int_1 = -1;

	private static ImageList imageList_0 = null;

	public static ImageList smethod_0()
	{
		if (imageList_0 == null)
		{
			imageList_0 = new ImageList();
			string text = Class49.GetStartPath() + "Station\\RptSetup\\";
			Bitmap image_ = SystemIconResource.smethod_3(text + "Include", bool_0: true);
			int_1 = SystemImageListResource2.smethod_0(ref imageList_0, image_);
			image_ = SystemIconResource.smethod_3(text + "Exclude", bool_0: true);
			int_0 = SystemImageListResource2.smethod_0(ref imageList_0, image_);
		}
		return imageList_0;
	}
}
