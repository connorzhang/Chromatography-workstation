using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SSTParasDlg : LclDialog
{
	private const string string_0 = "计算方法";

	private const string string_1 = "分析员    :";

	private const string string_2 = "创建      :";

	private const string string_3 = "描述      ";

	private const string string_4 = "EP(欧洲标准)";

	private const string string_5 = "JP(日本标准)";

	private const string string_6 = "USP(美国标准)";

	private const string string_7 = "SST属性";

	private const string string_8 = "常规";

	private const string string_9 = "参数.计算";

	private const string string_10 = "Calculate By";

	private const string string_11 = "Analyst   :";

	private const string string_12 = "Created   :";

	private const string string_13 = "Description";

	private const string string_14 = "EP(European Pharmacopeia)";

	private const string string_15 = "JP(Japanese Pharmacopeia)";

	private const string string_16 = "USP(United States Pharmacopeia)";

	private const string string_17 = "SST Properties";

	private const string string_18 = "General";

	private const string string_19 = "Parameters.Calculate By";

	private IContainer icontainer_1;

	private LclGroupBox gbpcCalcuBy;

	private LclLabel lbgnlAnalyst;

	private LclLabel lbgnlAnalystV;

	private LclLabel lbgnlCreated;

	private LclLabel lbgnlCreatedV;

	private LclLabel lbgnlDescription;

	private LclRadioButton rbcaEP;

	private LclRadioButton rbcaJP;

	private LclRadioButton rbcaUSP;

	private LclTextBox tbgnlDescription;

	private LclTabControl tcMain;

	private TabPage tpGeneral;

	private TabPage tpParasCalcu;

	public SSTParasDlg()
	{
		InitializeComponent();
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
		this.tcMain = new IBrainChrom2018.LclTabControl();
		this.tpGeneral = new System.Windows.Forms.TabPage();
		this.tbgnlDescription = new IBrainChrom2018.LclTextBox();
		this.lbgnlCreatedV = new IBrainChrom2018.LclLabel();
		this.lbgnlAnalystV = new IBrainChrom2018.LclLabel();
		this.lbgnlDescription = new IBrainChrom2018.LclLabel();
		this.lbgnlAnalyst = new IBrainChrom2018.LclLabel();
		this.lbgnlCreated = new IBrainChrom2018.LclLabel();
		this.tpParasCalcu = new System.Windows.Forms.TabPage();
		this.gbpcCalcuBy = new IBrainChrom2018.LclGroupBox();
		this.rbcaJP = new IBrainChrom2018.LclRadioButton();
		this.rbcaUSP = new IBrainChrom2018.LclRadioButton();
		this.rbcaEP = new IBrainChrom2018.LclRadioButton();
		this.tcMain.SuspendLayout();
		this.tpGeneral.SuspendLayout();
		this.tpParasCalcu.SuspendLayout();
		this.gbpcCalcuBy.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(166, 174);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(258, 174);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(76, 174);
		base.btnOK.Text = "确认";
		this.tcMain.Controls.Add(this.tpGeneral);
		this.tcMain.Controls.Add(this.tpParasCalcu);
		this.tcMain.ItemSize = new System.Drawing.Size(90, 19);
		this.tcMain.Location = new System.Drawing.Point(12, 12);
		this.tcMain.Name = "tcMain";
		this.tcMain.SelectedIndex = 0;
		this.tcMain.Size = new System.Drawing.Size(372, 151);
		this.tcMain.TabIndex = 1;
		this.tpGeneral.Controls.Add(this.tbgnlDescription);
		this.tpGeneral.Controls.Add(this.lbgnlCreatedV);
		this.tpGeneral.Controls.Add(this.lbgnlAnalystV);
		this.tpGeneral.Controls.Add(this.lbgnlDescription);
		this.tpGeneral.Controls.Add(this.lbgnlAnalyst);
		this.tpGeneral.Controls.Add(this.lbgnlCreated);
		this.tpGeneral.Location = new System.Drawing.Point(4, 23);
		this.tpGeneral.Name = "tpGeneral";
		this.tpGeneral.Size = new System.Drawing.Size(364, 124);
		this.tpGeneral.TabIndex = 0;
		this.tpGeneral.Text = "tabPage1";
		this.tpGeneral.UseVisualStyleBackColor = true;
		this.tbgnlDescription.Location = new System.Drawing.Point(110, 75);
		this.tbgnlDescription.Name = "tbgnlDescription";
		this.tbgnlDescription.Size = new System.Drawing.Size(235, 21);
		this.tbgnlDescription.TabIndex = 7;
		this.lbgnlCreatedV.AutoSize = true;
		this.lbgnlCreatedV.Location = new System.Drawing.Point(108, 28);
		this.lbgnlCreatedV.Name = "lbgnlCreatedV";
		this.lbgnlCreatedV.Size = new System.Drawing.Size(95, 12);
		this.lbgnlCreatedV.TabIndex = 6;
		this.lbgnlCreatedV.Text = "2008/10/17 8:00";
		this.lbgnlAnalystV.AutoSize = true;
		this.lbgnlAnalystV.Location = new System.Drawing.Point(108, 52);
		this.lbgnlAnalystV.Name = "lbgnlAnalystV";
		this.lbgnlAnalystV.Size = new System.Drawing.Size(59, 12);
		this.lbgnlAnalystV.TabIndex = 5;
		this.lbgnlAnalystV.Text = "Anonymous";
		this.lbgnlDescription.AutoSize = true;
		this.lbgnlDescription.Location = new System.Drawing.Point(20, 78);
		this.lbgnlDescription.Name = "lbgnlDescription";
		this.lbgnlDescription.Size = new System.Drawing.Size(59, 12);
		this.lbgnlDescription.TabIndex = 2;
		this.lbgnlDescription.Text = "lclLabel1";
		this.lbgnlAnalyst.AutoSize = true;
		this.lbgnlAnalyst.Location = new System.Drawing.Point(20, 52);
		this.lbgnlAnalyst.Name = "lbgnlAnalyst";
		this.lbgnlAnalyst.Size = new System.Drawing.Size(59, 12);
		this.lbgnlAnalyst.TabIndex = 3;
		this.lbgnlAnalyst.Text = "lclLabel1";
		this.lbgnlCreated.AutoSize = true;
		this.lbgnlCreated.Location = new System.Drawing.Point(20, 28);
		this.lbgnlCreated.Name = "lbgnlCreated";
		this.lbgnlCreated.Size = new System.Drawing.Size(59, 12);
		this.lbgnlCreated.TabIndex = 4;
		this.lbgnlCreated.Text = "lclLabel1";
		this.tpParasCalcu.Controls.Add(this.gbpcCalcuBy);
		this.tpParasCalcu.Location = new System.Drawing.Point(4, 23);
		this.tpParasCalcu.Name = "tpParasCalcu";
		this.tpParasCalcu.Size = new System.Drawing.Size(364, 124);
		this.tpParasCalcu.TabIndex = 1;
		this.tpParasCalcu.Text = "tabPage2";
		this.tpParasCalcu.UseVisualStyleBackColor = true;
		this.gbpcCalcuBy.Controls.Add(this.rbcaJP);
		this.gbpcCalcuBy.Controls.Add(this.rbcaUSP);
		this.gbpcCalcuBy.Controls.Add(this.rbcaEP);
		this.gbpcCalcuBy.Location = new System.Drawing.Point(26, 17);
		this.gbpcCalcuBy.Name = "gbpcCalcuBy";
		this.gbpcCalcuBy.Size = new System.Drawing.Size(199, 91);
		this.gbpcCalcuBy.TabIndex = 0;
		this.gbpcCalcuBy.TabStop = false;
		this.gbpcCalcuBy.Text = "lclGroupBox1";
		this.rbcaJP.AutoSize = true;
		this.rbcaJP.Location = new System.Drawing.Point(6, 63);
		this.rbcaJP.Name = "rbcaJP";
		this.rbcaJP.Size = new System.Drawing.Size(113, 16);
		this.rbcaJP.TabIndex = 0;
		this.rbcaJP.TabStop = true;
		this.rbcaJP.Text = "lclRadioButton1";
		this.rbcaJP.UseVisualStyleBackColor = true;
		this.rbcaUSP.AutoSize = true;
		this.rbcaUSP.Location = new System.Drawing.Point(6, 41);
		this.rbcaUSP.Name = "rbcaUSP";
		this.rbcaUSP.Size = new System.Drawing.Size(113, 16);
		this.rbcaUSP.TabIndex = 0;
		this.rbcaUSP.TabStop = true;
		this.rbcaUSP.Text = "lclRadioButton1";
		this.rbcaUSP.UseVisualStyleBackColor = true;
		this.rbcaEP.AutoSize = true;
		this.rbcaEP.Location = new System.Drawing.Point(6, 20);
		this.rbcaEP.Name = "rbcaEP";
		this.rbcaEP.Size = new System.Drawing.Size(113, 16);
		this.rbcaEP.TabIndex = 0;
		this.rbcaEP.TabStop = true;
		this.rbcaEP.Text = "lclRadioButton1";
		this.rbcaEP.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(396, 204);
		base.Controls.Add(this.tcMain);
		base.Name = "SSTParasDlg";
		this.Text = "SST属性";
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.tcMain, 0);
		this.tcMain.ResumeLayout(false);
		this.tpGeneral.ResumeLayout(false);
		this.tpGeneral.PerformLayout();
		this.tpParasCalcu.ResumeLayout(false);
		this.gbpcCalcuBy.ResumeLayout(false);
		this.gbpcCalcuBy.PerformLayout();
		base.ResumeLayout(false);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "SST属性";
			tpGeneral.Text = "常规";
			lbgnlCreated.Text = "创建      :";
			lbgnlAnalyst.Text = "分析员    :";
			lbgnlDescription.Text = "描述      ";
			tpParasCalcu.Text = "参数.计算";
			gbpcCalcuBy.Text = "计算方法";
			rbcaEP.Text = "EP(欧洲标准)";
			rbcaUSP.Text = "USP(美国标准)";
			rbcaJP.Text = "JP(日本标准)";
			break;
		case SysLanguage.EN:
			Text = "SST Properties";
			tpGeneral.Text = "General";
			lbgnlCreated.Text = "Created   :";
			lbgnlAnalyst.Text = "Analyst   :";
			lbgnlDescription.Text = "Description";
			tpParasCalcu.Text = "Parameters.Calculate By";
			gbpcCalcuBy.Text = "Calculate By";
			rbcaEP.Text = "EP(European Pharmacopeia)";
			rbcaUSP.Text = "USP(United States Pharmacopeia)";
			rbcaJP.Text = "JP(Japanese Pharmacopeia)";
			break;
		}
	}

	private void method_0(AccStyle accStyle_0, SSTParas sstparas_0)
	{
		if (accStyle_0 == AccStyle.Read)
		{
			DateTime dtCreate = sstparas_0.dtCreate;
			lbgnlCreatedV.Text = dtCreate.ToLongDateString() + " " + dtCreate.ToShortTimeString();
			lbgnlAnalystV.Text = sstparas_0.userName;
			tbgnlDescription.Text = sstparas_0.description;
			if (sstparas_0.criterion == SSTCriterion.EP)
			{
				rbcaEP.Checked = true;
			}
			else if (sstparas_0.criterion == SSTCriterion.USP)
			{
				rbcaUSP.Checked = true;
			}
			else if (sstparas_0.criterion == SSTCriterion.JP)
			{
				rbcaJP.Checked = true;
			}
		}
		if (accStyle_0 == AccStyle.Write)
		{
			sstparas_0.description = tbgnlDescription.Text;
			if (rbcaEP.Checked)
			{
				sstparas_0.criterion = SSTCriterion.EP;
			}
			else if (rbcaUSP.Checked)
			{
				sstparas_0.criterion = SSTCriterion.USP;
			}
			else if (rbcaJP.Checked)
			{
				sstparas_0.criterion = SSTCriterion.JP;
			}
		}
	}

	public DialogResult ShowDialog(SSTParas sstParas)
	{
		method_0(AccStyle.Read, sstParas);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_0(AccStyle.Write, sstParas);
		}
		return dialogResult;
	}
}
