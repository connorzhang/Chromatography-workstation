using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IBrainChrom2018.Properties;

namespace IBrainChrom2018;

public class FrmMsetup : Form
{
	public ChromFormInterface formMain_0;

	public string PathSunAquip = Application.StartupPath + "\\SPara.con";

	public string FileFodle = "";

	private bool bool_0;

	private IContainer icontainer_0;

	private GroupBox groupBox1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private GroupBox groupBox3;

	private TabPage tabPage2;

	private TabPage tabPage3;

	private TabPage tabPage4;

	private TabPage tabPage5;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	private Label label1;

	private Button button1;

	private TextBox textBox1;

	private CheckBox checkBox6;

	private CheckBox checkBox5;

	private CheckBox checkBox4;

	private Label label2;

	private Label label4;

	private Label label3;

	private CheckBox checkBox7;

	private Label label7;

	private Label label6;

	private Label label5;

	private CheckBox checkBox10;

	private CheckBox checkBox9;

	private CheckBox checkBox8;

	private Label label8;

	private Label label9;

	private GroupBox groupBox2;

	private Label label10;

	private CheckBox checkBox12;

	private CheckBox checkBox14;

	private CheckBox checkBox13;

	private CheckBox checkBox11;

	private TextBox textBox2;

	private CheckBox checkBox18;

	private CheckBox checkBox17;

	private CheckBox checkBox16;

	private TextBox textBox4;

	private Label label12;

	private Label label13;

	private TextBox textBox3;

	private Label label11;

	private GroupBox groupBox4;

	private RadioButton radioButton2;

	private RadioButton radioButton1;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label20;

	private Label label19;

	private Label label18;

	private Label label17;

	private ComboBox comboBox1;

	private Label label21;

	private Panel panel4;

	private Panel panel3;

	private Panel panel2;

	private Panel panel1;

	private ColorDialog colorDialog_0;

	private Button button3;

	private NumericUpDown numericUpDown1;

	private NumericUpDown numericUpDown2;

	private Label label22;

	private CheckBox checkBox19;

	private Label label23;

	public CheckBox ComUse;

	private CheckBox checkBox15;

	private GroupBox groupBox5;

	public CheckBox mivExtend;

	public CheckBox mivFix;

	private CheckBox cTempUpgrate;

	private LclTextBox lclTUser;

	private Label label24;

	private TabPage tabPage6;

	private TextBox tCNewPwd;

	private Label label25;

	private TextBox tNewPwd;

	private Label label26;

	private TextBox tOldPwd;

	private Label label27;

	private Label label29;

	private Label label28;

	private TextBox channel1CustomName;

	private TextBox channel3CustomName;

	private TextBox channel2CustomName;

	private Label label32;

	private Label label31;

	private Label label30;

	private Panel panel5;

	private Label label33;

	private GroupBox groupBox9;

	private Button button35;

	private Label label79;

	public MaskedTextBox localgateway;

	public MaskedTextBox localmask;

	public MaskedTextBox localip;

	private Label label76;

	private Label label77;

	private Label label78;

	public CheckBox upDataComUse;

	private ComboBox comboBox2;

	private Label label34;

	private GroupBox groupBox6;

	private Label label36;

	private Label label35;

	private Label label37;

	private NumericUpDown NudMin;

	private NumericUpDown nUDMax;

	private Label label38;

	private ComboBox comboBox3;

	private static string smethod_0()
	{
		long num = 1L;
		byte[] array = Guid.NewGuid().ToByteArray();
		foreach (byte b in array)
		{
			num *= b + 1;
		}
		return $"{num - DateTime.Now.Ticks:x}";
	}

	public string GetFileLongName(string EquipName, int ChannelIndex, string ChannelName, string filename, int injNo, int vialNo, int SaveIndex)
	{
		string text = FileFodle;
		if (text != "" && !Directory.Exists(text))
		{
			string pathRoot = Path.GetPathRoot(text);
			if (!Directory.Exists(pathRoot))
			{
				if (!Directory.Exists(text))
				{
					text = "D:";
				}
				if (!Directory.Exists(text))
				{
					text = "C:";
				}
				if (!Directory.Exists(text))
				{
					text = "E:";
				}
				if (!Directory.Exists(text))
				{
					text = Application.StartupPath;
				}
			}
			Directory.CreateDirectory(text);
		}
		if (checkBox1.Checked)
		{
			text = text + "\\" + EquipName;
		}
		if (checkBox2.Checked)
		{
			text = text + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd");
		}
		if (checkBox3.Checked)
		{
			if (ChannelName == "AUX")
			{
				ChannelName = "AUX1";
			}
			if (ChannelName == "aux")
			{
				ChannelName = "AUX1";
			}
			text = text + "\\" + ChannelName;
		}
		return text + "\\";
	}

	public FrmMsetup()
	{
		InitializeComponent();
	}

	public void Init(ChromFormInterface formMain_1)
	{
		PathSunAquip = Application.StartupPath + "\\SPara.con";
		formMain_0 = formMain_1;
		LoadFromFile(PathSunAquip);
		Class49.SetColor(0, panel1.BackColor);
		Class49.SetColor(1, panel2.BackColor);
		Class49.SetColor(2, panel4.BackColor);
		Class49.SetColor(3, panel3.BackColor);
		Class49.SetColor(4, panel5.BackColor);
		Class49.bool_1 = checkBox8.Checked;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		FolderDialog folderDialog = new FolderDialog();
		string description = Lang.PS("请选择存储谱图的文件目录", "Please select the storage map file directory ");
		if (folderDialog.DisplayDialog(description) == DialogResult.OK)
		{
			textBox1.Text = folderDialog.Path.ToString();
			FileFodle = folderDialog.Path.ToString();
		}
		else
		{
			textBox1.Text = Lang.PS("你没有选择目录", "You did not select directory ");
			textBox1.Text = FileFodle;
		}
	}

	private void panel1_Click(object sender, EventArgs e)
	{
		if (colorDialog_0.ShowDialog() == DialogResult.OK)
		{
			panel1.BackColor = colorDialog_0.Color;
			Class49.SetColor2(0, panel1.BackColor);
		}
	}

	private void panel2_Click(object sender, EventArgs e)
	{
		if (colorDialog_0.ShowDialog() == DialogResult.OK)
		{
			panel2.BackColor = colorDialog_0.Color;
			Class49.SetColor2(1, panel2.BackColor);
		}
	}

	private void panel4_Click(object sender, EventArgs e)
	{
		if (colorDialog_0.ShowDialog() == DialogResult.OK)
		{
			panel4.BackColor = colorDialog_0.Color;
			Class49.SetColor2(2, panel4.BackColor);
		}
	}

	private void panel3_Click(object sender, EventArgs e)
	{
		if (colorDialog_0.ShowDialog() == DialogResult.OK)
		{
			panel3.BackColor = colorDialog_0.Color;
			Class49.SetColor2(3, panel3.BackColor);
			formMain_0.sampleDisplay.setShowGrid = true;
		}
	}

	private void checkBox8_CheckedChanged(object sender, EventArgs e)
	{
		Class49.bool_1 = checkBox8.Checked;
	}

	public void SavePara()
	{
		SaveToFile(PathSunAquip);
	}

	private void button3_Click(object sender, EventArgs e)
	{
		SaveToFile(PathSunAquip);
		if (!bool_0)
		{
			Close();
			return;
		}
		string u_name = Class49.user_0.u_name;
		string text = tNewPwd.Text.Trim();
		string text2 = tCNewPwd.Text.Trim();
		string oldPwd = tOldPwd.Text.Trim();
		if (text != text2)
		{
			MessageBox.Show(Lang.PS("新密码不一致", "The new password does not match "));
		}
		else if (text2.Length >= 6)
		{
			Logon logon = new Logon();
			if (logon.ChangePwd(u_name, oldPwd, text))
			{
				Close();
				return;
			}
			MessageBox.Show(Lang.PS("原密码错误", "The old password is wrong "));
		}
		else
		{
			MessageBox.Show(Lang.PS("密码长度需要大于6位！", "Password length should be greater than 6!"));
		}
	}

