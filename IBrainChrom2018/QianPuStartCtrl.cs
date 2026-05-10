using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace IBrainChrom2018;

public class QianPuStartCtrl : UserControl
{
	public delegate void ButtonClick();

	private IContainer components = null;

	private BarManager barManager;

	private Bar bar1;

	private BarDockControl barDockControlTop;

	private BarDockControl barDockControlBottom;

	private BarDockControl barDockControlLeft;

	private BarDockControl barDockControlRight;

	private ImageList imageListLarge;

	private BarLargeButtonItem bbiStart;

	private BarLargeButtonItem bbiStop;

	private BarLargeButtonItem bbiLose;

	private ImageList imageListSmall;

	private RepositoryItemCheckEdit riceA;

	private RepositoryItemCheckEdit riceB;

	private RepositoryItemCheckEdit riceC;

	private BarEditItem beiD;

	private RepositoryItemCheckEdit riceD;

	private Bar bar2;

	private BarEditItem beiStopTime;

	private RepositoryItemSpinEdit riseStopTime;

	private BarEditItem beiSpendTime;

	private RepositoryItemSpinEdit riseSpendTime;

	private Bar bar3;

	private BarEditItem beiA;

	private RepositoryItemCheckEdit riceA2;

	private BarEditItem beiB;

	private RepositoryItemCheckEdit riceB2;

	private BarEditItem beiC;

	private BarEditItem barEditItem2;

	private ToolStrip toolStrip1;

	public ToolStripButton tsStart;

	private ToolStripSeparator toolStripSeparator1;

	public ToolStripButton tsstop;

	private ToolStripSeparator toolStripSeparator2;

	public ToolStripButton toolStripButton6;

	public MaskedTextBox maskedTextBox6;

	private Label label23;

	private Label label20;

	public MaskedTextBox maskedTextBox7;

	private Label label22;

	public Label label21;

	public int CurrentChannelIndex
	{
		get
		{
			if ((bool)beiB.EditValue)
			{
				return 1;
			}
			if ((bool)beiC.EditValue)
			{
				return 2;
			}
			if ((bool)beiD.EditValue)
			{
				return 3;
			}
			return 0;
		}
	}

	public int SelectedChannels
	{
		get
		{
			HWSendData hWSendData = HWSendData.Create();
			int channel = 0;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if ((bool)beiA.EditValue)
			{
				channel = hWSendData.IndexToChannel(0);
			}
			if ((bool)beiB.EditValue)
			{
				num = hWSendData.IndexToChannel(1);
			}
			if ((bool)beiC.EditValue)
			{
				num2 = hWSendData.IndexToChannel(2);
			}
			if ((bool)beiD.EditValue)
			{
				num3 = hWSendData.IndexToChannel(3);
			}
			return hWSendData.MergeChannels(channel, num, num2, num3);
		}
	}

	public int StopTime
	{
		get
		{
			int num = ((beiStopTime.EditValue == null) ? 600 : ((int)beiStopTime.EditValue));
			if (num > 9999)
			{
				num = 9999;
			}
			return num;
		}
	}

	public int SpendTime
	{
		get
		{
			return (beiSpendTime.EditValue != null) ? ((int)beiSpendTime.EditValue) : 0;
		}
		set
		{
			beiSpendTime.EditValue = value;
		}
	}

	public event ButtonClick OnStartButtonClick;

	public event ButtonClick OnStopButtonClick;

	public event ButtonClick OnLoseButtonClick;

	public QianPuStartCtrl()
	{
		InitializeComponent();
	}

	private void QianPuStartCtrl_Load(object sender, EventArgs e)
	{
		beiA.EditValue = true;
	}

	private void bbiStart_ItemClick(object sender, ItemClickEventArgs e)
	{
	}

	private void bbiStop_ItemClick(object sender, ItemClickEventArgs e)
	{
	}

	private void bbiLose_ItemClick(object sender, ItemClickEventArgs e)
	{
	}

	private void toolStripButton6_Click(object sender, EventArgs e)
	{
		if (this.OnLoseButtonClick != null)
		{
			this.OnLoseButtonClick();
		}
	}

	private void tsStart_Click(object sender, EventArgs e)
	{
		if (this.OnStartButtonClick != null)
		{
			this.OnStartButtonClick();
		}
	}

	private void tsstop_Click(object sender, EventArgs e)
	{
		if (this.OnStopButtonClick != null)
		{
			this.OnStopButtonClick();
		}
	}

	private void beiA_ItemClick(object sender, ItemClickEventArgs e)
	{
		beiB.EditValue = false;
		beiC.EditValue = false;
		beiD.EditValue = false;
	}

