using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SSOptDlg : LclDialog
{
	private IContainer icontainer_1;

	private LclGroupBox gbInjVolumnUnit;

	private LclLabel lbDescription;

	private LclRadioButton rbMl;

	private LclRadioButton rbUl;

	private LclTextBox tbDescription;

	public SSOptDlg()
	{
		InitializeComponent();
		gbInjVolumnUnit.Text = Lang.PS("进样单位", "Injection Uint");
		lbDescription.Text = Lang.PS("描述", "Description");
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.lbDescription = new IBrainChrom2018.LclLabel();
		this.gbInjVolumnUnit = new IBrainChrom2018.LclGroupBox();
		this.rbMl = new IBrainChrom2018.LclRadioButton();
		this.rbUl = new IBrainChrom2018.LclRadioButton();
		this.tbDescription = new IBrainChrom2018.LclTextBox();
		this.gbInjVolumnUnit.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(139, 150);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(244, 150);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(37, 150);
		base.btnOK.Text = "确认";
		this.lbDescription.AutoSize = true;
		this.lbDescription.Location = new System.Drawing.Point(12, 92);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(29, 12);
		this.lbDescription.TabIndex = 5;
		this.lbDescription.Text = "描述";
		this.gbInjVolumnUnit.Controls.Add(this.rbMl);
		this.gbInjVolumnUnit.Controls.Add(this.rbUl);
		this.gbInjVolumnUnit.Location = new System.Drawing.Point(12, 12);
		this.gbInjVolumnUnit.Name = "gbInjVolumnUnit";
		this.gbInjVolumnUnit.Size = new System.Drawing.Size(125, 65);
		this.gbInjVolumnUnit.TabIndex = 7;
		this.gbInjVolumnUnit.TabStop = false;
		this.gbInjVolumnUnit.Text = "进样单位";
		this.rbMl.AutoSize = true;
		this.rbMl.Location = new System.Drawing.Point(12, 40);
		this.rbMl.Name = "rbMl";
		this.rbMl.Size = new System.Drawing.Size(35, 16);
		this.rbMl.TabIndex = 0;
		this.rbMl.TabStop = true;
		this.rbMl.Text = "ml";
		this.rbMl.UseVisualStyleBackColor = true;
		this.rbUl.AutoSize = true;
		this.rbUl.Location = new System.Drawing.Point(12, 20);
		this.rbUl.Name = "rbUl";
		this.rbUl.Size = new System.Drawing.Size(41, 16);
		this.rbUl.TabIndex = 0;
		this.rbUl.TabStop = true;
		this.rbUl.Text = "μl";
		this.rbUl.UseVisualStyleBackColor = true;
		this.tbDescription.Location = new System.Drawing.Point(12, 107);
		this.tbDescription.Name = "tbDescription";
		this.tbDescription.Size = new System.Drawing.Size(365, 21);
		this.tbDescription.TabIndex = 6;
		base.ClientSize = new System.Drawing.Size(387, 190);
		base.Controls.Add(this.lbDescription);
		base.Controls.Add(this.gbInjVolumnUnit);
		base.Controls.Add(this.tbDescription);
		base.Name = "SSOptDlg";
		this.Text = "单针序列属性";
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.tbDescription, 0);
		base.Controls.SetChildIndex(this.gbInjVolumnUnit, 0);
		base.Controls.SetChildIndex(this.lbDescription, 0);
		this.gbInjVolumnUnit.ResumeLayout(false);
		this.gbInjVolumnUnit.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void method_0(AccStyle accStyle_0, SSOpt ssopt_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
			rbUl.Checked = ssopt_0.injVolumnUnit == VolumnUnits.const_0;
			rbMl.Checked = !rbUl.Checked;
			tbDescription.Text = ssopt_0.description;
			break;
		case AccStyle.Write:
			if (rbUl.Checked)
			{
				ssopt_0.injVolumnUnit = VolumnUnits.const_0;
			}
			else
			{
				ssopt_0.injVolumnUnit = VolumnUnits.const_1;
			}
			ssopt_0.description = tbDescription.Text;
			break;
		}
	}

	public DialogResult ShowDialog(SSOpt ssOpt)
	{
		method_0(AccStyle.Read, ssOpt);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_0(AccStyle.Write, ssOpt);
		}
		return dialogResult;
	}
}
