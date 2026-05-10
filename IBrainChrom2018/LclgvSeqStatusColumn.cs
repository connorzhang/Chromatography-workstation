using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvSeqStatusColumn : DataGridViewColumn
{
	public LclgvSeqStatusColumn()
	{
		CellTemplate = new LclgvSeqStatusCell();
	}
}
