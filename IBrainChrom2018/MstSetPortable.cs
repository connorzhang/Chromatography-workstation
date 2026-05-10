using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;
using HZH_Controls;
using HZH_Controls.Controls;
using HZH_Controls.Forms;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class MstSetPortable : UserControl
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CallbackFun(int i);

	private SystemParam sysParam = SystemParam.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

	private MtdSetup mtdMgr;

	public static MstSetPortable selfCtrl;

	public ChromFormInterface thisFormMf;

	public bool bCali = false;

	public Peak[] peakComp;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private ChromDisplay chromDisplay_0;

	private DisLg disLg_0 = default(DisLg);

	private Options options_0 = new Options();

	private bool bLoading = true;

	private bool bShowComponentTable;

	private bool bComponentTabpage;

	private bool bParametersTabpage;

	private bool bReportTabpage;

	private bool bOnlineMethod;

	private bool bOnlineMethod2;

	private bool bMethodNew;

	public int UsePlace;

	public float fAver1 = 0f;

	public float fAver2 = 0f;

	public float fHeightAVer1 = 0f;

	public float fHeightAVer2 = 0f;

	public List<calibraGridModel> caliSource = new List<calibraGridModel>();

	public List<Chromatogram> caliChromSource = new List<Chromatogram>();

	public int caliCnt = 0;

	public Series[] serieLines = new Series[2];

	private List<Color> strSerialColor = new List<Color>();

	private List<string> strSerialName = new List<string>();

	public PrintPara printParaOld = new PrintPara();

	public InsDeviceManager mstDeviceManager = new InsDeviceManager();

	private int m_iLevel = 0;

	private int int_3 = 0;

	private ColumnsSetupDlg columnsSetupDlg_0;

	private CaliGnlOptDlg caliGnlOptDlg_0;

	private CaliGnlIstdDlg caliGnlIstdDlg_0;

	private CaliGnlUserCtrl caliGnlForm_0;

	private DataGridViewColumn dataGridViewColumn_0;

	private OpenFileDialog openFileDialog_1;

	private IContainer icontainer_0;

	private TabControl tabControl3;

	private TabPage tabPage4;

	private LclPanel pnlcu;

	private LclGroupBox gbadvColumnCalcu;

	private LclRadioButton rbccFrom50per;

	private LclRadioButton rbccStatistical;

	private LclTextBox tbccColumnLength;

	public LclLabel lbccColumnLengthU;

	private LclTextBox tbccUnretainedPeak;

	private LclLabel lbccColumnLength;

	public LclLabel lbccUnretainedPeakU;

	private LclLabel lbccUnretainedPeak;

	private LclGroupBox gbadvAddSub;

	private LclRadioButton rbasSub;

	private LclRadioButton rbasAdd;

	private LclCusComboBox cbasMatching;

	private LclButton btnasNoneChrom;

	private LclTextBox tbasChrom;

	private LclLabel lbasMatching;

	private LclLabel lbasChrom;

	public LclCusComboBox cbprsUncalBase;

	private LclLabel lbcuUncalAmtRespF;

	private LclTextBox tbprsUncalAmtRespF;

	private LclGroupBox gbcuScale;

	private LclCheckBox cbprsUseScaleFactor;

	private LclLabel lbcuUnitAfterScale;

	private LclLabel lbcuScaleFactor;

	private LclTextBox tbprsUnitAfterScale;

	private LclTextBox tbprsScaleFactor;

	private LclGroupBox gbcuRltTableReport;

	private LclRadioButton rbrtrCaliPeaks;

	private LclRadioButton rbrtrIdentifiedPeaks;

	private LclRadioButton rbrtrAllDetectedPeaks;

	private LclCheckBox cbrtrHideISTDPeak;

	private TabPage tabPage17;

	private GroupBox groupBox18;

	public LclIntegGridView gvInteg;

	private TabPage tabPage19;

	private GroupBox groupBox17;

	private TextBox rptbotom;

	private GroupBox groupBox15;

	private TextBox rpthead;

	private GroupBox groupBox1;

	private RadioButton radioButton8;

	private GroupBox groupBox11;

	private Label label69;

	private NumericUpDown numericUpDown2;

	private CheckBox cbRptJYTime;

	private CheckBox cbRptWithResult;

	private CheckBox cbRptWithSourceData;

	private CheckBox cbRptChromBold;

	private CheckBox cbRptBoundary;

	private CheckBox cbRptWithChrom;

	private CheckBox cbRptFileName;

	private CheckBox cbRptWithTailFactor;

	private CheckBox cbRptWithCapFactor;

	private CheckBox cbRptWithValidNumber;

	private CheckBox cbRptWithZhuXiao;

	private CheckBox cbRptWithPeakResulution;

	private CheckBox cbRptWithCoef;

	private CheckBox cbRptWithCurve;

	private CheckBox cbRptWithPeakSingle;

	private CheckBox cbRptWithPeakHalf;

	private CheckBox cbRptWithPeakHeight;

	private CheckBox cbRptWithPeakArea;

	private CheckBox cbRptWithDensity;

	private CheckBox cbRptCorrFactor;

	private CheckBox cbRptZuFenName;

	private CheckBox cbRptKeepTime;

	private CheckBox cbRptWithIdx;

	private CheckBox cbRptPrintTime;

	private Label label12;

	private Label label30;

	private Label label56;

	private Label label68;

	private LclGroupBox gbCalibration;

	private LclButton btncclNew;

	private LclButton btncclView;

	private LclButton btncclNone;

	private LclButton btncclSet;

	private GroupBox groupBox2;

	private LclRadioButton lclRbInner;

	private LclRadioButton lclRbOuter;

	private LclRadioButton lclRbNo;

	private Button MethodNew;

	private Button MethodOpen;

	private TextBox tbMethName;

	private Label label1;

	private Button bUseSet;

	private Label label2;

	private ContextMenuStrip cmsIntegration;

	private ToolStripMenuItem miIntegAppendRow;

	private ToolStripMenuItem miIntegDeleteRows;

	private ToolStripMenuItem miIntegInsertRow;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripMenuItem miIntegResetRows;

	private ToolStripMenuItem miIntegRowsDown;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripMenuItem miIntegRowsUp;

	private ImageList imageList_0;

	private LclGroupBox lclGroupBox1;

	private LclLabel lbcuIstdAmount;

	private LclLabel lbcuDilution;

	private LclLabel lbcuInjVolume;

	private NumericUpDown numericUpDown1;

	private NumericUpDown numericUpDown3;

	public TextBox ReportTitle;

	private Label TipLable;

	private RadioButton radioButton1;

	private CheckBox cbRptWithPeakHeightPercent;

	private CheckBox cbRptWithPeakAreaPercent;

	private CheckBox cbRptWithDensityPercent;

	private TabPage tabPage1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;

	public DataGridView dgvCT6;

	private DataGridViewTextBoxColumn clmCT6CN;

	private DataGridViewTextBoxColumn clmCT6SetT;

	private LclGridView gvgcProgTemp;

	public Label lbptInitT;

	private Label label3;

	private Label label27;

	public TextBox tbptIniTempHoldT;

	private GroupBox groupBox4;

	private TabControl tabControl1;

	private TabPage tabPage2;

	private TabPage tabPage3;

	public DataGridView gvExtEvTP;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private DataGridViewTextBoxColumn 事件;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

	private Panel panel12;

	public MaskedTextBox maskedTextBox8;

	private Label label5;

	private Panel panelJY;

	private Panel panel15;

	private Label label13;

	public MaskedTextBox maskedTextBox5;

	public DataGridView dgInsamp1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

	private Label label6;

	public MaskedTextBox maskedTextBox9;

	private Label label7;

	private Label label8;

	private Label label9;

	public ComboBox comboBox6;

	private Panel panel7;

	public RadioButton radioButton25;

	public RadioButton radioButton5;

	public RadioButton radioButton6;

	private Panel panel6;

	public RadioButton radioButton3;

	public RadioButton radioButton2;

	public RadioButton radioButton9;

	private Label label11;

	public MaskedTextBox maskedTextBox4;

	public RadioButton radioButton28;

	public Label label18;

	private Label label19;

	public TabControl tabControl2;

	private TabPage tabPage6;

	private TabPage tabPage7;

	private TabPage tabPage8;

	private TabPage tabPage9;

	private TabPage tabPage10;

	private TabPage tabPage15;

	private Button btnasSetChrom;

	private Label Ljyq1;

	private Label Ljyq10;

	private GroupBox groupBox5;

	private GroupBox groupBox7;

	private GroupBox groupBox6;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label20;

	private Label label17;

	public LclButton lclButton2;

	public LclButton lclButton1;

	public CheckBox checkBox5;

	public CheckBox checkBox4;

	public CheckBox checkBox6;

	public CheckBox checkBox7;

	public LclTextBox tbcuAmount;

	public LclTextBox tbcuIstdAmount;

	public LclTextBox tbcuInjVolume;

	public LclTextBox tbcuDilution;

	public LclLabel lbcuAmount;

	private GroupBox groupBox8;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem 设为参比峰ToolStripMenuItem;

	private TextBox tbProgram;

	private TextBox tbEvent;

	private IContainer components;

	public LclTextBox tbcclCalibration;

	private SplitContainer spcComponent;

	private ContextMenuStrip cmsCali;

	private ToolStripMenuItem miAddrow;

	private ToolStripMenuItem miColumnsSetup;

	private ToolStripMenuItem miRestoreDftColumns;

	private ToolStripMenuItem miDelRow;

	private Button MethodSave;

	private SplitContainer spcIntegComponent;

	private GroupBox GbCmpds;

	private Button btnMethUse;

	private Button btnMethReset;

	public Button MethodReSave;

	private GroupBox gbMethods;

	private Label label10;

	private Button MethodOpen2;

	private TextBox tbMethName2;

	private Label label4;

	private Button MethodOpen1;

	private TextBox tbMethName1;

	private Button btnAdd1;

	private Button btnAdd2;

	public Button btnMutiPoint;

	public RadioButton radioButton4;

	public RadioButton radioButton7;

	public LclCombineCGridView gvCmpds;

	private GroupBox gbO;

	private TextBox tbSdaFileName;

	private Button sdaOpen;

	private TabPage tabPage5;

	private SplitContainer splitContainer1;

	private Label label21;

	private UCBtnExt btnStartCali;

	private UCBtnExt btnApply;

	private LclDisplayPanel dpgnlChrom;

	private UCNumTextBox ucNTBCnt;

	private TabPage tabPage11;

	private UCBtnExt ucBtnCaliApply;

	private UCDataGridView dGCalibration;

	private UCSplitLabel ucSplitLabel4;

	private Label label23;

	private Label label22;

	private TabPage tabPage12;

	private Chart chartPlots;

	private Panel panel1;

	private Label label34;

	private Label label28;

	private Label label24;

	private Label label26;

	private PrintDocument printDocument1;

	private UCBtnExt ucBtnSite;

	private UCTextBoxEx tbCollectP;

	private UCTextBoxEx tbCollectJYDW;

	private UCTextBoxEx tbCollectSite;

	private UCTextBoxEx tbCollectSJDW;

	private Label label25;

	private UCCombox ucCBSites;

	private UCKeyBorderNum ucKeyBorderNum1;

	private UCBtnExt ucBtnApplly;

	public bool ShowMethodNew
	{
		get
		{
			return bMethodNew;
		}
		set
		{
			bMethodNew = value;
			if (bMethodNew)
			{
				MethodNew.Visible = true;
			}
		}
	}

	public bool ShowOnlineMethod
	{
		get
		{
			return bOnlineMethod;
		}
		set
		{
			bOnlineMethod = value;
			if (bOnlineMethod)
			{
				label1.Visible = false;
				MethodNew.Visible = false;
				MethodReSave.Visible = false;
				MethodSave.Visible = false;
				MethodOpen.Visible = false;
				tbMethName.Visible = false;
				tabPage1.Parent = null;
				tabPage4.Parent = null;
				tabPage19.Parent = null;
				ShowComponentTable = false;
				btnMethReset.Visible = true;
				btnMethUse.Visible = true;
				bUseSet.Location = new Point(186, 7);
				btnMutiPoint.Visible = true;
				btnMutiPoint.Location = new Point(265, 7);
			}
			else
			{
				btnMethReset.Visible = false;
				btnMethUse.Visible = false;
				gbMethods.Visible = false;
			}
		}
	}

	public bool ShowOnlineMethod2
	{
		get
		{
			return bOnlineMethod2;
		}
		set
		{
			bOnlineMethod2 = value;
			if (bOnlineMethod2)
			{
				btnMethReset.Visible = false;
				btnMethUse.Visible = false;
				ShowComponentTable = true;
				tabPage1.Parent = null;
				gbMethods.Visible = false;
				if (frmParam.iOnlineMode == 1)
				{
					tabPage4.Parent = null;
				}
				else if (frmParam.iOnlineMode != 2 && frmParam.iOnlineMode != 3)
				{
				}
				tabPage19.Parent = null;
			}
		}
	}

	public bool ShowComponentTable
	{
		get
		{
			return bShowComponentTable;
		}
		set
		{
			bShowComponentTable = value;
			if (bShowComponentTable)
			{
				spcIntegComponent.Panel2Collapsed = false;
			}
			else
			{
				spcIntegComponent.Panel2Collapsed = true;
			}
		}
	}

	public MtdSetup CurMtdMgr => mtdMgr;

	public InsDeviceManager devManager
	{
		get
		{
			return cdlMgr.CurrentInsDeviceMgr;
		}
		set
		{
			cdlMgr.CurrentInsDeviceMgr = value;
		}
	}

	public PrintPara PrintMethod
	{
		get
		{
			return mtdMgr.printPara;
		}
		set
		{
		}
	}

	[XmlIgnore]
	private CaliGnl caliGnl_0
	{
		get
		{
			return mtdMgr.caliGnl;
		}
		set
		{
			mtdMgr.caliGnl = value;
		}
	}

	public event EventHandler OnDisDpRefresh;

	public event EventHandler OnAddAllCompnent;

	public event EventHandler OnMethodSaveEvent;

	public event EventHandler OnUseSet
	{
		add
		{
			EventHandler eventHandler = this.OnMethodSaveEvent;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref this.OnMethodSaveEvent, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler eventHandler = this.OnMethodSaveEvent;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref this.OnMethodSaveEvent, value2, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	[DllImport("system.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int add(int i);

	[DllImport("system.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void call(int i);

	[DllImport("system.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SetFunCallBack([MarshalAs(UnmanagedType.FunctionPtr)] CallbackFun pCallbackFun);

	public void SetCaliGnl(CaliGnl caliGnl)
	{
		caliGnl_0 = caliGnl;
	}

	public MstSetPortable()
	{
		InitializeComponent();
		selfCtrl = this;
		lclRbNo.Checked = true;
		radioButton1.Checked = false;
		lclRbOuter.Checked = false;
		lclRbInner.Checked = false;
		cbasMatching.SelectedIndex = 0;
		MtdSetup mtdSetup = null;
		if (cdlMgr.CurrentChartParaOpera != null)
		{
			mtdSetup = cdlMgr.CurrentChartParaOpera.mtdMgr;
			if (mtdSetup.sigIntegrations.Count == 0)
			{
				mtdSetup.sigIntegrations = new List<Integration>(1);
				mtdSetup.sigIntegrations[0].Reset();
			}
		}
		else
		{
			mtdSetup = new MtdSetup();
			if (mtdSetup.sigIntegrations.Count > 0)
			{
				mtdSetup.sigIntegrations[0].Reset();
			}
		}
		Loading(mtdSetup);
		InitGvCmpdsTable();
		SetGvCmpdsTableColumnText();
		InitGvCmpdsAllSetting();
		radioButton7.Visible = false;
		ShowComponentTable = false;
		AutoScroll = false;
		tabPage4.AutoScroll = true;
	}

	private void MstSet_Load(object sender, EventArgs e)
	{
		tbMethName1.Text = frmParam.strMethod1;
		tbMethName2.Text = frmParam.strMethod2;
		ucSplitLabel4.Text = "";
		bLoading = false;
	}

	public void Loading(MtdSetup mtd)
	{
		mtdMgr = mtd;
		Init_gvgcProgTemp(gvgcProgTemp);
		gvExtEvTP.ColumnCount = 9;
		for (int i = 1; i < 9; i++)
		{
			gvExtEvTP.Rows.Add(i.ToString(), "", "", "", "");
		}
		if (mtdMgr.sigIntegrations.Count == 0)
		{
			IArrayBase.NewArray(ref mtdMgr.sigIntegrations, 1);
			mtdMgr.sigIntegrations[0] = new Integration();
			mtdMgr.sigIntegrations[0].Reset();
		}
		gvInteg.BorderStyle = BorderStyle.None;
		gvInteg.InitColumns();
		gvInteg.LoadLanguage();
		gvInteg.SetimgContextButton((Bitmap)imageList_0.Images[0]);
		gvInteg.SetimgUnContextButton((Bitmap)imageList_0.Images[1]);
		gvInteg.Refresh(AccStyle.Read, mtdMgr.sigIntegrations[0]);
		if (lclRbNo.Checked)
		{
			TipLable.Text = "不校正按照面积或峰高计算含量百分比。";
		}
		if (lclRbOuter.Checked)
		{
			TipLable.Text = "根据定量组份表中定义的校正因子等参数进行计算  如：校正归一、单点外标、多点外标进行组份浓度计算。";
		}
		if (lclRbInner.Checked)
		{
			TipLable.Text = "根据定量组份表中定义的校正因子等参数进行计算 如：单点内标、多点内标。";
		}
		LoadLanguage();
	}

	public void LoadLanguage()
	{
		btnMutiPoint.Text = Lang.PS("新建多点标定", "New point calibration");
		label10.Text = Lang.PS("非甲烷总烃方法:", "NMHC Method:");
		label4.Text = Lang.PS("苯系物方法:", "MACHs Method:");
		btnAdd1.Text = Lang.PS("加入", "Add");
		btnAdd2.Text = Lang.PS("加入", "Add");
		gbMethods.Text = Lang.PS("子方法", "Submethod");
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			label1.Text = "方法文件";
			tbMethName.Text = "默认";
			MethodNew.Text = "新建";
			MethodReSave.Text = "另存";
			tabControl3.TabPages[0].Text = "方法仪器参数";
			tabControl3.TabPages[1].Text = "定量结果计算";
			tabControl3.TabPages[2].Text = "积分事件";
			groupBox5.Text = "进样器";
			checkBox4.Text = "进样器1";
			checkBox5.Text = "进样器2";
			checkBox6.Text = "检测器2";
			checkBox7.Text = "检测器1";
			groupBox7.Text = "检测器";
			groupBox6.Text = "柱箱";
			groupBox8.Text = "程升及事件";
			lclButton1.Text = "获取仪器参数";
			lclButton2.Text = "下载到仪器";
			gbCalibration.Text = "定量组份表文件";
			btncclSet.Text = "打开";
			btncclNone.Text = "清除";
			btncclView.Text = "查看";
			btncclNew.Text = "根据标样新建";
			gbcuRltTableReport.Text = "结果显示";
			gbadvColumnCalcu.Text = "柱效计算";
			cbrtrHideISTDPeak.Text = "隐藏内标峰";
			rbrtrAllDetectedPeaks.Text = "显示所有检测峰";
			rbrtrIdentifiedPeaks.Text = "显示所有识别峰";
			rbrtrCaliPeaks.Text = "显示所有校正峰";
			lbccUnretainedPeak.Text = "非保留峰时间";
			lbccColumnLength.Text = "柱长";
			rbccStatistical.Text = "静态时间";
			rbccFrom50per.Text = "50%宽起始";
			cbprsUseScaleFactor.Text = "匹配峰使用浓缩(结果乘数)";
			lbcuScaleFactor.Text = "浓缩因子";
			lbcuDilution.Text = "稀释因子";
			lbcuUnitAfterScale.Text = "浓缩后单位";
			lbcuUncalAmtRespF.Text = "未识别峰缩放因子";
			gbadvAddSub.Text = "加减谱图";
			rbasAdd.Text = "加";
			rbasSub.Text = "减";
			lbasChrom.Text = "谱图";
			lbasMatching.Text = "匹配方式";
			btnasNoneChrom.Text = "置空";
			lclGroupBox1.Text = "量值";
			lbcuAmount.Text = "外标浓度";
			lbcuInjVolume.Text = "进样量";
			lbcuIstdAmount.Text = "内标量";
			groupBox18.Text = "积分时间";
			groupBox11.Text = "打印内容";
			label68.Text = "标题";
			ReportTitle.Text = "XXXX分析报告";
			cbRptPrintTime.Text = "打印事件";
			cbRptJYTime.Text = "进样时间";
			cbRptWithSourceData.Text = "工作曲线原始数据";
			cbRptFileName.Text = "文件名";
			cbRptWithResult.Text = "结果数据";
			cbRptBoundary.Text = "谱图加框";
			label30.Text = "字号:";
			cbRptWithChrom.Text = "谱图";
			label56.Text = "宽:";
			label12.Text = "高:";
			cbRptChromBold.Text = "谱线加粗";
			label69.Text = "表格内容———————————————————————";
			cbRptWithIdx.Text = "序号";
			cbRptKeepTime.Text = "保留时间";
			cbRptZuFenName.Text = "组份名称";
			cbRptCorrFactor.Text = "校正因子";
			cbRptWithDensity.Text = "浓度";
			cbRptWithPeakArea.Text = "峰面积";
			cbRptWithPeakHeight.Text = "峰高";
			cbRptWithPeakHalf.Text = "半峰宽";
			cbRptWithPeakSingle.Text = "峰标志";
			cbRptWithDensityPercent.Text = "浓度百分比";
			cbRptWithPeakAreaPercent.Text = "峰面积百分比";
			cbRptWithPeakHeightPercent.Text = "峰高百分比";
			cbRptWithCurve.Text = "工作曲线方程";
			cbRptWithCoef.Text = "相关系数";
			cbRptWithPeakResulution.Text = "峰分离度";
			cbRptWithZhuXiao.Text = "柱效";
			cbRptWithValidNumber.Text = "有效塔板数";
			cbRptWithCapFactor.Text = "容量因子";
			cbRptWithTailFactor.Text = "拖尾因子";
			groupBox15.Text = "报告头";
			rpthead.Text = "质检（E）字第（ \u3000）号\r";
			rpthead.Text += "送样单位：         \u3000\u3000        仪器型号:      \r";
			rpthead.Text += "取样日期：   年  月  日       收样日期：     年  月  日\r";
			rpthead.Text += "样品批号：                       样品名称：固液\r";
			rpthead.Text += "样品罐号：                       仪器控制参数文件：";
			groupBox17.Text = "报告尾";
			rptbotom.Text = "备注：按                 检验，浓度含量单位：g/l\r";
			rptbotom.Text += "检测结果:                检验部门：\r";
			rptbotom.Text += "检验员：                 审核员：";
			groupBox1.Text = "预览";
			radioButton7.Text = "写字板";
			radioButton4.Text = "Word";
			radioButton8.Text = "程序自带";
			cbasMatching.Items.Clear();
			cbasMatching.Items.Add("无变化");
			cbasMatching.Items.Add("偏移谱图");
			cbasMatching.Items.Add("缩放谱图");
			break;
		case SysLanguage.EN:
			if (lclRbNo.Checked)
			{
				TipLable.Text = "No correction according to the area or peak height percentage calculation 。";
			}
			if (lclRbOuter.Checked)
			{
				TipLable.Text = "According to the calculation of quantitative components such as defined in correction factor: normalization, single point external standard, multiple point external standard of component concentration calculation. ";
			}
			if (lclRbInner.Checked)
			{
				TipLable.Text = "According to the calculation of quantitative components such as defined in the correction factor and other parameters: single point of internal standard, multiple internal standard. ";
			}
			label1.Text = "MethodFile";
			tbMethName.Text = "default method";
			MethodNew.Text = "new";
			MethodReSave.Text = "save as";
			bUseSet.Text = "UseSet";
			tabControl3.TabPages[0].Text = "Method/Para";
			tabControl3.TabPages[1].Text = "Quantitative results";
			tabControl3.TabPages[2].Text = "Integration";
			tabControl3.TabPages[3].Text = "Report";
			groupBox5.Text = "INJ";
			checkBox4.Text = "INJ1";
			checkBox5.Text = "INJ2";
			checkBox6.Text = "DEC2";
			checkBox7.Text = "DEC1";
			groupBox7.Text = "DEC";
			groupBox6.Text = "COL";
			groupBox8.Text = "Progam warming/events";
			lclButton1.Text = "Get parameters";
			lclButton2.Text = "Set parameters";
			gbCalibration.Text = "gbCalibration";
			btncclSet.Text = "Open";
			btncclNone.Text = "Clear";
			btncclView.Text = "View";
			btncclNew.Text = "New";
			gbcuRltTableReport.Text = "Result";
			gbadvColumnCalcu.Text = "ColumnCalcu";
			cbrtrHideISTDPeak.Text = "HideISTDPeak";
			rbrtrAllDetectedPeaks.Text = "AllDetectedPeaks";
			rbrtrIdentifiedPeaks.Text = "IdentifiedPeaks";
			rbrtrCaliPeaks.Text = "CaliPeaks";
			lbccUnretainedPeak.Text = "UnretainedPeak";
			lbccColumnLength.Text = "ColumnLength";
			rbccStatistical.Text = "Statistical";
			rbccFrom50per.Text = "From50per";
			cbprsUseScaleFactor.Text = "UseScaleFactor";
			lbcuScaleFactor.Text = "ScaleFactor";
			lbcuDilution.Text = "Dilution";
			lbcuUnitAfterScale.Text = "UnitAfterScale";
			lbcuUncalAmtRespF.Text = "UncalAmtRespF";
			gbadvAddSub.Text = "AddSub";
			rbasAdd.Text = "Add";
			rbasSub.Text = "Sub";
			lbasChrom.Text = "Chrom";
			lbasMatching.Text = "Matching";
			btnasNoneChrom.Text = "NoneChrom";
			lclGroupBox1.Text = "summary";
			lbcuAmount.Text = "Amount";
			lbcuInjVolume.Text = "InjVolume";
			lbcuIstdAmount.Text = "IstdVolume";
			groupBox18.Text = "Integration";
			groupBox11.Text = "PrintContent";
			label68.Text = "Title";
			ReportTitle.Text = "XXXXRePort";
			cbRptPrintTime.Text = "PrintTime";
			cbRptJYTime.Text = "SamplingTime";
			cbRptWithSourceData.Text = "original data";
			cbRptFileName.Text = "FileName";
			cbRptWithResult.Text = "RData";
			cbRptBoundary.Text = "drawFrame";
			label30.Text = "Font:";
			cbRptWithChrom.Text = "chromatogram";
			label56.Text = "W:";
			label12.Text = "H:";
			cbRptChromBold.Text = "SpectralLine";
			label69.Text = "Table Content—————————————————————";
			cbRptWithIdx.Text = "Index";
			cbRptKeepTime.Text = "Rt";
			cbRptZuFenName.Text = "PkName";
			cbRptCorrFactor.Text = "factor";
			cbRptWithDensity.Text = "Amount";
			cbRptWithPeakArea.Text = "Area";
			cbRptWithPeakHeight.Text = "Height";
			cbRptWithPeakHalf.Text = "WO5";
			cbRptWithPeakSingle.Text = "PkV";
			cbRptWithDensityPercent.Text = "AmountPer";
			cbRptWithPeakAreaPercent.Text = "AreaPer";
			cbRptWithPeakHeightPercent.Text = "HeightPer";
			cbRptWithCurve.Text = "equation";
			cbRptWithCoef.Text = "correlation";
			cbRptWithPeakResulution.Text = "ResolutionEP";
			cbRptWithZhuXiao.Text = "Efficiency";
			cbRptWithValidNumber.Text = "effective";
			cbRptWithCapFactor.Text = "capacityFactor";
			cbRptWithTailFactor.Text = "tailingFactor";
			groupBox15.Text = "Report Title";
			rpthead.Text = "Sample units：         \u3000\u3000        Instrument Type:      \r";
			rpthead.Text += "Date：              datereceived：      \r";
			rpthead.Text += "Sample number：                       Sample Names：\r";
			rpthead.Text += "sample tank No：                       ";
			groupBox17.Text = "Report tail";
			rptbotom.Text = "remark： \r";
			groupBox1.Text = "PreView";
			radioButton7.Text = "Wordpad";
			radioButton4.Text = "Word";
			radioButton8.Text = "NetChromPrint";
			cbasMatching.Items.Clear();
			cbasMatching.Items.Add("No Change");
			cbasMatching.Items.Add("Offset Chrom");
			cbasMatching.Items.Add("Scale Chrom");
			tbcuIstdAmount.Text = "results table modification";
			miIntegAppendRow.Text = "Add Line";
			miIntegDeleteRows.Text = "Insert Line";
			miIntegInsertRow.Text = "Delete Line";
			miIntegResetRows.Text = "Up";
			miIntegRowsUp.Text = "Reset";
			miIntegRowsDown.Text = "Down";
			break;
		}
	}

	private void InitChart()
	{
		chartPlots.ContextMenuStrip = contextMenuStrip1;
		chartPlots.Series.Clear();
		chartPlots.ChartAreas[0].AxisX.MajorGrid.Interval = 10.0;
		chartPlots.ChartAreas[0].AxisY.MajorGrid.Interval = 10.0;
		chartPlots.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
		chartPlots.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
		chartPlots.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
		chartPlots.ChartAreas[0].AxisX.IsMarginVisible = true;
		chartPlots.ChartAreas[0].AxisX.Title = "浓度";
		chartPlots.ChartAreas[0].AxisX.TitleForeColor = Color.Crimson;
		chartPlots.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "F" + Class49.int_8;
		chartPlots.ChartAreas[0].AxisX.Maximum = 100.0;
		chartPlots.ChartAreas[0].AxisX.Minimum = 0.0;
		chartPlots.ChartAreas[0].AxisY.Title = "面积";
		chartPlots.ChartAreas[0].AxisY.TitleForeColor = Color.Crimson;
		chartPlots.ChartAreas[0].AxisY.Maximum = 100.0;
		chartPlots.ChartAreas[0].AxisY.Minimum = 0.0;
		chartPlots.ChartAreas[0].AxisY.LabelStyle.Format = "F" + Class49.int_8;
		chartPlots.ChartAreas[0].CursorX.IsUserEnabled = true;
		chartPlots.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
		chartPlots.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
		chartPlots.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
		for (int i = 0; i < serieLines.Length; i++)
		{
			if (serieLines[i] == null)
			{
				serieLines[i] = new Series();
			}
			serieLines[i].ChartType = SeriesChartType.Line;
			serieLines[i].IsVisibleInLegend = true;
			serieLines[i].IsValueShownAsLabel = false;
			serieLines[i].XValueType = ChartValueType.String;
			serieLines[i].YValueType = ChartValueType.Double;
			serieLines[i].Color = strSerialColor[i];
			serieLines[i].MarkerStyle = MarkerStyle.Circle;
			serieLines[i].MarkerSize = 2;
			serieLines[i].LegendText = strSerialName[i];
			chartPlots.Series.Add(serieLines[i]);
		}
	}

	public void loadDefaultMethod(string strFileName)
	{
		if (!File.Exists(strFileName))
		{
			MessageBox.Show("请检查方法文件");
			return;
		}
		mtdMgr = new MtdSetup();
		mtdMgr.chromInfo = new ChromInfo();
		Clear();
		LoadMethodFile(strFileName);
		lclButton2.Enabled = true;
		string directoryName = Path.GetDirectoryName(strFileName);
		sysParam.strMtdDataFileDir = directoryName;
		sysParam.SaveParam();
		if (caliGnl_0 != null)
		{
			for (byte b = 0; b < caliGnl_0.cmpds.Length; b++)
			{
				float respFactor = caliGnl_0.cmpds[b].levels[0].respFactor;
				if (respFactor == 0f)
				{
					caliGnl_0.CalculateFunc(appendLink: false);
				}
				respFactor = caliGnl_0.cmpds[b].levels[0].respFactor;
				gvCmpds.Rows[b].Cells["FreeRespFactor"].Value = respFactor;
			}
		}
		if (this.OnMethodSaveEvent != null)
		{
			this.OnMethodSaveEvent(this, new EventArgs());
		}
	}

	public void ChangeMst()
	{
		MtdSetup mtdSetup = null;
		if (cdlMgr.CurrentChartParaOpera != null)
		{
			mtdSetup = cdlMgr.CurrentChartParaOpera.mtdMgr;
			if (mtdSetup.sigIntegrations.Count > 0 && mtdSetup.sigIntegrations[0].IntegRows.Length == 0)
			{
				mtdSetup.sigIntegrations[0].Reset();
			}
		}
		else
		{
			mtdSetup = new MtdSetup();
			mtdSetup.sigIntegrations[0].Reset();
		}
		Clear();
		InitIntegTable();
		ReadFromMtdMgr(mtdSetup);
		if (devManager != null && mtdMgr.strMtdFilePath.Trim() != "")
		{
			int currentChannelIndex = thisFormMf.CurrentChannelIndex;
			Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, thisFormMf.CurrentGCID, "绑定新方法", "通道:" + currentChannelIndex + " 绑定新方法:" + mtdMgr.strMtdFilePath);
		}
	}

	public void RefreshPara()
	{
		try
		{
			ReadFromPrintPara();
			ReadFromMtdMgr(mtdMgr);
			InitIntegTable();
		}
		catch
		{
		}
	}

	private string method_3(int int_1)
	{
		string text = "";
		EpcDeviceSetting epcDeviceSetting = devManager.epcDev1[int_1 * 3];
		if (epcDeviceSetting != null)
		{
			string text2 = Lang.PS("  H2流量", "  H2 flow");
			text = text + text2 + epcDeviceSetting.pressureData + " sccm\r";
		}
		epcDeviceSetting = devManager.epcDev1[int_1 * 3 + 1];
		if (epcDeviceSetting != null)
		{
			string text3 = Lang.PS("  Air流量", "  Air flow");
			text = text + text3 + epcDeviceSetting.pressureData + " sccm\r";
		}
		epcDeviceSetting = devManager.epcDev1[int_1 * 3 + 2];
		if (epcDeviceSetting != null)
		{
			string text4 = Lang.PS("  尾吹流量", "  Make_up flow");
			text = text + text4 + epcDeviceSetting.pressureData + " sccm\r";
		}
		if (epcDeviceSetting != null)
		{
			string text5 = Lang.PS("  尾吹气:", "  Make_up:");
			switch (epcDeviceSetting.gasType & 0xF)
			{
			case 0:
				text = text + text5 + "N2\r";
				break;
			case 1:
				text = text + text5 + "H2\r";
				break;
			case 2:
				text = text + text5 + "Air\r";
				break;
			case 3:
				text = text + text5 + "He\r";
				break;
			case 4:
				text = text + text5 + "Argon\r";
				break;
			}
		}
		return text;
	}

	private void method_4(LclGridView lclGridView_0, InsDeviceManager class56_1)
	{
		lbptInitT.Text = class56_1.tempSetedList[1].ToString("0.0");
		tbptIniTempHoldT.Text = class56_1.tempHoldTime.ToString("0.0");
		int num = Math.Min(class56_1.tempSettingList.Count, lclGridView_0.Rows.Count);
		for (int i = 0; i < num; i++)
		{
			lclGridView_0.Rows[i].Cells[0].Value = class56_1.tempSettingList[i].tempStart.ToString();
			lclGridView_0.Rows[i].Cells[1].Value = class56_1.tempSettingList[i].tempEnd.ToString();
			lclGridView_0.Rows[i].Cells[2].Value = class56_1.tempSettingList[i].tempKeep.ToString();
		}
	}

	private void method_5(InsDeviceManager class56_1)
	{
		int index = 0;
		switch (tabControl2.SelectedIndex)
		{
		case 0:
			if (radioButton9.Checked)
			{
				index = 0;
			}
			if (radioButton2.Checked)
			{
				index = 1;
			}
			if (radioButton3.Checked)
			{
				index = 2;
			}
			break;
		case 1:
			if (radioButton9.Checked)
			{
				index = 3;
			}
			if (radioButton2.Checked)
			{
				index = 4;
			}
			if (radioButton3.Checked)
			{
				index = 5;
			}
			break;
		case 2:
			if (radioButton9.Checked)
			{
				index = 6;
			}
			if (radioButton9.Checked)
			{
				index = 7;
			}
			if (radioButton3.Checked)
			{
				index = 8;
			}
			break;
		case 3:
			if (radioButton9.Checked)
			{
				index = 9;
			}
			if (radioButton2.Checked)
			{
				index = 10;
			}
			if (radioButton3.Checked)
			{
				index = 11;
			}
			break;
		case 4:
			if (radioButton9.Checked)
			{
				index = 12;
			}
			if (radioButton2.Checked)
			{
				index = 13;
			}
			if (radioButton3.Checked)
			{
				index = 14;
			}
			break;
		case 5:
			if (radioButton9.Checked)
			{
				index = 15;
			}
			if (radioButton2.Checked)
			{
				index = 16;
			}
			if (radioButton3.Checked)
			{
				index = 17;
			}
			break;
		}
		EpcDeviceSetting epcDeviceSetting = class56_1.epcDev1[index];
		if (epcDeviceSetting != null)
		{
			switch (epcDeviceSetting.gasType & 0xF)
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
				comboBox6.Text = Lang.PS("氩气", "Argon");
				break;
			}
			maskedTextBox8.Text = epcDeviceSetting.chromColLenth.ToString();
			maskedTextBox9.Text = epcDeviceSetting.chromColDiameter.ToString();
			maskedTextBox5.Text = epcDeviceSetting.initTime.ToString();
			if (epcDeviceSetting.ctrlModel == 0)
			{
				radioButton6.Checked = true;
				label18.Text = Lang.PS("压力", "Press");
				maskedTextBox4.Text = epcDeviceSetting.pressureData.ToString();
			}
			if (epcDeviceSetting.ctrlModel == 1)
			{
				radioButton5.Checked = true;
				label18.Text = Lang.PS("流量", "flow");
				maskedTextBox4.Text = epcDeviceSetting.pressureData.ToString();
			}
			if (epcDeviceSetting.ctrlModel == 2)
			{
				radioButton25.Checked = true;
				label18.Text = Lang.PS("分流", "split");
				maskedTextBox4.Text = epcDeviceSetting.pressureData.ToString();
			}
			dgInsamp1.Rows.Clear();
			for (int i = 0; i < epcDeviceSetting.tempSettingTable.Count; i++)
			{
				dgInsamp1.Rows.Add((i + 1).ToString(), epcDeviceSetting.tempSettingTable[i].tempStart.ToString(), epcDeviceSetting.tempSettingTable[i].tempEnd.ToString(), epcDeviceSetting.tempSettingTable[i].tempKeep.ToString());
			}
		}
	}

	private void Init_gvgcProgTemp(LclGridView dgvPT)
	{
		LclgvTextBoxColumn lclgvTextBoxColumn = dgvPT.AddLclTextBoxColumn("UpRate", 100, 3, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn.HeaderText = Lang.PS("升温[℃/min]", "UpRate[℃/min]");
		lclgvTextBoxColumn.ValueType = typeof(float);
		lclgvTextBoxColumn.Width = 60;
		lclgvTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		lclgvTextBoxColumn.DefaultCellStyle.Format = "f1";
		lclgvTextBoxColumn = dgvPT.AddLclTextBoxColumn("EndTemp", 100, 3, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn.HeaderText = Lang.PS("终温[℃]", "End Temp[℃]");
		lclgvTextBoxColumn.ValueType = typeof(float);
		lclgvTextBoxColumn.Width = 60;
		lclgvTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		lclgvTextBoxColumn.DefaultCellStyle.Format = "f1";
		lclgvTextBoxColumn = dgvPT.AddLclTextBoxColumn("HoldTime", 100, 3, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn.HeaderText = Lang.PS("保持[min]", "Hold T[min]");
		lclgvTextBoxColumn.ValueType = typeof(float);
		lclgvTextBoxColumn.Width = 60;
		lclgvTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		lclgvTextBoxColumn.DefaultCellStyle.Format = "f1";
		dgvPT.RowCount = 16;
	}

	private void InitIntegTable()
	{
		gvInteg.BorderStyle = BorderStyle.None;
		gvInteg.InitColumns();
		gvInteg.LoadLanguage();
		gvInteg.SetimgContextButton((Bitmap)imageList_0.Images[0]);
		gvInteg.SetimgUnContextButton((Bitmap)imageList_0.Images[1]);
		gvInteg.Refresh(AccStyle.Read, mtdMgr.sigIntegrations[0]);
	}

	private void miIntegAppendRow_Click(object sender, EventArgs e)
	{
		gvInteg.RowCount++;
	}

	private void miIntegInsertRow_Click(object sender, EventArgs e)
	{
		gvInteg.DeleteSelectedRows();
	}

	private void miIntegDeleteRows_Click(object sender, EventArgs e)
	{
		int num = gvInteg.Rows.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (gvInteg.Rows[num2].Selected)
			{
				num = num2;
			}
		}
		gvInteg.Rows.Insert(num, 1);
	}

	private void miIntegRowsUp_Click(object sender, EventArgs e)
	{
		gvInteg.Refresh(AccStyle.Clear, null);
		Integration integration = new Integration();
		integration.Reset();
		gvInteg.Refresh(AccStyle.Read, integration);
	}

	private void miIntegRowsDown_Click(object sender, EventArgs e)
	{
		gvInteg.AdjustSelectedRows(AdjustUpDown.Down);
	}

	private void miIntegResetRows_Click(object sender, EventArgs e)
	{
		gvInteg.AdjustSelectedRows(AdjustUpDown.Up);
	}

	private void SetCalFileName(ChromInfo chromInfo, string strFilePath)
	{
		chromInfo.cclCalibration = strFilePath;
		chromInfo.cclShowName = Path.GetFileName(strFilePath);
		tbcclCalibration.Text = chromInfo.cclShowName;
		FileInfo fileInfo = new FileInfo(strFilePath);
		sysParam.strCalDataFileDir = fileInfo.DirectoryName;
		sysParam.SaveParam();
	}

	private void btncclSet_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdMgr.chromInfo;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = btncclSet.Text;
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("全部文件") + "(*.cal,*.sda)|*.cal;*.sda|" + Lang.PS("组份文件") + "(*.cal)|*.cal|" + Lang.PS("谱图文件") + "(*.sda)|*.sda";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			string fileName = openFileDialog.FileName;
			string extension = Path.GetExtension(fileName);
			if (extension == ".cal")
			{
				OpenCaliGnlFile(fileName);
			}
			else
			{
				OpenChromFile(fileName);
			}
		}
	}

	public void Clear()
	{
		mtdMgr = new MtdSetup();
		tbMethName.Text = Lang.PS("默认", "default method");
		lclRbNo.Checked = true;
		cbrtrHideISTDPeak.Checked = false;
		rbrtrAllDetectedPeaks.Checked = true;
		btnasNoneChrom_Click(null, null);
		miIntegRowsUp_Click(null, null);
		tbcclCalibration.Text = "";
		lbptInitT.Text = "";
		for (int i = 0; i < dgvCT6.RowCount; i++)
		{
			dgvCT6.Rows[i].Cells[1].Value = "";
		}
		for (int j = 0; j < gvgcProgTemp.RowCount; j++)
		{
			gvgcProgTemp.Rows[j].Cells[0].Value = "";
			gvgcProgTemp.Rows[j].Cells[1].Value = "";
			gvgcProgTemp.Rows[j].Cells[2].Value = "";
		}
		tbptIniTempHoldT.Text = "?";
		for (int k = 0; k < gvExtEvTP.RowCount; k++)
		{
			gvExtEvTP.Rows[k].Cells[1].Value = "";
			gvExtEvTP.Rows[k].Cells[2].Value = "";
			gvExtEvTP.Rows[k].Cells[3].Value = "";
			gvExtEvTP.Rows[k].Cells[4].Value = "";
		}
		maskedTextBox4.Text = "000.00";
		maskedTextBox5.Text = "000.0";
		maskedTextBox9.Text = "000.00";
		maskedTextBox8.Text = "0.00";
		for (int l = 0; l < dgInsamp1.RowCount; l++)
		{
			dgInsamp1.Rows[l].Cells[1].Value = "";
			dgInsamp1.Rows[l].Cells[2].Value = "";
			dgInsamp1.Rows[l].Cells[3].Value = "";
		}
		Ljyq1.Text = "";
		Ljyq10.Text = "";
		label14.Text = "";
		label15.Text = "";
		label16.Text = "";
		checkBox4.Checked = false;
		checkBox5.Checked = false;
		checkBox6.Checked = false;
		checkBox7.Checked = false;
		tbcuAmount.Text = "";
		tbcuInjVolume.Text = "";
		tbcuDilution.Text = "";
		tbprsScaleFactor.Text = "1";
		tbcuDilution.Text = "1";
		lclRbNo.Checked = true;
		radioButton1.Checked = false;
		lclRbOuter.Checked = false;
		lclRbInner.Checked = false;
		ClearComponentData();
	}

	private void btncclNew_Click(object sender, EventArgs e)
	{
		CaliGnlForm caliGnlForm = new CaliGnlForm();
		caliGnlForm.Show();
	}

	private void btncclNone_Click(object sender, EventArgs e)
	{
		mtdMgr.chromInfo = new ChromInfo();
		tbcclCalibration.Text = "";
		mtdMgr.caliGnl = null;
	}

	private void btncclView_Click(object sender, EventArgs e)
	{
		if (mtdMgr.caliGnl == null)
		{
			return;
		}
		CaliGnlForm caliGnlForm = new CaliGnlForm();
		if (caliGnlForm.Visible)
		{
			if (caliGnlForm.WindowState == FormWindowState.Minimized)
			{
				caliGnlForm.WindowState = FormWindowState.Normal;
			}
			caliGnlForm.BringToFront();
		}
		else
		{
			caliGnlForm.Show();
		}
		ChromInfo chromInfo = mtdMgr.chromInfo;
		string cclCalibration = chromInfo.cclCalibration;
		if (cclCalibration != "")
		{
			if (!File.Exists(cclCalibration))
			{
				MessageBox.Show(Lang.PS("文件无效", "file Invalid"));
			}
			else
			{
				caliGnlForm.LoadFile(cclCalibration);
			}
		}
	}

	private void ReadFromInsDevEnable()
	{
		checkBox4.Checked = mtdMgr.insDevEnable0;
		checkBox5.Checked = mtdMgr.insDevEnable1;
		checkBox7.Checked = mtdMgr.insDevEnable2;
		checkBox6.Checked = mtdMgr.insDevEnable3;
	}

	private void WriteToInsDevEnable()
	{
		mtdMgr.insDevEnable0 = checkBox4.Checked;
		mtdMgr.insDevEnable1 = checkBox5.Checked;
		mtdMgr.insDevEnable2 = checkBox7.Checked;
		mtdMgr.insDevEnable3 = checkBox6.Checked;
	}

	private void ReadFromChromInfo()
	{
		ChromInfo chromInfo = mtdMgr.chromInfo;
		tbcclCalibration.Text = mtdMgr.chromInfo.cclShowName;
		switch (mtdMgr.chromInfo.cclCalcu)
		{
		case CalcuStyle.Uncal:
			lclRbNo.Checked = true;
			radioButton1.Checked = false;
			lclRbOuter.Checked = false;
			lclRbInner.Checked = false;
			break;
		case CalcuStyle.ESTD:
			if (!(mtdMgr.chromInfo.msmMtdDspt == "校正归一") && !(mtdMgr.chromInfo.msmMtdDspt == "Normalization"))
			{
				lclRbNo.Checked = false;
				radioButton1.Checked = false;
				lclRbOuter.Checked = true;
				lclRbInner.Checked = false;
			}
			else
			{
				lclRbNo.Checked = false;
				radioButton1.Checked = true;
				lclRbOuter.Checked = false;
				lclRbInner.Checked = false;
			}
			break;
		case CalcuStyle.ISTD:
			lclRbNo.Checked = false;
			radioButton1.Checked = false;
			lclRbOuter.Checked = false;
			lclRbInner.Checked = true;
			break;
		}
		cbrtrHideISTDPeak.Checked = mtdMgr.chromInfo.rtrHideISTDPeak;
		switch (mtdMgr.chromInfo.rtrRltReportPeaks)
		{
		case RltReportPeaks.AllDetectedPeaks:
			rbrtrAllDetectedPeaks.Checked = true;
			break;
		case RltReportPeaks.IdentifiedPeaks:
			rbrtrIdentifiedPeaks.Checked = true;
			break;
		case RltReportPeaks.CaliPeaks:
			rbrtrCaliPeaks.Checked = true;
			break;
		}
		cbprsUseScaleFactor.Checked = mtdMgr.chromInfo.prsUseScaleFactor;
		tbprsScaleFactor.Text = mtdMgr.chromInfo.prsScaleFactor.ToString();
		tbprsUnitAfterScale.Text = mtdMgr.chromInfo.prsUnitAfterScale;
		cbprsUncalBase.SelectedIndex = (int)mtdMgr.chromInfo.prsUncalBase;
		tbprsUncalAmtRespF.Text = mtdMgr.chromInfo.prsUncalAmtRespF.ToString();
		tbasChrom.Text = mtdMgr.chromInfo.asShowName;
		rbasAdd.Checked = mtdMgr.chromInfo.addChrom;
		rbasSub.Checked = !mtdMgr.chromInfo.addChrom;
		if (mtdMgr.chromInfo.asMatching > (ASMatchStyle)4)
		{
			cbasMatching.SelectedIndex = 0;
		}
		else
		{
			cbasMatching.SelectedIndex = (int)mtdMgr.chromInfo.asMatching;
		}
		tbccUnretainedPeak.Text = mtdMgr.chromInfo.ccColumnUT.ToString();
		tbccColumnLength.Text = mtdMgr.chromInfo.ccColumnLength.ToString();
		switch (mtdMgr.chromInfo.ccStyle)
		{
		case ColumnCalcuStyle.Statistical:
			rbccStatistical.Checked = true;
			break;
		case ColumnCalcuStyle.From50per:
			rbccFrom50per.Checked = true;
			break;
		}
		tbcuAmount.Text = mtdMgr.chromInfo.amount.ToString();
		tbcuIstdAmount.Text = mtdMgr.chromInfo.GetIstdAmount(0).ToString();
		tbcuInjVolume.Text = mtdMgr.chromInfo.injVolumn.ToString();
		tbcuDilution.Text = mtdMgr.chromInfo.dilution.ToString();
	}

	private void WriteToChromInfo()
	{
		if (lclRbNo.Checked)
		{
			mtdMgr.chromInfo.cclCalcu = CalcuStyle.Uncal;
		}
		if (radioButton1.Checked)
		{
			mtdMgr.chromInfo.cclCalcu = CalcuStyle.ESTD;
			mtdMgr.chromInfo.msmMtdDspt = Lang.PS("校正归一", "Normalization");
		}
		if (lclRbOuter.Checked)
		{
			mtdMgr.chromInfo.cclCalcu = CalcuStyle.ESTD;
			mtdMgr.chromInfo.msmMtdDspt = Lang.PS("作者", "author");
		}
		if (lclRbInner.Checked)
		{
			mtdMgr.chromInfo.cclCalcu = CalcuStyle.ISTD;
		}
		mtdMgr.chromInfo.rtrHideISTDPeak = cbrtrHideISTDPeak.Checked;
		if (rbrtrAllDetectedPeaks.Checked)
		{
			mtdMgr.chromInfo.rtrRltReportPeaks = RltReportPeaks.AllDetectedPeaks;
		}
		else if (rbrtrIdentifiedPeaks.Checked)
		{
			mtdMgr.chromInfo.rtrRltReportPeaks = RltReportPeaks.IdentifiedPeaks;
		}
		else if (rbrtrCaliPeaks.Checked)
		{
			mtdMgr.chromInfo.rtrRltReportPeaks = RltReportPeaks.CaliPeaks;
		}
		mtdMgr.chromInfo.prsUseScaleFactor = cbprsUseScaleFactor.Checked;
		mtdMgr.chromInfo.prsScaleFactor = Class49.String2Float(tbprsScaleFactor.Text, mtdMgr.chromInfo.prsScaleFactor);
		mtdMgr.chromInfo.prsUnitAfterScale = tbprsUnitAfterScale.Text;
		if (cbprsUncalBase.SelectedIndex > 0)
		{
			mtdMgr.chromInfo.prsUncalBase = (RespStyle)cbprsUncalBase.SelectedIndex;
		}
		mtdMgr.chromInfo.prsUncalAmtRespF = Class49.String2Float(tbprsUncalAmtRespF.Text, mtdMgr.chromInfo.prsUncalAmtRespF);
		mtdMgr.chromInfo.addChrom = rbasAdd.Checked;
		if (cbasMatching.SelectedIndex > 0)
		{
			mtdMgr.chromInfo.asMatching = (ASMatchStyle)cbasMatching.SelectedIndex;
		}
		mtdMgr.chromInfo.ccColumnUT = Class49.String2Float(tbccUnretainedPeak.Text, mtdMgr.chromInfo.ccColumnUT);
		mtdMgr.chromInfo.ccColumnLength = Class49.String2Float(tbccColumnLength.Text, mtdMgr.chromInfo.ccColumnLength);
		if (rbccStatistical.Checked)
		{
			mtdMgr.chromInfo.ccStyle = ColumnCalcuStyle.Statistical;
		}
		else if (rbccFrom50per.Checked)
		{
			mtdMgr.chromInfo.ccStyle = ColumnCalcuStyle.From50per;
		}
		mtdMgr.chromInfo.amount = Class49.String2Float(tbcuAmount.Text, 0f);
		mtdMgr.chromInfo.SetIstdAmountIndex(0, Class49.String2Float(tbcuIstdAmount.Text, 0f));
		mtdMgr.chromInfo.injVolumn = Class49.String2Float(tbcuInjVolume.Text, 0f);
		if (tbcuDilution.Text.Trim() == "")
		{
			tbcuDilution.Text = "1";
		}
		if (tbcuDilution.Text.Trim() == "0")
		{
			tbcuDilution.Text = "1";
		}
		mtdMgr.chromInfo.dilution = Class49.String2Float(tbcuDilution.Text, 1f);
	}

	public void ReadFromMtdMgr(MtdSetup mtd)
	{
		mtdMgr = mtd;
		tbMethName.Text = mtdMgr.strMtdShowName;
		RefreshIntegrations(AccStyle.Read);
		ReadFromInsDevEnable();
		ReadFromChromInfo();
		try
		{
			ReadFromPrintPara();
		}
		catch
		{
		}
		ReadFromTempSetedList();
		ReadComponentData();
		try
		{
			if (FormKR.selfCtrl != null)
			{
				for (int i = 0; i < cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Length && i < FormKR.selfCtrl.strCompName.Length; i++)
				{
					FormKR.selfCtrl.strCompName[i] = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[i].cmpdInfo.name;
				}
			}
			if (frmParam.kindMachine == 4)
			{
				if (VocCtrl.vocCtrl == null)
				{
					return;
				}
				for (int j = 0; j < VocCtrl.vocCtrl.strCompName.Length; j++)
				{
					VocCtrl.vocCtrl.strCompName[j] = "";
				}
				if (cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl != null)
				{
					for (int k = 0; k < VocCtrl.vocCtrl.strCompName.Length && k < cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Length; k++)
					{
						VocCtrl.vocCtrl.strCompName[k] = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[k].cmpdInfo.name;
					}
				}
			}
			else
			{
				if (cdlMgr.ChartParaOperaList.Count <= 1 || VocCtrl.vocCtrl == null)
				{
					return;
				}
				for (int l = 0; l < VocCtrl.vocCtrl.strCompName.Length; l++)
				{
					VocCtrl.vocCtrl.strCompName[l] = "";
				}
				if (cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl != null)
				{
					for (int m = 0; m < VocCtrl.vocCtrl.strCompName.Length && m < cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds.Length; m++)
					{
						VocCtrl.vocCtrl.strCompName[m] = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[m].cmpdInfo.name;
					}
				}
			}
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("组份名字赋值到前台：" + ex.Message);
		}
	}

	public void ReadFromMtdMgr()
	{
		if (mtdMgr != null)
		{
			tbMethName.Text = Path.GetFileNameWithoutExtension(mtdMgr.strMtdShowName);
			RefreshIntegrations(AccStyle.Read);
			ReadFromInsDevEnable();
			ReadFromChromInfo();
			ReadFromPrintPara();
			ReadFromTempSetedList();
			ReadComponentData();
		}
	}

	public void WriteToMtdMgr()
	{
		RefreshIntegrations(AccStyle.Write);
		WriteToInsDevEnable();
		WriteToChromInfo();
		WriteToPrintPara();
		WriteComponentData();
		WriteToPrintParaOld();
		mtdMgr.printPara = printParaOld;
	}

	public void ReadFromPrintPara()
	{
		try
		{
			if (mtdMgr.printPara != null)
			{
				printParaOld = mtdMgr.printPara;
			}
			ReportTitle.Text = printParaOld.Title;
			rpthead.Text = printParaOld.PrintTitleTop;
			rptbotom.Text = printParaOld.PrintTitleBotom;
			if (printParaOld.PPreView == PrintPreview.写字板)
			{
				printParaOld.PPreView = PrintPreview.程序自带;
			}
			switch (printParaOld.PPreView)
			{
			case PrintPreview.写字板:
				radioButton7.Checked = true;
				break;
			case PrintPreview.Word:
				radioButton4.Checked = true;
				break;
			case PrintPreview.程序自带:
				radioButton8.Checked = true;
				break;
			}
			cbRptPrintTime.Checked = printParaOld.BPTime;
			cbRptJYTime.Checked = printParaOld.BJtime;
			cbRptFileName.Checked = printParaOld.Bfname;
			cbRptWithResult.Checked = printParaOld.BRdata;
			cbRptWithSourceData.Checked = printParaOld.BSouredata;
			cbRptWithChrom.Checked = printParaOld.Bpic;
			numericUpDown1.Value = printParaOld.Bpicwidth;
			numericUpDown3.Value = printParaOld.Bpicheight;
			cbRptBoundary.Checked = printParaOld.BPicBound;
			cbRptChromBold.Checked = printParaOld.BPicLineB;
			numericUpDown2.Value = printParaOld.BPicFontSize;
			cbRptWithIdx.Checked = printParaOld.BIndex;
			cbRptKeepTime.Checked = printParaOld.BPTime;
			cbRptZuFenName.Checked = printParaOld.BPeakName;
			cbRptCorrFactor.Checked = printParaOld.BPeakPara;
			cbRptWithDensity.Checked = printParaOld.BPeakAmont;
			cbRptWithDensityPercent.Checked = printParaOld.BPeakAmontPer;
			cbRptWithPeakArea.Checked = printParaOld.BPeakArea;
			cbRptWithPeakAreaPercent.Checked = printParaOld.BPeakAreaPer;
			cbRptWithPeakHeight.Checked = printParaOld.BPeakheight;
			cbRptWithPeakHeightPercent.Checked = printParaOld.BPeakheightPer;
			cbRptWithPeakHalf.Checked = printParaOld.BPeakHalfheight;
			cbRptWithPeakSingle.Checked = printParaOld.BPeakV;
			cbRptWithCurve.Checked = printParaOld.BPeakFx;
			cbRptWithCoef.Checked = printParaOld.BPeakOtherPara;
			cbRptWithPeakResulution.Checked = printParaOld.BPeakLV;
			cbRptWithZhuXiao.Checked = printParaOld.BPeakTBPara;
			cbRptWithValidNumber.Checked = printParaOld.BPeakUTBPara;
			cbRptWithCapFactor.Checked = printParaOld.BPeakLPara;
			cbRptWithTailFactor.Checked = printParaOld.BPeaktailPara;
		}
		catch (Exception)
		{
		}
	}

	public void WriteToPrintPara()
	{
		if (mtdMgr.printPara == null)
		{
			mtdMgr.printPara = new PrintPara();
		}
		mtdMgr.printPara.Title = ReportTitle.Text.Trim();
		mtdMgr.printPara.PrintTitleTop = rpthead.Text.Trim();
		mtdMgr.printPara.PrintTitleBotom = rptbotom.Text.Trim();
		if (radioButton7.Checked)
		{
			mtdMgr.printPara.PPreView = PrintPreview.写字板;
		}
		if (radioButton4.Checked)
		{
			mtdMgr.printPara.PPreView = PrintPreview.Word;
		}
		if (radioButton8.Checked)
		{
			mtdMgr.printPara.PPreView = PrintPreview.程序自带;
		}
		mtdMgr.printPara.PPreView = PrintPreview.程序自带;
		mtdMgr.printPara.BPTime = cbRptPrintTime.Checked;
		mtdMgr.printPara.BJtime = cbRptJYTime.Checked;
		mtdMgr.printPara.Bfname = cbRptFileName.Checked;
		mtdMgr.printPara.BRdata = cbRptWithResult.Checked;
		mtdMgr.printPara.BSouredata = cbRptWithSourceData.Checked;
		mtdMgr.printPara.Bpic = cbRptWithChrom.Checked;
		mtdMgr.printPara.Bpicwidth = (int)numericUpDown1.Value;
		mtdMgr.printPara.Bpicheight = (int)numericUpDown3.Value;
		mtdMgr.printPara.BPicBound = cbRptBoundary.Checked;
		mtdMgr.printPara.BPicLineB = cbRptChromBold.Checked;
		mtdMgr.printPara.BPicFontSize = (int)numericUpDown2.Value;
		mtdMgr.printPara.BIndex = cbRptWithIdx.Checked;
		mtdMgr.printPara.BPeakMaxTime = cbRptKeepTime.Checked;
		mtdMgr.printPara.BPeakName = cbRptZuFenName.Checked;
		mtdMgr.printPara.BPeakPara = cbRptCorrFactor.Checked;
		mtdMgr.printPara.BPeakAmont = cbRptWithDensity.Checked;
		mtdMgr.printPara.BPeakArea = cbRptWithPeakArea.Checked;
		mtdMgr.printPara.BPeakheight = cbRptWithPeakHeight.Checked;
		mtdMgr.printPara.BPeakAmontPer = cbRptWithDensityPercent.Checked;
		mtdMgr.printPara.BPeakAreaPer = cbRptWithPeakAreaPercent.Checked;
		mtdMgr.printPara.BPeakheightPer = cbRptWithPeakHeightPercent.Checked;
		mtdMgr.printPara.BPeakHalfheight = cbRptWithPeakHalf.Checked;
		mtdMgr.printPara.BPeakV = cbRptWithPeakSingle.Checked;
		mtdMgr.printPara.BPeakFx = cbRptWithCurve.Checked;
		mtdMgr.printPara.BPeakOtherPara = cbRptWithCoef.Checked;
		mtdMgr.printPara.BPeakLV = cbRptWithPeakResulution.Checked;
		mtdMgr.printPara.BPeakTBPara = cbRptWithZhuXiao.Checked;
		mtdMgr.printPara.BPeakUTBPara = cbRptWithValidNumber.Checked;
		mtdMgr.printPara.BPeakLPara = cbRptWithCapFactor.Checked;
		mtdMgr.printPara.BPeaktailPara = cbRptWithTailFactor.Checked;
	}

	public void WriteToPrintParaOld()
	{
		if (printParaOld == null)
		{
			printParaOld = new PrintPara();
		}
		printParaOld.Title = ReportTitle.Text.Trim();
		printParaOld.PrintTitleTop = rpthead.Text.Trim();
		printParaOld.PrintTitleBotom = rptbotom.Text.Trim();
		if (radioButton7.Checked)
		{
			printParaOld.PPreView = PrintPreview.写字板;
		}
		if (radioButton4.Checked)
		{
			printParaOld.PPreView = PrintPreview.Word;
		}
		if (radioButton8.Checked)
		{
			printParaOld.PPreView = PrintPreview.程序自带;
		}
		printParaOld.BPTime = cbRptPrintTime.Checked;
		printParaOld.BJtime = cbRptJYTime.Checked;
		printParaOld.Bfname = cbRptFileName.Checked;
		printParaOld.BRdata = cbRptWithResult.Checked;
		printParaOld.BSouredata = cbRptWithSourceData.Checked;
		printParaOld.Bpic = cbRptWithChrom.Checked;
		printParaOld.Bpicwidth = (int)numericUpDown1.Value;
		printParaOld.Bpicheight = (int)numericUpDown3.Value;
		printParaOld.BPicBound = cbRptBoundary.Checked;
		printParaOld.BPicLineB = cbRptChromBold.Checked;
		printParaOld.BPicFontSize = (int)numericUpDown2.Value;
		printParaOld.BIndex = cbRptWithIdx.Checked;
		printParaOld.BPeakMaxTime = cbRptKeepTime.Checked;
		printParaOld.BPeakName = cbRptZuFenName.Checked;
		printParaOld.BPeakPara = cbRptCorrFactor.Checked;
		printParaOld.BPeakAmont = cbRptWithDensity.Checked;
		printParaOld.BPeakArea = cbRptWithPeakArea.Checked;
		printParaOld.BPeakheight = cbRptWithPeakHeight.Checked;
		printParaOld.BPeakAmontPer = cbRptWithDensityPercent.Checked;
		printParaOld.BPeakAreaPer = cbRptWithPeakAreaPercent.Checked;
		printParaOld.BPeakheightPer = cbRptWithPeakHeightPercent.Checked;
		printParaOld.BPeakHalfheight = cbRptWithPeakHalf.Checked;
		printParaOld.BPeakV = cbRptWithPeakSingle.Checked;
		printParaOld.BPeakFx = cbRptWithCurve.Checked;
		printParaOld.BPeakOtherPara = cbRptWithCoef.Checked;
		printParaOld.BPeakLV = cbRptWithPeakResulution.Checked;
		printParaOld.BPeakTBPara = cbRptWithZhuXiao.Checked;
		printParaOld.BPeakUTBPara = cbRptWithValidNumber.Checked;
		printParaOld.BPeakLPara = cbRptWithCapFactor.Checked;
		printParaOld.BPeaktailPara = cbRptWithTailFactor.Checked;
	}

	private void ReadFromTempSetedList()
	{
		dgvCT6.RowCount = 6;
		dgvCT6.Rows[0].Cells[0].Value = Lang.PS("进样器1", "Inj.1");
		dgvCT6.Rows[1].Cells[0].Value = Lang.PS("柱炉", "Envolve");
		dgvCT6.Rows[2].Cells[0].Value = Lang.PS("检测器1", "Dtc1");
		dgvCT6.Rows[3].Cells[0].Value = Lang.PS("辅1/进2", "Aux.1/Inj.2");
		dgvCT6.Rows[4].Cells[0].Value = Lang.PS("辅2/检2", "Aux.2/Dtc.2");
		dgvCT6.Rows[5].Cells[0].Value = Lang.PS("热导", "Therm.");
		if (devManager == null)
		{
			return;
		}
		for (int i = 0; i < devManager.tempSetedList.Length; i++)
		{
			dgvCT6.Rows[i].Cells[1].Value = devManager.tempSetedList[i];
		}
		method_4(gvgcProgTemp, devManager);
		method_5(devManager);
		string text = Lang.PS("进样器1:", "Inj.1:");
		string text2 = text + devManager.tempSetedList[0] + "℃\r";
		text2 += ReadInjectData(0);
		Ljyq1.Text = text2;
		string text3 = "";
		string text4 = Lang.PS("进样器2:", "Inj.2:");
		text3 = text3 + text4 + devManager.tempSetedList[4] + "℃\r";
		text3 += ReadInjectData(1);
		Ljyq10.Text = text3;
		string text5 = "";
		string text6 = Lang.PS("检测器1:", "Dtc1:");
		text5 = text5 + text6 + devManager.tempSetedList[2] + "℃\r";
		text5 += method_3(3);
		label15.Text = text5;
		string text7 = "";
		string text8 = Lang.PS("检测器2:", "Dtc2:");
		text7 = text7 + text8 + devManager.tempSetedList[3] + "℃\r";
		text7 += method_3(4);
		label14.Text = text7;
		string text9 = Lang.PS("柱箱:", "Envolve:");
		string text10 = text9 + devManager.tempSetedList[1] + "℃\r";
		label16.Text = text10;
		text10 = "";
		if (devManager.tempSettingList[0].tempStart == 0f)
		{
			string text11 = Lang.PS("程升: 无", "temperature programming: null");
			text10 += text11;
		}
		else
		{
			string text12 = Lang.PS("程升:", "temperature programming:");
			text10 = text10 + text12 + "\r\n";
			for (int j = 0; j < devManager.tempSettingList.Count && devManager.tempSettingList[j].tempStart != 0f; j++)
			{
				text10 = text10 + j + " " + devManager.tempSettingList[j].tempKeep.ToString("0.00") + " " + devManager.tempSettingList[j].tempStart.ToString("0.00") + " " + devManager.tempSettingList[j].tempEnd.ToString("0.00") + "\r\n";
			}
		}
		tbProgram.Text = text10;
		string text13 = Lang.PS("外部事件:", "External events:");
		text10 = text13 + "\r\n";
		for (int k = 0; k < 8; k++)
		{
			for (int l = 0; l < 4; l++)
			{
				text10 = text10 + devManager.eventCtrl0[l][k].ToString("00.00") + " ";
			}
			for (int m = 0; m < 2; m++)
			{
				text10 = text10 + devManager.eventCtrl1[m][k].ToString("00.00") + " ";
			}
			text10 += "\r\n";
		}
		tbEvent.Text = text10;
	}

	private string ReadInjectData(int int_1)
	{
		string text = "";
		EpcDeviceSetting epcDeviceSetting = devManager.epcDev1[int_1 * 3];
		if (epcDeviceSetting != null)
		{
			string text2 = Lang.PS("  柱流量", "  Column Flow:");
			text = text + text2 + epcDeviceSetting.pressureData + " sccm\r";
		}
		epcDeviceSetting = devManager.epcDev1[int_1 * 3 + 1];
		if (epcDeviceSetting != null)
		{
			string text3 = Lang.PS("  分流流量", "  Split flow:");
			text = text + text3 + epcDeviceSetting.pressureData + " sccm\r";
		}
		epcDeviceSetting = devManager.epcDev1[int_1 * 3 + 2];
		if (epcDeviceSetting != null)
		{
			string text4 = Lang.PS("  吹扫流量", "  Purge flow:");
			text = text + text4 + epcDeviceSetting.pressureData + " sccm\r";
		}
		if (epcDeviceSetting != null)
		{
			switch (epcDeviceSetting.gasType & 0xF)
			{
			case 0:
			{
				string text9 = Lang.PS("  载气:氮气", "  gas:N2");
				text = text + text9 + "\r";
				break;
			}
			case 1:
			{
				string text8 = Lang.PS("  载气:氢气", "  gas:H2");
				text = text + text8 + "\r";
				break;
			}
			case 2:
			{
				string text7 = Lang.PS("  载气:空气", "  gas:Air");
				text = text + text7 + "\r";
				break;
			}
			case 3:
			{
				string text6 = Lang.PS("  载气:氦气", "  gas:He");
				text = text + text6 + "\r";
				break;
			}
			case 4:
			{
				string text5 = Lang.PS("  载气:氩气", "  gas:argon");
				text = text + text5 + "\r";
				break;
			}
			}
		}
		return text;
	}

	public void ReadComponentData()
	{
		if (caliGnl_0 != null)
		{
			bLoading = true;
			gvCmpds.RowCount = caliGnl_0.cmpds.Length;
			gvCmpds.SuspendLayout();
			for (int i = 0; i < gvCmpds.RowCount; i++)
			{
				ReadComponentDataRow(i, caliGnl_0.cmpds[i]);
				gvCmpds.Rows[i].Tag = new CompoundRowIdx(caliGnl_0.cmpds[i], i);
			}
			gvCmpds.ResumeLayout();
			bLoading = false;
		}
	}

	private void ReadComponentDataRow(int iRow, Compound compound)
	{
		object obj = null;
		for (int i = 0; i < gvCmpds.ColumnCount; i++)
		{
			switch (gvCmpds.Columns[i].Name)
			{
			case "PeakColor":
				(gvCmpds.Rows[iRow].Cells[i] as LclgvColorCell).Color = compound.cmpdInfo.color;
				continue;
			case "RespArea":
				obj = compound.levels[m_iLevel].responseA;
				break;
			case "RespHeight":
				obj = compound.levels[m_iLevel].responseH;
				break;
			case "Amount":
				obj = compound.levels[m_iLevel].amount;
				break;
			case "RecordNumber":
				obj = compound.levels[m_iLevel].SecsNum;
				break;
			case "FreeRespFactor":
				obj = compound.levels[m_iLevel].respFactor;
				break;
			default:
				obj = gvCmpdsValue(gvUse: true, compound, gvCmpds.Columns[i].Name);
				break;
			}
			gvCmpds.Rows[iRow].Cells[i].Value = obj;
		}
	}

	private void WriteComponentData()
	{
		if (caliGnl_0 != null)
		{
			for (int i = 0; i < gvCmpds.RowCount && i < caliGnl_0.cmpds.Length; i++)
			{
				WriteComponentDataRow(i, ref caliGnl_0.cmpds[i]);
			}
		}
	}

	private void WriteComponentDataRow(int iRow, ref Compound compound)
	{
		object obj = null;
		for (int i = 0; i < gvCmpds.ColumnCount; i++)
		{
			obj = gvCmpds.Rows[iRow].Cells[i].Value;
			string name;
			if (obj != null && (name = gvCmpds.Columns[i].Name) != null)
			{
				switch (name)
				{
				case "Used":
					compound.used = (bool)obj;
					break;
				case "CpmdName":
					compound.cmpdInfo.name = obj.ToString();
					break;
				case "PeakRT":
					compound.cmpdInfo.retainTime = Class49.String2Float(obj, compound.cmpdInfo.retainTime);
					break;
				case "LeftWindow":
					compound.cmpdInfo.leftWindow = Class49.String2Float(obj, compound.cmpdInfo.leftWindow);
					break;
				case "RightWindow":
					compound.cmpdInfo.rightWindow = Class49.String2Float(obj, compound.cmpdInfo.rightWindow);
					break;
				case "HheatValue":
					compound.cmpdInfo.HheatValue = Class49.String2Float(obj, compound.cmpdInfo.HheatValue);
					break;
				case "LheatValue":
					compound.cmpdInfo.LheatValue = Class49.String2Float(obj, compound.cmpdInfo.LheatValue);
					break;
				case "PeakColor":
					compound.cmpdInfo.color = (gvCmpds.Rows[iRow].Cells[i] as LclgvColorCell).Color;
					break;
				case "RespStyle":
					compound.cmpdInfo.respStyle = (RespStyle)obj;
					break;
				case "RespArea":
					compound.levels[m_iLevel].responseA = Class49.String2Float(obj, compound.levels[m_iLevel].responseA);
					break;
				case "RespHeight":
					compound.levels[m_iLevel].responseH = Class49.String2Float(obj, compound.levels[m_iLevel].responseH);
					break;
				case "FreeRespFactor":
					compound.cmpdInfo.freeRespFactor = Class49.String2Float(obj, compound.cmpdInfo.freeRespFactor);
					break;
				case "Amount":
					compound.levels[m_iLevel].amount = Class49.String2Float(obj, compound.levels[m_iLevel].amount);
					break;
				case "IstdCmpds":
					compound.cmpdInfo.sl_IstdCmpdNo = (int)Class49.String2Float(obj, compound.cmpdInfo.sl_IstdCmpdNo);
					break;
				}
			}
		}
	}

	private void ClearComponentData()
	{
		gvCmpds.RowCount = 0;
	}

	private void RefreshIntegrations(AccStyle accStyle_0)
	{
		gvInteg.EndEdit();
		if (gvInteg.RowCount == 0)
		{
			gvInteg.Refresh(AccStyle.Clear, null);
			Integration integration = new Integration();
			integration.Reset();
			gvInteg.Refresh(AccStyle.Read, integration);
		}
		if (mtdMgr.sigIntegrations.Count > 0)
		{
			if (mtdMgr.sigIntegrations.Count > 1)
			{
				mtdMgr.sigIntegrations[0] = mtdMgr.sigIntegrations[1];
				gvInteg.Refresh(accStyle_0, mtdMgr.sigIntegrations[1]);
			}
			else
			{
				gvInteg.Refresh(accStyle_0, mtdMgr.sigIntegrations[0]);
			}
		}
		else
		{
			mtdMgr.ResetIntegrations();
		}
	}

	public void ReadComponentData(CaliGnl cali)
	{
		mtdMgr.caliGnl = cali.Copy();
		ReadComponentData();
	}

	public bool OpenCaliGnlFile(string strFileName)
	{
		ChromInfo chromInfo = mtdMgr.chromInfo;
		CaliGnl caliGnl = CaliGnl.LoadFromFile(strFileName);
		if (caliGnl == null)
		{
			MessageBox.Show("组份表文件损坏或过旧，请重建组份表文件。");
			return false;
		}
		caliGnl_0 = caliGnl;
		SetCalFileName(chromInfo, strFileName);
		if (caliGnl.caliOption.caliDisMode == CaliDisMode.Estd)
		{
			lclRbOuter.Checked = true;
		}
		else
		{
			lclRbInner.Checked = true;
		}
		ReadComponentData();
		return true;
	}

	private bool OpenChromFile(string strFileName)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(strFileName, DetectorStyle.General);
		if (chromatogram == null)
		{
			return false;
		}
		chromatogram.Process(InstruStyle.GC);
		if (chromatogram.caliGnl != null)
		{
			caliGnl_0 = chromatogram.caliGnl.Copy();
		}
		else
		{
			if (chromatogram.signal == null)
			{
				return false;
			}
			caliGnl_0 = new CaliGnl();
		}
		if (caliGnl_0.cmpds.Length == 0 && chromatogram.signal != null && chromatogram.signal.PeaksNum > 0)
		{
			for (int i = 0; i < chromatogram.signal.PeaksNum; i++)
			{
				Peak peak = chromatogram.signal.peaks[i];
				if (!CheckForAddCompound(peak))
				{
					caliGnl_0.add_splLevel(checkExists: false, canAddNew: true, peak.name, m_iLevel, peak.pkRT, peak.area, peak.height, peak.GasAmount);
				}
			}
		}
		SetCalFileName(mtdMgr.chromInfo, strFileName);
		caliGnl_0.CalculateFunc(appendLink: true);
		ReadComponentData();
		return true;
	}

	private bool CheckForAddCompound(Peak peak_0)
	{
		bool result = false;
		for (int i = 0; i < caliGnl_0.cmpds.Length; i++)
		{
			if (caliGnl_0.cmpds[i].cmpdInfo.retainTime != peak_0.pkRT)
			{
				continue;
			}
			Compound compound = caliGnl_0.cmpds[i];
			for (int j = 0; j < compound.levels.Length; j++)
			{
				if (peak_0.area == compound.levels[j].responseA && peak_0.height == compound.levels[j].responseH)
				{
					result = true;
				}
				if (peak_0.area == compound.levels[j].LastAddresponseA && peak_0.height == compound.levels[j].LastAddresponseH)
				{
					result = true;
				}
			}
		}
		return result;
	}

	private void MethodNew_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.管理员 && Class49.user_0.ULevel != User.Level.分析员)
		{
			MessageBox.Show(Lang.PS("没有编辑方法权限！", "No editing method permissions!"));
		}
		else
		{
			Clear();
			ReadFromPrintPara();
			bUseSet_Click(null, null);
		}
	}

	public void AutoMethodLoad(int indexChannel, string methodPath)
	{
		mtdMgr.chromInfo = new ChromInfo();
		Clear();
		LoadMethodFile(methodPath);
		lclButton2.Enabled = true;
		string directoryName = Path.GetDirectoryName(methodPath);
		sysParam.strMtdDataFileDir = directoryName;
		sysParam.SaveParam();
		if (caliGnl_0 != null)
		{
			for (byte b = 0; b < caliGnl_0.cmpds.Length; b++)
			{
				float respFactor = caliGnl_0.cmpds[b].levels[0].respFactor;
				if (respFactor == 0f)
				{
					caliGnl_0.CalculateFunc(appendLink: false);
				}
				respFactor = caliGnl_0.cmpds[b].levels[0].respFactor;
				gvCmpds.Rows[b].Cells["FreeRespFactor"].Value = respFactor;
			}
		}
		UsePara();
		if (this.OnMethodSaveEvent != null)
		{
			this.OnMethodSaveEvent(this, new EventArgs());
		}
	}

	private void MethodOpen1_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开方法", "open method");
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("方法文件") + "(*.mtd)|*.mtd";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			tbMethName1.Text = openFileDialog.FileName;
		}
	}

	private void MethodOpen2_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开方法", "open method");
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("方法文件") + "(*.mtd)|*.mtd";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			tbMethName2.Text = openFileDialog.FileName;
		}
	}

	private void BtnAdd2_Click(object sender, EventArgs e)
	{
		string text = tbMethName2.Text;
		if (text == "默认")
		{
			return;
		}
		MtdSetup mtdSetup = (MtdSetup)IBaseFileMgr.OpenFile(text);
		frmParam.strMethod2 = text;
		frmParam.SaveParam();
		if (mtdSetup != null && mtdSetup.sigIntegrations.Count > 0)
		{
			for (int i = 3; i < mtdSetup.sigIntegrations[0].IntegRows.Length; i++)
			{
				mtdMgr.sigIntegrations[0].AppendRow(mtdSetup.sigIntegrations[0].IntegRows[i]);
			}
			gvInteg.Refresh(AccStyle.Read, mtdMgr.sigIntegrations[0]);
		}
		if (mtdSetup.caliGnl != null)
		{
			int num = caliGnl_0.cmpds.Length;
			Array.Resize(ref caliGnl_0.cmpds, caliGnl_0.cmpds.Length + mtdSetup.caliGnl.cmpds.Length);
			for (int j = 0; j < mtdSetup.caliGnl.cmpds.Length; j++)
			{
				caliGnl_0.cmpds[num + j] = mtdSetup.caliGnl.cmpds[j];
			}
			ReadComponentData();
		}
	}

	private void BtnAdd1_Click(object sender, EventArgs e)
	{
		string text = tbMethName1.Text;
		if (text == "默认")
		{
			return;
		}
		MtdSetup mtdSetup = (MtdSetup)IBaseFileMgr.OpenFile(text);
		frmParam.strMethod1 = text;
		frmParam.SaveParam();
		if (mtdSetup != null && mtdSetup.sigIntegrations.Count > 0)
		{
			for (int i = 4; i < mtdSetup.sigIntegrations[0].IntegRows.Length; i++)
			{
				if (mtdSetup.sigIntegrations[0].IntegRows[i].oprtStyle != IntegOprtStyle.PkCut)
				{
					mtdMgr.sigIntegrations[0].AppendRow(mtdSetup.sigIntegrations[0].IntegRows[i]);
				}
			}
			mtdMgr.sigIntegrations[0].ResetDeletTime(mtdSetup.sigIntegrations[0].IntegRows[3]);
			gvInteg.Refresh(AccStyle.Read, mtdMgr.sigIntegrations[0]);
		}
		if (mtdSetup.caliGnl != null)
		{
			int num = caliGnl_0.cmpds.Length;
			Array.Resize(ref caliGnl_0.cmpds, caliGnl_0.cmpds.Length + mtdSetup.caliGnl.cmpds.Length);
			for (int j = 0; j < mtdSetup.caliGnl.cmpds.Length; j++)
			{
				caliGnl_0.cmpds[num + j] = mtdSetup.caliGnl.cmpds[j];
			}
			ReadComponentData();
		}
	}

	private void MethodOpen_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开方法", "open method");
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("方法文件") + "(*.mtd)|*.mtd";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			string fileName = openFileDialog.FileName;
			mtdMgr = new MtdSetup();
			mtdMgr.chromInfo = new ChromInfo();
			Clear();
			LoadMethodFile(fileName);
			lclButton2.Enabled = true;
			string directoryName = Path.GetDirectoryName(fileName);
			sysParam.strMtdDataFileDir = directoryName;
			sysParam.SaveParam();
			if (File.Exists(mtdMgr.chromInfo.cclCalibration))
			{
				OpenCaliGnlFile(mtdMgr.chromInfo.cclCalibration);
			}
			WriteToPrintPara();
			if (this.OnMethodSaveEvent != null)
			{
				this.OnMethodSaveEvent(this, new EventArgs());
			}
		}
	}

	public void loadSaveMethod(string fileName)
	{
		mtdMgr = new MtdSetup();
		mtdMgr.chromInfo = new ChromInfo();
		Clear();
		LoadMethodFile(fileName);
		lclButton2.Enabled = true;
		string directoryName = Path.GetDirectoryName(fileName);
		sysParam.strMtdDataFileDir = directoryName;
		sysParam.SaveParam();
		if (File.Exists(mtdMgr.chromInfo.cclCalibration))
		{
			OpenCaliGnlFile(mtdMgr.chromInfo.cclCalibration);
		}
		ReadFromChromInfo();
	}

	public void LoadMethodFile(string fileName)
	{
		if (File.Exists(fileName))
		{
			if (mstDeviceManager == null)
			{
				mstDeviceManager = new InsDeviceManager();
			}
			if (!mtdMgr.LoadFromFile(fileName, mstDeviceManager))
			{
				mtdMgr.LoadFromFile(fileName);
			}
			ReadFromMtdMgr();
			ReadFromTempSetedList();
		}
	}

	private void MethodSave_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.管理员 && Class49.user_0.ULevel != User.Level.分析员)
		{
			MessageBox.Show(Lang.PS("没有编辑方法权限！", "No editing method permissions!"));
			return;
		}
		if (mtdMgr.strMtdFilePath == "" || !File.Exists(mtdMgr.strMtdFilePath))
		{
			MethodReSave_Click(null, null);
			return;
		}
		WriteToMtdMgr();
		mstDeviceManager.printPara_0 = printParaOld;
		mtdMgr.SaveToFile(mtdMgr.strMtdFilePath, mstDeviceManager);
		if (File.Exists(mtdMgr.strMtdFilePath))
		{
			loadSaveMethod(mtdMgr.strMtdFilePath);
		}
		if (this.OnMethodSaveEvent != null)
		{
			this.OnMethodSaveEvent(this, new EventArgs());
		}
		Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", "保存方法", "保存方法:" + mtdMgr.strMtdFilePath);
	}

	private void MethodReSave_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.管理员 && Class49.user_0.ULevel != User.Level.分析员)
		{
			MessageBox.Show(Lang.PS("没有编辑方法权限！", "No editing method permissions!"));
			return;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Title = Lang.PS("另存方法", "save as method");
		saveFileDialog.Filter = Class49.MakeFileFilter(".mtd");
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			WriteToMtdMgr();
			mstDeviceManager.printPara_0 = printParaOld;
			mtdMgr.SaveToFile(saveFileDialog.FileName, mstDeviceManager);
			if (File.Exists(saveFileDialog.FileName))
			{
				loadSaveMethod(saveFileDialog.FileName);
			}
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", "另存方法", "另存方法:" + mtdMgr.strMtdShowName);
		}
	}

	public void ReSave_()
	{
		if (Class49.user_0.ULevel != User.Level.管理员 && Class49.user_0.ULevel != User.Level.分析员)
		{
			MessageBox.Show(Lang.PS("没有编辑方法权限！", "No editing method permissions!"));
			return;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Title = Lang.PS("另存方法", "save as method");
		saveFileDialog.Filter = Class49.MakeFileFilter(".mtd");
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			lclButton1_Click(null, null);
			Application.DoEvents();
			WriteToMtdMgr();
			mtdMgr.SaveToFile(saveFileDialog.FileName);
			tbMethName.Text = mtdMgr.strMtdShowName;
			sysParam.strMtdDataFileDir = Path.GetDirectoryName(saveFileDialog.FileName);
			sysParam.SaveParam();
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", "另存方法", "另存方法:" + mtdMgr.strMtdShowName);
		}
	}

	public void bUseSet_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.访问员)
		{
			MessageBox.Show(Lang.PS("没有调用方法权限！", "Without permission "));
			return;
		}
		if (caliGnl_0 != null)
		{
			for (byte b = 0; b < caliGnl_0.cmpds.Length; b++)
			{
				float freeRespFactor = caliGnl_0.cmpds[b].cmpdInfo.freeRespFactor;
				if (freeRespFactor == 0f)
				{
					caliGnl_0.CalculateFunc(appendLink: false);
					freeRespFactor = caliGnl_0.cmpds[b].levels[0].respFactor;
					if (gvCmpds.Rows.Count > 0)
					{
						gvCmpds.Rows[b].Cells["FreeRespFactor"].Value = freeRespFactor;
					}
				}
			}
		}
		UsePara();
		if (this.OnMethodSaveEvent != null)
		{
			this.OnMethodSaveEvent(this, new EventArgs());
		}
	}

	public void UsePara()
	{
		WriteToMtdMgr();
		if (mtdMgr.strMtdFilePath != "" && File.Exists(mtdMgr.strMtdFilePath))
		{
			mtdMgr.SaveToFile(mtdMgr.strMtdFilePath, devManager);
		}
	}

	private void method_11()
	{
		dataGridViewColumn_0.DefaultCellStyle.ForeColor = Color.Gray;
	}

	private void method_12(object sender, DataGridViewCellCancelEventArgs e)
	{
		e.Cancel = method_13(bool_1: true);
	}

	private bool method_13(bool bool_1)
	{
		bool flag = false;
		if (bool_1 && flag)
		{
			MessageBox.Show(Lang.PS("受限！", "No Right！"));
		}
		return flag;
	}

	private void method_14(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0 && e.ColumnIndex < 0)
		{
		}
	}

	private void btnasSetChrom_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdMgr.chromInfo;
		if (sender == btnasSetChrom)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = Lang.PS("设置加/减谱图", "Set plus/minus");
			openFileDialog.Filter = Class49.MakeFileFilter(".sda") + "|" + Class49.MakeFileFilter(".dat");
			openFileDialog.FilterIndex = 1;
			openFileDialog.InitialDirectory = chromInfo.asDirectory;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.asChrom = openFileDialog.FileName;
		}
		if (sender == btnasNoneChrom)
		{
			chromInfo.asChrom = "";
		}
		chromInfo.RefreshAsInfo();
		tbasChrom.Text = chromInfo.asShowName;
	}

	private void btnasNoneChrom_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdMgr.chromInfo;
		if (sender == btnasSetChrom)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = Lang.PS("设置加/减谱图", "Set plus/minus");
			openFileDialog.Filter = Class49.MakeFileFilter(".sda") + "|" + Class49.MakeFileFilter(".dat");
			openFileDialog.FilterIndex = 2;
			openFileDialog.InitialDirectory = chromInfo.asDirectory;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.asChrom = openFileDialog.FileName;
		}
		if (sender == btnasNoneChrom)
		{
			chromInfo.asChrom = "";
		}
		chromInfo.RefreshAsInfo();
		tbasChrom.Text = chromInfo.asShowName;
	}

	public void FrmToDevicePara()
	{
		if (thisFormMf == null)
		{
			return;
		}
		ChromDevice chromDevice = new ChromDevice();
		bool flag = false;
		int num = -1;
		for (int i = 0; i < thisFormMf.FrmEquip.SunAquips.Count; i++)
		{
			if (thisFormMf.FrmEquip.SunAquips[i].info.ID == thisFormMf.CurrentGCID)
			{
				chromDevice = thisFormMf.FrmEquip.SunAquips[i];
				flag = true;
				num = i;
				if (flag)
				{
					int currentChannelIndex = thisFormMf.CurrentChannelIndex;
					chromDevice.misMgr.ChartParaOperaS[currentChannelIndex].mtdMgr = mtdMgr;
					thisFormMf.FrmEquip.SunAquips[num] = chromDevice;
				}
				break;
			}
		}
	}

	private void lclRbNo_CheckedChanged(object sender, EventArgs e)
	{
		TipLable.Text = Lang.PS("不校正按照面积或峰高计算含量百分比。", "No correction according to the area or peak height percentage calculation.");
	}

	private void lclRbOuter_CheckedChanged(object sender, EventArgs e)
	{
		TipLable.Text = Lang.PS("根据定量组份表中定义的校正因子等参数进行计算   如：单点外标、多点外标进行组份浓度计算。", "According to the calculation of quantitative components such as defined in the correction factor and other parameters: single point external standard, multiple point external standard of component concentration calculation. ");
	}

	private void lclRbInner_CheckedChanged(object sender, EventArgs e)
	{
		TipLable.Text = Lang.PS("根据定量组份表中定义的校正因子等参数进行计算  如：单点内标、多点内标。", "According to the calculation of quantitative components such as defined in the correction factor and other parameters: single point of internal standard, multiple internal standard. ");
	}

	private void radioButton1_CheckedChanged(object sender, EventArgs e)
	{
		TipLable.Text = Lang.PS("根据定量组份表中定义的校正因子进行校正归一计算。", "According to the quantitative component table defined in the correction factor for normalization calculation");
	}

	private void lclButton1_Click(object sender, EventArgs e)
	{
		if (thisFormMf != null)
		{
			lclButton2.Enabled = true;
			TcpServerSocket currentTcpSocket = thisFormMf.GetCurrentTcpSocket();
			if (currentTcpSocket != null)
			{
				ReadFromTempSetedList();
			}
		}
	}

	private void lclButton2_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpSocket = thisFormMf.GetCurrentTcpSocket();
		bool flag = true;
		if (currentTcpSocket != null)
		{
			int millisecondsTimeout = 1;
			if (flag)
			{
				millisecondsTimeout = 100;
			}
			byte[] array = currentTcpSocket.CmdNum2Data_AndSend(8, devManager, 0, flag);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			array = currentTcpSocket.CmdNum2Data_AndSend(111, devManager, 0, flag);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			byte[] byte_ = currentTcpSocket.CmdNum2Data_AndSend(9, devManager, 0, flag);
			IBrainConvert.ArrayCopy(ref array, byte_);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			byte_ = currentTcpSocket.CmdNum2Data_AndSend(10, devManager, 0, flag);
			IBrainConvert.ArrayCopy(ref array, byte_);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			byte_ = currentTcpSocket.CmdNum2Data_AndSend(101, devManager, 0, flag);
			IBrainConvert.ArrayCopy(ref array, byte_);
			Thread.Sleep(millisecondsTimeout);
			Application.DoEvents();
			for (int i = 0; i < devManager.epcDev1.Count - 3; i++)
			{
				byte_ = currentTcpSocket.CmdNum2Data_AndSend(37, devManager, i, flag);
				IBrainConvert.ArrayCopy(ref array, byte_);
				Thread.Sleep(millisecondsTimeout);
				Application.DoEvents();
			}
			if (!flag)
			{
				currentTcpSocket.SendData(array);
			}
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, thisFormMf.CurrentGCID, "设置方法参数到设备", "设置方法参数到设备:" + sysParam.strMtdDataFileDir);
		}
		lclButton2.Enabled = false;
	}

	private void radioButton9_CheckedChanged(object sender, EventArgs e)
	{
		if (!radioButton9.Checked)
		{
			panelJY.Visible = false;
			panel15.Visible = false;
		}
		else if (tabControl2.SelectedIndex < 3)
		{
			panelJY.Visible = true;
			panel15.Visible = true;
			label12.Text = Lang.PS("色谱柱:", "column:");
		}
		else
		{
			panelJY.Visible = false;
			panel15.Visible = false;
			label12.Text = Lang.PS("气阻:", "air-resistor:");
		}
	}

	private void radioButton2_CheckedChanged(object sender, EventArgs e)
	{
		if (!radioButton2.Checked)
		{
			panelJY.Visible = true;
			panel15.Visible = true;
			return;
		}
		panelJY.Visible = false;
		panel15.Visible = false;
		label12.Text = Lang.PS("气阻:", "air-resistor:");
	}

	private void radioButton3_CheckedChanged(object sender, EventArgs e)
	{
		if (!radioButton3.Checked)
		{
			panelJY.Visible = true;
			panel15.Visible = true;
			return;
		}
		panelJY.Visible = false;
		panel15.Visible = false;
		label12.Text = Lang.PS("气阻:", "air-resistor:");
	}

	private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
	{
		method_5(devManager);
	}

	private void checkBox4_CheckedChanged(object sender, EventArgs e)
	{
		checkBox5.Checked = !checkBox4.Checked;
	}

	private void checkBox5_CheckedChanged(object sender, EventArgs e)
	{
		checkBox4.Checked = !checkBox5.Checked;
	}

	private void checkBox7_CheckedChanged(object sender, EventArgs e)
	{
		checkBox6.Checked = !checkBox7.Checked;
	}

	private void checkBox6_CheckedChanged(object sender, EventArgs e)
	{
		checkBox7.Checked = !checkBox6.Checked;
	}

	public void InitGvCmpdsAllSetting()
	{
		columnsSetupDlg_0 = new ColumnsSetupDlg("校正列设置", "Calibrate Columns Setup");
		gvCmpds.OnChangeColor += method_7;
		gvCmpds.CharacterHeaderColor = Color.Red;
		if (gvCmpds.LoadFromManager())
		{
			string combineText = "Level " + (m_iLevel + 1);
			gvCmpds.AdjustCombineDisInfo(read_refresh: false);
			gvCmpds.SetCombineCText(int_3, combineText);
		}
		else
		{
			miRestoreDftColumns_Click(miRestoreDftColumns, null);
		}
	}

	private void SetGvCmpdsTableColumnText()
	{
		if (gvCmpds.ColumnCount == 0)
		{
			return;
		}
		for (int i = 0; i < gvCmpds.ColumnCount; i++)
		{
			string name;
			switch (name = gvCmpds.Columns[i].Name)
			{
			case "Used":
				gvCmpds.Columns[i].HeaderText = Lang.PS("使用", "Used");
				break;
			case "CpmdName":
				gvCmpds.Columns[i].HeaderText = Lang.PS("组分名", "CpmdName");
				break;
			case "PeakRT":
				gvCmpds.Columns[i].HeaderText = Lang.PS("峰位RT\n[min]", "PeakRT");
				break;
			case "LeftWindow":
				gvCmpds.Columns[i].HeaderText = Lang.PS("左窗宽\n[min]", "LeftWindow");
				break;
			case "RightWindow":
				gvCmpds.Columns[i].HeaderText = Lang.PS("右窗宽\n[min]", "RightWindow");
				break;
			case "HheatValue":
				gvCmpds.Columns[i].HeaderText = Lang.PS("高热值", "HheatValue");
				break;
			case "LheatValue":
				gvCmpds.Columns[i].HeaderText = Lang.PS("低热值", "LheatValue");
				break;
			case "PeakColor":
				gvCmpds.Columns[i].HeaderText = Lang.PS("颜色", "PeakColor");
				break;
			case "IstdCmpd":
				gvCmpds.Columns[i].HeaderText = Lang.PS("内标", "IstdCmpd");
				break;
			case "RespStyle":
				gvCmpds.Columns[i].HeaderText = Lang.PS("响应", "RespStyle");
				break;
			case "FreeRespFactor":
				gvCmpds.Columns[i].HeaderText = Lang.PS("校正因子", "FreeRespFactor");
				break;
			case "RespArea":
				gvCmpds.Columns[i].HeaderText = Lang.PS("面积", "RespArea");
				break;
			case "RespHeight":
				gvCmpds.Columns[i].HeaderText = Lang.PS("高度", "RespHeight");
				break;
			case "Amount":
				gvCmpds.Columns[i].HeaderText = Lang.PS("浓度", "Amount");
				break;
			case "RecordNumber":
				gvCmpds.Columns[i].HeaderText = Lang.PS("记录", "RecordNumber");
				break;
			}
		}
	}

	private void InitGvCmpdsTable()
	{
		gvCmpds.textBox_dftDecimalPlaces = Class49.int_8;
		gvCmpds.textBox_dftAligement = StringAlignment.Far;
		gvCmpds.textBox_dftReadOnly = false;
		gvCmpds.AddLclCheckBoxColumn("Used", 30).Frozen = true;
		gvCmpds.AddLclTextBoxColumn("CpmdName", 100, StringAlignment.Near);
		dataGridViewColumn_0 = gvCmpds.AddLclTextBoxColumn("PeakRT", 55, 3, readOnly: false);
		dataGridViewColumn_0.DefaultCellStyle.ForeColor = Color.Blue;
		gvCmpds.AddLclTextBoxColumn("LeftWindow", 50);
		gvCmpds.AddLclTextBoxColumn("RightWindow", 50);
		gvCmpds.AddLclTextBoxColumn("HheatValue", 50);
		gvCmpds.AddLclTextBoxColumn("LheatValue", 50);
		gvCmpds.AddLclColorColumn("PeakColor", 50);
		gvCmpds.AddLclTextBoxColumn("IstdCmpd", 0, 0, StringAlignment.Near, readOnly: true);
		DataGridViewComboBoxColumn dataGridViewComboBoxColumn = gvCmpds.AddLclRespStyleColumn("RespStyle", 50);
		dataGridViewComboBoxColumn.Items.Add(RespStyle.Area);
		dataGridViewComboBoxColumn.Items.Add(RespStyle.Height);
		dataGridViewComboBoxColumn.Items.Add(RespStyle.AreaSquare);
		dataGridViewComboBoxColumn.Items.Add(RespStyle.PeakHeightSquare);
		gvCmpds.AddLclTextBoxColumn("FreeRespFactor", 50, 5, readOnly: false).DefaultCellStyle.ForeColor = Color.Black;
		gvCmpds.AddLclTextBoxColumn("RespArea", 80, 4, readOnly: false).DefaultCellStyle.ForeColor = Color.Black;
		gvCmpds.AddLclTextBoxColumn("RespHeight", 60, 4, readOnly: false).DefaultCellStyle.ForeColor = Color.Black;
		gvCmpds.AddLclTextBoxColumn("Amount", 70);
		gvCmpds.AddLclTextBoxColumn("RecordNumber", 30, 0, StringAlignment.Center, readOnly: true).DefaultCellStyle.ForeColor = Color.Gray;
		CombineC combineC = new CombineC();
		combineC.indices = new int[4]
		{
			gvCmpds.Columns["RespArea"].Index,
			gvCmpds.Columns["RespHeight"].Index,
			gvCmpds.Columns["Amount"].Index,
			gvCmpds.Columns["RecordNumber"].Index
		};
		int_3 = gvCmpds.AddCombineC(combineC);
		gvCmpds.combineH = 15;
	}

	private void miAddrow_Click(object sender, EventArgs e)
	{
		Peak peak = new Peak();
		caliGnl_0.add_splLevel(checkExists: true, canAddNew: true, m_iLevel, peak.pkRT, peak.area, peak.height);
		caliGnl_0.CalculateFunc(appendLink: true);
		ReadAndWriteCaliGnlData(AccStyle.Read);
	}

	private void miDelRow_Click(object sender, EventArgs e)
	{
		if (gvCmpds.SelectedRows != null && gvCmpds.SelectedRows.Count != 0)
		{
			Compound[] array = new Compound[gvCmpds.SelectedRows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (gvCmpds.SelectedRows[i].Tag as CompoundRowIdx).compound_0;
			}
			caliGnl_0.DeleteCmpds(array);
			caliGnl_0.CalculateFunc(appendLink: true);
			ReadAndWriteCaliGnlData(AccStyle.Read);
		}
	}

	private void miRestoreDftColumns_Click(object sender, EventArgs e)
	{
		string combineText = "Level " + (m_iLevel + 1);
		if (sender == miColumnsSetup)
		{
			columnsSetupDlg_0.ShowDialog(gvCmpds);
		}
		else if (sender == miRestoreDftColumns)
		{
			gvCmpds.ini_SetFirstVisibleColumn("Used");
			gvCmpds.ini_SetNextVisibleColumn("CpmdName");
			gvCmpds.ini_SetNextVisibleColumn("PeakRT");
			gvCmpds.ini_SetNextVisibleColumn("LeftWindow");
			gvCmpds.ini_SetNextVisibleColumn("RightWindow");
			gvCmpds.ini_SetNextUnVisibleColumn("HheatValue");
			gvCmpds.ini_SetNextUnVisibleColumn("LheatValue");
			gvCmpds.ini_SetNextUnVisibleColumn("PeakColor");
			gvCmpds.ini_SetNextVisibleColumn("IstdCmpd");
			gvCmpds.ini_SetNextVisibleColumn("RespStyle");
			gvCmpds.ini_SetNextVisibleColumn("FreeRespFactor");
			gvCmpds.ini_SetNextVisibleColumn("RespArea");
			gvCmpds.ini_SetNextVisibleColumn("RespHeight");
			gvCmpds.ini_SetNextVisibleColumn("Amount");
			gvCmpds.ini_SetNextVisibleColumn("RecordNumber");
			gvCmpds.ini_FinishVisibleColumn();
			gvCmpds.AdjustCombineDisInfo(read_refresh: false);
			gvCmpds.SetCombineCText(int_3, combineText);
		}
	}

	private Compound GetCompoundByName(string strName)
	{
		for (int i = 0; i < gvCmpds.RowCount; i++)
		{
			if (gvCmpds.Rows[i].Tag != null)
			{
				Compound compound_ = (gvCmpds.Rows[i].Tag as CompoundRowIdx).compound_0;
				if (compound_.cmpdInfo.name == strName)
				{
					return compound_;
				}
			}
		}
		return null;
	}

	public void GetCmpdsDisColumns(ref GvInfos gvInfos)
	{
		string[] string_ = new string[4] { "RespArea", "RespHeight", "Amount", "RecordNumber" };
		Class49.SetGridViewInfo(gvCmpds, ref gvInfos, string_);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text;
			if ((text = gvInfos.colNames[i]) != null)
			{
				if (!(text == "Used"))
				{
					if (text == "CpmdName" || text == "IstdCmpd")
					{
						num = 115;
					}
				}
				else
				{
					num = 45;
				}
			}
			gvInfos.colWidths[i] = num;
		}
	}

	private void gvCmpds_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		FrmAnchor m_frmAnchor;
		if (e.ColumnIndex == 1)
		{
			UCKeyBorderAll uCKeyBorderAll = new UCKeyBorderAll();
			m_frmAnchor = new FrmAnchor(gvCmpds, uCKeyBorderAll);
			uCKeyBorderAll.EnterClick += delegate
			{
				m_frmAnchor.Hide();
			};
		}
		else
		{
			UCKeyBorderNum uCKeyBorderNum = new UCKeyBorderNum();
			m_frmAnchor = new FrmAnchor(gvCmpds, uCKeyBorderNum);
			uCKeyBorderNum.EnterClick += delegate
			{
				m_frmAnchor.Hide();
			};
		}
		m_frmAnchor.VisibleChanged += delegate
		{
			if (!m_frmAnchor.Visible)
			{
			}
		};
		if (!m_frmAnchor.Visible)
		{
			m_frmAnchor.Show(gvCmpds);
		}
	}

	private void gvCmpds_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void gvCmpds_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (!bLoading)
		{
			int rowIndex = e.RowIndex;
			string name = gvCmpds.Columns[e.ColumnIndex].Name;
			if (!(name == "CpmdName") && !(name == "RespFactor") && !(name == "FreeRespFactor"))
			{
				ReadAndWriteCompoundList(AccStyle.Write);
				caliGnl_0.CalculateFunc(appendLink: false);
				float respFactor = caliGnl_0.cmpds[rowIndex].levels[0].respFactor;
				gvCmpds.Rows[rowIndex].Cells["FreeRespFactor"].Value = respFactor;
			}
		}
	}

	public void CalcuFreeRespFactor(CaliGnl calignl)
	{
		bool flag = false;
		try
		{
			string[] array = new string[0];
			for (int i = 0; i < gvCmpds.RowCount; i++)
			{
				if (gvCmpds.Rows[i].Cells["IstdCmpd"].Value.ToString().Trim() != "")
				{
					Array.Resize(ref array, array.Length + 1);
					array[array.Length - 1] = gvCmpds.Rows[i].Cells["IstdCmpd"].Value.ToString().Trim();
					flag = true;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < gvCmpds.RowCount; j++)
				{
					Compound compound = calignl.cmpds[j];
					double num = 0.0;
					double num2 = 0.0;
					int num3 = 0;
					for (int k = 0; k < 20; k++)
					{
						if (compound.levels[k].used)
						{
							num3++;
							num += (double)compound.levels[k].respFactor;
						}
					}
					if (num3 != 0)
					{
						num2 = num / (double)num3;
					}
					if (num2 != 0.0)
					{
						compound.cmpdInfo.freeRespFactor = (float)num2;
					}
					for (int l = 0; l < array.Length; l++)
					{
						if (gvCmpds.Rows[j].Cells["CpmdName"].Value.ToString().Trim() == array[l])
						{
							gvCmpds.Rows[j].Cells["FreeRespFactor"].Value = 1;
						}
					}
				}
				return;
			}
			for (int m = 0; m < gvCmpds.RowCount; m++)
			{
				Compound compound_ = (gvCmpds.Rows[m].Tag as Class74).compound_0;
				if (!double.IsNaN(compound_.iFunc.disCoefs[1]))
				{
					gvCmpds.Rows[m].Cells["FreeRespFactor"].Value = 1.0 / ((compound_.iFunc.disCoefs == null) ? 1.0 : compound_.iFunc.disCoefs[1]);
				}
				for (int n = 0; n < array.Length; n++)
				{
					if (gvCmpds.Rows[m].Cells["CpmdName"].Value.ToString().Trim() == array[n])
					{
						gvCmpds.Rows[m].Cells["FreeRespFactor"].Value = 1;
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void gvCmpds_DoubleClick(object sender, EventArgs e)
	{
		if (gvCmpds.CurrentCell != null && !gvCmpds.CurrentCell.IsInEditMode && gvCmpds.CurrentCell.RowIndex >= 0 && gvCmpds.CurrentCell.OwningColumn.Name == "IstdCmpd")
		{
			CaliGnlIstdDlg caliGnlIstdDlg = new CaliGnlIstdDlg();
			if (caliGnlIstdDlg.ShowDialog(gvCmpds, gvCmpds.CurrentCell.RowIndex, ref caliGnl_0.caliOption.caliDisMode) == DialogResult.OK)
			{
				ReadAndWriteCompoundOneRow(gvCmpds.CurrentCell.RowIndex, AccStyle.Read);
			}
		}
	}

	private void method_7(int int_5)
	{
		ReadAndWriteCompoundOneRow(int_5, AccStyle.Write);
	}

	private bool SelectPeaks(float pkRT)
	{
		if (gvCmpds.RowCount == 0 || gvCmpds.Rows[0].Tag != null)
		{
			for (int i = 0; i < gvCmpds.RowCount; i++)
			{
				if (gvCmpds.Rows[i].Selected && (gvCmpds.Rows[i].Tag as CompoundRowIdx).compound_0.Contains(pkRT))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void gvCmpds_SelectionChanged(object sender, EventArgs e)
	{
	}

	public object gvCmpdsValue(bool gvUse, Compound cmpd, string columnName)
	{
		object obj = null;
		string text = gvCmpds.ConvertValFmt(columnName);
		switch (columnName)
		{
		case "Used":
			obj = (gvUse ? ((object)cmpd.used) : (cmpd.used ? "√" : ""));
			break;
		case "CpmdName":
			obj = cmpd.cmpdInfo.name;
			break;
		case "PeakRT":
			obj = (gvUse ? ((object)cmpd.cmpdInfo.retainTime) : cmpd.cmpdInfo.retainTime.ToString(text));
			break;
		case "LeftWindow":
			obj = (gvUse ? ((object)cmpd.cmpdInfo.leftWindow) : cmpd.cmpdInfo.leftWindow.ToString(text));
			break;
		case "RightWindow":
			obj = (gvUse ? ((object)cmpd.cmpdInfo.rightWindow) : cmpd.cmpdInfo.rightWindow.ToString(text));
			break;
		case "HheatValue":
			obj = (gvUse ? ((object)cmpd.cmpdInfo.HheatValue) : cmpd.cmpdInfo.HheatValue.ToString(text));
			break;
		case "LheatValue":
			obj = (gvUse ? ((object)cmpd.cmpdInfo.LheatValue) : cmpd.cmpdInfo.LheatValue.ToString(text));
			break;
		case "PeakColor":
			obj = cmpd.cmpdInfo.color;
			break;
		case "IstdCmpd":
			if (cmpd.cmpdInfo.istdCmpd != null && cmpd.cmpdInfo.istdCmpd != "")
			{
				Compound compoundByName = GetCompoundByName(cmpd.cmpdInfo.istdCmpd);
				if (compoundByName != null)
				{
					obj = compoundByName.cmpdInfo.name;
				}
			}
			else
			{
				obj = "";
			}
			break;
		case "RespStyle":
			if (!gvUse)
			{
				if (cmpd.cmpdInfo.respStyle == RespStyle.Area)
				{
					obj = Lang.PS("面积", "Area");
				}
				else if (cmpd.cmpdInfo.respStyle == RespStyle.Height)
				{
					obj = Lang.PS("高度", "Height");
				}
				else if (cmpd.cmpdInfo.respStyle == RespStyle.AreaSquare)
				{
					obj = Lang.PS("面积平方根", "AreaSquare");
				}
				else if (cmpd.cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
				{
					obj = Lang.PS("高度平方根", "PeakHeightSquare");
				}
			}
			else
			{
				obj = cmpd.cmpdInfo.respStyle;
			}
			break;
		case "FreeRespFactor":
			obj = (gvUse ? ((object)cmpd.cmpdInfo.freeRespFactor) : cmpd.cmpdInfo.freeRespFactor.ToString(text));
			break;
		}
		if (!gvUse && obj == null)
		{
			obj = "";
		}
		return obj;
	}

	private void miCaliDeleteCmpd_Click(object sender, EventArgs e)
	{
		if (gvCmpds.SelectedRows != null && gvCmpds.SelectedRows.Count != 0)
		{
			Compound[] array = new Compound[gvCmpds.SelectedRows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = caliGnl_0.cmpds[i];
			}
			caliGnl_0.DeleteCmpds(array);
			caliGnl_0.CalculateFunc(appendLink: true);
			ReadComponentData();
		}
	}

	private void ReadAndWriteCaliGnlData(AccStyle accStyle_0)
	{
	}

	private void ReadAndWriteCompoundList(AccStyle accStyle_0)
	{
		gvCmpds.SuspendLayout();
		for (int i = 0; i < gvCmpds.RowCount; i++)
		{
			ReadAndWriteCompoundOneRow(i, accStyle_0);
		}
		gvCmpds.ResumeLayout();
	}

	private void ReadAndWriteCompoundOneRow(int int_5, AccStyle accStyle_0)
	{
		Compound compound_ = (gvCmpds.Rows[int_5].Tag as CompoundRowIdx).compound_0;
		object obj = null;
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			for (int j = 0; j < gvCmpds.ColumnCount; j++)
			{
				switch (gvCmpds.Columns[j].Name)
				{
				case "PeakColor":
					(gvCmpds.Rows[int_5].Cells[j] as LclgvColorCell).Color = compound_.cmpdInfo.color;
					continue;
				case "RespArea":
					obj = compound_.levels[m_iLevel].responseA;
					break;
				case "RespHeight":
					obj = compound_.levels[m_iLevel].responseH;
					break;
				case "Amount":
					obj = compound_.levels[m_iLevel].amount;
					break;
				case "RecordNumber":
					obj = compound_.levels[m_iLevel].SecsNum;
					break;
				default:
					obj = gvCmpdsValue(gvUse: true, compound_, gvCmpds.Columns[j].Name);
					break;
				}
				gvCmpds.Rows[int_5].Cells[j].Value = obj;
			}
			break;
		}
		case AccStyle.Write:
		{
			for (int i = 0; i < gvCmpds.ColumnCount; i++)
			{
				obj = gvCmpds.Rows[int_5].Cells[i].Value;
				string name;
				if (obj != null && (name = gvCmpds.Columns[i].Name) != null)
				{
					switch (name)
					{
					case "Used":
						compound_.used = (bool)obj;
						break;
					case "CpmdName":
						compound_.cmpdInfo.name = obj.ToString();
						break;
					case "PeakRT":
						compound_.cmpdInfo.retainTime = Class49.String2Float(obj, compound_.cmpdInfo.retainTime);
						break;
					case "LeftWindow":
						compound_.cmpdInfo.leftWindow = Class49.String2Float(obj, compound_.cmpdInfo.leftWindow);
						break;
					case "RightWindow":
						compound_.cmpdInfo.rightWindow = Class49.String2Float(obj, compound_.cmpdInfo.rightWindow);
						break;
					case "HheatValue":
						compound_.cmpdInfo.HheatValue = Class49.String2Float(obj, compound_.cmpdInfo.HheatValue);
						break;
					case "LheatValue":
						compound_.cmpdInfo.LheatValue = Class49.String2Float(obj, compound_.cmpdInfo.LheatValue);
						break;
					case "PeakColor":
						compound_.cmpdInfo.color = (gvCmpds.Rows[int_5].Cells[i] as LclgvColorCell).Color;
						break;
					case "RespStyle":
						compound_.cmpdInfo.respStyle = (RespStyle)obj;
						break;
					case "RespArea":
						compound_.levels[m_iLevel].responseA = Class49.String2Float(obj, compound_.levels[m_iLevel].responseA);
						break;
					case "RespHeight":
						compound_.levels[m_iLevel].responseH = Class49.String2Float(obj, compound_.levels[m_iLevel].responseH);
						break;
					case "FreeRespFactor":
						compound_.cmpdInfo.freeRespFactor = Class49.String2Float(obj, compound_.cmpdInfo.freeRespFactor);
						break;
					case "Amount":
						compound_.levels[m_iLevel].amount = Class49.String2Float(obj, compound_.levels[m_iLevel].amount);
						break;
					case "IstdCmpds":
						compound_.cmpdInfo.sl_IstdCmpdNo = (int)Class49.String2Float(obj, compound_.cmpdInfo.sl_IstdCmpdNo);
						break;
					}
				}
			}
			break;
		}
		}
	}

	private void gvCmpds_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void tcCmpds_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void lclbSave_Click(object sender, EventArgs e)
	{
	}

	private void lclbDelete_Click(object sender, EventArgs e)
	{
	}

	private void btnMethReset_Click(object sender, EventArgs e)
	{
		mtdMgr = new MtdSetup();
		Clear();
		ReadFromPrintPara();
		bUseSet_Click(null, null);
	}

	private void btnMethUse_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.管理员 && Class49.user_0.ULevel != User.Level.分析员)
		{
			MessageBox.Show(Lang.PS("没有编辑方法权限！", "No editing method permissions!"));
			return;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Title = Lang.PS("另存方法", "save as method");
		saveFileDialog.Filter = Class49.MakeFileFilter(".mtd");
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			lclButton1_Click(null, null);
			Application.DoEvents();
			WriteToMtdMgr();
			mtdMgr.SaveToFile(saveFileDialog.FileName);
			WriteToMtdMgr();
			mtdMgr.SaveToFile();
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", "另存方法", "另存方法:" + mtdMgr.strMtdShowName);
		}
	}

	private void btnMutiPoint_Click(object sender, EventArgs e)
	{
		ChromForm chromForm = new ChromForm();
		chromForm.InitFm();
		chromForm.Show();
	}

	private void sdaOpen_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开谱图", "open sdafile");
		openFileDialog.InitialDirectory = sysParam.strSdaDataFileDir;
		openFileDialog.Filter = Lang.PS("谱图文件") + "(*.sda)|*.sda";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string fileName = openFileDialog.FileName;
		sysParam.strSdaDataFileDir = Path.GetDirectoryName(fileName);
		sysParam.SaveParam();
		frmParam.strSdaFile = fileName;
		CurMtdMgr.chromInfoR.UvRange = fileName;
		tbSdaFileName.Text = Path.GetFileName(CurMtdMgr.chromInfoR.UvRange);
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(fileName, DetectorStyle.General);
		if (chromatogram != null)
		{
			Peak[] rltPeaks = chromatogram.RltPeaks;
			if (rltPeaks != null && rltPeaks.Length != 0)
			{
				CurMtdMgr.chromInfoR.UvwsStartT = rltPeaks[0].amount;
			}
		}
	}

	public void caliAddChromatogram(Chromatogram chromatogram)
	{
		caliChromSource.Add(chromatogram);
		Peak[] peakAllCompound = chromatogram.GetPeakAllCompound();
		calibraGridModel item = new calibraGridModel
		{
			ID = (caliSource.Count - 2).ToString(),
			RT1 = peakAllCompound[0].pkRT,
			Height1 = peakAllCompound[0].height,
			Area1 = peakAllCompound[0].area,
			RT2 = peakAllCompound[1].pkRT,
			Area2 = peakAllCompound[1].area,
			Height2 = peakAllCompound[1].height
		};
		caliSource.Insert(caliSource.Count - 2, item);
		dGCalibration.DataSource = caliSource;
		dGCalibration.ReloadSource();
	}

	public void caliComplete()
	{
		btnStartCali.BtnText = "开始校正";
		cdlMgr.formMain.bStart1 = false;
		tabControl3.SelectedIndex = 1;
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		if (bCali)
		{
			Invoke((MethodInvoker)delegate
			{
				LoadChromFile(fileName);
				caliAddChromatogram(chromatogram);
				if (caliCnt > 0)
				{
					caliCnt--;
					if (caliCnt <= 0)
					{
						bCali = false;
						caliComplete();
					}
				}
				else
				{
					bCali = false;
					caliComplete();
				}
			});
		}
		else
		{
			string[] array = new string[10];
			peakComp = chromatogram.GetPeakAllCompound();
			array[0] = peakComp[0].amount.ToString("F" + Class49.int_8);
			array[1] = peakComp[1].amount.ToString("F" + Class49.int_8);
			array[2] = ((peakComp[0].amount > peakComp[1].amount) ? (peakComp[0].amount - peakComp[1].amount) : 0f).ToString("F" + Class49.int_8);
			array[3] = peakComp[0].pkRT.ToString("F" + Class49.int_8);
			array[4] = peakComp[1].pkRT.ToString("F" + Class49.int_8);
			array[5] = lythcParamMgr.strCollectSJDW;
			array[6] = lythcParamMgr.strCollectSite;
			array[7] = lythcParamMgr.strCollectJYDW;
			array[8] = lythcParamMgr.strCollectP;
			array[9] = lythcParamMgr.strUnit;
			Class49.InsertIntoPortableSql(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), lythcParamMgr.strCollectSite, fileName, array);
		}
	}

	private void btnCycleAdd_BtnClick(object sender, EventArgs e)
	{
	}

	public void applyRtToSig()
	{
		float result = 0f;
		float num = 0f;
		try
		{
			float.TryParse(gvCmpds.Rows[0].Cells[2].Value.ToString(), out result);
			num = mtdMgr.sigIntegrations[0].IntegRows[4].timeB - mtdMgr.sigIntegrations[0].IntegRows[4].timeA;
			mtdMgr.sigIntegrations[0].IntegRows[4].timeA = result - num / 2f;
			mtdMgr.sigIntegrations[0].IntegRows[4].timeB = result + num / 2f;
			float.TryParse(gvCmpds.Rows[1].Cells[2].Value.ToString(), out result);
			num = mtdMgr.sigIntegrations[0].IntegRows[5].timeB - mtdMgr.sigIntegrations[0].IntegRows[5].timeA;
			mtdMgr.sigIntegrations[0].IntegRows[5].timeA = result - num / 2f;
			mtdMgr.sigIntegrations[0].IntegRows[5].timeB = result + num / 2f;
			RefreshIntegrations(AccStyle.Read);
		}
		catch
		{
		}
	}

	private void btnApply_BtnClick(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.访问员)
		{
			MessageBox.Show(Lang.PS("没有调用方法权限！", "Without permission "));
			return;
		}
		if (caliGnl_0 != null)
		{
			btnApply.Focus();
			applyRtToSig();
			for (byte b = 0; b < caliGnl_0.cmpds.Length; b++)
			{
				float freeRespFactor = caliGnl_0.cmpds[b].cmpdInfo.freeRespFactor;
				if (freeRespFactor == 0f)
				{
					caliGnl_0.CalculateFunc(appendLink: false);
					freeRespFactor = caliGnl_0.cmpds[b].levels[0].respFactor;
					if (gvCmpds.Rows.Count > 0)
					{
						gvCmpds.Rows[b].Cells["FreeRespFactor"].Value = freeRespFactor;
					}
				}
			}
		}
		UsePara();
		if (this.OnMethodSaveEvent != null)
		{
			this.OnMethodSaveEvent(this, new EventArgs());
		}
	}

	private void LoadChromFile(string strChromName)
	{
		try
		{
			if (strChromName == null)
			{
				return;
			}
			DetectorStyle detectorStyle = DetectorStyle.General;
			Chromatogram chromatogram = Chromatogram.LoadFromFile2(strChromName, detectorStyle);
			if (chromatogram != null)
			{
				chromatogram.signal.disColor = Color.Blue;
				chromDisplay_0.stDisChain.Clear();
				chromDisplay_0.SetFullDisLg(ref disLg_0, chromatogram.signal, second: true);
				float lgX = chromatogram.disLg.lgX;
				int num = (int)lgX;
				if (!float.IsNaN(lgX) && !float.IsInfinity(lgX) && (float)num > 0f)
				{
					chromDisplay_0.stDisChain.AppendFrameLg(chromatogram.disLg);
				}
				Chromatogram[] chroms = new Chromatogram[1] { chromatogram };
				int setChromNo = 0;
				chromDisplay_0.LinkDisChroms(chroms, ref setChromNo);
				dpgnlChrom.Refresh();
			}
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("MstSetPortable.LoadChromFile ," + ex.Message);
		}
	}

	private void dpgnlChrom_Paint(object sender, PaintEventArgs e)
	{
		try
		{
			if (chromDisplay_0 != null)
			{
				chromDisplay_0.Draw(e.Graphics, erase: true);
			}
		}
		catch (Exception)
		{
		}
	}

	public void startCali()
	{
		caliChromSource.Clear();
		caliSource.Clear();
		if (FormMainPortable.fromMain != null)
		{
			FormMainPortable.fromMain.cntAnalyze1 = 0u;
		}
		calibraGridModel item = new calibraGridModel
		{
			ID = "RSD",
			RT1 = 1f
		};
		caliSource.Add(item);
		calibraGridModel item2 = new calibraGridModel
		{
			ID = "AVR",
			RT1 = 1f
		};
		caliSource.Add(item2);
		dGCalibration.DataSource = caliSource;
		dGCalibration.ReloadSource();
	}

	private void btnStartCali_BtnClick(object sender, EventArgs e)
	{
		bCali = true;
		caliCnt = (int)ucNTBCnt.Num;
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			if (btnStartCali.BtnText == "开始校正")
			{
				startCali();
				btnStartCali.BtnText = "结束校正";
				currentTcpServerSocket.SendCmd(18);
			}
			else
			{
				btnStartCali.BtnText = "开始校正";
				caliCnt = 0;
				currentTcpServerSocket.SendCmd(19);
			}
		}
	}

	private void ucNTBCnt_NumChanged(object sender, EventArgs e)
	{
		caliCnt = (int)ucNTBCnt.Num;
	}

	private void ucBtnCaliApply_BtnClick(object sender, EventArgs e)
	{
		if (dGCalibration.SelectRow != null)
		{
			float amount = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].amount;
			float amount2 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[1].levels[0].amount;
			cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].responseA = fAver1;
			cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[1].levels[0].responseA = fAver2;
			cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].responseH = fHeightAVer1;
			cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[1].levels[0].responseH = fHeightAVer2;
			cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
			cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
			serieLines[0].Points.Clear();
			serieLines[0].Points.AddXY(0.0, 0.0);
			serieLines[0].Points.AddXY(cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].amount, fAver1);
			serieLines[1].Points.Clear();
			serieLines[1].Points.AddXY(0.0, 0.0);
			serieLines[1].Points.AddXY(cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[1].levels[0].amount, fAver2);
			chartPlots.ChartAreas[0].AxisX.Maximum = ((amount > amount2) ? (amount * 1.1f) : (amount2 * 1.1f));
			chartPlots.ChartAreas[0].AxisY.Maximum = ((fAver1 > fAver2) ? (fAver1 * 1.1f) : (fAver2 * 1.1f));
			btnApply_BtnClick(null, null);
		}
	}

	public void dGCalibration_HeadClick(object sender, DataGridViewEventArgs e)
	{
	}

	private void dGCalibration_ItemClick(object sender, DataGridViewEventArgs e)
	{
		if (e.CellIndex != 0)
		{
			return;
		}
		float num = 0f;
		int num2 = 0;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float[] array = new float[0];
		float[] array2 = new float[0];
		float[] array3 = new float[0];
		float[] array4 = new float[0];
		float[] array5 = new float[0];
		float[] array6 = new float[0];
		for (int i = 0; i < caliSource.Count - 2; i++)
		{
			if (dGCalibration.Rows[i].IsChecked)
			{
				num2++;
				Array.Resize(ref array, num2);
				Array.Resize(ref array2, num2);
				Array.Resize(ref array3, num2);
				Array.Resize(ref array4, num2);
				Array.Resize(ref array5, num2);
				Array.Resize(ref array6, num2);
				array[num2 - 1] = caliSource[i].RT1;
				array2[num2 - 1] = caliSource[i].RT2;
				array3[num2 - 1] = caliSource[i].Area1;
				array4[num2 - 1] = caliSource[i].Area2;
				array5[num2 - 1] = caliSource[i].Height1;
				array6[num2 - 1] = caliSource[i].Height2;
				num4 += caliSource[i].RT1;
				num5 += caliSource[i].RT2;
				num6 += caliSource[i].Area1;
				num7 += caliSource[i].Area2;
				num8 += caliSource[i].Height1;
				num9 += caliSource[i].Height2;
			}
		}
		num3 = num4 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array, num2);
		caliSource[caliSource.Count - 2].RT1 = num;
		caliSource[caliSource.Count - 1].RT1 = num3;
		num3 = num5 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array2, num2);
		caliSource[caliSource.Count - 2].RT2 = num;
		caliSource[caliSource.Count - 1].RT2 = num3;
		fAver1 = num6 / (float)num2;
		num = (float)Program.RSDCalculate(fAver1, array3, num2);
		caliSource[caliSource.Count - 2].Area1 = num;
		caliSource[caliSource.Count - 1].Area1 = fAver1;
		fHeightAVer1 = num8 / (float)num2;
		num = (float)Program.RSDCalculate(fHeightAVer1, array5, num2);
		caliSource[caliSource.Count - 1].Height1 = fHeightAVer1;
		fAver2 = num7 / (float)num2;
		num = (float)Program.RSDCalculate(fAver2, array4, num2);
		caliSource[caliSource.Count - 2].Area2 = num;
		caliSource[caliSource.Count - 1].Area2 = fAver2;
		fHeightAVer2 = num9 / (float)num2;
		num = (float)Program.RSDCalculate(fHeightAVer2, array6, num2);
		caliSource[caliSource.Count - 1].Height2 = fHeightAVer2;
		dGCalibration.DataSource = caliSource;
		dGCalibration.ReloadSource();
	}

	private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
	{
	}

	private void ucBtnPrintf_BtnClick(object sender, EventArgs e)
	{
		printDocument1.Print();
	}

	private void ucBtnSite_BtnClick(object sender, EventArgs e)
	{
		lythcParamMgr.strCollectSJDW = tbCollectSJDW.InputText;
		lythcParamMgr.strCollectJYDW = tbCollectJYDW.InputText;
		lythcParamMgr.strCollectSite = tbCollectSite.InputText;
		lythcParamMgr.strCollectP = tbCollectP.InputText;
		lythcParamMgr.SaveParam();
		createSiteDB(lythcParamMgr.strCollectSite);
	}

	public void createSiteDB(string strDB)
	{
		string text = Application.StartupPath + "\\ngmpolPortable.dll";
		SQLiteConnection.CreateFile("MyDatabase.sqlite");
		SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
		sQLiteConnection.Open();
		string text2 = " ";
		text2 = text2 + " CREATE TABLE [" + strDB + "] ( ";
		text2 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
		text2 += "[总烃] FLOAT NOT NULL ON CONFLICT FAIL,  ";
		text2 += "[甲烷] FLOAT NOT NULL ON CONFLICT FAIL,  ";
		text2 += "[非甲烷总烃] FLOAT NOT NULL ON CONFLICT FAIL, ";
		text2 += "[总烃保留时间] FLOAT NOT NULL ON CONFLICT FAIL,  ";
		text2 += "[甲烷保留时间] FLOAT NOT NULL ON CONFLICT FAIL,  ";
		text2 += "[被测单位] STRING ,  ";
		text2 += "[采样地点] STRING ,  ";
		text2 += "[检测单位] STRING , ";
		text2 += "[检测员] STRING , ";
		text2 += "[单位] STRING , ";
		text2 += "[谱图] STRING );  ";
		SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
		sQLiteCommand.ExecuteNonQuery();
	}

	private void ucCBSites_SelectedChangedEvent(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			lythcParamMgr.strUnit = ucCBSites.TextValue;
			lythcParamMgr.SaveParam();
		}
	}

	private void ucBtnApplly_BtnClick(object sender, EventArgs e)
	{
		UsePara();
		if (this.OnMethodSaveEvent != null)
		{
			this.OnMethodSaveEvent(this, new EventArgs());
		}
		btnApply_BtnClick(null, null);
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.MstSetPortable));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		this.btnMutiPoint = new System.Windows.Forms.Button();
		this.gbMethods = new System.Windows.Forms.GroupBox();
		this.btnAdd1 = new System.Windows.Forms.Button();
		this.btnAdd2 = new System.Windows.Forms.Button();
		this.label10 = new System.Windows.Forms.Label();
		this.MethodOpen2 = new System.Windows.Forms.Button();
		this.tbMethName2 = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.MethodOpen1 = new System.Windows.Forms.Button();
		this.tbMethName1 = new System.Windows.Forms.TextBox();
		this.MethodSave = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.MethodReSave = new System.Windows.Forms.Button();
		this.MethodNew = new System.Windows.Forms.Button();
		this.MethodOpen = new System.Windows.Forms.Button();
		this.tbMethName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.bUseSet = new System.Windows.Forms.Button();
		this.tabControl3 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.label20 = new System.Windows.Forms.Label();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.label14 = new System.Windows.Forms.Label();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.label15 = new System.Windows.Forms.Label();
		this.btnMethUse = new System.Windows.Forms.Button();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.tbProgram = new System.Windows.Forms.TextBox();
		this.tbEvent = new System.Windows.Forms.TextBox();
		this.btnMethReset = new System.Windows.Forms.Button();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.label16 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label17 = new System.Windows.Forms.Label();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.Ljyq10 = new System.Windows.Forms.Label();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.Ljyq1 = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.gvExtEvTP = new System.Windows.Forms.DataGridView();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.panel12 = new System.Windows.Forms.Panel();
		this.maskedTextBox8 = new System.Windows.Forms.MaskedTextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.panelJY = new System.Windows.Forms.Panel();
		this.panel15 = new System.Windows.Forms.Panel();
		this.label13 = new System.Windows.Forms.Label();
		this.maskedTextBox5 = new System.Windows.Forms.MaskedTextBox();
		this.dgInsamp1 = new System.Windows.Forms.DataGridView();
		this.label6 = new System.Windows.Forms.Label();
		this.maskedTextBox9 = new System.Windows.Forms.MaskedTextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.comboBox6 = new System.Windows.Forms.ComboBox();
		this.panel7 = new System.Windows.Forms.Panel();
		this.radioButton25 = new System.Windows.Forms.RadioButton();
		this.radioButton5 = new System.Windows.Forms.RadioButton();
		this.radioButton6 = new System.Windows.Forms.RadioButton();
		this.panel6 = new System.Windows.Forms.Panel();
		this.radioButton3 = new System.Windows.Forms.RadioButton();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton9 = new System.Windows.Forms.RadioButton();
		this.label11 = new System.Windows.Forms.Label();
		this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
		this.radioButton28 = new System.Windows.Forms.RadioButton();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.tabControl2 = new System.Windows.Forms.TabControl();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.tabPage7 = new System.Windows.Forms.TabPage();
		this.tabPage8 = new System.Windows.Forms.TabPage();
		this.tabPage9 = new System.Windows.Forms.TabPage();
		this.tabPage10 = new System.Windows.Forms.TabPage();
		this.tabPage15 = new System.Windows.Forms.TabPage();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.lbptInitT = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.tbptIniTempHoldT = new System.Windows.Forms.TextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.dgvCT6 = new System.Windows.Forms.DataGridView();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.tabPage11 = new System.Windows.Forms.TabPage();
		this.label23 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.ucSplitLabel4 = new HZH_Controls.Controls.UCSplitLabel();
		this.dGCalibration = new HZH_Controls.Controls.UCDataGridView();
		this.ucBtnCaliApply = new HZH_Controls.Controls.UCBtnExt();
		this.tabPage12 = new System.Windows.Forms.TabPage();
		this.chartPlots = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage17 = new System.Windows.Forms.TabPage();
		this.ucBtnApplly = new HZH_Controls.Controls.UCBtnExt();
		this.ucKeyBorderNum1 = new HZH_Controls.Controls.UCKeyBorderNum();
		this.cmsIntegration = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miIntegAppendRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegDeleteRows = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegInsertRow = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegResetRows = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegRowsDown = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegRowsUp = new System.Windows.Forms.ToolStripMenuItem();
		this.spcIntegComponent = new System.Windows.Forms.SplitContainer();
		this.groupBox18 = new System.Windows.Forms.GroupBox();
		this.GbCmpds = new System.Windows.Forms.GroupBox();
		this.tabPage19 = new System.Windows.Forms.TabPage();
		this.tbCollectP = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbCollectJYDW = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbCollectSite = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbCollectSJDW = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucBtnSite = new HZH_Controls.Controls.UCBtnExt();
		this.label34 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.label69 = new System.Windows.Forms.Label();
		this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
		this.cbRptJYTime = new System.Windows.Forms.CheckBox();
		this.cbRptWithResult = new System.Windows.Forms.CheckBox();
		this.cbRptWithSourceData = new System.Windows.Forms.CheckBox();
		this.cbRptChromBold = new System.Windows.Forms.CheckBox();
		this.cbRptBoundary = new System.Windows.Forms.CheckBox();
		this.cbRptWithChrom = new System.Windows.Forms.CheckBox();
		this.cbRptFileName = new System.Windows.Forms.CheckBox();
		this.cbRptWithTailFactor = new System.Windows.Forms.CheckBox();
		this.cbRptWithCapFactor = new System.Windows.Forms.CheckBox();
		this.cbRptWithValidNumber = new System.Windows.Forms.CheckBox();
		this.cbRptWithZhuXiao = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakResulution = new System.Windows.Forms.CheckBox();
		this.cbRptWithCoef = new System.Windows.Forms.CheckBox();
		this.cbRptWithCurve = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakSingle = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakHalf = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakHeightPercent = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakAreaPercent = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakHeight = new System.Windows.Forms.CheckBox();
		this.cbRptWithDensityPercent = new System.Windows.Forms.CheckBox();
		this.cbRptWithPeakArea = new System.Windows.Forms.CheckBox();
		this.cbRptWithDensity = new System.Windows.Forms.CheckBox();
		this.cbRptCorrFactor = new System.Windows.Forms.CheckBox();
		this.cbRptZuFenName = new System.Windows.Forms.CheckBox();
		this.cbRptKeepTime = new System.Windows.Forms.CheckBox();
		this.cbRptWithIdx = new System.Windows.Forms.CheckBox();
		this.cbRptPrintTime = new System.Windows.Forms.CheckBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label56 = new System.Windows.Forms.Label();
		this.ReportTitle = new System.Windows.Forms.TextBox();
		this.label68 = new System.Windows.Forms.Label();
		this.groupBox17 = new System.Windows.Forms.GroupBox();
		this.rptbotom = new System.Windows.Forms.TextBox();
		this.groupBox15 = new System.Windows.Forms.GroupBox();
		this.rpthead = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.radioButton8 = new System.Windows.Forms.RadioButton();
		this.radioButton4 = new System.Windows.Forms.RadioButton();
		this.radioButton7 = new System.Windows.Forms.RadioButton();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.设为参比峰ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
		this.cmsCali = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miAddrow = new System.Windows.Forms.ToolStripMenuItem();
		this.miColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.miDelRow = new System.Windows.Forms.ToolStripMenuItem();
		this.printDocument1 = new System.Drawing.Printing.PrintDocument();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.btnApply = new HZH_Controls.Controls.UCBtnExt();
		this.label25 = new System.Windows.Forms.Label();
		this.ucCBSites = new HZH_Controls.Controls.UCCombox();
		this.ucNTBCnt = new HZH_Controls.Controls.UCNumTextBox();
		this.btnStartCali = new HZH_Controls.Controls.UCBtnExt();
		this.label21 = new System.Windows.Forms.Label();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.gvgcProgTemp = new IBrainChrom2018.LclGridView();
		this.lclButton2 = new IBrainChrom2018.LclButton();
		this.lclButton1 = new IBrainChrom2018.LclButton();
		this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6CN = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6SetT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pnlcu = new IBrainChrom2018.LclPanel();
		this.spcComponent = new System.Windows.Forms.SplitContainer();
		this.gbO = new System.Windows.Forms.GroupBox();
		this.sdaOpen = new System.Windows.Forms.Button();
		this.tbSdaFileName = new System.Windows.Forms.TextBox();
		this.gbCalibration = new IBrainChrom2018.LclGroupBox();
		this.btncclNew = new IBrainChrom2018.LclButton();
		this.btncclView = new IBrainChrom2018.LclButton();
		this.btncclNone = new IBrainChrom2018.LclButton();
		this.btncclSet = new IBrainChrom2018.LclButton();
		this.tbcclCalibration = new IBrainChrom2018.LclTextBox();
		this.gbcuRltTableReport = new IBrainChrom2018.LclGroupBox();
		this.rbrtrCaliPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbrtrIdentifiedPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbrtrAllDetectedPeaks = new IBrainChrom2018.LclRadioButton();
		this.cbrtrHideISTDPeak = new IBrainChrom2018.LclCheckBox();
		this.lclGroupBox1 = new IBrainChrom2018.LclGroupBox();
		this.tbcuAmount = new IBrainChrom2018.LclTextBox();
		this.lbcuAmount = new IBrainChrom2018.LclLabel();
		this.lbcuIstdAmount = new IBrainChrom2018.LclLabel();
		this.tbcuIstdAmount = new IBrainChrom2018.LclTextBox();
		this.gbcuScale = new IBrainChrom2018.LclGroupBox();
		this.cbprsUseScaleFactor = new IBrainChrom2018.LclCheckBox();
		this.lbcuUnitAfterScale = new IBrainChrom2018.LclLabel();
		this.lbcuScaleFactor = new IBrainChrom2018.LclLabel();
		this.tbprsUnitAfterScale = new IBrainChrom2018.LclTextBox();
		this.lbcuDilution = new IBrainChrom2018.LclLabel();
		this.tbprsScaleFactor = new IBrainChrom2018.LclTextBox();
		this.tbcuDilution = new IBrainChrom2018.LclTextBox();
		this.lbcuInjVolume = new IBrainChrom2018.LclLabel();
		this.tbcuInjVolume = new IBrainChrom2018.LclTextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cbprsUncalBase = new IBrainChrom2018.LclCusComboBox();
		this.lbcuUncalAmtRespF = new IBrainChrom2018.LclLabel();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.tbprsUncalAmtRespF = new IBrainChrom2018.LclTextBox();
		this.TipLable = new System.Windows.Forms.Label();
		this.lclRbInner = new IBrainChrom2018.LclRadioButton();
		this.lclRbOuter = new IBrainChrom2018.LclRadioButton();
		this.lclRbNo = new IBrainChrom2018.LclRadioButton();
		this.gbadvAddSub = new IBrainChrom2018.LclGroupBox();
		this.btnasNoneChrom = new IBrainChrom2018.LclButton();
		this.rbasSub = new IBrainChrom2018.LclRadioButton();
		this.rbasAdd = new IBrainChrom2018.LclRadioButton();
		this.cbasMatching = new IBrainChrom2018.LclCusComboBox();
		this.btnasSetChrom = new System.Windows.Forms.Button();
		this.tbasChrom = new IBrainChrom2018.LclTextBox();
		this.lbasMatching = new IBrainChrom2018.LclLabel();
		this.lbasChrom = new IBrainChrom2018.LclLabel();
		this.gbadvColumnCalcu = new IBrainChrom2018.LclGroupBox();
		this.rbccFrom50per = new IBrainChrom2018.LclRadioButton();
		this.rbccStatistical = new IBrainChrom2018.LclRadioButton();
		this.tbccColumnLength = new IBrainChrom2018.LclTextBox();
		this.lbccColumnLengthU = new IBrainChrom2018.LclLabel();
		this.tbccUnretainedPeak = new IBrainChrom2018.LclTextBox();
		this.lbccColumnLength = new IBrainChrom2018.LclLabel();
		this.lbccUnretainedPeakU = new IBrainChrom2018.LclLabel();
		this.lbccUnretainedPeak = new IBrainChrom2018.LclLabel();
		this.gvCmpds = new IBrainChrom2018.LclCombineCGridView();
		this.dpgnlChrom = new IBrainChrom2018.LclDisplayPanel();
		this.gvInteg = new IBrainChrom2018.LclIntegGridView();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.事件 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.gbMethods.SuspendLayout();
		this.tabControl3.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.groupBox7.SuspendLayout();
		this.groupBox8.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvExtEvTP).BeginInit();
		this.tabPage2.SuspendLayout();
		this.panel12.SuspendLayout();
		this.panelJY.SuspendLayout();
		this.panel15.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgInsamp1).BeginInit();
		this.panel7.SuspendLayout();
		this.panel6.SuspendLayout();
		this.tabControl2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).BeginInit();
		this.tabPage4.SuspendLayout();
		this.tabPage5.SuspendLayout();
		this.tabPage11.SuspendLayout();
		this.tabPage12.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chartPlots).BeginInit();
		this.tabPage17.SuspendLayout();
		this.cmsIntegration.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.spcIntegComponent).BeginInit();
		this.spcIntegComponent.Panel1.SuspendLayout();
		this.spcIntegComponent.Panel2.SuspendLayout();
		this.spcIntegComponent.SuspendLayout();
		this.tabPage19.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
		this.groupBox17.SuspendLayout();
		this.groupBox15.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.cmsCali.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvgcProgTemp).BeginInit();
		this.pnlcu.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.spcComponent).BeginInit();
		this.spcComponent.Panel1.SuspendLayout();
		this.spcComponent.SuspendLayout();
		this.gbO.SuspendLayout();
		this.gbCalibration.SuspendLayout();
		this.gbcuRltTableReport.SuspendLayout();
		this.lclGroupBox1.SuspendLayout();
		this.gbcuScale.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.gbadvAddSub.SuspendLayout();
		this.gbadvColumnCalcu.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvCmpds).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvInteg).BeginInit();
		base.SuspendLayout();
		this.btnMutiPoint.Location = new System.Drawing.Point(347, 12);
		this.btnMutiPoint.Name = "btnMutiPoint";
		this.btnMutiPoint.Size = new System.Drawing.Size(80, 50);
		this.btnMutiPoint.TabIndex = 38;
		this.btnMutiPoint.Text = "新建多点标定";
		this.btnMutiPoint.UseVisualStyleBackColor = true;
		this.btnMutiPoint.Visible = false;
		this.btnMutiPoint.Click += new System.EventHandler(btnMutiPoint_Click);
		this.gbMethods.Controls.Add(this.btnAdd1);
		this.gbMethods.Controls.Add(this.btnAdd2);
		this.gbMethods.Controls.Add(this.label10);
		this.gbMethods.Controls.Add(this.MethodOpen2);
		this.gbMethods.Controls.Add(this.tbMethName2);
		this.gbMethods.Controls.Add(this.label4);
		this.gbMethods.Controls.Add(this.MethodOpen1);
		this.gbMethods.Controls.Add(this.tbMethName1);
		this.gbMethods.Location = new System.Drawing.Point(435, 7);
		this.gbMethods.Name = "gbMethods";
		this.gbMethods.Size = new System.Drawing.Size(342, 83);
		this.gbMethods.TabIndex = 37;
		this.gbMethods.TabStop = false;
		this.gbMethods.Text = "子方法";
		this.btnAdd1.Location = new System.Drawing.Point(285, 53);
		this.btnAdd1.Name = "btnAdd1";
		this.btnAdd1.Size = new System.Drawing.Size(75, 23);
		this.btnAdd1.TabIndex = 37;
		this.btnAdd1.Text = "加入";
		this.btnAdd1.UseVisualStyleBackColor = true;
		this.btnAdd1.Click += new System.EventHandler(BtnAdd1_Click);
		this.btnAdd2.Location = new System.Drawing.Point(285, 14);
		this.btnAdd2.Name = "btnAdd2";
		this.btnAdd2.Size = new System.Drawing.Size(75, 23);
		this.btnAdd2.TabIndex = 36;
		this.btnAdd2.Text = "加入";
		this.btnAdd2.UseVisualStyleBackColor = true;
		this.btnAdd2.Click += new System.EventHandler(BtnAdd2_Click);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(6, 18);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(101, 12);
		this.label10.TabIndex = 35;
		this.label10.Text = "非甲烷总烃方法：";
		this.MethodOpen2.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen2.Location = new System.Drawing.Point(238, 8);
		this.MethodOpen2.Name = "MethodOpen2";
		this.MethodOpen2.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen2.TabIndex = 34;
		this.MethodOpen2.UseVisualStyleBackColor = true;
		this.MethodOpen2.Click += new System.EventHandler(MethodOpen2_Click);
		this.tbMethName2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName2.Location = new System.Drawing.Point(110, 14);
		this.tbMethName2.Name = "tbMethName2";
		this.tbMethName2.ReadOnly = true;
		this.tbMethName2.Size = new System.Drawing.Size(117, 21);
		this.tbMethName2.TabIndex = 33;
		this.tbMethName2.Text = "默认";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 60);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(77, 12);
		this.label4.TabIndex = 32;
		this.label4.Text = "苯系物方法：";
		this.MethodOpen1.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen1.Location = new System.Drawing.Point(238, 48);
		this.MethodOpen1.Name = "MethodOpen1";
		this.MethodOpen1.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen1.TabIndex = 31;
		this.MethodOpen1.UseVisualStyleBackColor = true;
		this.MethodOpen1.Click += new System.EventHandler(MethodOpen1_Click);
		this.tbMethName1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName1.Location = new System.Drawing.Point(110, 52);
		this.tbMethName1.Name = "tbMethName1";
		this.tbMethName1.ReadOnly = true;
		this.tbMethName1.Size = new System.Drawing.Size(117, 21);
		this.tbMethName1.TabIndex = 30;
		this.tbMethName1.Text = "默认";
		this.MethodSave.Location = new System.Drawing.Point(74, 39);
		this.MethodSave.Name = "MethodSave";
		this.MethodSave.Size = new System.Drawing.Size(55, 23);
		this.MethodSave.TabIndex = 34;
		this.MethodSave.Text = "保存";
		this.MethodSave.UseVisualStyleBackColor = true;
		this.MethodSave.Click += new System.EventHandler(MethodSave_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(-2, 62);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(437, 12);
		this.label2.TabIndex = 33;
		this.label2.Text = "————————————————————————————————————";
		this.MethodReSave.Location = new System.Drawing.Point(136, 39);
		this.MethodReSave.Name = "MethodReSave";
		this.MethodReSave.Size = new System.Drawing.Size(55, 23);
		this.MethodReSave.TabIndex = 30;
		this.MethodReSave.Text = "另存";
		this.MethodReSave.UseVisualStyleBackColor = true;
		this.MethodReSave.Click += new System.EventHandler(MethodReSave_Click);
		this.MethodNew.Location = new System.Drawing.Point(8, 39);
		this.MethodNew.Name = "MethodNew";
		this.MethodNew.Size = new System.Drawing.Size(55, 23);
		this.MethodNew.TabIndex = 32;
		this.MethodNew.Text = "新建";
		this.MethodNew.UseVisualStyleBackColor = true;
		this.MethodNew.Click += new System.EventHandler(MethodNew_Click);
		this.MethodOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen.Location = new System.Drawing.Point(201, 7);
		this.MethodOpen.Name = "MethodOpen";
		this.MethodOpen.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen.TabIndex = 29;
		this.MethodOpen.UseVisualStyleBackColor = true;
		this.MethodOpen.Click += new System.EventHandler(MethodOpen_Click);
		this.tbMethName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName.Location = new System.Drawing.Point(73, 11);
		this.tbMethName.Name = "tbMethName";
		this.tbMethName.ReadOnly = true;
		this.tbMethName.Size = new System.Drawing.Size(117, 21);
		this.tbMethName.TabIndex = 28;
		this.tbMethName.Text = "默认";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(4, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 27;
		this.label1.Text = "方法文件:";
		this.bUseSet.BackColor = System.Drawing.Color.Lime;
		this.bUseSet.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.bUseSet.Location = new System.Drawing.Point(238, 8);
		this.bUseSet.Name = "bUseSet";
		this.bUseSet.Size = new System.Drawing.Size(115, 51);
		this.bUseSet.TabIndex = 26;
		this.bUseSet.Text = "应用设置";
		this.bUseSet.UseVisualStyleBackColor = false;
		this.bUseSet.Click += new System.EventHandler(bUseSet_Click);
		this.tabControl3.Controls.Add(this.tabPage1);
		this.tabControl3.Controls.Add(this.tabPage4);
		this.tabControl3.Controls.Add(this.tabPage5);
		this.tabControl3.Controls.Add(this.tabPage11);
		this.tabControl3.Controls.Add(this.tabPage12);
		this.tabControl3.Controls.Add(this.tabPage17);
		this.tabControl3.Controls.Add(this.tabPage19);
		this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl3.Location = new System.Drawing.Point(0, 0);
		this.tabControl3.Name = "tabControl3";
		this.tabControl3.SelectedIndex = 0;
		this.tabControl3.Size = new System.Drawing.Size(783, 548);
		this.tabControl3.TabIndex = 18;
		this.tabPage1.AutoScroll = true;
		this.tabPage1.Controls.Add(this.gvgcProgTemp);
		this.tabPage1.Controls.Add(this.groupBox7);
		this.tabPage1.Controls.Add(this.btnMethUse);
		this.tabPage1.Controls.Add(this.groupBox8);
		this.tabPage1.Controls.Add(this.btnMethReset);
		this.tabPage1.Controls.Add(this.groupBox6);
		this.tabPage1.Controls.Add(this.groupBox5);
		this.tabPage1.Controls.Add(this.lclButton2);
		this.tabPage1.Controls.Add(this.lclButton1);
		this.tabPage1.Controls.Add(this.tabControl1);
		this.tabPage1.Controls.Add(this.groupBox4);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(775, 522);
		this.tabPage1.TabIndex = 4;
		this.tabPage1.Text = "方法仪器参数";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.groupBox7.Controls.Add(this.label20);
		this.groupBox7.Controls.Add(this.checkBox6);
		this.groupBox7.Controls.Add(this.label14);
		this.groupBox7.Controls.Add(this.checkBox7);
		this.groupBox7.Controls.Add(this.label15);
		this.groupBox7.Location = new System.Drawing.Point(90, 1841);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(333, 124);
		this.groupBox7.TabIndex = 23;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "检测器";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(152, 9);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(11, 252);
		this.label20.TabIndex = 5;
		this.label20.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|";
		this.checkBox6.AutoSize = true;
		this.checkBox6.Location = new System.Drawing.Point(173, 16);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(66, 16);
		this.checkBox6.TabIndex = 3;
		this.checkBox6.Text = "检测器2";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox6.CheckedChanged += new System.EventHandler(checkBox6_CheckedChanged);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label14.ForeColor = System.Drawing.Color.Black;
		this.label14.Location = new System.Drawing.Point(173, 36);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(0, 14);
		this.label14.TabIndex = 4;
		this.checkBox7.AutoSize = true;
		this.checkBox7.Location = new System.Drawing.Point(9, 16);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(66, 16);
		this.checkBox7.TabIndex = 1;
		this.checkBox7.Text = "检测器1";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox7.CheckedChanged += new System.EventHandler(checkBox7_CheckedChanged);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label15.ForeColor = System.Drawing.Color.Black;
		this.label15.Location = new System.Drawing.Point(10, 36);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(0, 14);
		this.label15.TabIndex = 2;
		this.btnMethUse.Location = new System.Drawing.Point(818, 2055);
		this.btnMethUse.Name = "btnMethUse";
		this.btnMethUse.Size = new System.Drawing.Size(81, 47);
		this.btnMethUse.TabIndex = 36;
		this.btnMethUse.Text = "方法保存";
		this.btnMethUse.UseVisualStyleBackColor = true;
		this.btnMethUse.Click += new System.EventHandler(btnMethUse_Click);
		this.groupBox8.Controls.Add(this.tbProgram);
		this.groupBox8.Controls.Add(this.tbEvent);
		this.groupBox8.Location = new System.Drawing.Point(91, 2045);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(333, 190);
		this.groupBox8.TabIndex = 23;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "程升及事件";
		this.tbProgram.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbProgram.Location = new System.Drawing.Point(0, 19);
		this.tbProgram.Multiline = true;
		this.tbProgram.Name = "tbProgram";
		this.tbProgram.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
		this.tbProgram.Size = new System.Drawing.Size(162, 162);
		this.tbProgram.TabIndex = 2;
		this.tbProgram.WordWrap = false;
		this.tbEvent.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbEvent.Location = new System.Drawing.Point(162, 19);
		this.tbEvent.Multiline = true;
		this.tbEvent.Name = "tbEvent";
		this.tbEvent.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
		this.tbEvent.Size = new System.Drawing.Size(162, 162);
		this.tbEvent.TabIndex = 2;
		this.tbEvent.WordWrap = false;
		this.btnMethReset.Location = new System.Drawing.Point(831, 1988);
		this.btnMethReset.Name = "btnMethReset";
		this.btnMethReset.Size = new System.Drawing.Size(81, 47);
		this.btnMethReset.TabIndex = 35;
		this.btnMethReset.Text = "方法重置";
		this.btnMethReset.UseVisualStyleBackColor = true;
		this.btnMethReset.Click += new System.EventHandler(btnMethReset_Click);
		this.groupBox6.Controls.Add(this.label16);
		this.groupBox6.Location = new System.Drawing.Point(89, 1964);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(333, 81);
		this.groupBox6.TabIndex = 23;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "柱箱";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("宋体", 10.5f);
		this.label16.Location = new System.Drawing.Point(8, 24);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(0, 14);
		this.label16.TabIndex = 0;
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.checkBox5);
		this.groupBox5.Controls.Add(this.Ljyq10);
		this.groupBox5.Controls.Add(this.checkBox4);
		this.groupBox5.Controls.Add(this.Ljyq1);
		this.groupBox5.Location = new System.Drawing.Point(89, 1724);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(330, 118);
		this.groupBox5.TabIndex = 22;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "进样器";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(153, 8);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(11, 252);
		this.label17.TabIndex = 1;
		this.label17.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|";
		this.checkBox5.AutoSize = true;
		this.checkBox5.Location = new System.Drawing.Point(170, 13);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(66, 16);
		this.checkBox5.TabIndex = 0;
		this.checkBox5.Text = "进样器2";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox5.CheckedChanged += new System.EventHandler(checkBox5_CheckedChanged);
		this.Ljyq10.AutoSize = true;
		this.Ljyq10.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.Ljyq10.ForeColor = System.Drawing.Color.Black;
		this.Ljyq10.Location = new System.Drawing.Point(170, 33);
		this.Ljyq10.Name = "Ljyq10";
		this.Ljyq10.Size = new System.Drawing.Size(0, 14);
		this.Ljyq10.TabIndex = 0;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Location = new System.Drawing.Point(6, 13);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(66, 16);
		this.checkBox4.TabIndex = 0;
		this.checkBox4.Text = "进样器1";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox4.CheckedChanged += new System.EventHandler(checkBox4_CheckedChanged);
		this.Ljyq1.AutoSize = true;
		this.Ljyq1.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.Ljyq1.ForeColor = System.Drawing.Color.Black;
		this.Ljyq1.Location = new System.Drawing.Point(7, 33);
		this.Ljyq1.Name = "Ljyq1";
		this.Ljyq1.Size = new System.Drawing.Size(0, 14);
		this.Ljyq1.TabIndex = 0;
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(429, 1980);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(325, 263);
		this.tabControl1.TabIndex = 20;
		this.tabControl1.Visible = false;
		this.tabPage3.Controls.Add(this.gvExtEvTP);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(317, 237);
		this.tabPage3.TabIndex = 1;
		this.tabPage3.Text = "方法事件参数";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.gvExtEvTP.AllowUserToAddRows = false;
		this.gvExtEvTP.AllowUserToDeleteRows = false;
		this.gvExtEvTP.AllowUserToOrderColumns = true;
		this.gvExtEvTP.AllowUserToResizeColumns = false;
		this.gvExtEvTP.AllowUserToResizeRows = false;
		this.gvExtEvTP.BackgroundColor = System.Drawing.Color.AliceBlue;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvExtEvTP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.gvExtEvTP.ColumnHeadersHeight = 46;
		this.gvExtEvTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvExtEvTP.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gvExtEvTP.Enabled = false;
		this.gvExtEvTP.Location = new System.Drawing.Point(3, 3);
		this.gvExtEvTP.MultiSelect = false;
		this.gvExtEvTP.Name = "gvExtEvTP";
		this.gvExtEvTP.ReadOnly = true;
		this.gvExtEvTP.RowHeadersVisible = false;
		this.gvExtEvTP.RowTemplate.Height = 23;
		this.gvExtEvTP.Size = new System.Drawing.Size(311, 231);
		this.gvExtEvTP.TabIndex = 1;
		this.tabPage2.Controls.Add(this.panel12);
		this.tabPage2.Controls.Add(this.tabControl2);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(317, 237);
		this.tabPage2.TabIndex = 0;
		this.tabPage2.Text = "方法流量参数";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.panel12.Controls.Add(this.maskedTextBox8);
		this.panel12.Controls.Add(this.label5);
		this.panel12.Controls.Add(this.panelJY);
		this.panel12.Controls.Add(this.label6);
		this.panel12.Controls.Add(this.maskedTextBox9);
		this.panel12.Controls.Add(this.label7);
		this.panel12.Controls.Add(this.label8);
		this.panel12.Controls.Add(this.label9);
		this.panel12.Controls.Add(this.comboBox6);
		this.panel12.Controls.Add(this.panel7);
		this.panel12.Controls.Add(this.panel6);
		this.panel12.Controls.Add(this.label11);
		this.panel12.Controls.Add(this.maskedTextBox4);
		this.panel12.Controls.Add(this.radioButton28);
		this.panel12.Controls.Add(this.label18);
		this.panel12.Controls.Add(this.label19);
		this.panel12.Location = new System.Drawing.Point(4, 28);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(331, 280);
		this.panel12.TabIndex = 7;
		this.maskedTextBox8.BackColor = System.Drawing.Color.White;
		this.maskedTextBox8.Enabled = false;
		this.maskedTextBox8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox8.ForeColor = System.Drawing.Color.RoyalBlue;
		this.maskedTextBox8.Location = new System.Drawing.Point(45, 47);
		this.maskedTextBox8.Name = "maskedTextBox8";
		this.maskedTextBox8.Size = new System.Drawing.Size(49, 21);
		this.maskedTextBox8.TabIndex = 2;
		this.maskedTextBox8.Text = "0.00";
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.Color.Black;
		this.label5.Location = new System.Drawing.Point(227, 54);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(35, 12);
		this.label5.TabIndex = 0;
		this.label5.Text = "气体:";
		this.panelJY.Controls.Add(this.panel15);
		this.panelJY.Controls.Add(this.dgInsamp1);
		this.panelJY.Location = new System.Drawing.Point(1, 77);
		this.panelJY.Name = "panelJY";
		this.panelJY.Size = new System.Drawing.Size(306, 126);
		this.panelJY.TabIndex = 0;
		this.panel15.Controls.Add(this.label13);
		this.panel15.Controls.Add(this.maskedTextBox5);
		this.panel15.Location = new System.Drawing.Point(5, 6);
		this.panel15.Name = "panel15";
		this.panel15.Size = new System.Drawing.Size(66, 43);
		this.panel15.TabIndex = 5;
		this.label13.AutoSize = true;
		this.label13.ForeColor = System.Drawing.Color.Black;
		this.label13.Location = new System.Drawing.Point(3, 5);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(59, 12);
		this.label13.TabIndex = 0;
		this.label13.Text = "初始时间:";
		this.maskedTextBox5.BackColor = System.Drawing.Color.White;
		this.maskedTextBox5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox5.ForeColor = System.Drawing.Color.RoyalBlue;
		this.maskedTextBox5.Location = new System.Drawing.Point(3, 20);
		this.maskedTextBox5.Name = "maskedTextBox5";
		this.maskedTextBox5.Size = new System.Drawing.Size(44, 21);
		this.maskedTextBox5.TabIndex = 2;
		this.maskedTextBox5.Text = "000.0";
		this.dgInsamp1.AllowUserToAddRows = false;
		this.dgInsamp1.AllowUserToDeleteRows = false;
		this.dgInsamp1.AllowUserToOrderColumns = true;
		this.dgInsamp1.AllowUserToResizeColumns = false;
		this.dgInsamp1.AllowUserToResizeRows = false;
		this.dgInsamp1.BackgroundColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgInsamp1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgInsamp1.ColumnHeadersHeight = 45;
		this.dgInsamp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgInsamp1.Columns.AddRange(this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn13, this.dataGridViewTextBoxColumn14, this.dataGridViewTextBoxColumn15);
		this.dgInsamp1.Location = new System.Drawing.Point(2, 3);
		this.dgInsamp1.MultiSelect = false;
		this.dgInsamp1.Name = "dgInsamp1";
		this.dgInsamp1.RowHeadersVisible = false;
		this.dgInsamp1.RowTemplate.Height = 23;
		this.dgInsamp1.Size = new System.Drawing.Size(301, 141);
		this.dgInsamp1.TabIndex = 0;
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.Color.Black;
		this.label6.Location = new System.Drawing.Point(115, 52);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(17, 12);
		this.label6.TabIndex = 0;
		this.label6.Text = "×";
		this.maskedTextBox9.BackColor = System.Drawing.Color.White;
		this.maskedTextBox9.Enabled = false;
		this.maskedTextBox9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox9.ForeColor = System.Drawing.Color.RoyalBlue;
		this.maskedTextBox9.Location = new System.Drawing.Point(142, 48);
		this.maskedTextBox9.Name = "maskedTextBox9";
		this.maskedTextBox9.Size = new System.Drawing.Size(59, 21);
		this.maskedTextBox9.TabIndex = 2;
		this.maskedTextBox9.Text = "000.000";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(100, 51);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(17, 12);
		this.label7.TabIndex = 3;
		this.label7.Text = "mm";
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Black;
		this.label8.Location = new System.Drawing.Point(-1, 51);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(47, 12);
		this.label8.TabIndex = 0;
		this.label8.Text = "色谱柱:";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(207, 52);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(11, 12);
		this.label9.TabIndex = 3;
		this.label9.Text = "m";
		this.comboBox6.FormattingEnabled = true;
		this.comboBox6.Items.AddRange(new object[4] { "氮气", "氢气", "空气", "氦气" });
		this.comboBox6.Location = new System.Drawing.Point(263, 51);
		this.comboBox6.Name = "comboBox6";
		this.comboBox6.Size = new System.Drawing.Size(47, 20);
		this.comboBox6.TabIndex = 2;
		this.comboBox6.Text = "氮气";
		this.panel7.Controls.Add(this.radioButton25);
		this.panel7.Controls.Add(this.radioButton5);
		this.panel7.Controls.Add(this.radioButton6);
		this.panel7.Location = new System.Drawing.Point(48, 25);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(153, 18);
		this.panel7.TabIndex = 1;
		this.radioButton25.AutoSize = true;
		this.radioButton25.Location = new System.Drawing.Point(97, 1);
		this.radioButton25.Name = "radioButton25";
		this.radioButton25.Size = new System.Drawing.Size(47, 16);
		this.radioButton25.TabIndex = 0;
		this.radioButton25.Text = "分流";
		this.radioButton25.UseVisualStyleBackColor = true;
		this.radioButton25.Visible = false;
		this.radioButton5.AutoSize = true;
		this.radioButton5.Location = new System.Drawing.Point(50, 1);
		this.radioButton5.Name = "radioButton5";
		this.radioButton5.Size = new System.Drawing.Size(47, 16);
		this.radioButton5.TabIndex = 0;
		this.radioButton5.Text = "流量";
		this.radioButton5.UseVisualStyleBackColor = true;
		this.radioButton6.AutoSize = true;
		this.radioButton6.Checked = true;
		this.radioButton6.Location = new System.Drawing.Point(3, 1);
		this.radioButton6.Name = "radioButton6";
		this.radioButton6.Size = new System.Drawing.Size(47, 16);
		this.radioButton6.TabIndex = 0;
		this.radioButton6.TabStop = true;
		this.radioButton6.Text = "压力";
		this.radioButton6.UseVisualStyleBackColor = true;
		this.panel6.Controls.Add(this.radioButton3);
		this.panel6.Controls.Add(this.radioButton2);
		this.panel6.Controls.Add(this.radioButton9);
		this.panel6.Location = new System.Drawing.Point(48, 3);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(254, 18);
		this.panel6.TabIndex = 1;
		this.radioButton3.AutoSize = true;
		this.radioButton3.Location = new System.Drawing.Point(161, 0);
		this.radioButton3.Name = "radioButton3";
		this.radioButton3.Size = new System.Drawing.Size(47, 16);
		this.radioButton3.TabIndex = 0;
		this.radioButton3.Text = "吹扫";
		this.radioButton3.UseVisualStyleBackColor = true;
		this.radioButton3.CheckedChanged += new System.EventHandler(radioButton3_CheckedChanged);
		this.radioButton2.AutoSize = true;
		this.radioButton2.Location = new System.Drawing.Point(82, 0);
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.Size = new System.Drawing.Size(47, 16);
		this.radioButton2.TabIndex = 0;
		this.radioButton2.Text = "分流";
		this.radioButton2.UseVisualStyleBackColor = true;
		this.radioButton2.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
		this.radioButton9.AutoSize = true;
		this.radioButton9.Checked = true;
		this.radioButton9.Location = new System.Drawing.Point(3, 0);
		this.radioButton9.Name = "radioButton9";
		this.radioButton9.Size = new System.Drawing.Size(47, 16);
		this.radioButton9.TabIndex = 0;
		this.radioButton9.TabStop = true;
		this.radioButton9.Text = "载气";
		this.radioButton9.UseVisualStyleBackColor = true;
		this.radioButton9.CheckedChanged += new System.EventHandler(radioButton9_CheckedChanged);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(287, 30);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(23, 12);
		this.label11.TabIndex = 3;
		this.label11.Text = "psi";
		this.maskedTextBox4.BackColor = System.Drawing.Color.White;
		this.maskedTextBox4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox4.ForeColor = System.Drawing.Color.RoyalBlue;
		this.maskedTextBox4.Location = new System.Drawing.Point(237, 27);
		this.maskedTextBox4.Name = "maskedTextBox4";
		this.maskedTextBox4.Size = new System.Drawing.Size(49, 21);
		this.maskedTextBox4.TabIndex = 2;
		this.maskedTextBox4.Text = "000.00";
		this.radioButton28.AutoSize = true;
		this.radioButton28.Enabled = false;
		this.radioButton28.Location = new System.Drawing.Point(7, 3);
		this.radioButton28.Name = "radioButton28";
		this.radioButton28.Size = new System.Drawing.Size(35, 16);
		this.radioButton28.TabIndex = 4;
		this.radioButton28.Text = "关";
		this.radioButton28.UseVisualStyleBackColor = true;
		this.label18.AutoSize = true;
		this.label18.ForeColor = System.Drawing.Color.Black;
		this.label18.Location = new System.Drawing.Point(207, 30);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(35, 12);
		this.label18.TabIndex = 0;
		this.label18.Text = "设置:";
		this.label19.AutoSize = true;
		this.label19.ForeColor = System.Drawing.Color.Black;
		this.label19.Location = new System.Drawing.Point(10, 28);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(35, 12);
		this.label19.TabIndex = 0;
		this.label19.Text = "模式:";
		this.tabControl2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl2.Controls.Add(this.tabPage6);
		this.tabControl2.Controls.Add(this.tabPage7);
		this.tabControl2.Controls.Add(this.tabPage8);
		this.tabControl2.Controls.Add(this.tabPage9);
		this.tabControl2.Controls.Add(this.tabPage10);
		this.tabControl2.Controls.Add(this.tabPage15);
		this.tabControl2.Location = new System.Drawing.Point(3, 3);
		this.tabControl2.Name = "tabControl2";
		this.tabControl2.SelectedIndex = 0;
		this.tabControl2.Size = new System.Drawing.Size(330, 21);
		this.tabControl2.TabIndex = 6;
		this.tabControl2.SelectedIndexChanged += new System.EventHandler(tabControl2_SelectedIndexChanged);
		this.tabPage6.Location = new System.Drawing.Point(4, 22);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(322, 0);
		this.tabPage6.TabIndex = 0;
		this.tabPage6.Text = "进样1";
		this.tabPage6.UseVisualStyleBackColor = true;
		this.tabPage7.Location = new System.Drawing.Point(4, 22);
		this.tabPage7.Name = "tabPage7";
		this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage7.Size = new System.Drawing.Size(322, 0);
		this.tabPage7.TabIndex = 1;
		this.tabPage7.Text = "进样2";
		this.tabPage7.UseVisualStyleBackColor = true;
		this.tabPage8.Location = new System.Drawing.Point(4, 22);
		this.tabPage8.Name = "tabPage8";
		this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage8.Size = new System.Drawing.Size(322, 0);
		this.tabPage8.TabIndex = 2;
		this.tabPage8.Text = "进样3";
		this.tabPage8.UseVisualStyleBackColor = true;
		this.tabPage9.Location = new System.Drawing.Point(4, 22);
		this.tabPage9.Name = "tabPage9";
		this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage9.Size = new System.Drawing.Size(322, 0);
		this.tabPage9.TabIndex = 3;
		this.tabPage9.Text = "检测器1";
		this.tabPage9.UseVisualStyleBackColor = true;
		this.tabPage10.Location = new System.Drawing.Point(4, 22);
		this.tabPage10.Name = "tabPage10";
		this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage10.Size = new System.Drawing.Size(322, 0);
		this.tabPage10.TabIndex = 4;
		this.tabPage10.Text = "检测器2";
		this.tabPage10.UseVisualStyleBackColor = true;
		this.tabPage15.Location = new System.Drawing.Point(4, 22);
		this.tabPage15.Name = "tabPage15";
		this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage15.Size = new System.Drawing.Size(322, 0);
		this.tabPage15.TabIndex = 5;
		this.tabPage15.Text = "检测器3";
		this.tabPage15.UseVisualStyleBackColor = true;
		this.groupBox4.Controls.Add(this.lbptInitT);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Controls.Add(this.tbptIniTempHoldT);
		this.groupBox4.Controls.Add(this.label27);
		this.groupBox4.Controls.Add(this.dgvCT6);
		this.groupBox4.Location = new System.Drawing.Point(429, 1737);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(294, 237);
		this.groupBox4.TabIndex = 19;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "温度参数";
		this.groupBox4.Visible = false;
		this.lbptInitT.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lbptInitT.AutoSize = true;
		this.lbptInitT.Location = new System.Drawing.Point(84, 193);
		this.lbptInitT.Name = "lbptInitT";
		this.lbptInitT.Size = new System.Drawing.Size(23, 12);
		this.lbptInitT.TabIndex = 17;
		this.lbptInitT.Text = "100";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(2, 193);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 16;
		this.label3.Text = "初温[℃]";
		this.tbptIniTempHoldT.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.tbptIniTempHoldT.Enabled = false;
		this.tbptIniTempHoldT.Location = new System.Drawing.Point(86, 213);
		this.tbptIniTempHoldT.Name = "tbptIniTempHoldT";
		this.tbptIniTempHoldT.Size = new System.Drawing.Size(38, 21);
		this.tbptIniTempHoldT.TabIndex = 18;
		this.tbptIniTempHoldT.Text = "?";
		this.label27.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(1, 218);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(83, 12);
		this.label27.TabIndex = 15;
		this.label27.Text = "初温保持[min]";
		this.dgvCT6.AllowUserToAddRows = false;
		this.dgvCT6.AllowUserToDeleteRows = false;
		this.dgvCT6.BackgroundColor = System.Drawing.Color.White;
		this.dgvCT6.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCT6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvCT6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCT6.Columns.AddRange(this.clmCT6CN, this.clmCT6SetT);
		this.dgvCT6.Enabled = false;
		this.dgvCT6.EnableHeadersVisualStyles = false;
		this.dgvCT6.Location = new System.Drawing.Point(3, 20);
		this.dgvCT6.MultiSelect = false;
		this.dgvCT6.Name = "dgvCT6";
		this.dgvCT6.ReadOnly = true;
		this.dgvCT6.RowHeadersVisible = false;
		this.dgvCT6.RowHeadersWidth = 80;
		this.dgvCT6.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvCT6.RowTemplate.Height = 18;
		this.dgvCT6.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvCT6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCT6.ShowEditingIcon = false;
		this.dgvCT6.Size = new System.Drawing.Size(121, 210);
		this.dgvCT6.TabIndex = 8;
		this.tabPage4.AutoScroll = true;
		this.tabPage4.Controls.Add(this.pnlcu);
		this.tabPage4.Location = new System.Drawing.Point(4, 22);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage4.Size = new System.Drawing.Size(775, 522);
		this.tabPage4.TabIndex = 0;
		this.tabPage4.Text = "定量结果计算";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.tabPage5.Controls.Add(this.splitContainer1);
		this.tabPage5.Location = new System.Drawing.Point(4, 22);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Size = new System.Drawing.Size(775, 522);
		this.tabPage5.TabIndex = 5;
		this.tabPage5.Text = "组份信息";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.tabPage11.Controls.Add(this.label23);
		this.tabPage11.Controls.Add(this.label22);
		this.tabPage11.Controls.Add(this.ucSplitLabel4);
		this.tabPage11.Controls.Add(this.dGCalibration);
		this.tabPage11.Controls.Add(this.ucBtnCaliApply);
		this.tabPage11.Location = new System.Drawing.Point(4, 22);
		this.tabPage11.Name = "tabPage11";
		this.tabPage11.Size = new System.Drawing.Size(775, 522);
		this.tabPage11.TabIndex = 6;
		this.tabPage11.Text = "校正数据";
		this.tabPage11.UseVisualStyleBackColor = true;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label23.Location = new System.Drawing.Point(568, 53);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(51, 20);
		this.label23.TabIndex = 37;
		this.label23.Text = "甲烷";
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label22.Location = new System.Drawing.Point(339, 53);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(51, 20);
		this.label22.TabIndex = 36;
		this.label22.Text = "总烃";
		this.ucSplitLabel4.Font = new System.Drawing.Font("微软雅黑", 8f);
		this.ucSplitLabel4.LineColor = System.Drawing.Color.Blue;
		this.ucSplitLabel4.Location = new System.Drawing.Point(129, 82);
		this.ucSplitLabel4.MaximumSize = new System.Drawing.Size(0, 15);
		this.ucSplitLabel4.MinimumSize = new System.Drawing.Size(150, 15);
		this.ucSplitLabel4.Name = "ucSplitLabel4";
		this.ucSplitLabel4.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
		this.ucSplitLabel4.Size = new System.Drawing.Size(597, 15);
		this.ucSplitLabel4.TabIndex = 35;
		this.ucSplitLabel4.Text = "分割线";
		this.dGCalibration.BackColor = System.Drawing.Color.White;
		this.dGCalibration.Columns = null;
		this.dGCalibration.DataSource = null;
		this.dGCalibration.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.dGCalibration.HeadHeight = 40;
		this.dGCalibration.HeadPadingLeft = 0;
		this.dGCalibration.HeadTextColor = System.Drawing.Color.Black;
		this.dGCalibration.IsShowCheckBox = false;
		this.dGCalibration.IsShowHead = true;
		this.dGCalibration.Location = new System.Drawing.Point(4, 100);
		this.dGCalibration.Name = "dGCalibration";
		this.dGCalibration.Padding = new System.Windows.Forms.Padding(0, 40, 0, 0);
		this.dGCalibration.RowHeight = 41;
		this.dGCalibration.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.dGCalibration.Size = new System.Drawing.Size(739, 292);
		this.dGCalibration.TabIndex = 34;
		this.dGCalibration.ItemClick += new HZH_Controls.Controls.DataGridViewEventHandler(dGCalibration_ItemClick);
		this.ucBtnCaliApply.BackColor = System.Drawing.Color.White;
		this.ucBtnCaliApply.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnCaliApply.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnCaliApply.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnCaliApply.BtnText = "应用";
		this.ucBtnCaliApply.ConerRadius = 5;
		this.ucBtnCaliApply.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnCaliApply.EnabledMouseEffect = true;
		this.ucBtnCaliApply.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnCaliApply.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnCaliApply.IsRadius = true;
		this.ucBtnCaliApply.IsShowRect = true;
		this.ucBtnCaliApply.IsShowTips = false;
		this.ucBtnCaliApply.Location = new System.Drawing.Point(144, 401);
		this.ucBtnCaliApply.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnCaliApply.Name = "ucBtnCaliApply";
		this.ucBtnCaliApply.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnCaliApply.RectWidth = 1;
		this.ucBtnCaliApply.Size = new System.Drawing.Size(184, 60);
		this.ucBtnCaliApply.TabIndex = 33;
		this.ucBtnCaliApply.TabStop = false;
		this.ucBtnCaliApply.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnCaliApply.TipsText = "";
		this.ucBtnCaliApply.BtnClick += new System.EventHandler(ucBtnCaliApply_BtnClick);
		this.tabPage12.Controls.Add(this.chartPlots);
		this.tabPage12.Location = new System.Drawing.Point(4, 22);
		this.tabPage12.Name = "tabPage12";
		this.tabPage12.Size = new System.Drawing.Size(775, 522);
		this.tabPage12.TabIndex = 7;
		this.tabPage12.Text = "校正曲线";
		this.tabPage12.UseVisualStyleBackColor = true;
		chartArea.Name = "ChartArea1";
		this.chartPlots.ChartAreas.Add(chartArea);
		this.chartPlots.Dock = System.Windows.Forms.DockStyle.Bottom;
		legend.Name = "Legend1";
		this.chartPlots.Legends.Add(legend);
		this.chartPlots.Location = new System.Drawing.Point(0, 159);
		this.chartPlots.Name = "chartPlots";
		series.ChartArea = "ChartArea1";
		series.Legend = "Legend1";
		series.Name = "Series1";
		this.chartPlots.Series.Add(series);
		this.chartPlots.Size = new System.Drawing.Size(775, 363);
		this.chartPlots.TabIndex = 2;
		this.chartPlots.Text = "chart1";
		this.tabPage17.AutoScroll = true;
		this.tabPage17.Controls.Add(this.ucBtnApplly);
		this.tabPage17.Controls.Add(this.ucKeyBorderNum1);
		this.tabPage17.Controls.Add(this.gvInteg);
		this.tabPage17.Controls.Add(this.spcIntegComponent);
		this.tabPage17.Location = new System.Drawing.Point(4, 22);
		this.tabPage17.Name = "tabPage17";
		this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage17.Size = new System.Drawing.Size(775, 522);
		this.tabPage17.TabIndex = 1;
		this.tabPage17.Text = "积分事件";
		this.tabPage17.UseVisualStyleBackColor = true;
		this.ucBtnApplly.BackColor = System.Drawing.Color.White;
		this.ucBtnApplly.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnApplly.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnApplly.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnApplly.BtnText = "应用";
		this.ucBtnApplly.ConerRadius = 5;
		this.ucBtnApplly.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnApplly.EnabledMouseEffect = true;
		this.ucBtnApplly.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnApplly.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnApplly.IsRadius = true;
		this.ucBtnApplly.IsShowRect = true;
		this.ucBtnApplly.IsShowTips = false;
		this.ucBtnApplly.Location = new System.Drawing.Point(441, 390);
		this.ucBtnApplly.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnApplly.Name = "ucBtnApplly";
		this.ucBtnApplly.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnApplly.RectWidth = 1;
		this.ucBtnApplly.Size = new System.Drawing.Size(184, 60);
		this.ucBtnApplly.TabIndex = 19;
		this.ucBtnApplly.TabStop = false;
		this.ucBtnApplly.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnApplly.TipsText = "";
		this.ucBtnApplly.BtnClick += new System.EventHandler(ucBtnApplly_BtnClick);
		this.ucKeyBorderNum1.BackColor = System.Drawing.Color.White;
		this.ucKeyBorderNum1.Location = new System.Drawing.Point(374, 6);
		this.ucKeyBorderNum1.Name = "ucKeyBorderNum1";
		this.ucKeyBorderNum1.Size = new System.Drawing.Size(379, 218);
		this.ucKeyBorderNum1.TabIndex = 18;
		this.ucKeyBorderNum1.UseCustomEvent = false;
		this.cmsIntegration.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsIntegration.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miIntegAppendRow, this.miIntegDeleteRows, this.miIntegInsertRow, this.toolStripSeparator6, this.miIntegResetRows, this.miIntegRowsDown, this.toolStripSeparator7, this.miIntegRowsUp });
		this.cmsIntegration.Name = "cmsIntegration";
		this.cmsIntegration.Size = new System.Drawing.Size(113, 148);
		this.miIntegAppendRow.Name = "miIntegAppendRow";
		this.miIntegAppendRow.Size = new System.Drawing.Size(112, 22);
		this.miIntegAppendRow.Text = "添加行";
		this.miIntegAppendRow.Click += new System.EventHandler(miIntegAppendRow_Click);
		this.miIntegDeleteRows.Name = "miIntegDeleteRows";
		this.miIntegDeleteRows.Size = new System.Drawing.Size(112, 22);
		this.miIntegDeleteRows.Text = "插入行";
		this.miIntegDeleteRows.Click += new System.EventHandler(miIntegDeleteRows_Click);
		this.miIntegInsertRow.Name = "miIntegInsertRow";
		this.miIntegInsertRow.Size = new System.Drawing.Size(112, 22);
		this.miIntegInsertRow.Text = "删除行";
		this.miIntegInsertRow.Click += new System.EventHandler(miIntegInsertRow_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(109, 6);
		this.miIntegResetRows.Name = "miIntegResetRows";
		this.miIntegResetRows.Size = new System.Drawing.Size(112, 22);
		this.miIntegResetRows.Text = "上移";
		this.miIntegResetRows.Click += new System.EventHandler(miIntegResetRows_Click);
		this.miIntegRowsDown.Name = "miIntegRowsDown";
		this.miIntegRowsDown.Size = new System.Drawing.Size(112, 22);
		this.miIntegRowsDown.Text = "下移";
		this.miIntegRowsDown.Click += new System.EventHandler(miIntegRowsDown_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(109, 6);
		this.miIntegRowsUp.Name = "miIntegRowsUp";
		this.miIntegRowsUp.Size = new System.Drawing.Size(112, 22);
		this.miIntegRowsUp.Text = "重置";
		this.miIntegRowsUp.Click += new System.EventHandler(miIntegRowsUp_Click);
		this.spcIntegComponent.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.spcIntegComponent.Location = new System.Drawing.Point(685, 341);
		this.spcIntegComponent.Name = "spcIntegComponent";
		this.spcIntegComponent.Panel1.Controls.Add(this.groupBox18);
		this.spcIntegComponent.Panel2.Controls.Add(this.GbCmpds);
		this.spcIntegComponent.Size = new System.Drawing.Size(197, 268);
		this.spcIntegComponent.SplitterDistance = 168;
		this.spcIntegComponent.TabIndex = 17;
		this.groupBox18.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox18.Location = new System.Drawing.Point(0, 0);
		this.groupBox18.Name = "groupBox18";
		this.groupBox18.Size = new System.Drawing.Size(168, 268);
		this.groupBox18.TabIndex = 16;
		this.groupBox18.TabStop = false;
		this.groupBox18.Text = "积分事件";
		this.GbCmpds.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GbCmpds.Location = new System.Drawing.Point(0, 0);
		this.GbCmpds.Name = "GbCmpds";
		this.GbCmpds.Size = new System.Drawing.Size(25, 268);
		this.GbCmpds.TabIndex = 14;
		this.GbCmpds.TabStop = false;
		this.GbCmpds.Text = "组份信息";
		this.tabPage19.AutoScroll = true;
		this.tabPage19.Controls.Add(this.tbCollectP);
		this.tabPage19.Controls.Add(this.tbCollectJYDW);
		this.tabPage19.Controls.Add(this.tbCollectSite);
		this.tabPage19.Controls.Add(this.tbCollectSJDW);
		this.tabPage19.Controls.Add(this.ucBtnSite);
		this.tabPage19.Controls.Add(this.label34);
		this.tabPage19.Controls.Add(this.label28);
		this.tabPage19.Controls.Add(this.label24);
		this.tabPage19.Controls.Add(this.label26);
		this.tabPage19.Controls.Add(this.panel1);
		this.tabPage19.Location = new System.Drawing.Point(4, 22);
		this.tabPage19.Name = "tabPage19";
		this.tabPage19.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage19.Size = new System.Drawing.Size(775, 522);
		this.tabPage19.TabIndex = 3;
		this.tabPage19.Text = "采样点信息";
		this.tabPage19.UseVisualStyleBackColor = true;
		this.tbCollectP.BackColor = System.Drawing.Color.Transparent;
		this.tbCollectP.ConerRadius = 5;
		this.tbCollectP.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbCollectP.DecLength = 2;
		this.tbCollectP.FillColor = System.Drawing.Color.Empty;
		this.tbCollectP.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbCollectP.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectP.InputText = "";
		this.tbCollectP.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbCollectP.IsFocusColor = true;
		this.tbCollectP.IsRadius = true;
		this.tbCollectP.IsShowClearBtn = true;
		this.tbCollectP.IsShowKeyboard = false;
		this.tbCollectP.IsShowRect = true;
		this.tbCollectP.IsShowSearchBtn = false;
		this.tbCollectP.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderHand;
		this.tbCollectP.Location = new System.Drawing.Point(134, 182);
		this.tbCollectP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbCollectP.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbCollectP.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbCollectP.Name = "tbCollectP";
		this.tbCollectP.Padding = new System.Windows.Forms.Padding(5);
		this.tbCollectP.PasswordChar = '\0';
		this.tbCollectP.PromptColor = System.Drawing.Color.Gray;
		this.tbCollectP.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectP.PromptText = "";
		this.tbCollectP.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbCollectP.RectWidth = 1;
		this.tbCollectP.RegexPattern = "";
		this.tbCollectP.Size = new System.Drawing.Size(406, 42);
		this.tbCollectP.TabIndex = 116;
		this.tbCollectJYDW.BackColor = System.Drawing.Color.Transparent;
		this.tbCollectJYDW.ConerRadius = 5;
		this.tbCollectJYDW.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbCollectJYDW.DecLength = 2;
		this.tbCollectJYDW.FillColor = System.Drawing.Color.Empty;
		this.tbCollectJYDW.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbCollectJYDW.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectJYDW.InputText = "";
		this.tbCollectJYDW.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbCollectJYDW.IsFocusColor = true;
		this.tbCollectJYDW.IsRadius = true;
		this.tbCollectJYDW.IsShowClearBtn = true;
		this.tbCollectJYDW.IsShowKeyboard = false;
		this.tbCollectJYDW.IsShowRect = true;
		this.tbCollectJYDW.IsShowSearchBtn = false;
		this.tbCollectJYDW.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderHand;
		this.tbCollectJYDW.Location = new System.Drawing.Point(132, 127);
		this.tbCollectJYDW.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbCollectJYDW.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbCollectJYDW.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbCollectJYDW.Name = "tbCollectJYDW";
		this.tbCollectJYDW.Padding = new System.Windows.Forms.Padding(5);
		this.tbCollectJYDW.PasswordChar = '\0';
		this.tbCollectJYDW.PromptColor = System.Drawing.Color.Gray;
		this.tbCollectJYDW.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectJYDW.PromptText = "";
		this.tbCollectJYDW.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbCollectJYDW.RectWidth = 1;
		this.tbCollectJYDW.RegexPattern = "";
		this.tbCollectJYDW.Size = new System.Drawing.Size(406, 42);
		this.tbCollectJYDW.TabIndex = 115;
		this.tbCollectSite.BackColor = System.Drawing.Color.Transparent;
		this.tbCollectSite.ConerRadius = 5;
		this.tbCollectSite.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbCollectSite.DecLength = 2;
		this.tbCollectSite.FillColor = System.Drawing.Color.Empty;
		this.tbCollectSite.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbCollectSite.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectSite.InputText = "";
		this.tbCollectSite.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbCollectSite.IsFocusColor = true;
		this.tbCollectSite.IsRadius = true;
		this.tbCollectSite.IsShowClearBtn = true;
		this.tbCollectSite.IsShowKeyboard = false;
		this.tbCollectSite.IsShowRect = true;
		this.tbCollectSite.IsShowSearchBtn = false;
		this.tbCollectSite.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderHand;
		this.tbCollectSite.Location = new System.Drawing.Point(134, 72);
		this.tbCollectSite.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbCollectSite.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbCollectSite.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbCollectSite.Name = "tbCollectSite";
		this.tbCollectSite.Padding = new System.Windows.Forms.Padding(5);
		this.tbCollectSite.PasswordChar = '\0';
		this.tbCollectSite.PromptColor = System.Drawing.Color.Gray;
		this.tbCollectSite.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectSite.PromptText = "";
		this.tbCollectSite.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbCollectSite.RectWidth = 1;
		this.tbCollectSite.RegexPattern = "";
		this.tbCollectSite.Size = new System.Drawing.Size(406, 42);
		this.tbCollectSite.TabIndex = 114;
		this.tbCollectSJDW.BackColor = System.Drawing.Color.Transparent;
		this.tbCollectSJDW.ConerRadius = 5;
		this.tbCollectSJDW.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbCollectSJDW.DecLength = 2;
		this.tbCollectSJDW.FillColor = System.Drawing.Color.Empty;
		this.tbCollectSJDW.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbCollectSJDW.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectSJDW.InputText = "";
		this.tbCollectSJDW.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbCollectSJDW.IsFocusColor = true;
		this.tbCollectSJDW.IsRadius = true;
		this.tbCollectSJDW.IsShowClearBtn = true;
		this.tbCollectSJDW.IsShowKeyboard = false;
		this.tbCollectSJDW.IsShowRect = true;
		this.tbCollectSJDW.IsShowSearchBtn = false;
		this.tbCollectSJDW.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderHand;
		this.tbCollectSJDW.Location = new System.Drawing.Point(132, 17);
		this.tbCollectSJDW.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbCollectSJDW.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbCollectSJDW.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbCollectSJDW.Name = "tbCollectSJDW";
		this.tbCollectSJDW.Padding = new System.Windows.Forms.Padding(5);
		this.tbCollectSJDW.PasswordChar = '\0';
		this.tbCollectSJDW.PromptColor = System.Drawing.Color.Gray;
		this.tbCollectSJDW.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbCollectSJDW.PromptText = "";
		this.tbCollectSJDW.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbCollectSJDW.RectWidth = 1;
		this.tbCollectSJDW.RegexPattern = "";
		this.tbCollectSJDW.Size = new System.Drawing.Size(406, 42);
		this.tbCollectSJDW.TabIndex = 113;
		this.ucBtnSite.BackColor = System.Drawing.Color.White;
		this.ucBtnSite.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnSite.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnSite.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnSite.BtnText = "保存";
		this.ucBtnSite.ConerRadius = 30;
		this.ucBtnSite.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnSite.EnabledMouseEffect = true;
		this.ucBtnSite.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnSite.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnSite.IsRadius = true;
		this.ucBtnSite.IsShowRect = true;
		this.ucBtnSite.IsShowTips = false;
		this.ucBtnSite.Location = new System.Drawing.Point(516, 291);
		this.ucBtnSite.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnSite.Name = "ucBtnSite";
		this.ucBtnSite.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnSite.RectWidth = 1;
		this.ucBtnSite.Size = new System.Drawing.Size(184, 60);
		this.ucBtnSite.TabIndex = 112;
		this.ucBtnSite.TabStop = false;
		this.ucBtnSite.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnSite.TipsText = "";
		this.ucBtnSite.BtnClick += new System.EventHandler(ucBtnSite_BtnClick);
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("宋体", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label34.Location = new System.Drawing.Point(10, 132);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(115, 21);
		this.label34.TabIndex = 109;
		this.label34.Text = "检测单位：";
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("宋体", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label28.Location = new System.Drawing.Point(10, 77);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(115, 21);
		this.label28.TabIndex = 107;
		this.label28.Text = "采样地点：";
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("宋体", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label24.Location = new System.Drawing.Point(11, 187);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(94, 21);
		this.label24.TabIndex = 106;
		this.label24.Text = "检测员：";
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("宋体", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label26.Location = new System.Drawing.Point(11, 22);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(115, 21);
		this.label26.TabIndex = 105;
		this.label26.Text = "被测单位：";
		this.panel1.Controls.Add(this.groupBox11);
		this.panel1.Controls.Add(this.groupBox17);
		this.panel1.Controls.Add(this.groupBox15);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Location = new System.Drawing.Point(757, 6);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(10, 510);
		this.panel1.TabIndex = 5;
		this.panel1.Visible = false;
		this.groupBox11.Controls.Add(this.label69);
		this.groupBox11.Controls.Add(this.numericUpDown3);
		this.groupBox11.Controls.Add(this.numericUpDown1);
		this.groupBox11.Controls.Add(this.numericUpDown2);
		this.groupBox11.Controls.Add(this.cbRptJYTime);
		this.groupBox11.Controls.Add(this.cbRptWithResult);
		this.groupBox11.Controls.Add(this.cbRptWithSourceData);
		this.groupBox11.Controls.Add(this.cbRptChromBold);
		this.groupBox11.Controls.Add(this.cbRptBoundary);
		this.groupBox11.Controls.Add(this.cbRptWithChrom);
		this.groupBox11.Controls.Add(this.cbRptFileName);
		this.groupBox11.Controls.Add(this.cbRptWithTailFactor);
		this.groupBox11.Controls.Add(this.cbRptWithCapFactor);
		this.groupBox11.Controls.Add(this.cbRptWithValidNumber);
		this.groupBox11.Controls.Add(this.cbRptWithZhuXiao);
		this.groupBox11.Controls.Add(this.cbRptWithPeakResulution);
		this.groupBox11.Controls.Add(this.cbRptWithCoef);
		this.groupBox11.Controls.Add(this.cbRptWithCurve);
		this.groupBox11.Controls.Add(this.cbRptWithPeakSingle);
		this.groupBox11.Controls.Add(this.cbRptWithPeakHalf);
		this.groupBox11.Controls.Add(this.cbRptWithPeakHeightPercent);
		this.groupBox11.Controls.Add(this.cbRptWithPeakAreaPercent);
		this.groupBox11.Controls.Add(this.cbRptWithPeakHeight);
		this.groupBox11.Controls.Add(this.cbRptWithDensityPercent);
		this.groupBox11.Controls.Add(this.cbRptWithPeakArea);
		this.groupBox11.Controls.Add(this.cbRptWithDensity);
		this.groupBox11.Controls.Add(this.cbRptCorrFactor);
		this.groupBox11.Controls.Add(this.cbRptZuFenName);
		this.groupBox11.Controls.Add(this.cbRptKeepTime);
		this.groupBox11.Controls.Add(this.cbRptWithIdx);
		this.groupBox11.Controls.Add(this.cbRptPrintTime);
		this.groupBox11.Controls.Add(this.label12);
		this.groupBox11.Controls.Add(this.label30);
		this.groupBox11.Controls.Add(this.label56);
		this.groupBox11.Controls.Add(this.ReportTitle);
		this.groupBox11.Controls.Add(this.label68);
		this.groupBox11.Location = new System.Drawing.Point(27, 3);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(325, 250);
		this.groupBox11.TabIndex = 3;
		this.groupBox11.TabStop = false;
		this.groupBox11.Text = "打印内容";
		this.label69.AutoSize = true;
		this.label69.Location = new System.Drawing.Point(5, 125);
		this.label69.Name = "label69";
		this.label69.Size = new System.Drawing.Size(329, 12);
		this.label69.TabIndex = 5;
		this.label69.Text = "表格内容———————————————————————";
		this.numericUpDown3.Location = new System.Drawing.Point(153, 101);
		this.numericUpDown3.Maximum = new decimal(new int[4] { 3000, 0, 0, 0 });
		this.numericUpDown3.Name = "numericUpDown3";
		this.numericUpDown3.Size = new System.Drawing.Size(45, 21);
		this.numericUpDown3.TabIndex = 4;
		this.numericUpDown3.Value = new decimal(new int[4] { 528, 0, 0, 0 });
		this.numericUpDown3.Visible = false;
		this.numericUpDown1.Location = new System.Drawing.Point(77, 100);
		this.numericUpDown1.Maximum = new decimal(new int[4] { 3000, 0, 0, 0 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Size = new System.Drawing.Size(45, 21);
		this.numericUpDown1.TabIndex = 4;
		this.numericUpDown1.Value = new decimal(new int[4] { 1326, 0, 0, 0 });
		this.numericUpDown1.Visible = false;
		this.numericUpDown2.Location = new System.Drawing.Point(285, 78);
		this.numericUpDown2.Name = "numericUpDown2";
		this.numericUpDown2.Size = new System.Drawing.Size(34, 21);
		this.numericUpDown2.TabIndex = 4;
		this.numericUpDown2.Value = new decimal(new int[4] { 4, 0, 0, 0 });
		this.cbRptJYTime.AutoSize = true;
		this.cbRptJYTime.Checked = true;
		this.cbRptJYTime.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptJYTime.ForeColor = System.Drawing.Color.Black;
		this.cbRptJYTime.Location = new System.Drawing.Point(80, 58);
		this.cbRptJYTime.Name = "cbRptJYTime";
		this.cbRptJYTime.Size = new System.Drawing.Size(72, 16);
		this.cbRptJYTime.TabIndex = 3;
		this.cbRptJYTime.Text = "进样时间";
		this.cbRptJYTime.UseVisualStyleBackColor = true;
		this.cbRptWithResult.AutoSize = true;
		this.cbRptWithResult.Checked = true;
		this.cbRptWithResult.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithResult.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithResult.Location = new System.Drawing.Point(74, 78);
		this.cbRptWithResult.Name = "cbRptWithResult";
		this.cbRptWithResult.Size = new System.Drawing.Size(72, 16);
		this.cbRptWithResult.TabIndex = 3;
		this.cbRptWithResult.Text = "结果数据";
		this.cbRptWithResult.UseVisualStyleBackColor = true;
		this.cbRptWithSourceData.AutoSize = true;
		this.cbRptWithSourceData.Checked = true;
		this.cbRptWithSourceData.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithSourceData.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithSourceData.Location = new System.Drawing.Point(164, 58);
		this.cbRptWithSourceData.Name = "cbRptWithSourceData";
		this.cbRptWithSourceData.Size = new System.Drawing.Size(120, 16);
		this.cbRptWithSourceData.TabIndex = 3;
		this.cbRptWithSourceData.Text = "工作曲线原始数据";
		this.cbRptWithSourceData.UseVisualStyleBackColor = true;
		this.cbRptChromBold.AutoSize = true;
		this.cbRptChromBold.ForeColor = System.Drawing.Color.Black;
		this.cbRptChromBold.Location = new System.Drawing.Point(221, 103);
		this.cbRptChromBold.Name = "cbRptChromBold";
		this.cbRptChromBold.Size = new System.Drawing.Size(72, 16);
		this.cbRptChromBold.TabIndex = 3;
		this.cbRptChromBold.Text = "谱线加粗";
		this.cbRptChromBold.UseVisualStyleBackColor = true;
		this.cbRptBoundary.AutoSize = true;
		this.cbRptBoundary.Checked = true;
		this.cbRptBoundary.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptBoundary.ForeColor = System.Drawing.Color.Black;
		this.cbRptBoundary.Location = new System.Drawing.Point(164, 79);
		this.cbRptBoundary.Name = "cbRptBoundary";
		this.cbRptBoundary.Size = new System.Drawing.Size(72, 16);
		this.cbRptBoundary.TabIndex = 3;
		this.cbRptBoundary.Text = "谱图加框";
		this.cbRptBoundary.UseVisualStyleBackColor = true;
		this.cbRptWithChrom.AutoSize = true;
		this.cbRptWithChrom.Checked = true;
		this.cbRptWithChrom.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithChrom.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithChrom.Location = new System.Drawing.Point(8, 102);
		this.cbRptWithChrom.Name = "cbRptWithChrom";
		this.cbRptWithChrom.Size = new System.Drawing.Size(48, 16);
		this.cbRptWithChrom.TabIndex = 3;
		this.cbRptWithChrom.Text = "谱图";
		this.cbRptWithChrom.UseVisualStyleBackColor = true;
		this.cbRptFileName.AutoSize = true;
		this.cbRptFileName.Checked = true;
		this.cbRptFileName.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptFileName.ForeColor = System.Drawing.Color.Black;
		this.cbRptFileName.Location = new System.Drawing.Point(8, 80);
		this.cbRptFileName.Name = "cbRptFileName";
		this.cbRptFileName.Size = new System.Drawing.Size(60, 16);
		this.cbRptFileName.TabIndex = 3;
		this.cbRptFileName.Text = "文件名";
		this.cbRptFileName.UseVisualStyleBackColor = true;
		this.cbRptWithTailFactor.AutoSize = true;
		this.cbRptWithTailFactor.Checked = true;
		this.cbRptWithTailFactor.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithTailFactor.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithTailFactor.Location = new System.Drawing.Point(245, 221);
		this.cbRptWithTailFactor.Name = "cbRptWithTailFactor";
		this.cbRptWithTailFactor.Size = new System.Drawing.Size(72, 16);
		this.cbRptWithTailFactor.TabIndex = 3;
		this.cbRptWithTailFactor.Text = "拖尾因子";
		this.cbRptWithTailFactor.UseVisualStyleBackColor = true;
		this.cbRptWithCapFactor.AutoSize = true;
		this.cbRptWithCapFactor.Checked = true;
		this.cbRptWithCapFactor.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithCapFactor.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithCapFactor.Location = new System.Drawing.Point(154, 201);
		this.cbRptWithCapFactor.Name = "cbRptWithCapFactor";
		this.cbRptWithCapFactor.Size = new System.Drawing.Size(72, 16);
		this.cbRptWithCapFactor.TabIndex = 3;
		this.cbRptWithCapFactor.Text = "容量因子";
		this.cbRptWithCapFactor.UseVisualStyleBackColor = true;
		this.cbRptWithValidNumber.AutoSize = true;
		this.cbRptWithValidNumber.Checked = true;
		this.cbRptWithValidNumber.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithValidNumber.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithValidNumber.Location = new System.Drawing.Point(71, 201);
		this.cbRptWithValidNumber.Name = "cbRptWithValidNumber";
		this.cbRptWithValidNumber.Size = new System.Drawing.Size(84, 16);
		this.cbRptWithValidNumber.TabIndex = 3;
		this.cbRptWithValidNumber.Text = "有效塔板数";
		this.cbRptWithValidNumber.UseVisualStyleBackColor = true;
		this.cbRptWithZhuXiao.AutoSize = true;
		this.cbRptWithZhuXiao.Checked = true;
		this.cbRptWithZhuXiao.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithZhuXiao.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithZhuXiao.Location = new System.Drawing.Point(8, 202);
		this.cbRptWithZhuXiao.Name = "cbRptWithZhuXiao";
		this.cbRptWithZhuXiao.Size = new System.Drawing.Size(48, 16);
		this.cbRptWithZhuXiao.TabIndex = 3;
		this.cbRptWithZhuXiao.Text = "柱效";
		this.cbRptWithZhuXiao.UseVisualStyleBackColor = true;
		this.cbRptWithPeakResulution.AutoSize = true;
		this.cbRptWithPeakResulution.Checked = true;
		this.cbRptWithPeakResulution.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakResulution.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakResulution.Location = new System.Drawing.Point(244, 201);
		this.cbRptWithPeakResulution.Name = "cbRptWithPeakResulution";
		this.cbRptWithPeakResulution.Size = new System.Drawing.Size(72, 16);
		this.cbRptWithPeakResulution.TabIndex = 3;
		this.cbRptWithPeakResulution.Text = "峰分离度";
		this.cbRptWithPeakResulution.UseVisualStyleBackColor = true;
		this.cbRptWithCoef.AutoSize = true;
		this.cbRptWithCoef.Checked = true;
		this.cbRptWithCoef.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithCoef.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithCoef.Location = new System.Drawing.Point(120, 223);
		this.cbRptWithCoef.Name = "cbRptWithCoef";
		this.cbRptWithCoef.Size = new System.Drawing.Size(72, 16);
		this.cbRptWithCoef.TabIndex = 3;
		this.cbRptWithCoef.Text = "相关系数";
		this.cbRptWithCoef.UseVisualStyleBackColor = true;
		this.cbRptWithCoef.Visible = false;
		this.cbRptWithCurve.AutoSize = true;
		this.cbRptWithCurve.Checked = true;
		this.cbRptWithCurve.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithCurve.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithCurve.Location = new System.Drawing.Point(8, 223);
		this.cbRptWithCurve.Name = "cbRptWithCurve";
		this.cbRptWithCurve.Size = new System.Drawing.Size(108, 16);
		this.cbRptWithCurve.TabIndex = 3;
		this.cbRptWithCurve.Text = "工作曲线及方程";
		this.cbRptWithCurve.UseVisualStyleBackColor = true;
		this.cbRptWithPeakSingle.AutoSize = true;
		this.cbRptWithPeakSingle.Checked = true;
		this.cbRptWithPeakSingle.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakSingle.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakSingle.Location = new System.Drawing.Point(245, 160);
		this.cbRptWithPeakSingle.Name = "cbRptWithPeakSingle";
		this.cbRptWithPeakSingle.Size = new System.Drawing.Size(60, 16);
		this.cbRptWithPeakSingle.TabIndex = 3;
		this.cbRptWithPeakSingle.Text = "峰标志";
		this.cbRptWithPeakSingle.UseVisualStyleBackColor = true;
		this.cbRptWithPeakHalf.AutoSize = true;
		this.cbRptWithPeakHalf.Checked = true;
		this.cbRptWithPeakHalf.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakHalf.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakHalf.Location = new System.Drawing.Point(174, 160);
		this.cbRptWithPeakHalf.Name = "cbRptWithPeakHalf";
		this.cbRptWithPeakHalf.Size = new System.Drawing.Size(60, 16);
		this.cbRptWithPeakHalf.TabIndex = 3;
		this.cbRptWithPeakHalf.Text = "半峰宽";
		this.cbRptWithPeakHalf.UseVisualStyleBackColor = true;
		this.cbRptWithPeakHeightPercent.AutoSize = true;
		this.cbRptWithPeakHeightPercent.Checked = true;
		this.cbRptWithPeakHeightPercent.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakHeightPercent.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakHeightPercent.Location = new System.Drawing.Point(212, 179);
		this.cbRptWithPeakHeightPercent.Name = "cbRptWithPeakHeightPercent";
		this.cbRptWithPeakHeightPercent.Size = new System.Drawing.Size(84, 16);
		this.cbRptWithPeakHeightPercent.TabIndex = 3;
		this.cbRptWithPeakHeightPercent.Text = "峰高百分比";
		this.cbRptWithPeakHeightPercent.UseVisualStyleBackColor = true;
		this.cbRptWithPeakAreaPercent.AutoSize = true;
		this.cbRptWithPeakAreaPercent.Checked = true;
		this.cbRptWithPeakAreaPercent.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakAreaPercent.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakAreaPercent.Location = new System.Drawing.Point(111, 179);
		this.cbRptWithPeakAreaPercent.Name = "cbRptWithPeakAreaPercent";
		this.cbRptWithPeakAreaPercent.Size = new System.Drawing.Size(96, 16);
		this.cbRptWithPeakAreaPercent.TabIndex = 3;
		this.cbRptWithPeakAreaPercent.Text = "峰面积百分比";
		this.cbRptWithPeakAreaPercent.UseVisualStyleBackColor = true;
		this.cbRptWithPeakHeight.AutoSize = true;
		this.cbRptWithPeakHeight.Checked = true;
		this.cbRptWithPeakHeight.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakHeight.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakHeight.Location = new System.Drawing.Point(120, 160);
		this.cbRptWithPeakHeight.Name = "cbRptWithPeakHeight";
		this.cbRptWithPeakHeight.Size = new System.Drawing.Size(48, 16);
		this.cbRptWithPeakHeight.TabIndex = 3;
		this.cbRptWithPeakHeight.Text = "峰高";
		this.cbRptWithPeakHeight.UseVisualStyleBackColor = true;
		this.cbRptWithDensityPercent.AutoSize = true;
		this.cbRptWithDensityPercent.Checked = true;
		this.cbRptWithDensityPercent.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithDensityPercent.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithDensityPercent.Location = new System.Drawing.Point(8, 179);
		this.cbRptWithDensityPercent.Name = "cbRptWithDensityPercent";
		this.cbRptWithDensityPercent.Size = new System.Drawing.Size(84, 16);
		this.cbRptWithDensityPercent.TabIndex = 3;
		this.cbRptWithDensityPercent.Text = "浓度百分比";
		this.cbRptWithDensityPercent.UseVisualStyleBackColor = true;
		this.cbRptWithPeakArea.AutoSize = true;
		this.cbRptWithPeakArea.Checked = true;
		this.cbRptWithPeakArea.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithPeakArea.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithPeakArea.Location = new System.Drawing.Point(62, 160);
		this.cbRptWithPeakArea.Name = "cbRptWithPeakArea";
		this.cbRptWithPeakArea.Size = new System.Drawing.Size(60, 16);
		this.cbRptWithPeakArea.TabIndex = 3;
		this.cbRptWithPeakArea.Text = "峰面积";
		this.cbRptWithPeakArea.UseVisualStyleBackColor = true;
		this.cbRptWithDensity.AutoSize = true;
		this.cbRptWithDensity.Checked = true;
		this.cbRptWithDensity.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithDensity.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithDensity.Location = new System.Drawing.Point(8, 160);
		this.cbRptWithDensity.Name = "cbRptWithDensity";
		this.cbRptWithDensity.Size = new System.Drawing.Size(48, 16);
		this.cbRptWithDensity.TabIndex = 3;
		this.cbRptWithDensity.Text = "浓度";
		this.cbRptWithDensity.UseVisualStyleBackColor = true;
		this.cbRptCorrFactor.AutoSize = true;
		this.cbRptCorrFactor.Checked = true;
		this.cbRptCorrFactor.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptCorrFactor.ForeColor = System.Drawing.Color.Black;
		this.cbRptCorrFactor.Location = new System.Drawing.Point(245, 142);
		this.cbRptCorrFactor.Name = "cbRptCorrFactor";
		this.cbRptCorrFactor.Size = new System.Drawing.Size(72, 16);
		this.cbRptCorrFactor.TabIndex = 3;
		this.cbRptCorrFactor.Text = "校正因子";
		this.cbRptCorrFactor.UseVisualStyleBackColor = true;
		this.cbRptZuFenName.AutoSize = true;
		this.cbRptZuFenName.Checked = true;
		this.cbRptZuFenName.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptZuFenName.ForeColor = System.Drawing.Color.Black;
		this.cbRptZuFenName.Location = new System.Drawing.Point(145, 142);
		this.cbRptZuFenName.Name = "cbRptZuFenName";
		this.cbRptZuFenName.Size = new System.Drawing.Size(72, 16);
		this.cbRptZuFenName.TabIndex = 3;
		this.cbRptZuFenName.Text = "组份名称";
		this.cbRptZuFenName.UseVisualStyleBackColor = true;
		this.cbRptKeepTime.AutoSize = true;
		this.cbRptKeepTime.Checked = true;
		this.cbRptKeepTime.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptKeepTime.ForeColor = System.Drawing.Color.Black;
		this.cbRptKeepTime.Location = new System.Drawing.Point(62, 142);
		this.cbRptKeepTime.Name = "cbRptKeepTime";
		this.cbRptKeepTime.Size = new System.Drawing.Size(72, 16);
		this.cbRptKeepTime.TabIndex = 3;
		this.cbRptKeepTime.Text = "保留时间";
		this.cbRptKeepTime.UseVisualStyleBackColor = true;
		this.cbRptWithIdx.AutoSize = true;
		this.cbRptWithIdx.Checked = true;
		this.cbRptWithIdx.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptWithIdx.ForeColor = System.Drawing.Color.Black;
		this.cbRptWithIdx.Location = new System.Drawing.Point(8, 142);
		this.cbRptWithIdx.Name = "cbRptWithIdx";
		this.cbRptWithIdx.Size = new System.Drawing.Size(48, 16);
		this.cbRptWithIdx.TabIndex = 3;
		this.cbRptWithIdx.Text = "序号";
		this.cbRptWithIdx.UseVisualStyleBackColor = true;
		this.cbRptPrintTime.AutoSize = true;
		this.cbRptPrintTime.Checked = true;
		this.cbRptPrintTime.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbRptPrintTime.ForeColor = System.Drawing.Color.Black;
		this.cbRptPrintTime.Location = new System.Drawing.Point(8, 58);
		this.cbRptPrintTime.Name = "cbRptPrintTime";
		this.cbRptPrintTime.Size = new System.Drawing.Size(72, 16);
		this.cbRptPrintTime.TabIndex = 3;
		this.cbRptPrintTime.Text = "打印时间";
		this.cbRptPrintTime.UseVisualStyleBackColor = true;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(127, 103);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(23, 12);
		this.label12.TabIndex = 1;
		this.label12.Text = "高:";
		this.label12.Visible = false;
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(244, 80);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(35, 12);
		this.label30.TabIndex = 1;
		this.label30.Text = "字号:";
		this.label56.AutoSize = true;
		this.label56.Location = new System.Drawing.Point(57, 103);
		this.label56.Name = "label56";
		this.label56.Size = new System.Drawing.Size(23, 12);
		this.label56.TabIndex = 1;
		this.label56.Text = "宽:";
		this.label56.Visible = false;
		this.ReportTitle.Font = new System.Drawing.Font("宋体", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.ReportTitle.Location = new System.Drawing.Point(47, 12);
		this.ReportTitle.Multiline = true;
		this.ReportTitle.Name = "ReportTitle";
		this.ReportTitle.Size = new System.Drawing.Size(266, 40);
		this.ReportTitle.TabIndex = 2;
		this.ReportTitle.Text = "XXXX分析报告";
		this.label68.AutoSize = true;
		this.label68.Location = new System.Drawing.Point(6, 23);
		this.label68.Name = "label68";
		this.label68.Size = new System.Drawing.Size(35, 12);
		this.label68.TabIndex = 1;
		this.label68.Text = "标题:";
		this.groupBox17.Controls.Add(this.rptbotom);
		this.groupBox17.Location = new System.Drawing.Point(25, 365);
		this.groupBox17.Name = "groupBox17";
		this.groupBox17.Size = new System.Drawing.Size(327, 82);
		this.groupBox17.TabIndex = 4;
		this.groupBox17.TabStop = false;
		this.groupBox17.Text = "报告尾";
		this.rptbotom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rptbotom.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.rptbotom.Location = new System.Drawing.Point(3, 17);
		this.rptbotom.Multiline = true;
		this.rptbotom.Name = "rptbotom";
		this.rptbotom.Size = new System.Drawing.Size(321, 62);
		this.rptbotom.TabIndex = 1;
		this.rptbotom.Text = "备注：按                 检验，浓度含量单位：g/l\r\n检测结果:                检验部门：\r\n检验员：                 审核员：";
		this.groupBox15.Controls.Add(this.rpthead);
		this.groupBox15.Location = new System.Drawing.Point(28, 253);
		this.groupBox15.Name = "groupBox15";
		this.groupBox15.Size = new System.Drawing.Size(324, 110);
		this.groupBox15.TabIndex = 4;
		this.groupBox15.TabStop = false;
		this.groupBox15.Text = "报告头";
		this.rpthead.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rpthead.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.rpthead.Location = new System.Drawing.Point(3, 17);
		this.rpthead.Multiline = true;
		this.rpthead.Name = "rpthead";
		this.rpthead.Size = new System.Drawing.Size(318, 90);
		this.rpthead.TabIndex = 1;
		this.rpthead.Text = "质检（E）字第（ \u3000）号\r\n送样单位：         \u3000\u3000        仪器型号:\r\n取样日期：   年  月  日       收样日期：     年  月  日\r\n样品批号：                       样品名称：固液\r\n样品罐号：                       仪器控制参数文件：";
		this.groupBox1.Controls.Add(this.radioButton8);
		this.groupBox1.Controls.Add(this.radioButton4);
		this.groupBox1.Controls.Add(this.radioButton7);
		this.groupBox1.Location = new System.Drawing.Point(28, 453);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(324, 42);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "预览";
		this.radioButton8.AutoSize = true;
		this.radioButton8.Checked = true;
		this.radioButton8.Location = new System.Drawing.Point(211, 20);
		this.radioButton8.Name = "radioButton8";
		this.radioButton8.Size = new System.Drawing.Size(71, 16);
		this.radioButton8.TabIndex = 0;
		this.radioButton8.TabStop = true;
		this.radioButton8.Text = "程序自带";
		this.radioButton8.UseVisualStyleBackColor = true;
		this.radioButton4.AutoSize = true;
		this.radioButton4.Location = new System.Drawing.Point(107, 20);
		this.radioButton4.Name = "radioButton4";
		this.radioButton4.Size = new System.Drawing.Size(47, 16);
		this.radioButton4.TabIndex = 0;
		this.radioButton4.Text = "Word";
		this.radioButton4.UseVisualStyleBackColor = true;
		this.radioButton7.AutoSize = true;
		this.radioButton7.Location = new System.Drawing.Point(8, 20);
		this.radioButton7.Name = "radioButton7";
		this.radioButton7.Size = new System.Drawing.Size(59, 16);
		this.radioButton7.TabIndex = 0;
		this.radioButton7.Text = "写字板";
		this.radioButton7.UseVisualStyleBackColor = true;
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.设为参比峰ToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(69, 26);
		this.设为参比峰ToolStripMenuItem.Name = "设为参比峰ToolStripMenuItem";
		this.设为参比峰ToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_0.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "下载.gif");
		this.imageList_0.Images.SetKeyName(1, "gif_47_091.gif");
		this.cmsCali.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.miAddrow, this.miColumnsSetup, this.miRestoreDftColumns, this.miDelRow });
		this.cmsCali.Name = "cmsCali";
		this.cmsCali.Size = new System.Drawing.Size(161, 92);
		this.miAddrow.Name = "miAddrow";
		this.miAddrow.Size = new System.Drawing.Size(160, 22);
		this.miAddrow.Text = "添加行";
		this.miColumnsSetup.Name = "miColumnsSetup";
		this.miColumnsSetup.Size = new System.Drawing.Size(160, 22);
		this.miColumnsSetup.Text = "列设置...";
		this.miRestoreDftColumns.Name = "miRestoreDftColumns";
		this.miRestoreDftColumns.Size = new System.Drawing.Size(160, 22);
		this.miRestoreDftColumns.Text = "恢复默认列设置";
		this.miDelRow.Name = "miDelRow";
		this.miDelRow.Size = new System.Drawing.Size(160, 22);
		this.miDelRow.Text = "删除行";
		this.miDelRow.Click += new System.EventHandler(miDelRow_Click);
		this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.gvCmpds);
		this.splitContainer1.Panel1.Controls.Add(this.btnApply);
		this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
		this.splitContainer1.Panel2.Controls.Add(this.label25);
		this.splitContainer1.Panel2.Controls.Add(this.ucCBSites);
		this.splitContainer1.Panel2.Controls.Add(this.ucNTBCnt);
		this.splitContainer1.Panel2.Controls.Add(this.dpgnlChrom);
		this.splitContainer1.Panel2.Controls.Add(this.btnStartCali);
		this.splitContainer1.Panel2.Controls.Add(this.label21);
		this.splitContainer1.Size = new System.Drawing.Size(775, 522);
		this.splitContainer1.SplitterDistance = 228;
		this.splitContainer1.TabIndex = 14;
		this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnApply.BackColor = System.Drawing.Color.White;
		this.btnApply.BtnBackColor = System.Drawing.Color.White;
		this.btnApply.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnApply.BtnForeColor = System.Drawing.Color.White;
		this.btnApply.BtnText = "应用";
		this.btnApply.ConerRadius = 20;
		this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnApply.EnabledMouseEffect = true;
		this.btnApply.FillColor = System.Drawing.Color.OrangeRed;
		this.btnApply.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnApply.IsRadius = true;
		this.btnApply.IsShowRect = true;
		this.btnApply.IsShowTips = false;
		this.btnApply.Location = new System.Drawing.Point(588, 179);
		this.btnApply.Margin = new System.Windows.Forms.Padding(0);
		this.btnApply.Name = "btnApply";
		this.btnApply.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnApply.RectWidth = 1;
		this.btnApply.Size = new System.Drawing.Size(115, 46);
		this.btnApply.TabIndex = 7;
		this.btnApply.TabStop = false;
		this.btnApply.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnApply.TipsText = "";
		this.btnApply.BtnClick += new System.EventHandler(btnApply_BtnClick);
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label25.ForeColor = System.Drawing.Color.OrangeRed;
		this.label25.Location = new System.Drawing.Point(22, 213);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(49, 20);
		this.label25.TabIndex = 40;
		this.label25.Text = "单位";
		this.ucCBSites.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ucCBSites.BackColor = System.Drawing.Color.Transparent;
		this.ucCBSites.BackColorExt = System.Drawing.Color.FromArgb(240, 240, 240);
		this.ucCBSites.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
		this.ucCBSites.ConerRadius = 5;
		this.ucCBSites.DropPanelHeight = -1;
		this.ucCBSites.FillColor = System.Drawing.Color.White;
		this.ucCBSites.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.ucCBSites.IsRadius = true;
		this.ucCBSites.IsShowRect = true;
		this.ucCBSites.ItemWidth = 70;
		this.ucCBSites.Location = new System.Drawing.Point(117, 207);
		this.ucCBSites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucCBSites.Name = "ucCBSites";
		this.ucCBSites.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucCBSites.RectWidth = 1;
		this.ucCBSites.SelectedIndex = -1;
		this.ucCBSites.SelectedValue = "";
		this.ucCBSites.Size = new System.Drawing.Size(152, 32);
		this.ucCBSites.Source = null;
		this.ucCBSites.TabIndex = 39;
		this.ucCBSites.TextValue = null;
		this.ucCBSites.TriangleColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucCBSites.SelectedChangedEvent += new System.EventHandler(ucCBSites_SelectedChangedEvent);
		this.ucNTBCnt.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ucNTBCnt.Increment = new decimal(new int[4] { 1, 0, 0, 0 });
		this.ucNTBCnt.InputType = HZH_Controls.TextInputType.Number;
		this.ucNTBCnt.IsNumCanInput = true;
		this.ucNTBCnt.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucNTBCnt.Location = new System.Drawing.Point(117, 243);
		this.ucNTBCnt.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucNTBCnt.MinValue = new decimal(new int[4]);
		this.ucNTBCnt.Name = "ucNTBCnt";
		this.ucNTBCnt.Num = new decimal(new int[4] { 1, 0, 0, 0 });
		this.ucNTBCnt.Padding = new System.Windows.Forms.Padding(2);
		this.ucNTBCnt.Size = new System.Drawing.Size(152, 44);
		this.ucNTBCnt.TabIndex = 12;
		this.ucNTBCnt.NumChanged += new System.EventHandler(ucNTBCnt_NumChanged);
		this.btnStartCali.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnStartCali.BackColor = System.Drawing.Color.White;
		this.btnStartCali.BtnBackColor = System.Drawing.Color.White;
		this.btnStartCali.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnStartCali.BtnForeColor = System.Drawing.Color.White;
		this.btnStartCali.BtnText = "开始校正";
		this.btnStartCali.ConerRadius = 20;
		this.btnStartCali.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnStartCali.EnabledMouseEffect = true;
		this.btnStartCali.FillColor = System.Drawing.Color.OrangeRed;
		this.btnStartCali.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnStartCali.IsRadius = true;
		this.btnStartCali.IsShowRect = true;
		this.btnStartCali.IsShowTips = false;
		this.btnStartCali.Location = new System.Drawing.Point(386, 232);
		this.btnStartCali.Margin = new System.Windows.Forms.Padding(0);
		this.btnStartCali.Name = "btnStartCali";
		this.btnStartCali.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnStartCali.RectWidth = 1;
		this.btnStartCali.Size = new System.Drawing.Size(115, 46);
		this.btnStartCali.TabIndex = 6;
		this.btnStartCali.TabStop = false;
		this.btnStartCali.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnStartCali.TipsText = "";
		this.btnStartCali.BtnClick += new System.EventHandler(btnStartCali_BtnClick);
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label21.ForeColor = System.Drawing.Color.OrangeRed;
		this.label21.Location = new System.Drawing.Point(22, 256);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(89, 20);
		this.label21.TabIndex = 5;
		this.label21.Text = "运行次数";
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridViewTextBoxColumn1.HeaderText = "";
		this.dataGridViewTextBoxColumn1.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn1.MinimumWidth = 27;
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 60;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.Format = "0.0";
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dataGridViewTextBoxColumn2.HeaderText = "设定[℃]";
		this.dataGridViewTextBoxColumn2.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Width = 60;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dataGridViewTextBoxColumn3.HeaderText = "事件2 [min]";
		this.dataGridViewTextBoxColumn3.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 66;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle7;
		this.dataGridViewTextBoxColumn4.HeaderText = "事件3 [min]";
		this.dataGridViewTextBoxColumn4.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.Width = 66;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle8;
		this.dataGridViewTextBoxColumn5.HeaderText = "事件4 [min]";
		this.dataGridViewTextBoxColumn5.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		this.dataGridViewTextBoxColumn5.ReadOnly = true;
		this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn5.Width = 66;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle9;
		this.dataGridViewTextBoxColumn6.HeaderText = "";
		this.dataGridViewTextBoxColumn6.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.dataGridViewTextBoxColumn6.ReadOnly = true;
		this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn6.Width = 60;
		this.gvgcProgTemp.AllowUserToAddRows = false;
		this.gvgcProgTemp.AllowUserToDeleteRows = false;
		this.gvgcProgTemp.AllowUserToResizeRows = false;
		this.gvgcProgTemp.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvgcProgTemp.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvgcProgTemp.ColumnHeadersHeight = 32;
		this.gvgcProgTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvgcProgTemp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvgcProgTemp.Enabled = false;
		this.gvgcProgTemp.Location = new System.Drawing.Point(726, 1737);
		this.gvgcProgTemp.Name = "gvgcProgTemp";
		this.gvgcProgTemp.ReadOnly = true;
		this.gvgcProgTemp.RowHeadersWidth = 25;
		this.gvgcProgTemp.RowTemplate.Height = 16;
		this.gvgcProgTemp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvgcProgTemp.ShowCellToolTips = false;
		this.gvgcProgTemp.Size = new System.Drawing.Size(204, 214);
		this.gvgcProgTemp.TabIndex = 9;
		this.gvgcProgTemp.Visible = false;
		this.lclButton2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.lclButton2.ForeColor = System.Drawing.Color.Red;
		this.lclButton2.Location = new System.Drawing.Point(298, 2236);
		this.lclButton2.Name = "lclButton2";
		this.lclButton2.Size = new System.Drawing.Size(98, 23);
		this.lclButton2.TabIndex = 21;
		this.lclButton2.Text = "下载到仪器";
		this.lclButton2.UseVisualStyleBackColor = true;
		this.lclButton2.Click += new System.EventHandler(lclButton2_Click);
		this.lclButton1.Location = new System.Drawing.Point(100, 2236);
		this.lclButton1.Name = "lclButton1";
		this.lclButton1.Size = new System.Drawing.Size(98, 23);
		this.lclButton1.TabIndex = 21;
		this.lclButton1.Text = "获取仪器参数";
		this.lclButton1.UseVisualStyleBackColor = true;
		this.lclButton1.Click += new System.EventHandler(lclButton1_Click);
		dataGridViewCellStyle10.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle10;
		this.dataGridViewTextBoxColumn8.HeaderText = "";
		this.dataGridViewTextBoxColumn8.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn8.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
		this.dataGridViewTextBoxColumn8.ReadOnly = true;
		this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn8.Width = 80;
		dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle11;
		this.dataGridViewTextBoxColumn13.HeaderText = "速率    (psi/min)";
		this.dataGridViewTextBoxColumn13.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
		this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn13.Width = 80;
		dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle12;
		this.dataGridViewTextBoxColumn14.HeaderText = "保持    (psi)";
		this.dataGridViewTextBoxColumn14.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
		this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn14.Width = 80;
		dataGridViewCellStyle13.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle13;
		this.dataGridViewTextBoxColumn15.HeaderText = "时间    (min)";
		this.dataGridViewTextBoxColumn15.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
		this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn15.Width = 80;
		this.clmCT6CN.HeaderText = "";
		this.clmCT6CN.Name = "clmCT6CN";
		this.clmCT6CN.ReadOnly = true;
		this.clmCT6CN.Width = 60;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle14.Format = "0.0";
		this.clmCT6SetT.DefaultCellStyle = dataGridViewCellStyle14;
		this.clmCT6SetT.HeaderText = "设定[℃]";
		this.clmCT6SetT.Name = "clmCT6SetT";
		this.clmCT6SetT.ReadOnly = true;
		this.clmCT6SetT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmCT6SetT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmCT6SetT.Width = 60;
		this.pnlcu.AutoScroll = true;
		this.pnlcu.Controls.Add(this.spcComponent);
		this.pnlcu.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlcu.Location = new System.Drawing.Point(3, 3);
		this.pnlcu.Name = "pnlcu";
		this.pnlcu.Size = new System.Drawing.Size(769, 516);
		this.pnlcu.TabIndex = 17;
		this.spcComponent.Dock = System.Windows.Forms.DockStyle.Fill;
		this.spcComponent.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.spcComponent.IsSplitterFixed = true;
		this.spcComponent.Location = new System.Drawing.Point(0, 0);
		this.spcComponent.Name = "spcComponent";
		this.spcComponent.Panel1.Controls.Add(this.gbO);
		this.spcComponent.Panel1.Controls.Add(this.gbCalibration);
		this.spcComponent.Panel1.Controls.Add(this.gbcuRltTableReport);
		this.spcComponent.Panel1.Controls.Add(this.lclGroupBox1);
		this.spcComponent.Panel1.Controls.Add(this.groupBox2);
		this.spcComponent.Panel1.Controls.Add(this.gbadvAddSub);
		this.spcComponent.Panel1.Controls.Add(this.gbadvColumnCalcu);
		this.spcComponent.Size = new System.Drawing.Size(769, 516);
		this.spcComponent.SplitterDistance = 500;
		this.spcComponent.TabIndex = 14;
		this.gbO.Controls.Add(this.sdaOpen);
		this.gbO.Controls.Add(this.tbSdaFileName);
		this.gbO.Location = new System.Drawing.Point(5, 494);
		this.gbO.Name = "gbO";
		this.gbO.Size = new System.Drawing.Size(317, 46);
		this.gbO.TabIndex = 13;
		this.gbO.TabStop = false;
		this.gbO.Text = "氧含量谱图";
		this.gbO.Visible = false;
		this.sdaOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.sdaOpen.Location = new System.Drawing.Point(266, 19);
		this.sdaOpen.Name = "sdaOpen";
		this.sdaOpen.Size = new System.Drawing.Size(45, 22);
		this.sdaOpen.TabIndex = 31;
		this.sdaOpen.UseVisualStyleBackColor = true;
		this.sdaOpen.Click += new System.EventHandler(sdaOpen_Click);
		this.tbSdaFileName.Location = new System.Drawing.Point(4, 19);
		this.tbSdaFileName.Name = "tbSdaFileName";
		this.tbSdaFileName.Size = new System.Drawing.Size(248, 21);
		this.tbSdaFileName.TabIndex = 5;
		this.gbCalibration.Controls.Add(this.btncclNew);
		this.gbCalibration.Controls.Add(this.btncclView);
		this.gbCalibration.Controls.Add(this.btncclNone);
		this.gbCalibration.Controls.Add(this.btncclSet);
		this.gbCalibration.Controls.Add(this.tbcclCalibration);
		this.gbCalibration.Location = new System.Drawing.Point(3, 3);
		this.gbCalibration.Name = "gbCalibration";
		this.gbCalibration.Size = new System.Drawing.Size(323, 76);
		this.gbCalibration.TabIndex = 12;
		this.gbCalibration.TabStop = false;
		this.gbCalibration.Text = "定量组份表文件";
		this.btncclNew.Location = new System.Drawing.Point(185, 48);
		this.btncclNew.Name = "btncclNew";
		this.btncclNew.Size = new System.Drawing.Size(136, 23);
		this.btncclNew.TabIndex = 2;
		this.btncclNew.Text = "根据标样新建";
		this.btncclNew.UseVisualStyleBackColor = true;
		this.btncclNew.Click += new System.EventHandler(btncclNew_Click);
		this.btncclView.Location = new System.Drawing.Point(84, 48);
		this.btncclView.Name = "btncclView";
		this.btncclView.Size = new System.Drawing.Size(69, 23);
		this.btncclView.TabIndex = 1;
		this.btncclView.Text = "查看";
		this.btncclView.UseVisualStyleBackColor = true;
		this.btncclView.Click += new System.EventHandler(btncclView_Click);
		this.btncclNone.Location = new System.Drawing.Point(6, 48);
		this.btncclNone.Name = "btncclNone";
		this.btncclNone.Size = new System.Drawing.Size(60, 23);
		this.btncclNone.TabIndex = 1;
		this.btncclNone.Text = "清除";
		this.btncclNone.UseVisualStyleBackColor = true;
		this.btncclNone.Click += new System.EventHandler(btncclNone_Click);
		this.btncclSet.Location = new System.Drawing.Point(261, 18);
		this.btncclSet.Name = "btncclSet";
		this.btncclSet.Size = new System.Drawing.Size(60, 23);
		this.btncclSet.TabIndex = 1;
		this.btncclSet.Text = "打开";
		this.btncclSet.UseVisualStyleBackColor = true;
		this.btncclSet.Click += new System.EventHandler(btncclSet_Click);
		this.tbcclCalibration.Location = new System.Drawing.Point(6, 20);
		this.tbcclCalibration.Name = "tbcclCalibration";
		this.tbcclCalibration.ReadOnly = true;
		this.tbcclCalibration.Size = new System.Drawing.Size(248, 21);
		this.tbcclCalibration.TabIndex = 0;
		this.gbcuRltTableReport.Controls.Add(this.rbrtrCaliPeaks);
		this.gbcuRltTableReport.Controls.Add(this.rbrtrIdentifiedPeaks);
		this.gbcuRltTableReport.Controls.Add(this.rbrtrAllDetectedPeaks);
		this.gbcuRltTableReport.Controls.Add(this.cbrtrHideISTDPeak);
		this.gbcuRltTableReport.Location = new System.Drawing.Point(3, 80);
		this.gbcuRltTableReport.Name = "gbcuRltTableReport";
		this.gbcuRltTableReport.Size = new System.Drawing.Size(122, 92);
		this.gbcuRltTableReport.TabIndex = 0;
		this.gbcuRltTableReport.TabStop = false;
		this.gbcuRltTableReport.Text = "结果显示";
		this.rbrtrCaliPeaks.AutoSize = true;
		this.rbrtrCaliPeaks.Location = new System.Drawing.Point(10, 72);
		this.rbrtrCaliPeaks.Name = "rbrtrCaliPeaks";
		this.rbrtrCaliPeaks.Size = new System.Drawing.Size(107, 16);
		this.rbrtrCaliPeaks.TabIndex = 1;
		this.rbrtrCaliPeaks.Text = "显示所有校正峰";
		this.rbrtrCaliPeaks.UseVisualStyleBackColor = true;
		this.rbrtrIdentifiedPeaks.AutoSize = true;
		this.rbrtrIdentifiedPeaks.Location = new System.Drawing.Point(10, 54);
		this.rbrtrIdentifiedPeaks.Name = "rbrtrIdentifiedPeaks";
		this.rbrtrIdentifiedPeaks.Size = new System.Drawing.Size(107, 16);
		this.rbrtrIdentifiedPeaks.TabIndex = 1;
		this.rbrtrIdentifiedPeaks.Text = "显示所有识别峰";
		this.rbrtrIdentifiedPeaks.UseVisualStyleBackColor = true;
		this.rbrtrAllDetectedPeaks.AutoSize = true;
		this.rbrtrAllDetectedPeaks.Checked = true;
		this.rbrtrAllDetectedPeaks.Location = new System.Drawing.Point(10, 36);
		this.rbrtrAllDetectedPeaks.Name = "rbrtrAllDetectedPeaks";
		this.rbrtrAllDetectedPeaks.Size = new System.Drawing.Size(107, 16);
		this.rbrtrAllDetectedPeaks.TabIndex = 1;
		this.rbrtrAllDetectedPeaks.TabStop = true;
		this.rbrtrAllDetectedPeaks.Text = "显示所有检测峰";
		this.rbrtrAllDetectedPeaks.UseVisualStyleBackColor = true;
		this.cbrtrHideISTDPeak.AutoSize = true;
		this.cbrtrHideISTDPeak.Location = new System.Drawing.Point(10, 18);
		this.cbrtrHideISTDPeak.Name = "cbrtrHideISTDPeak";
		this.cbrtrHideISTDPeak.Size = new System.Drawing.Size(84, 16);
		this.cbrtrHideISTDPeak.TabIndex = 0;
		this.cbrtrHideISTDPeak.Text = "隐藏内标峰";
		this.cbrtrHideISTDPeak.UseVisualStyleBackColor = true;
		this.lclGroupBox1.Controls.Add(this.tbcuAmount);
		this.lclGroupBox1.Controls.Add(this.lbcuAmount);
		this.lclGroupBox1.Controls.Add(this.lbcuIstdAmount);
		this.lclGroupBox1.Controls.Add(this.tbcuIstdAmount);
		this.lclGroupBox1.Controls.Add(this.gbcuScale);
		this.lclGroupBox1.Controls.Add(this.lbcuInjVolume);
		this.lclGroupBox1.Controls.Add(this.tbcuInjVolume);
		this.lclGroupBox1.Location = new System.Drawing.Point(5, 178);
		this.lclGroupBox1.Name = "lclGroupBox1";
		this.lclGroupBox1.Size = new System.Drawing.Size(319, 150);
		this.lclGroupBox1.TabIndex = 0;
		this.lclGroupBox1.TabStop = false;
		this.lclGroupBox1.Text = "量值";
		this.tbcuAmount.Location = new System.Drawing.Point(231, 37);
		this.tbcuAmount.Name = "tbcuAmount";
		this.tbcuAmount.Size = new System.Drawing.Size(76, 21);
		this.tbcuAmount.TabIndex = 0;
		this.lbcuAmount.AutoSize = true;
		this.lbcuAmount.Location = new System.Drawing.Point(171, 41);
		this.lbcuAmount.Name = "lbcuAmount";
		this.lbcuAmount.Size = new System.Drawing.Size(29, 12);
		this.lbcuAmount.TabIndex = 1;
		this.lbcuAmount.Text = "浓度";
		this.lbcuIstdAmount.AutoSize = true;
		this.lbcuIstdAmount.Location = new System.Drawing.Point(3, 41);
		this.lbcuIstdAmount.Name = "lbcuIstdAmount";
		this.lbcuIstdAmount.Size = new System.Drawing.Size(41, 12);
		this.lbcuIstdAmount.TabIndex = 1;
		this.lbcuIstdAmount.Text = "内标量";
		this.tbcuIstdAmount.Location = new System.Drawing.Point(77, 37);
		this.tbcuIstdAmount.Name = "tbcuIstdAmount";
		this.tbcuIstdAmount.Size = new System.Drawing.Size(76, 21);
		this.tbcuIstdAmount.TabIndex = 0;
		this.tbcuIstdAmount.Text = "0";
		this.gbcuScale.Controls.Add(this.cbprsUseScaleFactor);
		this.gbcuScale.Controls.Add(this.lbcuUnitAfterScale);
		this.gbcuScale.Controls.Add(this.lbcuScaleFactor);
		this.gbcuScale.Controls.Add(this.tbprsUnitAfterScale);
		this.gbcuScale.Controls.Add(this.lbcuDilution);
		this.gbcuScale.Controls.Add(this.tbprsScaleFactor);
		this.gbcuScale.Controls.Add(this.tbcuDilution);
		this.gbcuScale.Location = new System.Drawing.Point(3, 76);
		this.gbcuScale.Name = "gbcuScale";
		this.gbcuScale.Size = new System.Drawing.Size(313, 71);
		this.gbcuScale.TabIndex = 1;
		this.gbcuScale.TabStop = false;
		this.cbprsUseScaleFactor.AutoSize = true;
		this.cbprsUseScaleFactor.Location = new System.Drawing.Point(2, 1);
		this.cbprsUseScaleFactor.Name = "cbprsUseScaleFactor";
		this.cbprsUseScaleFactor.Size = new System.Drawing.Size(168, 16);
		this.cbprsUseScaleFactor.TabIndex = 0;
		this.cbprsUseScaleFactor.Text = "匹配峰使用缩放(结果乘数)";
		this.cbprsUseScaleFactor.UseVisualStyleBackColor = true;
		this.lbcuUnitAfterScale.AutoSize = true;
		this.lbcuUnitAfterScale.Location = new System.Drawing.Point(8, 46);
		this.lbcuUnitAfterScale.Name = "lbcuUnitAfterScale";
		this.lbcuUnitAfterScale.Size = new System.Drawing.Size(65, 12);
		this.lbcuUnitAfterScale.TabIndex = 1;
		this.lbcuUnitAfterScale.Text = "缩放后单位";
		this.lbcuScaleFactor.AutoSize = true;
		this.lbcuScaleFactor.Location = new System.Drawing.Point(8, 24);
		this.lbcuScaleFactor.Name = "lbcuScaleFactor";
		this.lbcuScaleFactor.Size = new System.Drawing.Size(53, 12);
		this.lbcuScaleFactor.TabIndex = 1;
		this.lbcuScaleFactor.Text = "浓缩因子";
		this.tbprsUnitAfterScale.AcceptsTab = true;
		this.tbprsUnitAfterScale.Location = new System.Drawing.Point(84, 41);
		this.tbprsUnitAfterScale.Name = "tbprsUnitAfterScale";
		this.tbprsUnitAfterScale.Size = new System.Drawing.Size(70, 21);
		this.tbprsUnitAfterScale.TabIndex = 0;
		this.tbprsUnitAfterScale.Text = "ul";
		this.lbcuDilution.AutoSize = true;
		this.lbcuDilution.Location = new System.Drawing.Point(169, 21);
		this.lbcuDilution.Name = "lbcuDilution";
		this.lbcuDilution.Size = new System.Drawing.Size(29, 12);
		this.lbcuDilution.TabIndex = 1;
		this.lbcuDilution.Text = "稀释";
		this.tbprsScaleFactor.Location = new System.Drawing.Point(84, 18);
		this.tbprsScaleFactor.Name = "tbprsScaleFactor";
		this.tbprsScaleFactor.Size = new System.Drawing.Size(70, 21);
		this.tbprsScaleFactor.TabIndex = 0;
		this.tbcuDilution.Location = new System.Drawing.Point(228, 16);
		this.tbcuDilution.Name = "tbcuDilution";
		this.tbcuDilution.Size = new System.Drawing.Size(76, 21);
		this.tbcuDilution.TabIndex = 0;
		this.lbcuInjVolume.AutoSize = true;
		this.lbcuInjVolume.Location = new System.Drawing.Point(4, 15);
		this.lbcuInjVolume.Name = "lbcuInjVolume";
		this.lbcuInjVolume.Size = new System.Drawing.Size(41, 12);
		this.lbcuInjVolume.TabIndex = 1;
		this.lbcuInjVolume.Text = "样品量";
		this.tbcuInjVolume.Location = new System.Drawing.Point(78, 11);
		this.tbcuInjVolume.Name = "tbcuInjVolume";
		this.tbcuInjVolume.Size = new System.Drawing.Size(76, 21);
		this.tbcuInjVolume.TabIndex = 0;
		this.groupBox2.Controls.Add(this.cbprsUncalBase);
		this.groupBox2.Controls.Add(this.lbcuUncalAmtRespF);
		this.groupBox2.Controls.Add(this.radioButton1);
		this.groupBox2.Controls.Add(this.tbprsUncalAmtRespF);
		this.groupBox2.Controls.Add(this.TipLable);
		this.groupBox2.Controls.Add(this.lclRbInner);
		this.groupBox2.Controls.Add(this.lclRbOuter);
		this.groupBox2.Controls.Add(this.lclRbNo);
		this.groupBox2.Location = new System.Drawing.Point(7, 426);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(313, 68);
		this.groupBox2.TabIndex = 7;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "方法选项";
		this.groupBox2.Visible = false;
		this.cbprsUncalBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbprsUncalBase.FormattingEnabled = true;
		this.cbprsUncalBase.ItemExtString = "";
		this.cbprsUncalBase.Items.AddRange(new object[2] { "峰面积", "峰高" });
		this.cbprsUncalBase.Location = new System.Drawing.Point(225, 43);
		this.cbprsUncalBase.Name = "cbprsUncalBase";
		this.cbprsUncalBase.Size = new System.Drawing.Size(82, 20);
		this.cbprsUncalBase.TabIndex = 2;
		this.cbprsUncalBase.Visible = false;
		this.lbcuUncalAmtRespF.AutoSize = true;
		this.lbcuUncalAmtRespF.Location = new System.Drawing.Point(43, 46);
		this.lbcuUncalAmtRespF.Name = "lbcuUncalAmtRespF";
		this.lbcuUncalAmtRespF.Size = new System.Drawing.Size(101, 12);
		this.lbcuUncalAmtRespF.TabIndex = 1;
		this.lbcuUncalAmtRespF.Text = "未识别峰缩放因子";
		this.lbcuUncalAmtRespF.Visible = false;
		this.radioButton1.AutoSize = true;
		this.radioButton1.Location = new System.Drawing.Point(78, 20);
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.Size = new System.Drawing.Size(71, 16);
		this.radioButton1.TabIndex = 3;
		this.radioButton1.Text = "校正归一";
		this.radioButton1.UseVisualStyleBackColor = true;
		this.radioButton1.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
		this.tbprsUncalAmtRespF.Location = new System.Drawing.Point(149, 43);
		this.tbprsUncalAmtRespF.Name = "tbprsUncalAmtRespF";
		this.tbprsUncalAmtRespF.Size = new System.Drawing.Size(70, 21);
		this.tbprsUncalAmtRespF.TabIndex = 0;
		this.tbprsUncalAmtRespF.Visible = false;
		this.TipLable.ForeColor = System.Drawing.Color.Red;
		this.TipLable.Location = new System.Drawing.Point(5, 39);
		this.TipLable.Name = "TipLable";
		this.TipLable.Size = new System.Drawing.Size(306, 26);
		this.TipLable.TabIndex = 2;
		this.lclRbInner.AutoSize = true;
		this.lclRbInner.Location = new System.Drawing.Point(246, 20);
		this.lclRbInner.Name = "lclRbInner";
		this.lclRbInner.Size = new System.Drawing.Size(47, 16);
		this.lclRbInner.TabIndex = 1;
		this.lclRbInner.Text = "内标";
		this.lclRbInner.UseVisualStyleBackColor = true;
		this.lclRbInner.CheckedChanged += new System.EventHandler(lclRbInner_CheckedChanged);
		this.lclRbOuter.AutoSize = true;
		this.lclRbOuter.Location = new System.Drawing.Point(174, 20);
		this.lclRbOuter.Name = "lclRbOuter";
		this.lclRbOuter.Size = new System.Drawing.Size(47, 16);
		this.lclRbOuter.TabIndex = 1;
		this.lclRbOuter.Text = "外标";
		this.lclRbOuter.UseVisualStyleBackColor = true;
		this.lclRbOuter.CheckedChanged += new System.EventHandler(lclRbOuter_CheckedChanged);
		this.lclRbNo.AutoSize = true;
		this.lclRbNo.Checked = true;
		this.lclRbNo.Location = new System.Drawing.Point(6, 20);
		this.lclRbNo.Name = "lclRbNo";
		this.lclRbNo.Size = new System.Drawing.Size(47, 16);
		this.lclRbNo.TabIndex = 1;
		this.lclRbNo.TabStop = true;
		this.lclRbNo.Text = "归一";
		this.lclRbNo.UseVisualStyleBackColor = true;
		this.lclRbNo.CheckedChanged += new System.EventHandler(lclRbNo_CheckedChanged);
		this.gbadvAddSub.Controls.Add(this.btnasNoneChrom);
		this.gbadvAddSub.Controls.Add(this.rbasSub);
		this.gbadvAddSub.Controls.Add(this.rbasAdd);
		this.gbadvAddSub.Controls.Add(this.cbasMatching);
		this.gbadvAddSub.Controls.Add(this.btnasSetChrom);
		this.gbadvAddSub.Controls.Add(this.tbasChrom);
		this.gbadvAddSub.Controls.Add(this.lbasMatching);
		this.gbadvAddSub.Controls.Add(this.lbasChrom);
		this.gbadvAddSub.Location = new System.Drawing.Point(9, 330);
		this.gbadvAddSub.Name = "gbadvAddSub";
		this.gbadvAddSub.Size = new System.Drawing.Size(314, 94);
		this.gbadvAddSub.TabIndex = 5;
		this.gbadvAddSub.TabStop = false;
		this.gbadvAddSub.Text = "加减谱图";
		this.btnasNoneChrom.Location = new System.Drawing.Point(238, 61);
		this.btnasNoneChrom.Name = "btnasNoneChrom";
		this.btnasNoneChrom.Size = new System.Drawing.Size(75, 23);
		this.btnasNoneChrom.TabIndex = 3;
		this.btnasNoneChrom.Text = "置空";
		this.btnasNoneChrom.UseVisualStyleBackColor = true;
		this.btnasNoneChrom.Click += new System.EventHandler(btnasNoneChrom_Click);
		this.rbasSub.AutoSize = true;
		this.rbasSub.Location = new System.Drawing.Point(138, 17);
		this.rbasSub.Name = "rbasSub";
		this.rbasSub.Size = new System.Drawing.Size(35, 16);
		this.rbasSub.TabIndex = 5;
		this.rbasSub.TabStop = true;
		this.rbasSub.Text = "减";
		this.rbasSub.UseVisualStyleBackColor = true;
		this.rbasAdd.AutoSize = true;
		this.rbasAdd.Location = new System.Drawing.Point(70, 17);
		this.rbasAdd.Name = "rbasAdd";
		this.rbasAdd.Size = new System.Drawing.Size(35, 16);
		this.rbasAdd.TabIndex = 5;
		this.rbasAdd.TabStop = true;
		this.rbasAdd.Text = "加";
		this.rbasAdd.UseVisualStyleBackColor = true;
		this.cbasMatching.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbasMatching.FormattingEnabled = true;
		this.cbasMatching.ItemExtString = "";
		this.cbasMatching.Items.AddRange(new object[3] { "无变化", "偏移谱图", "缩放谱图" });
		this.cbasMatching.Location = new System.Drawing.Point(70, 63);
		this.cbasMatching.Name = "cbasMatching";
		this.cbasMatching.Size = new System.Drawing.Size(162, 20);
		this.cbasMatching.TabIndex = 4;
		this.btnasSetChrom.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.btnasSetChrom.Location = new System.Drawing.Point(239, 36);
		this.btnasSetChrom.Name = "btnasSetChrom";
		this.btnasSetChrom.Size = new System.Drawing.Size(27, 23);
		this.btnasSetChrom.TabIndex = 29;
		this.btnasSetChrom.UseVisualStyleBackColor = true;
		this.btnasSetChrom.Click += new System.EventHandler(btnasSetChrom_Click);
		this.tbasChrom.Location = new System.Drawing.Point(70, 38);
		this.tbasChrom.Name = "tbasChrom";
		this.tbasChrom.ReadOnly = true;
		this.tbasChrom.Size = new System.Drawing.Size(162, 21);
		this.tbasChrom.TabIndex = 1;
		this.lbasMatching.AutoSize = true;
		this.lbasMatching.Location = new System.Drawing.Point(6, 67);
		this.lbasMatching.Name = "lbasMatching";
		this.lbasMatching.Size = new System.Drawing.Size(53, 12);
		this.lbasMatching.TabIndex = 1;
		this.lbasMatching.Text = "匹配方式";
		this.lbasChrom.AutoSize = true;
		this.lbasChrom.Location = new System.Drawing.Point(6, 42);
		this.lbasChrom.Name = "lbasChrom";
		this.lbasChrom.Size = new System.Drawing.Size(29, 12);
		this.lbasChrom.TabIndex = 1;
		this.lbasChrom.Text = "谱图";
		this.gbadvColumnCalcu.Controls.Add(this.rbccFrom50per);
		this.gbadvColumnCalcu.Controls.Add(this.rbccStatistical);
		this.gbadvColumnCalcu.Controls.Add(this.tbccColumnLength);
		this.gbadvColumnCalcu.Controls.Add(this.lbccColumnLengthU);
		this.gbadvColumnCalcu.Controls.Add(this.tbccUnretainedPeak);
		this.gbadvColumnCalcu.Controls.Add(this.lbccColumnLength);
		this.gbadvColumnCalcu.Controls.Add(this.lbccUnretainedPeakU);
		this.gbadvColumnCalcu.Controls.Add(this.lbccUnretainedPeak);
		this.gbadvColumnCalcu.Location = new System.Drawing.Point(131, 80);
		this.gbadvColumnCalcu.Name = "gbadvColumnCalcu";
		this.gbadvColumnCalcu.Size = new System.Drawing.Size(195, 92);
		this.gbadvColumnCalcu.TabIndex = 6;
		this.gbadvColumnCalcu.TabStop = false;
		this.gbadvColumnCalcu.Text = "柱效计算";
		this.rbccFrom50per.AutoSize = true;
		this.rbccFrom50per.Checked = true;
		this.rbccFrom50per.Location = new System.Drawing.Point(96, 68);
		this.rbccFrom50per.Name = "rbccFrom50per";
		this.rbccFrom50per.Size = new System.Drawing.Size(77, 16);
		this.rbccFrom50per.TabIndex = 1;
		this.rbccFrom50per.TabStop = true;
		this.rbccFrom50per.Text = "50%宽起始";
		this.rbccFrom50per.UseVisualStyleBackColor = true;
		this.rbccStatistical.AutoSize = true;
		this.rbccStatistical.Enabled = false;
		this.rbccStatistical.Location = new System.Drawing.Point(7, 68);
		this.rbccStatistical.Name = "rbccStatistical";
		this.rbccStatistical.Size = new System.Drawing.Size(71, 16);
		this.rbccStatistical.TabIndex = 1;
		this.rbccStatistical.Text = "静态时间";
		this.rbccStatistical.UseVisualStyleBackColor = true;
		this.tbccColumnLength.Location = new System.Drawing.Point(79, 43);
		this.tbccColumnLength.Name = "tbccColumnLength";
		this.tbccColumnLength.Size = new System.Drawing.Size(59, 21);
		this.tbccColumnLength.TabIndex = 1;
		this.lbccColumnLengthU.AutoSize = true;
		this.lbccColumnLengthU.Location = new System.Drawing.Point(139, 47);
		this.lbccColumnLengthU.Name = "lbccColumnLengthU";
		this.lbccColumnLengthU.Size = new System.Drawing.Size(29, 12);
		this.lbccColumnLengthU.TabIndex = 1;
		this.lbccColumnLengthU.Text = "[mm]";
		this.tbccUnretainedPeak.Location = new System.Drawing.Point(80, 19);
		this.tbccUnretainedPeak.Name = "tbccUnretainedPeak";
		this.tbccUnretainedPeak.Size = new System.Drawing.Size(59, 21);
		this.tbccUnretainedPeak.TabIndex = 1;
		this.lbccColumnLength.AutoSize = true;
		this.lbccColumnLength.Location = new System.Drawing.Point(6, 46);
		this.lbccColumnLength.Name = "lbccColumnLength";
		this.lbccColumnLength.Size = new System.Drawing.Size(29, 12);
		this.lbccColumnLength.TabIndex = 1;
		this.lbccColumnLength.Text = "柱长";
		this.lbccUnretainedPeakU.AutoSize = true;
		this.lbccUnretainedPeakU.Location = new System.Drawing.Point(138, 23);
		this.lbccUnretainedPeakU.Name = "lbccUnretainedPeakU";
		this.lbccUnretainedPeakU.Size = new System.Drawing.Size(35, 12);
		this.lbccUnretainedPeakU.TabIndex = 1;
		this.lbccUnretainedPeakU.Text = "[min]";
		this.lbccUnretainedPeak.AutoSize = true;
		this.lbccUnretainedPeak.Location = new System.Drawing.Point(3, 22);
		this.lbccUnretainedPeak.Name = "lbccUnretainedPeak";
		this.lbccUnretainedPeak.Size = new System.Drawing.Size(77, 12);
		this.lbccUnretainedPeak.TabIndex = 1;
		this.lbccUnretainedPeak.Text = "非保留峰时间";
		this.gvCmpds.AllowUserToAddRows = false;
		this.gvCmpds.AllowUserToDeleteRows = false;
		this.gvCmpds.AllowUserToResizeRows = false;
		this.gvCmpds.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvCmpds.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.gvCmpds.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvCmpds.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
		this.gvCmpds.ColumnHeadersHeight = 50;
		this.gvCmpds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvCmpds.DefaultCellStyle = dataGridViewCellStyle16;
		this.gvCmpds.Dock = System.Windows.Forms.DockStyle.Top;
		this.gvCmpds.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvCmpds.Location = new System.Drawing.Point(0, 0);
		this.gvCmpds.Name = "gvCmpds";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvCmpds.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
		this.gvCmpds.RowHeadersWidth = 25;
		this.gvCmpds.RowTemplate.Height = 40;
		this.gvCmpds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvCmpds.ShowCellToolTips = false;
		this.gvCmpds.Size = new System.Drawing.Size(775, 153);
		this.gvCmpds.TabIndex = 13;
		this.gvCmpds.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvCmpds_CellBeginEdit);
		this.gvCmpds.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpds_CellEndEdit);
		this.gvCmpds.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpds_CellValueChanged);
		this.dpgnlChrom.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgnlChrom.Cursor = System.Windows.Forms.Cursors.Default;
		this.dpgnlChrom.Dock = System.Windows.Forms.DockStyle.Top;
		this.dpgnlChrom.Location = new System.Drawing.Point(0, 0);
		this.dpgnlChrom.Name = "dpgnlChrom";
		this.dpgnlChrom.Size = new System.Drawing.Size(775, 199);
		this.dpgnlChrom.TabIndex = 11;
		this.dpgnlChrom.Paint += new System.Windows.Forms.PaintEventHandler(dpgnlChrom_Paint);
		this.gvInteg.AllowUserToAddRows = false;
		this.gvInteg.AllowUserToDeleteRows = false;
		this.gvInteg.AllowUserToResizeRows = false;
		this.gvInteg.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvInteg.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvInteg.ColumnHeadersHeight = 50;
		this.gvInteg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvInteg.ContextMenuStrip = this.cmsIntegration;
		this.gvInteg.Dock = System.Windows.Forms.DockStyle.Left;
		this.gvInteg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvInteg.Location = new System.Drawing.Point(3, 3);
		this.gvInteg.Name = "gvInteg";
		this.gvInteg.RowHeadersWidth = 25;
		this.gvInteg.RowTemplate.Height = 40;
		this.gvInteg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvInteg.ShowCellToolTips = false;
		this.gvInteg.Size = new System.Drawing.Size(365, 606);
		this.gvInteg.TabIndex = 2;
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle18.Format = "0.0";
		this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle18;
		this.dataGridViewTextBoxColumn7.HeaderText = "设定[℃]";
		this.dataGridViewTextBoxColumn7.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.ReadOnly = true;
		this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn7.Width = 60;
		dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Blue;
		this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle19;
		this.dataGridViewTextBoxColumn9.HeaderText = "阶号";
		this.dataGridViewTextBoxColumn9.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn9.MinimumWidth = 27;
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		this.dataGridViewTextBoxColumn9.ReadOnly = true;
		this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn9.Width = 36;
		dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle20.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Blue;
		this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle20;
		this.dataGridViewTextBoxColumn10.HeaderText = "事件2 [min]";
		this.dataGridViewTextBoxColumn10.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
		this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn10.Width = 66;
		dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle21.Font = new System.Drawing.Font("宋体", 9f);
		dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Blue;
		this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle21;
		this.dataGridViewTextBoxColumn11.HeaderText = "事件3 [min]";
		this.dataGridViewTextBoxColumn11.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
		this.dataGridViewTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn11.Width = 66;
		dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle22.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle22.ForeColor = System.Drawing.Color.Blue;
		this.事件.DefaultCellStyle = dataGridViewCellStyle22;
		this.事件.HeaderText = "事件1 [min]";
		this.事件.Name = "事件";
		this.事件.ReadOnly = true;
		this.事件.Width = 66;
		dataGridViewCellStyle23.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle23.Font = new System.Drawing.Font("宋体", 9f);
		dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Blue;
		this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle23;
		this.dataGridViewTextBoxColumn12.HeaderText = "事件4 [min]";
		this.dataGridViewTextBoxColumn12.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
		this.dataGridViewTextBoxColumn12.ReadOnly = true;
		this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn12.Width = 66;
		dataGridViewCellStyle24.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle24.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle24.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle24;
		this.dataGridViewTextBoxColumn17.HeaderText = "保持时间";
		this.dataGridViewTextBoxColumn17.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
		this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn17.Width = 80;
		dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle25.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle25.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn18.DefaultCellStyle = dataGridViewCellStyle25;
		this.dataGridViewTextBoxColumn18.HeaderText = "起始瓶号";
		this.dataGridViewTextBoxColumn18.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn18.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
		this.dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn18.Width = 70;
		dataGridViewCellStyle26.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle26.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle26.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn19.DefaultCellStyle = dataGridViewCellStyle26;
		this.dataGridViewTextBoxColumn19.HeaderText = "终止瓶号";
		this.dataGridViewTextBoxColumn19.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn19.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
		this.dataGridViewTextBoxColumn19.ReadOnly = true;
		this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn19.Width = 70;
		dataGridViewCellStyle27.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle27.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle27;
		this.dataGridViewTextBoxColumn20.HeaderText = "进样量   [uL]";
		this.dataGridViewTextBoxColumn20.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
		this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn20.Width = 60;
		dataGridViewCellStyle28.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle28.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle28.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle28;
		this.dataGridViewTextBoxColumn21.HeaderText = "次/瓶";
		this.dataGridViewTextBoxColumn21.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
		this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn21.Width = 60;
		dataGridViewCellStyle29.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle29.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn22.DefaultCellStyle = dataGridViewCellStyle29;
		this.dataGridViewTextBoxColumn22.HeaderText = "间隔    [min]";
		this.dataGridViewTextBoxColumn22.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
		this.dataGridViewTextBoxColumn22.Width = 60;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tabControl3);
		base.Controls.Add(this.btnMutiPoint);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.bUseSet);
		base.Controls.Add(this.gbMethods);
		base.Controls.Add(this.MethodSave);
		base.Controls.Add(this.MethodReSave);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.MethodNew);
		base.Controls.Add(this.MethodOpen);
		base.Controls.Add(this.tbMethName);
		base.Name = "MstSetPortable";
		base.Size = new System.Drawing.Size(783, 548);
		base.Load += new System.EventHandler(MstSet_Load);
		this.gbMethods.ResumeLayout(false);
		this.gbMethods.PerformLayout();
		this.tabControl3.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvExtEvTP).EndInit();
		this.tabPage2.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		this.panel12.PerformLayout();
		this.panelJY.ResumeLayout(false);
		this.panel15.ResumeLayout(false);
		this.panel15.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgInsamp1).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		this.tabControl2.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).EndInit();
		this.tabPage4.ResumeLayout(false);
		this.tabPage5.ResumeLayout(false);
		this.tabPage11.ResumeLayout(false);
		this.tabPage11.PerformLayout();
		this.tabPage12.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.chartPlots).EndInit();
		this.tabPage17.ResumeLayout(false);
		this.cmsIntegration.ResumeLayout(false);
		this.spcIntegComponent.Panel1.ResumeLayout(false);
		this.spcIntegComponent.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.spcIntegComponent).EndInit();
		this.spcIntegComponent.ResumeLayout(false);
		this.tabPage19.ResumeLayout(false);
		this.tabPage19.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.groupBox11.ResumeLayout(false);
		this.groupBox11.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
		this.groupBox17.ResumeLayout(false);
		this.groupBox17.PerformLayout();
		this.groupBox15.ResumeLayout(false);
		this.groupBox15.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		this.cmsCali.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvgcProgTemp).EndInit();
		this.pnlcu.ResumeLayout(false);
		this.spcComponent.Panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.spcComponent).EndInit();
		this.spcComponent.ResumeLayout(false);
		this.gbO.ResumeLayout(false);
		this.gbO.PerformLayout();
		this.gbCalibration.ResumeLayout(false);
		this.gbCalibration.PerformLayout();
		this.gbcuRltTableReport.ResumeLayout(false);
		this.gbcuRltTableReport.PerformLayout();
		this.lclGroupBox1.ResumeLayout(false);
		this.lclGroupBox1.PerformLayout();
		this.gbcuScale.ResumeLayout(false);
		this.gbcuScale.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.gbadvAddSub.ResumeLayout(false);
		this.gbadvAddSub.PerformLayout();
		this.gbadvColumnCalcu.ResumeLayout(false);
		this.gbadvColumnCalcu.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvCmpds).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvInteg).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
