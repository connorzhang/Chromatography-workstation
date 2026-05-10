using System;
using System.Drawing;

namespace IBrainChrom2018;

public class GvInfos
{
	public const int noWidth = 45;

	public const int stringWidth = 115;

	public const int valueWidth = 80;

	public StringAlignment[] colAligns = new StringAlignment[0];

	public string[] colHdrTxts = new string[0];

	public string[] colNames = new string[0];

	public int[] colWidths = new int[0];

	public int ColCount => colNames.Length;

	public void SetLength(int length)
	{
		Array.Resize(ref colNames, length);
		Array.Resize(ref colWidths, length);
		Array.Resize(ref colHdrTxts, length);
		Array.Resize(ref colAligns, length);
	}
}
