using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018.ChromFile;

public class MisMgrAssist
{
	private SystemParam sysParam;

	private static MisMgrAssist myself = null;

	private MisMgrAssist()
	{
	}

	public static MisMgrAssist Create()
	{
		if (myself == null)
		{
			myself = new MisMgrAssist();
		}
		return myself;
	}

	public void SetForm()
	{
		if (UIProxy.Instance.MainForm.tsmiFileMain != null)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem();
			toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5 });
			toolStripMenuItem.Name = "misMgrFileMenuItem";
			toolStripMenuItem.Size = new Size(104, 26);
			toolStripMenuItem.Text = Lang.PS("仪器参数设定");
			toolStripMenuItem2.Name = "openMisFileMenuItem";
			toolStripMenuItem2.Size = new Size(104, 26);
			toolStripMenuItem2.Text = Lang.PS("打开仪器参数文件");
			toolStripMenuItem2.Click += openMisFileMenuItem_Click;
			toolStripMenuItem3.Name = "saveAsMisFileMenuItem";
			toolStripMenuItem3.Size = new Size(104, 26);
			toolStripMenuItem3.Text = Lang.PS("另存为仪器参数文件");
			toolStripMenuItem3.Click += saveAsMisFileMenuItem_Click;
			toolStripMenuItem4.Name = "saveMisFileMenuItem";
			toolStripMenuItem4.Size = new Size(104, 26);
			toolStripMenuItem4.Text = Lang.PS("保存cfg参数文件");
			toolStripMenuItem4.Click += saveSunFileMenuItem_Click;
			toolStripMenuItem5.Name = "saveMisFileMenuItem";
			toolStripMenuItem5.Size = new Size(104, 26);
			toolStripMenuItem5.Text = Lang.PS("清除cfg参数文件");
			toolStripMenuItem5.Click += clearSunFileMenuItem_Click;
		}
	}

	private void openMisFileMenuItem_Click(object sender, EventArgs e)
	{
		MisMgr baseFile = new MisMgr();
		baseFile = (MisMgr)IBaseFileMgr.OpenFile(baseFile);
		if (baseFile != null)
		{
			baseFile.m_strExt = "mis";
			SetFormFromMisData(baseFile);
			if (IBaseFileMgr.m_strFilePath != "")
			{
				sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
				sysParam.SaveParam();
			}
		}
	}

	private void saveAsMisFileMenuItem_Click(object sender, EventArgs e)
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

	private void saveSunFileMenuItem_Click(object sender, EventArgs e)
	{
		ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
		chromDeviceListMgr.SaveWorkSunFile();
	}

	private void clearSunFileMenuItem_Click(object sender, EventArgs e)
	{
		ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
		chromDeviceListMgr.Clear();
		chromDeviceListMgr.SaveWorkSunFile();
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

	public void SetFormFromMisData(MisMgr misMgr)
	{
		ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
		if (chromDeviceListMgr.CurrentChromDevice != null)
		{
			chromDeviceListMgr.CurrentChromDevice.misMgr = misMgr;
			chromDeviceListMgr.formMain.ReloadMisMgr2();
		}
	}
}
