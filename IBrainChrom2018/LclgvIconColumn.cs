using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvIconColumn : DataGridViewColumn
{
	public LclgvIconColumn()
	{
		CellTemplate = new LclgvIconCell();
	}
}
