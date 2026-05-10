using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DetectorControl1SetupForm : CtrlSetupDlg
{
	private const string string_2 = "通道";

	private const string string_3 = "翻转信号";

	private const string string_4 = "Channel";

	private const string string_5 = "Invers Signal";

	private LclComboBox cbChannelNum;

	private LclCheckBox cbInversSignal1;

	private LclCheckBox cbInversSignal2;

	private LclCheckBox cbInversSignal3;

	private LclCheckBox cbInversSignal4;

	private IContainer icontainer_2;

	private LclLabel lbChannelName1;

	private LclLabel lbChannelName2;

	private LclLabel lbChannelName3;

	private LclLabel lbChannelName4;

	private LclLabel lbChannelNum;

	private LclLabel lbInversSignal;

	private LclTextBox tbInversSignal1;

	private LclTextBox tbInversSignal2;

	private LclTextBox tbInversSignal3;

	private LclTextBox tbInversSignal4;

	public DetectorControl1SetupForm()
	{
		icontainer_2 = null;
		InitializeComponent_2();
	}

	public DetectorControl1SetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent_2();
		cbChannelNum.Items.Add(1);
		cbChannelNum.Items.Add(2);
		cbChannelNum.Items.Add(3);
		cbChannelNum.Items.Add(4);
	}

	private void cbChannelNum_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void DetectorControl1SetupForm_Load(object sender, EventArgs e)
	{
		cbChannelNum_SelectedIndexChanged(null, null);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent_2()
	{
		lbChannelNum = new LclLabel();
		cbChannelNum = new LclComboBox();
		lbInversSignal = new LclLabel();
		lbChannelName1 = new LclLabel();
		tbInversSignal1 = new LclTextBox();
		cbInversSignal1 = new LclCheckBox();
		cbInversSignal2 = new LclCheckBox();
		tbInversSignal2 = new LclTextBox();
		lbChannelName2 = new LclLabel();
		cbInversSignal3 = new LclCheckBox();
		tbInversSignal3 = new LclTextBox();
		lbChannelName3 = new LclLabel();
		cbInversSignal4 = new LclCheckBox();
		tbInversSignal4 = new LclTextBox();
		lbChannelName4 = new LclLabel();
		SuspendLayout();
		btnOK.Location = new Point(36, 179);
		btnOK.Text = "确认";
		btnCancel.Location = new Point(126, 179);
		btnCancel.Text = "取消";
		btnHelp.Location = new Point(218, 179);
		btnHelp.Text = "帮助";
		lbChannelNum.AutoSize = true;
		lbChannelNum.Location = new Point(34, 18);
		lbChannelNum.Name = "lbChannelNum";
		lbChannelNum.Size = new Size(59, 12);
		lbChannelNum.TabIndex = 1;
		lbChannelNum.Text = "lclLabel1";
		cbChannelNum.DropDownStyle = ComboBoxStyle.DropDownList;
		cbChannelNum.FormattingEnabled = true;
		cbChannelNum.ItemExtString = "";
		cbChannelNum.Location = new Point(110, 15);
		cbChannelNum.Name = "cbChannelNum";
		cbChannelNum.Size = new Size(77, 20);
		cbChannelNum.TabIndex = 2;
		cbChannelNum.SelectedIndexChanged += cbChannelNum_SelectedIndexChanged;
		lbInversSignal.Location = new Point(261, 43);
		lbInversSignal.Name = "lbInversSignal";
		lbInversSignal.Size = new Size(59, 12);
		lbInversSignal.TabIndex = 1;
		lbInversSignal.Text = "sds";
		lbInversSignal.TextAlign = ContentAlignment.MiddleRight;
		lbInversSignal.Visible = false;
		lbChannelName1.AutoSize = true;
		lbChannelName1.Location = new Point(22, 68);
		lbChannelName1.Name = "lbChannelName1";
		lbChannelName1.Size = new Size(59, 12);
		lbChannelName1.TabIndex = 1;
		lbChannelName1.Text = "lclLabel1";
		tbInversSignal1.Location = new Point(87, 65);
		tbInversSignal1.Name = "tbInversSignal1";
		tbInversSignal1.Size = new Size(206, 21);
		tbInversSignal1.TabIndex = 3;
		tbInversSignal1.Text = "1 Channel";
		cbInversSignal1.AutoSize = true;
		cbInversSignal1.Location = new Point(305, 68);
		cbInversSignal1.Name = "cbInversSignal1";
		cbInversSignal1.Size = new Size(15, 14);
		cbInversSignal1.TabIndex = 4;
		cbInversSignal1.UseVisualStyleBackColor = true;
		cbInversSignal1.Visible = false;
		cbInversSignal2.AutoSize = true;
		cbInversSignal2.Location = new Point(305, 95);
		cbInversSignal2.Name = "cbInversSignal2";
		cbInversSignal2.Size = new Size(15, 14);
		cbInversSignal2.TabIndex = 4;
		cbInversSignal2.UseVisualStyleBackColor = true;
		cbInversSignal2.Visible = false;
		tbInversSignal2.Location = new Point(87, 92);
		tbInversSignal2.Name = "tbInversSignal2";
		tbInversSignal2.Size = new Size(206, 21);
		tbInversSignal2.TabIndex = 3;
		tbInversSignal2.Text = "2 Channel";
		lbChannelName2.AutoSize = true;
		lbChannelName2.Location = new Point(22, 95);
		lbChannelName2.Name = "lbChannelName2";
		lbChannelName2.Size = new Size(59, 12);
		lbChannelName2.TabIndex = 1;
		lbChannelName2.Text = "lclLabel1";
		cbInversSignal3.AutoSize = true;
		cbInversSignal3.Location = new Point(305, 122);
		cbInversSignal3.Name = "cbInversSignal3";
		cbInversSignal3.Size = new Size(15, 14);
		cbInversSignal3.TabIndex = 4;
		cbInversSignal3.UseVisualStyleBackColor = true;
		cbInversSignal3.Visible = false;
		tbInversSignal3.Location = new Point(87, 119);
		tbInversSignal3.Name = "tbInversSignal3";
		tbInversSignal3.Size = new Size(206, 21);
		tbInversSignal3.TabIndex = 3;
		tbInversSignal3.Text = "3 Channel";
		lbChannelName3.AutoSize = true;
		lbChannelName3.Location = new Point(22, 122);
		lbChannelName3.Name = "lbChannelName3";
		lbChannelName3.Size = new Size(59, 12);
		lbChannelName3.TabIndex = 1;
		lbChannelName3.Text = "lclLabel1";
		cbInversSignal4.AutoSize = true;
		cbInversSignal4.Location = new Point(305, 149);
		cbInversSignal4.Name = "cbInversSignal4";
		cbInversSignal4.Size = new Size(15, 14);
		cbInversSignal4.TabIndex = 4;
		cbInversSignal4.UseVisualStyleBackColor = true;
		cbInversSignal4.Visible = false;
		tbInversSignal4.Location = new Point(87, 146);
		tbInversSignal4.Name = "tbInversSignal4";
		tbInversSignal4.Size = new Size(206, 21);
		tbInversSignal4.TabIndex = 3;
		tbInversSignal4.Text = "4 Channel";
		lbChannelName4.AutoSize = true;
		lbChannelName4.Location = new Point(22, 149);
		lbChannelName4.Name = "lbChannelName4";
		lbChannelName4.Size = new Size(59, 12);
		lbChannelName4.TabIndex = 1;
		lbChannelName4.Text = "lclLabel1";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(347, 212);
		base.Controls.Add(lbChannelNum);
		base.Controls.Add(cbChannelNum);
		base.Controls.Add(lbInversSignal);
		base.Controls.Add(lbChannelName1);
		base.Controls.Add(tbInversSignal1);
		base.Controls.Add(cbInversSignal1);
		base.Controls.Add(lbChannelName2);
		base.Controls.Add(tbInversSignal2);
		base.Controls.Add(cbInversSignal2);
		base.Controls.Add(lbChannelName3);
		base.Controls.Add(tbInversSignal3);
		base.Controls.Add(cbInversSignal3);
		base.Controls.Add(lbChannelName4);
		base.Controls.Add(tbInversSignal4);
		base.Controls.Add(cbInversSignal4);
		base.Name = "DetectorControl1SetupForm";
		Text = "";
		base.Load += DetectorControl1SetupForm_Load;
		base.Controls.SetChildIndex(cbInversSignal4, 0);
		base.Controls.SetChildIndex(tbInversSignal4, 0);
		base.Controls.SetChildIndex(lbChannelName4, 0);
		base.Controls.SetChildIndex(cbInversSignal3, 0);
		base.Controls.SetChildIndex(tbInversSignal3, 0);
		base.Controls.SetChildIndex(lbChannelName3, 0);
		base.Controls.SetChildIndex(cbInversSignal2, 0);
		base.Controls.SetChildIndex(tbInversSignal2, 0);
		base.Controls.SetChildIndex(lbChannelName2, 0);
		base.Controls.SetChildIndex(cbInversSignal1, 0);
		base.Controls.SetChildIndex(tbInversSignal1, 0);
		base.Controls.SetChildIndex(lbChannelName1, 0);
		base.Controls.SetChildIndex(lbInversSignal, 0);
		base.Controls.SetChildIndex(cbChannelNum, 0);
		base.Controls.SetChildIndex(lbChannelNum, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		DetectorControl1 detectorControl = sysCfgControl as DetectorControl1;
		tbInversSignal1.Text = detectorControl.bsCtrls[0].name;
		tbInversSignal2.Text = detectorControl.bsCtrls[1].name;
		tbInversSignal3.Text = detectorControl.bsCtrls[2].name;
		tbInversSignal4.Text = detectorControl.bsCtrls[3].name;
	}

	public override void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			lbChannelNum.Text = "通道";
			lbChannelName1.Text = "通道";
			lbChannelName2.Text = "通道";
			lbChannelName3.Text = "通道";
			lbChannelName4.Text = "通道";
			lbInversSignal.Text = "翻转信号";
			break;
		case SysLanguage.EN:
			lbChannelNum.Text = "Channel";
			lbChannelName1.Text = "Channel";
			lbChannelName2.Text = "Channel";
			lbChannelName3.Text = "Channel";
			lbChannelName4.Text = "Channel";
			lbInversSignal.Text = "Invers Signal";
			break;
		}
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		DetectorControl1 detectorControl = sysCfgControl as DetectorControl1;
		detectorControl.bsCtrls[0].name = tbInversSignal1.Text;
		detectorControl.bsCtrls[1].name = tbInversSignal2.Text;
		detectorControl.bsCtrls[2].name = tbInversSignal3.Text;
		detectorControl.bsCtrls[3].name = tbInversSignal4.Text;
	}
}
