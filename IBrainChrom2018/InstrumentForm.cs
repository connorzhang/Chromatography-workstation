using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class InstrumentForm : LclGnlForm
{
	private enum Enum3
	{
		const_0,
		const_1,
		const_2,
		const_3
	}

	private enum Enum4
	{
		const_0,
		const_1
	}

	private delegate void Delegate0(int int_0, IntPtr intptr_0, IntPtr intptr_1);

	private Delegate delegate_0;

	private IAsyncResult iasyncResult_0;

	public static Brush brTxtFrmItem = new SolidBrush(color_0);

	public static Brush brTxtFrmValue = new SolidBrush(color_1);

	public CaliGnlForm caliGnlForm;

	public CaliGpcForm caliGpcForm;

	public ChromForm chromForm;

	private static Color color_0 = Color.AliceBlue;

	private static Color color_1 = Color.Beige;

	public DataAcqForm dataAcqForm;

	public DevMonitorForm devMonitorForm;

	public MtdSetupDlg dlgMethodSetup;

	public RptSetupDlg dlgReportSetup;

	private static PjtDirDlg pjtDirDlg_0 = new PjtDirDlg();

	private static GraphicsPath graphicsPath_0 = CreateRoundedRectanglePath(rectangle_3, 4, 65, ref graphicsPath_1, ref graphicsPath_2);

	private static GraphicsPath graphicsPath_1;

	private static GraphicsPath graphicsPath_2;

	private static GraphicsPath graphicsPath_3 = CreateRoundedRectanglePath(rectangle_4, 4, 80, ref graphicsPath_5, ref graphicsPath_4);

	private static GraphicsPath graphicsPath_4;

	private static GraphicsPath graphicsPath_5;

	public EventHandler mubtnMainFormHandler;

	private OpenFileDialog openFileDialog_0;

	private OpenFileDialog openFileDialog_1;

	private object[] object_0 = new object[3];

	private static Pen pen_0 = Pens.Gray;

	private static Pen pen_1 = Pens.LightBlue;

	private static Color color_2 = Color.DimGray;

	private static Color color_3 = Color.Gray;

	public Color pipe_color;

	private static int int_9;

	private static Point point_0 = new Point(point_1.X, point_2.Y);

	private static Point point_1 = new Point(309, point_2.Y - 42);

	private static Point point_2 = new Point(156, point_4.Y);

	private static Point point_3 = new Point(207, point_4.Y);

	private static Point point_4 = new Point(105, rectangle_5.Bottom - 37);

	private static Point point_5 = new Point(point_1.X, point_2.Y + 42);

	private static Point point_6 = new Point(60, 10);

	private static Point point_7 = new Point(10, 10);

	private static Rectangle rectangle_0;

	private static Rectangle rectangle_1;

	private static Rectangle rectangle_2;

	private static Rectangle rectangle_3 = new Rectangle(108, 10, 237, 80);

	private static Rectangle rectangle_4 = new Rectangle(rectangle_5.Left, rectangle_5.Bottom + 10, 226, 18);

	private static Rectangle rectangle_5 = new Rectangle(17, 105, 74, 74);

	private Rectangle rectangle_6;

	private static Rectangle rectangle_7;

	public RptSetup rptSetup = new RptSetup();

	public SeqAlyForm seqAlyForm;

	private SaveFileDialog saveFileDialog_0;

	public SSAlyForm ssAlyForm;

	private StatusStrip ssInstru;

	private string string_52 = "";

	private Timer timer_0;

	private LclInstruButton btnCaliWindow;

	private LclInstruButton btnChromWindow;

	private LclInstruButton btnDataAcquisition;

	private LclInstruButton btnDeviceMonitor;

	private LclInstruButton btnDM2;

	private LclInstruButton btnDM3;

	private LclInstruButton btnDM4;

	private LclInstruButton btnIntegration;

	private LclInstruButton btnLampZero;

	private LclInstruButton btnMtdHard;

	private ToolStripButton btnNewMethod;

	private ToolStripButton btnOpenMethod;

	private ToolStripButton btnOptions;

	private ToolStripButton btnPjtDir;

	private LclInstruButton btnReportSetup;

	private ToolStripButton btnSaveMethod;

	private LclInstruButton btnSequence;

	private LclInstruButton btnSingle;

	private IContainer icontainer_2;

	private LclDisplayPanel dpInstru;

	private LclLabel lbFileName;

	private LclLabel lbFileNameV;

	private LclLabel lbMethod;

	private LclLabel lbMethodV;

	private LclLabel lbMode;

	private LclLabel lbModeV;

	private LclLabel lbSample;

	private LclLabel lbSampleID;

	private LclLabel lbSampleIDV;

	private LclLabel lbSampleV;

	private LclLabel lbStateV;

	private LclLabel lbTimeV;

	private ToolStripMenuItem miFiAutoOverW;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiNewMethod;

	private ToolStripMenuItem miFiOpenMethod;

	private ToolStripMenuItem miFiPjtDir;

	private ToolStripMenuItem miFiRecoverData;

	private ToolStripMenuItem miFiReportSetup;

	private ToolStripMenuItem miFiSaveMethod;

	private ToolStripMenuItem miFiSaveMethodAs;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_1 = new ToolStripMenuItem();

	private MenuStrip msInstru;

	private LclPictureBox pbLamp;

	private LclPictureBox pbPump0;

	private LclPictureBox pbPump1;

	public ToolStripStatusLabel slbExplain;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStrip tsInstru;

	private Panel panel3;

	private Panel panel2;

	private Panel panel1;

	private ToolStripStatusLabel tssInstruStyle;

	private IContainer components;

	public int InstruPageNo => instrument.pageNo;

	public bool OverWrite => miFiAutoOverW.Checked;

	public InstrumentForm()
	{
		InitializeComponent();
		dpInstru.Dock = DockStyle.Fill;
		btnLampZero.Height = pbPump0.Height + 1;
		instrument = null;
		btnLampZero.Text = Lang.PS("自动归零", "Auto Zero");
		LoadLanguage();
	}

	public InstrumentForm(Instrument instrument)
	{
		InitializeComponent();
		dpInstru.Dock = DockStyle.Fill;
		btnLampZero.Height = pbPump0.Height + 1;
		base.instrument = instrument;
		btnLampZero.Text = Lang.PS("自动归零", "Auto Zero");
		LoadLanguage();
	}

	public TcpServerSocket GetCurrentTcpSocket()
	{
		return ChromDeviceListMgr.Create().CurrentTcpServerSocket;
	}

	public Instrument GetInstrument()
	{
		return instrument;
	}

	public void btnCaliWindow_Click(object sender, EventArgs e)
	{
		LclForm lclForm = caliGnlForm;
		if (instrument.instruStyle == InstruStyle.GPC)
		{
			lclForm = caliGpcForm;
		}
		if (lclForm.Visible)
		{
			if (lclForm.WindowState == FormWindowState.Minimized)
			{
				lclForm.WindowState = FormWindowState.Normal;
			}
			lclForm.BringToFront();
		}
		else
		{
			lclForm.Show();
		}
	}

	public void btnChromWindow_Click(object sender, EventArgs e)
	{
		if (chromForm.Visible)
		{
			if (chromForm.WindowState == FormWindowState.Minimized)
			{
				chromForm.WindowState = FormWindowState.Normal;
			}
			chromForm.BringToFront();
		}
		else
		{
			chromForm.Show();
		}
	}

	public void btnDataAcquisition_Click(object sender, EventArgs e)
	{
		if (dataAcqForm.Visible)
		{
			if (dataAcqForm.WindowState == FormWindowState.Minimized)
			{
				dataAcqForm.WindowState = FormWindowState.Normal;
			}
			dataAcqForm.BringToFront();
		}
		else
		{
			dataAcqForm.Show();
		}
	}

	public void btnDeviceMonitor_Click(object sender, EventArgs e)
	{
		method_7(DM_InitPage.Device);
	}

	private void btnDM2_Click(object sender, EventArgs e)
	{
		method_7(DM_InitPage.DM2);
	}

	private void btnDM3_Click(object sender, EventArgs e)
	{
		method_7(DM_InitPage.DM3);
	}

	private void btnDM4_Click(object sender, EventArgs e)
	{
		method_7(DM_InitPage.DM4);
	}

	private void method_0(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Normal;
		BringToFront();
	}

	private void btnIntegration_Click(object sender, EventArgs e)
	{
		dlgMethodSetup.ShowDialog(instrument.methodSetup, MtdDlgInitStyle.Integration);
	}

	private void btnLampZero_Click(object sender, EventArgs e)
	{
		devMonitorForm.btnZero_Click(null, null);
	}

	private void btnMtdHard_Click(object sender, EventArgs e)
	{
		dlgMethodSetup.ShowDialog(instrument.methodSetup, MtdDlgInitStyle.Control);
	}

	public void btnReportSetup_Click(object sender, EventArgs e)
	{
		if (instrument.user.uar_EditReportStyle)
		{
			dlgReportSetup.ShowDialog(rptSetup);
			return;
		}
		MessageBox.Show(Lang.PS("受限！", "No Right！"));
	}

	public void btnSequence_Click(object sender, EventArgs e)
	{
		if (seqAlyForm.Visible)
		{
			if (seqAlyForm.WindowState == FormWindowState.Minimized)
			{
				seqAlyForm.WindowState = FormWindowState.Normal;
			}
			seqAlyForm.BringToFront();
		}
		else
		{
			seqAlyForm.Show();
		}
	}

	public void btnSingle_Click(object sender, EventArgs e)
	{
		if (ssAlyForm.Visible)
		{
			if (ssAlyForm.WindowState == FormWindowState.Minimized)
			{
				ssAlyForm.WindowState = FormWindowState.Normal;
			}
			ssAlyForm.BringToFront();
		}
		else
		{
			ssAlyForm.Show();
		}
	}

	public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius, int leftWidth, ref GraphicsPath leftRC, ref GraphicsPath rightRC)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
		graphicsPath.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
		graphicsPath.AddArc(rect.Right - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
		graphicsPath.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Bottom - cornerRadius * 2);
		graphicsPath.AddArc(rect.Right - cornerRadius * 2, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0f, 90f);
		graphicsPath.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
		graphicsPath.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90f, 90f);
		graphicsPath.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
		graphicsPath.CloseFigure();
		if (leftRC == null)
		{
			leftRC = new GraphicsPath();
		}
		leftRC.ClearMarkers();
		leftRC.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
		leftRC.AddLine(rect.X + cornerRadius, rect.Y, rect.Left + leftWidth, rect.Y);
		leftRC.AddLine(rect.Left + leftWidth, rect.Y, rect.Left + leftWidth, rect.Bottom);
		leftRC.AddLine(rect.Left + leftWidth, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
		leftRC.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90f, 90f);
		leftRC.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
		if (rightRC == null)
		{
			rightRC = new GraphicsPath();
		}
		rightRC.ClearMarkers();
		rightRC.AddLine(rect.Left + leftWidth, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
		rightRC.AddArc(rect.Right - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
		rightRC.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Bottom - cornerRadius * 2);
		rightRC.AddArc(rect.Right - cornerRadius * 2, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0f, 90f);
		rightRC.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.Left + leftWidth, rect.Bottom);
		rightRC.AddLine(rect.Left + leftWidth, rect.Bottom, rect.Left + leftWidth, rect.Y);
		return graphicsPath;
	}

	private void dpInstru_MouseDown(object sender, MouseEventArgs e)
	{
		bool flag = false;
		if (rectangle_2.Contains(e.Location))
		{
			instrument.runningInjInfo.openChromWin = !instrument.runningInjInfo.openChromWin;
			flag = true;
		}
		else if (rectangle_1.Contains(e.Location))
		{
			instrument.runningInjInfo.openCaliWin = !instrument.runningInjInfo.openCaliWin;
			flag = true;
		}
		else if (rectangle_7.Contains(e.Location))
		{
			instrument.runningInjInfo.openPrintWin = !instrument.runningInjInfo.openPrintWin;
			flag = true;
		}
		if (flag)
		{
			dpInstru.Refresh();
		}
	}

	private void dpInstru_MouseMove(object sender, MouseEventArgs e)
	{
		LclInstruButton lclInstruButton = btnDeviceMonitor;
		LclInstruButton lclInstruButton2 = btnDM3;
		LclInstruButton lclInstruButton3 = btnDM2;
		bool flag = (btnDM4.Visible = rectangle_5.Contains(e.Location));
		bool flag3 = (lclInstruButton3.Visible = flag);
		bool visible = (lclInstruButton2.Visible = flag3);
		lclInstruButton.Visible = visible;
		if (!rectangle_2.Contains(e.Location) && !rectangle_1.Contains(e.Location) && !rectangle_7.Contains(e.Location))
		{
			dpInstru.Cursor = Cursors.Default;
		}
		else
		{
			dpInstru.Cursor = Cursors.Hand;
		}
	}

	private void dpInstru_Paint(object sender, PaintEventArgs e)
	{
		try
		{
			method_1(e.Graphics);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void method_1(Graphics graphics_0)
	{
		if (!btnDeviceMonitor.Visible)
		{
			switch (instrument.instruStyle)
			{
			case InstruStyle.GC:
				ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_17(), rectangle_5);
				break;
			case InstruStyle.LC:
				ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_18(), rectangle_5);
				break;
			}
		}
		graphics_0.FillPath(brTxtFrmItem, graphicsPath_1);
		graphics_0.FillPath(brTxtFrmValue, graphicsPath_2);
		for (int i = 1; i <= 5; i++)
		{
			graphics_0.DrawLine(pen_1, rectangle_3.Left, rectangle_3.Top + i * 16, rectangle_3.Right, rectangle_3.Top + i * 16);
		}
		graphics_0.DrawPath(pen_0, graphicsPath_0);
		graphics_0.FillPath(brTxtFrmItem, graphicsPath_5);
		graphics_0.FillPath(brTxtFrmValue, graphicsPath_4);
		graphics_0.DrawPath(pen_0, graphicsPath_3);
		Color color = IBrainColor.GetColor(pipe_color, 50);
		Point point = new Point(btnSingle.Left + btnSingle.Width / 2, btnSingle.Bottom + 1);
		Point point2 = new Point(btnSequence.Left + btnSequence.Width / 2, btnSequence.Bottom + 1);
		Point point3 = new Point(rectangle_5.Left + rectangle_5.Width / 2, rectangle_5.Top - 1);
		int num = (int)((float)(point3.Y - point.Y) / 3f);
		if (!instrument.sampling)
		{
			drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, point, num, color_2, color_3);
			drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, point2, num, color_2, color_3);
		}
		else if (instrument.injectStyle == InjectStyle.Single)
		{
			drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, point, num, color, pipe_color);
			drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, point2, num, color_2, color_3);
		}
		else
		{
			drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, point, num, color_2, color_3);
			drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, point2, num, color, pipe_color);
		}
		int num2 = 10;
		Point point4 = point;
		point4.Y += num + 10 + 1;
		Point ptFrom = point2;
		ptFrom.Y = point4.Y;
		if (!instrument.sampling)
		{
			drawPipe(graphics_0, pipeStyle.UR, pipePort.Common, point4, num2, color_2, color_3);
			drawPipe(graphics_0, pipeStyle.UL, pipePort.Common, ptFrom, num2, color_2, color_3);
		}
		else if (instrument.injectStyle == InjectStyle.Single)
		{
			drawPipe(graphics_0, pipeStyle.UR, pipePort.Common, point4, num2, color, pipe_color);
			drawPipe(graphics_0, pipeStyle.UL, pipePort.Common, ptFrom, num2, color_2, color_3);
		}
		else
		{
			drawPipe(graphics_0, pipeStyle.UR, pipePort.Common, point4, num2, color_2, color_3);
			drawPipe(graphics_0, pipeStyle.UL, pipePort.Common, ptFrom, num2, color, pipe_color);
		}
		Point point5 = new Point(point4.X + (ptFrom.X - point4.X) / 2, ptFrom.Y);
		pipe_node(graphics_0, point5, color, pipe_color);
		Point ptFrom2 = point4;
		ptFrom2.X += num2 + 1;
		int num3 = point5.X - ptFrom2.X - int_9;
		Point ptFrom3 = point5;
		ptFrom3.X += point5.X - ptFrom2.X - num3 + 1;
		if (!instrument.sampling)
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom2, num3, color_2, color_3);
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, num3, color_2, color_3);
		}
		else if (instrument.injectStyle == InjectStyle.Single)
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom2, num3, color, pipe_color);
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, num3, color_2, color_3);
		}
		else
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom2, num3, color_2, color_3);
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, num3, color, pipe_color);
		}
		Point ptFrom4 = point5;
		ptFrom4.Y += int_9 + 1;
		drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, ptFrom4, point3.Y - ptFrom4.Y, color, pipe_color);
		ptFrom3.X = rectangle_5.Right + 1;
		ptFrom3.Y = point_4.Y + btnMtdHard.Height / 2 + 1;
		int length = point_4.X - ptFrom3.X - 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, length, color, pipe_color);
		ptFrom3.X = btnMtdHard.Right + 1;
		length = point_2.X - ptFrom3.X - 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, length, color, pipe_color);
		ptFrom3.X = btnDataAcquisition.Right + 1;
		length = point_3.X - ptFrom3.X - 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, length, color, pipe_color);
		ptFrom3.X = btnIntegration.Right + 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, ptFrom3, length, color, pipe_color);
		point5.X = ptFrom3.X + length + int_9;
		point5.Y = ptFrom3.Y;
		pipe_node(graphics_0, point5, color, pipe_color);
		Point point6 = new Point(point5.X, point_1.Y + btnChromWindow.Height / 2 + 1);
		drawPipe(graphics_0, pipeStyle.DR, pipePort.Common, point6, num2, color, pipe_color);
		ptFrom4 = new Point(point6.X, point6.Y + num2 + 1);
		drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, ptFrom4, point5.Y - ptFrom4.Y - int_9, color, pipe_color);
		Point point7 = new Point(point5.X, point_5.Y + btnReportSetup.Height / 2);
		drawPipe(graphics_0, pipeStyle.UR, pipePort.Common, point7, num2, color, pipe_color);
		ptFrom4 = new Point(point5.X, point5.Y + int_9 + 1);
		drawPipe(graphics_0, pipeStyle.Down, pipePort.Common, ptFrom4, point7.Y - ptFrom4.Y - num2 - 1, color, pipe_color);
		int num4 = point_0.X - 20;
		point6.X += num2 + 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point6, num4 - point6.X, color, pipe_color);
		point5.X += int_9 + 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point5, num4 - point5.X, color, pipe_color);
		point7.X += num2 + 1;
		drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point7, num4 - point7.X, color, pipe_color);
		num4 = 18;
		point6.X = point_0.X - 18 - 1;
		if (instrument.runningInjInfo.openChromWin)
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point6, num4, color, pipe_color);
		}
		else
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point6, num4, color_2, color_3);
		}
		point5.X = point6.X;
		if (instrument.runningInjInfo.openCaliWin)
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point5, num4, color, pipe_color);
		}
		else
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point5, num4, color_2, color_3);
		}
		point7.X = point6.X;
		if (instrument.runningInjInfo.openPrintWin)
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point7, num4, color, pipe_color);
		}
		else
		{
			drawPipe(graphics_0, pipeStyle.Right, pipePort.Common, point7, num4, color_2, color_3);
		}
		Point p = new Point(-12, -13);
		point6.Offset(p);
		if (instrument.runningInjInfo.openChromWin)
		{
			ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_24(), point6);
		}
		else
		{
			ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_10(), point6);
		}
		point5.Offset(p);
		if (instrument.runningInjInfo.openCaliWin)
		{
			ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_24(), point5);
		}
		else
		{
			ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_10(), point5);
		}
		point7.Offset(p);
		if (instrument.runningInjInfo.openPrintWin)
		{
			ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_24(), point7);
		}
		else
		{
			ResourceImageLoad.DrawToGraphic(graphics_0, SystemBitmapResource2.smethod_10(), point7);
		}
		Point location = new Point(rectangle_5.Left, rectangle_4.Bottom + 8);
		if (instrument.lcc_Pumps.Length >= 1)
		{
			pbPump0.Location = location;
			location.X += pbPump0.Width + 5;
		}
		if (instrument.lcc_Pumps.Length >= 2)
		{
			pbPump1.Location = location;
			location.X += pbPump1.Width + 5;
		}
		if (instrument.dtc_Channels.Length != 0)
		{
			pbLamp.Location = location;
			location.X += pbLamp.Width + 5;
			btnLampZero.Location = location;
		}
		Refresh1();
	}

	public static void drawPipe(Graphics graphics_0, pipeStyle style, pipePort port, Point ptFrom, int length, Color lightColor, Color heavyColor)
	{
		if (length < 0)
		{
			length = -length;
		}
		switch (style)
		{
		case pipeStyle.Right:
			if (port == pipePort.Common)
			{
				smethod_1(graphics_0, ptFrom, Enum4.const_0, 5, 2, lightColor, heavyColor);
				ptFrom.X += 2;
				length -= 4;
				smethod_1(graphics_0, ptFrom, Enum4.const_0, 4, length, lightColor, heavyColor);
				ptFrom.X += length;
				smethod_1(graphics_0, ptFrom, Enum4.const_0, 5, 2, lightColor, heavyColor);
			}
			break;
		case pipeStyle.Down:
			if (port == pipePort.Common)
			{
				smethod_1(graphics_0, ptFrom, Enum4.const_1, 5, 2, lightColor, heavyColor);
				ptFrom.Y += 2;
				length -= 4;
				smethod_1(graphics_0, ptFrom, Enum4.const_1, 4, length, lightColor, heavyColor);
				ptFrom.Y += length;
				smethod_1(graphics_0, ptFrom, Enum4.const_1, 5, 2, lightColor, heavyColor);
			}
			break;
		case pipeStyle.UL:
		case pipeStyle.UR:
		{
			Point point_2 = ptFrom;
			point_2.Y -= length;
			smethod_1(graphics_0, point_2, Enum4.const_1, 5, 2, lightColor, heavyColor);
			point_2.Y += 2;
			smethod_1(graphics_0, point_2, Enum4.const_1, 4, length - 2, lightColor, heavyColor);
			point_2 = ptFrom;
			switch (style)
			{
			case pipeStyle.UL:
				point_2.X -= length;
				smethod_1(graphics_0, point_2, Enum4.const_0, 5, 2, lightColor, heavyColor);
				point_2.X += 2;
				smethod_1(graphics_0, point_2, Enum4.const_0, 4, length - 2, lightColor, heavyColor);
				smethod_0(graphics_0, ptFrom, pipeStyle.UL, lightColor, heavyColor);
				break;
			case pipeStyle.UR:
				smethod_1(graphics_0, point_2, Enum4.const_0, 4, length - 2, lightColor, heavyColor);
				point_2.X += length - 2;
				smethod_1(graphics_0, point_2, Enum4.const_0, 5, 2, lightColor, heavyColor);
				smethod_0(graphics_0, ptFrom, pipeStyle.UR, heavyColor, lightColor);
				break;
			}
			break;
		}
		case pipeStyle.DR:
		case pipeStyle.DL:
		{
			smethod_1(graphics_0, ptFrom, Enum4.const_1, 4, length - 2, lightColor, heavyColor);
			Point point_ = ptFrom;
			point_.Y += length - 2;
			smethod_1(graphics_0, point_, Enum4.const_1, 5, 2, lightColor, heavyColor);
			switch (style)
			{
			case pipeStyle.DR:
				smethod_1(graphics_0, ptFrom, Enum4.const_0, 4, length - 2, lightColor, heavyColor);
				point_ = ptFrom;
				point_.X += length - 2;
				smethod_1(graphics_0, point_, Enum4.const_0, 5, 2, lightColor, heavyColor);
				smethod_0(graphics_0, ptFrom, pipeStyle.DR, heavyColor, lightColor);
				break;
			case pipeStyle.DL:
				point_ = ptFrom;
				point_.X -= length - 2;
				smethod_1(graphics_0, point_, Enum4.const_0, 4, length - 2, lightColor, heavyColor);
				point_.X -= 2;
				smethod_1(graphics_0, point_, Enum4.const_0, 5, 2, lightColor, heavyColor);
				smethod_0(graphics_0, ptFrom, pipeStyle.DL, heavyColor, lightColor);
				break;
			}
			break;
		}
		}
	}

	private void method_2(Graphics graphics_0, Point point_8, Enum3 enum3_0, int int_10, int int_11, Color color_4, Color color_5)
	{
		rectangle_6.Location = point_8;
		switch (enum3_0)
		{
		case Enum3.const_0:
			rectangle_6.Offset(-int_11, 0);
			goto case Enum3.const_1;
		case Enum3.const_1:
		{
			rectangle_6.Width = int_11;
			rectangle_6.Height = int_10 / 2;
			LinearGradientBrush brush2 = new LinearGradientBrush(rectangle_6, color_4, color_5, LinearGradientMode.Vertical);
			graphics_0.FillRectangle(brush2, rectangle_6);
			rectangle_6.Offset(0, -rectangle_6.Height);
			brush2 = new LinearGradientBrush(rectangle_6, color_5, color_4, LinearGradientMode.Vertical);
			graphics_0.FillRectangle(brush2, rectangle_6);
			break;
		}
		case Enum3.const_2:
		case Enum3.const_3:
		{
			rectangle_6.Width = int_10 / 2;
			switch (enum3_0)
			{
			case Enum3.const_3:
				rectangle_6.Offset(-rectangle_6.Width, 0);
				break;
			case Enum3.const_2:
				rectangle_6.Offset(-rectangle_6.Width, -int_11);
				break;
			}
			rectangle_6.Height = int_11;
			LinearGradientBrush brush = new LinearGradientBrush(rectangle_6, color_5, color_4, LinearGradientMode.Horizontal);
			graphics_0.FillRectangle(brush, rectangle_6);
			rectangle_6.Offset(rectangle_6.Width, 0);
			Rectangle rect = rectangle_6;
			rect.X--;
			brush = new LinearGradientBrush(rect, color_4, color_5, LinearGradientMode.Horizontal);
			graphics_0.FillRectangle(brush, rectangle_6);
			break;
		}
		}
	}

	public void HideChildWindows()
	{
		ssAlyForm.Visible = false;
		seqAlyForm.Visible = false;
		devMonitorForm.Visible = false;
		dataAcqForm.Visible = false;
		chromForm.Visible = false;
		caliGpcForm.Visible = false;
	}

	private void InstrumentForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		instrument.CloseInstru();
	}

	public void InstrumentFormLoad()
	{
		InstrumentForm_Load(null, null);
	}

	private void InstrumentForm_Load(object sender, EventArgs e)
	{
		devMonitorForm = new DevMonitorForm(instrument);
		dlgMethodSetup = new MtdSetupDlg(instrument);
		dataAcqForm = new DataAcqForm(instrument);
		chromForm = new ChromForm();
		caliGpcForm = new CaliGpcForm(instrument);
		miFiExit.Click += base.miFiExit_Click;
		msInstru.Items.Add(miView);
		miView.DropDownItems.Add(new ToolStripSeparator());
		miView.DropDownItems.Add(toolStripMenuItem_0);
		toolStripMenuItem_0.Click += toolStripMenuItem_0_Click;
		msInstru.Items.Add(miWindow);
		miWindow.DropDownItems.Insert(0, toolStripMenuItem_1);
		miWindow.DropDownItems.Insert(1, new ToolStripSeparator());
		toolStripMenuItem_1.Click += toolStripMenuItem_1_Click;
		msInstru.Items.Add(miHelp);
		msInstru.Items.Add(new ToolStripSeparator());
		msInstru.Items.Add(mubtnMainForm);
		ResourceImageLoad.SetCtrlBitmap(btnNewMethod, SystemIconResource.smethod_27());
		ResourceImageLoad.SetCtrlBitmap(btnOpenMethod, SystemIconResource.smethod_31());
		ResourceImageLoad.SetCtrlBitmap(btnSaveMethod, SystemIconResource.smethod_37());
		ResourceImageLoad.SetCtrlBitmap(btnPjtDir, SystemIconResource.smethod_19());
		ResourceImageLoad.SetCtrlBitmap(btnOptions, SystemIconResource.smethod_57());
		lbTimeV.Text = "";
		LclLabel lclLabel = lbFileName;
		LclLabel lclLabel2 = lbSample;
		LclLabel lclLabel3 = lbSampleID;
		LclLabel lclLabel4 = lbMethod;
		int num = (lbMode.Left = rectangle_3.Left + 1);
		int num3 = (lclLabel4.Left = num);
		int num5 = (lclLabel3.Left = num3);
		int left = (lclLabel2.Left = num5);
		lclLabel.Left = left;
		LclLabel lclLabel5 = lbFileNameV;
		LclLabel lclLabel6 = lbSampleV;
		LclLabel lclLabel7 = lbSampleIDV;
		LclLabel lclLabel8 = lbMethodV;
		num = (lbModeV.Left = rectangle_3.Left + 65 + 1);
		num3 = (lclLabel8.Left = num);
		num5 = (lclLabel7.Left = num3);
		left = (lclLabel6.Left = num5);
		lclLabel5.Left = left;
		LclLabel lclLabel9 = lbFileName;
		left = (lbFileNameV.Top = rectangle_3.Top + 1);
		lclLabel9.Top = left;
		LclLabel lclLabel10 = lbSample;
		left = (lbSampleV.Top = lbFileName.Top + 16);
		lclLabel10.Top = left;
		LclLabel lclLabel11 = lbSampleID;
		left = (lbSampleIDV.Top = lbSample.Top + 16);
		lclLabel11.Top = left;
		LclLabel lclLabel12 = lbMethod;
		left = (lbMethodV.Top = lbSampleID.Top + 16);
		lclLabel12.Top = left;
		LclLabel lclLabel13 = lbMode;
		left = (lbModeV.Top = lbMethod.Top + 16);
		lclLabel13.Top = left;
		LclLabel lclLabel14 = lbFileNameV;
		LclLabel lclLabel15 = lbSampleV;
		LclLabel lclLabel16 = lbSampleIDV;
		LclLabel lclLabel17 = lbMethodV;
		num = (lbModeV.Width = rectangle_3.Width - 65 - 4);
		num3 = (lclLabel17.Width = num);
		num5 = (lclLabel16.Width = num3);
		left = (lclLabel15.Width = num5);
		lclLabel14.Width = left;
		LclLabel lclLabel18 = lbFileNameV;
		LclLabel lclLabel19 = lbSampleV;
		LclLabel lclLabel20 = lbSampleIDV;
		LclLabel lclLabel21 = lbMethodV;
		num = (lbModeV.Height = lbFileName.Height);
		num3 = (lclLabel21.Height = num);
		num5 = (lclLabel20.Height = num3);
		left = (lclLabel19.Height = num5);
		lclLabel18.Height = left;
		LclLabel lclLabel22 = lbFileNameV;
		LclLabel lclLabel23 = lbSampleV;
		LclLabel lclLabel24 = lbSampleIDV;
		Color color = (lbModeV.BackColor = color_1);
		Color color3 = (lclLabel24.BackColor = color);
		Color backColor = (lclLabel23.BackColor = color3);
		lclLabel22.BackColor = backColor;
		lbMethodV.BackColor = color_0;
		lbTimeV.Left = rectangle_4.Left + 4;
		lbTimeV.Width = 76;
		lbStateV.Left = lbTimeV.Right + 1;
		lbStateV.Width = rectangle_4.Width - lbStateV.Left;
		LclLabel lclLabel25 = lbTimeV;
		left = (lbStateV.Top = rectangle_4.Top + 2);
		lclLabel25.Top = left;
		LclLabel lclLabel26 = lbTimeV;
		left = (lbStateV.Height = lbFileName.Height);
		lclLabel26.Height = left;
		lbTimeV.BackColor = color_0;
		lbStateV.BackColor = color_1;
		btnSingle.SetStillImage(SystemBitmapResource2.smethod_29());
		LclInstruButton lclInstruButton = btnSingle;
		Size size = (btnSequence.Size = new Size(37, 37));
		lclInstruButton.Size = size;
		int left2 = rectangle_5.Left;
		int num27 = rectangle_5.Width / 2;
		LclInstruButton lclInstruButton2 = btnDeviceMonitor;
		LclInstruButton lclInstruButton3 = btnDM3;
		LclInstruButton lclInstruButton4 = btnDM2;
		Size size3 = (btnDM4.Size = new Size(rectangle_5.Width / 2, rectangle_5.Height / 2));
		Size size5 = (lclInstruButton4.Size = size3);
		size = (lclInstruButton3.Size = size5);
		lclInstruButton2.Size = size;
		Control control = btnDeviceMonitor;
		Control control2 = btnDM3;
		Control control3 = btnDM2;
		btnDM4.Visible = false;
		control3.Visible = false;
		control2.Visible = false;
		control.Visible = false;
		btnIntegration.SetStillImage(SystemBitmapResource2.smethod_19());
		btnMtdHard.SetStillImage(SystemBitmapResource2.smethod_23());
		btnChromWindow.SetStillImage(SystemBitmapResource2.smethod_9());
		btnReportSetup.SetStillImage(SystemBitmapResource2.smethod_27());
		LclInstruButton lclInstruButton5 = btnDataAcquisition;
		LclInstruButton lclInstruButton6 = btnIntegration;
		LclInstruButton lclInstruButton7 = btnMtdHard;
		LclInstruButton lclInstruButton8 = btnChromWindow;
		LclInstruButton lclInstruButton9 = btnCaliWindow;
		Size size8 = (btnReportSetup.Size = new Size(37, 37));
		Size size10 = (lclInstruButton9.Size = size8);
		size3 = (lclInstruButton8.Size = size10);
		size5 = (lclInstruButton7.Size = size3);
		size = (lclInstruButton6.Size = size5);
		lclInstruButton5.Size = size;
		rectangle_2 = btnChromWindow.Bounds;
		rectangle_1 = btnCaliWindow.Bounds;
		rectangle_7 = btnReportSetup.Bounds;
		rectangle_2.Width *= 2;
		rectangle_2.Width /= 3;
		rectangle_2.Height *= 2;
		rectangle_2.Height /= 3;
		rectangle_1.Width *= 2;
		rectangle_1.Width /= 3;
		rectangle_1.Height *= 2;
		rectangle_1.Height /= 3;
		rectangle_7.Width *= 2;
		rectangle_7.Width /= 3;
		rectangle_7.Height *= 2;
		rectangle_7.Height /= 3;
		rectangle_2.Location = new Point(400, 74);
		rectangle_1.Location = new Point(400, 215);
		rectangle_7.Location = new Point(400, 235);
		EventHandler dropDownOpeningHandler = method_4;
		EventHandler clickHandler = method_3;
		miWindowsHandler(dropDownOpeningHandler, clickHandler);
		ssAlyForm.miWindowsHandler(dropDownOpeningHandler, clickHandler);
		seqAlyForm.miWindowsHandler(dropDownOpeningHandler, clickHandler);
		devMonitorForm.miWindowsHandler(dropDownOpeningHandler, clickHandler);
		dataAcqForm.miWindowsHandler(dropDownOpeningHandler, clickHandler);
		caliGpcForm.miWindowsHandler(dropDownOpeningHandler, clickHandler);
		method_5();
		timer_0_Tick(null, null);
		RefreshInfo(InjectStyle.Single);
		dataAcqForm.Set3Buttons(enabled: false);
		ssAlyForm.Set3Buttons(enabled: false);
		seqAlyForm.Set3Buttons(enabled: false);
	}

	private void InstrumentForm_VisibleChanged(object sender, EventArgs e)
	{
		int pageNo = instrument.pageNo;
		ATResult atResult = ATResult.Ok;
		ATType atType = ATType.OpenInstru;
		string u_name = instrument.user.u_name;
		string name = instrument.name;
		ATArea atArea = ATArea.Instru;
		string descript = Lang.PS("打开仪器", "Open Instrument");
		if (base.Visible)
		{
			MainForm.stationAdtTrlForm.AddTail(pageNo, atResult, atType, u_name, name, atArea, descript);
			return;
		}
		atType = ATType.CloseInstru;
		descript = Lang.PS("关闭仪器", "Close Instrument");
		MainForm.stationAdtTrlForm.AddTail(pageNo, atResult, atType, u_name, name, atArea, descript);
	}

	public void LoadCaliFile(string fileName)
	{
		if (File.Exists(fileName) && instrument.instruStyle != InstruStyle.GPC)
		{
			CaliGnlForm.LoadCalFileShowForm(fileName);
		}
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		miFile.Text = Lang.PS("文件", "File");
		miFiNewMethod.Text = Lang.PS("新建方法", "New Method");
		miFiOpenMethod.Text = Lang.PS("打开方法", "Open Method");
		miFiSaveMethod.Text = Lang.PS("保存方法", "Save Method");
		miFiSaveMethodAs.Text = Lang.PS("另存方法...", "Save Method as...");
		miFiPjtDir.Text = Lang.PS("工程目录", "Project...");
		miFiReportSetup.Text = Lang.PS("样式文件...", "Style Set...");
		miFiAutoOverW.Text = Lang.PS("覆盖文件", "Over write");
		miFiExit.Text = Lang.PS("退出", "Exit");
		toolStripMenuItem_1.Text = Lang.PS("隐藏窗口", "Hide Windows");
		toolStripMenuItem_0.Text = Lang.PS("选项...", "Options...");
		lbFileName.Text = Lang.PS("文件名:", "File Name:");
		lbSample.Text = Lang.PS("样品:", "Sample:");
		lbSampleID.Text = Lang.PS("样品ID:", "SampleID:");
		lbMethod.Text = Lang.PS("方法:", "Method:");
		lbMode.Text = Lang.PS("模式:", "Mode:");
		slbExplain.Text = Lang.PS("帮助，按F1", "For Help,press F1");
		btnNewMethod.Text = miFiNewMethod.Text;
		btnOpenMethod.Text = miFiOpenMethod.Text;
		btnSaveMethod.Text = miFiSaveMethod.Text;
		btnPjtDir.Text = miFiPjtDir.Text;
		btnOptions.Text = toolStripMenuItem_0.Text;
		refreshTitle();
	}

	public void LoadMethodFile(string fileName)
	{
		if (File.Exists(fileName))
		{
			instrument.methodSetup.LoadFromFile(fileName);
			lbMethodV.Text = instrument.methodSetup.strMtdShowName;
			instrument.ApplyMethod();
		}
	}

	public void LoadReportStyleFile(string fileName)
	{
		if (File.Exists(fileName))
		{
			rptSetup.LoadFromFile(fileName);
			string_52 = fileName;
		}
	}

	private void miFiAutoOverW_Click(object sender, EventArgs e)
	{
		miFiAutoOverW.Checked = !miFiAutoOverW.Checked;
	}

	private void btnNewMethod_Click(object sender, EventArgs e)
	{
		instrument.methodSetup = new MtdSetup();
		instrument.methodSetup.Init(instrument);
		lbMethodV.Text = instrument.methodSetup.strMtdShowName;
	}

	private void lbMethodV_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0 == null)
		{
			openFileDialog_0 = new OpenFileDialog();
			openFileDialog_0.Title = "打开方法";
			openFileDialog_0.Filter = Class49.MakeFileFilter(".mtd");
		}
		openFileDialog_0.InitialDirectory = instrument.PrjPath;
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			LoadMethodFile(openFileDialog_0.FileName);
		}
	}

	private void btnPjtDir_Click(object sender, EventArgs e)
	{
		HideChildWindows();
		pjtDirDlg_0.instrument = instrument;
		PjtDir pjtDir = pjtDirDlg_0.ShowDialog(instrument.pjtDir);
		if (pjtDir != null)
		{
			instrument.pjtDir = pjtDir;
			SetProjectDir();
		}
	}

	private void miFiRecoverData_Click(object sender, EventArgs e)
	{
	}

	private void miFiReportSetup_Click(object sender, EventArgs e)
	{
		if (openFileDialog_1 == null)
		{
			openFileDialog_1 = new OpenFileDialog();
			openFileDialog_1.Title = Lang.PS("选择报告样式", "Select Report Style");
			openFileDialog_1.Filter = Class49.MakeFileFilter(".sty");
		}
		openFileDialog_1.InitialDirectory = instrument.PrjPath;
		if (openFileDialog_1.ShowDialog() == DialogResult.OK)
		{
			LoadReportStyleFile(openFileDialog_1.FileName);
		}
	}

	private void btnSaveMethod_Click(object sender, EventArgs e)
	{
		if (instrument.methodSetup.strMtdFilePath == "")
		{
			miFiSaveMethodAs_Click(null, null);
		}
		else
		{
			instrument.methodSetup.SaveToFile(instrument.methodSetup.strMtdFilePath);
		}
	}

	private void miFiSaveMethodAs_Click(object sender, EventArgs e)
	{
		if (saveFileDialog_0 == null)
		{
			saveFileDialog_0 = new SaveFileDialog();
			saveFileDialog_0.Title = Lang.PS("另存方法", "Save As Method");
			saveFileDialog_0.InitialDirectory = instrument.PrjPath;
			saveFileDialog_0.Filter = Class49.MakeFileFilter(".mtd");
		}
		if (saveFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			instrument.methodSetup.SaveToFile(saveFileDialog_0.FileName);
			lbMethodV.Text = instrument.methodSetup.strMtdShowName;
		}
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
	}

	private void method_3(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		toolStripMenuItem.Checked = true;
		switch ((WinStyle)toolStripMenuItem.Tag)
		{
		case WinStyle.Instrument:
			base.WindowState = FormWindowState.Normal;
			BringToFront();
			break;
		case WinStyle.DataAcq:
			btnDataAcquisition_Click(null, null);
			break;
		case WinStyle.Chromatogram:
			btnChromWindow_Click(null, null);
			break;
		case WinStyle.CaliGnl:
		case WinStyle.CaliGpc:
			btnCaliWindow_Click(null, null);
			break;
		case WinStyle.SglAly:
			btnSingle_Click(null, null);
			break;
		case WinStyle.SeqAly:
			btnSequence_Click(null, null);
			break;
		case WinStyle.DevMonitor:
			btnDeviceMonitor_Click(null, null);
			break;
		case WinStyle.StationAdtTrl:
			MainForm.stationAdtTrlForm.Show(instrument.pageNo);
			break;
		}
	}

	private void toolStripMenuItem_0_Click(object sender, EventArgs e)
	{
		Class49.optionsDialog_0.ShowDialog(instrument, WinStyle.Instrument, instrument.user.options);
	}

	private void method_4(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		for (int i = 0; i < toolStripMenuItem.DropDownItems.Count; i++)
		{
			if (!(toolStripMenuItem.DropDownItems[i] is ToolStripMenuItem))
			{
				continue;
			}
			ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)toolStripMenuItem.DropDownItems[i];
			if (toolStripMenuItem2.Tag == null)
			{
				continue;
			}
			switch ((WinStyle)toolStripMenuItem2.Tag)
			{
			case WinStyle.DataAcq:
				toolStripMenuItem2.Checked = dataAcqForm.Visible;
				break;
			case WinStyle.Chromatogram:
				toolStripMenuItem2.Checked = chromForm.Visible;
				break;
			case WinStyle.CaliGpc:
				toolStripMenuItem2.Checked = caliGpcForm.Visible;
				break;
			case WinStyle.SglAly:
				toolStripMenuItem2.Checked = ssAlyForm.Visible;
				break;
			case WinStyle.SeqAly:
				toolStripMenuItem2.Checked = seqAlyForm.Visible;
				break;
			case WinStyle.DevMonitor:
				toolStripMenuItem2.Checked = devMonitorForm.Visible;
				break;
			case WinStyle.StationAdtTrl:
				if (MainForm.stationAdtTrlForm == null)
				{
					toolStripMenuItem2.Checked = false;
				}
				else
				{
					toolStripMenuItem2.Checked = MainForm.stationAdtTrlForm.Visible;
				}
				break;
			}
		}
	}

	private void toolStripMenuItem_1_Click(object sender, EventArgs e)
	{
		ssAlyForm.Visible = false;
		seqAlyForm.Visible = false;
		devMonitorForm.Visible = false;
		dataAcqForm.Visible = false;
		chromForm.Visible = false;
		caliGnlForm.Visible = false;
		caliGpcForm.Visible = false;
		if (MainForm.stationAdtTrlForm != null)
		{
			MainForm.stationAdtTrlForm.Visible = false;
		}
	}

	private void method_5()
	{
		mubtnClickHandler(mubtnMainFormHandler, null, null, null, null, null, null, null);
		devMonitorForm.mubtnClickHandler(mubtnMainFormHandler, method_0, null, null, null, null, null, null);
		ssAlyForm.mubtnClickHandler(mubtnMainFormHandler, method_0, btnDataAcquisition_Click, btnChromWindow_Click, btnCaliWindow_Click, null, null, null);
		seqAlyForm.mubtnClickHandler(mubtnMainFormHandler, method_0, btnDataAcquisition_Click, btnChromWindow_Click, btnCaliWindow_Click, null, null, null);
		dataAcqForm.mubtnClickHandler(mubtnMainFormHandler, method_0, btnDataAcquisition_Click, btnChromWindow_Click, btnCaliWindow_Click, btnSingle_Click, btnSequence_Click, btnDeviceMonitor_Click);
		caliGnlForm.mubtnClickHandler(mubtnMainFormHandler, method_0, btnDataAcquisition_Click, btnChromWindow_Click, btnCaliWindow_Click, btnSingle_Click, btnSequence_Click, btnDeviceMonitor_Click);
		caliGpcForm.mubtnClickHandler(mubtnMainFormHandler, method_0, btnDataAcquisition_Click, btnChromWindow_Click, btnCaliWindow_Click, btnSingle_Click, btnSequence_Click, btnDeviceMonitor_Click);
	}

	protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
	{
		if (instrument.sampling)
		{
			cancelEventArgs_0.Cancel = true;
		}
		else
		{
			base.OnClosing(cancelEventArgs_0);
		}
	}

	private void method_6(int int_10, IntPtr intptr_0, IntPtr intptr_1)
	{
		PostMessage(base.Handle, int_10, intptr_0, intptr_1);
	}

	private void pbLamp_Click(object sender, EventArgs e)
	{
		devMonitorForm.btnLight_Click(null, null);
	}

	private void pbPump0_Click(object sender, EventArgs e)
	{
		if (sender == pbPump0)
		{
			devMonitorForm.btnPumpClick(0);
		}
		if (sender == pbPump1)
		{
			devMonitorForm.btnPumpClick(1);
		}
	}

	private static void smethod_0(Graphics graphics_0, Point point_8, pipeStyle pipeStyle_0, Color color_4, Color color_5)
	{
		Point point = point_8;
		Point point2 = point_8;
		Point point3 = point_8;
		Point pt = default(Point);
		Point pt2 = default(Point);
		Point point4 = default(Point);
		Point point5 = default(Point);
		Point point6 = default(Point);
		Point point7 = default(Point);
		Point point8 = default(Point);
		Point point9 = default(Point);
		switch (pipeStyle_0)
		{
		case pipeStyle.UL:
			point.Offset(-4, -4);
			point2.Offset(0, 0);
			point3.Offset(3, 3);
			point_8.Offset(-5, -4);
			point_8.Offset(-4, -5);
			point_8.Offset(-1, 0);
			point_8.Offset(0, -1);
			point_8.Offset(2, 4);
			point_8.Offset(4, 2);
			point_8.Offset(-4, 0);
			point_8.Offset(0, -4);
			break;
		case pipeStyle.UR:
			point.Offset(4, -4);
			point2.Offset(0, 0);
			point3.Offset(-3, 3);
			point_8.Offset(5, -4);
			point_8.Offset(4, -5);
			point_8.Offset(1, 0);
			point_8.Offset(0, -1);
			point_8.Offset(-2, 4);
			point_8.Offset(-4, 2);
			point_8.Offset(4, 0);
			point_8.Offset(0, -4);
			break;
		case pipeStyle.DR:
			point.Offset(4, 4);
			point2.Offset(1, 1);
			point3.Offset(-3, -3);
			point_8.Offset(4, 5);
			point_8.Offset(5, 4);
			point_8.Offset(0, 1);
			point_8.Offset(1, 0);
			point_8.Offset(-4, -2);
			point_8.Offset(-2, -4);
			point_8.Offset(0, 4);
			point_8.Offset(4, 0);
			break;
		case pipeStyle.DL:
			point.Offset(-4, 4);
			point2.Offset(-1, 1);
			point3.Offset(3, -3);
			pt = point_8;
			pt.Offset(-4, 5);
			pt2 = point_8;
			pt2.Offset(-5, 4);
			point4 = point_8;
			point4.Offset(0, 1);
			point5 = point_8;
			point5.Offset(-1, 0);
			point6 = point_8;
			point6.Offset(4, -2);
			point7 = point_8;
			point7.Offset(2, -4);
			point8 = point_8;
			point8.Offset(0, 4);
			point9 = point_8;
			point9.Offset(-4, 0);
			break;
		}
		LinearGradientBrush brush = new LinearGradientBrush(point, point2, IBrainColor.GetColor(color_4, 150), color_4);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.Reset();
		graphicsPath.AddLine(pt, point8);
		graphicsPath.AddLine(point8, point4);
		graphicsPath.AddLine(point4, point5);
		graphicsPath.AddLine(point5, point9);
		graphicsPath.AddLine(point9, pt2);
		graphicsPath.CloseFigure();
		graphics_0.FillPath(brush, graphicsPath);
		point2.Offset(-1, -1);
		point3.Offset(-1, -1);
		brush = new LinearGradientBrush(point2, point3, color_5, IBrainColor.GetColor(color_5, 150));
		switch (pipeStyle_0)
		{
		case pipeStyle.UL:
			point_8.Offset(0, 4);
			point_8.Offset(4, 0);
			break;
		case pipeStyle.UR:
			point_8.Offset(0, 4);
			point_8.Offset(-4, 0);
			break;
		case pipeStyle.DR:
			point_8.Offset(-4, 0);
			point_8.Offset(0, -4);
			break;
		case pipeStyle.DL:
			point8 = point_8;
			point8.Offset(4, 0);
			point9 = point_8;
			point9.Offset(0, -4);
			break;
		}
		graphicsPath.Reset();
		graphicsPath.AddLine(point4, point8);
		graphicsPath.AddLine(point8, point6);
		graphicsPath.AddLine(point6, point7);
		graphicsPath.AddLine(point7, point9);
		graphicsPath.AddLine(point9, point5);
		graphicsPath.CloseFigure();
		graphics_0.FillPath(brush, graphicsPath);
	}

	private static void smethod_1(Graphics graphics_0, Point point_8, Enum4 enum4_0, int int_10, int int_11, Color color_4, Color color_5)
	{
		switch (enum4_0)
		{
		case Enum4.const_0:
		{
			rectangle_0.Size = new Size(int_11, int_10);
			rectangle_0.Location = new Point(point_8.X, point_8.Y - rectangle_0.Height);
			Rectangle rect2 = rectangle_0;
			rect2.Y++;
			LinearGradientBrush brush2 = new LinearGradientBrush(rect2, IBrainColor.GetColor(color_4, 150), color_4, LinearGradientMode.Vertical);
			graphics_0.FillRectangle(brush2, rectangle_0);
			rectangle_0.Y += rectangle_0.Height;
			rect2 = rectangle_0;
			rect2.Y--;
			brush2 = new LinearGradientBrush(rect2, color_5, IBrainColor.GetColor(color_5, 150), LinearGradientMode.Vertical);
			graphics_0.FillRectangle(brush2, rectangle_0);
			break;
		}
		case Enum4.const_1:
		{
			rectangle_0.Size = new Size(int_10, int_11);
			rectangle_0.Location = new Point(point_8.X - rectangle_0.Width, point_8.Y);
			LinearGradientBrush brush = new LinearGradientBrush(rectangle_0, IBrainColor.GetColor(color_4, 150), color_4, LinearGradientMode.Horizontal);
			graphics_0.FillRectangle(brush, rectangle_0);
			rectangle_0.X += rectangle_0.Width;
			Rectangle rect = rectangle_0;
			rect.X--;
			brush = new LinearGradientBrush(rect, color_5, IBrainColor.GetColor(color_5, 150), LinearGradientMode.Horizontal);
			graphics_0.FillRectangle(brush, rectangle_0);
			break;
		}
		}
	}

	public static void pipe_node(Graphics graphics_0, Point ptForm, Color color1, Color color2)
	{
		int_9 = 6;
		Point location = ptForm;
		location.Offset(-6, -6);
		Rectangle rect = new Rectangle(location, new Size(12, 12));
		SolidBrush brush = new SolidBrush(color1);
		graphics_0.FillEllipse(brush, rect);
		location = ptForm;
		location.Offset(-4, -4);
		rect = new Rectangle(location, new Size(8, 8));
		brush = new SolidBrush(color2);
		graphics_0.FillEllipse(brush, rect);
	}

	[DllImport("user32.dll")]
	public static extern IntPtr PostMessage(IntPtr hWnd, int int_10, IntPtr wParam, IntPtr lParam);

	public void PrintChromForm_CurChrom(string styName)
	{
		if (chromForm.CurChrom != null && styName != string_52)
		{
			string_52 = styName;
		}
	}

	public override void refresh_once()
	{
		base.refresh_once();
		refreshTitle();
		ToolStripItem toolStripItem = mubtnCaliGnl;
		miWinCaliGnl.Visible = true;
		toolStripItem.Visible = true;
		ToolStripItem toolStripItem2 = mubtnCaliGpc;
		miWinCaliGpc.Visible = false;
		toolStripItem2.Visible = false;
		btnCaliWindow.SetStillImage(SystemBitmapResource2.smethod_7());
		switch (instrument.instruStyle)
		{
		case InstruStyle.GC:
			tssInstruStyle.Text = Lang.PS("[气相]", "[GC]");
			break;
		case InstruStyle.LC:
			tssInstruStyle.Text = Lang.PS("[液相]", "[LC]");
			break;
		case InstruStyle.GPC:
		{
			tssInstruStyle.Text = Lang.PS("[凝胶渗透]", "[GPC]");
			ToolStripItem toolStripItem3 = mubtnCaliGnl;
			miWinCaliGnl.Visible = false;
			mubtnCaliGnl.Visible = false;
			miWinCaliGpc.Visible = true;
			mubtnCaliGpc.Visible = true;
			btnCaliWindow.SetStillImage(SystemBitmapResource2.smethod_8());
			break;
		}
		case InstruStyle.PDA:
			tssInstruStyle.Text = Lang.PS("[二极管阵列]", "[DAD]");
			break;
		}
		switch (instrument.pageNo)
		{
		case 0:
			base.Icon = SystemIconResource.smethod_11();
			break;
		case 1:
			base.Icon = SystemIconResource.smethod_12();
			break;
		case 2:
			base.Icon = SystemIconResource.smethod_13();
			break;
		case 3:
			base.Icon = SystemIconResource.smethod_14();
			break;
		}
		devMonitorForm.refresh_once();
		ssAlyForm.refresh_once();
		seqAlyForm.refresh_once();
		dataAcqForm.refresh_once();
		chromForm.refresh_once();
		dlgMethodSetup.refresh_once();
		caliGpcForm.refresh_once();
		dlgReportSetup = new RptSetupDlg(instrument);
		SetProjectDir();
		if (Class49.edition_0 != Edition.VI2010 && instrument.instruStyle != InstruStyle.GC)
		{
			btnLampZero.Enabled = true;
			pbPump1.Enabled = true;
			pbPump0.Enabled = true;
			pbLamp.Enabled = true;
		}
		else
		{
			btnLampZero.Enabled = false;
			pbPump1.Enabled = false;
			pbPump0.Enabled = false;
			pbLamp.Enabled = false;
		}
	}

	public void Refresh1()
	{
		if (pbPump0.Visible = instrument.lcc_Pumps.Length >= 1)
		{
			if (devMonitorForm.Pump0Running)
			{
				if (pbPump0.Tag == null)
				{
					pbPump0.Image = SystemBitmapResource2.smethod_25();
					pbPump0.Tag = 1;
				}
			}
			else if (pbPump0.Tag != null)
			{
				pbPump0.Image = SystemBitmapResource2.smethod_26();
				pbPump0.Tag = null;
			}
		}
		if (pbPump1.Visible = instrument.lcc_Pumps.Length >= 2)
		{
			if (devMonitorForm.Pump1Running)
			{
				if (pbPump1.Tag == null)
				{
					pbPump1.Image = SystemBitmapResource2.smethod_25();
					pbPump1.Tag = 1;
				}
			}
			else if (pbPump1.Tag != null)
			{
				pbPump1.Image = SystemBitmapResource2.smethod_26();
				pbPump1.Tag = null;
			}
		}
		LclPictureBox lclPictureBox = pbLamp;
		bool flag3 = (btnLampZero.Visible = instrument.dtc_Channels.Length != 0);
		if (!(lclPictureBox.Visible = flag3))
		{
			return;
		}
		if (devMonitorForm.Lighting)
		{
			if (pbLamp.Tag == null)
			{
				pbLamp.Image = SystemBitmapResource2.smethod_21();
				pbLamp.Tag = 1;
			}
		}
		else if (pbLamp.Tag != null)
		{
			pbLamp.Image = SystemBitmapResource2.smethod_20();
			pbLamp.Tag = null;
		}
	}

	public void RefreshInfo(InjectStyle injectStyle)
	{
		instrument.injectStyle = injectStyle;
		timer_0.Enabled = instrument.sampling;
		lbSampleV.Text = CMS_InfoParasFMT.FmtStr(1, instrument.runningInjInfo, instrument);
		lbSampleIDV.Text = CMS_InfoParasFMT.FmtStr(0, instrument.runningInjInfo, instrument);
		lbMethodV.Text = instrument.methodSetup.strMtdShowName;
		if (instrument.injectStyle == InjectStyle.Single)
		{
			if (instrument.sampling)
			{
				lbModeV.Text = Lang.PS("单针运行", "Single Run");
			}
			else
			{
				lbTimeV.Text = "";
				lbModeV.Text = "";
			}
		}
		else if (instrument.injectStyle == InjectStyle.Sequence)
		{
			string text = Lang.PS("完成  ", "Over   ");
			if (instrument.runningInjInfo.injStatus == InjStatusMeasure.Prepared)
			{
				text = Lang.PS("准备  ", "Prepare ");
			}
			else if (instrument.runningInjInfo.injStatus == InjStatusMeasure.BeingMeasured)
			{
				text = Lang.PS("进样, ", "Inject, ");
			}
			if (instrument.runningInjInfo.vialNo > 0)
			{
				string[] array = new string[8]
				{
					text,
					Lang.PS("瓶", "Vial"),
					":",
					instrument.runningInjInfo.vialNo.ToString(),
					" / ",
					Lang.PS("针", "Inj."),
					":",
					(instrument.runningInjInfo.injNo + 1).ToString()
				};
				lbModeV.Text = string.Concat(array);
			}
			else
			{
				lbModeV.Text = text;
			}
			if (!instrument.sampling)
			{
				timer_0_Tick(null, null);
				lbTimeV.Text = "";
			}
		}
		lbStateV.Text = (instrument.sampling ? Lang.PS("运行", "Running") : Lang.PS("等待", "Waitting"));
		dpInstru.Refresh();
		instrument.form.dataAcqForm.GetAutoStopState();
	}

	public void refreshTitle()
	{
		if (instrument != null)
		{
			if (instrument.pjtDir != null)
			{
				Text = instrument.name + "[" + Lang.PS("工程", "Project") + ":" + instrument.pjtDir.projectName + "]";
			}
			else
			{
				Text = instrument.name + "[" + Lang.PS("工程", "Project") + ":]";
			}
		}
	}

	public void SeqRefreshInfo()
	{
		if (iasyncResult_0 == null || iasyncResult_0.IsCompleted)
		{
			object_0[0] = 1027;
			object_0[1] = (object_0[2] = IntPtr.Zero);
			if ((object)delegate_0 == null)
			{
				delegate_0 = new Delegate0(method_6);
			}
			iasyncResult_0 = BeginInvoke(delegate_0, object_0);
		}
	}

	public void SetProjectDir()
	{
		refreshTitle();
		if (instrument.pjtDir != null)
		{
			string projectDir = instrument.pjtDir.PjtFullName + "\\";
			SetProjectDir(projectDir);
			ssAlyForm.SetProjectDir(projectDir);
			seqAlyForm.SetProjectDir(projectDir);
			dataAcqForm.SetProjectDir(projectDir);
			chromForm.SetProjectDir(projectDir);
		}
	}

	public new void Show()
	{
		btnSequence.Enabled = instrument.asc_Samplers.Length != 0;
		if (btnSequence.Enabled)
		{
			btnSequence.SetStillImage(SystemBitmapResource2.smethod_28());
		}
		else
		{
			btnSequence.SetStillImage(SystemBitmapResource2.smethod_5());
		}
		if (instrument.instruStyle == InstruStyle.GC)
		{
			btnDeviceMonitor.Enabled = instrument.gcc_GCss.Length != 0;
			if (btnDeviceMonitor.Enabled)
			{
				btnDeviceMonitor.SetStillImage(SystemBitmapResource2.smethod_14());
			}
			else
			{
				btnDeviceMonitor.SetStillImage(SystemBitmapResource2.smethod_3());
			}
			btnDM4.SetStillImage(SystemBitmapResource2.smethod_22());
		}
		else
		{
			btnDeviceMonitor.Enabled = instrument.lcc_Pumps.Length != 0;
			if (btnDeviceMonitor.Enabled)
			{
				btnDeviceMonitor.SetStillImage(SystemBitmapResource2.smethod_15());
			}
			else
			{
				btnDeviceMonitor.SetStillImage(SystemBitmapResource2.smethod_4());
			}
			btnDM4.SetStillImage(SystemBitmapResource2.smethod_11());
		}
		btnDM3.Enabled = true;
		if (btnDM3.Enabled)
		{
			btnDM3.SetStillImage(SystemBitmapResource2.smethod_12());
		}
		else
		{
			btnDM3.SetStillImage(SystemBitmapResource2.smethod_1());
		}
		btnDM2.Enabled = true;
		if (btnDM2.Enabled)
		{
			btnDM2.SetStillImage((instrument.instruStyle == InstruStyle.GC) ? SystemBitmapResource2.smethod_6() : SystemBitmapResource2.smethod_16());
		}
		else
		{
			btnDM2.SetStillImage((instrument.instruStyle == InstruStyle.GC) ? SystemBitmapResource2.smethod_0() : SystemBitmapResource2.smethod_16());
		}
		btnDataAcquisition.Enabled = instrument.dtc_Channels.Length != 0;
		btnDataAcquisition.SetStillImage(btnDataAcquisition.Enabled ? SystemBitmapResource2.smethod_13() : SystemBitmapResource2.smethod_2());
		base.Show();
		refresh_once();
	}

	private void method_7(DM_InitPage dm_InitPage_0)
	{
		devMonitorForm.SetTopPage(dm_InitPage_0);
		if (devMonitorForm.Visible)
		{
			if (devMonitorForm.WindowState == FormWindowState.Minimized)
			{
				devMonitorForm.WindowState = FormWindowState.Normal;
			}
			devMonitorForm.BringToFront();
		}
		else
		{
			devMonitorForm.Show();
		}
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		lbFileNameV.Text = (instrument.tmrFileName = CMS_InfoParasFMT.FmtStr(2, instrument.runningInjInfo, instrument));
		dataAcqForm.SetDrawName(lbFileNameV.Text);
		string text = ((!instrument.sampling) ? "" : (instrument.sample_time.ToString("0.00") + " min"));
		if (!text.Equals(lbTimeV.Text))
		{
			lbTimeV.Text = text;
		}
	}

	protected override void WndProc(ref Message message_0)
	{
		if (message_0.Msg == 1027)
		{
			RefreshInfo(InjectStyle.Sequence);
		}
		else if (message_0.Msg == 1028)
		{
			bool enabled = (int)message_0.WParam == 1;
			dataAcqForm.Set3Buttons(enabled);
			ssAlyForm.Set3Buttons(enabled);
			seqAlyForm.Set3Buttons(enabled);
		}
		else
		{
			base.WndProc(ref message_0);
		}
	}

	private void panel1_Click(object sender, EventArgs e)
	{
		if (panel1.BackColor == Color.Red)
		{
			panel1.BackColor = Color.RosyBrown;
		}
		else
		{
			panel1.BackColor = Color.Red;
		}
		instrument.runningInjInfo.openChromWin = !instrument.runningInjInfo.openChromWin;
	}

	private void panel2_Click(object sender, EventArgs e)
	{
		if (panel2.BackColor == Color.Green)
		{
			panel2.BackColor = Color.Gold;
		}
		else
		{
			panel2.BackColor = Color.Green;
		}
		instrument.runningInjInfo.openCaliWin = !instrument.runningInjInfo.openCaliWin;
	}

	private void panel3_Click(object sender, EventArgs e)
	{
		if (panel3.BackColor == Color.Blue)
		{
			panel3.BackColor = Color.BlueViolet;
		}
		else
		{
			panel3.BackColor = Color.Blue;
		}
		instrument.runningInjInfo.openPrintWin = !instrument.runningInjInfo.openPrintWin;
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
		this.msInstru = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiPjtDir = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiNewMethod = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOpenMethod = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveMethod = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveMethodAs = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiReportSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiAutoOverW = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiRecoverData = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.tsInstru = new System.Windows.Forms.ToolStrip();
		this.btnPjtDir = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.btnNewMethod = new System.Windows.Forms.ToolStripButton();
		this.btnOpenMethod = new System.Windows.Forms.ToolStripButton();
		this.btnSaveMethod = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.btnOptions = new System.Windows.Forms.ToolStripButton();
		this.ssInstru = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssInstruStyle = new System.Windows.Forms.ToolStripStatusLabel();
		this.dpInstru = new IBrainChrom2018.LclDisplayPanel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pbLamp = new IBrainChrom2018.LclPictureBox();
		this.pbPump1 = new IBrainChrom2018.LclPictureBox();
		this.pbPump0 = new IBrainChrom2018.LclPictureBox();
		this.lbStateV = new IBrainChrom2018.LclLabel();
		this.lbModeV = new IBrainChrom2018.LclLabel();
		this.lbTimeV = new IBrainChrom2018.LclLabel();
		this.lbMode = new IBrainChrom2018.LclLabel();
		this.lbMethodV = new IBrainChrom2018.LclLabel();
		this.lbMethod = new IBrainChrom2018.LclLabel();
		this.lbSampleIDV = new IBrainChrom2018.LclLabel();
		this.lbSampleV = new IBrainChrom2018.LclLabel();
		this.lbSampleID = new IBrainChrom2018.LclLabel();
		this.lbFileNameV = new IBrainChrom2018.LclLabel();
		this.lbSample = new IBrainChrom2018.LclLabel();
		this.lbFileName = new IBrainChrom2018.LclLabel();
		this.btnLampZero = new IBrainChrom2018.LclInstruButton();
		this.btnSequence = new IBrainChrom2018.LclInstruButton();
		this.btnReportSetup = new IBrainChrom2018.LclInstruButton();
		this.btnCaliWindow = new IBrainChrom2018.LclInstruButton();
		this.btnChromWindow = new IBrainChrom2018.LclInstruButton();
		this.btnMtdHard = new IBrainChrom2018.LclInstruButton();
		this.btnIntegration = new IBrainChrom2018.LclInstruButton();
		this.btnDataAcquisition = new IBrainChrom2018.LclInstruButton();
		this.btnDM4 = new IBrainChrom2018.LclInstruButton();
		this.btnDM2 = new IBrainChrom2018.LclInstruButton();
		this.btnDM3 = new IBrainChrom2018.LclInstruButton();
		this.btnDeviceMonitor = new IBrainChrom2018.LclInstruButton();
		this.btnSingle = new IBrainChrom2018.LclInstruButton();
		this.timer_0 = new System.Windows.Forms.Timer(this.components);
		this.msInstru.SuspendLayout();
		this.tsInstru.SuspendLayout();
		this.ssInstru.SuspendLayout();
		this.dpInstru.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbLamp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbPump1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbPump0).BeginInit();
		base.SuspendLayout();
		this.msInstru.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.miFile });
		this.msInstru.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
		this.msInstru.Location = new System.Drawing.Point(0, 0);
		this.msInstru.Name = "msInstru";
		this.msInstru.Size = new System.Drawing.Size(680, 25);
		this.msInstru.TabIndex = 0;
		this.msInstru.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.miFiPjtDir, this.toolStripSeparator1, this.miFiNewMethod, this.miFiOpenMethod, this.miFiSaveMethod, this.miFiSaveMethodAs, this.toolStripSeparator2, this.miFiReportSetup, this.toolStripSeparator4, this.miFiAutoOverW,
			this.miFiRecoverData, this.toolStripSeparator3, this.miFiExit
		});
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(44, 21);
		this.miFile.Text = "文件";
		this.miFiPjtDir.Name = "miFiPjtDir";
		this.miFiPjtDir.Size = new System.Drawing.Size(186, 22);
		this.miFiPjtDir.Text = "工程目录";
		this.miFiPjtDir.Click += new System.EventHandler(btnPjtDir_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
		this.miFiNewMethod.Name = "miFiNewMethod";
		this.miFiNewMethod.Size = new System.Drawing.Size(186, 22);
		this.miFiNewMethod.Text = "新建方法";
		this.miFiNewMethod.Click += new System.EventHandler(btnNewMethod_Click);
		this.miFiOpenMethod.Name = "miFiOpenMethod";
		this.miFiOpenMethod.Size = new System.Drawing.Size(186, 22);
		this.miFiOpenMethod.Text = "打开方法";
		this.miFiOpenMethod.Click += new System.EventHandler(lbMethodV_Click);
		this.miFiSaveMethod.Name = "miFiSaveMethod";
		this.miFiSaveMethod.Size = new System.Drawing.Size(186, 22);
		this.miFiSaveMethod.Text = "保存方法";
		this.miFiSaveMethod.Click += new System.EventHandler(btnSaveMethod_Click);
		this.miFiSaveMethodAs.Name = "miFiSaveMethodAs";
		this.miFiSaveMethodAs.Size = new System.Drawing.Size(186, 22);
		this.miFiSaveMethodAs.Text = "另存方法...";
		this.miFiSaveMethodAs.Click += new System.EventHandler(miFiSaveMethodAs_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
		this.miFiReportSetup.Name = "miFiReportSetup";
		this.miFiReportSetup.Size = new System.Drawing.Size(186, 22);
		this.miFiReportSetup.Text = "样式文件...";
		this.miFiReportSetup.Click += new System.EventHandler(miFiReportSetup_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(183, 6);
		this.miFiAutoOverW.Checked = true;
		this.miFiAutoOverW.CheckState = System.Windows.Forms.CheckState.Checked;
		this.miFiAutoOverW.Name = "miFiAutoOverW";
		this.miFiAutoOverW.Size = new System.Drawing.Size(186, 22);
		this.miFiAutoOverW.Text = "覆盖文件";
		this.miFiAutoOverW.Click += new System.EventHandler(miFiAutoOverW_Click);
		this.miFiRecoverData.Enabled = false;
		this.miFiRecoverData.Name = "miFiRecoverData";
		this.miFiRecoverData.Size = new System.Drawing.Size(186, 22);
		this.miFiRecoverData.Text = "数据恢复";
		this.miFiRecoverData.Visible = false;
		this.miFiRecoverData.Click += new System.EventHandler(miFiRecoverData_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(186, 22);
		this.miFiExit.Text = "退出";
		this.tsInstru.Dock = System.Windows.Forms.DockStyle.Left;
		this.tsInstru.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.btnPjtDir, this.toolStripSeparator5, this.btnNewMethod, this.btnOpenMethod, this.btnSaveMethod, this.toolStripSeparator6, this.btnOptions });
		this.tsInstru.Location = new System.Drawing.Point(0, 25);
		this.tsInstru.Name = "tsInstru";
		this.tsInstru.Size = new System.Drawing.Size(24, 334);
		this.tsInstru.TabIndex = 6;
		this.tsInstru.Text = "toolStrip1";
		this.btnPjtDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPjtDir.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPjtDir.Name = "btnPjtDir";
		this.btnPjtDir.Size = new System.Drawing.Size(21, 4);
		this.btnPjtDir.Click += new System.EventHandler(btnPjtDir_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(21, 6);
		this.btnNewMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNewMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNewMethod.Name = "btnNewMethod";
		this.btnNewMethod.Size = new System.Drawing.Size(21, 4);
		this.btnNewMethod.Click += new System.EventHandler(btnNewMethod_Click);
		this.btnOpenMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpenMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpenMethod.Name = "btnOpenMethod";
		this.btnOpenMethod.Size = new System.Drawing.Size(21, 4);
		this.btnOpenMethod.Click += new System.EventHandler(lbMethodV_Click);
		this.btnSaveMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSaveMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSaveMethod.Name = "btnSaveMethod";
		this.btnSaveMethod.Size = new System.Drawing.Size(21, 4);
		this.btnSaveMethod.Click += new System.EventHandler(btnSaveMethod_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(21, 6);
		this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOptions.Name = "btnOptions";
		this.btnOptions.Size = new System.Drawing.Size(21, 4);
		this.btnOptions.Click += new System.EventHandler(toolStripMenuItem_0_Click);
		this.ssInstru.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.slbExplain, this.tssInstruStyle });
		this.ssInstru.Location = new System.Drawing.Point(24, 337);
		this.ssInstru.Name = "ssInstru";
		this.ssInstru.Size = new System.Drawing.Size(656, 22);
		this.ssInstru.SizingGrip = false;
		this.ssInstru.TabIndex = 7;
		this.ssInstru.Text = "statusStrip1";
		this.slbExplain.AutoSize = false;
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(140, 17);
		this.slbExplain.Text = "帮助，按F1";
		this.slbExplain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tssInstruStyle.Name = "tssInstruStyle";
		this.tssInstruStyle.Size = new System.Drawing.Size(131, 17);
		this.tssInstruStyle.Text = "[气相]";
		this.dpInstru.BackColor = System.Drawing.Color.White;
		this.dpInstru.Controls.Add(this.panel3);
		this.dpInstru.Controls.Add(this.panel2);
		this.dpInstru.Controls.Add(this.panel1);
		this.dpInstru.Controls.Add(this.pbLamp);
		this.dpInstru.Controls.Add(this.pbPump1);
		this.dpInstru.Controls.Add(this.pbPump0);
		this.dpInstru.Controls.Add(this.lbStateV);
		this.dpInstru.Controls.Add(this.lbModeV);
		this.dpInstru.Controls.Add(this.lbTimeV);
		this.dpInstru.Controls.Add(this.lbMode);
		this.dpInstru.Controls.Add(this.lbMethodV);
		this.dpInstru.Controls.Add(this.lbMethod);
		this.dpInstru.Controls.Add(this.lbSampleIDV);
		this.dpInstru.Controls.Add(this.lbSampleV);
		this.dpInstru.Controls.Add(this.lbSampleID);
		this.dpInstru.Controls.Add(this.lbFileNameV);
		this.dpInstru.Controls.Add(this.lbSample);
		this.dpInstru.Controls.Add(this.lbFileName);
		this.dpInstru.Controls.Add(this.btnLampZero);
		this.dpInstru.Controls.Add(this.btnSequence);
		this.dpInstru.Controls.Add(this.btnReportSetup);
		this.dpInstru.Controls.Add(this.btnCaliWindow);
		this.dpInstru.Controls.Add(this.btnChromWindow);
		this.dpInstru.Controls.Add(this.btnMtdHard);
		this.dpInstru.Controls.Add(this.btnIntegration);
		this.dpInstru.Controls.Add(this.btnDataAcquisition);
		this.dpInstru.Controls.Add(this.btnDM4);
		this.dpInstru.Controls.Add(this.btnDM2);
		this.dpInstru.Controls.Add(this.btnDM3);
		this.dpInstru.Controls.Add(this.btnDeviceMonitor);
		this.dpInstru.Controls.Add(this.btnSingle);
		this.dpInstru.Location = new System.Drawing.Point(38, 28);
		this.dpInstru.Name = "dpInstru";
		this.dpInstru.Size = new System.Drawing.Size(610, 288);
		this.dpInstru.TabIndex = 20;
		this.dpInstru.Paint += new System.Windows.Forms.PaintEventHandler(dpInstru_Paint);
		this.dpInstru.MouseDown += new System.Windows.Forms.MouseEventHandler(dpInstru_MouseDown);
		this.dpInstru.MouseMove += new System.Windows.Forms.MouseEventHandler(dpInstru_MouseMove);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel3.Location = new System.Drawing.Point(429, 203);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(80, 40);
		this.panel3.TabIndex = 23;
		this.panel3.Click += new System.EventHandler(panel3_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel2.Location = new System.Drawing.Point(429, 157);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(80, 40);
		this.panel2.TabIndex = 23;
		this.panel2.Click += new System.EventHandler(panel2_Click);
		this.panel1.BackColor = System.Drawing.Color.Red;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel1.Location = new System.Drawing.Point(429, 102);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(80, 40);
		this.panel1.TabIndex = 23;
		this.panel1.Click += new System.EventHandler(panel1_Click);
		this.pbLamp.BackColor = System.Drawing.Color.White;
		this.pbLamp.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pbLamp.Location = new System.Drawing.Point(344, 125);
		this.pbLamp.Name = "pbLamp";
		this.pbLamp.Size = new System.Drawing.Size(50, 24);
		this.pbLamp.TabIndex = 22;
		this.pbLamp.TabStop = false;
		this.pbLamp.Click += new System.EventHandler(pbLamp_Click);
		this.pbPump1.BackColor = System.Drawing.Color.White;
		this.pbPump1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pbPump1.Location = new System.Drawing.Point(344, 74);
		this.pbPump1.Name = "pbPump1";
		this.pbPump1.Size = new System.Drawing.Size(48, 24);
		this.pbPump1.TabIndex = 22;
		this.pbPump1.TabStop = false;
		this.pbPump1.Tag = "1";
		this.pbPump1.Click += new System.EventHandler(pbPump0_Click);
		this.pbPump0.BackColor = System.Drawing.Color.White;
		this.pbPump0.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pbPump0.Location = new System.Drawing.Point(344, 235);
		this.pbPump0.Name = "pbPump0";
		this.pbPump0.Size = new System.Drawing.Size(48, 24);
		this.pbPump0.TabIndex = 22;
		this.pbPump0.TabStop = false;
		this.pbPump0.Tag = "1";
		this.pbPump0.Click += new System.EventHandler(pbPump0_Click);
		this.lbStateV.BackColor = System.Drawing.Color.Transparent;
		this.lbStateV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbStateV.Location = new System.Drawing.Point(37, 215);
		this.lbStateV.Name = "lbStateV";
		this.lbStateV.Size = new System.Drawing.Size(215, 26);
		this.lbStateV.TabIndex = 21;
		this.lbStateV.Text = "运行";
		this.lbModeV.BackColor = System.Drawing.Color.Transparent;
		this.lbModeV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbModeV.Location = new System.Drawing.Point(204, 59);
		this.lbModeV.Name = "lbModeV";
		this.lbModeV.Size = new System.Drawing.Size(57, 15);
		this.lbModeV.TabIndex = 21;
		this.lbModeV.Text = "单针运行";
		this.lbTimeV.BackColor = System.Drawing.Color.Transparent;
		this.lbTimeV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbTimeV.Location = new System.Drawing.Point(49, 182);
		this.lbTimeV.Name = "lbTimeV";
		this.lbTimeV.Size = new System.Drawing.Size(57, 15);
		this.lbTimeV.TabIndex = 21;
		this.lbTimeV.Text = "lclLabel1";
		this.lbTimeV.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lbMode.AutoSize = true;
		this.lbMode.BackColor = System.Drawing.Color.Transparent;
		this.lbMode.Font = new System.Drawing.Font("Arial", 9f);
		this.lbMode.Location = new System.Drawing.Point(139, 59);
		this.lbMode.Name = "lbMode";
		this.lbMode.Size = new System.Drawing.Size(57, 15);
		this.lbMode.TabIndex = 21;
		this.lbMode.Text = "模式:";
		this.lbMethodV.BackColor = System.Drawing.Color.Transparent;
		this.lbMethodV.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbMethodV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbMethodV.Location = new System.Drawing.Point(204, 47);
		this.lbMethodV.Name = "lbMethodV";
		this.lbMethodV.Size = new System.Drawing.Size(57, 15);
		this.lbMethodV.TabIndex = 21;
		this.lbMethodV.Text = "lclLabel1";
		this.lbMethodV.Click += new System.EventHandler(lbMethodV_Click);
		this.lbMethod.AutoSize = true;
		this.lbMethod.BackColor = System.Drawing.Color.Transparent;
		this.lbMethod.Font = new System.Drawing.Font("Arial", 9f);
		this.lbMethod.Location = new System.Drawing.Point(139, 47);
		this.lbMethod.Name = "lbMethod";
		this.lbMethod.Size = new System.Drawing.Size(57, 15);
		this.lbMethod.TabIndex = 21;
		this.lbMethod.Text = "方法:";
		this.lbSampleIDV.BackColor = System.Drawing.Color.Transparent;
		this.lbSampleIDV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbSampleIDV.Location = new System.Drawing.Point(204, 34);
		this.lbSampleIDV.Name = "lbSampleIDV";
		this.lbSampleIDV.Size = new System.Drawing.Size(57, 15);
		this.lbSampleIDV.TabIndex = 21;
		this.lbSampleIDV.Text = "lclLabel1";
		this.lbSampleV.BackColor = System.Drawing.Color.Transparent;
		this.lbSampleV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbSampleV.Location = new System.Drawing.Point(204, 23);
		this.lbSampleV.Name = "lbSampleV";
		this.lbSampleV.Size = new System.Drawing.Size(57, 15);
		this.lbSampleV.TabIndex = 21;
		this.lbSampleV.Text = "lclLabel1";
		this.lbSampleID.AutoSize = true;
		this.lbSampleID.BackColor = System.Drawing.Color.Transparent;
		this.lbSampleID.Font = new System.Drawing.Font("Arial", 9f);
		this.lbSampleID.Location = new System.Drawing.Point(139, 34);
		this.lbSampleID.Name = "lbSampleID";
		this.lbSampleID.Size = new System.Drawing.Size(46, 15);
		this.lbSampleID.TabIndex = 21;
		this.lbSampleID.Text = "样品ID:";
		this.lbFileNameV.AutoEllipsis = true;
		this.lbFileNameV.BackColor = System.Drawing.Color.Transparent;
		this.lbFileNameV.Font = new System.Drawing.Font("Arial", 9f);
		this.lbFileNameV.Location = new System.Drawing.Point(204, 11);
		this.lbFileNameV.Name = "lbFileNameV";
		this.lbFileNameV.Size = new System.Drawing.Size(57, 15);
		this.lbFileNameV.TabIndex = 21;
		this.lbFileNameV.Text = "lclLabel1";
		this.lbSample.AutoSize = true;
		this.lbSample.BackColor = System.Drawing.Color.Transparent;
		this.lbSample.Font = new System.Drawing.Font("Arial", 9f);
		this.lbSample.Location = new System.Drawing.Point(139, 23);
		this.lbSample.Name = "lbSample";
		this.lbSample.Size = new System.Drawing.Size(34, 15);
		this.lbSample.TabIndex = 21;
		this.lbSample.Text = "样品:";
		this.lbFileName.AutoSize = true;
		this.lbFileName.BackColor = System.Drawing.Color.Transparent;
		this.lbFileName.Font = new System.Drawing.Font("Arial", 9f);
		this.lbFileName.Location = new System.Drawing.Point(139, 11);
		this.lbFileName.Name = "lbFileName";
		this.lbFileName.Size = new System.Drawing.Size(43, 15);
		this.lbFileName.TabIndex = 21;
		this.lbFileName.Text = "文件名";
		this.btnLampZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnLampZero.Location = new System.Drawing.Point(127, 244);
		this.btnLampZero.Name = "btnLampZero";
		this.btnLampZero.Size = new System.Drawing.Size(65, 15);
		this.btnLampZero.TabIndex = 20;
		this.btnLampZero.Text = "自动归零";
		this.btnLampZero.UseVisualStyleBackColor = true;
		this.btnLampZero.Click += new System.EventHandler(btnLampZero_Click);
		this.btnSequence.Location = new System.Drawing.Point(78, 21);
		this.btnSequence.Name = "btnSequence";
		this.btnSequence.Size = new System.Drawing.Size(40, 39);
		this.btnSequence.TabIndex = 20;
		this.btnSequence.Text = "lclInstruButton1";
		this.btnSequence.UseVisualStyleBackColor = true;
		this.btnSequence.Click += new System.EventHandler(btnSequence_Click);
		this.btnReportSetup.Location = new System.Drawing.Point(244, 155);
		this.btnReportSetup.Name = "btnReportSetup";
		this.btnReportSetup.Size = new System.Drawing.Size(38, 29);
		this.btnReportSetup.TabIndex = 20;
		this.btnReportSetup.Text = "lclInstruButton1";
		this.btnReportSetup.UseVisualStyleBackColor = true;
		this.btnReportSetup.Click += new System.EventHandler(btnReportSetup_Click);
		this.btnCaliWindow.Location = new System.Drawing.Point(244, 120);
		this.btnCaliWindow.Name = "btnCaliWindow";
		this.btnCaliWindow.Size = new System.Drawing.Size(38, 29);
		this.btnCaliWindow.TabIndex = 20;
		this.btnCaliWindow.Text = "lclInstruButton1";
		this.btnCaliWindow.UseVisualStyleBackColor = true;
		this.btnCaliWindow.Click += new System.EventHandler(btnCaliWindow_Click);
		this.btnChromWindow.Location = new System.Drawing.Point(244, 85);
		this.btnChromWindow.Name = "btnChromWindow";
		this.btnChromWindow.Size = new System.Drawing.Size(38, 29);
		this.btnChromWindow.TabIndex = 20;
		this.btnChromWindow.Text = "lclInstruButton1";
		this.btnChromWindow.UseVisualStyleBackColor = true;
		this.btnChromWindow.Click += new System.EventHandler(btnChromWindow_Click);
		this.btnMtdHard.Location = new System.Drawing.Point(112, 120);
		this.btnMtdHard.Name = "btnMtdHard";
		this.btnMtdHard.Size = new System.Drawing.Size(38, 29);
		this.btnMtdHard.TabIndex = 20;
		this.btnMtdHard.Text = "lclInstruButton1";
		this.btnMtdHard.UseVisualStyleBackColor = true;
		this.btnMtdHard.Click += new System.EventHandler(btnMtdHard_Click);
		this.btnIntegration.Location = new System.Drawing.Point(198, 120);
		this.btnIntegration.Name = "btnIntegration";
		this.btnIntegration.Size = new System.Drawing.Size(38, 29);
		this.btnIntegration.TabIndex = 20;
		this.btnIntegration.Text = "lclInstruButton1";
		this.btnIntegration.UseVisualStyleBackColor = true;
		this.btnIntegration.Click += new System.EventHandler(btnIntegration_Click);
		this.btnDataAcquisition.Location = new System.Drawing.Point(154, 120);
		this.btnDataAcquisition.Name = "btnDataAcquisition";
		this.btnDataAcquisition.Size = new System.Drawing.Size(38, 29);
		this.btnDataAcquisition.TabIndex = 20;
		this.btnDataAcquisition.Text = "lclInstruButton1";
		this.btnDataAcquisition.UseVisualStyleBackColor = true;
		this.btnDataAcquisition.Click += new System.EventHandler(btnDataAcquisition_Click);
		this.btnDM4.Location = new System.Drawing.Point(68, 136);
		this.btnDM4.Name = "btnDM4";
		this.btnDM4.Size = new System.Drawing.Size(38, 29);
		this.btnDM4.TabIndex = 20;
		this.btnDM4.Text = "lclInstruButton1";
		this.btnDM4.UseVisualStyleBackColor = true;
		this.btnDM4.Click += new System.EventHandler(btnDM4_Click);
		this.btnDM2.Location = new System.Drawing.Point(68, 102);
		this.btnDM2.Name = "btnDM2";
		this.btnDM2.Size = new System.Drawing.Size(38, 28);
		this.btnDM2.TabIndex = 20;
		this.btnDM2.Text = "lclInstruButton1";
		this.btnDM2.UseVisualStyleBackColor = true;
		this.btnDM2.Click += new System.EventHandler(btnDM2_Click);
		this.btnDM3.Location = new System.Drawing.Point(24, 136);
		this.btnDM3.Name = "btnDM3";
		this.btnDM3.Size = new System.Drawing.Size(38, 29);
		this.btnDM3.TabIndex = 20;
		this.btnDM3.Text = "lclInstruButton1";
		this.btnDM3.UseVisualStyleBackColor = true;
		this.btnDM3.Click += new System.EventHandler(btnDM3_Click);
		this.btnDeviceMonitor.Location = new System.Drawing.Point(24, 102);
		this.btnDeviceMonitor.Name = "btnDeviceMonitor";
		this.btnDeviceMonitor.Size = new System.Drawing.Size(38, 28);
		this.btnDeviceMonitor.TabIndex = 20;
		this.btnDeviceMonitor.Text = "lclInstruButton1";
		this.btnDeviceMonitor.UseVisualStyleBackColor = true;
		this.btnDeviceMonitor.Click += new System.EventHandler(btnDeviceMonitor_Click);
		this.btnSingle.Location = new System.Drawing.Point(21, 21);
		this.btnSingle.Name = "btnSingle";
		this.btnSingle.Size = new System.Drawing.Size(41, 35);
		this.btnSingle.TabIndex = 20;
		this.btnSingle.Text = "lclInstruButton1";
		this.btnSingle.UseVisualStyleBackColor = true;
		this.btnSingle.Click += new System.EventHandler(btnSingle_Click);
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		base.ClientSize = new System.Drawing.Size(680, 359);
		base.Controls.Add(this.dpInstru);
		base.Controls.Add(this.ssInstru);
		base.Controls.Add(this.tsInstru);
		base.Controls.Add(this.msInstru);
		base.MainMenuStrip = this.msInstru;
		base.MaximizeBox = false;
		base.Name = "InstrumentForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "气相/液相工程";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(InstrumentForm_FormClosing);
		base.Load += new System.EventHandler(InstrumentForm_Load);
		base.VisibleChanged += new System.EventHandler(InstrumentForm_VisibleChanged);
		this.msInstru.ResumeLayout(false);
		this.msInstru.PerformLayout();
		this.tsInstru.ResumeLayout(false);
		this.tsInstru.PerformLayout();
		this.ssInstru.ResumeLayout(false);
		this.ssInstru.PerformLayout();
		this.dpInstru.ResumeLayout(false);
		this.dpInstru.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbLamp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbPump1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbPump0).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
