using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CusDlg : LclDialog
{
	public const string _Amount = "Amt";

	public const string _Area = "Ara";

	public const string _EndT = "EdT";

	public const string _EndV = "EdV";

	public const string _Height = "Hht";

	public const string _RetenT = "RtT";

	public const string _StartT = "StT";

	public const string _StartV = "StV";

	public const string _Width = "Wth";

	public const string _WO5 = "Whf";

	public const string sAmount = "数量";

	public const string sArea = "面积";

	public const string sEndT = "结束min";

	public const string sHeight = "高度";

	public const string sRetenT = "保留min";

	public const string sStartT = "起始min";

	public const string sWidth = "峰宽";

	public const string sWO5 = "半峰宽";

	private LclButton btn1;

	private LclButton btn10;

	private LclButton btn2;

	private LclButton btn3;

	private LclButton btn4;

	private LclButton btn5;

	private LclButton btn6;

	private LclButton btn7;

	private LclButton btn8;

	private LclButton btn9;

	private IContainer icontainer_1;

	private LclButton lclButton1;

	private LclButton lclButton2;

	private LclButton lclButton3;

	private LclButton lclButton4;

	private LclButton lclButton5;

	private LclButton lclButton6;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	public string sEndV = "结束" + Class49.MesureUnit();

	public string sStartV = "起始" + Class49.MesureUnit();

	private LclTextBox tbFormula;

	private LclTextBox tbName;

	public CusDlg()
	{
		InitializeComponent();
		btn1.Text = "起始min";
		btn2.Text = "结束min";
		btn3.Text = sStartV;
		btn4.Text = sEndV;
		btn5.Text = "保留min";
		btn6.Text = "面积";
		btn7.Text = "高度";
		btn8.Text = "峰宽";
		btn9.Text = "半峰宽";
		btn10.Text = "数量";
	}

	private void btn7_Click(object sender, EventArgs e)
	{
		string string_ = (sender as Button).Text;
		tbFormula.SelectedText = method_0(string_);
		tbFormula.Focus();
		tbFormula.Select(tbFormula.Text.Length, 0);
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
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.tbName = new IBrainChrom2018.LclTextBox();
		this.tbFormula = new IBrainChrom2018.LclTextBox();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.lclButton1 = new IBrainChrom2018.LclButton();
		this.lclButton2 = new IBrainChrom2018.LclButton();
		this.lclButton3 = new IBrainChrom2018.LclButton();
		this.lclButton4 = new IBrainChrom2018.LclButton();
		this.lclButton5 = new IBrainChrom2018.LclButton();
		this.lclButton6 = new IBrainChrom2018.LclButton();
		this.btn6 = new IBrainChrom2018.LclButton();
		this.btn5 = new IBrainChrom2018.LclButton();
		this.btn4 = new IBrainChrom2018.LclButton();
		this.btn3 = new IBrainChrom2018.LclButton();
		this.btn2 = new IBrainChrom2018.LclButton();
		this.btn1 = new IBrainChrom2018.LclButton();
		this.btn10 = new IBrainChrom2018.LclButton();
		this.btn9 = new IBrainChrom2018.LclButton();
		this.btn8 = new IBrainChrom2018.LclButton();
		this.btn7 = new IBrainChrom2018.LclButton();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(34, 178);
		base.btnOK.Text = "确认";
		base.btnCancel.Location = new System.Drawing.Point(136, 178);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(241, 178);
		base.btnHelp.Text = "帮助";
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(12, 19);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(29, 12);
		this.lclLabel1.TabIndex = 1;
		this.lclLabel1.Text = "列名";
		this.tbName.Location = new System.Drawing.Point(59, 12);
		this.tbName.Name = "tbName";
		this.tbName.Size = new System.Drawing.Size(276, 21);
		this.tbName.TabIndex = 2;
		this.tbFormula.Location = new System.Drawing.Point(59, 36);
		this.tbFormula.Name = "tbFormula";
		this.tbFormula.Size = new System.Drawing.Size(276, 21);
		this.tbFormula.TabIndex = 2;
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(12, 39);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(41, 12);
		this.lclLabel2.TabIndex = 1;
		this.lclLabel2.Text = "表达式";
		this.lclButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lclButton1.Location = new System.Drawing.Point(59, 63);
		this.lclButton1.Name = "lclButton1";
		this.lclButton1.Size = new System.Drawing.Size(32, 23);
		this.lclButton1.TabIndex = 3;
		this.lclButton1.Text = "+";
		this.lclButton1.UseVisualStyleBackColor = true;
		this.lclButton1.Click += new System.EventHandler(btn7_Click);
		this.lclButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lclButton2.Location = new System.Drawing.Point(97, 63);
		this.lclButton2.Name = "lclButton2";
		this.lclButton2.Size = new System.Drawing.Size(32, 23);
		this.lclButton2.TabIndex = 3;
		this.lclButton2.Text = "-";
		this.lclButton2.UseVisualStyleBackColor = true;
		this.lclButton2.Click += new System.EventHandler(btn7_Click);
		this.lclButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lclButton3.Location = new System.Drawing.Point(135, 63);
		this.lclButton3.Name = "lclButton3";
		this.lclButton3.Size = new System.Drawing.Size(32, 23);
		this.lclButton3.TabIndex = 3;
		this.lclButton3.Text = "*";
		this.lclButton3.UseVisualStyleBackColor = true;
		this.lclButton3.Click += new System.EventHandler(btn7_Click);
		this.lclButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lclButton4.Location = new System.Drawing.Point(173, 63);
		this.lclButton4.Name = "lclButton4";
		this.lclButton4.Size = new System.Drawing.Size(32, 23);
		this.lclButton4.TabIndex = 3;
		this.lclButton4.Text = "/";
		this.lclButton4.UseVisualStyleBackColor = true;
		this.lclButton4.Click += new System.EventHandler(btn7_Click);
		this.lclButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lclButton5.Location = new System.Drawing.Point(211, 63);
		this.lclButton5.Name = "lclButton5";
		this.lclButton5.Size = new System.Drawing.Size(32, 23);
		this.lclButton5.TabIndex = 3;
		this.lclButton5.Text = "(";
		this.lclButton5.UseVisualStyleBackColor = true;
		this.lclButton5.Click += new System.EventHandler(btn7_Click);
		this.lclButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lclButton6.Location = new System.Drawing.Point(249, 63);
		this.lclButton6.Name = "lclButton6";
		this.lclButton6.Size = new System.Drawing.Size(32, 23);
		this.lclButton6.TabIndex = 3;
		this.lclButton6.Text = ")";
		this.lclButton6.UseVisualStyleBackColor = true;
		this.lclButton6.Click += new System.EventHandler(btn7_Click);
		this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn6.Location = new System.Drawing.Point(332, 92);
		this.btn6.Name = "btn6";
		this.btn6.Size = new System.Drawing.Size(58, 23);
		this.btn6.TabIndex = 3;
		this.btn6.Text = "lclButton1";
		this.btn6.UseVisualStyleBackColor = true;
		this.btn6.Click += new System.EventHandler(btn7_Click);
		this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn5.Location = new System.Drawing.Point(268, 92);
		this.btn5.Name = "btn5";
		this.btn5.Size = new System.Drawing.Size(58, 23);
		this.btn5.TabIndex = 3;
		this.btn5.Text = "lclButton1";
		this.btn5.UseVisualStyleBackColor = true;
		this.btn5.Click += new System.EventHandler(btn7_Click);
		this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn4.Location = new System.Drawing.Point(204, 92);
		this.btn4.Name = "btn4";
		this.btn4.Size = new System.Drawing.Size(58, 23);
		this.btn4.TabIndex = 3;
		this.btn4.Text = "lclButton1";
		this.btn4.UseVisualStyleBackColor = true;
		this.btn4.Click += new System.EventHandler(btn7_Click);
		this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn3.Location = new System.Drawing.Point(140, 92);
		this.btn3.Name = "btn3";
		this.btn3.Size = new System.Drawing.Size(58, 23);
		this.btn3.TabIndex = 3;
		this.btn3.Text = "lclButton1";
		this.btn3.UseVisualStyleBackColor = true;
		this.btn3.Click += new System.EventHandler(btn7_Click);
		this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn2.Location = new System.Drawing.Point(76, 92);
		this.btn2.Name = "btn2";
		this.btn2.Size = new System.Drawing.Size(58, 23);
		this.btn2.TabIndex = 3;
		this.btn2.Text = "结束min";
		this.btn2.UseVisualStyleBackColor = true;
		this.btn2.Click += new System.EventHandler(btn7_Click);
		this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn1.Location = new System.Drawing.Point(12, 92);
		this.btn1.Name = "btn1";
		this.btn1.Size = new System.Drawing.Size(58, 23);
		this.btn1.TabIndex = 3;
		this.btn1.Text = "起始min";
		this.btn1.UseVisualStyleBackColor = true;
		this.btn1.Click += new System.EventHandler(btn7_Click);
		this.btn10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn10.Location = new System.Drawing.Point(204, 121);
		this.btn10.Name = "btn10";
		this.btn10.Size = new System.Drawing.Size(58, 23);
		this.btn10.TabIndex = 3;
		this.btn10.Text = "lclButton1";
		this.btn10.UseVisualStyleBackColor = true;
		this.btn10.Click += new System.EventHandler(btn7_Click);
		this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn9.Location = new System.Drawing.Point(140, 121);
		this.btn9.Name = "btn9";
		this.btn9.Size = new System.Drawing.Size(58, 23);
		this.btn9.TabIndex = 3;
		this.btn9.Text = "lclButton1";
		this.btn9.UseVisualStyleBackColor = true;
		this.btn9.Click += new System.EventHandler(btn7_Click);
		this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn8.Location = new System.Drawing.Point(76, 121);
		this.btn8.Name = "btn8";
		this.btn8.Size = new System.Drawing.Size(58, 23);
		this.btn8.TabIndex = 3;
		this.btn8.Text = "lclButton1";
		this.btn8.UseVisualStyleBackColor = true;
		this.btn8.Click += new System.EventHandler(btn7_Click);
		this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn7.Location = new System.Drawing.Point(12, 121);
		this.btn7.Name = "btn7";
		this.btn7.Size = new System.Drawing.Size(58, 23);
		this.btn7.TabIndex = 3;
		this.btn7.Text = "lclButton1";
		this.btn7.UseVisualStyleBackColor = true;
		this.btn7.Click += new System.EventHandler(btn7_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(401, 213);
		base.Controls.Add(this.lclLabel1);
		base.Controls.Add(this.tbName);
		base.Controls.Add(this.tbFormula);
		base.Controls.Add(this.lclLabel2);
		base.Controls.Add(this.lclButton1);
		base.Controls.Add(this.btn1);
		base.Controls.Add(this.btn7);
		base.Controls.Add(this.lclButton2);
		base.Controls.Add(this.btn3);
		base.Controls.Add(this.lclButton3);
		base.Controls.Add(this.btn2);
		base.Controls.Add(this.btn8);
		base.Controls.Add(this.lclButton4);
		base.Controls.Add(this.lclButton5);
		base.Controls.Add(this.btn9);
		base.Controls.Add(this.lclButton6);
		base.Controls.Add(this.btn4);
		base.Controls.Add(this.btn5);
		base.Controls.Add(this.btn6);
		base.Controls.Add(this.btn10);
		base.Name = "CusDlg";
		this.Text = "自定义结果列";
		base.Controls.SetChildIndex(this.btn10, 0);
		base.Controls.SetChildIndex(this.btn6, 0);
		base.Controls.SetChildIndex(this.btn5, 0);
		base.Controls.SetChildIndex(this.btn4, 0);
		base.Controls.SetChildIndex(this.lclButton6, 0);
		base.Controls.SetChildIndex(this.btn9, 0);
		base.Controls.SetChildIndex(this.lclButton5, 0);
		base.Controls.SetChildIndex(this.lclButton4, 0);
		base.Controls.SetChildIndex(this.btn8, 0);
		base.Controls.SetChildIndex(this.btn2, 0);
		base.Controls.SetChildIndex(this.lclButton3, 0);
		base.Controls.SetChildIndex(this.btn3, 0);
		base.Controls.SetChildIndex(this.lclButton2, 0);
		base.Controls.SetChildIndex(this.btn7, 0);
		base.Controls.SetChildIndex(this.btn1, 0);
		base.Controls.SetChildIndex(this.lclButton1, 0);
		base.Controls.SetChildIndex(this.lclLabel2, 0);
		base.Controls.SetChildIndex(this.tbFormula, 0);
		base.Controls.SetChildIndex(this.tbName, 0);
		base.Controls.SetChildIndex(this.lclLabel1, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private string method_0(string string_0)
	{
		switch (string_0)
		{
		case "起始min":
			return "StT";
		case "结束min":
			return "EdT";
		case "保留min":
			return "RtT";
		case "面积":
			return "Ara";
		case "高度":
			return "Hht";
		case "峰宽":
			return "Wth";
		case "半峰宽":
			return "Whf";
		case "数量":
			return "Amt";
		default:
			if (string_0 == sStartV)
			{
				return "StV";
			}
			if (string_0 == sEndV)
			{
				return "EdV";
			}
			return string_0;
		}
	}

	public DialogResult ShowDialog(ref string name, ref string formula)
	{
		tbName.Text = name;
		tbFormula.Text = formula;
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			name = tbName.Text;
			formula = tbFormula.Text;
		}
		return dialogResult;
	}
}
