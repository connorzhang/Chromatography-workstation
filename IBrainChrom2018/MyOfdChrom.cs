using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class MyOfdChrom : OpenFileDialogEx
{
	private LclDisplayPanel dpgnlChrom;

	private IContainer icontainer = null;

	private Options options_0 = new Options();

	private string strLastSelectChromFileName = "";

	private ChromDisplay chromDisplay_0;

	private DisLg disLg_0 = default(DisLg);

	public bool Checked => false;

	public Chromatogram CurChromatogram { get; internal set; }

	public MyOfdChrom()
	{
		try
		{
			InitializeComponent();
			base.StartLocation = AddonWindowLocation.Bottom;
			base.AllowFolderSelect = false;
			chromDisplay_0 = new ChromDisplay(WinStyle.Chromatogram, dpgnlChrom);
			chromDisplay_0.showMouseLgValue = false;
			chromDisplay_0.showProgTemp = false;
			chromDisplay_0.showPeakArea = true;
			chromDisplay_0.ExtDraw_begin();
			chromDisplay_0.LinkOptions(options_0);
			chromDisplay_0.setShowGrid = options_0.grpShowGrid;
		}
		catch (Exception)
		{
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

	private void MyOfdChrom_Load(object sender, EventArgs e)
	{
	}

	private void InitializeComponent()
	{
		this.dpgnlChrom = new IBrainChrom2018.LclDisplayPanel();
		base.SuspendLayout();
		this.dpgnlChrom.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgnlChrom.Cursor = System.Windows.Forms.Cursors.Default;
		this.dpgnlChrom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dpgnlChrom.Location = new System.Drawing.Point(0, 0);
		this.dpgnlChrom.Name = "dpgnlChrom";
		this.dpgnlChrom.Size = new System.Drawing.Size(629, 254);
		this.dpgnlChrom.TabIndex = 10;
		this.dpgnlChrom.Paint += new System.Windows.Forms.PaintEventHandler(dpgnlChrom_Paint);
		base.Controls.Add(this.dpgnlChrom);
		base.Name = "MyOfdChrom";
		base.Size = new System.Drawing.Size(629, 254);
		base.Title = "打开谱图";
		base.ResumeLayout(false);
	}

	private void method_3(int int_16, Signal signal_0)
	{
	}

	private void method_4(Signal signal_0)
	{
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
				chromatogram.signal.disColor = options_0.sgColors[0];
				chromDisplay_0.stDisChain.Clear();
				chromDisplay_0.SetFullDisLg(ref disLg_0, chromatogram.signal, second: true);
				float lgX = chromatogram.disLg.lgX;
				int num = (int)lgX;
				if (!float.IsNaN(lgX) && !float.IsInfinity(lgX) && (float)num > 0f)
				{
					chromDisplay_0.stDisChain.AppendFrameLg(chromatogram.disLg);
				}
				Chromatogram[] chroms = new Chromatogram[1] { chromatogram };
				CurChromatogram = chromatogram;
				int setChromNo = 0;
				chromDisplay_0.LinkDisChroms(chroms, ref setChromNo);
			}
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("MyOfdChrom 147 ," + ex.Message);
		}
	}

	public override void OnFileSelecting(string fileName)
	{
		try
		{
			base.OnFileSelecting(fileName);
			LoadChromFile(fileName);
			dpgnlChrom.Refresh();
			base.FileName = fileName;
			base.FileNames = new string[1] { fileName };
		}
		catch (Exception)
		{
		}
	}

	public override void OnItemSelected(List<string> selectedItems)
	{
		try
		{
			base.OnItemSelected(selectedItems);
		}
		catch (Exception)
		{
		}
	}

	public override void OnPathOpened(string path)
	{
		try
		{
			base.OnPathOpened(path);
		}
		catch (Exception)
		{
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
}
