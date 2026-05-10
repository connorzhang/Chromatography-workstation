using System.Drawing;

namespace IBrainChrom2018;

public class GvPrtInfos
{
	public StringAlignment[][] colAligns = new StringAlignment[0][];

	public string[][] colHdrTxts = new string[0][];

	public string[][] colNames = new string[0][];

	public float[][] colWidths = new float[0][];

	public float[] float_0 = new float[0];

	public float[] float_1 = new float[0];

	public int DimCount => colNames.Length;

	public int PartsNum => colNames.GetLength(0);
}
