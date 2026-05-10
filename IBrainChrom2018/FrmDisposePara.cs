using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IBrainChrom2018.Properties;

namespace IBrainChrom2018;

public class FrmDisposePara : LclForm
{
	public bool bLoading = true;

	private ChromFormInterface formMain_0;

	private TcpServerSocket tcpServerSocket_0;

	private TcpServerSocket tcpServerSocket_1;

	private Chromatogram chromatogram_0;

	private GcProgTemp gcProgTemp_0 = new GcProgTemp();

	private MtdSetup mtdSetup_0 = new MtdSetup();

	private GradientDisplay gradientDisplay_0;

	private OpenFileDialog openFileDialog_0;

	private OpenFileDialog openFileDialog_1;

	private SaveFileDialog saveFileDialog_0;

	private OpenFileDialog openFileDialog_2;

	private IContainer icontainer_1;

	private TableLayoutPanel tableLayoutPanel1;

	private Panel panel1;

	private Panel panel2;

	private Button button1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private ImageList imageList_0;

	private TextBox textBox1;

	private Label label1;

	private Button button3;

	public LclTabControl tcMethod;

	public TabPage tpTempProg;

	private LclDisplayPanel dpgcProgTemp;

	public TabPage tpCaculation;

	private LclCusComboBox cbcclCalcu;

	private LclGroupBox gbcclRltTableReport;

	private LclRadioButton rbrtrCaliPeaks;

	private LclRadioButton rbrtrIdentifiedPeaks;

	private LclRadioButton rbrtrAllDetectedPeaks;

	private LclCheckBox cbrtrHideISTDPeak;

	private LclGroupBox gbcclParas;

	private LclCusComboBox cbprsUncalBase;

	private LclCheckBox cbprsUseScaleFactor;

	private LclLabel lbprsUncalBase;

	public LclLabel lbprsUncalAmtRespFU;

	private LclLabel lbprsUncalAmtRespF;

	private LclLabel lbprsUnitAfterScale;

	private LclLabel lbprsScaleFactor;

	private LclTextBox tbprsUncalAmtRespF;

	private LclTextBox tbprsUnitAfterScale;

	private LclTextBox tbprsScaleFactor;

	private LclButton btncclSet;

	private LclButton btncclNone;

	private LclButton btncclView;

	private LclTextBox tbcclCalibration;

	private LclLabel lbcclModifiedTimeV;

	private LclLabel lbcclModifiedTime;

	private LclLabel lbcclCreateTimeV;

	private LclLabel lbcclCreateTime;

	private LclLabel lbcclDescriptionV;

	private LclLabel lbcclDescription;

	private LclLabel lbcclAuthorV;

	private LclLabel lbcclAuthor;

	private LclLabel lbcclCalcu;

	private LclLabel lbcclCalibration;

	private TabPage tpAdvanced;

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

	private LclButton btnasSetChrom;

	private LclButton btnasNoneChrom;

	private LclTextBox tbasChrom;

	private LclLabel lbasMatching;

	private LclLabel lbasChrom;

	public TabPage tpIntegration;

	public LclIntegGridView gvInteg;

	private TabPage tabPage2;

	private GroupBox groupBox2;

	private TextBox rptbotom;

	private GroupBox groupBox1;

	private TextBox rpthead;

	private Button button2;

	private Button button5;

	private ContextMenuStrip cmsIntegration;

	private ToolStripMenuItem miIntegAppendRow;

	private ToolStripMenuItem miIntegInsertRow;

	private ToolStripMenuItem miIntegDeleteRows;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem miIntegRowsUp;

	private ToolStripMenuItem miIntegRowsDown;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem miIntegResetRows;

	private Button button4;

	private TabPage tabPage1;

	private LclTextBox lclTextBox1;

	private Label label4;

	private Label label3;

	private Label label2;

	private ComboBox comboBox1;

	private ComboBox comboBox2;

	private LclTextBox lclTextBox2;

	private Label label7;

	private Label label6;

	private Label label5;

	private GroupBox groupBox4;

	private GroupBox groupBox3;

	private ComboBox comboBox8;

	private ComboBox comboBox7;

	private ComboBox comboBox6;

	private ComboBox comboBox5;

	private Label label8;

	private ComboBox comboBox4;

	private Label label9;

	private ComboBox comboBox3;

	private Label label11;

	private Label label12;

	private Label label14;

	private LclTextBox lclTextBox8;

	private Label label15;

	private LclTextBox lclTextBox7;

	private Label label17;

	private LclTextBox lclTextBox6;

	private Label label18;

	private LclTextBox lclTextBox5;

	private Label label20;

	private LclTextBox lclTextBox4;

	private Label label21;

	private LclTextBox lclTextBox3;

	private Label label23;

	private Label label24;

	private Label label25;

	private Label label22;

	private Label label10;

	private Label label19;

	private Label label13;

	private Label label16;

	private Label label26;

	private Label label27;

	private Label label28;

	private Label label29;

	private Label label30;

	private LclTextBox lclTextBox9;

	private LclTextBox lclTextBox10;

	private Label label31;

	private LclTextBox lclTextBox11;

	private LclTextBox lclTextBox12;

	private Label label32;

	private LclTextBox lclTextBox13;

	private LclTextBox lclTextBox14;

	private Label label33;

	private LclTextBox lclTextBox15;

	private LclTextBox lclTextBox16;

	private Label label34;

	private Label label35;

	private Label label36;

	private Label label37;

	private Label label38;

	private Label label39;

	private Label label40;

	private Label label41;

	private Label label42;

	private TabPage tabPage3;

	private CheckBox checkBox8;

	private LclButton lclButton2;

	private LclTextBox lclTextBox17;

	private LclLabel lclLabel1;

	private Button button6;

	private GroupBox groupBox5;

	private Label label46;

	private Label label47;

	private Label label48;

	private Label label49;

	private Label label50;

	private CheckBox FileNameAutoInject;

	private CheckBox FileNameDateTime;

	private CheckBox FileNameChannelName;

	private CheckBox FileNameAquipName;

	private CheckBox checkBox1;

	private LclTextBox lclTextBox20;

	private LclTextBox lclTextBox19;

	private LclTextBox lclTextBox18;

	private Label label45;

	private Label label44;

	private Label label43;

	private GroupBox groupBox6;

	private GroupBox BaselineRemove;

	private Label label51;

	private CheckBox InjectIndex;

	private ComboBox FileUserSet;

	private IContainer components;

	private Button button7;

	public FrmDisposePara()
	{
		InitializeComponent();
	}

	private void FrmDisposePara_Load(object sender, EventArgs e)
	{
		checkBox8.CheckedChanged += checkBox8_CheckedChanged;
		if (mtdSetup_0.sigIntegrations.Count == 0)
		{
			IArrayBase.NewArray(ref mtdSetup_0.sigIntegrations, 1);
			mtdSetup_0.sigIntegrations[0] = new Integration();
			mtdSetup_0.sigIntegrations[0].Reset();
		}
		gvInteg.BorderStyle = BorderStyle.None;
		gvInteg.InitColumns();
		gvInteg.LoadLanguage();
		gvInteg.SetimgContextButton((Bitmap)imageList_0.Images[0]);
		gvInteg.SetimgUnContextButton((Bitmap)imageList_0.Images[1]);
		gvInteg.Refresh(AccStyle.Read, mtdSetup_0.sigIntegrations[0]);
		if (mtdSetup_0.chromInfoR.GcProgTemp == null)
		{
			mtdSetup_0.chromInfoR.GcProgTemp = new GcProgTemp();
		}
		gcProgTemp_0.LoadFromObject(mtdSetup_0.chromInfoR.GcProgTemp);
		gradientDisplay_0 = new GradientDisplay(WinStyle.Method, dpgcProgTemp);
		gradientDisplay_0.instruStyle = InstruStyle.GC;
		gradientDisplay_0.txtY = "Temp.";
		gradientDisplay_0.unitY = "℃";
		gradientDisplay_0.fmtY = "0.0";
		gradientDisplay_0.showProgTemp = true;
		refresh_dpgcProgTemp();
		cbcclCalcu.InitItems(new object[3]
		{
			CalcuStyle.Uncal,
			CalcuStyle.ESTD,
			CalcuStyle.ISTD
		});
		cbcclCalcu.InitShowText(new string[3]
		{
			Lang.PS("无校正", "Uncal"),
			Lang.PS("外标法", "ESTD"),
			Lang.PS("内标法", "ISTD")
		});
		cbprsUncalBase.InitItems(new object[4]
		{
			RespStyle.Area,
			RespStyle.Height,
			RespStyle.AreaSquare,
			RespStyle.PeakHeightSquare
		});
		cbprsUncalBase.InitShowText(new string[4]
		{
			Lang.PS("面积", "Area"),
			Lang.PS("高度", "Height"),
			Lang.PS("面积平方根", "AreaSquare"),
			Lang.PS("高度平方根", "PeakHeightSquare")
		});
		cbasMatching.InitItems(new object[3]
		{
			ASMatchStyle.NoChange,
			ASMatchStyle.OffsetChrom,
			ASMatchStyle.ScaleChrom
		});
		cbasMatching.InitShowText(new string[3]
		{
			Lang.PS("无变化", "No Change"),
			Lang.PS("偏移谱图", "Offset Chrom"),
			Lang.PS("缩放谱图", "Scale Chrom")
		});
		method_9(AccStyle.Read);
		try
		{
			method_3();
		}
		catch
		{
		}
		while (tcMethod.TabPages.Count > 1)
		{
			tcMethod.TabPages.RemoveAt(1);
		}
		method_2();
		method_0();
	}

	private void method_0()
	{
		FileUserSet.Items.Clear();
		string text = Class49.ReadConfigSection("SampleName");
		if (text != null)
		{
			string[] array = text.Split('$');
			for (int i = 0; i < array.Length; i++)
			{
				FileUserSet.Items.Add(array[i]);
			}
		}
	}

