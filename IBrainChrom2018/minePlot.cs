using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;
using NPlot;
using NPlot.Windows;

namespace IBrainChrom2018;

public class minePlot : UserControl
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private MineParam mineParam = MineParam.Create();

	public static minePlot minePlotSelf;

	public ArrayList alFIDX1 = new ArrayList();

	public ArrayList alFIDY1 = new ArrayList();

	private ArrayList alFIDX2 = new ArrayList();

	private ArrayList alFIDY2 = new ArrayList();

	private ArrayList alTCDX2 = new ArrayList();

	private ArrayList alTCDY2 = new ArrayList();

	public LinePlot lp = new LinePlot();

	public LinePlot lp2 = new LinePlot();

	public LinePlot lp3 = new LinePlot();

	private byte bFID1 = 0;

	private byte bFID2 = 0;

	private byte bTCD2 = 0;

	public bool flagChannelOver1 = false;

	public bool flagChannelOver2 = false;

	public bool flagChannelOver3 = false;

	public float[] zufenAmount = new float[15];

	public string[] strZufenAmount = new string[15];

	public string[] zufenName = new string[15];

	public string strJidian = "手动进样";

	public int[] countZufen = new int[3];

	public int StateYiqi = 0;

	public long CountAnalyse = 0L;

	private DataTable dataSource;

	public bool bUpdateDateSource = false;

	private bool m_bLoading = true;

	private IContainer components = null;

	private SplitContainer splitContainer2;

	private SplitContainer splitContainer4;

	private SplitContainer splitContainer5;

	private SplitContainer splitContainer6;

	private Label labCycles;

	public Button bControlTemp;

	private GroupBox groupBox16;

	private Label label84;

	private Label label85;

	private Label label86;

	private Label label81;

	private Label label82;

	private Label label83;

	private Label label78;

	private Label label76;

	private Label label23;

	public MaskedTextBox tbPressure8;

	public MaskedTextBox tbPressure9;

	public MaskedTextBox tbPressure1;

	public MaskedTextBox tbPressure3;

	public MaskedTextBox tbPressure4;

	public MaskedTextBox tbPressure2;

	public MaskedTextBox tbPressure7;

	public MaskedTextBox tbPressure5;

	public MaskedTextBox tbPressure6;

	public Button button37;

	public Button button38;

	private CheckBox cbAutoheight;

	private Label label22;

	public PictureBox pictureBox1;

	public CheckBox cbAllChannelUnification;

	public MaskedTextBox maskedTextBox11;

	private Label label33;

	private Label label34;

	public Label labFireState;

	public MaskedTextBox maskedTextBox7;

	private DataGridView dataGridView1;

	private Splitter splitter1;

	private GroupBox groupBox15;

	private MaskedTextBox tbTime;

	private Label label32;

	private Button btnXYFull;

	public Label label21;

	private CheckBox cbDisPlayAll;

	private Label label31;

	public LclCheckBox lclCPauseRefresh;

	private MaskedTextBox tbSigYEnd;

	private CheckBox checkBox5;

	private CheckBox checkBox2;

	public MaskedTextBox maskedTextBox12;

	private Button btnTimeFull;

	public Label label29;

	private MaskedTextBox tbSigYBeg;

	private Label label27;

	private Label label20;

	private Button button25;

	private Label label119;

	private SplitContainer splitContainer3;

	private ToolStrip toolStrip1;

	public ToolStripButton tsStart;

	private ToolStripSeparator toolStripSeparator1;

	public ToolStripButton tsstop;

	private ToolStripButton toolStripButton6;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton toolStripButton4;

	private ToolStripButton toolStripButton5;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripButton toolStripButton7;

	private ToolStripButton toolStripButton1;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton toolStripButton10;

	private Label label116;

	private MaskedTextBox maskedTextBox13;

	private Label label115;

	private Button button26;

	private CheckBox checkBox3;

	private CheckBox cbFullScreem;

	public MaskedTextBox maskedTextBox6;

	public Button btnPrintDate;

	public Button btnCycle;

	public Button btnDecSet;

	public Button btnGiveUp;

	private Button button39;

	private Button btnShuGuan;

	public Button button14;

	private GroupBox groupBox9;

	private Label lbBTEXt;

	private Label lbBTEX;

	private Label lbBTEX8T;

	private Label lbBTEX7T;

	private Label lbNMHC;

	private Label lbBTEX9;

	private Label lbNMHCT;

	private Label lbCH4;

	private Label lbBTEX7;

	private Label label79;

	private Label lbBTEX2;

	private Label lbTHC;

	private Label lbBTEX5;

	private Label label77;

	private Label lbBTEX3;

	private Label lbBTEX9T;

	private Label lbBTEX1;

	private Label lbBTEX6;

	private Label lbBTEX2T;

	private Label lbBTEX3T;

	private Label lbBTEX8;

	private Label lbBTEX1T;

	private Label lbBTEX4;

	private Label lbBTEX5T;

	private Label lbBTEX4T;

	private Label lbBTEX6T;

	private Label labStateIns;

	private Button btShowDesktop;

	private Button button36;

	private Button NetConfig;

	private GroupBox groupBox2;

	private TableLayoutPanel tableLayoutPanel2;

	private Panel panel4;

	private Button button12;

	private RadioButton rdcs;

	private Button button32;

	private Button button23;

	private CheckBox checkBox4;

	private Label label24;

	public TextBox textBox3;

	private Button button5;

	private Button button1;

	private Label label25;

	public TextBox textBox2;

	private Label label26;

	public TextBox textBox1;

	private Panel panel5;

	public ListView InstrumlistView;

	private Button button2;

	public NPlot.Windows.PlotSurface2D plotSurfaceFID1;

	public SplitContainer splitContainer7;

	public NPlot.Windows.PlotSurface2D plotSurfaceFID2;

	public NPlot.Windows.PlotSurface2D plotSurfaceTCD2;

	private Button btnNewSamplingSite;

	private System.Windows.Forms.Timer timer1;

	private Label label1;

	private TextBox tbSamplingSite;

	public Button btnExplosion;

	private Button MethodSave;

	private Label label2;

	public Button MethodReSave;

	private Button MethodNew;

	private Button MethodOpen;

	private TextBox tbMethName;

	private Label label3;

	private Button MethodView;

	private MineChromCtrl mineChromCtrl1;

	private MineChromCtrl mineChromCtrl2;

	private MineChromCtrl mineChromCtrl3;

	public minePlot()
	{
		InitializeComponent();
		minePlotSelf = this;
		PlotWavelet();
		cbAutoheight.Checked = mineParam.bAutoheight;
		cbAllChannelUnification.Checked = mineParam.bcombexChannel;
		tbSamplingSite.Text = mineParam.strCurrentSamplingSite;
		try
		{
			DataTable dataTable = Class49.GetDataTable("select * from " + mineParam.strCurrentSamplingSite);
			DataRow dataRow = dataTable.AsEnumerable().First();
			dataGridView1.DataSource = dataTable;
			for (int i = 0; i < dataGridView1.ColumnCount - 3; i++)
			{
				dataGridView1.Columns[3 + i].HeaderText = dataRow[3 + i].ToString();
			}
			dataGridView1.Rows.RemoveAt(0);
			dataGridView1.AutoResizeColumns();
			bUpdateDateSource = true;
		}
		catch (Exception)
		{
		}
		tbSamplingSite.Text = mineParam.strCurrentSamplingSite;
		m_bLoading = false;
		timer1.Start();
	}

	public void PlotWavelet()
	{
		plotSurfaceFID1.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;
		plotSurfaceFID2.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;
		plotSurfaceTCD2.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;
		plotSurfaceFID1.Clear();
		plotSurfaceFID2.Clear();
		plotSurfaceTCD2.Clear();
		lp.AbscissaData = alFIDX1;
		lp.OrdinateData = alFIDY1;
		lp.Color = Color.Green;
		lp.Label = "Daubechies Wavelet";
		Grid grid = new Grid();
		grid.VerticalGridType = Grid.GridType.Fine;
		grid.HorizontalGridType = Grid.GridType.Coarse;
		plotSurfaceFID1.Add(grid);
		plotSurfaceFID1.Add(lp);
		plotSurfaceFID1.Title = "FID1";
		LinearAxis linearAxis = new LinearAxis(plotSurfaceFID1.YAxis1);
		linearAxis.NumberOfSmallTicks = 2;
		plotSurfaceFID1.YAxis1 = linearAxis;
		plotSurfaceFID1.YAxis1.WorldMax = 220.0;
		plotSurfaceFID1.YAxis1.WorldMin = -10.0;
		plotSurfaceFID1.XAxis1.WorldMax = 15.0;
		plotSurfaceFID1.PlotBackColor = Color.OldLace;
		plotSurfaceFID1.XAxis1.Reversed = false;
		plotSurfaceFID1.YAxis1.Reversed = false;
		plotSurfaceFID1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.RubberBandSelection());
		plotSurfaceFID1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(enableDragWithCtr: true));
		plotSurfaceFID1.Refresh();
		lp2.AbscissaData = alFIDX2;
		lp2.OrdinateData = alFIDY2;
		lp2.Color = Color.Green;
		lp2.Label = "Daubechies Wavelet";
		Grid grid2 = new Grid();
		grid2.VerticalGridType = Grid.GridType.Fine;
		grid2.HorizontalGridType = Grid.GridType.Coarse;
		plotSurfaceFID2.Add(grid2);
		plotSurfaceFID2.Add(lp2);
		plotSurfaceFID2.Title = "FID2";
		LinearAxis linearAxis2 = new LinearAxis(plotSurfaceFID2.YAxis1);
		linearAxis2.NumberOfSmallTicks = 2;
		plotSurfaceFID2.YAxis1 = linearAxis2;
		plotSurfaceFID2.YAxis1.WorldMax = 220.0;
		plotSurfaceFID2.YAxis1.WorldMin = 0.0;
		plotSurfaceFID2.XAxis1.WorldMax = 15.0;
		plotSurfaceFID2.XAxis1.AutoScaleTicks = true;
		plotSurfaceFID2.PlotBackColor = Color.OldLace;
		plotSurfaceFID2.XAxis1.Reversed = false;
		plotSurfaceFID2.YAxis1.Reversed = false;
		plotSurfaceFID2.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.RubberBandSelection());
		plotSurfaceFID2.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(enableDragWithCtr: true));
		plotSurfaceFID2.Refresh();
		lp3.AbscissaData = alTCDX2;
		lp3.OrdinateData = alTCDY2;
		lp3.Color = Color.Green;
		lp3.Label = "Daubechies Wavelet";
		Grid grid3 = new Grid();
		grid3.VerticalGridType = Grid.GridType.Fine;
		grid3.HorizontalGridType = Grid.GridType.Coarse;
		plotSurfaceTCD2.Add(grid3);
		plotSurfaceTCD2.Add(lp3);
		plotSurfaceTCD2.Title = "TCD2";
		LinearAxis linearAxis3 = new LinearAxis(plotSurfaceTCD2.YAxis1);
		linearAxis3.NumberOfSmallTicks = 2;
		plotSurfaceTCD2.YAxis1 = linearAxis3;
		plotSurfaceTCD2.YAxis1.WorldMax = 600.0;
		plotSurfaceTCD2.YAxis1.WorldMin = 0.0;
		plotSurfaceTCD2.XAxis1.WorldMax = 15.0;
		plotSurfaceTCD2.XAxis1.AutoScaleTicks = true;
		plotSurfaceTCD2.PlotBackColor = Color.OldLace;
		plotSurfaceTCD2.XAxis1.Reversed = false;
		plotSurfaceTCD2.YAxis1.Reversed = false;
		plotSurfaceTCD2.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.RubberBandSelection());
		plotSurfaceTCD2.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(enableDragWithCtr: true));
		plotSurfaceTCD2.Refresh();
	}

	public void addSignal(Signal signal, int channelIndex)
	{
		if (channelIndex == 0)
		{
			mineChromCtrl1.addSignal(signal);
		}
		if (channelIndex == 1)
		{
			mineChromCtrl2.addSignal(signal);
		}
		if (channelIndex == 2)
		{
			mineChromCtrl3.addSignal(signal);
		}
	}

	public void AddDots(float[] values, byte mark, ArrayList aldots)
	{
		float num = 0f;
		float num2 = 0f;
		if (maskedTextBox7.Text != "")
		{
			num = float.Parse(maskedTextBox7.Text);
		}
		num -= 0.05f;
		if (num < 0f)
		{
			num = 0f;
		}
		if (mark == 64)
		{
			alFIDX1.Clear();
			alFIDY1.Clear();
			ArrayList arrayList = (ArrayList)aldots[0];
			ArrayList arrayList2 = (ArrayList)aldots[1];
			if (num > 200f)
			{
				for (int i = 0; i < arrayList2.Count && (i <= 10 || Convert.ToSingle(arrayList[i]) != 0f); i++)
				{
					alFIDY1.Add(arrayList2[i]);
					num2 = (float)arrayList2[i];
					alFIDX1.Add(arrayList[i]);
				}
			}
			else
			{
				bFID1++;
				for (int j = 0; j < arrayList2.Count && (j <= 10 || Convert.ToSingle(arrayList[j]) != 0f); j++)
				{
					alFIDY1.Add(arrayList2[j]);
					num2 = (float)arrayList2[j];
					alFIDX1.Add(arrayList[j]);
				}
			}
			plotSurfaceFID1.Title = "FID1   " + num2.ToString("0.000");
		}
		if (mark == 65)
		{
			alFIDX2.Clear();
			alFIDY2.Clear();
			ArrayList arrayList3 = (ArrayList)aldots[0];
			ArrayList arrayList4 = (ArrayList)aldots[1];
			if (num > 200f)
			{
				for (int k = 0; k < arrayList4.Count && (k <= 10 || Convert.ToSingle(arrayList3[k]) != 0f); k++)
				{
					alFIDY2.Add(arrayList4[k]);
					num2 = (float)arrayList4[k];
					alFIDX2.Add(arrayList3[k]);
				}
			}
			else
			{
				bFID2++;
				for (int l = 0; l < arrayList4.Count && (l <= 10 || Convert.ToSingle(arrayList3[l]) != 0f); l++)
				{
					alFIDY2.Add(arrayList4[l]);
					num2 = (float)arrayList4[l];
					alFIDX2.Add(arrayList3[l]);
				}
			}
			plotSurfaceFID2.Title = "FID2   " + num2.ToString("0.000");
		}
		if (mark != 80)
		{
			return;
		}
		alTCDX2.Clear();
		alTCDY2.Clear();
		ArrayList arrayList5 = (ArrayList)aldots[0];
		ArrayList arrayList6 = (ArrayList)aldots[1];
		if (num > 200f)
		{
			for (int m = 0; m < arrayList6.Count && (m <= 10 || Convert.ToSingle(arrayList5[m]) != 0f); m++)
			{
				alTCDY2.Add(arrayList6[m]);
				num2 = (float)arrayList6[m];
				alTCDX2.Add(arrayList5[m]);
			}
		}
		else
		{
			bTCD2++;
			for (int n = 0; n < arrayList6.Count && (n <= 10 || Convert.ToSingle(arrayList5[n]) != 0f); n++)
			{
				alTCDY2.Add(arrayList6[n]);
				num2 = (float)arrayList6[n];
				alTCDX2.Add(arrayList5[n]);
			}
		}
		plotSurfaceTCD2.Title = "TCD2   " + num2.ToString("0.000");
	}

	private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
	{
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

	public void LoadChannelChartPara(ChannelChartPara oneEquipPara)
	{
		m_bLoading = true;
		maskedTextBox11.Text = oneEquipPara.stopTime.ToString();
		m_bLoading = false;
	}

	private void BControlTemp_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
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

	private void Button2_Click(object sender, EventArgs e)
	{
		cdlMgr.formMain.FrmEquip.RefreshTree();
		cdlMgr.formMain.FrmEquip.StartPosition = FormStartPosition.CenterScreen;
		cdlMgr.formMain.FrmEquip.TopMost = true;
		cdlMgr.formMain.FrmEquip.Show();
	}

	private void NetConfig_Click(object sender, EventArgs e)
	{
		NetSetForm netSetForm = new NetSetForm();
		netSetForm.StartPosition = FormStartPosition.CenterScreen;
		netSetForm.TopMost = true;
		netSetForm.Show();
	}

	private void BtnShuGuan_Click(object sender, EventArgs e)
	{
		FormSGSet formSGSet = new FormSGSet();
		formSGSet.StartPosition = FormStartPosition.CenterScreen;
		formSGSet.TopMost = true;
		formSGSet.Show();
	}

	private void CbAutoheight_CheckedChanged(object sender, EventArgs e)
	{
		mineParam.bAutoheight = cbAutoheight.Checked;
		mineParam.SaveParam();
	}

	private void BtnDecSet_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int fIDIndex = 2;
			currentTcpServerSocket.ShowDtcrForm(fIDIndex, "TCD1");
			Thread.Sleep(100);
			currentTcpServerSocket.SendCmd(13);
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
	}

	private void Button14_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				if (button14.Text == Lang.PS("开始分析", "StartAll"))
				{
					currentTcpServerSocket.SendCmd(18);
				}
				else
				{
					currentTcpServerSocket.SendCmd(19);
				}
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
		}
	}

	private void CbAllChannelUnification_CheckedChanged(object sender, EventArgs e)
	{
		mineParam.bcombexChannel = cbAllChannelUnification.Checked;
		mineParam.SaveParam();
	}

	public void calorificValue(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		int num = 0;
		float num2 = 0f;
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		Peak[] rltPeaks = chromatogram.RltPeaks;
		CaliGnl caliGnl2 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
		countZufen[0] = caliGnl2.cmpds.Length;
		for (int i = 0; i < countZufen[0]; i++)
		{
			mineParam.zufenName[i] = caliGnl2.cmpds[i].cmpdInfo.name;
		}
		CaliGnl caliGnl3 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
		countZufen[1] = caliGnl3.cmpds.Length;
		for (int j = 0; j < countZufen[1]; j++)
		{
			mineParam.zufenName[countZufen[0] + j] = caliGnl3.cmpds[j].cmpdInfo.name;
		}
		CaliGnl caliGnl4 = cdlMgr.ChartParaOperaList[2].mtdMgr.caliGnl;
		countZufen[2] = caliGnl4.cmpds.Length;
		for (int k = 0; k < countZufen[2]; k++)
		{
			mineParam.zufenName[countZufen[0] + countZufen[1] + k] = caliGnl4.cmpds[k].cmpdInfo.name;
			if (countZufen[0] + countZufen[1] + k == 14)
			{
				break;
			}
		}
		if (selectedIndex == 0 && rltPeaks.Length != 0)
		{
			for (int l = 0; l < caliGnl.cmpds.Length; l++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (!(rltPeaks[num].height < num2) && !(rltPeaks[num].name == ""))
					{
						if (rltPeaks[num].pkRT >= caliGnl.cmpds[l].cmpdInfo.retainTime - caliGnl.cmpds[l].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl.cmpds[l].cmpdInfo.retainTime + caliGnl.cmpds[l].cmpdInfo.rightWindow)
						{
							zufenAmount[l] = rltPeaks[num].amount;
							break;
						}
						zufenAmount[l] = 0f;
					}
					num++;
				}
			}
		}
		if (selectedIndex == 1 && rltPeaks.Length != 0)
		{
			for (int m = 0; m < caliGnl.cmpds.Length; m++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (!(rltPeaks[num].height < num2) && !(rltPeaks[num].name == ""))
					{
						if (rltPeaks[num].pkRT >= caliGnl.cmpds[m].cmpdInfo.retainTime - caliGnl.cmpds[m].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl.cmpds[m].cmpdInfo.retainTime + caliGnl.cmpds[m].cmpdInfo.rightWindow)
						{
							zufenAmount[countZufen[1] + m] = rltPeaks[num].amount;
							break;
						}
						zufenAmount[countZufen[1] + m] = 0f;
					}
					num++;
				}
			}
		}
		if (selectedIndex == 2 && rltPeaks.Length != 0)
		{
			for (int n = 0; n < caliGnl.cmpds.Length; n++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (!(rltPeaks[num].height < num2) && !(rltPeaks[num].name == ""))
					{
						if (rltPeaks[num].pkRT >= caliGnl.cmpds[n].cmpdInfo.retainTime - caliGnl.cmpds[n].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl.cmpds[n].cmpdInfo.retainTime + caliGnl.cmpds[n].cmpdInfo.rightWindow)
						{
							zufenAmount[countZufen[1] + countZufen[0] + n] = rltPeaks[num].amount;
							break;
						}
						zufenAmount[countZufen[1] + countZufen[0] + n] = 0f;
					}
					num++;
				}
			}
		}
		if (StateYiqi != 6)
		{
			StateYiqi = 5;
		}
		ushort[] dst = new ushort[2];
		long[] src = new long[1] { CountAnalyse * 10 + StateYiqi };
		Buffer.BlockCopy(src, 0, dst, 0, 4);
		LogMgr.Instance.Write2RunLog("FormMainVOC.disposeVOCPeaks   end");
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		bool flag = true;
		if (selectedIndex == 0)
		{
			flagChannelOver1 = true;
			calorificValue(0, fileName, strID, "1", chromatogram);
		}
		if (selectedIndex == 1)
		{
			flagChannelOver2 = true;
			calorificValue(1, fileName, strID, "1", chromatogram);
		}
		if (selectedIndex == 2)
		{
			flagChannelOver3 = true;
			calorificValue(2, fileName, strID, "1", chromatogram);
		}
		if (!flagChannelOver1 || !flagChannelOver2 || !flagChannelOver3)
		{
			return;
		}
		flagChannelOver1 = false;
		flagChannelOver2 = false;
		flagChannelOver3 = false;
		mineParam.SaveParam();
		if (cbAllChannelUnification.Checked)
		{
			float num = 0f;
			for (int i = 0; i < 15; i++)
			{
				num += zufenAmount[i];
			}
			if (num == 0f)
			{
				num = 1f;
			}
			for (int j = 0; j < 15; j++)
			{
				zufenAmount[j] /= num;
				zufenAmount[j] *= 100f;
			}
		}
		for (int k = 0; k < 15; k++)
		{
			strZufenAmount[k] = zufenAmount[k].ToString("0.0000");
		}
		Class49.InsertIntoMine(mineParam.iCurrentSamplingSiteCount, mineParam.strCurrentSamplingSite, 2, mineParam.zufenName, strZufenAmount, strJidian);
		bUpdateDateSource = true;
	}

	private void Button3_Click(object sender, EventArgs e)
	{
		Regex regex = new Regex("^[0-9]");
		if (regex.IsMatch(tbSamplingSite.Text.Trim()))
		{
			MessageBox.Show("采样点名字不能数字开头", "返回值 确定1", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (cdlMgr.ChartParaOperaList == null)
		{
			MessageBox.Show("没有联机无法新建采样点", "返回值 确定1", MessageBoxButtons.OK, MessageBoxIcon.Question);
			return;
		}
		CaliGnl caliGnl = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
		CaliGnl caliGnl2 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
		CaliGnl caliGnl3 = cdlMgr.ChartParaOperaList[2].mtdMgr.caliGnl;
		if (caliGnl.cmpds.Length == 0)
		{
			MessageBox.Show("通道:FID1没有加载组份表或组份表为0，请先加载组份表再建立采样点", "返回值 确定1", MessageBoxButtons.OK, MessageBoxIcon.Question);
			return;
		}
		if (caliGnl2.cmpds.Length == 0)
		{
			MessageBox.Show("通道:FID2没有加载组份表或组份表为0，请先加载组份表再建立采样点", "返回值 确定1", MessageBoxButtons.OK, MessageBoxIcon.Question);
			return;
		}
		if (caliGnl3.cmpds.Length == 0)
		{
			MessageBox.Show("通道:TCD1没有加载组份表或组份表为0，请先加载组份表再建立采样点", "返回值 确定1", MessageBoxButtons.OK, MessageBoxIcon.Question);
			return;
		}
		mineParam.iCurrentSamplingSiteCount = caliGnl.cmpds.Length + caliGnl2.cmpds.Length + caliGnl3.cmpds.Length;
		string text = " ";
		text = text + " CREATE TABLE [" + tbSamplingSite.Text.Trim() + "] ( ";
		text += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
		text += "[采样点] CHAR NOT NULL ON CONFLICT FAIL,  ";
		text += "[爆炸区域] CHAR NOT NULL ON CONFLICT FAIL,  ";
		for (int i = 0; i < caliGnl.cmpds.Length; i++)
		{
			text = text + "[" + i + "] CHAR NOT NULL ON CONFLICT FAIL,  ";
			mineParam.zufenName[i] = caliGnl.cmpds[i].cmpdInfo.name;
		}
		for (int j = 0; j < caliGnl2.cmpds.Length; j++)
		{
			text = text + "[" + (caliGnl.cmpds.Length + j) + "] CHAR NOT NULL ON CONFLICT FAIL,  ";
			mineParam.zufenName[caliGnl.cmpds.Length + j] = caliGnl.cmpds[j].cmpdInfo.name;
		}
		for (int k = 0; k < caliGnl3.cmpds.Length - 1; k++)
		{
			text = text + "[" + (caliGnl.cmpds.Length + caliGnl2.cmpds.Length + k) + "] CHAR NOT NULL ON CONFLICT FAIL,  ";
			mineParam.zufenName[caliGnl.cmpds.Length + caliGnl2.cmpds.Length + k] = caliGnl.cmpds[k].cmpdInfo.name;
		}
		text = text + "[" + (caliGnl.cmpds.Length + caliGnl2.cmpds.Length + caliGnl3.cmpds.Length - 1) + "] CHAR NOT NULL ON CONFLICT FAIL); ";
		mineParam.zufenName[caliGnl.cmpds.Length + caliGnl2.cmpds.Length + caliGnl3.cmpds.Length - 1] = caliGnl3.cmpds[caliGnl3.cmpds.Length - 1].cmpdInfo.name;
		countZufen[0] = caliGnl.cmpds.Length;
		for (int l = 0; l < countZufen[0]; l++)
		{
			mineParam.zufenName[l] = caliGnl.cmpds[l].cmpdInfo.name;
		}
		string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpol.dll';Version=3;";
		StringBuilder stringBuilder = new StringBuilder();
		string text2 = "";
		using SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString);
		sQLiteConnection.Open();
		string text3 = "select name from sqlite_master where type='table' order by name;";
		string commandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + tbSamplingSite.Text.Trim() + "';";
		SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
		if (Convert.ToInt32(sQLiteCommand.ExecuteScalar()) == 0)
		{
			string text4 = Application.StartupPath + "\\ngmpol.dll";
			SQLiteConnection sQLiteConnection2 = new SQLiteConnection("Data Source=" + text4);
			sQLiteConnection2.Open();
			SQLiteCommand sQLiteCommand2 = new SQLiteCommand(text, sQLiteConnection2);
			sQLiteCommand2.ExecuteNonQuery();
			sQLiteConnection2.Close();
			mineParam.strCurrentSamplingSite = tbSamplingSite.Text.Trim();
			mineParam.SaveParam();
			Class49.InsertIntoMine(mineParam.iCurrentSamplingSiteCount, mineParam.strCurrentSamplingSite, 2, mineParam.zufenName, mineParam.zufenName, strJidian);
			string strSql = "select * from " + mineParam.strCurrentSamplingSite;
			dataSource = Class49.GetDataTable(strSql);
			DataRow dataRow = dataSource.AsEnumerable().First();
			dataGridView1.DataSource = dataSource;
			for (int m = 0; m < dataGridView1.ColumnCount - 3; m++)
			{
				dataGridView1.Columns[3 + m].HeaderText = dataRow[3 + m].ToString();
			}
			dataGridView1.Rows.RemoveAt(0);
			dataGridView1.AutoResizeColumns();
		}
		else
		{
			mineParam.strCurrentSamplingSite = tbSamplingSite.Text.Trim();
			mineParam.SaveParam();
			DataTable dataTable = Class49.GetDataTable("select * from " + mineParam.strCurrentSamplingSite);
			dataGridView1.DataSource = dataTable;
		}
	}

	private void MaskedTextBox11_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			double num = Class49.String2Float(maskedTextBox11.Text.Trim(), -10000f);
			if (num == -10000.0)
			{
				maskedTextBox11.Text = "600";
			}
			else
			{
				MaskedTextBox11_DoubleClick(null, null);
			}
		}
	}

	private void MaskedTextBox11_DoubleClick(object sender, EventArgs e)
	{
		if (!m_bLoading && !(maskedTextBox11.Text.Trim() == ""))
		{
			float num = Class49.String2Float(maskedTextBox11.Text.Trim(), 600f);
			if (num > 9999f)
			{
				num = 9999f;
			}
			if ((double)num < 0.1)
			{
				num = 0.1f;
			}
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			if (currentTcpServerSocket != null)
			{
				currentTcpServerSocket.dtc_Channels[currentChannelIdx].chromInfoR.AcqRunTime = num;
			}
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			currentChannelChartPara.stopTime = num;
		}
	}

	private void Timer1_Tick(object sender, EventArgs e)
	{
		plotSurfaceFID1.Refresh();
		plotSurfaceFID2.Refresh();
		plotSurfaceTCD2.Refresh();
		if (bUpdateDateSource)
		{
			bUpdateDateSource = false;
			dataSource = Class49.GetDataTable("select * from " + mineParam.strCurrentSamplingSite);
			dataGridView1.DataSource = dataSource;
			dataGridView1.Rows.RemoveAt(0);
			dataGridView1.AutoResizeColumns();
		}
	}

	private void BtnPrintDate_Click(object sender, EventArgs e)
	{
		ExportReport exportReport = new ExportReport();
		exportReport.StartPosition = FormStartPosition.CenterScreen;
		exportReport.TopMost = true;
		exportReport.Show();
	}

	public void SaveCSV(DataTable dt, string fileName)
	{
		FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
		StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
		string text = "";
		for (int i = 0; i < dt.Columns.Count; i++)
		{
			text += dt.Columns[i].ColumnName.ToString();
			if (i < dt.Columns.Count - 1)
			{
				text += ",";
			}
		}
		streamWriter.WriteLine(text);
		for (int j = 0; j < dt.Rows.Count; j++)
		{
			text = "";
			for (int k = 0; k < dt.Columns.Count; k++)
			{
				text += dt.Rows[j][k].ToString();
				if (k < dt.Columns.Count - 1)
				{
					text += ",";
				}
			}
			streamWriter.WriteLine(text);
		}
		streamWriter.Close();
		fileStream.Close();
	}

	private void BtnExplosion_Click(object sender, EventArgs e)
	{
		FormExplosionDelta formExplosionDelta = new FormExplosionDelta();
		formExplosionDelta.StartPosition = FormStartPosition.CenterScreen;
		formExplosionDelta.TopMost = true;
		formExplosionDelta.Show();
	}

	private void TbSamplingSite_TextChanged(object sender, EventArgs e)
	{
	}

	private void MethodNew_Click(object sender, EventArgs e)
	{
		FormMineMethod formMineMethod = new FormMineMethod();
		formMineMethod.StartPosition = FormStartPosition.CenterScreen;
		formMineMethod.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.minePlot));
		System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem("色谱1", 19);
		System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("色谱2", 19);
		System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("色谱3", 6);
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.splitContainer4 = new System.Windows.Forms.SplitContainer();
		this.splitContainer5 = new System.Windows.Forms.SplitContainer();
		this.splitContainer6 = new System.Windows.Forms.SplitContainer();
		this.mineChromCtrl1 = new IBrainChrom2018.MineChromCtrl();
		this.mineChromCtrl2 = new IBrainChrom2018.MineChromCtrl();
		this.mineChromCtrl3 = new IBrainChrom2018.MineChromCtrl();
		this.splitContainer7 = new System.Windows.Forms.SplitContainer();
		this.label1 = new System.Windows.Forms.Label();
		this.tbSamplingSite = new System.Windows.Forms.TextBox();
		this.btnNewSamplingSite = new System.Windows.Forms.Button();
		this.labCycles = new System.Windows.Forms.Label();
		this.bControlTemp = new System.Windows.Forms.Button();
		this.groupBox16 = new System.Windows.Forms.GroupBox();
		this.label84 = new System.Windows.Forms.Label();
		this.label85 = new System.Windows.Forms.Label();
		this.label86 = new System.Windows.Forms.Label();
		this.label81 = new System.Windows.Forms.Label();
		this.label82 = new System.Windows.Forms.Label();
		this.label83 = new System.Windows.Forms.Label();
		this.label78 = new System.Windows.Forms.Label();
		this.label76 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.tbPressure8 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure9 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure1 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure3 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure4 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure2 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure7 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure5 = new System.Windows.Forms.MaskedTextBox();
		this.tbPressure6 = new System.Windows.Forms.MaskedTextBox();
		this.button37 = new System.Windows.Forms.Button();
		this.button38 = new System.Windows.Forms.Button();
		this.cbAutoheight = new System.Windows.Forms.CheckBox();
		this.label22 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.cbAllChannelUnification = new System.Windows.Forms.CheckBox();
		this.maskedTextBox11 = new System.Windows.Forms.MaskedTextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.labFireState = new System.Windows.Forms.Label();
		this.maskedTextBox7 = new System.Windows.Forms.MaskedTextBox();
		this.plotSurfaceTCD2 = new NPlot.Windows.PlotSurface2D();
		this.plotSurfaceFID2 = new NPlot.Windows.PlotSurface2D();
		this.plotSurfaceFID1 = new NPlot.Windows.PlotSurface2D();
		this.MethodView = new System.Windows.Forms.Button();
		this.MethodSave = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.MethodReSave = new System.Windows.Forms.Button();
		this.MethodNew = new System.Windows.Forms.Button();
		this.MethodOpen = new System.Windows.Forms.Button();
		this.tbMethName = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.groupBox15 = new System.Windows.Forms.GroupBox();
		this.tbTime = new System.Windows.Forms.MaskedTextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.btnXYFull = new System.Windows.Forms.Button();
		this.label21 = new System.Windows.Forms.Label();
		this.cbDisPlayAll = new System.Windows.Forms.CheckBox();
		this.label31 = new System.Windows.Forms.Label();
		this.lclCPauseRefresh = new IBrainChrom2018.LclCheckBox();
		this.tbSigYEnd = new System.Windows.Forms.MaskedTextBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.maskedTextBox12 = new System.Windows.Forms.MaskedTextBox();
		this.btnTimeFull = new System.Windows.Forms.Button();
		this.label29 = new System.Windows.Forms.Label();
		this.tbSigYBeg = new System.Windows.Forms.MaskedTextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.button25 = new System.Windows.Forms.Button();
		this.label119 = new System.Windows.Forms.Label();
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.tsStart = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.tsstop = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
		this.label116 = new System.Windows.Forms.Label();
		this.maskedTextBox13 = new System.Windows.Forms.MaskedTextBox();
		this.label115 = new System.Windows.Forms.Label();
		this.button26 = new System.Windows.Forms.Button();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.cbFullScreem = new System.Windows.Forms.CheckBox();
		this.maskedTextBox6 = new System.Windows.Forms.MaskedTextBox();
		this.btnExplosion = new System.Windows.Forms.Button();
		this.btnPrintDate = new System.Windows.Forms.Button();
		this.btnCycle = new System.Windows.Forms.Button();
		this.btnDecSet = new System.Windows.Forms.Button();
		this.btnGiveUp = new System.Windows.Forms.Button();
		this.button39 = new System.Windows.Forms.Button();
		this.btnShuGuan = new System.Windows.Forms.Button();
		this.button14 = new System.Windows.Forms.Button();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.lbBTEXt = new System.Windows.Forms.Label();
		this.lbBTEX = new System.Windows.Forms.Label();
		this.lbBTEX8T = new System.Windows.Forms.Label();
		this.lbBTEX7T = new System.Windows.Forms.Label();
		this.lbNMHC = new System.Windows.Forms.Label();
		this.lbBTEX9 = new System.Windows.Forms.Label();
		this.lbNMHCT = new System.Windows.Forms.Label();
		this.lbCH4 = new System.Windows.Forms.Label();
		this.lbBTEX7 = new System.Windows.Forms.Label();
		this.label79 = new System.Windows.Forms.Label();
		this.lbBTEX2 = new System.Windows.Forms.Label();
		this.lbTHC = new System.Windows.Forms.Label();
		this.lbBTEX5 = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.lbBTEX3 = new System.Windows.Forms.Label();
		this.lbBTEX9T = new System.Windows.Forms.Label();
		this.lbBTEX1 = new System.Windows.Forms.Label();
		this.lbBTEX6 = new System.Windows.Forms.Label();
		this.lbBTEX2T = new System.Windows.Forms.Label();
		this.lbBTEX3T = new System.Windows.Forms.Label();
		this.lbBTEX8 = new System.Windows.Forms.Label();
		this.lbBTEX1T = new System.Windows.Forms.Label();
		this.lbBTEX4 = new System.Windows.Forms.Label();
		this.lbBTEX5T = new System.Windows.Forms.Label();
		this.lbBTEX4T = new System.Windows.Forms.Label();
		this.lbBTEX6T = new System.Windows.Forms.Label();
		this.labStateIns = new System.Windows.Forms.Label();
		this.btShowDesktop = new System.Windows.Forms.Button();
		this.button36 = new System.Windows.Forms.Button();
		this.NetConfig = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.button12 = new System.Windows.Forms.Button();
		this.rdcs = new System.Windows.Forms.RadioButton();
		this.button32 = new System.Windows.Forms.Button();
		this.button23 = new System.Windows.Forms.Button();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.label24 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.button5 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.label25 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.InstrumlistView = new System.Windows.Forms.ListView();
		this.button2 = new System.Windows.Forms.Button();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer4).BeginInit();
		this.splitContainer4.Panel1.SuspendLayout();
		this.splitContainer4.Panel2.SuspendLayout();
		this.splitContainer4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer5).BeginInit();
		this.splitContainer5.Panel1.SuspendLayout();
		this.splitContainer5.Panel2.SuspendLayout();
		this.splitContainer5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer6).BeginInit();
		this.splitContainer6.Panel1.SuspendLayout();
		this.splitContainer6.Panel2.SuspendLayout();
		this.splitContainer6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer7).BeginInit();
		this.splitContainer7.Panel1.SuspendLayout();
		this.splitContainer7.Panel2.SuspendLayout();
		this.splitContainer7.SuspendLayout();
		this.groupBox16.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox15.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
		this.splitContainer3.Panel1.SuspendLayout();
		this.splitContainer3.Panel2.SuspendLayout();
		this.splitContainer3.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		this.groupBox9.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
		this.splitContainer2.Panel1.Controls.Add(this.splitter1);
		this.splitContainer2.Panel1.Controls.Add(this.groupBox15);
		this.splitContainer2.Panel2.Controls.Add(this.btnExplosion);
		this.splitContainer2.Panel2.Controls.Add(this.btnPrintDate);
		this.splitContainer2.Panel2.Controls.Add(this.btnCycle);
		this.splitContainer2.Panel2.Controls.Add(this.btnDecSet);
		this.splitContainer2.Panel2.Controls.Add(this.btnGiveUp);
		this.splitContainer2.Panel2.Controls.Add(this.button39);
		this.splitContainer2.Panel2.Controls.Add(this.btnShuGuan);
		this.splitContainer2.Panel2.Controls.Add(this.button14);
		this.splitContainer2.Panel2.Controls.Add(this.groupBox9);
		this.splitContainer2.Panel2.Controls.Add(this.labStateIns);
		this.splitContainer2.Panel2.Controls.Add(this.btShowDesktop);
		this.splitContainer2.Panel2.Controls.Add(this.button36);
		this.splitContainer2.Panel2.Controls.Add(this.NetConfig);
		this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
		this.splitContainer2.Panel2.Controls.Add(this.button2);
		this.splitContainer2.Size = new System.Drawing.Size(1285, 784);
		this.splitContainer2.SplitterDistance = 709;
		this.splitContainer2.TabIndex = 1;
		this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer4.Location = new System.Drawing.Point(3, 0);
		this.splitContainer4.Name = "splitContainer4";
		this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
		this.splitContainer4.Panel2.Controls.Add(this.splitContainer7);
		this.splitContainer4.Size = new System.Drawing.Size(1280, 707);
		this.splitContainer4.SplitterDistance = 926;
		this.splitContainer4.TabIndex = 66;
		this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer5.Location = new System.Drawing.Point(0, 0);
		this.splitContainer5.Name = "splitContainer5";
		this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
		this.splitContainer5.Panel2.Controls.Add(this.plotSurfaceTCD2);
		this.splitContainer5.Size = new System.Drawing.Size(926, 707);
		this.splitContainer5.SplitterDistance = 481;
		this.splitContainer5.TabIndex = 0;
		this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer6.Location = new System.Drawing.Point(0, 0);
		this.splitContainer6.Name = "splitContainer6";
		this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer6.Panel1.Controls.Add(this.plotSurfaceFID1);
		this.splitContainer6.Panel2.Controls.Add(this.plotSurfaceFID2);
		this.splitContainer6.Size = new System.Drawing.Size(926, 481);
		this.splitContainer6.SplitterDistance = 254;
		this.splitContainer6.TabIndex = 0;
		this.mineChromCtrl1.Location = new System.Drawing.Point(97, 109);
		this.mineChromCtrl1.Name = "mineChromCtrl1";
		this.mineChromCtrl1.ShowManuAndStateBar = true;
		this.mineChromCtrl1.ShowOnlineMethod = false;
		this.mineChromCtrl1.Size = new System.Drawing.Size(128, 114);
		this.mineChromCtrl1.TabIndex = 0;
		this.mineChromCtrl1.Visible = false;
		this.mineChromCtrl2.Location = new System.Drawing.Point(158, 160);
		this.mineChromCtrl2.Name = "mineChromCtrl2";
		this.mineChromCtrl2.ShowManuAndStateBar = true;
		this.mineChromCtrl2.ShowOnlineMethod = false;
		this.mineChromCtrl2.Size = new System.Drawing.Size(114, 103);
		this.mineChromCtrl2.TabIndex = 1;
		this.mineChromCtrl2.Visible = false;
		this.mineChromCtrl3.Location = new System.Drawing.Point(72, 190);
		this.mineChromCtrl3.Name = "mineChromCtrl3";
		this.mineChromCtrl3.ShowManuAndStateBar = true;
		this.mineChromCtrl3.ShowOnlineMethod = false;
		this.mineChromCtrl3.Size = new System.Drawing.Size(127, 143);
		this.mineChromCtrl3.TabIndex = 1;
		this.mineChromCtrl3.Visible = false;
		this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer7.Location = new System.Drawing.Point(0, 0);
		this.splitContainer7.Name = "splitContainer7";
		this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer7.Panel1.Controls.Add(this.label1);
		this.splitContainer7.Panel1.Controls.Add(this.tbSamplingSite);
		this.splitContainer7.Panel1.Controls.Add(this.btnNewSamplingSite);
		this.splitContainer7.Panel1.Controls.Add(this.labCycles);
		this.splitContainer7.Panel1.Controls.Add(this.bControlTemp);
		this.splitContainer7.Panel1.Controls.Add(this.groupBox16);
		this.splitContainer7.Panel1.Controls.Add(this.button37);
		this.splitContainer7.Panel1.Controls.Add(this.button38);
		this.splitContainer7.Panel1.Controls.Add(this.cbAutoheight);
		this.splitContainer7.Panel1.Controls.Add(this.label22);
		this.splitContainer7.Panel1.Controls.Add(this.pictureBox1);
		this.splitContainer7.Panel1.Controls.Add(this.cbAllChannelUnification);
		this.splitContainer7.Panel1.Controls.Add(this.maskedTextBox11);
		this.splitContainer7.Panel1.Controls.Add(this.label33);
		this.splitContainer7.Panel1.Controls.Add(this.label34);
		this.splitContainer7.Panel1.Controls.Add(this.labFireState);
		this.splitContainer7.Panel1.Controls.Add(this.maskedTextBox7);
		this.splitContainer7.Panel2.Controls.Add(this.mineChromCtrl2);
		this.splitContainer7.Panel2.Controls.Add(this.mineChromCtrl3);
		this.splitContainer7.Panel2.Controls.Add(this.mineChromCtrl1);
		this.splitContainer7.Panel2.Controls.Add(this.MethodView);
		this.splitContainer7.Panel2.Controls.Add(this.MethodSave);
		this.splitContainer7.Panel2.Controls.Add(this.label2);
		this.splitContainer7.Panel2.Controls.Add(this.MethodReSave);
		this.splitContainer7.Panel2.Controls.Add(this.MethodNew);
		this.splitContainer7.Panel2.Controls.Add(this.MethodOpen);
		this.splitContainer7.Panel2.Controls.Add(this.tbMethName);
		this.splitContainer7.Panel2.Controls.Add(this.label3);
		this.splitContainer7.Panel2.Controls.Add(this.dataGridView1);
		this.splitContainer7.Size = new System.Drawing.Size(350, 707);
		this.splitContainer7.SplitterDistance = 301;
		this.splitContainer7.TabIndex = 0;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(21, 276);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 81;
		this.label1.Text = "采样点：";
		this.tbSamplingSite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.tbSamplingSite.Location = new System.Drawing.Point(80, 272);
		this.tbSamplingSite.Name = "tbSamplingSite";
		this.tbSamplingSite.Size = new System.Drawing.Size(100, 21);
		this.tbSamplingSite.TabIndex = 80;
		this.tbSamplingSite.TextChanged += new System.EventHandler(TbSamplingSite_TextChanged);
		this.btnNewSamplingSite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNewSamplingSite.Location = new System.Drawing.Point(197, 270);
		this.btnNewSamplingSite.Name = "btnNewSamplingSite";
		this.btnNewSamplingSite.Size = new System.Drawing.Size(75, 23);
		this.btnNewSamplingSite.TabIndex = 79;
		this.btnNewSamplingSite.Text = "新建采样点";
		this.btnNewSamplingSite.UseVisualStyleBackColor = true;
		this.btnNewSamplingSite.Click += new System.EventHandler(Button3_Click);
		this.labCycles.AutoSize = true;
		this.labCycles.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labCycles.Location = new System.Drawing.Point(13, 239);
		this.labCycles.Name = "labCycles";
		this.labCycles.Size = new System.Drawing.Size(20, 20);
		this.labCycles.TabIndex = 78;
		this.labCycles.Text = "0";
		this.bControlTemp.BackColor = System.Drawing.Color.Transparent;
		this.bControlTemp.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.bControlTemp.Location = new System.Drawing.Point(257, 40);
		this.bControlTemp.Name = "bControlTemp";
		this.bControlTemp.Size = new System.Drawing.Size(77, 49);
		this.bControlTemp.TabIndex = 1;
		this.bControlTemp.Text = "开始控温";
		this.bControlTemp.UseVisualStyleBackColor = false;
		this.bControlTemp.Click += new System.EventHandler(BControlTemp_Click);
		this.groupBox16.Controls.Add(this.label84);
		this.groupBox16.Controls.Add(this.label85);
		this.groupBox16.Controls.Add(this.label86);
		this.groupBox16.Controls.Add(this.label81);
		this.groupBox16.Controls.Add(this.label82);
		this.groupBox16.Controls.Add(this.label83);
		this.groupBox16.Controls.Add(this.label78);
		this.groupBox16.Controls.Add(this.label76);
		this.groupBox16.Controls.Add(this.label23);
		this.groupBox16.Controls.Add(this.tbPressure8);
		this.groupBox16.Controls.Add(this.tbPressure9);
		this.groupBox16.Controls.Add(this.tbPressure1);
		this.groupBox16.Controls.Add(this.tbPressure3);
		this.groupBox16.Controls.Add(this.tbPressure4);
		this.groupBox16.Controls.Add(this.tbPressure2);
		this.groupBox16.Controls.Add(this.tbPressure7);
		this.groupBox16.Controls.Add(this.tbPressure5);
		this.groupBox16.Controls.Add(this.tbPressure6);
		this.groupBox16.Location = new System.Drawing.Point(13, 97);
		this.groupBox16.Name = "groupBox16";
		this.groupBox16.Size = new System.Drawing.Size(321, 100);
		this.groupBox16.TabIndex = 77;
		this.groupBox16.TabStop = false;
		this.groupBox16.Text = "压力(psi)";
		this.label84.AutoSize = true;
		this.label84.Location = new System.Drawing.Point(219, 75);
		this.label84.Name = "label84";
		this.label84.Size = new System.Drawing.Size(29, 12);
		this.label84.TabIndex = 85;
		this.label84.Text = "备用";
		this.label85.AutoSize = true;
		this.label85.Location = new System.Drawing.Point(219, 53);
		this.label85.Name = "label85";
		this.label85.Size = new System.Drawing.Size(35, 12);
		this.label85.TabIndex = 84;
		this.label85.Text = "空气2";
		this.label86.AutoSize = true;
		this.label86.Location = new System.Drawing.Point(218, 26);
		this.label86.Name = "label86";
		this.label86.Size = new System.Drawing.Size(35, 12);
		this.label86.TabIndex = 83;
		this.label86.Text = "空气1";
		this.label81.AutoSize = true;
		this.label81.Location = new System.Drawing.Point(119, 75);
		this.label81.Name = "label81";
		this.label81.Size = new System.Drawing.Size(35, 12);
		this.label81.TabIndex = 82;
		this.label81.Text = "氢气2";
		this.label82.AutoSize = true;
		this.label82.Location = new System.Drawing.Point(119, 53);
		this.label82.Name = "label82";
		this.label82.Size = new System.Drawing.Size(35, 12);
		this.label82.TabIndex = 81;
		this.label82.Text = "氢气1";
		this.label83.AutoSize = true;
		this.label83.Location = new System.Drawing.Point(118, 26);
		this.label83.Name = "label83";
		this.label83.Size = new System.Drawing.Size(35, 12);
		this.label83.TabIndex = 80;
		this.label83.Text = "载气4";
		this.label78.AutoSize = true;
		this.label78.Location = new System.Drawing.Point(15, 75);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(35, 12);
		this.label78.TabIndex = 79;
		this.label78.Text = "载气3";
		this.label76.AutoSize = true;
		this.label76.Location = new System.Drawing.Point(15, 53);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(35, 12);
		this.label76.TabIndex = 78;
		this.label76.Text = "载气2";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(14, 26);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(35, 12);
		this.label23.TabIndex = 77;
		this.label23.Text = "载气1";
		this.tbPressure8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure8.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure8.Location = new System.Drawing.Point(262, 47);
		this.tbPressure8.Name = "tbPressure8";
		this.tbPressure8.ReadOnly = true;
		this.tbPressure8.Size = new System.Drawing.Size(52, 21);
		this.tbPressure8.TabIndex = 66;
		this.tbPressure8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure9.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure9.Location = new System.Drawing.Point(263, 73);
		this.tbPressure9.Name = "tbPressure9";
		this.tbPressure9.ReadOnly = true;
		this.tbPressure9.Size = new System.Drawing.Size(52, 21);
		this.tbPressure9.TabIndex = 68;
		this.tbPressure9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure1.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure1.Location = new System.Drawing.Point(56, 20);
		this.tbPressure1.Name = "tbPressure1";
		this.tbPressure1.ReadOnly = true;
		this.tbPressure1.Size = new System.Drawing.Size(52, 21);
		this.tbPressure1.TabIndex = 60;
		this.tbPressure1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure3.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure3.Location = new System.Drawing.Point(56, 71);
		this.tbPressure3.Name = "tbPressure3";
		this.tbPressure3.ReadOnly = true;
		this.tbPressure3.Size = new System.Drawing.Size(52, 21);
		this.tbPressure3.TabIndex = 62;
		this.tbPressure3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure4.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure4.Location = new System.Drawing.Point(160, 17);
		this.tbPressure4.Name = "tbPressure4";
		this.tbPressure4.ReadOnly = true;
		this.tbPressure4.Size = new System.Drawing.Size(52, 21);
		this.tbPressure4.TabIndex = 72;
		this.tbPressure4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure2.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure2.Location = new System.Drawing.Point(56, 47);
		this.tbPressure2.Name = "tbPressure2";
		this.tbPressure2.ReadOnly = true;
		this.tbPressure2.Size = new System.Drawing.Size(52, 21);
		this.tbPressure2.TabIndex = 70;
		this.tbPressure2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure7.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure7.Location = new System.Drawing.Point(262, 20);
		this.tbPressure7.Name = "tbPressure7";
		this.tbPressure7.ReadOnly = true;
		this.tbPressure7.Size = new System.Drawing.Size(52, 21);
		this.tbPressure7.TabIndex = 76;
		this.tbPressure7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure5.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure5.Location = new System.Drawing.Point(160, 44);
		this.tbPressure5.Name = "tbPressure5";
		this.tbPressure5.ReadOnly = true;
		this.tbPressure5.Size = new System.Drawing.Size(52, 21);
		this.tbPressure5.TabIndex = 64;
		this.tbPressure5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tbPressure6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbPressure6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPressure6.ForeColor = System.Drawing.Color.Blue;
		this.tbPressure6.Location = new System.Drawing.Point(160, 71);
		this.tbPressure6.Name = "tbPressure6";
		this.tbPressure6.ReadOnly = true;
		this.tbPressure6.Size = new System.Drawing.Size(52, 21);
		this.tbPressure6.TabIndex = 74;
		this.tbPressure6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.button37.Image = (System.Drawing.Image)resources.GetObject("button37.Image");
		this.button37.Location = new System.Drawing.Point(15, 35);
		this.button37.Name = "button37";
		this.button37.Size = new System.Drawing.Size(47, 25);
		this.button37.TabIndex = 10;
		this.button37.UseVisualStyleBackColor = true;
		this.button38.Image = (System.Drawing.Image)resources.GetObject("button38.Image");
		this.button38.Location = new System.Drawing.Point(60, 35);
		this.button38.Name = "button38";
		this.button38.Size = new System.Drawing.Size(45, 26);
		this.button38.TabIndex = 9;
		this.button38.UseVisualStyleBackColor = true;
		this.cbAutoheight.AutoSize = true;
		this.cbAutoheight.Location = new System.Drawing.Point(13, 206);
		this.cbAutoheight.Name = "cbAutoheight";
		this.cbAutoheight.Size = new System.Drawing.Size(84, 16);
		this.cbAutoheight.TabIndex = 9;
		this.cbAutoheight.Text = "峰高自适应";
		this.cbAutoheight.UseVisualStyleBackColor = true;
		this.cbAutoheight.CheckedChanged += new System.EventHandler(CbAutoheight_CheckedChanged);
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(221, 77);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(23, 12);
		this.label22.TabIndex = 3;
		this.label22.Text = "min";
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(133, 40);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(20, 20);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.cbAllChannelUnification.AutoSize = true;
		this.cbAllChannelUnification.Location = new System.Drawing.Point(119, 206);
		this.cbAllChannelUnification.Name = "cbAllChannelUnification";
		this.cbAllChannelUnification.Size = new System.Drawing.Size(96, 16);
		this.cbAllChannelUnification.TabIndex = 59;
		this.cbAllChannelUnification.Text = "三通道归一化";
		this.cbAllChannelUnification.UseVisualStyleBackColor = true;
		this.cbAllChannelUnification.CheckedChanged += new System.EventHandler(CbAllChannelUnification_CheckedChanged);
		this.maskedTextBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox11.ForeColor = System.Drawing.Color.Red;
		this.maskedTextBox11.Location = new System.Drawing.Point(82, 70);
		this.maskedTextBox11.Name = "maskedTextBox11";
		this.maskedTextBox11.PromptChar = ' ';
		this.maskedTextBox11.Size = new System.Drawing.Size(53, 21);
		this.maskedTextBox11.TabIndex = 2;
		this.maskedTextBox11.Text = "45";
		this.maskedTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.maskedTextBox11.TextChanged += new System.EventHandler(MaskedTextBox11_TextChanged);
		this.maskedTextBox11.DoubleClick += new System.EventHandler(MaskedTextBox11_DoubleClick);
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(134, 73);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(23, 12);
		this.label33.TabIndex = 3;
		this.label33.Text = "min";
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label34.ForeColor = System.Drawing.Color.Black;
		this.label34.Location = new System.Drawing.Point(17, 73);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(64, 12);
		this.label34.TabIndex = 0;
		this.label34.Text = "停止时间:";
		this.labFireState.AutoSize = true;
		this.labFireState.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labFireState.Location = new System.Drawing.Point(172, 42);
		this.labFireState.Name = "labFireState";
		this.labFireState.Size = new System.Drawing.Size(76, 16);
		this.labFireState.TabIndex = 12;
		this.labFireState.Text = "仪器启动";
		this.maskedTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox7.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox7.Location = new System.Drawing.Point(163, 70);
		this.maskedTextBox7.Name = "maskedTextBox7";
		this.maskedTextBox7.ReadOnly = true;
		this.maskedTextBox7.Size = new System.Drawing.Size(52, 21);
		this.maskedTextBox7.TabIndex = 2;
		this.maskedTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.plotSurfaceTCD2.AutoScaleAutoGeneratedAxes = false;
		this.plotSurfaceTCD2.AutoScaleTitle = false;
		this.plotSurfaceTCD2.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.plotSurfaceTCD2.DateTimeToolTip = false;
		this.plotSurfaceTCD2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.plotSurfaceTCD2.Legend = null;
		this.plotSurfaceTCD2.LegendZOrder = -1;
		this.plotSurfaceTCD2.Location = new System.Drawing.Point(0, 0);
		this.plotSurfaceTCD2.Name = "plotSurfaceTCD2";
		this.plotSurfaceTCD2.RightMenu = null;
		this.plotSurfaceTCD2.ShowCoordinates = false;
		this.plotSurfaceTCD2.Size = new System.Drawing.Size(926, 222);
		this.plotSurfaceTCD2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
		this.plotSurfaceTCD2.TabIndex = 63;
		this.plotSurfaceTCD2.Text = "plotSurface2D1";
		this.plotSurfaceTCD2.Title = "";
		this.plotSurfaceTCD2.TitleFont = new System.Drawing.Font("Arial", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.plotSurfaceTCD2.XAxis1 = null;
		this.plotSurfaceTCD2.XAxis2 = null;
		this.plotSurfaceTCD2.YAxis1 = null;
		this.plotSurfaceTCD2.YAxis2 = null;
		this.plotSurfaceFID2.AutoScaleAutoGeneratedAxes = false;
		this.plotSurfaceFID2.AutoScaleTitle = false;
		this.plotSurfaceFID2.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.plotSurfaceFID2.DateTimeToolTip = false;
		this.plotSurfaceFID2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.plotSurfaceFID2.Legend = null;
		this.plotSurfaceFID2.LegendZOrder = -1;
		this.plotSurfaceFID2.Location = new System.Drawing.Point(0, 0);
		this.plotSurfaceFID2.Name = "plotSurfaceFID2";
		this.plotSurfaceFID2.RightMenu = null;
		this.plotSurfaceFID2.ShowCoordinates = false;
		this.plotSurfaceFID2.Size = new System.Drawing.Size(926, 223);
		this.plotSurfaceFID2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
		this.plotSurfaceFID2.TabIndex = 62;
		this.plotSurfaceFID2.Text = "plotSurface2D1";
		this.plotSurfaceFID2.Title = "";
		this.plotSurfaceFID2.TitleFont = new System.Drawing.Font("Arial", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.plotSurfaceFID2.XAxis1 = null;
		this.plotSurfaceFID2.XAxis2 = null;
		this.plotSurfaceFID2.YAxis1 = null;
		this.plotSurfaceFID2.YAxis2 = null;
		this.plotSurfaceFID1.AutoScaleAutoGeneratedAxes = false;
		this.plotSurfaceFID1.AutoScaleTitle = false;
		this.plotSurfaceFID1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.plotSurfaceFID1.DateTimeToolTip = false;
		this.plotSurfaceFID1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.plotSurfaceFID1.Legend = null;
		this.plotSurfaceFID1.LegendZOrder = -1;
		this.plotSurfaceFID1.Location = new System.Drawing.Point(0, 0);
		this.plotSurfaceFID1.Name = "plotSurfaceFID1";
		this.plotSurfaceFID1.RightMenu = null;
		this.plotSurfaceFID1.ShowCoordinates = false;
		this.plotSurfaceFID1.Size = new System.Drawing.Size(926, 254);
		this.plotSurfaceFID1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
		this.plotSurfaceFID1.TabIndex = 61;
		this.plotSurfaceFID1.Text = "plotSurface2D1";
		this.plotSurfaceFID1.Title = "";
		this.plotSurfaceFID1.TitleFont = new System.Drawing.Font("Arial", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.plotSurfaceFID1.XAxis1 = null;
		this.plotSurfaceFID1.XAxis2 = null;
		this.plotSurfaceFID1.YAxis1 = null;
		this.plotSurfaceFID1.YAxis2 = null;
		this.MethodView.Location = new System.Drawing.Point(82, 51);
		this.MethodView.Name = "MethodView";
		this.MethodView.Size = new System.Drawing.Size(55, 23);
		this.MethodView.TabIndex = 68;
		this.MethodView.Text = "查看";
		this.MethodView.UseVisualStyleBackColor = true;
		this.MethodView.Visible = false;
		this.MethodSave.Location = new System.Drawing.Point(149, 51);
		this.MethodSave.Name = "MethodSave";
		this.MethodSave.Size = new System.Drawing.Size(55, 23);
		this.MethodSave.TabIndex = 67;
		this.MethodSave.Text = "保存";
		this.MethodSave.UseVisualStyleBackColor = true;
		this.MethodSave.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(7, 73);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(317, 12);
		this.label2.TabIndex = 66;
		this.label2.Text = "——————————————————————————";
		this.label2.Visible = false;
		this.MethodReSave.Location = new System.Drawing.Point(210, 51);
		this.MethodReSave.Name = "MethodReSave";
		this.MethodReSave.Size = new System.Drawing.Size(55, 23);
		this.MethodReSave.TabIndex = 64;
		this.MethodReSave.Text = "另存";
		this.MethodReSave.UseVisualStyleBackColor = true;
		this.MethodReSave.Visible = false;
		this.MethodNew.Location = new System.Drawing.Point(17, 51);
		this.MethodNew.Name = "MethodNew";
		this.MethodNew.Size = new System.Drawing.Size(55, 23);
		this.MethodNew.TabIndex = 65;
		this.MethodNew.Text = "新建";
		this.MethodNew.UseVisualStyleBackColor = true;
		this.MethodNew.Visible = false;
		this.MethodNew.Click += new System.EventHandler(MethodNew_Click);
		this.MethodOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen.Location = new System.Drawing.Point(210, 18);
		this.MethodOpen.Name = "MethodOpen";
		this.MethodOpen.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen.TabIndex = 63;
		this.MethodOpen.UseVisualStyleBackColor = true;
		this.MethodOpen.Visible = false;
		this.tbMethName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName.Location = new System.Drawing.Point(82, 22);
		this.tbMethName.Name = "tbMethName";
		this.tbMethName.ReadOnly = true;
		this.tbMethName.Size = new System.Drawing.Size(117, 21);
		this.tbMethName.TabIndex = 62;
		this.tbMethName.Text = "默认";
		this.tbMethName.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(13, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(59, 12);
		this.label3.TabIndex = 61;
		this.label3.Text = "方法文件:";
		this.label3.Visible = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Location = new System.Drawing.Point(276, 362);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(74, 40);
		this.dataGridView1.TabIndex = 60;
		this.dataGridView1.Visible = false;
		this.splitter1.Location = new System.Drawing.Point(0, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(3, 707);
		this.splitter1.TabIndex = 65;
		this.splitter1.TabStop = false;
		this.groupBox15.Controls.Add(this.tbTime);
		this.groupBox15.Controls.Add(this.label32);
		this.groupBox15.Controls.Add(this.btnXYFull);
		this.groupBox15.Controls.Add(this.label21);
		this.groupBox15.Controls.Add(this.cbDisPlayAll);
		this.groupBox15.Controls.Add(this.label31);
		this.groupBox15.Controls.Add(this.lclCPauseRefresh);
		this.groupBox15.Controls.Add(this.tbSigYEnd);
		this.groupBox15.Controls.Add(this.checkBox5);
		this.groupBox15.Controls.Add(this.checkBox2);
		this.groupBox15.Controls.Add(this.maskedTextBox12);
		this.groupBox15.Controls.Add(this.btnTimeFull);
		this.groupBox15.Controls.Add(this.label29);
		this.groupBox15.Controls.Add(this.tbSigYBeg);
		this.groupBox15.Controls.Add(this.label27);
		this.groupBox15.Controls.Add(this.label20);
		this.groupBox15.Controls.Add(this.button25);
		this.groupBox15.Controls.Add(this.label119);
		this.groupBox15.Controls.Add(this.splitContainer3);
		this.groupBox15.Controls.Add(this.button26);
		this.groupBox15.Controls.Add(this.checkBox3);
		this.groupBox15.Controls.Add(this.cbFullScreem);
		this.groupBox15.Controls.Add(this.maskedTextBox6);
		this.groupBox15.Location = new System.Drawing.Point(1195, 270);
		this.groupBox15.Name = "groupBox15";
		this.groupBox15.Size = new System.Drawing.Size(10, 10);
		this.groupBox15.TabIndex = 64;
		this.groupBox15.TabStop = false;
		this.groupBox15.Text = "groupBox15";
		this.groupBox15.Visible = false;
		this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbTime.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTime.ForeColor = System.Drawing.Color.Lime;
		this.tbTime.Location = new System.Drawing.Point(84, 107);
		this.tbTime.Name = "tbTime";
		this.tbTime.PromptChar = ' ';
		this.tbTime.Size = new System.Drawing.Size(40, 21);
		this.tbTime.TabIndex = 2;
		this.tbTime.Text = "30";
		this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label32.ForeColor = System.Drawing.Color.Black;
		this.label32.Location = new System.Drawing.Point(19, 110);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(64, 12);
		this.label32.TabIndex = 0;
		this.label32.Text = "满屏时间:";
		this.btnXYFull.Location = new System.Drawing.Point(55, 74);
		this.btnXYFull.Name = "btnXYFull";
		this.btnXYFull.Size = new System.Drawing.Size(47, 26);
		this.btnXYFull.TabIndex = 8;
		this.btnXYFull.Text = "满屏";
		this.btnXYFull.UseVisualStyleBackColor = true;
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(323, 116);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(17, 12);
		this.label21.TabIndex = 11;
		this.label21.Text = "pA";
		this.label21.Visible = false;
		this.cbDisPlayAll.AutoSize = true;
		this.cbDisPlayAll.Location = new System.Drawing.Point(233, 19);
		this.cbDisPlayAll.Name = "cbDisPlayAll";
		this.cbDisPlayAll.Size = new System.Drawing.Size(48, 16);
		this.cbDisPlayAll.TabIndex = 7;
		this.cbDisPlayAll.Text = "全显";
		this.cbDisPlayAll.UseVisualStyleBackColor = true;
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(127, 110);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(23, 12);
		this.label31.TabIndex = 3;
		this.label31.Text = "min";
		this.lclCPauseRefresh.AutoSize = true;
		this.lclCPauseRefresh.Location = new System.Drawing.Point(159, 20);
		this.lclCPauseRefresh.Name = "lclCPauseRefresh";
		this.lclCPauseRefresh.Size = new System.Drawing.Size(72, 16);
		this.lclCPauseRefresh.TabIndex = 6;
		this.lclCPauseRefresh.Text = "暂停刷新";
		this.lclCPauseRefresh.UseVisualStyleBackColor = true;
		this.tbSigYEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbSigYEnd.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbSigYEnd.ForeColor = System.Drawing.Color.Lime;
		this.tbSigYEnd.Location = new System.Drawing.Point(38, 47);
		this.tbSigYEnd.Name = "tbSigYEnd";
		this.tbSigYEnd.PromptChar = ' ';
		this.tbSigYEnd.Size = new System.Drawing.Size(52, 21);
		this.tbSigYEnd.TabIndex = 2;
		this.tbSigYEnd.Text = "500";
		this.tbSigYEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox5.Location = new System.Drawing.Point(84, 30);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(89, 16);
		this.checkBox5.TabIndex = 5;
		this.checkBox5.Text = "结束后打印";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox5.Visible = false;
		this.checkBox2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox2.ForeColor = System.Drawing.Color.Green;
		this.checkBox2.Location = new System.Drawing.Point(114, 18);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(48, 20);
		this.checkBox2.TabIndex = 4;
		this.checkBox2.Text = "调零";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox2.Visible = false;
		this.maskedTextBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox12.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox12.Location = new System.Drawing.Point(270, 112);
		this.maskedTextBox12.Name = "maskedTextBox12";
		this.maskedTextBox12.ReadOnly = true;
		this.maskedTextBox12.Size = new System.Drawing.Size(47, 21);
		this.maskedTextBox12.TabIndex = 8;
		this.maskedTextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.btnTimeFull.Location = new System.Drawing.Point(207, 41);
		this.btnTimeFull.Name = "btnTimeFull";
		this.btnTimeFull.Size = new System.Drawing.Size(47, 26);
		this.btnTimeFull.TabIndex = 8;
		this.btnTimeFull.Text = "满屏";
		this.btnTimeFull.UseVisualStyleBackColor = true;
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(95, 51);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(17, 12);
		this.label29.TabIndex = 3;
		this.label29.Text = "mV";
		this.tbSigYBeg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbSigYBeg.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbSigYBeg.ForeColor = System.Drawing.Color.Lime;
		this.tbSigYBeg.Location = new System.Drawing.Point(38, 20);
		this.tbSigYBeg.Name = "tbSigYBeg";
		this.tbSigYBeg.PromptChar = ' ';
		this.tbSigYBeg.Size = new System.Drawing.Size(52, 21);
		this.tbSigYBeg.TabIndex = 2;
		this.tbSigYBeg.Text = "-10";
		this.tbSigYBeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label27.ForeColor = System.Drawing.Color.Black;
		this.label27.Location = new System.Drawing.Point(-1, 51);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(38, 12);
		this.label27.TabIndex = 0;
		this.label27.Text = "上限:";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label20.ForeColor = System.Drawing.Color.Black;
		this.label20.Location = new System.Drawing.Point(182, 116);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(38, 12);
		this.label20.TabIndex = 0;
		this.label20.Text = "信号:";
		this.button25.Enabled = false;
		this.button25.Image = (System.Drawing.Image)resources.GetObject("button25.Image");
		this.button25.Location = new System.Drawing.Point(129, 52);
		this.button25.Name = "button25";
		this.button25.Size = new System.Drawing.Size(45, 44);
		this.button25.TabIndex = 4;
		this.button25.UseVisualStyleBackColor = true;
		this.label119.AutoSize = true;
		this.label119.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label119.ForeColor = System.Drawing.Color.Black;
		this.label119.Location = new System.Drawing.Point(0, 24);
		this.label119.Name = "label119";
		this.label119.Size = new System.Drawing.Size(38, 12);
		this.label119.TabIndex = 0;
		this.label119.Text = "下限:";
		this.splitContainer3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splitContainer3.Location = new System.Drawing.Point(53, 24);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer3.Panel1.Controls.Add(this.toolStrip1);
		this.splitContainer3.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(splitContainer3_Panel1_Paint);
		this.splitContainer3.Panel2.Controls.Add(this.label116);
		this.splitContainer3.Panel2.Controls.Add(this.maskedTextBox13);
		this.splitContainer3.Panel2.Controls.Add(this.label115);
		this.splitContainer3.Size = new System.Drawing.Size(0, 54);
		this.splitContainer3.SplitterDistance = 25;
		this.splitContainer3.TabIndex = 1;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.tsStart, this.toolStripSeparator1, this.tsstop, this.toolStripButton6, this.toolStripSeparator2, this.toolStripButton4, this.toolStripButton5, this.toolStripSeparator3, this.toolStripButton7, this.toolStripButton1,
			this.toolStripSeparator4, this.toolStripButton10
		});
		this.toolStrip1.Location = new System.Drawing.Point(238, 1);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(44, 29);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStrip1.Visible = false;
		this.tsStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tsStart.Image = (System.Drawing.Image)resources.GetObject("tsStart.Image");
		this.tsStart.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsStart.Name = "tsStart";
		this.tsStart.Size = new System.Drawing.Size(26, 26);
		this.tsStart.Text = "toolStripButton1";
		this.tsStart.ToolTipText = "开始采集";
		this.tsStart.Visible = false;
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
		this.toolStripSeparator1.Visible = false;
		this.tsstop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tsstop.Image = (System.Drawing.Image)resources.GetObject("tsstop.Image");
		this.tsstop.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsstop.Name = "tsstop";
		this.tsstop.Size = new System.Drawing.Size(26, 26);
		this.tsstop.Text = "toolStripButton3";
		this.tsstop.ToolTipText = "停止采集";
		this.tsstop.Visible = false;
		this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton6.Image = (System.Drawing.Image)resources.GetObject("toolStripButton6.Image");
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(26, 26);
		this.toolStripButton6.Text = "toolStripButton6";
		this.toolStripButton6.ToolTipText = "放弃采集";
		this.toolStripButton6.Visible = false;
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
		this.toolStripSeparator2.Visible = false;
		this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton4.Image = (System.Drawing.Image)resources.GetObject("toolStripButton4.Image");
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(26, 26);
		this.toolStripButton4.Text = "toolStripButton4";
		this.toolStripButton4.ToolTipText = "上一视图";
		this.toolStripButton4.Visible = false;
		this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton5.Image = (System.Drawing.Image)resources.GetObject("toolStripButton5.Image");
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.Size = new System.Drawing.Size(26, 26);
		this.toolStripButton5.Text = "toolStripButton5";
		this.toolStripButton5.ToolTipText = "下一视图";
		this.toolStripButton5.Visible = false;
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
		this.toolStripSeparator3.Visible = false;
		this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton7.Image = (System.Drawing.Image)resources.GetObject("toolStripButton7.Image");
		this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton7.Name = "toolStripButton7";
		this.toolStripButton7.Size = new System.Drawing.Size(26, 26);
		this.toolStripButton7.ToolTipText = "文件命名";
		this.toolStripButton7.Visible = false;
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(26, 26);
		this.toolStripButton1.Text = "toolStripButton1";
		this.toolStripButton1.ToolTipText = "检测器设置";
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
		this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton10.Image = (System.Drawing.Image)resources.GetObject("toolStripButton10.Image");
		this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton10.Name = "toolStripButton10";
		this.toolStripButton10.Size = new System.Drawing.Size(26, 26);
		this.toolStripButton10.Text = "toolStripButton10";
		this.toolStripButton10.ToolTipText = "基线扣除";
		this.toolStripButton10.Visible = false;
		this.label116.AutoSize = true;
		this.label116.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label116.ForeColor = System.Drawing.Color.Black;
		this.label116.Location = new System.Drawing.Point(168, -27);
		this.label116.Name = "label116";
		this.label116.Size = new System.Drawing.Size(38, 12);
		this.label116.TabIndex = 0;
		this.label116.Text = "信号:";
		this.maskedTextBox13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox13.ForeColor = System.Drawing.Color.Lime;
		this.maskedTextBox13.Location = new System.Drawing.Point(233, -30);
		this.maskedTextBox13.Mask = "000.00";
		this.maskedTextBox13.Name = "maskedTextBox13";
		this.maskedTextBox13.Size = new System.Drawing.Size(52, 21);
		this.maskedTextBox13.TabIndex = 2;
		this.maskedTextBox13.Text = "00000";
		this.label115.AutoSize = true;
		this.label115.Location = new System.Drawing.Point(283, -27);
		this.label115.Name = "label115";
		this.label115.Size = new System.Drawing.Size(17, 12);
		this.label115.TabIndex = 3;
		this.label115.Text = "mv";
		this.button26.Enabled = false;
		this.button26.Image = (System.Drawing.Image)resources.GetObject("button26.Image");
		this.button26.Location = new System.Drawing.Point(132, 50);
		this.button26.Name = "button26";
		this.button26.Size = new System.Drawing.Size(45, 44);
		this.button26.TabIndex = 4;
		this.button26.UseVisualStyleBackColor = true;
		this.button26.Visible = false;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox3.ForeColor = System.Drawing.Color.Green;
		this.checkBox3.Location = new System.Drawing.Point(245, 20);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(84, 16);
		this.checkBox3.TabIndex = 5;
		this.checkBox3.Text = "结束后显示";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.cbFullScreem.AutoSize = true;
		this.cbFullScreem.Location = new System.Drawing.Point(132, 20);
		this.cbFullScreem.Name = "cbFullScreem";
		this.cbFullScreem.Size = new System.Drawing.Size(48, 16);
		this.cbFullScreem.TabIndex = 7;
		this.cbFullScreem.Text = "全屏";
		this.cbFullScreem.UseVisualStyleBackColor = true;
		this.maskedTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox6.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox6.Location = new System.Drawing.Point(221, 112);
		this.maskedTextBox6.Name = "maskedTextBox6";
		this.maskedTextBox6.ReadOnly = true;
		this.maskedTextBox6.Size = new System.Drawing.Size(47, 21);
		this.maskedTextBox6.TabIndex = 2;
		this.maskedTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.btnExplosion.Location = new System.Drawing.Point(1100, 4);
		this.btnExplosion.Name = "btnExplosion";
		this.btnExplosion.Size = new System.Drawing.Size(126, 53);
		this.btnExplosion.TabIndex = 65;
		this.btnExplosion.Text = "三角爆炸";
		this.btnExplosion.UseVisualStyleBackColor = true;
		this.btnExplosion.Click += new System.EventHandler(BtnExplosion_Click);
		this.btnPrintDate.Location = new System.Drawing.Point(968, 3);
		this.btnPrintDate.Name = "btnPrintDate";
		this.btnPrintDate.Size = new System.Drawing.Size(126, 53);
		this.btnPrintDate.TabIndex = 64;
		this.btnPrintDate.Text = "数据管理";
		this.btnPrintDate.UseVisualStyleBackColor = true;
		this.btnPrintDate.Click += new System.EventHandler(BtnPrintDate_Click);
		this.btnCycle.Location = new System.Drawing.Point(440, 3);
		this.btnCycle.Name = "btnCycle";
		this.btnCycle.Size = new System.Drawing.Size(126, 53);
		this.btnCycle.TabIndex = 63;
		this.btnCycle.Text = "开始循环";
		this.btnCycle.UseVisualStyleBackColor = true;
		this.btnDecSet.Location = new System.Drawing.Point(704, 4);
		this.btnDecSet.Name = "btnDecSet";
		this.btnDecSet.Size = new System.Drawing.Size(126, 53);
		this.btnDecSet.TabIndex = 62;
		this.btnDecSet.Text = "检测器设定";
		this.btnDecSet.UseVisualStyleBackColor = true;
		this.btnDecSet.Click += new System.EventHandler(BtnDecSet_Click);
		this.btnGiveUp.Location = new System.Drawing.Point(572, 3);
		this.btnGiveUp.Name = "btnGiveUp";
		this.btnGiveUp.Size = new System.Drawing.Size(126, 53);
		this.btnGiveUp.TabIndex = 61;
		this.btnGiveUp.Text = "放弃采集";
		this.btnGiveUp.UseVisualStyleBackColor = true;
		this.button39.Location = new System.Drawing.Point(1130, 31);
		this.button39.Name = "button39";
		this.button39.Size = new System.Drawing.Size(75, 23);
		this.button39.TabIndex = 60;
		this.button39.Text = "button39";
		this.button39.UseVisualStyleBackColor = true;
		this.button39.Visible = false;
		this.btnShuGuan.Location = new System.Drawing.Point(294, 4);
		this.btnShuGuan.Name = "btnShuGuan";
		this.btnShuGuan.Size = new System.Drawing.Size(139, 52);
		this.btnShuGuan.TabIndex = 58;
		this.btnShuGuan.Text = "束管设置";
		this.btnShuGuan.UseVisualStyleBackColor = true;
		this.btnShuGuan.Click += new System.EventHandler(BtnShuGuan_Click);
		this.button14.Location = new System.Drawing.Point(836, 3);
		this.button14.Name = "button14";
		this.button14.Size = new System.Drawing.Size(126, 53);
		this.button14.TabIndex = 1;
		this.button14.Text = "开始分析";
		this.button14.UseVisualStyleBackColor = true;
		this.button14.Click += new System.EventHandler(Button14_Click);
		this.groupBox9.Controls.Add(this.lbBTEXt);
		this.groupBox9.Controls.Add(this.lbBTEX);
		this.groupBox9.Controls.Add(this.lbBTEX8T);
		this.groupBox9.Controls.Add(this.lbBTEX7T);
		this.groupBox9.Controls.Add(this.lbNMHC);
		this.groupBox9.Controls.Add(this.lbBTEX9);
		this.groupBox9.Controls.Add(this.lbNMHCT);
		this.groupBox9.Controls.Add(this.lbCH4);
		this.groupBox9.Controls.Add(this.lbBTEX7);
		this.groupBox9.Controls.Add(this.label79);
		this.groupBox9.Controls.Add(this.lbBTEX2);
		this.groupBox9.Controls.Add(this.lbTHC);
		this.groupBox9.Controls.Add(this.lbBTEX5);
		this.groupBox9.Controls.Add(this.label77);
		this.groupBox9.Controls.Add(this.lbBTEX3);
		this.groupBox9.Controls.Add(this.lbBTEX9T);
		this.groupBox9.Controls.Add(this.lbBTEX1);
		this.groupBox9.Controls.Add(this.lbBTEX6);
		this.groupBox9.Controls.Add(this.lbBTEX2T);
		this.groupBox9.Controls.Add(this.lbBTEX3T);
		this.groupBox9.Controls.Add(this.lbBTEX8);
		this.groupBox9.Controls.Add(this.lbBTEX1T);
		this.groupBox9.Controls.Add(this.lbBTEX4);
		this.groupBox9.Controls.Add(this.lbBTEX5T);
		this.groupBox9.Controls.Add(this.lbBTEX4T);
		this.groupBox9.Controls.Add(this.lbBTEX6T);
		this.groupBox9.Location = new System.Drawing.Point(1195, 91);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(28, 10);
		this.groupBox9.TabIndex = 57;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "groupBox9";
		this.groupBox9.Visible = false;
		this.lbBTEXt.AutoSize = true;
		this.lbBTEXt.Location = new System.Drawing.Point(125, 80);
		this.lbBTEXt.Name = "lbBTEXt";
		this.lbBTEXt.Size = new System.Drawing.Size(53, 12);
		this.lbBTEXt.TabIndex = 46;
		this.lbBTEXt.Text = "苯系物：";
		this.lbBTEX.AutoSize = true;
		this.lbBTEX.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX.Location = new System.Drawing.Point(177, 80);
		this.lbBTEX.Name = "lbBTEX";
		this.lbBTEX.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX.TabIndex = 48;
		this.lbBTEX.Text = "0";
		this.lbBTEX8T.AutoSize = true;
		this.lbBTEX8T.Location = new System.Drawing.Point(125, 51);
		this.lbBTEX8T.Name = "lbBTEX8T";
		this.lbBTEX8T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX8T.TabIndex = 50;
		this.lbBTEX8T.Text = "苯：";
		this.lbBTEX7T.AutoSize = true;
		this.lbBTEX7T.Location = new System.Drawing.Point(126, 27);
		this.lbBTEX7T.Name = "lbBTEX7T";
		this.lbBTEX7T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX7T.TabIndex = 44;
		this.lbBTEX7T.Text = "苯：";
		this.lbNMHC.AutoSize = true;
		this.lbNMHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbNMHC.Location = new System.Drawing.Point(141, 75);
		this.lbNMHC.Name = "lbNMHC";
		this.lbNMHC.Size = new System.Drawing.Size(11, 12);
		this.lbNMHC.TabIndex = 49;
		this.lbNMHC.Text = "0";
		this.lbBTEX9.AutoSize = true;
		this.lbBTEX9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX9.Location = new System.Drawing.Point(172, 77);
		this.lbBTEX9.Name = "lbBTEX9";
		this.lbBTEX9.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX9.TabIndex = 53;
		this.lbBTEX9.Text = "0";
		this.lbNMHCT.AutoSize = true;
		this.lbNMHCT.Location = new System.Drawing.Point(65, 74);
		this.lbNMHCT.Name = "lbNMHCT";
		this.lbNMHCT.Size = new System.Drawing.Size(77, 12);
		this.lbNMHCT.TabIndex = 47;
		this.lbNMHCT.Text = "非甲烷总烃：";
		this.lbCH4.AutoSize = true;
		this.lbCH4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbCH4.Location = new System.Drawing.Point(142, 45);
		this.lbCH4.Name = "lbCH4";
		this.lbCH4.Size = new System.Drawing.Size(11, 12);
		this.lbCH4.TabIndex = 31;
		this.lbCH4.Text = "0";
		this.lbBTEX7.AutoSize = true;
		this.lbBTEX7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX7.Location = new System.Drawing.Point(173, 27);
		this.lbBTEX7.Name = "lbBTEX7";
		this.lbBTEX7.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX7.TabIndex = 45;
		this.lbBTEX7.Text = "0";
		this.label79.AutoSize = true;
		this.label79.Location = new System.Drawing.Point(101, 45);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(41, 12);
		this.label79.TabIndex = 30;
		this.label79.Text = "甲烷：";
		this.lbBTEX2.AutoSize = true;
		this.lbBTEX2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX2.Location = new System.Drawing.Point(177, 51);
		this.lbBTEX2.Name = "lbBTEX2";
		this.lbBTEX2.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX2.TabIndex = 37;
		this.lbBTEX2.Text = "0";
		this.lbTHC.AutoSize = true;
		this.lbTHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbTHC.Location = new System.Drawing.Point(142, 24);
		this.lbTHC.Name = "lbTHC";
		this.lbTHC.Size = new System.Drawing.Size(11, 12);
		this.lbTHC.TabIndex = 29;
		this.lbTHC.Text = "0";
		this.lbBTEX5.AutoSize = true;
		this.lbBTEX5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX5.Location = new System.Drawing.Point(184, 51);
		this.lbBTEX5.Name = "lbBTEX5";
		this.lbBTEX5.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX5.TabIndex = 43;
		this.lbBTEX5.Text = "0";
		this.label77.AutoSize = true;
		this.label77.Location = new System.Drawing.Point(101, 22);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(41, 12);
		this.label77.TabIndex = 28;
		this.label77.Text = "总烃：";
		this.lbBTEX3.AutoSize = true;
		this.lbBTEX3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX3.Location = new System.Drawing.Point(177, 74);
		this.lbBTEX3.Name = "lbBTEX3";
		this.lbBTEX3.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX3.TabIndex = 36;
		this.lbBTEX3.Text = "0";
		this.lbBTEX9T.AutoSize = true;
		this.lbBTEX9T.Location = new System.Drawing.Point(125, 77);
		this.lbBTEX9T.Name = "lbBTEX9T";
		this.lbBTEX9T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX9T.TabIndex = 52;
		this.lbBTEX9T.Text = "苯：";
		this.lbBTEX1.AutoSize = true;
		this.lbBTEX1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX1.Location = new System.Drawing.Point(177, 31);
		this.lbBTEX1.Name = "lbBTEX1";
		this.lbBTEX1.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX1.TabIndex = 35;
		this.lbBTEX1.Text = "0";
		this.lbBTEX6.AutoSize = true;
		this.lbBTEX6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX6.Location = new System.Drawing.Point(184, 74);
		this.lbBTEX6.Name = "lbBTEX6";
		this.lbBTEX6.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX6.TabIndex = 42;
		this.lbBTEX6.Text = "0";
		this.lbBTEX2T.AutoSize = true;
		this.lbBTEX2T.Location = new System.Drawing.Point(113, 51);
		this.lbBTEX2T.Name = "lbBTEX2T";
		this.lbBTEX2T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX2T.TabIndex = 34;
		this.lbBTEX2T.Text = "甲苯：";
		this.lbBTEX3T.AutoSize = true;
		this.lbBTEX3T.Location = new System.Drawing.Point(113, 74);
		this.lbBTEX3T.Name = "lbBTEX3T";
		this.lbBTEX3T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX3T.TabIndex = 33;
		this.lbBTEX3T.Text = "乙苯：";
		this.lbBTEX8.AutoSize = true;
		this.lbBTEX8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX8.Location = new System.Drawing.Point(172, 51);
		this.lbBTEX8.Name = "lbBTEX8";
		this.lbBTEX8.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX8.TabIndex = 51;
		this.lbBTEX8.Text = "0";
		this.lbBTEX1T.AutoSize = true;
		this.lbBTEX1T.Location = new System.Drawing.Point(113, 31);
		this.lbBTEX1T.Name = "lbBTEX1T";
		this.lbBTEX1T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX1T.TabIndex = 32;
		this.lbBTEX1T.Text = "苯：";
		this.lbBTEX4.AutoSize = true;
		this.lbBTEX4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX4.Location = new System.Drawing.Point(184, 31);
		this.lbBTEX4.Name = "lbBTEX4";
		this.lbBTEX4.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX4.TabIndex = 41;
		this.lbBTEX4.Text = "0";
		this.lbBTEX5T.AutoSize = true;
		this.lbBTEX5T.Location = new System.Drawing.Point(125, 51);
		this.lbBTEX5T.Name = "lbBTEX5T";
		this.lbBTEX5T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX5T.TabIndex = 40;
		this.lbBTEX5T.Text = "甲苯：";
		this.lbBTEX4T.AutoSize = true;
		this.lbBTEX4T.Location = new System.Drawing.Point(125, 31);
		this.lbBTEX4T.Name = "lbBTEX4T";
		this.lbBTEX4T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX4T.TabIndex = 38;
		this.lbBTEX4T.Text = "苯：";
		this.lbBTEX6T.AutoSize = true;
		this.lbBTEX6T.Location = new System.Drawing.Point(125, 74);
		this.lbBTEX6T.Name = "lbBTEX6T";
		this.lbBTEX6T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX6T.TabIndex = 39;
		this.lbBTEX6T.Text = "乙苯：";
		this.labStateIns.AutoSize = true;
		this.labStateIns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labStateIns.Location = new System.Drawing.Point(1156, 8);
		this.labStateIns.Name = "labStateIns";
		this.labStateIns.Size = new System.Drawing.Size(67, 14);
		this.labStateIns.TabIndex = 56;
		this.labStateIns.Text = "非在线循环";
		this.labStateIns.Visible = false;
		this.btShowDesktop.Location = new System.Drawing.Point(1112, 31);
		this.btShowDesktop.Name = "btShowDesktop";
		this.btShowDesktop.Size = new System.Drawing.Size(75, 23);
		this.btShowDesktop.TabIndex = 55;
		this.btShowDesktop.Text = "显示桌面";
		this.btShowDesktop.UseVisualStyleBackColor = true;
		this.btShowDesktop.Visible = false;
		this.button36.Location = new System.Drawing.Point(1163, 25);
		this.button36.Name = "button36";
		this.button36.Size = new System.Drawing.Size(75, 23);
		this.button36.TabIndex = 54;
		this.button36.Text = "监控界面";
		this.button36.UseVisualStyleBackColor = true;
		this.button36.Visible = false;
		this.NetConfig.Location = new System.Drawing.Point(148, 4);
		this.NetConfig.Name = "NetConfig";
		this.NetConfig.Size = new System.Drawing.Size(140, 51);
		this.NetConfig.TabIndex = 7;
		this.NetConfig.Text = "网络配置";
		this.NetConfig.UseVisualStyleBackColor = true;
		this.NetConfig.Click += new System.EventHandler(NetConfig_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.groupBox2.Controls.Add(this.tableLayoutPanel2);
		this.groupBox2.ForeColor = System.Drawing.Color.Blue;
		this.groupBox2.Location = new System.Drawing.Point(1166, 184);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(92, 119);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "设备管理";
		this.tableLayoutPanel2.ColumnCount = 1;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 1);
		this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 2;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.25f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.75f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(86, 99);
		this.tableLayoutPanel2.TabIndex = 0;
		this.panel4.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.panel4.Controls.Add(this.button12);
		this.panel4.Controls.Add(this.rdcs);
		this.panel4.Controls.Add(this.button32);
		this.panel4.Controls.Add(this.button23);
		this.panel4.Controls.Add(this.checkBox4);
		this.panel4.Controls.Add(this.label24);
		this.panel4.Controls.Add(this.textBox3);
		this.panel4.Controls.Add(this.button5);
		this.panel4.Controls.Add(this.button1);
		this.panel4.Controls.Add(this.label25);
		this.panel4.Controls.Add(this.textBox2);
		this.panel4.Controls.Add(this.label26);
		this.panel4.Controls.Add(this.textBox1);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(3, 3);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(80, 24);
		this.panel4.TabIndex = 2;
		this.button12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button12.Location = new System.Drawing.Point(286, 1);
		this.button12.Name = "button12";
		this.button12.Size = new System.Drawing.Size(81, 23);
		this.button12.TabIndex = 1;
		this.button12.Text = "EPS查询";
		this.button12.UseVisualStyleBackColor = true;
		this.button12.Visible = false;
		this.rdcs.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.rdcs.AutoSize = true;
		this.rdcs.Enabled = false;
		this.rdcs.Location = new System.Drawing.Point(56, 4);
		this.rdcs.Name = "rdcs";
		this.rdcs.Size = new System.Drawing.Size(65, 16);
		this.rdcs.TabIndex = 3;
		this.rdcs.TabStop = true;
		this.rdcs.Text = "DCS空闲";
		this.rdcs.UseVisualStyleBackColor = true;
		this.button32.Location = new System.Drawing.Point(193, 1);
		this.button32.Name = "button32";
		this.button32.Size = new System.Drawing.Size(95, 23);
		this.button32.TabIndex = 1;
		this.button32.Text = "软键盘输入";
		this.button32.UseVisualStyleBackColor = true;
		this.button23.Location = new System.Drawing.Point(120, 1);
		this.button23.Name = "button23";
		this.button23.Size = new System.Drawing.Size(75, 23);
		this.button23.TabIndex = 2;
		this.button23.Text = "设置";
		this.button23.UseVisualStyleBackColor = true;
		this.button23.Visible = false;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox4.Location = new System.Drawing.Point(436, 6);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(115, 16);
		this.checkBox4.TabIndex = 5;
		this.checkBox4.Text = "停止后谱图校正";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox4.Visible = false;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label24.ForeColor = System.Drawing.Color.Black;
		this.label24.Location = new System.Drawing.Point(615, 4);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(51, 12);
		this.label24.TabIndex = 0;
		this.label24.Text = "检测器:";
		this.label24.Visible = false;
		this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox3.Location = new System.Drawing.Point(853, 0);
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.Size = new System.Drawing.Size(47, 21);
		this.textBox3.TabIndex = 4;
		this.textBox3.Visible = false;
		this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button5.Location = new System.Drawing.Point(365, 1);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(70, 23);
		this.button5.TabIndex = 1;
		this.button5.Text = "查询气路";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Visible = false;
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(7, 0);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(43, 21);
		this.button1.TabIndex = 1;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label25.ForeColor = System.Drawing.Color.Black;
		this.label25.Location = new System.Drawing.Point(718, 4);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(38, 12);
		this.label25.TabIndex = 0;
		this.label25.Text = "量程:";
		this.label25.Visible = false;
		this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox2.Location = new System.Drawing.Point(757, 0);
		this.textBox2.Name = "textBox2";
		this.textBox2.ReadOnly = true;
		this.textBox2.Size = new System.Drawing.Size(47, 21);
		this.textBox2.TabIndex = 4;
		this.textBox2.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label26.ForeColor = System.Drawing.Color.Black;
		this.label26.Location = new System.Drawing.Point(811, 4);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(38, 12);
		this.label26.TabIndex = 0;
		this.label26.Text = "采样:";
		this.label26.Visible = false;
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox1.Location = new System.Drawing.Point(670, 0);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(47, 21);
		this.textBox1.TabIndex = 4;
		this.textBox1.Visible = false;
		this.panel5.Controls.Add(this.InstrumlistView);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel5.Location = new System.Drawing.Point(3, 33);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(80, 63);
		this.panel5.TabIndex = 3;
		this.InstrumlistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.InstrumlistView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.InstrumlistView.HideSelection = false;
		this.InstrumlistView.Items.AddRange(new System.Windows.Forms.ListViewItem[3] { listViewItem, listViewItem2, listViewItem3 });
		this.InstrumlistView.Location = new System.Drawing.Point(0, 0);
		this.InstrumlistView.MultiSelect = false;
		this.InstrumlistView.Name = "InstrumlistView";
		this.InstrumlistView.Size = new System.Drawing.Size(80, 63);
		this.InstrumlistView.TabIndex = 0;
		this.InstrumlistView.UseCompatibleStateImageBehavior = false;
		this.button2.Location = new System.Drawing.Point(7, 4);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(135, 50);
		this.button2.TabIndex = 1;
		this.button2.Text = "设备管理";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(Button2_Click);
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(Timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.splitContainer2);
		base.Name = "minePlot";
		base.Size = new System.Drawing.Size(1285, 784);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		this.splitContainer4.Panel1.ResumeLayout(false);
		this.splitContainer4.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer4).EndInit();
		this.splitContainer4.ResumeLayout(false);
		this.splitContainer5.Panel1.ResumeLayout(false);
		this.splitContainer5.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer5).EndInit();
		this.splitContainer5.ResumeLayout(false);
		this.splitContainer6.Panel1.ResumeLayout(false);
		this.splitContainer6.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer6).EndInit();
		this.splitContainer6.ResumeLayout(false);
		this.splitContainer7.Panel1.ResumeLayout(false);
		this.splitContainer7.Panel1.PerformLayout();
		this.splitContainer7.Panel2.ResumeLayout(false);
		this.splitContainer7.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer7).EndInit();
		this.splitContainer7.ResumeLayout(false);
		this.groupBox16.ResumeLayout(false);
		this.groupBox16.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox15.ResumeLayout(false);
		this.groupBox15.PerformLayout();
		this.splitContainer3.Panel1.ResumeLayout(false);
		this.splitContainer3.Panel1.PerformLayout();
		this.splitContainer3.Panel2.ResumeLayout(false);
		this.splitContainer3.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
		this.splitContainer3.ResumeLayout(false);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.tableLayoutPanel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.panel5.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
