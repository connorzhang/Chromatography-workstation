using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class InsDeviceCtrl : UserControl
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private IniParam iniParam = new IniParam(Application.StartupPath + "\\iniParam.dll");

	public static InsDeviceCtrl self;

	public List<EpcDeviceSetting> epcDevR = IArrayBase.NewArray<EpcDeviceSetting>(18);

	public List<EpcDeviceSetting> epcDev2 = IArrayBase.NewArray<EpcDeviceSetting>(18);

	private bool bVOCMode;

	public TextBox tbMachineState;

	private ComboBox cbKindMachine;

	private double double0 = 1.0;

	private SystemParam sysParam;

	public byte bData = 0;

	public int TempProgram = 16;

	private IContainer components = null;

	public DataGridView gvExtEvTP;

	private GroupBox gbtempset;

	private Panel panel1;

	private Label label19;

	public Button bControlTemp;

	private Label label18;

	public Button button14;

	private Label label17;

	private Label label16;

	private Label label15;

	public PictureBox pictureBox5;

	public PictureBox pictureBox4;

	public PictureBox pictureBox3;

	public PictureBox pictureBox2;

	public PictureBox pictureBox1;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage1;

	private Button button35;

	private Button button3;

	private Panel panel12;

	private Button button11;

	public DataGridView EPCControl;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;

	private DataGridViewTextBoxColumn Column5;

	public Label lFliuBi;

	private Label label69;

	public MaskedTextBox maskedTextBox8;

	private Label label4;

	private Panel panelJY;

	private Panel panel15;

	private Label label13;

	public MaskedTextBox maskedTextBox5;

	public DataGridView dgInsamp1;

	private Label label68;

	public MaskedTextBox maskedTextBox3;

	public MaskedTextBox maskedTextBox9;

	private Label label30;

	private Label label12;

	private Label label56;

	public MaskedTextBox maskedTextBox1;

	public ComboBox comboBox6;

	private Panel panel7;

	public RadioButton radioButton25;

	public RadioButton radioButton5;

	public RadioButton radioButton6;

	public MaskedTextBox maskedTextBox2;

	private Panel panel6;

	public RadioButton radioButton3;

	public RadioButton radioButton2;

	public RadioButton radioButton1;

	private Label label11;

	private Label label8;

	private Label label7;

	private Label label2;

	public MaskedTextBox maskedTextBox4;

	private Label label5;

	public RadioButton radioButton28;

	private Label label9;

	public Label label10;

	private Label label3;

	public System.Windows.Forms.TabControl tabControl2;

	private TabPage tabPage6;

	private TabPage tabPage7;

	private TabPage tabPage8;

	private TabPage tabPage9;

	private TabPage tabPage10;

	private TabPage tabPage15;

	private GroupBox groupBox3;

	public DataGridView dgtempControl;

	private DataGridViewTextBoxColumn 名称;

	private DataGridViewTextBoxColumn 实测温度;

	private DataGridViewTextBoxColumn 设定温度;

	private DataGridViewTextBoxColumn 保护温度;

	private TabPage tabPage2;

	private GroupBox groupBox7;

	private Button button34;

	private Label label75;

	public CheckBox checkBox_0;

	public CheckBox checkBox_1;

	public CheckBox checkBox_2;

	public CheckBox checkBox_3;

	public CheckBox checkBox_4;

	public CheckBox checkBox_5;

	public CheckBox checkBox_6;

	public CheckBox checkBox_7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private DataGridViewTextBoxColumn 事件;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

	private DataGridViewTextBoxColumn Column1;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewTextBoxColumn Column3;

	private DataGridViewTextBoxColumn Column4;

	private GroupBox groupBox6;

	private Button button9;

	public MaskedTextBox tbptIniTempHoldT;

	private Button button10;

	private Button button7;

	private Button button8;

	public DataGridView gdTempControl;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private Label label35;

	private TabPage tabPage3;

	private Button liquidstatusselect;

	private GroupBox groupBox14;

	private Button button21;

	private Button button20;

	public MaskedTextBox maskedTextBox20;

	private Label label44;

	private Label label6;

	private Label label67;

	private Label label66;

	public MaskedTextBox maskedTextBox14;

	private Label label14;

	public MaskedTextBox maskedTextBox19;

	private Label label65;

	private GroupBox groupBox8;

	public TextBox textBox7;

	public TextBox textBox6;

	public TextBox textBox5;

	public TextBox textBox4;

	private Label label38;

	private Label label37;

	private Label label36;

	private TabPage tabPage5;

	private TextBox tBshuaijian3;

	private TextBox tBshuaijian2;

	private TextBox tBshuaijian;

	private Button btnShuaijian;

	private TextBox tBpasswordShJ;

	public Button button27;

	private Button button18;

	private GroupBox groupBox5;

	public TextBox textBox8;

	private Button button31;

	private CheckBox checkBox9;

	private Button button29;

	private Button button30;

	private Button bsavmess;

	private Button button28;

	private Label label28;

	private Label label70;

	public ComboBox comboBox13;

	public ComboBox comboBox4;

	public CheckBox checkBox7;

	public CheckBox cbAlarm;

	private Button button22;

	private Button button13;

	private GroupBox groupBox12;

	public MaskedTextBox IP3;

	public MaskedTextBox IP2;

	public MaskedTextBox IP1;

	private Label label61;

	private Label label60;

	private Label label59;

	public DataGridView gvHardVersion;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

	private Button button19;

	private GroupBox groupBox13;

	public MaskedTextBox IP6;

	private Label label63;

	public MaskedTextBox IP5;

	private Label label62;

	public MaskedTextBox IP4;

	private Label label64;

	private TabPage tabPage16;

	public ListBox listMess;

	public ImageList imageList1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;

	private GroupBox groupBox2;

	private CheckBox chbTestModbus;

	private GroupBox groupBox16;

	private TextBox tbMount202;

	private TextBox tbMount42;

	private Button btnMountTest;

	private Label label94;

	private TextBox tbMountTest;

	private TextBox tbMount201;

	private TextBox tbMount41;

	private Label label93;

	private Label label92;

	private TabPage tabPage4;

	private Button btnDownload;

	private Label label76;

	private Button MethodReSave;

	private Button MethodSave;

	private Button MethodOpen;

	public TextBox tbMethName;

	private GroupBox groupBox15;

	private Label label20;

	private Label label23;

	private Label label22;

	private Label label29;

	private Label label27;

	private Label label26;

	private Label label21;

	private Label label24;

	private Label label25;

	private Label label31;

	private Label label32;

	private Label label33;

	private Button btnEPCSet;

	private Label label78;

	private Label label79;

	private Label label77;

	private Label label34;

	public TextBox tbColPreSet1;

	public TextBox tbAirCur1;

	public TextBox tbAirSet1;

	public TextBox tbHHCur1;

	public TextBox tbHHSet1;

	public TextBox tbColPreCur1;

	public TextBox tbAirCur2;

	public TextBox tbAirSet2;

	public TextBox tbHHCur2;

	public TextBox tbHHSet2;

	public TextBox tbColPreCur2;

	public TextBox tbColPreSet2;

	private GroupBox groupBox9;

	private Label label80;

	private Label label82;

	private Label label84;

	private Label label85;

	private Label label86;

	public TextBox tbAirP2;

	private Label label87;

	public TextBox tbHHP2;

	private Label label88;

	public TextBox tbColP2;

	private Label label89;

	private Label label90;

	private Label label91;

	private Label label95;

	public TextBox tbAirP1;

	private Label label96;

	public TextBox tbHHP1;

	private Label label97;

	public TextBox tbColP1;

	private Label label98;

	private Label label99;

	public TextBox tbColP4;

	private Label label100;

	private Label label81;

	public TextBox tbColP3;

	private Label label83;

	private GroupBox groupBox17;

	private ComboBox cbEPCSe;

	private TextBox tbMount2012;

	private TextBox tbMount412;

	private TextBox tbMount2011;

	private TextBox tbMount411;

	private TextBox tbMount2010;

	private TextBox tbMount410;

	private TextBox tbMount209;

	private TextBox tbMount49;

	private TextBox tbMount208;

	private TextBox tbMount48;

	private TextBox tbMount207;

	private TextBox tbMount47;

	private Label label101;

	private Label label102;

	private TextBox tbMount206;

	private TextBox tbMount46;

	private TextBox tbMount205;

	private TextBox tbMount45;

	private TextBox tbMount204;

	private TextBox tbMount44;

	private TextBox tbMount203;

	private TextBox tbMount43;

	private Label label103;

	private Label label107;

	private Label label108;

	private Label label105;

	private Label label106;

	private Label label104;

	private Label label109;

	private Label label110;

	private Label label111;

	private Label label112;

	private Label label113;

	private Label label114;

	private TextBox tbChannelACnt;

	private Label label115;

	private Button btnSave;

	private TextBox tbCompen6;

	private TextBox tbCompen5;

	private TextBox tbCompen4;

	private TextBox tbCompen3;

	private TextBox tbCompen2;

	private TextBox tbCompen1;

	private Label label116;

	private TextBox tbCompen12;

	private TextBox tbCompen11;

	private TextBox tbCompen10;

	private TextBox tbCompen9;

	private TextBox tbCompen8;

	private TextBox tbCompen7;

	public Button button4;

	public Button button33;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	public GroupBox groupBox4;

	private TextBox tbSmooth;

	private TextBox tbKRName;

	private Button btnActivate;

	private Panel panConfig;

	public Button button6;

	private Label label117;

	private TabPage tabPage11;

	private DevComponents.DotNetBar.TabControl tabControl3;

	private TabControlPanel tabControlPanel1;

	private TabItem tabItem1;

	public TextBox tbREPT;

	private Label label125;

	private Label label123;

	private Label label122;

	public TextBox tbTANL;

	private Label label121;

	public TextBox tbIVOL;

	public TextBox tbSINT;

	public TextBox tbFSAM;

	private Label label118;

	private Label label119;

	private Label label120;

	private Button resetInj;

	public CheckBox checkBox6;

	public TextBox textBox10;

	private Button button15;

	public TextBox textBox12;

	public TextBox textBox14;

	private Button btnStartAutoINJ;

	private Button button24;

	private Button btnStopAutoINJ;

	public TextBox textBox13;

	public TextBox textBox11;

	public TextBox textBox15;

	public TextBox textBox9;

	public ComboBox comboBox2;

	private Label label40;

	public ComboBox comboBox12;

	public ComboBox comboBox11;

	public ComboBox comboBox3;

	public ComboBox comboBox1;

	private Label label1;

	private Label label48;

	private Label label52;

	private Label label54;

	private Label label50;

	private Label label46;

	private Label label47;

	private Label label55;

	private Label label58;

	private Label label42;

	private Label label57;

	private Label label51;

	private Label label53;

	private Label label43;

	private Label label49;

	private Label label45;

	private Label label41;

	private Label label39;

	public DataGridView dgGramset;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

	private DataGridViewTextBoxColumn 间隔;

	private GroupBox groupBox1;

	private GroupBox groupBox11;

	public RadioButton rliquid;

	private Button liquidsetvaluequery;

	private Button liquidset;

	public RadioButton rpass;

	public TextBox liquidpass;

	private Label label74;

	public TextBox liquidpress;

	private Label label71;

	public TextBox Maxliquidpress;

	public TextBox Minliquidpress;

	private Label label72;

	private Label label73;

	public Label liquidStatus;

	private Button liquidclose;
    private Button SendBtn;
    private TextBox textBox1;
    private GroupBox groupBox10;
    private Button liquidopen;

	public string strIniTempHoldT
	{
		get
		{
			return tbptIniTempHoldT.Text;
		}
		set
		{
			tbptIniTempHoldT.Text = value;
		}
	}

	public bool ShowVOCMode
	{
		get
		{
			return bVOCMode;
		}
		set
		{
			bVOCMode = value;
			if (bVOCMode)
			{
				tabPage4.Parent = null;
				groupBox7.Height = 150;
				gvExtEvTP.Height = 110;
				if (frmParam.epcMode == 0)
				{
					groupBox9.Visible = false;
					groupBox15.Visible = true;
					groupBox15.Location = new Point(2, 420);
				}
				else if (frmParam.epcMode == 1)
				{
					groupBox15.Visible = false;
					groupBox9.Visible = true;
					groupBox9.Location = new Point(2, 420);
				}
				else
				{
					groupBox15.Visible = false;
					groupBox9.Visible = false;
				}
				if (frmParam.kindMachine == 4)
				{
					panel1.Visible = false;
				}
				else
				{
					panel1.Visible = true;
				}
				groupBox4.Visible = false;
				tabPage1.Controls.Add(groupBox15);
				tabPage1.Controls.Add(groupBox9);
				tabPage5.Parent = null;
				tabPage16.Parent = null;
				tbMachineState = new TextBox();
				tabPage1.Controls.Add(tbMachineState);
				tbMachineState.Location = new Point(260, 5);
				tbMachineState.Name = "tbMachineState";
				tbMachineState.Size = new Size(70, 21);
				tbMachineState.ReadOnly = true;
				tbMachineState.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
				tbMachineState.ForeColor = SystemColors.MenuHighlight;
				tbMachineState.Text = Lang.PS("开机运行", "Boot operation");
				tbMachineState.TabIndex = 0;
				if (frmParam.kindMachine == 4)
				{
					tabPage1.Controls.Add(pictureBox1);
					pictureBox1.Location = new Point(2, 3);
					tabPage1.Controls.Add(label15);
					label15.Location = new Point(32, 5);
				}
				else
				{
					pictureBox2.Visible = false;
					pictureBox3.Visible = false;
					pictureBox4.Visible = false;
					pictureBox5.Visible = false;
					label16.Visible = false;
					label17.Visible = false;
					label18.Visible = false;
					label19.Visible = false;
				}
				groupBox3.Location = new Point(8, 30);
				button3.Location = new Point(235, 213);
				button4.Location = new Point(283, 213);
				button8.Location = new Point(230, 390);
				button7.Location = new Point(288, 390);
				tabPage1.Controls.Add(button8);
				tabPage1.Controls.Add(button7);
				tabPage1.Controls.Add(groupBox7);
				groupBox7.Location = new Point(2, 230);
				groupBox16.Visible = true;
				tabPage2.Parent = null;
				button35.Visible = false;
				tabPage3.Text = Lang.PS("循环次数设定", "cycle-index");
				dgGramset.Visible = false;
				button15.Visible = false;
				btnStopAutoINJ.Visible = false;
				button24.Visible = false;
				groupBox1.Visible = false;
				groupBox14.Location = new Point(2, 42);
				label14.Visible = false;
				maskedTextBox14.Visible = false;
				label44.Visible = false;
				label37.Text = Lang.PS("次数/流路号", "Number");
				cbKindMachine = new ComboBox();
				tabPage3.Controls.Add(groupBox17);
				panConfig.Controls.Add(cbKindMachine);
				groupBox17.Location = new Point(9, 450);
				cbKindMachine.Location = new Point(126, 18);
				cbKindMachine.Name = "cbKindMachine";
				cbKindMachine.Size = new Size(125, 20);
				cbKindMachine.TabIndex = 93;
				cbKindMachine.Text = Lang.PS("非甲烷总烃+苯系物", "NMHC+THC");
				cbKindMachine.Visible = true;
				cbKindMachine.SelectedIndexChanged += cbKindMachine_SelectedIndexChanged;
				cbKindMachine.DropDownStyle = ComboBoxStyle.DropDownList;
				cbKindMachine.Items.Add(Lang.PS("非甲烷总烃+苯系物", "NMHC+THC"));
				cbKindMachine.Items.Add(Lang.PS("单非甲烷总烃", "NMHC"));
				cbKindMachine.Items.Add(Lang.PS("双非甲烷总烃", "Two NMHC"));
				cbKindMachine.Items.Add(Lang.PS("单苯系物", "Benzenes "));
				cbKindMachine.Items.Add(Lang.PS("B 型", "B"));
				cbKindMachine.SelectedIndex = frmParam.kindMachine;
				cbEPCSe.SelectedIndex = frmParam.epcMode;
				dgtempControl.BackgroundColor = Color.White;
				for (int i = 0; i < 8; i++)
				{
					dgtempControl.Rows[i].Cells[0].Style.BackColor = Color.White;
					dgtempControl.Rows[i].Cells[0].Style.ForeColor = Color.Black;
					dgtempControl.Rows[i].Cells[1].Style.BackColor = Color.White;
					dgtempControl.Rows[i].Cells[1].Style.ForeColor = Color.Black;
					dgtempControl.Rows[i].Cells[2].Style.BackColor = Color.White;
					dgtempControl.Rows[i].Cells[2].Style.ForeColor = Color.Black;
					dgtempControl.Rows[i].Cells[3].Style.BackColor = Color.White;
					dgtempControl.Rows[i].Cells[3].Style.ForeColor = Color.Black;
				}
				for (int j = 0; j < 8; j++)
				{
					gvExtEvTP.Rows[j].Cells[0].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[0].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[1].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[1].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[2].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[2].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[3].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[3].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[4].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[4].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[5].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[5].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[6].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[6].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[7].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[7].Style.ForeColor = Color.Black;
					gvExtEvTP.Rows[j].Cells[8].Style.BackColor = Color.White;
					gvExtEvTP.Rows[j].Cells[8].Style.ForeColor = Color.Black;
				}
				gvExtEvTP.BackgroundColor = Color.White;
				groupBox2.Visible = false;
			}
			else
			{
				groupBox16.Visible = false;
				groupBox15.Visible = false;
				groupBox9.Visible = false;
			}
		}
	}

	public InsDeviceManager devManager => cdlMgr.CurrentInsDeviceMgr;

	public int CurrentEpcIdx => tabControl2.SelectedIndex;

	private void cbKindMachine_SelectedIndexChanged(object sender, EventArgs e)
	{
		frmParam.kindMachine = cbKindMachine.SelectedIndex;
		frmParam.SaveParam();
		if (frmParam.kindMachine != 0 && frmParam.kindMachine != 1)
		{
		}
	}

	public static bool IsDesignMode()
	{
		return false;
	}

	public InsDeviceCtrl()
	{
		self = this;
		InitializeComponent();
		if (!IsDesignMode())
		{
			sysParam = SystemParam.Create();
			Load();
			groupBox2.Visible = false;
		}
	}

	public void ReadInsDeviceMgr(InsDeviceManager myDevMgr)
	{
		ReadEventTable(devManager);
		ReadTempratureTable(devManager);
		ReadTempratureTable2(devManager);
		ReadTempratureControl(devManager);
		ReadIpAddress(devManager);
		ReadInjectorBaseSetting(devManager);
		ReadAutoInjectorSetting(devManager);
		ReadEpcInfo(devManager);
		ReadInjectNumList(devManager);
		ReadHardVersion(devManager);
	}

	public void WriteInsDeviceMgr(InsDeviceManager myDevMgr)
	{
		WriteEventTable(devManager);
		WriteTempratureTable(devManager);
		WriteTempratureTable2(devManager);
		WriteIpAddress(devManager);
		WriteInjectorBaseSetting(devManager);
		WriteTempratureControl(devManager);
		WriteAutoInjectorSetting(devManager);
		WriteEpcInfo(devManager);
		WriteHardVersion(devManager);
	}

	public void ReadEventTable(InsDeviceManager myDevMgr)
	{
		ReadEventTable0(myDevMgr);
		ReadEventTable1(myDevMgr);
	}

	public void WriteEventTable(InsDeviceManager myDevMgr)
	{
		WriteEventTable0(myDevMgr);
		WriteEventTable1(myDevMgr);
	}

	public void ReadEventTable0(InsDeviceManager myDevMgr)
	{
		for (int i = 0; i < gvExtEvTP.RowCount; i++)
		{
			for (int j = 1; j < 5; j++)
			{
				gvExtEvTP.Rows[i].Cells[j].Value = myDevMgr.eventCtrl0[j - 1][i];
				if (FormMainPortable.fromMain != null)
				{
					if (i == 0 && j == 1)
					{
						FormMainPortable.fromMain.ucTBJump1.InputText = myDevMgr.eventCtrl0[j - 1][i].ToString("0.00");
					}
					else if (i == 0 && j == 2)
					{
						FormMainPortable.fromMain.ucTBValve1.InputText = myDevMgr.eventCtrl0[j - 1][i].ToString("0.00");
					}
					else if (i == 0 && j == 3)
					{
						FormMainPortable.fromMain.ucTBValve31.InputText = myDevMgr.eventCtrl0[j - 1][i].ToString("0.00");
					}
					else if (i == 1 && j == 1)
					{
						FormMainPortable.fromMain.ucTBJump2.InputText = myDevMgr.eventCtrl0[j - 1][i].ToString("0.00");
					}
					else if (i == 1 && j == 2)
					{
						FormMainPortable.fromMain.ucTBValve2.InputText = myDevMgr.eventCtrl0[j - 1][i].ToString("0.00");
					}
					else if (i == 1 && j == 3)
					{
						FormMainPortable.fromMain.ucTBValve32.InputText = myDevMgr.eventCtrl0[j - 1][i].ToString("0.00");
					}
				}
			}
		}
	}

	public void ReadEventTable1(InsDeviceManager myDevMgr)
	{
		for (int i = 0; i < gvExtEvTP.RowCount; i++)
		{
			for (int j = 5; j < gvExtEvTP.ColumnCount; j++)
			{
				gvExtEvTP.Rows[i].Cells[j].Value = myDevMgr.eventCtrl1[j - 5][i];
			}
		}
	}

	public void WriteEventTable0(InsDeviceManager myDevMgr)
	{
		for (int i = 0; i < gvExtEvTP.RowCount; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				myDevMgr.eventCtrl0[j][i] = String2Float(gvExtEvTP.Rows[i].Cells[j + 1].Value);
			}
		}
	}

	public void WriteEventTable1(InsDeviceManager myDevMgr)
	{
		for (int i = 0; i < gvExtEvTP.RowCount; i++)
		{
			for (int j = 5; j < gvExtEvTP.ColumnCount; j++)
			{
				myDevMgr.eventCtrl1[j - 5][i] = String2Float(gvExtEvTP.Rows[i].Cells[j].Value);
			}
		}
	}

	public void ReadTempratureTableColor(InsDeviceManager myDevMgr)
	{
		if (!myDevMgr.insDevEnable0)
		{
			dgtempControl.Rows[0].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[0].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable1)
		{
			dgtempControl.Rows[1].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[1].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable2)
		{
			dgtempControl.Rows[2].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[2].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable3)
		{
			dgtempControl.Rows[4].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[4].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable4)
		{
			dgtempControl.Rows[3].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[3].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable5)
		{
			dgtempControl.Rows[5].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[5].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable6)
		{
			dgtempControl.Rows[6].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[6].Cells[0].Style.BackColor = Color.Blue;
		}
		if (!myDevMgr.insDevEnable7)
		{
			dgtempControl.Rows[7].Cells[0].Style.BackColor = Color.Red;
		}
		else
		{
			dgtempControl.Rows[7].Cells[0].Style.BackColor = Color.Blue;
		}
	}

	public void ReadTempratureTableName(InsDeviceManager myDevMgr)
	{
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.Language == "en")
		{
			dgtempControl.Rows[0].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[0].strNameEn;
			dgtempControl.Rows[1].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[1].strNameEn;
			dgtempControl.Rows[2].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[2].strNameEn;
			dgtempControl.Rows[4].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[3].strNameEn;
			dgtempControl.Rows[3].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[4].strNameEn;
			dgtempControl.Rows[5].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[5].strNameEn;
			dgtempControl.Rows[6].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[6].strNameEn;
			dgtempControl.Rows[7].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[7].strNameEn;
		}
		else
		{
			dgtempControl.Rows[0].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[0].strNameCn;
			dgtempControl.Rows[1].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[1].strNameCn;
			dgtempControl.Rows[2].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[2].strNameCn;
			dgtempControl.Rows[4].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[3].strNameCn;
			dgtempControl.Rows[3].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[4].strNameCn;
			dgtempControl.Rows[5].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[5].strNameCn;
			dgtempControl.Rows[6].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[6].strNameCn;
			dgtempControl.Rows[7].Cells[0].Value = myDevMgr.tempCtrlAreaTable.tempList[7].strNameCn;
		}
	}

	public void ReadTempratureTable(Class44 class44_0, float fuzhu3, float zhulu2)
	{
		int num = 1;
		for (int i = 0; i < class44_0.float_0.Length; i++)
		{
			switch (i)
			{
			case 3:
				dgtempControl.Rows[i].Cells[1].Value = class44_0.float_0[4].ToString("F" + num);
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[3].curV = class44_0.float_0[4].ToString("0.0");
				}
				continue;
			case 4:
				dgtempControl.Rows[i].Cells[1].Value = class44_0.float_0[3].ToString("F" + num);
				continue;
			}
			if (splitSCtrl.self != null && i == 0)
			{
				splitSCtrl.self.strCurTemp = class44_0.float_0[i].ToString("0.0");
			}
			if (FormMainPortable.fromMain != null)
			{
				switch (i)
				{
				case 0:
					FormMainPortable.fromMain.lstSource[2].curV = class44_0.float_0[i].ToString("0.0");
					break;
				case 1:
					FormMainPortable.fromMain.lstSource[0].curV = class44_0.float_0[i].ToString("0.0");
					break;
				case 2:
					FormMainPortable.fromMain.lstSource[1].curV = class44_0.float_0[i].ToString("0.0");
					FormMainPortable.fromMain.dGPara1.ReloadSource();
					break;
				}
			}
			dgtempControl.Rows[i].Cells[1].Value = class44_0.float_0[i].ToString("F" + num);
		}
		dgtempControl.Rows[6].Cells[1].Value = fuzhu3.ToString("F" + num);
		dgtempControl.Rows[7].Cells[1].Value = zhulu2.ToString("F" + num);
	}

	public void ReadTempratureTable(InsDeviceManager myDevMgr)
	{
		int num = 1;
		for (int i = 0; i < 6; i++)
		{
			switch (i)
			{
			case 3:
				dgtempControl.Rows[i].Cells[3].Value = myDevMgr.tempProtectList[4].ToString("F" + num);
				dgtempControl.Rows[i].Cells[2].Value = myDevMgr.tempSetedList[4].ToString("F" + num);
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[3].setV = myDevMgr.tempSetedList[4].ToString("0.0");
					FormMainPortable.fromMain.tbColueCata.InputText = myDevMgr.tempSetedList[4].ToString("0.0");
				}
				continue;
			case 4:
				dgtempControl.Rows[i].Cells[3].Value = myDevMgr.tempProtectList[3].ToString("F" + num);
				dgtempControl.Rows[i].Cells[2].Value = myDevMgr.tempSetedList[3].ToString("F" + num);
				continue;
			}
			if (FormMainPortable.fromMain != null)
			{
				switch (i)
				{
				case 0:
					FormMainPortable.fromMain.lstSource[2].setV = myDevMgr.tempSetedList[i].ToString("F" + num);
					FormMainPortable.fromMain.tbColueAUX.InputText = myDevMgr.tempSetedList[i].ToString("F" + num);
					break;
				case 1:
					FormMainPortable.fromMain.lstSource[0].setV = myDevMgr.tempSetedList[i].ToString("F" + num);
					FormMainPortable.fromMain.tbColuenTemp.InputText = myDevMgr.tempSetedList[i].ToString("F" + num);
					break;
				case 2:
					FormMainPortable.fromMain.lstSource[1].setV = myDevMgr.tempSetedList[i].ToString("F" + num);
					FormMainPortable.fromMain.tbColuenDetec.InputText = myDevMgr.tempSetedList[i].ToString("F" + num);
					FormMainPortable.fromMain.dGPara1.ReloadSource();
					break;
				}
			}
			dgtempControl.Rows[i].Cells[3].Value = myDevMgr.tempProtectList[i].ToString("F" + num);
			dgtempControl.Rows[i].Cells[2].Value = myDevMgr.tempSetedList[i].ToString("F" + num);
		}
	}

	public void WriteTempratureTable(InsDeviceManager myDevMgr)
	{
		for (int i = 0; i < 6; i++)
		{
			switch (i)
			{
			case 3:
				myDevMgr.tempProtectList[i] = String2Float(dgtempControl.Rows[4].Cells[3].Value);
				myDevMgr.tempSetedList[i] = String2Float(dgtempControl.Rows[4].Cells[2].Value);
				break;
			case 4:
				myDevMgr.tempProtectList[i] = String2Float(dgtempControl.Rows[3].Cells[3].Value);
				myDevMgr.tempSetedList[i] = String2Float(dgtempControl.Rows[3].Cells[2].Value);
				break;
			default:
				myDevMgr.tempProtectList[i] = String2Float(dgtempControl.Rows[i].Cells[3].Value);
				myDevMgr.tempSetedList[i] = String2Float(dgtempControl.Rows[i].Cells[2].Value);
				break;
			}
		}
	}

	public void ReadTempratureTable2(InsDeviceManager myDevMgr)
	{
		int num = 1;
		for (int i = 0; i < 2; i++)
		{
			dgtempControl.Rows[6 + i].Cells[3].Value = myDevMgr.tempSetedList2[i].ToString("F" + num);
			dgtempControl.Rows[6 + i].Cells[2].Value = myDevMgr.tempProtectList2[i].ToString("F" + num);
		}
	}

	public void WriteTempratureTable2(InsDeviceManager myDevMgr)
	{
		for (int i = 0; i < 2; i++)
		{
			myDevMgr.tempSetedList2[i] = String2Float(dgtempControl.Rows[i + 6].Cells[3].Value);
			myDevMgr.tempProtectList2[i] = String2Float(dgtempControl.Rows[i + 6].Cells[2].Value);
		}
	}

	public void ReadIpAddress(InsDeviceManager myDevMgr)
	{
		if (myDevMgr.ipAddressList.Length >= 6 || myDevMgr.ipAddressList[0] != null)
		{
			IP1.Text = myDevMgr.ipAddressList[0].ToString();
			IP2.Text = myDevMgr.ipAddressList[1].ToString();
			IP3.Text = myDevMgr.ipAddressList[2].ToString();
			IP4.Text = myDevMgr.ipAddressList[3].ToString();
			IP5.Text = myDevMgr.ipAddressList[4].ToString();
			IP6.Text = myDevMgr.ipAddressList[5].ToString();
		}
	}

	public void WriteIpAddress(InsDeviceManager myDevMgr)
	{
		if (myDevMgr.ipAddressList.Length >= 6)
		{
			myDevMgr.ipAddressList[0] = new MyIPAddress(IP1.Text.Trim());
			myDevMgr.ipAddressList[1] = new MyIPAddress(IP2.Text.Trim());
			myDevMgr.ipAddressList[2] = new MyIPAddress(IP3.Text.Trim());
			myDevMgr.ipAddressList[3] = new MyIPAddress(IP4.Text.Trim());
			myDevMgr.ipAddressList[4] = new MyIPAddress(IP5.Text.Trim());
			myDevMgr.ipAddressList[5] = new MyIPAddress(IP6.Text.Trim());
		}
	}

	public void ReadAutoInjectorSetting(InsDeviceManager myDevMgr)
	{
		maskedTextBox20.Text = myDevMgr.injectInterval.ToString();
		maskedTextBox19.Text = myDevMgr.injectNTimes.ToString();
		if (myDevMgr.injectSpendTime == 16f)
		{
			checkBox6.Checked = true;
		}
		else
		{
			checkBox6.Checked = false;
		}
		maskedTextBox14.Text = ((int)myDevMgr.injectLightTime).ToString("X");
	}

	public void WriteAutoInjectorSetting(InsDeviceManager myDevMgr)
	{
		myDevMgr.injectInterval = Class49.String2Float(maskedTextBox20.Text, 0f);
		myDevMgr.injectNTimes = Class49.String2Float(maskedTextBox19.Text, 0f);
		if (checkBox6.Checked)
		{
			myDevMgr.injectSpendTime = 16f;
		}
		else
		{
			myDevMgr.injectSpendTime = 0f;
		}
		myDevMgr.injectLightTime = Class49.String2Float(maskedTextBox14.Text, 99f);
	}

	public void ReadInjectorBaseSetting(InsDeviceManager myDevMgr)
	{
		comboBox2.SelectedIndex = myDevMgr.injectSet.int_4;
		textBox9.Text = myDevMgr.injectSet.solClearTimeBeforInject.ToString();
		textBox14.Text = myDevMgr.injectSet.sampClearTimeBeforInject.ToString();
		textBox15.Text = myDevMgr.injectSet.solClearTimeAfterInject.ToString();
		textBox13.Text = myDevMgr.injectSet.pumpTime.ToString();
		textBox12.Text = myDevMgr.injectSet.visDelyTime.ToString();
		textBox10.Text = myDevMgr.injectSet.delyStartTime.ToString();
		comboBox2.SelectedIndex = myDevMgr.injectSet.int_4;
		textBox11.Text = myDevMgr.injectSet.float_3.ToString();
		comboBox3.SelectedIndex = myDevMgr.injectSet.injectMethod;
		comboBox11.SelectedIndex = myDevMgr.injectSet.injectSpeed;
		comboBox12.SelectedIndex = myDevMgr.injectSet.styletSpeed;
		dgGramset.Rows.Clear();
		for (int i = 0; i < myDevMgr.injectSet.injectorQRow.Count; i++)
		{
			if (myDevMgr.injectSet.injectorQRow[i] != null)
			{
				dgGramset.Rows.Add(myDevMgr.injectSet.injectorQRow[i].startBotNo.ToString(), myDevMgr.injectSet.injectorQRow[i].endBotNo.ToString(), myDevMgr.injectSet.injectorQRow[i].fQuantity.ToString(), myDevMgr.injectSet.injectorQRow[i].iTime.ToString(), myDevMgr.injectSet.injectorQRow[i].iInterval.ToString());
			}
		}
	}

	public void WriteInjectorBaseSetting(InsDeviceManager myDevMgr)
	{
		myDevMgr.injectSet.int_4 = comboBox2.SelectedIndex;
		myDevMgr.injectSet.solClearTimeBeforInject = int.Parse(textBox9.Text.Trim());
		myDevMgr.injectSet.sampClearTimeBeforInject = int.Parse(textBox14.Text.Trim());
		myDevMgr.injectSet.solClearTimeAfterInject = int.Parse(textBox15.Text.Trim());
		myDevMgr.injectSet.pumpTime = int.Parse(textBox13.Text.Trim());
		myDevMgr.injectSet.visDelyTime = float.Parse(textBox12.Text.Trim());
		myDevMgr.injectSet.delyStartTime = float.Parse(textBox10.Text.Trim());
		myDevMgr.injectSet.int_4 = comboBox2.SelectedIndex;
		myDevMgr.injectSet.float_3 = float.Parse(textBox11.Text.Trim());
		myDevMgr.injectSet.injectMethod = comboBox3.SelectedIndex;
		myDevMgr.injectSet.injectSpeed = comboBox11.SelectedIndex;
		myDevMgr.injectSet.styletSpeed = comboBox12.SelectedIndex;
		myDevMgr.injectSet.iIvol = Convert.ToInt16(float.Parse(tbIVOL.Text.Trim()) * 10f);
		myDevMgr.injectSet.iSINT = int.Parse(tbSINT.Text.Trim());
		myDevMgr.injectSet.iFSAM = int.Parse(tbFSAM.Text.Trim());
		myDevMgr.injectSet.iTANL = int.Parse(tbTANL.Text.Trim());
		myDevMgr.injectSet.iREPT = int.Parse(tbREPT.Text.Trim());
		for (int i = 0; i < myDevMgr.injectSet.injectorQRow.Count; i++)
		{
			if (i <= dgGramset.Rows.Count)
			{
				myDevMgr.injectSet.injectorQRow[i].startBotNo = ToInt(dgGramset.Rows[i].Cells[0].Value);
				myDevMgr.injectSet.injectorQRow[i].endBotNo = ToInt(dgGramset.Rows[i].Cells[1].Value);
				myDevMgr.injectSet.injectorQRow[i].fQuantity = ToFloat(dgGramset.Rows[i].Cells[2].Value);
				myDevMgr.injectSet.injectorQRow[i].iTime = ToInt(dgGramset.Rows[i].Cells[3].Value);
				myDevMgr.injectSet.injectorQRow[i].iInterval = ToInt(dgGramset.Rows[i].Cells[4].Value);
			}
		}
	}

	private int ToInt(object obj)
	{
		int result = 0;
		string s = "0";
		if (obj != null)
		{
			s = obj.ToString();
		}
		int.TryParse(s, out result);
		return result;
	}

	private float ToFloat(object obj)
	{
		float result = 0f;
		string s = "0";
		if (obj != null)
		{
			s = obj.ToString();
		}
		float.TryParse(s, out result);
		return result;
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

	public void ReadEpcInfo(InsDeviceManager myDevMgr)
	{
		epcDevR = myDevMgr.epcDev2;
	}

	public void WriteEpcInfo(InsDeviceManager myDevMgr)
	{
		int currentChannelIdx = cdlMgr.CurrentChannelIdx;
		int selectedIndex = tabControl2.SelectedIndex;
		myDevMgr.epcDev2 = epcDev2;
	}

	public void ReadEpcInfo(InsDeviceManager myDevMgr, int int_7, byte Type)
	{
		int linkepcParasNumber = GetLinkepcParasNumber(Type);
		if ((!radioButton1.Checked || linkepcParasNumber == 0) && (!radioButton2.Checked || linkepcParasNumber == 1) && (!radioButton3.Checked || linkepcParasNumber == 2))
		{
			switch (myDevMgr.epcDev0[int_7].gasType & 0xF)
			{
			case 0:
				comboBox6.Text = Lang.PS("氮气", "N2");
				break;
			case 1:
				comboBox6.Text = Lang.PS("氢气", "H2");
				break;
			case 2:
				comboBox6.Text = Lang.PS("空气", "Air");
				break;
			case 3:
				comboBox6.Text = Lang.PS("氦气", "He");
				break;
			case 4:
				comboBox6.Text = Lang.PS("氩气", "Ar");
				break;
			}
			maskedTextBox8.Text = myDevMgr.epcDev0[int_7].chromColLenth.ToString();
			maskedTextBox9.Text = myDevMgr.epcDev0[int_7].chromColDiameter.ToString();
			maskedTextBox5.Text = myDevMgr.epcDev0[int_7].initTime.ToString();
			if (myDevMgr.epcDev0[int_7].ctrlModel == 0)
			{
				radioButton6.Checked = true;
				label10.Text = Lang.PS("压力", "Press");
				maskedTextBox4.Text = myDevMgr.epcDev0[int_7].pressureData.ToString();
			}
			if (myDevMgr.epcDev0[int_7].ctrlModel == 1)
			{
				radioButton5.Checked = true;
				label10.Text = Lang.PS("流量", "Flow");
				maskedTextBox4.Text = myDevMgr.epcDev0[int_7].pressureData.ToString();
			}
			if (myDevMgr.epcDev0[int_7].ctrlModel == 2)
			{
				radioButton25.Checked = true;
				label10.Text = Lang.PS("分流", "Bypass");
				maskedTextBox4.Text = myDevMgr.epcDev0[int_7].pressureData.ToString();
			}
			dgInsamp1.Rows.Clear();
			for (int i = 0; i < myDevMgr.epcDev0[int_7].tempSettingTable.Count; i++)
			{
				dgInsamp1.Rows.Add((i + 1).ToString(), myDevMgr.epcDev0[int_7].tempSettingTable[i].tempStart.ToString(), myDevMgr.epcDev0[int_7].tempSettingTable[i].tempEnd.ToString(), myDevMgr.epcDev0[int_7].tempSettingTable[i].tempKeep.ToString());
			}
		}
	}

	public void WriteEpcInfo(InsDeviceManager myDevMgr, int idxSelTab)
	{
		byte epcSplitRatio = GetEpcSplitRatio();
		if (myDevMgr.epcDev0[idxSelTab] == null)
		{
			myDevMgr.epcDev0[idxSelTab] = new EpcDeviceSetting();
		}
		myDevMgr.epcDev0[idxSelTab].splitRatio = epcSplitRatio;
		myDevMgr.epcDev0[idxSelTab].gasType = GetEpcGasType();
		myDevMgr.epcDev0[idxSelTab].initTime = Class49.String2Float(maskedTextBox5.Text, 0f);
		myDevMgr.epcDev0[idxSelTab].pressureData = Class49.String2Float(maskedTextBox4.Text, 0f);
		if (radioButton6.Checked)
		{
			myDevMgr.epcDev0[idxSelTab].ctrlModel = 0;
		}
		if (radioButton5.Checked)
		{
			myDevMgr.epcDev0[idxSelTab].ctrlModel = 1;
		}
		if (radioButton25.Checked)
		{
			myDevMgr.epcDev0[idxSelTab].ctrlModel = 2;
		}
		myDevMgr.epcDev0[idxSelTab].chromColLenth = Class49.String2Float(maskedTextBox8.Text, 0f);
		myDevMgr.epcDev0[idxSelTab].chromColDiameter = Class49.String2Float(maskedTextBox9.Text, 0f);
		for (int i = 0; i < dgInsamp1.RowCount; i++)
		{
			myDevMgr.epcDev0[idxSelTab].tempSettingTable[i].tempStart = Class49.String2Float(dgInsamp1.Rows[i].Cells[1].Value, 0f);
			myDevMgr.epcDev0[idxSelTab].tempSettingTable[i].tempEnd = Class49.String2Float(dgInsamp1.Rows[i].Cells[2].Value, 0f);
			myDevMgr.epcDev0[idxSelTab].tempSettingTable[i].tempKeep = Class49.String2Float(dgInsamp1.Rows[i].Cells[3].Value, 0f);
		}
	}

	public void ReadTempratureControl(InsDeviceManager myDevMgr)
	{
		tbptIniTempHoldT.Text = myDevMgr.tempHoldTime.ToString("0.0");
		MtdSetup mtdMgr = cdlMgr.CurrentChartParaOpera.mtdMgr;
		mtdMgr.chromInfoR.GcProgTemp.initHoldTime = myDevMgr.tempHoldTime;
		for (int i = 0; i < TempProgram; i++)
		{
			gdTempControl.Rows[i].Cells[1].Value = myDevMgr.tempSettingList[i].tempStart;
			gdTempControl.Rows[i].Cells[2].Value = (int)Math.Floor(myDevMgr.tempSettingList[i].tempEnd);
			gdTempControl.Rows[i].Cells[3].Value = myDevMgr.tempSettingList[i].tempKeep;
			if (i < mtdMgr.chromInfoR.GcProgTemp.progTempRows.Length)
			{
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[i].upRate = myDevMgr.tempSettingList[i].tempStart;
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[i].holdTime = (int)Math.Floor(myDevMgr.tempSettingList[i].tempEnd);
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[i].endTemp = myDevMgr.tempSettingList[i].tempStart;
			}
		}
	}

	public void WriteTempratureControl(InsDeviceManager myDevMgr)
	{
		myDevMgr.tempHoldTime = String2Float(tbptIniTempHoldT.Text);
		for (int i = 0; i < gdTempControl.RowCount; i++)
		{
			myDevMgr.tempSettingList[i].tempStart = String2Float(gdTempControl.Rows[i].Cells[1].Value);
			myDevMgr.tempSettingList[i].tempEnd = String2Float(gdTempControl.Rows[i].Cells[2].Value);
			myDevMgr.tempSettingList[i].tempKeep = String2Float(gdTempControl.Rows[i].Cells[3].Value);
		}
	}

	public void ReadInjectNumList(InsDeviceManager myDevMgr)
	{
		try
		{
			if (myDevMgr.injectNumList.Length != 0)
			{
				for (int i = 0; i < myDevMgr.injectNumList.Length; i++)
				{
					if (i < tabControl2.TabPages.Count)
					{
						tabControl2.TabPages[i].Tag = myDevMgr.injectNumList[i];
					}
				}
			}
			else
			{
				tabControl2.TabPages[0].Text = Lang.PS("进样1", "INJ1");
				tabControl2.TabPages[1].Text = Lang.PS("进样2", "INJ2");
				tabControl2.TabPages[2].Text = Lang.PS("进样3", "INJ3");
			}
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("InsDeviceCtrl.ReadInjectNumList(InsDeviceManager myDevMgr)" + ex.Message);
		}
	}

	public void ReadHardVersion(InsDeviceManager myDevMgr)
	{
		gvHardVersion.Rows.Clear();
		for (int i = 0; i < myDevMgr.netDevList.Count; i++)
		{
			gvHardVersion.Rows.Add(new DataGridViewRow());
			gvHardVersion.Rows[i].Cells[0].Value = (i + 1).ToString();
			gvHardVersion.Rows[i].Cells[1].Value = myDevMgr.netDevList[i].ToString();
		}
	}

	public void WriteHardVersion(InsDeviceManager myDevMgr)
	{
		myDevMgr.injectInterval = Class49.String2Float(maskedTextBox20.Text, 0f);
		myDevMgr.injectNTimes = Class49.String2Float(maskedTextBox19.Text, 0f);
		if (checkBox6.Checked)
		{
			myDevMgr.injectSpendTime = 16f;
		}
		else
		{
			myDevMgr.injectSpendTime = 0f;
		}
		myDevMgr.injectLightTime = Class49.String2Float(maskedTextBox14.Text, 99f);
	}

	public void SetFromPictureBoxImage(Class44 class44_0)
	{
		if (imageList1.Images.Count < 22)
		{
			LogMgr.Instance.LogError("TcpServerSocket.SetFromPictureBoxImage Error: this.imageList1.Images.Count" + imageList1.Images.Count);
			return;
		}
		if (class44_0.bool_12)
		{
			pictureBox1.Image = imageList1.Images[20];
		}
		else
		{
			pictureBox1.Image = imageList1.Images[21];
		}
		if (class44_0.bool_10)
		{
			pictureBox2.Image = imageList1.Images[20];
		}
		else
		{
			pictureBox2.Image = imageList1.Images[21];
		}
		if (class44_0.bool_11)
		{
			pictureBox3.Image = imageList1.Images[20];
		}
		else
		{
			pictureBox3.Image = imageList1.Images[21];
		}
		if (class44_0.bool_9)
		{
			pictureBox4.Image = imageList1.Images[20];
		}
		else
		{
			pictureBox4.Image = imageList1.Images[21];
		}
		if (class44_0.bool_8)
		{
			pictureBox5.Image = imageList1.Images[20];
		}
		else
		{
			pictureBox5.Image = imageList1.Images[21];
		}
	}

	public void WriteSmsSetiingInfo(InsDeviceManager myDevMgr)
	{
		checkBox9.Checked = myDevMgr.Msg.AutoSendByStopTime;
		textBox8.Text = myDevMgr.Msg.Mess;
		checkBox7.Checked = myDevMgr.Msg.sound;
		comboBox13.Text = myDevMgr.Msg.soundTimes.ToString();
	}

	private float String2Float(object object_0)
	{
		if (object_0 == null)
		{
			throw new Exception("相关字段没有赋值");
		}
		return float.Parse(object_0.ToString());
	}

	public void UpdateControlTempText(bool bCtrl)
	{
		if (bCtrl)
		{
			bControlTemp.Text = Lang.PS("关闭控温", "Stop Temp");
		}
		else
		{
			bControlTemp.Text = Lang.PS("开始控温", "Start Temp");
		}
	}

	public void UpdateControlAnalyzeText(bool bCtrl)
	{
		if (bCtrl)
		{
			button14.Text = Lang.PS("结束分析", "Stop All");
		}
		else
		{
			button14.Text = Lang.PS("开始分析", "Start All");
		}
        if (LYTHCtrl2.selfCtrl != null)
        {
            LYTHCtrl2.selfCtrl.UpdateControlAnalyzeText(bCtrl);
        }
    }

	public void UpdateEpcInfo(int idx, byte myByte, string str0, string str1, string str2, double mydouble, byte[] mybyte)
	{
		string value = Lang.PS("--", "--");
		string value2 = Lang.PS("--", "--");
		if (mydouble == 655.35)
		{
			mydouble = 0.0;
		}
		if (idx == 9)
		{
			tbColP2.Text = str1;
		}
		if (idx == 10)
		{
			tbHHP2.Text = str1;
		}
		if (idx == 11)
		{
			tbAirP2.Text = str1;
		}
		try
		{
			switch (idx)
			{
			case 0:
			{
				for (int j = 0; j < 8; j++)
				{
					if (mybyte.Length > 145 + j)
					{
						epcDev2[0].setData[j] = mybyte[145 + j];
					}
				}
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[6].setV = str0;
					FormMainPortable.fromMain.lstSource[6].curV = str1;
				}
				break;
			}
			case 1:
				epcDev2[1].setData[0] = mybyte[11];
				epcDev2[1].setData[1] = mybyte[12];
				float.TryParse(str0, out epcDev2[1].pressureData);
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[4].setV = str0;
					FormMainPortable.fromMain.lstSource[4].curV = str2;
				}
				break;
			case 2:
				epcDev2[2].setData[0] = mybyte[20];
				epcDev2[2].setData[1] = mybyte[21];
				float.TryParse(str0, out epcDev2[2].pressureData);
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[5].setV = str0;
					FormMainPortable.fromMain.lstSource[5].curV = str2;
				}
				break;
			case 3:
			{
				for (int i = 0; i < 8; i++)
				{
					if (mybyte.Length > 153 + i)
					{
						epcDev2[3].setData[i] = mybyte[153 + i];
					}
				}
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[7].setV = str0;
					FormMainPortable.fromMain.lstSource[7].curV = str1;
				}
				tbColP3.Text = str1;
				tbColPreSet2.Text = str0;
				tbColPreCur2.Text = str1;
				break;
			}
			case 4:
				epcDev2[4].setData[0] = mybyte[38];
				epcDev2[4].setData[1] = mybyte[39];
				float.TryParse(str0, out epcDev2[4].pressureData);
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.lstSource[8].setV = str0;
					FormMainPortable.fromMain.lstSource[8].curV = str2;
				}
				tbColP4.Text = str1;
				tbHHSet2.Text = str0;
				tbHHCur2.Text = str2;
				break;
			case 5:
				epcDev2[5].setData[0] = mybyte[47];
				epcDev2[5].setData[1] = mybyte[48];
				float.TryParse(str0, out epcDev2[5].pressureData);
				tbAirSet2.Text = str0;
				tbAirCur2.Text = str2;
				break;
			case 6:
				epcDev2[6].setData[0] = mybyte[56];
				epcDev2[6].setData[1] = mybyte[57];
				break;
			case 7:
				epcDev2[7].setData[0] = mybyte[65];
				epcDev2[7].setData[1] = mybyte[66];
				float.TryParse(str0, out epcDev2[7].pressureData);
				break;
			case 8:
				epcDev2[8].setData[0] = mybyte[74];
				epcDev2[8].setData[1] = mybyte[75];
				float.TryParse(str0, out epcDev2[8].pressureData);
				break;
			case 9:
				epcDev2[9].setData[0] = mybyte[83];
				epcDev2[9].setData[1] = mybyte[84];
				float.TryParse(str0, out epcDev2[9].pressureData);
				break;
			case 10:
				epcDev2[10].setData[0] = mybyte[92];
				epcDev2[10].setData[1] = mybyte[93];
				float.TryParse(str0, out epcDev2[10].pressureData);
				break;
			case 11:
				epcDev2[11].setData[0] = mybyte[101];
				epcDev2[11].setData[1] = mybyte[102];
				float.TryParse(str0, out epcDev2[11].pressureData);
				break;
			case 12:
				epcDev2[12].setData[0] = mybyte[110];
				epcDev2[12].setData[1] = mybyte[111];
				float.TryParse(str0, out epcDev2[12].pressureData);
				break;
			case 13:
				epcDev2[13].setData[0] = mybyte[119];
				epcDev2[13].setData[1] = mybyte[120];
				float.TryParse(str0, out epcDev2[13].pressureData);
				break;
			case 14:
				epcDev2[14].setData[0] = mybyte[128];
				epcDev2[14].setData[1] = mybyte[129];
				float.TryParse(str0, out epcDev2[14].pressureData);
				break;
			}
		}
		catch (Exception)
		{
		}
		switch (tabControl2.SelectedIndex)
		{
		case 0:
			if (idx == 0)
			{
				EPCControl.Rows[0].Cells[1].Value = str0;
				EPCControl.Rows[0].Cells[2].Value = str1;
				EPCControl.Rows[0].Cells[3].Value = str2;
				tbColPreSet1.Text = str0;
				tbColPreCur1.Text = str1;
				tbColP1.Text = str1;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[0].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[0].Cells[4].Value = value2;
				}
			}
			if (idx == 1)
			{
				EPCControl.Rows[1].Cells[1].Value = str0;
				EPCControl.Rows[1].Cells[2].Value = str1;
				EPCControl.Rows[1].Cells[3].Value = str2;
				tbHHSet1.Text = str0;
				tbHHCur1.Text = str2;
				tbHHP1.Text = str1;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[1].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[1].Cells[4].Value = value2;
				}
			}
			if (idx == 2)
			{
				EPCControl.Rows[2].Cells[1].Value = str0;
				EPCControl.Rows[2].Cells[2].Value = str1;
				EPCControl.Rows[2].Cells[3].Value = str2;
				tbAirSet1.Text = str0;
				tbAirCur1.Text = str2;
				tbAirP1.Text = str1;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[2].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[2].Cells[4].Value = value2;
				}
			}
			if (idx == 0)
			{
				double0 = mydouble;
				if (double0 == 0.0)
				{
					double0 = 1.0;
				}
			}
			if (idx == 1)
			{
				lFliuBi.Text = (int)(mydouble / double0 + 0.5) + "：1";
			}
			break;
		case 1:
			if (idx == 3)
			{
				EPCControl.Rows[0].Cells[1].Value = str0;
				EPCControl.Rows[0].Cells[2].Value = str1;
				EPCControl.Rows[0].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[0].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[0].Cells[4].Value = value2;
				}
			}
			if (idx == 4)
			{
				EPCControl.Rows[1].Cells[1].Value = str0;
				EPCControl.Rows[1].Cells[2].Value = str1;
				EPCControl.Rows[1].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[1].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[1].Cells[4].Value = value2;
				}
			}
			if (idx == 5)
			{
				EPCControl.Rows[2].Cells[1].Value = str0;
				EPCControl.Rows[2].Cells[2].Value = str1;
				EPCControl.Rows[2].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[2].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[2].Cells[4].Value = value2;
				}
			}
			if (idx == 3)
			{
				double0 = mydouble;
				if (double0 == 0.0)
				{
					double0 = 1.0;
				}
			}
			if (idx == 4)
			{
				lFliuBi.Text = (int)(mydouble / double0 + 0.5) + "：1";
			}
			break;
		case 2:
			if (idx == 6)
			{
				EPCControl.Rows[0].Cells[1].Value = str0;
				EPCControl.Rows[0].Cells[2].Value = str1;
				EPCControl.Rows[0].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[0].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[0].Cells[4].Value = value2;
				}
			}
			if (idx == 7)
			{
				EPCControl.Rows[1].Cells[1].Value = str0;
				EPCControl.Rows[1].Cells[2].Value = str1;
				EPCControl.Rows[1].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[1].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[1].Cells[4].Value = value2;
				}
			}
			if (idx == 8)
			{
				EPCControl.Rows[2].Cells[1].Value = str0;
				EPCControl.Rows[2].Cells[2].Value = str1;
				EPCControl.Rows[2].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[2].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[2].Cells[4].Value = value2;
				}
			}
			if (idx == 6)
			{
				double0 = mydouble;
				if (double0 == 0.0)
				{
					double0 = 1.0;
				}
			}
			if (idx == 7)
			{
				lFliuBi.Text = (int)(mydouble / double0 + 0.5) + "：1";
			}
			break;
		case 3:
			if (idx == 9)
			{
				EPCControl.Rows[0].Cells[1].Value = str0;
				EPCControl.Rows[0].Cells[2].Value = str1;
				EPCControl.Rows[0].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[0].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[0].Cells[4].Value = value2;
				}
			}
			if (idx == 10)
			{
				EPCControl.Rows[1].Cells[1].Value = str0;
				EPCControl.Rows[1].Cells[2].Value = str1;
				EPCControl.Rows[1].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[1].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[1].Cells[4].Value = value2;
				}
			}
			if (idx == 11)
			{
				EPCControl.Rows[2].Cells[1].Value = str0;
				EPCControl.Rows[2].Cells[2].Value = str1;
				EPCControl.Rows[2].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[2].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[2].Cells[4].Value = value2;
				}
			}
			if (idx == 10)
			{
				double0 = mydouble;
				if (double0 == 0.0)
				{
					double0 = 1.0;
				}
			}
			if (idx == 11)
			{
				lFliuBi.Text = (int)(mydouble / double0 + 0.5) + "：1";
			}
			break;
		case 4:
			if (idx == 12)
			{
				EPCControl.Rows[0].Cells[1].Value = str0;
				EPCControl.Rows[0].Cells[2].Value = str1;
				EPCControl.Rows[0].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[0].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[0].Cells[4].Value = value2;
				}
			}
			if (idx == 13)
			{
				EPCControl.Rows[1].Cells[1].Value = str0;
				EPCControl.Rows[1].Cells[2].Value = str1;
				EPCControl.Rows[1].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[1].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[1].Cells[4].Value = value2;
				}
			}
			if (idx == 14)
			{
				EPCControl.Rows[2].Cells[1].Value = str0;
				EPCControl.Rows[2].Cells[2].Value = str1;
				EPCControl.Rows[2].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[2].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[2].Cells[4].Value = value2;
				}
			}
			if (idx == 12)
			{
				double0 = mydouble;
				if (double0 == 0.0)
				{
					double0 = 1.0;
				}
			}
			if (idx == 13)
			{
				lFliuBi.Text = (int)(mydouble / double0 + 0.5) + "：1";
			}
			break;
		case 5:
			if (idx == 15)
			{
				EPCControl.Rows[0].Cells[1].Value = str0;
				EPCControl.Rows[0].Cells[2].Value = str1;
				EPCControl.Rows[0].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[0].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[0].Cells[4].Value = value2;
				}
			}
			if (idx == 16)
			{
				EPCControl.Rows[1].Cells[1].Value = str0;
				EPCControl.Rows[1].Cells[2].Value = str1;
				EPCControl.Rows[1].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[1].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[1].Cells[4].Value = value2;
				}
			}
			if (idx == 17)
			{
				EPCControl.Rows[2].Cells[1].Value = str0;
				EPCControl.Rows[2].Cells[2].Value = str1;
				EPCControl.Rows[2].Cells[3].Value = str2;
				if ((myByte & 0x80) == 128)
				{
					radioButton28.Checked = true;
					EPCControl.Rows[2].Cells[4].Value = value;
				}
				else
				{
					radioButton28.Checked = false;
					EPCControl.Rows[2].Cells[4].Value = value2;
				}
			}
			if (idx == 15)
			{
				double0 = mydouble;
				if (double0 == 0.0)
				{
					double0 = 1.0;
				}
			}
			if (idx == 16)
			{
				lFliuBi.Text = (int)(mydouble / double0 + 0.5) + "：1";
			}
			break;
		}
	}

	public byte GetEpcSplitRatio()
	{
		int currentEpcIdx = CurrentEpcIdx;
		byte result = 0;
		switch (currentEpcIdx)
		{
		case 0:
			if (radioButton1.Checked)
			{
				result = 48;
			}
			if (radioButton2.Checked)
			{
				result = 49;
			}
			if (radioButton3.Checked)
			{
				result = 50;
			}
			break;
		case 1:
			if (radioButton1.Checked)
			{
				result = 51;
			}
			if (radioButton2.Checked)
			{
				result = 52;
			}
			if (radioButton3.Checked)
			{
				result = 53;
			}
			break;
		case 2:
			if (radioButton1.Checked)
			{
				result = 54;
			}
			if (radioButton2.Checked)
			{
				result = 55;
			}
			if (radioButton3.Checked)
			{
				result = 56;
			}
			break;
		case 3:
			if (radioButton1.Checked)
			{
				result = 57;
			}
			if (radioButton2.Checked)
			{
				result = 58;
			}
			if (radioButton3.Checked)
			{
				result = 59;
			}
			break;
		case 4:
			if (radioButton1.Checked)
			{
				result = 60;
			}
			if (radioButton2.Checked)
			{
				result = 61;
			}
			if (radioButton3.Checked)
			{
				result = 62;
			}
			break;
		case 5:
			if (radioButton1.Checked)
			{
				result = 63;
			}
			if (radioButton2.Checked)
			{
				result = 64;
			}
			if (radioButton3.Checked)
			{
				result = 65;
			}
			break;
		}
		return result;
	}

	public byte GetEpcGasType()
	{
		byte result = 0;
		string text = comboBox6.Text.Trim();
		if (!(text == Lang.PS("氮气", "N2")))
		{
			if (!(text == Lang.PS("氢气", "H2")))
			{
				if (!(text == Lang.PS("空气", "Air")))
				{
					if (!(text == Lang.PS("氦气", "He")))
					{
						if (text == Lang.PS("氩气", "Ar"))
						{
							result = 4;
						}
					}
					else
					{
						result = 3;
					}
				}
				else
				{
					result = 2;
				}
			}
			else
			{
				result = 1;
			}
		}
		else
		{
			result = 0;
		}
		return result;
	}

	public string GetTempratureTableTextInfo(InsDeviceManager myDevMgr)
	{
		string text = "";
		for (int i = 6; i < dgtempControl.RowCount; i++)
		{
			string text2 = text;
			text = text2 + (i - 6) + "路,设置:" + myDevMgr.tempSetedList[i - 6] + ",保护:" + myDevMgr.tempProtectList[i - 6] + "\r\n";
		}
		return text;
	}

	public string GetTempratureControlTextInfo(InsDeviceManager myDevMgr)
	{
		string text = "";
		text = text + "初始温度:" + myDevMgr.tempHoldTime;
		for (int i = 0; i < gdTempControl.RowCount; i++)
		{
			string text2 = text;
			text = text2 + i + "升温速率:" + myDevMgr.tempSettingList[i].tempStart + ",结束温度:" + myDevMgr.tempSettingList[i].tempEnd + ",保持时间:" + myDevMgr.tempSettingList[i].tempKeep + "\r\n";
		}
		return text;
	}

	public string GetExtEvTPTextInfo(InsDeviceManager myDevMgr)
	{
		string text = "";
		for (int i = 0; i < gvExtEvTP.RowCount; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				text = text + i + j + myDevMgr.eventCtrl0[j][i];
			}
			text += "\r\n";
		}
		return text;
	}

	public void SetLiquidInfo(byte[] byte_0)
	{
		switch (byte_0[0])
		{
		case 0:
			rliquid.Checked = false;
			rpass.Checked = true;
			break;
		case 1:
			rliquid.Checked = true;
			rpass.Checked = false;
			break;
		}
		liquidStatus.Text = "";
		Maxliquidpress.Text = IBrainConvert.ByteArray2Float(byte_0, 1, 1).ToString();
		Minliquidpress.Text = IBrainConvert.ByteArray2Float(byte_0, 3, 1).ToString();
		liquidpass.Text = IBrainConvert.ByteArray2Float(byte_0, 5, 2).ToString();
		liquidpress.Text = IBrainConvert.ByteArray2Float(byte_0, 7, 1).ToString();
	}

	public byte[] GetLiquidInfo()
	{
		byte[] array = new byte[16];
		if (rpass.Checked)
		{
			array[0] = 1;
		}
		byte[] array2 = new byte[2];
		array2 = IBrainConvert.Float2Byte(Class49.String2Float(Maxliquidpress.Text, 0f), 1);
		array2.CopyTo(array, 1);
		array2 = IBrainConvert.Float2Byte(Class49.String2Float(Minliquidpress.Text, 0f), 1);
		array2.CopyTo(array, 3);
		array2 = IBrainConvert.Float2Byte(Class49.String2Float(liquidpass.Text, 0f), 2);
		array2.CopyTo(array, 5);
		array2 = IBrainConvert.Float2Byte(Class49.String2Float(liquidpress.Text, 0f), 1);
		array2.CopyTo(array, 7);
		return array;
	}

	public void SetLiquidState(byte[] byte_0)
	{
		string text = "";
		switch (byte_0[0])
		{
		case 0:
			text = Lang.PS("关闭", "close");
			break;
		case 1:
			text = Lang.PS("打开", "open");
			break;
		case 128:
			text = Lang.PS("泵未安装", "Pump not installed");
			break;
		case 129:
			text = Lang.PS("超压报警", "Overpressure alarm");
			break;
		case 130:
			text = Lang.PS("低压报警", "Lowpressure alarm");
			break;
		case 131:
			text = Lang.PS("其他错误", "otherErrors");
			break;
		}
		text += "\r\n";
		text = text + Lang.PS("流量:", "flow:") + IBrainConvert.ByteArray2Float(byte_0, 1, 2) + "\r\n";
		text = text + Lang.PS("压力:", "Press:") + IBrainConvert.ByteArray2Float(byte_0, 3, 1);
		liquidStatus.Text = text;
	}

	public void SetEventSwitchInfo(byte[] byte_0)
	{
		checkBox_0.Checked = (byte_0[25] & 1) == 1;
		checkBox_1.Checked = (byte_0[25] & 2) == 2;
		checkBox_3.Checked = (byte_0[25] & 4) == 4;
		checkBox_5.Checked = (byte_0[25] & 8) == 8;
		checkBox_2.Checked = (byte_0[25] & 0x10) == 16;
		checkBox_4.Checked = (byte_0[25] & 0x20) == 32;
		checkBox_6.Checked = (byte_0[25] & 0x40) == 64;
		checkBox_7.Checked = (byte_0[25] & 0x80) == 128;
		if (checkBox_0.Checked)
		{
			cdlMgr.formMain.iChannel = 1;
		}
		else if (checkBox_1.Checked)
		{
			cdlMgr.formMain.iChannel = 2;
		}
		else if (checkBox_3.Checked)
		{
			cdlMgr.formMain.iChannel = 3;
		}
		else if (checkBox_5.Checked)
		{
			cdlMgr.formMain.iChannel = 4;
		}
		else if (checkBox_2.Checked)
		{
			cdlMgr.formMain.iChannel = 5;
		}
		else if (checkBox_4.Checked)
		{
			cdlMgr.formMain.iChannel = 6;
		}
	}

	public byte[] GetSmsSetiingInfo()
	{
		string s = textBox8.Text.Trim();
		byte[] bytes = Encoding.Default.GetBytes(s);
		byte[] array = new byte[3];
		if (checkBox7.Checked)
		{
			array[0] = 1;
		}
		else
		{
			array[0] = 0;
		}
		array[1] = (byte)Class49.Object2Int(comboBox13.Text, 1);
		byte[] array2;
		if (bytes.Length % 24 == 0)
		{
			array[2] = (byte)(int)Math.Ceiling((double)(bytes.Length / 24));
			array2 = bytes;
		}
		else
		{
			array[2] = (byte)(Math.Ceiling((double)(bytes.Length / 24)) + 1.0);
			array2 = new byte[array[2] * 24];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = 32;
			}
			bytes.CopyTo(array2, 0);
		}
		byte[] array3 = new byte[array2.Length + array.Length];
		array.CopyTo(array3, 0);
		array2.CopyTo(array3, 3);
		return array3;
	}

	public byte[] GetSmsAlarm()
	{
		byte b = 0;
		if (cbAlarm.Checked)
		{
			b = 1;
		}
		return new byte[1] { b };
	}

	public void SetSmsAlarm(bool bcheck)
	{
		cbAlarm.Checked = bcheck;
	}

	public void SetFlowState(bool mybool)
	{
		button27.Text = (mybool ? Lang.PS("流量正常", "Normal flow") : Lang.PS("流量节省", "save flow"));
	}

	public byte[] GetFlowState()
	{
		byte[] array = new byte[1];
		byte[] array2 = array;
		if (button27.Text == Lang.PS("流量节省", "save flow"))
		{
			array2[0] = 1;
		}
		return array2;
	}

	public void SetFormTextBoxOnLineState(InsDeviceManager myDevMgr)
	{
		if (myDevMgr.injectConnState == "000")
		{
			textBox4.Text = Lang.PS("离线", "OffLine");
		}
		else
		{
			textBox4.Text = Lang.PS("在线", "OnLine");
		}
		if (myDevMgr.injectWorkState == "000")
		{
			textBox5.Text = Lang.PS("取样", "Sample");
		}
		else
		{
			textBox5.Text = Lang.PS("空闲", "Free");
		}
		textBox6.Text = myDevMgr.injectBotNum;
		textBox7.Text = myDevMgr.injectNeedleNum;
	}

	public void AddMessList(string strMess)
	{
		listMess.Items.Add(DateTime.Now.ToString() + strMess + "——" + strMess);
		if (listMess.Items.Count > 100)
		{
			listMess.Items.RemoveAt(0);
		}
		listMess.SetSelected(listMess.Items.Count - 1, value: true);
	}

	public void ClearEpcCtrl()
	{
		for (int i = 0; i < EPCControl.Rows.Count; i++)
		{
			for (int j = 1; j < EPCControl.Columns.Count; j++)
			{
				EPCControl.Rows[i].Cells[j].Value = "000.0";
			}
		}
	}

	public int GetLinkepcParasNumber(byte Type)
	{
		int result = 0;
		switch (Type)
		{
		case 48:
			result = 0;
			break;
		case 49:
			result = 1;
			break;
		case 50:
			result = 2;
			break;
		case 51:
			result = 0;
			break;
		case 52:
			result = 1;
			break;
		case 53:
			result = 2;
			break;
		case 54:
			result = 0;
			break;
		case 55:
			result = 1;
			break;
		case 56:
			result = 2;
			break;
		case 57:
			result = 0;
			break;
		case 58:
			result = 1;
			break;
		case 59:
			result = 2;
			break;
		case 60:
			result = 0;
			break;
		case 61:
			result = 1;
			break;
		case 62:
			result = 2;
			break;
		case 63:
			result = 0;
			break;
		case 64:
			result = 1;
			break;
		case 65:
			result = 2;
			break;
		}
		return result;
	}

	public void addTabpage(TabPage tabPage)
	{
		tabControl1.Controls.Add(tabPage);
		TabPage[] array = new TabPage[tabControl1.TabCount];
		int num = 0;
		foreach (TabPage tabPage2 in tabControl1.TabPages)
		{
			array[num++] = tabPage2;
		}
		tabControl1.TabPages.Clear();
		tabControl1.Controls.Add(array[array.Length - 1]);
		for (int i = 0; i < array.Length - 1; i++)
		{
			tabControl1.Controls.Add(array[i]);
		}
	}

	public void addTabpage(TabPage tabPage, int index)
	{
		if (index == 0)
		{
			tabControl1.Controls.Add(tabPage);
			return;
		}
		tabControl1.Controls.Add(tabPage);
		TabPage[] array = new TabPage[tabControl1.TabCount];
		int num = 0;
		foreach (TabPage tabPage2 in tabControl1.TabPages)
		{
			array[num++] = tabPage2;
		}
		tabControl1.TabPages.Clear();
		tabControl1.Controls.Add(array[array.Length - 1]);
		for (int i = 0; i < array.Length - 1; i++)
		{
			tabControl1.Controls.Add(array[i]);
		}
	}

	private new void Load()
	{
		dgtempControl.Rows.Add(Lang.PS("进样1", "INJ1"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("柱炉", "COL"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("检测1", "DET"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("进样2", "INJ2"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("检测2", "DET2"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("辅助", "AUX"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("辅助 3", "AUX3"), "000.0", 0, 0);
		dgtempControl.Rows.Add(Lang.PS("柱炉 2", "COL2"), "000.0", 0, 0);
		EPCControl.Rows.Add(Lang.PS("载气", "press"), "000.0", "000.0", "000.0");
		if (frmParam.iDetector != 0)
		{
			EPCControl.Rows.Add(Lang.PS("样品", "flow"), "000.0", "000.0", "000.0");
			EPCControl.Rows.Add(Lang.PS("驱动气", "bpass"), "000.0", "000.0", "000.0");
		}
		else
		{
			EPCControl.Rows.Add(Lang.PS("分流", "flow"), "000.0", "000.0", "000.0");
			EPCControl.Rows.Add(Lang.PS("吹扫", "bpass"), "000.0", "000.0", "000.0");
		}
		for (int i = 1; i < 5; i++)
		{
			dgInsamp1.Rows.Add(i + Lang.PS("阶"), "000.0", "000.0", "000.0");
		}
		for (int j = 1; j < 17; j++)
		{
			gdTempControl.Rows.Add(j.ToString(), "000.0", "000.0", "000.0");
		}
		for (int k = 1; k < 9; k++)
		{
			gvExtEvTP.Rows.Add(k.ToString(), "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00");
		}
		for (int l = 1; l < 5; l++)
		{
			dgGramset.Rows.Add("", "", "", "");
		}
		for (int m = 0; m < dgGramset.ColumnCount; m++)
		{
			dgGramset.Columns[m].ReadOnly = false;
		}
		for (int n = 0; n < gvExtEvTP.ColumnCount; n++)
		{
			gvExtEvTP.Columns[n].ReadOnly = false;
		}
		gdTempControl.Columns[1].ReadOnly = false;
		gdTempControl.Columns[2].ReadOnly = false;
		gdTempControl.Columns[3].ReadOnly = false;
		dgtempControl.Columns[0].ReadOnly = true;
		dgtempControl.Columns[1].ReadOnly = true;
		dgtempControl.ClearSelection();
		dgInsamp1.Columns[1].ReadOnly = false;
		dgInsamp1.Columns[2].ReadOnly = false;
		dgInsamp1.Columns[3].ReadOnly = false;
		comboBox4.SelectedIndex = 0;
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		Regex regex = new Regex("^[1-2]\\d+\\.(\\d+.){2}(([1-2](\\d){2})|((\\d){2})|\\d)");
		if (hostEntry.AddressList != null && hostEntry.AddressList.Length != 0)
		{
			for (int num = 0; num < hostEntry.AddressList.Length; num++)
			{
				Match match = regex.Match(hostEntry.AddressList[num].ToString());
				if (match.Success)
				{
					IP4.Text = match.Groups[0].Value;
					break;
				}
			}
		}
		tBshuaijian.Text = frmParam.fShuaijian.ToString();
		tBshuaijian2.Text = frmParam.fShuaijian2.ToString();
		tBshuaijian3.Text = frmParam.fShuaijian3.ToString();
		tbKRName.Text = frmParam.strName;
		tbSmooth.Text = frmParam.iSmooths.ToString();
		tbMount41.Text = frmParam.fmount41.ToString();
		tbMount42.Text = frmParam.fmount42.ToString();
		tbMount43.Text = frmParam.fmount43.ToString();
		tbMount44.Text = frmParam.fmount44.ToString();
		tbMount45.Text = frmParam.fmount45.ToString();
		tbMount46.Text = frmParam.fmount46.ToString();
		tbMount47.Text = frmParam.fmount47.ToString();
		tbMount48.Text = frmParam.fmount48.ToString();
		tbMount49.Text = frmParam.fmount49.ToString();
		tbMount410.Text = frmParam.fmount410.ToString();
		tbMount411.Text = frmParam.fmount411.ToString();
		tbMount412.Text = frmParam.fmount412.ToString();
		tbMount201.Text = frmParam.fmount201.ToString();
		tbMount202.Text = frmParam.fmount202.ToString();
		tbMount203.Text = frmParam.fmount203.ToString();
		tbMount204.Text = frmParam.fmount204.ToString();
		tbMount205.Text = frmParam.fmount205.ToString();
		tbMount206.Text = frmParam.fmount206.ToString();
		tbMount207.Text = frmParam.fmount207.ToString();
		tbMount208.Text = frmParam.fmount208.ToString();
		tbMount209.Text = frmParam.fmount209.ToString();
		tbMount2010.Text = frmParam.fmount2010.ToString();
		tbMount2011.Text = frmParam.fmount2011.ToString();
		tbMount2012.Text = frmParam.fmount2012.ToString();
		tbCompen1.Text = frmParam.fCompen1.ToString();
		tbCompen2.Text = frmParam.fCompen2.ToString();
		tbCompen3.Text = frmParam.fCompen3.ToString();
		tbCompen4.Text = frmParam.fCompen4.ToString();
		tbCompen5.Text = frmParam.fCompen5.ToString();
		tbCompen6.Text = frmParam.fCompen6.ToString();
		tbCompen7.Text = frmParam.fCompen7.ToString();
		tbCompen8.Text = frmParam.fCompen8.ToString();
		tbCompen9.Text = frmParam.fCompen9.ToString();
		tbCompen10.Text = frmParam.fCompen10.ToString();
		tbCompen11.Text = frmParam.fCompen11.ToString();
		tbCompen12.Text = frmParam.fCompen12.ToString();
		tbChannelACnt.Text = frmParam.iChannelACnt.ToString();
		frmParam.resetCom();
		LoadLanguage();
		iniParam.LoadParam();
		EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[0];
		EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[1];
		EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[2];
		radioButton1.Text = frmParam.strName1;
		radioButton2.Text = frmParam.strName2;
		radioButton3.Text = frmParam.strName3;
		btnActivate.Visible = false;
		try
		{
			tbMountTest.Text = "0";
			test420mA();
		}
		catch
		{
		}
		tabPage16.Parent = null;
		tabPage4.Parent = null;
		tabControl1.SizeMode = TabSizeMode.Fixed;
		if (tabControl1.ItemSize.Width != tabControl1.Size.Width / tabControl1.TabCount)
		{
			tabControl1.ItemSize = new Size(tabControl1.Size.Width / tabControl1.TabCount, 20);
		}
	}

	public void LoadLanguage()
	{
		label115.Text = Lang.PS("通道一路数", "Channel number one");
		gbtempset.Text = Lang.PS("仪器设置", "Instrument Set ");
		button14.Text = Lang.PS("开始分析", "startAll");
		bControlTemp.Text = Lang.PS("开始控温", "Start Temp");
		label15.Text = Lang.PS("准备", "ready");
		label16.Text = Lang.PS("初温", "init");
		label17.Text = Lang.PS("升温", "rise");
		label18.Text = Lang.PS("保持", "hold");
		label19.Text = Lang.PS("降温", "cooling");
		tabControl1.TabPages[0].Text = Lang.PS("温度/流量", "Temp/Flow");
		tabControl1.TabPages[1].Text = Lang.PS("程升/事件", "Prom/Event");
		tabControl1.TabPages[2].Text = Lang.PS("进样器", "injector");
		tabControl1.TabPages[3].Text = Lang.PS("网络/版本", "Ordinal/Version");
		tabControl1.TabPages[5].Text = Lang.PS("消息", "Mess");
		tabControl1.TabPages[4].Text = Lang.PS("仪器方法", "Instrument method");
		groupBox3.Text = Lang.PS("温度控制", "TempControl");
		dgtempControl.Columns[1].HeaderText = Lang.PS("实测(°C)", "measure(°C)");
		dgtempControl.Columns[2].HeaderText = Lang.PS("设定(°C)", "Set(°C)");
		dgtempControl.Columns[3].HeaderText = Lang.PS("保护(°C)", "protect(°C)");
		button3.Text = Lang.PS("查询", "query");
		button4.Text = Lang.PS("设定", "set");
		groupBox4.Text = Lang.PS("流量控制", "flowControl ");
		tabControl2.TabPages[0].Text = Lang.PS("进样1", "INJ1");
		tabControl2.TabPages[1].Text = Lang.PS("进样2", "INJ2");
		tabControl2.TabPages[2].Text = Lang.PS("进样3", "INJ3");
		tabControl2.TabPages[3].Text = Lang.PS("检测器1", "DET1");
		tabControl2.TabPages[4].Text = Lang.PS("检测器2", "DET2");
		tabControl2.TabPages[5].Text = Lang.PS("检测器3", "DET3");
		iniParam.LoadParam();
		for (int i = 0; i < 6; i++)
		{
			tabControl2.TabPages[i].Text = iniParam.strGasNAME[18 + i];
		}
		EPCControl.Columns[1].HeaderText = Lang.PS("输入(psi)", "Input(psi)");
		EPCControl.Columns[2].HeaderText = Lang.PS("实测(psi)", "output(psi)");
		EPCControl.Columns[3].HeaderText = Lang.PS("实测(sccm)", "output(sccm)");
		EPCControl.Columns[4].HeaderText = Lang.PS("开关", "state");
		label3.Text = Lang.PS("模式", "mode");
		radioButton6.Text = Lang.PS("压力", "press");
		radioButton5.Text = Lang.PS("流量", "flow");
		radioButton25.Text = Lang.PS("线速度", "velocity");
		label10.Text = Lang.PS("设置", "set");
		label12.Text = Lang.PS("色谱柱", "column");
		label4.Text = Lang.PS("气体:", "gas:");
		label69.Text = Lang.PS("分流比", "ratio");
		radioButton1.Text = Lang.PS("载气", "carrier");
		radioButton2.Text = Lang.PS("分流", "split");
		radioButton3.Text = Lang.PS("吹扫", "Purge");
		button11.Text = Lang.PS("查询", "query");
		button6.Text = Lang.PS("设定", "set");
		comboBox6.Text = Lang.PS("N2");
		comboBox6.Items.Clear();
		comboBox6.Items.Add(Lang.PS("氮气", "N2"));
		comboBox6.Items.Add(Lang.PS("氢气", "H2"));
		comboBox6.Items.Add(Lang.PS("空气", "Air"));
		comboBox6.Items.Add(Lang.PS("氦气", "He"));
		comboBox6.Items.Add(Lang.PS("氩气", "Ar"));
		label13.Text = Lang.PS("初始时间:", "InitTime:");
		dgInsamp1.Columns[1].HeaderText = Lang.PS("速率    (psi/min)", "speed   (psi/min)");
		dgInsamp1.Columns[2].HeaderText = Lang.PS("保持    (psi/min)", "hold    (psi/min)");
		dgInsamp1.Columns[3].HeaderText = Lang.PS("时间    (min)", "time    (min)");
		groupBox6.Text = Lang.PS("程升控制", "Program control");
		label35.Text = Lang.PS("初始时间", "InitTime");
		gdTempControl.Columns[0].HeaderText = Lang.PS("阶号", "Index");
		gdTempControl.Columns[1].HeaderText = Lang.PS("升温速率", "rate");
		gdTempControl.Columns[2].HeaderText = Lang.PS("保持温度", "hold");
		gdTempControl.Columns[3].HeaderText = Lang.PS("保持时间", "time");
		button8.Text = Lang.PS("查询", "query");
		button7.Text = Lang.PS("设定", "set");
		button10.Text = Lang.PS("查询", "query");
		button9.Text = Lang.PS("设定", "set");
		groupBox7.Text = Lang.PS("事件控制", "eventControl");
		gvExtEvTP.Columns[0].HeaderText = Lang.PS("阶号", "Index");
		gvExtEvTP.Columns[1].HeaderText = Lang.PS("事件1 [min]", "event1 [min]");
		gvExtEvTP.Columns[2].HeaderText = Lang.PS("事件2 [min]", "event2 [min]");
		gvExtEvTP.Columns[3].HeaderText = Lang.PS("事件3 [min]", "event3 [min]");
		gvExtEvTP.Columns[4].HeaderText = Lang.PS("事件4 [min]", "event4 [min]");
		gvExtEvTP.Columns[5].HeaderText = Lang.PS("事件5 [min]", "event5 [min]");
		gvExtEvTP.Columns[6].HeaderText = Lang.PS("事件6 [min]", "event6 [min]");
		gvExtEvTP.Columns[7].HeaderText = Lang.PS("事件7 [min]", "event7 [min]");
		gvExtEvTP.Columns[8].HeaderText = Lang.PS("事件8 [min]", "event8 [min]");
		groupBox8.Text = Lang.PS("状态", "status");
		label36.Text = Lang.PS("进样器状态", "Sampler");
		textBox4.Text = Lang.PS("离线", "OffLine");
		textBox5.Text = Lang.PS("空闲", "free");
		label37.Text = Lang.PS("瓶号/针号", "bottle/needle");
		dgGramset.Columns[0].HeaderText = Lang.PS("起始瓶号", "Sbottle");
		dgGramset.Columns[1].HeaderText = Lang.PS("终止瓶号", "Ebottle");
		dgGramset.Columns[2].HeaderText = Lang.PS("进样量   [uL]", "inL [uL]");
		dgGramset.Columns[3].HeaderText = Lang.PS("次/瓶", "Times/bottle");
		dgGramset.Columns[4].HeaderText = Lang.PS("间隔    [min]", "apart    [min]");
		checkBox6.Text = Lang.PS("安装", "install");
		label39.Text = Lang.PS("型号", "model");
		label40.Text = Lang.PS("进样口", "injector");
		label41.Text = Lang.PS("进样前溶剂清洗:", "Injection of solvent purge:");
		label42.Text = Lang.PS("次");
		label43.Text = Lang.PS("进样前样品清洗:", "Injection of samples before purge:");
		label1.Text = Lang.PS("次");
		label45.Text = Lang.PS("进样后溶剂清洗:", "After sample injection solvent purge:");
		label46.Text = Lang.PS("次");
		label47.Text = Lang.PS("泵样次数:", "Pump times:");
		label48.Text = Lang.PS("次");
		label49.Text = Lang.PS("粘度延时:", "Viscosity delay:");
		label50.Text = Lang.PS("秒", "S");
		label51.Text = Lang.PS("进样后驻留:", "sampling holed:");
		label52.Text = Lang.PS("秒", "S");
		label53.Text = Lang.PS("延时启动:", "Start delay:");
		label54.Text = Lang.PS("小时:", "h:");
		label55.Text = Lang.PS("取样方式", "sampling methods");
		label57.Text = Lang.PS("进样速度:", "Injection speed:");
		label58.Text = Lang.PS("针芯速度:", "needle speed:");
		comboBox11.Text = Lang.PS("慢速", "low");
		comboBox11.Items.Clear();
		comboBox11.Items.Add(Lang.PS("慢速", "low"));
		comboBox11.Items.Add(Lang.PS("中速", "moderate"));
		comboBox11.Items.Add(Lang.PS("快速", "fast"));
		comboBox12.Text = Lang.PS("慢速", "low");
		comboBox12.Items.Clear();
		comboBox12.Items.Add(Lang.PS("慢速", "low"));
		comboBox12.Items.Add(Lang.PS("快速", "fast"));
		button15.Text = Lang.PS("查询", "queryType");
		button24.Text = Lang.PS("设定", "setType");
		btnStopAutoINJ.Text = Lang.PS("停止进样", "StopSample");
		groupBox14.Text = Lang.PS("自动进样", "AutoSampler");
		label65.Text = Lang.PS("进样次数：", "SamplerTimes: ");
		label6.Text = Lang.PS("次");
		label66.Text = Lang.PS("时间间隔：", "timeinterval:");
		label14.Text = Lang.PS("背光时间：", "BackLight Time:");
		label44.Text = Lang.PS("秒", "S");
		button20.Text = Lang.PS("查询", "query");
		button21.Text = Lang.PS("设定", "set");
		button13.Text = Lang.PS("查询", "query");
		groupBox5.Text = Lang.PS("短消息发送", "Message");
		textBox8.Text = Lang.PS("色谱机短信", "short message");
		checkBox7.Text = Lang.PS("鸣叫提醒", "tweet");
		label28.Text = Lang.PS("鸣叫次数:", "calltimes:");
		checkBox9.Text = Lang.PS("停止时间到后自动发送", "auto send when stop");
		bsavmess.Text = Lang.PS("保存", "save");
		button22.Text = Lang.PS("发送", "send");
		label70.Text = Lang.PS("量程", "range");
		button30.Text = Lang.PS("查询", "query");
		button31.Text = Lang.PS("设定", "set");
		cbAlarm.Text = Lang.PS("指令鸣叫", "CommCall");
		button28.Text = Lang.PS("查询", "query");
		button29.Text = Lang.PS("设定", "set");
		groupBox12.Text = Lang.PS("色谱机网络参数", "IBrainChromNetPara");
		label59.Text = Lang.PS("本地  IP:", "IP:");
		label60.Text = Lang.PS("子网掩码:", "subnetmask: ");
		label61.Text = Lang.PS("网    关:", "gateway:");
		groupBox13.Text = Lang.PS("色谱仪内上报IP", "IBrainChromNetPara");
		label62.Text = Lang.PS("本地主管:", "LheadIP:");
		label63.Text = Lang.PS("业务主管:", "CoOIP:");
		label64.Text = Lang.PS("上级主管:", "higherIP:");
		button27.Text = Lang.PS("流量节省", "Flow save");
		button19.Text = Lang.PS("查询", "query");
		button18.Text = Lang.PS("设定", "set");
		button35.Text = Lang.PS("多位阀使能控制", "Multivalve control");
		label75.Text = Lang.PS("事件开关", "EnevtSwitch");
		button34.Text = Lang.PS("查询", "query");
		button33.Text = Lang.PS("设定", "set");
		groupBox1.Text = Lang.PS("液相泵控制", "pump Control");
		liquidopen.Text = Lang.PS("打开", "open");
		liquidclose.Text = Lang.PS("关闭", "close");
		liquidStatus.Text = Lang.PS("*液相泵状态", "*pump status");
		groupBox11.Text = Lang.PS("泵控制状态设定", "pump set");
		label71.Text = Lang.PS("最大压力", "MaxPress");
		label74.Text = Lang.PS("最小压力", "MinPress");
		rliquid.Text = Lang.PS("恒压", "CV");
		rpass.Text = Lang.PS("恒流", "CC");
		liquidsetvaluequery.Text = Lang.PS("查询", "Query");
		liquidset.Text = Lang.PS("设定", "set");
		label72.Text = Lang.PS("压力", "Press");
		label73.Text = Lang.PS("流量", "flow");
		gvHardVersion.Columns[0].HeaderText = Lang.PS("序 号", "Number");
		gvHardVersion.Columns[1].HeaderText = Lang.PS("版 本", "Version");
		label76.Text = Lang.PS("参数方法:", "Parameter method:");
		label77.Text = Lang.PS("实测", "measure");
		label78.Text = Lang.PS("实测", "measure");
		btnEPCSet.Text = Lang.PS("高级参数", "Advanced parameters");
		btnSave.Text = Lang.PS("保存设置", "Save Settings");
		label20.Text = Lang.PS("载气1", "Column 1");
		label22.Text = Lang.PS("氢气1", "HH 1");
		label23.Text = Lang.PS("空气1", "Air 1");
		label31.Text = Lang.PS("载气4", "Column 4");
		label32.Text = Lang.PS("载气3", "Column 3");
		label33.Text = Lang.PS("载气2", "Column 2");
	}

	public void method_14()
	{
		button3.Focus();
	}

	public void button14_Click(object sender, EventArgs e)
	{
		method_14();
		try {
			string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
			System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] button14_Click: Button Text = '{button14.Text}', StartAll={Lang.PS("开始分析", "StartAll")}\r\n");
		} catch {}

		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				// 放宽字符串匹配限制，防止中英文空格或换行等不可见字符导致不匹配
				if (button14.Text.Contains("开始分析") || button14.Text.Contains("StartAll") || button14.Text.Contains("startAll") || button14.Text.Contains("Start All"))
				{
					try {
						string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
						System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] button14_Click: Sending Command 18\r\n");
					} catch {}
					currentTcpServerSocket.SendCmd(18);
				}
				else
				{
					try {
						string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
						System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] button14_Click: Sending Command 19 (Stop)\r\n");
					} catch {}
					currentTcpServerSocket.SendCmd(19);
					
					// 手动停止时，强制取消可能存在的自动进样循环
					ChromFormInterface mainForm = null;
					if (FormMain.fromMain != null) mainForm = FormMain.fromMain;
					else if (FormMainCtrl.fromMain != null) mainForm = FormMainCtrl.fromMain;
					else if (FormMainPortable.fromMain != null) mainForm = FormMainPortable.fromMain;
					else if (FormMainPortableRH.fromMain != null) mainForm = FormMainPortableRH.fromMain;
					
					if (mainForm != null && mainForm.GetInstrument() != null)
					{
						mainForm.GetInstrument().CancelAutoInjectorCycle();
					}
				}
			}
			else
			{
				try {
					string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
					System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] button14_Click: CurrentTcpServerSocket is NULL!\r\n");
				} catch {}
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
		}
	}

	public void button16_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(98);
	}

	private void button34_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(83);
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		int millisecondsTimeout = 100;
		switch (tabControl1.SelectedIndex)
		{
		case 0:
			button3_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button11_Click(null, null);
			break;
		case 1:
			button34_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button8_Click(null, null);
			break;
		case 2:
			button15_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button16_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button20_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			liquidstatusselect_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			liquidsetvaluequery_Click(null, null);
			break;
		case 3:
			button30_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button28_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button13_Click(null, null);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			button19_Click(null, null);
			break;
		}
	}

	private void button19_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(48);
	}

	public void button4_Click(object sender, EventArgs e)
	{
		int millisecondsTimeout = 100;
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				currentTcpServerSocket.SendCmd(8);
				Thread.Sleep(millisecondsTimeout);
				Application.DoEvents();
				currentTcpServerSocket.SendCmd(106);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	public void toolStripMenuItem3_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			cdlMgr.formMain.Fmultivalve.Show();
			cdlMgr.formMain.Fmultivalve.button1_Click(null, null);
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		int millisecondsTimeout = 100;
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(0);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
		}
	}

	public void button6_Click(object sender, EventArgs e)
	{
		if (tabControl2.SelectedIndex != 5)
		{
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				cdlMgr.CurrentTcpServerSocket?.SendCmd(37);
				return;
			}
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	public void button11_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			maskedTextBox4.Text = "0";
			currentTcpServerSocket.SendCmd(36);
		}
	}

	private void maskedTextBox8_TextChanged(object sender, EventArgs e)
	{
	}

	private void IP4_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void dgInsamp1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		float num = Class49.String2Float(dgInsamp1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, 0f);
		if (num <= 0f)
		{
			dgInsamp1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
			return;
		}
		if (num > 450f)
		{
		}
		dgInsamp1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)num).ToString();
	}

	private void dgGramset_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void maskedTextBox4_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(maskedTextBox4.Text.Trim(), 0f);
		if ((double)num > 999.99)
		{
			maskedTextBox4.Text = "999.99";
		}
		if (num < 0f)
		{
			maskedTextBox4.Text = "0";
		}
	}

	private void comboBox3_TextChanged(object sender, EventArgs e)
	{
		int num = Class49.Object2Int(comboBox3.Text.Trim(), 0);
		if (num > 3)
		{
			comboBox3.Text = "3";
		}
		if (num <= 0)
		{
			comboBox3.Text = "0";
		}
	}

	private void radioButton5_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton5.Checked)
		{
			label11.Text = "ml/min";
			label10.Text = Lang.PS("设置", "set");
			dgInsamp1.Columns[1].HeaderText = Lang.PS("速率    (ml/min)", "rate    (ml/min)");
			dgInsamp1.Columns[2].HeaderText = Lang.PS("保持    (ml/min)", "hold    (ml/min)");
		}
	}

	private void radioButton25_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton25.Checked)
		{
			label11.Text = "cm/s";
			label10.Text = Lang.PS("设置", "set");
			dgInsamp1.Columns[1].HeaderText = Lang.PS("速率    (cm/s)", "rate    (cm/s)");
			dgInsamp1.Columns[2].HeaderText = Lang.PS("保持    (cm/s)", "hold    (cm/s)");
		}
	}

	private void radioButton6_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton6.Checked)
		{
			label11.Text = "psi";
			label10.Text = Lang.PS("设置", "set");
			dgInsamp1.Columns[1].HeaderText = Lang.PS("速率    (psi/min)", "rate    (psi/min)");
			dgInsamp1.Columns[2].HeaderText = Lang.PS("保持    (psi)", "hold    (psi)");
		}
	}

	private void radioButton2_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton2.Checked)
		{
			panelJY.Visible = false;
			panel15.Visible = false;
			label12.Text = Lang.PS("气阻:", "A-resistor:");
		}
		else
		{
			panelJY.Visible = true;
			panel15.Visible = true;
		}
		button11_Click(null, null);
	}

	private void radioButton3_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton3.Checked)
		{
			panelJY.Visible = false;
			panel15.Visible = false;
			label12.Text = Lang.PS("气阻:", "A-resistor:");
		}
		else
		{
			panelJY.Visible = true;
			panel15.Visible = true;
		}
		button11_Click(null, null);
	}

	private void radioButton28_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton28.Checked)
		{
			radioButton28.Text = "开";
		}
		else
		{
			radioButton28.Text = "关";
		}
	}

	private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
	{
		iniParam.LoadParam();
		if (tabControl2.SelectedIndex > 2)
		{
			radioButton1.Text = Lang.PS("氢气", "H2");
			radioButton2.Text = Lang.PS("空气", "Air");
			radioButton3.Text = Lang.PS("尾吹", "Make_up");
			EPCControl.Rows[0].Cells[0].Value = Lang.PS("氢气", "H2");
			EPCControl.Rows[1].Cells[0].Value = Lang.PS("空气", "Air");
			EPCControl.Rows[2].Cells[0].Value = Lang.PS("尾吹", "Make_up");
			panelJY.Visible = false;
			panel15.Visible = false;
			label69.Visible = false;
			lFliuBi.Visible = false;
			if (tabControl2.SelectedIndex == 3)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[9];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[10];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[11];
				radioButton1.Text = iniParam.strGasNAME[9];
				radioButton2.Text = iniParam.strGasNAME[10];
				radioButton3.Text = iniParam.strGasNAME[11];
			}
			else if (tabControl2.SelectedIndex == 4)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[12];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[13];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[14];
				radioButton1.Text = iniParam.strGasNAME[12];
				radioButton2.Text = iniParam.strGasNAME[13];
				radioButton3.Text = iniParam.strGasNAME[14];
			}
			else if (tabControl2.SelectedIndex == 5)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[15];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[16];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[17];
				radioButton1.Text = iniParam.strGasNAME[15];
				radioButton2.Text = iniParam.strGasNAME[16];
				radioButton3.Text = iniParam.strGasNAME[17];
			}
		}
		else
		{
			bool flag = false;
			radioButton1.Text = Lang.PS("载气", "carrier");
			radioButton2.Text = Lang.PS("分流", "split");
			radioButton3.Text = Lang.PS("吹扫", "Purge");
			if (tabControl2.SelectedIndex == 0)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[0];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[1];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[2];
				radioButton1.Text = iniParam.strGasNAME[0];
				radioButton2.Text = iniParam.strGasNAME[1];
				radioButton3.Text = iniParam.strGasNAME[2];
			}
			else if (tabControl2.SelectedIndex == 1)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[3];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[4];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[5];
				radioButton1.Text = iniParam.strGasNAME[3];
				radioButton2.Text = iniParam.strGasNAME[4];
				radioButton3.Text = iniParam.strGasNAME[5];
			}
			else if (tabControl2.SelectedIndex == 2)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[6];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[7];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[8];
				radioButton1.Text = iniParam.strGasNAME[6];
				radioButton2.Text = iniParam.strGasNAME[7];
				radioButton3.Text = iniParam.strGasNAME[8];
			}
			else if (tabControl2.SelectedIndex == 3)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[9];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[10];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[11];
				radioButton1.Text = iniParam.strGasNAME[9];
				radioButton2.Text = iniParam.strGasNAME[10];
				radioButton3.Text = iniParam.strGasNAME[11];
			}
			else if (tabControl2.SelectedIndex == 4)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[12];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[13];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[14];
				radioButton1.Text = iniParam.strGasNAME[12];
				radioButton2.Text = iniParam.strGasNAME[13];
				radioButton3.Text = iniParam.strGasNAME[14];
			}
			else if (tabControl2.SelectedIndex == 5)
			{
				EPCControl.Rows[0].Cells[0].Value = iniParam.strGasNAME[15];
				EPCControl.Rows[1].Cells[0].Value = iniParam.strGasNAME[16];
				EPCControl.Rows[2].Cells[0].Value = iniParam.strGasNAME[17];
				radioButton1.Text = iniParam.strGasNAME[15];
				radioButton2.Text = iniParam.strGasNAME[16];
				radioButton3.Text = iniParam.strGasNAME[17];
			}
			if (radioButton1.Checked)
			{
				panelJY.Visible = true;
				panel15.Visible = true;
			}
			else
			{
				panelJY.Visible = false;
				panel15.Visible = false;
			}
			label69.Visible = true;
			lFliuBi.Visible = true;
		}
		maskedTextBox1.Text = "";
		maskedTextBox2.Text = "";
		maskedTextBox3.Text = "";
		for (int i = 0; i < EPCControl.Rows.Count; i++)
		{
			for (int j = 1; j < EPCControl.Columns.Count; j++)
			{
				EPCControl.Rows[i].Cells[j].Value = "--";
			}
		}
		button11_Click(null, null);
	}

	private void dgtempControl_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			dgtempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = dgtempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
		}
	}

	private void dgtempControl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		float num = Class49.String2Float(dgtempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, 0f);
		if (num <= 0f)
		{
			dgtempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
			return;
		}
		if (num > 450f)
		{
		}
		dgtempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = num.ToString();
	}

	private void gvExtEvTP_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		float num = Class49.String2Float(gvExtEvTP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, 0f);
		if (num == 0f)
		{
			gvExtEvTP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = num.ToString();
		}
		if (num <= 0f || num >= 9999.99f)
		{
			gvExtEvTP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
		}
	}

	public bool IPCheck(string IP)
	{
		string text = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";
		return Regex.IsMatch(IP, "^" + text + "\\." + text + "\\." + text + "\\." + text + "$");
	}

	private void tbptIniTempHoldT_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(tbptIniTempHoldT.Text, 0f);
		if ((double)num > 999.9)
		{
			tbptIniTempHoldT.Text = "999.9";
		}
		if (num == 0f)
		{
			tbptIniTempHoldT.Text = num.ToString();
		}
	}

	public void button10_Click(object sender, EventArgs e)
	{
		gvExtEvTP.Rows.Clear();
		for (int i = 1; i < 9; i++)
		{
			gvExtEvTP.Rows.Add(i.ToString(), "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00");
		}
		for (int j = 0; j < 8; j++)
		{
			gvExtEvTP.Rows[j].Cells[0].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[0].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[1].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[1].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[2].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[2].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[3].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[3].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[4].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[4].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[5].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[5].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[6].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[6].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[7].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[7].Style.ForeColor = Color.Black;
			gvExtEvTP.Rows[j].Cells[8].Style.BackColor = Color.White;
			gvExtEvTP.Rows[j].Cells[8].Style.ForeColor = Color.Black;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(2);
			Thread.Sleep(100);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(100);
			Thread.Sleep(100);
			Application.DoEvents();
		}
	}

	public void button7_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(9);
			Thread.Sleep(100);
			button9_Click(null, null);
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void button8_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(1);
		Thread.Sleep(200);
		button10_Click(null, null);
	}

	private void button9_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(10);
			Thread.Sleep(500);
			Thread.Sleep(500);
			currentTcpServerSocket.SendCmd(101);
		}
	}

	private void gdTempControl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		float num = Class49.String2Float(gdTempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, 0f);
		float num2 = Class49.String2Float(dgtempControl.Rows[1].Cells[2].Value, 0f);
		float num3;
		if (!((e.RowIndex == 0) & (e.ColumnIndex == 2)))
		{
			num3 = ((e.RowIndex != 0) ? Class49.String2Float(gdTempControl.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value, 0f) : num2);
		}
		else
		{
			if (num < num2)
			{
				MessageBox.Show(Lang.PS("该值需要大于柱炉设定温度。", "The values need to be bigger than the column oven temperature setting !"));
				gdTempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
				return;
			}
			num3 = num2;
		}
		if (num == 0f)
		{
			gdTempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = num.ToString();
		}
		if (e.ColumnIndex != 1 || num <= 0f || num >= 80f)
		{
		}
		if (e.ColumnIndex == 2)
		{
			if (num <= 0f || num >= 420f)
			{
			}
			if (num <= num3)
			{
				MessageBox.Show(Lang.PS("该值需大于上一价设定值！", "This value must be greater than a set value!"));
				gdTempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
				return;
			}
			gdTempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)num).ToString();
		}
		if (e.ColumnIndex == 3 && (num <= 0f || num >= 1000f))
		{
			gdTempControl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
		}
	}

	private void dgGramset_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		float num = Class49.String2Float(dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, 0f);
		if (num == 0f)
		{
			dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = num.ToString();
		}
		if (e.ColumnIndex == 0)
		{
			if (num > 0f && num <= 150f)
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)num).ToString();
			}
			else
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
			}
		}
		if (e.ColumnIndex == 1)
		{
			if (num > 0f && num <= 150f)
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)num).ToString();
			}
			else
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
			}
		}
		if (e.ColumnIndex == 2 && ((double)num < 0.1 || num > 200f))
		{
			dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
		}
		if (e.ColumnIndex == 3)
		{
			if (num >= 1f && num <= 999f)
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)num).ToString();
			}
			else
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
			}
		}
		if (e.ColumnIndex == 4)
		{
			if (num < 1f || num > 655f)
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
			}
			else
			{
				dgGramset.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)num).ToString();
			}
		}
	}

	private void liquidsetvaluequery_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(90);
	}

	private void liquidset_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(91);
	}

	private void Minliquidpress_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(((TextBox)sender).Text.Trim(), 0f);
		if ((double)num > 99.99)
		{
			((TextBox)sender).Text = "99.99";
		}
		if (num <= 0f)
		{
			((TextBox)sender).Text = "0";
		}
	}

	private void Maxliquidpress_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(((TextBox)sender).Text.Trim(), 0f);
		if (num > 45f)
		{
			((TextBox)sender).Text = "45";
		}
		if (num <= 0f)
		{
			((TextBox)sender).Text = "0";
		}
	}

	private void liquidclose_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendEPCCmd(88, 0);
	}

	private void liquidopen_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendEPCCmd(88, 1);
	}

	private void button15_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(96);
	}

	private void liquidstatusselect_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(89);
	}

	private void button21_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(12);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void button20_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(4);
	}

	private void maskedTextBox20_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(maskedTextBox20.Text, 0f);
		if ((double)num > 999.9)
		{
			maskedTextBox20.Text = "0";
		}
		if (num < 0f)
		{
			maskedTextBox20.Text = "0";
		}
	}

	private void maskedTextBox19_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(maskedTextBox19.Text.Trim(), 0f);
		if (num > 9999f)
		{
			maskedTextBox19.Text = "9999";
		}
		if (num <= 0f)
		{
			maskedTextBox19.Text = "0";
		}
	}

	private void checkBox6_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void textBox11_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(((TextBox)sender).Text.Trim(), 0f);
		if ((double)num > 99.9)
		{
			((TextBox)sender).Text = "99.9";
		}
		if (num <= 0f)
		{
			((TextBox)sender).Text = "0";
		}
	}

	private void textBox9_TextChanged(object sender, EventArgs e)
	{
		int num = Class49.Object2Int(((TextBox)sender).Text.Trim(), 0);
		if (num > 99)
		{
			((TextBox)sender).Text = "99";
		}
		if (num <= 0)
		{
			((TextBox)sender).Text = "0";
		}
	}

	private void button24_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(97);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission !"));
	}

	private void button17_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(99);
			button21_Click(null, null);
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void tBshuaijian_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tBshuaijian.Text, out var result);
		frmParam.fShuaijian = result;
		frmParam.SaveParam();
	}

	private void tBshuaijian2_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tBshuaijian2.Text, out var result);
		frmParam.fShuaijian2 = result;
		frmParam.SaveParam();
	}

	private void tBshuaijian3_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tBshuaijian3.Text, out var result);
		frmParam.fShuaijian3 = result;
		frmParam.SaveParam();
	}

	private void btnShuaijian_Click(object sender, EventArgs e)
	{
		string text = tBpasswordShJ.Text;
		text = text.Trim();
		if (text.Equals("0632"))
		{
			panConfig.Visible = true;
			tBshuaijian.Visible = true;
			tBshuaijian2.Visible = true;
			tBshuaijian3.Visible = true;
			cbEPCSe.Visible = true;
			tbSmooth.Visible = true;
			if (cbKindMachine != null)
			{
				cbKindMachine.Visible = true;
			}
			return;
		}
		panConfig.Visible = false;
		tBshuaijian.Visible = false;
		tBshuaijian2.Visible = false;
		tBshuaijian3.Visible = false;
		cbEPCSe.Visible = false;
		tbSmooth.Visible = false;
		if (cbKindMachine != null)
		{
			cbKindMachine.Visible = false;
		}
		tbKRName.Visible = false;
	}

	private void button27_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(69);
	}

	private void button18_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(49);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void bsavmess_Click(object sender, EventArgs e)
	{
		cdlMgr.formMain.FrmEquip.UpdateMess(cdlMgr.CurrentGCID, checkBox9.Checked, textBox8.Text.Trim(), checkBox7.Checked, Class49.Object2Int(comboBox13.Text.Trim(), 1));
	}

	private void button30_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(60);
	}

	private void button29_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(63);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission !"));
	}

	private void button31_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(61);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission !"));
	}

	private void button28_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(62);
	}

	private void checkBox7_CheckedChanged(object sender, EventArgs e)
	{
		comboBox13.Enabled = checkBox7.Checked;
	}

	private void button22_Click(object sender, EventArgs e)
	{
		if (!(textBox8.Text.Trim() == ""))
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(80);
		}
	}

	private void button13_Click(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(5);
	}

	private void IP4_Leave(object sender, EventArgs e)
	{
		if (!IPCheck(((MaskedTextBox)sender).Text))
		{
			((MaskedTextBox)sender).Text = "127.0.0.1";
		}
	}

	private void bControlTemp_Click(object sender, EventArgs e)
	{
		method_14();
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			if (bControlTemp.Text == Lang.PS("关闭控温", "Stop Temp"))
			{
				cdlMgr.formMain.chrAcqCtrl.AutoTempCtr = false;
			}
			else
			{
				cdlMgr.formMain.chrAcqCtrl.AutoTempCtr = true;
			}
			if (!currentTcpServerSocket.ControlTemp)
			{
				currentTcpServerSocket.SendCmd(16);
			}
			else
			{
				currentTcpServerSocket.SendCmd(17);
			}
		}
	}

	private void radioButton1_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton1.Checked)
		{
			if (tabControl2.SelectedIndex < 3)
			{
				panelJY.Visible = true;
				panel15.Visible = true;
				label12.Text = Lang.PS("column:", "A-resistor:");
			}
			else
			{
				panelJY.Visible = false;
				panel15.Visible = false;
				label12.Text = Lang.PS("气阻:", "A-resistor:");
			}
		}
		else
		{
			panelJY.Visible = false;
			panel15.Visible = false;
		}
		button11_Click(null, null);
	}

	public void method_13()
	{
		dgInsamp1.Rows.Clear();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
		{
			for (int j = 1; j < 5; j++)
			{
				dgInsamp1.Rows.Add(j + "阶", "000.0", "000.0", "000.0");
			}
			break;
		}
		case SysLanguage.EN:
		{
			for (int i = 1; i < 5; i++)
			{
				dgInsamp1.Rows.Add(i.ToString(), "000.0", "000.0", "000.0");
			}
			break;
		}
		}
		maskedTextBox1.Text = "";
		maskedTextBox2.Text = "";
		maskedTextBox3.Text = "";
		maskedTextBox4.Text = "000.00";
		maskedTextBox8.Text = "0.00";
		maskedTextBox9.Text = "000.000";
	}

	public void button33_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				byte b = 0;
				if (checkBox_7.Checked)
				{
					b += 128;
				}
				if (checkBox_6.Checked)
				{
					b += 64;
				}
				if (checkBox_4.Checked)
				{
					b += 32;
				}
				if (checkBox_2.Checked)
				{
					b += 16;
				}
				if (checkBox_5.Checked)
				{
					b += 8;
				}
				if (checkBox_3.Checked)
				{
					b += 4;
				}
				if (checkBox_1.Checked)
				{
					b += 2;
				}
				if (checkBox_0.Checked)
				{
					b++;
				}
				currentTcpServerSocket.SendEPCCmd(84, b);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void button34_Click_1(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(83);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void BtnTest_Click(object sender, EventArgs e)
	{
	}

	private void ChbTestModbus_CheckedChanged(object sender, EventArgs e)
	{
		frmParam.bTestModbus = chbTestModbus.Checked;
		frmParam.SaveParam();
	}

	private void BtnMountTest_Click(object sender, EventArgs e)
	{
		test420mA();
	}

	public void test420mA()
	{
		if (!(tbMountTest.Text != ""))
		{
			return;
		}
		if (PDDCtrl.self != null)
		{
			short num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
			if (num < 0)
			{
				num = 0;
			}
			if (num > 4095)
			{
				num = 4095;
			}
			PDDCtrl.self.serialPoartBase.Data2[7] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[8] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[9] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[10] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[11] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[12] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[13] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[14] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[15] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[16] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[17] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[18] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[19] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[20] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[21] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[22] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[23] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[24] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[25] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[26] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[27] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[28] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			PDDCtrl.self.serialPoartBase.Data2[29] = (byte)(num >> 8);
			PDDCtrl.self.serialPoartBase.Data2[30] = (byte)num;
		}
		if (OnlineCtrl.selfCtrl != null)
		{
			short num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
			if (num < 0)
			{
				num = 0;
			}
			if (num > 4095)
			{
				num = 4095;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[7] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[8] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[9] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[10] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[11] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[12] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[13] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[14] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[15] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[16] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[17] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[18] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[19] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[20] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[21] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[22] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[23] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[24] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[25] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[26] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[27] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[28] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[29] = (byte)(num >> 8);
			OnlineCtrl.selfCtrl.serialPoartBase.Data2[30] = (byte)num;
		}
		if (MicrFPDCtrl.selfCtrl != null)
		{
			float num2 = float.Parse(tbMountTest.Text);
			short num = (short)((!(num2 - frmParam.fmount41 < 0f)) ? ((short)((num2 - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[102] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount42 < 0f)) ? ((short)((num2 - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[103] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount43 < 0f)) ? ((short)((num2 - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[104] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount44 < 0f)) ? ((short)((num2 - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f)) : 0);
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[105] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount45 < 0f)) ? ((short)((num2 - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[106] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount46 < 0f)) ? ((short)((num2 - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[107] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount47 < 0f)) ? ((short)((num2 - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[108] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount48 < 0f)) ? ((short)((num2 - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[109] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount49 < 0f)) ? ((short)((num2 - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[110] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount410 < 0f)) ? ((short)((num2 - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[111] = (ushort)num;
			num = (short)((!(num2 - frmParam.fmount411 < 0f)) ? ((short)((num2 - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f)) : 0);
			if (num > 4095)
			{
				num = 4095;
			}
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[112] = (ushort)num;
		}
		if (VocCtrl.vocCtrl != null)
		{
			short num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[7] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[8] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[9] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[10] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[11] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[12] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[13] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[14] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[15] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[16] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[17] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[18] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[19] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[20] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[21] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[22] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[23] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[24] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[25] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[26] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[27] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[28] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
			VocCtrl.vocCtrl.serialPoartBase.Data2[29] = (byte)(num >> 8);
			VocCtrl.vocCtrl.serialPoartBase.Data2[30] = (byte)num;
		}
		if (RZCtrl.selfCtrl != null)
		{
			float num3 = float.Parse(tbMountTest.Text);
			if (num3 < frmParam.fmount41)
			{
				num3 = frmParam.fmount41;
			}
			if (num3 > frmParam.fmount201)
			{
				num3 = frmParam.fmount201;
			}
			short num = (short)((num3 - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[7] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[8] = (byte)num;
			if (num3 < frmParam.fmount42)
			{
				num3 = frmParam.fmount42;
			}
			if (num3 > frmParam.fmount202)
			{
				num3 = frmParam.fmount202;
			}
			num = (short)((num3 - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[9] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[10] = (byte)num;
			if (num3 < frmParam.fmount43)
			{
				num3 = frmParam.fmount43;
			}
			if (num3 > frmParam.fmount203)
			{
				num3 = frmParam.fmount203;
			}
			num = (short)((num3 - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[11] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[12] = (byte)num;
			if (num3 < frmParam.fmount44)
			{
				num3 = frmParam.fmount44;
			}
			if (num3 > frmParam.fmount204)
			{
				num3 = frmParam.fmount204;
			}
			num = (short)((num3 - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[13] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[14] = (byte)num;
			if (num3 < frmParam.fmount45)
			{
				num3 = frmParam.fmount45;
			}
			if (num3 > frmParam.fmount205)
			{
				num3 = frmParam.fmount205;
			}
			num = (short)((num3 - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[15] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[16] = (byte)num;
			if (num3 < frmParam.fmount46)
			{
				num3 = frmParam.fmount46;
			}
			if (num3 > frmParam.fmount206)
			{
				num3 = frmParam.fmount206;
			}
			num = (short)((num3 - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[17] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[18] = (byte)num;
			if (num3 < frmParam.fmount47)
			{
				num3 = frmParam.fmount47;
			}
			if (num3 > frmParam.fmount207)
			{
				num3 = frmParam.fmount207;
			}
			num = (short)((num3 - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[19] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[20] = (byte)num;
			if (num3 < frmParam.fmount48)
			{
				num3 = frmParam.fmount48;
			}
			if (num3 > frmParam.fmount208)
			{
				num3 = frmParam.fmount208;
			}
			num = (short)((num3 - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[21] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[22] = (byte)num;
			if (num3 < frmParam.fmount49)
			{
				num3 = frmParam.fmount49;
			}
			if (num3 > frmParam.fmount209)
			{
				num3 = frmParam.fmount209;
			}
			num = (short)((num3 - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[23] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[24] = (byte)num;
			if (num3 < frmParam.fmount410)
			{
				num3 = frmParam.fmount410;
			}
			if (num3 > frmParam.fmount2010)
			{
				num3 = frmParam.fmount2010;
			}
			num = (short)((num3 - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[25] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[26] = (byte)num;
			if (num3 < frmParam.fmount411)
			{
				num3 = frmParam.fmount411;
			}
			if (num3 > frmParam.fmount2011)
			{
				num3 = frmParam.fmount2011;
			}
			num = (short)((num3 - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[27] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[28] = (byte)num;
			if (num3 < frmParam.fmount412)
			{
				num3 = frmParam.fmount412;
			}
			if (num3 > frmParam.fmount2012)
			{
				num3 = frmParam.fmount2012;
			}
			num = (short)((num3 - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
			RZCtrl.selfCtrl.serialPoartBase.Data2[29] = (byte)(num >> 8);
			RZCtrl.selfCtrl.serialPoartBase.Data2[30] = (byte)num;
		}
		if (ChromDeviceCtrl.selfCtrl != null)
		{
			short num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[7] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[8] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[9] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[10] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[11] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[12] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[13] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[14] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[15] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[16] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[17] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[18] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[19] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[20] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[21] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[22] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[23] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[24] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[25] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[26] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[27] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[28] = (byte)num;
			num = (short)((float.Parse(tbMountTest.Text) - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
			if (num > 4095)
			{
				num = 4095;
			}
			if (num < 0)
			{
				num = 0;
			}
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[29] = (byte)(num >> 8);
			ChromDeviceCtrl.selfCtrl.serialPoartBase.Data2[30] = (byte)num;
		}
	}

	private void MethodOpen_Click(object sender, EventArgs e)
	{
		MisMgr baseFile = new MisMgr();
		baseFile = (MisMgr)IBaseFileMgr.OpenFile(baseFile);
		if (baseFile != null)
		{
			baseFile.m_strExt = "mis";
			MisMgrAssist misMgrAssist = MisMgrAssist.Create();
			misMgrAssist.SetFormFromMisData(baseFile);
			if (IBaseFileMgr.m_strFilePath != "")
			{
				tbMethName.Text = IBaseFileMgr.m_strFilePath;
				sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
				sysParam.SaveParam();
			}
		}
	}

	private void MethodSave_Click(object sender, EventArgs e)
	{
		MisMgrAssist misMgrAssist = MisMgrAssist.Create();
		MisMgr misMgr = misMgrAssist.MakeMisData();
		misMgr.m_strExt = "mis";
		if (IBaseFileMgr.m_strFilePath != "")
		{
			IBaseFileMgr.SaveFile(IBaseFileMgr.m_strFilePath, misMgr);
		}
		else
		{
			IBaseFileMgr.SaveFile(misMgr);
		}
		if (IBaseFileMgr.m_strFilePath != "")
		{
			tbMethName.Text = IBaseFileMgr.m_strFilePath;
			sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
			sysParam.SaveParam();
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

	private void MethodReSave_Click(object sender, EventArgs e)
	{
		MisMgr misMgr = MakeMisData();
		if (misMgr == null)
		{
			MessageBox.Show("请先选择设备!");
			return;
		}
		misMgr.m_strExt = "mis";
		IBaseFileMgr.SaveFile(misMgr);
		if (IBaseFileMgr.m_strFilePath != "")
		{
			sysParam.strMisDataFilePath = Path.GetFullPath(IBaseFileMgr.m_strFilePath);
			sysParam.SaveParam();
		}
	}

	private void downLoadParameter3(float ftemp)
	{
		byte[] array = Encoding.ASCII.GetBytes("GCHH");
		int num = 4;
		Array.Resize(ref array, 100);
		array[num++] = 7;
		array[num++] = 208;
		array[num++] = 6;
		array[num++] = 0;
		float num2 = 0f;
		bool flag = true;
		array[num++] = 1;
		num2 = ftemp;
		int num3 = (int)(num2 * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		num3 = (int)(epcDevR[9].pressureData * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		num3 = (int)(epcDevR[10].pressureData * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		num3 = (int)(epcDevR[11].pressureData * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			cdlMgr.CurrentTcpServerSocket.SendData(array);
		}
	}

	private void BtnDownload_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员 || Class49.user_0.ULevel == User.Level.检验员)
		{
			LogMgr.Instance.Write2RunLog("btnDownload_Click   start");
			MisMgr misMgr = new MisMgr();
			IBaseFileMgr.m_strFilePath = tbMethName.Text;
			misMgr = (MisMgr)IBaseFileMgr.OpenFile(tbMethName.Text);
			if (misMgr == null)
			{
				return;
			}
			List<EpcDeviceSetting> epcDev = misMgr.devManager.epcDev1;
			misMgr.m_strExt = "mis";
			MisMgrAssist misMgrAssist = MisMgrAssist.Create();
			misMgrAssist.SetFormFromMisData(misMgr);
			if (IBaseFileMgr.m_strFilePath != "")
			{
				tbMethName.Text = IBaseFileMgr.m_strFilePath;
				sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
				sysParam.SaveParam();
			}
			byte[] byte_ = new byte[0];
			byte[] array = Encoding.ASCII.GetBytes("GCHH");
			int num = 4;
			Array.Resize(ref array, 2000);
			array[num++] = 7;
			array[num++] = 208;
			array[num++] = 2;
			num++;
			float num2 = 0f;
			num2 = misMgr.devManager.tempSetedList[1];
			int num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempHoldTime;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			for (int i = 0; i < misMgr.devManager.tempSettingList.Count; i++)
			{
				float.TryParse(misMgr.devManager.tempSettingList[i].tempStart.ToString(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				float.TryParse(misMgr.devManager.tempSettingList[i].tempEnd.ToString(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				float.TryParse(misMgr.devManager.tempSettingList[i].tempKeep.ToString(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
			}
			bool flag = true;
			array[num++] |= 128;
			num2 = 0f;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = 20f;
			num3 = (int)num2;
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempSetedList[2];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[9].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[10].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[11].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag2 = true;
			array[num++] |= 128;
			num2 = 0f;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = 20f;
			num3 = (int)num2;
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempSetedList[3];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[12].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[13].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[14].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag3 = true;
			array[num++] |= 128;
			num2 = 0f;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = 20f;
			num3 = (int)num2;
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempSetedList[5];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag4 = true;
			array[num++] |= 128;
			num2 = misMgr.devManager.tempSetedList[0];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[1].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[2].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag5 = true;
			array[num++] |= 128;
			num2 = misMgr.devManager.tempSetedList[4];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[4].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[5].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag6 = true;
			array[num++] |= 128;
			num2 = misMgr.devManager.tempSetedList[5];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[7].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[8].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			array[num] = epcDev[0].gasType;
			if (epcDev[0].ctrlModel == 1)
			{
				array[num++] &= 127;
			}
			else
			{
				array[num++] |= 128;
			}
			IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.ToBcd_2B(epcDev[0].pressureData, 1));
			num3 = Convert.ToInt32(epcDev[0].pressureData * 10f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[0].pressureData, 1);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[0].chromColLenth * 100f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[0].chromColLenth, 2);
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[0].chromColDiameter * 100f);
			byte_ = IBrainConvert.ToBcd_3B_new(epcDev[0].chromColDiameter, 3);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			array[num++] = byte_[2];
			array[num++] = 0;
			array[num] = epcDev[3].gasType;
			if (epcDev[3].ctrlModel == 1)
			{
				array[num++] &= 127;
			}
			else
			{
				array[num++] |= 128;
			}
			num3 = Convert.ToInt32(epcDev[3].pressureData * 10f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[3].pressureData, 1);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[3].chromColLenth * 100f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[3].chromColLenth, 2);
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[3].chromColDiameter * 100f);
			byte_ = IBrainConvert.ToBcd_3B_new(epcDev[3].chromColDiameter, 3);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			array[num++] = byte_[2];
			array[num] = epcDev[6].gasType;
			if (epcDev[6].ctrlModel == 1)
			{
				array[num++] &= 127;
			}
			else
			{
				array[num++] |= 128;
			}
			num3 = Convert.ToInt32(epcDev[6].pressureData * 10f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[6].pressureData, 1);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[6].chromColLenth * 100f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[6].chromColLenth, 2);
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[6].chromColDiameter * 100f);
			byte_ = IBrainConvert.ToBcd_3B_new(epcDev[6].chromColDiameter, 3);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			array[num++] = byte_[2];
			num = 204;
			for (int j = 0; j < 8; j++)
			{
				if (j < 4)
				{
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[0];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[1];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[2];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[3];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[4];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[5];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[6];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[7];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
				}
				else
				{
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[0];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[1];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[2];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[3];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[4];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[5];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[6];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[7];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
				}
			}
			if (cdlMgr.CurrentTcpServerSocket != null)
			{
				cdlMgr.CurrentTcpServerSocket.SendData(array);
			}
			Thread.Sleep(5000);
			downLoadParameter3(misMgr.devManager.tempSetedList[2]);
			LogMgr.Instance.Write2RunLog("method download  end");
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void BtnEPCCheck_Click(object sender, EventArgs e)
	{
	}

	private void BtnEPCSet_Click(object sender, EventArgs e)
	{
		FormAdvancedP formAdvancedP = new FormAdvancedP();
		formAdvancedP.TopMost = true;
		formAdvancedP.Show();
	}

	private void CbEPCSe_SelectedIndexChanged(object sender, EventArgs e)
	{
		frmParam.epcMode = cbEPCSe.SelectedIndex;
		frmParam.SaveParam();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		frmParam.fmount41 = float.Parse(tbMount41.Text.Trim());
		frmParam.fmount42 = float.Parse(tbMount42.Text.Trim());
		frmParam.fmount43 = float.Parse(tbMount43.Text.Trim());
		frmParam.fmount44 = float.Parse(tbMount44.Text.Trim());
		frmParam.fmount45 = float.Parse(tbMount45.Text.Trim());
		frmParam.fmount46 = float.Parse(tbMount46.Text.Trim());
		frmParam.fmount47 = float.Parse(tbMount47.Text.Trim());
		frmParam.fmount48 = float.Parse(tbMount48.Text.Trim());
		frmParam.fmount49 = float.Parse(tbMount49.Text.Trim());
		frmParam.fmount410 = float.Parse(tbMount410.Text.Trim());
		frmParam.fmount411 = float.Parse(tbMount411.Text.Trim());
		frmParam.fmount412 = float.Parse(tbMount412.Text.Trim());
		frmParam.fmount201 = float.Parse(tbMount201.Text.Trim());
		frmParam.fmount202 = float.Parse(tbMount202.Text.Trim());
		frmParam.fmount203 = float.Parse(tbMount203.Text.Trim());
		frmParam.fmount204 = float.Parse(tbMount204.Text.Trim());
		frmParam.fmount205 = float.Parse(tbMount205.Text.Trim());
		frmParam.fmount206 = float.Parse(tbMount206.Text.Trim());
		frmParam.fmount207 = float.Parse(tbMount207.Text.Trim());
		frmParam.fmount208 = float.Parse(tbMount208.Text.Trim());
		frmParam.fmount209 = float.Parse(tbMount209.Text.Trim());
		frmParam.fmount2010 = float.Parse(tbMount2010.Text.Trim());
		frmParam.fmount2011 = float.Parse(tbMount2011.Text.Trim());
		frmParam.fmount2012 = float.Parse(tbMount2012.Text.Trim());
		frmParam.fCompen1 = float.Parse(tbCompen1.Text.Trim());
		frmParam.fCompen2 = float.Parse(tbCompen2.Text.Trim());
		frmParam.fCompen3 = float.Parse(tbCompen3.Text.Trim());
		frmParam.fCompen4 = float.Parse(tbCompen4.Text.Trim());
		frmParam.fCompen5 = float.Parse(tbCompen5.Text.Trim());
		frmParam.fCompen6 = float.Parse(tbCompen6.Text.Trim());
		frmParam.fCompen7 = float.Parse(tbCompen7.Text.Trim());
		frmParam.fCompen8 = float.Parse(tbCompen8.Text.Trim());
		frmParam.fCompen9 = float.Parse(tbCompen9.Text.Trim());
		frmParam.fCompen10 = float.Parse(tbCompen10.Text.Trim());
		frmParam.fCompen11 = float.Parse(tbCompen11.Text.Trim());
		frmParam.fCompen12 = float.Parse(tbCompen12.Text.Trim());
		frmParam.iChannelACnt = int.Parse(tbChannelACnt.Text.Trim());
		frmParam.SaveParam();
		frmParam.resetCom();
	}

	private void tbSmooth_TextChanged(object sender, EventArgs e)
	{
		int.TryParse(tbSmooth.Text.Trim(), out var result);
		frmParam.iSmooths = result;
		frmParam.SaveParam();
	}

	private void tbKRName_TextChanged(object sender, EventArgs e)
	{
		frmParam.strName = tbKRName.Text;
		frmParam.SaveParam();
	}

	private void btnActivate_Click(object sender, EventArgs e)
	{
		RegisterForm registerForm = new RegisterForm();
		registerForm.Show();
	}

	private void button24_Click_1(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(97);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission !"));
	}

	private void button15_Click_1(object sender, EventArgs e)
	{
		cdlMgr.CurrentTcpServerSocket?.SendCmd(96);
	}

	private void btnStartAutoINJ_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				bData = 3;
				currentTcpServerSocket.SendCmd(99);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void btnStopAutoINJ_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			bData = 4;
			currentTcpServerSocket.SendCmd(99);
		}
	}

	private void resetInj_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				bData = 2;
				currentTcpServerSocket.SendCmd(99);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle72 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle67 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle68 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle69 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle70 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle71 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle73 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle74 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle75 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle76 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle77 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle78 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle83 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle79 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle80 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle81 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle82 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle84 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle85 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle86 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle87 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle88 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle89 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle90 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle91 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle92 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle93 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle94 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle95 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle96 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle97 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle98 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle99 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle100 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle101 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle102 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle103 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle104 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle105 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle106 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle107 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle108 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle109 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle110 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle111 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle112 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.事件 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbtempset = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SendBtn = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.EPCControl = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lFliuBi = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.maskedTextBox8 = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelJY = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.maskedTextBox5 = new System.Windows.Forms.MaskedTextBox();
            this.dgInsamp1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label68 = new System.Windows.Forms.Label();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox9 = new System.Windows.Forms.MaskedTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.radioButton25 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton28 = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgtempControl = new System.Windows.Forms.DataGridView();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实测温度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设定温度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.保护温度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button34 = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.label75 = new System.Windows.Forms.Label();
            this.checkBox_0 = new System.Windows.Forms.CheckBox();
            this.checkBox_1 = new System.Windows.Forms.CheckBox();
            this.checkBox_2 = new System.Windows.Forms.CheckBox();
            this.checkBox_3 = new System.Windows.Forms.CheckBox();
            this.checkBox_4 = new System.Windows.Forms.CheckBox();
            this.checkBox_5 = new System.Windows.Forms.CheckBox();
            this.checkBox_6 = new System.Windows.Forms.CheckBox();
            this.checkBox_7 = new System.Windows.Forms.CheckBox();
            this.gvExtEvTP = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.gdTempControl = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button9 = new System.Windows.Forms.Button();
            this.tbptIniTempHoldT = new System.Windows.Forms.MaskedTextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label117 = new System.Windows.Forms.Label();
            this.tbCompen12 = new System.Windows.Forms.TextBox();
            this.tbCompen11 = new System.Windows.Forms.TextBox();
            this.tbCompen10 = new System.Windows.Forms.TextBox();
            this.tbCompen9 = new System.Windows.Forms.TextBox();
            this.tbCompen8 = new System.Windows.Forms.TextBox();
            this.tbCompen7 = new System.Windows.Forms.TextBox();
            this.tbCompen6 = new System.Windows.Forms.TextBox();
            this.tbCompen5 = new System.Windows.Forms.TextBox();
            this.tbCompen4 = new System.Windows.Forms.TextBox();
            this.tbCompen3 = new System.Windows.Forms.TextBox();
            this.tbCompen2 = new System.Windows.Forms.TextBox();
            this.tbCompen1 = new System.Windows.Forms.TextBox();
            this.label116 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbChannelACnt = new System.Windows.Forms.TextBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.tbMount2012 = new System.Windows.Forms.TextBox();
            this.tbMount412 = new System.Windows.Forms.TextBox();
            this.tbMount2011 = new System.Windows.Forms.TextBox();
            this.tbMount411 = new System.Windows.Forms.TextBox();
            this.tbMount2010 = new System.Windows.Forms.TextBox();
            this.tbMount410 = new System.Windows.Forms.TextBox();
            this.tbMount209 = new System.Windows.Forms.TextBox();
            this.tbMount49 = new System.Windows.Forms.TextBox();
            this.tbMount208 = new System.Windows.Forms.TextBox();
            this.tbMount48 = new System.Windows.Forms.TextBox();
            this.tbMount207 = new System.Windows.Forms.TextBox();
            this.tbMount47 = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.tbMount206 = new System.Windows.Forms.TextBox();
            this.tbMount46 = new System.Windows.Forms.TextBox();
            this.tbMount205 = new System.Windows.Forms.TextBox();
            this.tbMount45 = new System.Windows.Forms.TextBox();
            this.tbMount204 = new System.Windows.Forms.TextBox();
            this.tbMount44 = new System.Windows.Forms.TextBox();
            this.tbMount203 = new System.Windows.Forms.TextBox();
            this.tbMount43 = new System.Windows.Forms.TextBox();
            this.tbMount202 = new System.Windows.Forms.TextBox();
            this.tbMount42 = new System.Windows.Forms.TextBox();
            this.btnMountTest = new System.Windows.Forms.Button();
            this.label94 = new System.Windows.Forms.Label();
            this.tbMountTest = new System.Windows.Forms.TextBox();
            this.tbMount201 = new System.Windows.Forms.TextBox();
            this.tbMount41 = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbTestModbus = new System.Windows.Forms.CheckBox();
            this.liquidstatusselect = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.button21 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.maskedTextBox20 = new System.Windows.Forms.MaskedTextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.maskedTextBox14 = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.maskedTextBox19 = new System.Windows.Forms.MaskedTextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.panConfig = new System.Windows.Forms.Panel();
            this.btnActivate = new System.Windows.Forms.Button();
            this.tbKRName = new System.Windows.Forms.TextBox();
            this.tBshuaijian = new System.Windows.Forms.TextBox();
            this.tbSmooth = new System.Windows.Forms.TextBox();
            this.cbEPCSe = new System.Windows.Forms.ComboBox();
            this.tBshuaijian2 = new System.Windows.Forms.TextBox();
            this.tBshuaijian3 = new System.Windows.Forms.TextBox();
            this.tBpasswordShJ = new System.Windows.Forms.TextBox();
            this.btnShuaijian = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.button31 = new System.Windows.Forms.Button();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.button29 = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.bsavmess = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.button22 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.IP3 = new System.Windows.Forms.MaskedTextBox();
            this.IP2 = new System.Windows.Forms.MaskedTextBox();
            this.IP1 = new System.Windows.Forms.MaskedTextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.gvHardVersion = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button19 = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.IP6 = new System.Windows.Forms.MaskedTextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.IP5 = new System.Windows.Forms.MaskedTextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.IP4 = new System.Windows.Forms.MaskedTextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label99 = new System.Windows.Forms.Label();
            this.tbColP4 = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.btnEPCSet = new System.Windows.Forms.Button();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.tbAirCur2 = new System.Windows.Forms.TextBox();
            this.tbAirSet2 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tbHHCur2 = new System.Windows.Forms.TextBox();
            this.tbHHSet2 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.tbColPreCur2 = new System.Windows.Forms.TextBox();
            this.tbColPreSet2 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tbAirCur1 = new System.Windows.Forms.TextBox();
            this.tbAirSet1 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbHHCur1 = new System.Windows.Forms.TextBox();
            this.tbHHSet1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbColPreCur1 = new System.Windows.Forms.TextBox();
            this.tbColPreSet1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.tbColP3 = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.tbAirP2 = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.tbHHP2 = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.tbColP2 = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.tbAirP1 = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.tbHHP1 = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.tbColP1 = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label76 = new System.Windows.Forms.Label();
            this.MethodReSave = new System.Windows.Forms.Button();
            this.MethodSave = new System.Windows.Forms.Button();
            this.tbMethName = new System.Windows.Forms.TextBox();
            this.MethodOpen = new System.Windows.Forms.Button();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.listMess = new System.Windows.Forms.ListBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.rliquid = new System.Windows.Forms.RadioButton();
            this.liquidsetvaluequery = new System.Windows.Forms.Button();
            this.liquidset = new System.Windows.Forms.Button();
            this.rpass = new System.Windows.Forms.RadioButton();
            this.liquidpass = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.liquidpress = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.Maxliquidpress = new System.Windows.Forms.TextBox();
            this.Minliquidpress = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.liquidStatus = new System.Windows.Forms.Label();
            this.liquidclose = new System.Windows.Forms.Button();
            this.liquidopen = new System.Windows.Forms.Button();
            this.dgGramset = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.间隔 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbREPT = new System.Windows.Forms.TextBox();
            this.label125 = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.label123 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.tbTANL = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.tbIVOL = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.tbSINT = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.tbFSAM = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.resetInj = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.button15 = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.btnStartAutoINJ = new System.Windows.Forms.Button();
            this.label48 = new System.Windows.Forms.Label();
            this.button24 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStopAutoINJ = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.bControlTemp = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.gbtempset.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPCControl)).BeginInit();
            this.panelJY.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInsamp1)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgtempControl)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvExtEvTP)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdTempControl)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.panConfig.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvHardVersion)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl3)).BeginInit();
            this.tabControl3.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGramset)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle57.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle57.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle57.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle57;
            this.dataGridViewTextBoxColumn9.HeaderText = "阶号";
            this.dataGridViewTextBoxColumn9.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn9.MinimumWidth = 27;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 36;
            // 
            // 事件
            // 
            dataGridViewCellStyle58.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.事件.DefaultCellStyle = dataGridViewCellStyle58;
            this.事件.HeaderText = "事件1 [min]";
            this.事件.Name = "事件";
            this.事件.Width = 66;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle59.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle59.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle59.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle59;
            this.dataGridViewTextBoxColumn10.HeaderText = "事件2 [min]";
            this.dataGridViewTextBoxColumn10.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn10.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 66;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewCellStyle60.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle60.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle60;
            this.dataGridViewTextBoxColumn11.HeaderText = "事件3 [min]";
            this.dataGridViewTextBoxColumn11.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Width = 66;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle61.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle61.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle61.ForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle61;
            this.dataGridViewTextBoxColumn12.HeaderText = "事件4 [min]";
            this.dataGridViewTextBoxColumn12.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 66;
            // 
            // Column1
            // 
            dataGridViewCellStyle62.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle62.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle62.ForeColor = System.Drawing.Color.White;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle62;
            this.Column1.HeaderText = "事件5 [min]";
            this.Column1.Name = "Column1";
            this.Column1.Width = 66;
            // 
            // Column2
            // 
            dataGridViewCellStyle63.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle63.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle63.ForeColor = System.Drawing.Color.White;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle63;
            this.Column2.HeaderText = "事件6 [min]";
            this.Column2.Name = "Column2";
            this.Column2.Width = 66;
            // 
            // Column3
            // 
            dataGridViewCellStyle64.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle64.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle64.ForeColor = System.Drawing.Color.White;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle64;
            this.Column3.HeaderText = "事件7 [min]";
            this.Column3.Name = "Column3";
            this.Column3.Width = 66;
            // 
            // Column4
            // 
            dataGridViewCellStyle65.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle65.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle65.ForeColor = System.Drawing.Color.White;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle65;
            this.Column4.HeaderText = "事件8 [min]";
            this.Column4.Name = "Column4";
            this.Column4.Width = 66;
            // 
            // gbtempset
            // 
            this.gbtempset.Controls.Add(this.tabControl1);
            this.gbtempset.Controls.Add(this.panel1);
            this.gbtempset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbtempset.ForeColor = System.Drawing.Color.Blue;
            this.gbtempset.Location = new System.Drawing.Point(0, 0);
            this.gbtempset.Name = "gbtempset";
            this.gbtempset.Size = new System.Drawing.Size(367, 800);
            this.gbtempset.TabIndex = 1;
            this.gbtempset.TabStop = false;
            this.gbtempset.Text = "仪器设置";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage16);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(361, 728);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.groupBox10);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button35);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(353, 702);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "温度/流量";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(107, 20);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 2;
            this.SendBtn.Text = "SendBtn";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(283, 183);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(49, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "设定";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button35
            // 
            this.button35.Location = new System.Drawing.Point(83, 183);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(115, 23);
            this.button35.TabIndex = 1;
            this.button35.Text = "多位阀使能控制";
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Visible = false;
            this.button35.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(235, 183);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(46, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "查询";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel12);
            this.groupBox4.Controls.Add(this.tabControl2);
            this.groupBox4.Location = new System.Drawing.Point(2, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(336, 368);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "流量控制";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.button6);
            this.panel12.Controls.Add(this.button11);
            this.panel12.Controls.Add(this.EPCControl);
            this.panel12.Controls.Add(this.lFliuBi);
            this.panel12.Controls.Add(this.label69);
            this.panel12.Controls.Add(this.maskedTextBox8);
            this.panel12.Controls.Add(this.label4);
            this.panel12.Controls.Add(this.panelJY);
            this.panel12.Controls.Add(this.label68);
            this.panel12.Controls.Add(this.maskedTextBox3);
            this.panel12.Controls.Add(this.maskedTextBox9);
            this.panel12.Controls.Add(this.label30);
            this.panel12.Controls.Add(this.label12);
            this.panel12.Controls.Add(this.label56);
            this.panel12.Controls.Add(this.maskedTextBox1);
            this.panel12.Controls.Add(this.comboBox6);
            this.panel12.Controls.Add(this.panel7);
            this.panel12.Controls.Add(this.maskedTextBox2);
            this.panel12.Controls.Add(this.panel6);
            this.panel12.Controls.Add(this.label11);
            this.panel12.Controls.Add(this.label8);
            this.panel12.Controls.Add(this.label7);
            this.panel12.Controls.Add(this.label2);
            this.panel12.Controls.Add(this.maskedTextBox4);
            this.panel12.Controls.Add(this.label5);
            this.panel12.Controls.Add(this.radioButton28);
            this.panel12.Controls.Add(this.label9);
            this.panel12.Controls.Add(this.label10);
            this.panel12.Controls.Add(this.label3);
            this.panel12.Location = new System.Drawing.Point(4, 41);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(331, 331);
            this.panel12.TabIndex = 5;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(288, 142);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(42, 23);
            this.button6.TabIndex = 1;
            this.button6.Text = "设定";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(242, 142);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(41, 23);
            this.button11.TabIndex = 1;
            this.button11.Text = "查询";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // EPCControl
            // 
            this.EPCControl.AllowUserToAddRows = false;
            this.EPCControl.AllowUserToDeleteRows = false;
            this.EPCControl.AllowUserToOrderColumns = true;
            this.EPCControl.AllowUserToResizeColumns = false;
            this.EPCControl.AllowUserToResizeRows = false;
            this.EPCControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EPCControl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EPCControl.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle66.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle66.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle66.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle66.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle66.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EPCControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle66;
            this.EPCControl.ColumnHeadersHeight = 25;
            this.EPCControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.EPCControl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38,
            this.Column5});
            dataGridViewCellStyle72.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle72.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle72.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle72.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle72.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle72.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle72.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EPCControl.DefaultCellStyle = dataGridViewCellStyle72;
            this.EPCControl.Location = new System.Drawing.Point(8, 3);
            this.EPCControl.MultiSelect = false;
            this.EPCControl.Name = "EPCControl";
            this.EPCControl.ReadOnly = true;
            this.EPCControl.RowHeadersVisible = false;
            this.EPCControl.RowTemplate.Height = 23;
            this.EPCControl.Size = new System.Drawing.Size(318, 96);
            this.EPCControl.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn35
            // 
            dataGridViewCellStyle67.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle67.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle67.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn35.DefaultCellStyle = dataGridViewCellStyle67;
            this.dataGridViewTextBoxColumn35.HeaderText = "";
            this.dataGridViewTextBoxColumn35.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn36
            // 
            dataGridViewCellStyle68.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle68.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle68.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn36.DefaultCellStyle = dataGridViewCellStyle68;
            this.dataGridViewTextBoxColumn36.HeaderText = "输入(psi)";
            this.dataGridViewTextBoxColumn36.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn37
            // 
            dataGridViewCellStyle69.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle69.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle69.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn37.DefaultCellStyle = dataGridViewCellStyle69;
            this.dataGridViewTextBoxColumn37.HeaderText = "输出(psi)";
            this.dataGridViewTextBoxColumn37.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn38
            // 
            dataGridViewCellStyle70.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle70.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle70.ForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewTextBoxColumn38.DefaultCellStyle = dataGridViewCellStyle70;
            this.dataGridViewTextBoxColumn38.HeaderText = "流量(sccm)";
            this.dataGridViewTextBoxColumn38.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            this.dataGridViewTextBoxColumn38.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            dataGridViewCellStyle71.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle71.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle71.ForeColor = System.Drawing.Color.Red;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle71;
            this.Column5.HeaderText = "开关";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // lFliuBi
            // 
            this.lFliuBi.AutoSize = true;
            this.lFliuBi.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lFliuBi.Location = new System.Drawing.Point(54, 148);
            this.lFliuBi.Name = "lFliuBi";
            this.lFliuBi.Size = new System.Drawing.Size(32, 12);
            this.lFliuBi.TabIndex = 5;
            this.lFliuBi.Text = "*：1";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label69.Location = new System.Drawing.Point(6, 147);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(51, 12);
            this.label69.TabIndex = 5;
            this.label69.Text = "分流比:";
            // 
            // maskedTextBox8
            // 
            this.maskedTextBox8.BackColor = System.Drawing.SystemColors.InfoText;
            this.maskedTextBox8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.maskedTextBox8.Location = new System.Drawing.Point(54, 123);
            this.maskedTextBox8.Name = "maskedTextBox8";
            this.maskedTextBox8.Size = new System.Drawing.Size(49, 21);
            this.maskedTextBox8.TabIndex = 2;
            this.maskedTextBox8.Text = "0.00";
            this.maskedTextBox8.TextChanged += new System.EventHandler(this.maskedTextBox8_TextChanged);
            this.maskedTextBox8.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(236, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "气体:";
            // 
            // panelJY
            // 
            this.panelJY.Controls.Add(this.panel15);
            this.panelJY.Controls.Add(this.dgInsamp1);
            this.panelJY.Location = new System.Drawing.Point(6, 165);
            this.panelJY.Name = "panelJY";
            this.panelJY.Size = new System.Drawing.Size(324, 151);
            this.panelJY.TabIndex = 0;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label13);
            this.panel15.Controls.Add(this.maskedTextBox5);
            this.panel15.Location = new System.Drawing.Point(5, 6);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(66, 43);
            this.panel15.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(3, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "初始时间:";
            // 
            // maskedTextBox5
            // 
            this.maskedTextBox5.BackColor = System.Drawing.SystemColors.InfoText;
            this.maskedTextBox5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.maskedTextBox5.Location = new System.Drawing.Point(3, 20);
            this.maskedTextBox5.Name = "maskedTextBox5";
            this.maskedTextBox5.Size = new System.Drawing.Size(44, 21);
            this.maskedTextBox5.TabIndex = 2;
            this.maskedTextBox5.Text = "000.0";
            this.maskedTextBox5.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // dgInsamp1
            // 
            this.dgInsamp1.AllowUserToAddRows = false;
            this.dgInsamp1.AllowUserToDeleteRows = false;
            this.dgInsamp1.AllowUserToOrderColumns = true;
            this.dgInsamp1.AllowUserToResizeColumns = false;
            this.dgInsamp1.AllowUserToResizeRows = false;
            this.dgInsamp1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInsamp1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle73.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle73.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle73.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle73.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle73.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle73.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgInsamp1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle73;
            this.dgInsamp1.ColumnHeadersHeight = 45;
            this.dgInsamp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgInsamp1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgInsamp1.Location = new System.Drawing.Point(2, 3);
            this.dgInsamp1.MultiSelect = false;
            this.dgInsamp1.Name = "dgInsamp1";
            this.dgInsamp1.RowHeadersVisible = false;
            this.dgInsamp1.RowTemplate.Height = 23;
            this.dgInsamp1.Size = new System.Drawing.Size(324, 141);
            this.dgInsamp1.TabIndex = 0;
            this.dgInsamp1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInsamp1_CellEndEdit);
            this.dgInsamp1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramset_CellEnter);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle74.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle74.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle74.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle74;
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle75.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle75.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle75.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle75;
            this.dataGridViewTextBoxColumn2.HeaderText = "速率    (psi/min)";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle76.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle76.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle76.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle76;
            this.dataGridViewTextBoxColumn3.HeaderText = "保持    (psi)";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle77.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle77.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle77.ForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle77;
            this.dataGridViewTextBoxColumn4.HeaderText = "时间    (min)";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.ForeColor = System.Drawing.Color.Black;
            this.label68.Location = new System.Drawing.Point(124, 128);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(17, 12);
            this.label68.TabIndex = 0;
            this.label68.Text = "×";
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox3.ForeColor = System.Drawing.Color.Lime;
            this.maskedTextBox3.Location = new System.Drawing.Point(154, 23);
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.ReadOnly = true;
            this.maskedTextBox3.Size = new System.Drawing.Size(52, 21);
            this.maskedTextBox3.TabIndex = 2;
            this.maskedTextBox3.Visible = false;
            // 
            // maskedTextBox9
            // 
            this.maskedTextBox9.BackColor = System.Drawing.SystemColors.InfoText;
            this.maskedTextBox9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox9.ForeColor = System.Drawing.Color.RoyalBlue;
            this.maskedTextBox9.Location = new System.Drawing.Point(151, 124);
            this.maskedTextBox9.Name = "maskedTextBox9";
            this.maskedTextBox9.Size = new System.Drawing.Size(59, 21);
            this.maskedTextBox9.TabIndex = 2;
            this.maskedTextBox9.Text = "000.000";
            this.maskedTextBox9.TextChanged += new System.EventHandler(this.maskedTextBox4_TextChanged);
            this.maskedTextBox9.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(109, 127);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(17, 12);
            this.label30.TabIndex = 3;
            this.label30.Text = "mm";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(4, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "色谱柱:";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(216, 128);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(11, 12);
            this.label56.TabIndex = 3;
            this.label56.Text = "m";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox1.ForeColor = System.Drawing.Color.Lime;
            this.maskedTextBox1.Location = new System.Drawing.Point(50, 22);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.ReadOnly = true;
            this.maskedTextBox1.Size = new System.Drawing.Size(47, 21);
            this.maskedTextBox1.TabIndex = 2;
            this.maskedTextBox1.Visible = false;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "氮气",
            "氢气",
            "空气",
            "氦气"});
            this.comboBox6.Location = new System.Drawing.Point(272, 124);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(47, 20);
            this.comboBox6.TabIndex = 2;
            this.comboBox6.Text = "氮气";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.radioButton25);
            this.panel7.Controls.Add(this.radioButton5);
            this.panel7.Controls.Add(this.radioButton6);
            this.panel7.Location = new System.Drawing.Point(51, 103);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(153, 18);
            this.panel7.TabIndex = 1;
            // 
            // radioButton25
            // 
            this.radioButton25.AutoSize = true;
            this.radioButton25.Location = new System.Drawing.Point(97, 1);
            this.radioButton25.Name = "radioButton25";
            this.radioButton25.Size = new System.Drawing.Size(47, 16);
            this.radioButton25.TabIndex = 0;
            this.radioButton25.Text = "分流";
            this.radioButton25.UseVisualStyleBackColor = true;
            this.radioButton25.CheckedChanged += new System.EventHandler(this.radioButton25_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(50, 1);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(47, 16);
            this.radioButton5.TabIndex = 0;
            this.radioButton5.Text = "流量";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Checked = true;
            this.radioButton6.Location = new System.Drawing.Point(3, 2);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(47, 16);
            this.radioButton6.TabIndex = 0;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "压力";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox2.ForeColor = System.Drawing.Color.Lime;
            this.maskedTextBox2.Location = new System.Drawing.Point(239, 25);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.ReadOnly = true;
            this.maskedTextBox2.Size = new System.Drawing.Size(49, 21);
            this.maskedTextBox2.TabIndex = 2;
            this.maskedTextBox2.Visible = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.radioButton3);
            this.panel6.Controls.Add(this.radioButton2);
            this.panel6.Controls.Add(this.radioButton1);
            this.panel6.Location = new System.Drawing.Point(96, 147);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(179, 18);
            this.panel6.TabIndex = 1;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(99, 0);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "吹扫";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(50, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "分流";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "载气";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(290, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "psi";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(122, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "输出:";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "sccm";
            this.label7.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "输入:";
            this.label2.Visible = false;
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.BackColor = System.Drawing.SystemColors.InfoText;
            this.maskedTextBox4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.maskedTextBox4.Location = new System.Drawing.Point(240, 101);
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new System.Drawing.Size(49, 21);
            this.maskedTextBox4.TabIndex = 2;
            this.maskedTextBox4.Text = "000.00";
            this.maskedTextBox4.TextChanged += new System.EventHandler(this.maskedTextBox4_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "psi";
            this.label5.Visible = false;
            // 
            // radioButton28
            // 
            this.radioButton28.AutoSize = true;
            this.radioButton28.Location = new System.Drawing.Point(169, 147);
            this.radioButton28.Name = "radioButton28";
            this.radioButton28.Size = new System.Drawing.Size(35, 16);
            this.radioButton28.TabIndex = 4;
            this.radioButton28.Text = "关";
            this.radioButton28.UseVisualStyleBackColor = true;
            this.radioButton28.Visible = false;
            this.radioButton28.CheckedChanged += new System.EventHandler(this.radioButton28_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(204, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "psi";
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(210, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "设置:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(7, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "模式:";
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage15);
            this.tabControl2.Location = new System.Drawing.Point(3, 16);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(330, 21);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(322, 0);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "进样1";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(322, 0);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "进样2";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(322, 0);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "进样3";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(322, 0);
            this.tabPage9.TabIndex = 3;
            this.tabPage9.Text = "检测器1";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(322, 0);
            this.tabPage10.TabIndex = 4;
            this.tabPage10.Text = "检测器2";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage15
            // 
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(322, 0);
            this.tabPage15.TabIndex = 5;
            this.tabPage15.Text = "检测器3";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgtempControl);
            this.groupBox3.Location = new System.Drawing.Point(8, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 182);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "温度控制";
            // 
            // dgtempControl
            // 
            this.dgtempControl.AllowUserToAddRows = false;
            this.dgtempControl.AllowUserToDeleteRows = false;
            this.dgtempControl.AllowUserToOrderColumns = true;
            this.dgtempControl.AllowUserToResizeColumns = false;
            this.dgtempControl.AllowUserToResizeRows = false;
            this.dgtempControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgtempControl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgtempControl.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle78.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle78.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle78.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle78.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle78.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle78.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle78.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgtempControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle78;
            this.dgtempControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgtempControl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名称,
            this.实测温度,
            this.设定温度,
            this.保护温度});
            dataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle83.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle83.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle83.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle83.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle83.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle83.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgtempControl.DefaultCellStyle = dataGridViewCellStyle83;
            this.dgtempControl.Location = new System.Drawing.Point(6, 14);
            this.dgtempControl.MultiSelect = false;
            this.dgtempControl.Name = "dgtempControl";
            this.dgtempControl.RowHeadersVisible = false;
            this.dgtempControl.RowTemplate.Height = 17;
            this.dgtempControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgtempControl.Size = new System.Drawing.Size(315, 163);
            this.dgtempControl.TabIndex = 0;
            this.dgtempControl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgtempControl_CellClick);
            this.dgtempControl.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgtempControl_CellEndEdit);
            this.dgtempControl.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramset_CellEnter);
            this.dgtempControl.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // 名称
            // 
            dataGridViewCellStyle79.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle79.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle79.ForeColor = System.Drawing.Color.White;
            this.名称.DefaultCellStyle = dataGridViewCellStyle79;
            this.名称.HeaderText = "";
            this.名称.MinimumWidth = 20;
            this.名称.Name = "名称";
            this.名称.ReadOnly = true;
            this.名称.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 实测温度
            // 
            dataGridViewCellStyle80.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle80.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle80.ForeColor = System.Drawing.Color.Lime;
            this.实测温度.DefaultCellStyle = dataGridViewCellStyle80;
            this.实测温度.HeaderText = "实测(°C)";
            this.实测温度.MaxInputLength = 10;
            this.实测温度.Name = "实测温度";
            this.实测温度.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 设定温度
            // 
            dataGridViewCellStyle81.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle81.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle81.ForeColor = System.Drawing.Color.White;
            this.设定温度.DefaultCellStyle = dataGridViewCellStyle81;
            this.设定温度.HeaderText = "设定(°C)";
            this.设定温度.MaxInputLength = 10;
            this.设定温度.Name = "设定温度";
            this.设定温度.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 保护温度
            // 
            dataGridViewCellStyle82.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle82.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle82.ForeColor = System.Drawing.Color.Yellow;
            this.保护温度.DefaultCellStyle = dataGridViewCellStyle82;
            this.保护温度.HeaderText = "保护(°C)";
            this.保护温度.MaxInputLength = 10;
            this.保护温度.Name = "保护温度";
            this.保护温度.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(353, 702);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "程升/事件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button34);
            this.groupBox7.Controls.Add(this.button33);
            this.groupBox7.Controls.Add(this.label75);
            this.groupBox7.Controls.Add(this.checkBox_0);
            this.groupBox7.Controls.Add(this.checkBox_1);
            this.groupBox7.Controls.Add(this.checkBox_2);
            this.groupBox7.Controls.Add(this.checkBox_3);
            this.groupBox7.Controls.Add(this.checkBox_4);
            this.groupBox7.Controls.Add(this.checkBox_5);
            this.groupBox7.Controls.Add(this.checkBox_6);
            this.groupBox7.Controls.Add(this.checkBox_7);
            this.groupBox7.Controls.Add(this.gvExtEvTP);
            this.groupBox7.Location = new System.Drawing.Point(5, 236);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(332, 302);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "事件控制";
            // 
            // button34
            // 
            this.button34.Location = new System.Drawing.Point(212, 15);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(46, 23);
            this.button34.TabIndex = 4;
            this.button34.Text = "查询";
            this.button34.UseVisualStyleBackColor = true;
            this.button34.Click += new System.EventHandler(this.button34_Click_1);
            // 
            // button33
            // 
            this.button33.Location = new System.Drawing.Point(270, 15);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(46, 23);
            this.button33.TabIndex = 4;
            this.button33.Text = "设定";
            this.button33.UseVisualStyleBackColor = true;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(6, 21);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(59, 12);
            this.label75.TabIndex = 3;
            this.label75.Text = "事件开关:";
            // 
            // checkBox_0
            // 
            this.checkBox_0.AutoSize = true;
            this.checkBox_0.Location = new System.Drawing.Point(70, 21);
            this.checkBox_0.Name = "checkBox_0";
            this.checkBox_0.Size = new System.Drawing.Size(15, 14);
            this.checkBox_0.TabIndex = 2;
            this.checkBox_0.UseVisualStyleBackColor = true;
            // 
            // checkBox_1
            // 
            this.checkBox_1.AutoSize = true;
            this.checkBox_1.Location = new System.Drawing.Point(87, 21);
            this.checkBox_1.Name = "checkBox_1";
            this.checkBox_1.Size = new System.Drawing.Size(15, 14);
            this.checkBox_1.TabIndex = 1;
            this.checkBox_1.UseVisualStyleBackColor = true;
            // 
            // checkBox_2
            // 
            this.checkBox_2.AutoSize = true;
            this.checkBox_2.Location = new System.Drawing.Point(138, 21);
            this.checkBox_2.Name = "checkBox_2";
            this.checkBox_2.Size = new System.Drawing.Size(15, 14);
            this.checkBox_2.TabIndex = 2;
            this.checkBox_2.UseVisualStyleBackColor = true;
            // 
            // checkBox_3
            // 
            this.checkBox_3.AutoSize = true;
            this.checkBox_3.Location = new System.Drawing.Point(104, 21);
            this.checkBox_3.Name = "checkBox_3";
            this.checkBox_3.Size = new System.Drawing.Size(15, 14);
            this.checkBox_3.TabIndex = 1;
            this.checkBox_3.UseVisualStyleBackColor = true;
            // 
            // checkBox_4
            // 
            this.checkBox_4.AutoSize = true;
            this.checkBox_4.Location = new System.Drawing.Point(155, 21);
            this.checkBox_4.Name = "checkBox_4";
            this.checkBox_4.Size = new System.Drawing.Size(15, 14);
            this.checkBox_4.TabIndex = 1;
            this.checkBox_4.UseVisualStyleBackColor = true;
            // 
            // checkBox_5
            // 
            this.checkBox_5.AutoSize = true;
            this.checkBox_5.Location = new System.Drawing.Point(121, 21);
            this.checkBox_5.Name = "checkBox_5";
            this.checkBox_5.Size = new System.Drawing.Size(15, 14);
            this.checkBox_5.TabIndex = 1;
            this.checkBox_5.UseVisualStyleBackColor = true;
            // 
            // checkBox_6
            // 
            this.checkBox_6.AutoSize = true;
            this.checkBox_6.Location = new System.Drawing.Point(172, 21);
            this.checkBox_6.Name = "checkBox_6";
            this.checkBox_6.Size = new System.Drawing.Size(15, 14);
            this.checkBox_6.TabIndex = 1;
            this.checkBox_6.UseVisualStyleBackColor = true;
            // 
            // checkBox_7
            // 
            this.checkBox_7.AutoSize = true;
            this.checkBox_7.Location = new System.Drawing.Point(189, 21);
            this.checkBox_7.Name = "checkBox_7";
            this.checkBox_7.Size = new System.Drawing.Size(15, 14);
            this.checkBox_7.TabIndex = 1;
            this.checkBox_7.UseVisualStyleBackColor = true;
            // 
            // gvExtEvTP
            // 
            this.gvExtEvTP.AllowUserToAddRows = false;
            this.gvExtEvTP.AllowUserToDeleteRows = false;
            this.gvExtEvTP.AllowUserToOrderColumns = true;
            this.gvExtEvTP.AllowUserToResizeColumns = false;
            this.gvExtEvTP.AllowUserToResizeRows = false;
            this.gvExtEvTP.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gvExtEvTP.ColumnHeadersHeight = 46;
            this.gvExtEvTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvExtEvTP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.事件,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.gvExtEvTP.Location = new System.Drawing.Point(3, 44);
            this.gvExtEvTP.MultiSelect = false;
            this.gvExtEvTP.Name = "gvExtEvTP";
            this.gvExtEvTP.RowHeadersVisible = false;
            this.gvExtEvTP.RowTemplate.Height = 23;
            this.gvExtEvTP.Size = new System.Drawing.Size(324, 252);
            this.gvExtEvTP.TabIndex = 0;
            this.gvExtEvTP.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvExtEvTP_CellEndEdit);
            this.gvExtEvTP.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramset_CellEnter);
            this.gvExtEvTP.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.gdTempControl);
            this.groupBox6.Controls.Add(this.button9);
            this.groupBox6.Controls.Add(this.tbptIniTempHoldT);
            this.groupBox6.Controls.Add(this.button10);
            this.groupBox6.Controls.Add(this.button7);
            this.groupBox6.Controls.Add(this.button8);
            this.groupBox6.Controls.Add(this.label35);
            this.groupBox6.Location = new System.Drawing.Point(6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(331, 230);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "程升控制";
            // 
            // gdTempControl
            // 
            this.gdTempControl.AllowUserToAddRows = false;
            this.gdTempControl.AllowUserToDeleteRows = false;
            this.gdTempControl.AllowUserToOrderColumns = true;
            this.gdTempControl.AllowUserToResizeColumns = false;
            this.gdTempControl.AllowUserToResizeRows = false;
            this.gdTempControl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gdTempControl.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle84.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle84.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle84.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle84.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle84.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gdTempControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle84;
            this.gdTempControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gdTempControl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.gdTempControl.Location = new System.Drawing.Point(0, 27);
            this.gdTempControl.MultiSelect = false;
            this.gdTempControl.Name = "gdTempControl";
            this.gdTempControl.RowHeadersVisible = false;
            this.gdTempControl.RowTemplate.Height = 23;
            this.gdTempControl.Size = new System.Drawing.Size(325, 169);
            this.gdTempControl.TabIndex = 2;
            this.gdTempControl.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdTempControl_CellEndEdit);
            this.gdTempControl.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramset_CellEnter);
            this.gdTempControl.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle85.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle85.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle85.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle85;
            this.dataGridViewTextBoxColumn5.HeaderText = "阶号";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle86.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle86.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle86.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle86;
            this.dataGridViewTextBoxColumn6.HeaderText = "升温速率";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn6.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle87.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle87.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle87.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle87;
            this.dataGridViewTextBoxColumn7.HeaderText = "保持温度";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle88.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle88.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle88.ForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle88;
            this.dataGridViewTextBoxColumn8.HeaderText = "保持时间";
            this.dataGridViewTextBoxColumn8.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(59, 201);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(49, 23);
            this.button9.TabIndex = 1;
            this.button9.Text = "设定";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            // 
            // tbptIniTempHoldT
            // 
            this.tbptIniTempHoldT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbptIniTempHoldT.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbptIniTempHoldT.ForeColor = System.Drawing.Color.Lime;
            this.tbptIniTempHoldT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbptIniTempHoldT.Location = new System.Drawing.Point(251, 0);
            this.tbptIniTempHoldT.Name = "tbptIniTempHoldT";
            this.tbptIniTempHoldT.Size = new System.Drawing.Size(52, 21);
            this.tbptIniTempHoldT.TabIndex = 2;
            this.tbptIniTempHoldT.Text = "0";
            this.tbptIniTempHoldT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbptIniTempHoldT.TextChanged += new System.EventHandler(this.tbptIniTempHoldT_TextChanged);
            this.tbptIniTempHoldT.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(6, 201);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(47, 23);
            this.button10.TabIndex = 1;
            this.button10.Text = "查询";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Visible = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(269, 202);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(49, 23);
            this.button7.TabIndex = 1;
            this.button7.Text = "设定";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(145, 202);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(47, 23);
            this.button8.TabIndex = 1;
            this.button8.Text = "查询";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.Location = new System.Drawing.Point(193, 5);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(64, 12);
            this.label35.TabIndex = 0;
            this.label35.Text = "初始时间:";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.groupBox16);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.liquidstatusselect);
            this.tabPage3.Controls.Add(this.groupBox14);
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(353, 702);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "自动进样";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label117);
            this.groupBox16.Controls.Add(this.tbCompen12);
            this.groupBox16.Controls.Add(this.tbCompen11);
            this.groupBox16.Controls.Add(this.tbCompen10);
            this.groupBox16.Controls.Add(this.tbCompen9);
            this.groupBox16.Controls.Add(this.tbCompen8);
            this.groupBox16.Controls.Add(this.tbCompen7);
            this.groupBox16.Controls.Add(this.tbCompen6);
            this.groupBox16.Controls.Add(this.tbCompen5);
            this.groupBox16.Controls.Add(this.tbCompen4);
            this.groupBox16.Controls.Add(this.tbCompen3);
            this.groupBox16.Controls.Add(this.tbCompen2);
            this.groupBox16.Controls.Add(this.tbCompen1);
            this.groupBox16.Controls.Add(this.label116);
            this.groupBox16.Controls.Add(this.btnSave);
            this.groupBox16.Controls.Add(this.tbChannelACnt);
            this.groupBox16.Controls.Add(this.label115);
            this.groupBox16.Controls.Add(this.label109);
            this.groupBox16.Controls.Add(this.label110);
            this.groupBox16.Controls.Add(this.label111);
            this.groupBox16.Controls.Add(this.label112);
            this.groupBox16.Controls.Add(this.label113);
            this.groupBox16.Controls.Add(this.label114);
            this.groupBox16.Controls.Add(this.label107);
            this.groupBox16.Controls.Add(this.label108);
            this.groupBox16.Controls.Add(this.label105);
            this.groupBox16.Controls.Add(this.label106);
            this.groupBox16.Controls.Add(this.label104);
            this.groupBox16.Controls.Add(this.label103);
            this.groupBox16.Controls.Add(this.tbMount2012);
            this.groupBox16.Controls.Add(this.tbMount412);
            this.groupBox16.Controls.Add(this.tbMount2011);
            this.groupBox16.Controls.Add(this.tbMount411);
            this.groupBox16.Controls.Add(this.tbMount2010);
            this.groupBox16.Controls.Add(this.tbMount410);
            this.groupBox16.Controls.Add(this.tbMount209);
            this.groupBox16.Controls.Add(this.tbMount49);
            this.groupBox16.Controls.Add(this.tbMount208);
            this.groupBox16.Controls.Add(this.tbMount48);
            this.groupBox16.Controls.Add(this.tbMount207);
            this.groupBox16.Controls.Add(this.tbMount47);
            this.groupBox16.Controls.Add(this.label101);
            this.groupBox16.Controls.Add(this.label102);
            this.groupBox16.Controls.Add(this.tbMount206);
            this.groupBox16.Controls.Add(this.tbMount46);
            this.groupBox16.Controls.Add(this.tbMount205);
            this.groupBox16.Controls.Add(this.tbMount45);
            this.groupBox16.Controls.Add(this.tbMount204);
            this.groupBox16.Controls.Add(this.tbMount44);
            this.groupBox16.Controls.Add(this.tbMount203);
            this.groupBox16.Controls.Add(this.tbMount43);
            this.groupBox16.Controls.Add(this.tbMount202);
            this.groupBox16.Controls.Add(this.tbMount42);
            this.groupBox16.Controls.Add(this.btnMountTest);
            this.groupBox16.Controls.Add(this.label94);
            this.groupBox16.Controls.Add(this.tbMountTest);
            this.groupBox16.Controls.Add(this.tbMount201);
            this.groupBox16.Controls.Add(this.tbMount41);
            this.groupBox16.Controls.Add(this.label93);
            this.groupBox16.Controls.Add(this.label92);
            this.groupBox16.Location = new System.Drawing.Point(3, 117);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(334, 284);
            this.groupBox16.TabIndex = 5;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "4-20mA设置";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(302, 25);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(29, 12);
            this.label117.TabIndex = 59;
            this.label117.Text = "补偿";
            // 
            // tbCompen12
            // 
            this.tbCompen12.Location = new System.Drawing.Point(301, 167);
            this.tbCompen12.Name = "tbCompen12";
            this.tbCompen12.Size = new System.Drawing.Size(30, 21);
            this.tbCompen12.TabIndex = 58;
            this.tbCompen12.Text = "100";
            // 
            // tbCompen11
            // 
            this.tbCompen11.Location = new System.Drawing.Point(301, 142);
            this.tbCompen11.Name = "tbCompen11";
            this.tbCompen11.Size = new System.Drawing.Size(30, 21);
            this.tbCompen11.TabIndex = 57;
            this.tbCompen11.Text = "100";
            // 
            // tbCompen10
            // 
            this.tbCompen10.Location = new System.Drawing.Point(302, 117);
            this.tbCompen10.Name = "tbCompen10";
            this.tbCompen10.Size = new System.Drawing.Size(29, 21);
            this.tbCompen10.TabIndex = 56;
            this.tbCompen10.Text = "100";
            // 
            // tbCompen9
            // 
            this.tbCompen9.Location = new System.Drawing.Point(302, 92);
            this.tbCompen9.Name = "tbCompen9";
            this.tbCompen9.Size = new System.Drawing.Size(29, 21);
            this.tbCompen9.TabIndex = 55;
            this.tbCompen9.Text = "100";
            // 
            // tbCompen8
            // 
            this.tbCompen8.Location = new System.Drawing.Point(302, 66);
            this.tbCompen8.Name = "tbCompen8";
            this.tbCompen8.Size = new System.Drawing.Size(29, 21);
            this.tbCompen8.TabIndex = 54;
            this.tbCompen8.Text = "100";
            // 
            // tbCompen7
            // 
            this.tbCompen7.Location = new System.Drawing.Point(302, 41);
            this.tbCompen7.Name = "tbCompen7";
            this.tbCompen7.Size = new System.Drawing.Size(29, 21);
            this.tbCompen7.TabIndex = 53;
            this.tbCompen7.Text = "100";
            // 
            // tbCompen6
            // 
            this.tbCompen6.Location = new System.Drawing.Point(124, 167);
            this.tbCompen6.Name = "tbCompen6";
            this.tbCompen6.Size = new System.Drawing.Size(30, 21);
            this.tbCompen6.TabIndex = 52;
            this.tbCompen6.Text = "100";
            // 
            // tbCompen5
            // 
            this.tbCompen5.Location = new System.Drawing.Point(124, 142);
            this.tbCompen5.Name = "tbCompen5";
            this.tbCompen5.Size = new System.Drawing.Size(30, 21);
            this.tbCompen5.TabIndex = 51;
            this.tbCompen5.Text = "100";
            // 
            // tbCompen4
            // 
            this.tbCompen4.Location = new System.Drawing.Point(125, 117);
            this.tbCompen4.Name = "tbCompen4";
            this.tbCompen4.Size = new System.Drawing.Size(29, 21);
            this.tbCompen4.TabIndex = 50;
            this.tbCompen4.Text = "100";
            // 
            // tbCompen3
            // 
            this.tbCompen3.Location = new System.Drawing.Point(125, 92);
            this.tbCompen3.Name = "tbCompen3";
            this.tbCompen3.Size = new System.Drawing.Size(29, 21);
            this.tbCompen3.TabIndex = 49;
            this.tbCompen3.Text = "100";
            // 
            // tbCompen2
            // 
            this.tbCompen2.Location = new System.Drawing.Point(125, 66);
            this.tbCompen2.Name = "tbCompen2";
            this.tbCompen2.Size = new System.Drawing.Size(29, 21);
            this.tbCompen2.TabIndex = 48;
            this.tbCompen2.Text = "100";
            // 
            // tbCompen1
            // 
            this.tbCompen1.Location = new System.Drawing.Point(125, 41);
            this.tbCompen1.Name = "tbCompen1";
            this.tbCompen1.Size = new System.Drawing.Size(29, 21);
            this.tbCompen1.TabIndex = 47;
            this.tbCompen1.Text = "100";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(123, 25);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(29, 12);
            this.label116.TabIndex = 46;
            this.label116.Text = "补偿";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(206, 207);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 23);
            this.btnSave.TabIndex = 45;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tbChannelACnt
            // 
            this.tbChannelACnt.Location = new System.Drawing.Point(76, 201);
            this.tbChannelACnt.Name = "tbChannelACnt";
            this.tbChannelACnt.Size = new System.Drawing.Size(100, 21);
            this.tbChannelACnt.TabIndex = 44;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(7, 204);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(71, 12);
            this.label115.TabIndex = 43;
            this.label115.Text = "通道1路数：";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(177, 167);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(23, 12);
            this.label109.TabIndex = 42;
            this.label109.Text = "12:";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(176, 142);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(23, 12);
            this.label110.TabIndex = 41;
            this.label110.Text = "11:";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(177, 117);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(23, 12);
            this.label111.TabIndex = 40;
            this.label111.Text = "10:";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(176, 92);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(17, 12);
            this.label112.TabIndex = 39;
            this.label112.Text = "9:";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(177, 66);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(17, 12);
            this.label113.TabIndex = 38;
            this.label113.Text = "8:";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(176, 41);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(17, 12);
            this.label114.TabIndex = 37;
            this.label114.Text = "7:";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(8, 170);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(17, 12);
            this.label107.TabIndex = 36;
            this.label107.Text = "6:";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(7, 145);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(17, 12);
            this.label108.TabIndex = 35;
            this.label108.Text = "5:";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(8, 120);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(17, 12);
            this.label105.TabIndex = 34;
            this.label105.Text = "4:";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(7, 95);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(17, 12);
            this.label106.TabIndex = 33;
            this.label106.Text = "3:";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(8, 69);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(17, 12);
            this.label104.TabIndex = 32;
            this.label104.Text = "2:";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(7, 44);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(17, 12);
            this.label103.TabIndex = 31;
            this.label103.Text = "1:";
            // 
            // tbMount2012
            // 
            this.tbMount2012.Location = new System.Drawing.Point(250, 167);
            this.tbMount2012.Name = "tbMount2012";
            this.tbMount2012.Size = new System.Drawing.Size(45, 21);
            this.tbMount2012.TabIndex = 30;
            this.tbMount2012.Text = "100";
            // 
            // tbMount412
            // 
            this.tbMount412.Location = new System.Drawing.Point(202, 167);
            this.tbMount412.Name = "tbMount412";
            this.tbMount412.Size = new System.Drawing.Size(45, 21);
            this.tbMount412.TabIndex = 29;
            this.tbMount412.Text = "0";
            // 
            // tbMount2011
            // 
            this.tbMount2011.Location = new System.Drawing.Point(250, 142);
            this.tbMount2011.Name = "tbMount2011";
            this.tbMount2011.Size = new System.Drawing.Size(45, 21);
            this.tbMount2011.TabIndex = 28;
            this.tbMount2011.Text = "100";
            // 
            // tbMount411
            // 
            this.tbMount411.Location = new System.Drawing.Point(202, 142);
            this.tbMount411.Name = "tbMount411";
            this.tbMount411.Size = new System.Drawing.Size(45, 21);
            this.tbMount411.TabIndex = 27;
            this.tbMount411.Text = "0";
            // 
            // tbMount2010
            // 
            this.tbMount2010.Location = new System.Drawing.Point(251, 117);
            this.tbMount2010.Name = "tbMount2010";
            this.tbMount2010.Size = new System.Drawing.Size(45, 21);
            this.tbMount2010.TabIndex = 26;
            this.tbMount2010.Text = "100";
            // 
            // tbMount410
            // 
            this.tbMount410.Location = new System.Drawing.Point(203, 117);
            this.tbMount410.Name = "tbMount410";
            this.tbMount410.Size = new System.Drawing.Size(45, 21);
            this.tbMount410.TabIndex = 25;
            this.tbMount410.Text = "0";
            // 
            // tbMount209
            // 
            this.tbMount209.Location = new System.Drawing.Point(251, 92);
            this.tbMount209.Name = "tbMount209";
            this.tbMount209.Size = new System.Drawing.Size(45, 21);
            this.tbMount209.TabIndex = 24;
            this.tbMount209.Text = "100";
            // 
            // tbMount49
            // 
            this.tbMount49.Location = new System.Drawing.Point(203, 92);
            this.tbMount49.Name = "tbMount49";
            this.tbMount49.Size = new System.Drawing.Size(45, 21);
            this.tbMount49.TabIndex = 23;
            this.tbMount49.Text = "0";
            // 
            // tbMount208
            // 
            this.tbMount208.Location = new System.Drawing.Point(251, 66);
            this.tbMount208.Name = "tbMount208";
            this.tbMount208.Size = new System.Drawing.Size(45, 21);
            this.tbMount208.TabIndex = 22;
            this.tbMount208.Text = "100";
            // 
            // tbMount48
            // 
            this.tbMount48.Location = new System.Drawing.Point(203, 66);
            this.tbMount48.Name = "tbMount48";
            this.tbMount48.Size = new System.Drawing.Size(45, 21);
            this.tbMount48.TabIndex = 21;
            this.tbMount48.Text = "0";
            // 
            // tbMount207
            // 
            this.tbMount207.Location = new System.Drawing.Point(251, 41);
            this.tbMount207.Name = "tbMount207";
            this.tbMount207.Size = new System.Drawing.Size(45, 21);
            this.tbMount207.TabIndex = 20;
            this.tbMount207.Text = "100";
            // 
            // tbMount47
            // 
            this.tbMount47.Location = new System.Drawing.Point(203, 41);
            this.tbMount47.Name = "tbMount47";
            this.tbMount47.Size = new System.Drawing.Size(45, 21);
            this.tbMount47.TabIndex = 19;
            this.tbMount47.Text = "0";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(249, 25);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(53, 12);
            this.label101.TabIndex = 18;
            this.label101.Text = "20mA浓度";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(201, 25);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(47, 12);
            this.label102.TabIndex = 17;
            this.label102.Text = "4mA浓度";
            // 
            // tbMount206
            // 
            this.tbMount206.Location = new System.Drawing.Point(73, 167);
            this.tbMount206.Name = "tbMount206";
            this.tbMount206.Size = new System.Drawing.Size(45, 21);
            this.tbMount206.TabIndex = 16;
            this.tbMount206.Text = "100";
            // 
            // tbMount46
            // 
            this.tbMount46.Location = new System.Drawing.Point(25, 167);
            this.tbMount46.Name = "tbMount46";
            this.tbMount46.Size = new System.Drawing.Size(45, 21);
            this.tbMount46.TabIndex = 15;
            this.tbMount46.Text = "0";
            // 
            // tbMount205
            // 
            this.tbMount205.Location = new System.Drawing.Point(73, 142);
            this.tbMount205.Name = "tbMount205";
            this.tbMount205.Size = new System.Drawing.Size(45, 21);
            this.tbMount205.TabIndex = 14;
            this.tbMount205.Text = "100";
            // 
            // tbMount45
            // 
            this.tbMount45.Location = new System.Drawing.Point(25, 142);
            this.tbMount45.Name = "tbMount45";
            this.tbMount45.Size = new System.Drawing.Size(45, 21);
            this.tbMount45.TabIndex = 13;
            this.tbMount45.Text = "0";
            // 
            // tbMount204
            // 
            this.tbMount204.Location = new System.Drawing.Point(74, 117);
            this.tbMount204.Name = "tbMount204";
            this.tbMount204.Size = new System.Drawing.Size(45, 21);
            this.tbMount204.TabIndex = 12;
            this.tbMount204.Text = "100";
            // 
            // tbMount44
            // 
            this.tbMount44.Location = new System.Drawing.Point(26, 117);
            this.tbMount44.Name = "tbMount44";
            this.tbMount44.Size = new System.Drawing.Size(45, 21);
            this.tbMount44.TabIndex = 11;
            this.tbMount44.Text = "0";
            // 
            // tbMount203
            // 
            this.tbMount203.Location = new System.Drawing.Point(74, 92);
            this.tbMount203.Name = "tbMount203";
            this.tbMount203.Size = new System.Drawing.Size(45, 21);
            this.tbMount203.TabIndex = 10;
            this.tbMount203.Text = "100";
            // 
            // tbMount43
            // 
            this.tbMount43.Location = new System.Drawing.Point(26, 92);
            this.tbMount43.Name = "tbMount43";
            this.tbMount43.Size = new System.Drawing.Size(45, 21);
            this.tbMount43.TabIndex = 9;
            this.tbMount43.Text = "0";
            // 
            // tbMount202
            // 
            this.tbMount202.Location = new System.Drawing.Point(74, 66);
            this.tbMount202.Name = "tbMount202";
            this.tbMount202.Size = new System.Drawing.Size(45, 21);
            this.tbMount202.TabIndex = 8;
            this.tbMount202.Text = "100";
            // 
            // tbMount42
            // 
            this.tbMount42.Location = new System.Drawing.Point(26, 66);
            this.tbMount42.Name = "tbMount42";
            this.tbMount42.Size = new System.Drawing.Size(45, 21);
            this.tbMount42.TabIndex = 7;
            this.tbMount42.Text = "0";
            // 
            // btnMountTest
            // 
            this.btnMountTest.Location = new System.Drawing.Point(206, 240);
            this.btnMountTest.Name = "btnMountTest";
            this.btnMountTest.Size = new System.Drawing.Size(96, 22);
            this.btnMountTest.TabIndex = 6;
            this.btnMountTest.Text = "测试数据上传";
            this.btnMountTest.UseVisualStyleBackColor = true;
            this.btnMountTest.Click += new System.EventHandler(this.BtnMountTest_Click);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(6, 244);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(53, 12);
            this.label94.TabIndex = 5;
            this.label94.Text = "测试数据";
            // 
            // tbMountTest
            // 
            this.tbMountTest.Location = new System.Drawing.Point(76, 240);
            this.tbMountTest.Name = "tbMountTest";
            this.tbMountTest.Size = new System.Drawing.Size(100, 21);
            this.tbMountTest.TabIndex = 4;
            this.tbMountTest.Text = "0";
            // 
            // tbMount201
            // 
            this.tbMount201.Location = new System.Drawing.Point(74, 41);
            this.tbMount201.Name = "tbMount201";
            this.tbMount201.Size = new System.Drawing.Size(45, 21);
            this.tbMount201.TabIndex = 3;
            this.tbMount201.Text = "100";
            // 
            // tbMount41
            // 
            this.tbMount41.Location = new System.Drawing.Point(26, 41);
            this.tbMount41.Name = "tbMount41";
            this.tbMount41.Size = new System.Drawing.Size(42, 21);
            this.tbMount41.TabIndex = 2;
            this.tbMount41.Text = "0";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(72, 25);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(53, 12);
            this.label93.TabIndex = 1;
            this.label93.Text = "20mA浓度";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(24, 25);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(47, 12);
            this.label92.TabIndex = 0;
            this.label92.Text = "4mA浓度";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chbTestModbus);
            this.groupBox2.Location = new System.Drawing.Point(4, 439);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(333, 81);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modbus";
            // 
            // chbTestModbus
            // 
            this.chbTestModbus.AutoSize = true;
            this.chbTestModbus.Location = new System.Drawing.Point(3, 19);
            this.chbTestModbus.Margin = new System.Windows.Forms.Padding(2);
            this.chbTestModbus.Name = "chbTestModbus";
            this.chbTestModbus.Size = new System.Drawing.Size(72, 16);
            this.chbTestModbus.TabIndex = 1;
            this.chbTestModbus.Text = "测试通信";
            this.chbTestModbus.UseVisualStyleBackColor = true;
            this.chbTestModbus.CheckedChanged += new System.EventHandler(this.ChbTestModbus_CheckedChanged);
            // 
            // liquidstatusselect
            // 
            this.liquidstatusselect.Location = new System.Drawing.Point(87, 541);
            this.liquidstatusselect.Name = "liquidstatusselect";
            this.liquidstatusselect.Size = new System.Drawing.Size(82, 23);
            this.liquidstatusselect.TabIndex = 0;
            this.liquidstatusselect.Text = "状态查询";
            this.liquidstatusselect.UseVisualStyleBackColor = true;
            this.liquidstatusselect.Visible = false;
            this.liquidstatusselect.Click += new System.EventHandler(this.liquidstatusselect_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.button21);
            this.groupBox14.Controls.Add(this.button20);
            this.groupBox14.Controls.Add(this.maskedTextBox20);
            this.groupBox14.Controls.Add(this.label44);
            this.groupBox14.Controls.Add(this.label6);
            this.groupBox14.Controls.Add(this.label67);
            this.groupBox14.Controls.Add(this.label66);
            this.groupBox14.Controls.Add(this.maskedTextBox14);
            this.groupBox14.Controls.Add(this.label14);
            this.groupBox14.Controls.Add(this.maskedTextBox19);
            this.groupBox14.Controls.Add(this.label65);
            this.groupBox14.Location = new System.Drawing.Point(4, 41);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(333, 70);
            this.groupBox14.TabIndex = 1;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "自动进样";
            // 
            // button21
            // 
            this.button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button21.Location = new System.Drawing.Point(256, 43);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(71, 23);
            this.button21.TabIndex = 3;
            this.button21.Text = "设定";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button20
            // 
            this.button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button20.Location = new System.Drawing.Point(182, 43);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(65, 23);
            this.button20.TabIndex = 2;
            this.button20.Text = "查询";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // maskedTextBox20
            // 
            this.maskedTextBox20.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.maskedTextBox20.Location = new System.Drawing.Point(256, 17);
            this.maskedTextBox20.Name = "maskedTextBox20";
            this.maskedTextBox20.Size = new System.Drawing.Size(40, 21);
            this.maskedTextBox20.TabIndex = 1;
            this.maskedTextBox20.Text = "0";
            this.maskedTextBox20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox20.TextChanged += new System.EventHandler(this.maskedTextBox20_TextChanged);
            this.maskedTextBox20.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(136, 51);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(17, 12);
            this.label44.TabIndex = 0;
            this.label44.Text = "秒";
            this.label44.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "次";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(302, 20);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(23, 12);
            this.label67.TabIndex = 0;
            this.label67.Text = "min";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(181, 20);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(65, 12);
            this.label66.TabIndex = 0;
            this.label66.Text = "循环时间：";
            // 
            // maskedTextBox14
            // 
            this.maskedTextBox14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.maskedTextBox14.Location = new System.Drawing.Point(85, 44);
            this.maskedTextBox14.Name = "maskedTextBox14";
            this.maskedTextBox14.ReadOnly = true;
            this.maskedTextBox14.Size = new System.Drawing.Size(37, 21);
            this.maskedTextBox14.TabIndex = 1;
            this.maskedTextBox14.Text = "99";
            this.maskedTextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox14.Visible = false;
            this.maskedTextBox14.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "背光时间：";
            this.label14.Visible = false;
            // 
            // maskedTextBox19
            // 
            this.maskedTextBox19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maskedTextBox19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.maskedTextBox19.Location = new System.Drawing.Point(86, 17);
            this.maskedTextBox19.Name = "maskedTextBox19";
            this.maskedTextBox19.Size = new System.Drawing.Size(37, 21);
            this.maskedTextBox19.TabIndex = 1;
            this.maskedTextBox19.Text = "0";
            this.maskedTextBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox19.TextChanged += new System.EventHandler(this.maskedTextBox19_TextChanged);
            this.maskedTextBox19.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(15, 20);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(65, 12);
            this.label65.TabIndex = 0;
            this.label65.Text = "进样次数：";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox7);
            this.groupBox8.Controls.Add(this.textBox6);
            this.groupBox8.Controls.Add(this.textBox5);
            this.groupBox8.Controls.Add(this.textBox4);
            this.groupBox8.Controls.Add(this.label38);
            this.groupBox8.Controls.Add(this.label37);
            this.groupBox8.Controls.Add(this.label36);
            this.groupBox8.Location = new System.Drawing.Point(3, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(337, 38);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "状态";
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox7.Location = new System.Drawing.Point(279, 14);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(39, 21);
            this.textBox7.TabIndex = 1;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox6.Location = new System.Drawing.Point(234, 14);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(39, 21);
            this.textBox6.TabIndex = 1;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox5.Location = new System.Drawing.Point(133, 14);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(39, 21);
            this.textBox5.TabIndex = 1;
            this.textBox5.Text = "空闲";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(88, 14);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(39, 21);
            this.textBox4.TabIndex = 1;
            this.textBox4.Text = "离线";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(270, 17);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(11, 12);
            this.label38.TabIndex = 0;
            this.label38.Text = "/";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(175, 17);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(65, 12);
            this.label37.TabIndex = 0;
            this.label37.Text = "瓶号/针号:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(18, 17);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(71, 12);
            this.label36.TabIndex = 0;
            this.label36.Text = "进样器状态:";
            // 
            // tabPage5
            // 
            this.tabPage5.AutoScroll = true;
            this.tabPage5.Controls.Add(this.groupBox17);
            this.tabPage5.Controls.Add(this.button27);
            this.tabPage5.Controls.Add(this.button18);
            this.tabPage5.Controls.Add(this.groupBox5);
            this.tabPage5.Controls.Add(this.button13);
            this.tabPage5.Controls.Add(this.groupBox12);
            this.tabPage5.Controls.Add(this.gvHardVersion);
            this.tabPage5.Controls.Add(this.button19);
            this.tabPage5.Controls.Add(this.groupBox13);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(353, 702);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "网络/版本";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.panConfig);
            this.groupBox17.Controls.Add(this.tBpasswordShJ);
            this.groupBox17.Controls.Add(this.btnShuaijian);
            this.groupBox17.Location = new System.Drawing.Point(6, 185);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(341, 175);
            this.groupBox17.TabIndex = 29;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "厂家配置";
            // 
            // panConfig
            // 
            this.panConfig.Controls.Add(this.btnActivate);
            this.panConfig.Controls.Add(this.tbKRName);
            this.panConfig.Controls.Add(this.tBshuaijian);
            this.panConfig.Controls.Add(this.tbSmooth);
            this.panConfig.Controls.Add(this.cbEPCSe);
            this.panConfig.Controls.Add(this.tBshuaijian2);
            this.panConfig.Controls.Add(this.tBshuaijian3);
            this.panConfig.Location = new System.Drawing.Point(20, 49);
            this.panConfig.Name = "panConfig";
            this.panConfig.Size = new System.Drawing.Size(312, 130);
            this.panConfig.TabIndex = 33;
            this.panConfig.Visible = false;
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(126, 47);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(100, 23);
            this.btnActivate.TabIndex = 32;
            this.btnActivate.Text = "激活苯系物";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // tbKRName
            // 
            this.tbKRName.Location = new System.Drawing.Point(126, 76);
            this.tbKRName.Name = "tbKRName";
            this.tbKRName.Size = new System.Drawing.Size(100, 21);
            this.tbKRName.TabIndex = 31;
            this.tbKRName.Visible = false;
            this.tbKRName.TextChanged += new System.EventHandler(this.tbKRName_TextChanged);
            // 
            // tBshuaijian
            // 
            this.tBshuaijian.Location = new System.Drawing.Point(14, 18);
            this.tBshuaijian.Name = "tBshuaijian";
            this.tBshuaijian.Size = new System.Drawing.Size(100, 21);
            this.tBshuaijian.TabIndex = 24;
            this.tBshuaijian.Visible = false;
            this.tBshuaijian.TextChanged += new System.EventHandler(this.tBshuaijian_TextChanged);
            // 
            // tbSmooth
            // 
            this.tbSmooth.Location = new System.Drawing.Point(126, 99);
            this.tbSmooth.Name = "tbSmooth";
            this.tbSmooth.Size = new System.Drawing.Size(100, 21);
            this.tbSmooth.TabIndex = 30;
            this.tbSmooth.Visible = false;
            this.tbSmooth.TextChanged += new System.EventHandler(this.tbSmooth_TextChanged);
            // 
            // cbEPCSe
            // 
            this.cbEPCSe.FormattingEnabled = true;
            this.cbEPCSe.Items.AddRange(new object[] {
            "EPC",
            "阀调数显",
            "机械阀"});
            this.cbEPCSe.Location = new System.Drawing.Point(14, 100);
            this.cbEPCSe.Name = "cbEPCSe";
            this.cbEPCSe.Size = new System.Drawing.Size(100, 20);
            this.cbEPCSe.TabIndex = 29;
            this.cbEPCSe.Visible = false;
            this.cbEPCSe.SelectedIndexChanged += new System.EventHandler(this.CbEPCSe_SelectedIndexChanged);
            // 
            // tBshuaijian2
            // 
            this.tBshuaijian2.Location = new System.Drawing.Point(14, 49);
            this.tBshuaijian2.Name = "tBshuaijian2";
            this.tBshuaijian2.Size = new System.Drawing.Size(100, 21);
            this.tBshuaijian2.TabIndex = 27;
            this.tBshuaijian2.Visible = false;
            this.tBshuaijian2.TextChanged += new System.EventHandler(this.tBshuaijian2_TextChanged);
            // 
            // tBshuaijian3
            // 
            this.tBshuaijian3.Location = new System.Drawing.Point(14, 76);
            this.tBshuaijian3.Name = "tBshuaijian3";
            this.tBshuaijian3.Size = new System.Drawing.Size(100, 21);
            this.tBshuaijian3.TabIndex = 28;
            this.tBshuaijian3.Visible = false;
            this.tBshuaijian3.TextChanged += new System.EventHandler(this.tBshuaijian3_TextChanged);
            // 
            // tBpasswordShJ
            // 
            this.tBpasswordShJ.Location = new System.Drawing.Point(20, 22);
            this.tBpasswordShJ.Name = "tBpasswordShJ";
            this.tBpasswordShJ.Size = new System.Drawing.Size(100, 21);
            this.tBpasswordShJ.TabIndex = 26;
            // 
            // btnShuaijian
            // 
            this.btnShuaijian.Location = new System.Drawing.Point(132, 22);
            this.btnShuaijian.Name = "btnShuaijian";
            this.btnShuaijian.Size = new System.Drawing.Size(75, 23);
            this.btnShuaijian.TabIndex = 25;
            this.btnShuaijian.UseVisualStyleBackColor = true;
            this.btnShuaijian.Click += new System.EventHandler(this.btnShuaijian_Click);
            // 
            // button27
            // 
            this.button27.Location = new System.Drawing.Point(16, 552);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(67, 23);
            this.button27.TabIndex = 7;
            this.button27.Text = "流量节省";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(255, 552);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 23);
            this.button18.TabIndex = 3;
            this.button18.Text = "设定";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox8);
            this.groupBox5.Controls.Add(this.button31);
            this.groupBox5.Controls.Add(this.checkBox9);
            this.groupBox5.Controls.Add(this.button29);
            this.groupBox5.Controls.Add(this.button30);
            this.groupBox5.Controls.Add(this.bsavmess);
            this.groupBox5.Controls.Add(this.button28);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.label70);
            this.groupBox5.Controls.Add(this.comboBox13);
            this.groupBox5.Controls.Add(this.comboBox4);
            this.groupBox5.Controls.Add(this.checkBox7);
            this.groupBox5.Controls.Add(this.cbAlarm);
            this.groupBox5.Controls.Add(this.button22);
            this.groupBox5.Location = new System.Drawing.Point(282, 156);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(48, 61);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "短消息发送";
            this.groupBox5.Visible = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(10, 15);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(184, 35);
            this.textBox8.TabIndex = 5;
            this.textBox8.Text = "色谱机短信";
            this.textBox8.Enter += new System.EventHandler(this.IP4_Enter);
            // 
            // button31
            // 
            this.button31.Location = new System.Drawing.Point(147, 82);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(41, 23);
            this.button31.TabIndex = 9;
            this.button31.Text = "设定";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(10, 121);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(144, 16);
            this.checkBox9.TabIndex = 10;
            this.checkBox9.Text = "停止时间到后自动发送";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // button29
            // 
            this.button29.Location = new System.Drawing.Point(273, 57);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(41, 23);
            this.button29.TabIndex = 9;
            this.button29.Text = "设定";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // button30
            // 
            this.button30.Location = new System.Drawing.Point(88, 82);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(45, 23);
            this.button30.TabIndex = 9;
            this.button30.Text = "查询";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // bsavmess
            // 
            this.bsavmess.Location = new System.Drawing.Point(240, 67);
            this.bsavmess.Name = "bsavmess";
            this.bsavmess.Size = new System.Drawing.Size(75, 23);
            this.bsavmess.TabIndex = 9;
            this.bsavmess.Text = "保存";
            this.bsavmess.UseVisualStyleBackColor = true;
            this.bsavmess.Click += new System.EventHandler(this.bsavmess_Click);
            // 
            // button28
            // 
            this.button28.Location = new System.Drawing.Point(229, 57);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(45, 23);
            this.button28.TabIndex = 9;
            this.button28.Text = "查询";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(87, 61);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(59, 12);
            this.label28.TabIndex = 8;
            this.label28.Text = "鸣叫次数:";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(2, 62);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(35, 12);
            this.label70.TabIndex = 8;
            this.label70.Text = "量程:";
            // 
            // comboBox13
            // 
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBox13.Location = new System.Drawing.Point(147, 56);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(41, 20);
            this.comboBox13.TabIndex = 7;
            this.comboBox13.Text = "1";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "±1",
            "±2"});
            this.comboBox4.Location = new System.Drawing.Point(38, 58);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(44, 20);
            this.comboBox4.TabIndex = 7;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Checked = true;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.Location = new System.Drawing.Point(10, 99);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(72, 16);
            this.checkBox7.TabIndex = 6;
            this.checkBox7.Text = "鸣叫提醒";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // cbAlarm
            // 
            this.cbAlarm.AutoSize = true;
            this.cbAlarm.Location = new System.Drawing.Point(10, 81);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(72, 16);
            this.cbAlarm.TabIndex = 8;
            this.cbAlarm.Text = "指令鸣叫";
            this.cbAlarm.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(240, 89);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(75, 23);
            this.button22.TabIndex = 2;
            this.button22.Text = "发送";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(272, 156);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 2;
            this.button13.Text = "查询";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.IP3);
            this.groupBox12.Controls.Add(this.IP2);
            this.groupBox12.Controls.Add(this.IP1);
            this.groupBox12.Controls.Add(this.label61);
            this.groupBox12.Controls.Add(this.label60);
            this.groupBox12.Controls.Add(this.label59);
            this.groupBox12.Location = new System.Drawing.Point(6, 366);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(341, 89);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "色谱机网络参数";
            // 
            // IP3
            // 
            this.IP3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IP3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IP3.Location = new System.Drawing.Point(65, 58);
            this.IP3.Name = "IP3";
            this.IP3.Size = new System.Drawing.Size(130, 21);
            this.IP3.TabIndex = 1;
            this.IP3.Text = "127.0.0.1";
            this.IP3.Enter += new System.EventHandler(this.IP4_Enter);
            this.IP3.Leave += new System.EventHandler(this.IP4_Leave);
            // 
            // IP2
            // 
            this.IP2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IP2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IP2.Location = new System.Drawing.Point(65, 36);
            this.IP2.Name = "IP2";
            this.IP2.Size = new System.Drawing.Size(130, 21);
            this.IP2.TabIndex = 1;
            this.IP2.Text = "127.0.0.1";
            this.IP2.Enter += new System.EventHandler(this.IP4_Enter);
            this.IP2.Leave += new System.EventHandler(this.IP4_Leave);
            // 
            // IP1
            // 
            this.IP1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IP1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IP1.Location = new System.Drawing.Point(65, 16);
            this.IP1.Name = "IP1";
            this.IP1.Size = new System.Drawing.Size(130, 21);
            this.IP1.TabIndex = 1;
            this.IP1.Text = "127.0.0.1";
            this.IP1.Enter += new System.EventHandler(this.IP4_Enter);
            this.IP1.Leave += new System.EventHandler(this.IP4_Leave);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(6, 61);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(59, 12);
            this.label61.TabIndex = 0;
            this.label61.Text = "网    关:";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(6, 39);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(59, 12);
            this.label60.TabIndex = 0;
            this.label60.Text = "子网掩码:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 19);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(59, 12);
            this.label59.TabIndex = 0;
            this.label59.Text = "本地  IP:";
            // 
            // gvHardVersion
            // 
            this.gvHardVersion.AllowUserToAddRows = false;
            this.gvHardVersion.AllowUserToDeleteRows = false;
            this.gvHardVersion.AllowUserToOrderColumns = true;
            this.gvHardVersion.AllowUserToResizeColumns = false;
            this.gvHardVersion.AllowUserToResizeRows = false;
            this.gvHardVersion.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle89.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle89.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle89.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle89.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle89.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle89.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle89.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHardVersion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle89;
            this.gvHardVersion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvHardVersion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19});
            this.gvHardVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.gvHardVersion.Location = new System.Drawing.Point(3, 3);
            this.gvHardVersion.MultiSelect = false;
            this.gvHardVersion.Name = "gvHardVersion";
            this.gvHardVersion.RowHeadersVisible = false;
            this.gvHardVersion.RowTemplate.Height = 23;
            this.gvHardVersion.Size = new System.Drawing.Size(347, 147);
            this.gvHardVersion.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn18
            // 
            dataGridViewCellStyle90.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle90.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle90.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn18.DefaultCellStyle = dataGridViewCellStyle90;
            this.dataGridViewTextBoxColumn18.HeaderText = "序    号";
            this.dataGridViewTextBoxColumn18.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn18.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.Width = 80;
            // 
            // dataGridViewTextBoxColumn19
            // 
            dataGridViewCellStyle91.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle91.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle91.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn19.DefaultCellStyle = dataGridViewCellStyle91;
            this.dataGridViewTextBoxColumn19.HeaderText = "         版         本";
            this.dataGridViewTextBoxColumn19.MaxInputLength = 100;
            this.dataGridViewTextBoxColumn19.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn19.Width = 250;
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(163, 552);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 2;
            this.button19.Text = "查询";
            this.button19.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.IP6);
            this.groupBox13.Controls.Add(this.label63);
            this.groupBox13.Controls.Add(this.IP5);
            this.groupBox13.Controls.Add(this.label62);
            this.groupBox13.Controls.Add(this.IP4);
            this.groupBox13.Controls.Add(this.label64);
            this.groupBox13.Location = new System.Drawing.Point(6, 461);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(341, 90);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "色谱仪内上报IP";
            // 
            // IP6
            // 
            this.IP6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IP6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IP6.Location = new System.Drawing.Point(64, 59);
            this.IP6.Name = "IP6";
            this.IP6.Size = new System.Drawing.Size(131, 21);
            this.IP6.TabIndex = 1;
            this.IP6.Text = "127.0.0.1";
            this.IP6.Enter += new System.EventHandler(this.IP4_Enter);
            this.IP6.Leave += new System.EventHandler(this.IP4_Leave);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(5, 40);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(59, 12);
            this.label63.TabIndex = 0;
            this.label63.Text = "业务主管:";
            // 
            // IP5
            // 
            this.IP5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IP5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IP5.Location = new System.Drawing.Point(64, 37);
            this.IP5.Name = "IP5";
            this.IP5.Size = new System.Drawing.Size(131, 21);
            this.IP5.TabIndex = 1;
            this.IP5.Text = "127.0.0.1";
            this.IP5.Enter += new System.EventHandler(this.IP4_Enter);
            this.IP5.Leave += new System.EventHandler(this.IP4_Leave);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.ForeColor = System.Drawing.Color.Blue;
            this.label62.Location = new System.Drawing.Point(5, 20);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(59, 12);
            this.label62.TabIndex = 0;
            this.label62.Text = "本地主管:";
            // 
            // IP4
            // 
            this.IP4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IP4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IP4.Location = new System.Drawing.Point(64, 17);
            this.IP4.Name = "IP4";
            this.IP4.Size = new System.Drawing.Size(131, 21);
            this.IP4.TabIndex = 1;
            this.IP4.Text = "127.0.0.1";
            this.IP4.Enter += new System.EventHandler(this.IP4_Enter);
            this.IP4.Leave += new System.EventHandler(this.IP4_Leave);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(5, 62);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(59, 12);
            this.label64.TabIndex = 0;
            this.label64.Text = "上级主管:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox9);
            this.tabPage4.Controls.Add(this.btnDownload);
            this.tabPage4.Controls.Add(this.label76);
            this.tabPage4.Controls.Add(this.MethodReSave);
            this.tabPage4.Controls.Add(this.MethodSave);
            this.tabPage4.Controls.Add(this.tbMethName);
            this.tabPage4.Controls.Add(this.MethodOpen);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(353, 702);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Text = "仪器方法";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label99);
            this.groupBox9.Controls.Add(this.tbColP4);
            this.groupBox9.Controls.Add(this.groupBox15);
            this.groupBox9.Controls.Add(this.label100);
            this.groupBox9.Controls.Add(this.label81);
            this.groupBox9.Controls.Add(this.tbColP3);
            this.groupBox9.Controls.Add(this.label83);
            this.groupBox9.Controls.Add(this.label80);
            this.groupBox9.Controls.Add(this.label82);
            this.groupBox9.Controls.Add(this.label84);
            this.groupBox9.Controls.Add(this.label85);
            this.groupBox9.Controls.Add(this.label86);
            this.groupBox9.Controls.Add(this.tbAirP2);
            this.groupBox9.Controls.Add(this.label87);
            this.groupBox9.Controls.Add(this.tbHHP2);
            this.groupBox9.Controls.Add(this.label88);
            this.groupBox9.Controls.Add(this.tbColP2);
            this.groupBox9.Controls.Add(this.label89);
            this.groupBox9.Controls.Add(this.label90);
            this.groupBox9.Controls.Add(this.label91);
            this.groupBox9.Controls.Add(this.label95);
            this.groupBox9.Controls.Add(this.tbAirP1);
            this.groupBox9.Controls.Add(this.label96);
            this.groupBox9.Controls.Add(this.tbHHP1);
            this.groupBox9.Controls.Add(this.label97);
            this.groupBox9.Controls.Add(this.tbColP1);
            this.groupBox9.Controls.Add(this.label98);
            this.groupBox9.Location = new System.Drawing.Point(6, 331);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(338, 185);
            this.groupBox9.TabIndex = 55;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "压力显示";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.ForeColor = System.Drawing.Color.Black;
            this.label99.Location = new System.Drawing.Point(259, 139);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(23, 12);
            this.label99.TabIndex = 41;
            this.label99.Text = "psi";
            // 
            // tbColP4
            // 
            this.tbColP4.Location = new System.Drawing.Point(212, 134);
            this.tbColP4.Name = "tbColP4";
            this.tbColP4.Size = new System.Drawing.Size(41, 21);
            this.tbColP4.TabIndex = 43;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.btnEPCSet);
            this.groupBox15.Controls.Add(this.label78);
            this.groupBox15.Controls.Add(this.label79);
            this.groupBox15.Controls.Add(this.label77);
            this.groupBox15.Controls.Add(this.label34);
            this.groupBox15.Controls.Add(this.label21);
            this.groupBox15.Controls.Add(this.label24);
            this.groupBox15.Controls.Add(this.label25);
            this.groupBox15.Controls.Add(this.tbAirCur2);
            this.groupBox15.Controls.Add(this.tbAirSet2);
            this.groupBox15.Controls.Add(this.label31);
            this.groupBox15.Controls.Add(this.tbHHCur2);
            this.groupBox15.Controls.Add(this.tbHHSet2);
            this.groupBox15.Controls.Add(this.label32);
            this.groupBox15.Controls.Add(this.tbColPreCur2);
            this.groupBox15.Controls.Add(this.tbColPreSet2);
            this.groupBox15.Controls.Add(this.label33);
            this.groupBox15.Controls.Add(this.label29);
            this.groupBox15.Controls.Add(this.label27);
            this.groupBox15.Controls.Add(this.label26);
            this.groupBox15.Controls.Add(this.tbAirCur1);
            this.groupBox15.Controls.Add(this.tbAirSet1);
            this.groupBox15.Controls.Add(this.label23);
            this.groupBox15.Controls.Add(this.tbHHCur1);
            this.groupBox15.Controls.Add(this.tbHHSet1);
            this.groupBox15.Controls.Add(this.label22);
            this.groupBox15.Controls.Add(this.tbColPreCur1);
            this.groupBox15.Controls.Add(this.tbColPreSet1);
            this.groupBox15.Controls.Add(this.label20);
            this.groupBox15.Location = new System.Drawing.Point(3, 55);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(338, 150);
            this.groupBox15.TabIndex = 54;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "EPC";
            // 
            // btnEPCSet
            // 
            this.btnEPCSet.ForeColor = System.Drawing.Color.Black;
            this.btnEPCSet.Location = new System.Drawing.Point(259, 121);
            this.btnEPCSet.Name = "btnEPCSet";
            this.btnEPCSet.Size = new System.Drawing.Size(75, 23);
            this.btnEPCSet.TabIndex = 56;
            this.btnEPCSet.Text = "高级参数";
            this.btnEPCSet.UseVisualStyleBackColor = true;
            this.btnEPCSet.Click += new System.EventHandler(this.BtnEPCSet_Click);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.ForeColor = System.Drawing.Color.Black;
            this.label78.Location = new System.Drawing.Point(263, 14);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(29, 12);
            this.label78.TabIndex = 37;
            this.label78.Text = "实测";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.ForeColor = System.Drawing.Color.Black;
            this.label79.Location = new System.Drawing.Point(218, 14);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(29, 12);
            this.label79.TabIndex = 36;
            this.label79.Text = "设定";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.ForeColor = System.Drawing.Color.Black;
            this.label77.Location = new System.Drawing.Point(96, 14);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(29, 12);
            this.label77.TabIndex = 35;
            this.label77.Text = "实测";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(51, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(29, 12);
            this.label34.TabIndex = 34;
            this.label34.Text = "设定";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(306, 86);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(23, 12);
            this.label21.TabIndex = 33;
            this.label21.Text = "psi";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(306, 59);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 12);
            this.label24.TabIndex = 32;
            this.label24.Text = "psi";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(306, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(23, 12);
            this.label25.TabIndex = 22;
            this.label25.Text = "psi";
            // 
            // tbAirCur2
            // 
            this.tbAirCur2.Location = new System.Drawing.Point(259, 83);
            this.tbAirCur2.Name = "tbAirCur2";
            this.tbAirCur2.Size = new System.Drawing.Size(41, 21);
            this.tbAirCur2.TabIndex = 31;
            // 
            // tbAirSet2
            // 
            this.tbAirSet2.Location = new System.Drawing.Point(212, 83);
            this.tbAirSet2.Name = "tbAirSet2";
            this.tbAirSet2.Size = new System.Drawing.Size(41, 21);
            this.tbAirSet2.TabIndex = 30;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(172, 86);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(41, 12);
            this.label31.TabIndex = 29;
            this.label31.Text = "载气4:";
            // 
            // tbHHCur2
            // 
            this.tbHHCur2.Location = new System.Drawing.Point(259, 56);
            this.tbHHCur2.Name = "tbHHCur2";
            this.tbHHCur2.Size = new System.Drawing.Size(41, 21);
            this.tbHHCur2.TabIndex = 28;
            // 
            // tbHHSet2
            // 
            this.tbHHSet2.Location = new System.Drawing.Point(212, 56);
            this.tbHHSet2.Name = "tbHHSet2";
            this.tbHHSet2.Size = new System.Drawing.Size(41, 21);
            this.tbHHSet2.TabIndex = 27;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(172, 59);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 12);
            this.label32.TabIndex = 26;
            this.label32.Text = "载气3:";
            // 
            // tbColPreCur2
            // 
            this.tbColPreCur2.Location = new System.Drawing.Point(259, 29);
            this.tbColPreCur2.Name = "tbColPreCur2";
            this.tbColPreCur2.Size = new System.Drawing.Size(41, 21);
            this.tbColPreCur2.TabIndex = 25;
            // 
            // tbColPreSet2
            // 
            this.tbColPreSet2.Location = new System.Drawing.Point(212, 29);
            this.tbColPreSet2.Name = "tbColPreSet2";
            this.tbColPreSet2.Size = new System.Drawing.Size(41, 21);
            this.tbColPreSet2.TabIndex = 24;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(172, 32);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(41, 12);
            this.label33.TabIndex = 23;
            this.label33.Text = "载气2:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(133, 86);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(41, 12);
            this.label29.TabIndex = 21;
            this.label29.Text = "ml/min";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(133, 59);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(41, 12);
            this.label27.TabIndex = 20;
            this.label27.Text = "ml/min";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(133, 32);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(23, 12);
            this.label26.TabIndex = 0;
            this.label26.Text = "psi";
            // 
            // tbAirCur1
            // 
            this.tbAirCur1.Location = new System.Drawing.Point(91, 83);
            this.tbAirCur1.Name = "tbAirCur1";
            this.tbAirCur1.Size = new System.Drawing.Size(41, 21);
            this.tbAirCur1.TabIndex = 10;
            // 
            // tbAirSet1
            // 
            this.tbAirSet1.Location = new System.Drawing.Point(46, 83);
            this.tbAirSet1.Name = "tbAirSet1";
            this.tbAirSet1.Size = new System.Drawing.Size(41, 21);
            this.tbAirSet1.TabIndex = 9;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(6, 86);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 8;
            this.label23.Text = "空气1:";
            // 
            // tbHHCur1
            // 
            this.tbHHCur1.Location = new System.Drawing.Point(91, 56);
            this.tbHHCur1.Name = "tbHHCur1";
            this.tbHHCur1.Size = new System.Drawing.Size(41, 21);
            this.tbHHCur1.TabIndex = 7;
            // 
            // tbHHSet1
            // 
            this.tbHHSet1.Location = new System.Drawing.Point(46, 56);
            this.tbHHSet1.Name = "tbHHSet1";
            this.tbHHSet1.Size = new System.Drawing.Size(41, 21);
            this.tbHHSet1.TabIndex = 6;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(6, 59);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 5;
            this.label22.Text = "氢气1:";
            // 
            // tbColPreCur1
            // 
            this.tbColPreCur1.Location = new System.Drawing.Point(91, 29);
            this.tbColPreCur1.Name = "tbColPreCur1";
            this.tbColPreCur1.Size = new System.Drawing.Size(41, 21);
            this.tbColPreCur1.TabIndex = 4;
            // 
            // tbColPreSet1
            // 
            this.tbColPreSet1.Location = new System.Drawing.Point(46, 29);
            this.tbColPreSet1.Name = "tbColPreSet1";
            this.tbColPreSet1.Size = new System.Drawing.Size(41, 21);
            this.tbColPreSet1.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(6, 32);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "载气1:";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.ForeColor = System.Drawing.Color.Black;
            this.label100.Location = new System.Drawing.Point(172, 137);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(41, 12);
            this.label100.TabIndex = 42;
            this.label100.Text = "载气4:";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.ForeColor = System.Drawing.Color.Black;
            this.label81.Location = new System.Drawing.Point(93, 137);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(23, 12);
            this.label81.TabIndex = 38;
            this.label81.Text = "psi";
            // 
            // tbColP3
            // 
            this.tbColP3.Location = new System.Drawing.Point(46, 134);
            this.tbColP3.Name = "tbColP3";
            this.tbColP3.Size = new System.Drawing.Size(41, 21);
            this.tbColP3.TabIndex = 40;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.ForeColor = System.Drawing.Color.Black;
            this.label83.Location = new System.Drawing.Point(6, 137);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(41, 12);
            this.label83.TabIndex = 39;
            this.label83.Text = "载气3:";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.ForeColor = System.Drawing.Color.Black;
            this.label80.Location = new System.Drawing.Point(216, 14);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(29, 12);
            this.label80.TabIndex = 37;
            this.label80.Text = "实测";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.ForeColor = System.Drawing.Color.Black;
            this.label82.Location = new System.Drawing.Point(51, 14);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(29, 12);
            this.label82.TabIndex = 35;
            this.label82.Text = "实测";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.ForeColor = System.Drawing.Color.Black;
            this.label84.Location = new System.Drawing.Point(259, 86);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(23, 12);
            this.label84.TabIndex = 33;
            this.label84.Text = "psi";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.ForeColor = System.Drawing.Color.Black;
            this.label85.Location = new System.Drawing.Point(259, 59);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(23, 12);
            this.label85.TabIndex = 32;
            this.label85.Text = "psi";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.ForeColor = System.Drawing.Color.Black;
            this.label86.Location = new System.Drawing.Point(259, 32);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(23, 12);
            this.label86.TabIndex = 22;
            this.label86.Text = "psi";
            // 
            // tbAirP2
            // 
            this.tbAirP2.Location = new System.Drawing.Point(212, 83);
            this.tbAirP2.Name = "tbAirP2";
            this.tbAirP2.Size = new System.Drawing.Size(41, 21);
            this.tbAirP2.TabIndex = 31;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.ForeColor = System.Drawing.Color.Black;
            this.label87.Location = new System.Drawing.Point(172, 86);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(41, 12);
            this.label87.TabIndex = 29;
            this.label87.Text = "空气2:";
            // 
            // tbHHP2
            // 
            this.tbHHP2.Location = new System.Drawing.Point(212, 56);
            this.tbHHP2.Name = "tbHHP2";
            this.tbHHP2.Size = new System.Drawing.Size(41, 21);
            this.tbHHP2.TabIndex = 28;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.ForeColor = System.Drawing.Color.Black;
            this.label88.Location = new System.Drawing.Point(172, 59);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(41, 12);
            this.label88.TabIndex = 26;
            this.label88.Text = "氢气2:";
            // 
            // tbColP2
            // 
            this.tbColP2.Location = new System.Drawing.Point(212, 29);
            this.tbColP2.Name = "tbColP2";
            this.tbColP2.Size = new System.Drawing.Size(41, 21);
            this.tbColP2.TabIndex = 25;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.ForeColor = System.Drawing.Color.Black;
            this.label89.Location = new System.Drawing.Point(172, 32);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(41, 12);
            this.label89.TabIndex = 23;
            this.label89.Text = "载气2:";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.ForeColor = System.Drawing.Color.Black;
            this.label90.Location = new System.Drawing.Point(93, 86);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(23, 12);
            this.label90.TabIndex = 21;
            this.label90.Text = "psi";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.ForeColor = System.Drawing.Color.Black;
            this.label91.Location = new System.Drawing.Point(93, 59);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(23, 12);
            this.label91.TabIndex = 20;
            this.label91.Text = "psi";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.ForeColor = System.Drawing.Color.Black;
            this.label95.Location = new System.Drawing.Point(93, 32);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(23, 12);
            this.label95.TabIndex = 0;
            this.label95.Text = "psi";
            // 
            // tbAirP1
            // 
            this.tbAirP1.Location = new System.Drawing.Point(46, 83);
            this.tbAirP1.Name = "tbAirP1";
            this.tbAirP1.Size = new System.Drawing.Size(41, 21);
            this.tbAirP1.TabIndex = 10;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.ForeColor = System.Drawing.Color.Black;
            this.label96.Location = new System.Drawing.Point(6, 86);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(41, 12);
            this.label96.TabIndex = 8;
            this.label96.Text = "空气1:";
            // 
            // tbHHP1
            // 
            this.tbHHP1.Location = new System.Drawing.Point(46, 56);
            this.tbHHP1.Name = "tbHHP1";
            this.tbHHP1.Size = new System.Drawing.Size(41, 21);
            this.tbHHP1.TabIndex = 7;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.ForeColor = System.Drawing.Color.Black;
            this.label97.Location = new System.Drawing.Point(6, 59);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(41, 12);
            this.label97.TabIndex = 5;
            this.label97.Text = "氢气1:";
            // 
            // tbColP1
            // 
            this.tbColP1.Location = new System.Drawing.Point(46, 29);
            this.tbColP1.Name = "tbColP1";
            this.tbColP1.Size = new System.Drawing.Size(41, 21);
            this.tbColP1.TabIndex = 4;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.ForeColor = System.Drawing.Color.Black;
            this.label98.Location = new System.Drawing.Point(6, 32);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(41, 12);
            this.label98.TabIndex = 0;
            this.label98.Text = "载气1:";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(129, 131);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(107, 54);
            this.btnDownload.TabIndex = 52;
            this.btnDownload.Text = "下载到仪器";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Visible = false;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 53);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(65, 12);
            this.label76.TabIndex = 51;
            this.label76.Text = "参数方法：";
            this.label76.Visible = false;
            // 
            // MethodReSave
            // 
            this.MethodReSave.Location = new System.Drawing.Point(166, 78);
            this.MethodReSave.Name = "MethodReSave";
            this.MethodReSave.Size = new System.Drawing.Size(70, 23);
            this.MethodReSave.TabIndex = 49;
            this.MethodReSave.Text = "另存";
            this.MethodReSave.UseVisualStyleBackColor = true;
            this.MethodReSave.Visible = false;
            this.MethodReSave.Click += new System.EventHandler(this.MethodReSave_Click);
            // 
            // MethodSave
            // 
            this.MethodSave.Location = new System.Drawing.Point(77, 78);
            this.MethodSave.Name = "MethodSave";
            this.MethodSave.Size = new System.Drawing.Size(75, 23);
            this.MethodSave.TabIndex = 50;
            this.MethodSave.Text = "保存";
            this.MethodSave.UseVisualStyleBackColor = true;
            this.MethodSave.Visible = false;
            this.MethodSave.Click += new System.EventHandler(this.MethodSave_Click);
            // 
            // tbMethName
            // 
            this.tbMethName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMethName.Location = new System.Drawing.Point(77, 51);
            this.tbMethName.Name = "tbMethName";
            this.tbMethName.ReadOnly = true;
            this.tbMethName.Size = new System.Drawing.Size(263, 21);
            this.tbMethName.TabIndex = 47;
            this.tbMethName.Text = "默认";
            this.tbMethName.Visible = false;
            // 
            // MethodOpen
            // 
            this.MethodOpen.Location = new System.Drawing.Point(309, 73);
            this.MethodOpen.Name = "MethodOpen";
            this.MethodOpen.Size = new System.Drawing.Size(31, 32);
            this.MethodOpen.TabIndex = 48;
            this.MethodOpen.UseVisualStyleBackColor = true;
            this.MethodOpen.Visible = false;
            this.MethodOpen.Click += new System.EventHandler(this.MethodOpen_Click);
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.listMess);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(353, 702);
            this.tabPage16.TabIndex = 5;
            this.tabPage16.Text = "消息";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // listMess
            // 
            this.listMess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMess.FormattingEnabled = true;
            this.listMess.ItemHeight = 12;
            this.listMess.Location = new System.Drawing.Point(3, 3);
            this.listMess.Name = "listMess";
            this.listMess.Size = new System.Drawing.Size(347, 696);
            this.listMess.TabIndex = 0;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.tabControl3);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(353, 702);
            this.tabPage11.TabIndex = 7;
            this.tabPage11.Text = "自动进样器";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.BackColor = System.Drawing.Color.Transparent;
            this.tabControl3.CanReorderTabs = true;
            this.tabControl3.Controls.Add(this.tabControlPanel1);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(3, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl3.SelectedTabIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(347, 696);
            this.tabControl3.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005Document;
            this.tabControl3.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.tabControl3.TabIndex = 0;
            this.tabControl3.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl3.Tabs.Add(this.tabItem1);
            this.tabControl3.Text = "tabControl3";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.groupBox1);
            this.tabControlPanel1.Controls.Add(this.dgGramset);
            this.tabControlPanel1.Controls.Add(this.tbREPT);
            this.tabControlPanel1.Controls.Add(this.label125);
            this.tabControlPanel1.Controls.Add(this.checkBox6);
            this.tabControlPanel1.Controls.Add(this.label123);
            this.tabControlPanel1.Controls.Add(this.label39);
            this.tabControlPanel1.Controls.Add(this.label122);
            this.tabControlPanel1.Controls.Add(this.label41);
            this.tabControlPanel1.Controls.Add(this.tbTANL);
            this.tabControlPanel1.Controls.Add(this.label45);
            this.tabControlPanel1.Controls.Add(this.label121);
            this.tabControlPanel1.Controls.Add(this.label49);
            this.tabControlPanel1.Controls.Add(this.tbIVOL);
            this.tabControlPanel1.Controls.Add(this.label43);
            this.tabControlPanel1.Controls.Add(this.tbSINT);
            this.tabControlPanel1.Controls.Add(this.label53);
            this.tabControlPanel1.Controls.Add(this.tbFSAM);
            this.tabControlPanel1.Controls.Add(this.label51);
            this.tabControlPanel1.Controls.Add(this.label118);
            this.tabControlPanel1.Controls.Add(this.label57);
            this.tabControlPanel1.Controls.Add(this.label119);
            this.tabControlPanel1.Controls.Add(this.label42);
            this.tabControlPanel1.Controls.Add(this.label120);
            this.tabControlPanel1.Controls.Add(this.label58);
            this.tabControlPanel1.Controls.Add(this.resetInj);
            this.tabControlPanel1.Controls.Add(this.label55);
            this.tabControlPanel1.Controls.Add(this.label47);
            this.tabControlPanel1.Controls.Add(this.textBox10);
            this.tabControlPanel1.Controls.Add(this.label46);
            this.tabControlPanel1.Controls.Add(this.button15);
            this.tabControlPanel1.Controls.Add(this.label50);
            this.tabControlPanel1.Controls.Add(this.textBox12);
            this.tabControlPanel1.Controls.Add(this.label54);
            this.tabControlPanel1.Controls.Add(this.textBox14);
            this.tabControlPanel1.Controls.Add(this.label52);
            this.tabControlPanel1.Controls.Add(this.btnStartAutoINJ);
            this.tabControlPanel1.Controls.Add(this.label48);
            this.tabControlPanel1.Controls.Add(this.button24);
            this.tabControlPanel1.Controls.Add(this.label1);
            this.tabControlPanel1.Controls.Add(this.btnStopAutoINJ);
            this.tabControlPanel1.Controls.Add(this.comboBox1);
            this.tabControlPanel1.Controls.Add(this.textBox13);
            this.tabControlPanel1.Controls.Add(this.comboBox3);
            this.tabControlPanel1.Controls.Add(this.textBox11);
            this.tabControlPanel1.Controls.Add(this.comboBox11);
            this.tabControlPanel1.Controls.Add(this.textBox15);
            this.tabControlPanel1.Controls.Add(this.comboBox12);
            this.tabControlPanel1.Controls.Add(this.textBox9);
            this.tabControlPanel1.Controls.Add(this.label40);
            this.tabControlPanel1.Controls.Add(this.comboBox2);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(26, 0);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(321, 696);
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(250)))), ((int)(((byte)(247)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox11);
            this.groupBox1.Controls.Add(this.liquidStatus);
            this.groupBox1.Controls.Add(this.liquidclose);
            this.groupBox1.Controls.Add(this.liquidopen);
            this.groupBox1.Location = new System.Drawing.Point(82, 452);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(42, 58);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "液相泵控制";
            this.groupBox1.Visible = false;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.rliquid);
            this.groupBox11.Controls.Add(this.liquidsetvaluequery);
            this.groupBox11.Controls.Add(this.liquidset);
            this.groupBox11.Controls.Add(this.rpass);
            this.groupBox11.Controls.Add(this.liquidpass);
            this.groupBox11.Controls.Add(this.label74);
            this.groupBox11.Controls.Add(this.liquidpress);
            this.groupBox11.Controls.Add(this.label71);
            this.groupBox11.Controls.Add(this.Maxliquidpress);
            this.groupBox11.Controls.Add(this.Minliquidpress);
            this.groupBox11.Controls.Add(this.label72);
            this.groupBox11.Controls.Add(this.label73);
            this.groupBox11.Location = new System.Drawing.Point(101, 16);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(177, 60);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "泵控制状态设定";
            // 
            // rliquid
            // 
            this.rliquid.AutoSize = true;
            this.rliquid.Checked = true;
            this.rliquid.Location = new System.Drawing.Point(133, 14);
            this.rliquid.Name = "rliquid";
            this.rliquid.Size = new System.Drawing.Size(47, 16);
            this.rliquid.TabIndex = 6;
            this.rliquid.TabStop = true;
            this.rliquid.Text = "恒压";
            this.rliquid.UseVisualStyleBackColor = true;
            // 
            // liquidsetvaluequery
            // 
            this.liquidsetvaluequery.Location = new System.Drawing.Point(184, 20);
            this.liquidsetvaluequery.Name = "liquidsetvaluequery";
            this.liquidsetvaluequery.Size = new System.Drawing.Size(42, 23);
            this.liquidsetvaluequery.TabIndex = 2;
            this.liquidsetvaluequery.Text = "查询";
            this.liquidsetvaluequery.UseVisualStyleBackColor = true;
            // 
            // liquidset
            // 
            this.liquidset.Location = new System.Drawing.Point(184, 52);
            this.liquidset.Name = "liquidset";
            this.liquidset.Size = new System.Drawing.Size(42, 23);
            this.liquidset.TabIndex = 2;
            this.liquidset.Text = "设定";
            this.liquidset.UseVisualStyleBackColor = true;
            // 
            // rpass
            // 
            this.rpass.AutoSize = true;
            this.rpass.Location = new System.Drawing.Point(133, 36);
            this.rpass.Name = "rpass";
            this.rpass.Size = new System.Drawing.Size(47, 16);
            this.rpass.TabIndex = 6;
            this.rpass.Text = "恒流";
            this.rpass.UseVisualStyleBackColor = true;
            // 
            // liquidpass
            // 
            this.liquidpass.Location = new System.Drawing.Point(133, 61);
            this.liquidpass.Name = "liquidpass";
            this.liquidpass.Size = new System.Drawing.Size(46, 21);
            this.liquidpass.TabIndex = 4;
            this.liquidpass.Text = "99.99";
            this.liquidpass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(6, 42);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(59, 12);
            this.label74.TabIndex = 1;
            this.label74.Text = "最小压力:";
            // 
            // liquidpress
            // 
            this.liquidpress.Location = new System.Drawing.Point(44, 61);
            this.liquidpress.Name = "liquidpress";
            this.liquidpress.Size = new System.Drawing.Size(46, 21);
            this.liquidpress.TabIndex = 4;
            this.liquidpress.Text = "45";
            this.liquidpress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 17);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(59, 12);
            this.label71.TabIndex = 1;
            this.label71.Text = "最大压力:";
            // 
            // Maxliquidpress
            // 
            this.Maxliquidpress.Location = new System.Drawing.Point(71, 14);
            this.Maxliquidpress.Name = "Maxliquidpress";
            this.Maxliquidpress.Size = new System.Drawing.Size(46, 21);
            this.Maxliquidpress.TabIndex = 4;
            this.Maxliquidpress.Text = "45";
            this.Maxliquidpress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Minliquidpress
            // 
            this.Minliquidpress.Location = new System.Drawing.Point(71, 38);
            this.Minliquidpress.Name = "Minliquidpress";
            this.Minliquidpress.Size = new System.Drawing.Size(46, 21);
            this.Minliquidpress.TabIndex = 4;
            this.Minliquidpress.Text = "0";
            this.Minliquidpress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(7, 64);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(35, 12);
            this.label72.TabIndex = 1;
            this.label72.Text = "压力:";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(97, 65);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(35, 12);
            this.label73.TabIndex = 1;
            this.label73.Text = "流量:";
            // 
            // liquidStatus
            // 
            this.liquidStatus.Location = new System.Drawing.Point(4, 42);
            this.liquidStatus.Name = "liquidStatus";
            this.liquidStatus.Size = new System.Drawing.Size(91, 62);
            this.liquidStatus.TabIndex = 1;
            this.liquidStatus.Text = "*液相泵状态";
            // 
            // liquidclose
            // 
            this.liquidclose.Location = new System.Drawing.Point(46, 16);
            this.liquidclose.Name = "liquidclose";
            this.liquidclose.Size = new System.Drawing.Size(39, 23);
            this.liquidclose.TabIndex = 0;
            this.liquidclose.Text = "关闭";
            this.liquidclose.UseVisualStyleBackColor = true;
            // 
            // liquidopen
            // 
            this.liquidopen.Location = new System.Drawing.Point(5, 16);
            this.liquidopen.Name = "liquidopen";
            this.liquidopen.Size = new System.Drawing.Size(37, 23);
            this.liquidopen.TabIndex = 0;
            this.liquidopen.Text = "打开";
            this.liquidopen.UseVisualStyleBackColor = true;
            // 
            // dgGramset
            // 
            this.dgGramset.AllowUserToAddRows = false;
            this.dgGramset.AllowUserToDeleteRows = false;
            this.dgGramset.AllowUserToOrderColumns = true;
            this.dgGramset.AllowUserToResizeColumns = false;
            this.dgGramset.AllowUserToResizeRows = false;
            this.dgGramset.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle92.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle92.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle92.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle92.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle92.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle92.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgGramset.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle92;
            this.dgGramset.ColumnHeadersHeight = 43;
            this.dgGramset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgGramset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.间隔});
            this.dgGramset.Location = new System.Drawing.Point(130, 443);
            this.dgGramset.MultiSelect = false;
            this.dgGramset.Name = "dgGramset";
            this.dgGramset.RowHeadersVisible = false;
            this.dgGramset.RowTemplate.Height = 23;
            this.dgGramset.Size = new System.Drawing.Size(52, 23);
            this.dgGramset.TabIndex = 18;
            this.dgGramset.Visible = false;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewCellStyle93.BackColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle93.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle93.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle93;
            this.dataGridViewTextBoxColumn13.HeaderText = "起始瓶号";
            this.dataGridViewTextBoxColumn13.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn13.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.Width = 70;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewCellStyle94.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle94.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle94.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle94;
            this.dataGridViewTextBoxColumn14.HeaderText = "终止瓶号";
            this.dataGridViewTextBoxColumn14.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn14.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 70;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewCellStyle95.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle95.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle95.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle95;
            this.dataGridViewTextBoxColumn15.HeaderText = "进样量   [uL]";
            this.dataGridViewTextBoxColumn15.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 60;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewCellStyle96.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle96.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle96.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle96;
            this.dataGridViewTextBoxColumn16.HeaderText = "次/瓶";
            this.dataGridViewTextBoxColumn16.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn16.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn16.Width = 60;
            // 
            // 间隔
            // 
            dataGridViewCellStyle97.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle97.ForeColor = System.Drawing.Color.White;
            this.间隔.DefaultCellStyle = dataGridViewCellStyle97;
            this.间隔.HeaderText = "间隔    [min]";
            this.间隔.MaxInputLength = 10;
            this.间隔.Name = "间隔";
            this.间隔.Width = 60;
            // 
            // tbREPT
            // 
            this.tbREPT.Location = new System.Drawing.Point(111, 285);
            this.tbREPT.Name = "tbREPT";
            this.tbREPT.Size = new System.Drawing.Size(46, 21);
            this.tbREPT.TabIndex = 17;
            this.tbREPT.Text = "0";
            this.tbREPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.BackColor = System.Drawing.Color.Transparent;
            this.label125.Location = new System.Drawing.Point(44, 287);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(59, 12);
            this.label125.TabIndex = 16;
            this.label125.Text = "化验次数:";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.BackColor = System.Drawing.Color.Transparent;
            this.checkBox6.Location = new System.Drawing.Point(15, 20);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(48, 16);
            this.checkBox6.TabIndex = 0;
            this.checkBox6.Text = "安装";
            this.checkBox6.UseVisualStyleBackColor = false;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.BackColor = System.Drawing.Color.Transparent;
            this.label123.ForeColor = System.Drawing.Color.Black;
            this.label123.Location = new System.Drawing.Point(162, 261);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(29, 12);
            this.label123.TabIndex = 15;
            this.label123.Text = "分钟";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Location = new System.Drawing.Point(105, 19);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(35, 12);
            this.label39.TabIndex = 1;
            this.label39.Text = "型号:";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.BackColor = System.Drawing.Color.Transparent;
            this.label122.ForeColor = System.Drawing.Color.Black;
            this.label122.Location = new System.Drawing.Point(162, 189);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(71, 12);
            this.label122.TabIndex = 14;
            this.label122.Text = "ul（0.3-8）";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Location = new System.Drawing.Point(7, 45);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(95, 12);
            this.label41.TabIndex = 1;
            this.label41.Text = "进样前溶剂清洗:";
            // 
            // tbTANL
            // 
            this.tbTANL.Location = new System.Drawing.Point(110, 258);
            this.tbTANL.Name = "tbTANL";
            this.tbTANL.Size = new System.Drawing.Size(46, 21);
            this.tbTANL.TabIndex = 13;
            this.tbTANL.Text = "30";
            this.tbTANL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Location = new System.Drawing.Point(7, 65);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(95, 12);
            this.label45.TabIndex = 1;
            this.label45.Text = "采样后溶剂清洗:";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.BackColor = System.Drawing.Color.Transparent;
            this.label121.Location = new System.Drawing.Point(43, 260);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(59, 12);
            this.label121.TabIndex = 12;
            this.label121.Text = "化验时间:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Location = new System.Drawing.Point(43, 85);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(59, 12);
            this.label49.TabIndex = 1;
            this.label49.Text = "粘度延时:";
            // 
            // tbIVOL
            // 
            this.tbIVOL.Location = new System.Drawing.Point(110, 185);
            this.tbIVOL.Name = "tbIVOL";
            this.tbIVOL.Size = new System.Drawing.Size(46, 21);
            this.tbIVOL.TabIndex = 9;
            this.tbIVOL.Text = "0";
            this.tbIVOL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Location = new System.Drawing.Point(189, 42);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(95, 12);
            this.label43.TabIndex = 1;
            this.label43.Text = "采样前溶剂清洗:";
            // 
            // tbSINT
            // 
            this.tbSINT.Location = new System.Drawing.Point(110, 208);
            this.tbSINT.Name = "tbSINT";
            this.tbSINT.Size = new System.Drawing.Size(46, 21);
            this.tbSINT.TabIndex = 10;
            this.tbSINT.Text = "0";
            this.tbSINT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.label53.Location = new System.Drawing.Point(43, 105);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(59, 12);
            this.label53.TabIndex = 1;
            this.label53.Text = "延时启动:";
            // 
            // tbFSAM
            // 
            this.tbFSAM.Location = new System.Drawing.Point(110, 231);
            this.tbFSAM.Name = "tbFSAM";
            this.tbFSAM.Size = new System.Drawing.Size(46, 21);
            this.tbFSAM.TabIndex = 11;
            this.tbFSAM.Text = "0";
            this.tbFSAM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Location = new System.Drawing.Point(189, 86);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(71, 12);
            this.label51.TabIndex = 1;
            this.label51.Text = "进样后驻留:";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.BackColor = System.Drawing.Color.Transparent;
            this.label118.Location = new System.Drawing.Point(43, 211);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(59, 12);
            this.label118.TabIndex = 6;
            this.label118.Text = "开始瓶号:";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Location = new System.Drawing.Point(43, 164);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(59, 12);
            this.label57.TabIndex = 1;
            this.label57.Text = "插针速度:";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.BackColor = System.Drawing.Color.Transparent;
            this.label119.Location = new System.Drawing.Point(43, 233);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(59, 12);
            this.label119.TabIndex = 7;
            this.label119.Text = "结束瓶号:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.Black;
            this.label42.Location = new System.Drawing.Point(162, 45);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(17, 12);
            this.label42.TabIndex = 1;
            this.label42.Text = "次";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.BackColor = System.Drawing.Color.Transparent;
            this.label120.Location = new System.Drawing.Point(55, 189);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(47, 12);
            this.label120.TabIndex = 8;
            this.label120.Text = "进样量:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Location = new System.Drawing.Point(17, 141);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(83, 12);
            this.label58.TabIndex = 1;
            this.label58.Text = "注射样品速度:";
            // 
            // resetInj
            // 
            this.resetInj.Location = new System.Drawing.Point(210, 318);
            this.resetInj.Name = "resetInj";
            this.resetInj.Size = new System.Drawing.Size(89, 23);
            this.resetInj.TabIndex = 5;
            this.resetInj.Text = "复位进样器";
            this.resetInj.UseVisualStyleBackColor = true;
            this.resetInj.Click += new System.EventHandler(this.resetInj_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.Location = new System.Drawing.Point(201, 276);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(59, 12);
            this.label55.TabIndex = 1;
            this.label55.Text = "取样方式:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Location = new System.Drawing.Point(189, 64);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(59, 12);
            this.label47.TabIndex = 1;
            this.label47.Text = "泵样次数:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(110, 109);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(46, 21);
            this.textBox10.TabIndex = 4;
            this.textBox10.Text = "99.9";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.ForeColor = System.Drawing.Color.Black;
            this.label46.Location = new System.Drawing.Point(162, 71);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(17, 12);
            this.label46.TabIndex = 1;
            this.label46.Text = "次";
            // 
            // button15
            // 
            this.button15.ForeColor = System.Drawing.Color.Indigo;
            this.button15.Location = new System.Drawing.Point(9, 318);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(76, 23);
            this.button15.TabIndex = 1;
            this.button15.Text = "型号查询";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click_1);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.ForeColor = System.Drawing.Color.Black;
            this.label50.Location = new System.Drawing.Point(162, 91);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(17, 12);
            this.label50.TabIndex = 1;
            this.label50.Text = "秒";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(110, 88);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(46, 21);
            this.textBox12.TabIndex = 4;
            this.textBox12.Text = "99.9";
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.label54.ForeColor = System.Drawing.Color.Black;
            this.label54.Location = new System.Drawing.Point(162, 116);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(29, 12);
            this.label54.TabIndex = 1;
            this.label54.Text = "小时";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(282, 38);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(34, 21);
            this.textBox14.TabIndex = 4;
            this.textBox14.Text = "0";
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(318, 90);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(17, 12);
            this.label52.TabIndex = 1;
            this.label52.Text = "秒";
            // 
            // btnStartAutoINJ
            // 
            this.btnStartAutoINJ.Location = new System.Drawing.Point(100, 318);
            this.btnStartAutoINJ.Name = "btnStartAutoINJ";
            this.btnStartAutoINJ.Size = new System.Drawing.Size(91, 23);
            this.btnStartAutoINJ.TabIndex = 1;
            this.btnStartAutoINJ.Text = "启动进样";
            this.btnStartAutoINJ.UseVisualStyleBackColor = true;
            this.btnStartAutoINJ.Click += new System.EventHandler(this.btnStartAutoINJ_Click);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Location = new System.Drawing.Point(317, 65);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(17, 12);
            this.label48.TabIndex = 1;
            this.label48.Text = "次";
            // 
            // button24
            // 
            this.button24.ForeColor = System.Drawing.Color.Indigo;
            this.button24.Location = new System.Drawing.Point(11, 347);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(71, 23);
            this.button24.TabIndex = 1;
            this.button24.Text = "型号设定";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(318, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "次";
            // 
            // btnStopAutoINJ
            // 
            this.btnStopAutoINJ.Location = new System.Drawing.Point(100, 350);
            this.btnStopAutoINJ.Name = "btnStopAutoINJ";
            this.btnStopAutoINJ.Size = new System.Drawing.Size(89, 23);
            this.btnStopAutoINJ.TabIndex = 1;
            this.btnStopAutoINJ.Text = "停止进样";
            this.btnStopAutoINJ.UseVisualStyleBackColor = true;
            this.btnStopAutoINJ.Click += new System.EventHandler(this.btnStopAutoINJ_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "AOC-20i",
            "AS-2902"});
            this.comboBox1.Location = new System.Drawing.Point(153, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(70, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "AOC-20i";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(282, 61);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(34, 21);
            this.textBox13.TabIndex = 4;
            this.textBox13.Text = "0";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox3.Location = new System.Drawing.Point(288, 275);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(45, 20);
            this.comboBox3.TabIndex = 2;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(282, 84);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(34, 21);
            this.textBox11.TabIndex = 4;
            this.textBox11.Text = "99.9";
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox11
            // 
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Items.AddRange(new object[] {
            "慢速",
            "快速"});
            this.comboBox11.Location = new System.Drawing.Point(111, 160);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(45, 20);
            this.comboBox11.TabIndex = 2;
            this.comboBox11.Text = "慢速";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(110, 67);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(46, 21);
            this.textBox15.TabIndex = 4;
            this.textBox15.Text = "0";
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox12
            // 
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Items.AddRange(new object[] {
            "慢速",
            "中速",
            "快速"});
            this.comboBox12.Location = new System.Drawing.Point(109, 137);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(45, 20);
            this.comboBox12.TabIndex = 2;
            this.comboBox12.Text = "慢速";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(110, 40);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(46, 21);
            this.textBox9.TabIndex = 4;
            this.textBox9.Text = "0";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Location = new System.Drawing.Point(229, 19);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(47, 12);
            this.label40.TabIndex = 1;
            this.label40.Text = "进样口:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.comboBox2.Location = new System.Drawing.Point(282, 16);
            this.comboBox2.MaxLength = 1;
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(34, 20);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.Text = "0";
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "液体自动进样器";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.bControlTemp);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 52);
            this.panel1.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(256, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 12);
            this.label19.TabIndex = 1;
            this.label19.Text = "降温";
            // 
            // bControlTemp
            // 
            this.bControlTemp.BackColor = System.Drawing.SystemColors.Control;
            this.bControlTemp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bControlTemp.Location = new System.Drawing.Point(300, 2);
            this.bControlTemp.Name = "bControlTemp";
            this.bControlTemp.Size = new System.Drawing.Size(44, 43);
            this.bControlTemp.TabIndex = 1;
            this.bControlTemp.Text = "开始控温";
            this.bControlTemp.UseVisualStyleBackColor = false;
            this.bControlTemp.Click += new System.EventHandler(this.bControlTemp_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(211, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 1;
            this.label18.Text = "保持";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(6, 3);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(40, 42);
            this.button14.TabIndex = 1;
            this.button14.Text = "开始分析";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(160, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "升温";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(109, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "初温";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(57, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "准备";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(262, 5);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(20, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(213, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(163, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(111, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(61, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewCellStyle98.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle98.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle98.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle98;
            this.dataGridViewTextBoxColumn17.HeaderText = "事件3 [min]";
            this.dataGridViewTextBoxColumn17.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn17.Width = 66;
            // 
            // dataGridViewTextBoxColumn20
            // 
            dataGridViewCellStyle99.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle99.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle99.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle99;
            this.dataGridViewTextBoxColumn20.HeaderText = "事件6 [min]";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.Width = 66;
            // 
            // dataGridViewTextBoxColumn21
            // 
            dataGridViewCellStyle100.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle100.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle100.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle100;
            this.dataGridViewTextBoxColumn21.HeaderText = "事件7 [min]";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.Width = 66;
            // 
            // dataGridViewTextBoxColumn22
            // 
            dataGridViewCellStyle101.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle101.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle101.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn22.DefaultCellStyle = dataGridViewCellStyle101;
            this.dataGridViewTextBoxColumn22.HeaderText = "事件8 [min]";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.Width = 66;
            // 
            // dataGridViewTextBoxColumn23
            // 
            dataGridViewCellStyle102.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle102.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle102.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn23.DefaultCellStyle = dataGridViewCellStyle102;
            this.dataGridViewTextBoxColumn23.HeaderText = "阶号";
            this.dataGridViewTextBoxColumn23.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn23.Width = 60;
            // 
            // dataGridViewTextBoxColumn24
            // 
            dataGridViewCellStyle103.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle103.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle103.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn24.DefaultCellStyle = dataGridViewCellStyle103;
            this.dataGridViewTextBoxColumn24.HeaderText = "升温速率";
            this.dataGridViewTextBoxColumn24.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn24.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn24.Width = 80;
            // 
            // dataGridViewTextBoxColumn25
            // 
            dataGridViewCellStyle104.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle104.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle104.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle104;
            this.dataGridViewTextBoxColumn25.HeaderText = "保持温度";
            this.dataGridViewTextBoxColumn25.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn25.Width = 80;
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewCellStyle105.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle105.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle105.ForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle105;
            this.dataGridViewTextBoxColumn26.HeaderText = "保持时间";
            this.dataGridViewTextBoxColumn26.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn26.Width = 80;
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewCellStyle106.BackColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle106.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle106.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn27.DefaultCellStyle = dataGridViewCellStyle106;
            this.dataGridViewTextBoxColumn27.HeaderText = "起始瓶号";
            this.dataGridViewTextBoxColumn27.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn27.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn27.Width = 70;
            // 
            // dataGridViewTextBoxColumn28
            // 
            dataGridViewCellStyle107.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle107.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle107.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn28.DefaultCellStyle = dataGridViewCellStyle107;
            this.dataGridViewTextBoxColumn28.HeaderText = "终止瓶号";
            this.dataGridViewTextBoxColumn28.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn28.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn28.Width = 70;
            // 
            // dataGridViewTextBoxColumn29
            // 
            dataGridViewCellStyle108.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle108.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle108.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn29.DefaultCellStyle = dataGridViewCellStyle108;
            this.dataGridViewTextBoxColumn29.HeaderText = "进样量   [uL]";
            this.dataGridViewTextBoxColumn29.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn29.Width = 60;
            // 
            // dataGridViewTextBoxColumn30
            // 
            dataGridViewCellStyle109.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle109.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle109.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn30.DefaultCellStyle = dataGridViewCellStyle109;
            this.dataGridViewTextBoxColumn30.HeaderText = "次/瓶";
            this.dataGridViewTextBoxColumn30.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn30.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn30.Width = 60;
            // 
            // dataGridViewTextBoxColumn31
            // 
            dataGridViewCellStyle110.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle110.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn31.DefaultCellStyle = dataGridViewCellStyle110;
            this.dataGridViewTextBoxColumn31.HeaderText = "间隔    [min]";
            this.dataGridViewTextBoxColumn31.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.Width = 60;
            // 
            // dataGridViewTextBoxColumn32
            // 
            dataGridViewCellStyle111.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle111.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle111.ForeColor = System.Drawing.Color.Lime;
            this.dataGridViewTextBoxColumn32.DefaultCellStyle = dataGridViewCellStyle111;
            this.dataGridViewTextBoxColumn32.HeaderText = "序    号";
            this.dataGridViewTextBoxColumn32.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn32.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn32.Width = 80;
            // 
            // dataGridViewTextBoxColumn33
            // 
            dataGridViewCellStyle112.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle112.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle112.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn33.DefaultCellStyle = dataGridViewCellStyle112;
            this.dataGridViewTextBoxColumn33.HeaderText = "         版         本";
            this.dataGridViewTextBoxColumn33.MaxInputLength = 100;
            this.dataGridViewTextBoxColumn33.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn33.Width = 250;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 3;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.textBox1);
            this.groupBox10.Controls.Add(this.SendBtn);
            this.groupBox10.Location = new System.Drawing.Point(5, 574);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(200, 100);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "groupBox10";
            // 
            // InsDeviceCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbtempset);
            this.Name = "InsDeviceCtrl";
            this.Size = new System.Drawing.Size(367, 800);
            this.gbtempset.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPCControl)).EndInit();
            this.panelJY.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInsamp1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgtempControl)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvExtEvTP)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdTempControl)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.panConfig.ResumeLayout(false);
            this.panConfig.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvHardVersion)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tabPage16.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl3)).EndInit();
            this.tabControl3.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGramset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

	}

    private void button1_Click(object sender, EventArgs e)
    {

    }
}
