using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HZH_Controls;
using HZH_Controls.Controls;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Properties;

namespace IBrainChrom2018;

public class splitSCtrl : UserControl
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private splitParam splitparam = splitParam.Create();

	public List<PortableGridModel> lstSource1 = new List<PortableGridModel>();

	private static MisMgrAssist myself = null;

	public bool m_bLoading = true;

	public bool bSaveHe = false;

	public static splitSCtrl self;

	public ushort u16Step = 0;

	public uint u32CntSecond = 0u;

	public uint u32CntSecondWait = 0u;

	public bool bClean = false;

	public bool bWait = false;

	public bool bStart = false;

	public bool bSendStop = false;

	public bool bSendCMDAn = false;

	public bool bResetTemp = false;

	public int iTempChannel = 0;

	private IContainer components = null;

	private Button btnDownload;

	private Label label76;

	private Button MethodReSave;

	private Button MethodSave;

	private Button MethodOpen;

	public TextBox tbMethName;

	private Label label1;

	private Label label2;

	private UCSwitch uSAutoMode;

	private DateTimePicker dateTimePicker2;

	private DateTimePicker dateTimePicker1;

	private Timer timer1;

	public UCSwitch uSHe;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private UCBtnExt uBtnStart;

	private UCCombox ucCbInject;

	private Label label3;

	private Label label5;

	private Label label4;

	private Label label7;

	private Label label6;

	private UCTextBoxEx ucTBTempCur;

	private UCTextBoxEx ucTBTimeHold;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Label label8;

	private Label label9;

	private Label label10;

	private UCTextBoxEx ucTBInjTime;

	private UCTextBoxEx ucTBWaitTime;

	private UCTextBoxEx ucTBCleanTime;

	private Label label19;

	private Label label11;

	private UCTextBoxEx ucTBWeight;

	private Label label16;

	private Label label17;

	private Label label18;

	private Label label12;

	private Label label13;

	private Label label14;

	private Label label15;

	public UCTextBoxEx ucTBTemp2;

	public UCTextBoxEx ucTBTemp1;

	public string strTemp1 => ucTBTemp1.InputText.Trim();

	public string strTemp2 => ucTBTemp2.InputText.Trim();

	public string strCurTemp
	{
		set
		{
			ucTBTempCur.InputText = value;
		}
	}

	public splitSCtrl()
	{
		self = this;
		InitializeComponent();
		initForm();
		m_bLoading = false;
	}

	public void initForm()
	{
		tabPage2.Parent = null;
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		list.Add(new KeyValuePair<string, string>(0.ToString(), "热脱附"));
		ucCbInject.Source = list;
		ucCbInject.SelectedIndex = 0;
		ucTBTemp1.IsShowKeyboard = true;
		ucTBTemp2.IsShowKeyboard = true;
		ucTBTimeHold.IsShowKeyboard = true;
		ucTBInjTime.IsShowKeyboard = true;
		ucTBCleanTime.IsShowKeyboard = true;
		ucTBWaitTime.IsShowKeyboard = true;
		ucTBWeight.IsShowKeyboard = true;
		ucTBTemp1.InputText = splitparam.fTemp1.ToString();
		ucTBTemp2.InputText = splitparam.fTemp2.ToString();
		ucTBTimeHold.InputText = splitparam.fHoldTime.ToString();
		ucTBInjTime.InputText = splitparam.fInjTime.ToString();
		ucTBCleanTime.InputText = splitparam.fCleanTime.ToString();
		ucTBWaitTime.InputText = splitparam.fWaitTime.ToString();
		ucTBWeight.InputText = splitparam.fWeight.ToString();
		ucTBTemp1.txtInput.KeyDown += ucTBTemp1Input;
		ucTBTemp2.txtInput.KeyDown += ucTBTemp2Input;
		ucTBTimeHold.txtInput.KeyDown += ucTBTimeHoldInput;
		ucTBInjTime.txtInput.KeyDown += ucTBInjTimeInput;
		ucTBCleanTime.txtInput.KeyDown += ucTBCleanTimeInput;
		ucTBWaitTime.txtInput.KeyDown += ucTBWaitTimeInput;
		ucTBWeight.txtInput.KeyDown += ucTBWeightInput;
	}

	public void ucTBTemp1Input(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBTemp1.InputText, out splitparam.fTemp1);
			splitparam.SaveParam();
		}
	}

	public void ucTBTemp2Input(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBTemp2.InputText, out splitparam.fTemp2);
			splitparam.SaveParam();
		}
	}

	public void ucTBTimeHoldInput(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBTimeHold.InputText, out splitparam.fHoldTime);
			splitparam.SaveParam();
		}
	}

	public void ucTBInjTimeInput(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBInjTime.InputText, out splitparam.fInjTime);
			splitparam.SaveParam();
		}
	}

	public void ucTBCleanTimeInput(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBCleanTime.InputText, out splitparam.fCleanTime);
			splitparam.SaveParam();
		}
	}

	public void ucTBWaitTimeInput(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBWaitTime.InputText, out splitparam.fWaitTime);
			splitparam.SaveParam();
		}
	}

	public void ucTBWeightInput(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(ucTBWeight.InputText, out splitparam.fWeight);
			splitparam.SaveParam();
		}
	}

	public MisMgr MakeMisData()
	{
		ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
		if (chromDeviceListMgr.CurrentChromDevice == null)
		{
			return null;
		}
		chromDeviceListMgr.formMain.UpdateMisMgr();
		return chromDeviceListMgr.CurrentChromDevice.misMgr;
	}

	private void MethodNew_Click(object sender, EventArgs e)
	{
	}

	private void uSHe_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			InsDeviceCtrl.self.checkBox_6.Checked = uSHe.Checked;
			InsDeviceCtrl.self.button33_Click(null, null);
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (bSendCMDAn)
		{
			bSendCMDAn = false;
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				cdlMgr.currentTcpServerMgrSendCmd(18);
				bStart = true;
			}
		}
		if (bResetTemp)
		{
			bResetTemp = false;
			iTempChannel = 0;
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				cdlMgr.currentTcpServerMgrSendCmd(8);
			}
		}
		if (bStart)
		{
			u32CntSecond++;
			if (u16Step == 3)
			{
				u32CntSecondWait++;
			}
			autoStep();
		}
		else
		{
			upColor(8);
		}
	}

	public void autoStep()
	{
		switch (u16Step)
		{
		case 0:
			if ((float)u32CntSecond >= splitparam.fHoldTime)
			{
				u16Step = 1;
				u32CntSecond = 0u;
				eventSet(0, bValue: true);
			}
			break;
		case 1:
			if ((float)u32CntSecond >= splitparam.fHoldTime)
			{
				u16Step = 2;
				u32CntSecond = 0u;
				eventSet(0, bValue: false);
				bResetTemp = true;
			}
			break;
		case 2:
			if (u32CntSecond >= 2)
			{
				u16Step = 3;
				u32CntSecond = 0u;
				u32CntSecondWait = 0u;
				eventSet(8, bValue: true);
				bClean = true;
				bWait = true;
			}
			break;
		case 3:
			if (bClean && (float)u32CntSecond >= splitparam.fCleanTime)
			{
				u32CntSecond = 0u;
				bClean = false;
				eventSet(1, bValue: false);
			}
			if (bWait && (float)u32CntSecondWait >= splitparam.fWaitTime)
			{
				u32CntSecondWait = 0u;
				bWait = false;
				eventSet(2, bValue: false);
			}
			if (!bWait && !bClean)
			{
				bStart = false;
				uBtnStart.BtnText = "开始";
				u16Step = 0;
			}
			break;
		}
	}

	public void eventSet(int iEvent, bool bValue)
	{
		switch (iEvent)
		{
		case 0:
			InsDeviceCtrl.self.checkBox_0.Checked = bValue;
			InsDeviceCtrl.self.button33_Click(null, null);
			return;
		case 1:
			InsDeviceCtrl.self.checkBox_1.Checked = bValue;
			InsDeviceCtrl.self.button33_Click(null, null);
			return;
		case 2:
			InsDeviceCtrl.self.checkBox_3.Checked = bValue;
			InsDeviceCtrl.self.button33_Click(null, null);
			return;
		case 3:
			return;
		case 4:
			return;
		case 5:
			return;
		case 6:
			return;
		case 7:
			return;
		case 8:
			InsDeviceCtrl.self.checkBox_1.Checked = bValue;
			InsDeviceCtrl.self.checkBox_3.Checked = bValue;
			InsDeviceCtrl.self.button33_Click(null, null);
			return;
		}
		InsDeviceCtrl.self.checkBox_0.Checked = false;
		InsDeviceCtrl.self.checkBox_1.Checked = false;
		InsDeviceCtrl.self.checkBox_3.Checked = false;
		InsDeviceCtrl.self.checkBox_5.Checked = false;
		InsDeviceCtrl.self.checkBox_2.Checked = false;
		InsDeviceCtrl.self.checkBox_4.Checked = false;
		InsDeviceCtrl.self.checkBox_6.Checked = false;
		InsDeviceCtrl.self.checkBox_7.Checked = false;
		InsDeviceCtrl.self.button33_Click(null, null);
	}

	private void uBtnStart_BtnClick(object sender, EventArgs e)
	{
		if (uBtnStart.BtnText == "开始")
		{
			u32CntSecond = 0u;
			iTempChannel = 1;
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				cdlMgr.currentTcpServerMgrSendCmd(8);
				bSendCMDAn = true;
				uBtnStart.BtnText = "结束";
				u16Step = 0;
				setMtep(u16Step);
			}
			else
			{
				MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
			}
		}
		else if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(19);
			bStart = false;
			u32CntSecond = 0u;
			uBtnStart.BtnText = "开始";
			bSendStop = true;
			bResetTemp = true;
			u16Step = 0;
			setMtep(8);
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	public void upColor(int iStep)
	{
		switch (iStep)
		{
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			break;
		case 7:
			break;
		case 8:
			break;
		}
	}

	public void setMtep(int iStep)
	{
		switch (iStep)
		{
		case 0:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 1:
			InsDeviceCtrl.self.checkBox_0.Checked = true;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 2:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 3:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 4:
			InsDeviceCtrl.self.checkBox_0.Checked = true;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = true;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 5:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 6:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = true;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 7:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = true;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = true;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		default:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
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
		this.btnDownload = new System.Windows.Forms.Button();
		this.label76 = new System.Windows.Forms.Label();
		this.MethodReSave = new System.Windows.Forms.Button();
		this.MethodSave = new System.Windows.Forms.Button();
		this.MethodOpen = new System.Windows.Forms.Button();
		this.tbMethName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.uSAutoMode = new HZH_Controls.Controls.UCSwitch();
		this.uSHe = new HZH_Controls.Controls.UCSwitch();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.label19 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.ucTBWeight = new HZH_Controls.Controls.UCTextBoxEx();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.ucTBInjTime = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTBWaitTime = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTBCleanTime = new HZH_Controls.Controls.UCTextBoxEx();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.ucTBTemp1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.label6 = new System.Windows.Forms.Label();
		this.ucTBTemp2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTBTempCur = new HZH_Controls.Controls.UCTextBoxEx();
		this.label5 = new System.Windows.Forms.Label();
		this.ucTBTimeHold = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucCbInject = new HZH_Controls.Controls.UCCombox();
		this.label3 = new System.Windows.Forms.Label();
		this.uBtnStart = new HZH_Controls.Controls.UCBtnExt();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tabControl1.SuspendLayout();
		this.ucTBWeight.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		base.SuspendLayout();
		this.btnDownload.Location = new System.Drawing.Point(301, 55);
		this.btnDownload.Name = "btnDownload";
		this.btnDownload.Size = new System.Drawing.Size(103, 54);
		this.btnDownload.TabIndex = 46;
		this.btnDownload.Text = "下载到仪器";
		this.btnDownload.UseVisualStyleBackColor = true;
		this.label76.AutoSize = true;
		this.label76.Location = new System.Drawing.Point(56, 61);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(84, 20);
		this.label76.TabIndex = 45;
		this.label76.Text = "参数方法：";
		this.MethodReSave.Location = new System.Drawing.Point(216, 86);
		this.MethodReSave.Name = "MethodReSave";
		this.MethodReSave.Size = new System.Drawing.Size(70, 23);
		this.MethodReSave.TabIndex = 42;
		this.MethodReSave.Text = "另存";
		this.MethodReSave.UseVisualStyleBackColor = true;
		this.MethodSave.Location = new System.Drawing.Point(127, 86);
		this.MethodSave.Name = "MethodSave";
		this.MethodSave.Size = new System.Drawing.Size(75, 23);
		this.MethodSave.TabIndex = 43;
		this.MethodSave.Text = "保存";
		this.MethodSave.UseVisualStyleBackColor = true;
		this.MethodOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen.Location = new System.Drawing.Point(255, 55);
		this.MethodOpen.Name = "MethodOpen";
		this.MethodOpen.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen.TabIndex = 41;
		this.MethodOpen.UseVisualStyleBackColor = true;
		this.tbMethName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName.Location = new System.Drawing.Point(127, 59);
		this.tbMethName.Name = "tbMethName";
		this.tbMethName.ReadOnly = true;
		this.tbMethName.Size = new System.Drawing.Size(117, 27);
		this.tbMethName.TabIndex = 40;
		this.tbMethName.Text = "默认";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.Location = new System.Drawing.Point(413, 64);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 16);
		this.label1.TabIndex = 49;
		this.label1.Text = "省气开始";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.Location = new System.Drawing.Point(413, 93);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(72, 16);
		this.label2.TabIndex = 50;
		this.label2.Text = "省气结束";
		this.uSAutoMode.BackColor = System.Drawing.Color.Transparent;
		this.uSAutoMode.Checked = false;
		this.uSAutoMode.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSAutoMode.FalseTextColr = System.Drawing.Color.White;
		this.uSAutoMode.Location = new System.Drawing.Point(416, 135);
		this.uSAutoMode.Name = "uSAutoMode";
		this.uSAutoMode.Size = new System.Drawing.Size(81, 31);
		this.uSAutoMode.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSAutoMode.TabIndex = 52;
		this.uSAutoMode.Texts = new string[2] { "自动", "手动" };
		this.uSAutoMode.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSAutoMode.TrueTextColr = System.Drawing.Color.White;
		this.uSHe.BackColor = System.Drawing.Color.Transparent;
		this.uSHe.Checked = false;
		this.uSHe.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSHe.FalseTextColr = System.Drawing.Color.White;
		this.uSHe.Location = new System.Drawing.Point(589, 135);
		this.uSHe.Name = "uSHe";
		this.uSHe.Size = new System.Drawing.Size(81, 31);
		this.uSHe.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSHe.TabIndex = 51;
		this.uSHe.Texts = new string[2] { "断", "通" };
		this.uSHe.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSHe.TrueTextColr = System.Drawing.Color.White;
		this.uSHe.CheckedChanged += new System.EventHandler(uSHe_CheckedChanged);
		this.dateTimePicker2.CustomFormat = "HH:mm";
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker2.Location = new System.Drawing.Point(491, 89);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.ShowUpDown = true;
		this.dateTimePicker2.Size = new System.Drawing.Size(151, 27);
		this.dateTimePicker2.TabIndex = 54;
		this.dateTimePicker1.CustomFormat = "HH:mm";
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker1.Location = new System.Drawing.Point(491, 59);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.ShowUpDown = true;
		this.dateTimePicker1.Size = new System.Drawing.Size(151, 27);
		this.dateTimePicker1.TabIndex = 55;
		this.dateTimePicker1.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(205, 140);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(10, 34);
		this.tabControl1.TabIndex = 56;
		this.tabPage1.AutoScroll = true;
		this.tabPage1.BackColor = System.Drawing.Color.White;
		this.tabPage1.Location = new System.Drawing.Point(4, 29);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(2, 1);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "模式参数";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label19.Location = new System.Drawing.Point(259, 424);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(24, 16);
		this.label19.TabIndex = 40;
		this.label19.Text = "mg";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label11.Location = new System.Drawing.Point(19, 424);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(72, 16);
		this.label11.TabIndex = 39;
		this.label11.Text = "样品重量";
		this.ucTBWeight.BackColor = System.Drawing.Color.Transparent;
		this.ucTBWeight.ConerRadius = 5;
		this.ucTBWeight.Controls.Add(this.tabControl1);
		this.ucTBWeight.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBWeight.DecLength = 2;
		this.ucTBWeight.FillColor = System.Drawing.Color.Empty;
		this.ucTBWeight.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBWeight.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBWeight.InputText = "";
		this.ucTBWeight.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBWeight.IsFocusColor = true;
		this.ucTBWeight.IsRadius = true;
		this.ucTBWeight.IsShowClearBtn = true;
		this.ucTBWeight.IsShowKeyboard = true;
		this.ucTBWeight.IsShowRect = true;
		this.ucTBWeight.IsShowSearchBtn = false;
		this.ucTBWeight.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBWeight.Location = new System.Drawing.Point(107, 416);
		this.ucTBWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBWeight.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBWeight.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBWeight.Name = "ucTBWeight";
		this.ucTBWeight.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBWeight.PasswordChar = '\0';
		this.ucTBWeight.PromptColor = System.Drawing.Color.Gray;
		this.ucTBWeight.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBWeight.PromptText = "";
		this.ucTBWeight.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBWeight.RectWidth = 1;
		this.ucTBWeight.RegexPattern = "";
		this.ucTBWeight.Size = new System.Drawing.Size(145, 32);
		this.ucTBWeight.TabIndex = 38;
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.label17);
		this.groupBox2.Controls.Add(this.label18);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.ucTBInjTime);
		this.groupBox2.Controls.Add(this.ucTBWaitTime);
		this.groupBox2.Controls.Add(this.ucTBCleanTime);
		this.groupBox2.Location = new System.Drawing.Point(6, 256);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(299, 147);
		this.groupBox2.TabIndex = 35;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "流程控制";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label16.Location = new System.Drawing.Point(252, 110);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(16, 16);
		this.label16.TabIndex = 40;
		this.label16.Text = "S";
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label17.Location = new System.Drawing.Point(252, 65);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(16, 16);
		this.label17.TabIndex = 39;
		this.label17.Text = "S";
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label18.Location = new System.Drawing.Point(252, 20);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(16, 16);
		this.label18.TabIndex = 38;
		this.label18.Text = "S";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label8.Location = new System.Drawing.Point(17, 112);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(72, 16);
		this.label8.TabIndex = 37;
		this.label8.Text = "等待时长";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label9.Location = new System.Drawing.Point(17, 67);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(72, 16);
		this.label9.TabIndex = 36;
		this.label9.Text = "清洗时长";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label10.Location = new System.Drawing.Point(17, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(72, 16);
		this.label10.TabIndex = 35;
		this.label10.Text = "进样时长";
		this.ucTBInjTime.BackColor = System.Drawing.Color.Transparent;
		this.ucTBInjTime.ConerRadius = 5;
		this.ucTBInjTime.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBInjTime.DecLength = 2;
		this.ucTBInjTime.FillColor = System.Drawing.Color.Empty;
		this.ucTBInjTime.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBInjTime.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBInjTime.InputText = "";
		this.ucTBInjTime.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBInjTime.IsFocusColor = true;
		this.ucTBInjTime.IsRadius = true;
		this.ucTBInjTime.IsShowClearBtn = true;
		this.ucTBInjTime.IsShowKeyboard = true;
		this.ucTBInjTime.IsShowRect = true;
		this.ucTBInjTime.IsShowSearchBtn = false;
		this.ucTBInjTime.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBInjTime.Location = new System.Drawing.Point(101, 13);
		this.ucTBInjTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBInjTime.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBInjTime.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBInjTime.Name = "ucTBInjTime";
		this.ucTBInjTime.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBInjTime.PasswordChar = '\0';
		this.ucTBInjTime.PromptColor = System.Drawing.Color.Gray;
		this.ucTBInjTime.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBInjTime.PromptText = "";
		this.ucTBInjTime.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBInjTime.RectWidth = 1;
		this.ucTBInjTime.RegexPattern = "";
		this.ucTBInjTime.Size = new System.Drawing.Size(145, 32);
		this.ucTBInjTime.TabIndex = 32;
		this.ucTBWaitTime.BackColor = System.Drawing.Color.Transparent;
		this.ucTBWaitTime.ConerRadius = 5;
		this.ucTBWaitTime.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBWaitTime.DecLength = 2;
		this.ucTBWaitTime.FillColor = System.Drawing.Color.Empty;
		this.ucTBWaitTime.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBWaitTime.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBWaitTime.InputText = "";
		this.ucTBWaitTime.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBWaitTime.IsFocusColor = true;
		this.ucTBWaitTime.IsRadius = true;
		this.ucTBWaitTime.IsShowClearBtn = true;
		this.ucTBWaitTime.IsShowKeyboard = true;
		this.ucTBWaitTime.IsShowRect = true;
		this.ucTBWaitTime.IsShowSearchBtn = false;
		this.ucTBWaitTime.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBWaitTime.Location = new System.Drawing.Point(101, 103);
		this.ucTBWaitTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBWaitTime.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBWaitTime.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBWaitTime.Name = "ucTBWaitTime";
		this.ucTBWaitTime.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBWaitTime.PasswordChar = '\0';
		this.ucTBWaitTime.PromptColor = System.Drawing.Color.Gray;
		this.ucTBWaitTime.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBWaitTime.PromptText = "";
		this.ucTBWaitTime.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBWaitTime.RectWidth = 1;
		this.ucTBWaitTime.RegexPattern = "";
		this.ucTBWaitTime.Size = new System.Drawing.Size(145, 32);
		this.ucTBWaitTime.TabIndex = 34;
		this.ucTBCleanTime.BackColor = System.Drawing.Color.Transparent;
		this.ucTBCleanTime.ConerRadius = 5;
		this.ucTBCleanTime.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBCleanTime.DecLength = 2;
		this.ucTBCleanTime.FillColor = System.Drawing.Color.Empty;
		this.ucTBCleanTime.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBCleanTime.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBCleanTime.InputText = "";
		this.ucTBCleanTime.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBCleanTime.IsFocusColor = true;
		this.ucTBCleanTime.IsRadius = true;
		this.ucTBCleanTime.IsShowClearBtn = true;
		this.ucTBCleanTime.IsShowKeyboard = true;
		this.ucTBCleanTime.IsShowRect = true;
		this.ucTBCleanTime.IsShowSearchBtn = false;
		this.ucTBCleanTime.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBCleanTime.Location = new System.Drawing.Point(101, 58);
		this.ucTBCleanTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBCleanTime.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBCleanTime.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBCleanTime.Name = "ucTBCleanTime";
		this.ucTBCleanTime.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBCleanTime.PasswordChar = '\0';
		this.ucTBCleanTime.PromptColor = System.Drawing.Color.Gray;
		this.ucTBCleanTime.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBCleanTime.PromptText = "";
		this.ucTBCleanTime.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBCleanTime.RectWidth = 1;
		this.ucTBCleanTime.RegexPattern = "";
		this.ucTBCleanTime.Size = new System.Drawing.Size(145, 32);
		this.ucTBCleanTime.TabIndex = 33;
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.ucTBTemp1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.ucTBTemp2);
		this.groupBox1.Controls.Add(this.ucTBTempCur);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.ucTBTimeHold);
		this.groupBox1.Location = new System.Drawing.Point(6, 50);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(299, 200);
		this.groupBox1.TabIndex = 34;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "温度(脱附管)";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label12.Location = new System.Drawing.Point(255, 25);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(24, 16);
		this.label12.TabIndex = 34;
		this.label12.Text = "℃";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label13.Location = new System.Drawing.Point(255, 160);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(24, 16);
		this.label13.TabIndex = 37;
		this.label13.Text = "℃";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label14.Location = new System.Drawing.Point(255, 115);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(16, 16);
		this.label14.TabIndex = 36;
		this.label14.Text = "S";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label15.Location = new System.Drawing.Point(255, 70);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(24, 16);
		this.label15.TabIndex = 35;
		this.label15.Text = "℃";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label4.Location = new System.Drawing.Point(32, 27);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 16);
		this.label4.TabIndex = 28;
		this.label4.Text = "初温";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label7.Location = new System.Drawing.Point(32, 162);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(40, 16);
		this.label7.TabIndex = 33;
		this.label7.Text = "实测";
		this.ucTBTemp1.BackColor = System.Drawing.Color.Transparent;
		this.ucTBTemp1.ConerRadius = 5;
		this.ucTBTemp1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBTemp1.DecLength = 2;
		this.ucTBTemp1.FillColor = System.Drawing.Color.Empty;
		this.ucTBTemp1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBTemp1.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTemp1.InputText = "";
		this.ucTBTemp1.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBTemp1.IsFocusColor = true;
		this.ucTBTemp1.IsRadius = true;
		this.ucTBTemp1.IsShowClearBtn = true;
		this.ucTBTemp1.IsShowKeyboard = true;
		this.ucTBTemp1.IsShowRect = true;
		this.ucTBTemp1.IsShowSearchBtn = false;
		this.ucTBTemp1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBTemp1.Location = new System.Drawing.Point(101, 19);
		this.ucTBTemp1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBTemp1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBTemp1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBTemp1.Name = "ucTBTemp1";
		this.ucTBTemp1.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBTemp1.PasswordChar = '\0';
		this.ucTBTemp1.PromptColor = System.Drawing.Color.Gray;
		this.ucTBTemp1.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTemp1.PromptText = "";
		this.ucTBTemp1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBTemp1.RectWidth = 1;
		this.ucTBTemp1.RegexPattern = "";
		this.ucTBTemp1.Size = new System.Drawing.Size(145, 32);
		this.ucTBTemp1.TabIndex = 26;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label6.Location = new System.Drawing.Point(32, 117);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(40, 16);
		this.label6.TabIndex = 32;
		this.label6.Text = "保持";
		this.ucTBTemp2.BackColor = System.Drawing.Color.Transparent;
		this.ucTBTemp2.ConerRadius = 5;
		this.ucTBTemp2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBTemp2.DecLength = 2;
		this.ucTBTemp2.FillColor = System.Drawing.Color.Empty;
		this.ucTBTemp2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBTemp2.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTemp2.InputText = "";
		this.ucTBTemp2.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBTemp2.IsFocusColor = true;
		this.ucTBTemp2.IsRadius = true;
		this.ucTBTemp2.IsShowClearBtn = true;
		this.ucTBTemp2.IsShowKeyboard = true;
		this.ucTBTemp2.IsShowRect = true;
		this.ucTBTemp2.IsShowSearchBtn = false;
		this.ucTBTemp2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBTemp2.Location = new System.Drawing.Point(101, 64);
		this.ucTBTemp2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBTemp2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBTemp2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBTemp2.Name = "ucTBTemp2";
		this.ucTBTemp2.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBTemp2.PasswordChar = '\0';
		this.ucTBTemp2.PromptColor = System.Drawing.Color.Gray;
		this.ucTBTemp2.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTemp2.PromptText = "";
		this.ucTBTemp2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBTemp2.RectWidth = 1;
		this.ucTBTemp2.RegexPattern = "";
		this.ucTBTemp2.Size = new System.Drawing.Size(145, 32);
		this.ucTBTemp2.TabIndex = 27;
		this.ucTBTempCur.BackColor = System.Drawing.Color.Transparent;
		this.ucTBTempCur.ConerRadius = 5;
		this.ucTBTempCur.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBTempCur.DecLength = 2;
		this.ucTBTempCur.FillColor = System.Drawing.Color.Empty;
		this.ucTBTempCur.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBTempCur.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTempCur.InputText = "";
		this.ucTBTempCur.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBTempCur.IsFocusColor = true;
		this.ucTBTempCur.IsRadius = true;
		this.ucTBTempCur.IsShowClearBtn = true;
		this.ucTBTempCur.IsShowKeyboard = true;
		this.ucTBTempCur.IsShowRect = true;
		this.ucTBTempCur.IsShowSearchBtn = false;
		this.ucTBTempCur.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBTempCur.Location = new System.Drawing.Point(101, 154);
		this.ucTBTempCur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBTempCur.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBTempCur.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBTempCur.Name = "ucTBTempCur";
		this.ucTBTempCur.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBTempCur.PasswordChar = '\0';
		this.ucTBTempCur.PromptColor = System.Drawing.Color.Gray;
		this.ucTBTempCur.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTempCur.PromptText = "";
		this.ucTBTempCur.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBTempCur.RectWidth = 1;
		this.ucTBTempCur.RegexPattern = "";
		this.ucTBTempCur.Size = new System.Drawing.Size(145, 32);
		this.ucTBTempCur.TabIndex = 31;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label5.Location = new System.Drawing.Point(32, 72);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(40, 16);
		this.label5.TabIndex = 29;
		this.label5.Text = "终温";
		this.ucTBTimeHold.BackColor = System.Drawing.Color.Transparent;
		this.ucTBTimeHold.ConerRadius = 5;
		this.ucTBTimeHold.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBTimeHold.DecLength = 2;
		this.ucTBTimeHold.FillColor = System.Drawing.Color.Empty;
		this.ucTBTimeHold.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBTimeHold.Font = new System.Drawing.Font("Microsoft YaHei", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTimeHold.InputText = "";
		this.ucTBTimeHold.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBTimeHold.IsFocusColor = true;
		this.ucTBTimeHold.IsRadius = true;
		this.ucTBTimeHold.IsShowClearBtn = true;
		this.ucTBTimeHold.IsShowKeyboard = true;
		this.ucTBTimeHold.IsShowRect = true;
		this.ucTBTimeHold.IsShowSearchBtn = false;
		this.ucTBTimeHold.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBTimeHold.Location = new System.Drawing.Point(101, 109);
		this.ucTBTimeHold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBTimeHold.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBTimeHold.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBTimeHold.Name = "ucTBTimeHold";
		this.ucTBTimeHold.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBTimeHold.PasswordChar = '\0';
		this.ucTBTimeHold.PromptColor = System.Drawing.Color.Gray;
		this.ucTBTimeHold.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBTimeHold.PromptText = "";
		this.ucTBTimeHold.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBTimeHold.RectWidth = 1;
		this.ucTBTimeHold.RegexPattern = "";
		this.ucTBTimeHold.Size = new System.Drawing.Size(145, 32);
		this.ucTBTimeHold.TabIndex = 30;
		this.ucCbInject.BackColor = System.Drawing.Color.Transparent;
		this.ucCbInject.BackColorExt = System.Drawing.Color.FromArgb(240, 240, 240);
		this.ucCbInject.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
		this.ucCbInject.ConerRadius = 5;
		this.ucCbInject.DropPanelHeight = -1;
		this.ucCbInject.FillColor = System.Drawing.Color.White;
		this.ucCbInject.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.ucCbInject.IsRadius = true;
		this.ucCbInject.IsShowRect = true;
		this.ucCbInject.ItemWidth = 70;
		this.ucCbInject.Location = new System.Drawing.Point(98, 10);
		this.ucCbInject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucCbInject.Name = "ucCbInject";
		this.ucCbInject.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucCbInject.RectWidth = 1;
		this.ucCbInject.SelectedIndex = -1;
		this.ucCbInject.SelectedValue = "";
		this.ucCbInject.Size = new System.Drawing.Size(173, 32);
		this.ucCbInject.Source = null;
		this.ucCbInject.TabIndex = 25;
		this.ucCbInject.TextValue = null;
		this.ucCbInject.TriangleColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label3.Location = new System.Drawing.Point(3, 10);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(88, 16);
		this.label3.TabIndex = 23;
		this.label3.Text = "进样器类型";
		this.uBtnStart.BackColor = System.Drawing.Color.White;
		this.uBtnStart.BtnBackColor = System.Drawing.Color.White;
		this.uBtnStart.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.uBtnStart.BtnForeColor = System.Drawing.Color.White;
		this.uBtnStart.BtnText = "开始";
		this.uBtnStart.ConerRadius = 5;
		this.uBtnStart.Cursor = System.Windows.Forms.Cursors.Hand;
		this.uBtnStart.EnabledMouseEffect = false;
		this.uBtnStart.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uBtnStart.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uBtnStart.IsRadius = true;
		this.uBtnStart.IsShowRect = true;
		this.uBtnStart.IsShowTips = false;
		this.uBtnStart.Location = new System.Drawing.Point(26, 458);
		this.uBtnStart.Margin = new System.Windows.Forms.Padding(0);
		this.uBtnStart.Name = "uBtnStart";
		this.uBtnStart.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.uBtnStart.RectWidth = 1;
		this.uBtnStart.Size = new System.Drawing.Size(248, 52);
		this.uBtnStart.TabIndex = 21;
		this.uBtnStart.TabStop = false;
		this.uBtnStart.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.uBtnStart.TipsText = "";
		this.uBtnStart.BtnClick += new System.EventHandler(uBtnStart_BtnClick);
		this.tabPage2.Controls.Add(this.label76);
		this.tabPage2.Controls.Add(this.dateTimePicker1);
		this.tabPage2.Controls.Add(this.tbMethName);
		this.tabPage2.Controls.Add(this.dateTimePicker2);
		this.tabPage2.Controls.Add(this.MethodOpen);
		this.tabPage2.Controls.Add(this.uSAutoMode);
		this.tabPage2.Controls.Add(this.MethodSave);
		this.tabPage2.Controls.Add(this.uSHe);
		this.tabPage2.Controls.Add(this.MethodReSave);
		this.tabPage2.Controls.Add(this.label2);
		this.tabPage2.Controls.Add(this.btnDownload);
		this.tabPage2.Controls.Add(this.label1);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(334, 620);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "tabPage2";
		this.tabPage2.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.Controls.Add(this.label19);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.ucTBWeight);
		base.Controls.Add(this.uBtnStart);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.ucCbInject);
		base.Controls.Add(this.groupBox1);
		base.Name = "splitSCtrl";
		base.Size = new System.Drawing.Size(342, 523);
		this.tabControl1.ResumeLayout(false);
		this.ucTBWeight.ResumeLayout(false);
		this.ucTBWeight.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
