using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class DevMonitorForm : LclGnlForm
{
	private FormMainParam formmainParam = FormMainParam.Create();

	private const int int_0 = 110;

	public int ChannelNo;

	private int int_1;

	private byte[] byte_0 = new byte[0];

	private string string_45;

	private GraphicsPath[] graphicsPath_0 = new GraphicsPath[4];

	private GraphicsPath[] graphicsPath_1 = new GraphicsPath[4];

	private GraphicsPath[] graphicsPath_2 = new GraphicsPath[4];

	private GraphicsPath graphicsPath_3 = new GraphicsPath();

	private GraphicsPath graphicsPath_4 = new GraphicsPath();

	private GraphicsPath graphicsPath_5 = new GraphicsPath();

	private GraphicsPath graphicsPath_6 = new GraphicsPath();

	private GraphicsPath graphicsPath_7 = new GraphicsPath();

	private GraphicsPath graphicsPath_8 = new GraphicsPath();

	private GraphicsPath graphicsPath_9 = new GraphicsPath();

	private GraphicsPath graphicsPath_10 = new GraphicsPath();

	private GraphicsPath graphicsPath_11 = new GraphicsPath();

	public int injValveNum;

	private static Pen pen_0 = Pens.Gray;

	private static Point[] point_0 = new Point[4];

	private Struct1[] struct1_0;

	private static Rectangle[] rectangle_0 = new Rectangle[4];

	private float float_0;

	private float float_1;

	private float float_2;

	private BackgroundWorker backgroundWorker_0;

	public Button btnAlyStart;

	public Button btnAlyStop;

	private LclButton btnCloseCT;

	private LclButton btnClr1;

	private LclButton btnClr2;

	private LclButton btnClr3;

	private LclButton btnClr4;

	private LclButton btnClr5;

	private LclButton btnClr6;

	private LclButton btnCT6Qry;

	private LclButton btnCT6Set;

	private LclButton btnCteSet;

	private LclButton btnctrlNameSet;

	private Button btnDtcrsQry;

	private Button btnDtcrsSet;

	private LclButton btnFlow0;

	private LclButton btnFlow1;

	private Button btnHardVersionQry;

	private Button btninsSerialSet;

	public Button btnLight0;

	private LclButton btnMax0;

	private LclButton btnMax1;

	private LclButton btnMin0;

	private LclButton btnMin1;

	private LclButton btnMl0;

	private LclButton btnMl1;

	private Button btnNpQry;

	private Button btnNpSet;

	public LclButton btnPump0;

	public LclButton btnPump1;

	public LclButton btnPurge0;

	public LclButton btnPurge1;

	private LclButton btnRange0;

	private LclButton btnRistTime0;

	private LclButton btnSolvent0;

	private LclButton btnSolvent1;

	private LclButton btnStartCT;

	public Button btnStartFID1;

	public Button btnStartFID2;

	private LclButton btnStopMl;

	private LclButton btnTotalMl;

	private LclButton btnWarnMl;

	private LclButton btnWave0;

	private Button btnZero0;

	private Button button1;

	private CheckBox cbCapacityCtrl;

	private ComboBox cbRange0;

	private ComboBox cbRistTime0;

	private ComboBox cbSolvent0;

	private ComboBox cbSolvent1;

	public DataGridViewTextBoxColumn clmCT6CN;

	public DataGridViewCheckBoxColumn clmCT6CtrlT;

	public DataGridViewTextBoxColumn clmCT6EN;

	public DataGridViewTextBoxColumn clmCT6PtcT;

	public DataGridViewTextBoxColumn clmCT6SetT;

	public DataGridViewTextBoxColumn clmCT6T;

	public DataGridViewCheckBoxColumn clmDtBsdct;

	public DataGridViewComboBoxColumn clmDtFreq;

	public DataGridViewComboBoxColumn clmDtMark;

	public DataGridViewCheckBoxColumn clmDtPosi;

	public DataGridViewTextBoxColumn clmDtRange;

	private DataGridViewButtonColumn clmDtStart;

	private DataGridViewButtonColumn clmDtStop;

	public DataGridViewTextBoxColumn clmHV;

	private Struct5 struct5_0;

	private IContainer icontainer;

	private GradientRow gradientRow_0;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	public DataGridView dgvCT6;

	private LclDisplayPanel dpLC;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	public DataGridView gvDtcrs;

	public DataGridView gvHardVersion;

	public DataGridView gvNet;

	private Struct5 struct5_1;

	private Label label1;

	private Label label19;

	private Label label2;

	private Label label22;

	private Label label23;

	private Label label25;

	private Label label26;

	private Label label27;

	private Label label28;

	private Label label29;

	private Label label3;

	private Label label30;

	private Label label4;

	private Label label8;

	private Label label_0;

	private Label label_1;

	private LclLabel lbChkValve;

	private LclLabel[] lclLabel_0 = new LclLabel[4];

	private LclLabel lbFlow0;

	private LclLabel lbFlow1;

	private LclLabel[] lclLabel_1 = new LclLabel[4];

	public Label lbinsSerial;

	private LclLabel lblcComponent;

	private LclLabel lblcCpnt1;

	private LclLabel lblcCpnt2;

	private LclLabel lblcCpnt3;

	private LclLabel lblcCpnt4;

	private LclLabel lblcFlow;

	private LclLabel lblcFlow1;

	private LclLabel lblcFlow2;

	private LclLabel lblcFlow3;

	private LclLabel lblcFlow4;

	private LclLabel lblcInsLoginTime;

	private LclLabel lblcInsLoginTimeV;

	private LclLabel lblcTime;

	private LclLabel lblcTimeV;

	private LclLabel lblcTotalFlow;

	private LclLabel lblcTotalFlowV;

	private LclLabel lbMax;

	private LclLabel lbMin;

	private LclLabel lbMl0;

	private LclLabel lbMl1;

	private LclLabel lbPiston;

	private LclLabel lbPress;

	private LclLabel lbPress0;

	private LclLabel lbPress1;

	private Label lbRange0;

	private Label lbRistTime0;

	private LclLabel lbSeal;

	private LclLabel lbSolvent0;

	private LclLabel lbSolvent1;

	private Label lbStopMl;

	private Label lbTotalMl;

	private Label lbWarnMl;

	private Label lbWave0;

	private Color color_0 = Color.Black;

	private LclButton lclButton2;

	private LclButton lclButton4;

	private LclButton lclButton5;

	private LclButton lclButton6;

	private LclGroupBox lclGroupBox2;

	private LclGroupBox lclGroupBox3;

	private LclLabel lclLabel1;

	private LclLabel lclLabel10;

	private LclLabel lclLabel11;

	private LclLabel lclLabel12;

	private LclLabel lclLabel13;

	private LclLabel lclLabel14;

	private LclLabel lclLabel15;

	private LclLabel lclLabel16;

	private LclLabel lclLabel17;

	private LclLabel lclLabel18;

	private LclLabel lclLabel19;

	private LclLabel lclLabel2;

	private LclLabel lclLabel20;

	private LclLabel lclLabel21;

	private LclLabel lclLabel22;

	private LclLabel lclLabel23;

	private LclLabel lclLabel24;

	private LclLabel lclLabel3;

	private LclLabel lclLabel4;

	private LclLabel lclLabel5;

	private LclLabel lclLabel6;

	private LclLabel lclLabel7;

	private LclLabel lclLabel8;

	private LclLabel lclLabel9;

	private Struct5 struct5_2;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miGc;

	private ToolStripMenuItem migcAlyStart;

	private ToolStripMenuItem migcAlyStop;

	private ToolStripMenuItem migcDtStart1;

	private ToolStripMenuItem migcDtStart2;

	private ToolStripMenuItem migcDtStart3;

	private ToolStripMenuItem migcDtStart4;

	private ToolStripMenuItem migcDtStop1;

	private ToolStripMenuItem migcDtStop2;

	private ToolStripMenuItem migcDtStop3;

	private ToolStripMenuItem migcDtStop4;

	private MenuStrip msDevMnt;

	public NumericUpDown nudDtcrNum;

	private PictureBox pictureBox1;

	private ToolStripStatusLabel slbExplain;

	private StatusStrip ssDevMnt;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TabPage tabPage3;

	private TabPage tabPage4;

	private TabPage tabPage5;

	private TabPage tabPage6;

	private LclTextBox tbChkValveTotal;

	private LclTextBox tbColumnCurrent;

	private TextBox tbColumnTemp;

	private LclTextBox tbColumnTotal;

	private LclTextBox tbFlow0;

	private LclTextBox tbFlow1;

	public TextBox tbinsSerial;

	private TextBox tbLgtCurrent0;

	private TextBox tbLgtTotal0;

	private LclTextBox tbMax0;

	private LclTextBox tbMax1;

	private LclTextBox tbMin0;

	private LclTextBox tbMin1;

	private LclTextBox tbPump0ChkValveCurrent;

	private LclTextBox tbPump0ChkValveTotal;

	private LclTextBox tbPump0PistonCurrent;

	private LclTextBox tbPump0PistonTotal;

	private LclTextBox tbPump0SealCurrent;

	private LclTextBox tbPump0SealTotal;

	private LclTextBox tbPump1ChkValveCurrent;

	private LclTextBox tbPump1ChkValveTotal;

	private LclTextBox tbPump1PistonCurrent;

	private LclTextBox tbPump1PistonTotal;

	private LclTextBox tbPump1SealCurrent;

	private LclTextBox tbPump1SealTotal;

	private TextBox tbReferPool0;

	private TextBox tbSamplePool0;

	private TextBox tbStopMl;

	private TextBox tbTotalMl;

	private TextBox tbWarnMl;

	private TextBox tbWave0;

	private LclTabControl tcGcDevMnt;

	private LclTabControl tcLamp0;

	private LclTabControl tcLcDevMnt;

	private LclTabControl tcPump0;

	private LclTabControl tcPump1;

	private System.Windows.Forms.Timer timer_0;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	private TabPage tpChkValve;

	private TabPage tpColumn;

	private TabPage tpgcDtcs;

	private TabPage tpgcParas;

	private TabPage tpgcTemp;

	private TabPage tpLamp;

	private TabPage tpLC;

	private TabPage tpLCItems;

	private TabPage tpPump;

	public ToolStripStatusLabel tsslbGcListen;

	public ToolStripStatusLabel tsslbGcStatus;

	private IContainer components;

	private bool chkStopPump => cbCapacityCtrl.Checked && float_1 > float_0 && float_0 > 0f;

	private bool chkWarn => cbCapacityCtrl.Checked && float_1 > float_2 && float_2 > 0f;

	public bool Lighting => instrument.dtc_Channels.Length != 0 && instrument.dtc_Channels[0].Working;

	public bool Pump0Running => instrument.lcc_Pumps.Length >= 1 && instrument.lcc_Pumps[0].Working;

	public bool Pump1Running => instrument.lcc_Pumps.Length >= 2 && instrument.lcc_Pumps[1].Working;

	public DevMonitorForm(Instrument instrument)
	{
		InitializeComponent();
		Control.CheckForIllegalCrossThreadCalls = false;
		tcPump0.TabPages[1].Text = (tcPump1.TabPages[1].Text = (tcLamp0.TabPages[1].Text = Lang.PS("部件使用", "Components Use")));
		lclLabel19.Text = (lclLabel22.Text = Lang.PS("流量[ml/min]", "Flow[ml/min]"));
		btnPurge0.Text = (btnPurge1.Text = Lang.PS("冲洗[10]", "Purge[10]"));
		label_1.Text = Lang.PS("波长(nm)", "Wave (nm)");
		label_0.Text = Lang.PS("AU范围", "AU Range");
		label3.Text = Lang.PS("响应时间(s)", "Response(s)");
		label25.Text = Lang.PS("参照池", "refer pool");
		label26.Text = Lang.PS("样品池", "sample pool");
		label27.Text = Lang.PS("本次时间", "Current Time");
		label28.Text = Lang.PS("累计时间", "Total Time");
		btnZero0.Text = Lang.PS("自动归零", "Auto Zero");
		cbCapacityCtrl.Text = Lang.PS("流动相总量控制[ml]", "Flow phase gross control[ml]");
		label19.Text = Lang.PS("流动相总容量", "Total Capacity");
		label22.Text = Lang.PS("警告", "Warning");
		label23.Text = Lang.PS("停泵", "Stop pump");
		groupBox2.Text = Lang.PS("柱温箱", "Column oven");
		label29.Text = Lang.PS("柱温[℃]", "Temp.[℃]");
		button1.Text = Lang.PS("加热", "Heating");
		lclGroupBox2.Text = Lang.PS(" 使用时间[T]", " Used Time[T]");
		lclGroupBox3.Text = Lang.PS("使用次数[N]", "Used Times[N]");
		tpgcTemp.Text = Lang.PS("温度", "Temp.");
		tpgcParas.Text = Lang.PS("仪器", "Ins.");
		tpgcDtcs.Text = Lang.PS("检测器", "Dtcs.");
		tsslbGcListen.Text = (tsslbGcStatus.Text = "");
		base.instrument = instrument;
		backgroundWorker_0 = new BackgroundWorker();
		backgroundWorker_0.WorkerSupportsCancellation = true;
		backgroundWorker_0.WorkerReportsProgress = true;
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.ProgressChanged += backgroundWorker_0_ProgressChanged;
		cbRange0.SelectedIndex = 1;
		cbRistTime0.SelectedIndex = 1;
		dgvCT6.RowCount = 6;
		clmCT6CN.Visible = Class49.smethod_35();
		clmCT6EN.Visible = !Class49.smethod_35();
		int index = clmCT6CN.Index;
		dgvCT6.Rows[0].Cells[index].Value = "进样器1";
		dgvCT6.Rows[1].Cells[index].Value = "柱炉";
		dgvCT6.Rows[2].Cells[index].Value = "检测器1";
		dgvCT6.Rows[3].Cells[index].Value = "辅1/进2";
		dgvCT6.Rows[4].Cells[index].Value = "辅2/检2";
		dgvCT6.Rows[5].Cells[index].Value = "热导";
		int index2 = clmCT6EN.Index;
		dgvCT6.Rows[0].Cells[index2].Value = "Inj.1";
		dgvCT6.Rows[1].Cells[index2].Value = "Envolve";
		dgvCT6.Rows[2].Cells[index2].Value = "Dtc1";
		dgvCT6.Rows[3].Cells[index2].Value = "Aux.1/Inj.2";
		dgvCT6.Rows[4].Cells[index2].Value = "Aux.2/Dtc.2";
		dgvCT6.Rows[5].Cells[index2].Value = "Therm.";
		clmCT6T.ValueType = (clmCT6SetT.ValueType = (clmCT6PtcT.ValueType = typeof(float)));
		clmCT6CtrlT.ValueType = typeof(bool);
		clmCT6T.HeaderText = Lang.PS("实测[℃]", "Detect[℃]");
		clmCT6SetT.HeaderText = Lang.PS("设定[℃]", "Set[℃]");
		clmCT6PtcT.HeaderText = Lang.PS("保护[℃]", "Protect[℃]");
		clmCT6CtrlT.HeaderText = Lang.PS("控温", "Ctrl T");
		btnCT6Qry.Text = Lang.PS("查询", "Query");
		btnctrlNameSet.Text = Lang.PS("写控区名", "Set Ctrl Name");
		btnCT6Set.Text = Lang.PS("设置温度", "Set");
		btnCteSet.Text = Lang.PS("控温使能", "Ctrl Temp");
		btnStartCT.Text = Lang.PS("开始控温", "Start CtrlT");
		btnCloseCT.Text = Lang.PS("关闭控温", "Close CtrlT");
		clmDtRange.ValueType = typeof(byte);
		if (formmainParam.iDetector == 0)
		{
			clmDtMark.Items.Add("FID1");
			clmDtMark.Items.Add("FID2");
		}
		else if (formmainParam.iDetector == 1)
		{
			clmDtMark.Items.Add("FID1");
			clmDtMark.Items.Add("PDD2");
		}
		else if (formmainParam.iDetector == 2)
		{
			clmDtMark.Items.Add("PDD1");
			clmDtMark.Items.Add("PDD2");
		}
		clmDtMark.Items.Add("TCD1");
		clmDtMark.Items.Add("TCD2");
		clmDtMark.Items.Add("FPD1");
		clmDtMark.Items.Add("FPD2");
		clmDtMark.Items.Add("ECD1");
		clmDtMark.Items.Add("ECD2");
		clmDtMark.Items.Add("NPD1");
		clmDtMark.Items.Add("NPD2");
		clmDtFreq.Items.Add("1");
		clmDtFreq.Items.Add("2");
		clmDtFreq.Items.Add("3");
		clmDtFreq.Items.Add("4");
		clmDtFreq.Items.Add("5");
		gvNet.RowCount = 6;
		gvNet.Rows[0].HeaderCell.Value = Lang.PS("本机IP");
		gvNet.Rows[1].HeaderCell.Value = Lang.PS("掩码");
		gvNet.Rows[2].HeaderCell.Value = Lang.PS("网关");
		gvNet.Rows[3].HeaderCell.Value = Lang.PS("本地处理");
		gvNet.Rows[4].HeaderCell.Value = Lang.PS("主管");
		gvNet.Rows[5].HeaderCell.Value = Lang.PS("上级");
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		if (instrument.instruStyle != InstruStyle.GC)
		{
			while (instrument.logged && !e.Cancel)
			{
				Thread.Sleep(1000);
				backgroundWorker_0.ReportProgress(0);
			}
		}
	}

	private void backgroundWorker_0_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		method_5();
	}

	private void btnClr1_Click(object sender, EventArgs e)
	{
		struct1_0[0].struct5_0.dateTime_0 = DateTime.Now;
		struct1_0[0].struct5_0.timeSpan_0 = (struct1_0[0].struct5_0.timeSpan_1 = TimeSpan.Zero);
	}

	private void btnClr2_Click(object sender, EventArgs e)
	{
		struct1_0[0].struct5_1.dateTime_0 = DateTime.Now;
		struct1_0[0].struct5_1.timeSpan_0 = (struct1_0[0].struct5_1.timeSpan_1 = TimeSpan.Zero);
	}

	private void btnClr3_Click(object sender, EventArgs e)
	{
		struct1_0[0].struct5_2.dateTime_0 = DateTime.Now;
		struct1_0[0].struct5_2.timeSpan_0 = (struct1_0[0].struct5_2.timeSpan_1 = TimeSpan.Zero);
	}

	private void btnClr4_Click(object sender, EventArgs e)
	{
		struct1_0[1].struct5_0.dateTime_0 = DateTime.Now;
		struct1_0[1].struct5_0.timeSpan_0 = (struct1_0[1].struct5_0.timeSpan_1 = TimeSpan.Zero);
	}

	private void btnClr5_Click(object sender, EventArgs e)
	{
		struct1_0[1].struct5_1.dateTime_0 = DateTime.Now;
		struct1_0[1].struct5_1.timeSpan_0 = (struct1_0[1].struct5_1.timeSpan_1 = TimeSpan.Zero);
	}

	private void btnClr6_Click(object sender, EventArgs e)
	{
		struct1_0[1].struct5_2.dateTime_0 = DateTime.Now;
		struct1_0[1].struct5_2.timeSpan_0 = (struct1_0[1].struct5_2.timeSpan_1 = TimeSpan.Zero);
	}

	private void btnFlow0_Click(object sender, EventArgs e)
	{
		string s = "";
		LCC_Pump lCC_Pump = null;
		LclButton lclButton = null;
		if (sender == btnFlow0)
		{
			s = tbFlow0.Text;
			lCC_Pump = instrument.lcc_Pumps[0];
			lclButton = btnPurge0;
		}
		if (sender == btnFlow1)
		{
			s = tbFlow1.Text;
			lCC_Pump = instrument.lcc_Pumps[1];
			lclButton = btnPurge1;
		}
		if (float.TryParse(s, out var result))
		{
			lCC_Pump.Flow(write: true, result);
		}
		else
		{
			Class49.MessageBoxCheckInput();
		}
		lclButton.BackColor = Color.Transparent;
	}

	private void btnDtcrsSet_Click(object sender, EventArgs e)
	{
		if (!(sender as Button).Enabled)
		{
			return;
		}
		byte b = byte.MaxValue;
		if (sender == btnCT6Qry)
		{
			Array.Resize(ref byte_0, 3);
			byte_0[0] = 0;
			byte_0[1] = 66;
			byte_0[2] = 64;
			int_1 = 0;
			timer_0.Enabled = true;
			return;
		}
		if (sender == btnHardVersionQry)
		{
			b = 5;
		}
		if (sender == btnDtcrsQry)
		{
			b = 13;
		}
		if (sender == btnNpQry)
		{
			b = 48;
		}
		if (sender == btnCT6Set)
		{
			b = 8;
		}
		if (sender == btnctrlNameSet)
		{
			b = 65;
		}
		if (sender == btnCteSet)
		{
			b = 67;
		}
		if (sender == btninsSerialSet)
		{
			b = 7;
		}
		if (sender == btnDtcrsSet)
		{
			b = 14;
		}
		if (sender == btnStartCT)
		{
			b = 16;
		}
		if (sender == btnCloseCT)
		{
			b = 17;
		}
		if (sender == btnAlyStart)
		{
			instrument.form.btnDataAcquisition_Click(null, null);
			b = 18;
		}
		if (sender == btnAlyStop)
		{
			b = 19;
		}
		if (sender == btnStartFID1)
		{
			b = 20;
		}
		if (sender == btnStartFID2)
		{
			b = 21;
		}
		if (sender == btnNpSet)
		{
			b = 49;
		}
		Array.Resize(ref byte_0, 1);
		byte_0[0] = b;
		int_1 = 0;
		timer_0.Enabled = true;
	}

	public void btnLight_Click(object sender, EventArgs e)
	{
		if (!Lighting)
		{
			method_7();
		}
		else
		{
			struct5_2.timeSpan_0 = struct5_2.timeSpan_0.Add(struct5_2.timeSpan_1);
			if (instrument.sampling)
			{
				instrument.form.dataAcqForm.miAlyStopAcquisition_Click(null, null);
			}
			instrument.Detector_stop(onlyVirtual: false);
		}
		if (instrument.dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			dtC_Detector.OpenCloseLight(write: true, !Lighting);
		}
	}

	private void btnMax0_Click(object sender, EventArgs e)
	{
		string s = "";
		LCC_Pump lCC_Pump = null;
		if (sender == btnMax0)
		{
			s = tbMax0.Text;
			lCC_Pump = instrument.lcc_Pumps[0];
		}
		if (sender == btnMax1)
		{
			s = tbMax1.Text;
			lCC_Pump = instrument.lcc_Pumps[1];
		}
		if (float.TryParse(s, out var result))
		{
			lCC_Pump.MaxPress(write: true, result);
		}
		else
		{
			Class49.MessageBoxCheckInput();
		}
	}

	private void btnMin0_Click(object sender, EventArgs e)
	{
		string s = "";
		LCC_Pump lCC_Pump = null;
		if (sender == btnMin0)
		{
			s = tbMin0.Text;
			lCC_Pump = instrument.lcc_Pumps[0];
		}
		if (sender == btnMin1)
		{
			s = tbMin1.Text;
			lCC_Pump = instrument.lcc_Pumps[1];
		}
		if (float.TryParse(s, out var result))
		{
			lCC_Pump.MinPress(write: true, result);
		}
		else
		{
			Class49.MessageBoxCheckInput();
		}
	}

	private void btnMl0_Click(object sender, EventArgs e)
	{
		if (sender == btnMl0)
		{
			instrument.lcc_Pumps[0].double_0 = 0.0;
		}
		if (sender == btnMl1)
		{
			instrument.lcc_Pumps[1].double_0 = 0.0;
		}
	}

	private void btnPump0_Click(object sender, EventArgs e)
	{
		if (sender == btnPump0)
		{
			btnPumpClick(0);
		}
		if (sender == btnPump1)
		{
			btnPumpClick(1);
		}
	}

	public void btnPumpClick(int pumpNo)
	{
		bool flag = Pump0Running || Pump1Running;
		if (!((pumpNo == 0) ? Pump0Running : Pump1Running))
		{
			LCC_Pump lCC_Pump = instrument.lcc_Pumps[pumpNo];
			lCC_Pump.StartStop(1);
			struct1_0[pumpNo].dateTime_0 = DateTime.Now;
			struct1_0[pumpNo].float_0 = lCC_Pump.fFlow;
			lCC_Pump.hasShowWarn = false;
			struct1_0[pumpNo].struct5_0.dateTime_0 = (struct1_0[pumpNo].struct5_1.dateTime_0 = (struct1_0[pumpNo].struct5_2.dateTime_0 = DateTime.Now));
			struct1_0[pumpNo].struct5_0.timeSpan_1 = (struct1_0[pumpNo].struct5_1.timeSpan_1 = (struct1_0[pumpNo].struct5_2.timeSpan_1 = TimeSpan.Zero));
			bool flag2 = Pump0Running || Pump1Running;
			if (!flag && flag2)
			{
				struct5_0.dateTime_0 = DateTime.Now;
				struct5_0.timeSpan_1 = TimeSpan.Zero;
			}
		}
		else
		{
			instrument.lcc_Pumps[pumpNo].StartStop(0);
			struct1_0[pumpNo].struct5_0.timeSpan_0 = struct1_0[pumpNo].struct5_0.timeSpan_0.Add(struct1_0[pumpNo].struct5_0.timeSpan_1);
			struct1_0[pumpNo].struct5_1.timeSpan_0 = struct1_0[pumpNo].struct5_1.timeSpan_0.Add(struct1_0[pumpNo].struct5_1.timeSpan_1);
			struct1_0[pumpNo].struct5_2.timeSpan_0 = struct1_0[pumpNo].struct5_2.timeSpan_0.Add(struct1_0[pumpNo].struct5_2.timeSpan_1);
			bool flag3 = Pump0Running || Pump1Running;
			if (flag && !flag3)
			{
				struct5_0.timeSpan_0 = struct5_0.timeSpan_0.Add(struct5_0.timeSpan_1);
			}
		}
	}

	private void btnPurge0_Click(object sender, EventArgs e)
	{
		if (sender == btnPurge0)
		{
			instrument.lcc_Pumps[0].StartStop(2);
		}
		if (sender == btnPurge1)
		{
			instrument.lcc_Pumps[1].StartStop(2);
		}
	}

	private void btnRange0_Click(object sender, EventArgs e)
	{
		if (instrument.dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			dtC_Detector.Range(write: true, Class49.String2Float(cbRange0.SelectedItem, -1f));
		}
	}

	private void btnRistTime0_Click(object sender, EventArgs e)
	{
		if (instrument.dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			dtC_Detector.RistTime(write: true, Class49.String2Float(cbRistTime0.SelectedItem, -1f));
		}
	}

	private void btnSolvent0_Click(object sender, EventArgs e)
	{
		if (sender == btnSolvent0)
		{
			instrument.lcc_Pumps[0].Solvent(write: true, (byte)cbSolvent0.SelectedIndex);
		}
		if (sender == btnSolvent1)
		{
			instrument.lcc_Pumps[1].Solvent(write: true, (byte)cbSolvent1.SelectedIndex);
		}
	}

	private void btnStopMl_Click(object sender, EventArgs e)
	{
		if (!float.TryParse(tbStopMl.Text, out float_0) || float_0 <= 0f)
		{
			method_6();
		}
	}

	private void btnTotalMl_Click(object sender, EventArgs e)
	{
		if (!float.TryParse(tbTotalMl.Text, out float_1) || float_1 <= 0f)
		{
			method_6();
		}
	}

	private void btnWarnMl_Click(object sender, EventArgs e)
	{
		if (!float.TryParse(tbWarnMl.Text, out float_2) || float_2 <= 0f)
		{
			method_6();
		}
	}

	private void btnWave0_Click(object sender, EventArgs e)
	{
		if (ushort.TryParse(tbWave0.Text, out var result) && instrument.dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			dtC_Detector.Wave(write: true, result);
		}
	}

	public void btnZero_Click(object sender, EventArgs e)
	{
		if (instrument.dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			dtC_Detector.Zero();
		}
	}

	private void method_0(LCC_Pump lcc_Pump_0, int int_2)
	{
		if (chkStopPump && lcc_Pump_0.double_0 >= (double)(float_1 - float_0))
		{
			btnPumpClick(int_2);
		}
	}

	private void method_1(LCC_Pump lcc_Pump_0)
	{
		if (chkWarn && !lcc_Pump_0.hasShowWarn && lcc_Pump_0.double_0 >= (double)(float_1 - float_2))
		{
			lcc_Pump_0.hasShowWarn = true;
			MessageBox.Show(Lang.PS("流动相即将用完！", "Flow phase would be over!"), Lang.PS("警告", "Warn"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private string method_2(string string_46)
	{
		int num = string_46.IndexOf('.');
		if (num == -1)
		{
			return string_46;
		}
		return string_46.Remove(num);
	}

	private void DevMonitorForm_Load(object sender, EventArgs e)
	{
		Control control = tcLcDevMnt;
		tcGcDevMnt.Dock = DockStyle.Fill;
		control.Dock = DockStyle.Fill;
		base.Icon = SystemIconResource.smethod_10();
		miFiExit.Click += base.miFiExit_Click;
		msDevMnt.Items.Add(miView);
		msDevMnt.Items.Add(miWindow);
		miWindow.DropDownItems[2].Visible = false;
		miWinDataAcq.Visible = false;
		miWinChromatogram.Visible = false;
		miWinCaliGnl.Visible = false;
		miWinCaliGpc.Visible = false;
		miWinSglAly.Visible = false;
		miWinSeqAly.Visible = false;
		miWinDevMonitor.Visible = false;
		miWinStationAuditTrail.Visible = false;
		msDevMnt.Items.Add(miHelp);
		msDevMnt.Items.Add(new ToolStripSeparator());
		msDevMnt.Items.Add(mubtnMainForm);
		msDevMnt.Items.Add(mubtnInstrument);
		dpLC.Dock = DockStyle.Fill;
		for (int i = 0; i < 4; i++)
		{
			rectangle_0[i].X = 8;
			rectangle_0[i].Y = 29 + i * 28;
			rectangle_0[i].Size = new Size(160, 18);
			graphicsPath_2[i] = InstrumentForm.CreateRoundedRectanglePath(rectangle_0[i], 4, 110, ref graphicsPath_0[i], ref graphicsPath_1[i]);
			point_0[i].X = rectangle_0[i].Right + 7;
			point_0[i].Y = rectangle_0[i].Top - 4;
			switch (i)
			{
			case 0:
				lclLabel_0[i] = lblcCpnt1;
				break;
			case 1:
				lclLabel_0[i] = lblcCpnt2;
				break;
			case 2:
				lclLabel_0[i] = lblcCpnt3;
				break;
			case 3:
				lclLabel_0[i] = lblcCpnt4;
				break;
			}
			lclLabel_0[i].Location = new Point(rectangle_0[i].Left + 3, rectangle_0[i].Top + 3);
			lclLabel_0[i].Width = 110;
			lclLabel_0[i].Text = "-";
			switch (i)
			{
			case 0:
				lclLabel_1[i] = lblcFlow1;
				break;
			case 1:
				lclLabel_1[i] = lblcFlow2;
				break;
			case 2:
				lclLabel_1[i] = lblcFlow3;
				break;
			case 3:
				lclLabel_1[i] = lblcFlow4;
				break;
			}
			lclLabel_1[i].Location = lclLabel_0[i].Location;
			lclLabel_1[i].Left += 110;
			lclLabel_1[i].Width = rectangle_0[i].Width - 110 - 6;
			lclLabel_1[i].Text = "-";
		}
		lblcComponent.Location = new Point(rectangle_0[0].Left, rectangle_0[0].Top - 18);
		lblcComponent.Width = lclLabel_0[0].Width;
		lblcFlow.Location = new Point(lclLabel_1[0].Left, lblcComponent.Top);
		lblcFlow.Width = lclLabel_1[0].Width;
		Rectangle rect = rectangle_0[0];
		rect.X += 195;
		graphicsPath_6 = InstrumentForm.CreateRoundedRectanglePath(rect, 4, 110, ref graphicsPath_7, ref graphicsPath_8);
		lblcTime.Location = new Point(rect.Left + 3, rect.Top + 3);
		lblcTime.Width = lclLabel_0[0].Width;
		lblcTimeV.Location = new Point(rect.Left + 110 + 3, rect.Top + 3);
		int num = rectangle_0[0].Height + (rectangle_0[2].Y - rectangle_0[1].Y) / 5;
		rect.Y += num;
		graphicsPath_3 = InstrumentForm.CreateRoundedRectanglePath(rect, 4, 110, ref graphicsPath_4, ref graphicsPath_5);
		lblcInsLoginTime.Location = new Point(rect.Left + 3, rect.Top + 3);
		lblcInsLoginTime.Width = lclLabel_0[0].Width;
		lblcInsLoginTimeV.Location = new Point(rect.Left + 110 + 3, rect.Top + 3);
		rect.Y += num;
		graphicsPath_9 = InstrumentForm.CreateRoundedRectanglePath(rect, 4, 110, ref graphicsPath_10, ref graphicsPath_11);
		lblcTotalFlow.Location = new Point(rect.Left + 3, rect.Top + 3);
		lblcTotalFlow.Width = lclLabel_0[0].Width;
		lblcTotalFlowV.Location = new Point(rect.Left + 110 + 3, rect.Top + 3);
		LclLabel lclLabel = lblcTimeV;
		LclLabel lclLabel2 = lblcInsLoginTimeV;
		int num2 = (lblcTotalFlowV.Width = rect.Width - 110 - 6);
		int num4 = (lclLabel2.Width = num2);
		lclLabel.Width = num4;
		for (int j = 0; j < dpLC.Controls.Count; j++)
		{
			if (dpLC.Controls[j] is LclLabel || dpLC.Controls[j] is Label)
			{
				(dpLC.Controls[j] as Label).ForeColor = color_0;
			}
		}
		InstruWinsInfo instruWinsInfo = instrument.user.instrusWinsInfo[instrument.pageNo];
		if (instruWinsInfo.valid)
		{
			ReadWinInfo(instruWinsInfo.winInfos[2]);
		}
	}

	private void gvDtcrs_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		DataGridView dataGridView = sender as DataGridView;
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		MessageBox.Show(e.Exception.Message + rowIndex.ToString("\n行: 0") + columnIndex.ToString(" 列: 0\n值: ") + dataGridView.Rows[rowIndex].Cells[columnIndex].Value, "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		e.ThrowException = false;
	}

	private void dpLC_Paint(object sender, PaintEventArgs e)
	{
		try
		{
			method_3(e.Graphics);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void method_3(Graphics graphics_0)
	{
		for (int i = 0; i < 4; i++)
		{
			graphics_0.FillPath(InstrumentForm.brTxtFrmItem, graphicsPath_0[i]);
			graphics_0.FillPath(InstrumentForm.brTxtFrmValue, graphicsPath_1[i]);
			graphics_0.DrawPath(pen_0, graphicsPath_2[i]);
		}
		ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource8.smethod_0(), point_0[0]);
		ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource8.smethod_1(), point_0[1]);
		ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource8.smethod_2(), point_0[2]);
		ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource8.smethod_3(), point_0[3]);
		graphics_0.FillPath(InstrumentForm.brTxtFrmItem, graphicsPath_7);
		graphics_0.FillPath(InstrumentForm.brTxtFrmValue, graphicsPath_8);
		graphics_0.DrawPath(pen_0, graphicsPath_6);
		graphics_0.FillPath(InstrumentForm.brTxtFrmItem, graphicsPath_4);
		graphics_0.FillPath(InstrumentForm.brTxtFrmValue, graphicsPath_5);
		graphics_0.DrawPath(pen_0, graphicsPath_3);
		graphics_0.FillPath(InstrumentForm.brTxtFrmItem, graphicsPath_10);
		graphics_0.FillPath(InstrumentForm.brTxtFrmValue, graphicsPath_11);
		graphics_0.DrawPath(pen_0, graphicsPath_9);
	}

	public void gcShow(string listen, string satus, bool? enable)
	{
		if (listen != null)
		{
			tsslbGcListen.Text = listen;
		}
		if (satus != null)
		{
			tsslbGcStatus.Text = satus;
		}
		if (enable.HasValue)
		{
			tcGcDevMnt.Enabled = enable.Value;
		}
		if (listen == "停止侦听")
		{
			Array.Resize(ref byte_0, 6);
			byte_0[0] = 0;
			byte_0[1] = 66;
			byte_0[2] = 64;
			byte_0[3] = 5;
			byte_0[4] = 48;
			byte_0[5] = 13;
			int_1 = 0;
			timer_0.Enabled = true;
		}
		if (listen == "开始侦听")
		{
			for (int i = 0; i < 6; i++)
			{
				dgvCT6.Rows[i].Cells[clmCT6T.Index].Value = null;
			}
		}
	}

	private void gvDtcrs_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if ((e.ColumnIndex != clmDtStart.Index && e.ColumnIndex != clmDtStop.Index) || e.RowIndex < 0)
		{
			return;
		}
		byte b = byte.MaxValue;
		if (gvDtcrs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
		{
			if (e.ColumnIndex == clmDtStart.Index)
			{
				instrument.form.btnDataAcquisition_Click(null, null);
				b = 22;
			}
			if (e.ColumnIndex == clmDtStop.Index)
			{
				b = 23;
			}
			ChannelNo = e.RowIndex;
			Array.Resize(ref byte_0, 1);
			byte_0[0] = b;
			int_1 = 0;
			timer_0.Enabled = true;
		}
	}

	private void method_4(int int_2)
	{
		switch (int_2)
		{
		case 0:
			cbSolvent0.SelectedIndex = 0;
			break;
		case 1:
			cbSolvent1.SelectedIndex = 0;
			break;
		}
		struct1_0[int_2].struct5_0.timeSpan_0 = (struct1_0[int_2].struct5_1.timeSpan_0 = (struct1_0[int_2].struct5_2.timeSpan_0 = TimeSpan.Zero));
	}

	private void method_5()
	{
		if (method_8(instrument.sample_time))
		{
			GrdtOpt gradientOption = instrument.methodSetup.chromInfoR.LcGradient.gradientOption;
			lblcCpnt1.Text = (gradientOption.hasSolvent1 ? gradientOption.solvent1Name : "-");
			lblcCpnt2.Text = (gradientOption.hasSolvent2 ? gradientOption.solvent2Name : "-");
			lblcCpnt3.Text = (gradientOption.hasSolvent3 ? gradientOption.solvent3Name : "-");
			lblcCpnt4.Text = (gradientOption.hasSolvent4 ? gradientOption.solvent4Name : "-");
			lblcFlow1.Text = (gradientOption.hasSolvent1 ? (gradientRow_0.flow * gradientRow_0.float_0).ToString("0.000") : "-");
			lblcFlow2.Text = (gradientOption.hasSolvent2 ? (gradientRow_0.flow * gradientRow_0.float_1).ToString("0.000") : "-");
			lblcFlow3.Text = (gradientOption.hasSolvent3 ? (gradientRow_0.flow * gradientRow_0.float_2).ToString("0.000") : "-");
			lblcFlow4.Text = (gradientOption.hasSolvent4 ? (gradientRow_0.flow * gradientRow_0.float_3).ToString("0.000") : "-");
		}
		lblcTimeV.Text = (instrument.sampling ? instrument.sample_time : instrument.idle_time).ToString("0.00 min");
		struct5_1.timeSpan_1 = DateTime.Now.Subtract(struct5_1.dateTime_0);
		lblcInsLoginTimeV.Text = struct5_1.timeSpan_1.TotalMinutes.ToString("0.0");
		lblcTotalFlowV.Text = method_10();
		if (tcPump0.Visible = instrument.lcc_Pumps.Length >= 1)
		{
			LCC_Pump lCC_Pump = instrument.lcc_Pumps[0];
			tcPump0.TabPages[0].Text = lCC_Pump.name;
			if (Pump0Running)
			{
				btnPump0.Text = Lang.PS("停止泵", "Stop Pump");
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = now.Subtract(struct1_0[0].dateTime_0);
				lCC_Pump.double_0 += (double)((struct1_0[0].float_0 + lCC_Pump.fFlow) / 2f) * timeSpan.TotalMinutes;
				struct1_0[0].dateTime_0 = now;
				struct1_0[0].float_0 = lCC_Pump.fFlow;
				struct1_0[0].struct5_0.timeSpan_1 = now.Subtract(struct1_0[0].struct5_0.dateTime_0);
				struct1_0[0].struct5_1.timeSpan_1 = now.Subtract(struct1_0[0].struct5_1.dateTime_0);
				struct1_0[0].struct5_2.timeSpan_1 = now.Subtract(struct1_0[0].struct5_2.dateTime_0);
			}
			else
			{
				btnPump0.Text = Lang.PS("启动泵", "Start Pump");
			}
			lbPress0.Text = "-";
			lbFlow0.Text = lCC_Pump.fFlow.ToString("0.000");
			lbMl0.Text = lCC_Pump.double_0.ToString("0.0 ml");
			lbSolvent0.Text = cbSolvent0.Items[lCC_Pump.idxSolvent].ToString();
			tbPump0PistonCurrent.Text = method_2(struct1_0[0].struct5_0.timeSpan_1.ToString());
			tbPump0PistonTotal.Text = method_2(struct1_0[0].struct5_0.timeSpan_0.Add(Pump0Running ? struct1_0[0].struct5_0.timeSpan_1 : TimeSpan.Zero).ToString());
			tbPump0SealCurrent.Text = method_2(struct1_0[0].struct5_1.timeSpan_1.ToString());
			tbPump0SealTotal.Text = method_2(struct1_0[0].struct5_1.timeSpan_0.Add(Pump0Running ? struct1_0[0].struct5_1.timeSpan_1 : TimeSpan.Zero).ToString());
			tbPump0ChkValveCurrent.Text = method_2(struct1_0[0].struct5_2.timeSpan_1.ToString());
			tbPump0ChkValveTotal.Text = method_2(struct1_0[0].struct5_2.timeSpan_0.Add(Pump0Running ? struct1_0[0].struct5_2.timeSpan_1 : TimeSpan.Zero).ToString());
			if (Pump0Running)
			{
				method_1(lCC_Pump);
				method_0(lCC_Pump, 0);
			}
		}
		if (tcPump1.Visible = instrument.lcc_Pumps.Length >= 2)
		{
			LCC_Pump lCC_Pump2 = instrument.lcc_Pumps[1];
			tcPump1.TabPages[0].Text = lCC_Pump2.name;
			if (Pump1Running)
			{
				btnPump1.Text = Lang.PS("停止泵", "Stop Pump");
				DateTime now2 = DateTime.Now;
				TimeSpan timeSpan2 = now2.Subtract(struct1_0[1].dateTime_0);
				lCC_Pump2.double_0 += (double)((struct1_0[1].float_0 + lCC_Pump2.fFlow) / 2f) * timeSpan2.TotalMinutes;
				struct1_0[1].dateTime_0 = now2;
				struct1_0[1].float_0 = lCC_Pump2.fFlow;
				struct1_0[1].struct5_0.timeSpan_1 = now2.Subtract(struct1_0[1].struct5_0.dateTime_0);
				struct1_0[1].struct5_1.timeSpan_1 = now2.Subtract(struct1_0[1].struct5_1.dateTime_0);
				struct1_0[1].struct5_2.timeSpan_1 = now2.Subtract(struct1_0[1].struct5_2.dateTime_0);
			}
			else
			{
				btnPump1.Text = Lang.PS("启动泵", "Start Pump");
			}
			lbPress1.Text = "-";
			lbFlow1.Text = lCC_Pump2.fFlow.ToString("0.000");
			lbMl1.Text = lCC_Pump2.double_0.ToString("0.0 ml");
			lbSolvent1.Text = cbSolvent1.Items[lCC_Pump2.idxSolvent].ToString();
			tbPump1PistonCurrent.Text = method_2(struct1_0[1].struct5_0.timeSpan_1.ToString());
			tbPump1PistonTotal.Text = method_2(struct1_0[1].struct5_0.timeSpan_0.Add(Pump1Running ? struct1_0[1].struct5_0.timeSpan_1 : TimeSpan.Zero).ToString());
			tbPump1SealCurrent.Text = method_2(struct1_0[1].struct5_1.timeSpan_1.ToString());
			tbPump1SealTotal.Text = method_2(struct1_0[1].struct5_1.timeSpan_0.Add(Pump1Running ? struct1_0[1].struct5_1.timeSpan_1 : TimeSpan.Zero).ToString());
			tbPump1ChkValveCurrent.Text = method_2(struct1_0[1].struct5_2.timeSpan_1.ToString());
			tbPump1ChkValveTotal.Text = method_2(struct1_0[1].struct5_2.timeSpan_0.Add(Pump1Running ? struct1_0[1].struct5_2.timeSpan_1 : TimeSpan.Zero).ToString());
			if (Pump1Running)
			{
				method_1(lCC_Pump2);
				method_0(lCC_Pump2, 1);
			}
		}
		bool flag3 = (tcLamp0.Visible = instrument.dtc_Channels.Length != 0);
		if (flag3 && instrument.dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			tcLamp0.TabPages[0].Text = dtC_Detector.name;
			if (Lighting)
			{
				btnLight0.Text = Lang.PS("关闭氘灯", "Close Lamp");
				struct5_2.timeSpan_1 = DateTime.Now.Subtract(struct5_2.dateTime_0);
			}
			else
			{
				btnLight0.Text = Lang.PS("打开氘灯", "Open Lamp");
			}
			lbWave0.Text = dtC_Detector.wave.ToString();
			lbRange0.Text = dtC_Detector.range.ToString("0.00");
			lbRistTime0.Text = dtC_Detector.ristTime.ToString("0.0");
			tbLgtCurrent0.Text = method_2(struct5_2.timeSpan_1.ToString());
			tbLgtTotal0.Text = method_2(struct5_2.timeSpan_0.Add(Lighting ? struct5_2.timeSpan_1 : TimeSpan.Zero).ToString());
		}
		tbChkValveTotal.Text = injValveNum.ToString();
		bool flag5;
		if (flag5 = Pump0Running || Pump1Running)
		{
			struct5_0.timeSpan_1 = DateTime.Now.Subtract(struct5_0.dateTime_0);
		}
		tbColumnCurrent.Text = method_2(struct5_0.timeSpan_1.ToString());
		tbColumnTotal.Text = method_2(struct5_0.timeSpan_0.Add(flag5 ? struct5_0.timeSpan_1 : TimeSpan.Zero).ToString());
		lbTotalMl.Text = float_1.ToString();
		lbWarnMl.Text = float_2.ToString();
		lbStopMl.Text = float_0.ToString();
		instrument.form.Refresh1();
	}

	private void lclButton4_Click(object sender, EventArgs e)
	{
		struct5_2.dateTime_0 = DateTime.Now;
		struct5_2.timeSpan_0 = (struct5_2.timeSpan_1 = TimeSpan.Zero);
	}

	private void lclButton5_Click(object sender, EventArgs e)
	{
		struct5_0.dateTime_0 = DateTime.Now;
		struct5_0.timeSpan_0 = (struct5_0.timeSpan_1 = TimeSpan.Zero);
	}

	private void lclButton6_Click(object sender, EventArgs e)
	{
		injValveNum = 0;
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
		{
			Text = "设备监控";
			miFile.Text = "文件";
			miFiExit.Text = "关闭";
			tpLC.Text = "信息";
			lblcComponent.Text = "组分";
			lblcFlow.Text = "流速[mL/min]";
			lblcTime.Text = "本次时间[min.]";
			lblcInsLoginTime.Text = "仪器登录时间[min]";
			lblcTotalFlow.Text = "总流速[mL/min]";
			tpPump.Text = "泵";
			LclLabel lclLabel17 = lbPress;
			string text = (this.lclLabel21.Text = "压力[MPa]");
			lclLabel17.Text = text;
			LclLabel lclLabel18 = lbMin;
			text = (this.lclLabel23.Text = "最小");
			lclLabel18.Text = text;
			LclLabel lclLabel19 = lbMax;
			text = (this.lclLabel20.Text = "最大");
			lclLabel19.Text = text;
			Label label2 = label8;
			text = (label1.Text = "溶剂 ");
			label2.Text = text;
			LclLabel lclLabel20 = lbPiston;
			text = (this.lclLabel18.Text = "柱塞杆");
			lclLabel20.Text = text;
			LclLabel lclLabel21 = lbSeal;
			text = (this.lclLabel17.Text = "密封圈");
			lclLabel21.Text = text;
			LclLabel lclLabel22 = lbChkValve;
			text = (this.lclLabel13.Text = "单向阀");
			lclLabel22.Text = text;
			LclLabel lclLabel23 = this.lclLabel4;
			LclLabel lclLabel24 = this.lclLabel5;
			LclLabel lclLabel25 = this.lclLabel6;
			LclLabel lclLabel26 = this.lclLabel15;
			LclLabel lclLabel27 = this.lclLabel11;
			string text9 = (this.lclLabel10.Text = "总计");
			string text11 = (lclLabel27.Text = text9);
			string text13 = (lclLabel26.Text = text11);
			string text15 = (lclLabel25.Text = text13);
			text = (lclLabel24.Text = text15);
			lclLabel23.Text = text;
			LclLabel lclLabel28 = this.lclLabel7;
			LclLabel lclLabel29 = this.lclLabel8;
			LclLabel lclLabel30 = this.lclLabel9;
			LclLabel lclLabel31 = this.lclLabel16;
			LclLabel lclLabel32 = this.lclLabel14;
			text9 = (this.lclLabel12.Text = "本次");
			text11 = (lclLabel32.Text = text9);
			text13 = (lclLabel31.Text = text11);
			text15 = (lclLabel30.Text = text13);
			text = (lclLabel29.Text = text15);
			lclLabel28.Text = text;
			tpColumn.Text = "色谱柱";
			lclLabel1.Text = "总计";
			this.lclLabel2.Text = "本次";
			tpLamp.Text = "检测器";
			tpChkValve.Text = "进样阀";
			this.lclLabel3.Text = "总计";
			tpLCItems.Text = "项目";
			break;
		}
		case SysLanguage.EN:
		{
			Text = "Device Monitor";
			miFile.Text = "File";
			miFiExit.Text = "Close";
			tpLC.Text = "Info.";
			lblcComponent.Text = "Component";
			lblcFlow.Text = "Flow[mL/min]";
			lblcTime.Text = "Current Time[min.]";
			lblcInsLoginTime.Text = "Ins. Longin Time[min]";
			lblcTotalFlow.Text = "Total Flow[mL/min]";
			tpPump.Text = "Pump";
			LclLabel lclLabel = lbPress;
			string text = (this.lclLabel21.Text = "Pressure[MPa]");
			lclLabel.Text = text;
			LclLabel lclLabel2 = lbMin;
			text = (this.lclLabel23.Text = "Min");
			lclLabel2.Text = text;
			LclLabel lclLabel3 = lbMax;
			text = (this.lclLabel20.Text = "Max");
			lclLabel3.Text = text;
			Label label = label8;
			text = (label1.Text = "Solvent ");
			label.Text = text;
			LclLabel lclLabel4 = lbPiston;
			text = (this.lclLabel18.Text = "Piston");
			lclLabel4.Text = text;
			LclLabel lclLabel5 = lbSeal;
			text = (this.lclLabel17.Text = "Seal");
			lclLabel5.Text = text;
			LclLabel lclLabel6 = lbChkValve;
			text = (this.lclLabel13.Text = "ChkValve");
			lclLabel6.Text = text;
			LclLabel lclLabel7 = this.lclLabel4;
			LclLabel lclLabel8 = this.lclLabel5;
			LclLabel lclLabel9 = this.lclLabel6;
			LclLabel lclLabel10 = this.lclLabel15;
			LclLabel lclLabel11 = this.lclLabel11;
			string text9 = (this.lclLabel10.Text = "Total");
			string text11 = (lclLabel11.Text = text9);
			string text13 = (lclLabel10.Text = text11);
			string text15 = (lclLabel9.Text = text13);
			text = (lclLabel8.Text = text15);
			lclLabel7.Text = text;
			LclLabel lclLabel12 = this.lclLabel7;
			LclLabel lclLabel13 = this.lclLabel8;
			LclLabel lclLabel14 = this.lclLabel9;
			LclLabel lclLabel15 = this.lclLabel16;
			LclLabel lclLabel16 = this.lclLabel14;
			text9 = (this.lclLabel12.Text = "Current");
			text11 = (lclLabel16.Text = text9);
			text13 = (lclLabel15.Text = text11);
			text15 = (lclLabel14.Text = text13);
			text = (lclLabel13.Text = text15);
			lclLabel12.Text = text;
			tpColumn.Text = "Column";
			lclLabel1.Text = "Total";
			this.lclLabel2.Text = "Current";
			tpLamp.Text = "Detector";
			tpChkValve.Text = "Inj. Valve";
			this.lclLabel3.Text = "Total";
			tpLCItems.Text = "Items";
			break;
		}
		}
	}

	private void migcDtStop4_Click(object sender, EventArgs e)
	{
		if (sender == migcAlyStart)
		{
			btnDtcrsSet_Click(btnAlyStart, null);
			return;
		}
		if (sender == migcAlyStop)
		{
			btnDtcrsSet_Click(btnAlyStop, null);
			return;
		}
		byte b = byte.MaxValue;
		if (sender == migcDtStart1 || sender == migcDtStart2 || sender == migcDtStart3 || sender == migcDtStart4)
		{
			instrument.form.btnDataAcquisition_Click(null, null);
			b = 22;
		}
		if (sender == migcDtStop1 || sender == migcDtStop2 || sender == migcDtStop3 || sender == migcDtStop4)
		{
			b = 23;
		}
		ChannelNo = int.Parse((sender as ToolStripMenuItem).Tag.ToString());
		if (ChannelNo < gvDtcrs.RowCount)
		{
			Array.Resize(ref byte_0, 1);
			byte_0[0] = b;
			int_1 = 0;
			timer_0.Enabled = true;
		}
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
		Class49.smethod_32("设备监控");
	}

	private void method_6()
	{
		MessageBox.Show(Lang.PS("要求大于0数值！", "Need >0 value!"));
	}

	private void nudDtcrNum_ValueChanged(object sender, EventArgs e)
	{
		gvDtcrs.RowCount = (int)nudDtcrNum.Value;
		for (int i = 0; i < gvDtcrs.RowCount; i++)
		{
			gvDtcrs.Rows[i].HeaderCell.Value = (i + 1).ToString();
			gvDtcrs.Rows[i].Cells[clmDtStart.Index].Value = "启动";
			gvDtcrs.Rows[i].Cells[clmDtStop.Index].Value = "停止";
		}
	}

	public void OnLoginInstrument()
	{
		string_45 = ResourceImageLoad.ExePath() + "Common\\DevMonitor" + instrument.pageNo;
		struct5_1.dateTime_0 = DateTime.Now;
		if (File.Exists(string_45))
		{
			Class49.OpenBinaryReader(string_45, out var _, out var fileStream_, out var binaryReader_);
			try
			{
				Array.Resize(ref struct1_0, binaryReader_.ReadInt32());
				for (int i = 0; i < struct1_0.Length; i++)
				{
					switch (i)
					{
					case 0:
						tbMin0.Text = binaryReader_.ReadString();
						tbMax0.Text = binaryReader_.ReadString();
						tbFlow0.Text = binaryReader_.ReadString();
						cbSolvent0.SelectedIndex = binaryReader_.ReadInt32();
						break;
					case 1:
						tbMin1.Text = binaryReader_.ReadString();
						tbMax1.Text = binaryReader_.ReadString();
						tbFlow1.Text = binaryReader_.ReadString();
						cbSolvent1.SelectedIndex = binaryReader_.ReadInt32();
						break;
					}
					struct1_0[i].struct5_0.timeSpan_0 = TimeSpan.FromTicks(binaryReader_.ReadInt64());
					struct1_0[i].struct5_1.timeSpan_0 = TimeSpan.FromTicks(binaryReader_.ReadInt64());
					struct1_0[i].struct5_2.timeSpan_0 = TimeSpan.FromTicks(binaryReader_.ReadInt64());
				}
				int num = struct1_0.Length;
				Array.Resize(ref struct1_0, instrument.lcc_Pumps.Length);
				for (int j = num; j < struct1_0.Length; j++)
				{
					struct1_0[j].string_0 = "20";
					struct1_0[j].string_1 = "25";
					struct1_0[j].string_2 = "1";
					struct1_0[j].int_0 = 0;
					method_4(j);
				}
				tbWave0.Text = binaryReader_.ReadString();
				cbRange0.SelectedIndex = binaryReader_.ReadInt32();
				cbRistTime0.SelectedIndex = binaryReader_.ReadInt32();
				tbReferPool0.Text = binaryReader_.ReadString();
				tbSamplePool0.Text = binaryReader_.ReadString();
				struct5_2.dateTime_0 = DateTime.Now;
				struct5_2.timeSpan_0 = TimeSpan.FromTicks(binaryReader_.ReadInt64());
				injValveNum = binaryReader_.ReadInt32();
				struct5_0.timeSpan_0 = TimeSpan.FromTicks(binaryReader_.ReadInt64());
				tbColumnTemp.Text = binaryReader_.ReadString();
				cbCapacityCtrl.Checked = binaryReader_.ReadBoolean();
				tbTotalMl.Text = binaryReader_.ReadString();
				tbWarnMl.Text = binaryReader_.ReadString();
				tbStopMl.Text = binaryReader_.ReadString();
			}
			finally
			{
				binaryReader_.Close();
				fileStream_.Close();
			}
		}
		else
		{
			Array.Resize(ref struct1_0, instrument.lcc_Pumps.Length);
			for (int k = 0; k < struct1_0.Length; k++)
			{
				method_4(k);
			}
			cbRange0.SelectedIndex = 3;
			cbRistTime0.SelectedIndex = 3;
			struct5_2.timeSpan_0 = TimeSpan.Zero;
			injValveNum = 0;
			struct5_0.timeSpan_0 = TimeSpan.Zero;
		}
		if (!backgroundWorker_0.IsBusy)
		{
			backgroundWorker_0.RunWorkerAsync();
		}
		if (Lighting)
		{
			method_7();
		}
	}

	public void OnLogoutInstrument()
	{
		if (Pump0Running)
		{
			btnPumpClick(0);
		}
		if (Pump1Running)
		{
			btnPumpClick(1);
		}
		string_45 = ResourceImageLoad.ExePath() + "Common\\DevMonitor" + instrument.pageNo;
		Class49.OpenBinaryWriter(string_45, out var _, out var fileStream_, out var binaryWriter_);
		try
		{
			binaryWriter_.Write(struct1_0.Length);
			for (int i = 0; i < struct1_0.Length; i++)
			{
				switch (i)
				{
				case 0:
					binaryWriter_.Write(tbMin0.Text);
					binaryWriter_.Write(tbMax0.Text);
					binaryWriter_.Write(tbFlow0.Text);
					binaryWriter_.Write(cbSolvent0.SelectedIndex);
					break;
				case 1:
					binaryWriter_.Write(tbMin1.Text);
					binaryWriter_.Write(tbMax1.Text);
					binaryWriter_.Write(tbFlow1.Text);
					binaryWriter_.Write(cbSolvent1.SelectedIndex);
					break;
				}
				binaryWriter_.Write(struct1_0[i].struct5_0.timeSpan_0.Ticks);
				binaryWriter_.Write(struct1_0[i].struct5_1.timeSpan_0.Ticks);
				binaryWriter_.Write(struct1_0[i].struct5_2.timeSpan_0.Ticks);
			}
			binaryWriter_.Write(tbWave0.Text);
			binaryWriter_.Write(cbRange0.SelectedIndex);
			binaryWriter_.Write(cbRistTime0.SelectedIndex);
			binaryWriter_.Write(tbReferPool0.Text);
			binaryWriter_.Write(tbSamplePool0.Text);
			binaryWriter_.Write(struct5_2.timeSpan_0.Ticks);
			binaryWriter_.Write(injValveNum);
			binaryWriter_.Write(struct5_0.timeSpan_0.Ticks);
			binaryWriter_.Write(tbColumnTemp.Text);
			binaryWriter_.Write(cbCapacityCtrl.Checked);
			binaryWriter_.Write(tbTotalMl.Text);
			binaryWriter_.Write(tbWarnMl.Text);
			binaryWriter_.Write(tbStopMl.Text);
		}
		finally
		{
			binaryWriter_.Close();
			fileStream_.Close();
		}
		if (backgroundWorker_0.IsBusy)
		{
			backgroundWorker_0.CancelAsync();
		}
		for (int j = 0; j < instrument.gcc_GCss.Length; j++)
		{
			if (instrument.gcc_GCss[j] is GC08_GCs)
			{
				(instrument.gcc_GCss[j] as GC08_GCs).LogOut();
			}
		}
	}

	private void method_7()
	{
		struct5_2.dateTime_0 = DateTime.Now;
		struct5_2.timeSpan_1 = TimeSpan.Zero;
		instrument.beginIdleTC = Environment.TickCount;
		instrument.ResetSglsSamplingOriDots(createDiskFile: false);
		instrument.daf_BeginGather(sample: false, InjectStyle.Single);
	}

	public override void refresh_once()
	{
		base.refresh_once();
		slbExplain.Text = instrument.name;
		tcLcDevMnt.Visible = instrument.instruStyle != InstruStyle.GC;
		LclTabControl lclTabControl = tcGcDevMnt;
		ToolStripMenuItem toolStripMenuItem = miGc;
		ToolStripStatusLabel toolStripStatusLabel = tsslbGcStatus;
		bool flag = (tsslbGcListen.Visible = instrument.instruStyle == InstruStyle.GC);
		bool flag3 = (toolStripStatusLabel.Visible = flag);
		bool visible = (toolStripMenuItem.Visible = flag3);
		lclTabControl.Visible = visible;
	}

	private bool method_8(float float_3)
	{
		return instrument.retGrdtRow(instrument.sampling ? float_3 : 0f, ref gradientRow_0);
	}

	private bool method_9(byte byte_1)
	{
		if (!(tsslbGcListen.Text == "开始侦听") && byte_1 != byte.MaxValue)
		{
			for (int i = 0; i < instrument.gcc_GCss.Length; i++)
			{
				if (instrument.gcc_GCss[i] is GC08_GCs)
				{
					(instrument.gcc_GCss[i] as GC08_GCs).Send(byte_1);
				}
			}
			return true;
		}
		return false;
	}

	public void SetTopPage(DM_InitPage initPage)
	{
		switch (initPage)
		{
		case DM_InitPage.Device:
			tcLcDevMnt.SelectedTab = ((instrument.instruStyle == InstruStyle.GC) ? null : tpLC);
			break;
		case DM_InitPage.DM2:
			tcLcDevMnt.SelectedTab = ((instrument.instruStyle == InstruStyle.GC) ? null : tpChkValve);
			break;
		case DM_InitPage.DM3:
			tcLcDevMnt.SelectedTab = ((instrument.instruStyle == InstruStyle.GC) ? null : tpPump);
			break;
		case DM_InitPage.DM4:
			tcLcDevMnt.SelectedTab = ((instrument.instruStyle == InstruStyle.GC) ? null : tpColumn);
			break;
		}
	}

	public void StopPumps()
	{
		if (Pump0Running)
		{
			btnPumpClick(0);
		}
		if (Pump1Running)
		{
			btnPumpClick(1);
		}
	}

	public void SubmitFlows()
	{
		if (btnFlow0.Visible)
		{
			btnFlow0_Click(btnFlow0, null);
		}
		if (btnFlow1.Visible)
		{
			btnFlow0_Click(btnFlow1, null);
		}
	}

	private void tbFlow0_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (sender == tbFlow0)
			{
				btnFlow0_Click(btnFlow0, null);
			}
			if (sender == tbFlow1)
			{
				btnFlow0_Click(btnFlow1, null);
			}
		}
	}

	private void tbMax0_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (sender == tbMax0)
			{
				btnMax0_Click(btnMax0, null);
			}
			if (sender == tbMax1)
			{
				btnMax0_Click(btnMax1, null);
			}
		}
	}

	private void tbMin0_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (sender == tbMin0)
			{
				btnMin0_Click(btnMin0, null);
			}
			if (sender == tbMin1)
			{
				btnMin0_Click(btnMin1, null);
			}
		}
	}

	private void tbStopMl_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnStopMl_Click(null, null);
		}
	}

	private void tbTotalMl_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnTotalMl_Click(null, null);
		}
	}

	private void tbWarnMl_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnWarnMl_Click(null, null);
		}
	}

	private void tbWave0_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnWave0_Click(null, null);
		}
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		if (method_9(byte_0[int_1++]))
		{
			timer_0.Enabled = int_1 < byte_0.Length;
		}
		else
		{
			timer_0.Enabled = false;
		}
	}

	private string method_10()
	{
		float num = 0f;
		for (int i = 0; i < instrument.lcc_Pumps.Length; i++)
		{
			num += instrument.lcc_Pumps[i].fFlow;
		}
		return num.ToString("0.000");
	}

	private void tsslbGcListen_Click(object sender, EventArgs e)
	{
		if (tsslbGcListen.Text == "停止侦听")
		{
			for (int i = 0; i < instrument.gcc_GCss.Length; i++)
			{
			}
			gcShow("开始侦听", "", false);
		}
		else
		{
			for (int j = 0; j < instrument.gcc_GCss.Length; j++)
			{
			}
			gcShow("停止侦听", "[侦听进行中]", false);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer != null)
		{
			icontainer.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		this.msDevMnt = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miGc = new System.Windows.Forms.ToolStripMenuItem();
		this.migcAlyStart = new System.Windows.Forms.ToolStripMenuItem();
		this.migcAlyStop = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.migcDtStart1 = new System.Windows.Forms.ToolStripMenuItem();
		this.migcDtStart2 = new System.Windows.Forms.ToolStripMenuItem();
		this.migcDtStart3 = new System.Windows.Forms.ToolStripMenuItem();
		this.migcDtStart4 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.migcDtStop1 = new System.Windows.Forms.ToolStripMenuItem();
		this.migcDtStop2 = new System.Windows.Forms.ToolStripMenuItem();
		this.migcDtStop3 = new System.Windows.Forms.ToolStripMenuItem();
		this.migcDtStop4 = new System.Windows.Forms.ToolStripMenuItem();
		this.ssDevMnt = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslbGcListen = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslbGcStatus = new System.Windows.Forms.ToolStripStatusLabel();
		this.tcLcDevMnt = new IBrainChrom2018.LclTabControl();
		this.tpLC = new System.Windows.Forms.TabPage();
		this.dpLC = new IBrainChrom2018.LclDisplayPanel();
		this.lblcComponent = new IBrainChrom2018.LclLabel();
		this.lblcFlow4 = new IBrainChrom2018.LclLabel();
		this.lblcFlow3 = new IBrainChrom2018.LclLabel();
		this.lblcFlow2 = new IBrainChrom2018.LclLabel();
		this.lblcFlow1 = new IBrainChrom2018.LclLabel();
		this.lblcCpnt1 = new IBrainChrom2018.LclLabel();
		this.lblcTime = new IBrainChrom2018.LclLabel();
		this.lblcInsLoginTimeV = new IBrainChrom2018.LclLabel();
		this.lblcTotalFlowV = new IBrainChrom2018.LclLabel();
		this.lblcCpnt2 = new IBrainChrom2018.LclLabel();
		this.lblcTimeV = new IBrainChrom2018.LclLabel();
		this.lblcInsLoginTime = new IBrainChrom2018.LclLabel();
		this.lblcTotalFlow = new IBrainChrom2018.LclLabel();
		this.lblcFlow = new IBrainChrom2018.LclLabel();
		this.lblcCpnt3 = new IBrainChrom2018.LclLabel();
		this.lblcCpnt4 = new IBrainChrom2018.LclLabel();
		this.tpPump = new System.Windows.Forms.TabPage();
		this.tcPump1 = new IBrainChrom2018.LclTabControl();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.btnMl1 = new IBrainChrom2018.LclButton();
		this.btnPurge1 = new IBrainChrom2018.LclButton();
		this.tbMin1 = new IBrainChrom2018.LclTextBox();
		this.btnMax1 = new IBrainChrom2018.LclButton();
		this.btnSolvent1 = new IBrainChrom2018.LclButton();
		this.tbMax1 = new IBrainChrom2018.LclTextBox();
		this.btnMin1 = new IBrainChrom2018.LclButton();
		this.lbSolvent1 = new IBrainChrom2018.LclLabel();
		this.tbFlow1 = new IBrainChrom2018.LclTextBox();
		this.btnFlow1 = new IBrainChrom2018.LclButton();
		this.btnPump1 = new IBrainChrom2018.LclButton();
		this.lclLabel23 = new IBrainChrom2018.LclLabel();
		this.lbPress1 = new IBrainChrom2018.LclLabel();
		this.lbMl1 = new IBrainChrom2018.LclLabel();
		this.label1 = new System.Windows.Forms.Label();
		this.cbSolvent1 = new System.Windows.Forms.ComboBox();
		this.lbFlow1 = new IBrainChrom2018.LclLabel();
		this.lclLabel20 = new IBrainChrom2018.LclLabel();
		this.lclLabel22 = new IBrainChrom2018.LclLabel();
		this.lclLabel21 = new IBrainChrom2018.LclLabel();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.tbPump1ChkValveCurrent = new IBrainChrom2018.LclTextBox();
		this.tbPump1SealCurrent = new IBrainChrom2018.LclTextBox();
		this.tbPump1ChkValveTotal = new IBrainChrom2018.LclTextBox();
		this.lclLabel18 = new IBrainChrom2018.LclLabel();
		this.lclLabel17 = new IBrainChrom2018.LclLabel();
		this.tbPump1SealTotal = new IBrainChrom2018.LclTextBox();
		this.lclLabel16 = new IBrainChrom2018.LclLabel();
		this.tbPump1PistonCurrent = new IBrainChrom2018.LclTextBox();
		this.lclLabel15 = new IBrainChrom2018.LclLabel();
		this.tbPump1PistonTotal = new IBrainChrom2018.LclTextBox();
		this.lclLabel14 = new IBrainChrom2018.LclLabel();
		this.btnClr6 = new IBrainChrom2018.LclButton();
		this.lclLabel13 = new IBrainChrom2018.LclLabel();
		this.btnClr5 = new IBrainChrom2018.LclButton();
		this.lclLabel12 = new IBrainChrom2018.LclLabel();
		this.btnClr4 = new IBrainChrom2018.LclButton();
		this.lclLabel11 = new IBrainChrom2018.LclLabel();
		this.lclLabel10 = new IBrainChrom2018.LclLabel();
		this.tcPump0 = new IBrainChrom2018.LclTabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.btnMl0 = new IBrainChrom2018.LclButton();
		this.btnPurge0 = new IBrainChrom2018.LclButton();
		this.btnMax0 = new IBrainChrom2018.LclButton();
		this.btnMin0 = new IBrainChrom2018.LclButton();
		this.btnSolvent0 = new IBrainChrom2018.LclButton();
		this.btnFlow0 = new IBrainChrom2018.LclButton();
		this.label8 = new System.Windows.Forms.Label();
		this.lbSolvent0 = new IBrainChrom2018.LclLabel();
		this.lbFlow0 = new IBrainChrom2018.LclLabel();
		this.lclLabel19 = new IBrainChrom2018.LclLabel();
		this.lbMin = new IBrainChrom2018.LclLabel();
		this.lbPress0 = new IBrainChrom2018.LclLabel();
		this.lbPress = new IBrainChrom2018.LclLabel();
		this.lbMl0 = new IBrainChrom2018.LclLabel();
		this.lbMax = new IBrainChrom2018.LclLabel();
		this.cbSolvent0 = new System.Windows.Forms.ComboBox();
		this.btnPump0 = new IBrainChrom2018.LclButton();
		this.tbFlow0 = new IBrainChrom2018.LclTextBox();
		this.tbMin0 = new IBrainChrom2018.LclTextBox();
		this.tbMax0 = new IBrainChrom2018.LclTextBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tbPump0ChkValveCurrent = new IBrainChrom2018.LclTextBox();
		this.lclLabel8 = new IBrainChrom2018.LclLabel();
		this.tbPump0ChkValveTotal = new IBrainChrom2018.LclTextBox();
		this.lbPiston = new IBrainChrom2018.LclLabel();
		this.tbPump0SealCurrent = new IBrainChrom2018.LclTextBox();
		this.lbSeal = new IBrainChrom2018.LclLabel();
		this.tbPump0SealTotal = new IBrainChrom2018.LclTextBox();
		this.lclLabel7 = new IBrainChrom2018.LclLabel();
		this.tbPump0PistonCurrent = new IBrainChrom2018.LclTextBox();
		this.lclLabel4 = new IBrainChrom2018.LclLabel();
		this.tbPump0PistonTotal = new IBrainChrom2018.LclTextBox();
		this.lbChkValve = new IBrainChrom2018.LclLabel();
		this.btnClr3 = new IBrainChrom2018.LclButton();
		this.lclLabel9 = new IBrainChrom2018.LclLabel();
		this.btnClr2 = new IBrainChrom2018.LclButton();
		this.lclLabel5 = new IBrainChrom2018.LclLabel();
		this.btnClr1 = new IBrainChrom2018.LclButton();
		this.lclLabel6 = new IBrainChrom2018.LclLabel();
		this.tpLamp = new System.Windows.Forms.TabPage();
		this.tcLamp0 = new IBrainChrom2018.LclTabControl();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.label25 = new System.Windows.Forms.Label();
		this.label_1 = new System.Windows.Forms.Label();
		this.btnLight0 = new System.Windows.Forms.Button();
		this.label26 = new System.Windows.Forms.Label();
		this.btnRistTime0 = new IBrainChrom2018.LclButton();
		this.tbReferPool0 = new System.Windows.Forms.TextBox();
		this.tbSamplePool0 = new System.Windows.Forms.TextBox();
		this.btnRange0 = new IBrainChrom2018.LclButton();
		this.btnWave0 = new IBrainChrom2018.LclButton();
		this.btnZero0 = new System.Windows.Forms.Button();
		this.lbRistTime0 = new System.Windows.Forms.Label();
		this.lbRange0 = new System.Windows.Forms.Label();
		this.cbRistTime0 = new System.Windows.Forms.ComboBox();
		this.lbWave0 = new System.Windows.Forms.Label();
		this.cbRange0 = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label_0 = new System.Windows.Forms.Label();
		this.tbWave0 = new System.Windows.Forms.TextBox();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.lclButton4 = new IBrainChrom2018.LclButton();
		this.label27 = new System.Windows.Forms.Label();
		this.tbLgtCurrent0 = new System.Windows.Forms.TextBox();
		this.label28 = new System.Windows.Forms.Label();
		this.tbLgtTotal0 = new System.Windows.Forms.TextBox();
		this.tpChkValve = new System.Windows.Forms.TabPage();
		this.lclGroupBox3 = new IBrainChrom2018.LclGroupBox();
		this.lclLabel3 = new IBrainChrom2018.LclLabel();
		this.tbChkValveTotal = new IBrainChrom2018.LclTextBox();
		this.lclButton6 = new IBrainChrom2018.LclButton();
		this.tpColumn = new System.Windows.Forms.TabPage();
		this.lclGroupBox2 = new IBrainChrom2018.LclGroupBox();
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.tbColumnCurrent = new IBrainChrom2018.LclTextBox();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.tbColumnTotal = new IBrainChrom2018.LclTextBox();
		this.lclButton5 = new IBrainChrom2018.LclButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lclButton2 = new IBrainChrom2018.LclButton();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.button1 = new System.Windows.Forms.Button();
		this.tbColumnTemp = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.tpLCItems = new System.Windows.Forms.TabPage();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnStopMl = new IBrainChrom2018.LclButton();
		this.btnWarnMl = new IBrainChrom2018.LclButton();
		this.btnTotalMl = new IBrainChrom2018.LclButton();
		this.cbCapacityCtrl = new System.Windows.Forms.CheckBox();
		this.lbWarnMl = new System.Windows.Forms.Label();
		this.lbStopMl = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.lbTotalMl = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.tbStopMl = new System.Windows.Forms.TextBox();
		this.tbWarnMl = new System.Windows.Forms.TextBox();
		this.tbTotalMl = new System.Windows.Forms.TextBox();
		this.tcGcDevMnt = new IBrainChrom2018.LclTabControl();
		this.tpgcTemp = new System.Windows.Forms.TabPage();
		this.btnCloseCT = new IBrainChrom2018.LclButton();
		this.btnCT6Set = new IBrainChrom2018.LclButton();
		this.btnStartCT = new IBrainChrom2018.LclButton();
		this.btnCT6Qry = new IBrainChrom2018.LclButton();
		this.btnctrlNameSet = new IBrainChrom2018.LclButton();
		this.btnCteSet = new IBrainChrom2018.LclButton();
		this.dgvCT6 = new System.Windows.Forms.DataGridView();
		this.clmCT6CN = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6EN = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6T = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6SetT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6PtcT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6CtrlT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.tpgcParas = new System.Windows.Forms.TabPage();
		this.btnNpSet = new System.Windows.Forms.Button();
		this.btnNpQry = new System.Windows.Forms.Button();
		this.tbinsSerial = new System.Windows.Forms.TextBox();
		this.gvNet = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnHardVersionQry = new System.Windows.Forms.Button();
		this.btninsSerialSet = new System.Windows.Forms.Button();
		this.lbinsSerial = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.gvHardVersion = new System.Windows.Forms.DataGridView();
		this.clmHV = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tpgcDtcs = new System.Windows.Forms.TabPage();
		this.btnAlyStop = new System.Windows.Forms.Button();
		this.btnAlyStart = new System.Windows.Forms.Button();
		this.btnStartFID1 = new System.Windows.Forms.Button();
		this.btnStartFID2 = new System.Windows.Forms.Button();
		this.lclLabel24 = new IBrainChrom2018.LclLabel();
		this.label4 = new System.Windows.Forms.Label();
		this.btnDtcrsQry = new System.Windows.Forms.Button();
		this.gvDtcrs = new System.Windows.Forms.DataGridView();
		this.clmDtMark = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.clmDtPosi = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.clmDtRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmDtBsdct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.clmDtFreq = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.clmDtStart = new System.Windows.Forms.DataGridViewButtonColumn();
		this.clmDtStop = new System.Windows.Forms.DataGridViewButtonColumn();
		this.nudDtcrNum = new System.Windows.Forms.NumericUpDown();
		this.btnDtcrsSet = new System.Windows.Forms.Button();
		this.timer_0 = new System.Windows.Forms.Timer(this.components);
		this.msDevMnt.SuspendLayout();
		this.ssDevMnt.SuspendLayout();
		this.tcLcDevMnt.SuspendLayout();
		this.tpLC.SuspendLayout();
		this.dpLC.SuspendLayout();
		this.tpPump.SuspendLayout();
		this.tcPump1.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.tabPage4.SuspendLayout();
		this.tcPump0.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.tpLamp.SuspendLayout();
		this.tcLamp0.SuspendLayout();
		this.tabPage5.SuspendLayout();
		this.tabPage6.SuspendLayout();
		this.tpChkValve.SuspendLayout();
		this.lclGroupBox3.SuspendLayout();
		this.tpColumn.SuspendLayout();
		this.lclGroupBox2.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.tpLCItems.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tcGcDevMnt.SuspendLayout();
		this.tpgcTemp.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).BeginInit();
		this.tpgcParas.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvNet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvHardVersion).BeginInit();
		this.tpgcDtcs.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvDtcrs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudDtcrNum).BeginInit();
		base.SuspendLayout();
		this.msDevMnt.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miFile, this.miGc });
		this.msDevMnt.Location = new System.Drawing.Point(0, 0);
		this.msDevMnt.Name = "msDevMnt";
		this.msDevMnt.Size = new System.Drawing.Size(867, 25);
		this.msDevMnt.TabIndex = 0;
		this.msDevMnt.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.miFiExit });
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(44, 21);
		this.miFile.Text = "文件";
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(152, 22);
		this.miFiExit.Text = "关闭";
		this.miGc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.migcAlyStart, this.migcAlyStop, this.toolStripSeparator1, this.migcDtStart1, this.migcDtStart2, this.migcDtStart3, this.migcDtStart4, this.toolStripSeparator2, this.migcDtStop1, this.migcDtStop2,
			this.migcDtStop3, this.migcDtStop4
		});
		this.miGc.Name = "miGc";
		this.miGc.Size = new System.Drawing.Size(44, 21);
		this.miGc.Text = "命令";
		this.migcAlyStart.Name = "migcAlyStart";
		this.migcAlyStart.ShortcutKeys = System.Windows.Forms.Keys.F3;
		this.migcAlyStart.Size = new System.Drawing.Size(159, 22);
		this.migcAlyStart.Text = "分析样品";
		this.migcAlyStart.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcAlyStop.Name = "migcAlyStop";
		this.migcAlyStop.ShortcutKeys = System.Windows.Forms.Keys.F4;
		this.migcAlyStop.Size = new System.Drawing.Size(159, 22);
		this.migcAlyStop.Text = "停止分析";
		this.migcAlyStop.Click += new System.EventHandler(migcDtStop4_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
		this.migcDtStart1.Name = "migcDtStart1";
		this.migcDtStart1.ShortcutKeys = System.Windows.Forms.Keys.F5;
		this.migcDtStart1.Size = new System.Drawing.Size(159, 22);
		this.migcDtStart1.Tag = "0";
		this.migcDtStart1.Text = "启动通道1";
		this.migcDtStart1.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcDtStart2.Name = "migcDtStart2";
		this.migcDtStart2.ShortcutKeys = System.Windows.Forms.Keys.F6;
		this.migcDtStart2.Size = new System.Drawing.Size(159, 22);
		this.migcDtStart2.Tag = "1";
		this.migcDtStart2.Text = "启动通道2";
		this.migcDtStart2.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcDtStart3.Name = "migcDtStart3";
		this.migcDtStart3.ShortcutKeys = System.Windows.Forms.Keys.F7;
		this.migcDtStart3.Size = new System.Drawing.Size(159, 22);
		this.migcDtStart3.Tag = "2";
		this.migcDtStart3.Text = "启动通道3";
		this.migcDtStart3.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcDtStart4.Name = "migcDtStart4";
		this.migcDtStart4.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.migcDtStart4.Size = new System.Drawing.Size(159, 22);
		this.migcDtStart4.Tag = "3";
		this.migcDtStart4.Text = "启动通道4";
		this.migcDtStart4.Click += new System.EventHandler(migcDtStop4_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
		this.migcDtStop1.Name = "migcDtStop1";
		this.migcDtStop1.ShortcutKeys = System.Windows.Forms.Keys.F9;
		this.migcDtStop1.Size = new System.Drawing.Size(159, 22);
		this.migcDtStop1.Tag = "0";
		this.migcDtStop1.Text = "停止通道1";
		this.migcDtStop1.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcDtStop2.Name = "migcDtStop2";
		this.migcDtStop2.ShortcutKeys = System.Windows.Forms.Keys.F10;
		this.migcDtStop2.Size = new System.Drawing.Size(159, 22);
		this.migcDtStop2.Tag = "1";
		this.migcDtStop2.Text = "停止通道2";
		this.migcDtStop2.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcDtStop3.Name = "migcDtStop3";
		this.migcDtStop3.ShortcutKeys = System.Windows.Forms.Keys.F11;
		this.migcDtStop3.Size = new System.Drawing.Size(159, 22);
		this.migcDtStop3.Tag = "2";
		this.migcDtStop3.Text = "停止通道3";
		this.migcDtStop3.Click += new System.EventHandler(migcDtStop4_Click);
		this.migcDtStop4.Name = "migcDtStop4";
		this.migcDtStop4.ShortcutKeys = System.Windows.Forms.Keys.F12;
		this.migcDtStop4.Size = new System.Drawing.Size(159, 22);
		this.migcDtStop4.Tag = "3";
		this.migcDtStop4.Text = "停止通道4";
		this.migcDtStop4.Click += new System.EventHandler(migcDtStop4_Click);
		this.ssDevMnt.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.slbExplain, this.tsslbGcListen, this.tsslbGcStatus });
		this.ssDevMnt.Location = new System.Drawing.Point(0, 193);
		this.ssDevMnt.Name = "ssDevMnt";
		this.ssDevMnt.Size = new System.Drawing.Size(867, 22);
		this.ssDevMnt.TabIndex = 1;
		this.ssDevMnt.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(131, 17);
		this.slbExplain.Text = "toolStripStatusLabel1";
		this.tsslbGcListen.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
		this.tsslbGcListen.IsLink = true;
		this.tsslbGcListen.Margin = new System.Windows.Forms.Padding(4, 3, 0, 2);
		this.tsslbGcListen.Name = "tsslbGcListen";
		this.tsslbGcListen.Size = new System.Drawing.Size(131, 17);
		this.tsslbGcListen.Text = "toolStripStatusLabel2";
		this.tsslbGcListen.Click += new System.EventHandler(tsslbGcListen_Click);
		this.tsslbGcStatus.Margin = new System.Windows.Forms.Padding(4, 3, 0, 2);
		this.tsslbGcStatus.Name = "tsslbGcStatus";
		this.tsslbGcStatus.Size = new System.Drawing.Size(131, 17);
		this.tsslbGcStatus.Text = "toolStripStatusLabel1";
		this.tcLcDevMnt.Controls.Add(this.tpLC);
		this.tcLcDevMnt.Controls.Add(this.tpPump);
		this.tcLcDevMnt.Controls.Add(this.tpLamp);
		this.tcLcDevMnt.Controls.Add(this.tpChkValve);
		this.tcLcDevMnt.Controls.Add(this.tpColumn);
		this.tcLcDevMnt.Controls.Add(this.tpLCItems);
		this.tcLcDevMnt.ItemSize = new System.Drawing.Size(90, 19);
		this.tcLcDevMnt.Location = new System.Drawing.Point(490, 28);
		this.tcLcDevMnt.Name = "tcLcDevMnt";
		this.tcLcDevMnt.SelectedIndex = 0;
		this.tcLcDevMnt.Size = new System.Drawing.Size(440, 256);
		this.tcLcDevMnt.TabIndex = 2;
		this.tpLC.Controls.Add(this.dpLC);
		this.tpLC.Location = new System.Drawing.Point(4, 23);
		this.tpLC.Name = "tpLC";
		this.tpLC.Size = new System.Drawing.Size(432, 229);
		this.tpLC.TabIndex = 0;
		this.tpLC.Text = "信息";
		this.tpLC.UseVisualStyleBackColor = true;
		this.dpLC.BackColor = System.Drawing.Color.Transparent;
		this.dpLC.Controls.Add(this.lblcComponent);
		this.dpLC.Controls.Add(this.lblcFlow4);
		this.dpLC.Controls.Add(this.lblcFlow3);
		this.dpLC.Controls.Add(this.lblcFlow2);
		this.dpLC.Controls.Add(this.lblcFlow1);
		this.dpLC.Controls.Add(this.lblcCpnt1);
		this.dpLC.Controls.Add(this.lblcTime);
		this.dpLC.Controls.Add(this.lblcInsLoginTimeV);
		this.dpLC.Controls.Add(this.lblcTotalFlowV);
		this.dpLC.Controls.Add(this.lblcCpnt2);
		this.dpLC.Controls.Add(this.lblcTimeV);
		this.dpLC.Controls.Add(this.lblcInsLoginTime);
		this.dpLC.Controls.Add(this.lblcTotalFlow);
		this.dpLC.Controls.Add(this.lblcFlow);
		this.dpLC.Controls.Add(this.lblcCpnt3);
		this.dpLC.Controls.Add(this.lblcCpnt4);
		this.dpLC.Location = new System.Drawing.Point(7, 3);
		this.dpLC.Name = "dpLC";
		this.dpLC.Size = new System.Drawing.Size(352, 110);
		this.dpLC.TabIndex = 2;
		this.dpLC.Paint += new System.Windows.Forms.PaintEventHandler(dpLC_Paint);
		this.lblcComponent.AutoSize = true;
		this.lblcComponent.BackColor = System.Drawing.Color.Transparent;
		this.lblcComponent.Location = new System.Drawing.Point(12, 10);
		this.lblcComponent.Name = "lblcComponent";
		this.lblcComponent.Size = new System.Drawing.Size(83, 12);
		this.lblcComponent.TabIndex = 1;
		this.lblcComponent.Text = "lblcComponent";
		this.lblcFlow4.BackColor = System.Drawing.Color.Transparent;
		this.lblcFlow4.Location = new System.Drawing.Point(96, 87);
		this.lblcFlow4.Name = "lblcFlow4";
		this.lblcFlow4.Size = new System.Drawing.Size(59, 12);
		this.lblcFlow4.TabIndex = 1;
		this.lblcFlow4.Text = "lblcFlow4";
		this.lblcFlow3.BackColor = System.Drawing.Color.Transparent;
		this.lblcFlow3.Location = new System.Drawing.Point(96, 69);
		this.lblcFlow3.Name = "lblcFlow3";
		this.lblcFlow3.Size = new System.Drawing.Size(59, 12);
		this.lblcFlow3.TabIndex = 1;
		this.lblcFlow3.Text = "lblcFlow3";
		this.lblcFlow2.BackColor = System.Drawing.Color.Transparent;
		this.lblcFlow2.Location = new System.Drawing.Point(96, 52);
		this.lblcFlow2.Name = "lblcFlow2";
		this.lblcFlow2.Size = new System.Drawing.Size(59, 12);
		this.lblcFlow2.TabIndex = 1;
		this.lblcFlow2.Text = "lblcFlow2";
		this.lblcFlow1.BackColor = System.Drawing.Color.Transparent;
		this.lblcFlow1.Location = new System.Drawing.Point(96, 34);
		this.lblcFlow1.Name = "lblcFlow1";
		this.lblcFlow1.Size = new System.Drawing.Size(59, 12);
		this.lblcFlow1.TabIndex = 1;
		this.lblcFlow1.Text = "lblcFlow1";
		this.lblcCpnt1.BackColor = System.Drawing.Color.Transparent;
		this.lblcCpnt1.Location = new System.Drawing.Point(12, 34);
		this.lblcCpnt1.Name = "lblcCpnt1";
		this.lblcCpnt1.Size = new System.Drawing.Size(59, 12);
		this.lblcCpnt1.TabIndex = 1;
		this.lblcCpnt1.Text = "lblcCpnt1";
		this.lblcTime.BackColor = System.Drawing.Color.Transparent;
		this.lblcTime.Location = new System.Drawing.Point(198, 30);
		this.lblcTime.Name = "lblcTime";
		this.lblcTime.Size = new System.Drawing.Size(53, 12);
		this.lblcTime.TabIndex = 1;
		this.lblcTime.Text = "lblcTime";
		this.lblcInsLoginTimeV.BackColor = System.Drawing.Color.Transparent;
		this.lblcInsLoginTimeV.Location = new System.Drawing.Point(283, 52);
		this.lblcInsLoginTimeV.Name = "lblcInsLoginTimeV";
		this.lblcInsLoginTimeV.Size = new System.Drawing.Size(89, 12);
		this.lblcInsLoginTimeV.TabIndex = 1;
		this.lblcInsLoginTimeV.Text = "-";
		this.lblcTotalFlowV.BackColor = System.Drawing.Color.Transparent;
		this.lblcTotalFlowV.Location = new System.Drawing.Point(283, 77);
		this.lblcTotalFlowV.Name = "lblcTotalFlowV";
		this.lblcTotalFlowV.Size = new System.Drawing.Size(89, 12);
		this.lblcTotalFlowV.TabIndex = 1;
		this.lblcTotalFlowV.Text = "-";
		this.lblcCpnt2.BackColor = System.Drawing.Color.Transparent;
		this.lblcCpnt2.Location = new System.Drawing.Point(12, 52);
		this.lblcCpnt2.Name = "lblcCpnt2";
		this.lblcCpnt2.Size = new System.Drawing.Size(59, 12);
		this.lblcCpnt2.TabIndex = 1;
		this.lblcCpnt2.Text = "lblcCpnt2";
		this.lblcTimeV.BackColor = System.Drawing.Color.Transparent;
		this.lblcTimeV.Location = new System.Drawing.Point(283, 30);
		this.lblcTimeV.Name = "lblcTimeV";
		this.lblcTimeV.Size = new System.Drawing.Size(59, 12);
		this.lblcTimeV.TabIndex = 1;
		this.lblcTimeV.Text = "-";
		this.lblcInsLoginTime.BackColor = System.Drawing.Color.Transparent;
		this.lblcInsLoginTime.Location = new System.Drawing.Point(198, 52);
		this.lblcInsLoginTime.Name = "lblcInsLoginTime";
		this.lblcInsLoginTime.Size = new System.Drawing.Size(83, 12);
		this.lblcInsLoginTime.TabIndex = 1;
		this.lblcInsLoginTime.Text = "lblcInsTotalTime";
		this.lblcTotalFlow.BackColor = System.Drawing.Color.Transparent;
		this.lblcTotalFlow.Location = new System.Drawing.Point(198, 77);
		this.lblcTotalFlow.Name = "lblcTotalFlow";
		this.lblcTotalFlow.Size = new System.Drawing.Size(83, 12);
		this.lblcTotalFlow.TabIndex = 1;
		this.lblcTotalFlow.Text = "lblcTotalFlow";
		this.lblcFlow.AutoSize = true;
		this.lblcFlow.BackColor = System.Drawing.Color.Transparent;
		this.lblcFlow.Location = new System.Drawing.Point(96, 10);
		this.lblcFlow.Name = "lblcFlow";
		this.lblcFlow.Size = new System.Drawing.Size(53, 12);
		this.lblcFlow.TabIndex = 1;
		this.lblcFlow.Text = "lblcFlow";
		this.lblcCpnt3.BackColor = System.Drawing.Color.Transparent;
		this.lblcCpnt3.Location = new System.Drawing.Point(12, 69);
		this.lblcCpnt3.Name = "lblcCpnt3";
		this.lblcCpnt3.Size = new System.Drawing.Size(59, 12);
		this.lblcCpnt3.TabIndex = 1;
		this.lblcCpnt3.Text = "lblcCpnt3";
		this.lblcCpnt4.BackColor = System.Drawing.Color.Transparent;
		this.lblcCpnt4.Location = new System.Drawing.Point(12, 87);
		this.lblcCpnt4.Name = "lblcCpnt4";
		this.lblcCpnt4.Size = new System.Drawing.Size(59, 12);
		this.lblcCpnt4.TabIndex = 1;
		this.lblcCpnt4.Text = "lblcCpnt4";
		this.tpPump.AutoScroll = true;
		this.tpPump.Controls.Add(this.tcPump1);
		this.tpPump.Controls.Add(this.tcPump0);
		this.tpPump.Location = new System.Drawing.Point(4, 23);
		this.tpPump.Name = "tpPump";
		this.tpPump.Size = new System.Drawing.Size(432, 229);
		this.tpPump.TabIndex = 2;
		this.tpPump.Text = "泵";
		this.tpPump.UseVisualStyleBackColor = true;
		this.tcPump1.Controls.Add(this.tabPage3);
		this.tcPump1.Controls.Add(this.tabPage4);
		this.tcPump1.ItemSize = new System.Drawing.Size(90, 19);
		this.tcPump1.Location = new System.Drawing.Point(3, 114);
		this.tcPump1.Name = "tcPump1";
		this.tcPump1.SelectedIndex = 0;
		this.tcPump1.Size = new System.Drawing.Size(346, 109);
		this.tcPump1.TabIndex = 3;
		this.tabPage3.Controls.Add(this.btnMl1);
		this.tabPage3.Controls.Add(this.btnPurge1);
		this.tabPage3.Controls.Add(this.tbMin1);
		this.tabPage3.Controls.Add(this.btnMax1);
		this.tabPage3.Controls.Add(this.btnSolvent1);
		this.tabPage3.Controls.Add(this.tbMax1);
		this.tabPage3.Controls.Add(this.btnMin1);
		this.tabPage3.Controls.Add(this.lbSolvent1);
		this.tabPage3.Controls.Add(this.tbFlow1);
		this.tabPage3.Controls.Add(this.btnFlow1);
		this.tabPage3.Controls.Add(this.btnPump1);
		this.tabPage3.Controls.Add(this.lclLabel23);
		this.tabPage3.Controls.Add(this.lbPress1);
		this.tabPage3.Controls.Add(this.lbMl1);
		this.tabPage3.Controls.Add(this.label1);
		this.tabPage3.Controls.Add(this.cbSolvent1);
		this.tabPage3.Controls.Add(this.lbFlow1);
		this.tabPage3.Controls.Add(this.lclLabel20);
		this.tabPage3.Controls.Add(this.lclLabel22);
		this.tabPage3.Controls.Add(this.lclLabel21);
		this.tabPage3.Location = new System.Drawing.Point(4, 23);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(338, 82);
		this.tabPage3.TabIndex = 0;
		this.tabPage3.Text = "tabPage3";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.btnMl1.Location = new System.Drawing.Point(303, 30);
		this.btnMl1.Name = "btnMl1";
		this.btnMl1.Size = new System.Drawing.Size(31, 23);
		this.btnMl1.TabIndex = 30;
		this.btnMl1.Text = "X";
		this.btnMl1.UseVisualStyleBackColor = true;
		this.btnMl1.Click += new System.EventHandler(btnMl0_Click);
		this.btnPurge1.Location = new System.Drawing.Point(196, 31);
		this.btnPurge1.Name = "btnPurge1";
		this.btnPurge1.Size = new System.Drawing.Size(43, 23);
		this.btnPurge1.TabIndex = 29;
		this.btnPurge1.Text = "Purge";
		this.btnPurge1.UseVisualStyleBackColor = true;
		this.btnPurge1.Click += new System.EventHandler(btnPurge0_Click);
		this.tbMin1.Location = new System.Drawing.Point(158, 6);
		this.tbMin1.Name = "tbMin1";
		this.tbMin1.Size = new System.Drawing.Size(35, 21);
		this.tbMin1.TabIndex = 25;
		this.tbMin1.Text = "20";
		this.tbMin1.KeyDown += new System.Windows.Forms.KeyEventHandler(tbMin0_KeyDown);
		this.btnMax1.Location = new System.Drawing.Point(303, 6);
		this.btnMax1.Name = "btnMax1";
		this.btnMax1.Size = new System.Drawing.Size(31, 21);
		this.btnMax1.TabIndex = 28;
		this.btnMax1.Text = "√";
		this.btnMax1.UseVisualStyleBackColor = true;
		this.btnMax1.Click += new System.EventHandler(btnMax0_Click);
		this.btnSolvent1.Location = new System.Drawing.Point(197, 56);
		this.btnSolvent1.Name = "btnSolvent1";
		this.btnSolvent1.Size = new System.Drawing.Size(31, 21);
		this.btnSolvent1.TabIndex = 28;
		this.btnSolvent1.Text = "√";
		this.btnSolvent1.UseVisualStyleBackColor = true;
		this.btnSolvent1.Click += new System.EventHandler(btnSolvent0_Click);
		this.tbMax1.Location = new System.Drawing.Point(263, 6);
		this.tbMax1.Name = "tbMax1";
		this.tbMax1.Size = new System.Drawing.Size(35, 21);
		this.tbMax1.TabIndex = 24;
		this.tbMax1.Text = "25";
		this.tbMax1.KeyDown += new System.Windows.Forms.KeyEventHandler(tbMax0_KeyDown);
		this.btnMin1.Location = new System.Drawing.Point(197, 7);
		this.btnMin1.Name = "btnMin1";
		this.btnMin1.Size = new System.Drawing.Size(31, 21);
		this.btnMin1.TabIndex = 28;
		this.btnMin1.Text = "√";
		this.btnMin1.UseVisualStyleBackColor = true;
		this.btnMin1.Click += new System.EventHandler(btnMin0_Click);
		this.lbSolvent1.AutoSize = true;
		this.lbSolvent1.Location = new System.Drawing.Point(82, 60);
		this.lbSolvent1.Name = "lbSolvent1";
		this.lbSolvent1.Size = new System.Drawing.Size(11, 12);
		this.lbSolvent1.TabIndex = 23;
		this.lbSolvent1.Text = "1";
		this.tbFlow1.Location = new System.Drawing.Point(123, 31);
		this.tbFlow1.Name = "tbFlow1";
		this.tbFlow1.Size = new System.Drawing.Size(35, 21);
		this.tbFlow1.TabIndex = 25;
		this.tbFlow1.Text = "1";
		this.tbFlow1.KeyDown += new System.Windows.Forms.KeyEventHandler(tbFlow0_KeyDown);
		this.btnFlow1.Location = new System.Drawing.Point(162, 31);
		this.btnFlow1.Name = "btnFlow1";
		this.btnFlow1.Size = new System.Drawing.Size(31, 21);
		this.btnFlow1.TabIndex = 28;
		this.btnFlow1.Text = "√";
		this.btnFlow1.UseVisualStyleBackColor = true;
		this.btnFlow1.Click += new System.EventHandler(btnFlow0_Click);
		this.btnPump1.Location = new System.Drawing.Point(263, 55);
		this.btnPump1.Name = "btnPump1";
		this.btnPump1.Size = new System.Drawing.Size(71, 23);
		this.btnPump1.TabIndex = 0;
		this.btnPump1.Text = "btnlcStopFlow";
		this.btnPump1.UseVisualStyleBackColor = true;
		this.btnPump1.Click += new System.EventHandler(btnPump0_Click);
		this.lclLabel23.AutoSize = true;
		this.lclLabel23.Location = new System.Drawing.Point(122, 11);
		this.lclLabel23.Name = "lclLabel23";
		this.lclLabel23.Size = new System.Drawing.Size(23, 12);
		this.lclLabel23.TabIndex = 23;
		this.lclLabel23.Text = "min";
		this.lbPress1.AutoSize = true;
		this.lbPress1.Location = new System.Drawing.Point(82, 11);
		this.lbPress1.Name = "lbPress1";
		this.lbPress1.Size = new System.Drawing.Size(17, 12);
		this.lbPress1.TabIndex = 23;
		this.lbPress1.Text = "20";
		this.lbMl1.AutoSize = true;
		this.lbMl1.Location = new System.Drawing.Point(246, 36);
		this.lbMl1.Name = "lbMl1";
		this.lbMl1.Size = new System.Drawing.Size(11, 12);
		this.lbMl1.TabIndex = 20;
		this.lbMl1.Text = "0";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 60);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 27;
		this.label1.Text = "溶剂sfda";
		this.cbSolvent1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbSolvent1.FormattingEnabled = true;
		this.cbSolvent1.Items.AddRange(new object[16]
		{
			"H20", "IPA", "Aceto", "MeOH", "THF", "STD", "PPNL2", "AGN", "NHPTN", "BTNL2",
			"ISBTL", "BTNL1", "BTLAC", "DMTNS", "ACN", "S16-S29"
		});
		this.cbSolvent1.Location = new System.Drawing.Point(123, 56);
		this.cbSolvent1.Name = "cbSolvent1";
		this.cbSolvent1.Size = new System.Drawing.Size(70, 20);
		this.cbSolvent1.TabIndex = 26;
		this.lbFlow1.AutoSize = true;
		this.lbFlow1.Location = new System.Drawing.Point(82, 36);
		this.lbFlow1.Name = "lbFlow1";
		this.lbFlow1.Size = new System.Drawing.Size(11, 12);
		this.lbFlow1.TabIndex = 23;
		this.lbFlow1.Text = "1";
		this.lclLabel20.AutoSize = true;
		this.lclLabel20.Location = new System.Drawing.Point(234, 11);
		this.lclLabel20.Name = "lclLabel20";
		this.lclLabel20.Size = new System.Drawing.Size(23, 12);
		this.lclLabel20.TabIndex = 20;
		this.lclLabel20.Text = "max";
		this.lclLabel22.AutoSize = true;
		this.lclLabel22.Location = new System.Drawing.Point(3, 36);
		this.lclLabel22.Name = "lclLabel22";
		this.lclLabel22.Size = new System.Drawing.Size(59, 12);
		this.lclLabel22.TabIndex = 23;
		this.lclLabel22.Text = "lclLabel1";
		this.lclLabel21.AutoSize = true;
		this.lclLabel21.Location = new System.Drawing.Point(3, 11);
		this.lclLabel21.Name = "lclLabel21";
		this.lclLabel21.Size = new System.Drawing.Size(59, 12);
		this.lclLabel21.TabIndex = 23;
		this.lclLabel21.Text = "lclLabel1";
		this.tabPage4.Controls.Add(this.tbPump1ChkValveCurrent);
		this.tabPage4.Controls.Add(this.tbPump1SealCurrent);
		this.tabPage4.Controls.Add(this.tbPump1ChkValveTotal);
		this.tabPage4.Controls.Add(this.lclLabel18);
		this.tabPage4.Controls.Add(this.lclLabel17);
		this.tabPage4.Controls.Add(this.tbPump1SealTotal);
		this.tabPage4.Controls.Add(this.lclLabel16);
		this.tabPage4.Controls.Add(this.tbPump1PistonCurrent);
		this.tabPage4.Controls.Add(this.lclLabel15);
		this.tabPage4.Controls.Add(this.tbPump1PistonTotal);
		this.tabPage4.Controls.Add(this.lclLabel14);
		this.tabPage4.Controls.Add(this.btnClr6);
		this.tabPage4.Controls.Add(this.lclLabel13);
		this.tabPage4.Controls.Add(this.btnClr5);
		this.tabPage4.Controls.Add(this.lclLabel12);
		this.tabPage4.Controls.Add(this.btnClr4);
		this.tabPage4.Controls.Add(this.lclLabel11);
		this.tabPage4.Controls.Add(this.lclLabel10);
		this.tabPage4.Location = new System.Drawing.Point(4, 23);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Size = new System.Drawing.Size(338, 82);
		this.tabPage4.TabIndex = 1;
		this.tabPage4.Text = "tabPage4";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.tbPump1ChkValveCurrent.Location = new System.Drawing.Point(98, 56);
		this.tbPump1ChkValveCurrent.Name = "tbPump1ChkValveCurrent";
		this.tbPump1ChkValveCurrent.Size = new System.Drawing.Size(62, 21);
		this.tbPump1ChkValveCurrent.TabIndex = 2;
		this.tbPump1SealCurrent.Location = new System.Drawing.Point(98, 31);
		this.tbPump1SealCurrent.Name = "tbPump1SealCurrent";
		this.tbPump1SealCurrent.Size = new System.Drawing.Size(62, 21);
		this.tbPump1SealCurrent.TabIndex = 2;
		this.tbPump1ChkValveTotal.Location = new System.Drawing.Point(202, 56);
		this.tbPump1ChkValveTotal.Name = "tbPump1ChkValveTotal";
		this.tbPump1ChkValveTotal.Size = new System.Drawing.Size(67, 21);
		this.tbPump1ChkValveTotal.TabIndex = 2;
		this.lclLabel18.AutoSize = true;
		this.lclLabel18.Location = new System.Drawing.Point(5, 9);
		this.lclLabel18.Name = "lclLabel18";
		this.lclLabel18.Size = new System.Drawing.Size(59, 12);
		this.lclLabel18.TabIndex = 0;
		this.lclLabel18.Text = "lclLabel1";
		this.lclLabel17.AutoSize = true;
		this.lclLabel17.Location = new System.Drawing.Point(5, 35);
		this.lclLabel17.Name = "lclLabel17";
		this.lclLabel17.Size = new System.Drawing.Size(59, 12);
		this.lclLabel17.TabIndex = 0;
		this.lclLabel17.Text = "lclLabel1";
		this.tbPump1SealTotal.Location = new System.Drawing.Point(202, 31);
		this.tbPump1SealTotal.Name = "tbPump1SealTotal";
		this.tbPump1SealTotal.Size = new System.Drawing.Size(67, 21);
		this.tbPump1SealTotal.TabIndex = 2;
		this.lclLabel16.AutoSize = true;
		this.lclLabel16.Location = new System.Drawing.Point(57, 9);
		this.lclLabel16.Name = "lclLabel16";
		this.lclLabel16.Size = new System.Drawing.Size(59, 12);
		this.lclLabel16.TabIndex = 0;
		this.lclLabel16.Text = "lclLabel1";
		this.lclLabel16.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.tbPump1PistonCurrent.Location = new System.Drawing.Point(98, 6);
		this.tbPump1PistonCurrent.Name = "tbPump1PistonCurrent";
		this.tbPump1PistonCurrent.Size = new System.Drawing.Size(62, 21);
		this.tbPump1PistonCurrent.TabIndex = 2;
		this.lclLabel15.AutoSize = true;
		this.lclLabel15.Location = new System.Drawing.Point(166, 9);
		this.lclLabel15.Name = "lclLabel15";
		this.lclLabel15.Size = new System.Drawing.Size(59, 12);
		this.lclLabel15.TabIndex = 0;
		this.lclLabel15.Text = "lclLabel1";
		this.tbPump1PistonTotal.Location = new System.Drawing.Point(202, 6);
		this.tbPump1PistonTotal.Name = "tbPump1PistonTotal";
		this.tbPump1PistonTotal.Size = new System.Drawing.Size(67, 21);
		this.tbPump1PistonTotal.TabIndex = 2;
		this.lclLabel14.AutoSize = true;
		this.lclLabel14.Location = new System.Drawing.Point(57, 35);
		this.lclLabel14.Name = "lclLabel14";
		this.lclLabel14.Size = new System.Drawing.Size(59, 12);
		this.lclLabel14.TabIndex = 0;
		this.lclLabel14.Text = "lclLabel1";
		this.lclLabel14.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.btnClr6.Location = new System.Drawing.Point(274, 57);
		this.btnClr6.Name = "btnClr6";
		this.btnClr6.Size = new System.Drawing.Size(30, 18);
		this.btnClr6.TabIndex = 1;
		this.btnClr6.Text = "X";
		this.btnClr6.UseVisualStyleBackColor = true;
		this.btnClr6.Click += new System.EventHandler(btnClr6_Click);
		this.lclLabel13.AutoSize = true;
		this.lclLabel13.Location = new System.Drawing.Point(5, 59);
		this.lclLabel13.Name = "lclLabel13";
		this.lclLabel13.Size = new System.Drawing.Size(59, 12);
		this.lclLabel13.TabIndex = 0;
		this.lclLabel13.Text = "lclLabel1";
		this.btnClr5.Location = new System.Drawing.Point(274, 32);
		this.btnClr5.Name = "btnClr5";
		this.btnClr5.Size = new System.Drawing.Size(30, 18);
		this.btnClr5.TabIndex = 1;
		this.btnClr5.Text = "X";
		this.btnClr5.UseVisualStyleBackColor = true;
		this.btnClr5.Click += new System.EventHandler(btnClr5_Click);
		this.lclLabel12.AutoSize = true;
		this.lclLabel12.Location = new System.Drawing.Point(57, 59);
		this.lclLabel12.Name = "lclLabel12";
		this.lclLabel12.Size = new System.Drawing.Size(59, 12);
		this.lclLabel12.TabIndex = 0;
		this.lclLabel12.Text = "lclLabel1";
		this.lclLabel12.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.btnClr4.Location = new System.Drawing.Point(274, 7);
		this.btnClr4.Name = "btnClr4";
		this.btnClr4.Size = new System.Drawing.Size(30, 18);
		this.btnClr4.TabIndex = 1;
		this.btnClr4.Text = "X";
		this.btnClr4.UseVisualStyleBackColor = true;
		this.btnClr4.Click += new System.EventHandler(btnClr4_Click);
		this.lclLabel11.AutoSize = true;
		this.lclLabel11.Location = new System.Drawing.Point(166, 35);
		this.lclLabel11.Name = "lclLabel11";
		this.lclLabel11.Size = new System.Drawing.Size(59, 12);
		this.lclLabel11.TabIndex = 0;
		this.lclLabel11.Text = "lclLabel1";
		this.lclLabel10.AutoSize = true;
		this.lclLabel10.Location = new System.Drawing.Point(166, 59);
		this.lclLabel10.Name = "lclLabel10";
		this.lclLabel10.Size = new System.Drawing.Size(59, 12);
		this.lclLabel10.TabIndex = 0;
		this.lclLabel10.Text = "lclLabel1";
		this.tcPump0.Controls.Add(this.tabPage1);
		this.tcPump0.Controls.Add(this.tabPage2);
		this.tcPump0.ItemSize = new System.Drawing.Size(90, 19);
		this.tcPump0.Location = new System.Drawing.Point(3, 3);
		this.tcPump0.Name = "tcPump0";
		this.tcPump0.SelectedIndex = 0;
		this.tcPump0.Size = new System.Drawing.Size(346, 109);
		this.tcPump0.TabIndex = 2;
		this.tabPage1.Controls.Add(this.btnMl0);
		this.tabPage1.Controls.Add(this.btnPurge0);
		this.tabPage1.Controls.Add(this.btnMax0);
		this.tabPage1.Controls.Add(this.btnMin0);
		this.tabPage1.Controls.Add(this.btnSolvent0);
		this.tabPage1.Controls.Add(this.btnFlow0);
		this.tabPage1.Controls.Add(this.label8);
		this.tabPage1.Controls.Add(this.lbSolvent0);
		this.tabPage1.Controls.Add(this.lbFlow0);
		this.tabPage1.Controls.Add(this.lclLabel19);
		this.tabPage1.Controls.Add(this.lbMin);
		this.tabPage1.Controls.Add(this.lbPress0);
		this.tabPage1.Controls.Add(this.lbPress);
		this.tabPage1.Controls.Add(this.lbMl0);
		this.tabPage1.Controls.Add(this.lbMax);
		this.tabPage1.Controls.Add(this.cbSolvent0);
		this.tabPage1.Controls.Add(this.btnPump0);
		this.tabPage1.Controls.Add(this.tbFlow0);
		this.tabPage1.Controls.Add(this.tbMin0);
		this.tabPage1.Controls.Add(this.tbMax0);
		this.tabPage1.Location = new System.Drawing.Point(4, 23);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(338, 82);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "tabPage1";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.btnMl0.Location = new System.Drawing.Point(303, 30);
		this.btnMl0.Name = "btnMl0";
		this.btnMl0.Size = new System.Drawing.Size(31, 23);
		this.btnMl0.TabIndex = 30;
		this.btnMl0.Text = "X";
		this.btnMl0.UseVisualStyleBackColor = true;
		this.btnMl0.Click += new System.EventHandler(btnMl0_Click);
		this.btnPurge0.BackColor = System.Drawing.Color.Transparent;
		this.btnPurge0.Location = new System.Drawing.Point(196, 30);
		this.btnPurge0.Name = "btnPurge0";
		this.btnPurge0.Size = new System.Drawing.Size(43, 23);
		this.btnPurge0.TabIndex = 29;
		this.btnPurge0.Text = "冲洗";
		this.btnPurge0.UseVisualStyleBackColor = false;
		this.btnPurge0.Click += new System.EventHandler(btnPurge0_Click);
		this.btnMax0.Location = new System.Drawing.Point(303, 6);
		this.btnMax0.Name = "btnMax0";
		this.btnMax0.Size = new System.Drawing.Size(31, 21);
		this.btnMax0.TabIndex = 28;
		this.btnMax0.Text = "√";
		this.btnMax0.UseVisualStyleBackColor = true;
		this.btnMax0.Click += new System.EventHandler(btnMax0_Click);
		this.btnMin0.Location = new System.Drawing.Point(196, 6);
		this.btnMin0.Name = "btnMin0";
		this.btnMin0.Size = new System.Drawing.Size(31, 21);
		this.btnMin0.TabIndex = 28;
		this.btnMin0.Text = "√";
		this.btnMin0.UseVisualStyleBackColor = true;
		this.btnMin0.Click += new System.EventHandler(btnMin0_Click);
		this.btnSolvent0.Location = new System.Drawing.Point(196, 56);
		this.btnSolvent0.Name = "btnSolvent0";
		this.btnSolvent0.Size = new System.Drawing.Size(31, 21);
		this.btnSolvent0.TabIndex = 28;
		this.btnSolvent0.Text = "√";
		this.btnSolvent0.UseVisualStyleBackColor = true;
		this.btnSolvent0.Click += new System.EventHandler(btnSolvent0_Click);
		this.btnFlow0.Location = new System.Drawing.Point(162, 31);
		this.btnFlow0.Name = "btnFlow0";
		this.btnFlow0.Size = new System.Drawing.Size(31, 21);
		this.btnFlow0.TabIndex = 28;
		this.btnFlow0.Text = "√";
		this.btnFlow0.UseVisualStyleBackColor = true;
		this.btnFlow0.Click += new System.EventHandler(btnFlow0_Click);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(3, 60);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(53, 12);
		this.label8.TabIndex = 27;
		this.label8.Text = "溶剂sfda";
		this.lbSolvent0.AutoSize = true;
		this.lbSolvent0.Location = new System.Drawing.Point(82, 60);
		this.lbSolvent0.Name = "lbSolvent0";
		this.lbSolvent0.Size = new System.Drawing.Size(11, 12);
		this.lbSolvent0.TabIndex = 23;
		this.lbSolvent0.Text = "1";
		this.lbFlow0.AutoSize = true;
		this.lbFlow0.Location = new System.Drawing.Point(82, 36);
		this.lbFlow0.Name = "lbFlow0";
		this.lbFlow0.Size = new System.Drawing.Size(11, 12);
		this.lbFlow0.TabIndex = 23;
		this.lbFlow0.Text = "1";
		this.lclLabel19.AutoSize = true;
		this.lclLabel19.Location = new System.Drawing.Point(3, 36);
		this.lclLabel19.Name = "lclLabel19";
		this.lclLabel19.Size = new System.Drawing.Size(59, 12);
		this.lclLabel19.TabIndex = 23;
		this.lclLabel19.Text = "lclLabel1";
		this.lbMin.AutoSize = true;
		this.lbMin.Location = new System.Drawing.Point(121, 11);
		this.lbMin.Name = "lbMin";
		this.lbMin.Size = new System.Drawing.Size(23, 12);
		this.lbMin.TabIndex = 23;
		this.lbMin.Text = "min";
		this.lbPress0.AutoSize = true;
		this.lbPress0.Location = new System.Drawing.Point(82, 11);
		this.lbPress0.Name = "lbPress0";
		this.lbPress0.Size = new System.Drawing.Size(17, 12);
		this.lbPress0.TabIndex = 23;
		this.lbPress0.Text = "20";
		this.lbPress.AutoSize = true;
		this.lbPress.Location = new System.Drawing.Point(3, 11);
		this.lbPress.Name = "lbPress";
		this.lbPress.Size = new System.Drawing.Size(59, 12);
		this.lbPress.TabIndex = 23;
		this.lbPress.Text = "lclLabel1";
		this.lbMl0.AutoSize = true;
		this.lbMl0.Location = new System.Drawing.Point(246, 36);
		this.lbMl0.Name = "lbMl0";
		this.lbMl0.Size = new System.Drawing.Size(11, 12);
		this.lbMl0.TabIndex = 20;
		this.lbMl0.Text = "0";
		this.lbMax.AutoSize = true;
		this.lbMax.Location = new System.Drawing.Point(233, 11);
		this.lbMax.Name = "lbMax";
		this.lbMax.Size = new System.Drawing.Size(23, 12);
		this.lbMax.TabIndex = 20;
		this.lbMax.Text = "max";
		this.cbSolvent0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbSolvent0.FormattingEnabled = true;
		this.cbSolvent0.Items.AddRange(new object[16]
		{
			"H20", "IPA", "Aceto", "MeOH", "THF", "STD", "PPNL2", "AGN", "NHPTN", "BTNL2",
			"ISBTL", "BTNL1", "BTLAC", "DMTNS", "ACN", "S16-S29"
		});
		this.cbSolvent0.Location = new System.Drawing.Point(123, 56);
		this.cbSolvent0.Name = "cbSolvent0";
		this.cbSolvent0.Size = new System.Drawing.Size(70, 20);
		this.cbSolvent0.TabIndex = 26;
		this.btnPump0.Location = new System.Drawing.Point(263, 55);
		this.btnPump0.Name = "btnPump0";
		this.btnPump0.Size = new System.Drawing.Size(71, 23);
		this.btnPump0.TabIndex = 0;
		this.btnPump0.Text = "btnlcStopFlow";
		this.btnPump0.UseVisualStyleBackColor = true;
		this.btnPump0.Click += new System.EventHandler(btnPump0_Click);
		this.tbFlow0.Location = new System.Drawing.Point(123, 31);
		this.tbFlow0.Name = "tbFlow0";
		this.tbFlow0.Size = new System.Drawing.Size(35, 21);
		this.tbFlow0.TabIndex = 25;
		this.tbFlow0.Text = "1";
		this.tbFlow0.KeyDown += new System.Windows.Forms.KeyEventHandler(tbFlow0_KeyDown);
		this.tbMin0.Location = new System.Drawing.Point(157, 6);
		this.tbMin0.Name = "tbMin0";
		this.tbMin0.Size = new System.Drawing.Size(35, 21);
		this.tbMin0.TabIndex = 25;
		this.tbMin0.Text = "20";
		this.tbMin0.KeyDown += new System.Windows.Forms.KeyEventHandler(tbMin0_KeyDown);
		this.tbMax0.Location = new System.Drawing.Point(263, 6);
		this.tbMax0.Name = "tbMax0";
		this.tbMax0.Size = new System.Drawing.Size(35, 21);
		this.tbMax0.TabIndex = 24;
		this.tbMax0.Text = "25";
		this.tbMax0.KeyDown += new System.Windows.Forms.KeyEventHandler(tbMax0_KeyDown);
		this.tabPage2.Controls.Add(this.tbPump0ChkValveCurrent);
		this.tabPage2.Controls.Add(this.lclLabel8);
		this.tabPage2.Controls.Add(this.tbPump0ChkValveTotal);
		this.tabPage2.Controls.Add(this.lbPiston);
		this.tabPage2.Controls.Add(this.tbPump0SealCurrent);
		this.tabPage2.Controls.Add(this.lbSeal);
		this.tabPage2.Controls.Add(this.tbPump0SealTotal);
		this.tabPage2.Controls.Add(this.lclLabel7);
		this.tabPage2.Controls.Add(this.tbPump0PistonCurrent);
		this.tabPage2.Controls.Add(this.lclLabel4);
		this.tabPage2.Controls.Add(this.tbPump0PistonTotal);
		this.tabPage2.Controls.Add(this.lbChkValve);
		this.tabPage2.Controls.Add(this.btnClr3);
		this.tabPage2.Controls.Add(this.lclLabel9);
		this.tabPage2.Controls.Add(this.btnClr2);
		this.tabPage2.Controls.Add(this.lclLabel5);
		this.tabPage2.Controls.Add(this.btnClr1);
		this.tabPage2.Controls.Add(this.lclLabel6);
		this.tabPage2.Location = new System.Drawing.Point(4, 23);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Size = new System.Drawing.Size(338, 82);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "tabPage2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.tbPump0ChkValveCurrent.Location = new System.Drawing.Point(98, 56);
		this.tbPump0ChkValveCurrent.Name = "tbPump0ChkValveCurrent";
		this.tbPump0ChkValveCurrent.Size = new System.Drawing.Size(62, 21);
		this.tbPump0ChkValveCurrent.TabIndex = 2;
		this.lclLabel8.AutoSize = true;
		this.lclLabel8.Location = new System.Drawing.Point(57, 35);
		this.lclLabel8.Name = "lclLabel8";
		this.lclLabel8.Size = new System.Drawing.Size(59, 12);
		this.lclLabel8.TabIndex = 0;
		this.lclLabel8.Text = "lclLabel1";
		this.lclLabel8.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.tbPump0ChkValveTotal.Location = new System.Drawing.Point(202, 56);
		this.tbPump0ChkValveTotal.Name = "tbPump0ChkValveTotal";
		this.tbPump0ChkValveTotal.Size = new System.Drawing.Size(67, 21);
		this.tbPump0ChkValveTotal.TabIndex = 2;
		this.lbPiston.AutoSize = true;
		this.lbPiston.Location = new System.Drawing.Point(5, 9);
		this.lbPiston.Name = "lbPiston";
		this.lbPiston.Size = new System.Drawing.Size(59, 12);
		this.lbPiston.TabIndex = 0;
		this.lbPiston.Text = "lclLabel1";
		this.tbPump0SealCurrent.Location = new System.Drawing.Point(98, 31);
		this.tbPump0SealCurrent.Name = "tbPump0SealCurrent";
		this.tbPump0SealCurrent.Size = new System.Drawing.Size(62, 21);
		this.tbPump0SealCurrent.TabIndex = 2;
		this.lbSeal.AutoSize = true;
		this.lbSeal.Location = new System.Drawing.Point(5, 35);
		this.lbSeal.Name = "lbSeal";
		this.lbSeal.Size = new System.Drawing.Size(59, 12);
		this.lbSeal.TabIndex = 0;
		this.lbSeal.Text = "lclLabel1";
		this.tbPump0SealTotal.Location = new System.Drawing.Point(202, 31);
		this.tbPump0SealTotal.Name = "tbPump0SealTotal";
		this.tbPump0SealTotal.Size = new System.Drawing.Size(67, 21);
		this.tbPump0SealTotal.TabIndex = 2;
		this.lclLabel7.AutoSize = true;
		this.lclLabel7.Location = new System.Drawing.Point(57, 9);
		this.lclLabel7.Name = "lclLabel7";
		this.lclLabel7.Size = new System.Drawing.Size(59, 12);
		this.lclLabel7.TabIndex = 0;
		this.lclLabel7.Text = "lclLabel1";
		this.lclLabel7.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.tbPump0PistonCurrent.Location = new System.Drawing.Point(98, 6);
		this.tbPump0PistonCurrent.Name = "tbPump0PistonCurrent";
		this.tbPump0PistonCurrent.Size = new System.Drawing.Size(62, 21);
		this.tbPump0PistonCurrent.TabIndex = 2;
		this.lclLabel4.AutoSize = true;
		this.lclLabel4.Location = new System.Drawing.Point(166, 9);
		this.lclLabel4.Name = "lclLabel4";
		this.lclLabel4.Size = new System.Drawing.Size(59, 12);
		this.lclLabel4.TabIndex = 0;
		this.lclLabel4.Text = "lclLabel1";
		this.tbPump0PistonTotal.Location = new System.Drawing.Point(202, 6);
		this.tbPump0PistonTotal.Name = "tbPump0PistonTotal";
		this.tbPump0PistonTotal.Size = new System.Drawing.Size(67, 21);
		this.tbPump0PistonTotal.TabIndex = 2;
		this.lbChkValve.AutoSize = true;
		this.lbChkValve.Location = new System.Drawing.Point(5, 59);
		this.lbChkValve.Name = "lbChkValve";
		this.lbChkValve.Size = new System.Drawing.Size(59, 12);
		this.lbChkValve.TabIndex = 0;
		this.lbChkValve.Text = "lclLabel1";
		this.btnClr3.Location = new System.Drawing.Point(274, 58);
		this.btnClr3.Name = "btnClr3";
		this.btnClr3.Size = new System.Drawing.Size(30, 18);
		this.btnClr3.TabIndex = 1;
		this.btnClr3.Text = "X";
		this.btnClr3.UseVisualStyleBackColor = true;
		this.btnClr3.Click += new System.EventHandler(btnClr3_Click);
		this.lclLabel9.AutoSize = true;
		this.lclLabel9.Location = new System.Drawing.Point(57, 59);
		this.lclLabel9.Name = "lclLabel9";
		this.lclLabel9.Size = new System.Drawing.Size(59, 12);
		this.lclLabel9.TabIndex = 0;
		this.lclLabel9.Text = "lclLabel1";
		this.lclLabel9.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.btnClr2.Location = new System.Drawing.Point(274, 33);
		this.btnClr2.Name = "btnClr2";
		this.btnClr2.Size = new System.Drawing.Size(30, 18);
		this.btnClr2.TabIndex = 1;
		this.btnClr2.Text = "X";
		this.btnClr2.UseVisualStyleBackColor = true;
		this.btnClr2.Click += new System.EventHandler(btnClr2_Click);
		this.lclLabel5.AutoSize = true;
		this.lclLabel5.Location = new System.Drawing.Point(166, 35);
		this.lclLabel5.Name = "lclLabel5";
		this.lclLabel5.Size = new System.Drawing.Size(59, 12);
		this.lclLabel5.TabIndex = 0;
		this.lclLabel5.Text = "lclLabel1";
		this.btnClr1.Location = new System.Drawing.Point(274, 8);
		this.btnClr1.Name = "btnClr1";
		this.btnClr1.Size = new System.Drawing.Size(30, 18);
		this.btnClr1.TabIndex = 1;
		this.btnClr1.Text = "X";
		this.btnClr1.UseVisualStyleBackColor = true;
		this.btnClr1.Click += new System.EventHandler(btnClr1_Click);
		this.lclLabel6.AutoSize = true;
		this.lclLabel6.Location = new System.Drawing.Point(166, 59);
		this.lclLabel6.Name = "lclLabel6";
		this.lclLabel6.Size = new System.Drawing.Size(59, 12);
		this.lclLabel6.TabIndex = 0;
		this.lclLabel6.Text = "lclLabel1";
		this.tpLamp.Controls.Add(this.tcLamp0);
		this.tpLamp.Location = new System.Drawing.Point(4, 23);
		this.tpLamp.Name = "tpLamp";
		this.tpLamp.Size = new System.Drawing.Size(432, 229);
		this.tpLamp.TabIndex = 4;
		this.tpLamp.Text = "检测器";
		this.tpLamp.UseVisualStyleBackColor = true;
		this.tcLamp0.Controls.Add(this.tabPage5);
		this.tcLamp0.Controls.Add(this.tabPage6);
		this.tcLamp0.ItemSize = new System.Drawing.Size(90, 19);
		this.tcLamp0.Location = new System.Drawing.Point(3, 3);
		this.tcLamp0.Name = "tcLamp0";
		this.tcLamp0.SelectedIndex = 0;
		this.tcLamp0.Size = new System.Drawing.Size(312, 132);
		this.tcLamp0.TabIndex = 25;
		this.tabPage5.Controls.Add(this.label25);
		this.tabPage5.Controls.Add(this.label_1);
		this.tabPage5.Controls.Add(this.btnLight0);
		this.tabPage5.Controls.Add(this.label26);
		this.tabPage5.Controls.Add(this.btnRistTime0);
		this.tabPage5.Controls.Add(this.tbReferPool0);
		this.tabPage5.Controls.Add(this.tbSamplePool0);
		this.tabPage5.Controls.Add(this.btnRange0);
		this.tabPage5.Controls.Add(this.btnWave0);
		this.tabPage5.Controls.Add(this.btnZero0);
		this.tabPage5.Controls.Add(this.lbRistTime0);
		this.tabPage5.Controls.Add(this.lbRange0);
		this.tabPage5.Controls.Add(this.cbRistTime0);
		this.tabPage5.Controls.Add(this.lbWave0);
		this.tabPage5.Controls.Add(this.cbRange0);
		this.tabPage5.Controls.Add(this.label3);
		this.tabPage5.Controls.Add(this.label_0);
		this.tabPage5.Controls.Add(this.tbWave0);
		this.tabPage5.Location = new System.Drawing.Point(4, 23);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Size = new System.Drawing.Size(304, 105);
		this.tabPage5.TabIndex = 0;
		this.tabPage5.Text = "tabPage5";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(3, 83);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(41, 12);
		this.label25.TabIndex = 17;
		this.label25.Text = "参照池";
		this.label_1.AutoSize = true;
		this.label_1.Location = new System.Drawing.Point(3, 11);
		this.label_1.Name = "label_1";
		this.label_1.Size = new System.Drawing.Size(53, 12);
		this.label_1.TabIndex = 3;
		this.label_1.Text = "波长(nm)";
		this.btnLight0.Location = new System.Drawing.Point(224, 5);
		this.btnLight0.Name = "btnLight0";
		this.btnLight0.Size = new System.Drawing.Size(75, 23);
		this.btnLight0.TabIndex = 7;
		this.btnLight0.UseVisualStyleBackColor = true;
		this.btnLight0.Click += new System.EventHandler(btnLight_Click);
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(153, 83);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(41, 12);
		this.label26.TabIndex = 18;
		this.label26.Text = "样品池";
		this.btnRistTime0.Location = new System.Drawing.Point(175, 55);
		this.btnRistTime0.Name = "btnRistTime0";
		this.btnRistTime0.Size = new System.Drawing.Size(31, 21);
		this.btnRistTime0.TabIndex = 24;
		this.btnRistTime0.Text = "√";
		this.btnRistTime0.UseVisualStyleBackColor = true;
		this.btnRistTime0.Click += new System.EventHandler(btnRistTime0_Click);
		this.tbReferPool0.Location = new System.Drawing.Point(78, 79);
		this.tbReferPool0.Name = "tbReferPool0";
		this.tbReferPool0.Size = new System.Drawing.Size(66, 21);
		this.tbReferPool0.TabIndex = 20;
		this.tbReferPool0.Text = "...";
		this.tbSamplePool0.Location = new System.Drawing.Point(224, 79);
		this.tbSamplePool0.Name = "tbSamplePool0";
		this.tbSamplePool0.Size = new System.Drawing.Size(75, 21);
		this.tbSamplePool0.TabIndex = 19;
		this.tbSamplePool0.Text = "...";
		this.btnRange0.Location = new System.Drawing.Point(175, 31);
		this.btnRange0.Name = "btnRange0";
		this.btnRange0.Size = new System.Drawing.Size(31, 21);
		this.btnRange0.TabIndex = 24;
		this.btnRange0.Text = "√";
		this.btnRange0.UseVisualStyleBackColor = true;
		this.btnRange0.Click += new System.EventHandler(btnRange0_Click);
		this.btnWave0.Location = new System.Drawing.Point(175, 6);
		this.btnWave0.Name = "btnWave0";
		this.btnWave0.Size = new System.Drawing.Size(31, 21);
		this.btnWave0.TabIndex = 24;
		this.btnWave0.Text = "√";
		this.btnWave0.UseVisualStyleBackColor = true;
		this.btnWave0.Click += new System.EventHandler(btnWave0_Click);
		this.btnZero0.Location = new System.Drawing.Point(223, 54);
		this.btnZero0.Name = "btnZero0";
		this.btnZero0.Size = new System.Drawing.Size(76, 21);
		this.btnZero0.TabIndex = 8;
		this.btnZero0.Text = "自动归零";
		this.btnZero0.UseVisualStyleBackColor = true;
		this.btnZero0.Click += new System.EventHandler(btnZero_Click);
		this.lbRistTime0.AutoSize = true;
		this.lbRistTime0.Location = new System.Drawing.Point(76, 59);
		this.lbRistTime0.Name = "lbRistTime0";
		this.lbRistTime0.Size = new System.Drawing.Size(29, 12);
		this.lbRistTime0.TabIndex = 4;
		this.lbRistTime0.Text = "[..]";
		this.lbRange0.AutoSize = true;
		this.lbRange0.Location = new System.Drawing.Point(76, 35);
		this.lbRange0.Name = "lbRange0";
		this.lbRange0.Size = new System.Drawing.Size(29, 12);
		this.lbRange0.TabIndex = 5;
		this.lbRange0.Text = "[..]";
		this.cbRistTime0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRistTime0.FormattingEnabled = true;
		this.cbRistTime0.Items.AddRange(new object[8] { "0.1", "0.2", "1.0", "2.0", "3.0", "3.5", "4.0", "10.0" });
		this.cbRistTime0.Location = new System.Drawing.Point(111, 55);
		this.cbRistTime0.Name = "cbRistTime0";
		this.cbRistTime0.Size = new System.Drawing.Size(60, 20);
		this.cbRistTime0.TabIndex = 22;
		this.lbWave0.AutoSize = true;
		this.lbWave0.Location = new System.Drawing.Point(76, 11);
		this.lbWave0.Name = "lbWave0";
		this.lbWave0.Size = new System.Drawing.Size(29, 12);
		this.lbWave0.TabIndex = 6;
		this.lbWave0.Text = "[..]";
		this.cbRange0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRange0.FormattingEnabled = true;
		this.cbRange0.Items.AddRange(new object[8] { "0.01", "0.02", "0.05", "0.10", "0.20", "1.00", "2.00", "5.00" });
		this.cbRange0.Location = new System.Drawing.Point(111, 31);
		this.cbRange0.Name = "cbRange0";
		this.cbRange0.Size = new System.Drawing.Size(60, 20);
		this.cbRange0.TabIndex = 23;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 59);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(71, 12);
		this.label3.TabIndex = 1;
		this.label3.Text = "响应时间(s)";
		this.label_0.AutoSize = true;
		this.label_0.Location = new System.Drawing.Point(3, 35);
		this.label_0.Name = "label_0";
		this.label_0.Size = new System.Drawing.Size(41, 12);
		this.label_0.TabIndex = 2;
		this.label_0.Text = "AU范围";
		this.tbWave0.Location = new System.Drawing.Point(111, 6);
		this.tbWave0.Name = "tbWave0";
		this.tbWave0.Size = new System.Drawing.Size(60, 21);
		this.tbWave0.TabIndex = 21;
		this.tbWave0.Text = "440";
		this.tbWave0.KeyDown += new System.Windows.Forms.KeyEventHandler(tbWave0_KeyDown);
		this.tabPage6.Controls.Add(this.lclButton4);
		this.tabPage6.Controls.Add(this.label27);
		this.tabPage6.Controls.Add(this.tbLgtCurrent0);
		this.tabPage6.Controls.Add(this.label28);
		this.tabPage6.Controls.Add(this.tbLgtTotal0);
		this.tabPage6.Location = new System.Drawing.Point(4, 23);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Size = new System.Drawing.Size(304, 105);
		this.tabPage6.TabIndex = 1;
		this.tabPage6.Text = "tabPage6";
		this.tabPage6.UseVisualStyleBackColor = true;
		this.lclButton4.Location = new System.Drawing.Point(150, 56);
		this.lclButton4.Name = "lclButton4";
		this.lclButton4.Size = new System.Drawing.Size(31, 21);
		this.lclButton4.TabIndex = 24;
		this.lclButton4.Text = "X";
		this.lclButton4.UseVisualStyleBackColor = true;
		this.lclButton4.Click += new System.EventHandler(lclButton4_Click);
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(3, 36);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(53, 12);
		this.label27.TabIndex = 9;
		this.label27.Text = "本次时间";
		this.tbLgtCurrent0.Location = new System.Drawing.Point(79, 31);
		this.tbLgtCurrent0.Name = "tbLgtCurrent0";
		this.tbLgtCurrent0.Size = new System.Drawing.Size(66, 21);
		this.tbLgtCurrent0.TabIndex = 12;
		this.tbLgtCurrent0.Text = "-";
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(3, 60);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(53, 12);
		this.label28.TabIndex = 10;
		this.label28.Text = "累计时间";
		this.tbLgtTotal0.Location = new System.Drawing.Point(79, 56);
		this.tbLgtTotal0.Name = "tbLgtTotal0";
		this.tbLgtTotal0.Size = new System.Drawing.Size(66, 21);
		this.tbLgtTotal0.TabIndex = 11;
		this.tbLgtTotal0.Text = "-";
		this.tpChkValve.Controls.Add(this.lclGroupBox3);
		this.tpChkValve.Location = new System.Drawing.Point(4, 23);
		this.tpChkValve.Name = "tpChkValve";
		this.tpChkValve.Size = new System.Drawing.Size(432, 229);
		this.tpChkValve.TabIndex = 5;
		this.tpChkValve.Text = "进样阈";
		this.tpChkValve.UseVisualStyleBackColor = true;
		this.lclGroupBox3.Controls.Add(this.lclLabel3);
		this.lclGroupBox3.Controls.Add(this.tbChkValveTotal);
		this.lclGroupBox3.Controls.Add(this.lclButton6);
		this.lclGroupBox3.Location = new System.Drawing.Point(9, 7);
		this.lclGroupBox3.Name = "lclGroupBox3";
		this.lclGroupBox3.Size = new System.Drawing.Size(153, 45);
		this.lclGroupBox3.TabIndex = 18;
		this.lclGroupBox3.TabStop = false;
		this.lclGroupBox3.Text = "lclGroupBox3";
		this.lclLabel3.AutoSize = true;
		this.lclLabel3.Location = new System.Drawing.Point(9, 20);
		this.lclLabel3.Name = "lclLabel3";
		this.lclLabel3.Size = new System.Drawing.Size(59, 12);
		this.lclLabel3.TabIndex = 12;
		this.lclLabel3.Text = "lclLabel3";
		this.tbChkValveTotal.Location = new System.Drawing.Point(49, 17);
		this.tbChkValveTotal.Name = "tbChkValveTotal";
		this.tbChkValveTotal.Size = new System.Drawing.Size(53, 21);
		this.tbChkValveTotal.TabIndex = 16;
		this.lclButton6.Location = new System.Drawing.Point(111, 17);
		this.lclButton6.Name = "lclButton6";
		this.lclButton6.Size = new System.Drawing.Size(30, 18);
		this.lclButton6.TabIndex = 14;
		this.lclButton6.Text = "X";
		this.lclButton6.UseVisualStyleBackColor = true;
		this.lclButton6.Click += new System.EventHandler(lclButton6_Click);
		this.tpColumn.Controls.Add(this.lclGroupBox2);
		this.tpColumn.Controls.Add(this.groupBox2);
		this.tpColumn.Location = new System.Drawing.Point(4, 23);
		this.tpColumn.Name = "tpColumn";
		this.tpColumn.Size = new System.Drawing.Size(432, 229);
		this.tpColumn.TabIndex = 3;
		this.tpColumn.Text = "色谱柱";
		this.tpColumn.UseVisualStyleBackColor = true;
		this.lclGroupBox2.Controls.Add(this.lclLabel1);
		this.lclGroupBox2.Controls.Add(this.tbColumnCurrent);
		this.lclGroupBox2.Controls.Add(this.lclLabel2);
		this.lclGroupBox2.Controls.Add(this.tbColumnTotal);
		this.lclGroupBox2.Controls.Add(this.lclButton5);
		this.lclGroupBox2.Location = new System.Drawing.Point(9, 7);
		this.lclGroupBox2.Name = "lclGroupBox2";
		this.lclGroupBox2.Size = new System.Drawing.Size(260, 47);
		this.lclGroupBox2.TabIndex = 17;
		this.lclGroupBox2.TabStop = false;
		this.lclGroupBox2.Text = "lclGroupBox2";
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(108, 20);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(59, 12);
		this.lclLabel1.TabIndex = 12;
		this.lclLabel1.Text = "lclLabel1";
		this.tbColumnCurrent.Location = new System.Drawing.Point(49, 17);
		this.tbColumnCurrent.Name = "tbColumnCurrent";
		this.tbColumnCurrent.Size = new System.Drawing.Size(53, 21);
		this.tbColumnCurrent.TabIndex = 15;
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(8, 20);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(59, 12);
		this.lclLabel2.TabIndex = 13;
		this.lclLabel2.Text = "lclLabel1";
		this.lclLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.tbColumnTotal.Location = new System.Drawing.Point(148, 17);
		this.tbColumnTotal.Name = "tbColumnTotal";
		this.tbColumnTotal.Size = new System.Drawing.Size(70, 21);
		this.tbColumnTotal.TabIndex = 16;
		this.lclButton5.Location = new System.Drawing.Point(224, 16);
		this.lclButton5.Name = "lclButton5";
		this.lclButton5.Size = new System.Drawing.Size(30, 18);
		this.lclButton5.TabIndex = 14;
		this.lclButton5.Text = "X";
		this.lclButton5.UseVisualStyleBackColor = true;
		this.lclButton5.Click += new System.EventHandler(lclButton5_Click);
		this.groupBox2.Controls.Add(this.lclButton2);
		this.groupBox2.Controls.Add(this.pictureBox1);
		this.groupBox2.Controls.Add(this.button1);
		this.groupBox2.Controls.Add(this.tbColumnTemp);
		this.groupBox2.Controls.Add(this.label30);
		this.groupBox2.Controls.Add(this.label29);
		this.groupBox2.Location = new System.Drawing.Point(9, 60);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(199, 77);
		this.groupBox2.TabIndex = 11;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "柱温箱";
		this.lclButton2.Location = new System.Drawing.Point(158, 17);
		this.lclButton2.Name = "lclButton2";
		this.lclButton2.Size = new System.Drawing.Size(31, 21);
		this.lclButton2.TabIndex = 25;
		this.lclButton2.Text = "√";
		this.lclButton2.UseVisualStyleBackColor = true;
		this.pictureBox1.Location = new System.Drawing.Point(14, 42);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(74, 24);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		this.pictureBox1.TabIndex = 8;
		this.pictureBox1.TabStop = false;
		this.button1.Location = new System.Drawing.Point(94, 44);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(57, 21);
		this.button1.TabIndex = 0;
		this.button1.Text = "加热";
		this.button1.UseVisualStyleBackColor = true;
		this.tbColumnTemp.Location = new System.Drawing.Point(116, 17);
		this.tbColumnTemp.Name = "tbColumnTemp";
		this.tbColumnTemp.Size = new System.Drawing.Size(39, 21);
		this.tbColumnTemp.TabIndex = 7;
		this.tbColumnTemp.Text = "40.0";
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(71, 20);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(29, 12);
		this.label30.TabIndex = 4;
		this.label30.Text = "40.0";
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(12, 20);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(53, 12);
		this.label29.TabIndex = 4;
		this.label29.Text = "柱温[℃]";
		this.tpLCItems.Controls.Add(this.groupBox1);
		this.tpLCItems.Location = new System.Drawing.Point(4, 23);
		this.tpLCItems.Name = "tpLCItems";
		this.tpLCItems.Size = new System.Drawing.Size(432, 229);
		this.tpLCItems.TabIndex = 1;
		this.tpLCItems.Text = "项目";
		this.tpLCItems.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.btnStopMl);
		this.groupBox1.Controls.Add(this.btnWarnMl);
		this.groupBox1.Controls.Add(this.btnTotalMl);
		this.groupBox1.Controls.Add(this.cbCapacityCtrl);
		this.groupBox1.Controls.Add(this.lbWarnMl);
		this.groupBox1.Controls.Add(this.lbStopMl);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.lbTotalMl);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.tbStopMl);
		this.groupBox1.Controls.Add(this.tbWarnMl);
		this.groupBox1.Controls.Add(this.tbTotalMl);
		this.groupBox1.Location = new System.Drawing.Point(7, 5);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(239, 96);
		this.groupBox1.TabIndex = 10;
		this.groupBox1.TabStop = false;
		this.btnStopMl.Location = new System.Drawing.Point(183, 69);
		this.btnStopMl.Name = "btnStopMl";
		this.btnStopMl.Size = new System.Drawing.Size(31, 21);
		this.btnStopMl.TabIndex = 25;
		this.btnStopMl.Text = "√";
		this.btnStopMl.UseVisualStyleBackColor = true;
		this.btnStopMl.Click += new System.EventHandler(btnStopMl_Click);
		this.btnWarnMl.Location = new System.Drawing.Point(183, 44);
		this.btnWarnMl.Name = "btnWarnMl";
		this.btnWarnMl.Size = new System.Drawing.Size(31, 21);
		this.btnWarnMl.TabIndex = 25;
		this.btnWarnMl.Text = "√";
		this.btnWarnMl.UseVisualStyleBackColor = true;
		this.btnWarnMl.Click += new System.EventHandler(btnWarnMl_Click);
		this.btnTotalMl.Location = new System.Drawing.Point(183, 19);
		this.btnTotalMl.Name = "btnTotalMl";
		this.btnTotalMl.Size = new System.Drawing.Size(31, 21);
		this.btnTotalMl.TabIndex = 25;
		this.btnTotalMl.Text = "√";
		this.btnTotalMl.UseVisualStyleBackColor = true;
		this.btnTotalMl.Click += new System.EventHandler(btnTotalMl_Click);
		this.cbCapacityCtrl.AutoSize = true;
		this.cbCapacityCtrl.BackColor = System.Drawing.SystemColors.Window;
		this.cbCapacityCtrl.Location = new System.Drawing.Point(6, 0);
		this.cbCapacityCtrl.Name = "cbCapacityCtrl";
		this.cbCapacityCtrl.Size = new System.Drawing.Size(132, 16);
		this.cbCapacityCtrl.TabIndex = 8;
		this.cbCapacityCtrl.Text = "流动相总量控制[ml]";
		this.cbCapacityCtrl.UseVisualStyleBackColor = false;
		this.lbWarnMl.AutoSize = true;
		this.lbWarnMl.Location = new System.Drawing.Point(105, 49);
		this.lbWarnMl.Name = "lbWarnMl";
		this.lbWarnMl.Size = new System.Drawing.Size(29, 12);
		this.lbWarnMl.TabIndex = 4;
		this.lbWarnMl.Text = "停泵";
		this.lbStopMl.AutoSize = true;
		this.lbStopMl.Location = new System.Drawing.Point(105, 74);
		this.lbStopMl.Name = "lbStopMl";
		this.lbStopMl.Size = new System.Drawing.Size(29, 12);
		this.lbStopMl.TabIndex = 4;
		this.lbStopMl.Text = "停泵";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(6, 73);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(29, 12);
		this.label23.TabIndex = 4;
		this.label23.Text = "停泵";
		this.lbTotalMl.AutoSize = true;
		this.lbTotalMl.Location = new System.Drawing.Point(105, 24);
		this.lbTotalMl.Name = "lbTotalMl";
		this.lbTotalMl.Size = new System.Drawing.Size(41, 12);
		this.lbTotalMl.TabIndex = 4;
		this.lbTotalMl.Text = "总容量";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(7, 23);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(41, 12);
		this.label19.TabIndex = 4;
		this.label19.Text = "总容量";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(7, 48);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(29, 12);
		this.label22.TabIndex = 4;
		this.label22.Text = "警告";
		this.tbStopMl.Location = new System.Drawing.Point(140, 69);
		this.tbStopMl.Name = "tbStopMl";
		this.tbStopMl.Size = new System.Drawing.Size(39, 21);
		this.tbStopMl.TabIndex = 7;
		this.tbStopMl.Text = "10";
		this.tbStopMl.KeyDown += new System.Windows.Forms.KeyEventHandler(tbStopMl_KeyDown);
		this.tbWarnMl.Location = new System.Drawing.Point(140, 44);
		this.tbWarnMl.Name = "tbWarnMl";
		this.tbWarnMl.Size = new System.Drawing.Size(39, 21);
		this.tbWarnMl.TabIndex = 7;
		this.tbWarnMl.Text = "20";
		this.tbWarnMl.KeyDown += new System.Windows.Forms.KeyEventHandler(tbWarnMl_KeyDown);
		this.tbTotalMl.Location = new System.Drawing.Point(140, 19);
		this.tbTotalMl.Name = "tbTotalMl";
		this.tbTotalMl.Size = new System.Drawing.Size(39, 21);
		this.tbTotalMl.TabIndex = 7;
		this.tbTotalMl.Text = "500";
		this.tbTotalMl.KeyDown += new System.Windows.Forms.KeyEventHandler(tbTotalMl_KeyDown);
		this.tcGcDevMnt.Controls.Add(this.tpgcTemp);
		this.tcGcDevMnt.Controls.Add(this.tpgcParas);
		this.tcGcDevMnt.Controls.Add(this.tpgcDtcs);
		this.tcGcDevMnt.Enabled = false;
		this.tcGcDevMnt.ItemSize = new System.Drawing.Size(90, 19);
		this.tcGcDevMnt.Location = new System.Drawing.Point(0, 28);
		this.tcGcDevMnt.Name = "tcGcDevMnt";
		this.tcGcDevMnt.SelectedIndex = 0;
		this.tcGcDevMnt.Size = new System.Drawing.Size(440, 162);
		this.tcGcDevMnt.TabIndex = 4;
		this.tpgcTemp.Controls.Add(this.btnCloseCT);
		this.tpgcTemp.Controls.Add(this.btnCT6Set);
		this.tpgcTemp.Controls.Add(this.btnStartCT);
		this.tpgcTemp.Controls.Add(this.btnCT6Qry);
		this.tpgcTemp.Controls.Add(this.btnctrlNameSet);
		this.tpgcTemp.Controls.Add(this.btnCteSet);
		this.tpgcTemp.Controls.Add(this.dgvCT6);
		this.tpgcTemp.Location = new System.Drawing.Point(4, 23);
		this.tpgcTemp.Name = "tpgcTemp";
		this.tpgcTemp.Size = new System.Drawing.Size(432, 135);
		this.tpgcTemp.TabIndex = 0;
		this.tpgcTemp.Text = "温度";
		this.tpgcTemp.UseVisualStyleBackColor = true;
		this.btnCloseCT.Location = new System.Drawing.Point(287, 108);
		this.btnCloseCT.Name = "btnCloseCT";
		this.btnCloseCT.Size = new System.Drawing.Size(122, 23);
		this.btnCloseCT.TabIndex = 4;
		this.btnCloseCT.Text = "lclButton1";
		this.btnCloseCT.UseVisualStyleBackColor = true;
		this.btnCloseCT.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnCT6Set.Location = new System.Drawing.Point(336, 31);
		this.btnCT6Set.Name = "btnCT6Set";
		this.btnCT6Set.Size = new System.Drawing.Size(73, 23);
		this.btnCT6Set.TabIndex = 4;
		this.btnCT6Set.Text = "设置温度";
		this.btnCT6Set.UseVisualStyleBackColor = true;
		this.btnCT6Set.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnStartCT.Location = new System.Drawing.Point(287, 84);
		this.btnStartCT.Name = "btnStartCT";
		this.btnStartCT.Size = new System.Drawing.Size(122, 23);
		this.btnStartCT.TabIndex = 4;
		this.btnStartCT.Text = "lclButton1";
		this.btnStartCT.UseVisualStyleBackColor = true;
		this.btnStartCT.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnCT6Qry.Location = new System.Drawing.Point(287, 7);
		this.btnCT6Qry.Name = "btnCT6Qry";
		this.btnCT6Qry.Size = new System.Drawing.Size(43, 71);
		this.btnCT6Qry.TabIndex = 4;
		this.btnCT6Qry.Text = "查询";
		this.btnCT6Qry.UseVisualStyleBackColor = true;
		this.btnCT6Qry.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnctrlNameSet.Location = new System.Drawing.Point(336, 7);
		this.btnctrlNameSet.Name = "btnctrlNameSet";
		this.btnctrlNameSet.Size = new System.Drawing.Size(73, 23);
		this.btnctrlNameSet.TabIndex = 4;
		this.btnctrlNameSet.Text = "写控区名";
		this.btnctrlNameSet.UseVisualStyleBackColor = true;
		this.btnctrlNameSet.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnCteSet.Location = new System.Drawing.Point(336, 55);
		this.btnCteSet.Name = "btnCteSet";
		this.btnCteSet.Size = new System.Drawing.Size(73, 23);
		this.btnCteSet.TabIndex = 4;
		this.btnCteSet.Text = "控温使能";
		this.btnCteSet.UseVisualStyleBackColor = true;
		this.btnCteSet.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.dgvCT6.AllowUserToAddRows = false;
		this.dgvCT6.AllowUserToDeleteRows = false;
		this.dgvCT6.BackgroundColor = System.Drawing.Color.White;
		this.dgvCT6.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCT6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dgvCT6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCT6.Columns.AddRange(this.clmCT6CN, this.clmCT6EN, this.clmCT6T, this.clmCT6SetT, this.clmCT6PtcT, this.clmCT6CtrlT);
		this.dgvCT6.Dock = System.Windows.Forms.DockStyle.Left;
		this.dgvCT6.EnableHeadersVisualStyles = false;
		this.dgvCT6.Location = new System.Drawing.Point(0, 0);
		this.dgvCT6.MultiSelect = false;
		this.dgvCT6.Name = "dgvCT6";
		this.dgvCT6.RowHeadersVisible = false;
		this.dgvCT6.RowHeadersWidth = 80;
		this.dgvCT6.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvCT6.RowTemplate.Height = 18;
		this.dgvCT6.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvCT6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCT6.ShowEditingIcon = false;
		this.dgvCT6.Size = new System.Drawing.Size(330, 135);
		this.dgvCT6.TabIndex = 3;
		this.dgvCT6.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(gvDtcrs_DataError);
		this.clmCT6CN.HeaderText = "";
		this.clmCT6CN.Name = "clmCT6CN";
		this.clmCT6CN.Width = 60;
		this.clmCT6EN.HeaderText = "";
		this.clmCT6EN.Name = "clmCT6EN";
		this.clmCT6EN.Width = 60;
		dataGridViewCellStyle2.Format = "0.0";
		this.clmCT6T.DefaultCellStyle = dataGridViewCellStyle2;
		this.clmCT6T.HeaderText = "实测[℃]";
		this.clmCT6T.Name = "clmCT6T";
		this.clmCT6T.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmCT6T.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmCT6T.Width = 60;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.Format = "0.0";
		this.clmCT6SetT.DefaultCellStyle = dataGridViewCellStyle3;
		this.clmCT6SetT.HeaderText = "设定[℃]";
		this.clmCT6SetT.Name = "clmCT6SetT";
		this.clmCT6SetT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmCT6SetT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmCT6SetT.Width = 60;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.Format = "0.0";
		this.clmCT6PtcT.DefaultCellStyle = dataGridViewCellStyle4;
		this.clmCT6PtcT.HeaderText = "保护[℃]";
		this.clmCT6PtcT.Name = "clmCT6PtcT";
		this.clmCT6PtcT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmCT6PtcT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmCT6PtcT.Width = 60;
		this.clmCT6CtrlT.HeaderText = "Column1";
		this.clmCT6CtrlT.Name = "clmCT6CtrlT";
		this.clmCT6CtrlT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.clmCT6CtrlT.Width = 40;
		this.tpgcParas.Controls.Add(this.btnNpSet);
		this.tpgcParas.Controls.Add(this.btnNpQry);
		this.tpgcParas.Controls.Add(this.tbinsSerial);
		this.tpgcParas.Controls.Add(this.gvNet);
		this.tpgcParas.Controls.Add(this.btnHardVersionQry);
		this.tpgcParas.Controls.Add(this.btninsSerialSet);
		this.tpgcParas.Controls.Add(this.lbinsSerial);
		this.tpgcParas.Controls.Add(this.label2);
		this.tpgcParas.Controls.Add(this.gvHardVersion);
		this.tpgcParas.Location = new System.Drawing.Point(4, 23);
		this.tpgcParas.Name = "tpgcParas";
		this.tpgcParas.Size = new System.Drawing.Size(432, 135);
		this.tpgcParas.TabIndex = 1;
		this.tpgcParas.Text = "仪器";
		this.tpgcParas.UseVisualStyleBackColor = true;
		this.btnNpSet.Location = new System.Drawing.Point(356, 111);
		this.btnNpSet.Name = "btnNpSet";
		this.btnNpSet.Size = new System.Drawing.Size(52, 23);
		this.btnNpSet.TabIndex = 3;
		this.btnNpSet.Text = "设置";
		this.btnNpSet.UseVisualStyleBackColor = true;
		this.btnNpSet.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnNpQry.Location = new System.Drawing.Point(298, 112);
		this.btnNpQry.Name = "btnNpQry";
		this.btnNpQry.Size = new System.Drawing.Size(52, 23);
		this.btnNpQry.TabIndex = 3;
		this.btnNpQry.Text = "查询";
		this.btnNpQry.UseVisualStyleBackColor = true;
		this.btnNpQry.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.tbinsSerial.Location = new System.Drawing.Point(8, 22);
		this.tbinsSerial.Name = "tbinsSerial";
		this.tbinsSerial.Size = new System.Drawing.Size(143, 21);
		this.tbinsSerial.TabIndex = 2;
		this.tbinsSerial.Text = "?";
		this.gvNet.AllowUserToAddRows = false;
		this.gvNet.AllowUserToDeleteRows = false;
		this.gvNet.BackgroundColor = System.Drawing.Color.White;
		this.gvNet.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvNet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
		this.gvNet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvNet.Columns.AddRange(this.dataGridViewTextBoxColumn1);
		this.gvNet.EnableHeadersVisualStyles = false;
		this.gvNet.Location = new System.Drawing.Point(213, 0);
		this.gvNet.MultiSelect = false;
		this.gvNet.Name = "gvNet";
		this.gvNet.RowHeadersWidth = 85;
		this.gvNet.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvNet.RowTemplate.Height = 18;
		this.gvNet.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.gvNet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.gvNet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvNet.ShowEditingIcon = false;
		this.gvNet.Size = new System.Drawing.Size(213, 108);
		this.gvNet.TabIndex = 3;
		this.gvNet.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(gvDtcrs_DataError);
		this.dataGridViewTextBoxColumn1.HeaderText = "地址";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.Width = 105;
		this.btnHardVersionQry.Location = new System.Drawing.Point(5, 50);
		this.btnHardVersionQry.Name = "btnHardVersionQry";
		this.btnHardVersionQry.Size = new System.Drawing.Size(52, 23);
		this.btnHardVersionQry.TabIndex = 3;
		this.btnHardVersionQry.Text = "查询";
		this.btnHardVersionQry.UseVisualStyleBackColor = true;
		this.btnHardVersionQry.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btninsSerialSet.Location = new System.Drawing.Point(155, 20);
		this.btninsSerialSet.Name = "btninsSerialSet";
		this.btninsSerialSet.Size = new System.Drawing.Size(52, 23);
		this.btninsSerialSet.TabIndex = 3;
		this.btninsSerialSet.Text = "设置";
		this.btninsSerialSet.UseVisualStyleBackColor = true;
		this.btninsSerialSet.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.lbinsSerial.AutoSize = true;
		this.lbinsSerial.Location = new System.Drawing.Point(76, 6);
		this.lbinsSerial.Name = "lbinsSerial";
		this.lbinsSerial.Size = new System.Drawing.Size(11, 12);
		this.lbinsSerial.TabIndex = 1;
		this.lbinsSerial.Text = "X";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(5, 5);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "仪器序列号";
		this.gvHardVersion.AllowUserToAddRows = false;
		this.gvHardVersion.AllowUserToDeleteRows = false;
		this.gvHardVersion.BackgroundColor = System.Drawing.Color.White;
		this.gvHardVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvHardVersion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.gvHardVersion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvHardVersion.Columns.AddRange(this.clmHV);
		this.gvHardVersion.EnableHeadersVisualStyles = false;
		this.gvHardVersion.Location = new System.Drawing.Point(0, 76);
		this.gvHardVersion.MultiSelect = false;
		this.gvHardVersion.Name = "gvHardVersion";
		this.gvHardVersion.RowHeadersWidth = 40;
		this.gvHardVersion.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvHardVersion.RowTemplate.Height = 18;
		this.gvHardVersion.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.gvHardVersion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvHardVersion.ShowEditingIcon = false;
		this.gvHardVersion.Size = new System.Drawing.Size(204, 57);
		this.gvHardVersion.TabIndex = 3;
		this.gvHardVersion.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(gvDtcrs_DataError);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.clmHV.DefaultCellStyle = dataGridViewCellStyle7;
		this.clmHV.HeaderText = "类型   版本";
		this.clmHV.Name = "clmHV";
		this.clmHV.ReadOnly = true;
		this.clmHV.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmHV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmHV.Width = 144;
		this.tpgcDtcs.Controls.Add(this.btnAlyStop);
		this.tpgcDtcs.Controls.Add(this.btnAlyStart);
		this.tpgcDtcs.Controls.Add(this.btnStartFID1);
		this.tpgcDtcs.Controls.Add(this.btnStartFID2);
		this.tpgcDtcs.Controls.Add(this.lclLabel24);
		this.tpgcDtcs.Controls.Add(this.label4);
		this.tpgcDtcs.Controls.Add(this.btnDtcrsQry);
		this.tpgcDtcs.Controls.Add(this.gvDtcrs);
		this.tpgcDtcs.Controls.Add(this.nudDtcrNum);
		this.tpgcDtcs.Controls.Add(this.btnDtcrsSet);
		this.tpgcDtcs.Location = new System.Drawing.Point(4, 23);
		this.tpgcDtcs.Name = "tpgcDtcs";
		this.tpgcDtcs.Size = new System.Drawing.Size(432, 135);
		this.tpgcDtcs.TabIndex = 2;
		this.tpgcDtcs.Text = "检测器";
		this.tpgcDtcs.UseVisualStyleBackColor = true;
		this.btnAlyStop.Location = new System.Drawing.Point(359, 41);
		this.btnAlyStop.Name = "btnAlyStop";
		this.btnAlyStop.Size = new System.Drawing.Size(70, 23);
		this.btnAlyStop.TabIndex = 9;
		this.btnAlyStop.Text = "结束(F4)";
		this.btnAlyStop.UseVisualStyleBackColor = true;
		this.btnAlyStop.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnAlyStart.Location = new System.Drawing.Point(359, 13);
		this.btnAlyStart.Name = "btnAlyStart";
		this.btnAlyStart.Size = new System.Drawing.Size(70, 23);
		this.btnAlyStart.TabIndex = 9;
		this.btnAlyStart.Text = "分析(F3)";
		this.btnAlyStart.UseVisualStyleBackColor = true;
		this.btnAlyStart.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnStartFID1.Location = new System.Drawing.Point(359, 79);
		this.btnStartFID1.Name = "btnStartFID1";
		this.btnStartFID1.Size = new System.Drawing.Size(70, 23);
		this.btnStartFID1.TabIndex = 9;
		this.btnStartFID1.Text = "FID1点火";
		this.btnStartFID1.UseVisualStyleBackColor = true;
		this.btnStartFID1.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.btnStartFID2.Location = new System.Drawing.Point(359, 107);
		this.btnStartFID2.Name = "btnStartFID2";
		this.btnStartFID2.Size = new System.Drawing.Size(70, 23);
		this.btnStartFID2.TabIndex = 8;
		this.btnStartFID2.Text = "FID2点火";
		this.btnStartFID2.UseVisualStyleBackColor = true;
		this.btnStartFID2.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.lclLabel24.AutoSize = true;
		this.lclLabel24.Location = new System.Drawing.Point(6, 9);
		this.lclLabel24.Name = "lclLabel24";
		this.lclLabel24.Size = new System.Drawing.Size(29, 12);
		this.lclLabel24.TabIndex = 7;
		this.lclLabel24.Text = "个数";
		this.label4.AutoSize = true;
		this.label4.ForeColor = System.Drawing.Color.Blue;
		this.label4.Location = new System.Drawing.Point(8, 113);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(197, 12);
		this.label4.TabIndex = 6;
		this.label4.Text = "TCD:桥流 0—230   其它:量程6—10";
		this.btnDtcrsQry.Location = new System.Drawing.Point(203, 3);
		this.btnDtcrsQry.Name = "btnDtcrsQry";
		this.btnDtcrsQry.Size = new System.Drawing.Size(52, 23);
		this.btnDtcrsQry.TabIndex = 3;
		this.btnDtcrsQry.Text = "查询";
		this.btnDtcrsQry.UseVisualStyleBackColor = true;
		this.btnDtcrsQry.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.gvDtcrs.AllowUserToAddRows = false;
		this.gvDtcrs.AllowUserToDeleteRows = false;
		this.gvDtcrs.BackgroundColor = System.Drawing.Color.White;
		this.gvDtcrs.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvDtcrs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.gvDtcrs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvDtcrs.Columns.AddRange(this.clmDtMark, this.clmDtPosi, this.clmDtRange, this.clmDtBsdct, this.clmDtFreq, this.clmDtStart, this.clmDtStop);
		this.gvDtcrs.EnableHeadersVisualStyles = false;
		this.gvDtcrs.Location = new System.Drawing.Point(0, 28);
		this.gvDtcrs.MultiSelect = false;
		this.gvDtcrs.Name = "gvDtcrs";
		this.gvDtcrs.RowHeadersVisible = false;
		this.gvDtcrs.RowHeadersWidth = 35;
		this.gvDtcrs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvDtcrs.RowTemplate.Height = 18;
		this.gvDtcrs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.gvDtcrs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvDtcrs.ShowCellErrors = false;
		this.gvDtcrs.ShowCellToolTips = false;
		this.gvDtcrs.ShowEditingIcon = false;
		this.gvDtcrs.ShowRowErrors = false;
		this.gvDtcrs.Size = new System.Drawing.Size(355, 90);
		this.gvDtcrs.TabIndex = 4;
		this.gvDtcrs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvDtcrs_CellClick);
		this.gvDtcrs.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(gvDtcrs_DataError);
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.clmDtMark.DefaultCellStyle = dataGridViewCellStyle9;
		this.clmDtMark.HeaderText = "标识";
		this.clmDtMark.Name = "clmDtMark";
		this.clmDtMark.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmDtMark.Width = 50;
		this.clmDtPosi.HeaderText = "正极性";
		this.clmDtPosi.Name = "clmDtPosi";
		this.clmDtPosi.Width = 60;
		this.clmDtRange.HeaderText = "桥/量";
		this.clmDtRange.Name = "clmDtRange";
		this.clmDtRange.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmDtRange.Width = 60;
		this.clmDtBsdct.HeaderText = "基线扣除";
		this.clmDtBsdct.Name = "clmDtBsdct";
		this.clmDtBsdct.Width = 60;
		this.clmDtFreq.HeaderText = "频率";
		this.clmDtFreq.Name = "clmDtFreq";
		this.clmDtFreq.Width = 50;
		this.clmDtStart.HeaderText = "F5-8";
		this.clmDtStart.Name = "clmDtStart";
		this.clmDtStart.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmDtStart.Width = 35;
		this.clmDtStop.HeaderText = "F9-12";
		this.clmDtStop.Name = "clmDtStop";
		this.clmDtStop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmDtStop.Width = 35;
		this.nudDtcrNum.Location = new System.Drawing.Point(38, 4);
		this.nudDtcrNum.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.nudDtcrNum.Name = "nudDtcrNum";
		this.nudDtcrNum.Size = new System.Drawing.Size(42, 21);
		this.nudDtcrNum.TabIndex = 5;
		this.nudDtcrNum.ValueChanged += new System.EventHandler(nudDtcrNum_ValueChanged);
		this.btnDtcrsSet.Location = new System.Drawing.Point(261, 3);
		this.btnDtcrsSet.Name = "btnDtcrsSet";
		this.btnDtcrsSet.Size = new System.Drawing.Size(52, 23);
		this.btnDtcrsSet.TabIndex = 3;
		this.btnDtcrsSet.Text = "设置";
		this.btnDtcrsSet.UseVisualStyleBackColor = true;
		this.btnDtcrsSet.Click += new System.EventHandler(btnDtcrsSet_Click);
		this.timer_0.Interval = 400;
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		base.ClientSize = new System.Drawing.Size(867, 215);
		base.Controls.Add(this.tcGcDevMnt);
		base.Controls.Add(this.tcLcDevMnt);
		base.Controls.Add(this.ssDevMnt);
		base.Controls.Add(this.msDevMnt);
		base.MainMenuStrip = this.msDevMnt;
		base.MaximizeBox = false;
		base.Name = "DevMonitorForm";
		this.Text = "设备监控";
		base.Load += new System.EventHandler(DevMonitorForm_Load);
		this.msDevMnt.ResumeLayout(false);
		this.msDevMnt.PerformLayout();
		this.ssDevMnt.ResumeLayout(false);
		this.ssDevMnt.PerformLayout();
		this.tcLcDevMnt.ResumeLayout(false);
		this.tpLC.ResumeLayout(false);
		this.dpLC.ResumeLayout(false);
		this.dpLC.PerformLayout();
		this.tpPump.ResumeLayout(false);
		this.tcPump1.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		this.tabPage4.ResumeLayout(false);
		this.tabPage4.PerformLayout();
		this.tcPump0.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.tpLamp.ResumeLayout(false);
		this.tcLamp0.ResumeLayout(false);
		this.tabPage5.ResumeLayout(false);
		this.tabPage5.PerformLayout();
		this.tabPage6.ResumeLayout(false);
		this.tabPage6.PerformLayout();
		this.tpChkValve.ResumeLayout(false);
		this.lclGroupBox3.ResumeLayout(false);
		this.lclGroupBox3.PerformLayout();
		this.tpColumn.ResumeLayout(false);
		this.lclGroupBox2.ResumeLayout(false);
		this.lclGroupBox2.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.tpLCItems.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tcGcDevMnt.ResumeLayout(false);
		this.tpgcTemp.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).EndInit();
		this.tpgcParas.ResumeLayout(false);
		this.tpgcParas.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvNet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvHardVersion).EndInit();
		this.tpgcDtcs.ResumeLayout(false);
		this.tpgcDtcs.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvDtcrs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudDtcrNum).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
