using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChannelCtrl : UserControl
{
	public static ChannelCtrl selfCtrl;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private ChannelTCPServer ChannelTCPserver = new ChannelTCPServer(2001);

	public int anysisChannel = 0;

	public int anysisChannelOld = 0;

	public long anysisStart = 0L;

	private byte channelEnabelState = 0;

	public long currentTimes = 0L;

	private IContainer components = null;

	private Label label84;

	private Label label83;

	private Label label82;

	private Label label81;

	public PictureBox pibChannel8;

	public PictureBox pibChannel7;

	public PictureBox pibChannel6;

	public PictureBox pibChannel5;

	public PictureBox pibChannel4;

	public PictureBox pibChannel3;

	public PictureBox pibChannel2;

	public PictureBox pibChannel1;

	private Button btnSet;

	private Button btnStart;

	private TextBox tbComTimes;

	private TextBox tbCycleTimes;

	private TextBox tbAnyTime;

	private TextBox tbInjecTime;

	private Label label80;

	private Label label79;

	private Label label78;

	private Label label77;

	private CheckBox chbChannel8;

	private CheckBox chbChannel7;

	private CheckBox chbChannel6;

	private CheckBox chbChannel5;

	private CheckBox chbChannel4;

	private CheckBox chbChannel3;

	private CheckBox chbChannel2;

	private CheckBox chbChannel1;

	private CheckBox cbEnNMHC;

	private TextBox tbChannelNumber;

	private Label label76;

	private GroupBox groupBox1;

	public static bool IsDesignMode()
	{
		return false;
	}

	public ChannelCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		if (!IsDesignMode())
		{
			chbChannel1.Checked = frmParam.bChannel1;
			chbChannel2.Checked = frmParam.bChannel2;
			chbChannel3.Checked = frmParam.bChannel3;
			chbChannel4.Checked = frmParam.bChannel4;
			chbChannel5.Checked = frmParam.bChannel5;
			chbChannel6.Checked = frmParam.bChannel6;
			chbChannel7.Checked = frmParam.bChannel7;
			chbChannel8.Checked = frmParam.bChannel8;
			tbInjecTime.Text = frmParam.fInjecTime.ToString();
			tbCycleTimes.Text = frmParam.iCycleTimes.ToString();
			tbAnyTime.Text = frmParam.fAnyTime.ToString();
			tbChannelNumber.Text = frmParam.iChannelNumber.ToString();
			tbComTimes.Text = frmParam.iComTimes.ToString();
		}
	}

	public void channelTCPServerStart()
	{
	}

	public string updatefilename(string filename)
	{
		switch (anysisChannel)
		{
		case 1:
			tbChannelNumber.Text = "1";
			return "通道1-" + currentTimes + "-" + filename;
		case 2:
			tbChannelNumber.Text = "2";
			return "通道2-" + currentTimes + "-" + filename;
		case 3:
			tbChannelNumber.Text = "3";
			return "通道3-" + currentTimes + "-" + filename;
		case 4:
			tbChannelNumber.Text = "4";
			return "通道4-" + currentTimes + "-" + filename;
		case 5:
			tbChannelNumber.Text = "5";
			return "通道5-" + currentTimes + "-" + filename;
		case 6:
			tbChannelNumber.Text = "6";
			return "通道6-" + currentTimes + "-" + filename;
		case 7:
			tbChannelNumber.Text = "7";
			return "通道7-" + currentTimes + "-" + filename;
		case 8:
			tbChannelNumber.Text = "8";
			return "通道8-" + currentTimes + "-" + filename;
		default:
			anysisChannel = 0;
			return null;
		}
	}

	public void channelUpdate()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket == null)
		{
			return;
		}
		if (currentTcpServerSocket.Ready)
		{
			byte[] data = new byte[3] { 192, 168, 1 };
			foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
			{
				ChannelTCPserver.Send(client, data);
			}
		}
		else
		{
			byte[] data2 = new byte[3] { 192, 168, 0 };
			foreach (ChannelTCPClientState client2 in ChannelTCPserver._clients)
			{
				ChannelTCPserver.Send(client2, data2);
			}
		}
		if (ChannelTCPserver.dataBuff[0] != 192)
		{
			return;
		}
		switch (ChannelTCPserver.dataBuff[1])
		{
		case 160:
			switch (ChannelTCPserver.dataBuff[2])
			{
			case 0:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				break;
			case 1:
				anysisChannel = 1;
				pibChannel1.Image = Resources.x12;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				break;
			case 2:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x12;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				anysisChannel = 2;
				break;
			case 4:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x12;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				anysisChannel = 3;
				break;
			case 8:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x12;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				anysisChannel = 4;
				break;
			case 16:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x12;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				anysisChannel = 5;
				break;
			case 32:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x12;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x13;
				anysisChannel = 6;
				break;
			case 64:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x12;
				pibChannel8.Image = Resources.x13;
				anysisChannel = 7;
				break;
			case 128:
				pibChannel1.Image = Resources.x13;
				pibChannel2.Image = Resources.x13;
				pibChannel3.Image = Resources.x13;
				pibChannel4.Image = Resources.x13;
				pibChannel5.Image = Resources.x13;
				pibChannel6.Image = Resources.x13;
				pibChannel7.Image = Resources.x13;
				pibChannel8.Image = Resources.x12;
				anysisChannel = 8;
				break;
			case 250:
				ChannelTCPserver.dataBuff[2] = 0;
				if (Class49.user_0.ULevel != User.Level.访问员)
				{
					currentTcpServerSocket.SendCmd(18);
					return;
				}
				MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
				break;
			}
			break;
		case 161:
			channelEnabelState = ChannelTCPserver.dataBuff[2];
			if ((channelEnabelState & 1) == 1)
			{
				chbChannel1.Checked = true;
			}
			else
			{
				chbChannel1.Checked = false;
			}
			if ((channelEnabelState & 2) == 2)
			{
				chbChannel2.Checked = true;
			}
			else
			{
				chbChannel2.Checked = false;
			}
			if ((channelEnabelState & 4) == 4)
			{
				chbChannel3.Checked = true;
			}
			else
			{
				chbChannel3.Checked = false;
			}
			if ((channelEnabelState & 8) == 8)
			{
				chbChannel4.Checked = true;
			}
			else
			{
				chbChannel4.Checked = false;
			}
			if ((channelEnabelState & 0x10) == 16)
			{
				chbChannel5.Checked = true;
			}
			else
			{
				chbChannel5.Checked = false;
			}
			if ((channelEnabelState & 0x20) == 32)
			{
				chbChannel6.Checked = true;
			}
			else
			{
				chbChannel6.Checked = false;
			}
			if ((channelEnabelState & 0x40) == 64)
			{
				chbChannel7.Checked = true;
			}
			else
			{
				chbChannel7.Checked = false;
			}
			if ((channelEnabelState & 0x80) == 128)
			{
				chbChannel8.Checked = true;
			}
			else
			{
				chbChannel8.Checked = false;
			}
			break;
		case 162:
			if (ChannelTCPserver.dataBuff[2] == 1)
			{
				btnStart.Text = "停止采集";
				chbChannel1.Enabled = false;
				chbChannel2.Enabled = false;
				chbChannel3.Enabled = false;
				chbChannel4.Enabled = false;
				chbChannel5.Enabled = false;
				chbChannel6.Enabled = false;
				chbChannel7.Enabled = false;
				chbChannel8.Enabled = false;
				tbInjecTime.ReadOnly = true;
				tbCycleTimes.ReadOnly = true;
				tbAnyTime.ReadOnly = true;
			}
			else
			{
				btnStart.Text = "循环采集";
				chbChannel1.Enabled = true;
				chbChannel2.Enabled = true;
				chbChannel3.Enabled = true;
				chbChannel4.Enabled = true;
				chbChannel5.Enabled = true;
				chbChannel6.Enabled = true;
				chbChannel7.Enabled = true;
				chbChannel8.Enabled = true;
				tbInjecTime.ReadOnly = false;
				tbCycleTimes.ReadOnly = false;
				tbAnyTime.ReadOnly = false;
			}
			break;
		case 163:
			if (Class49.user_0.ULevel != User.Level.访问员)
			{
				if (ChannelTCPserver.dataBuff[2] == 1)
				{
					currentTcpServerSocket.SendCmd(18);
				}
				else
				{
					currentTcpServerSocket.SendCmd(19);
				}
			}
			else
			{
				MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
			}
			break;
		case 164:
		{
			byte[] array2 = new byte[4];
			byte[] array3 = new byte[6]
			{
				192,
				164,
				ChannelTCPserver.dataBuff[2],
				ChannelTCPserver.dataBuff[3],
				ChannelTCPserver.dataBuff[4],
				ChannelTCPserver.dataBuff[5]
			};
			float num2 = 0f;
			float[] array = new float[1];
			Buffer.BlockCopy(new ushort[2]
			{
				(ushort)((ChannelTCPserver.dataBuff[4] << 8) | ChannelTCPserver.dataBuff[5]),
				(ushort)((ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3])
			}, 0, array, 0, 4);
			tbInjecTime.Text = array[0].ToString("0.0");
			break;
		}
		case 165:
		{
			float[] array = new float[1];
			Buffer.BlockCopy(new ushort[2]
			{
				(ushort)((ChannelTCPserver.dataBuff[4] << 8) | ChannelTCPserver.dataBuff[5]),
				(ushort)((ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3])
			}, 0, array, 0, 4);
			tbAnyTime.Text = array[0].ToString("0.0");
			break;
		}
		case 166:
		{
			int num = 0;
			num = (ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3];
			tbCycleTimes.Text = num.ToString();
			break;
		}
		case 167:
		{
			int num = (ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3];
			currentTimes = num + 1;
			tbComTimes.Text = num.ToString();
			break;
		}
		}
		ChannelTCPserver.dataBuff[0] = 0;
		ChannelTCPserver.dataBuff[1] = 0;
		ChannelTCPserver.dataBuff[2] = 0;
		ChannelTCPserver.dataBuff[3] = 0;
	}

	private void chbChannel2_CheckedChanged(object sender, EventArgs e)
	{
		byte b = 0;
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel1.Checked)
		{
			b |= 1;
		}
		if (chbChannel2.Checked)
		{
			b |= 2;
		}
		if (chbChannel3.Checked)
		{
			b |= 4;
		}
		if (chbChannel4.Checked)
		{
			b |= 8;
		}
		if (chbChannel5.Checked)
		{
			b |= 0x10;
		}
		if (chbChannel6.Checked)
		{
			b |= 0x20;
		}
		if (chbChannel7.Checked)
		{
			b |= 0x40;
		}
		if (chbChannel8.Checked)
		{
			b |= 0x80;
		}
		array[2] = b;
		channelEnabelState = b;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void SaveParam()
	{
		frmParam.bChannel1 = chbChannel1.Checked;
		frmParam.bChannel2 = chbChannel2.Checked;
		frmParam.bChannel3 = chbChannel3.Checked;
		frmParam.bChannel4 = chbChannel4.Checked;
		frmParam.bChannel5 = chbChannel5.Checked;
		frmParam.bChannel6 = chbChannel6.Checked;
		frmParam.bChannel7 = chbChannel7.Checked;
		frmParam.bChannel8 = chbChannel8.Checked;
		frmParam.fInjecTime = ToFloat(tbInjecTime.Text);
		frmParam.iCycleTimes = ToInt(tbCycleTimes.Text);
		frmParam.fAnyTime = ToFloat(tbAnyTime.Text);
		frmParam.iChannelNumber = ToInt(tbChannelNumber.Text);
		frmParam.iComTimes = ToInt(tbComTimes.Text);
		frmParam.SaveParam();
	}

	private int ToInt(string str)
	{
		int result = 0;
		int.TryParse(str, out result);
		return result;
	}

	private float ToFloat(string str)
	{
		float result = 0f;
		float.TryParse(str, out result);
		return result;
	}

	private void btnSet_Click(object sender, EventArgs e)
	{
		byte[] array = new byte[4];
		byte[] array2 = new byte[6] { 192, 164, 0, 0, 0, 0 };
		float result = 0f;
		float.TryParse(tbInjecTime.Text, out result);
		if (result <= 0f)
		{
			MessageBox.Show("采样时间须大于0！");
			return;
		}
		float[] array3 = new float[1];
		ushort[] array4 = new ushort[2];
		array3[0] = result;
		Buffer.BlockCopy(array3, 0, array4, 0, 4);
		Buffer.BlockCopy(array4, 0, array, 0, 4);
		array2[2] = array[3];
		array2[3] = array[2];
		array2[4] = array[1];
		array2[5] = array[0];
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array2);
		}
		Thread.Sleep(50);
		float.TryParse(tbAnyTime.Text, out result);
		if (result <= 0f)
		{
			MessageBox.Show("分析时间须大于0！");
			return;
		}
		array3[0] = result;
		Buffer.BlockCopy(array3, 0, array4, 0, 4);
		Buffer.BlockCopy(array4, 0, array, 0, 4);
		array2[0] = 192;
		array2[1] = 165;
		array2[2] = array[3];
		array2[3] = array[2];
		array2[4] = array[1];
		array2[5] = array[0];
		foreach (ChannelTCPClientState client2 in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client2, array2);
		}
		Thread.Sleep(50);
		int result2 = 0;
		int.TryParse(tbCycleTimes.Text, out result2);
		if (result2 < 0)
		{
			MessageBox.Show("循环次数须大于等于0！");
			return;
		}
		byte[] data = new byte[4]
		{
			192,
			166,
			(byte)(result2 >> 8),
			(byte)result2
		};
		foreach (ChannelTCPClientState client3 in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client3, data);
		}
		SaveParam();
	}

	private void btnStart_Click(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 162, 0 };
		if (btnStart.Text == Lang.PS("循环采集", "StartAll"))
		{
			if (channelEnabelState == 0)
			{
				MessageBox.Show("至少选择一个流路");
				return;
			}
			chbChannel1.Enabled = false;
			chbChannel2.Enabled = false;
			chbChannel3.Enabled = false;
			chbChannel4.Enabled = false;
			chbChannel5.Enabled = false;
			chbChannel6.Enabled = false;
			chbChannel7.Enabled = false;
			chbChannel8.Enabled = false;
			tbInjecTime.ReadOnly = true;
			tbCycleTimes.ReadOnly = true;
			tbAnyTime.ReadOnly = true;
			array[2] = 1;
			btnStart.Text = Lang.PS("停止采集", "StartAll");
		}
		else
		{
			btnStart.Text = Lang.PS("循环采集", "StartAll");
			chbChannel1.Enabled = true;
			chbChannel2.Enabled = true;
			chbChannel3.Enabled = true;
			chbChannel4.Enabled = true;
			chbChannel5.Enabled = true;
			chbChannel6.Enabled = true;
			chbChannel7.Enabled = true;
			chbChannel8.Enabled = true;
			tbInjecTime.ReadOnly = false;
			tbCycleTimes.ReadOnly = false;
			tbAnyTime.ReadOnly = false;
			array[2] = 0;
		}
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
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
		this.label84 = new System.Windows.Forms.Label();
		this.label83 = new System.Windows.Forms.Label();
		this.label82 = new System.Windows.Forms.Label();
		this.label81 = new System.Windows.Forms.Label();
		this.pibChannel8 = new System.Windows.Forms.PictureBox();
		this.pibChannel7 = new System.Windows.Forms.PictureBox();
		this.pibChannel6 = new System.Windows.Forms.PictureBox();
		this.pibChannel5 = new System.Windows.Forms.PictureBox();
		this.pibChannel4 = new System.Windows.Forms.PictureBox();
		this.pibChannel3 = new System.Windows.Forms.PictureBox();
		this.pibChannel2 = new System.Windows.Forms.PictureBox();
		this.pibChannel1 = new System.Windows.Forms.PictureBox();
		this.btnSet = new System.Windows.Forms.Button();
		this.btnStart = new System.Windows.Forms.Button();
		this.tbComTimes = new System.Windows.Forms.TextBox();
		this.tbCycleTimes = new System.Windows.Forms.TextBox();
		this.tbAnyTime = new System.Windows.Forms.TextBox();
		this.tbInjecTime = new System.Windows.Forms.TextBox();
		this.label80 = new System.Windows.Forms.Label();
		this.label79 = new System.Windows.Forms.Label();
		this.label78 = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.chbChannel8 = new System.Windows.Forms.CheckBox();
		this.chbChannel7 = new System.Windows.Forms.CheckBox();
		this.chbChannel6 = new System.Windows.Forms.CheckBox();
		this.chbChannel5 = new System.Windows.Forms.CheckBox();
		this.chbChannel4 = new System.Windows.Forms.CheckBox();
		this.chbChannel3 = new System.Windows.Forms.CheckBox();
		this.chbChannel2 = new System.Windows.Forms.CheckBox();
		this.chbChannel1 = new System.Windows.Forms.CheckBox();
		this.cbEnNMHC = new System.Windows.Forms.CheckBox();
		this.tbChannelNumber = new System.Windows.Forms.TextBox();
		this.label76 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.pibChannel8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.label84.AutoSize = true;
		this.label84.Location = new System.Drawing.Point(534, 108);
		this.label84.Name = "label84";
		this.label84.Size = new System.Drawing.Size(17, 12);
		this.label84.TabIndex = 74;
		this.label84.Text = "次";
		this.label83.AutoSize = true;
		this.label83.Location = new System.Drawing.Point(534, 82);
		this.label83.Name = "label83";
		this.label83.Size = new System.Drawing.Size(17, 12);
		this.label83.TabIndex = 73;
		this.label83.Text = "次";
		this.label82.AutoSize = true;
		this.label82.Location = new System.Drawing.Point(534, 51);
		this.label82.Name = "label82";
		this.label82.Size = new System.Drawing.Size(29, 12);
		this.label82.TabIndex = 72;
		this.label82.Text = "分钟";
		this.label81.AutoSize = true;
		this.label81.Location = new System.Drawing.Point(534, 20);
		this.label81.Name = "label81";
		this.label81.Size = new System.Drawing.Size(29, 12);
		this.label81.TabIndex = 71;
		this.label81.Text = "分钟";
		this.pibChannel8.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel8.Location = new System.Drawing.Point(306, 88);
		this.pibChannel8.Name = "pibChannel8";
		this.pibChannel8.Size = new System.Drawing.Size(63, 63);
		this.pibChannel8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel8.TabIndex = 70;
		this.pibChannel8.TabStop = false;
		this.pibChannel7.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel7.Location = new System.Drawing.Point(201, 88);
		this.pibChannel7.Name = "pibChannel7";
		this.pibChannel7.Size = new System.Drawing.Size(63, 63);
		this.pibChannel7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel7.TabIndex = 69;
		this.pibChannel7.TabStop = false;
		this.pibChannel6.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel6.Location = new System.Drawing.Point(103, 88);
		this.pibChannel6.Name = "pibChannel6";
		this.pibChannel6.Size = new System.Drawing.Size(63, 63);
		this.pibChannel6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel6.TabIndex = 68;
		this.pibChannel6.TabStop = false;
		this.pibChannel5.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel5.Location = new System.Drawing.Point(13, 88);
		this.pibChannel5.Name = "pibChannel5";
		this.pibChannel5.Size = new System.Drawing.Size(63, 63);
		this.pibChannel5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel5.TabIndex = 67;
		this.pibChannel5.TabStop = false;
		this.pibChannel4.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel4.Location = new System.Drawing.Point(306, 19);
		this.pibChannel4.Name = "pibChannel4";
		this.pibChannel4.Size = new System.Drawing.Size(63, 63);
		this.pibChannel4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel4.TabIndex = 66;
		this.pibChannel4.TabStop = false;
		this.pibChannel3.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel3.Location = new System.Drawing.Point(201, 18);
		this.pibChannel3.Name = "pibChannel3";
		this.pibChannel3.Size = new System.Drawing.Size(63, 63);
		this.pibChannel3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel3.TabIndex = 65;
		this.pibChannel3.TabStop = false;
		this.pibChannel2.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel2.Location = new System.Drawing.Point(103, 19);
		this.pibChannel2.Name = "pibChannel2";
		this.pibChannel2.Size = new System.Drawing.Size(63, 63);
		this.pibChannel2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel2.TabIndex = 64;
		this.pibChannel2.TabStop = false;
		this.pibChannel1.Image = IBrainChrom2018.Properties.Resources.x13;
		this.pibChannel1.Location = new System.Drawing.Point(13, 18);
		this.pibChannel1.Name = "pibChannel1";
		this.pibChannel1.Size = new System.Drawing.Size(63, 63);
		this.pibChannel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel1.TabIndex = 63;
		this.pibChannel1.TabStop = false;
		this.btnSet.Location = new System.Drawing.Point(423, 131);
		this.btnSet.Name = "btnSet";
		this.btnSet.Size = new System.Drawing.Size(75, 23);
		this.btnSet.TabIndex = 62;
		this.btnSet.Text = "设定";
		this.btnSet.UseVisualStyleBackColor = true;
		this.btnSet.Click += new System.EventHandler(btnSet_Click);
		this.btnStart.Location = new System.Drawing.Point(569, 16);
		this.btnStart.Name = "btnStart";
		this.btnStart.Size = new System.Drawing.Size(121, 39);
		this.btnStart.TabIndex = 61;
		this.btnStart.Text = "循环采集";
		this.btnStart.UseVisualStyleBackColor = true;
		this.btnStart.Click += new System.EventHandler(btnStart_Click);
		this.tbComTimes.Location = new System.Drawing.Point(478, 103);
		this.tbComTimes.Name = "tbComTimes";
		this.tbComTimes.ReadOnly = true;
		this.tbComTimes.Size = new System.Drawing.Size(48, 21);
		this.tbComTimes.TabIndex = 60;
		this.tbCycleTimes.Location = new System.Drawing.Point(478, 73);
		this.tbCycleTimes.Name = "tbCycleTimes";
		this.tbCycleTimes.Size = new System.Drawing.Size(48, 21);
		this.tbCycleTimes.TabIndex = 59;
		this.tbAnyTime.Location = new System.Drawing.Point(478, 46);
		this.tbAnyTime.Name = "tbAnyTime";
		this.tbAnyTime.Size = new System.Drawing.Size(48, 21);
		this.tbAnyTime.TabIndex = 58;
		this.tbInjecTime.Location = new System.Drawing.Point(478, 16);
		this.tbInjecTime.Name = "tbInjecTime";
		this.tbInjecTime.Size = new System.Drawing.Size(48, 21);
		this.tbInjecTime.TabIndex = 57;
		this.label80.AutoSize = true;
		this.label80.Location = new System.Drawing.Point(419, 108);
		this.label80.Name = "label80";
		this.label80.Size = new System.Drawing.Size(53, 12);
		this.label80.TabIndex = 56;
		this.label80.Text = "完成次数";
		this.label79.AutoSize = true;
		this.label79.Location = new System.Drawing.Point(419, 82);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(53, 12);
		this.label79.TabIndex = 55;
		this.label79.Text = "循环次数";
		this.label78.AutoSize = true;
		this.label78.Location = new System.Drawing.Point(419, 51);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(53, 12);
		this.label78.TabIndex = 54;
		this.label78.Text = "分析时间";
		this.label77.AutoSize = true;
		this.label77.Location = new System.Drawing.Point(419, 19);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(53, 12);
		this.label77.TabIndex = 53;
		this.label77.Text = "进样时间";
		this.chbChannel8.AutoSize = true;
		this.chbChannel8.Location = new System.Drawing.Point(379, 126);
		this.chbChannel8.Name = "chbChannel8";
		this.chbChannel8.Size = new System.Drawing.Size(15, 14);
		this.chbChannel8.TabIndex = 52;
		this.chbChannel8.UseVisualStyleBackColor = true;
		this.chbChannel8.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel7.AutoSize = true;
		this.chbChannel7.Location = new System.Drawing.Point(282, 126);
		this.chbChannel7.Name = "chbChannel7";
		this.chbChannel7.Size = new System.Drawing.Size(15, 14);
		this.chbChannel7.TabIndex = 51;
		this.chbChannel7.UseVisualStyleBackColor = true;
		this.chbChannel7.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel6.AutoSize = true;
		this.chbChannel6.Location = new System.Drawing.Point(176, 126);
		this.chbChannel6.Name = "chbChannel6";
		this.chbChannel6.Size = new System.Drawing.Size(15, 14);
		this.chbChannel6.TabIndex = 50;
		this.chbChannel6.UseVisualStyleBackColor = true;
		this.chbChannel6.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel5.AutoSize = true;
		this.chbChannel5.Location = new System.Drawing.Point(82, 126);
		this.chbChannel5.Name = "chbChannel5";
		this.chbChannel5.Size = new System.Drawing.Size(15, 14);
		this.chbChannel5.TabIndex = 49;
		this.chbChannel5.UseVisualStyleBackColor = true;
		this.chbChannel5.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel4.AutoSize = true;
		this.chbChannel4.Location = new System.Drawing.Point(379, 65);
		this.chbChannel4.Name = "chbChannel4";
		this.chbChannel4.Size = new System.Drawing.Size(15, 14);
		this.chbChannel4.TabIndex = 48;
		this.chbChannel4.UseVisualStyleBackColor = true;
		this.chbChannel4.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel3.AutoSize = true;
		this.chbChannel3.Location = new System.Drawing.Point(282, 65);
		this.chbChannel3.Name = "chbChannel3";
		this.chbChannel3.Size = new System.Drawing.Size(15, 14);
		this.chbChannel3.TabIndex = 47;
		this.chbChannel3.UseVisualStyleBackColor = true;
		this.chbChannel3.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel2.AutoSize = true;
		this.chbChannel2.Location = new System.Drawing.Point(176, 65);
		this.chbChannel2.Name = "chbChannel2";
		this.chbChannel2.Size = new System.Drawing.Size(15, 14);
		this.chbChannel2.TabIndex = 46;
		this.chbChannel2.UseVisualStyleBackColor = true;
		this.chbChannel2.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel1.AutoSize = true;
		this.chbChannel1.Location = new System.Drawing.Point(82, 65);
		this.chbChannel1.Name = "chbChannel1";
		this.chbChannel1.Size = new System.Drawing.Size(15, 14);
		this.chbChannel1.TabIndex = 45;
		this.chbChannel1.UseVisualStyleBackColor = true;
		this.chbChannel1.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.cbEnNMHC.AutoSize = true;
		this.cbEnNMHC.Location = new System.Drawing.Point(570, 108);
		this.cbEnNMHC.Name = "cbEnNMHC";
		this.cbEnNMHC.Size = new System.Drawing.Size(78, 16);
		this.cbEnNMHC.TabIndex = 44;
		this.cbEnNMHC.Text = "checkBox1";
		this.cbEnNMHC.UseVisualStyleBackColor = true;
		this.cbEnNMHC.Visible = false;
		this.tbChannelNumber.Location = new System.Drawing.Point(628, 79);
		this.tbChannelNumber.Name = "tbChannelNumber";
		this.tbChannelNumber.Size = new System.Drawing.Size(39, 21);
		this.tbChannelNumber.TabIndex = 43;
		this.tbChannelNumber.Visible = false;
		this.label76.AutoSize = true;
		this.label76.Location = new System.Drawing.Point(566, 83);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(65, 12);
		this.label76.TabIndex = 42;
		this.label76.Text = "样品流路：";
		this.label76.Visible = false;
		this.groupBox1.Controls.Add(this.pibChannel7);
		this.groupBox1.Controls.Add(this.label84);
		this.groupBox1.Controls.Add(this.label76);
		this.groupBox1.Controls.Add(this.label83);
		this.groupBox1.Controls.Add(this.tbChannelNumber);
		this.groupBox1.Controls.Add(this.label82);
		this.groupBox1.Controls.Add(this.cbEnNMHC);
		this.groupBox1.Controls.Add(this.label81);
		this.groupBox1.Controls.Add(this.chbChannel1);
		this.groupBox1.Controls.Add(this.pibChannel8);
		this.groupBox1.Controls.Add(this.chbChannel2);
		this.groupBox1.Controls.Add(this.chbChannel3);
		this.groupBox1.Controls.Add(this.pibChannel6);
		this.groupBox1.Controls.Add(this.chbChannel4);
		this.groupBox1.Controls.Add(this.pibChannel5);
		this.groupBox1.Controls.Add(this.chbChannel5);
		this.groupBox1.Controls.Add(this.pibChannel4);
		this.groupBox1.Controls.Add(this.chbChannel6);
		this.groupBox1.Controls.Add(this.pibChannel3);
		this.groupBox1.Controls.Add(this.chbChannel7);
		this.groupBox1.Controls.Add(this.pibChannel2);
		this.groupBox1.Controls.Add(this.chbChannel8);
		this.groupBox1.Controls.Add(this.pibChannel1);
		this.groupBox1.Controls.Add(this.label77);
		this.groupBox1.Controls.Add(this.btnSet);
		this.groupBox1.Controls.Add(this.label78);
		this.groupBox1.Controls.Add(this.btnStart);
		this.groupBox1.Controls.Add(this.label79);
		this.groupBox1.Controls.Add(this.tbComTimes);
		this.groupBox1.Controls.Add(this.label80);
		this.groupBox1.Controls.Add(this.tbCycleTimes);
		this.groupBox1.Controls.Add(this.tbInjecTime);
		this.groupBox1.Controls.Add(this.tbAnyTime);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(743, 158);
		this.groupBox1.TabIndex = 75;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "在线版";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "ChannelCtrl";
		base.Padding = new System.Windows.Forms.Padding(3);
		base.Size = new System.Drawing.Size(749, 164);
		((System.ComponentModel.ISupportInitialize)this.pibChannel8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
