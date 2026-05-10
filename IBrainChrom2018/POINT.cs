using System.Drawing;

namespace IBrainChrom2018;

public struct POINT
{
	public int x;

	public int y;

	public POINT(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public POINT(Point point)
	{
		x = point.X;
		y = point.Y;
	}
}
