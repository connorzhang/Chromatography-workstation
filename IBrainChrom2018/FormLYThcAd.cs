using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormLYThcAd : Form
{
	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

	private IContainer components = null;

	private Label label1;

	private TextBox tbCatalytic;

	private Label label2;

	private Button btnSave;

	private Label label3;

	private TextBox tbSample;

	private Label label4;

	private Label label5;

	private TextBox tbSample2;

	private Label label6;

	public FormLYThcAd()
	{
		InitializeComponent();
		tbCatalytic.Text = lythcParamMgr.fCatalytic.ToString("0.0");
		tbSample.Text = lythcParamMgr.fSample.ToString("0.0");
		tbSample2.Text = lythcParamMgr.fSample2.ToString("0.0");
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		float.TryParse(tbCatalytic.Text, out lythcParamMgr.fCatalytic);
		float.TryParse(tbSample.Text, out lythcParamMgr.fSample);
		float.TryParse(tbSample2.Text, out lythcParamMgr.fSample2);
		lythcParamMgr.SaveParam();
		Close();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormLYThcAd));
		this.label1 = new System.Windows.Forms.Label();
		this.tbCatalytic = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.tbSample = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.tbSample2 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(54, 27);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(101, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "催化剂活化温度：";
		this.tbCatalytic.Location = new System.Drawing.Point(161, 24);
		this.tbCatalytic.Name = "tbCatalytic";
		this.tbCatalytic.Size = new System.Drawing.Size(100, 21);
		this.tbCatalytic.TabIndex = 1;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(266, 32);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "℃";
		this.btnSave.Location = new System.Drawing.Point(56, 158);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(221, 36);
		this.btnSave.TabIndex = 3;
		this.btnSave.Text = "保存";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(266, 74);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(17, 12);
		this.label3.TabIndex = 6;
		this.label3.Text = "℃";
		this.tbSample.Location = new System.Drawing.Point(161, 66);
		this.tbSample.Name = "tbSample";
		this.tbSample.Size = new System.Drawing.Size(100, 21);
		this.tbSample.TabIndex = 5;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 69);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(149, 12);
		this.label4.TabIndex = 4;
		this.label4.Text = "固定污染源废气测定温度：";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(267, 115);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(17, 12);
		this.label5.TabIndex = 9;
		this.label5.Text = "℃";
		this.tbSample2.Location = new System.Drawing.Point(161, 106);
		this.tbSample2.Name = "tbSample2";
		this.tbSample2.Size = new System.Drawing.Size(100, 21);
		this.tbSample2.TabIndex = 8;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(42, 109);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(113, 12);
		this.label6.TabIndex = 7;
		this.label6.Text = "环境空气测定温度：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(354, 229);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.tbSample2);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.tbSample);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.tbCatalytic);
		base.Controls.Add(this.label1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormLYThcAd";
		this.Text = "FormLYThcAd";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
