using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace IBrainChrom2018;

public class CaliGnlUserCtrl : UserControl
{
	public delegate void myUpdateMtdForm(CaliGnl cali);

	public static CaliGnlUserCtrl caliGnlUserCtrl;

	public myUpdateMtdForm updateMtdForm = null;

	private Enum7 enum7_0;

	private CmpdDisplay cmpdDisplay;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private ChromDisplay chromDisplay_0;

	private Chromatogram[] chromatogram_0;

	private Chromatogram curChromatogram;

	private Compound compound_0;

	private string m_strChromFile = "";

	private string m_strCalFile;

	private bool bool_0;

	private int iLevel;

	private int int_2;

	private DisLg disLg_0;

	private int int_3;

	private LineBE[] lineBE_0;

	private int int_4;

	private Point point_0;

	private bool bool_1;

	private bool m_ShowManuAndStateBar = true;

	private ColumnsSetupDlg columnsSetupDlg_0;

	private CaliGnlOptDlg caliGnlOptDlg_0;

	private CaliGnlIstdDlg caliGnlIstdDlg_0;

	private ToolStripButton btnAddAll;

	private ToolStripButton btnAddExists;

	private ToolStripButton btnAddGroup;

	private ToolStripButton btnAddPeak;

	private ToolStripButton btnCloseChrom;

	private ToolStripButton btnDeleteCmpd;

	private ToolStripButton btnNewCali;

	private ToolStripButton btnNextZoom;

	private ToolStripButton btnOpenCali;

	private ToolStripButton btnOpenChrom;

	private ToolStripButton btnOptions;

	private ToolStripButton btnPreviousZoom;

	private ToolStripButton btnSaveCali;

	private ToolStripButton btnUnzoom;

	private ContextMenuStrip cmsCali;

	private IContainer icontainer_2;

	private LclDisplayPanel dpDisplay;

	private ToolStripMenuItem toolStripMenuItem_0;

	private ToolStripMenuItem miCaliAddAll;

	private ToolStripMenuItem miCaliAddExists;

	private ToolStripMenuItem miCaliAddGroup;

	private ToolStripMenuItem miCaliAddPeak;

	private ToolStripMenuItem miCalibration;

	private ToolStripMenuItem miCaliClearAllLevels;

	private ToolStripMenuItem miCaliClearSelectedLevel;

	private ToolStripMenuItem miCaliDeleteAllCmpds;

	private ToolStripMenuItem miCaliDeleteCmpd;

	private ToolStripMenuItem miCaliOptions;

	private ToolStripMenuItem miCaliSetLevel;

	private ToolStripMenuItem miColumnsSetup;

	private ToolStripMenuItem miDisNextZoom;

	private ToolStripMenuItem miDisplay;

	private ToolStripMenuItem miDisPreviousZoom;

	private ToolStripMenuItem miDisProperties;

	private ToolStripMenuItem miDisUnzoom;

	private ToolStripMenuItem miFiCloseChrom;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiNewCali;

	private ToolStripMenuItem miFiOpenCali;

	private ToolStripMenuItem miFiOpenChrom;

	private ToolStripMenuItem miFiPreview;

	private ToolStripMenuItem miFiPrint;

	private ToolStripMenuItem miFiReportSetup;

	private ToolStripMenuItem miFiSaveAsCali;

	private ToolStripMenuItem miFiSaveCali;

	private ToolStripMenuItem miRestoreDftColumns;

	private MenuStrip msCali;

	private RectangleF rectangleF_0;

	private LclNumericUpDown lclNumericUpDown_0;

	private DataGridViewColumn dataGridViewColumn_0;

	private LclPanel pnlCmpds;

	private SaveFileDialog saveFileDialog_0;

	private ToolStripStatusLabel slbExplain;

	private LclSplitter splt;

	private StatusStrip ssCali;

	public LclTabControl tcCmpds;

	private ToolStripLabel toolStripLabel1;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator10;

	private ToolStripSeparator toolStripSeparator11;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripSeparator toolStripSeparator9;

	private TabPage tpCL;

	private ToolStripButton toolStripButton1;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripComboBox cboxPKpeak;

	private ToolStripLabel toolStripLabel2;

	private ToolStripMenuItem miAddrow;

	private ToolStripSeparator toolStripSeparator12;

	private ToolStripLabel toolStripLabel3;

	private ToolStripComboBox cbUpPeak;

	private IContainer components;

	private LclCombineCGridView gvCmpds;

	public ToolStrip tsCali;

	private Panel pnlFill;

	private CaliGnlCurveCtrl ccCmpd;

	public Chromatogram Chromatogram => curChromatogram;

	private MtdSetup mtdSetup { get; set; }

	private CaliGnl caliGnl_0
	{
		get
		{
			if (mtdSetup == null)
			{
				return null;
			}
			return mtdSetup.caliGnl;
		}
	}

	public CaliGnl CurCaliGnl => caliGnl_0;

	private Signal CurSignal => chromDisplay_0.curSignal;

	public static string Filter => "(*.cal)|*.cal";

	public bool ShowManuAndStateBar
	{
		get
		{
			return m_ShowManuAndStateBar;
		}
		set
		{
			m_ShowManuAndStateBar = value;
			if (m_ShowManuAndStateBar)
			{
				msCali.Visible = true;
				ssCali.Visible = true;
			}
			else
			{
				msCali.Visible = false;
				ssCali.Visible = false;
			}
		}
	}

	private bool HasChrom => chromatogram_0.Length != 0;

	public static bool IsDesignMode()
	{
		return false;
	}

