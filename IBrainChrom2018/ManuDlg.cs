using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class ManuDlg : LclDialog
{
	public delegate void OKClick();

	public delegate void SuggestClick();

	private LclButton btnSuggest;

	private IContainer icontainer_1;

	private LclLabel lbEndTime;

	private LclLabel lbStartTime;

	private LclLabel lbUnitT1;

	private LclLabel lbUnitT2;

	private LclLabel lbUnitV;

	private LclLabel lbValue;

	private LclLabel lbValue2;

	private LclLabel lbValue3;

	private LclLabel lbValue4;

	private IntegRow integRow_0;

	private LclTextBox tbEndTime;

	private LclTextBox tbStartTime;

	private LclTextBox tbValue;

	private LclTextBox tbValue2;

	private LclTextBox tbValue3;

	private LclTextBox tbValue4;

	private OKClick okclick_0;

	private SuggestClick suggestClick_0;

	public event OKClick OnOKClick
	{
		add
		{
			OKClick oKClick = okclick_0;
			OKClick oKClick2;
			do
			{
				oKClick2 = oKClick;
				OKClick value2 = (OKClick)Delegate.Combine(oKClick2, value);
				oKClick = Interlocked.CompareExchange(ref okclick_0, value2, oKClick2);
			}
			while (oKClick != oKClick2);
		}
		remove
		{
			OKClick oKClick = okclick_0;
			OKClick oKClick2;
			do
			{
				oKClick2 = oKClick;
				OKClick value2 = (OKClick)Delegate.Remove(oKClick2, value);
				oKClick = Interlocked.CompareExchange(ref okclick_0, value2, oKClick2);
			}
			while (oKClick != oKClick2);
		}
	}

	public event SuggestClick OnSuggestClick
	{
		add
		{
			SuggestClick suggestClick = suggestClick_0;
			SuggestClick suggestClick2;
			do
			{
				suggestClick2 = suggestClick;
				SuggestClick value2 = (SuggestClick)Delegate.Combine(suggestClick2, value);
				suggestClick = Interlocked.CompareExchange(ref suggestClick_0, value2, suggestClick2);
			}
			while (suggestClick != suggestClick2);
		}
		remove
		{
			SuggestClick suggestClick = suggestClick_0;
			SuggestClick suggestClick2;
			do
			{
				suggestClick2 = suggestClick;
				SuggestClick value2 = (SuggestClick)Delegate.Remove(suggestClick2, value);
				suggestClick = Interlocked.CompareExchange(ref suggestClick_0, value2, suggestClick2);
			}
			while (suggestClick != suggestClick2);
		}
	}

	public ManuDlg()
	{
		InitializeComponent();
		lbValue3.Text = Lang.PS("左肩切系数", "Lf Tgnt.F");
		lbValue4.Text = Lang.PS("右肩切系数", "Rt Tgnt.F");
	}

	private void method_0(object sender, EventArgs e)
	{
		base.Visible = false;
	}

	private void method_1(object sender, EventArgs e)
	{
		base.Visible = false;
		if (okclick_0 != null)
		{
			okclick_0();
		}
	}

	private void btnSuggest_Click(object sender, EventArgs e)
	{
		base.Visible = false;
		if (suggestClick_0 != null)
		{
			suggestClick_0();
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

	public IntegRow GetNewIntegRow()
	{
		integRow_0.timeA = Class49.String2Float(tbStartTime.Text, integRow_0.timeA);
		integRow_0.timeB = Class49.String2Float(tbEndTime.Text, integRow_0.timeB);
		integRow_0.value = Class49.String2Float(tbValue.Text, integRow_0.value);
		integRow_0.value2 = Class49.String2Float(tbValue2.Text, integRow_0.value2);
		integRow_0.value3 = Class49.String2Float(tbValue3.Text, integRow_0.value3);
		integRow_0.value4 = Class49.String2Float(tbValue4.Text, integRow_0.value4);
		return integRow_0;
	}

	private void InitializeComponent()
	{
		this.lbStartTime = new IBrainChrom2018.LclLabel();
		this.lbEndTime = new IBrainChrom2018.LclLabel();
		this.lbValue = new IBrainChrom2018.LclLabel();
		this.lbUnitV = new IBrainChrom2018.LclLabel();
		this.lbUnitT2 = new IBrainChrom2018.LclLabel();
		this.lbUnitT1 = new IBrainChrom2018.LclLabel();
		this.tbStartTime = new IBrainChrom2018.LclTextBox();
		this.tbEndTime = new IBrainChrom2018.LclTextBox();
		this.tbValue = new IBrainChrom2018.LclTextBox();
		this.btnSuggest = new IBrainChrom2018.LclButton();
		this.tbValue2 = new IBrainChrom2018.LclTextBox();
		this.lbValue2 = new IBrainChrom2018.LclLabel();
		this.tbValue3 = new IBrainChrom2018.LclTextBox();
		this.lbValue3 = new IBrainChrom2018.LclLabel();
		this.tbValue4 = new IBrainChrom2018.LclTextBox();
		this.lbValue4 = new IBrainChrom2018.LclLabel();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(116, 148);
		base.btnCancel.Text = "取消";
		base.btnCancel.Click += new System.EventHandler(method_0);
		base.btnHelp.Location = new System.Drawing.Point(213, 148);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(24, 148);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_1);
		this.lbStartTime.Location = new System.Drawing.Point(34, 23);
		this.lbStartTime.Name = "lbStartTime";
		this.lbStartTime.Size = new System.Drawing.Size(59, 12);
		this.lbStartTime.TabIndex = 1;
		this.lbStartTime.Text = "开始时间";
		this.lbEndTime.AutoSize = true;
		this.lbEndTime.Location = new System.Drawing.Point(34, 51);
		this.lbEndTime.Name = "lbEndTime";
		this.lbEndTime.Size = new System.Drawing.Size(53, 12);
		this.lbEndTime.TabIndex = 1;
		this.lbEndTime.Text = "结束时间";
		this.lbValue.AutoSize = true;
		this.lbValue.Location = new System.Drawing.Point(34, 78);
		this.lbValue.Name = "lbValue";
		this.lbValue.Size = new System.Drawing.Size(41, 12);
		this.lbValue.TabIndex = 1;
		this.lbValue.Text = "面积比";
		this.lbUnitV.AutoSize = true;
		this.lbUnitV.Location = new System.Drawing.Point(174, 78);
		this.lbUnitV.Name = "lbUnitV";
		this.lbUnitV.Size = new System.Drawing.Size(59, 12);
		this.lbUnitV.TabIndex = 1;
		this.lbUnitV.Text = "lclLabel1";
		this.lbUnitT2.AutoSize = true;
		this.lbUnitT2.Location = new System.Drawing.Point(174, 51);
		this.lbUnitT2.Name = "lbUnitT2";
		this.lbUnitT2.Size = new System.Drawing.Size(35, 12);
		this.lbUnitT2.TabIndex = 1;
		this.lbUnitT2.Text = "[min]";
		this.lbUnitT1.AutoSize = true;
		this.lbUnitT1.Location = new System.Drawing.Point(174, 23);
		this.lbUnitT1.Name = "lbUnitT1";
		this.lbUnitT1.Size = new System.Drawing.Size(35, 12);
		this.lbUnitT1.TabIndex = 1;
		this.lbUnitT1.Text = "[min]";
		this.tbStartTime.Location = new System.Drawing.Point(99, 21);
		this.tbStartTime.Name = "tbStartTime";
		this.tbStartTime.Size = new System.Drawing.Size(69, 21);
		this.tbStartTime.TabIndex = 2;
		this.tbStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbEndTime.Location = new System.Drawing.Point(99, 48);
		this.tbEndTime.Name = "tbEndTime";
		this.tbEndTime.Size = new System.Drawing.Size(69, 21);
		this.tbEndTime.TabIndex = 2;
		this.tbEndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbValue.Location = new System.Drawing.Point(99, 75);
		this.tbValue.Name = "tbValue";
		this.tbValue.Size = new System.Drawing.Size(69, 21);
		this.tbValue.TabIndex = 2;
		this.tbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.btnSuggest.Location = new System.Drawing.Point(239, 75);
		this.btnSuggest.Name = "btnSuggest";
		this.btnSuggest.Size = new System.Drawing.Size(55, 23);
		this.btnSuggest.TabIndex = 3;
		this.btnSuggest.Text = "...";
		this.btnSuggest.UseVisualStyleBackColor = true;
		this.btnSuggest.Click += new System.EventHandler(btnSuggest_Click);
		this.tbValue2.Location = new System.Drawing.Point(99, 98);
		this.tbValue2.Name = "tbValue2";
		this.tbValue2.Size = new System.Drawing.Size(69, 21);
		this.tbValue2.TabIndex = 2;
		this.tbValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.lbValue2.AutoSize = true;
		this.lbValue2.Location = new System.Drawing.Point(34, 101);
		this.lbValue2.Name = "lbValue2";
		this.lbValue2.Size = new System.Drawing.Size(41, 12);
		this.lbValue2.TabIndex = 1;
		this.lbValue2.Text = "斜率比";
		this.tbValue3.Location = new System.Drawing.Point(99, 121);
		this.tbValue3.Name = "tbValue3";
		this.tbValue3.Size = new System.Drawing.Size(63, 21);
		this.tbValue3.TabIndex = 2;
		this.tbValue3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.lbValue3.AutoSize = true;
		this.lbValue3.Location = new System.Drawing.Point(34, 125);
		this.lbValue3.Name = "lbValue3";
		this.lbValue3.Size = new System.Drawing.Size(65, 12);
		this.lbValue3.TabIndex = 1;
		this.lbValue3.Text = "左肩切系数";
		this.tbValue4.Location = new System.Drawing.Point(239, 121);
		this.tbValue4.Name = "tbValue4";
		this.tbValue4.Size = new System.Drawing.Size(55, 21);
		this.tbValue4.TabIndex = 2;
		this.tbValue4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.lbValue4.AutoSize = true;
		this.lbValue4.Location = new System.Drawing.Point(168, 125);
		this.lbValue4.Name = "lbValue4";
		this.lbValue4.Size = new System.Drawing.Size(65, 12);
		this.lbValue4.TabIndex = 1;
		this.lbValue4.Text = "右肩切系数";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(314, 182);
		base.Controls.Add(this.lbStartTime);
		base.Controls.Add(this.lbEndTime);
		base.Controls.Add(this.lbValue4);
		base.Controls.Add(this.lbValue3);
		base.Controls.Add(this.lbValue2);
		base.Controls.Add(this.lbValue);
		base.Controls.Add(this.tbStartTime);
		base.Controls.Add(this.lbUnitV);
		base.Controls.Add(this.tbValue4);
		base.Controls.Add(this.tbValue3);
		base.Controls.Add(this.tbValue2);
		base.Controls.Add(this.tbValue);
		base.Controls.Add(this.lbUnitT1);
		base.Controls.Add(this.tbEndTime);
		base.Controls.Add(this.btnSuggest);
		base.Controls.Add(this.lbUnitT2);
		base.Name = "ManuDlg";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ManuDlg_FormClosing);
		base.Controls.SetChildIndex(this.lbUnitT2, 0);
		base.Controls.SetChildIndex(this.btnSuggest, 0);
		base.Controls.SetChildIndex(this.tbEndTime, 0);
		base.Controls.SetChildIndex(this.lbUnitT1, 0);
		base.Controls.SetChildIndex(this.tbValue, 0);
		base.Controls.SetChildIndex(this.tbValue2, 0);
		base.Controls.SetChildIndex(this.tbValue3, 0);
		base.Controls.SetChildIndex(this.tbValue4, 0);
		base.Controls.SetChildIndex(this.lbUnitV, 0);
		base.Controls.SetChildIndex(this.tbStartTime, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.lbValue, 0);
		base.Controls.SetChildIndex(this.lbValue2, 0);
		base.Controls.SetChildIndex(this.lbValue3, 0);
		base.Controls.SetChildIndex(this.lbValue4, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(this.lbEndTime, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbStartTime, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		bool flag = integRow_0.oprtStyle == IntegOprtStyle.BsTgnt;
		lbStartTime.Text = Lang.PS("开始时间", "Start Time");
		lbEndTime.Text = Lang.PS("结束时间", "End Time");
		lbValue.Text = Lang.PS(flag ? "面积比" : "值", flag ? "Area Ratio" : "Value");
		lbValue2.Text = Lang.PS("斜率比", "Slope Ratio");
	}

	private void ManuDlg_FormClosing(object sender, FormClosingEventArgs e)
	{
		base.Visible = false;
		e.Cancel = true;
	}

	public void RefreshValue(LclIntegGridView lclIntegGridView_0, IntegRow integRow)
	{
		tbValue.Text = integRow.value.ToString(lclIntegGridView_0.ColValueFmt);
	}

	public void SetTitleValueU(LclIntegGridView lclIntegGridView_0, IntegRow integRow, string fmtTime)
	{
		Text = integRow.ExpString(-1);
		string text = ((integRow.ValueUnitStr != "") ? integRow.ValueUnitStr : "-");
		lbUnitV.Text = "[" + text + "]";
		integRow_0 = integRow;
		tbStartTime.Text = integRow.timeA.ToString(fmtTime);
		tbEndTime.Text = integRow.timeB.ToString(fmtTime);
		tbValue.Text = integRow.value.ToString(lclIntegGridView_0.ColValueFmt);
		tbValue2.Text = integRow.value2.ToString(lclIntegGridView_0.ColValueFmt);
		tbValue3.Text = integRow.value3.ToString(lclIntegGridView_0.ColValueFmt);
		tbValue4.Text = integRow.value4.ToString(lclIntegGridView_0.ColValueFmt);
	}

	public void Show(bool showSuggest)
	{
		btnSuggest.Enabled = showSuggest;
		bool flag = integRow_0.oprtStyle == IntegOprtStyle.BsTgnt;
		LclLabel lclLabel = lbValue2;
		LclTextBox lclTextBox = tbValue2;
		LclLabel lclLabel2 = lbValue3;
		LclTextBox lclTextBox2 = tbValue3;
		LclLabel lclLabel3 = lbValue4;
		bool flag2 = (tbValue4.Visible = flag);
		bool flag4 = (lclLabel3.Visible = flag2);
		bool flag6 = (lclTextBox2.Visible = flag4);
		bool flag8 = (lclLabel2.Visible = flag6);
		bool visible = (lclTextBox.Visible = flag8);
		lclLabel.Visible = visible;
		lbValue.Text = Lang.PS(flag ? "面积比" : "值", flag ? "Area Ratio" : "Value");
		Show();
	}
}
