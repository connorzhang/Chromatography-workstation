using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FrmTip : Form
{
	private IContainer icontainer_0;

	private Label label2;

	public Label label1;

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
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Blue;
		this.label1.Location = new System.Drawing.Point(105, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = IBrainChrom2018.Lang.PS("正在查找....", "Looking for....");
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label2.Location = new System.Drawing.Point(12, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(287, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = IBrainChrom2018.Lang.PS("*查找时间取决指定查找目录内文件以及子文件夹多少....", "The lookup time depends on how many files and subfolders you specify to find in the directory");
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(308, 62);
		base.ControlBox = false;
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmTip";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = IBrainChrom2018.Lang.PS("文件查找", "File search");
		base.TopMost = true;
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FrmTip()
	{
		InitializeComponent();
	}
}
