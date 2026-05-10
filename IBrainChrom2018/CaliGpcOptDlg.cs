using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CaliGpcOptDlg : LclDialog
{
	private const string string_0 = "流速矫正";

	private const string string_1 = "普适校正";

	private const string string_2 = "描述";

	private const string string_3 = " 信号:流速峰保留时间";

	private const string string_4 = "再校正幅度";

	private const string string_5 = "方程";

	private const string string_6 = "信号数";

	private const string string_7 = "校正选项";

	private const string string_8 = "Flow Rate Correction";

	private const string string_9 = "Universal Calibrate";

	private const string string_10 = "Description";

	private const string string_11 = " Signal:FlowMarker RT";

	private const string string_12 = "Recali. Search";

	private const string string_13 = "Curve";

	private const string string_14 = "Sig. Num";

	private const string string_15 = "Calibration Options";

	private LclCheckBox cbFlowRateCorr;

	private LclTextBox cbRecaliSearch;

	private LclComboBox[] lclComboBox_0;

	private LclComboBox cbSignalNumber;

	private LclCheckBox cbUniversalCali;

	private IContainer icontainer_1;

	private LclLabel lbDescription;

	private LclLabel[] lclLabel_0;

	private LclLabel lbRecaliSearch;

	private LclLabel[] lclLabel_1;

	private LclLabel lbSignalNumber;

	private LclLabel lclLabel1;

	private LclPanel pnlSignals;

	private LclTextBox tbDescription;

	private LclTextBox[] lclTextBox_0;

	public CaliGpcOptDlg()
	{
		icontainer_1 = null;
		lclLabel_0 = new LclLabel[0];
		lclTextBox_0 = new LclTextBox[0];
		lclLabel_1 = new LclLabel[0];
		lclComboBox_0 = new LclComboBox[0];
		InitializeComponent_1();
		Array.Resize(ref lclLabel_0, 12);
		Array.Resize(ref lclTextBox_0, 12);
		Array.Resize(ref lclLabel_1, 12);
		Array.Resize(ref lclComboBox_0, 12);
		for (int i = 0; i < 12; i++)
		{
			lclLabel_0[i] = new LclLabel();
			lclTextBox_0[i] = new LclTextBox();
			lclLabel_1[i] = new LclLabel();
			lclComboBox_0[i] = new LclComboBox();
			pnlSignals.Controls.Add(lclLabel_0[i]);
			pnlSignals.Controls.Add(lclTextBox_0[i]);
			pnlSignals.Controls.Add(lclLabel_1[i]);
			pnlSignals.Controls.Add(lclComboBox_0[i]);
			lclLabel_0[i].Location = new Point(5, i * 22 + 4);
			lclTextBox_0[i].Location = new Point(150, lclLabel_0[i].Top - 4);
			lclLabel_1[i].Location = new Point(205, lclLabel_0[i].Top);
			lclComboBox_0[i].Location = new Point(260, lclLabel_1[i].Top - 4);
			Control control = lclLabel_0[i];
			lclLabel_1[i].AutoSize = true;
			control.AutoSize = true;
			lclTextBox_0[i].Width = 45;
			lclComboBox_0[i].Width = 70;
		}
		for (int j = 1; j <= 12; j++)
		{
			cbSignalNumber.Items.Add(j);
		}
		cbSignalNumber.SelectedIndex = 2;
	}

	private void cbSignalNumber_SelectedIndexChanged(object sender, EventArgs e)
	{
		int num = (int)cbSignalNumber.SelectedItem;
		pnlSignals.Height = lclComboBox_0[num - 1].Bottom + 2;
		base.Height = pnlSignals.Height + 200;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent_1()
	{
		lbDescription = new LclLabel();
		lbSignalNumber = new LclLabel();
		tbDescription = new LclTextBox();
		cbSignalNumber = new LclComboBox();
		cbFlowRateCorr = new LclCheckBox();
		cbUniversalCali = new LclCheckBox();
		lbRecaliSearch = new LclLabel();
		cbRecaliSearch = new LclTextBox();
		lclLabel1 = new LclLabel();
		pnlSignals = new LclPanel();
		SuspendLayout();
		btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnOK.Location = new Point(30, 287);
		btnOK.Text = "确认";
		btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnCancel.Location = new Point(132, 287);
		btnCancel.Text = "取消";
		btnHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnHelp.Location = new Point(237, 287);
		btnHelp.Text = "帮助";
		lbDescription.AutoSize = true;
		lbDescription.Location = new Point(12, 19);
		lbDescription.Name = "lbDescription";
		lbDescription.Size = new Size(59, 12);
		lbDescription.TabIndex = 1;
		lbDescription.Text = "lclLabel1";
		lbSignalNumber.AutoSize = true;
		lbSignalNumber.Location = new Point(12, 43);
		lbSignalNumber.Name = "lbSignalNumber";
		lbSignalNumber.Size = new Size(59, 12);
		lbSignalNumber.TabIndex = 1;
		lbSignalNumber.Text = "lclLabel1";
		tbDescription.Location = new Point(91, 15);
		tbDescription.Name = "tbDescription";
		tbDescription.Size = new Size(250, 21);
		tbDescription.TabIndex = 2;
		cbSignalNumber.DropDownHeight = 70;
		cbSignalNumber.DropDownStyle = ComboBoxStyle.DropDownList;
		cbSignalNumber.FormattingEnabled = true;
		cbSignalNumber.IntegralHeight = false;
		cbSignalNumber.Location = new Point(91, 39);
		cbSignalNumber.Name = "cbSignalNumber";
		cbSignalNumber.Size = new Size(77, 20);
		cbSignalNumber.TabIndex = 3;
		cbSignalNumber.SelectedIndexChanged += cbSignalNumber_SelectedIndexChanged;
		cbFlowRateCorr.AutoSize = true;
		cbFlowRateCorr.Location = new Point(14, 63);
		cbFlowRateCorr.Name = "cbFlowRateCorr";
		cbFlowRateCorr.Size = new Size(96, 16);
		cbFlowRateCorr.TabIndex = 4;
		cbFlowRateCorr.Text = "lclCheckBox1";
		cbFlowRateCorr.UseVisualStyleBackColor = true;
		cbUniversalCali.AutoSize = true;
		cbUniversalCali.Location = new Point(14, 83);
		cbUniversalCali.Name = "cbUniversalCali";
		cbUniversalCali.Size = new Size(96, 16);
		cbUniversalCali.TabIndex = 4;
		cbUniversalCali.Text = "lclCheckBox1";
		cbUniversalCali.UseVisualStyleBackColor = true;
		lbRecaliSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		lbRecaliSearch.AutoSize = true;
		lbRecaliSearch.Location = new Point(12, 258);
		lbRecaliSearch.Name = "lbRecaliSearch";
		lbRecaliSearch.Size = new Size(59, 12);
		lbRecaliSearch.TabIndex = 1;
		lbRecaliSearch.Text = "lclLabel1";
		cbRecaliSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		cbRecaliSearch.Location = new Point(91, 253);
		cbRecaliSearch.Name = "cbRecaliSearch";
		cbRecaliSearch.Size = new Size(59, 21);
		cbRecaliSearch.TabIndex = 2;
		lclLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		lclLabel1.AutoSize = true;
		lclLabel1.Location = new Point(156, 258);
		lclLabel1.Name = "lclLabel1";
		lclLabel1.Size = new Size(23, 12);
		lclLabel1.TabIndex = 1;
		lclLabel1.Text = "[%]";
		pnlSignals.Location = new Point(7, 100);
		pnlSignals.Name = "pnlSignals";
		pnlSignals.Size = new Size(337, 145);
		pnlSignals.TabIndex = 5;
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(347, 320);
		base.Controls.Add(lbDescription);
		base.Controls.Add(lbSignalNumber);
		base.Controls.Add(tbDescription);
		base.Controls.Add(pnlSignals);
		base.Controls.Add(cbSignalNumber);
		base.Controls.Add(cbFlowRateCorr);
		base.Controls.Add(cbUniversalCali);
		base.Controls.Add(cbRecaliSearch);
		base.Controls.Add(lbRecaliSearch);
		base.Controls.Add(lclLabel1);
		base.Name = "CaliGpcOptDlg";
		base.Controls.SetChildIndex(lclLabel1, 0);
		base.Controls.SetChildIndex(lbRecaliSearch, 0);
		base.Controls.SetChildIndex(cbRecaliSearch, 0);
		base.Controls.SetChildIndex(cbUniversalCali, 0);
		base.Controls.SetChildIndex(cbFlowRateCorr, 0);
		base.Controls.SetChildIndex(cbSignalNumber, 0);
		base.Controls.SetChildIndex(pnlSignals, 0);
		base.Controls.SetChildIndex(tbDescription, 0);
		base.Controls.SetChildIndex(lbSignalNumber, 0);
		base.Controls.SetChildIndex(lbDescription, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
		{
			Text = "校正选项";
			lbDescription.Text = "描述";
			lbSignalNumber.Text = "信号数";
			cbFlowRateCorr.Text = "流速矫正";
			cbUniversalCali.Text = "普适校正";
			lbRecaliSearch.Text = "再校正幅度";
			for (int j = 0; j < lclLabel_1.Length; j++)
			{
				lclLabel_0[j].Text = j + 1 + " 信号:流速峰保留时间";
				lclLabel_1[j].Text = "方程";
				lclComboBox_0[j].Items.Clear();
				lclComboBox_0[j].Items.Add("自由");
				lclComboBox_0[j].Items.Add("点到点");
				lclComboBox_0[j].Items.Add("线性");
				lclComboBox_0[j].Items.Add("二次");
				lclComboBox_0[j].Items.Add("三次");
				lclComboBox_0[j].Items.Add("四次");
			}
			break;
		}
		case SysLanguage.EN:
		{
			Text = "Calibration Options";
			lbDescription.Text = "Description";
			lbSignalNumber.Text = "Sig. Num";
			cbFlowRateCorr.Text = "Flow Rate Correction";
			cbUniversalCali.Text = "Universal Calibrate";
			lbRecaliSearch.Text = "Recali. Search";
			for (int i = 0; i < lclLabel_1.Length; i++)
			{
				lclLabel_0[i].Text = i + 1 + " Signal:FlowMarker RT";
				lclLabel_1[i].Text = "Curve";
				lclComboBox_0[i].Items.Clear();
				lclComboBox_0[i].Items.Add("Free");
				lclComboBox_0[i].Items.Add("Pt to Pt");
				lclComboBox_0[i].Items.Add("Linear");
				lclComboBox_0[i].Items.Add("Quadratic");
				lclComboBox_0[i].Items.Add("Cubic");
				lclComboBox_0[i].Items.Add("Sigmoid");
			}
			break;
		}
		}
	}
}
