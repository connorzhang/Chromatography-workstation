using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvCheckBoxColumn : DataGridViewColumn
{
	public LclgvCheckBoxColumn()
	{
		CellTemplate = new LclgvCheckBoxCell();
	}
}
