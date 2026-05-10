using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormAlarm : Form
{
	public static FormAlarm form = null;

	private bool bSwitch = true;

	public string strAlarmFile = "";

	private IContainer components = null;

	private Label label1;

	private Timer timer1;

	private Label label2;

	private Button btnClose;

	public string[] StrAlarmArray { get; set; }

	[DllImport("Kernel32.dll")]
	public static extern bool Beep(int frequency, int duration);

	public FormAlarm()
	{
		form = this;
		InitializeComponent();
		base.TopMost = true;
		label1.ForeColor = Color.Red;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void FormAlarm_FormClosing(object sender, FormClosingEventArgs e)
	{
		form = null;
	}

	private void Timer1_Tick(object sender, EventArgs e)
	{
		if (bSwitch)
		{
			bSwitch = false;
			if (StrAlarmArray != null)
			{
				label2.Text = "报警文件：" + strAlarmFile + "\n";
				for (int i = 0; i < StrAlarmArray.Count(); i++)
				{
					Label label = label2;
					label.Text = label.Text + StrAlarmArray[i] + "\n";
				}
			}
			label1.ForeColor = Color.Red;
			label2.ForeColor = Color.Red;
			Beep(1000, 200);
		}
		else
		{
			bSwitch = true;
			label1.ForeColor = Color.Black;
			label2.ForeColor = Color.Black;
		}
	}

	private void BtnClose_Click(object sender, EventArgs e)
	{
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormAlarm));
		this.label1 = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.label2 = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("宋体", 42f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.Location = new System.Drawing.Point(245, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(248, 56);
		this.label1.TabIndex = 0;
		this.label1.Text = "数据超标";
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(Timer1_Tick);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.Location = new System.Drawing.Point(12, 104);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 21);
		this.label2.TabIndex = 1;
		this.label2.Text = "超标数据";
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.Location = new System.Drawing.Point(713, 415);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(75, 23);
		this.btnClose.TabIndex = 2;
		this.btnClose.Text = "确定";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(BtnClose_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormAlarm";
		this.Text = "FormAlarm";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormAlarm_FormClosing);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