	public CaliGnlUserCtrl()
	{
		caliGnlUserCtrl = this;
		InitializeComponent();
		if (!IsDesignMode())
		{
			mtdSetup = new MtdSetup();
			chromatogram_0 = new Chromatogram[0];
			lclNumericUpDown_0 = new LclNumericUpDown();
			caliGnlIstdDlg_0 = new CaliGnlIstdDlg();
			columnsSetupDlg_0 = new ColumnsSetupDlg("校正列设置", "Calibrate Columns Setup");
			m_strCalFile = "";
			enum7_0 = Enum7.const_0;
			int_4 = -1;
			lineBE_0 = new LineBE[0];
			disLg_0 = default(DisLg);
			icontainer_2 = null;
			chromDisplay_0 = new ChromDisplay(WinStyle.Chromatogram, dpDisplay);
			LoadOptions();
			chromDisplay_0.OnSignalClick += method_1;
			chromDisplay_0.OnSignalDoubleClick += method_2;
			gvCmpds.OnChangeColor += method_7;
			caliGnlOptDlg_0 = new CaliGnlOptDlg();
			caliGnlOptDlg_0.OnSetAllCmpds += method_4;
			refresh_once();
			for (int i = 0; i < 20; i++)
			{
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)miCaliSetLevel.DropDownItems.Add((i + 1).ToString());
				toolStripMenuItem.Tag = i;
				toolStripMenuItem.Click += lclNumericUpDown_0_ValueChanged;
			}
			lclNumericUpDown_0.ReadOnly = true;
			lclNumericUpDown_0.Minimum = 1m;
			lclNumericUpDown_0.Maximum = 20m;
			ToolStripControlHost value = new ToolStripControlHost(lclNumericUpDown_0)
			{
				AutoSize = false,
				Width = 50
			};
			tsCali.Items.Insert(tsCali.Items.IndexOf(btnDeleteCmpd), value);
			lclNumericUpDown_0.ValueChanged += lclNumericUpDown_0_ValueChanged;
			lclNumericUpDown_0_ValueChanged(miCaliSetLevel.DropDownItems[0], null);
			gvCmpds.CharacterHeaderColor = Color.Red;
			method_9();
			method_8();
			if (gvCmpds.LoadFromManager())
			{
				string combineText = "Level " + (iLevel + 1);
				gvCmpds.AdjustCombineDisInfo(read_refresh: false);
				gvCmpds.SetCombineCText(int_3, combineText);
			}
			else
			{
				miRestoreDftColumns_Click(miRestoreDftColumns, null);
			}
			ccCmpd.GetCaliGnl = GetCaliGnl;
			ccCmpd.GetCurrentChrom = GetCurrentChrom;
			ccCmpd.GetCurrentCompound = GetCurrentCompound;
			tcCmpds.tabStyle = TabStyle.Special;
			ccCmpd.Dock = DockStyle.Fill;
			pnlCmpds.Dock = DockStyle.Fill;
			pnlCmpds.BringToFront();
			tpCL.Text = Lang.PS("组分列表.", "Component list");
			btnOpenCali.ToolTipText = Lang.PS("打开组份表", "OpenCali");
			btnNewCali.ToolTipText = Lang.PS("新建组份表", "NewCali");
			btnSaveCali.ToolTipText = Lang.PS("保存组份表", "SaveCali");
			btnOpenChrom.ToolTipText = Lang.PS("打开标样", "OpenChrom");
			btnCloseChrom.ToolTipText = Lang.PS("关闭标样", "CloseChrom");
			btnPreviousZoom.ToolTipText = Lang.PS("上一视图", "PreviousZoom");
			btnNextZoom.ToolTipText = Lang.PS("下一视图", "NextZoom");
			btnUnzoom.ToolTipText = Lang.PS("原始视图", "Unzoom");
			btnAddAll.Text = Lang.PS("添加所有峰", "AddAll");
			btnAddExists.Text = Lang.PS("添加已有组份", "AddExists");
			btnAddPeak.ToolTipText = Lang.PS("添加峰", "AddPeak");
			btnAddGroup.ToolTipText = Lang.PS("添加分组", "AddGroup");
			toolStripButton1.ToolTipText = Lang.PS("取校正因子", "Calculation of correction factor");
		}
	}

	private CaliGnl GetCaliGnl()
	{
		return caliGnl_0;
	}

	private Chromatogram GetCurrentChrom()
	{
		return curChromatogram;
	}

	private Compound GetCurrentCompound()
	{
		return compound_0;
	}

	private void CaliGnlForm_Load(object sender, EventArgs e)
	{
		if (!IsDesignMode())
		{
		}
	}

	private void method_0(bool bool_2)
	{
		if (bool_2)
		{
			btnAddAll.Enabled = true;
			btnAddExists.Enabled = false;
		}
		else
		{
			btnAddAll.Enabled = false;
			btnAddExists.Enabled = true;
		}
	}

	public void AutoAddLevel()
	{
		if (caliGnl_0.cmpds.Length != 0)
		{
			if (iLevel < 19)
			{
				iLevel++;
			}
			miCaliAddExists_Click(null, null);
		}
	}

	public void refreshcboxPKpeakItem()
	{
		cboxPKpeak.Items.Clear();
		cboxPKpeak.Items.Add("");
		if (caliGnl_0.cmpds != null)
		{
			for (int i = 1; i < caliGnl_0.cmpds.Length + 1; i++)
			{
				cboxPKpeak.Items.Add(i.ToString());
			}
		}
		cbUpPeak.Items.Clear();
		cbUpPeak.Items.Add("");
		if (caliGnl_0.cmpds != null)
		{
			for (int j = 1; j < caliGnl_0.cmpds.Length + 1; j++)
			{
				cbUpPeak.Items.Add(j.ToString());
			}
		}
	}

	private void CaliGnlForm_KeyDown(object sender, KeyEventArgs e)
	{
		bool flag = false;
		if (e.Control && e.KeyCode == Keys.Z)
		{
			if (e.Shift)
			{
				if (caliGnl_0.Redo())
				{
					flag = true;
				}
			}
			else if (caliGnl_0.Undo())
			{
				flag = true;
			}
		}
		if (flag)
		{
			caliGnl_0.CalculateFunc(appendLink: false);
			tcCmpds_SelectedIndexChanged(null, null);
		}
	}

	private void method_1(int int_5, Signal signal_0)
	{
		curChromatogram = null;
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i].signal == signal_0)
			{
				curChromatogram = chromatogram_0[i];
				break;
			}
		}
	}

	private void method_2(Signal signal_0)
	{
		SetChromDisplayCurDisLg();
		SetDisZoomButtonEnableState();
	}

	public void CloseAllChroms()
	{
		Array.Resize(ref chromatogram_0, 0);
		chromDisplay_0.ClearDisSignals();
		chromDisplay_0.stDisChain.Clear();
		SetSignalsColor();
		SetChromDisplayCurDisLg();
	}

	private void miRestoreDftColumns_Click(object sender, EventArgs e)
	{
		string combineText = "Level " + (iLevel + 1);
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
			Compound compound = (gvCmpds.Rows[i].Tag as Class74).compound_0;
			if (compound.cmpdInfo.name == strName)
			{
				return compound;
			}
		}
		return null;
	}

	private void method_4(CaliGnlOpt caliGnlOpt_0)
	{
		for (int i = 0; i < gvCmpds.RowCount; i++)
		{
			Compound compound = (gvCmpds.Rows[i].Tag as Class74).compound_0;
			compound.cmpdInfo.leftWindow = caliGnlOpt_0.leftWindow;
			compound.cmpdInfo.rightWindow = caliGnlOpt_0.rightWindow;
			compound.cmpdInfo.respStyle = caliGnlOpt_0.respStyle;
			compound.eFunc.original = (compound.iFunc.original = caliGnlOpt_0.original);
			compound.eFunc.curveFit = (compound.iFunc.curveFit = caliGnlOpt_0.curveFit);
		}
		gvCmpds.EndEdit();
		Array.Resize(ref caliGnl_0.cmpds, gvCmpds.RowCount);
		for (int j = 0; j < gvCmpds.RowCount; j++)
		{
			caliGnl_0.cmpds[j] = (gvCmpds.Rows[j].Tag as Class74).compound_0;
		}
		caliGnl_0.CalculateFunc(appendLink: true);
		ReadAndWriteCompoundList(AccStyle.Read);
	}

	private bool CheckPeakExist(Peak peak_0)
	{
		slbExplain.Text = "";
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

	private void dpDisplay_MouseDown(object sender, MouseEventArgs e)
	{
		if (IsDesignMode() || !HasChrom)
		{
			return;
		}
		if (e.Button == MouseButtons.Left)
		{
			chromDisplay_0.ptScaleBegin = e.Location;
			if (enum7_0 == Enum7.const_1)
			{
				if (int_4 >= 0)
				{
					Peak peak = CurSignal.peaks[int_4];
					if (!CheckPeakExist(peak))
					{
						caliGnl_0.add_splLevel(checkExists: true, canAddNew: true, iLevel, peak.pkRT, peak.area, peak.height);
						caliGnl_0.CalculateFunc(appendLink: true);
						ReadAndWriteCaliGnlData(AccStyle.Read);
						gvCmpds_SelectionChanged(null, null);
					}
					else
					{
						slbExplain.Text = Lang.PS("不需要重复添加该组分.", "Already Add.");
					}
				}
			}
			else if (enum7_0 != Enum7.const_2)
			{
			}
		}
		else if (e.Button == MouseButtons.Right)
		{
			chromDisplay_0.mouseLocation = (point_0 = e.Location);
			if (enum7_0 == Enum7.const_1)
			{
				enum7_0 = Enum7.const_0;
				SetChromDisplayCurDisLg();
				chromDisplay_0.DrawL_end2();
				btnAddPeak.Checked = false;
				miCaliAddPeak.Checked = false;
			}
		}
	}

	private void dpDisplay_MouseLeave(object sender, EventArgs e)
	{
		enum7_0 = Enum7.const_0;
		chromDisplay_0.DrawL_end2();
		btnAddPeak.Checked = false;
		miCaliAddPeak.Checked = false;
	}

	private void dpDisplay_MouseMove(object sender, MouseEventArgs e)
	{
		if (IsDesignMode() || !HasChrom)
		{
			return;
		}
		if (enum7_0 == Enum7.const_0)
		{
			if (e.Button == MouseButtons.Left)
			{
				chromDisplay_0.scaling = true;
				chromDisplay_0.mouseLocation = e.Location;
				chromDisplay_0.DrawScale_moving();
				chromDisplay_0.DrawMouseLgValue();
			}
			else if (e.Button == MouseButtons.Right)
			{
				chromDisplay_0.displayPanel.Cursor = Cursors.SizeAll;
				if (!chromDisplay_0.moving)
				{
					chromDisplay_0.stDisChain.MustAppendFrameLg(disLg_0);
				}
				Size szScr = new Size(e.X - chromDisplay_0.mouseLocation.X, e.Y - chromDisplay_0.mouseLocation.Y);
				SizeF sizeF = chromDisplay_0.scrToLg(szScr, bool_0: true);
				disLg_0.lgXBeg -= sizeF.Width;
				disLg_0.lgYBeg += sizeF.Height;
				chromDisplay_0.stDisChain.ReplaceCurFrameLg(disLg_0);
				chromDisplay_0.moving = true;
				chromDisplay_0.mouseLocation = e.Location;
				SetChromDisplayCurDisLg();
			}
		}
		chromDisplay_0.mouseLocation = e.Location;
		chromDisplay_0.DrawMouseLgValue();
		if (enum7_0 == Enum7.const_1)
		{
			PointF mouseLgValue = chromDisplay_0.MouseLgValue;
			bool flag = false;
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < CurSignal.PeaksNum; i++)
			{
				num = CurSignal.peaks[i].Get_lf(CurSignal.dots);
				num2 = CurSignal.peaks[i].Get_rt(CurSignal.dots);
				if (num >= mouseLgValue.X || mouseLgValue.X >= num2)
				{
					continue;
				}
				if (1 == 0)
				{
					if (int_4 >= 0)
					{
						chromDisplay_0.DrawL_end2();
					}
					Array.Resize(ref lineBE_0, 0);
					int_4 = -1;
				}
				else if (int_4 != i)
				{
					Array.Resize(ref lineBE_0, 1);
					lineBE_0[0].begin = Convert.ToInt32(chromDisplay_0.lgToScr(new PointF(num, 0f), bool_0: true).X);
					lineBE_0[0].int_0 = Convert.ToInt32(chromDisplay_0.lgToScr(new PointF(num2, 0f), bool_0: true).X);
					chromDisplay_0.DrawL2(lineBE_0);
					int_4 = i;
				}
				return;
			}
		}
		if (enum7_0 != Enum7.const_2)
		{
		}
	}

	private void dpDisplay_MouseUp(object sender, MouseEventArgs e)
	{
		if (IsDesignMode())
		{
			return;
		}
		chromDisplay_0.displayPanel.Cursor = Cursors.Default;
		if (chromDisplay_0.moving)
		{
			SetChromDisplayCurDisLg();
			SetDisZoomButtonEnableState();
		}
		if (chromDisplay_0.scaling)
		{
			chromDisplay_0.DrawScale_end();
			if (Math.Abs(chromDisplay_0.ptScaleBegin.X - chromDisplay_0.mouseLocation.X) > 10 && Math.Abs(chromDisplay_0.ptScaleBegin.Y - chromDisplay_0.mouseLocation.Y) > 10)
			{
				PointF pointF = chromDisplay_0.scrToLg(chromDisplay_0.ptScaleBegin, bool_0: true);
				PointF pointF2 = chromDisplay_0.scrToLg(chromDisplay_0.mouseLocation, bool_0: true);
				rectangleF_0.X = Math.Min(pointF.X, pointF2.X);
				rectangleF_0.Y = Math.Min(pointF.Y, pointF2.Y);
				rectangleF_0.Width = Math.Max(pointF.X, pointF2.X) - rectangleF_0.X;
				rectangleF_0.Height = Math.Max(pointF.Y, pointF2.Y) - rectangleF_0.Y;
				method_12(rectangleF_0.X, rectangleF_0.Width, rectangleF_0.Y, rectangleF_0.Height);
				chromDisplay_0.scaling = false;
				SetChromDisplayCurDisLg();
				SetDisZoomButtonEnableState();
			}
		}
		chromDisplay_0.moving = false;
		chromDisplay_0.scaling = false;
		if (e.Button == MouseButtons.Right && e.Location == point_0)
		{
			new Point(Cursor.Position.X + 1, Cursor.Position.Y);
		}
	}

	private void dpDisplay_Paint(object sender, PaintEventArgs e)
	{
		if (!IsDesignMode())
		{
			chromDisplay_0.Draw(e.Graphics, erase: true);
		}
	}

	private void SetChromDisplayCurDisLg()
	{
		dpDisplay.Refresh();
		if (chromDisplay_0.stDisChain.Count != 0)
		{
			disLg_0 = chromDisplay_0.stDisChain.CurDisLg;
		}
	}

	public void DpRefresh()
	{
		if (!IsDesignMode() && pnlCmpds.Visible)
		{
			SetChromDisplayCurDisLg();
		}
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
		e.Cancel = method_11(bool_2: true);
		bool_0 = false;
	}

	private void gvCmpds_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (bool_0)
		{
			if (gvCmpds.Columns[e.ColumnIndex].Name.Equals("CpmdName"))
			{
				(gvCmpds.Rows[e.RowIndex].Tag as Class74).tabPage_0.Text = gvCmpds.CurrentCell.Value.ToString();
			}
			ReadAndWriteCaliGnlData(AccStyle.Write);
			ReadAndWriteCompoundList(AccStyle.Read);
		}
		toolStripButton1_Click(null, null);
		ReadAndWriteCaliGnlData(AccStyle.Write);
		ReadAndWriteCompoundList(AccStyle.Read);
	}

	private void gvCmpds_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
		{
			bool_0 = true;
		}
	}

	private void gvCmpds_DoubleClick(object sender, EventArgs e)
	{
		if (gvCmpds.CurrentCell != null && !gvCmpds.CurrentCell.IsInEditMode && gvCmpds.CurrentCell.RowIndex >= 0 && gvCmpds.CurrentCell.OwningColumn.Name == "IstdCmpd" && !method_11(bool_2: true) && caliGnlIstdDlg_0.ShowDialog(gvCmpds, gvCmpds.CurrentCell.RowIndex, ref caliGnl_0.caliOption.caliDisMode) == DialogResult.OK)
		{
			ReadAndWriteCompoundOneRow(gvCmpds.CurrentCell.RowIndex, AccStyle.Read);
			ReadAndWriteCaliGnlData(AccStyle.Write);
			tcCmpds_SelectedIndexChanged(null, null);
		}
	}

	private void method_7(int int_5)
	{
		ReadAndWriteCompoundOneRow(int_5, AccStyle.Write);
		SetChromDisplayCurDisLg();
	}

	private void method_8()
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
			case "CriticalAmount":
				gvCmpds.Columns[i].HeaderText = Lang.PS("判定值", "CriticalAmount");
				break;
			}
		}
	}

	private bool SelectPeaks(float pkRT)
	{
		if (gvCmpds.RowCount == 0 || gvCmpds.Rows[0].Tag != null)
		{
			for (int i = 0; i < gvCmpds.RowCount; i++)
			{
				if (gvCmpds.Rows[i].Selected && (gvCmpds.Rows[i].Tag as Class74).compound_0.Contains(pkRT))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void gvCmpds_SelectionChanged(object sender, EventArgs e)
	{
		if (!bool_1 && CurSignal != null)
		{
			for (int i = 0; i < CurSignal.PeaksNum; i++)
			{
				CurSignal.peaks[i].selected = SelectPeaks(CurSignal.peaks[i].pkRT);
			}
			SetChromDisplayCurDisLg();
		}
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
				obj = compoundByName.cmpdInfo.name;
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

	private void method_9()
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
		gvCmpds.AddLclTextBoxColumn("IstdCmpd", 100, 0, StringAlignment.Near, readOnly: true);
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

	public void LoadLanguage()
	{
	}

	public void LoadOptions()
	{
		SetDisZoomButtonEnableState();
	}

	private void miCaliAddAll_Click(object sender, EventArgs e)
	{
		if (CurSignal == null)
		{
			return;
		}
		bool checkExists = !caliGnl_0.IsNull;
		for (int i = 0; i < CurSignal.PeaksNum; i++)
		{
			Peak peak = CurSignal.peaks[i];
			if (!CheckPeakExist(peak))
			{
				caliGnl_0.add_splLevel(checkExists, canAddNew: true, iLevel, peak.pkRT, peak.area, peak.height);
				continue;
			}
			slbExplain.Text = Lang.PS("不需要重复添加该组分.", "Already Add.");
		}
		caliGnl_0.CalculateFunc(appendLink: true);
		ReadAndWriteCaliGnlData(AccStyle.Read);
	}

	private void miCaliAddExists_Click(object sender, EventArgs e)
	{
		if (CurSignal == null || method_11(bool_2: true))
		{
			return;
		}
		for (int i = 0; i < CurSignal.PeaksNum; i++)
		{
			Peak peak = CurSignal.peaks[i];
			if (!CheckPeakExist(peak))
			{
				caliGnl_0.add_splLevel(checkExists: true, canAddNew: false, iLevel, peak.pkRT, peak.area, peak.height);
				continue;
			}
			slbExplain.Text = Lang.PS("不需要重复添加该组分.", "There is no need to repeat the addition of the component");
		}
		caliGnl_0.CalculateFunc(appendLink: true);
		ReadAndWriteCaliGnlData(AccStyle.Read);
	}

	private void miCaliAddGroup_Click(object sender, EventArgs e)
	{
		if (!method_11(bool_2: true))
		{
			chromDisplay_0.ExtDraw_begin2();
		}
	}

	private void miCaliAddPeak_Click(object sender, EventArgs e)
	{
		if (!method_11(bool_2: true))
		{
			if (miCaliAddPeak.Checked)
			{
				btnAddPeak.Checked = false;
				miCaliAddPeak.Checked = false;
				return;
			}
			enum7_0 = Enum7.const_1;
			int_4 = -1;
			btnAddPeak.Checked = true;
			miCaliAddPeak.Checked = true;
			chromDisplay_0.ExtDraw_begin2();
			chromDisplay_0.DrawL_begin();
		}
	}

	private void miCaliClearAllLevels_Click(object sender, EventArgs e)
	{
		if (!method_11(bool_2: true))
		{
			caliGnl_0.ClearLevels();
			caliGnl_0.CalculateFunc(appendLink: true);
			ReadAndWriteCaliGnlData(AccStyle.Read);
		}
	}

	private void miCaliClearSelectedLevel_Click(object sender, EventArgs e)
	{
		if (!method_11(bool_2: true))
		{
			caliGnl_0.ClearLevel(iLevel);
			caliGnl_0.CalculateFunc(appendLink: true);
			ReadAndWriteCaliGnlData(AccStyle.Read);
		}
	}

	private void miCaliDeleteAllCmpds_Click(object sender, EventArgs e)
	{
		if (!method_11(bool_2: true))
		{
			Array.Resize(ref caliGnl_0.cmpds, 0);
			caliGnl_0.CalculateFunc(appendLink: true);
			ReadAndWriteCaliGnlData(AccStyle.Read);
		}
	}

	private void miCaliDeleteCmpd_Click(object sender, EventArgs e)
	{
		if (gvCmpds.SelectedRows != null && gvCmpds.SelectedRows.Count != 0 && !method_11(bool_2: true))
		{
			Compound[] array = new Compound[gvCmpds.SelectedRows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (gvCmpds.SelectedRows[i].Tag as Class74).compound_0;
			}
			caliGnl_0.DeleteCmpds(array);
			caliGnl_0.CalculateFunc(appendLink: true);
			ReadAndWriteCaliGnlData(AccStyle.Read);
		}
	}

	private void miCaliOptions_Click(object sender, EventArgs e)
	{
		gvCmpds.EndEdit();
		if (caliGnlOptDlg_0.ShowDialog(caliGnl_0.caliOption, method_11(bool_2: false)) == DialogResult.OK)
		{
			caliGnl_0.SetRecaliMode(caliGnl_0.caliOption.recaliMode);
			tcCmpds_SelectedIndexChanged(null, null);
		}
	}

	private void lclNumericUpDown_0_ValueChanged(object sender, EventArgs e)
	{
		if (toolStripMenuItem_0 != null)
		{
			toolStripMenuItem_0.Checked = false;
		}
		if (sender is ToolStripMenuItem)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			toolStripMenuItem.Checked = true;
			iLevel = (int)toolStripMenuItem.Tag;
			lclNumericUpDown_0.Value = iLevel + 1;
			toolStripMenuItem_0 = toolStripMenuItem;
		}
		else if (sender is LclNumericUpDown)
		{
			LclNumericUpDown lclNumericUpDown = sender as LclNumericUpDown;
			iLevel = (int)lclNumericUpDown.Value - 1;
			for (int i = 0; i < 20; i++)
			{
				ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)miCaliSetLevel.DropDownItems[i];
				if ((int)toolStripMenuItem2.Tag == iLevel)
				{
					toolStripMenuItem_0 = toolStripMenuItem2;
					toolStripMenuItem2.Checked = true;
					break;
				}
			}
		}
		gvCmpds.SetCombineCText(int_3, "Level " + (iLevel + 1));
		ReadAndWriteCompoundList(AccStyle.Read);
	}

	private void miDisNextZoom_Click(object sender, EventArgs e)
	{
		chromDisplay_0.stDisChain.DynNo++;
		SetChromDisplayCurDisLg();
		SetDisZoomButtonEnableState();
	}

	private void dpDisplay_DoubleClick(object sender, EventArgs e)
	{
		chromDisplay_0.stDisChain.DynNo--;
		SetChromDisplayCurDisLg();
		SetDisZoomButtonEnableState();
	}

	private void miDisProperties_Click(object sender, EventArgs e)
	{
	}

	private void miDisUnzoom_Click(object sender, EventArgs e)
	{
		if (chromDisplay_0.SetFullDisLg(ref disLg_0, CurSignal, second: true))
		{
			SetChromDisplayCurDisLg();
			SetDisZoomButtonEnableState();
		}
	}

	private void miFiCloseChrom_Click(object sender, EventArgs e)
	{
		if (curChromatogram == null)
		{
			return;
		}
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i] == curChromatogram)
			{
				for (int j = i; j < chromatogram_0.Length - 1; j++)
				{
					chromatogram_0[j] = chromatogram_0[j + 1];
				}
				Array.Resize(ref chromatogram_0, chromatogram_0.Length - 1);
				curChromatogram = chromDisplay_0.LinkDisChroms(chromatogram_0, ref int_2);
				if (chromatogram_0.Length == 0)
				{
					chromDisplay_0.ClearDisSignals();
				}
				SetSignalsColor();
				SetChromDisplayCurDisLg();
				break;
			}
		}
	}

	public void miFiNewCali_Click(object sender, EventArgs e)
	{
		gvCmpds.EndEdit();
		m_strCalFile = "";
		SetThisFormText();
		if (miCaliSetLevel.DropDownItems.Count != 0)
		{
			lclNumericUpDown_0_ValueChanged(miCaliSetLevel.DropDownItems[0], null);
		}
		caliGnl_0.Clear();
		tcCmpds.SelectedTab = tpCL;
		ReadAndWriteCaliGnlData(AccStyle.Read);
	}

	private void miFiOpenCali_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = Filter;
		openFileDialog.Title = miFiOpenCali.Text;
		openFileDialog.FileName = "";
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			LoadFile(openFileDialog.FileName);
			tcCmpds_SelectedIndexChanged(null, null);
			refreshcboxPKpeakItem();
			if (caliGnl_0.PKPeakIndex >= 0)
			{
				cboxPKpeak.SelectedIndex = caliGnl_0.PKPeakIndex + 1;
			}
			if (caliGnl_0.UpDataPeakIndex >= 0)
			{
				cbUpPeak.SelectedIndex = caliGnl_0.UpDataPeakIndex + 1;
			}
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", "打开组份表文件", "打开组份表文件:" + openFileDialog.FileName);
		}
	}

	private void miFiOpenChrom_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "(*.sda)|*.sda";
		openFileDialog.Title = miFiOpenChrom.Text;
		openFileDialog.FileName = "";
		if (chromatogram_0.Length == 0)
		{
			chromDisplay_0.ClearDisSignals();
			chromDisplay_0.stDisChain.Clear();
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			OpenChrom(openFileDialog.FileName);
		}
	}

	private void miFiPreview_Click(object sender, EventArgs e)
	{
	}

	private void miFiPrint_Click(object sender, EventArgs e)
	{
		CreateDocStatistics("");
	}

	public Image[] GetAllImage()
	{
		Image[] array = new Image[tcCmpds.TabCount - 1];
		for (int i = 1; i < tcCmpds.TabCount; i++)
		{
			tcCmpds.SelectedIndex = i;
			Thread.Sleep(1000);
			Application.DoEvents();
			Control dpCmpd = ccCmpd.dpCmpd;
			Bitmap bitmap = new Bitmap(dpCmpd.Width, dpCmpd.Height);
			Graphics.FromImage(bitmap);
			Rectangle bounds = dpCmpd.Bounds;
			bounds.X = 0;
			bounds.Y = 0;
			dpCmpd.DrawToBitmap(bitmap, bounds);
			array[i - 1] = bitmap;
		}
		return array;
	}

	public void CreateDocStatistics(string docTemplatePath)
	{
		bool flag = true;
		string text = "D:\\" + Guid.NewGuid().ToString() + ".docx";
		FileStream fileStream = File.OpenWrite(text);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		xWPFParagraph.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText("校准批处理报告");
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph2.CreateRun();
		xWPFRun2.AppendText("校准总结");
		XWPFTable[] array = new XWPFTable[caliGnl_0.cmpds.Length + 1];
		array[0] = xWPFDocument.CreateTable(1, 8);
		array[0].GetRow(0).GetCell(0).SetText("峰名称");
		array[0].GetRow(0).GetCell(1).SetText("评估类型");
		array[0].GetRow(0).GetCell(2).SetText("校准类型");
		array[0].GetRow(0).GetCell(3).SetText("数据点");
		array[0].GetRow(0).GetCell(4).SetText("截距");
		array[0].GetRow(0).GetCell(5).SetText("斜率");
		array[0].GetRow(0).GetCell(6).SetText("曲率");
		array[0].GetRow(0).GetCell(7).SetText("判定系数");
		XWPFParagraph xWPFParagraph3 = xWPFDocument.CreateParagraph();
		xWPFParagraph3.CreateRun().AddCarriageReturn();
		xWPFParagraph3.CreateRun().AddCarriageReturn();
		int num = 0;
		for (int i = 0; i < 20 && caliGnl_0.cmpds[0].levels[i].used; i++)
		{
			num++;
		}
		for (int j = 0; j < caliGnl_0.cmpds.Length; j++)
		{
			XWPFTableRow xWPFTableRow = array[0].CreateRow();
			xWPFTableRow.GetCell(0).SetText(caliGnl_0.cmpds[j].cmpdInfo.name);
			if (caliGnl_0.cmpds[j].cmpdInfo.respStyle == RespStyle.Area)
			{
				xWPFTableRow.GetCell(1).SetText("峰面积");
			}
			else if (caliGnl_0.cmpds[j].cmpdInfo.respStyle == RespStyle.Height)
			{
				xWPFTableRow.GetCell(1).SetText("峰高");
			}
			else if (caliGnl_0.cmpds[j].cmpdInfo.respStyle == RespStyle.AreaSquare)
			{
				xWPFTableRow.GetCell(1).SetText("峰面积平方根");
			}
			else if (caliGnl_0.cmpds[j].cmpdInfo.respStyle == RespStyle.Height)
			{
				xWPFTableRow.GetCell(1).SetText("峰高平方根");
			}
			xWPFTableRow.GetCell(2).SetText(caliGnl_0.cmpds[j].eFunc.curveFit.ToString());
			xWPFTableRow.GetCell(3).SetText(num.ToString());
			xWPFTableRow.GetCell(4).SetText(caliGnl_0.cmpds[j].eFunc.disCoefs[0].ToString("0.000"));
			if (caliGnl_0.cmpds[j].eFunc.disCoefs.Length > 1)
			{
				xWPFTableRow.GetCell(5).SetText(caliGnl_0.cmpds[j].eFunc.disCoefs[1].ToString("0.000"));
			}
			xWPFTableRow.GetCell(6).SetText("0.000");
			xWPFTableRow.GetCell(7).SetText(caliGnl_0.cmpds[j].eFunc.corrFactor.ToString("0.0000"));
		}
		float[] array2 = new float[num];
		float[] array3 = new float[num];
		float[] array4 = new float[num];
		float[] array5 = new float[num];
		string[] array6 = new string[num];
		for (int k = 0; k < caliGnl_0.cmpds.Length; k++)
		{
			array[k + 1] = xWPFDocument.CreateTable(1, 6);
			array[k + 1].GetRow(0).GetCell(0).SetText("进样名称");
			array[k + 1].GetRow(0).GetCell(1).SetText("保留时间\r\n min");
			array[k + 1].GetRow(0).GetCell(2).SetText("峰面积");
			array[k + 1].GetRow(0).GetCell(3).SetText("峰高");
			array[k + 1].GetRow(0).GetCell(4).SetText("样品量");
			array[k + 1].GetRow(0).GetCell(5).SetText("曲线");
			array[k + 1].SetColumnWidth(0, 76800uL);
			array[k + 1].SetColumnWidth(1, 7680uL);
			array[k + 1].SetColumnWidth(2, 7680uL);
			XWPFParagraph xWPFParagraph4 = xWPFDocument.CreateParagraph();
			XWPFRun xWPFRun3 = xWPFParagraph4.CreateRun();
			CaliGnl caliGnl = caliGnl_0;
			Compound[] cmpds = caliGnl.cmpds;
			CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
			Size size = new Size(200, 150);
			RectangleF rectangleF = new RectangleF(0f, 0f, size.Width, size.Height);
			Compound compound = cmpds[k];
			if (compound.levels == null || compound.levels.Length == 0)
			{
				break;
			}
			Bitmap bitmap = new Bitmap(200, 150);
			Graphics graphics = Graphics.FromImage(bitmap);
			string string_ = Class49.MesureUnit() + ".s";
			if (compound.cmpdInfo.respStyle == RespStyle.Height)
			{
				string_ = Class49.MesureUnit();
			}
			cmpdDisplay.rcPage = rectangleF;
			cmpdDisplay.dskRC = rectangleF;
			cmpdDisplay.SetCompound2(compound, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
			cmpdDisplay.Draw(graphics, erase: true);
			graphics.Dispose();
			bitmap.Save("D:\\abc.bmp");
			FileStream fileStream2 = new FileStream("D:\\abc.bmp", FileMode.Open, FileAccess.Read);
			xWPFRun3.AddCarriageReturn();
			XWPFTableRow row = array[k + 1].GetRow(0);
			XWPFTableCell cell = row.GetCell(0);
			CT_Tc cTTc = cell.GetCTTc();
			CT_TcPr cT_TcPr = cTTc.AddNewTcPr();
			CT_Row row2 = new CT_Row();
			row = new XWPFTableRow(row2, array[k + 1]);
			array[k + 1].AddRow(row);
			cell = row.CreateCell();
			cell = row.CreateCell();
			cell = row.CreateCell();
			cell = row.CreateCell();
			cell = row.CreateCell();
			cell = row.CreateCell();
			cTTc = cell.GetCTTc();
			cT_TcPr = cTTc.AddNewTcPr();
			cT_TcPr.AddNewVMerge().val = ST_Merge.restart;
			cT_TcPr.AddNewVAlign().val = ST_VerticalJc.center;
			array[k + 1].RemoveRow(1);
			for (int l = 0; l < num; l++)
			{
				row2 = new CT_Row();
				row = new XWPFTableRow(row2, array[k + 1]);
				array[k + 1].AddRow(row);
				cell = row.CreateCell();
				cell = row.CreateCell();
				cell = row.CreateCell();
				cell = row.CreateCell();
				cell = row.CreateCell();
				cell = row.CreateCell();
				cTTc = cell.GetCTTc();
				cT_TcPr = cTTc.AddNewTcPr();
				cT_TcPr.AddNewVMerge().val = ST_Merge.@continue;
				array2[l] = caliGnl_0.cmpds[k].levels[l].respFactor;
				array3[l] = caliGnl_0.cmpds[k].levels[l].responseA;
				array4[l] = caliGnl_0.cmpds[k].levels[l].responseH;
				array5[l] = caliGnl_0.cmpds[k].levels[l].amount;
				array6[l] = caliGnl_0.cmpds[k].cmpdInfo.name.PadRightWhileDouble(21, ' ');
				array[k + 1].GetRow(l + 1).GetCell(0).SetText(array6[l]);
				array[k + 1].GetRow(l + 1).GetCell(1).SetText(array2[l].ToString("0.000"));
				array[k + 1].GetRow(l + 1).GetCell(2).SetText(array3[l].ToString("0.0000"));
				array[k + 1].GetRow(l + 1).GetCell(3).SetText(array4[l].ToString("0.0000"));
				array[k + 1].GetRow(l + 1).GetCell(4).SetText(array5[l].ToString("0.000"));
			}
			array[k + 1].CreateRow();
			array[k + 1].CreateRow();
			array[k + 1].GetRow(num + 1).GetCell(0).SetText("平均值");
			array[k + 1].GetRow(num + 2).GetCell(0).SetText("相对标准偏差");
			float num2 = 0f;
			for (int m = 0; m < array2.Length; m++)
			{
				num2 += array2[m];
			}
			array[k + 1].GetRow(num + 1).GetCell(1).SetText((num2 / (float)array2.Length).ToString("0.000"));
			array[k + 1].GetRow(num + 2).GetCell(1).SetText(Program.RSDCalculate(num2 / (float)array2.Length, array2, array2.Length).ToString("0.000"));
			XWPFParagraph xWPFParagraph5 = array[k + 1].GetRow(1).GetCell(5).AddParagraph();
			XWPFRun xWPFRun4 = xWPFParagraph5.CreateRun();
			xWPFRun4.AddPicture(fileStream2, 11, "D:\\abc.bmp", 1905000, 952500);
			fileStream2.Close();
		}
		CT_TcPr cT_TcPr2 = array[0].GetRow(0).GetCell(0).GetCTTc()
			.AddNewTcPr();
		cT_TcPr2.tcW = new CT_TblWidth();
		cT_TcPr2.tcW.w = "1700";
		cT_TcPr2.tcW.type = ST_TblWidth.dxa;
		for (int n = 0; n < 7; n++)
		{
			cT_TcPr2 = array[0].GetRow(0).GetCell(n + 1).GetCTTc()
				.AddNewTcPr();
			cT_TcPr2.tcW = new CT_TblWidth();
			cT_TcPr2.tcW.w = "950";
			cT_TcPr2.tcW.type = ST_TblWidth.dxa;
		}
		for (int num3 = 1; num3 < array.Length; num3++)
		{
			for (int num4 = 0; num4 < 6; cT_TcPr2.tcW.type = ST_TblWidth.dxa, num4++)
			{
				cT_TcPr2 = array[num3].GetRow(0).GetCell(num4).GetCTTc()
					.AddNewTcPr();
				cT_TcPr2.tcW = new CT_TblWidth();
				switch (num4)
				{
				case 0:
					cT_TcPr2.tcW.w = "1700";
					continue;
				case 5:
					if (num3 != 0)
					{
						cT_TcPr2.tcW.w = "3000";
						continue;
					}
					break;
				}
				cT_TcPr2.tcW.w = "950";
			}
		}
		xWPFDocument.Write(fileStream);
		Process.Start(text);
	}

	private void miFiReportSetup_Click(object sender, EventArgs e)
	{
	}

	private void miFiSaveAsCali_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.访问员)
		{
			MessageBox.Show("没有编辑组份表权限！");
		}
		else if (Class49.user_0.ULevel == User.Level.检验员)
		{
			MessageBox.Show("没有编辑组份表权限！");
		}
		else if (caliGnl_0 != null)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "(*.cal)|*.cal";
			saveFileDialog.Title = Lang.PS("保存", "save");
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				m_strCalFile = saveFileDialog.FileName;
				caliGnl_0.SaveToFileV11(m_strCalFile, Class49.user_0.u_name);
			}
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", Lang.PS("另存组份表文件"), Lang.PS("另存组份表文件:") + m_strCalFile);
		}
	}

	private void miFiSaveCali_Click(object sender, EventArgs e)
	{
		DateTime dateTime = default(DateTime);
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i].injAnalysis.dtAcquire > dateTime)
			{
				dateTime = chromatogram_0[i].injAnalysis.dtAcquire;
			}
		}
		if (chromatogram_0.Length != 0)
		{
			for (int j = 0; j < caliGnl_0.cmpds.Length; j++)
			{
				caliGnl_0.cmpds[j].cmpdInfo.BLString[0] = dateTime.ToString("s");
			}
		}
		miFiSaveAsCali_Click(null, null);
	}

	private bool CheckCmpdNameDuplication(out string strCmpdName)
	{
		strCmpdName = "";
		if (caliGnl_0 == null || caliGnl_0.cmpds == null)
		{
			return false;
		}
		for (int i = 0; i < caliGnl_0.cmpds.Length; i++)
		{
			string name = caliGnl_0.cmpds[i].cmpdInfo.name;
			int cmpdNameCount = caliGnl_0.GetCmpdNameCount(name);
			if (cmpdNameCount > 1)
			{
				strCmpdName = name;
				return true;
			}
		}
		return false;
	}

	private bool method_11(bool bool_2)
	{
		return false;
	}

	private void method_12(float float_0, float float_1, float float_2, float float_3)
	{
		disLg_0.lgXBeg = float_0;
		disLg_0.lgX = float_1;
		disLg_0.lgYBeg = float_2;
		disLg_0.lgY = float_3;
		chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
	}

	private void ReadAndWriteCaliGnlData(AccStyle accStyle_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			SetPeakListCompound();
			bool_1 = true;
			gvCmpds.RowCount = caliGnl_0.cmpds.Length;
			bool_1 = false;
			int num = gvCmpds.RowCount + 1;
			while (tcCmpds.TabCount < num)
			{
				tcCmpds.TabPages.Add(new TabPage(""));
			}
			while (tcCmpds.TabCount > num)
			{
				tcCmpds.TabPages[tcCmpds.TabCount - 1].Dispose();
			}
			for (int i = 0; i < gvCmpds.RowCount; i++)
			{
				Class74 class2;
				if (gvCmpds.Rows[i].Tag == null)
				{
					object obj = (gvCmpds.Rows[i].Tag = new Class74(caliGnl_0.cmpds[i], tcCmpds.TabPages[i + 1]));
					class2 = (Class74)obj;
				}
				else
				{
					class2 = gvCmpds.Rows[i].Tag as Class74;
					class2.compound_0 = caliGnl_0.cmpds[i];
					class2.tabPage_0 = tcCmpds.TabPages[i + 1];
				}
				class2.tabPage_0.Text = class2.compound_0.cmpdInfo.name;
			}
			ReadAndWriteCompoundList(AccStyle.Read);
			refreshcboxPKpeakItem();
			break;
		}
		case AccStyle.Write:
			if (caliGnl_0.cmpds.Length != gvCmpds.RowCount)
			{
			}
			gvCmpds.EndEdit();
			ReadAndWriteCompoundList(AccStyle.Write);
			caliGnl_0.CalculateFunc(appendLink: true);
			break;
		}
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
		Compound compound = (gvCmpds.Rows[int_5].Tag as Class74).compound_0;
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
					(gvCmpds.Rows[int_5].Cells[j] as LclgvColorCell).Color = compound.cmpdInfo.color;
					continue;
				case "RespArea":
					obj = compound.levels[iLevel].responseA;
					break;
				case "RespHeight":
					obj = compound.levels[iLevel].responseH;
					break;
				case "Amount":
					obj = compound.levels[iLevel].amount;
					break;
				case "RecordNumber":
					obj = compound.levels[iLevel].SecsNum;
					break;
				default:
					obj = gvCmpdsValue(gvUse: true, compound, gvCmpds.Columns[j].Name);
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
						compound.cmpdInfo.color = (gvCmpds.Rows[int_5].Cells[i] as LclgvColorCell).Color;
						break;
					case "RespStyle":
						compound.cmpdInfo.respStyle = (RespStyle)obj;
						break;
					case "RespArea":
						compound.levels[iLevel].responseA = Class49.String2Float(obj, compound.levels[iLevel].responseA);
						break;
					case "RespHeight":
						compound.levels[iLevel].responseH = Class49.String2Float(obj, compound.levels[iLevel].responseH);
						break;
					case "FreeRespFactor":
						compound.cmpdInfo.freeRespFactor = Class49.String2Float(obj, compound.cmpdInfo.freeRespFactor);
						break;
					case "Amount":
						compound.levels[iLevel].amount = Class49.String2Float(obj, compound.levels[iLevel].amount);
						break;
					case "IstdCmpds":
						compound.cmpdInfo.sl_IstdCmpdNo = (int)Class49.String2Float(obj, compound.cmpdInfo.sl_IstdCmpdNo);
						break;
					}
				}
			}
			break;
		}
		}
	}

	public void refresh_once()
	{
		SetThisFormText();
		chromDisplay_0.ExtDraw_begin();
	}

	private void SetDisZoomButtonEnableState()
	{
		ToolStripMenuItem toolStripMenuItem = miDisPreviousZoom;
		bool enabled = (btnPreviousZoom.Enabled = chromDisplay_0.stDisChain.HasPrevious);
		toolStripMenuItem.Enabled = enabled;
		ToolStripMenuItem toolStripMenuItem2 = miDisNextZoom;
		enabled = (btnNextZoom.Enabled = chromDisplay_0.stDisChain.HasNext);
		toolStripMenuItem2.Enabled = enabled;
	}

	private void SetThisFormText()
	{
		Text = Lang.PS("定量组份编辑", "Calibration") + ": " + m_strCalFile;
	}

	private void SetPeakListCompound()
	{
		if (CurSignal == null || caliGnl_0 == null)
		{
			return;
		}
		for (int i = 0; i < CurSignal.PeaksNum; i++)
		{
			if (CurSignal.peaks == null || CurSignal.peaks[i] == null)
			{
				throw new Exception("this.CurSignal.peaks为空，其中i=" + i);
			}
			caliGnl_0.SetCompound(CurSignal.peaks[i], CurSignal.peaks, i);
		}
	}

	public void SetSignalsColor()
	{
	}

	private void tcCmpds_SelectedIndexChanged(object sender, EventArgs e)
	{
		ToolStripButton toolStripButton = btnAddAll;
		ToolStripMenuItem toolStripMenuItem = miCaliAddAll;
		ToolStripButton toolStripButton2 = btnAddExists;
		ToolStripMenuItem toolStripMenuItem2 = miCaliAddExists;
		ToolStripButton toolStripButton3 = btnAddPeak;
		ToolStripMenuItem toolStripMenuItem3 = miCaliAddPeak;
		ToolStripButton toolStripButton4 = btnAddGroup;
		ToolStripMenuItem toolStripMenuItem4 = miCaliAddGroup;
		LclNumericUpDown lclNumericUpDown = lclNumericUpDown_0;
		ToolStripMenuItem toolStripMenuItem5 = miCaliSetLevel;
		ToolStripButton toolStripButton5 = btnDeleteCmpd;
		bool flag = (miCaliDeleteCmpd.Enabled = tcCmpds.SelectedIndex == 0);
		bool flag3 = (toolStripButton5.Enabled = flag);
		bool flag5 = (toolStripMenuItem5.Enabled = flag3);
		bool flag7 = (lclNumericUpDown.Enabled = flag5);
		bool flag9 = (toolStripMenuItem4.Enabled = flag7);
		bool flag11 = (toolStripButton4.Enabled = flag9);
		bool flag13 = (toolStripMenuItem3.Enabled = flag11);
		bool flag15 = (toolStripButton3.Enabled = flag13);
		bool flag17 = (toolStripMenuItem2.Enabled = flag15);
		bool flag19 = (toolStripButton2.Enabled = flag17);
		bool enabled = (toolStripMenuItem.Enabled = flag19);
		toolStripButton.Enabled = enabled;
		pnlCmpds.Visible = tcCmpds.SelectedIndex == 0;
		ReadAndWriteCaliGnlData(AccStyle.Read);
		if (!pnlCmpds.Visible)
		{
			compound_0 = null;
			if (gvCmpds.RowCount != 0)
			{
				int num = 0;
				while (num < gvCmpds.RowCount)
				{
					if ((gvCmpds.Rows[num].Tag as Class74).tabPage_0 == tcCmpds.SelectedTab)
					{
						compound_0 = (gvCmpds.Rows[num].Tag as Class74).compound_0;
						if (compound_0 != null)
						{
							caliGnl_0.CalculateFunc(appendLink: true);
							ccCmpd.LoadCompound(curChromatogram, compound_0);
							break;
						}
					}
					else
					{
						num++;
					}
				}
			}
		}
		if (tcCmpds.SelectedIndex == 0)
		{
			toolStripButton1_Click(null, null);
		}
		ccCmpd.ShowCompound();
	}

	private void CaliGnlForm_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
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
					Compound compound = (gvCmpds.Rows[j].Tag as Class74).compound_0;
					double num = 0.0;
					double num2 = 0.0;
					int num3 = 0;
					for (int k = 0; k < 20; k++)
					{
						if (compound.levels[k].used)
						{
							num3++;
							num += double.Parse(gvCmpdValue(gvUse: true, compound.levels[k], "RespFactor").ToString());
						}
					}
					if (num3 != 0)
					{
						num2 = num / (double)num3;
					}
					if (num2 != 0.0)
					{
						gvCmpds.Rows[j].Cells["FreeRespFactor"].Value = num2;
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
				Compound compound2 = (gvCmpds.Rows[m].Tag as Class74).compound_0;
				if (!double.IsNaN(compound2.iFunc.disCoefs[1]))
				{
					gvCmpds.Rows[m].Cells["FreeRespFactor"].Value = 1.0 / ((compound2.iFunc.disCoefs == null) ? 1.0 : compound2.iFunc.disCoefs[1]);
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
		catch
		{
		}
	}

	private void cboxPKpeak_SelectedIndexChanged(object sender, EventArgs e)
	{
		caliGnl_0.PKPeakIndex = cboxPKpeak.SelectedIndex - 1;
	}

	private void gvCmpd_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void gvCmpds_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void miAddrow_Click(object sender, EventArgs e)
	{
		Peak peak = new Peak();
		caliGnl_0.add_splLevel(checkExists: true, canAddNew: true, iLevel, peak.pkRT, peak.area, peak.height);
		caliGnl_0.CalculateFunc(appendLink: true);
		ReadAndWriteCaliGnlData(AccStyle.Read);
	}

	private void cbUpPeak_SelectedIndexChanged(object sender, EventArgs e)
	{
		caliGnl_0.UpDataPeakIndex = cbUpPeak.SelectedIndex - 1;
	}

	public object gvCmpdValue(bool gvUse, Level level, string columnName)
	{
		return ccCmpd.gvCmpdValue(gvUse, level, columnName);
	}

	public void GetCmpdDisColumns(ref GvInfos gvInfos)
	{
	}

	public void OpenChrom(string fileName)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(fileName, DetectorStyle.General);
		if (chromatogram != null)
		{
			OpenChrom(chromatogram);
		}
	}

	public void OpenChrom(Chromatogram chromatogram)
	{
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i].fullName == chromatogram.fullName)
			{
				MessageBox.Show("已打开谱图!");
				return;
			}
		}
		if (chromatogram_0.Length == 0)
		{
			chromatogram.signal.refresh_TimeValue();
			if (chromDisplay_0.SetFullDisLg(ref disLg_0, chromatogram.signal, second: false))
			{
				int_2 = 0;
			}
			curChromatogram = chromatogram;
			SetDisZoomButtonEnableState();
		}
		Array.Resize(ref chromatogram_0, chromatogram_0.Length + 1);
		chromatogram_0[chromatogram_0.Length - 1] = chromatogram;
		curChromatogram = chromDisplay_0.LinkDisChroms(chromatogram_0, ref int_2);
		SetSignalsColor();
		LoadOptions();
		if (mtdSetup.caliGnl.IsNull && curChromatogram.mtdSetup != null)
		{
			LoadFilefromObj(curChromatogram.mtdSetup);
			caliGnl_0.Clear();
		}
		SetPeakListCompound();
		gvCmpds_SelectionChanged(null, null);
		dpDisplay.Refresh();
	}

	public void LoadFile(CaliGnl cali)
	{
		if (cali != null && !cali.IsNull)
		{
			LoadFilefromObj(cali);
		}
	}

	public bool LoadFile(string caliFileName)
	{
		CaliGnl caliGnl = CaliGnl.LoadFromFile(caliFileName);
		if (caliGnl == null)
		{
			return false;
		}
		m_strCalFile = caliFileName;
		LoadFilefromObj(caliGnl);
		return true;
	}

	public void LoadFilefromObj(CaliGnl cali)
	{
		mtdSetup.caliGnl = cali.Copy();
		UpdateCurrentCaliGnlData();
	}

	public void LoadFilefromObj(MtdSetup mtd)
	{
		mtdSetup = mtd.Copy();
		caliGnl_0.CalculateFunc(appendLink: false);
		SetThisFormText();
		lclNumericUpDown_0_ValueChanged(miCaliSetLevel.DropDownItems[0], null);
		gvCmpds_SelectionChanged(null, null);
	}

	public void UpdateCurrentCaliGnlData()
	{
		caliGnl_0.CalculateFunc(appendLink: false);
		SetThisFormText();
		lclNumericUpDown_0_ValueChanged(miCaliSetLevel.DropDownItems[0], null);
		ReadAndWriteCaliGnlData(AccStyle.Read);
		gvCmpds_SelectionChanged(null, null);
		if (caliGnl_0.PKPeakIndex >= 0)
		{
			cboxPKpeak.SelectedIndex = caliGnl_0.PKPeakIndex + 1;
		}
		if (caliGnl_0.UpDataPeakIndex >= 0)
		{
			cbUpPeak.SelectedIndex = caliGnl_0.UpDataPeakIndex + 1;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.CaliGnlUserCtrl));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.ssCali = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsCali = new System.Windows.Forms.ToolStrip();
		this.btnNewCali = new System.Windows.Forms.ToolStripButton();
		this.btnOpenCali = new System.Windows.Forms.ToolStripButton();
		this.btnSaveCali = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.btnOpenChrom = new System.Windows.Forms.ToolStripButton();
		this.btnCloseChrom = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.btnPreviousZoom = new System.Windows.Forms.ToolStripButton();
		this.btnNextZoom = new System.Windows.Forms.ToolStripButton();
		this.btnUnzoom = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.btnAddAll = new System.Windows.Forms.ToolStripButton();
		this.btnAddExists = new System.Windows.Forms.ToolStripButton();
		this.btnAddPeak = new System.Windows.Forms.ToolStripButton();
		this.btnAddGroup = new System.Windows.Forms.ToolStripButton();
		this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.btnDeleteCmpd = new System.Windows.Forms.ToolStripButton();
		this.btnOptions = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
		this.cboxPKpeak = new System.Windows.Forms.ToolStripComboBox();
		this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
		this.cbUpPeak = new System.Windows.Forms.ToolStripComboBox();
		this.msCali = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiNewCali = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOpenCali = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveCali = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveAsCali = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiOpenChrom = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiCloseChrom = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiReportSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiPreview = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiPrint = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisplay = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisPreviousZoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisNextZoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisUnzoom = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.miDisProperties = new System.Windows.Forms.ToolStripMenuItem();
		this.miCalibration = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliSetLevel = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliAddAll = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliAddExists = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliAddPeak = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliAddGroup = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliDeleteCmpd = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliDeleteAllCmpds = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.miCaliClearAllLevels = new System.Windows.Forms.ToolStripMenuItem();
		this.miCaliClearSelectedLevel = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
		this.miCaliOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.cmsCali = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miAddrow = new System.Windows.Forms.ToolStripMenuItem();
		this.miColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.pnlFill = new System.Windows.Forms.Panel();
		this.ccCmpd = new IBrainChrom2018.CaliGnlCurveCtrl();
		this.pnlCmpds = new IBrainChrom2018.LclPanel();
		this.gvCmpds = new IBrainChrom2018.LclCombineCGridView();
		this.splt = new IBrainChrom2018.LclSplitter();
		this.dpDisplay = new IBrainChrom2018.LclDisplayPanel();
		this.tcCmpds = new IBrainChrom2018.LclTabControl();
		this.tpCL = new System.Windows.Forms.TabPage();
		this.ssCali.SuspendLayout();
		this.tsCali.SuspendLayout();
		this.msCali.SuspendLayout();
		this.cmsCali.SuspendLayout();
		this.pnlFill.SuspendLayout();
		this.pnlCmpds.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvCmpds).BeginInit();
		this.tcCmpds.SuspendLayout();
		base.SuspendLayout();
		this.ssCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.slbExplain });
		this.ssCali.Location = new System.Drawing.Point(0, 541);
		this.ssCali.Name = "ssCali";
		this.ssCali.Size = new System.Drawing.Size(1038, 22);
		this.ssCali.TabIndex = 4;
		this.ssCali.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(80, 17);
		this.slbExplain.Text = "普通色谱校正";
		this.tsCali.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.tsCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[26]
		{
			this.btnNewCali, this.btnOpenCali, this.btnSaveCali, this.toolStripSeparator7, this.btnOpenChrom, this.btnCloseChrom, this.toolStripSeparator8, this.btnPreviousZoom, this.btnNextZoom, this.btnUnzoom,
			this.toolStripSeparator9, this.btnAddAll, this.btnAddExists, this.btnAddPeak, this.btnAddGroup, this.toolStripLabel1, this.btnDeleteCmpd, this.btnOptions, this.toolStripSeparator10, this.toolStripButton1,
			this.toolStripSeparator4, this.toolStripLabel2, this.cboxPKpeak, this.toolStripSeparator12, this.toolStripLabel3, this.cbUpPeak
		});
		this.tsCali.Location = new System.Drawing.Point(0, 25);
		this.tsCali.Name = "tsCali";
		this.tsCali.Size = new System.Drawing.Size(1038, 39);
		this.tsCali.TabIndex = 6;
		this.tsCali.Text = "toolStrip1";
		this.btnNewCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNewCali.Image = (System.Drawing.Image)resources.GetObject("btnNewCali.Image");
		this.btnNewCali.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNewCali.Name = "btnNewCali";
		this.btnNewCali.Size = new System.Drawing.Size(36, 36);
		this.btnNewCali.Text = "新建组份表";
		this.btnNewCali.ToolTipText = "新建组份表";
		this.btnNewCali.Click += new System.EventHandler(miFiNewCali_Click);
		this.btnOpenCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpenCali.Image = (System.Drawing.Image)resources.GetObject("btnOpenCali.Image");
		this.btnOpenCali.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpenCali.Name = "btnOpenCali";
		this.btnOpenCali.Size = new System.Drawing.Size(36, 36);
		this.btnOpenCali.Text = "打开组份表";
		this.btnOpenCali.ToolTipText = "打开组份表";
		this.btnOpenCali.Click += new System.EventHandler(miFiOpenCali_Click);
		this.btnSaveCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSaveCali.Image = (System.Drawing.Image)resources.GetObject("btnSaveCali.Image");
		this.btnSaveCali.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSaveCali.Name = "btnSaveCali";
		this.btnSaveCali.Size = new System.Drawing.Size(36, 36);
		this.btnSaveCali.Text = "保存组份表";
		this.btnSaveCali.ToolTipText = "保存组份表";
		this.btnSaveCali.Click += new System.EventHandler(miFiSaveCali_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
		this.btnOpenChrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpenChrom.Image = (System.Drawing.Image)resources.GetObject("btnOpenChrom.Image");
		this.btnOpenChrom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpenChrom.Name = "btnOpenChrom";
		this.btnOpenChrom.Size = new System.Drawing.Size(36, 36);
		this.btnOpenChrom.Text = "打开标样";
		this.btnOpenChrom.ToolTipText = "打开标样";
		this.btnOpenChrom.Click += new System.EventHandler(miFiOpenChrom_Click);
		this.btnCloseChrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnCloseChrom.Image = (System.Drawing.Image)resources.GetObject("btnCloseChrom.Image");
		this.btnCloseChrom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnCloseChrom.Name = "btnCloseChrom";
		this.btnCloseChrom.Size = new System.Drawing.Size(36, 36);
		this.btnCloseChrom.Text = "关闭标样";
		this.btnCloseChrom.ToolTipText = "关闭标样";
		this.btnCloseChrom.Click += new System.EventHandler(miFiCloseChrom_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(6, 39);
		this.btnPreviousZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreviousZoom.Image = (System.Drawing.Image)resources.GetObject("btnPreviousZoom.Image");
		this.btnPreviousZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreviousZoom.Name = "btnPreviousZoom";
		this.btnPreviousZoom.Size = new System.Drawing.Size(36, 36);
		this.btnPreviousZoom.Text = "上一视图";
		this.btnPreviousZoom.ToolTipText = "上一视图";
		this.btnPreviousZoom.Click += new System.EventHandler(dpDisplay_DoubleClick);
		this.btnNextZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNextZoom.Image = (System.Drawing.Image)resources.GetObject("btnNextZoom.Image");
		this.btnNextZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNextZoom.Name = "btnNextZoom";
		this.btnNextZoom.Size = new System.Drawing.Size(36, 36);
		this.btnNextZoom.Text = "下一视图";
		this.btnNextZoom.ToolTipText = "下一视图";
		this.btnNextZoom.Click += new System.EventHandler(miDisNextZoom_Click);
		this.btnUnzoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnUnzoom.Image = (System.Drawing.Image)resources.GetObject("btnUnzoom.Image");
		this.btnUnzoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnUnzoom.Name = "btnUnzoom";
		this.btnUnzoom.Size = new System.Drawing.Size(36, 36);
		this.btnUnzoom.Text = "原始视图";
		this.btnUnzoom.ToolTipText = "原始视图";
		this.btnUnzoom.Click += new System.EventHandler(miDisUnzoom_Click);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(6, 39);
		this.btnAddAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAddAll.Image = (System.Drawing.Image)resources.GetObject("btnAddAll.Image");
		this.btnAddAll.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAddAll.Name = "btnAddAll";
		this.btnAddAll.Size = new System.Drawing.Size(36, 36);
		this.btnAddAll.Text = "添加所有峰";
		this.btnAddAll.Click += new System.EventHandler(miCaliAddAll_Click);
		this.btnAddExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAddExists.Image = (System.Drawing.Image)resources.GetObject("btnAddExists.Image");
		this.btnAddExists.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAddExists.Name = "btnAddExists";
		this.btnAddExists.Size = new System.Drawing.Size(36, 36);
		this.btnAddExists.Text = "添加已有组份";
		this.btnAddExists.Click += new System.EventHandler(miCaliAddExists_Click);
		this.btnAddPeak.AutoToolTip = false;
		this.btnAddPeak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAddPeak.Image = (System.Drawing.Image)resources.GetObject("btnAddPeak.Image");
		this.btnAddPeak.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAddPeak.Name = "btnAddPeak";
		this.btnAddPeak.Size = new System.Drawing.Size(36, 36);
		this.btnAddPeak.Text = "添加峰";
		this.btnAddPeak.ToolTipText = "添加峰";
		this.btnAddPeak.Click += new System.EventHandler(miCaliAddPeak_Click);
		this.btnAddGroup.AutoToolTip = false;
		this.btnAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAddGroup.Image = (System.Drawing.Image)resources.GetObject("btnAddGroup.Image");
		this.btnAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAddGroup.Name = "btnAddGroup";
		this.btnAddGroup.Size = new System.Drawing.Size(36, 36);
		this.btnAddGroup.Text = "添加分组";
		this.btnAddGroup.ToolTipText = "添加分组";
		this.btnAddGroup.Click += new System.EventHandler(miCaliAddGroup_Click);
		this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(35, 36);
		this.toolStripLabel1.Text = "级别:";
		this.btnDeleteCmpd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnDeleteCmpd.Image = (System.Drawing.Image)resources.GetObject("btnDeleteCmpd.Image");
		this.btnDeleteCmpd.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnDeleteCmpd.Name = "btnDeleteCmpd";
		this.btnDeleteCmpd.Size = new System.Drawing.Size(36, 36);
		this.btnDeleteCmpd.Text = "删除组分";
		this.btnDeleteCmpd.Click += new System.EventHandler(miCaliDeleteCmpd_Click);
		this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOptions.Image = (System.Drawing.Image)resources.GetObject("btnOptions.Image");
		this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOptions.Name = "btnOptions";
		this.btnOptions.Size = new System.Drawing.Size(36, 36);
		this.btnOptions.Text = "选项...";
		this.btnOptions.Click += new System.EventHandler(miCaliOptions_Click);
		this.toolStripSeparator10.Name = "toolStripSeparator10";
		this.toolStripSeparator10.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton1.Text = "取校正因子";
		this.toolStripButton1.ToolTipText = "取校正因子";
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
		this.toolStripLabel2.ForeColor = System.Drawing.Color.Blue;
		this.toolStripLabel2.Name = "toolStripLabel2";
		this.toolStripLabel2.Size = new System.Drawing.Size(163, 36);
		this.toolStripLabel2.Text = "参比峰(建议使用面积最大峰):";
		this.cboxPKpeak.AutoSize = false;
		this.cboxPKpeak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboxPKpeak.DropDownWidth = 40;
		this.cboxPKpeak.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		this.cboxPKpeak.IntegralHeight = false;
		this.cboxPKpeak.MaxDropDownItems = 100;
		this.cboxPKpeak.Name = "cboxPKpeak";
		this.cboxPKpeak.Size = new System.Drawing.Size(50, 25);
		this.cboxPKpeak.Sorted = true;
		this.cboxPKpeak.SelectedIndexChanged += new System.EventHandler(cboxPKpeak_SelectedIndexChanged);
		this.toolStripSeparator12.Name = "toolStripSeparator12";
		this.toolStripSeparator12.Size = new System.Drawing.Size(6, 39);
		this.toolStripLabel3.Name = "toolStripLabel3";
		this.toolStripLabel3.Size = new System.Drawing.Size(47, 36);
		this.toolStripLabel3.Text = "监测峰:";
		this.cbUpPeak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbUpPeak.DropDownWidth = 40;
		this.cbUpPeak.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
		this.cbUpPeak.Name = "cbUpPeak";
		this.cbUpPeak.Size = new System.Drawing.Size(75, 39);
		this.cbUpPeak.SelectedIndexChanged += new System.EventHandler(cbUpPeak_SelectedIndexChanged);
		this.msCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miFile, this.miDisplay, this.miCalibration });
		this.msCali.Location = new System.Drawing.Point(0, 0);
		this.msCali.Name = "msCali";
		this.msCali.Size = new System.Drawing.Size(1038, 25);
		this.msCali.TabIndex = 5;
		this.msCali.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.miFiNewCali, this.miFiOpenCali, this.miFiSaveCali, this.miFiSaveAsCali, this.toolStripSeparator1, this.miFiOpenChrom, this.miFiCloseChrom, this.toolStripSeparator2, this.miFiReportSetup, this.miFiPreview,
			this.miFiPrint, this.toolStripSeparator3, this.miFiExit
		});
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(44, 21);
		this.miFile.Text = "文件";
		this.miFiNewCali.Name = "miFiNewCali";
		this.miFiNewCali.Size = new System.Drawing.Size(145, 22);
		this.miFiNewCali.Text = "新建";
		this.miFiNewCali.Click += new System.EventHandler(miFiNewCali_Click);
		this.miFiOpenCali.Name = "miFiOpenCali";
		this.miFiOpenCali.Size = new System.Drawing.Size(145, 22);
		this.miFiOpenCali.Text = "打开组份表...";
		this.miFiOpenCali.Click += new System.EventHandler(miFiOpenCali_Click);
		this.miFiSaveCali.Name = "miFiSaveCali";
		this.miFiSaveCali.Size = new System.Drawing.Size(145, 22);
		this.miFiSaveCali.Text = "保存";
		this.miFiSaveCali.Click += new System.EventHandler(miFiSaveCali_Click);
		this.miFiSaveAsCali.Name = "miFiSaveAsCali";
		this.miFiSaveAsCali.Size = new System.Drawing.Size(145, 22);
		this.miFiSaveAsCali.Text = "另存...";
		this.miFiSaveAsCali.Click += new System.EventHandler(miFiSaveAsCali_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
		this.miFiOpenChrom.Name = "miFiOpenChrom";
		this.miFiOpenChrom.Size = new System.Drawing.Size(145, 22);
		this.miFiOpenChrom.Text = "打开标样";
		this.miFiOpenChrom.Click += new System.EventHandler(miFiOpenChrom_Click);
		this.miFiCloseChrom.Name = "miFiCloseChrom";
		this.miFiCloseChrom.Size = new System.Drawing.Size(145, 22);
		this.miFiCloseChrom.Text = "关闭标样";
		this.miFiCloseChrom.Click += new System.EventHandler(miFiCloseChrom_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
		this.miFiReportSetup.Name = "miFiReportSetup";
		this.miFiReportSetup.Size = new System.Drawing.Size(145, 22);
		this.miFiReportSetup.Text = "样式文件...";
		this.miFiReportSetup.Visible = false;
		this.miFiReportSetup.Click += new System.EventHandler(miFiReportSetup_Click);
		this.miFiPreview.Name = "miFiPreview";
		this.miFiPreview.Size = new System.Drawing.Size(145, 22);
		this.miFiPreview.Text = "预览";
		this.miFiPreview.Visible = false;
		this.miFiPreview.Click += new System.EventHandler(miFiPreview_Click);
		this.miFiPrint.Name = "miFiPrint";
		this.miFiPrint.Size = new System.Drawing.Size(145, 22);
		this.miFiPrint.Text = "打印";
		this.miFiPrint.Click += new System.EventHandler(miFiPrint_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(142, 6);
		this.toolStripSeparator3.Visible = false;
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(145, 22);
		this.miFiExit.Text = "退出";
		this.miDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.miDisPreviousZoom, this.miDisNextZoom, this.miDisUnzoom, this.toolStripSeparator5, this.miDisProperties });
		this.miDisplay.Name = "miDisplay";
		this.miDisplay.Size = new System.Drawing.Size(44, 21);
		this.miDisplay.Text = "显示";
		this.miDisPreviousZoom.Name = "miDisPreviousZoom";
		this.miDisPreviousZoom.Size = new System.Drawing.Size(109, 22);
		this.miDisPreviousZoom.Text = "后退";
		this.miDisPreviousZoom.Click += new System.EventHandler(dpDisplay_DoubleClick);
		this.miDisNextZoom.Name = "miDisNextZoom";
		this.miDisNextZoom.Size = new System.Drawing.Size(109, 22);
		this.miDisNextZoom.Text = "前进";
		this.miDisNextZoom.Click += new System.EventHandler(miDisNextZoom_Click);
		this.miDisUnzoom.Name = "miDisUnzoom";
		this.miDisUnzoom.Size = new System.Drawing.Size(109, 22);
		this.miDisUnzoom.Text = "复位";
		this.miDisUnzoom.Click += new System.EventHandler(miDisUnzoom_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(106, 6);
		this.toolStripSeparator5.Visible = false;
		this.miDisProperties.Name = "miDisProperties";
		this.miDisProperties.Size = new System.Drawing.Size(109, 22);
		this.miDisProperties.Text = "属性...";
		this.miDisProperties.Visible = false;
		this.miDisProperties.Click += new System.EventHandler(miDisProperties_Click);
		this.miCalibration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.miCaliSetLevel, this.miCaliAddAll, this.miCaliAddExists, this.miCaliAddPeak, this.miCaliAddGroup, this.miCaliDeleteCmpd, this.miCaliDeleteAllCmpds, this.toolStripSeparator6, this.miCaliClearAllLevels, this.miCaliClearSelectedLevel,
			this.toolStripSeparator11, this.miCaliOptions
		});
		this.miCalibration.Name = "miCalibration";
		this.miCalibration.Size = new System.Drawing.Size(44, 21);
		this.miCalibration.Text = "校正";
		this.miCaliSetLevel.Name = "miCaliSetLevel";
		this.miCaliSetLevel.Size = new System.Drawing.Size(153, 22);
		this.miCaliSetLevel.Text = "选择Level";
		this.miCaliAddAll.Name = "miCaliAddAll";
		this.miCaliAddAll.Size = new System.Drawing.Size(153, 22);
		this.miCaliAddAll.Text = "添加所有峰";
		this.miCaliAddAll.Click += new System.EventHandler(miCaliAddAll_Click);
		this.miCaliAddExists.Name = "miCaliAddExists";
		this.miCaliAddExists.Size = new System.Drawing.Size(153, 22);
		this.miCaliAddExists.Text = "添加已有组分";
		this.miCaliAddExists.Click += new System.EventHandler(miCaliAddExists_Click);
		this.miCaliAddPeak.Name = "miCaliAddPeak";
		this.miCaliAddPeak.Size = new System.Drawing.Size(153, 22);
		this.miCaliAddPeak.Text = "添加峰";
		this.miCaliAddPeak.Click += new System.EventHandler(miCaliAddPeak_Click);
		this.miCaliAddGroup.Name = "miCaliAddGroup";
		this.miCaliAddGroup.Size = new System.Drawing.Size(153, 22);
		this.miCaliAddGroup.Text = "添加组";
		this.miCaliAddGroup.Visible = false;
		this.miCaliAddGroup.Click += new System.EventHandler(miCaliAddGroup_Click);
		this.miCaliDeleteCmpd.Name = "miCaliDeleteCmpd";
		this.miCaliDeleteCmpd.Size = new System.Drawing.Size(153, 22);
		this.miCaliDeleteCmpd.Text = "删除组分";
		this.miCaliDeleteCmpd.Click += new System.EventHandler(miCaliDeleteCmpd_Click);
		this.miCaliDeleteAllCmpds.Name = "miCaliDeleteAllCmpds";
		this.miCaliDeleteAllCmpds.Size = new System.Drawing.Size(153, 22);
		this.miCaliDeleteAllCmpds.Text = "删除全部组分";
		this.miCaliDeleteAllCmpds.Click += new System.EventHandler(miCaliDeleteAllCmpds_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(150, 6);
		this.miCaliClearAllLevels.Name = "miCaliClearAllLevels";
		this.miCaliClearAllLevels.Size = new System.Drawing.Size(153, 22);
		this.miCaliClearAllLevels.Text = "清除所有Level";
		this.miCaliClearAllLevels.Click += new System.EventHandler(miCaliClearAllLevels_Click);
		this.miCaliClearSelectedLevel.Name = "miCaliClearSelectedLevel";
		this.miCaliClearSelectedLevel.Size = new System.Drawing.Size(153, 22);
		this.miCaliClearSelectedLevel.Text = "清除选择Level";
		this.miCaliClearSelectedLevel.Click += new System.EventHandler(miCaliClearSelectedLevel_Click);
		this.toolStripSeparator11.Name = "toolStripSeparator11";
		this.toolStripSeparator11.Size = new System.Drawing.Size(150, 6);
		this.miCaliOptions.Name = "miCaliOptions";
		this.miCaliOptions.Size = new System.Drawing.Size(153, 22);
		this.miCaliOptions.Text = "选项...";
		this.miCaliOptions.Click += new System.EventHandler(miCaliOptions_Click);
		this.cmsCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miAddrow, this.miColumnsSetup, this.miRestoreDftColumns });
		this.cmsCali.Name = "cmsCali";
		this.cmsCali.Size = new System.Drawing.Size(161, 70);
		this.miAddrow.Name = "miAddrow";
		this.miAddrow.Size = new System.Drawing.Size(160, 22);
		this.miAddrow.Text = "添加行";
		this.miAddrow.Click += new System.EventHandler(miAddrow_Click);
		this.miColumnsSetup.Name = "miColumnsSetup";
		this.miColumnsSetup.Size = new System.Drawing.Size(160, 22);
		this.miColumnsSetup.Text = "列设置...";
		this.miColumnsSetup.Click += new System.EventHandler(miRestoreDftColumns_Click);
		this.miRestoreDftColumns.Name = "miRestoreDftColumns";
		this.miRestoreDftColumns.Size = new System.Drawing.Size(160, 22);
		this.miRestoreDftColumns.Text = "恢复默认列设置";
		this.miRestoreDftColumns.Click += new System.EventHandler(miRestoreDftColumns_Click);
		this.pnlFill.Controls.Add(this.ccCmpd);
		this.pnlFill.Controls.Add(this.pnlCmpds);
		this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlFill.Location = new System.Drawing.Point(0, 64);
		this.pnlFill.Name = "pnlFill";
		this.pnlFill.Size = new System.Drawing.Size(1038, 451);
		this.pnlFill.TabIndex = 10;
		this.ccCmpd.Compound = null;
		this.ccCmpd.GetCaliGnl = null;
		this.ccCmpd.GetCurrentChrom = null;
		this.ccCmpd.GetCurrentCompound = null;
		this.ccCmpd.Location = new System.Drawing.Point(464, 12);
		this.ccCmpd.Name = "ccCmpd";
		this.ccCmpd.Size = new System.Drawing.Size(874, 397);
		this.ccCmpd.TabIndex = 9;
		this.pnlCmpds.Controls.Add(this.gvCmpds);
		this.pnlCmpds.Controls.Add(this.splt);
		this.pnlCmpds.Controls.Add(this.dpDisplay);
		this.pnlCmpds.Location = new System.Drawing.Point(12, 12);
		this.pnlCmpds.Name = "pnlCmpds";
		this.pnlCmpds.Size = new System.Drawing.Size(412, 418);
		this.pnlCmpds.TabIndex = 8;
		this.gvCmpds.AllowUserToAddRows = false;
		this.gvCmpds.AllowUserToDeleteRows = false;
		this.gvCmpds.AllowUserToResizeRows = false;
		this.gvCmpds.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvCmpds.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.gvCmpds.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvCmpds.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.gvCmpds.ColumnHeadersHeight = 32;
		this.gvCmpds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvCmpds.ContextMenuStrip = this.cmsCali;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvCmpds.DefaultCellStyle = dataGridViewCellStyle2;
		this.gvCmpds.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gvCmpds.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvCmpds.Location = new System.Drawing.Point(0, 185);
		this.gvCmpds.Name = "gvCmpds";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvCmpds.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.gvCmpds.RowHeadersWidth = 25;
		this.gvCmpds.RowTemplate.Height = 16;
		this.gvCmpds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvCmpds.ShowCellToolTips = false;
		this.gvCmpds.Size = new System.Drawing.Size(412, 233);
		this.gvCmpds.TabIndex = 10;
		this.gvCmpds.OnChangeColor += new IBrainChrom2018.LclGridView.ChangeColor(method_7);
		this.gvCmpds.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvCmpds_CellBeginEdit);
		this.gvCmpds.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpds_CellEndEdit);
		this.gvCmpds.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpds_CellEnter);
		this.gvCmpds.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpds_CellValueChanged);
		this.gvCmpds.SelectionChanged += new System.EventHandler(gvCmpds_SelectionChanged);
		this.gvCmpds.DoubleClick += new System.EventHandler(gvCmpds_DoubleClick);
		this.gvCmpds.Enter += new System.EventHandler(gvCmpd_Enter);
		this.splt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splt.Dock = System.Windows.Forms.DockStyle.Top;
		this.splt.Location = new System.Drawing.Point(0, 180);
		this.splt.Name = "splt";
		this.splt.Size = new System.Drawing.Size(412, 5);
		this.splt.TabIndex = 7;
		this.splt.TabStop = false;
		this.dpDisplay.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpDisplay.Dock = System.Windows.Forms.DockStyle.Top;
		this.dpDisplay.Location = new System.Drawing.Point(0, 0);
		this.dpDisplay.Name = "dpDisplay";
		this.dpDisplay.Size = new System.Drawing.Size(412, 180);
		this.dpDisplay.TabIndex = 6;
		this.dpDisplay.Paint += new System.Windows.Forms.PaintEventHandler(dpDisplay_Paint);
		this.dpDisplay.DoubleClick += new System.EventHandler(dpDisplay_DoubleClick);
		this.dpDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(dpDisplay_MouseDown);
		this.dpDisplay.MouseLeave += new System.EventHandler(dpDisplay_MouseLeave);
		this.dpDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(dpDisplay_MouseMove);
		this.dpDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(dpDisplay_MouseUp);
		this.tcCmpds.Alignment = System.Windows.Forms.TabAlignment.Bottom;
		this.tcCmpds.Controls.Add(this.tpCL);
		this.tcCmpds.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.tcCmpds.ItemSize = new System.Drawing.Size(90, 19);
		this.tcCmpds.Location = new System.Drawing.Point(0, 515);
		this.tcCmpds.Name = "tcCmpds";
		this.tcCmpds.SelectedIndex = 0;
		this.tcCmpds.Size = new System.Drawing.Size(1038, 26);
		this.tcCmpds.TabIndex = 9;
		this.tcCmpds.SelectedIndexChanged += new System.EventHandler(tcCmpds_SelectedIndexChanged);
		this.tpCL.Location = new System.Drawing.Point(4, 4);
		this.tpCL.Name = "tpCL";
		this.tpCL.Size = new System.Drawing.Size(1030, 0);
		this.tpCL.TabIndex = 0;
		this.tpCL.Text = "组分列表";
		this.tpCL.UseVisualStyleBackColor = true;
		base.Controls.Add(this.pnlFill);
		base.Controls.Add(this.tcCmpds);
		base.Controls.Add(this.tsCali);
		base.Controls.Add(this.msCali);
		base.Controls.Add(this.ssCali);
		base.Name = "CaliGnlUserCtrl";
		base.Size = new System.Drawing.Size(1038, 563);
		base.Load += new System.EventHandler(CaliGnlForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(CaliGnlForm_KeyDown);
		this.ssCali.ResumeLayout(false);
		this.ssCali.PerformLayout();
		this.tsCali.ResumeLayout(false);
		this.tsCali.PerformLayout();
		this.msCali.ResumeLayout(false);
		this.msCali.PerformLayout();
		this.cmsCali.ResumeLayout(false);
		this.pnlFill.ResumeLayout(false);
		this.pnlCmpds.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvCmpds).EndInit();
		this.tcCmpds.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
