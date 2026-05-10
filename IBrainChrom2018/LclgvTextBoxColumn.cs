using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvTextBoxColumn : DataGridViewColumn
{
	public LclgvTextBoxColumn()
	{
		CellTemplate = new LclgvTextBoxCell();
	}
}