	private void method_1()
	{
		if (!(FileUserSet.Text.Trim() != ""))
		{
			return;
		}
		string text = Class49.ReadConfigSection("SampleName");
		if (text == null)
		{
			return;
		}
		string[] array = text.Split('$');
		bool flag = true;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == FileUserSet.Text.Trim())
			{
				flag = false;
			}
		}
		if (flag)
		{
			if (text == "")
			{
				Class49.Write2ConfigFile(FileUserSet.Text.Trim());
			}
			else
			{
				Class49.Write2ConfigFile(text + "$" + FileUserSet.Text.Trim());
			}
		}
	}

	private void method_2()
	{
		SysLanguage sysLanguage_ = Class49.sysLanguage_0;
		if (sysLanguage_ != SysLanguage.CN && sysLanguage_ == SysLanguage.EN)
		{
			InjectIndex.Text = "SampleNum";
			button7.Text = "DeleteCurbatch";
			Text = "Spectrum processing settings";
			tcMethod.TabPages[0].Text = "Time Program";
			groupBox3.Text = "Time Program";
			label2.Text = "TLen";
			label3.Text = "detector:";
			label5.Text = "TLen";
			label6.Text = "detector:";
			label8.Text = "TLen";
			label9.Text = "detector:";
			label11.Text = "TLen";
			label12.Text = "detector:";
			label14.Text = "TLen";
			label15.Text = "detector:";
			label17.Text = "TLen";
			label18.Text = "detector:";
			label20.Text = "TLen";
			label21.Text = "detector:";
			label23.Text = "TLen";
			label24.Text = "detector:";
			groupBox4.Text = "Flush function";
			label26.Text = "Start";
			label27.Text = "End:";
			label28.Text = "Start";
			label29.Text = "End:";
			label30.Text = "Start";
			label31.Text = "End:";
			label32.Text = "Start";
			label33.Text = "End:";
			label42.Text = "tip:Time time program and flush function press the small to large fill!";
			button1.Text = "UseSet";
			lclLabel1.Text = "BaselineFile";
			lclButton2.Text = "Clear";
			checkBox8.Text = "Baseline Subtract";
			groupBox5.Text = "File naming settings ";
			FileNameAquipName.Text = "Name/ID";
			FileNameChannelName.Text = "ChannelName";
			FileNameDateTime.Text = "Time";
			label47.Text = "UserCustom";
			FileNameAutoInject.Text = "AutoInject";
		}
	}

	private void method_3()
	{
		ChromDevice chromDevice = new ChromDevice();
		for (int i = 0; i < formMain_0.FrmEquip.SunAquips.Count; i++)
		{
			if (formMain_0.FrmEquip.SunAquips[i].info.ID == formMain_0.CurrentGCID)
			{
				chromDevice = formMain_0.FrmEquip.SunAquips[i];
			}
		}
		int selectedIndex = formMain_0.tabChannel.SelectedIndex;
		lclTextBox17.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].TemplatePath;
		checkBox8.Checked = chromDevice.misMgr.ChannelChartParaS[selectedIndex].bBaselineDeduction;
		lclTextBox1.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[0].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[0].TestCard > 0)
		{
			comboBox1.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[0].TestCard;
		}
		lclTextBox2.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[1].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[1].TestCard > 0)
		{
			comboBox2.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[1].TestCard;
		}
		lclTextBox3.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[2].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[2].TestCard > 0)
		{
			comboBox3.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[2].TestCard;
		}
		lclTextBox4.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[3].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[3].TestCard > 0)
		{
			comboBox4.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[3].TestCard;
		}
		lclTextBox5.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[4].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[4].TestCard > 0)
		{
			comboBox5.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[4].TestCard;
		}
		lclTextBox6.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[5].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[5].TestCard > 0)
		{
			comboBox6.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[5].TestCard;
		}
		lclTextBox7.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[6].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[6].TestCard > 0)
		{
			comboBox7.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[6].TestCard;
		}
		lclTextBox8.Text = chromDevice.misMgr.ChartParaOperaS[0].tProgram[7].TimeValue.ToString("0.00");
		if (chromDevice.misMgr.ChartParaOperaS[0].tProgram[7].TestCard > 0)
		{
			comboBox8.SelectedIndex = chromDevice.misMgr.ChartParaOperaS[0].tProgram[7].TestCard;
		}
		lclTextBox16.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[0].TimeStart.ToString("0.00");
		lclTextBox15.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[0].TimeEnd.ToString("0.00");
		lclTextBox14.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[1].TimeStart.ToString("0.00");
		lclTextBox13.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[1].TimeEnd.ToString("0.00");
		lclTextBox12.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[2].TimeStart.ToString("0.00");
		lclTextBox11.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[2].TimeEnd.ToString("0.00");
		lclTextBox10.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[3].TimeStart.ToString("0.00");
		lclTextBox9.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[3].TimeEnd.ToString("0.00");
		FileNameAquipName.Checked = chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameAquipName;
		FileNameAutoInject.Checked = chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameAutoInject;
		FileNameChannelName.Checked = chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameChannelName;
		FileNameDateTime.Checked = chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameDateTime;
		InjectIndex.Checked = chromDevice.misMgr.ChartParaOperaS[selectedIndex].InjectIndex;
		FileUserSet.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileUserSet;
		checkBox1.Checked = chromDevice.misMgr.ChartParaOperaS[selectedIndex].UseUserZeroTime;
		lclTextBox18.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].ZeroTime.ToString("0.00");
		lclTextBox19.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].ZeroTimeLeft.ToString("0.00");
		lclTextBox20.Text = chromDevice.misMgr.ChartParaOperaS[selectedIndex].ZeroTimeRight.ToString("0.00");
	}

	public void refresh_dpgcProgTemp()
	{
		if (gradientDisplay_0 != null)
		{
			gradientDisplay_0.PrepareInfo(gcProgTemp_0);
		}
		dpgcProgTemp.Refresh();
	}

	private void dpgcProgTemp_Paint(object sender, PaintEventArgs e)
	{
		gradientDisplay_0.Draw(e.Graphics, erase: true);
	}

	public void Init(ChromFormInterface Mf, int RemoveIndex)
	{
		if (RemoveIndex > 1)
		{
			tcMethod.TabPages.RemoveAt(0);
		}
		else
		{
			tcMethod.TabPages.RemoveAt(RemoveIndex);
		}
		if (RemoveIndex == 0)
		{
			BaselineRemove.Parent.Text = Text;
			groupBox5.Text = "";
			BaselineRemove.Visible = false;
			groupBox5.Dock = DockStyle.Fill;
		}
		if (RemoveIndex == 10)
		{
			BaselineRemove.Parent.Text = Text;
			BaselineRemove.Text = "";
			groupBox5.Visible = false;
			BaselineRemove.Dock = DockStyle.Fill;
		}
		formMain_0 = Mf;
		if (!(Mf.CurrentGCID == ""))
		{
			ChartParaOpera chartParaOpera = Mf.FrmEquip.GetOneEquip(Mf.CurrentGCID).misMgr.ChartParaOperaS[Mf.tabChannel.SelectedIndex];
			mtdSetup_0 = chartParaOpera.mtdMgr;
			if (mtdSetup_0 == null)
			{
				mtdSetup_0 = new MtdSetup();
			}
			if (mtdSetup_0.printPara == null)
			{
				mtdSetup_0.printPara = new PrintPara();
			}
			comboBox1.Items.Clear();
			comboBox1.Items.Add("");
			comboBox2.Items.Clear();
			comboBox2.Items.Add("");
			comboBox3.Items.Clear();
			comboBox3.Items.Add("");
			comboBox4.Items.Clear();
			comboBox4.Items.Add("");
			comboBox5.Items.Clear();
			comboBox5.Items.Add("");
			comboBox6.Items.Clear();
			comboBox6.Items.Add("");
			comboBox7.Items.Clear();
			comboBox7.Items.Add("");
			comboBox8.Items.Clear();
			comboBox8.Items.Add("");
			for (int i = 0; i < formMain_0.tabChannel.TabPages.Count; i++)
			{
				comboBox1.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox2.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox3.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox4.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox5.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox6.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox7.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
				comboBox8.Items.Add(formMain_0.tabChannel.TabPages[i].Text.Trim());
			}
			rpthead.Text = mtdSetup_0.printPara.PrintTitleTop;
			rptbotom.Text = mtdSetup_0.printPara.PrintTitleBotom;
			textBox1.Text = mtdSetup_0.strMtdShowName;
		}
	}

	private void cbprsUseScaleFactor_Click(object sender, EventArgs e)
	{
		LclTextBox lclTextBox = tbprsScaleFactor;
		bool enabled = (tbprsUnitAfterScale.Enabled = cbprsUseScaleFactor.Checked);
		lclTextBox.Enabled = enabled;
	}

	private void method_4(object sender, EventArgs e)
	{
	}

	public void FrmToDevicePara(ChromFormInterface Mf)
	{
		ChromDevice chromDevice = new ChromDevice();
		for (int i = 0; i < Mf.FrmEquip.SunAquips.Count; i++)
		{
			if (Mf.FrmEquip.SunAquips[i].info.ID == Mf.CurrentGCID)
			{
				chromDevice = Mf.FrmEquip.SunAquips[i];
				int selectedIndex = Mf.tabChannel.SelectedIndex;
				chromDevice.misMgr.ChannelChartParaS[selectedIndex].bBaselineDeduction = checkBox8.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].TemplatePath = lclTextBox17.Text.Trim();
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[0].TimeValue = Class49.String2Float(lclTextBox1.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[0].TestCard = Class49.Object2Int(comboBox1.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[1].TimeValue = Class49.String2Float(lclTextBox2.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[1].TestCard = Class49.Object2Int(comboBox2.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[2].TimeValue = Class49.String2Float(lclTextBox3.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[2].TestCard = Class49.Object2Int(comboBox3.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[3].TimeValue = Class49.String2Float(lclTextBox4.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[3].TestCard = Class49.Object2Int(comboBox4.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[4].TimeValue = Class49.String2Float(lclTextBox5.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[4].TestCard = Class49.Object2Int(comboBox5.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[5].TimeValue = Class49.String2Float(lclTextBox6.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[5].TestCard = Class49.Object2Int(comboBox6.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[6].TimeValue = Class49.String2Float(lclTextBox7.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[6].TestCard = Class49.Object2Int(comboBox7.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[7].TimeValue = Class49.String2Float(lclTextBox8.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[0].tProgram[7].TestCard = Class49.Object2Int(comboBox8.SelectedIndex, -1);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[0].TimeStart = Class49.String2Float(lclTextBox16.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[0].TimeEnd = Class49.String2Float(lclTextBox15.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[1].TimeStart = Class49.String2Float(lclTextBox14.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[1].TimeEnd = Class49.String2Float(lclTextBox13.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[2].TimeStart = Class49.String2Float(lclTextBox12.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[2].TimeEnd = Class49.String2Float(lclTextBox11.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[3].TimeStart = Class49.String2Float(lclTextBox10.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].evenPara[3].TimeEnd = Class49.String2Float(lclTextBox9.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameAquipName = FileNameAquipName.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameAutoInject = FileNameAutoInject.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameChannelName = FileNameChannelName.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileNameDateTime = FileNameDateTime.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].InjectIndex = InjectIndex.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].FileUserSet = FileUserSet.Text.Trim();
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].UseUserZeroTime = checkBox1.Checked;
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].ZeroTime = Class49.String2Float(lclTextBox18.Text.Trim(), 0f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].ZeroTimeLeft = Class49.String2Float(lclTextBox19.Text.Trim(), 0.1f);
				chromDevice.misMgr.ChartParaOperaS[selectedIndex].ZeroTimeRight = Class49.String2Float(lclTextBox20.Text.Trim(), 0.1f);
				Mf.FrmEquip.SunAquips[i] = chromDevice;
				break;
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		string text = FileUserSet.Text.Trim();
		string text2 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
		string text3 = text2;
		for (int i = 0; i < text3.Length; i++)
		{
			text = text.Replace(text3[i].ToString(), "");
		}
		if (FileUserSet.Text.Trim().CompareTo(text) != 0)
		{
			MessageBox.Show("自定义名称出现非法字符,已为你替换。");
		}
		FileUserSet.Text = text;
		method_9(AccStyle.Write);
		FrmToDevicePara(formMain_0);
		method_1();
		Close();
	}

	private void method_5(object sender, EventArgs e)
	{
		Close();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0 == null)
		{
			openFileDialog_0 = new OpenFileDialog();
			openFileDialog_0.Title = "打开方法";
			openFileDialog_0.Filter = Class49.MakeFileFilter(".mtd");
		}
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			LoadMethodFile(openFileDialog_0.FileName);
		}
	}

	public void LoadMethodFile(string fileName)
	{
		if (File.Exists(fileName))
		{
			mtdSetup_0.LoadFromFile(fileName);
			textBox1.Text = mtdSetup_0.strMtdShowName;
			method_9(AccStyle.Read);
		}
	}

	public void Template(string TPath)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(TPath, DetectorStyle.General);
		if (chromatogram != null)
		{
			chromatogram_0 = chromatogram;
		}
		if (chromatogram != null)
		{
			mtdSetup_0.sigIntegrations[0] = chromatogram_0.integ;
			gvInteg.Refresh(AccStyle.Read, mtdSetup_0.sigIntegrations[0]);
		}
	}

	private void btncclView_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdSetup_0.chromInfo;
		if (sender == btncclSet)
		{
			if (openFileDialog_1 == null)
			{
				openFileDialog_1 = new OpenFileDialog();
				openFileDialog_1.Title = btncclSet.Text;
			}
			openFileDialog_1.InitialDirectory = chromInfo.cclDirectory;
			openFileDialog_1.Filter = CaliGnlUserCtrl.Filter;
			if (openFileDialog_1.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.cclCalibration = openFileDialog_1.FileName;
			chromInfo.LoadFromFile();
			tbcclCalibration.Text = chromInfo.cclShowName;
		}
		if (sender == btncclNone)
		{
			tbcclCalibration.Text = (chromInfo.cclCalibration = (chromInfo.cclDirectory = (chromInfo.cclShowName = "")));
			chromInfo.LoadFromFile();
		}
		if (sender == btncclView)
		{
			string cclCalibration = chromInfo.cclCalibration;
			if (cclCalibration != "")
			{
				if (!File.Exists(cclCalibration))
				{
					MessageBox.Show("file error");
				}
				else
				{
					CaliGnlForm.LoadCalFileShowForm(cclCalibration);
				}
			}
		}
		else
		{
			method_6();
		}
	}

	private void method_6()
	{
		lbcclAuthorV.Text = mtdSetup_0.chromInfo.cclAuthor;
		lbcclDescriptionV.Text = mtdSetup_0.chromInfo.cclDescription;
		lbcclCreateTimeV.Text = mtdSetup_0.chromInfo.cclCreateTime.ToShortDateString();
		lbcclModifiedTimeV.Text = mtdSetup_0.chromInfo.cclModifiedTime.ToShortDateString();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		mtdSetup_0 = new MtdSetup();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			textBox1.Text = "默认";
			break;
		case SysLanguage.EN:
			textBox1.Text = "Default Method";
			break;
		}
	}

	private void button4_Click(object sender, EventArgs e)
	{
		if (mtdSetup_0.strMtdFilePath == "")
		{
			button5_Click(null, null);
			return;
		}
		method_9(AccStyle.Write);
		FrmToDevicePara(formMain_0);
		mtdSetup_0.SaveToFile(mtdSetup_0.strMtdFilePath);
	}

	private void button5_Click(object sender, EventArgs e)
	{
		if (mtdSetup_0.SaveToFileAs())
		{
			textBox1.Text = mtdSetup_0.strMtdShowName;
		}
	}

	private void method_7(AccStyle accStyle_0, GcProgTemp gcProgTemp_1)
	{
	}

	private void method_8(AccStyle accStyle_0)
	{
		if (mtdSetup_0.sigIntegrations.Count > 0)
		{
			gvInteg.Refresh(accStyle_0, mtdSetup_0.sigIntegrations[0]);
		}
	}

	private void method_9(AccStyle accStyle_0)
	{
		gvInteg.EndEdit();
		method_8(accStyle_0);
		switch (accStyle_0)
		{
		case AccStyle.Write:
			method_7(accStyle_0, mtdSetup_0.chromInfoR.GcProgTemp);
			mtdSetup_0.chromInfo.cclCalcu = (CalcuStyle)cbcclCalcu.SelectedIndex;
			mtdSetup_0.chromInfo.rtrHideISTDPeak = cbrtrHideISTDPeak.Checked;
			if (rbrtrAllDetectedPeaks.Checked)
			{
				mtdSetup_0.chromInfo.rtrRltReportPeaks = RltReportPeaks.AllDetectedPeaks;
			}
			else if (rbrtrIdentifiedPeaks.Checked)
			{
				mtdSetup_0.chromInfo.rtrRltReportPeaks = RltReportPeaks.IdentifiedPeaks;
			}
			else if (rbrtrCaliPeaks.Checked)
			{
				mtdSetup_0.chromInfo.rtrRltReportPeaks = RltReportPeaks.CaliPeaks;
			}
			mtdSetup_0.chromInfo.prsUseScaleFactor = cbprsUseScaleFactor.Checked;
			mtdSetup_0.chromInfo.prsScaleFactor = Class49.String2Float(tbprsScaleFactor.Text, mtdSetup_0.chromInfo.prsScaleFactor);
			mtdSetup_0.chromInfo.prsUnitAfterScale = tbprsUnitAfterScale.Text;
			mtdSetup_0.chromInfo.prsUncalBase = (RespStyle)cbprsUncalBase.SelectedIndex;
			mtdSetup_0.chromInfo.prsUncalAmtRespF = Class49.String2Float(tbprsUncalAmtRespF.Text, mtdSetup_0.chromInfo.prsUncalAmtRespF);
			mtdSetup_0.chromInfo.addChrom = rbasAdd.Checked;
			mtdSetup_0.chromInfo.asMatching = (ASMatchStyle)cbasMatching.SelectedIndex;
			mtdSetup_0.chromInfo.ccColumnUT = Class49.String2Float(tbccUnretainedPeak.Text, mtdSetup_0.chromInfo.ccColumnUT);
			mtdSetup_0.chromInfo.ccColumnLength = Class49.String2Float(tbccColumnLength.Text, mtdSetup_0.chromInfo.ccColumnLength);
			if (rbccStatistical.Checked)
			{
				mtdSetup_0.chromInfo.ccStyle = ColumnCalcuStyle.Statistical;
			}
			else if (rbccFrom50per.Checked)
			{
				mtdSetup_0.chromInfo.ccStyle = ColumnCalcuStyle.From50per;
			}
			break;
		case AccStyle.Read:
			method_7(accStyle_0, mtdSetup_0.chromInfoR.GcProgTemp);
			gcProgTemp_0.LoadFromObject(mtdSetup_0.chromInfoR.GcProgTemp);
			refresh_dpgcProgTemp();
			tbcclCalibration.Text = mtdSetup_0.chromInfo.cclShowName;
			cbcclCalcu.SelectedIndex = (int)mtdSetup_0.chromInfo.cclCalcu;
			method_6();
			cbrtrHideISTDPeak.Checked = mtdSetup_0.chromInfo.rtrHideISTDPeak;
			switch (mtdSetup_0.chromInfo.rtrRltReportPeaks)
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
			cbprsUseScaleFactor.Checked = mtdSetup_0.chromInfo.prsUseScaleFactor;
			tbprsScaleFactor.Text = mtdSetup_0.chromInfo.prsScaleFactor.ToString();
			tbprsUnitAfterScale.Text = mtdSetup_0.chromInfo.prsUnitAfterScale;
			cbprsUncalBase.SelectedIndex = (int)mtdSetup_0.chromInfo.prsUncalBase;
			tbprsUncalAmtRespF.Text = mtdSetup_0.chromInfo.prsUncalAmtRespF.ToString();
			tbasChrom.Text = mtdSetup_0.chromInfo.asShowName;
			rbasAdd.Checked = mtdSetup_0.chromInfo.addChrom;
			rbasSub.Checked = !mtdSetup_0.chromInfo.addChrom;
			cbasMatching.SelectedIndex = (int)mtdSetup_0.chromInfo.asMatching;
			tbccUnretainedPeak.Text = mtdSetup_0.chromInfo.ccColumnUT.ToString();
			tbccColumnLength.Text = mtdSetup_0.chromInfo.ccColumnLength.ToString();
			switch (mtdSetup_0.chromInfo.ccStyle)
			{
			case ColumnCalcuStyle.Statistical:
				rbccStatistical.Checked = true;
				break;
			case ColumnCalcuStyle.From50per:
				rbccFrom50per.Checked = true;
				break;
			}
			break;
		}
	}

	private void miIntegAppendRow_Click(object sender, EventArgs e)
	{
		gvInteg.RowCount++;
	}

	private void method_10(object sender, EventArgs e)
	{
		gvInteg.DeleteSelectedRows();
	}

	private void miIntegInsertRow_Click(object sender, EventArgs e)
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
		gvInteg.Refresh(AccStyle.Read, new Integration());
	}

	private void miIntegRowsDown_Click(object sender, EventArgs e)
	{
		gvInteg.AdjustSelectedRows(AdjustUpDown.Down);
	}

	private void miIntegResetRows_Click(object sender, EventArgs e)
	{
		gvInteg.AdjustSelectedRows(AdjustUpDown.Up);
	}

	private void btnasSetChrom_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdSetup_0.chromInfo;
		if (sender == btnasSetChrom)
		{
			if (openFileDialog_2 == null)
			{
				openFileDialog_2 = new OpenFileDialog();
				openFileDialog_2.Title = "设置加/减谱图";
				openFileDialog_2.Filter = Class49.MakeFileFilter(".chm") + "|" + Class49.MakeFileFilter(".dat");
				openFileDialog_2.FilterIndex = 2;
			}
			openFileDialog_2.InitialDirectory = chromInfo.asDirectory;
			if (openFileDialog_2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.asChrom = openFileDialog_2.FileName;
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
		ChromInfo chromInfo = mtdSetup_0.chromInfo;
		if (sender == btnasSetChrom)
		{
			if (openFileDialog_2 == null)
			{
				openFileDialog_2 = new OpenFileDialog();
				openFileDialog_2.Title = "设置加/减谱图";
				openFileDialog_2.Filter = Class49.MakeFileFilter(".chm") + "|" + Class49.MakeFileFilter(".dat");
				openFileDialog_2.FilterIndex = 2;
			}
			openFileDialog_2.InitialDirectory = chromInfo.asDirectory;
			if (openFileDialog_2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.asChrom = openFileDialog_2.FileName;
		}
		if (sender == btnasNoneChrom)
		{
			chromInfo.asChrom = "";
		}
		chromInfo.RefreshAsInfo();
		tbasChrom.Text = chromInfo.asShowName;
	}

	private void button6_Click(object sender, EventArgs e)
	{
		openFileDialog_0 = new OpenFileDialog();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			openFileDialog_0.Title = "打开基线文件";
			break;
		case SysLanguage.EN:
			openFileDialog_0.Title = "open baseline file";
			break;
		}
		openFileDialog_0.Filter = Class49.MakeFileFilter(".sda");
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			lclTextBox17.Text = openFileDialog_0.FileName;
		}
	}

	private void checkBox8_CheckedChanged(object sender, EventArgs e)
	{
		if (bLoading)
		{
			return;
		}
		TcpServerSocket currentTcpSocket = formMain_0.GetCurrentTcpSocket();
		if (currentTcpSocket == null)
		{
			return;
		}
		ChannelChartPara oneEquipPara = formMain_0.FrmEquip.GetOneEquipPara(formMain_0.CurrentGCID, formMain_0.tabChannel.SelectedIndex);
		if (!checkBox8.Checked)
		{
			currentTcpSocket.sglsSampling[formMain_0.tabChannel.SelectedIndex].baseLinededuct = false;
			oneEquipPara.bBaselineDeduction = false;
			formMain_0.FrmEquip.UpdateOneEquipPara(formMain_0.CurrentGCID, formMain_0.tabChannel.SelectedIndex, oneEquipPara);
			return;
		}
		method_9(AccStyle.Write);
		FrmToDevicePara(formMain_0);
		ChromDevice chromDevice = new ChromDevice();
		for (int i = 0; i < formMain_0.FrmEquip.SunAquips.Count; i++)
		{
			if (formMain_0.FrmEquip.SunAquips[i].info.ID == formMain_0.CurrentGCID)
			{
				chromDevice = formMain_0.FrmEquip.SunAquips[i];
			}
		}
		int selectedIndex = formMain_0.tabChannel.SelectedIndex;
		if (chromDevice.misMgr.ChartParaOperaS[selectedIndex].TemplatePath.Trim() == "")
		{
			MessageBox.Show(Lang.PS("请先设置基线文件！", "Please set the baseline file !"));
			checkBox8.Checked = false;
		}
		else
		{
			currentTcpSocket.sglsSampling[formMain_0.tabChannel.SelectedIndex].baseLinededuct = true;
			oneEquipPara.bBaselineDeduction = true;
			formMain_0.FrmEquip.UpdateOneEquipPara(formMain_0.CurrentGCID, formMain_0.tabChannel.SelectedIndex, oneEquipPara);
		}
	}

	private void lclButton2_Click(object sender, EventArgs e)
	{
		lclTextBox17.Text = "";
	}

	private void lclTextBox18_TextChanged(object sender, EventArgs e)
	{
		float num = Class49.String2Float(((LclTextBox)sender).Text, -111f);
		if (num == -111f)
		{
			((LclTextBox)sender).Text = "";
		}
	}

	private void button7_Click(object sender, EventArgs e)
	{
		if (!(FileUserSet.Text.Trim() != ""))
		{
			return;
		}
		string text = Class49.ReadConfigSection("SampleName");
		if (text != null)
		{
			string[] array = text.Split('$');
			string text2 = "";
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != FileUserSet.Text.Trim())
				{
					text2 = ((i != array.Length - 1) ? (text2 + array[i] + "$") : (text2 + array[i]));
				}
			}
			Class49.Write2ConfigFile(text2);
		}
		method_0();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FrmDisposePara));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.tcMethod = new IBrainChrom2018.LclTabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.label42 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label26 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.lclTextBox9 = new IBrainChrom2018.LclTextBox();
		this.lclTextBox10 = new IBrainChrom2018.LclTextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.lclTextBox11 = new IBrainChrom2018.LclTextBox();
		this.lclTextBox12 = new IBrainChrom2018.LclTextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.lclTextBox13 = new IBrainChrom2018.LclTextBox();
		this.lclTextBox14 = new IBrainChrom2018.LclTextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.lclTextBox15 = new IBrainChrom2018.LclTextBox();
		this.lclTextBox16 = new IBrainChrom2018.LclTextBox();
		this.label34 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.label39 = new System.Windows.Forms.Label();
		this.label40 = new System.Windows.Forms.Label();
		this.label41 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.comboBox8 = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.comboBox7 = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.comboBox6 = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.comboBox5 = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.comboBox4 = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.comboBox3 = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.comboBox2 = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.lclTextBox8 = new IBrainChrom2018.LclTextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.lclTextBox7 = new IBrainChrom2018.LclTextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.lclTextBox6 = new IBrainChrom2018.LclTextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.lclTextBox5 = new IBrainChrom2018.LclTextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.lclTextBox4 = new IBrainChrom2018.LclTextBox();
		this.label21 = new System.Windows.Forms.Label();
		this.lclTextBox3 = new IBrainChrom2018.LclTextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.lclTextBox2 = new IBrainChrom2018.LclTextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.lclTextBox1 = new IBrainChrom2018.LclTextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.BaselineRemove = new System.Windows.Forms.GroupBox();
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.button6 = new System.Windows.Forms.Button();
		this.lclTextBox17 = new IBrainChrom2018.LclTextBox();
		this.lclButton2 = new IBrainChrom2018.LclButton();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.lclTextBox20 = new IBrainChrom2018.LclTextBox();
		this.label44 = new System.Windows.Forms.Label();
		this.lclTextBox19 = new IBrainChrom2018.LclTextBox();
		this.label43 = new System.Windows.Forms.Label();
		this.lclTextBox18 = new IBrainChrom2018.LclTextBox();
		this.label45 = new System.Windows.Forms.Label();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.button7 = new System.Windows.Forms.Button();
		this.FileUserSet = new System.Windows.Forms.ComboBox();
		this.label46 = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.label50 = new System.Windows.Forms.Label();
		this.InjectIndex = new System.Windows.Forms.CheckBox();
		this.FileNameAutoInject = new System.Windows.Forms.CheckBox();
		this.FileNameDateTime = new System.Windows.Forms.CheckBox();
		this.FileNameChannelName = new System.Windows.Forms.CheckBox();
		this.FileNameAquipName = new System.Windows.Forms.CheckBox();
		this.tpTempProg = new System.Windows.Forms.TabPage();
		this.dpgcProgTemp = new IBrainChrom2018.LclDisplayPanel();
		this.tpIntegration = new System.Windows.Forms.TabPage();
		this.gvInteg = new IBrainChrom2018.LclIntegGridView();
		this.cmsIntegration = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miIntegAppendRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegDeleteRows = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegInsertRow = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegResetRows = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegRowsDown = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegRowsUp = new System.Windows.Forms.ToolStripMenuItem();
		this.tpCaculation = new System.Windows.Forms.TabPage();
		this.cbcclCalcu = new IBrainChrom2018.LclCusComboBox();
		this.gbcclRltTableReport = new IBrainChrom2018.LclGroupBox();
		this.rbrtrCaliPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbrtrIdentifiedPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbrtrAllDetectedPeaks = new IBrainChrom2018.LclRadioButton();
		this.cbrtrHideISTDPeak = new IBrainChrom2018.LclCheckBox();
		this.gbcclParas = new IBrainChrom2018.LclGroupBox();
		this.cbprsUncalBase = new IBrainChrom2018.LclCusComboBox();
		this.cbprsUseScaleFactor = new IBrainChrom2018.LclCheckBox();
		this.lbprsUncalBase = new IBrainChrom2018.LclLabel();
		this.lbprsUncalAmtRespFU = new IBrainChrom2018.LclLabel();
		this.lbprsUncalAmtRespF = new IBrainChrom2018.LclLabel();
		this.lbprsUnitAfterScale = new IBrainChrom2018.LclLabel();
		this.lbprsScaleFactor = new IBrainChrom2018.LclLabel();
		this.tbprsUncalAmtRespF = new IBrainChrom2018.LclTextBox();
		this.tbprsUnitAfterScale = new IBrainChrom2018.LclTextBox();
		this.tbprsScaleFactor = new IBrainChrom2018.LclTextBox();
		this.btncclSet = new IBrainChrom2018.LclButton();
		this.btncclNone = new IBrainChrom2018.LclButton();
		this.btncclView = new IBrainChrom2018.LclButton();
		this.tbcclCalibration = new IBrainChrom2018.LclTextBox();
		this.lbcclModifiedTimeV = new IBrainChrom2018.LclLabel();
		this.lbcclModifiedTime = new IBrainChrom2018.LclLabel();
		this.lbcclCreateTimeV = new IBrainChrom2018.LclLabel();
		this.lbcclCreateTime = new IBrainChrom2018.LclLabel();
		this.lbcclDescriptionV = new IBrainChrom2018.LclLabel();
		this.lbcclDescription = new IBrainChrom2018.LclLabel();
		this.lbcclAuthorV = new IBrainChrom2018.LclLabel();
		this.lbcclAuthor = new IBrainChrom2018.LclLabel();
		this.lbcclCalcu = new IBrainChrom2018.LclLabel();
		this.lbcclCalibration = new IBrainChrom2018.LclLabel();
		this.tpAdvanced = new System.Windows.Forms.TabPage();
		this.gbadvColumnCalcu = new IBrainChrom2018.LclGroupBox();
		this.rbccFrom50per = new IBrainChrom2018.LclRadioButton();
		this.rbccStatistical = new IBrainChrom2018.LclRadioButton();
		this.tbccColumnLength = new IBrainChrom2018.LclTextBox();
		this.lbccColumnLengthU = new IBrainChrom2018.LclLabel();
		this.tbccUnretainedPeak = new IBrainChrom2018.LclTextBox();
		this.lbccColumnLength = new IBrainChrom2018.LclLabel();
		this.lbccUnretainedPeakU = new IBrainChrom2018.LclLabel();
		this.lbccUnretainedPeak = new IBrainChrom2018.LclLabel();
		this.gbadvAddSub = new IBrainChrom2018.LclGroupBox();
		this.rbasSub = new IBrainChrom2018.LclRadioButton();
		this.rbasAdd = new IBrainChrom2018.LclRadioButton();
		this.cbasMatching = new IBrainChrom2018.LclCusComboBox();
		this.btnasSetChrom = new IBrainChrom2018.LclButton();
		this.btnasNoneChrom = new IBrainChrom2018.LclButton();
		this.tbasChrom = new IBrainChrom2018.LclTextBox();
		this.lbasMatching = new IBrainChrom2018.LclLabel();
		this.lbasChrom = new IBrainChrom2018.LclLabel();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rptbotom = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rpthead = new System.Windows.Forms.TextBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.button5 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
		this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tableLayoutPanel1.SuspendLayout();
		this.panel1.SuspendLayout();
		this.tcMethod.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.BaselineRemove.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.tpTempProg.SuspendLayout();
		this.tpIntegration.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvInteg).BeginInit();
		this.cmsIntegration.SuspendLayout();
		this.tpCaculation.SuspendLayout();
		this.gbcclRltTableReport.SuspendLayout();
		this.gbcclParas.SuspendLayout();
		this.tpAdvanced.SuspendLayout();
		this.gbadvColumnCalcu.SuspendLayout();
		this.gbadvAddSub.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.tableLayoutPanel1.ColumnCount = 1;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 2;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.51807f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.48193f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 404);
		this.tableLayoutPanel1.TabIndex = 0;
		this.panel1.Controls.Add(this.tcMethod);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(3, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(456, 315);
		this.panel1.TabIndex = 0;
		this.tcMethod.Controls.Add(this.tabPage1);
		this.tcMethod.Controls.Add(this.tabPage3);
		this.tcMethod.Controls.Add(this.tpTempProg);
		this.tcMethod.Controls.Add(this.tpIntegration);
		this.tcMethod.Controls.Add(this.tpCaculation);
		this.tcMethod.Controls.Add(this.tpAdvanced);
		this.tcMethod.Controls.Add(this.tabPage2);
		this.tcMethod.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tcMethod.ItemSize = new System.Drawing.Size(90, 19);
		this.tcMethod.Location = new System.Drawing.Point(0, 0);
		this.tcMethod.Name = "tcMethod";
		this.tcMethod.SelectedIndex = 0;
		this.tcMethod.Size = new System.Drawing.Size(456, 315);
		this.tcMethod.TabIndex = 6;
		this.tabPage1.Controls.Add(this.label42);
		this.tabPage1.Controls.Add(this.groupBox4);
		this.tabPage1.Controls.Add(this.groupBox3);
		this.tabPage1.Location = new System.Drawing.Point(4, 23);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(448, 288);
		this.tabPage1.TabIndex = 15;
		this.tabPage1.Text = "时间程序及平齐功能";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.label42.AutoSize = true;
		this.label42.ForeColor = System.Drawing.Color.Blue;
		this.label42.Location = new System.Drawing.Point(147, 211);
		this.label42.Name = "label42";
		this.label42.Size = new System.Drawing.Size(287, 12);
		this.label42.TabIndex = 10;
		this.label42.Text = "注:以上时间程序及平齐功能内时间请按小到大填写！";
		this.groupBox4.Controls.Add(this.label26);
		this.groupBox4.Controls.Add(this.label27);
		this.groupBox4.Controls.Add(this.label28);
		this.groupBox4.Controls.Add(this.label29);
		this.groupBox4.Controls.Add(this.label30);
		this.groupBox4.Controls.Add(this.lclTextBox9);
		this.groupBox4.Controls.Add(this.lclTextBox10);
		this.groupBox4.Controls.Add(this.label31);
		this.groupBox4.Controls.Add(this.lclTextBox11);
		this.groupBox4.Controls.Add(this.lclTextBox12);
		this.groupBox4.Controls.Add(this.label32);
		this.groupBox4.Controls.Add(this.lclTextBox13);
		this.groupBox4.Controls.Add(this.lclTextBox14);
		this.groupBox4.Controls.Add(this.label33);
		this.groupBox4.Controls.Add(this.lclTextBox15);
		this.groupBox4.Controls.Add(this.lclTextBox16);
		this.groupBox4.Controls.Add(this.label34);
		this.groupBox4.Controls.Add(this.label35);
		this.groupBox4.Controls.Add(this.label36);
		this.groupBox4.Controls.Add(this.label37);
		this.groupBox4.Controls.Add(this.label38);
		this.groupBox4.Controls.Add(this.label39);
		this.groupBox4.Controls.Add(this.label40);
		this.groupBox4.Controls.Add(this.label41);
		this.groupBox4.Location = new System.Drawing.Point(299, 3);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(143, 205);
		this.groupBox4.TabIndex = 4;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "平齐功能";
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(6, 17);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(35, 12);
		this.label26.TabIndex = 17;
		this.label26.Text = "开始:";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(6, 40);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(35, 12);
		this.label27.TabIndex = 18;
		this.label27.Text = "终止:";
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(6, 63);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(35, 12);
		this.label28.TabIndex = 14;
		this.label28.Text = "开始:";
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(6, 86);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(35, 12);
		this.label29.TabIndex = 15;
		this.label29.Text = "终止:";
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(6, 109);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(35, 12);
		this.label30.TabIndex = 13;
		this.label30.Text = "开始:";
		this.lclTextBox9.Location = new System.Drawing.Point(47, 174);
		this.lclTextBox9.Name = "lclTextBox9";
		this.lclTextBox9.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox9.TabIndex = 20;
		this.lclTextBox9.Text = "0";
		this.lclTextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lclTextBox10.Location = new System.Drawing.Point(47, 149);
		this.lclTextBox10.Name = "lclTextBox10";
		this.lclTextBox10.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox10.TabIndex = 24;
		this.lclTextBox10.Text = "0";
		this.lclTextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(6, 131);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(35, 12);
		this.label31.TabIndex = 16;
		this.label31.Text = "终止:";
		this.lclTextBox11.Location = new System.Drawing.Point(47, 126);
		this.lclTextBox11.Name = "lclTextBox11";
		this.lclTextBox11.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox11.TabIndex = 22;
		this.lclTextBox11.Text = "0";
		this.lclTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lclTextBox12.Location = new System.Drawing.Point(47, 104);
		this.lclTextBox12.Name = "lclTextBox12";
		this.lclTextBox12.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox12.TabIndex = 23;
		this.lclTextBox12.Text = "0";
		this.lclTextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label32.AutoSize = true;
		this.label32.Location = new System.Drawing.Point(6, 154);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(35, 12);
		this.label32.TabIndex = 6;
		this.label32.Text = "开始:";
		this.lclTextBox13.Location = new System.Drawing.Point(47, 81);
		this.lclTextBox13.Name = "lclTextBox13";
		this.lclTextBox13.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox13.TabIndex = 19;
		this.lclTextBox13.Text = "0";
		this.lclTextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lclTextBox14.Location = new System.Drawing.Point(47, 58);
		this.lclTextBox14.Name = "lclTextBox14";
		this.lclTextBox14.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox14.TabIndex = 21;
		this.lclTextBox14.Text = "0";
		this.lclTextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(6, 179);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(35, 12);
		this.label33.TabIndex = 5;
		this.label33.Text = "终止:";
		this.lclTextBox15.Location = new System.Drawing.Point(47, 35);
		this.lclTextBox15.Name = "lclTextBox15";
		this.lclTextBox15.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox15.TabIndex = 26;
		this.lclTextBox15.Text = "0";
		this.lclTextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lclTextBox16.Location = new System.Drawing.Point(47, 12);
		this.lclTextBox16.Name = "lclTextBox16";
		this.lclTextBox16.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox16.TabIndex = 25;
		this.lclTextBox16.Text = "0";
		this.lclTextBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label34.AutoSize = true;
		this.label34.Location = new System.Drawing.Point(112, 17);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(23, 12);
		this.label34.TabIndex = 3;
		this.label34.Text = "min";
		this.label35.AutoSize = true;
		this.label35.Location = new System.Drawing.Point(112, 179);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(23, 12);
		this.label35.TabIndex = 4;
		this.label35.Text = "min";
		this.label36.AutoSize = true;
		this.label36.Location = new System.Drawing.Point(112, 40);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(23, 12);
		this.label36.TabIndex = 11;
		this.label36.Text = "min";
		this.label37.AutoSize = true;
		this.label37.Location = new System.Drawing.Point(112, 154);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(23, 12);
		this.label37.TabIndex = 12;
		this.label37.Text = "min";
		this.label38.AutoSize = true;
		this.label38.Location = new System.Drawing.Point(112, 63);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(23, 12);
		this.label38.TabIndex = 10;
		this.label38.Text = "min";
		this.label39.AutoSize = true;
		this.label39.Location = new System.Drawing.Point(112, 131);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(23, 12);
		this.label39.TabIndex = 8;
		this.label39.Text = "min";
		this.label40.AutoSize = true;
		this.label40.Location = new System.Drawing.Point(112, 86);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(23, 12);
		this.label40.TabIndex = 9;
		this.label40.Text = "min";
		this.label41.AutoSize = true;
		this.label41.Location = new System.Drawing.Point(112, 109);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(23, 12);
		this.label41.TabIndex = 7;
		this.label41.Text = "min";
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.comboBox8);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.comboBox7);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.comboBox6);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.comboBox5);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.comboBox4);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.comboBox3);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.comboBox2);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.comboBox1);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.lclTextBox8);
		this.groupBox3.Controls.Add(this.label15);
		this.groupBox3.Controls.Add(this.lclTextBox7);
		this.groupBox3.Controls.Add(this.label17);
		this.groupBox3.Controls.Add(this.lclTextBox6);
		this.groupBox3.Controls.Add(this.label18);
		this.groupBox3.Controls.Add(this.lclTextBox5);
		this.groupBox3.Controls.Add(this.label20);
		this.groupBox3.Controls.Add(this.lclTextBox4);
		this.groupBox3.Controls.Add(this.label21);
		this.groupBox3.Controls.Add(this.lclTextBox3);
		this.groupBox3.Controls.Add(this.label23);
		this.groupBox3.Controls.Add(this.lclTextBox2);
		this.groupBox3.Controls.Add(this.label24);
		this.groupBox3.Controls.Add(this.lclTextBox1);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.label25);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.label22);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.label19);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Controls.Add(this.label16);
		this.groupBox3.Location = new System.Drawing.Point(6, 3);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(287, 205);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "合成时间程序";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 17);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "时长:";
		this.comboBox8.FormattingEnabled = true;
		this.comboBox8.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox8.Location = new System.Drawing.Point(199, 174);
		this.comboBox8.Name = "comboBox8";
		this.comboBox8.Size = new System.Drawing.Size(77, 20);
		this.comboBox8.TabIndex = 1;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(141, 17);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(47, 12);
		this.label3.TabIndex = 0;
		this.label3.Text = "检测器:";
		this.comboBox7.FormattingEnabled = true;
		this.comboBox7.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox7.Location = new System.Drawing.Point(199, 149);
		this.comboBox7.Name = "comboBox7";
		this.comboBox7.Size = new System.Drawing.Size(77, 20);
		this.comboBox7.TabIndex = 1;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 40);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(35, 12);
		this.label5.TabIndex = 0;
		this.label5.Text = "时长:";
		this.comboBox6.FormattingEnabled = true;
		this.comboBox6.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox6.Location = new System.Drawing.Point(199, 126);
		this.comboBox6.Name = "comboBox6";
		this.comboBox6.Size = new System.Drawing.Size(77, 20);
		this.comboBox6.TabIndex = 1;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(141, 40);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(47, 12);
		this.label6.TabIndex = 0;
		this.label6.Text = "检测器:";
		this.comboBox5.FormattingEnabled = true;
		this.comboBox5.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox5.Location = new System.Drawing.Point(199, 104);
		this.comboBox5.Name = "comboBox5";
		this.comboBox5.Size = new System.Drawing.Size(77, 20);
		this.comboBox5.TabIndex = 1;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(6, 63);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 12);
		this.label8.TabIndex = 0;
		this.label8.Text = "时长:";
		this.comboBox4.FormattingEnabled = true;
		this.comboBox4.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox4.Location = new System.Drawing.Point(199, 81);
		this.comboBox4.Name = "comboBox4";
		this.comboBox4.Size = new System.Drawing.Size(77, 20);
		this.comboBox4.TabIndex = 1;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(141, 63);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(47, 12);
		this.label9.TabIndex = 0;
		this.label9.Text = "检测器:";
		this.comboBox3.FormattingEnabled = true;
		this.comboBox3.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox3.Location = new System.Drawing.Point(199, 58);
		this.comboBox3.Name = "comboBox3";
		this.comboBox3.Size = new System.Drawing.Size(77, 20);
		this.comboBox3.TabIndex = 1;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(6, 86);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(35, 12);
		this.label11.TabIndex = 0;
		this.label11.Text = "时长:";
		this.comboBox2.FormattingEnabled = true;
		this.comboBox2.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox2.Location = new System.Drawing.Point(199, 35);
		this.comboBox2.Name = "comboBox2";
		this.comboBox2.Size = new System.Drawing.Size(77, 20);
		this.comboBox2.TabIndex = 1;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(141, 86);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(47, 12);
		this.label12.TabIndex = 0;
		this.label12.Text = "检测器:";
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[5] { "", "检测器1", "检测器2", "检测器3", "检测器4" });
		this.comboBox1.Location = new System.Drawing.Point(199, 12);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(77, 20);
		this.comboBox1.TabIndex = 1;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(6, 109);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(35, 12);
		this.label14.TabIndex = 0;
		this.label14.Text = "时长:";
		this.lclTextBox8.Location = new System.Drawing.Point(47, 174);
		this.lclTextBox8.Name = "lclTextBox8";
		this.lclTextBox8.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox8.TabIndex = 2;
		this.lclTextBox8.Text = "0";
		this.lclTextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(141, 109);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(47, 12);
		this.label15.TabIndex = 0;
		this.label15.Text = "检测器:";
		this.lclTextBox7.Location = new System.Drawing.Point(47, 149);
		this.lclTextBox7.Name = "lclTextBox7";
		this.lclTextBox7.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox7.TabIndex = 2;
		this.lclTextBox7.Text = "0";
		this.lclTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(6, 131);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(35, 12);
		this.label17.TabIndex = 0;
		this.label17.Text = "时长:";
		this.lclTextBox6.Location = new System.Drawing.Point(47, 126);
		this.lclTextBox6.Name = "lclTextBox6";
		this.lclTextBox6.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox6.TabIndex = 2;
		this.lclTextBox6.Text = "0";
		this.lclTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(141, 131);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(47, 12);
		this.label18.TabIndex = 0;
		this.label18.Text = "检测器:";
		this.lclTextBox5.Location = new System.Drawing.Point(47, 104);
		this.lclTextBox5.Name = "lclTextBox5";
		this.lclTextBox5.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox5.TabIndex = 2;
		this.lclTextBox5.Text = "0";
		this.lclTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(6, 154);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(35, 12);
		this.label20.TabIndex = 0;
		this.label20.Text = "时长:";
		this.lclTextBox4.Location = new System.Drawing.Point(47, 81);
		this.lclTextBox4.Name = "lclTextBox4";
		this.lclTextBox4.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox4.TabIndex = 2;
		this.lclTextBox4.Text = "0";
		this.lclTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(141, 154);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(47, 12);
		this.label21.TabIndex = 0;
		this.label21.Text = "检测器:";
		this.lclTextBox3.Location = new System.Drawing.Point(47, 58);
		this.lclTextBox3.Name = "lclTextBox3";
		this.lclTextBox3.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox3.TabIndex = 2;
		this.lclTextBox3.Text = "0";
		this.lclTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(6, 179);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(35, 12);
		this.label23.TabIndex = 0;
		this.label23.Text = "时长:";
		this.lclTextBox2.Location = new System.Drawing.Point(47, 35);
		this.lclTextBox2.Name = "lclTextBox2";
		this.lclTextBox2.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox2.TabIndex = 2;
		this.lclTextBox2.Text = "0";
		this.lclTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(141, 179);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(47, 12);
		this.label24.TabIndex = 0;
		this.label24.Text = "检测器:";
		this.lclTextBox1.Location = new System.Drawing.Point(47, 12);
		this.lclTextBox1.Name = "lclTextBox1";
		this.lclTextBox1.Size = new System.Drawing.Size(59, 21);
		this.lclTextBox1.TabIndex = 2;
		this.lclTextBox1.Text = "0";
		this.lclTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(112, 17);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(23, 12);
		this.label4.TabIndex = 0;
		this.label4.Text = "min";
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(112, 179);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(23, 12);
		this.label25.TabIndex = 0;
		this.label25.Text = "min";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(112, 40);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(23, 12);
		this.label7.TabIndex = 0;
		this.label7.Text = "min";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(112, 154);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(23, 12);
		this.label22.TabIndex = 0;
		this.label22.Text = "min";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(112, 63);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(23, 12);
		this.label10.TabIndex = 0;
		this.label10.Text = "min";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(112, 131);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(23, 12);
		this.label19.TabIndex = 0;
		this.label19.Text = "min";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(112, 86);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(23, 12);
		this.label13.TabIndex = 0;
		this.label13.Text = "min";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(112, 109);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(23, 12);
		this.label16.TabIndex = 0;
		this.label16.Text = "min";
		this.tabPage3.Controls.Add(this.BaselineRemove);
		this.tabPage3.Controls.Add(this.groupBox6);
		this.tabPage3.Controls.Add(this.checkBox1);
		this.tabPage3.Controls.Add(this.groupBox5);
		this.tabPage3.Location = new System.Drawing.Point(4, 23);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(448, 288);
		this.tabPage3.TabIndex = 16;
		this.tabPage3.Text = "基线扣除文件命名";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.BaselineRemove.Controls.Add(this.lclLabel1);
		this.BaselineRemove.Controls.Add(this.button6);
		this.BaselineRemove.Controls.Add(this.lclTextBox17);
		this.BaselineRemove.Controls.Add(this.lclButton2);
		this.BaselineRemove.Controls.Add(this.checkBox8);
		this.BaselineRemove.Location = new System.Drawing.Point(7, 3);
		this.BaselineRemove.Name = "BaselineRemove";
		this.BaselineRemove.Size = new System.Drawing.Size(436, 80);
		this.BaselineRemove.TabIndex = 21;
		this.BaselineRemove.TabStop = false;
		this.BaselineRemove.Text = "基线扣除";
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(6, 17);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(53, 12);
		this.lclLabel1.TabIndex = 12;
		this.lclLabel1.Text = "基线文件";
		this.button6.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.button6.Location = new System.Drawing.Point(291, 25);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(31, 32);
		this.button6.TabIndex = 15;
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.lclTextBox17.Location = new System.Drawing.Point(8, 32);
		this.lclTextBox17.Name = "lclTextBox17";
		this.lclTextBox17.ReadOnly = true;
		this.lclTextBox17.Size = new System.Drawing.Size(276, 21);
		this.lclTextBox17.TabIndex = 13;
		this.lclButton2.Location = new System.Drawing.Point(9, 55);
		this.lclButton2.Name = "lclButton2";
		this.lclButton2.Size = new System.Drawing.Size(75, 23);
		this.lclButton2.TabIndex = 14;
		this.lclButton2.Text = "清空";
		this.lclButton2.UseVisualStyleBackColor = true;
		this.lclButton2.Click += new System.EventHandler(lclButton2_Click);
		this.checkBox8.AutoSize = true;
		this.checkBox8.Location = new System.Drawing.Point(117, 59);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(72, 16);
		this.checkBox8.TabIndex = 16;
		this.checkBox8.Text = "基线扣除";
		this.checkBox8.UseVisualStyleBackColor = true;
		this.groupBox6.Controls.Add(this.lclTextBox20);
		this.groupBox6.Controls.Add(this.label44);
		this.groupBox6.Controls.Add(this.lclTextBox19);
		this.groupBox6.Controls.Add(this.label43);
		this.groupBox6.Controls.Add(this.lclTextBox18);
		this.groupBox6.Controls.Add(this.label45);
		this.groupBox6.Location = new System.Drawing.Point(169, 215);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(274, 56);
		this.groupBox6.TabIndex = 20;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "零点参数";
		this.groupBox6.Visible = false;
		this.lclTextBox20.Location = new System.Drawing.Point(188, 33);
		this.lclTextBox20.Name = "lclTextBox20";
		this.lclTextBox20.Size = new System.Drawing.Size(49, 21);
		this.lclTextBox20.TabIndex = 19;
		this.lclTextBox20.TextChanged += new System.EventHandler(lclTextBox18_TextChanged);
		this.label44.AutoSize = true;
		this.label44.Location = new System.Drawing.Point(127, 13);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(59, 12);
		this.label44.TabIndex = 3;
		this.label44.Text = "左时间窗:";
		this.lclTextBox19.Location = new System.Drawing.Point(188, 9);
		this.lclTextBox19.Name = "lclTextBox19";
		this.lclTextBox19.Size = new System.Drawing.Size(49, 21);
		this.lclTextBox19.TabIndex = 19;
		this.lclTextBox19.TextChanged += new System.EventHandler(lclTextBox18_TextChanged);
		this.label43.AutoSize = true;
		this.label43.Location = new System.Drawing.Point(8, 29);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(35, 12);
		this.label43.TabIndex = 3;
		this.label43.Text = "零点:";
		this.lclTextBox18.Location = new System.Drawing.Point(47, 25);
		this.lclTextBox18.Name = "lclTextBox18";
		this.lclTextBox18.Size = new System.Drawing.Size(74, 21);
		this.lclTextBox18.TabIndex = 19;
		this.lclTextBox18.TextChanged += new System.EventHandler(lclTextBox18_TextChanged);
		this.label45.AutoSize = true;
		this.label45.Location = new System.Drawing.Point(127, 36);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(59, 12);
		this.label45.TabIndex = 3;
		this.label45.Text = "右时间窗:";
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(14, 221);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(120, 16);
		this.checkBox1.TabIndex = 18;
		this.checkBox1.Text = "使用相对保留时间";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Visible = false;
		this.groupBox5.Controls.Add(this.button7);
		this.groupBox5.Controls.Add(this.FileUserSet);
		this.groupBox5.Controls.Add(this.label46);
		this.groupBox5.Controls.Add(this.label47);
		this.groupBox5.Controls.Add(this.label51);
		this.groupBox5.Controls.Add(this.label48);
		this.groupBox5.Controls.Add(this.label49);
		this.groupBox5.Controls.Add(this.label50);
		this.groupBox5.Controls.Add(this.InjectIndex);
		this.groupBox5.Controls.Add(this.FileNameAutoInject);
		this.groupBox5.Controls.Add(this.FileNameDateTime);
		this.groupBox5.Controls.Add(this.FileNameChannelName);
		this.groupBox5.Controls.Add(this.FileNameAquipName);
		this.groupBox5.Location = new System.Drawing.Point(7, 84);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(436, 125);
		this.groupBox5.TabIndex = 17;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "文件命名设置";
		this.button7.Location = new System.Drawing.Point(103, 83);
		this.button7.Name = "button7";
		this.button7.Size = new System.Drawing.Size(190, 23);
		this.button7.TabIndex = 9;
		this.button7.Text = "删除当前批号";
		this.button7.UseVisualStyleBackColor = true;
		this.button7.Click += new System.EventHandler(button7_Click);
		this.FileUserSet.FormattingEnabled = true;
		this.FileUserSet.Location = new System.Drawing.Point(103, 52);
		this.FileUserSet.Name = "FileUserSet";
		this.FileUserSet.Size = new System.Drawing.Size(190, 20);
		this.FileUserSet.TabIndex = 8;
		this.label46.AutoSize = true;
		this.label46.Location = new System.Drawing.Point(5, 55);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(11, 12);
		this.label46.TabIndex = 5;
		this.label46.Text = "+";
		this.label47.AutoSize = true;
		this.label47.Location = new System.Drawing.Point(18, 55);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(83, 12);
		this.label47.TabIndex = 3;
		this.label47.Text = "样品名称批号:";
		this.label51.AutoSize = true;
		this.label51.Location = new System.Drawing.Point(299, 56);
		this.label51.Name = "label51";
		this.label51.Size = new System.Drawing.Size(11, 12);
		this.label51.TabIndex = 1;
		this.label51.Text = "+";
		this.label48.AutoSize = true;
		this.label48.Location = new System.Drawing.Point(299, 93);
		this.label48.Name = "label48";
		this.label48.Size = new System.Drawing.Size(11, 12);
		this.label48.TabIndex = 1;
		this.label48.Text = "+";
		this.label49.AutoSize = true;
		this.label49.Location = new System.Drawing.Point(300, 24);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(11, 12);
		this.label49.TabIndex = 1;
		this.label49.Text = "+";
		this.label50.AutoSize = true;
		this.label50.Location = new System.Drawing.Point(110, 24);
		this.label50.Name = "label50";
		this.label50.Size = new System.Drawing.Size(11, 12);
		this.label50.TabIndex = 1;
		this.label50.Text = "+";
		this.InjectIndex.AutoSize = true;
		this.InjectIndex.Checked = true;
		this.InjectIndex.CheckState = System.Windows.Forms.CheckState.Checked;
		this.InjectIndex.Enabled = false;
		this.InjectIndex.Location = new System.Drawing.Point(341, 54);
		this.InjectIndex.Name = "InjectIndex";
		this.InjectIndex.Size = new System.Drawing.Size(72, 16);
		this.InjectIndex.TabIndex = 0;
		this.InjectIndex.Text = "进样序号";
		this.InjectIndex.UseVisualStyleBackColor = true;
		this.FileNameAutoInject.AutoSize = true;
		this.FileNameAutoInject.Checked = true;
		this.FileNameAutoInject.CheckState = System.Windows.Forms.CheckState.Checked;
		this.FileNameAutoInject.Location = new System.Drawing.Point(341, 91);
		this.FileNameAutoInject.Name = "FileNameAutoInject";
		this.FileNameAutoInject.Size = new System.Drawing.Size(72, 16);
		this.FileNameAutoInject.TabIndex = 0;
		this.FileNameAutoInject.Text = "自动进样";
		this.FileNameAutoInject.UseVisualStyleBackColor = true;
		this.FileNameDateTime.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
		this.FileNameDateTime.AutoSize = true;
		this.FileNameDateTime.Location = new System.Drawing.Point(341, 23);
		this.FileNameDateTime.Name = "FileNameDateTime";
		this.FileNameDateTime.Size = new System.Drawing.Size(48, 16);
		this.FileNameDateTime.TabIndex = 0;
		this.FileNameDateTime.Text = "时间";
		this.FileNameDateTime.UseVisualStyleBackColor = true;
		this.FileNameChannelName.AutoSize = true;
		this.FileNameChannelName.Checked = true;
		this.FileNameChannelName.CheckState = System.Windows.Forms.CheckState.Checked;
		this.FileNameChannelName.Location = new System.Drawing.Point(144, 22);
		this.FileNameChannelName.Name = "FileNameChannelName";
		this.FileNameChannelName.Size = new System.Drawing.Size(72, 16);
		this.FileNameChannelName.TabIndex = 0;
		this.FileNameChannelName.Text = "通道名称";
		this.FileNameChannelName.UseVisualStyleBackColor = true;
		this.FileNameAquipName.AutoSize = true;
		this.FileNameAquipName.Checked = true;
		this.FileNameAquipName.CheckState = System.Windows.Forms.CheckState.Checked;
		this.FileNameAquipName.Location = new System.Drawing.Point(8, 21);
		this.FileNameAquipName.Name = "FileNameAquipName";
		this.FileNameAquipName.Size = new System.Drawing.Size(78, 16);
		this.FileNameAquipName.TabIndex = 0;
		this.FileNameAquipName.Text = "机器名/ID";
		this.FileNameAquipName.UseVisualStyleBackColor = true;
		this.tpTempProg.Controls.Add(this.dpgcProgTemp);
		this.tpTempProg.Location = new System.Drawing.Point(4, 23);
		this.tpTempProg.Name = "tpTempProg";
		this.tpTempProg.Size = new System.Drawing.Size(448, 288);
		this.tpTempProg.TabIndex = 13;
		this.tpTempProg.Text = "程序升温";
		this.tpTempProg.UseVisualStyleBackColor = true;
		this.dpgcProgTemp.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgcProgTemp.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dpgcProgTemp.Location = new System.Drawing.Point(0, 0);
		this.dpgcProgTemp.Name = "dpgcProgTemp";
		this.dpgcProgTemp.Size = new System.Drawing.Size(448, 288);
		this.dpgcProgTemp.TabIndex = 4;
		this.dpgcProgTemp.Paint += new System.Windows.Forms.PaintEventHandler(dpgcProgTemp_Paint);
		this.tpIntegration.Controls.Add(this.gvInteg);
		this.tpIntegration.Location = new System.Drawing.Point(4, 23);
		this.tpIntegration.Name = "tpIntegration";
		this.tpIntegration.Size = new System.Drawing.Size(448, 288);
		this.tpIntegration.TabIndex = 6;
		this.tpIntegration.Text = "积分";
		this.tpIntegration.UseVisualStyleBackColor = true;
		this.gvInteg.AllowUserToAddRows = false;
		this.gvInteg.AllowUserToDeleteRows = false;
		this.gvInteg.AllowUserToResizeRows = false;
		this.gvInteg.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvInteg.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvInteg.ColumnHeadersHeight = 32;
		this.gvInteg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvInteg.ContextMenuStrip = this.cmsIntegration;
		this.gvInteg.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gvInteg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvInteg.Location = new System.Drawing.Point(0, 0);
		this.gvInteg.Name = "gvInteg";
		this.gvInteg.RowHeadersWidth = 25;
		this.gvInteg.RowTemplate.Height = 16;
		this.gvInteg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvInteg.ShowCellToolTips = false;
		this.gvInteg.Size = new System.Drawing.Size(448, 288);
		this.gvInteg.TabIndex = 1;
		this.cmsIntegration.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miIntegAppendRow, this.miIntegDeleteRows, this.miIntegInsertRow, this.toolStripSeparator1, this.miIntegResetRows, this.miIntegRowsDown, this.toolStripSeparator2, this.miIntegRowsUp });
		this.cmsIntegration.Name = "cmsIntegration";
		this.cmsIntegration.Size = new System.Drawing.Size(113, 148);
		this.miIntegAppendRow.Name = "miIntegAppendRow";
		this.miIntegAppendRow.Size = new System.Drawing.Size(112, 22);
		this.miIntegAppendRow.Text = "添加行";
		this.miIntegAppendRow.Click += new System.EventHandler(miIntegAppendRow_Click);
		this.miIntegDeleteRows.Name = "miIntegDeleteRows";
		this.miIntegDeleteRows.Size = new System.Drawing.Size(112, 22);
		this.miIntegDeleteRows.Text = "插入行";
		this.miIntegDeleteRows.Click += new System.EventHandler(miIntegInsertRow_Click);
		this.miIntegInsertRow.Name = "miIntegInsertRow";
		this.miIntegInsertRow.Size = new System.Drawing.Size(112, 22);
		this.miIntegInsertRow.Text = "删除行";
		this.miIntegInsertRow.Click += new System.EventHandler(miIntegInsertRow_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
		this.miIntegResetRows.Name = "miIntegResetRows";
		this.miIntegResetRows.Size = new System.Drawing.Size(112, 22);
		this.miIntegResetRows.Text = "上移";
		this.miIntegResetRows.Click += new System.EventHandler(miIntegResetRows_Click);
		this.miIntegRowsDown.Name = "miIntegRowsDown";
		this.miIntegRowsDown.Size = new System.Drawing.Size(112, 22);
		this.miIntegRowsDown.Text = "下移";
		this.miIntegRowsDown.Click += new System.EventHandler(miIntegRowsDown_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(109, 6);
		this.miIntegRowsUp.Name = "miIntegRowsUp";
		this.miIntegRowsUp.Size = new System.Drawing.Size(112, 22);
		this.miIntegRowsUp.Text = "重置";
		this.miIntegRowsUp.Click += new System.EventHandler(miIntegRowsUp_Click);
		this.tpCaculation.Controls.Add(this.cbcclCalcu);
		this.tpCaculation.Controls.Add(this.gbcclRltTableReport);
		this.tpCaculation.Controls.Add(this.gbcclParas);
		this.tpCaculation.Controls.Add(this.btncclSet);
		this.tpCaculation.Controls.Add(this.btncclNone);
		this.tpCaculation.Controls.Add(this.btncclView);
		this.tpCaculation.Controls.Add(this.tbcclCalibration);
		this.tpCaculation.Controls.Add(this.lbcclModifiedTimeV);
		this.tpCaculation.Controls.Add(this.lbcclModifiedTime);
		this.tpCaculation.Controls.Add(this.lbcclCreateTimeV);
		this.tpCaculation.Controls.Add(this.lbcclCreateTime);
		this.tpCaculation.Controls.Add(this.lbcclDescriptionV);
		this.tpCaculation.Controls.Add(this.lbcclDescription);
		this.tpCaculation.Controls.Add(this.lbcclAuthorV);
		this.tpCaculation.Controls.Add(this.lbcclAuthor);
		this.tpCaculation.Controls.Add(this.lbcclCalcu);
		this.tpCaculation.Controls.Add(this.lbcclCalibration);
		this.tpCaculation.Location = new System.Drawing.Point(4, 23);
		this.tpCaculation.Name = "tpCaculation";
		this.tpCaculation.Size = new System.Drawing.Size(448, 288);
		this.tpCaculation.TabIndex = 7;
		this.tpCaculation.Text = "校正文件";
		this.tpCaculation.UseVisualStyleBackColor = true;
		this.cbcclCalcu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbcclCalcu.FormattingEnabled = true;
		this.cbcclCalcu.ItemExtString = "";
		this.cbcclCalcu.Location = new System.Drawing.Point(72, 84);
		this.cbcclCalcu.Name = "cbcclCalcu";
		this.cbcclCalcu.Size = new System.Drawing.Size(85, 20);
		this.cbcclCalcu.TabIndex = 6;
		this.gbcclRltTableReport.Controls.Add(this.rbrtrCaliPeaks);
		this.gbcclRltTableReport.Controls.Add(this.rbrtrIdentifiedPeaks);
		this.gbcclRltTableReport.Controls.Add(this.rbrtrAllDetectedPeaks);
		this.gbcclRltTableReport.Controls.Add(this.cbrtrHideISTDPeak);
		this.gbcclRltTableReport.Location = new System.Drawing.Point(289, 162);
		this.gbcclRltTableReport.Name = "gbcclRltTableReport";
		this.gbcclRltTableReport.Size = new System.Drawing.Size(201, 76);
		this.gbcclRltTableReport.TabIndex = 5;
		this.gbcclRltTableReport.TabStop = false;
		this.gbcclRltTableReport.Text = "结果表报告";
		this.rbrtrCaliPeaks.AutoSize = true;
		this.rbrtrCaliPeaks.Location = new System.Drawing.Point(103, 55);
		this.rbrtrCaliPeaks.Name = "rbrtrCaliPeaks";
		this.rbrtrCaliPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbrtrCaliPeaks.TabIndex = 0;
		this.rbrtrCaliPeaks.TabStop = true;
		this.rbrtrCaliPeaks.Text = "所有校正峰";
		this.rbrtrCaliPeaks.UseVisualStyleBackColor = true;
		this.rbrtrIdentifiedPeaks.AutoSize = true;
		this.rbrtrIdentifiedPeaks.Location = new System.Drawing.Point(103, 35);
		this.rbrtrIdentifiedPeaks.Name = "rbrtrIdentifiedPeaks";
		this.rbrtrIdentifiedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbrtrIdentifiedPeaks.TabIndex = 0;
		this.rbrtrIdentifiedPeaks.TabStop = true;
		this.rbrtrIdentifiedPeaks.Text = "所有识别峰";
		this.rbrtrIdentifiedPeaks.UseVisualStyleBackColor = true;
		this.rbrtrAllDetectedPeaks.AutoSize = true;
		this.rbrtrAllDetectedPeaks.Location = new System.Drawing.Point(103, 15);
		this.rbrtrAllDetectedPeaks.Name = "rbrtrAllDetectedPeaks";
		this.rbrtrAllDetectedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbrtrAllDetectedPeaks.TabIndex = 0;
		this.rbrtrAllDetectedPeaks.TabStop = true;
		this.rbrtrAllDetectedPeaks.Text = "所有检测峰";
		this.rbrtrAllDetectedPeaks.UseVisualStyleBackColor = true;
		this.cbrtrHideISTDPeak.AutoSize = true;
		this.cbrtrHideISTDPeak.Location = new System.Drawing.Point(6, 16);
		this.cbrtrHideISTDPeak.Name = "cbrtrHideISTDPeak";
		this.cbrtrHideISTDPeak.Size = new System.Drawing.Size(84, 16);
		this.cbrtrHideISTDPeak.TabIndex = 3;
		this.cbrtrHideISTDPeak.Text = "隐藏内标峰";
		this.cbrtrHideISTDPeak.UseVisualStyleBackColor = true;
		this.gbcclParas.Controls.Add(this.cbprsUncalBase);
		this.gbcclParas.Controls.Add(this.cbprsUseScaleFactor);
		this.gbcclParas.Controls.Add(this.lbprsUncalBase);
		this.gbcclParas.Controls.Add(this.lbprsUncalAmtRespFU);
		this.gbcclParas.Controls.Add(this.lbprsUncalAmtRespF);
		this.gbcclParas.Controls.Add(this.lbprsUnitAfterScale);
		this.gbcclParas.Controls.Add(this.lbprsScaleFactor);
		this.gbcclParas.Controls.Add(this.tbprsUncalAmtRespF);
		this.gbcclParas.Controls.Add(this.tbprsUnitAfterScale);
		this.gbcclParas.Controls.Add(this.tbprsScaleFactor);
		this.gbcclParas.Location = new System.Drawing.Point(289, 14);
		this.gbcclParas.Name = "gbcclParas";
		this.gbcclParas.Size = new System.Drawing.Size(201, 142);
		this.gbcclParas.TabIndex = 4;
		this.gbcclParas.TabStop = false;
		this.gbcclParas.Text = "参数";
		this.cbprsUncalBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbprsUncalBase.FormattingEnabled = true;
		this.cbprsUncalBase.ItemExtString = "";
		this.cbprsUncalBase.Location = new System.Drawing.Point(74, 92);
		this.cbprsUncalBase.Name = "cbprsUncalBase";
		this.cbprsUncalBase.Size = new System.Drawing.Size(60, 20);
		this.cbprsUncalBase.TabIndex = 4;
		this.cbprsUseScaleFactor.AutoSize = true;
		this.cbprsUseScaleFactor.Location = new System.Drawing.Point(6, 16);
		this.cbprsUseScaleFactor.Name = "cbprsUseScaleFactor";
		this.cbprsUseScaleFactor.Size = new System.Drawing.Size(96, 16);
		this.cbprsUseScaleFactor.TabIndex = 3;
		this.cbprsUseScaleFactor.Text = "使用缩放因子";
		this.cbprsUseScaleFactor.UseVisualStyleBackColor = true;
		this.cbprsUseScaleFactor.Click += new System.EventHandler(cbprsUseScaleFactor_Click);
		this.lbprsUncalBase.AutoSize = true;
		this.lbprsUncalBase.Location = new System.Drawing.Point(6, 96);
		this.lbprsUncalBase.Name = "lbprsUncalBase";
		this.lbprsUncalBase.Size = new System.Drawing.Size(65, 12);
		this.lbprsUncalBase.TabIndex = 0;
		this.lbprsUncalBase.Text = "未识别响应";
		this.lbprsUncalAmtRespFU.AutoSize = true;
		this.lbprsUncalAmtRespFU.Location = new System.Drawing.Point(135, 119);
		this.lbprsUncalAmtRespFU.Name = "lbprsUncalAmtRespFU";
		this.lbprsUncalAmtRespFU.Size = new System.Drawing.Size(65, 12);
		this.lbprsUncalAmtRespFU.TabIndex = 0;
		this.lbprsUncalAmtRespFU.Text = "[Amt/Resp]";
		this.lbprsUncalAmtRespF.AutoSize = true;
		this.lbprsUncalAmtRespF.Location = new System.Drawing.Point(6, 119);
		this.lbprsUncalAmtRespF.Name = "lbprsUncalAmtRespF";
		this.lbprsUncalAmtRespF.Size = new System.Drawing.Size(65, 12);
		this.lbprsUncalAmtRespF.TabIndex = 0;
		this.lbprsUncalAmtRespF.Text = "未识别因子";
		this.lbprsUnitAfterScale.AutoSize = true;
		this.lbprsUnitAfterScale.Location = new System.Drawing.Point(6, 64);
		this.lbprsUnitAfterScale.Name = "lbprsUnitAfterScale";
		this.lbprsUnitAfterScale.Size = new System.Drawing.Size(65, 12);
		this.lbprsUnitAfterScale.TabIndex = 0;
		this.lbprsUnitAfterScale.Text = "缩放后单位";
		this.lbprsScaleFactor.AutoSize = true;
		this.lbprsScaleFactor.Location = new System.Drawing.Point(6, 39);
		this.lbprsScaleFactor.Name = "lbprsScaleFactor";
		this.lbprsScaleFactor.Size = new System.Drawing.Size(53, 12);
		this.lbprsScaleFactor.TabIndex = 0;
		this.lbprsScaleFactor.Text = "缩放因子";
		this.tbprsUncalAmtRespF.Location = new System.Drawing.Point(75, 115);
		this.tbprsUncalAmtRespF.Name = "tbprsUncalAmtRespF";
		this.tbprsUncalAmtRespF.Size = new System.Drawing.Size(59, 21);
		this.tbprsUncalAmtRespF.TabIndex = 1;
		this.tbprsUnitAfterScale.Location = new System.Drawing.Point(75, 60);
		this.tbprsUnitAfterScale.Name = "tbprsUnitAfterScale";
		this.tbprsUnitAfterScale.Size = new System.Drawing.Size(59, 21);
		this.tbprsUnitAfterScale.TabIndex = 1;
		this.tbprsScaleFactor.Location = new System.Drawing.Point(75, 36);
		this.tbprsScaleFactor.Name = "tbprsScaleFactor";
		this.tbprsScaleFactor.Size = new System.Drawing.Size(59, 21);
		this.tbprsScaleFactor.TabIndex = 1;
		this.btncclSet.Location = new System.Drawing.Point(46, 46);
		this.btncclSet.Name = "btncclSet";
		this.btncclSet.Size = new System.Drawing.Size(75, 23);
		this.btncclSet.TabIndex = 3;
		this.btncclSet.Text = "设置";
		this.btncclSet.UseVisualStyleBackColor = true;
		this.btncclSet.Click += new System.EventHandler(btncclView_Click);
		this.btncclNone.Location = new System.Drawing.Point(127, 46);
		this.btncclNone.Name = "btncclNone";
		this.btncclNone.Size = new System.Drawing.Size(75, 23);
		this.btncclNone.TabIndex = 3;
		this.btncclNone.Text = "清空";
		this.btncclNone.UseVisualStyleBackColor = true;
		this.btncclNone.Click += new System.EventHandler(btncclView_Click);
		this.btncclView.Location = new System.Drawing.Point(208, 46);
		this.btncclView.Name = "btncclView";
		this.btncclView.Size = new System.Drawing.Size(75, 23);
		this.btncclView.TabIndex = 3;
		this.btncclView.Text = "查看";
		this.btncclView.UseVisualStyleBackColor = true;
		this.btncclView.Click += new System.EventHandler(btncclView_Click);
		this.tbcclCalibration.Location = new System.Drawing.Point(7, 22);
		this.tbcclCalibration.Name = "tbcclCalibration";
		this.tbcclCalibration.ReadOnly = true;
		this.tbcclCalibration.Size = new System.Drawing.Size(276, 21);
		this.tbcclCalibration.TabIndex = 2;
		this.lbcclModifiedTimeV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclModifiedTimeV.Location = new System.Drawing.Point(72, 225);
		this.lbcclModifiedTimeV.Name = "lbcclModifiedTimeV";
		this.lbcclModifiedTimeV.Size = new System.Drawing.Size(211, 19);
		this.lbcclModifiedTimeV.TabIndex = 1;
		this.lbcclModifiedTimeV.Text = "lclLabel1";
		this.lbcclModifiedTimeV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbcclModifiedTime.AutoSize = true;
		this.lbcclModifiedTime.Location = new System.Drawing.Point(3, 228);
		this.lbcclModifiedTime.Name = "lbcclModifiedTime";
		this.lbcclModifiedTime.Size = new System.Drawing.Size(53, 12);
		this.lbcclModifiedTime.TabIndex = 1;
		this.lbcclModifiedTime.Text = "修改时间";
		this.lbcclCreateTimeV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclCreateTimeV.Location = new System.Drawing.Point(72, 202);
		this.lbcclCreateTimeV.Name = "lbcclCreateTimeV";
		this.lbcclCreateTimeV.Size = new System.Drawing.Size(211, 19);
		this.lbcclCreateTimeV.TabIndex = 1;
		this.lbcclCreateTimeV.Text = "lclLabel1";
		this.lbcclCreateTimeV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbcclCreateTime.AutoSize = true;
		this.lbcclCreateTime.Location = new System.Drawing.Point(3, 206);
		this.lbcclCreateTime.Name = "lbcclCreateTime";
		this.lbcclCreateTime.Size = new System.Drawing.Size(53, 12);
		this.lbcclCreateTime.TabIndex = 1;
		this.lbcclCreateTime.Text = "创建时间";
		this.lbcclDescriptionV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclDescriptionV.Location = new System.Drawing.Point(72, 131);
		this.lbcclDescriptionV.Name = "lbcclDescriptionV";
		this.lbcclDescriptionV.Size = new System.Drawing.Size(211, 67);
		this.lbcclDescriptionV.TabIndex = 1;
		this.lbcclDescriptionV.Text = "lclLabel1";
		this.lbcclDescription.AutoSize = true;
		this.lbcclDescription.Location = new System.Drawing.Point(3, 135);
		this.lbcclDescription.Name = "lbcclDescription";
		this.lbcclDescription.Size = new System.Drawing.Size(29, 12);
		this.lbcclDescription.TabIndex = 1;
		this.lbcclDescription.Text = "描述";
		this.lbcclAuthorV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclAuthorV.Location = new System.Drawing.Point(72, 108);
		this.lbcclAuthorV.Name = "lbcclAuthorV";
		this.lbcclAuthorV.Size = new System.Drawing.Size(211, 19);
		this.lbcclAuthorV.TabIndex = 1;
		this.lbcclAuthorV.Text = "lclLabel1";
		this.lbcclAuthorV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbcclAuthor.AutoSize = true;
		this.lbcclAuthor.Location = new System.Drawing.Point(3, 111);
		this.lbcclAuthor.Name = "lbcclAuthor";
		this.lbcclAuthor.Size = new System.Drawing.Size(29, 12);
		this.lbcclAuthor.TabIndex = 1;
		this.lbcclAuthor.Text = "作者";
		this.lbcclCalcu.AutoSize = true;
		this.lbcclCalcu.Location = new System.Drawing.Point(5, 87);
		this.lbcclCalcu.Name = "lbcclCalcu";
		this.lbcclCalcu.Size = new System.Drawing.Size(29, 12);
		this.lbcclCalcu.TabIndex = 1;
		this.lbcclCalcu.Text = "计算";
		this.lbcclCalibration.AutoSize = true;
		this.lbcclCalibration.Location = new System.Drawing.Point(5, 7);
		this.lbcclCalibration.Name = "lbcclCalibration";
		this.lbcclCalibration.Size = new System.Drawing.Size(65, 12);
		this.lbcclCalibration.TabIndex = 1;
		this.lbcclCalibration.Text = "峰校正文件";
		this.tpAdvanced.Controls.Add(this.gbadvColumnCalcu);
		this.tpAdvanced.Controls.Add(this.gbadvAddSub);
		this.tpAdvanced.Location = new System.Drawing.Point(4, 23);
		this.tpAdvanced.Name = "tpAdvanced";
		this.tpAdvanced.Size = new System.Drawing.Size(448, 288);
		this.tpAdvanced.TabIndex = 10;
		this.tpAdvanced.Text = "高级";
		this.tpAdvanced.UseVisualStyleBackColor = true;
		this.gbadvColumnCalcu.Controls.Add(this.rbccFrom50per);
		this.gbadvColumnCalcu.Controls.Add(this.rbccStatistical);
		this.gbadvColumnCalcu.Controls.Add(this.tbccColumnLength);
		this.gbadvColumnCalcu.Controls.Add(this.lbccColumnLengthU);
		this.gbadvColumnCalcu.Controls.Add(this.tbccUnretainedPeak);
		this.gbadvColumnCalcu.Controls.Add(this.lbccColumnLength);
		this.gbadvColumnCalcu.Controls.Add(this.lbccUnretainedPeakU);
		this.gbadvColumnCalcu.Controls.Add(this.lbccUnretainedPeak);
		this.gbadvColumnCalcu.Location = new System.Drawing.Point(5, 126);
		this.gbadvColumnCalcu.Name = "gbadvColumnCalcu";
		this.gbadvColumnCalcu.Size = new System.Drawing.Size(238, 108);
		this.gbadvColumnCalcu.TabIndex = 0;
		this.gbadvColumnCalcu.TabStop = false;
		this.gbadvColumnCalcu.Text = "柱效计算";
		this.rbccFrom50per.AutoSize = true;
		this.rbccFrom50per.Checked = true;
		this.rbccFrom50per.Location = new System.Drawing.Point(32, 87);
		this.rbccFrom50per.Name = "rbccFrom50per";
		this.rbccFrom50per.Size = new System.Drawing.Size(77, 16);
		this.rbccFrom50per.TabIndex = 1;
		this.rbccFrom50per.TabStop = true;
		this.rbccFrom50per.Text = "50%宽起始";
		this.rbccFrom50per.UseVisualStyleBackColor = true;
		this.rbccStatistical.AutoSize = true;
		this.rbccStatistical.Enabled = false;
		this.rbccStatistical.Location = new System.Drawing.Point(32, 67);
		this.rbccStatistical.Name = "rbccStatistical";
		this.rbccStatistical.Size = new System.Drawing.Size(71, 16);
		this.rbccStatistical.TabIndex = 1;
		this.rbccStatistical.Text = "静态时间";
		this.rbccStatistical.UseVisualStyleBackColor = true;
		this.tbccColumnLength.Location = new System.Drawing.Point(122, 42);
		this.tbccColumnLength.Name = "tbccColumnLength";
		this.tbccColumnLength.Size = new System.Drawing.Size(59, 21);
		this.tbccColumnLength.TabIndex = 1;
		this.lbccColumnLengthU.AutoSize = true;
		this.lbccColumnLengthU.Location = new System.Drawing.Point(187, 46);
		this.lbccColumnLengthU.Name = "lbccColumnLengthU";
		this.lbccColumnLengthU.Size = new System.Drawing.Size(29, 12);
		this.lbccColumnLengthU.TabIndex = 1;
		this.lbccColumnLengthU.Text = "[mm]";
		this.tbccUnretainedPeak.Location = new System.Drawing.Point(122, 18);
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
		this.lbccUnretainedPeakU.Location = new System.Drawing.Point(187, 22);
		this.lbccUnretainedPeakU.Name = "lbccUnretainedPeakU";
		this.lbccUnretainedPeakU.Size = new System.Drawing.Size(35, 12);
		this.lbccUnretainedPeakU.TabIndex = 1;
		this.lbccUnretainedPeakU.Text = "[min]";
		this.lbccUnretainedPeak.AutoSize = true;
		this.lbccUnretainedPeak.Location = new System.Drawing.Point(6, 22);
		this.lbccUnretainedPeak.Name = "lbccUnretainedPeak";
		this.lbccUnretainedPeak.Size = new System.Drawing.Size(77, 12);
		this.lbccUnretainedPeak.TabIndex = 1;
		this.lbccUnretainedPeak.Text = "非保留峰时间";
		this.gbadvAddSub.Controls.Add(this.rbasSub);
		this.gbadvAddSub.Controls.Add(this.rbasAdd);
		this.gbadvAddSub.Controls.Add(this.cbasMatching);
		this.gbadvAddSub.Controls.Add(this.btnasSetChrom);
		this.gbadvAddSub.Controls.Add(this.btnasNoneChrom);
		this.gbadvAddSub.Controls.Add(this.tbasChrom);
		this.gbadvAddSub.Controls.Add(this.lbasMatching);
		this.gbadvAddSub.Controls.Add(this.lbasChrom);
		this.gbadvAddSub.Location = new System.Drawing.Point(5, 6);
		this.gbadvAddSub.Name = "gbadvAddSub";
		this.gbadvAddSub.Size = new System.Drawing.Size(277, 114);
		this.gbadvAddSub.TabIndex = 0;
		this.gbadvAddSub.TabStop = false;
		this.gbadvAddSub.Text = "加减谱图";
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
		this.cbasMatching.Location = new System.Drawing.Point(70, 63);
		this.cbasMatching.Name = "cbasMatching";
		this.cbasMatching.Size = new System.Drawing.Size(202, 20);
		this.cbasMatching.TabIndex = 4;
		this.btnasSetChrom.Location = new System.Drawing.Point(116, 86);
		this.btnasSetChrom.Name = "btnasSetChrom";
		this.btnasSetChrom.Size = new System.Drawing.Size(75, 23);
		this.btnasSetChrom.TabIndex = 3;
		this.btnasSetChrom.Text = "设置";
		this.btnasSetChrom.UseVisualStyleBackColor = true;
		this.btnasSetChrom.Click += new System.EventHandler(btnasSetChrom_Click);
		this.btnasNoneChrom.Location = new System.Drawing.Point(197, 86);
		this.btnasNoneChrom.Name = "btnasNoneChrom";
		this.btnasNoneChrom.Size = new System.Drawing.Size(75, 23);
		this.btnasNoneChrom.TabIndex = 3;
		this.btnasNoneChrom.Text = "置空";
		this.btnasNoneChrom.UseVisualStyleBackColor = true;
		this.btnasNoneChrom.Click += new System.EventHandler(btnasNoneChrom_Click);
		this.tbasChrom.Location = new System.Drawing.Point(70, 38);
		this.tbasChrom.Name = "tbasChrom";
		this.tbasChrom.ReadOnly = true;
		this.tbasChrom.Size = new System.Drawing.Size(202, 21);
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
		this.tabPage2.Controls.Add(this.groupBox2);
		this.tabPage2.Controls.Add(this.groupBox1);
		this.tabPage2.Location = new System.Drawing.Point(4, 23);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Size = new System.Drawing.Size(448, 288);
		this.tabPage2.TabIndex = 14;
		this.tabPage2.Text = "报告打印";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.rptbotom);
		this.groupBox2.Location = new System.Drawing.Point(4, 168);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(479, 100);
		this.groupBox2.TabIndex = 6;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "报告尾";
		this.rptbotom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rptbotom.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.rptbotom.Location = new System.Drawing.Point(3, 17);
		this.rptbotom.Multiline = true;
		this.rptbotom.Name = "rptbotom";
		this.rptbotom.Size = new System.Drawing.Size(473, 80);
		this.rptbotom.TabIndex = 0;
		this.rptbotom.Text = "备注：按GB10345-89检验，浓度含量单位：g/l\r\n检验部门：品控部\r\n检验员：检一015  018\r\n审核员：";
		this.groupBox1.Controls.Add(this.rpthead);
		this.groupBox1.Location = new System.Drawing.Point(6, 10);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(480, 152);
		this.groupBox1.TabIndex = 5;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "报告头";
		this.rpthead.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rpthead.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.rpthead.Location = new System.Drawing.Point(3, 17);
		this.rpthead.Multiline = true;
		this.rpthead.Name = "rpthead";
		this.rpthead.Size = new System.Drawing.Size(474, 132);
		this.rpthead.TabIndex = 0;
		this.rpthead.Text = "\r\n质检（E）字第（ \u3000）号\r\n送样单位：         \u3000\u3000                  仪器型号:\r\n取样日期：2012年  月  日                 收样日期：2012年  月  日\r\n样品批号：                               样品名称：固液\r\n样品罐号：A-1-2\r\n仪器控制参数文件：\r\n";
		this.panel2.Controls.Add(this.button5);
		this.panel2.Controls.Add(this.button4);
		this.panel2.Controls.Add(this.button2);
		this.panel2.Controls.Add(this.button1);
		this.panel2.Controls.Add(this.button3);
		this.panel2.Controls.Add(this.textBox1);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(3, 324);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(456, 77);
		this.panel2.TabIndex = 1;
		this.button5.Location = new System.Drawing.Point(293, 35);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(75, 23);
		this.button5.TabIndex = 10;
		this.button5.Text = "另存方法";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Visible = false;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.button4.Location = new System.Drawing.Point(145, 35);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 10;
		this.button4.Text = "保存方法";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Visible = false;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button2.Location = new System.Drawing.Point(4, 35);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 10;
		this.button2.Text = "新建方法";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button1.Location = new System.Drawing.Point(368, 11);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(84, 46);
		this.button1.TabIndex = 0;
		this.button1.Text = "应用设置";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button3.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.button3.Location = new System.Drawing.Point(337, 3);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(31, 32);
		this.button3.TabIndex = 9;
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Visible = false;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox1.Location = new System.Drawing.Point(73, 5);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(258, 21);
		this.textBox1.TabIndex = 8;
		this.textBox1.Text = "默认";
		this.textBox1.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(8, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 7;
		this.label1.Text = "打开方法:";
		this.label1.Visible = false;
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_0.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "下载.gif");
		this.imageList_0.Images.SetKeyName(1, "gif_47_091.gif");
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle.NullValue = false;
		this.dataGridViewCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle;
		this.dataGridViewCheckBoxColumn1.HeaderText = "时间校正";
		this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
		this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridViewCheckBoxColumn1.Width = 80;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridViewTextBoxColumn1.HeaderText = "套峰时间";
		this.dataGridViewTextBoxColumn1.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 80;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridViewTextBoxColumn2.HeaderText = "组份名称";
		this.dataGridViewTextBoxColumn2.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Width = 150;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridViewTextBoxColumn3.HeaderText = "时间窗";
		this.dataGridViewTextBoxColumn3.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 80;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle5;
		this.dataGridViewTextBoxColumn4.HeaderText = "组份名称";
		this.dataGridViewTextBoxColumn4.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.Width = 150;
		this.dataGridViewTextBoxColumn9.HeaderText = "ModBus地址";
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(462, 404);
		base.Controls.Add(this.tableLayoutPanel1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmDisposePara";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "谱图处理设置";
		base.TopMost = true;
		base.Load += new System.EventHandler(FrmDisposePara_Load);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.tcMethod.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		this.BaselineRemove.ResumeLayout(false);
		this.BaselineRemove.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.tpTempProg.ResumeLayout(false);
		this.tpIntegration.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvInteg).EndInit();
		this.cmsIntegration.ResumeLayout(false);
		this.tpCaculation.ResumeLayout(false);
		this.tpCaculation.PerformLayout();
		this.gbcclRltTableReport.ResumeLayout(false);
		this.gbcclRltTableReport.PerformLayout();
		this.gbcclParas.ResumeLayout(false);
		this.gbcclParas.PerformLayout();
		this.tpAdvanced.ResumeLayout(false);
		this.gbadvColumnCalcu.ResumeLayout(false);
		this.gbadvColumnCalcu.PerformLayout();
		this.gbadvAddSub.ResumeLayout(false);
		this.gbadvAddSub.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		base.ResumeLayout(false);
	}
}
