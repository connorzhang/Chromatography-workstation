using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormRltReminder : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private bool bSwitch = true;

	private Peak[] peakReminde;

	private int cntAlarm = 0;

	private int indexChannelStart = 1;

	private IContainer components = null;

	private RichTextBox rTBResult;

	private Button btnConfirm;

	private Button btnCancel;

	private Timer timer1;

	private TextBox tbCurChrom;

	private Label 流路;

	[DllImport("Kernel32.dll")]
	public static extern bool Beep(int frequency, int duration);

	public FormRltReminder(Peak[] peak, int channelIndex)
	{
		InitializeComponent();
		base.TopMost = true;
		peakReminde = peak;
		tbCurChrom.Text = channelIndex.ToString();
		indexChannelStart = channelIndex;
		initForm();
	}

	public void initForm()
	{
		string text = "";
		float num = 0f;
		float num2 = 0f;
		for (int i = 2; i < peakReminde.Count(); i++)
		{
			if (peakReminde[i] != null)
			{
				num2 += peakReminde[i].area;
			}
		}
		for (int j = 0; j < peakReminde.Count(); j++)
		{
			if (peakReminde[j] != null)
			{
				if (j < 2)
				{
					num += peakReminde[j].amount;
					text = text + peakReminde[j].name + "：" + peakReminde[j].amount.ToString("0.000") + "\r\n";
				}
				else
				{
					text = text + peakReminde[j].name + "：" + (peakReminde[j].area / num2 * (100f - num)).ToString("0.000") + "\r\n";
				}
			}
		}
		rTBResult.Text = text;
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnConfirm_Click(object sender, EventArgs e)
	{
		float[] array = new float[100];
		float[] array2 = new float[1];
		ushort[] array3 = new ushort[2];
		float num = 0f;
		float num2 = 0f;
		int.TryParse(tbCurChrom.Text, out indexChannelStart);
		if (indexChannelStart < 1)
		{
			indexChannelStart = 1;
		}
		if (OnlineCtrl.selfCtrl == null)
		{
			return;
		}
		for (int i = 2; i < peakReminde.Count(); i++)
		{
			num2 += peakReminde[i].area;
		}
		for (int j = 0; j < peakReminde.Count(); j++)
		{
			if (peakReminde[j] != null)
			{
				if (j < 2)
				{
					num += peakReminde[j].amount;
					array[j] = peakReminde[j].area;
					array2 = new float[1] { array[j] };
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
					array2[0] = peakReminde[j].areaPer;
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
					array2[0] = peakReminde[j].amount;
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
					array2[0] = peakReminde[j].amountPer;
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
				}
				else
				{
					array[j] = peakReminde[j].area;
					array2 = new float[1] { array[j] };
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
					array2[0] = peakReminde[j].area / num2;
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
					array2[0] = peakReminde[j].area / num2 * (100f - num);
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
					array2[0] = peakReminde[j].amountPer;
					Buffer.BlockCopy(array2, 0, array3, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + j * 10 + (indexChannelStart - 1) * 100] = array3[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + j * 10 + (indexChannelStart - 1) * 100] = array3[1];
				}
			}
		}
	}

	private void tTBResult_TextChanged(object sender, EventArgs e)
	{
		string text = "";
		for (int i = 0; i < peakReminde.Count(); i++)
		{
			text = text + peakReminde[i].name + "  含量：" + peakReminde[i].amount.ToString("0.000") + "\r\n";
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		cntAlarm++;
		if (cntAlarm > 180)
		{
			Close();
		}
		if (bSwitch)
		{
			bSwitch = false;
			Beep(3000, 800);
		}
		else
		{
			bSwitch = true;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormRltReminder));
		this.rTBResult = new System.Windows.Forms.RichTextBox();
		this.btnConfirm = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.tbCurChrom = new System.Windows.Forms.TextBox();
		this.流路 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.rTBResult.Location = new System.Drawing.Point(12, 12);
		this.rTBResult.Name = "rTBResult";
		this.rTBResult.Size = new System.Drawing.Size(320, 334);
		this.rTBResult.TabIndex = 0;
		this.rTBResult.Text = "";
		this.rTBResult.TextChanged += new System.EventHandler(tTBResult_TextChanged);
		this.btnConfirm.Location = new System.Drawing.Point(257, 381);
		this.btnConfirm.Name = "btnConfirm";
		this.btnConfirm.Size = new System.Drawing.Size(75, 23);
		this.btnConfirm.TabIndex = 1;
		this.btnConfirm.Text = "上传";
		this.btnConfirm.UseVisualStyleBackColor = true;
		this.btnConfirm.Click += new System.EventHandler(btnConfirm_Click);
		this.btnCancel.Location = new System.Drawing.Point(162, 381);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "放弃";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.timer1.Enabled = true;
		this.timer1.Interval = 300;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.tbCurChrom.Location = new System.Drawing.Point(64, 384);
		this.tbCurChrom.Name = "tbCurChrom";
		this.tbCurChrom.Size = new System.Drawing.Size(48, 21);
		this.tbCurChrom.TabIndex = 70;
		this.流路.AutoSize = true;
		this.流路.Location = new System.Drawing.Point(5, 387);
		this.流路.Name = "流路";
		this.流路.Size = new System.Drawing.Size(53, 12);
		this.流路.TabIndex = 69;
		this.流路.Text = "采样延迟";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(347, 450);
		base.Controls.Add(this.tbCurChrom);
		base.Controls.Add(this.流路);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnConfirm);
		base.Controls.Add(this.rTBResult);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormRltReminder";
		this.Text = "FormRltReminder";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
