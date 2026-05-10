using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SglAlyDlg : LclDialog
{
	private CMS_InfoParasFMT cms_InfoParasFMT_0 = new CMS_InfoParasFMT();

	private Injection injection_0 = new Injection();

	private LclButton btnAbort;

	private LclButton btnBrowse;

	private LclButton btnDataRecovery;

	private LclButton btnFileNameMenu;

	private LclButton btnKAlpha;

	public LclButton btnMethod;

	private LclButton btnRun;

	private LclButton btnSnapshot;

	private LclButton btnStop;

	private LclCheckBox cbCalibrationStand;

	private LclCheckBox cbFileOverwrite;

	private IContainer icontainer_1;

	private LclGroupBox gbAnalysis;

	private LclGroupBox gbControl;

	private LclLabel lbAlpha;

	private LclLabel lbAmount;

	private LclLabel lbChromFileName;

	private LclLabel lbCounter;

	private LclLabel lbDilution;

	private LclLabel lbInjVolume;

	private LclLabel lbISTDAmount;

	private LclLabel lbK;

	private LclLabel lbSample;

	private LclLabel lbSampleID;

	private LclTextBox tbAlpha;

	private LclTextBox tbAmount;

	private LclTextBox tbCounter;

	private LclTextBox tbDilution;

	private LclTextBox tbFileNameFMT;

	private LclTextBox tbInjVolume;

	private LclTextBox tbISTDAmount;

	private LclTextBox tbK;

	private LclTextBox tbSample;

	private LclTextBox tbSampleID;

	public SglAlyDlg(Instrument instrument)
	{
		InitializeComponent();
		base.instrument = instrument;
		base.Icon = SystemIconResource.smethod_17();
		ResourceImageLoad.SetCtrlBitmap(btnFileNameMenu, SystemIconResource.smethod_40());
		cms_InfoParasFMT_0.miVialNumber.Visible = false;
		cms_InfoParasFMT_0.miInjNumber.Visible = false;
		method_0(AccStyle.Read, injection_0);
	}

	public void btnAbort_Click(object sender, EventArgs e)
	{
		instrument.daf_StopGather();
	}

	private void btnBrowse_Click(object sender, EventArgs e)
	{
	}

	private void btnDataRecovery_Click(object sender, EventArgs e)
	{
	}

	private void btnFileNameMenu_Click(object sender, EventArgs e)
	{
		cms_InfoParasFMT_0.Show(btnFileNameMenu, 25, 15, tbFileNameFMT);
	}

	private void btnKAlpha_Click(object sender, EventArgs e)
	{
		if (Class49.kalphaDlg_0.ShowDialog() == DialogResult.OK)
		{
			tbK.Text = KAlphaDlg.float_0.ToString();
			tbAlpha.Text = KAlphaDlg.alpha.ToString();
		}
	}

	public void btnRun_Click(object sender, EventArgs e)
	{
		method_0(AccStyle.Write, injection_0);
		method_0(AccStyle.Read, injection_0);
		injection_0.dtAcquire = DateTime.Now;
		injection_0.analyst = instrument.user.u_name;
		instrument.runningInjInfo.LoadFromObject(injection_0);
		instrument.daf_BeginGather(sample: true, InjectStyle.Single);
		instrument.form.RefreshInfo(InjectStyle.Single);
		injection_0.counter++;
	}

	private void btnSnapshot_Click(object sender, EventArgs e)
	{
		instrument.Save();
	}

	public void btnStop_Click(object sender, EventArgs e)
	{
		instrument.Save();
		btnAbort_Click(null, null);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = Lang.PS("单针分析", "Single Analysis");
			gbAnalysis.Text = Lang.PS("分析", "Analysis");
			lbSampleID.Text = Lang.PS("样品ID", "Sample ID");
			lbSample.Text = Lang.PS("样品", "Sample");
			lbAmount.Text = Lang.PS("数量", "Amount");
			lbISTDAmount.Text = Lang.PS("ISTD数量", "ISTD Amount");
			lbDilution.Text = Lang.PS("稀释", "Dilution");
			lbInjVolume.Text = Lang.PS("进样体积", "Inj. Volume");
			cbCalibrationStand.Text = Lang.PS("校正标准[标样]", "Calibration Stand");
			btnKAlpha.Text = Lang.PS("载入K , Alpha", "Load K , Alpha");
			btnMethod.Text = Lang.PS("方法...", "Method...");
			gbControl.Text = Lang.PS("控制", "Control");
			btnRun.Text = Lang.PS("运行", "Run");
			btnStop.Text = Lang.PS("停止", "Stop");
			btnAbort.Text = Lang.PS("取消", "Abort");
			btnSnapshot.Text = Lang.PS("快照", "Snapshot");
			lbChromFileName.Text = Lang.PS("谱图文件名", "Chrom File Name");
			cbFileOverwrite.Text = Lang.PS("支持覆盖文件", "Enable File Overwrite");
			lbCounter.Text = Lang.PS("计数器", "Counter");
			btnDataRecovery.Text = Lang.PS("数据恢复", "DataRecovery...");
			break;
		case SysLanguage.EN:
			Text = "Single Analysis";
			gbAnalysis.Text = "Analysis";
			lbSampleID.Text = "Sample ID";
			lbSample.Text = "Sample";
			lbAmount.Text = "Amount";
			lbISTDAmount.Text = "ISTD Amount";
			lbDilution.Text = "Dilution";
			lbInjVolume.Text = "Inj. Volume";
			cbCalibrationStand.Text = "Calibration Stand";
			btnKAlpha.Text = "Load K , Alpha";
			btnMethod.Text = "Method...";
			gbControl.Text = "Control";
			btnRun.Text = "Run";
			btnStop.Text = "Stop";
			btnAbort.Text = "Abort";
			btnSnapshot.Text = "Snapshot";
			lbChromFileName.Text = "Chrom File Name";
			cbFileOverwrite.Text = "Enable File Overwrite";
			lbCounter.Text = "Counter";
			btnDataRecovery.Text = "DataRecovery...";
			break;
		}
		cms_InfoParasFMT_0.LoadLanguage();
	}

	private void method_0(AccStyle accStyle_0, Injection injection_1)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
			tbSampleID.Text = injection_1.sampleID;
			tbSample.Text = injection_1.sample;
			tbAmount.Text = injection_1.amount.ToString();
			tbISTDAmount.Text = injection_1.ISTD_amount.ToString();
			tbDilution.Text = injection_1.dilution.ToString();
			tbInjVolume.Text = injection_1.inj_volume.ToString();
			tbK.Text = injection_1.gpc_k.ToString();
			tbAlpha.Text = injection_1.gpc_alpha.ToString();
			cbCalibrationStand.Checked = injection_1.cali_stand;
			tbFileNameFMT.Text = injection_1.fileNameFMT;
			tbCounter.Text = injection_1.counter.ToString();
			break;
		case AccStyle.Write:
			injection_1.sampleID = tbSampleID.Text;
			injection_1.sample = tbSample.Text;
			injection_1.amount = Class49.String2Float(tbAmount.Text, 0f);
			injection_1.ISTD_amount = Class49.String2Float(tbISTDAmount.Text, 0f);
			injection_1.dilution = Class49.String2Float(tbDilution.Text, 1f);
			injection_1.inj_volume = Class49.String2Float(tbInjVolume.Text, 0f);
			injection_1.gpc_k = Class49.String2Float(tbK.Text, 0f);
			injection_1.gpc_alpha = Class49.String2Float(tbAlpha.Text, 0f);
			injection_1.cali_stand = cbCalibrationStand.Checked;
			injection_1.fileNameFMT = tbFileNameFMT.Text;
			injection_1.counter = Class49.Object2Int(tbCounter.Text, injection_1.counter);
			break;
		}
	}

	public new DialogResult ShowDialog()
	{
		if (instrument.instruStyle == InstruStyle.GPC)
		{
			gbAnalysis.Height = 194;
		}
		else
		{
			gbAnalysis.Height = 144;
		}
		LclLabel lclLabel = lbK;
		LclLabel lclLabel2 = lbAlpha;
		LclTextBox lclTextBox = tbK;
		LclTextBox lclTextBox2 = tbAlpha;
		bool flag = (btnKAlpha.Visible = instrument.instruStyle == InstruStyle.GPC);
		bool flag3 = (lclTextBox2.Visible = flag);
		bool flag5 = (lclTextBox.Visible = flag3);
		bool visible = (lclLabel2.Visible = flag5);
		lclLabel.Visible = visible;
		btnMethod.Top = gbAnalysis.Height - 29;
		cbCalibrationStand.Top = btnMethod.Top + 4;
		gbControl.Top = gbAnalysis.Bottom + 5;
		base.Height = gbControl.Bottom + 75;
		method_0(AccStyle.Read, injection_0);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_0(AccStyle.Write, injection_0);
		}
		return dialogResult;
	}

	private void SglAlyDlg_Load(object sender, EventArgs e)
	{
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
		this.gbAnalysis = new IBrainChrom2018.LclGroupBox();
		this.btnMethod = new IBrainChrom2018.LclButton();
		this.btnKAlpha = new IBrainChrom2018.LclButton();
		this.cbCalibrationStand = new IBrainChrom2018.LclCheckBox();
		this.tbAlpha = new IBrainChrom2018.LclTextBox();
		this.tbK = new IBrainChrom2018.LclTextBox();
		this.tbInjVolume = new IBrainChrom2018.LclTextBox();
		this.tbDilution = new IBrainChrom2018.LclTextBox();
		this.tbISTDAmount = new IBrainChrom2018.LclTextBox();
		this.tbAmount = new IBrainChrom2018.LclTextBox();
		this.tbSample = new IBrainChrom2018.LclTextBox();
		this.lbAlpha = new IBrainChrom2018.LclLabel();
		this.tbSampleID = new IBrainChrom2018.LclTextBox();
		this.lbInjVolume = new IBrainChrom2018.LclLabel();
		this.lbK = new IBrainChrom2018.LclLabel();
		this.lbISTDAmount = new IBrainChrom2018.LclLabel();
		this.lbDilution = new IBrainChrom2018.LclLabel();
		this.lbAmount = new IBrainChrom2018.LclLabel();
		this.lbSample = new IBrainChrom2018.LclLabel();
		this.lbSampleID = new IBrainChrom2018.LclLabel();
		this.gbControl = new IBrainChrom2018.LclGroupBox();
		this.btnFileNameMenu = new IBrainChrom2018.LclButton();
		this.btnBrowse = new IBrainChrom2018.LclButton();
		this.cbFileOverwrite = new IBrainChrom2018.LclCheckBox();
		this.btnDataRecovery = new IBrainChrom2018.LclButton();
		this.btnSnapshot = new IBrainChrom2018.LclButton();
		this.btnAbort = new IBrainChrom2018.LclButton();
		this.btnStop = new IBrainChrom2018.LclButton();
		this.tbCounter = new IBrainChrom2018.LclTextBox();
		this.btnRun = new IBrainChrom2018.LclButton();
		this.lbChromFileName = new IBrainChrom2018.LclLabel();
		this.tbFileNameFMT = new IBrainChrom2018.LclTextBox();
		this.lbCounter = new IBrainChrom2018.LclLabel();
		this.gbAnalysis.SuspendLayout();
		this.gbControl.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		base.btnCancel.Location = new System.Drawing.Point(277, 336);
		base.btnCancel.Text = "取消";
		base.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		base.btnHelp.Location = new System.Drawing.Point(369, 336);
		base.btnHelp.Text = "帮助";
		base.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		base.btnOK.Location = new System.Drawing.Point(187, 336);
		base.btnOK.Text = "确认";
		this.gbAnalysis.Controls.Add(this.btnMethod);
		this.gbAnalysis.Controls.Add(this.btnKAlpha);
		this.gbAnalysis.Controls.Add(this.cbCalibrationStand);
		this.gbAnalysis.Controls.Add(this.tbAlpha);
		this.gbAnalysis.Controls.Add(this.tbK);
		this.gbAnalysis.Controls.Add(this.tbInjVolume);
		this.gbAnalysis.Controls.Add(this.tbDilution);
		this.gbAnalysis.Controls.Add(this.tbISTDAmount);
		this.gbAnalysis.Controls.Add(this.tbAmount);
		this.gbAnalysis.Controls.Add(this.tbSample);
		this.gbAnalysis.Controls.Add(this.lbAlpha);
		this.gbAnalysis.Controls.Add(this.tbSampleID);
		this.gbAnalysis.Controls.Add(this.lbInjVolume);
		this.gbAnalysis.Controls.Add(this.lbK);
		this.gbAnalysis.Controls.Add(this.lbISTDAmount);
		this.gbAnalysis.Controls.Add(this.lbDilution);
		this.gbAnalysis.Controls.Add(this.lbAmount);
		this.gbAnalysis.Controls.Add(this.lbSample);
		this.gbAnalysis.Controls.Add(this.lbSampleID);
		this.gbAnalysis.Location = new System.Drawing.Point(8, 9);
		this.gbAnalysis.Name = "gbAnalysis";
		this.gbAnalysis.Size = new System.Drawing.Size(452, 194);
		this.gbAnalysis.TabIndex = 3;
		this.gbAnalysis.TabStop = false;
		this.gbAnalysis.Text = "分析";
		this.btnMethod.Location = new System.Drawing.Point(319, 164);
		this.btnMethod.Name = "btnMethod";
		this.btnMethod.Size = new System.Drawing.Size(123, 23);
		this.btnMethod.TabIndex = 3;
		this.btnMethod.Text = "方法...";
		this.btnMethod.UseVisualStyleBackColor = true;
		this.btnKAlpha.Location = new System.Drawing.Point(319, 139);
		this.btnKAlpha.Name = "btnKAlpha";
		this.btnKAlpha.Size = new System.Drawing.Size(123, 23);
		this.btnKAlpha.TabIndex = 3;
		this.btnKAlpha.Text = "载入K , Alpha";
		this.btnKAlpha.UseVisualStyleBackColor = true;
		this.btnKAlpha.Click += new System.EventHandler(btnKAlpha_Click);
		this.cbCalibrationStand.AutoSize = true;
		this.cbCalibrationStand.Location = new System.Drawing.Point(92, 168);
		this.cbCalibrationStand.Name = "cbCalibrationStand";
		this.cbCalibrationStand.Size = new System.Drawing.Size(108, 16);
		this.cbCalibrationStand.TabIndex = 2;
		this.cbCalibrationStand.Text = "校正标准[标样]";
		this.cbCalibrationStand.UseVisualStyleBackColor = true;
		this.tbAlpha.Location = new System.Drawing.Point(319, 115);
		this.tbAlpha.Name = "tbAlpha";
		this.tbAlpha.Size = new System.Drawing.Size(123, 21);
		this.tbAlpha.TabIndex = 1;
		this.tbK.Location = new System.Drawing.Point(92, 115);
		this.tbK.Name = "tbK";
		this.tbK.Size = new System.Drawing.Size(123, 21);
		this.tbK.TabIndex = 1;
		this.tbInjVolume.Location = new System.Drawing.Point(319, 90);
		this.tbInjVolume.Name = "tbInjVolume";
		this.tbInjVolume.Size = new System.Drawing.Size(123, 21);
		this.tbInjVolume.TabIndex = 1;
		this.tbDilution.Location = new System.Drawing.Point(92, 90);
		this.tbDilution.Name = "tbDilution";
		this.tbDilution.Size = new System.Drawing.Size(123, 21);
		this.tbDilution.TabIndex = 1;
		this.tbISTDAmount.Location = new System.Drawing.Point(319, 65);
		this.tbISTDAmount.Name = "tbISTDAmount";
		this.tbISTDAmount.Size = new System.Drawing.Size(123, 21);
		this.tbISTDAmount.TabIndex = 1;
		this.tbAmount.Location = new System.Drawing.Point(92, 65);
		this.tbAmount.Name = "tbAmount";
		this.tbAmount.Size = new System.Drawing.Size(123, 21);
		this.tbAmount.TabIndex = 1;
		this.tbSample.Location = new System.Drawing.Point(92, 40);
		this.tbSample.Name = "tbSample";
		this.tbSample.Size = new System.Drawing.Size(350, 21);
		this.tbSample.TabIndex = 1;
		this.lbAlpha.AutoSize = true;
		this.lbAlpha.Location = new System.Drawing.Point(237, 119);
		this.lbAlpha.Name = "lbAlpha";
		this.lbAlpha.Size = new System.Drawing.Size(35, 12);
		this.lbAlpha.TabIndex = 0;
		this.lbAlpha.Text = "Alpha";
		this.tbSampleID.Location = new System.Drawing.Point(92, 15);
		this.tbSampleID.Name = "tbSampleID";
		this.tbSampleID.Size = new System.Drawing.Size(350, 21);
		this.tbSampleID.TabIndex = 1;
		this.lbInjVolume.AutoSize = true;
		this.lbInjVolume.Location = new System.Drawing.Point(236, 95);
		this.lbInjVolume.Name = "lbInjVolume";
		this.lbInjVolume.Size = new System.Drawing.Size(53, 12);
		this.lbInjVolume.TabIndex = 0;
		this.lbInjVolume.Text = "进样体积";
		this.lbK.AutoSize = true;
		this.lbK.Location = new System.Drawing.Point(10, 120);
		this.lbK.Name = "lbK";
		this.lbK.Size = new System.Drawing.Size(77, 12);
		this.lbK.TabIndex = 0;
		this.lbK.Text = "K[dL/g*10^3]";
		this.lbISTDAmount.AutoSize = true;
		this.lbISTDAmount.Location = new System.Drawing.Point(237, 70);
		this.lbISTDAmount.Name = "lbISTDAmount";
		this.lbISTDAmount.Size = new System.Drawing.Size(53, 12);
		this.lbISTDAmount.TabIndex = 0;
		this.lbISTDAmount.Text = "ISTD数量";
		this.lbDilution.AutoSize = true;
		this.lbDilution.Location = new System.Drawing.Point(9, 95);
		this.lbDilution.Name = "lbDilution";
		this.lbDilution.Size = new System.Drawing.Size(29, 12);
		this.lbDilution.TabIndex = 0;
		this.lbDilution.Text = "稀释";
		this.lbAmount.AutoSize = true;
		this.lbAmount.Location = new System.Drawing.Point(9, 70);
		this.lbAmount.Name = "lbAmount";
		this.lbAmount.Size = new System.Drawing.Size(29, 12);
		this.lbAmount.TabIndex = 0;
		this.lbAmount.Text = "数量";
		this.lbSample.AutoSize = true;
		this.lbSample.Location = new System.Drawing.Point(10, 43);
		this.lbSample.Name = "lbSample";
		this.lbSample.Size = new System.Drawing.Size(29, 12);
		this.lbSample.TabIndex = 0;
		this.lbSample.Text = "样品";
		this.lbSampleID.AutoSize = true;
		this.lbSampleID.Location = new System.Drawing.Point(9, 18);
		this.lbSampleID.Name = "lbSampleID";
		this.lbSampleID.Size = new System.Drawing.Size(41, 12);
		this.lbSampleID.TabIndex = 0;
		this.lbSampleID.Text = "样品ID";
		this.gbControl.Controls.Add(this.btnFileNameMenu);
		this.gbControl.Controls.Add(this.btnBrowse);
		this.gbControl.Controls.Add(this.cbFileOverwrite);
		this.gbControl.Controls.Add(this.btnDataRecovery);
		this.gbControl.Controls.Add(this.btnSnapshot);
		this.gbControl.Controls.Add(this.btnAbort);
		this.gbControl.Controls.Add(this.btnStop);
		this.gbControl.Controls.Add(this.tbCounter);
		this.gbControl.Controls.Add(this.btnRun);
		this.gbControl.Controls.Add(this.lbChromFileName);
		this.gbControl.Controls.Add(this.tbFileNameFMT);
		this.gbControl.Controls.Add(this.lbCounter);
		this.gbControl.Location = new System.Drawing.Point(8, 212);
		this.gbControl.Name = "gbControl";
		this.gbControl.Size = new System.Drawing.Size(452, 118);
		this.gbControl.TabIndex = 4;
		this.gbControl.TabStop = false;
		this.gbControl.Text = "控制";
		this.btnFileNameMenu.Location = new System.Drawing.Point(375, 59);
		this.btnFileNameMenu.Name = "btnFileNameMenu";
		this.btnFileNameMenu.Size = new System.Drawing.Size(32, 23);
		this.btnFileNameMenu.TabIndex = 3;
		this.btnFileNameMenu.UseVisualStyleBackColor = true;
		this.btnFileNameMenu.Click += new System.EventHandler(btnFileNameMenu_Click);
		this.btnBrowse.Location = new System.Drawing.Point(410, 59);
		this.btnBrowse.Name = "btnBrowse";
		this.btnBrowse.Size = new System.Drawing.Size(32, 23);
		this.btnBrowse.TabIndex = 3;
		this.btnBrowse.Text = "...";
		this.btnBrowse.UseVisualStyleBackColor = true;
		this.btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
		this.cbFileOverwrite.AutoSize = true;
		this.cbFileOverwrite.Location = new System.Drawing.Point(12, 92);
		this.cbFileOverwrite.Name = "cbFileOverwrite";
		this.cbFileOverwrite.Size = new System.Drawing.Size(96, 16);
		this.cbFileOverwrite.TabIndex = 2;
		this.cbFileOverwrite.Text = "支持覆盖文件";
		this.cbFileOverwrite.UseVisualStyleBackColor = true;
		this.btnDataRecovery.Location = new System.Drawing.Point(319, 88);
		this.btnDataRecovery.Name = "btnDataRecovery";
		this.btnDataRecovery.Size = new System.Drawing.Size(123, 23);
		this.btnDataRecovery.TabIndex = 3;
		this.btnDataRecovery.Text = "数据恢复";
		this.btnDataRecovery.UseVisualStyleBackColor = true;
		this.btnDataRecovery.Click += new System.EventHandler(btnDataRecovery_Click);
		this.btnSnapshot.Location = new System.Drawing.Point(375, 20);
		this.btnSnapshot.Name = "btnSnapshot";
		this.btnSnapshot.Size = new System.Drawing.Size(67, 23);
		this.btnSnapshot.TabIndex = 3;
		this.btnSnapshot.Text = "快照";
		this.btnSnapshot.UseVisualStyleBackColor = true;
		this.btnSnapshot.Click += new System.EventHandler(btnSnapshot_Click);
		this.btnAbort.Location = new System.Drawing.Point(157, 20);
		this.btnAbort.Name = "btnAbort";
		this.btnAbort.Size = new System.Drawing.Size(67, 23);
		this.btnAbort.TabIndex = 3;
		this.btnAbort.Text = "取消";
		this.btnAbort.UseVisualStyleBackColor = true;
		this.btnAbort.Click += new System.EventHandler(btnAbort_Click);
		this.btnStop.Location = new System.Drawing.Point(84, 20);
		this.btnStop.Name = "btnStop";
		this.btnStop.Size = new System.Drawing.Size(67, 23);
		this.btnStop.TabIndex = 3;
		this.btnStop.Text = "停止";
		this.btnStop.UseVisualStyleBackColor = true;
		this.btnStop.Click += new System.EventHandler(btnStop_Click);
		this.tbCounter.Location = new System.Drawing.Point(212, 88);
		this.tbCounter.Name = "tbCounter";
		this.tbCounter.Size = new System.Drawing.Size(60, 21);
		this.tbCounter.TabIndex = 1;
		this.btnRun.Location = new System.Drawing.Point(11, 20);
		this.btnRun.Name = "btnRun";
		this.btnRun.Size = new System.Drawing.Size(67, 23);
		this.btnRun.TabIndex = 3;
		this.btnRun.Text = "运行";
		this.btnRun.UseVisualStyleBackColor = true;
		this.btnRun.Click += new System.EventHandler(btnRun_Click);
		this.lbChromFileName.AutoSize = true;
		this.lbChromFileName.Location = new System.Drawing.Point(10, 46);
		this.lbChromFileName.Name = "lbChromFileName";
		this.lbChromFileName.Size = new System.Drawing.Size(65, 12);
		this.lbChromFileName.TabIndex = 0;
		this.lbChromFileName.Text = "谱图文件名";
		this.tbFileNameFMT.Location = new System.Drawing.Point(12, 61);
		this.tbFileNameFMT.Name = "tbFileNameFMT";
		this.tbFileNameFMT.Size = new System.Drawing.Size(357, 21);
		this.tbFileNameFMT.TabIndex = 1;
		this.lbCounter.AutoSize = true;
		this.lbCounter.Location = new System.Drawing.Point(147, 93);
		this.lbCounter.Name = "lbCounter";
		this.lbCounter.Size = new System.Drawing.Size(41, 12);
		this.lbCounter.TabIndex = 0;
		this.lbCounter.Text = "计数器";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(469, 367);
		base.Controls.Add(this.gbControl);
		base.Controls.Add(this.gbAnalysis);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
		base.Name = "SglAlyDlg";
		this.Text = "单针分析";
		base.Load += new System.EventHandler(SglAlyDlg_Load);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.gbAnalysis, 0);
		base.Controls.SetChildIndex(this.gbControl, 0);
		this.gbAnalysis.ResumeLayout(false);
		this.gbAnalysis.PerformLayout();
		this.gbControl.ResumeLayout(false);
		this.gbControl.PerformLayout();
		base.ResumeLayout(false);
	}
}
