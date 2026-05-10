using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvItgOprtColumn : DataGridViewComboBoxColumn
{
	public LclgvItgOprtColumn()
	{
		CellTemplate = new LclgvItgOprtCell();
	}
}
