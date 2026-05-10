using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal static class IBrainCommon
{
	public static void spectraCombined(string file1, string file2, string file4, string file3)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(file1, DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(file2, DetectorStyle.General);
		Chromatogram chromatogram3 = Chromatogram.LoadFromFile2(file4, DetectorStyle.General);
		float num = 0f;
		if (chromatogram != null && chromatogram2 != null && chromatogram3 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				if (i == dotsNum)
				{
					num = chromatogram.signal.oriDots[i - 1].Y - chromatogram2.signal.oriDots[i - dotsNum].Y;
				}
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y + num;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram3.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			num = 0f;
			for (int j = dotsNum; j < chromatogram.signal.oriDots.Length; j++)
			{
				if (j == dotsNum)
				{
					num = chromatogram.signal.oriDots[j - 1].Y - chromatogram3.signal.oriDots[j - dotsNum].Y;
				}
				chromatogram.signal.oriDots[j].X = chromatogram3.signal.oriDots[j - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[j].Y = chromatogram3.signal.oriDots[j - dotsNum].Y + num;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			chromatogram.SaveToFileOld(file3);
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}

	public static void spectraCombined(string file1, string file2, string file3)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(file1, DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(file2, DetectorStyle.General);
		float num = 0f;
		if (chromatogram != null && chromatogram2 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				if (i == dotsNum)
				{
					num = chromatogram.signal.oriDots[i - 1].Y - chromatogram2.signal.oriDots[i - dotsNum].Y;
				}
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y + num;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			chromatogram.SaveToFileOld(file3);
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}
}
