using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvRespStyleColumn : DataGridViewComboBoxColumn
{
	public LclgvRespStyleColumn()
	{
		CellTemplate = new LclgvRespStyleCell();
	}
}
