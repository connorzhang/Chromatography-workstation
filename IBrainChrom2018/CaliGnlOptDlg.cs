using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class CaliGnlOptDlg : LclDialog
{
	public delegate void SetAllCmpds(CaliGnlOpt caliOption);

	private FormMainParam frmParam = FormMainParam.Create();

	private bool bool_1;

	private SetAllCmpds setAllCmpds_0;

	private LclButton btnSetAll;

	private LclCusComboBox cbCurveFit;

	private LclCusComboBox cbDisplayMode;

	private LclCusComboBox cbOriginal;

	private LclCusComboBox cbRespStyle;

	private LclCheckBox cbUpdateRT;

	private IContainer icontainer_1;

	private LclGroupBox gbRecalibration;

	private LclLabel lbCmpdUnits;

	private LclLabel lbCurveFit;

	private LclLabel lbDescription;

	private LclLabel lbDisplayMode;

	private LclLabel lbLeftWindow;

	private LclLabel lbOriginal;

	private LclLabel lbRespStyle;

	private LclLabel lbRightWindow;

	private LclRadioButton rbAverage;

	private LclRadioButton rbReplace;

	private LclTextBox tbCmpdUnits;

	private LclTextBox tbDescription;

	private LclTextBox tbLeftWindow;

	private LclTextBox tbRightWindow;

	private LclTabControl tcCali;

	private TabPage tpDefaults;

	private TabPage tpOptions;

	private CheckBox chbMiniTime;

	private CheckBox chbTimeSlot;

	public static string sAverage => Lang.PS("平均", "Average");

	public static string sCmpdUnits => Lang.PS("组分单位", "Compound Unit");

	public static string sDescription => Lang.PS("描述", "Description");

	public static string sDisplayMode => Lang.PS("显示模式", "Display Mode");

	public static string sdmESTD => Lang.PS("外标模式", "ESTD Style");

	public static string sdmISTD => Lang.PS("内标模式", "ISTD Style");

	public static string sRecalibration => Lang.PS("再校正", "Recalibration");

	public static string sReplace => Lang.PS("替换", "Replace");

	public static string sUpdateRT => Lang.PS("刷新保留时间", "Update Retain Time");

	public event SetAllCmpds OnSetAllCmpds
	{
		add
		{
			SetAllCmpds setAllCmpds = setAllCmpds_0;
			SetAllCmpds setAllCmpds2;
			do
			{
				setAllCmpds2 = setAllCmpds;
				SetAllCmpds value2 = (SetAllCmpds)Delegate.Combine(setAllCmpds2, value);
				setAllCmpds = Interlocked.CompareExchange(ref setAllCmpds_0, value2, setAllCmpds2);
			}
			while (setAllCmpds != setAllCmpds2);
		}
		remove
		{
			SetAllCmpds setAllCmpds = setAllCmpds_0;
			SetAllCmpds setAllCmpds2;
			do
			{
				setAllCmpds2 = setAllCmpds;
				SetAllCmpds value2 = (SetAllCmpds)Delegate.Remove(setAllCmpds2, value);
				setAllCmpds = Interlocked.CompareExchange(ref setAllCmpds_0, value2, setAllCmpds2);
			}
			while (setAllCmpds != setAllCmpds2);
		}
	}

	public CaliGnlOptDlg()
	{
		InitializeComponent();
		cbDisplayMode.InitItems(new object[2]
		{
			CaliDisMode.Estd,
			CaliDisMode.Istd
		});
		cbDisplayMode.InitShowText(new string[2]
		{
			Lang.PS("外标模式", "ESTD Style"),
			Lang.PS("内标模式", "ISTD Style")
		});
		cbRespStyle.InitItems(new object[4]
		{
			RespStyle.Area,
			RespStyle.Height,
			RespStyle.AreaSquare,
			RespStyle.PeakHeightSquare
		});
		cbRespStyle.InitShowText(new string[4]
		{
			Lang.PS("面积", "Area"),
			Lang.PS("高度", "Height"),
			Lang.PS("面积平方根", "AreaSquare"),
			Lang.PS("高度平方根", "PeakHeightSquare")
		});
		cbOriginal.InitItems(new object[3]
		{
			Original.Ignore,
			Original.With,
			Original.Pass
		});
		cbOriginal.InitShowText(new string[3]
		{
			Lang.PS("忽略", "Ignore"),
			Lang.PS("考虑", "Compute with"),
			Lang.PS("经过", "Pass through")
		});
		cbCurveFit.InitItems(new object[6]
		{
			CurveFit.Free,
			CurveFit.PtToPt,
			CurveFit.Linear,
			CurveFit.Quadratic,
			CurveFit.Cubic,
			CurveFit.Exponent
		});
		cbCurveFit.InitShowText(new string[6]
		{
			Lang.PS("校正归一(基于校正因子)", "Free"),
			Lang.PS("单点校正点到点(基于线性直线)", "Pt to Pt"),
			Lang.PS("单点校正线性(基于线性直线)", "Linear"),
			Lang.PS("多点校正二次(基于工作曲线)", "Quadratic2"),
			Lang.PS("多点校正三次(基于工作曲线)", "Quadratic3"),
			Lang.PS("指数", "Cubic")
		});
		chbTimeSlot.Checked = frmParam.bChannel;
		chbMiniTime.Checked = frmParam.bChanne2;
	}

	private void btnSetAll_Click(object sender, EventArgs e)
	{
		if (bool_1)
		{
			MessageBox.Show(Lang.PS("受限！", "No Right！"));
			return;
		}
		CaliGnlOpt caliGnlOpt = new CaliGnlOpt();
		method_0(AccStyle.Write, caliGnlOpt);
		if (setAllCmpds_0 != null)
		{
			setAllCmpds_0(caliGnlOpt);
		}
		SystemParam systemParam = SystemParam.Create();
		systemParam.iCaliGnlOptReCali = ((!rbReplace.Checked) ? 1 : 0);
		systemParam.SaveParam();
		frmParam.bChannel = chbTimeSlot.Checked;
		frmParam.bChanne2 = chbMiniTime.Checked;
		frmParam.SaveParam();
	}

	public override void LoadLanguage()
	{
		Text = Lang.PS("校正选项", "Calibration Options");
		tpOptions.Text = Lang.PS("校正选项", "Calibration Options");
		lbDescription.Text = Lang.PS("描述", "Description");
		lbDisplayMode.Text = Lang.PS("显示模式", "Display Mode");
		gbRecalibration.Text = Lang.PS("再校正", "Recalibration");
		rbReplace.Text = Lang.PS("替换", "Replace");
		rbAverage.Text = Lang.PS("平均", "Average");
		lbCmpdUnits.Text = Lang.PS("组分单位", "Compound Unit");
		cbUpdateRT.Text = Lang.PS("刷新保留时间", "Update Retain Time");
		tpDefaults.Text = Lang.PS("默认", "Defaults");
		lbRespStyle.Text = Lang.PS("响应类型", "Resp. Style");
		lbOriginal.Text = Lang.PS("原点方案", "Original");
		lbCurveFit.Text = Lang.PS("曲线类型", "Curve Fit");
		lbLeftWindow.Text = Lang.PS("左窗宽", "Left Window");
		lbRightWindow.Text = Lang.PS("右窗宽", "Right Window");
		btnSetAll.Text = Lang.PS("置所有组分", "Set All Compounds");
		btnOK.Text = Lang.PS("确定", "OK");
		btnCancel.Text = Lang.PS("取消", "Cancel");
		btnHelp.Text = Lang.PS("帮助", "Help");
	}

	private void method_0(AccStyle accStyle_0, CaliGnlOpt caliGnlOpt_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Write:
			caliGnlOpt_0.description = tbDescription.Text;
			caliGnlOpt_0.caliDisMode = (CaliDisMode)cbDisplayMode.SelectedIndex;
			caliGnlOpt_0.cmpdUnit = tbCmpdUnits.Text.Trim();
			caliGnlOpt_0.updateRT = cbUpdateRT.Checked;
			if (rbAverage.Checked)
			{
				caliGnlOpt_0.recaliMode = RecaliMode.Average;
			}
			if (rbReplace.Checked)
			{
				caliGnlOpt_0.recaliMode = RecaliMode.Replace;
			}
			caliGnlOpt_0.respStyle = (RespStyle)cbRespStyle.SelectedIndex;
			caliGnlOpt_0.original = (Original)cbOriginal.SelectedIndex;
			caliGnlOpt_0.curveFit = (CurveFit)cbCurveFit.SelectedIndex;
			caliGnlOpt_0.leftWindow = Class49.String2Float(tbLeftWindow.Text, caliGnlOpt_0.leftWindow);
			caliGnlOpt_0.rightWindow = Class49.String2Float(tbRightWindow.Text, caliGnlOpt_0.rightWindow);
			break;
		case AccStyle.Read:
			tbDescription.Text = caliGnlOpt_0.description;
			cbDisplayMode.SelectedIndex = (int)caliGnlOpt_0.caliDisMode;
			tbCmpdUnits.Text = caliGnlOpt_0.cmpdUnit;
			cbUpdateRT.Checked = caliGnlOpt_0.updateRT;
			switch (caliGnlOpt_0.recaliMode)
			{
			case RecaliMode.Replace:
				rbReplace.Checked = true;
				break;
			case RecaliMode.Average:
				rbAverage.Checked = true;
				break;
			}
			try
			{
				cbRespStyle.SelectedIndex = (int)caliGnlOpt_0.respStyle;
				cbOriginal.SelectedIndex = (int)caliGnlOpt_0.original;
				cbCurveFit.SelectedIndex = (int)caliGnlOpt_0.curveFit;
				tbLeftWindow.Text = caliGnlOpt_0.leftWindow.ToString();
				tbRightWindow.Text = caliGnlOpt_0.rightWindow.ToString();
				break;
			}
			catch
			{
				cbRespStyle.SelectedIndex = 0;
				cbOriginal.SelectedIndex = 0;
				cbCurveFit.SelectedIndex = 0;
				tbLeftWindow.Text = "0.1";
				tbRightWindow.Text = "0.1";
				break;
			}
		}
	}

	public DialogResult ShowDialog(CaliGnlOpt caliOption, bool read_only)
	{
		bool_1 = read_only;
		method_0(AccStyle.Read, caliOption);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK && !read_only)
		{
			method_0(AccStyle.Write, caliOption);
		}
		btnSetAll_Click(null, null);
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
		this.tcCali = new IBrainChrom2018.LclTabControl();
		this.tpOptions = new System.Windows.Forms.TabPage();
		this.cbDisplayMode = new IBrainChrom2018.LclCusComboBox();
		this.gbRecalibration = new IBrainChrom2018.LclGroupBox();
		this.rbAverage = new IBrainChrom2018.LclRadioButton();
		this.rbReplace = new IBrainChrom2018.LclRadioButton();
		this.tbCmpdUnits = new IBrainChrom2018.LclTextBox();
		this.tbDescription = new IBrainChrom2018.LclTextBox();
		this.cbUpdateRT = new IBrainChrom2018.LclCheckBox();
		this.lbCmpdUnits = new IBrainChrom2018.LclLabel();
		this.lbDisplayMode = new IBrainChrom2018.LclLabel();
		this.lbDescription = new IBrainChrom2018.LclLabel();
		this.tpDefaults = new System.Windows.Forms.TabPage();
		this.chbMiniTime = new System.Windows.Forms.CheckBox();
		this.chbTimeSlot = new System.Windows.Forms.CheckBox();
		this.cbCurveFit = new IBrainChrom2018.LclCusComboBox();
		this.cbOriginal = new IBrainChrom2018.LclCusComboBox();
		this.cbRespStyle = new IBrainChrom2018.LclCusComboBox();
		this.btnSetAll = new IBrainChrom2018.LclButton();
		this.lbRespStyle = new IBrainChrom2018.LclLabel();
		this.lbOriginal = new IBrainChrom2018.LclLabel();
		this.tbRightWindow = new IBrainChrom2018.LclTextBox();
		this.tbLeftWindow = new IBrainChrom2018.LclTextBox();
		this.lbRightWindow = new IBrainChrom2018.LclLabel();
		this.lbCurveFit = new IBrainChrom2018.LclLabel();
		this.lbLeftWindow = new IBrainChrom2018.LclLabel();
		this.tcCali.SuspendLayout();
		this.tpOptions.SuspendLayout();
		this.gbRecalibration.SuspendLayout();
		this.tpDefaults.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(131, 238);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(231, 238);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(34, 238);
		base.btnOK.Text = "确认";
		this.tcCali.Controls.Add(this.tpOptions);
		this.tcCali.Controls.Add(this.tpDefaults);
		this.tcCali.ItemSize = new System.Drawing.Size(90, 19);
		this.tcCali.Location = new System.Drawing.Point(4, 9);
		this.tcCali.Name = "tcCali";
		this.tcCali.SelectedIndex = 0;
		this.tcCali.Size = new System.Drawing.Size(325, 221);
		this.tcCali.TabIndex = 1;
		this.tpOptions.Controls.Add(this.cbDisplayMode);
		this.tpOptions.Controls.Add(this.gbRecalibration);
		this.tpOptions.Controls.Add(this.tbCmpdUnits);
		this.tpOptions.Controls.Add(this.tbDescription);
		this.tpOptions.Controls.Add(this.cbUpdateRT);
		this.tpOptions.Controls.Add(this.lbCmpdUnits);
		this.tpOptions.Controls.Add(this.lbDisplayMode);
		this.tpOptions.Controls.Add(this.lbDescription);
		this.tpOptions.Location = new System.Drawing.Point(4, 23);
		this.tpOptions.Name = "tpOptions";
		this.tpOptions.Size = new System.Drawing.Size(317, 194);
		this.tpOptions.TabIndex = 0;
		this.tpOptions.Text = "校正选项";
		this.tpOptions.UseVisualStyleBackColor = true;
		this.cbDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbDisplayMode.FormattingEnabled = true;
		this.cbDisplayMode.ItemExtString = "";
		this.cbDisplayMode.Location = new System.Drawing.Point(98, 41);
		this.cbDisplayMode.Name = "cbDisplayMode";
		this.cbDisplayMode.Size = new System.Drawing.Size(214, 20);
		this.cbDisplayMode.TabIndex = 5;
		this.cbDisplayMode.Visible = false;
		this.gbRecalibration.Controls.Add(this.rbAverage);
		this.gbRecalibration.Controls.Add(this.rbReplace);
		this.gbRecalibration.Location = new System.Drawing.Point(15, 124);
		this.gbRecalibration.Name = "gbRecalibration";
		this.gbRecalibration.Size = new System.Drawing.Size(120, 57);
		this.gbRecalibration.TabIndex = 4;
		this.gbRecalibration.TabStop = false;
		this.gbRecalibration.Text = "再校正";
		this.rbAverage.AutoSize = true;
		this.rbAverage.Location = new System.Drawing.Point(10, 35);
		this.rbAverage.Name = "rbAverage";
		this.rbAverage.Size = new System.Drawing.Size(47, 16);
		this.rbAverage.TabIndex = 5;
		this.rbAverage.TabStop = true;
		this.rbAverage.Text = "平均";
		this.rbAverage.UseVisualStyleBackColor = true;
		this.rbReplace.AutoSize = true;
		this.rbReplace.Location = new System.Drawing.Point(10, 16);
		this.rbReplace.Name = "rbReplace";
		this.rbReplace.Size = new System.Drawing.Size(47, 16);
		this.rbReplace.TabIndex = 5;
		this.rbReplace.TabStop = true;
		this.rbReplace.Text = "替换";
		this.rbReplace.UseVisualStyleBackColor = true;
		this.tbCmpdUnits.Location = new System.Drawing.Point(98, 68);
		this.tbCmpdUnits.Name = "tbCmpdUnits";
		this.tbCmpdUnits.Size = new System.Drawing.Size(75, 21);
		this.tbCmpdUnits.TabIndex = 2;
		this.tbCmpdUnits.Text = "g/L";
		this.tbDescription.Location = new System.Drawing.Point(98, 13);
		this.tbDescription.Name = "tbDescription";
		this.tbDescription.Size = new System.Drawing.Size(214, 21);
		this.tbDescription.TabIndex = 2;
		this.cbUpdateRT.AutoSize = true;
		this.cbUpdateRT.Location = new System.Drawing.Point(15, 104);
		this.cbUpdateRT.Name = "cbUpdateRT";
		this.cbUpdateRT.Size = new System.Drawing.Size(96, 16);
		this.cbUpdateRT.TabIndex = 1;
		this.cbUpdateRT.Text = "刷新保留时间";
		this.cbUpdateRT.UseVisualStyleBackColor = true;
		this.lbCmpdUnits.AutoSize = true;
		this.lbCmpdUnits.Location = new System.Drawing.Point(13, 73);
		this.lbCmpdUnits.Name = "lbCmpdUnits";
		this.lbCmpdUnits.Size = new System.Drawing.Size(53, 12);
		this.lbCmpdUnits.TabIndex = 0;
		this.lbCmpdUnits.Text = "组分单位";
		this.lbDisplayMode.AutoSize = true;
		this.lbDisplayMode.Location = new System.Drawing.Point(13, 45);
		this.lbDisplayMode.Name = "lbDisplayMode";
		this.lbDisplayMode.Size = new System.Drawing.Size(53, 12);
		this.lbDisplayMode.TabIndex = 0;
		this.lbDisplayMode.Text = "显示模式";
		this.lbDisplayMode.Visible = false;
		this.lbDescription.AutoSize = true;
		this.lbDescription.Location = new System.Drawing.Point(13, 18);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(29, 12);
		this.lbDescription.TabIndex = 0;
		this.lbDescription.Text = "描述";
		this.tpDefaults.Controls.Add(this.chbMiniTime);
		this.tpDefaults.Controls.Add(this.chbTimeSlot);
		this.tpDefaults.Controls.Add(this.cbCurveFit);
		this.tpDefaults.Controls.Add(this.cbOriginal);
		this.tpDefaults.Controls.Add(this.cbRespStyle);
		this.tpDefaults.Controls.Add(this.btnSetAll);
		this.tpDefaults.Controls.Add(this.lbRespStyle);
		this.tpDefaults.Controls.Add(this.lbOriginal);
		this.tpDefaults.Controls.Add(this.tbRightWindow);
		this.tpDefaults.Controls.Add(this.tbLeftWindow);
		this.tpDefaults.Controls.Add(this.lbRightWindow);
		this.tpDefaults.Controls.Add(this.lbCurveFit);
		this.tpDefaults.Controls.Add(this.lbLeftWindow);
		this.tpDefaults.Location = new System.Drawing.Point(4, 23);
		this.tpDefaults.Name = "tpDefaults";
		this.tpDefaults.Size = new System.Drawing.Size(317, 194);
		this.tpDefaults.TabIndex = 1;
		this.tpDefaults.Text = "默认";
		this.tpDefaults.UseVisualStyleBackColor = true;
		this.chbMiniTime.AutoSize = true;
		this.chbMiniTime.Location = new System.Drawing.Point(183, 119);
		this.chbMiniTime.Name = "chbMiniTime";
		this.chbMiniTime.Size = new System.Drawing.Size(96, 16);
		this.chbMiniTime.TabIndex = 13;
		this.chbMiniTime.Text = "最小时间匹配";
		this.chbMiniTime.UseVisualStyleBackColor = true;
		this.chbTimeSlot.AutoSize = true;
		this.chbTimeSlot.Location = new System.Drawing.Point(183, 94);
		this.chbTimeSlot.Name = "chbTimeSlot";
		this.chbTimeSlot.Size = new System.Drawing.Size(108, 16);
		this.chbTimeSlot.TabIndex = 12;
		this.chbTimeSlot.Text = "时间带(百分比)";
		this.chbTimeSlot.UseVisualStyleBackColor = true;
		this.cbCurveFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbCurveFit.FormattingEnabled = true;
		this.cbCurveFit.ItemExtString = "";
		this.cbCurveFit.Location = new System.Drawing.Point(95, 65);
		this.cbCurveFit.Name = "cbCurveFit";
		this.cbCurveFit.Size = new System.Drawing.Size(121, 20);
		this.cbCurveFit.TabIndex = 9;
		this.cbOriginal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbOriginal.FormattingEnabled = true;
		this.cbOriginal.ItemExtString = "";
		this.cbOriginal.Location = new System.Drawing.Point(95, 39);
		this.cbOriginal.Name = "cbOriginal";
		this.cbOriginal.Size = new System.Drawing.Size(121, 20);
		this.cbOriginal.TabIndex = 9;
		this.cbRespStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRespStyle.FormattingEnabled = true;
		this.cbRespStyle.ItemExtString = "";
		this.cbRespStyle.Location = new System.Drawing.Point(95, 13);
		this.cbRespStyle.Name = "cbRespStyle";
		this.cbRespStyle.Size = new System.Drawing.Size(121, 20);
		this.cbRespStyle.TabIndex = 9;
		this.btnSetAll.Location = new System.Drawing.Point(37, 154);
		this.btnSetAll.Name = "btnSetAll";
		this.btnSetAll.Size = new System.Drawing.Size(243, 23);
		this.btnSetAll.TabIndex = 8;
		this.btnSetAll.Text = "置所有组分";
		this.btnSetAll.UseVisualStyleBackColor = true;
		this.btnSetAll.Click += new System.EventHandler(btnSetAll_Click);
		this.lbRespStyle.AutoSize = true;
		this.lbRespStyle.Location = new System.Drawing.Point(10, 18);
		this.lbRespStyle.Name = "lbRespStyle";
		this.lbRespStyle.Size = new System.Drawing.Size(53, 12);
		this.lbRespStyle.TabIndex = 4;
		this.lbRespStyle.Text = "响应类型";
		this.lbOriginal.AutoSize = true;
		this.lbOriginal.Location = new System.Drawing.Point(10, 44);
		this.lbOriginal.Name = "lbOriginal";
		this.lbOriginal.Size = new System.Drawing.Size(53, 12);
		this.lbOriginal.TabIndex = 4;
		this.lbOriginal.Text = "原点方案";
		this.tbRightWindow.Location = new System.Drawing.Point(95, 118);
		this.tbRightWindow.Name = "tbRightWindow";
		this.tbRightWindow.Size = new System.Drawing.Size(63, 21);
		this.tbRightWindow.TabIndex = 6;
		this.tbRightWindow.Text = "0.100";
		this.tbLeftWindow.Location = new System.Drawing.Point(95, 91);
		this.tbLeftWindow.Name = "tbLeftWindow";
		this.tbLeftWindow.Size = new System.Drawing.Size(63, 21);
		this.tbLeftWindow.TabIndex = 6;
		this.tbLeftWindow.Text = "0.100";
		this.lbRightWindow.AutoSize = true;
		this.lbRightWindow.Location = new System.Drawing.Point(10, 122);
		this.lbRightWindow.Name = "lbRightWindow";
		this.lbRightWindow.Size = new System.Drawing.Size(41, 12);
		this.lbRightWindow.TabIndex = 5;
		this.lbRightWindow.Text = "右窗宽";
		this.lbCurveFit.AutoSize = true;
		this.lbCurveFit.Location = new System.Drawing.Point(10, 70);
		this.lbCurveFit.Name = "lbCurveFit";
		this.lbCurveFit.Size = new System.Drawing.Size(53, 12);
		this.lbCurveFit.TabIndex = 4;
		this.lbCurveFit.Text = "曲线类型";
		this.lbLeftWindow.AutoSize = true;
		this.lbLeftWindow.Location = new System.Drawing.Point(10, 95);
		this.lbLeftWindow.Name = "lbLeftWindow";
		this.lbLeftWindow.Size = new System.Drawing.Size(41, 12);
		this.lbLeftWindow.TabIndex = 5;
		this.lbLeftWindow.Text = "左窗宽";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(332, 269);
		base.Controls.Add(this.tcCali);
		base.Name = "CaliGnlOptDlg";
		this.Text = "校正选项";
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.tcCali, 0);
		this.tcCali.ResumeLayout(false);
		this.tpOptions.ResumeLayout(false);
		this.tpOptions.PerformLayout();
		this.gbRecalibration.ResumeLayout(false);
		this.gbRecalibration.PerformLayout();
		this.tpDefaults.ResumeLayout(false);
		this.tpDefaults.PerformLayout();
		base.ResumeLayout(false);
	}
}
