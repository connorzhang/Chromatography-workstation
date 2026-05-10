using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvColorColumn : DataGridViewColumn
{
	public LclgvColorColumn()
	{
		CellTemplate = new LclgvColorCell();
	}
}
