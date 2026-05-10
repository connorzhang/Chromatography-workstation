using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Instrus_ATDirsDialog : LclDialog
{
	private const int int_0 = 25;

	private const string string_0 = "全部同仪器1";

	private const string string_1 = "选择日志目录";

	private const string string_2 = "选择仪器目录";

	private const string string_3 = "仪器目录";

	private const string string_4 = "All As Instrument 1";

	private const string string_5 = "Select Audit Trail Directory";

	private const string string_6 = "Select Instrument Directory";

	private const string string_7 = "Instrument Directories";

	private LclButton btnAuditTrail;

	private LclButton btnDefault;

	private LclButton btnInstru1;

	private LclButton btnInstru2;

	private LclButton btnInstru3;

	private LclButton btnInstru4;

	private LclButton[] lclButton_0 = new LclButton[0];

	private LclCheckBox cbAllAsInstru1;

	private IContainer icontainer_1;

	private FolderBrowserDialog folderBrowserDialog_0 = new FolderBrowserDialog();

	public static Instrus_ATDirs instrus_ATDirs;

	private LclLabel lbAuditTrail;

	private LclLabel lbInstru1;

	private LclLabel lbInstru2;

	private LclLabel lbInstru3;

	private LclLabel lbInstru4;

	public LclLabel[] lbInstrus = new LclLabel[0];

	private Instrus_ATDirs instrus_ATDirs_0;

	private LclTextBox tbAuditTrail;

	private LclTextBox tbInstru1;

	private LclTextBox tbInstru2;

	private LclTextBox tbInstru3;

	private LclTextBox tbInstru4;

	private LclTextBox[] lclTextBox_0 = new LclTextBox[0];

	public Instrus_ATDirsDialog()
	{
		InitializeComponent();
		int num = SysCfgDlg.sysConfig.pageInstrus.Length;
		Array.Resize(ref lbInstrus, num);
		Array.Resize(ref lclTextBox_0, num);
		Array.Resize(ref lclButton_0, num);
		lbInstrus[0] = lbInstru1;
		lclTextBox_0[0] = tbInstru1;
		lclButton_0[0] = btnInstru1;
		lbInstrus[1] = lbInstru2;
		lclTextBox_0[1] = tbInstru2;
		lclButton_0[1] = btnInstru2;
		lbInstrus[2] = lbInstru3;
		lclTextBox_0[2] = tbInstru3;
		lclButton_0[2] = btnInstru3;
		lbInstrus[3] = lbInstru4;
		lclTextBox_0[3] = tbInstru4;
		lclButton_0[3] = btnInstru4;
		for (int i = 0; i < num; i++)
		{
			lclButton_0[i].Click += method_0;
			lbInstrus[i].Text = "*";
		}
	}

	private void btnAuditTrail_Click(object sender, EventArgs e)
	{
		folderBrowserDialog_0.Description = Lang.PS("选择日志目录", "Select Audit Trail Directory");
		folderBrowserDialog_0.SelectedPath = tbAuditTrail.Text;
		if (folderBrowserDialog_0.ShowDialog() == DialogResult.OK)
		{
			string text = folderBrowserDialog_0.SelectedPath;
			if (!text.EndsWith("\\"))
			{
				text += "\\";
			}
			tbAuditTrail.Text = text;
		}
	}

	private void btnDefault_Click(object sender, EventArgs e)
	{
		instrus_ATDirs_0.DefaultDirections();
		method_2();
	}

	private void method_0(object sender, EventArgs e)
	{
		folderBrowserDialog_0.Description = Lang.PS("选择仪器目录", "Select Instrument Directory");
		if (sender == btnInstru1)
		{
			folderBrowserDialog_0.SelectedPath = tbInstru1.Text;
		}
		else if (sender == btnInstru2)
		{
			folderBrowserDialog_0.SelectedPath = tbInstru2.Text;
		}
		else if (sender == btnInstru3)
		{
			folderBrowserDialog_0.SelectedPath = tbInstru3.Text;
		}
		else if (sender == btnInstru4)
		{
			folderBrowserDialog_0.SelectedPath = tbInstru4.Text;
		}
		if (folderBrowserDialog_0.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string text = folderBrowserDialog_0.SelectedPath;
		if (!text.EndsWith("\\"))
		{
			text += "\\";
		}
		for (int i = 0; i < lclButton_0.Length; i++)
		{
			if (lclButton_0[i] == sender)
			{
				lclTextBox_0[i].Text = text;
				cbAllAsInstru1_Click(null, null);
				break;
			}
		}
	}

	private void method_1(object sender, EventArgs e)
	{
		method_3();
		if (instrus_ATDirs_0.CreateDirectories())
		{
			instrus_ATDirs.LoadFromObject(instrus_ATDirs_0);
			base.DialogResult = DialogResult.OK;
			MainForm.stationAdtTrlForm.CreateAndInit();
		}
	}

	private void cbAllAsInstru1_Click(object sender, EventArgs e)
	{
		for (int i = 1; i < lclTextBox_0.Length; i++)
		{
			lclTextBox_0[i].Enabled = !cbAllAsInstru1.Checked;
			lclButton_0[i].Enabled = !cbAllAsInstru1.Checked;
		}
		if (cbAllAsInstru1.Checked)
		{
			for (int j = 1; j < lclTextBox_0.Length; j++)
			{
				lclTextBox_0[j].Text = lclTextBox_0[0].Text;
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	public static string GetInstruDir(int instruPageNo)
	{
		return instrus_ATDirs.dirInstrus[instruPageNo];
	}

	private void InitializeComponent()
	{
		this.lbAuditTrail = new IBrainChrom2018.LclLabel();
		this.cbAllAsInstru1 = new IBrainChrom2018.LclCheckBox();
		this.tbAuditTrail = new IBrainChrom2018.LclTextBox();
		this.btnDefault = new IBrainChrom2018.LclButton();
		this.btnAuditTrail = new IBrainChrom2018.LclButton();
		this.lbInstru1 = new IBrainChrom2018.LclLabel();
		this.btnInstru1 = new IBrainChrom2018.LclButton();
		this.tbInstru1 = new IBrainChrom2018.LclTextBox();
		this.lbInstru2 = new IBrainChrom2018.LclLabel();
		this.btnInstru2 = new IBrainChrom2018.LclButton();
		this.tbInstru2 = new IBrainChrom2018.LclTextBox();
		this.lbInstru3 = new IBrainChrom2018.LclLabel();
		this.btnInstru3 = new IBrainChrom2018.LclButton();
		this.tbInstru3 = new IBrainChrom2018.LclTextBox();
		this.lbInstru4 = new IBrainChrom2018.LclLabel();
		this.btnInstru4 = new IBrainChrom2018.LclButton();
		this.tbInstru4 = new IBrainChrom2018.LclTextBox();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(158, 167);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_1);
		base.btnCancel.Location = new System.Drawing.Point(251, 167);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(343, 167);
		base.btnHelp.Text = "帮助";
		this.lbAuditTrail.Location = new System.Drawing.Point(8, 136);
		this.lbAuditTrail.Name = "lbAuditTrail";
		this.lbAuditTrail.Size = new System.Drawing.Size(59, 12);
		this.lbAuditTrail.TabIndex = 1;
		this.lbAuditTrail.Text = "lclLabel1";
		this.cbAllAsInstru1.AutoSize = true;
		this.cbAllAsInstru1.Location = new System.Drawing.Point(78, 114);
		this.cbAllAsInstru1.Name = "cbAllAsInstru1";
		this.cbAllAsInstru1.Size = new System.Drawing.Size(96, 16);
		this.cbAllAsInstru1.TabIndex = 2;
		this.cbAllAsInstru1.Text = "lclCheckBox1";
		this.cbAllAsInstru1.UseVisualStyleBackColor = true;
		this.cbAllAsInstru1.Click += new System.EventHandler(cbAllAsInstru1_Click);
		this.tbAuditTrail.Location = new System.Drawing.Point(78, 133);
		this.tbAuditTrail.Name = "tbAuditTrail";
		this.tbAuditTrail.Size = new System.Drawing.Size(315, 21);
		this.tbAuditTrail.TabIndex = 3;
		this.btnDefault.Location = new System.Drawing.Point(36, 168);
		this.btnDefault.Name = "btnDefault";
		this.btnDefault.Size = new System.Drawing.Size(75, 23);
		this.btnDefault.TabIndex = 5;
		this.btnDefault.Text = "Default";
		this.btnDefault.UseVisualStyleBackColor = true;
		this.btnDefault.Click += new System.EventHandler(btnDefault_Click);
		this.btnAuditTrail.Location = new System.Drawing.Point(399, 131);
		this.btnAuditTrail.Name = "btnAuditTrail";
		this.btnAuditTrail.Size = new System.Drawing.Size(45, 23);
		this.btnAuditTrail.TabIndex = 5;
		this.btnAuditTrail.Text = "...";
		this.btnAuditTrail.UseVisualStyleBackColor = true;
		this.btnAuditTrail.Click += new System.EventHandler(btnAuditTrail_Click);
		this.lbInstru1.AutoSize = true;
		this.lbInstru1.Location = new System.Drawing.Point(8, 15);
		this.lbInstru1.Name = "lbInstru1";
		this.lbInstru1.Size = new System.Drawing.Size(59, 12);
		this.lbInstru1.TabIndex = 1;
		this.lbInstru1.Text = "lclLabel1";
		this.btnInstru1.Location = new System.Drawing.Point(399, 10);
		this.btnInstru1.Name = "btnInstru1";
		this.btnInstru1.Size = new System.Drawing.Size(45, 23);
		this.btnInstru1.TabIndex = 5;
		this.btnInstru1.Text = "...";
		this.btnInstru1.UseVisualStyleBackColor = true;
		this.tbInstru1.Location = new System.Drawing.Point(78, 12);
		this.tbInstru1.Name = "tbInstru1";
		this.tbInstru1.Size = new System.Drawing.Size(315, 21);
		this.tbInstru1.TabIndex = 3;
		this.lbInstru2.AutoSize = true;
		this.lbInstru2.Location = new System.Drawing.Point(8, 40);
		this.lbInstru2.Name = "lbInstru2";
		this.lbInstru2.Size = new System.Drawing.Size(59, 12);
		this.lbInstru2.TabIndex = 1;
		this.lbInstru2.Text = "lclLabel1";
		this.btnInstru2.Location = new System.Drawing.Point(399, 35);
		this.btnInstru2.Name = "btnInstru2";
		this.btnInstru2.Size = new System.Drawing.Size(45, 23);
		this.btnInstru2.TabIndex = 5;
		this.btnInstru2.Text = "...";
		this.btnInstru2.UseVisualStyleBackColor = true;
		this.tbInstru2.Location = new System.Drawing.Point(78, 37);
		this.tbInstru2.Name = "tbInstru2";
		this.tbInstru2.Size = new System.Drawing.Size(315, 21);
		this.tbInstru2.TabIndex = 3;
		this.lbInstru3.AutoSize = true;
		this.lbInstru3.Location = new System.Drawing.Point(8, 65);
		this.lbInstru3.Name = "lbInstru3";
		this.lbInstru3.Size = new System.Drawing.Size(59, 12);
		this.lbInstru3.TabIndex = 1;
		this.lbInstru3.Text = "lclLabel1";
		this.btnInstru3.Location = new System.Drawing.Point(399, 60);
		this.btnInstru3.Name = "btnInstru3";
		this.btnInstru3.Size = new System.Drawing.Size(45, 23);
		this.btnInstru3.TabIndex = 5;
		this.btnInstru3.Text = "...";
		this.btnInstru3.UseVisualStyleBackColor = true;
		this.tbInstru3.Location = new System.Drawing.Point(78, 62);
		this.tbInstru3.Name = "tbInstru3";
		this.tbInstru3.Size = new System.Drawing.Size(315, 21);
		this.tbInstru3.TabIndex = 3;
		this.lbInstru4.AutoSize = true;
		this.lbInstru4.Location = new System.Drawing.Point(8, 90);
		this.lbInstru4.Name = "lbInstru4";
		this.lbInstru4.Size = new System.Drawing.Size(59, 12);
		this.lbInstru4.TabIndex = 1;
		this.lbInstru4.Text = "lclLabel1";
		this.btnInstru4.Location = new System.Drawing.Point(399, 85);
		this.btnInstru4.Name = "btnInstru4";
		this.btnInstru4.Size = new System.Drawing.Size(45, 23);
		this.btnInstru4.TabIndex = 5;
		this.btnInstru4.Text = "...";
		this.btnInstru4.UseVisualStyleBackColor = true;
		this.tbInstru4.Location = new System.Drawing.Point(78, 87);
		this.tbInstru4.Name = "tbInstru4";
		this.tbInstru4.Size = new System.Drawing.Size(315, 21);
		this.tbInstru4.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(450, 200);
		base.Controls.Add(this.tbInstru4);
		base.Controls.Add(this.tbInstru3);
		base.Controls.Add(this.tbInstru2);
		base.Controls.Add(this.btnInstru4);
		base.Controls.Add(this.tbInstru1);
		base.Controls.Add(this.btnInstru3);
		base.Controls.Add(this.tbAuditTrail);
		base.Controls.Add(this.btnInstru2);
		base.Controls.Add(this.lbInstru4);
		base.Controls.Add(this.lbInstru3);
		base.Controls.Add(this.btnInstru1);
		base.Controls.Add(this.lbInstru2);
		base.Controls.Add(this.btnDefault);
		base.Controls.Add(this.lbInstru1);
		base.Controls.Add(this.btnAuditTrail);
		base.Controls.Add(this.lbAuditTrail);
		base.Controls.Add(this.cbAllAsInstru1);
		base.Name = "Instrus_ATDirsDialog";
		base.Load += new System.EventHandler(Instrus_ATDirsDialog_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Instrus_ATDirsDialog_KeyDown);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.cbAllAsInstru1, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(this.lbAuditTrail, 0);
		base.Controls.SetChildIndex(this.btnAuditTrail, 0);
		base.Controls.SetChildIndex(this.lbInstru1, 0);
		base.Controls.SetChildIndex(this.btnDefault, 0);
		base.Controls.SetChildIndex(this.lbInstru2, 0);
		base.Controls.SetChildIndex(this.btnInstru1, 0);
		base.Controls.SetChildIndex(this.lbInstru3, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbInstru4, 0);
		base.Controls.SetChildIndex(this.btnInstru2, 0);
		base.Controls.SetChildIndex(this.tbAuditTrail, 0);
		base.Controls.SetChildIndex(this.btnInstru3, 0);
		base.Controls.SetChildIndex(this.tbInstru1, 0);
		base.Controls.SetChildIndex(this.btnInstru4, 0);
		base.Controls.SetChildIndex(this.tbInstru2, 0);
		base.Controls.SetChildIndex(this.tbInstru3, 0);
		base.Controls.SetChildIndex(this.tbInstru4, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void Instrus_ATDirsDialog_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			Class49.smethod_32("仪器目录");
		}
	}

	private void Instrus_ATDirsDialog_Load(object sender, EventArgs e)
	{
		int instrumentsNum = SysCfgDlg.sysConfig.GetInstrumentsNum();
		for (int i = 0; i < lclTextBox_0.Length; i++)
		{
			LclTextBox obj = lclTextBox_0[i];
			LclButton obj2 = lclButton_0[i];
			bool flag = (lbInstrus[i].Visible = i < instrumentsNum);
			bool visible = (obj2.Visible = flag);
			obj.Visible = visible;
		}
		for (int j = 0; j < lbInstrus.Length; j++)
		{
			lbInstrus[j].Text = MainForm.lbNames[j].Text;
		}
		cbAllAsInstru1.Checked = false;
		instrus_ATDirs_0.LoadFromObject(instrus_ATDirs);
		method_2();
		for (int k = 1; k < lclTextBox_0.Length; k++)
		{
			lclTextBox_0[k].Text = instrus_ATDirs_0.dirInstrus[k];
		}
		cbAllAsInstru1_Click(null, null);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "仪器目录";
			cbAllAsInstru1.Text = "全部同仪器1";
			lbAuditTrail.Text = "日志";
			btnDefault.Text = "默认";
			break;
		case SysLanguage.EN:
			Text = "Instrument Directories";
			cbAllAsInstru1.Text = "All As Instrument 1";
			lbAuditTrail.Text = "Audit Trail";
			btnDefault.Text = "Default";
			break;
		}
	}

	public new void ShowDialog()
	{
		if (Class49.loginDlg_0.ShowDialog(AccessType.OpenConfiguration))
		{
			base.ShowDialog();
		}
	}

	private void method_2()
	{
		lclTextBox_0[0].Text = instrus_ATDirs_0.dirInstrus[0];
		tbAuditTrail.Text = instrus_ATDirs_0.dirAuditTrail;
	}

	private void method_3()
	{
		for (int i = 0; i < instrus_ATDirs_0.dirInstrus.Length; i++)
		{
			instrus_ATDirs_0.dirInstrus[i] = lclTextBox_0[i].Text;
		}
		instrus_ATDirs_0.dirAuditTrail = tbAuditTrail.Text;
	}
}
