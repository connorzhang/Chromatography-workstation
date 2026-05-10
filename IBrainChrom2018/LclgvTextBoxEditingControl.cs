using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvTextBoxEditingControl : DataGridViewTextBoxEditingControl
{
	protected override bool ProcessCmdKey(ref Message message_0, Keys keyData)
	{
		if (keyData == Keys.Return)
		{
			EditingControlDataGridView.EndEdit();
			return true;
		}
		return ProcessCmdKey(ref message_0, keyData);
	}
}
