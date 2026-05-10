using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class ResourceImageLoad
{
	public static void DrawToGraphic(Graphics graphics, Image image, Point point)
	{
		if (image != null)
		{
			graphics.DrawImage(image, point);
		}
	}

	public static void DrawToGraphic(Graphics graphics, Image image, Rectangle rectangle)
	{
		if (image != null)
		{
			graphics.DrawImage(image, rectangle);
		}
	}

	private static Icon LoadIcon(Bitmap bitmap)
	{
		if (bitmap == null)
		{
			return null;
		}
		return Icon.FromHandle(bitmap.GetHicon());
	}

	private static Icon LoadIcon(string pathFile)
	{
		Bitmap bitmap = LoadBitmap(pathFile, bMakeTransparent: true);
		if (bitmap == null)
		{
			return null;
		}
		return Icon.FromHandle(bitmap.GetHicon());
	}

	public static Bitmap LoadBitmap(string pathFile, bool bMakeTransparent = false)
	{
		string text = pathFile;
		if (!File.Exists(text))
		{
			string text2 = pathFile + ".png";
			FileInfo fileInfo = new FileInfo(text2);
			string text3 = pathFile + ".jpg";
			FileInfo fileInfo2 = new FileInfo(text3);
			string text4 = pathFile + ".bmp";
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
		if (bMakeTransparent)
		{
			Color pixel = bitmap.GetPixel(0, 0);
			bitmap.MakeTransparent(pixel);
		}
		return bitmap;
	}

	public static void SetCtrlBitmap(object object_0, Bitmap bitmap)
	{
		if (bitmap != null)
		{
			if (object_0 is Button)
			{
				Button button = object_0 as Button;
				button.ImageAlign = ContentAlignment.MiddleCenter;
				button.Image = bitmap;
			}
			if (object_0 is ToolStripButton)
			{
				ToolStripButton toolStripButton = object_0 as ToolStripButton;
				toolStripButton.ImageScaling = ToolStripItemImageScaling.None;
				toolStripButton.Image = bitmap;
			}
			if (object_0 is ToolStripSplitButton)
			{
				ToolStripSplitButton toolStripSplitButton = object_0 as ToolStripSplitButton;
				toolStripSplitButton.ImageScaling = ToolStripItemImageScaling.None;
				toolStripSplitButton.Image = bitmap;
			}
			if (object_0 is ToolStripMenuItem)
			{
				ToolStripMenuItem toolStripMenuItem = object_0 as ToolStripMenuItem;
				toolStripMenuItem.Image = bitmap;
			}
		}
	}

	public static string ExePath()
	{
		FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
		return fileInfo.Directory.ToString() + "\\";
	}

	public static string PathInstruPic()
	{
		return ExePath() + "Station\\InstruPic\\";
	}
}
