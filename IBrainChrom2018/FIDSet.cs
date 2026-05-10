using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FIDSet : Form
{
	public CheckBox BsDeduct;

	public Button button1;

	public Button button2;

	public Button button3;

	private Button button4;

	private Button button5;

	private ChromFormInterface formMain_0;

	public TextBox Freq;

	public GroupBox groupBox1;

	private IContainer icontainer_0;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	public TextBox Mark;

	public CheckBox Positive;

	public ComboBox range;

	private Button button6;

	public TextBox textBox3;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private Button btnHighValueSet;

	public TextBox tbHighV;

	private Button btnHighValueCheck;

	private DetectorParam detectParam = DetectorParam.Create();

	public static FIDSet myself = null;

	public FIDSet()
	{
		InitializeComponent();
		if (myself == null)
		{
			myself = this;
		}
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
		formMain_0 = cdlMgr.formMain;
		button5_Click(null, null);
		Thread.Sleep(100);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		formMain_0.FID1Fire();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (Mark.Text.Trim().Substring(0, 3) == "ECD")
		{
			range.Text = (double.Parse(range.Text.Trim()) * 100.0).ToString();
		}
		formMain_0.DtrSet();
		Close();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button4_Click(object sender, EventArgs e)
	{
		int num = Class49.Object2Int(textBox3.Text.Trim(), 0);
		if (num >= 0 && num <= 9)
		{
			formMain_0.SetFireLengthValue = num;
			formMain_0.DtrSetFireLength();
			return;
		}
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			MessageBox.Show(Lang.PS("点火时长输入范围不正确。", "IgnitionTime input Error。"), "点火时长设置", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			break;
		case SysLanguage.EN:
			MessageBox.Show("IgnitionTime input Error。", "IgnitionTime Set", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			break;
		}
	}

	public void button5_Click(object sender, EventArgs e)
	{
		formMain_0.DtrSelectFireLength();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void FIDSet_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void FIDSet_Load(object sender, EventArgs e)
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = Lang.PS("检测器参数", " Detector parameter");
			break;
		case SysLanguage.EN:
			Text = Lang.PS("检测器参数", " Detector parameter");
			groupBox1.Text = "detector parameters";
			button1.Text = "Fire";
			button2.Text = "OK";
			button3.Text = "Cancle";
			button4.Text = "set";
			button5.Text = "query";
			Positive.Text = "electrode";
			label1.Text = "detector:";
			label5.Text = "range:";
			label3.Text = "power";
			label8.Text = "s";
			label6.Text = "IgnitionTime";
			BsDeduct.Text = "The instrument baseline deduction";
			label2.Text = "Sampling:";
			label7.Text = "Times/S";
			break;
		}
	}

	private void FIDSet_Validating(object sender, CancelEventArgs e)
	{
	}

	private void FIDSet_VisibleChanged(object sender, EventArgs e)
	{
		if (Mark.Text.Trim().Substring(0, 3) == "ECD")
		{
			range.Text = (double.Parse(range.Text.Trim()) / 100.0).ToString();
		}
	}

	private void InitializeComponent()
	{
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnHighValueCheck = new System.Windows.Forms.Button();
		this.btnHighValueSet = new System.Windows.Forms.Button();
		this.tbHighV = new System.Windows.Forms.TextBox();
		this.button6 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.range = new System.Windows.Forms.ComboBox();
		this.BsDeduct = new System.Windows.Forms.CheckBox();
		this.Positive = new System.Windows.Forms.CheckBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.Freq = new System.Windows.Forms.TextBox();
		this.Mark = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox1.Controls.Add(this.btnHighValueCheck);
		this.groupBox1.Controls.Add(this.btnHighValueSet);
		this.groupBox1.Controls.Add(this.tbHighV);
		this.groupBox1.Controls.Add(this.button6);
		this.groupBox1.Controls.Add(this.button5);
		this.groupBox1.Controls.Add(this.button4);
		this.groupBox1.Controls.Add(this.range);
		this.groupBox1.Controls.Add(this.BsDeduct);
		this.groupBox1.Controls.Add(this.Positive);
		this.groupBox1.Controls.Add(this.textBox3);
		this.groupBox1.Controls.Add(this.Freq);
		this.groupBox1.Controls.Add(this.Mark);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.ForeColor = System.Drawing.Color.Blue;
		this.groupBox1.Location = new System.Drawing.Point(4, 12);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.groupBox1.Size = new System.Drawing.Size(592, 159);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = IBrainChrom2018.Lang.PS("检测器参数", " Detector parameter");
		this.btnHighValueCheck.Location = new System.Drawing.Point(349, 54);
		this.btnHighValueCheck.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
		this.btnHighValueCheck.Name = "btnHighValueCheck";
		this.btnHighValueCheck.Size = new System.Drawing.Size(108, 36);
		this.btnHighValueCheck.TabIndex = 86;
		this.btnHighValueCheck.Text = IBrainChrom2018.Lang.PS("高压查询", "High pressure query");
		this.btnHighValueCheck.UseVisualStyleBackColor = true;
		this.btnHighValueCheck.Visible = false;
		this.btnHighValueCheck.Click += new System.EventHandler(BtnHighValueCheck_Click);
		this.btnHighValueSet.Location = new System.Drawing.Point(467, 54);
		this.btnHighValueSet.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
		this.btnHighValueSet.Name = "btnHighValueSet";
		this.btnHighValueSet.Size = new System.Drawing.Size(111, 36);
		this.btnHighValueSet.TabIndex = 85;
		this.btnHighValueSet.Text = IBrainChrom2018.Lang.PS("高压设定", "Voltage set");
		this.btnHighValueSet.UseVisualStyleBackColor = true;
		this.btnHighValueSet.Visible = false;
		this.btnHighValueSet.Click += new System.EventHandler(BtnHighValueSet_Click);
		this.tbHighV.Location = new System.Drawing.Point(222, 62);
		this.tbHighV.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
		this.tbHighV.Name = "tbHighV";
		this.tbHighV.Size = new System.Drawing.Size(117, 25);
		this.tbHighV.TabIndex = 84;
		this.tbHighV.Visible = false;
		this.button6.Location = new System.Drawing.Point(419, 120);
		this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(91, 25);
		this.button6.TabIndex = 6;
		this.button6.Text = IBrainChrom2018.Lang.PS("调零", "zero set");
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.button5.Location = new System.Drawing.Point(211, 118);
		this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(100, 29);
		this.button5.TabIndex = 5;
		this.button5.Text = IBrainChrom2018.Lang.PS("查询", "query");
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.button4.Location = new System.Drawing.Point(317, 118);
		this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(100, 29);
		this.button4.TabIndex = 4;
		this.button4.Text = IBrainChrom2018.Lang.PS("设置", "set");
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.range.FormattingEnabled = true;
		this.range.Items.AddRange(new object[6] { "0.05", "0.1", "0.2", "0.5", "1.0", "2.0" });
		this.range.Location = new System.Drawing.Point(97, 91);
		this.range.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.range.Name = "range";
		this.range.Size = new System.Drawing.Size(65, 23);
		this.range.TabIndex = 3;
		this.BsDeduct.AutoSize = true;
		this.BsDeduct.Location = new System.Drawing.Point(233, 20);
		this.BsDeduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.BsDeduct.Name = "BsDeduct";
		this.BsDeduct.Size = new System.Drawing.Size(119, 19);
		this.BsDeduct.TabIndex = 2;
		this.BsDeduct.Text = IBrainChrom2018.Lang.PS("仪器基线扣除", "Instrument baseline deduction");
		this.BsDeduct.UseVisualStyleBackColor = true;
		this.Positive.AutoSize = true;
		this.Positive.Location = new System.Drawing.Point(175, 21);
		this.Positive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.Positive.Name = "Positive";
		this.Positive.Size = new System.Drawing.Size(59, 19);
		this.Positive.TabIndex = 2;
		this.Positive.Text = IBrainChrom2018.Lang.PS("极性", "Positive");
		this.Positive.UseVisualStyleBackColor = true;
		this.textBox3.Location = new System.Drawing.Point(97, 120);
		this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(63, 25);
		this.textBox3.TabIndex = 1;
		this.Freq.Location = new System.Drawing.Point(97, 59);
		this.Freq.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.Freq.Name = "Freq";
		this.Freq.Size = new System.Drawing.Size(63, 25);
		this.Freq.TabIndex = 1;
		this.Mark.Location = new System.Drawing.Point(97, 18);
		this.Mark.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.Mark.Name = "Mark";
		this.Mark.ReadOnly = true;
		this.Mark.Size = new System.Drawing.Size(63, 25);
		this.Mark.TabIndex = 1;
		this.Mark.TextChanged += new System.EventHandler(Mark_TextChanged);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(15, 125);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(75, 15);
		this.label6.TabIndex = 0;
		this.label6.Text = IBrainChrom2018.Lang.PS("点火时长", "The ignition time");
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(15, 95);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(77, 15);
		this.label5.TabIndex = 0;
		this.label5.Text = IBrainChrom2018.Lang.PS("量    程:", "Amount of routine:");
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 62);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(85, 15);
		this.label2.TabIndex = 0;
		this.label2.Text = IBrainChrom2018.Lang.PS("采     样:", "sampling:");
		this.label4.AutoSize = true;
		this.label4.ForeColor = System.Drawing.Color.Red;
		this.label4.Location = new System.Drawing.Point(219, 95);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(160, 15);
		this.label4.TabIndex = 0;
		this.label4.Text = IBrainChrom2018.Lang.PS("*输入范围7、8、9、10", "*input range7、8、9、10");
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Black;
		this.label8.Location = new System.Drawing.Point(172, 125);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(22, 15);
		this.label8.TabIndex = 0;
		this.label8.Text = IBrainChrom2018.Lang.PS("秒", "s");
		this.label7.AutoSize = true;
		this.label7.ForeColor = System.Drawing.Color.Black;
		this.label7.Location = new System.Drawing.Point(172, 62);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 15);
		this.label7.TabIndex = 0;
		this.label7.Text = IBrainChrom2018.Lang.PS("次/秒", "Times/SEC");
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Location = new System.Drawing.Point(172, 95);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(37, 15);
		this.label3.TabIndex = 0;
		this.label3.Text = IBrainChrom2018.Lang.PS("次方", "power");
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 21);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(76, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = IBrainChrom2018.Lang.PS("检 测 器:", "Detection sensor");
		this.button1.Location = new System.Drawing.Point(33, 189);
		this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(117, 36);
		this.button1.TabIndex = 1;
		this.button1.Text = IBrainChrom2018.Lang.PS("点火", " ignition");
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(223, 189);
		this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(117, 36);
		this.button2.TabIndex = 1;
		this.button2.Text = IBrainChrom2018.Lang.PS("确定", "confirm");
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.Location = new System.Drawing.Point(420, 189);
		this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(117, 36);
		this.button3.TabIndex = 1;
		this.button3.Text = IBrainChrom2018.Lang.PS("取消", " cancel");
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(601, 234);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.groupBox1);
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FIDSet";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = IBrainChrom2018.Lang.PS("检测器参数设置", "Detector parameter setting");
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FIDSet_FormClosing);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FIDSet_FormClosed);
		base.Load += new System.EventHandler(FIDSet_Load);
		base.VisibleChanged += new System.EventHandler(FIDSet_VisibleChanged);
		base.Validating += new System.ComponentModel.CancelEventHandler(FIDSet_Validating);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void Mark_TextChanged(object sender, EventArgs e)
	{
	}

	private void method_0(object sender, EventArgs e)
	{
		bool flag = Mark.Text.Trim().Substring(0, 3) == "ECD";
	}

	private void button6_Click(object sender, EventArgs e)
	{
		formMain_0.SetZero();
	}

	public void Reload(int FIDIndex, string DtrMark2)
	{
		DtrMark2 = DtrMark2.Trim();
		InsDeviceManager currentInsDeviceMgr = cdlMgr.CurrentInsDeviceMgr;
		DetectorParamItem detectorParamItem = detectParam.GetDetectorParamItem(DtrMark2);
		if (detectorParamItem == null)
		{
			MessageBox.Show(this, Lang.PS("该检测器在配置文件中不存在，请设置后再使用!检测器名称:", "The detector does not exist in the configuration file, please set it up and use it later!Detector name:") + DtrMark2 + ":");
			return;
		}
		string text = DtrMark2.Substring(0, 3);
		Mark.Text = DtrMark2;
		Positive.Text = Lang.PS("极性", "electrode");
		label5.Text = detectorParamItem.strParamName;
		label3.Text = detectorParamItem.strParamUnit;
		label4.Text = detectorParamItem.strParamRemark;
		label6.Text = detectorParamItem.strAddtionParamName;
		label8.Text = detectorParamItem.strAddtionParamUnit;
		range.Text = detectorParamItem.fParamValue.ToString();
		if (DtrMark2 == "FID1")
		{
			label6.Text = Lang.PS("点火时长", "IgnitionTime");
			label8.Text = Lang.PS("秒", "s");
		}
		if (DtrMark2 == "FID2")
		{
			label6.Text = Lang.PS("点火时长", "IgnitionTime");
			label8.Text = Lang.PS("秒", "s");
		}
		if (DtrMark2 == "PDD1" || DtrMark2 == "PDD2")
		{
			Positive.Text = Lang.PS("高压", "Voltage");
			Positive.Visible = true;
			textBox3.Visible = true;
			label6.Visible = true;
			label8.Visible = true;
			button4.Visible = true;
			button5.Visible = true;
			button1.Visible = true;
			label5.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			label6.Visible = false;
			label8.Visible = false;
			range.Visible = false;
			label7.Visible = false;
			label2.Visible = false;
			Freq.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button6.Visible = false;
			BsDeduct.Visible = false;
			button1.Visible = false;
			textBox3.Visible = false;
		}
		else
		{
			textBox3.Visible = true;
			label6.Visible = true;
			label8.Visible = true;
			button4.Visible = true;
			button5.Visible = true;
			button1.Visible = true;
		}
		if (text == "FID")
		{
			range.Items.Clear();
			range.Items.Add("7");
			range.Items.Add("8");
			range.Items.Add("9");
			range.Items.Add("10");
			tbHighV.Visible = false;
			btnHighValueCheck.Visible = false;
			btnHighValueSet.Visible = false;
			label2.Visible = false;
			Freq.Visible = false;
			label7.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			range.Visible = false;
			BsDeduct.Visible = false;
			Positive.Visible = false;
			button6.Visible = false;
			Positive.Text = Lang.PS("极性", "electrode");
		}
		if (text == "ECD")
		{
			range.Items.Clear();
			range.Items.Add("0.05");
			range.Items.Add("0.1");
			range.Items.Add("0.2");
			range.Items.Add("0.5");
			range.Items.Add("1.0");
			range.Items.Add("2.0");
			tbHighV.Visible = false;
			btnHighValueCheck.Visible = false;
			btnHighValueSet.Visible = false;
			label2.Visible = false;
			Freq.Visible = false;
			label7.Visible = false;
			label3.Visible = true;
			label4.Visible = false;
			label5.Visible = true;
			range.Visible = true;
			BsDeduct.Visible = false;
			Positive.Text = Lang.PS("极性", "electrode");
			Positive.Visible = true;
			button6.Visible = true;
		}
		if (text == "NPD")
		{
			textBox3.Visible = false;
			label6.Visible = false;
			label8.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button1.Visible = false;
			label2.Visible = false;
			label5.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			label7.Visible = false;
			Freq.Visible = false;
			range.Visible = false;
			Positive.Visible = false;
			BsDeduct.Visible = false;
		}
		if (text == "TCD")
		{
			range.Items.Clear();
			tbHighV.Visible = false;
			btnHighValueCheck.Visible = false;
			btnHighValueSet.Visible = false;
			label2.Visible = false;
			Freq.Visible = false;
			label7.Visible = false;
			label3.Visible = true;
			label4.Visible = false;
			label5.Visible = true;
			range.Visible = true;
			BsDeduct.Visible = false;
			textBox3.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button1.Visible = false;
			Positive.Text = Lang.PS("极性", "electrode");
			label5.Text = Lang.PS("桥流:", "Bridge current");
			label3.Text = Lang.PS("毫安", "mA");
			Positive.Visible = true;
			button6.Visible = true;
		}
		if (text == "FPD")
		{
			range.Items.Clear();
			range.Items.Add("7");
			range.Items.Add("8");
			range.Items.Add("9");
			range.Items.Add("10");
			tbHighV.Visible = true;
			btnHighValueCheck.Visible = true;
			btnHighValueSet.Visible = true;
			label2.Visible = false;
			Freq.Visible = false;
			label7.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			range.Visible = false;
			BsDeduct.Visible = false;
			Positive.Visible = true;
			Positive.Text = Lang.PS("高压", "Voltage");
		}
		DetectorSettingRow detectorSettingRow = currentInsDeviceMgr.detectorSettingList[FIDIndex];
		Positive.Checked = !detectorSettingRow.GetPolarity();
		if (detectorSettingRow.GetDeviceTypeName().StartsWith("ECD"))
		{
			range.Text = ((float)(int)detectorSettingRow.range / 100f).ToString();
		}
		else
		{
			range.Text = detectorSettingRow.range.ToString();
		}
		BsDeduct.Checked = detectorSettingRow.GetBaselineDeduction();
		Freq.Text = (detectorSettingRow.GetFreq() * 10).ToString();
	}

	private void BtnHighValueCheck_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			if (Mark.Text.Trim().Contains("FPD1"))
			{
				cdlMgr.CurrentTcpServerSocket.indexFPDHIGHV = 1;
			}
			else if (Mark.Text.Trim().Contains("FPD2"))
			{
				cdlMgr.CurrentTcpServerSocket.indexFPDHIGHV = 2;
			}
			cdlMgr.currentTcpServerMgrSendCmd(253);
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void BtnHighValueSet_Click(object sender, EventArgs e)
	{
		if (int.Parse(tbHighV.Text.Trim()) > 800)
		{
			MessageBox.Show("高压值设定不能高于800V");
		}
		else if (Class49.user_0.ULevel == User.Level.管理员)
		{
			if (Mark.Text.Trim().Contains("FPD1"))
			{
				cdlMgr.CurrentTcpServerSocket.indexFPDHIGHV = 1;
			}
			else if (Mark.Text.Trim().Contains("FPD2"))
			{
				cdlMgr.CurrentTcpServerSocket.indexFPDHIGHV = 2;
			}
			cdlMgr.CurrentTcpServerSocket.fpdHighValue = tbHighV.Text.Trim();
			cdlMgr.currentTcpServerMgrSendCmd(252);
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void FIDSet_FormClosed(object sender, FormClosedEventArgs e)
	{
		myself = null;
	}
}
