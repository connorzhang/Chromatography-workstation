using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvComboBoxColumn : DataGridViewComboBoxColumn
{
	public LclgvComboBoxColumn()
	{
		CellTemplate = new LclgvComboBoxCell();
	}
}
