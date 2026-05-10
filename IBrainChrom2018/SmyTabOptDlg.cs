using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SmyTabOptDlg : LclDialog
{
	private IContainer icontainer_1;

	private LclGroupBox gbParaHeader;

	private LclGroupBox gbReportInSummaryTable;

	private LclRadioButton rbAllIdentifiedPeaks;

	private LclRadioButton rbAllPeaksInCali;

	private LclRadioButton rbCmpd_Para;

	private LclRadioButton rbPara_Cmpd;

	public SmyTabOptDlg()
	{
		InitializeComponent();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = Lang.PS("总结表选项", "Summary Table Options");
			gbReportInSummaryTable.Text = Lang.PS("总结表报告", "Report in Summary Table");
			rbAllIdentifiedPeaks.Text = Lang.PS("所有确认峰", "All Identified Peaks");
			rbAllPeaksInCali.Text = Lang.PS("校正文件所有峰", "All Peaks in Cali.");
			gbParaHeader.Text = Lang.PS("表头参数", "Header Parameter");
			rbCmpd_Para.Text = Lang.PS("组分 / 参数", "Compound / Parameter");
			rbPara_Cmpd.Text = Lang.PS("参数 / 组分", "Parameter / Compound");
			break;
		case SysLanguage.EN:
			Text = "Summary Table Options";
			gbReportInSummaryTable.Text = "Report in Summary Table";
			rbAllIdentifiedPeaks.Text = "All Identified Peaks";
			rbAllPeaksInCali.Text = "All Peaks in Cali.";
			gbParaHeader.Text = "Header Parameter";
			rbCmpd_Para.Text = "Compound / Parameter";
			rbPara_Cmpd.Text = "Parameter / Compound";
			break;
		}
	}

	private void method_0(AccStyle accStyle_0, SmyTabOpt smyTabOpt_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
			rbAllIdentifiedPeaks.Checked = smyTabOpt_0.smyTabRpt == SmyTabRpt.AllIdentifiedPeaks;
			rbAllPeaksInCali.Checked = smyTabOpt_0.smyTabRpt == SmyTabRpt.AllPeaksInCali;
			rbCmpd_Para.Checked = smyTabOpt_0.smyHdrPara == SmyHdrPara.Cmpd_Para;
			rbPara_Cmpd.Checked = smyTabOpt_0.smyHdrPara == SmyHdrPara.Para_Cmpd;
			break;
		case AccStyle.Write:
			if (rbAllIdentifiedPeaks.Checked)
			{
				smyTabOpt_0.smyTabRpt = SmyTabRpt.AllIdentifiedPeaks;
			}
			else
			{
				smyTabOpt_0.smyTabRpt = SmyTabRpt.AllPeaksInCali;
			}
			if (rbCmpd_Para.Checked)
			{
				smyTabOpt_0.smyHdrPara = SmyHdrPara.Cmpd_Para;
			}
			else
			{
				smyTabOpt_0.smyHdrPara = SmyHdrPara.Para_Cmpd;
			}
			break;
		}
	}

	public DialogResult ShowDialog(SmyTabOpt summaryTableOptions)
	{
		method_0(AccStyle.Read, summaryTableOptions);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_0(AccStyle.Write, summaryTableOptions);
		}
		return dialogResult;
	}

	private void method_1(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void method_2(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
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
		this.gbReportInSummaryTable = new IBrainChrom2018.LclGroupBox();
		this.rbAllPeaksInCali = new IBrainChrom2018.LclRadioButton();
		this.rbAllIdentifiedPeaks = new IBrainChrom2018.LclRadioButton();
		this.gbParaHeader = new IBrainChrom2018.LclGroupBox();
		this.rbPara_Cmpd = new IBrainChrom2018.LclRadioButton();
		this.rbCmpd_Para = new IBrainChrom2018.LclRadioButton();
		this.gbReportInSummaryTable.SuspendLayout();
		this.gbParaHeader.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(139, 156);
		base.btnCancel.Text = "取消";
		base.btnCancel.Click += new System.EventHandler(method_2);
		base.btnHelp.Location = new System.Drawing.Point(220, 156);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(49, 156);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_1);
		this.gbReportInSummaryTable.Controls.Add(this.rbAllPeaksInCali);
		this.gbReportInSummaryTable.Controls.Add(this.rbAllIdentifiedPeaks);
		this.gbReportInSummaryTable.Location = new System.Drawing.Point(13, 12);
		this.gbReportInSummaryTable.Name = "gbReportInSummaryTable";
		this.gbReportInSummaryTable.Size = new System.Drawing.Size(176, 64);
		this.gbReportInSummaryTable.TabIndex = 1;
		this.gbReportInSummaryTable.TabStop = false;
		this.gbReportInSummaryTable.Text = "总结表报告";
		this.rbAllPeaksInCali.AutoSize = true;
		this.rbAllPeaksInCali.Location = new System.Drawing.Point(6, 42);
		this.rbAllPeaksInCali.Name = "rbAllPeaksInCali";
		this.rbAllPeaksInCali.Size = new System.Drawing.Size(107, 16);
		this.rbAllPeaksInCali.TabIndex = 0;
		this.rbAllPeaksInCali.TabStop = true;
		this.rbAllPeaksInCali.Text = "校正文件所有峰";
		this.rbAllPeaksInCali.UseVisualStyleBackColor = true;
		this.rbAllIdentifiedPeaks.AutoSize = true;
		this.rbAllIdentifiedPeaks.Location = new System.Drawing.Point(6, 20);
		this.rbAllIdentifiedPeaks.Name = "rbAllIdentifiedPeaks";
		this.rbAllIdentifiedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbAllIdentifiedPeaks.TabIndex = 0;
		this.rbAllIdentifiedPeaks.TabStop = true;
		this.rbAllIdentifiedPeaks.Text = "所有确认峰";
		this.rbAllIdentifiedPeaks.UseVisualStyleBackColor = true;
		this.gbParaHeader.Controls.Add(this.rbPara_Cmpd);
		this.gbParaHeader.Controls.Add(this.rbCmpd_Para);
		this.gbParaHeader.Location = new System.Drawing.Point(13, 82);
		this.gbParaHeader.Name = "gbParaHeader";
		this.gbParaHeader.Size = new System.Drawing.Size(176, 64);
		this.gbParaHeader.TabIndex = 1;
		this.gbParaHeader.TabStop = false;
		this.gbParaHeader.Text = "表头参数";
		this.rbPara_Cmpd.AutoSize = true;
		this.rbPara_Cmpd.Location = new System.Drawing.Point(6, 42);
		this.rbPara_Cmpd.Name = "rbPara_Cmpd";
		this.rbPara_Cmpd.Size = new System.Drawing.Size(89, 16);
		this.rbPara_Cmpd.TabIndex = 0;
		this.rbPara_Cmpd.TabStop = true;
		this.rbPara_Cmpd.Text = "参数 / 组分";
		this.rbPara_Cmpd.UseVisualStyleBackColor = true;
		this.rbCmpd_Para.AutoSize = true;
		this.rbCmpd_Para.Location = new System.Drawing.Point(6, 20);
		this.rbCmpd_Para.Name = "rbCmpd_Para";
		this.rbCmpd_Para.Size = new System.Drawing.Size(89, 16);
		this.rbCmpd_Para.TabIndex = 0;
		this.rbCmpd_Para.TabStop = true;
		this.rbCmpd_Para.Text = "组分 / 参数";
		this.rbCmpd_Para.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(347, 186);
		base.Controls.Add(this.gbParaHeader);
		base.Controls.Add(this.gbReportInSummaryTable);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
		base.Name = "SmyTabOptDlg";
		this.Text = "总结表选项";
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(this.gbReportInSummaryTable, 0);
		base.Controls.SetChildIndex(this.gbParaHeader, 0);
		this.gbReportInSummaryTable.ResumeLayout(false);
		this.gbReportInSummaryTable.PerformLayout();
		this.gbParaHeader.ResumeLayout(false);
		this.gbParaHeader.PerformLayout();
		base.ResumeLayout(false);
	}
}
