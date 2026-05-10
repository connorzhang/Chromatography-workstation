using System.Drawing;

namespace IBrainChrom2018;

internal static class IBrainColor
{
	public static Color GetColor(Color color_0, int int_0)
	{
		int r = color_0.R;
		int g = color_0.G;
		int b = color_0.B;
		r += int_0;
		g += int_0;
		b += int_0;
		Class49.SafeValueCheck(ref r, 0, 254);
		Class49.SafeValueCheck(ref g, 0, 254);
		Class49.SafeValueCheck(ref b, 0, 254);
		return Color.FromArgb(r, g, b);
	}

	public static Color RandomColor()
	{
		return Color.FromArgb(Class49.random_0.Next(0, 150), Class49.random_0.Next(0, 150), Class49.random_0.Next(0, 150));
	}
}