	public bool LoadFromFile(string fileName)
	{
		if (!File.Exists(fileName))
		{
			return false;
		}
		fileName = fileName.ToLower();
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_, out fileStream_, out binaryReader_);
			panel1.BackColor = Color.FromArgb(binaryReader_.ReadInt32());
			panel2.BackColor = Color.FromArgb(binaryReader_.ReadInt32());
			panel4.BackColor = Color.FromArgb(binaryReader_.ReadInt32());
			panel3.BackColor = Color.FromArgb(binaryReader_.ReadInt32());
			checkBox8.Checked = binaryReader_.ReadBoolean();
			checkBox9.Checked = binaryReader_.ReadBoolean();
			checkBox10.Checked = binaryReader_.ReadBoolean();
			numericUpDown1.Value = binaryReader_.ReadInt32();
			Class49.int_8 = (int)numericUpDown1.Value;
			textBox1.Text = binaryReader_.ReadString();
			if (textBox1.Text.Trim() == "")
			{
				textBox1.Text = Application.StartupPath;
			}
			FileFodle = textBox1.Text.Trim();
			checkBox1.Checked = binaryReader_.ReadBoolean();
			checkBox2.Checked = binaryReader_.ReadBoolean();
			checkBox3.Checked = binaryReader_.ReadBoolean();
			checkBox4.Checked = binaryReader_.ReadBoolean();
			checkBox5.Checked = binaryReader_.ReadBoolean();
			checkBox6.Checked = binaryReader_.ReadBoolean();
			checkBox7.Checked = binaryReader_.ReadBoolean();
			checkBox19.Checked = binaryReader_.ReadBoolean();
			lclTUser.Text = binaryReader_.ReadString();
			checkBox9.Checked = binaryReader_.ReadBoolean();
			textBox2.Text = binaryReader_.ReadString();
			checkBox11.Checked = binaryReader_.ReadBoolean();
			checkBox12.Checked = binaryReader_.ReadBoolean();
			checkBox13.Checked = binaryReader_.ReadBoolean();
			checkBox14.Checked = binaryReader_.ReadBoolean();
			checkBox15.Checked = binaryReader_.ReadBoolean();
			checkBox16.Checked = binaryReader_.ReadBoolean();
			checkBox17.Checked = binaryReader_.ReadBoolean();
			checkBox18.Checked = binaryReader_.ReadBoolean();
			textBox3.Text = binaryReader_.ReadString();
			textBox4.Text = binaryReader_.ReadString();
			numericUpDown2.Value = binaryReader_.ReadInt32();
			radioButton1.Checked = binaryReader_.ReadBoolean();
			radioButton2.Checked = binaryReader_.ReadBoolean();
			if (radioButton1.Checked)
			{
				Class49.int_12 = 0;
			}
			if (radioButton2.Checked)
			{
				Class49.int_12 = 1;
			}
			channel1CustomName.Text = binaryReader_.ReadString();
			channel2CustomName.Text = binaryReader_.ReadString();
			channel3CustomName.Text = binaryReader_.ReadString();
			comboBox1.Text = binaryReader_.ReadString();
			ComUse.Checked = binaryReader_.ReadBoolean();
			mivFix.Checked = binaryReader_.ReadBoolean();
			mivExtend.Checked = binaryReader_.ReadBoolean();
			cTempUpgrate.Checked = binaryReader_.ReadBoolean();
			Class49.string_10 = comboBox1.Text.Trim();
			Class49.bool_2 = cTempUpgrate.Checked;
			panel5.BackColor = Color.FromArgb(binaryReader_.ReadInt32());
			Class49.string_12 = binaryReader_.ReadString();
			Class49.string_13 = binaryReader_.ReadString();
			Class49.strSdaDataFileDir = binaryReader_.ReadString();
			comboBox2.Text = binaryReader_.ReadString();
			upDataComUse.Checked = binaryReader_.ReadBoolean();
			Class49.string_11 = comboBox2.Text.Trim();
			NudMin.Value = binaryReader_.ReadInt32();
			nUDMax.Value = binaryReader_.ReadInt32();
			Class49.int_9 = (int)NudMin.Value;
			Class49.int_10 = (int)nUDMax.Value;
			int num = binaryReader_.ReadInt32();
			if (num > 1)
			{
				num = 1;
			}
			if (num < 0)
			{
				num = 0;
			}
			comboBox3.SelectedIndex = num;
			Class49.int_11 = num;
			if (Class49.strSdaDataFileDir == "--End--" || Class49.strSdaDataFileDir == "")
			{
				Class49.strSdaDataFileDir = Application.StartupPath;
			}
		}
		catch (Exception)
		{
			NudMin.Value = 0m;
			nUDMax.Value = 100m;
			Class49.int_9 = (int)NudMin.Value;
			Class49.int_10 = (int)nUDMax.Value;
			comboBox3.SelectedIndex = 1;
			Class49.int_11 = 1;
			switch (Class49.sysLanguage_0)
			{
			case SysLanguage.CN:
				MessageBox.Show("配置文件载入失败");
				break;
			case SysLanguage.EN:
				MessageBox.Show("load error");
				break;
			}
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
		return true;
	}

	public void SaveToFile(string fileName)
	{
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_, out fileStream_, out binaryWriter_);
			binaryWriter_.Write(panel1.BackColor.ToArgb());
			binaryWriter_.Write(panel2.BackColor.ToArgb());
			binaryWriter_.Write(panel4.BackColor.ToArgb());
			binaryWriter_.Write(panel3.BackColor.ToArgb());
			binaryWriter_.Write(checkBox8.Checked);
			binaryWriter_.Write(checkBox9.Checked);
			binaryWriter_.Write(checkBox10.Checked);
			binaryWriter_.Write((int)numericUpDown1.Value);
			Class49.int_8 = (int)numericUpDown1.Value;
			binaryWriter_.Write(FileFodle.Trim());
			binaryWriter_.Write(checkBox1.Checked);
			binaryWriter_.Write(checkBox2.Checked);
			binaryWriter_.Write(checkBox3.Checked);
			binaryWriter_.Write(checkBox4.Checked);
			binaryWriter_.Write(checkBox5.Checked);
			binaryWriter_.Write(checkBox6.Checked);
			binaryWriter_.Write(checkBox7.Checked);
			binaryWriter_.Write(checkBox19.Checked);
			binaryWriter_.Write(lclTUser.Text.Trim());
			binaryWriter_.Write(checkBox9.Checked);
			binaryWriter_.Write(textBox2.Text.Trim());
			binaryWriter_.Write(checkBox11.Checked);
			binaryWriter_.Write(checkBox12.Checked);
			binaryWriter_.Write(checkBox13.Checked);
			binaryWriter_.Write(checkBox14.Checked);
			binaryWriter_.Write(checkBox15.Checked);
			binaryWriter_.Write(checkBox16.Checked);
			binaryWriter_.Write(checkBox17.Checked);
			binaryWriter_.Write(checkBox18.Checked);
			binaryWriter_.Write(textBox3.Text.Trim());
			binaryWriter_.Write(textBox4.Text.Trim());
			binaryWriter_.Write((int)numericUpDown2.Value);
			binaryWriter_.Write(radioButton1.Checked);
			binaryWriter_.Write(radioButton2.Checked);
			if (radioButton1.Checked)
			{
				Class49.int_12 = 0;
			}
			if (radioButton2.Checked)
			{
				Class49.int_12 = 1;
			}
			binaryWriter_.Write(channel1CustomName.Text.Trim());
			binaryWriter_.Write(channel2CustomName.Text.Trim());
			binaryWriter_.Write(channel3CustomName.Text.Trim());
			binaryWriter_.Write(comboBox1.Text.Trim());
			binaryWriter_.Write(ComUse.Checked);
			binaryWriter_.Write(mivFix.Checked);
			binaryWriter_.Write(mivExtend.Checked);
			binaryWriter_.Write(cTempUpgrate.Checked);
			Class49.bool_2 = cTempUpgrate.Checked;
			binaryWriter_.Write(panel5.BackColor.ToArgb());
			binaryWriter_.Write(Class49.string_12);
			binaryWriter_.Write(Class49.string_13);
			binaryWriter_.Write(Class49.strSdaDataFileDir);
			binaryWriter_.Write(comboBox2.Text.Trim());
			binaryWriter_.Write(upDataComUse.Checked);
			Class49.string_11 = comboBox2.Text.Trim();
			binaryWriter_.Write((int)NudMin.Value);
			Class49.int_9 = (int)NudMin.Value;
			binaryWriter_.Write((int)nUDMax.Value);
			Class49.int_10 = (int)nUDMax.Value;
			Class49.int_11 = comboBox3.SelectedIndex;
			binaryWriter_.Write(Class49.int_11);
			binaryWriter_.Write("--End--");
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
			switch (Class49.sysLanguage_0)
			{
			case SysLanguage.CN:
				Text = "保存成功";
				break;
			case SysLanguage.EN:
				Text = "Successfully saved";
				break;
			}
		}
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		Text = Lang.PS("参数设置", "Option");
	}

	private void FrmMsetup_FormClosing(object sender, FormClosingEventArgs e)
	{
		Hide();
		e.Cancel = true;
	}

	private void FrmMsetup_Load(object sender, EventArgs e)
	{
		comboBox1.Items.Clear();
		comboBox2.Items.Clear();
		string[] portNames = SerialPort.GetPortNames();
		foreach (string item in portNames)
		{
			comboBox1.Items.Add(item);
			comboBox2.Items.Add(item);
		}
		LoadLanguage();
		tabPage3.Parent = null;
		tabPage4.Parent = null;
		tabPage6.Parent = null;
	}

	private void LoadLanguage()
	{
		Text = Lang.PS("参数设置", "Option");
		tabControl1.TabPages[0].Text = Lang.PS("显示", "dispaly");
		tabControl1.TabPages[1].Text = Lang.PS("操作", "operation");
		tabControl1.TabPages[2].Text = Lang.PS("打印", "print");
		tabControl1.TabPages[3].Text = Lang.PS("密码", "password");
		tabControl1.TabPages[4].Text = Lang.PS("串口及本机IP设置", "com&ip Set");
		tabControl1.TabPages[5].Text = Lang.PS("修改密码", "change pass");
		button3.Text = Lang.PS("确定", "OK");
		label5.Text = Lang.PS("背景颜色", "background");
		label6.Text = Lang.PS("基线颜色", "baseline");
		label7.Text = Lang.PS("采样颜色", "sample");
		label8.Text = Lang.PS("网格颜色", "grid");
		checkBox8.Text = Lang.PS("同时显示网格线", "show grid line");
		cTempUpgrate.Text = Lang.PS("同时标识程升曲线", "show programmed temp curve");
		checkBox9.Text = Lang.PS("标出峰间分割线", "show peak line");
		checkBox10.Text = Lang.PS("标出保留时间", "show retention time");
		groupBox5.Text = Lang.PS("谱图显示", "spectrogram display");
		mivFix.Text = Lang.PS("超出范围后平移", "out translation");
		mivExtend.Text = Lang.PS("超出范围后缩进", "out indentation ");
		label9.Text = Lang.PS("浓度计算结果显示小数点后位数:", "the number of digits after the decimal point :");
		groupBox1.Text = Lang.PS("目录设置", "dir set");
		label1.Text = Lang.PS("保存起始目录:", "home directory :");
		checkBox1.Text = Lang.PS("增加色谱机名称文件夹:", "add instrument folder");
		checkBox2.Text = Lang.PS("增加日期文件夹", "add date folder");
		checkBox3.Text = Lang.PS("增加通道名称文件夹", "add channel folder");
		groupBox3.Text = Lang.PS("文件命名设置", "file name set");
		checkBox4.Text = Lang.PS("机器名/ID", "instrument name/ID");
		checkBox5.Text = Lang.PS("通道名称", "channel name");
		checkBox6.Text = Lang.PS("时间", "time");
		label28.Text = Lang.PS("通道自定义:", "channel user set:");
		checkBox19.Text = Lang.PS("自动进样", "auto sampling");
		label21.Text = Lang.PS("色谱机串口:", "com");
		ComUse.Text = Lang.PS("使用串口", "use com");
		label23.Text = Lang.PS("*串口号修改需重启程序后生效", "*change com num must restart IBrainChrom");
		label27.Text = Lang.PS("原  密  码:", "old password:");
		label26.Text = Lang.PS("新  密  码:", "new password:");
		label25.Text = Lang.PS("确认新密码:", "conform password:");
		groupBox9.Text = Lang.PS("设定本计算机本地连接IP", "Set local connection for this computer IP");
		label78.Text = Lang.PS("本地  IP:", "IP");
		label77.Text = Lang.PS("子网掩码:", "Mask");
		label76.Text = Lang.PS("网    关:", "Gateway");
		label79.Text = Lang.PS("注:修改本计算机IP为色谱仪默认的工作站IP,如仍无法连接请关闭计算机防火墙", "Note: modify the computer IP as the default workstation IP, if still unable to connect, please turn off the computer firewall");
		label33.Text = Lang.PS("打印谱图颜色:", "PrintLineColor");
		button35.Text = Lang.PS("设定", "Set");
		groupBox6.Text = Lang.PS("DCS上传数据配置", "DCS Upload data configuration ");
		label34.Text = Lang.PS("上传数据串口:", "UploadDataCom");
		upDataComUse.Text = Lang.PS("使用串口", "use com");
		label37.Text = Lang.PS("注:本设置将某一浓度结果转换为电流值上传至DCS。为提高传输精度，用户可对这种转换的对应值进行设置。", "Note: this setting will be converted to a concentration of the current value to DCS. In order to improve the transmission accuracy, the user can set up the corresponding value of the conversion.");
		label38.Text = Lang.PS("TCP ModBus版本:", "TCP ModBus Ver:");
	}

	private void mivFix_CheckedChanged(object sender, EventArgs e)
	{
		mivExtend.Checked = !mivFix.Checked;
	}

	private void mivExtend_CheckedChanged(object sender, EventArgs e)
	{
		mivFix.Checked = !mivExtend.Checked;
	}

	private void tOldPwd_TextChanged(object sender, EventArgs e)
	{
		bool_0 = true;
	}

	private void panel5_Click(object sender, EventArgs e)
	{
		if (colorDialog_0.ShowDialog() == DialogResult.OK)
		{
			panel5.BackColor = colorDialog_0.Color;
			Class49.SetColor2(4, panel5.BackColor);
		}
	}

	private void localip_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void localip_Leave(object sender, EventArgs e)
	{
		if (!IPCheck(((MaskedTextBox)sender).Text))
		{
			((MaskedTextBox)sender).Text = "127.0.0.1";
		}
	}

	public bool IPCheck(string IP)
	{
		string text = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";
		return Regex.IsMatch(IP, "^" + text + "\\." + text + "\\." + text + "\\." + text + "$");
	}

	private void button35_Click(object sender, EventArgs e)
	{
		string iP = localip.Text;
		string mask = localmask.Text;
		string gateway = localgateway.Text;
		string nICname = "本地连接";
		SetupIpAddress(nICname, iP, mask, gateway);
	}

	public string SetupIpAddress(string NICname, string IP, string mask, string gateway)
	{
		NICname = "本地连接";
		Process process = new Process();
		string empty = string.Empty;
		process.StartInfo.FileName = "cmd.exe";
		process.StartInfo.RedirectStandardInput = true;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;
		try
		{
			if (process.Start())
			{
				process.StandardInput.WriteLine("netsh");
				process.StandardInput.WriteLine("interface");
				process.StandardInput.WriteLine("ip");
				string value = "set  address  \"" + NICname + "\"  static " + IP + " " + mask + " " + gateway + " 1";
				process.StandardInput.WriteLine(value);
				process.StandardInput.WriteLine("exit");
				process.StandardInput.WriteLine("exit");
				empty = string.Empty;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		finally
		{
			process?.Close();
			process = null;
		}
		return empty;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FrmMsetup));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.cTempUpgrate = new System.Windows.Forms.CheckBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.mivFix = new System.Windows.Forms.CheckBox();
		this.mivExtend = new System.Windows.Forms.CheckBox();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.panel5 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.checkBox10 = new System.Windows.Forms.CheckBox();
		this.checkBox9 = new System.Windows.Forms.CheckBox();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label32 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.channel3CustomName = new System.Windows.Forms.TextBox();
		this.channel2CustomName = new System.Windows.Forms.TextBox();
		this.channel1CustomName = new System.Windows.Forms.TextBox();
		this.label29 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.checkBox19 = new System.Windows.Forms.CheckBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
		this.checkBox12 = new System.Windows.Forms.CheckBox();
		this.checkBox15 = new System.Windows.Forms.CheckBox();
		this.checkBox14 = new System.Windows.Forms.CheckBox();
		this.checkBox18 = new System.Windows.Forms.CheckBox();
		this.checkBox17 = new System.Windows.Forms.CheckBox();
		this.checkBox16 = new System.Windows.Forms.CheckBox();
		this.checkBox13 = new System.Windows.Forms.CheckBox();
		this.checkBox11 = new System.Windows.Forms.CheckBox();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.comboBox3 = new System.Windows.Forms.ComboBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.nUDMax = new System.Windows.Forms.NumericUpDown();
		this.NudMin = new System.Windows.Forms.NumericUpDown();
		this.label37 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.comboBox2 = new System.Windows.Forms.ComboBox();
		this.label34 = new System.Windows.Forms.Label();
		this.upDataComUse = new System.Windows.Forms.CheckBox();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.button35 = new System.Windows.Forms.Button();
		this.label79 = new System.Windows.Forms.Label();
		this.localgateway = new System.Windows.Forms.MaskedTextBox();
		this.localmask = new System.Windows.Forms.MaskedTextBox();
		this.localip = new System.Windows.Forms.MaskedTextBox();
		this.label76 = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.label78 = new System.Windows.Forms.Label();
		this.ComUse = new System.Windows.Forms.CheckBox();
		this.label23 = new System.Windows.Forms.Label();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label38 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.tCNewPwd = new System.Windows.Forms.TextBox();
		this.label25 = new System.Windows.Forms.Label();
		this.tNewPwd = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.tOldPwd = new System.Windows.Forms.TextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.colorDialog_0 = new System.Windows.Forms.ColorDialog();
		this.button3 = new System.Windows.Forms.Button();
		this.lclTUser = new IBrainChrom2018.LclTextBox();
		this.groupBox1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		this.tabPage1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
		this.tabPage4.SuspendLayout();
		this.tabPage5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nUDMax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NudMin).BeginInit();
		this.groupBox9.SuspendLayout();
		this.tabPage6.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.checkBox3);
		this.groupBox1.Controls.Add(this.checkBox2);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(3, 6);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(374, 101);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "目录设置";
		this.button1.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.button1.Location = new System.Drawing.Point(342, 18);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(31, 32);
		this.button1.TabIndex = 3;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.textBox1.Location = new System.Drawing.Point(95, 14);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(244, 38);
		this.textBox1.TabIndex = 2;
		this.checkBox3.AutoSize = true;
		this.checkBox3.ForeColor = System.Drawing.Color.Black;
		this.checkBox3.Location = new System.Drawing.Point(8, 79);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(132, 16);
		this.checkBox3.TabIndex = 1;
		this.checkBox3.Text = "增加通道名称文件夹";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.ForeColor = System.Drawing.Color.Black;
		this.checkBox2.Location = new System.Drawing.Point(185, 58);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(108, 16);
		this.checkBox2.TabIndex = 1;
		this.checkBox2.Text = "增加日期文件夹";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Checked = true;
		this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox1.ForeColor = System.Drawing.Color.Black;
		this.checkBox1.Location = new System.Drawing.Point(8, 58);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(144, 16);
		this.checkBox1.TabIndex = 1;
		this.checkBox1.Text = "增加色谱机名称文件夹";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Blue;
		this.label1.Location = new System.Drawing.Point(6, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(83, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "保存起始目录:";
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Controls.Add(this.tabPage5);
		this.tabControl1.Controls.Add(this.tabPage6);
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(390, 302);
		this.tabControl1.TabIndex = 2;
		this.tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
		this.tabPage2.Controls.Add(this.cTempUpgrate);
		this.tabPage2.Controls.Add(this.groupBox5);
		this.tabPage2.Controls.Add(this.numericUpDown1);
		this.tabPage2.Controls.Add(this.panel5);
		this.tabPage2.Controls.Add(this.panel4);
		this.tabPage2.Controls.Add(this.panel3);
		this.tabPage2.Controls.Add(this.panel2);
		this.tabPage2.Controls.Add(this.panel1);
		this.tabPage2.Controls.Add(this.checkBox10);
		this.tabPage2.Controls.Add(this.checkBox9);
		this.tabPage2.Controls.Add(this.checkBox8);
		this.tabPage2.Controls.Add(this.label8);
		this.tabPage2.Controls.Add(this.label33);
		this.tabPage2.Controls.Add(this.label9);
		this.tabPage2.Controls.Add(this.label7);
		this.tabPage2.Controls.Add(this.label6);
		this.tabPage2.Controls.Add(this.label5);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(382, 276);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "显示";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.cTempUpgrate.AutoSize = true;
		this.cTempUpgrate.Location = new System.Drawing.Point(182, 119);
		this.cTempUpgrate.Name = "cTempUpgrate";
		this.cTempUpgrate.Size = new System.Drawing.Size(120, 16);
		this.cTempUpgrate.TabIndex = 8;
		this.cTempUpgrate.Text = "同时标识程升曲线";
		this.cTempUpgrate.UseVisualStyleBackColor = true;
		this.groupBox5.Controls.Add(this.mivFix);
		this.groupBox5.Controls.Add(this.mivExtend);
		this.groupBox5.Location = new System.Drawing.Point(8, 169);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(365, 42);
		this.groupBox5.TabIndex = 7;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "谱图显示";
		this.mivFix.AutoSize = true;
		this.mivFix.Checked = true;
		this.mivFix.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mivFix.Location = new System.Drawing.Point(6, 20);
		this.mivFix.Name = "mivFix";
		this.mivFix.Size = new System.Drawing.Size(108, 16);
		this.mivFix.TabIndex = 6;
		this.mivFix.Text = "超出范围后平移";
		this.mivFix.UseVisualStyleBackColor = true;
		this.mivFix.CheckedChanged += new System.EventHandler(mivFix_CheckedChanged);
		this.mivExtend.AutoSize = true;
		this.mivExtend.Location = new System.Drawing.Point(173, 20);
		this.mivExtend.Name = "mivExtend";
		this.mivExtend.Size = new System.Drawing.Size(108, 16);
		this.mivExtend.TabIndex = 6;
		this.mivExtend.Text = "超出范围后缩进";
		this.mivExtend.UseVisualStyleBackColor = true;
		this.mivExtend.CheckedChanged += new System.EventHandler(mivExtend_CheckedChanged);
		this.numericUpDown1.Location = new System.Drawing.Point(201, 224);
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Size = new System.Drawing.Size(37, 21);
		this.numericUpDown1.TabIndex = 5;
		this.numericUpDown1.Value = new decimal(new int[4] { 4, 0, 0, 0 });
		this.panel5.BackColor = System.Drawing.Color.Black;
		this.panel5.Location = new System.Drawing.Point(93, 80);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(81, 24);
		this.panel5.TabIndex = 4;
		this.panel5.Click += new System.EventHandler(panel5_Click);
		this.panel4.BackColor = System.Drawing.Color.Red;
		this.panel4.Location = new System.Drawing.Point(93, 48);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(80, 24);
		this.panel4.TabIndex = 4;
		this.panel4.Click += new System.EventHandler(panel4_Click);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.panel3.Location = new System.Drawing.Point(273, 48);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(80, 24);
		this.panel3.TabIndex = 4;
		this.panel3.Click += new System.EventHandler(panel3_Click);
		this.panel2.BackColor = System.Drawing.Color.Blue;
		this.panel2.Location = new System.Drawing.Point(273, 13);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(80, 24);
		this.panel2.TabIndex = 4;
		this.panel2.Click += new System.EventHandler(panel2_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Location = new System.Drawing.Point(93, 13);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(80, 24);
		this.panel1.TabIndex = 4;
		this.panel1.Click += new System.EventHandler(panel1_Click);
		this.checkBox10.AutoSize = true;
		this.checkBox10.Checked = true;
		this.checkBox10.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox10.ForeColor = System.Drawing.Color.Black;
		this.checkBox10.Location = new System.Drawing.Point(181, 145);
		this.checkBox10.Name = "checkBox10";
		this.checkBox10.Size = new System.Drawing.Size(96, 16);
		this.checkBox10.TabIndex = 2;
		this.checkBox10.Text = "标出保留时间";
		this.checkBox10.UseVisualStyleBackColor = true;
		this.checkBox9.AutoSize = true;
		this.checkBox9.Checked = true;
		this.checkBox9.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox9.ForeColor = System.Drawing.Color.Black;
		this.checkBox9.Location = new System.Drawing.Point(11, 145);
		this.checkBox9.Name = "checkBox9";
		this.checkBox9.Size = new System.Drawing.Size(108, 16);
		this.checkBox9.TabIndex = 2;
		this.checkBox9.Text = "标出峰间分割线";
		this.checkBox9.UseVisualStyleBackColor = true;
		this.checkBox8.AutoSize = true;
		this.checkBox8.Checked = true;
		this.checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox8.ForeColor = System.Drawing.Color.Black;
		this.checkBox8.Location = new System.Drawing.Point(11, 123);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(108, 16);
		this.checkBox8.TabIndex = 2;
		this.checkBox8.Text = "同时显示网格线";
		this.checkBox8.UseVisualStyleBackColor = true;
		this.checkBox8.CheckedChanged += new System.EventHandler(checkBox8_CheckedChanged);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(208, 53);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(59, 12);
		this.label8.TabIndex = 0;
		this.label8.Text = "网格颜色:";
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(10, 89);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(83, 12);
		this.label33.TabIndex = 0;
		this.label33.Text = "打印谱图颜色:";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(9, 226);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(179, 12);
		this.label9.TabIndex = 0;
		this.label9.Text = "浓度计算结果显示小数点后位数:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(9, 53);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(59, 12);
		this.label7.TabIndex = 0;
		this.label7.Text = "采样颜色:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(208, 17);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(59, 12);
		this.label6.TabIndex = 0;
		this.label6.Text = "基线颜色:";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(9, 17);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(59, 12);
		this.label5.TabIndex = 0;
		this.label5.Text = "谱图背景:";
		this.tabPage1.Controls.Add(this.groupBox3);
		this.tabPage1.Controls.Add(this.groupBox1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(382, 276);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "操作";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.groupBox3.Controls.Add(this.label32);
		this.groupBox3.Controls.Add(this.label31);
		this.groupBox3.Controls.Add(this.label30);
		this.groupBox3.Controls.Add(this.channel3CustomName);
		this.groupBox3.Controls.Add(this.channel2CustomName);
		this.groupBox3.Controls.Add(this.channel1CustomName);
		this.groupBox3.Controls.Add(this.label29);
		this.groupBox3.Controls.Add(this.label28);
		this.groupBox3.Controls.Add(this.label22);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.checkBox19);
		this.groupBox3.Controls.Add(this.checkBox6);
		this.groupBox3.Controls.Add(this.checkBox5);
		this.groupBox3.Controls.Add(this.checkBox4);
		this.groupBox3.Location = new System.Drawing.Point(3, 113);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(373, 157);
		this.groupBox3.TabIndex = 0;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "文件命名设置";
		this.groupBox3.Visible = false;
		this.label32.AutoSize = true;
		this.label32.Location = new System.Drawing.Point(88, 116);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(11, 12);
		this.label32.TabIndex = 13;
		this.label32.Text = "3";
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(89, 93);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(11, 12);
		this.label31.TabIndex = 12;
		this.label31.Text = "2";
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(89, 70);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(11, 12);
		this.label30.TabIndex = 11;
		this.label30.Text = "1";
		this.channel3CustomName.Location = new System.Drawing.Point(106, 112);
		this.channel3CustomName.Name = "channel3CustomName";
		this.channel3CustomName.Size = new System.Drawing.Size(107, 21);
		this.channel3CustomName.TabIndex = 9;
		this.channel2CustomName.AcceptsReturn = true;
		this.channel2CustomName.Location = new System.Drawing.Point(106, 89);
		this.channel2CustomName.Name = "channel2CustomName";
		this.channel2CustomName.Size = new System.Drawing.Size(107, 21);
		this.channel2CustomName.TabIndex = 8;
		this.channel1CustomName.Location = new System.Drawing.Point(106, 66);
		this.channel1CustomName.Name = "channel1CustomName";
		this.channel1CustomName.Size = new System.Drawing.Size(107, 21);
		this.channel1CustomName.TabIndex = 7;
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(5, 70);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(11, 12);
		this.label29.TabIndex = 5;
		this.label29.Text = "+";
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(18, 70);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(71, 12);
		this.label28.TabIndex = 3;
		this.label28.Text = "通道自定义:";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(249, 96);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(11, 12);
		this.label22.TabIndex = 1;
		this.label22.Text = "+";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(250, 25);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(11, 12);
		this.label3.TabIndex = 1;
		this.label3.Text = "+";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(110, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(11, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "+";
		this.checkBox19.AutoSize = true;
		this.checkBox19.Checked = true;
		this.checkBox19.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox19.Location = new System.Drawing.Point(291, 94);
		this.checkBox19.Name = "checkBox19";
		this.checkBox19.Size = new System.Drawing.Size(72, 16);
		this.checkBox19.TabIndex = 0;
		this.checkBox19.Text = "自动进样";
		this.checkBox19.UseVisualStyleBackColor = true;
		this.checkBox6.AutoSize = true;
		this.checkBox6.Checked = true;
		this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox6.Enabled = false;
		this.checkBox6.Location = new System.Drawing.Point(291, 24);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(48, 16);
		this.checkBox6.TabIndex = 0;
		this.checkBox6.Text = "时间";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Checked = true;
		this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox5.Location = new System.Drawing.Point(144, 22);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(72, 16);
		this.checkBox5.TabIndex = 0;
		this.checkBox5.Text = "通道名称";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Checked = true;
		this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox4.Enabled = false;
		this.checkBox4.Location = new System.Drawing.Point(8, 21);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(78, 16);
		this.checkBox4.TabIndex = 0;
		this.checkBox4.Text = "机器名/ID";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.tabPage3.Controls.Add(this.groupBox4);
		this.tabPage3.Controls.Add(this.groupBox2);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(382, 276);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "打印";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.groupBox4.Controls.Add(this.radioButton2);
		this.groupBox4.Controls.Add(this.radioButton1);
		this.groupBox4.Location = new System.Drawing.Point(9, 195);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(368, 42);
		this.groupBox4.TabIndex = 2;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "预览方式";
		this.radioButton2.AutoSize = true;
		this.radioButton2.Location = new System.Drawing.Point(107, 20);
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.Size = new System.Drawing.Size(47, 16);
		this.radioButton2.TabIndex = 0;
		this.radioButton2.Text = "Word";
		this.radioButton2.UseVisualStyleBackColor = true;
		this.radioButton1.AutoSize = true;
		this.radioButton1.Checked = true;
		this.radioButton1.Location = new System.Drawing.Point(8, 20);
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.Size = new System.Drawing.Size(59, 16);
		this.radioButton1.TabIndex = 0;
		this.radioButton1.TabStop = true;
		this.radioButton1.Text = "写字板";
		this.radioButton1.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.numericUpDown2);
		this.groupBox2.Controls.Add(this.checkBox12);
		this.groupBox2.Controls.Add(this.checkBox15);
		this.groupBox2.Controls.Add(this.checkBox14);
		this.groupBox2.Controls.Add(this.checkBox18);
		this.groupBox2.Controls.Add(this.checkBox17);
		this.groupBox2.Controls.Add(this.checkBox16);
		this.groupBox2.Controls.Add(this.checkBox13);
		this.groupBox2.Controls.Add(this.checkBox11);
		this.groupBox2.Controls.Add(this.textBox4);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.textBox3);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.textBox2);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Location = new System.Drawing.Point(9, 4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(368, 144);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "打印内容";
		this.numericUpDown2.Location = new System.Drawing.Point(120, 123);
		this.numericUpDown2.Name = "numericUpDown2";
		this.numericUpDown2.Size = new System.Drawing.Size(34, 21);
		this.numericUpDown2.TabIndex = 4;
		this.numericUpDown2.Value = new decimal(new int[4] { 4, 0, 0, 0 });
		this.numericUpDown2.Visible = false;
		this.checkBox12.AutoSize = true;
		this.checkBox12.Checked = true;
		this.checkBox12.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox12.ForeColor = System.Drawing.Color.Black;
		this.checkBox12.Location = new System.Drawing.Point(108, 58);
		this.checkBox12.Name = "checkBox12";
		this.checkBox12.Size = new System.Drawing.Size(72, 16);
		this.checkBox12.TabIndex = 3;
		this.checkBox12.Text = "进样时间";
		this.checkBox12.UseVisualStyleBackColor = true;
		this.checkBox15.AutoSize = true;
		this.checkBox15.Checked = true;
		this.checkBox15.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox15.ForeColor = System.Drawing.Color.Black;
		this.checkBox15.Location = new System.Drawing.Point(108, 80);
		this.checkBox15.Name = "checkBox15";
		this.checkBox15.Size = new System.Drawing.Size(72, 16);
		this.checkBox15.TabIndex = 3;
		this.checkBox15.Text = "结果数据";
		this.checkBox15.UseVisualStyleBackColor = true;
		this.checkBox14.AutoSize = true;
		this.checkBox14.Checked = true;
		this.checkBox14.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox14.ForeColor = System.Drawing.Color.Black;
		this.checkBox14.Location = new System.Drawing.Point(234, 80);
		this.checkBox14.Name = "checkBox14";
		this.checkBox14.Size = new System.Drawing.Size(120, 16);
		this.checkBox14.TabIndex = 3;
		this.checkBox14.Text = "工作曲线原始数据";
		this.checkBox14.UseVisualStyleBackColor = true;
		this.checkBox14.Visible = false;
		this.checkBox18.AutoSize = true;
		this.checkBox18.ForeColor = System.Drawing.Color.Black;
		this.checkBox18.Location = new System.Drawing.Point(8, 124);
		this.checkBox18.Name = "checkBox18";
		this.checkBox18.Size = new System.Drawing.Size(72, 16);
		this.checkBox18.TabIndex = 3;
		this.checkBox18.Text = "谱线加粗";
		this.checkBox18.UseVisualStyleBackColor = true;
		this.checkBox18.Visible = false;
		this.checkBox17.AutoSize = true;
		this.checkBox17.Checked = true;
		this.checkBox17.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox17.ForeColor = System.Drawing.Color.Black;
		this.checkBox17.Location = new System.Drawing.Point(234, 102);
		this.checkBox17.Name = "checkBox17";
		this.checkBox17.Size = new System.Drawing.Size(72, 16);
		this.checkBox17.TabIndex = 3;
		this.checkBox17.Text = "谱图加框";
		this.checkBox17.UseVisualStyleBackColor = true;
		this.checkBox17.Visible = false;
		this.checkBox16.AutoSize = true;
		this.checkBox16.Checked = true;
		this.checkBox16.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox16.ForeColor = System.Drawing.Color.Black;
		this.checkBox16.Location = new System.Drawing.Point(8, 102);
		this.checkBox16.Name = "checkBox16";
		this.checkBox16.Size = new System.Drawing.Size(48, 16);
		this.checkBox16.TabIndex = 3;
		this.checkBox16.Text = "谱图";
		this.checkBox16.UseVisualStyleBackColor = true;
		this.checkBox13.AutoSize = true;
		this.checkBox13.Checked = true;
		this.checkBox13.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox13.ForeColor = System.Drawing.Color.Black;
		this.checkBox13.Location = new System.Drawing.Point(8, 80);
		this.checkBox13.Name = "checkBox13";
		this.checkBox13.Size = new System.Drawing.Size(60, 16);
		this.checkBox13.TabIndex = 3;
		this.checkBox13.Text = "文件名";
		this.checkBox13.UseVisualStyleBackColor = true;
		this.checkBox11.AutoSize = true;
		this.checkBox11.Checked = true;
		this.checkBox11.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox11.ForeColor = System.Drawing.Color.Black;
		this.checkBox11.Location = new System.Drawing.Point(8, 58);
		this.checkBox11.Name = "checkBox11";
		this.checkBox11.Size = new System.Drawing.Size(72, 16);
		this.checkBox11.TabIndex = 3;
		this.checkBox11.Text = "打印时间";
		this.checkBox11.UseVisualStyleBackColor = true;
		this.textBox4.Location = new System.Drawing.Point(172, 100);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(32, 21);
		this.textBox4.TabIndex = 2;
		this.textBox4.Text = "528";
		this.textBox4.Visible = false;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(143, 103);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(23, 12);
		this.label12.TabIndex = 1;
		this.label12.Text = "高:";
		this.label12.Visible = false;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(79, 125);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(35, 12);
		this.label13.TabIndex = 1;
		this.label13.Text = "字号:";
		this.label13.Visible = false;
		this.textBox3.Location = new System.Drawing.Point(102, 100);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(32, 21);
		this.textBox3.TabIndex = 2;
		this.textBox3.Text = "1326";
		this.textBox3.Visible = false;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(73, 103);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(23, 12);
		this.label11.TabIndex = 1;
		this.label11.Text = "宽:";
		this.label11.Visible = false;
		this.textBox2.Font = new System.Drawing.Font("宋体", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.textBox2.Location = new System.Drawing.Point(47, 12);
		this.textBox2.Multiline = true;
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(266, 40);
		this.textBox2.TabIndex = 2;
		this.textBox2.Text = "XXXX分析报告";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(6, 23);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(35, 12);
		this.label10.TabIndex = 1;
		this.label10.Text = "标题:";
		this.tabPage4.Controls.Add(this.label16);
		this.tabPage4.Controls.Add(this.label15);
		this.tabPage4.Controls.Add(this.label20);
		this.tabPage4.Controls.Add(this.label19);
		this.tabPage4.Controls.Add(this.label18);
		this.tabPage4.Controls.Add(this.label17);
		this.tabPage4.Controls.Add(this.label14);
		this.tabPage4.Location = new System.Drawing.Point(4, 22);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Size = new System.Drawing.Size(382, 276);
		this.tabPage4.TabIndex = 3;
		this.tabPage4.Text = "密码";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(16, 121);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(71, 12);
		this.label16.TabIndex = 3;
		this.label16.Text = "访问员密码:";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(16, 73);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(71, 12);
		this.label15.TabIndex = 3;
		this.label15.Text = "分析员密码:";
		this.label20.ForeColor = System.Drawing.Color.Blue;
		this.label20.Location = new System.Drawing.Point(16, 168);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(361, 37);
		this.label20.TabIndex = 3;
		this.label20.Text = "*注:可不填密码，如不填启动时默认为管理员权限";
		this.label19.Location = new System.Drawing.Point(225, 118);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(157, 37);
		this.label19.TabIndex = 3;
		this.label19.Text = "谱图打印权限";
		this.label18.Location = new System.Drawing.Point(225, 70);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(157, 37);
		this.label18.TabIndex = 3;
		this.label18.Text = "谱图采集分析、建立方法、编辑组份表\r\n谱图保存、谱图打印权限";
		this.label17.Location = new System.Drawing.Point(225, 11);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(157, 59);
		this.label17.TabIndex = 3;
		this.label17.Text = "创建用户、修改用户密码、谱图采集分析、建立方法、编辑组份表、谱图保存、谱图打印权限";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(16, 30);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(71, 12);
		this.label14.TabIndex = 3;
		this.label14.Text = "管理员密码:";
		this.tabPage5.Controls.Add(this.comboBox3);
		this.tabPage5.Controls.Add(this.groupBox6);
		this.tabPage5.Controls.Add(this.groupBox9);
		this.tabPage5.Controls.Add(this.ComUse);
		this.tabPage5.Controls.Add(this.label23);
		this.tabPage5.Controls.Add(this.comboBox1);
		this.tabPage5.Controls.Add(this.label38);
		this.tabPage5.Controls.Add(this.label21);
		this.tabPage5.Location = new System.Drawing.Point(4, 22);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Size = new System.Drawing.Size(382, 276);
		this.tabPage5.TabIndex = 4;
		this.tabPage5.Text = "串口及本机IP设置";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.comboBox3.FormattingEnabled = true;
		this.comboBox3.Items.AddRange(new object[2] { "V1.0", "V2.0" });
		this.comboBox3.Location = new System.Drawing.Point(322, 17);
		this.comboBox3.Name = "comboBox3";
		this.comboBox3.Size = new System.Drawing.Size(51, 20);
		this.comboBox3.TabIndex = 11;
		this.groupBox6.Controls.Add(this.nUDMax);
		this.groupBox6.Controls.Add(this.NudMin);
		this.groupBox6.Controls.Add(this.label37);
		this.groupBox6.Controls.Add(this.label36);
		this.groupBox6.Controls.Add(this.label35);
		this.groupBox6.Controls.Add(this.comboBox2);
		this.groupBox6.Controls.Add(this.label34);
		this.groupBox6.Controls.Add(this.upDataComUse);
		this.groupBox6.Location = new System.Drawing.Point(10, 46);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(363, 118);
		this.groupBox6.TabIndex = 10;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "DCS上传数据配置";
		this.nUDMax.Location = new System.Drawing.Point(195, 45);
		this.nUDMax.Maximum = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.nUDMax.Name = "nUDMax";
		this.nUDMax.Size = new System.Drawing.Size(71, 21);
		this.nUDMax.TabIndex = 12;
		this.nUDMax.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.NudMin.Location = new System.Drawing.Point(88, 46);
		this.NudMin.Maximum = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.NudMin.Name = "NudMin";
		this.NudMin.Size = new System.Drawing.Size(60, 21);
		this.NudMin.TabIndex = 12;
		this.label37.ForeColor = System.Drawing.SystemColors.WindowText;
		this.label37.Location = new System.Drawing.Point(6, 69);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(351, 50);
		this.label37.TabIndex = 3;
		this.label37.Text = "注:本设置将某一浓度结果转换为电流值上传至DCS。为提高传输精度，用户可对这种转换的对应值进行设置。";
		this.label36.AutoSize = true;
		this.label36.Location = new System.Drawing.Point(154, 50);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(35, 12);
		this.label36.TabIndex = 9;
		this.label36.Text = "20mA=";
		this.label35.AutoSize = true;
		this.label35.Location = new System.Drawing.Point(55, 50);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(29, 12);
		this.label35.TabIndex = 9;
		this.label35.Text = "4mA=";
		this.comboBox2.FormattingEnabled = true;
		this.comboBox2.Items.AddRange(new object[10] { "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "com10" });
		this.comboBox2.Location = new System.Drawing.Point(90, 20);
		this.comboBox2.Name = "comboBox2";
		this.comboBox2.Size = new System.Drawing.Size(121, 20);
		this.comboBox2.TabIndex = 6;
		this.label34.AutoSize = true;
		this.label34.Location = new System.Drawing.Point(4, 23);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(83, 12);
		this.label34.TabIndex = 5;
		this.label34.Text = "上传数据串口:";
		this.upDataComUse.AutoSize = true;
		this.upDataComUse.Location = new System.Drawing.Point(216, 23);
		this.upDataComUse.Name = "upDataComUse";
		this.upDataComUse.Size = new System.Drawing.Size(72, 16);
		this.upDataComUse.TabIndex = 8;
		this.upDataComUse.Text = "使用串口";
		this.upDataComUse.UseVisualStyleBackColor = true;
		this.groupBox9.Controls.Add(this.button35);
		this.groupBox9.Controls.Add(this.label79);
		this.groupBox9.Controls.Add(this.localgateway);
		this.groupBox9.Controls.Add(this.localmask);
		this.groupBox9.Controls.Add(this.localip);
		this.groupBox9.Controls.Add(this.label76);
		this.groupBox9.Controls.Add(this.label77);
		this.groupBox9.Controls.Add(this.label78);
		this.groupBox9.Location = new System.Drawing.Point(10, 170);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(363, 96);
		this.groupBox9.TabIndex = 9;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "设定本计算机本地连接IP";
		this.button35.ForeColor = System.Drawing.SystemColors.Desktop;
		this.button35.Location = new System.Drawing.Point(201, 66);
		this.button35.Name = "button35";
		this.button35.Size = new System.Drawing.Size(75, 23);
		this.button35.TabIndex = 2;
		this.button35.Text = "设定";
		this.button35.UseVisualStyleBackColor = true;
		this.button35.Click += new System.EventHandler(button35_Click);
		this.label79.ForeColor = System.Drawing.SystemColors.WindowText;
		this.label79.Location = new System.Drawing.Point(193, 14);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(164, 59);
		this.label79.TabIndex = 3;
		this.label79.Text = "注:修改本计算机IP为色谱仪默认的工作站IP,如仍无法连接请关闭计算机防火墙";
		this.localgateway.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.localgateway.ForeColor = System.Drawing.SystemColors.Desktop;
		this.localgateway.Location = new System.Drawing.Point(65, 58);
		this.localgateway.Name = "localgateway";
		this.localgateway.Size = new System.Drawing.Size(122, 21);
		this.localgateway.TabIndex = 1;
		this.localgateway.Text = "192.168.18.1";
		this.localgateway.Enter += new System.EventHandler(localip_Enter);
		this.localgateway.Leave += new System.EventHandler(localip_Leave);
		this.localmask.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.localmask.ForeColor = System.Drawing.SystemColors.Desktop;
		this.localmask.Location = new System.Drawing.Point(65, 36);
		this.localmask.Name = "localmask";
		this.localmask.Size = new System.Drawing.Size(122, 21);
		this.localmask.TabIndex = 1;
		this.localmask.Text = "255.255.255.0";
		this.localmask.Enter += new System.EventHandler(localip_Enter);
		this.localmask.Leave += new System.EventHandler(localip_Leave);
		this.localip.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.localip.ForeColor = System.Drawing.SystemColors.Desktop;
		this.localip.Location = new System.Drawing.Point(65, 16);
		this.localip.Name = "localip";
		this.localip.Size = new System.Drawing.Size(122, 21);
		this.localip.TabIndex = 1;
		this.localip.Text = "192.168.18.200";
		this.localip.Enter += new System.EventHandler(localip_Enter);
		this.localip.Leave += new System.EventHandler(localip_Leave);
		this.label76.AutoSize = true;
		this.label76.ForeColor = System.Drawing.SystemColors.Desktop;
		this.label76.Location = new System.Drawing.Point(6, 61);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(59, 12);
		this.label76.TabIndex = 0;
		this.label76.Text = "网    关:";
		this.label77.AutoSize = true;
		this.label77.ForeColor = System.Drawing.SystemColors.Desktop;
		this.label77.Location = new System.Drawing.Point(6, 39);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(59, 12);
		this.label77.TabIndex = 0;
		this.label77.Text = "子网掩码:";
		this.label78.AutoSize = true;
		this.label78.ForeColor = System.Drawing.SystemColors.Desktop;
		this.label78.Location = new System.Drawing.Point(6, 19);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(59, 12);
		this.label78.TabIndex = 0;
		this.label78.Text = "本地  IP:";
		this.ComUse.AutoSize = true;
		this.ComUse.Location = new System.Drawing.Point(161, 19);
		this.ComUse.Name = "ComUse";
		this.ComUse.Size = new System.Drawing.Size(72, 16);
		this.ComUse.TabIndex = 8;
		this.ComUse.Text = "使用串口";
		this.ComUse.UseVisualStyleBackColor = true;
		this.label23.AutoSize = true;
		this.label23.ForeColor = System.Drawing.Color.Blue;
		this.label23.Location = new System.Drawing.Point(8, 126);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(167, 12);
		this.label23.TabIndex = 7;
		this.label23.Text = "*串口号修改需重启程序后生效";
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[10] { "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "com10" });
		this.comboBox1.Location = new System.Drawing.Point(76, 17);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(79, 20);
		this.comboBox1.TabIndex = 6;
		this.label38.AutoSize = true;
		this.label38.Location = new System.Drawing.Point(232, 20);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(95, 12);
		this.label38.TabIndex = 5;
		this.label38.Text = "TCP ModBus版本:";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(5, 20);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(71, 12);
		this.label21.TabIndex = 5;
		this.label21.Text = "色谱机串口:";
		this.tabPage6.Controls.Add(this.tCNewPwd);
		this.tabPage6.Controls.Add(this.label25);
		this.tabPage6.Controls.Add(this.tNewPwd);
		this.tabPage6.Controls.Add(this.label26);
		this.tabPage6.Controls.Add(this.tOldPwd);
		this.tabPage6.Controls.Add(this.label27);
		this.tabPage6.Location = new System.Drawing.Point(4, 22);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(382, 276);
		this.tabPage6.TabIndex = 5;
		this.tabPage6.Text = "修改密码";
		this.tabPage6.UseVisualStyleBackColor = true;
		this.tCNewPwd.Location = new System.Drawing.Point(124, 115);
		this.tCNewPwd.Name = "tCNewPwd";
		this.tCNewPwd.PasswordChar = '*';
		this.tCNewPwd.Size = new System.Drawing.Size(124, 21);
		this.tCNewPwd.TabIndex = 8;
		this.tCNewPwd.TextChanged += new System.EventHandler(tOldPwd_TextChanged);
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(45, 118);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(71, 12);
		this.label25.TabIndex = 6;
		this.label25.Text = "确认新密码:";
		this.tNewPwd.Location = new System.Drawing.Point(124, 67);
		this.tNewPwd.Name = "tNewPwd";
		this.tNewPwd.PasswordChar = '*';
		this.tNewPwd.Size = new System.Drawing.Size(124, 21);
		this.tNewPwd.TabIndex = 10;
		this.tNewPwd.TextChanged += new System.EventHandler(tOldPwd_TextChanged);
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(45, 70);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(71, 12);
		this.label26.TabIndex = 5;
		this.label26.Text = "新  密  码:";
		this.tOldPwd.Location = new System.Drawing.Point(124, 24);
		this.tOldPwd.Name = "tOldPwd";
		this.tOldPwd.PasswordChar = '*';
		this.tOldPwd.Size = new System.Drawing.Size(124, 21);
		this.tOldPwd.TabIndex = 9;
		this.tOldPwd.TextChanged += new System.EventHandler(tOldPwd_TextChanged);
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(45, 27);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(71, 12);
		this.label27.TabIndex = 7;
		this.label27.Text = "原  密  码:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(180, 314);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(11, 12);
		this.label4.TabIndex = 1;
		this.label4.Text = "+";
		this.label4.Visible = false;
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(9, 313);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(53, 12);
		this.label24.TabIndex = 1;
		this.label24.Text = "+自定义:";
		this.label24.Visible = false;
		this.checkBox7.AutoSize = true;
		this.checkBox7.Location = new System.Drawing.Point(192, 313);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(60, 16);
		this.checkBox7.TabIndex = 0;
		this.checkBox7.Text = "序列号";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox7.Visible = false;
		this.colorDialog_0.AnyColor = true;
		this.colorDialog_0.FullOpen = true;
		this.button3.Location = new System.Drawing.Point(302, 306);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 23);
		this.button3.TabIndex = 3;
		this.button3.Text = "确定";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.lclTUser.Location = new System.Drawing.Point(68, 308);
		this.lclTUser.Name = "lclTUser";
		this.lclTUser.Size = new System.Drawing.Size(91, 21);
		this.lclTUser.TabIndex = 2;
		this.lclTUser.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(387, 331);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.lclTUser);
		base.Controls.Add(this.label24);
		base.Controls.Add(this.checkBox7);
		base.Controls.Add(this.label4);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmMsetup";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "参数设置";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmMsetup_FormClosing);
		base.Load += new System.EventHandler(FrmMsetup_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		this.tabPage1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
		this.tabPage4.ResumeLayout(false);
		this.tabPage4.PerformLayout();
		this.tabPage5.ResumeLayout(false);
		this.tabPage5.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nUDMax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NudMin).EndInit();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		this.tabPage6.ResumeLayout(false);
		this.tabPage6.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
