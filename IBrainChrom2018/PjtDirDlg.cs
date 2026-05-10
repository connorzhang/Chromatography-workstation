using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class PjtDirDlg : LclDialog
{
	private const string string_0 = "分析[试样]";

	private const string string_1 = "校正[标样]";

	private const string string_2 = "工程名";

	private const string string_3 = "工程设置";

	private const string string_4 = "Analysis";

	private const string string_5 = "Calibration";

	private const string string_6 = "Project Name";

	private const string string_7 = "Project Setup";

	private LclButton btnNew;

	private LclButton btnOpen;

	private LclButton btnSave;

	private LclButton btnSaveAs;

	private IContainer icontainer_1;

	private PjtDir pjtDir_0;

	private InstruPjtDirsDlg instruPjtDirsDlg_0 = new InstruPjtDirsDlg();

	private LclLabel lbAnalysis;

	private LclLabel lbCalibration;

	private LclLabel lbProjectName;

	private PjtDir pjtDir_1;

	private LclTextBox tbAnalysis;

	private LclTextBox tbCalibration;

	private LclTextBox tbProjectName;

	public PjtDirDlg()
	{
		InitializeComponent();
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		method_0(btnNew.Text, bool_1: false, bool_2: true, DlgOpenStyle.New);
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		method_0(btnOpen.Text, bool_1: false, bool_2: false, DlgOpenStyle.Open);
	}

	private void btnSaveAs_Click(object sender, EventArgs e)
	{
		method_0(btnSaveAs.Text, bool_1: true, bool_2: true, DlgOpenStyle.SaveAs);
	}

	private void method_0(string string_8, bool bool_1, bool bool_2, DlgOpenStyle dlgOpenStyle_0)
	{
		instruPjtDirsDlg_0.instrument = instrument;
		pjtDir_1 = instruPjtDirsDlg_0.ShowDialog(pjtDir_0, string_8, bool_1, bool_2, dlgOpenStyle_0);
		base.DialogResult = DialogResult.OK;
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
		this.lbProjectName = new IBrainChrom2018.LclLabel();
		this.lbCalibration = new IBrainChrom2018.LclLabel();
		this.lbAnalysis = new IBrainChrom2018.LclLabel();
		this.tbProjectName = new IBrainChrom2018.LclTextBox();
		this.tbCalibration = new IBrainChrom2018.LclTextBox();
		this.tbAnalysis = new IBrainChrom2018.LclTextBox();
		this.btnOpen = new IBrainChrom2018.LclButton();
		this.btnSave = new IBrainChrom2018.LclButton();
		this.btnSaveAs = new IBrainChrom2018.LclButton();
		this.btnNew = new IBrainChrom2018.LclButton();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(234, 67);
		base.btnOK.Text = "确认";
		base.btnOK.Visible = false;
		base.btnCancel.Location = new System.Drawing.Point(234, 96);
		base.btnCancel.Text = "取消";
		base.btnCancel.Visible = false;
		base.btnHelp.Location = new System.Drawing.Point(234, 125);
		base.btnHelp.Text = "帮助";
		base.btnHelp.Visible = false;
		this.lbProjectName.AutoSize = true;
		this.lbProjectName.Location = new System.Drawing.Point(12, 18);
		this.lbProjectName.Name = "lbProjectName";
		this.lbProjectName.Size = new System.Drawing.Size(59, 12);
		this.lbProjectName.TabIndex = 1;
		this.lbProjectName.Text = "lclLabel1";
		this.lbCalibration.AutoSize = true;
		this.lbCalibration.Location = new System.Drawing.Point(12, 200);
		this.lbCalibration.Name = "lbCalibration";
		this.lbCalibration.Size = new System.Drawing.Size(59, 12);
		this.lbCalibration.TabIndex = 1;
		this.lbCalibration.Text = "lclLabel1";
		this.lbAnalysis.AutoSize = true;
		this.lbAnalysis.Location = new System.Drawing.Point(196, 200);
		this.lbAnalysis.Name = "lbAnalysis";
		this.lbAnalysis.Size = new System.Drawing.Size(59, 12);
		this.lbAnalysis.TabIndex = 1;
		this.lbAnalysis.Text = "lclLabel1";
		this.tbProjectName.BackColor = System.Drawing.Color.LightGray;
		this.tbProjectName.Location = new System.Drawing.Point(92, 15);
		this.tbProjectName.Name = "tbProjectName";
		this.tbProjectName.ReadOnly = true;
		this.tbProjectName.Size = new System.Drawing.Size(235, 21);
		this.tbProjectName.TabIndex = 7;
		this.tbCalibration.BackColor = System.Drawing.Color.LightGray;
		this.tbCalibration.Location = new System.Drawing.Point(12, 215);
		this.tbCalibration.Name = "tbCalibration";
		this.tbCalibration.ReadOnly = true;
		this.tbCalibration.Size = new System.Drawing.Size(175, 21);
		this.tbCalibration.TabIndex = 7;
		this.tbAnalysis.BackColor = System.Drawing.Color.LightGray;
		this.tbAnalysis.Location = new System.Drawing.Point(193, 215);
		this.tbAnalysis.Name = "tbAnalysis";
		this.tbAnalysis.ReadOnly = true;
		this.tbAnalysis.Size = new System.Drawing.Size(134, 21);
		this.tbAnalysis.TabIndex = 7;
		this.btnOpen.Location = new System.Drawing.Point(350, 15);
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(75, 23);
		this.btnOpen.TabIndex = 8;
		this.btnOpen.Text = "Open";
		this.btnOpen.UseVisualStyleBackColor = true;
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.btnSave.Enabled = false;
		this.btnSave.Location = new System.Drawing.Point(350, 41);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(75, 23);
		this.btnSave.TabIndex = 8;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSaveAs.Location = new System.Drawing.Point(350, 67);
		this.btnSaveAs.Name = "btnSaveAs";
		this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
		this.btnSaveAs.TabIndex = 8;
		this.btnSaveAs.Text = "SaveAs";
		this.btnSaveAs.UseVisualStyleBackColor = true;
		this.btnSaveAs.Click += new System.EventHandler(btnSaveAs_Click);
		this.btnNew.Location = new System.Drawing.Point(350, 94);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(75, 23);
		this.btnNew.TabIndex = 8;
		this.btnNew.Text = "New";
		this.btnNew.UseVisualStyleBackColor = true;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(437, 250);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.btnSaveAs);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.btnOpen);
		base.Controls.Add(this.tbAnalysis);
		base.Controls.Add(this.tbCalibration);
		base.Controls.Add(this.tbProjectName);
		base.Controls.Add(this.lbProjectName);
		base.Controls.Add(this.lbCalibration);
		base.Controls.Add(this.lbAnalysis);
		base.Name = "PjtDirDlg";
		base.Controls.SetChildIndex(this.lbAnalysis, 0);
		base.Controls.SetChildIndex(this.lbCalibration, 0);
		base.Controls.SetChildIndex(this.lbProjectName, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.tbProjectName, 0);
		base.Controls.SetChildIndex(this.tbCalibration, 0);
		base.Controls.SetChildIndex(this.tbAnalysis, 0);
		base.Controls.SetChildIndex(this.btnOpen, 0);
		base.Controls.SetChildIndex(this.btnSave, 0);
		base.Controls.SetChildIndex(this.btnSaveAs, 0);
		base.Controls.SetChildIndex(this.btnNew, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "工程设置";
			lbProjectName.Text = "工程名";
			lbCalibration.Text = "校正[标样]";
			lbAnalysis.Text = "分析[试样]";
			btnOpen.Text = "打开...";
			btnSave.Text = "保存";
			btnSaveAs.Text = "另存...";
			btnNew.Text = "新建";
			break;
		case SysLanguage.EN:
			Text = "Project Setup";
			lbProjectName.Text = "Project Name";
			lbCalibration.Text = "Calibration";
			lbAnalysis.Text = "Analysis";
			btnOpen.Text = "Open...";
			btnSave.Text = "Save";
			btnSaveAs.Text = "Save as...";
			btnNew.Text = "New";
			break;
		}
	}

	public PjtDir ShowDialog(PjtDir pjtDir)
	{
		pjtDir_0 = pjtDir;
		btnSaveAs.Enabled = pjtDir != null;
		if (pjtDir != null)
		{
			tbProjectName.Text = pjtDir.projectName;
		}
		else
		{
			tbProjectName.Text = "";
		}
		tbCalibration.Text = "Calib";
		tbAnalysis.Text = "Data";
		pjtDir_1 = null;
		ShowDialog();
		return pjtDir_1;
	}
}
