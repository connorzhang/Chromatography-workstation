using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvTextBoxCtxBtnColumn : DataGridViewColumn
{
	public LclgvTextBoxCtxBtnColumn(object linkObject)
	{
		(CellTemplate = new LclgvTextBoxCtxBtnCell()).Tag = linkObject;
	}
}
