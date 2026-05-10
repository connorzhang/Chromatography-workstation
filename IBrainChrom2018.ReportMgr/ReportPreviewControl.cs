using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;

namespace IBrainChrom2018.ReportMgr;

public class ReportPreviewControl : UserControl
{
	private PrintControl pcContainer;

	private ToolBarButton toolBarButton1;

	private ToolBarButton toolBarButton2;

	private ToolBarButton toolBarButton3;

	private ToolBarButton toolBarButton4;

	private ToolBarButton toolBarButton5;

	private ToolBarButton toolBarButton6;

	private MenuItem miWord;

	private MenuItem miExcel;

	private MenuItem miHtml;

	private MenuItem miTxt;

	private ContextMenu ctmExport;

	private PrintBarManager printBarManager1;

	private PreviewBar previewBar1;

	private PrintPreviewBarItem printPreviewBarItem2;

	private PrintPreviewBarItem printPreviewBarItem3;

	private PrintPreviewBarItem printPreviewBarItem4;

	private PrintPreviewBarItem printPreviewBarItem5;

	private PrintPreviewBarItem printPreviewBarItem6;

	private PrintPreviewBarItem printPreviewBarItem7;

	private PrintPreviewBarItem printPreviewBarItem8;

	private PrintPreviewBarItem printPreviewBarItem9;

	private PrintPreviewBarItem printPreviewBarItem10;

	private PrintPreviewBarItem printPreviewBarItem11;

	private PrintPreviewBarItem printPreviewBarItem12;

	private PrintPreviewBarItem printPreviewBarItem13;

	private PrintPreviewBarItem printPreviewBarItem14;

	private PrintPreviewBarItem printPreviewBarItem15;

	private ZoomBarEditItem zoomBarEditItem1;

	private PrintPreviewRepositoryItemComboBox printPreviewRepositoryItemComboBox1;

	private PrintPreviewBarItem printPreviewBarItem16;

	private PrintPreviewBarItem printPreviewBarItem17;

	private PrintPreviewBarItem printPreviewBarItem18;

	private PrintPreviewBarItem printPreviewBarItem19;

	private PrintPreviewBarItem printPreviewBarItem20;

	private PrintPreviewBarItem printPreviewBarItem21;

	private PrintPreviewBarItem printPreviewBarItem22;

	private PrintPreviewBarItem printPreviewBarItem23;

	private PrintPreviewBarItem printPreviewBarItem24;

	private PrintPreviewBarItem printPreviewBarItem25;

	private PrintPreviewBarItem printPreviewBarItem26;

	private PreviewBar previewBar2;

	private PrintPreviewStaticItem printPreviewStaticItem1;

	private BarStaticItem barStaticItem1;

	private ProgressBarEditItem progressBarEditItem1;

	private RepositoryItemProgressBar repositoryItemProgressBar1;

	private PrintPreviewBarItem printPreviewBarItem1;

	private BarButtonItem barButtonItem1;

	private PrintPreviewStaticItem printPreviewStaticItem2;

	private ZoomTrackBarEditItem zoomTrackBarEditItem1;

	private RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;

	private PreviewBar previewBar3;

	private PrintPreviewSubItem printPreviewSubItem1;

	private PrintPreviewSubItem printPreviewSubItem2;

	private PrintPreviewSubItem printPreviewSubItem4;

	private PrintPreviewBarItem printPreviewBarItem27;

	private PrintPreviewBarItem printPreviewBarItem28;

	private BarToolbarsListItem barToolbarsListItem1;

	private PrintPreviewSubItem printPreviewSubItem3;

	private BarDockControl barDockControlTop;

	private BarDockControl barDockControlBottom;

	private BarDockControl barDockControlLeft;

	private BarDockControl barDockControlRight;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem1;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem2;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem3;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem4;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem5;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem6;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem7;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem8;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem9;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem10;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem11;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem12;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem13;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem14;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem15;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem16;

	private PrintPreviewBarCheckItem printPreviewBarCheckItem17;

	private IContainer components;

	private XtraReport m_Report = null;

