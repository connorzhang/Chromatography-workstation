using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvCpmdNameColumn : DataGridViewComboBoxColumn
{
	public LclgvCpmdNameColumn()
	{
		CellTemplate = new LclgvRespStyleCell.LclgvCpmdNameStyleCell();
	}
}