	private void beiB_ItemClick(object sender, ItemClickEventArgs e)
	{
		beiA.EditValue = false;
		beiC.EditValue = false;
		beiD.EditValue = false;
	}

	private void beiC_ItemClick(object sender, ItemClickEventArgs e)
	{
		beiA.EditValue = false;
		beiB.EditValue = false;
		beiD.EditValue = false;
	}

	private void beiD_ItemClick(object sender, ItemClickEventArgs e)
	{
		beiA.EditValue = false;
		beiB.EditValue = false;
		beiC.EditValue = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.QianPuStartCtrl));
		this.barManager = new DevExpress.XtraBars.BarManager(this.components);
		this.bar1 = new DevExpress.XtraBars.Bar();
		this.bbiStart = new DevExpress.XtraBars.BarLargeButtonItem();
		this.bbiStop = new DevExpress.XtraBars.BarLargeButtonItem();
		this.bbiLose = new DevExpress.XtraBars.BarLargeButtonItem();
		this.bar2 = new DevExpress.XtraBars.Bar();
		this.beiStopTime = new DevExpress.XtraBars.BarEditItem();
		this.riseStopTime = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		this.bar3 = new DevExpress.XtraBars.Bar();
		this.beiA = new DevExpress.XtraBars.BarEditItem();
		this.riceA = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.beiB = new DevExpress.XtraBars.BarEditItem();
		this.riceB = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.beiC = new DevExpress.XtraBars.BarEditItem();
		this.riceC = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.beiD = new DevExpress.XtraBars.BarEditItem();
		this.riceD = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
		this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
		this.beiSpendTime = new DevExpress.XtraBars.BarEditItem();
		this.riseSpendTime = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
		this.riceA2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.riceB2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.tsStart = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.tsstop = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.maskedTextBox6 = new System.Windows.Forms.MaskedTextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.maskedTextBox7 = new System.Windows.Forms.MaskedTextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.barManager).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riseStopTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceC).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceD).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riseSpendTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceA2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceB2).BeginInit();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[3] { this.bar1, this.bar2, this.bar3 });
		this.barManager.DockControls.Add(this.barDockControlTop);
		this.barManager.DockControls.Add(this.barDockControlBottom);
		this.barManager.DockControls.Add(this.barDockControlLeft);
		this.barManager.DockControls.Add(this.barDockControlRight);
		this.barManager.Form = this;
		this.barManager.Images = this.imageListSmall;
		this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[9] { this.bbiStart, this.bbiStop, this.bbiLose, this.beiD, this.beiStopTime, this.beiSpendTime, this.beiA, this.beiB, this.beiC });
		this.barManager.LargeImages = this.imageListLarge;
		this.barManager.MaxItemId = 20;
		this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[8] { this.riceA, this.riceB, this.riceC, this.riceD, this.riseStopTime, this.riseSpendTime, this.riceA2, this.riceB2 });
		this.bar1.BarName = "Tools";
		this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
		this.bar1.DockCol = 0;
		this.bar1.DockRow = 0;
		this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
		this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.bbiStart),
			new DevExpress.XtraBars.LinkPersistInfo(this.bbiStop, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.bbiLose)
		});
		this.bar1.OptionsBar.AllowQuickCustomization = false;
		this.bar1.OptionsBar.DrawDragBorder = false;
		this.bar1.OptionsBar.UseWholeRow = true;
		this.bar1.Text = "Tools";
		this.bbiStart.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
		this.bbiStart.Description = "启动";
		this.bbiStart.Id = 8;
		this.bbiStart.LargeImageIndex = 1;
		this.bbiStart.Name = "bbiStart";
		this.bbiStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiStart_ItemClick);
		this.bbiStop.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
		this.bbiStop.Description = "暂停";
		this.bbiStop.Id = 9;
		this.bbiStop.LargeImageIndex = 0;
		this.bbiStop.Name = "bbiStop";
		this.bbiStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiStop_ItemClick);
		this.bbiLose.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
		this.bbiLose.Description = "放弃";
		this.bbiLose.Id = 10;
		this.bbiLose.LargeImageIndex = 2;
		this.bbiLose.Name = "bbiLose";
		this.bbiLose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiLose_ItemClick);
		this.bar2.BarName = "ParamBar";
		this.bar2.DockCol = 0;
		this.bar2.DockRow = 1;
		this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
		this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
		{
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.beiStopTime, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)
		});
		this.bar2.OptionsBar.AllowQuickCustomization = false;
		this.bar2.OptionsBar.DrawDragBorder = false;
		this.bar2.OptionsBar.UseWholeRow = true;
		this.bar2.Text = "ParamBar";
		this.beiStopTime.Caption = "停止时间";
		this.beiStopTime.Edit = this.riseStopTime;
		this.beiStopTime.EditValue = 45;
		this.beiStopTime.Id = 15;
		this.beiStopTime.Name = "beiStopTime";
		this.riseStopTime.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.riseStopTime.Appearance.ForeColor = System.Drawing.Color.Red;
		this.riseStopTime.Appearance.Options.UseFont = true;
		this.riseStopTime.Appearance.Options.UseForeColor = true;
		this.riseStopTime.AutoHeight = false;
		this.riseStopTime.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.riseStopTime.MaxValue = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.riseStopTime.MinValue = new decimal(new int[4] { 1, 0, 0, 0 });
		this.riseStopTime.Name = "riseStopTime";
		this.bar3.BarName = "ParamBar2";
		this.bar3.DockCol = 0;
		this.bar3.DockRow = 2;
		this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
		this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.beiA, true),
			new DevExpress.XtraBars.LinkPersistInfo(this.beiB),
			new DevExpress.XtraBars.LinkPersistInfo(this.beiC),
			new DevExpress.XtraBars.LinkPersistInfo(this.beiD, true)
		});
		this.bar3.OptionsBar.DrawDragBorder = false;
		this.bar3.OptionsBar.UseWholeRow = true;
		this.bar3.Text = "ParamBar2";
		this.beiA.Caption = "A通道";
		this.beiA.Edit = this.riceA;
		this.beiA.EditValue = true;
		this.beiA.Id = 17;
		this.beiA.Name = "beiA";
		this.beiA.Width = 55;
		this.beiA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(beiA_ItemClick);
		this.riceA.AutoHeight = false;
		this.riceA.Caption = "A通道  ";
		this.riceA.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
		this.riceA.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
		this.riceA.Name = "riceA";
		this.riceA.RadioGroupIndex = 3;
		this.beiB.Caption = "B";
		this.beiB.Edit = this.riceB;
		this.beiB.EditValue = false;
		this.beiB.Id = 18;
		this.beiB.Name = "beiB";
		this.beiB.Width = 55;
		this.beiB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(beiB_ItemClick);
		this.riceB.AutoHeight = false;
		this.riceB.Caption = "B通道  ";
		this.riceB.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
		this.riceB.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
		this.riceB.Name = "riceB";
		this.riceB.RadioGroupIndex = 3;
		this.beiC.Caption = "C";
		this.beiC.Edit = this.riceC;
		this.beiC.EditValue = false;
		this.beiC.Id = 19;
		this.beiC.Name = "beiC";
		this.beiC.Width = 55;
		this.beiC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(beiC_ItemClick);
		this.riceC.AutoHeight = false;
		this.riceC.Caption = "C通道";
		this.riceC.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
		this.riceC.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
		this.riceC.Name = "riceC";
		this.riceC.RadioGroupIndex = 3;
		this.beiD.Edit = this.riceD;
		this.beiD.EditValue = false;
		this.beiD.Id = 14;
		this.beiD.Name = "beiD";
		this.beiD.Width = 55;
		this.beiD.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(beiD_ItemClick);
		this.riceD.AutoHeight = false;
		this.riceD.Caption = "D通道";
		this.riceD.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
		this.riceD.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
		this.riceD.Name = "riceD";
		this.riceD.RadioGroupIndex = 3;
		this.barDockControlTop.CausesValidation = false;
		this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
		this.barDockControlTop.Size = new System.Drawing.Size(338, 107);
		this.barDockControlBottom.CausesValidation = false;
		this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.barDockControlBottom.Location = new System.Drawing.Point(0, 264);
		this.barDockControlBottom.Size = new System.Drawing.Size(338, 0);
		this.barDockControlLeft.CausesValidation = false;
		this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.barDockControlLeft.Location = new System.Drawing.Point(0, 107);
		this.barDockControlLeft.Size = new System.Drawing.Size(0, 157);
		this.barDockControlRight.CausesValidation = false;
		this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
		this.barDockControlRight.Location = new System.Drawing.Point(338, 107);
		this.barDockControlRight.Size = new System.Drawing.Size(0, 157);
		this.imageListSmall.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListSmall.ImageStream");
		this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
		this.imageListSmall.Images.SetKeyName(0, "暂停.png");
		this.imageListSmall.Images.SetKeyName(1, "启动.png");
		this.imageListSmall.Images.SetKeyName(2, "放弃.png");
		this.beiSpendTime.Caption = "时间";
		this.beiSpendTime.Edit = this.riseSpendTime;
		this.beiSpendTime.Id = 16;
		this.beiSpendTime.Name = "beiSpendTime";
		this.riseSpendTime.AutoHeight = false;
		this.riseSpendTime.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.riseSpendTime.Name = "riseSpendTime";
		this.imageListLarge.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListLarge.ImageStream");
		this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
		this.imageListLarge.Images.SetKeyName(0, "暂停.png");
		this.imageListLarge.Images.SetKeyName(1, "启动.png");
		this.imageListLarge.Images.SetKeyName(2, "放弃.png");
		this.riceA2.AutoHeight = false;
		this.riceA2.Caption = "A通道";
		this.riceA2.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
		this.riceA2.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;
		this.riceA2.Name = "riceA2";
		this.riceA2.RadioGroupIndex = 1;
		this.riceB2.AutoHeight = false;
		this.riceB2.Caption = "B通道  ";
		this.riceB2.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
		this.riceB2.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;
		this.riceB2.Name = "riceB2";
		this.riceB2.RadioGroupIndex = 1;
		this.barEditItem2.Edit = this.riceD;
		this.barEditItem2.Id = 14;
		this.barEditItem2.Name = "barEditItem2";
		this.barEditItem2.Width = 55;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.tsStart, this.toolStripSeparator1, this.tsstop, this.toolStripButton6, this.toolStripSeparator2 });
		this.toolStrip1.Location = new System.Drawing.Point(7, 3);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(132, 39);
		this.toolStrip1.TabIndex = 4;
		this.toolStrip1.Text = "toolStrip1";
		this.tsStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tsStart.Image = (System.Drawing.Image)resources.GetObject("tsStart.Image");
		this.tsStart.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsStart.Name = "tsStart";
		this.tsStart.Size = new System.Drawing.Size(36, 36);
		this.tsStart.Text = "toolStripButton1";
		this.tsStart.ToolTipText = "开始采集";
		this.tsStart.Click += new System.EventHandler(tsStart_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
		this.tsstop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tsstop.Image = (System.Drawing.Image)resources.GetObject("tsstop.Image");
		this.tsstop.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsstop.Name = "tsstop";
		this.tsstop.Size = new System.Drawing.Size(36, 36);
		this.tsstop.Text = "toolStripButton3";
		this.tsstop.ToolTipText = "停止采集";
		this.tsstop.Click += new System.EventHandler(tsstop_Click);
		this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton6.Image = (System.Drawing.Image)resources.GetObject("toolStripButton6.Image");
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton6.Text = "toolStripButton6";
		this.toolStripButton6.ToolTipText = "放弃采集";
		this.toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
		this.maskedTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox6.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox6.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox6.Location = new System.Drawing.Point(155, 54);
		this.maskedTextBox6.Name = "maskedTextBox6";
		this.maskedTextBox6.ReadOnly = true;
		this.maskedTextBox6.Size = new System.Drawing.Size(51, 21);
		this.maskedTextBox6.TabIndex = 11;
		this.maskedTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("SimSun", 7.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label23.ForeColor = System.Drawing.Color.Black;
		this.label23.Location = new System.Drawing.Point(223, 60);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(25, 10);
		this.label23.TabIndex = 9;
		this.label23.Text = "时间";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("SimSun", 7.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label20.ForeColor = System.Drawing.Color.Black;
		this.label20.Location = new System.Drawing.Point(111, 58);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(25, 10);
		this.label20.TabIndex = 10;
		this.label20.Text = "信号";
		this.maskedTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox7.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox7.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox7.Location = new System.Drawing.Point(266, 54);
		this.maskedTextBox7.Name = "maskedTextBox7";
		this.maskedTextBox7.ReadOnly = true;
		this.maskedTextBox7.Size = new System.Drawing.Size(52, 21);
		this.maskedTextBox7.TabIndex = 12;
		this.maskedTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(318, 58);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(23, 12);
		this.label22.TabIndex = 13;
		this.label22.Text = "min";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(204, 59);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(17, 12);
		this.label21.TabIndex = 14;
		this.label21.Text = "mV";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.maskedTextBox7);
		base.Controls.Add(this.maskedTextBox6);
		base.Controls.Add(this.label23);
		base.Controls.Add(this.label20);
		base.Controls.Add(this.label22);
		base.Controls.Add(this.label21);
		base.Controls.Add(this.toolStrip1);
		base.Controls.Add(this.barDockControlLeft);
		base.Controls.Add(this.barDockControlRight);
		base.Controls.Add(this.barDockControlBottom);
		base.Controls.Add(this.barDockControlTop);
		base.Name = "QianPuStartCtrl";
		base.Size = new System.Drawing.Size(338, 264);
		base.Load += new System.EventHandler(QianPuStartCtrl_Load);
		((System.ComponentModel.ISupportInitialize)this.barManager).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riseStopTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceC).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceD).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riseSpendTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceA2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceB2).EndInit();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