	public ReportPreviewControl()
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ReportMgr.ReportPreviewControl));
		this.pcContainer = new DevExpress.XtraPrinting.Control.PrintControl();
		this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
		this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
		this.ctmExport = new System.Windows.Forms.ContextMenu();
		this.miWord = new System.Windows.Forms.MenuItem();
		this.miExcel = new System.Windows.Forms.MenuItem();
		this.miHtml = new System.Windows.Forms.MenuItem();
		this.miTxt = new System.Windows.Forms.MenuItem();
		this.printBarManager1 = new DevExpress.XtraPrinting.Preview.PrintBarManager(this.components);
		this.previewBar1 = new DevExpress.XtraPrinting.Preview.PreviewBar();
		this.printPreviewBarItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem8 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem9 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem12 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem14 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.zoomBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomBarEditItem();
		this.printPreviewRepositoryItemComboBox1 = new DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox();
		this.printPreviewBarItem16 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem17 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem18 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem19 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem20 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem21 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem22 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem23 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem24 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem25 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem26 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.previewBar2 = new DevExpress.XtraPrinting.Preview.PreviewBar();
		this.printPreviewStaticItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
		this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
		this.progressBarEditItem1 = new DevExpress.XtraPrinting.Preview.ProgressBarEditItem();
		this.printPreviewBarItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
		this.printPreviewStaticItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
		this.zoomTrackBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem();
		this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
		this.previewBar3 = new DevExpress.XtraPrinting.Preview.PreviewBar();
		this.printPreviewSubItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
		this.printPreviewSubItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
		this.printPreviewSubItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
		this.printPreviewBarItem27 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.printPreviewBarItem28 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
		this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
		this.printPreviewSubItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
		this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
		this.printPreviewBarCheckItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem8 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem9 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem12 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem14 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem16 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		this.printPreviewBarCheckItem17 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
		((System.ComponentModel.ISupportInitialize)this.printBarManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.printPreviewRepositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemZoomTrackBar1).BeginInit();
		base.SuspendLayout();
		this.pcContainer.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.pcContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pcContainer.IsMetric = true;
		this.pcContainer.Location = new System.Drawing.Point(0, 55);
		this.pcContainer.Name = "pcContainer";
		this.pcContainer.Size = new System.Drawing.Size(981, 546);
		this.pcContainer.Status = "当前没有任何页面";
		this.pcContainer.TabIndex = 5;
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
		this.ctmExport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[4] { this.miWord, this.miExcel, this.miHtml, this.miTxt });
		this.miWord.Index = 0;
		this.miWord.Text = "Word文档";
		this.miWord.Click += new System.EventHandler(miWord_Click);
		this.miExcel.Index = 1;
		this.miExcel.Text = "Excel文档";
		this.miExcel.Click += new System.EventHandler(miExcel_Click);
		this.miHtml.Index = 2;
		this.miHtml.Text = "网页文档";
		this.miHtml.Click += new System.EventHandler(miHtml_Click);
		this.miTxt.Index = 3;
		this.miTxt.Text = "文本文档";
		this.miTxt.Click += new System.EventHandler(miTxt_Click);
		this.printBarManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[3] { this.previewBar1, this.previewBar2, this.previewBar3 });
		this.printBarManager1.DockControls.Add(this.barDockControlTop);
		this.printBarManager1.DockControls.Add(this.barDockControlBottom);
		this.printBarManager1.DockControls.Add(this.barDockControlLeft);
		this.printBarManager1.DockControls.Add(this.barDockControlRight);
		this.printBarManager1.Form = this;
		this.printBarManager1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("printBarManager1.ImageStream");
		this.printBarManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[57]
		{
			this.printPreviewStaticItem1, this.barStaticItem1, this.progressBarEditItem1, this.printPreviewBarItem1, this.barButtonItem1, this.printPreviewStaticItem2, this.zoomTrackBarEditItem1, this.printPreviewBarItem2, this.printPreviewBarItem3, this.printPreviewBarItem4,
			this.printPreviewBarItem5, this.printPreviewBarItem6, this.printPreviewBarItem7, this.printPreviewBarItem8, this.printPreviewBarItem9, this.printPreviewBarItem10, this.printPreviewBarItem11, this.printPreviewBarItem12, this.printPreviewBarItem13, this.printPreviewBarItem14,
			this.printPreviewBarItem15, this.zoomBarEditItem1, this.printPreviewBarItem16, this.printPreviewBarItem17, this.printPreviewBarItem18, this.printPreviewBarItem19, this.printPreviewBarItem20, this.printPreviewBarItem21, this.printPreviewBarItem22, this.printPreviewBarItem23,
			this.printPreviewBarItem24, this.printPreviewBarItem25, this.printPreviewBarItem26, this.printPreviewSubItem1, this.printPreviewSubItem2, this.printPreviewSubItem3, this.printPreviewSubItem4, this.printPreviewBarItem27, this.printPreviewBarItem28, this.barToolbarsListItem1,
			this.printPreviewBarCheckItem1, this.printPreviewBarCheckItem2, this.printPreviewBarCheckItem3, this.printPreviewBarCheckItem4, this.printPreviewBarCheckItem5, this.printPreviewBarCheckItem6, this.printPreviewBarCheckItem7, this.printPreviewBarCheckItem8, this.printPreviewBarCheckItem9, this.printPreviewBarCheckItem10,
			this.printPreviewBarCheckItem11, this.printPreviewBarCheckItem12, this.printPreviewBarCheckItem13, this.printPreviewBarCheckItem14, this.printPreviewBarCheckItem15, this.printPreviewBarCheckItem16, this.printPreviewBarCheckItem17
		});
		this.printBarManager1.MainMenu = this.previewBar3;
		this.printBarManager1.MaxItemId = 57;
		this.printBarManager1.PreviewBar = this.previewBar1;
		this.printBarManager1.PrintControl = this.pcContainer;
		this.printBarManager1.StatusBar = this.previewBar2;
		this.printBarManager1.TransparentEditors = true;
		this.previewBar1.BarName = "工具栏";
		this.previewBar1.DockCol = 0;
		this.previewBar1.DockRow = 1;
		this.previewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
		this.previewBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[26]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem2),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem3),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem4),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem5, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem6, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem7),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem8, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem9),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem10),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem11),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem12),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem13, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem14),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem15, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.zoomBarEditItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem16),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem17, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem18),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem19),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem20),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem21, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem22),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem23),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem24, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem25),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem26, true)
		});
		this.previewBar1.Text = "工具栏";
		this.printPreviewBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
		this.printPreviewBarItem2.Caption = "页面布局";
		this.printPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap;
		this.printPreviewBarItem2.Enabled = false;
		this.printPreviewBarItem2.Hint = "Document Map";
		this.printPreviewBarItem2.Id = 7;
		this.printPreviewBarItem2.ImageIndex = 19;
		this.printPreviewBarItem2.Name = "printPreviewBarItem2";
		this.printPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
		this.printPreviewBarItem3.Caption = "参数设置";
		this.printPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Parameters;
		this.printPreviewBarItem3.Enabled = false;
		this.printPreviewBarItem3.Hint = "参数设置";
		this.printPreviewBarItem3.Id = 8;
		this.printPreviewBarItem3.ImageIndex = 22;
		this.printPreviewBarItem3.Name = "printPreviewBarItem3";
		this.printPreviewBarItem4.Caption = "搜索";
		this.printPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find;
		this.printPreviewBarItem4.Enabled = false;
		this.printPreviewBarItem4.Hint = "搜索";
		this.printPreviewBarItem4.Id = 9;
		this.printPreviewBarItem4.ImageIndex = 20;
		this.printPreviewBarItem4.Name = "printPreviewBarItem4";
		this.printPreviewBarItem5.Caption = "自定义";
		this.printPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize;
		this.printPreviewBarItem5.Enabled = false;
		this.printPreviewBarItem5.Hint = "自定义";
		this.printPreviewBarItem5.Id = 10;
		this.printPreviewBarItem5.ImageIndex = 14;
		this.printPreviewBarItem5.Name = "printPreviewBarItem5";
		this.printPreviewBarItem6.Caption = "打开文档";
		this.printPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open;
		this.printPreviewBarItem6.Enabled = false;
		this.printPreviewBarItem6.Hint = "打开文档";
		this.printPreviewBarItem6.Id = 11;
		this.printPreviewBarItem6.ImageIndex = 23;
		this.printPreviewBarItem6.Name = "printPreviewBarItem6";
		this.printPreviewBarItem7.Caption = "保存文档";
		this.printPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save;
		this.printPreviewBarItem7.Enabled = false;
		this.printPreviewBarItem7.Hint = "保存文档";
		this.printPreviewBarItem7.Id = 12;
		this.printPreviewBarItem7.ImageIndex = 24;
		this.printPreviewBarItem7.Name = "printPreviewBarItem7";
		this.printPreviewBarItem8.Caption = "&P打印";
		this.printPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print;
		this.printPreviewBarItem8.Enabled = false;
		this.printPreviewBarItem8.Hint = "打印";
		this.printPreviewBarItem8.Id = 13;
		this.printPreviewBarItem8.ImageIndex = 0;
		this.printPreviewBarItem8.Name = "printPreviewBarItem8";
		this.printPreviewBarItem9.Caption = "&R直接打印";
		this.printPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect;
		this.printPreviewBarItem9.Enabled = false;
		this.printPreviewBarItem9.Hint = "快速打印";
		this.printPreviewBarItem9.Id = 14;
		this.printPreviewBarItem9.ImageIndex = 1;
		this.printPreviewBarItem9.Name = "printPreviewBarItem9";
		this.printPreviewBarItem10.Caption = "&U页面设置";
		this.printPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup;
		this.printPreviewBarItem10.Enabled = false;
		this.printPreviewBarItem10.Hint = "页面设置";
		this.printPreviewBarItem10.Id = 15;
		this.printPreviewBarItem10.ImageIndex = 2;
		this.printPreviewBarItem10.Name = "printPreviewBarItem10";
		this.printPreviewBarItem10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(printPreviewBarItem10_ItemClick);
		this.printPreviewBarItem11.Caption = "页眉和页脚";
		this.printPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF;
		this.printPreviewBarItem11.Enabled = false;
		this.printPreviewBarItem11.Hint = "页眉和页脚";
		this.printPreviewBarItem11.Id = 16;
		this.printPreviewBarItem11.ImageIndex = 15;
		this.printPreviewBarItem11.Name = "printPreviewBarItem11";
		this.printPreviewBarItem12.ActAsDropDown = true;
		this.printPreviewBarItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
		this.printPreviewBarItem12.Caption = "比例";
		this.printPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale;
		this.printPreviewBarItem12.Enabled = false;
		this.printPreviewBarItem12.Hint = "比例";
		this.printPreviewBarItem12.Id = 17;
		this.printPreviewBarItem12.ImageIndex = 25;
		this.printPreviewBarItem12.Name = "printPreviewBarItem12";
		this.printPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
		this.printPreviewBarItem13.Caption = "手动拖动";
		this.printPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool;
		this.printPreviewBarItem13.Enabled = false;
		this.printPreviewBarItem13.Hint = "手动拖动";
		this.printPreviewBarItem13.Id = 18;
		this.printPreviewBarItem13.ImageIndex = 16;
		this.printPreviewBarItem13.Name = "printPreviewBarItem13";
		this.printPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
		this.printPreviewBarItem14.Caption = "放大镜";
		this.printPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier;
		this.printPreviewBarItem14.Enabled = false;
		this.printPreviewBarItem14.Hint = "放大镜";
		this.printPreviewBarItem14.Id = 19;
		this.printPreviewBarItem14.ImageIndex = 3;
		this.printPreviewBarItem14.Name = "printPreviewBarItem14";
		this.printPreviewBarItem15.Caption = "缩小";
		this.printPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut;
		this.printPreviewBarItem15.Enabled = false;
		this.printPreviewBarItem15.Hint = "缩小";
		this.printPreviewBarItem15.Id = 20;
		this.printPreviewBarItem15.ImageIndex = 5;
		this.printPreviewBarItem15.Name = "printPreviewBarItem15";
		this.zoomBarEditItem1.Caption = "缩放";
		this.zoomBarEditItem1.Edit = this.printPreviewRepositoryItemComboBox1;
		this.zoomBarEditItem1.EditValue = "100%";
		this.zoomBarEditItem1.Enabled = false;
		this.zoomBarEditItem1.Hint = "缩放";
		this.zoomBarEditItem1.Id = 21;
		this.zoomBarEditItem1.Name = "zoomBarEditItem1";
		this.zoomBarEditItem1.Width = 70;
		this.printPreviewRepositoryItemComboBox1.AutoComplete = false;
		this.printPreviewRepositoryItemComboBox1.DropDownRows = 11;
		this.printPreviewRepositoryItemComboBox1.Name = "printPreviewRepositoryItemComboBox1";
		this.printPreviewBarItem16.Caption = "放大";
		this.printPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn;
		this.printPreviewBarItem16.Enabled = false;
		this.printPreviewBarItem16.Hint = "放大";
		this.printPreviewBarItem16.Id = 22;
		this.printPreviewBarItem16.ImageIndex = 4;
		this.printPreviewBarItem16.Name = "printPreviewBarItem16";
		this.printPreviewBarItem17.Caption = "第一页";
		this.printPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage;
		this.printPreviewBarItem17.Enabled = false;
		this.printPreviewBarItem17.Hint = "第一页";
		this.printPreviewBarItem17.Id = 23;
		this.printPreviewBarItem17.ImageIndex = 7;
		this.printPreviewBarItem17.Name = "printPreviewBarItem17";
		this.printPreviewBarItem18.Caption = "前一页";
		this.printPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage;
		this.printPreviewBarItem18.Enabled = false;
		this.printPreviewBarItem18.Hint = "前一页";
		this.printPreviewBarItem18.Id = 24;
		this.printPreviewBarItem18.ImageIndex = 8;
		this.printPreviewBarItem18.Name = "printPreviewBarItem18";
		this.printPreviewBarItem19.Caption = "下一页";
		this.printPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage;
		this.printPreviewBarItem19.Enabled = false;
		this.printPreviewBarItem19.Hint = "下一页";
		this.printPreviewBarItem19.Id = 25;
		this.printPreviewBarItem19.ImageIndex = 9;
		this.printPreviewBarItem19.Name = "printPreviewBarItem19";
		this.printPreviewBarItem20.Caption = "最后一页";
		this.printPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage;
		this.printPreviewBarItem20.Enabled = false;
		this.printPreviewBarItem20.Hint = "最后一页";
		this.printPreviewBarItem20.Id = 26;
		this.printPreviewBarItem20.ImageIndex = 10;
		this.printPreviewBarItem20.Name = "printPreviewBarItem20";
		this.printPreviewBarItem21.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
		this.printPreviewBarItem21.Caption = "多页";
		this.printPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages;
		this.printPreviewBarItem21.Enabled = false;
		this.printPreviewBarItem21.Hint = "多页";
		this.printPreviewBarItem21.Id = 27;
		this.printPreviewBarItem21.ImageIndex = 11;
		this.printPreviewBarItem21.Name = "printPreviewBarItem21";
		this.printPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
		this.printPreviewBarItem22.Caption = "&C颜色";
		this.printPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground;
		this.printPreviewBarItem22.Enabled = false;
		this.printPreviewBarItem22.Hint = "背景颜色";
		this.printPreviewBarItem22.Id = 28;
		this.printPreviewBarItem22.ImageIndex = 12;
		this.printPreviewBarItem22.Name = "printPreviewBarItem22";
		this.printPreviewBarItem23.Caption = "&W水印标记";
		this.printPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark;
		this.printPreviewBarItem23.Enabled = false;
		this.printPreviewBarItem23.Hint = "水印标记";
		this.printPreviewBarItem23.Id = 29;
		this.printPreviewBarItem23.ImageIndex = 21;
		this.printPreviewBarItem23.Name = "printPreviewBarItem23";
		this.printPreviewBarItem24.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
		this.printPreviewBarItem24.Caption = "导出文档";
		this.printPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile;
		this.printPreviewBarItem24.Enabled = false;
		this.printPreviewBarItem24.Hint = "导出文档";
		this.printPreviewBarItem24.Id = 30;
		this.printPreviewBarItem24.ImageIndex = 18;
		this.printPreviewBarItem24.Name = "printPreviewBarItem24";
		this.printPreviewBarItem25.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
		this.printPreviewBarItem25.Caption = "E-Mail 文档";
		this.printPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile;
		this.printPreviewBarItem25.Enabled = false;
		this.printPreviewBarItem25.Hint = "E-Mail 文档";
		this.printPreviewBarItem25.Id = 31;
		this.printPreviewBarItem25.ImageIndex = 17;
		this.printPreviewBarItem25.Name = "printPreviewBarItem25";
		this.printPreviewBarItem26.Caption = "&X退出";
		this.printPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview;
		this.printPreviewBarItem26.Enabled = false;
		this.printPreviewBarItem26.Hint = "退出报表";
		this.printPreviewBarItem26.Id = 32;
		this.printPreviewBarItem26.ImageIndex = 13;
		this.printPreviewBarItem26.Name = "printPreviewBarItem26";
		this.previewBar2.BarName = "状态栏";
		this.previewBar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
		this.previewBar2.DockCol = 0;
		this.previewBar2.DockRow = 0;
		this.previewBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
		this.previewBar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[7]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewStaticItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.progressBarEditItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewStaticItem2, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.zoomTrackBarEditItem1)
		});
		this.previewBar2.OptionsBar.AllowQuickCustomization = false;
		this.previewBar2.OptionsBar.DrawDragBorder = false;
		this.previewBar2.OptionsBar.UseWholeRow = true;
		this.previewBar2.Text = "状态栏";
		this.printPreviewStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
		this.printPreviewStaticItem1.Caption = "Nothing";
		this.printPreviewStaticItem1.Id = 0;
		this.printPreviewStaticItem1.LeftIndent = 1;
		this.printPreviewStaticItem1.Name = "printPreviewStaticItem1";
		this.printPreviewStaticItem1.RightIndent = 1;
		this.printPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
		this.printPreviewStaticItem1.Type = "PageOfPages";
		this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
		this.barStaticItem1.Id = 1;
		this.barStaticItem1.Name = "barStaticItem1";
		this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
		this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
		this.progressBarEditItem1.Edit = null;
		this.progressBarEditItem1.EditHeight = 12;
		this.progressBarEditItem1.Id = 2;
		this.progressBarEditItem1.Name = "progressBarEditItem1";
		this.progressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.progressBarEditItem1.Width = 150;
		this.printPreviewBarItem1.Caption = "停止";
		this.printPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding;
		this.printPreviewBarItem1.Enabled = false;
		this.printPreviewBarItem1.Hint = "停止";
		this.printPreviewBarItem1.Id = 3;
		this.printPreviewBarItem1.Name = "printPreviewBarItem1";
		this.printPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
		this.barButtonItem1.Enabled = false;
		this.barButtonItem1.Id = 4;
		this.barButtonItem1.Name = "barButtonItem1";
		this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
		this.printPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.printPreviewStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
		this.printPreviewStaticItem2.Caption = "100%";
		this.printPreviewStaticItem2.Id = 5;
		this.printPreviewStaticItem2.Name = "printPreviewStaticItem2";
		this.printPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far;
		this.printPreviewStaticItem2.Type = "ZoomFactor";
		this.printPreviewStaticItem2.Width = 40;
		this.zoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.zoomTrackBarEditItem1.Edit = this.repositoryItemZoomTrackBar1;
		this.zoomTrackBarEditItem1.EditValue = 90;
		this.zoomTrackBarEditItem1.Enabled = false;
		this.zoomTrackBarEditItem1.Id = 6;
		this.zoomTrackBarEditItem1.Name = "zoomTrackBarEditItem1";
		this.zoomTrackBarEditItem1.Range = new int[2] { 10, 500 };
		this.zoomTrackBarEditItem1.Width = 140;
		this.repositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center;
		this.repositoryItemZoomTrackBar1.AllowFocused = false;
		this.repositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
		this.repositoryItemZoomTrackBar1.Maximum = 180;
		this.repositoryItemZoomTrackBar1.Middle = 5;
		this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
		this.repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
		this.previewBar3.BarName = "菜单栏";
		this.previewBar3.DockCol = 0;
		this.previewBar3.DockRow = 0;
		this.previewBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
		this.previewBar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem1),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem2),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem3)
		});
		this.previewBar3.OptionsBar.MultiLine = true;
		this.previewBar3.OptionsBar.UseWholeRow = true;
		this.previewBar3.Text = "菜单栏";
		this.previewBar3.Visible = false;
		this.printPreviewSubItem1.Caption = "&F文件";
		this.printPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File;
		this.printPreviewSubItem1.Id = 33;
		this.printPreviewSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[5]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem10),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem8),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem9),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem24, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem26, true)
		});
		this.printPreviewSubItem1.Name = "printPreviewSubItem1";
		this.printPreviewSubItem2.Caption = "&V视图";
		this.printPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View;
		this.printPreviewSubItem2.Id = 34;
		this.printPreviewSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem4, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.barToolbarsListItem1, true)
		});
		this.printPreviewSubItem2.Name = "printPreviewSubItem2";
		this.printPreviewSubItem4.Caption = "&P页面布局";
		this.printPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout;
		this.printPreviewSubItem4.Id = 36;
		this.printPreviewSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem27),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem28)
		});
		this.printPreviewSubItem4.Name = "printPreviewSubItem4";
		this.printPreviewBarItem27.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
		this.printPreviewBarItem27.Caption = "&F单页";
		this.printPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing;
		this.printPreviewBarItem27.Enabled = false;
		this.printPreviewBarItem27.GroupIndex = 100;
		this.printPreviewBarItem27.Id = 37;
		this.printPreviewBarItem27.Name = "printPreviewBarItem27";
		this.printPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
		this.printPreviewBarItem28.Caption = "&C连续";
		this.printPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous;
		this.printPreviewBarItem28.Down = true;
		this.printPreviewBarItem28.Enabled = false;
		this.printPreviewBarItem28.GroupIndex = 100;
		this.printPreviewBarItem28.Id = 38;
		this.printPreviewBarItem28.Name = "printPreviewBarItem28";
		this.barToolbarsListItem1.Caption = "工具栏";
		this.barToolbarsListItem1.Id = 39;
		this.barToolbarsListItem1.Name = "barToolbarsListItem1";
		this.printPreviewSubItem3.Caption = "&B背景设置";
		this.printPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background;
		this.printPreviewSubItem3.Id = 35;
		this.printPreviewSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem22),
			new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem23)
		});
		this.printPreviewSubItem3.Name = "printPreviewSubItem3";
		this.barDockControlTop.CausesValidation = false;
		this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
		this.barDockControlTop.Size = new System.Drawing.Size(981, 55);
		this.barDockControlBottom.CausesValidation = false;
		this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.barDockControlBottom.Location = new System.Drawing.Point(0, 601);
		this.barDockControlBottom.Size = new System.Drawing.Size(981, 27);
		this.barDockControlLeft.CausesValidation = false;
		this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.barDockControlLeft.Location = new System.Drawing.Point(0, 55);
		this.barDockControlLeft.Size = new System.Drawing.Size(0, 546);
		this.barDockControlRight.CausesValidation = false;
		this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
		this.barDockControlRight.Location = new System.Drawing.Point(981, 55);
		this.barDockControlRight.Size = new System.Drawing.Size(0, 546);
		this.printPreviewBarCheckItem1.Caption = "PDF File";
		this.printPreviewBarCheckItem1.Checked = true;
		this.printPreviewBarCheckItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
		this.printPreviewBarCheckItem1.Enabled = false;
		this.printPreviewBarCheckItem1.GroupIndex = 2;
		this.printPreviewBarCheckItem1.Hint = "PDF File";
		this.printPreviewBarCheckItem1.Id = 40;
		this.printPreviewBarCheckItem1.Name = "printPreviewBarCheckItem1";
		this.printPreviewBarCheckItem2.Caption = "HTML File";
		this.printPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm;
		this.printPreviewBarCheckItem2.Enabled = false;
		this.printPreviewBarCheckItem2.GroupIndex = 2;
		this.printPreviewBarCheckItem2.Hint = "HTML File";
		this.printPreviewBarCheckItem2.Id = 41;
		this.printPreviewBarCheckItem2.Name = "printPreviewBarCheckItem2";
		this.printPreviewBarCheckItem3.Caption = "MHT File";
		this.printPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht;
		this.printPreviewBarCheckItem3.Enabled = false;
		this.printPreviewBarCheckItem3.GroupIndex = 2;
		this.printPreviewBarCheckItem3.Hint = "MHT File";
		this.printPreviewBarCheckItem3.Id = 42;
		this.printPreviewBarCheckItem3.Name = "printPreviewBarCheckItem3";
		this.printPreviewBarCheckItem4.Caption = "RTF File";
		this.printPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf;
		this.printPreviewBarCheckItem4.Enabled = false;
		this.printPreviewBarCheckItem4.GroupIndex = 2;
		this.printPreviewBarCheckItem4.Hint = "RTF File";
		this.printPreviewBarCheckItem4.Id = 43;
		this.printPreviewBarCheckItem4.Name = "printPreviewBarCheckItem4";
		this.printPreviewBarCheckItem5.Caption = "XLS File";
		this.printPreviewBarCheckItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls;
		this.printPreviewBarCheckItem5.Enabled = false;
		this.printPreviewBarCheckItem5.GroupIndex = 2;
		this.printPreviewBarCheckItem5.Hint = "XLS File";
		this.printPreviewBarCheckItem5.Id = 44;
		this.printPreviewBarCheckItem5.Name = "printPreviewBarCheckItem5";
		this.printPreviewBarCheckItem6.Caption = "XLSX File";
		this.printPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx;
		this.printPreviewBarCheckItem6.Enabled = false;
		this.printPreviewBarCheckItem6.GroupIndex = 2;
		this.printPreviewBarCheckItem6.Hint = "XLSX File";
		this.printPreviewBarCheckItem6.Id = 45;
		this.printPreviewBarCheckItem6.Name = "printPreviewBarCheckItem6";
		this.printPreviewBarCheckItem7.Caption = "CSV File";
		this.printPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv;
		this.printPreviewBarCheckItem7.Enabled = false;
		this.printPreviewBarCheckItem7.GroupIndex = 2;
		this.printPreviewBarCheckItem7.Hint = "CSV File";
		this.printPreviewBarCheckItem7.Id = 46;
		this.printPreviewBarCheckItem7.Name = "printPreviewBarCheckItem7";
		this.printPreviewBarCheckItem8.Caption = "Text File";
		this.printPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt;
		this.printPreviewBarCheckItem8.Enabled = false;
		this.printPreviewBarCheckItem8.GroupIndex = 2;
		this.printPreviewBarCheckItem8.Hint = "Text File";
		this.printPreviewBarCheckItem8.Id = 47;
		this.printPreviewBarCheckItem8.Name = "printPreviewBarCheckItem8";
		this.printPreviewBarCheckItem9.Caption = "Image File";
		this.printPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic;
		this.printPreviewBarCheckItem9.Enabled = false;
		this.printPreviewBarCheckItem9.GroupIndex = 2;
		this.printPreviewBarCheckItem9.Hint = "Image File";
		this.printPreviewBarCheckItem9.Id = 48;
		this.printPreviewBarCheckItem9.Name = "printPreviewBarCheckItem9";
		this.printPreviewBarCheckItem9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(printPreviewBarCheckItem9_ItemClick);
		this.printPreviewBarCheckItem10.Caption = "PDF File";
		this.printPreviewBarCheckItem10.Checked = true;
		this.printPreviewBarCheckItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf;
		this.printPreviewBarCheckItem10.Enabled = false;
		this.printPreviewBarCheckItem10.GroupIndex = 1;
		this.printPreviewBarCheckItem10.Hint = "PDF File";
		this.printPreviewBarCheckItem10.Id = 49;
		this.printPreviewBarCheckItem10.Name = "printPreviewBarCheckItem10";
		this.printPreviewBarCheckItem11.Caption = "MHT File";
		this.printPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht;
		this.printPreviewBarCheckItem11.Enabled = false;
		this.printPreviewBarCheckItem11.GroupIndex = 1;
		this.printPreviewBarCheckItem11.Hint = "MHT File";
		this.printPreviewBarCheckItem11.Id = 50;
		this.printPreviewBarCheckItem11.Name = "printPreviewBarCheckItem11";
		this.printPreviewBarCheckItem12.Caption = "RTF File";
		this.printPreviewBarCheckItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf;
		this.printPreviewBarCheckItem12.Enabled = false;
		this.printPreviewBarCheckItem12.GroupIndex = 1;
		this.printPreviewBarCheckItem12.Hint = "RTF File";
		this.printPreviewBarCheckItem12.Id = 51;
		this.printPreviewBarCheckItem12.Name = "printPreviewBarCheckItem12";
		this.printPreviewBarCheckItem13.Caption = "XLS File";
		this.printPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls;
		this.printPreviewBarCheckItem13.Enabled = false;
		this.printPreviewBarCheckItem13.GroupIndex = 1;
		this.printPreviewBarCheckItem13.Hint = "XLS File";
		this.printPreviewBarCheckItem13.Id = 52;
		this.printPreviewBarCheckItem13.Name = "printPreviewBarCheckItem13";
		this.printPreviewBarCheckItem14.Caption = "XLSX File";
		this.printPreviewBarCheckItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx;
		this.printPreviewBarCheckItem14.Enabled = false;
		this.printPreviewBarCheckItem14.GroupIndex = 1;
		this.printPreviewBarCheckItem14.Hint = "XLSX File";
		this.printPreviewBarCheckItem14.Id = 53;
		this.printPreviewBarCheckItem14.Name = "printPreviewBarCheckItem14";
		this.printPreviewBarCheckItem15.Caption = "CSV File";
		this.printPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv;
		this.printPreviewBarCheckItem15.Enabled = false;
		this.printPreviewBarCheckItem15.GroupIndex = 1;
		this.printPreviewBarCheckItem15.Hint = "CSV File";
		this.printPreviewBarCheckItem15.Id = 54;
		this.printPreviewBarCheckItem15.Name = "printPreviewBarCheckItem15";
		this.printPreviewBarCheckItem16.Caption = "Text File";
		this.printPreviewBarCheckItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt;
		this.printPreviewBarCheckItem16.Enabled = false;
		this.printPreviewBarCheckItem16.GroupIndex = 1;
		this.printPreviewBarCheckItem16.Hint = "Text File";
		this.printPreviewBarCheckItem16.Id = 55;
		this.printPreviewBarCheckItem16.Name = "printPreviewBarCheckItem16";
		this.printPreviewBarCheckItem17.Caption = "Image File";
		this.printPreviewBarCheckItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic;
		this.printPreviewBarCheckItem17.Enabled = false;
		this.printPreviewBarCheckItem17.GroupIndex = 1;
		this.printPreviewBarCheckItem17.Hint = "Image File";
		this.printPreviewBarCheckItem17.Id = 56;
		this.printPreviewBarCheckItem17.Name = "printPreviewBarCheckItem17";
		base.Controls.Add(this.pcContainer);
		base.Controls.Add(this.barDockControlLeft);
		base.Controls.Add(this.barDockControlRight);
		base.Controls.Add(this.barDockControlBottom);
		base.Controls.Add(this.barDockControlTop);
		base.Name = "ReportPreviewControl";
		base.Size = new System.Drawing.Size(981, 628);
		((System.ComponentModel.ISupportInitialize)this.printBarManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.printPreviewRepositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemZoomTrackBar1).EndInit();
		base.ResumeLayout(false);
	}

	public void SetReport(XtraReport report)
	{
		m_Report = report;
		pcContainer.PrintingSystem = report.PrintingSystem;
		pcContainer.SelectFirstPage();
	}

	private string GetReportPath(XtraReport report, string ext)
	{
		Assembly entryAssembly = Assembly.GetEntryAssembly();
		string text = $"{entryAssembly.GetName().Name}.{report.GetType().Name}";
		string directoryName = Path.GetDirectoryName(entryAssembly.Location);
		return Path.Combine(directoryName, text + "." + ext);
	}

	private void SaveDocument(string exname)
	{
		string text = CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString();
		string text2 = "";
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "*.*|." + exname;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			text2 = saveFileDialog.FileName;
		}
	}

	private void miWord_Click(object sender, EventArgs e)
	{
		SaveDocument("rtf");
	}

	private void miExcel_Click(object sender, EventArgs e)
	{
		SaveDocument("xls");
	}

	private void miHtml_Click(object sender, EventArgs e)
	{
		SaveDocument("html");
	}

	private void miTxt_Click(object sender, EventArgs e)
	{
		SaveDocument("txt");
	}

	private void printPreviewBarCheckItem9_ItemClick(object sender, ItemClickEventArgs e)
	{
	}

	private void printPreviewBarItem10_ItemClick(object sender, ItemClickEventArgs e)
	{
	}
}
