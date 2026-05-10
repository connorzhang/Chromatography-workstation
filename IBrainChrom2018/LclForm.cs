using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclForm : Form
{
	private IContainer icontainer_0;

	public LclForm()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(401, 198);
		base.KeyPreview = true;
		base.Name = "LclForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "LclForm";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(LclForm_KeyDown);
		base.ResumeLayout(false);
	}

	private void LclForm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Close();
		}
	}

	protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
	{
		cancelEventArgs_0.Cancel = true;
		base.Visible = false;
	}
}
