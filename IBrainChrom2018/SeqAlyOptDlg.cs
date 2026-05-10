using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SeqAlyOptDlg : LclDialog
{
	private LclCheckBox cbActiveSequence;

	private LclCheckBox cbIdleBeforeFirstInj;

	private IContainer icontainer_1;

	private LclGroupBox gbCounter;

	private LclGroupBox gbFormat;

	private LclGroupBox gbInjVolumnUnit;

	private LclLabel lbCurrent;

	private LclLabel lbDescription;

	private LclLabel lbIdleTime;

	public LclLabel lbIdleTimeU;

	private LclLabel lbReset;

	private LclLabel lbStart;

	private LclPanel lclPanel1;

	private LclRadioButton rbAutomatically;

	private LclRadioButton rbManually;

	private LclRadioButton rbMl;

	private LclRadioButton rbNever;

	private LclRadioButton rbOpenInstrument;

	private LclRadioButton rbRunSequence;

	private LclRadioButton rbStart0;

	private LclRadioButton rbStart1;

	private LclRadioButton rbUl;

	private LclTextBox tbCurrent;

	private LclTextBox tbDescription;

	private LclTextBox tbIdleTime;

	public static string sAutomatically => Lang.PS("自动", "Automatically");

	public static string sCounter => Lang.PS("计数器", "Counter");

	public static string sDescription => Lang.PS("描述", "Description");

	public static string sFormat => Lang.PS("格式(文件名)", "Format(FileName)");

	public static string sIdleBeforeFirstInj => Lang.PS("第一针进样前空闲", "Idle Time before First Injection");

	public static string sInjVolumnUnit => Lang.PS("进样单位", "Inj. Volumn Unit");

	public static string sManually => Lang.PS("手动", "Manually");

	public static string sNever => Lang.PS("从不", "Never");

	public static string sOpenInstrument => Lang.PS("打开仪器", "Open Instrument");

	public static string sReset => Lang.PS("重置", "Reset");

	public static string sRunSequence => Lang.PS("开始队列", "Run Sequence");

	public static string sStart => Lang.PS("起始", "Start");

	public SeqAlyOptDlg()
	{
		InitializeComponent();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		Text = Lang.PS("队列选项", "Sequence Options");
		cbActiveSequence.Text = Lang.PS("激活队列", "Active Sequence");
		cbIdleBeforeFirstInj.Text = Lang.PS("第一针进样前空闲", "Idle Time before First Injection");
		lbIdleTime.Text = Lang.PS("空闲时间", "Idle Time");
		gbCounter.Text = Lang.PS("计数器 (%n)", "Counter (%n)");
		lbStart.Text = Lang.PS("起始", "Start");
		lbReset.Text = Lang.PS("重置", "Reset");
		lbCurrent.Text = Lang.PS("当前", "Current");
		rbRunSequence.Text = Lang.PS("开始队列", "Run Sequence");
		rbOpenInstrument.Text = Lang.PS("打开仪器", "Open Instrument");
		rbNever.Text = Lang.PS("从不", "Never");
		gbFormat.Text = Lang.PS("格式(文件名)", "Format(FileName)");
		rbAutomatically.Text = Lang.PS("自动", "Automatically");
		rbManually.Text = Lang.PS("手动", "Manually");
		gbInjVolumnUnit.Text = Lang.PS("进样单位", "Inj. Volumn Unit");
		lbDescription.Text = Lang.PS("描述", "Description");
	}

	private void rbStart0_Click(object sender, EventArgs e)
	{
		if (sender == rbStart0)
		{
			tbCurrent.Text = "0";
		}
		else if (sender == rbStart1)
		{
			tbCurrent.Text = "1";
		}
	}

	private void method_0(AccStyle accStyle_0, SeqAlyOpt seqAlyOpt_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Write:
			seqAlyOpt_0.activeSequence = cbActiveSequence.Checked;
			seqAlyOpt_0.idleBeforeFirstInj = cbIdleBeforeFirstInj.Checked;
			seqAlyOpt_0.idleTime = Class49.String2Float(tbIdleTime.Text, 0f);
			if (rbStart0.Checked)
			{
				seqAlyOpt_0.counter_start = 0;
			}
			else
			{
				seqAlyOpt_0.counter_start = 1;
			}
			if (rbRunSequence.Checked)
			{
				seqAlyOpt_0.counter_resetStyle = CounterResetStyle.RunSequence;
			}
			else if (rbOpenInstrument.Checked)
			{
				seqAlyOpt_0.counter_resetStyle = CounterResetStyle.OpenInstrument;
			}
			else
			{
				seqAlyOpt_0.counter_resetStyle = CounterResetStyle.Never;
			}
			seqAlyOpt_0.counter_current = Class49.Object2Int(tbCurrent.Text, seqAlyOpt_0.counter_start);
			if (rbAutomatically.Checked)
			{
				seqAlyOpt_0.formatStyle = FormatStyle.Automatically;
			}
			else
			{
				seqAlyOpt_0.formatStyle = FormatStyle.Manually;
			}
			if (rbUl.Checked)
			{
				seqAlyOpt_0.injVolumnUnit = VolumnUnits.const_0;
			}
			else
			{
				seqAlyOpt_0.injVolumnUnit = VolumnUnits.const_1;
			}
			seqAlyOpt_0.description = tbDescription.Text;
			break;
		case AccStyle.Read:
			cbActiveSequence.Checked = seqAlyOpt_0.activeSequence;
			cbIdleBeforeFirstInj.Checked = seqAlyOpt_0.idleBeforeFirstInj;
			tbIdleTime.Text = seqAlyOpt_0.idleTime.ToString();
			rbStart0.Checked = seqAlyOpt_0.counter_start == 0;
			rbStart1.Checked = !rbStart0.Checked;
			switch (seqAlyOpt_0.counter_resetStyle)
			{
			case CounterResetStyle.RunSequence:
				rbRunSequence.Checked = true;
				break;
			case CounterResetStyle.OpenInstrument:
				rbOpenInstrument.Checked = true;
				break;
			case CounterResetStyle.Never:
				rbNever.Checked = true;
				break;
			}
			tbCurrent.Text = seqAlyOpt_0.counter_current.ToString();
			rbAutomatically.Checked = seqAlyOpt_0.formatStyle == FormatStyle.Automatically;
			rbManually.Checked = !rbAutomatically.Checked;
			rbUl.Checked = seqAlyOpt_0.injVolumnUnit == VolumnUnits.const_0;
			rbMl.Checked = !rbUl.Checked;
			tbDescription.Text = seqAlyOpt_0.description;
			break;
		}
	}

	public DialogResult ShowDialog(SeqAlyOpt sequenceAnalysisOptions, bool readOnly)
	{
		method_0(AccStyle.Read, sequenceAnalysisOptions);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK && !readOnly)
		{
			method_0(AccStyle.Write, sequenceAnalysisOptions);
		}
		return dialogResult;
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
		this.cbActiveSequence = new IBrainChrom2018.LclCheckBox();
		this.cbIdleBeforeFirstInj = new IBrainChrom2018.LclCheckBox();
		this.lbIdleTime = new IBrainChrom2018.LclLabel();
		this.tbIdleTime = new IBrainChrom2018.LclTextBox();
		this.lbIdleTimeU = new IBrainChrom2018.LclLabel();
		this.gbCounter = new IBrainChrom2018.LclGroupBox();
		this.rbStart1 = new IBrainChrom2018.LclRadioButton();
		this.rbStart0 = new IBrainChrom2018.LclRadioButton();
		this.lclPanel1 = new IBrainChrom2018.LclPanel();
		this.rbNever = new IBrainChrom2018.LclRadioButton();
		this.rbOpenInstrument = new IBrainChrom2018.LclRadioButton();
		this.rbRunSequence = new IBrainChrom2018.LclRadioButton();
		this.lbCurrent = new IBrainChrom2018.LclLabel();
		this.lbReset = new IBrainChrom2018.LclLabel();
		this.lbStart = new IBrainChrom2018.LclLabel();
		this.tbCurrent = new IBrainChrom2018.LclTextBox();
		this.tbDescription = new IBrainChrom2018.LclTextBox();
		this.lbDescription = new IBrainChrom2018.LclLabel();
		this.gbFormat = new IBrainChrom2018.LclGroupBox();
		this.rbManually = new IBrainChrom2018.LclRadioButton();
		this.rbAutomatically = new IBrainChrom2018.LclRadioButton();
		this.gbInjVolumnUnit = new IBrainChrom2018.LclGroupBox();
		this.rbMl = new IBrainChrom2018.LclRadioButton();
		this.rbUl = new IBrainChrom2018.LclRadioButton();
		this.gbCounter.SuspendLayout();
		this.lclPanel1.SuspendLayout();
		this.gbFormat.SuspendLayout();
		this.gbInjVolumnUnit.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(147, 302);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(252, 302);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(45, 302);
		base.btnOK.Text = "确认";
		this.cbActiveSequence.AutoSize = true;
		this.cbActiveSequence.Location = new System.Drawing.Point(107, 12);
		this.cbActiveSequence.Name = "cbActiveSequence";
		this.cbActiveSequence.Size = new System.Drawing.Size(72, 16);
		this.cbActiveSequence.TabIndex = 1;
		this.cbActiveSequence.Text = "激活队列";
		this.cbActiveSequence.UseVisualStyleBackColor = true;
		this.cbIdleBeforeFirstInj.AutoSize = true;
		this.cbIdleBeforeFirstInj.Location = new System.Drawing.Point(107, 34);
		this.cbIdleBeforeFirstInj.Name = "cbIdleBeforeFirstInj";
		this.cbIdleBeforeFirstInj.Size = new System.Drawing.Size(120, 16);
		this.cbIdleBeforeFirstInj.TabIndex = 1;
		this.cbIdleBeforeFirstInj.Text = "第一针进样前空闲";
		this.cbIdleBeforeFirstInj.UseVisualStyleBackColor = true;
		this.lbIdleTime.AutoSize = true;
		this.lbIdleTime.Location = new System.Drawing.Point(105, 57);
		this.lbIdleTime.Name = "lbIdleTime";
		this.lbIdleTime.Size = new System.Drawing.Size(53, 12);
		this.lbIdleTime.TabIndex = 2;
		this.lbIdleTime.Text = "空闲时间";
		this.tbIdleTime.Location = new System.Drawing.Point(170, 54);
		this.tbIdleTime.Name = "tbIdleTime";
		this.tbIdleTime.Size = new System.Drawing.Size(52, 21);
		this.tbIdleTime.TabIndex = 3;
		this.lbIdleTimeU.AutoSize = true;
		this.lbIdleTimeU.Location = new System.Drawing.Point(228, 57);
		this.lbIdleTimeU.Name = "lbIdleTimeU";
		this.lbIdleTimeU.Size = new System.Drawing.Size(35, 12);
		this.lbIdleTimeU.TabIndex = 2;
		this.lbIdleTimeU.Text = "[min]";
		this.gbCounter.Controls.Add(this.rbStart1);
		this.gbCounter.Controls.Add(this.rbStart0);
		this.gbCounter.Controls.Add(this.lclPanel1);
		this.gbCounter.Controls.Add(this.lbCurrent);
		this.gbCounter.Controls.Add(this.lbReset);
		this.gbCounter.Controls.Add(this.lbStart);
		this.gbCounter.Controls.Add(this.tbCurrent);
		this.gbCounter.Location = new System.Drawing.Point(12, 81);
		this.gbCounter.Name = "gbCounter";
		this.gbCounter.Size = new System.Drawing.Size(234, 164);
		this.gbCounter.TabIndex = 4;
		this.gbCounter.TabStop = false;
		this.gbCounter.Text = "计数器 (%n)";
		this.rbStart1.AutoSize = true;
		this.rbStart1.Location = new System.Drawing.Point(87, 42);
		this.rbStart1.Name = "rbStart1";
		this.rbStart1.Size = new System.Drawing.Size(29, 16);
		this.rbStart1.TabIndex = 0;
		this.rbStart1.TabStop = true;
		this.rbStart1.Text = "1";
		this.rbStart1.UseVisualStyleBackColor = true;
		this.rbStart1.Click += new System.EventHandler(rbStart0_Click);
		this.rbStart0.AutoSize = true;
		this.rbStart0.Location = new System.Drawing.Point(87, 22);
		this.rbStart0.Name = "rbStart0";
		this.rbStart0.Size = new System.Drawing.Size(29, 16);
		this.rbStart0.TabIndex = 0;
		this.rbStart0.TabStop = true;
		this.rbStart0.Text = "0";
		this.rbStart0.UseVisualStyleBackColor = true;
		this.rbStart0.Click += new System.EventHandler(rbStart0_Click);
		this.lclPanel1.Controls.Add(this.rbNever);
		this.lclPanel1.Controls.Add(this.rbOpenInstrument);
		this.lclPanel1.Controls.Add(this.rbRunSequence);
		this.lclPanel1.Location = new System.Drawing.Point(80, 66);
		this.lclPanel1.Name = "lclPanel1";
		this.lclPanel1.Size = new System.Drawing.Size(148, 64);
		this.lclPanel1.TabIndex = 3;
		this.rbNever.AutoSize = true;
		this.rbNever.Location = new System.Drawing.Point(7, 43);
		this.rbNever.Name = "rbNever";
		this.rbNever.Size = new System.Drawing.Size(47, 16);
		this.rbNever.TabIndex = 0;
		this.rbNever.TabStop = true;
		this.rbNever.Text = "从不";
		this.rbNever.UseVisualStyleBackColor = true;
		this.rbOpenInstrument.AutoSize = true;
		this.rbOpenInstrument.Location = new System.Drawing.Point(7, 23);
		this.rbOpenInstrument.Name = "rbOpenInstrument";
		this.rbOpenInstrument.Size = new System.Drawing.Size(71, 16);
		this.rbOpenInstrument.TabIndex = 0;
		this.rbOpenInstrument.TabStop = true;
		this.rbOpenInstrument.Text = "打开仪器";
		this.rbOpenInstrument.UseVisualStyleBackColor = true;
		this.rbRunSequence.AutoSize = true;
		this.rbRunSequence.Location = new System.Drawing.Point(7, 3);
		this.rbRunSequence.Name = "rbRunSequence";
		this.rbRunSequence.Size = new System.Drawing.Size(71, 16);
		this.rbRunSequence.TabIndex = 0;
		this.rbRunSequence.TabStop = true;
		this.rbRunSequence.Text = "开始队列";
		this.rbRunSequence.UseVisualStyleBackColor = true;
		this.lbCurrent.AutoSize = true;
		this.lbCurrent.Location = new System.Drawing.Point(15, 139);
		this.lbCurrent.Name = "lbCurrent";
		this.lbCurrent.Size = new System.Drawing.Size(29, 12);
		this.lbCurrent.TabIndex = 2;
		this.lbCurrent.Text = "当前";
		this.lbReset.AutoSize = true;
		this.lbReset.Location = new System.Drawing.Point(15, 71);
		this.lbReset.Name = "lbReset";
		this.lbReset.Size = new System.Drawing.Size(29, 12);
		this.lbReset.TabIndex = 2;
		this.lbReset.Text = "重置";
		this.lbStart.AutoSize = true;
		this.lbStart.Location = new System.Drawing.Point(15, 24);
		this.lbStart.Name = "lbStart";
		this.lbStart.Size = new System.Drawing.Size(29, 12);
		this.lbStart.TabIndex = 2;
		this.lbStart.Text = "起始";
		this.tbCurrent.Location = new System.Drawing.Point(80, 136);
		this.tbCurrent.Name = "tbCurrent";
		this.tbCurrent.Size = new System.Drawing.Size(52, 21);
		this.tbCurrent.TabIndex = 3;
		this.tbDescription.Location = new System.Drawing.Point(12, 267);
		this.tbDescription.Name = "tbDescription";
		this.tbDescription.Size = new System.Drawing.Size(365, 21);
		this.tbDescription.TabIndex = 3;
		this.lbDescription.AutoSize = true;
		this.lbDescription.Location = new System.Drawing.Point(12, 252);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(29, 12);
		this.lbDescription.TabIndex = 2;
		this.lbDescription.Text = "描述";
		this.gbFormat.Controls.Add(this.rbManually);
		this.gbFormat.Controls.Add(this.rbAutomatically);
		this.gbFormat.Location = new System.Drawing.Point(252, 82);
		this.gbFormat.Name = "gbFormat";
		this.gbFormat.Size = new System.Drawing.Size(125, 65);
		this.gbFormat.TabIndex = 4;
		this.gbFormat.TabStop = false;
		this.gbFormat.Text = "格式(文件名)";
		this.rbManually.AutoSize = true;
		this.rbManually.Enabled = false;
		this.rbManually.Location = new System.Drawing.Point(12, 40);
		this.rbManually.Name = "rbManually";
		this.rbManually.Size = new System.Drawing.Size(47, 16);
		this.rbManually.TabIndex = 0;
		this.rbManually.TabStop = true;
		this.rbManually.Text = "手动";
		this.rbManually.UseVisualStyleBackColor = true;
		this.rbAutomatically.AutoSize = true;
		this.rbAutomatically.Location = new System.Drawing.Point(12, 20);
		this.rbAutomatically.Name = "rbAutomatically";
		this.rbAutomatically.Size = new System.Drawing.Size(47, 16);
		this.rbAutomatically.TabIndex = 0;
		this.rbAutomatically.TabStop = true;
		this.rbAutomatically.Text = "自动";
		this.rbAutomatically.UseVisualStyleBackColor = true;
		this.gbInjVolumnUnit.Controls.Add(this.rbMl);
		this.gbInjVolumnUnit.Controls.Add(this.rbUl);
		this.gbInjVolumnUnit.Location = new System.Drawing.Point(252, 153);
		this.gbInjVolumnUnit.Name = "gbInjVolumnUnit";
		this.gbInjVolumnUnit.Size = new System.Drawing.Size(125, 65);
		this.gbInjVolumnUnit.TabIndex = 4;
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(387, 337);
		base.Controls.Add(this.cbActiveSequence);
		base.Controls.Add(this.cbIdleBeforeFirstInj);
		base.Controls.Add(this.lbDescription);
		base.Controls.Add(this.lbIdleTime);
		base.Controls.Add(this.gbInjVolumnUnit);
		base.Controls.Add(this.gbFormat);
		base.Controls.Add(this.gbCounter);
		base.Controls.Add(this.tbDescription);
		base.Controls.Add(this.tbIdleTime);
		base.Controls.Add(this.lbIdleTimeU);
		base.Name = "SeqAlyOptDlg";
		this.Text = "进样队列选项";
		base.Controls.SetChildIndex(this.lbIdleTimeU, 0);
		base.Controls.SetChildIndex(this.tbIdleTime, 0);
		base.Controls.SetChildIndex(this.tbDescription, 0);
		base.Controls.SetChildIndex(this.gbCounter, 0);
		base.Controls.SetChildIndex(this.gbFormat, 0);
		base.Controls.SetChildIndex(this.gbInjVolumnUnit, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbIdleTime, 0);
		base.Controls.SetChildIndex(this.lbDescription, 0);
		base.Controls.SetChildIndex(this.cbIdleBeforeFirstInj, 0);
		base.Controls.SetChildIndex(this.cbActiveSequence, 0);
		this.gbCounter.ResumeLayout(false);
		this.gbCounter.PerformLayout();
		this.lclPanel1.ResumeLayout(false);
		this.lclPanel1.PerformLayout();
		this.gbFormat.ResumeLayout(false);
		this.gbFormat.PerformLayout();
		this.gbInjVolumnUnit.ResumeLayout(false);
		this.gbInjVolumnUnit.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
