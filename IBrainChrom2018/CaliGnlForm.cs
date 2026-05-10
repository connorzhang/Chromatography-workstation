using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CaliGnlForm : LclGnlForm
{
	public static CaliGnlForm caliGnlForm = null;

	public static bool m_bFromMtd = false;

	private CaliGnlUserCtrl.myUpdateMtdForm updateMtdForm = null;

	private ContextMenuStrip cmsCali;

	private ToolStripMenuItem miColumnsSetup;

	private ToolStripMenuItem miRestoreDftColumns;

	private ToolStripMenuItem miAddrow;

	private IContainer components;

	private Panel pnlFill;

	public CaliGnlUserCtrl caliGnlUserCtrl;

	public Chromatogram CurChromatogram => caliGnlUserCtrl.Chromatogram;

	public CaliGnl CurCaliGnl
	{
		get
		{
			if (CurChromatogram == null)
			{
				return null;
			}
			return CurChromatogram.caliGnl;
		}
	}

	private static void ShowCaliGnlForm()
	{
		if (caliGnlForm == null)
		{
			caliGnlForm = new CaliGnlForm();
		}
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
	}

	private static void ShowCaliGnlTabCtrl()
	{
		if (CaliGnlUserCtrl.caliGnlUserCtrl != null)
		{
			TabPage tabPage = (TabPage)CaliGnlUserCtrl.caliGnlUserCtrl.Parent;
			TabControl tabControl = (TabControl)tabPage.Parent;
			if (tabControl != null)
			{
				tabControl.SelectedIndex = 3;
			}
		}
	}

	public static void ShowCaliGnlParaent()
	{
		if (CaliGnlUserCtrl.caliGnlUserCtrl == null || CaliGnlUserCtrl.caliGnlUserCtrl.ParentForm.GetType() == typeof(CaliGnlForm))
		{
			ShowCaliGnlForm();
		}
		else
		{
			ShowCaliGnlTabCtrl();
		}
	}

	public void LoadFile(CaliGnl cali)
	{
		if (CaliGnlUserCtrl.caliGnlUserCtrl != null)
		{
			m_bFromMtd = true;
			CaliGnlUserCtrl.caliGnlUserCtrl.LoadFile(cali);
			CaliGnlUserCtrl.caliGnlUserCtrl.updateMtdForm = updateMtdForm;
		}
	}

	public void LoadFile(string strCali)
	{
		if (CaliGnlUserCtrl.caliGnlUserCtrl != null)
		{
			m_bFromMtd = true;
			CaliGnlUserCtrl.caliGnlUserCtrl.LoadFile(strCali);
			CaliGnlUserCtrl.caliGnlUserCtrl.updateMtdForm = updateMtdForm;
		}
	}

	public static void LoadCalFileShowForm(string cclCalibration)
	{
		ShowCaliGnlParaent();
		if (File.Exists(cclCalibration) && CaliGnlUserCtrl.caliGnlUserCtrl != null)
		{
			m_bFromMtd = false;
			CaliGnlUserCtrl.caliGnlUserCtrl.LoadFile(cclCalibration);
		}
	}

	public static void LoadCalFileShowForm(CaliGnl cali, CaliGnlUserCtrl.myUpdateMtdForm updateMtdForm)
	{
		ShowCaliGnlParaent();
		if (CaliGnlUserCtrl.caliGnlUserCtrl != null)
		{
			m_bFromMtd = true;
			CaliGnlUserCtrl.caliGnlUserCtrl.LoadFile(cali);
			CaliGnlUserCtrl.caliGnlUserCtrl.updateMtdForm = updateMtdForm;
		}
	}

	public CaliGnlForm()
	{
		InitializeComponent();
		CaliGnlUserCtrl.caliGnlUserCtrl = caliGnlUserCtrl;
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	private void CaliGnlForm_Load(object sender, EventArgs e)
	{
		InstruWinsInfo instruWinsInfo = new InstruWinsInfo();
		if (instruWinsInfo.valid)
		{
			ReadWinInfo(instruWinsInfo.winInfos[5]);
		}
	}

	private void CaliGnlForm_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void CaliGnlForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		CaliGnlUserCtrl.caliGnlUserCtrl = null;
		caliGnlForm = null;
	}

	public void DpRefresh()
	{
	}

	public void LoadOptions()
	{
	}

	private void miDisProperties_Click(object sender, EventArgs e)
	{
		Class49.optionsDialog_0.ShowDialog(instrument, WinStyle.CaliGnl, instrument.user.options);
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
	}

	public override void ReadWinInfo(WinInfo winInfo)
	{
		base.ReadWinInfo(winInfo);
	}

	public override void refresh_once()
	{
		base.refresh_once();
	}

	public override void WriteWinInfo(WinInfo winInfo)
	{
	}

	public object gvCmpdsValue(bool gvUse, Compound cmpd, string columnName)
	{
		return CaliGnlUserCtrl.caliGnlUserCtrl.gvCmpdsValue(gvUse, cmpd, columnName);
	}

	public object gvCmpdValue(bool gvUse, Level level, string columnName)
	{
		return CaliGnlUserCtrl.caliGnlUserCtrl.gvCmpdValue(gvUse, level, columnName);
	}

	public void CloseAllChroms()
	{
		CaliGnlUserCtrl.caliGnlUserCtrl.CloseAllChroms();
	}

	public void AutoAddLevel()
	{
		CaliGnlUserCtrl.caliGnlUserCtrl.AutoAddLevel();
	}

	public void OpenChrom(string fileName)
	{
		CaliGnlUserCtrl.caliGnlUserCtrl.OpenChrom(fileName);
	}

	public void GetCmpdDisColumns(ref GvInfos gvInfos)
	{
		CaliGnlUserCtrl.caliGnlUserCtrl.GetCmpdDisColumns(ref gvInfos);
	}

	public void GetCmpdsDisColumns(ref GvInfos gvInfos)
	{
		CaliGnlUserCtrl.caliGnlUserCtrl.GetCmpdsDisColumns(ref gvInfos);
	}

	public void miFiNewCali_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.CaliGnlForm));
		this.cmsCali = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miAddrow = new System.Windows.Forms.ToolStripMenuItem();
		this.miColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.pnlFill = new System.Windows.Forms.Panel();
		this.caliGnlUserCtrl = new IBrainChrom2018.CaliGnlUserCtrl();
		this.cmsCali.SuspendLayout();
		this.pnlFill.SuspendLayout();
		base.SuspendLayout();
		this.cmsCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miAddrow, this.miColumnsSetup, this.miRestoreDftColumns });
		this.cmsCali.Name = "cmsCali";
		this.cmsCali.Size = new System.Drawing.Size(161, 70);
		this.miAddrow.Name = "miAddrow";
		this.miAddrow.Size = new System.Drawing.Size(160, 22);
		this.miAddrow.Text = "添加行";
		this.miColumnsSetup.Name = "miColumnsSetup";
		this.miColumnsSetup.Size = new System.Drawing.Size(160, 22);
		this.miColumnsSetup.Text = "列设置...";
		this.miRestoreDftColumns.Name = "miRestoreDftColumns";
		this.miRestoreDftColumns.Size = new System.Drawing.Size(160, 22);
		this.miRestoreDftColumns.Text = "恢复默认列设置";
		this.pnlFill.Controls.Add(this.caliGnlUserCtrl);
		this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlFill.Location = new System.Drawing.Point(0, 0);
		this.pnlFill.Name = "pnlFill";
		this.pnlFill.Size = new System.Drawing.Size(1038, 563);
		this.pnlFill.TabIndex = 10;
		this.caliGnlUserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.caliGnlUserCtrl.Location = new System.Drawing.Point(0, 0);
		this.caliGnlUserCtrl.Name = "caliGnlUserCtrl";
		this.caliGnlUserCtrl.ShowManuAndStateBar = true;
		this.caliGnlUserCtrl.Size = new System.Drawing.Size(1038, 563);
		this.caliGnlUserCtrl.TabIndex = 0;
		base.ClientSize = new System.Drawing.Size(1038, 563);
		base.Controls.Add(this.pnlFill);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "CaliGnlForm";
		this.Text = "定量组份编辑";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CaliGnlForm_FormClosing);
		base.Load += new System.EventHandler(CaliGnlForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(CaliGnlForm_KeyDown);
		this.cmsCali.ResumeLayout(false);
		this.pnlFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
