using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;

namespace IBrainChrom2018.ReportMgr;

public class ReportPreview : Form
{
	private PrintControl pcContainer;

	private ToolBarButton toolBarButton1;

	private ToolBarButton toolBarButton2;

	private ToolBarButton toolBarButton3;

	private ToolBarButton toolBarButton4;

	private ToolBarButton toolBarButton5;

	private ToolBarButton toolBarButton6;

	private Container components = null;

	public ReportPreview()
	{
		InitializeComponent();
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
		this.pcContainer = new DevExpress.XtraPrinting.Control.PrintControl();
		this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
		base.SuspendLayout();
		this.pcContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pcContainer.IsMetric = true;
		this.pcContainer.Location = new System.Drawing.Point(0, 0);
		this.pcContainer.Name = "pcContainer";
		this.pcContainer.Size = new System.Drawing.Size(560, 493);
		this.pcContainer.TabIndex = 2;
		this.pcContainer.TooltipFont = new System.Drawing.Font("Tahoma", 9f);
		this.toolBarButton1.Name = "toolBarButton1";
		this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
		this.toolBarButton2.Name = "toolBarButton2";
		this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
		this.toolBarButton3.Name = "toolBarButton3";
		this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
		this.toolBarButton4.Name = "toolBarButton4";
		this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
		this.toolBarButton5.Name = "toolBarButton5";
		this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
		this.toolBarButton6.Name = "toolBarButton6";
		this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		base.ClientSize = new System.Drawing.Size(560, 493);
		base.Controls.Add(this.pcContainer);
		base.HelpButton = true;
		base.Name = "ReportPreview";
		base.Tag = "ReportPreview";
		this.Text = "打印预览";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}

	private void mitPrint_Click(object sender, EventArgs e)
	{
	}

	private void mitExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void SetReport(XtraReport report)
	{
		pcContainer.PrintingSystem = report.PrintingSystem;
	}
}
