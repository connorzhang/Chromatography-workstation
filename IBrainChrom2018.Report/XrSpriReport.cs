using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using IBrainChrom2018._205.Report.instockListTableAdapters;
using IBrainChrom2018.ReportMgr;

namespace IBrainChrom2018.Report;

public class XrSpriReport : XtraReport
{
	public static int iPicWidth = 770;

	public static int iPicHeight = 250;

	public instockList stockDataSet = null;

	private IContainer components = null;

	private DetailBand Detail;

	private instockList instockList1;

	private GroupFooterBand groupFooterBand1;

	private XRTable xrtList;

	private XRTableRow xrTableRow4;

	private XRTableCell xrtcL0;

	private XRTableCell xrtcL1;

	private XRTableCell xrtcL4;

	private XRTableCell xrtcL5;

	private XRTableCell xrtcL6;

	private XRTableCell xrtcL7;

	private XRTableCell xrtcL8;

	private PageHeaderBand pageHeaderBand1;

	private XRTable xrtHead;

	private XRTableRow xrTableRow3;

	private XRTableCell xrtcH0;

	private XRTableCell xrtcH1;

	private XRTableCell xrtcH2;

	private XRTableCell xrtcH3;

	private XRTableCell xrtcH4;

	private XRTableCell xrtcH5;

	private XRTableCell xrtcH6;

	private XRTableCell xrtcH7;

	private XRTableCell xrtcH8;

	private XRTableRow xrTableRow1;

	private XRTableCell xrTableCell1;

	private XRTableCell xrTableCell2;

	private XRTableCell xrTableCell3;

	private XRTableRow xrTableRow2;

	private XRTableCell xrTableCell4;

	private XRTableCell xrTableCell5;

	private XRTableCell xrTableCell6;

	private PageFooterBand pageFooterBand1;

	private XRPageInfo xrPageInfo1;

	private XRPageInfo xrPageInfo2;

	private ReportHeaderBand reportHeaderBand1;

	private XRControlStyle Title;

	private XRControlStyle FieldCaption;

	private XRControlStyle PageInfo;

	private XRControlStyle DataField;

	private TopMarginBand topMarginBand1;

	private BottomMarginBand bottomMarginBand1;

	private FormattingRule formattingRule1;

	private XRControlStyle xrControlStyle1;

	private XRTableCell xrtcL2;

	private XRTableCell xrtcL3;

	private storgeOrderAdapter poOrderListReportBomTableAdapter1;

	private XRTableCell xrtcH9;

	private XRTableCell xrtcH10;

	private XRTableCell xrtcH11;

	private XRTableCell xrtcL9;

	private XRTableCell xrtcL10;

	private XRTableCell xrtcL11;

	private ReportFooterBand ReportFooter;

	public XRLabel xrlCaption;

	public XRLabel xrlFoot;

	private FormattingRule fRulePeakAmont;

	private XRTable xrTable1;

	private XRTableRow xrTableRow6;

	private XRTableCell xrTableCell7;

	private XRTableCell xrTableCell12;

	public XRTableCell xrTableCell8;

	private XRTableCell xrTableCell11;

	private XRTableCell xrTableCell9;

	private XRTableCell xrTableCell13;

	private XRTableCell xrTableCell10;

	private XRTableCell xrTableCell14;

	public XRLabel xrLabel1;

	private instockList instockList2;

	private instockList instockList3;

	private instockList instockList4;

	private storgeOrderAdapter storgeOrderAdapter;

	public XrSpriReport()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	public void SetOrderType(Chromatogram chromatogram, Image imgChrom, string strFileName)
	{
		PrintPara pPara = chromatogram.PPara;
		xrlCaption.Text = pPara.Title;
		int num = iPicWidth;
		float num2 = 48.6f;
		int num3 = 17;
		xrtcH4.Text = Lang.PS("浓度") + "[" + chromatogram.AmountUnit + "]";
	}

	public static XrAnysReport CreateReport(Chromatogram chromatogram, Image imgChrom, string strFileName)
	{
		PrintPara pPara = chromatogram.PPara;
		XrAnysReport xrAnysReport = new XrAnysReport();
		SizeF sizeF = xrAnysReport.xrPictureBox.SizeF;
		Size size = new Size((int)sizeF.Width, (int)sizeF.Height);
		instockList instockList2 = new instockList();
		Peak[] rltPeaks = chromatogram.GetRltPeaks(combine: false);
		for (int i = 0; i < rltPeaks.Length; i++)
		{
			Peak peak = rltPeaks[i];
			string peakFx = "";
			double num = 0.0;
			float num2 = 0f;
			if (peak.compound != null)
			{
				num2 = peak.compound.cmpdInfo.freeRespFactor;
				if (chromatogram.chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					peakFx = peak.compound.eFunc.GetEquationStr();
					num = peak.compound.eFunc.corrFactor;
				}
				else if (chromatogram.chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					peakFx = peak.compound.iFunc.GetEquationStr();
					num = peak.compound.iFunc.corrFactor;
				}
				if (double.IsNaN(num))
				{
					num = 0.0;
				}
			}
			if (float.IsNaN(peak.Resolution_EP))
			{
				peak.Resolution_EP = 0f;
			}
			if (float.IsNaN(peak.Eff_Column_EP))
			{
				peak.Eff_Column_EP = 0f;
			}
			if (float.IsNaN(peak.Capacity))
			{
				peak.Capacity = 0f;
			}
			if (float.IsNaN(peak.SymmetryTailing))
			{
				peak.SymmetryTailing = 0f;
			}
			if (peak.amount < 0f)
			{
				peak.amount = 0f;
			}
			if (peak.amountPer < 0f)
			{
				peak.amountPer = 0f;
			}
			if (peak.area < 0f)
			{
				peak.area = 0f;
			}
			if (peak.areaPer < 0f)
			{
				peak.areaPer = 0f;
			}
			if (peak.height < 0f)
			{
				peak.height = 0f;
			}
			if (peak.heightPer < 0f)
			{
				peak.heightPer = 0f;
			}
			if (peak.WO5 < 0f)
			{
				peak.WO5 = 0f;
			}
			if (peak.pkStyle < PeakStyle.None)
			{
				peak.pkStyle = PeakStyle.None;
			}
			if (num < 0.0)
			{
				num = 0.0;
			}
			if (peak.Resolution_EP < 0f)
			{
				peak.Resolution_EP = 0f;
			}
			if (peak.Eff_Column_EP < 0f)
			{
				peak.Eff_Column_EP = 0f;
			}
			if (peak.Capacity < 0f)
			{
				peak.Capacity = 0f;
			}
			if (peak.SymmetryTailing < 0f)
			{
				peak.SymmetryTailing = 0f;
			}
			instockList2.zufenTable.AddzufenTableRow(i, (decimal)peak.pkRT, peak.name, (decimal)num2, (decimal)peak.amount, (decimal)(peak.amountPer * 100f), (decimal)peak.area, (decimal)(peak.areaPer * 100f), (decimal)peak.height, (decimal)(peak.heightPer * 100f), (decimal)peak.WO5, (int)peak.pkStyle, (decimal)num, (decimal)peak.Resolution_EP, 0m, (decimal)peak.Eff_Column_EP, (decimal)peak.Capacity, (decimal)peak.SymmetryTailing, peakFx);
		}
		xrAnysReport.SetOrderType(chromatogram, imgChrom, strFileName);
		xrAnysReport.DataAdapter = null;
		xrAnysReport.DataSource = instockList2;
		xrAnysReport.DataMember = "zufenTable";
		xrAnysReport.stockDataSet = instockList2;
		return xrAnysReport;
	}

	public static XrAnysReport CreateReport(Chromatogram chromatogram, string strFileName)
	{
		int setChromNo = 0;
		Size size = new Size(iPicWidth, iPicHeight);
		RectangleF rectangleF = new RectangleF(0f, 0f, size.Width, size.Height);
		Image image = new Bitmap(size.Width, size.Height);
		Graphics graphics = Graphics.FromImage(image);
		ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
		chromDisplay.LinkDisChroms(new Chromatogram[1] { chromatogram }, ref setChromNo);
		chromDisplay.showMouseLgValue = false;
		chromDisplay.showProgTemp = false;
		chromDisplay.ShowBgChrom = true;
		chromDisplay.setShowGrid = true;
		chromDisplay.rcPage = rectangleF;
		chromDisplay.dskRC = rectangleF;
		chromDisplay.Draw(graphics, erase: true);
		graphics.Dispose();
		return CreateReport(chromatogram, image, strFileName);
	}

	public static void OpenReportForm(Chromatogram chromatogram, string strFileName)
	{
		List<XtraReport> list = new List<XtraReport>();
		XrAnysReport xrAnysReport = CreateReport(chromatogram, strFileName);
		if (ReportPreviewForm.form == null)
		{
			ReportPreviewForm.form = new ReportPreviewForm();
		}
		if (chromatogram.PPara.BPeakFx && chromatogram.caliGnl != null)
		{
			XrCmpdReport item = XrCmpdReport.CreateReport(chromatogram, strFileName, xrAnysReport.stockDataSet);
			list.Add(xrAnysReport);
			list.Add(item);
			ReportPreviewForm.form.ShowReportMain(list.ToArray());
		}
		else
		{
			ReportPreviewForm.form.ShowReportMain(xrAnysReport);
		}
	}

	public static void ExportReport(Chromatogram chromatogram, string strFileName)
	{
		string text = Path.GetFileNameWithoutExtension(strFileName) + ".xls";
		string directoryName = Path.GetDirectoryName(strFileName);
		XrAnysReport xrAnysReport = CreateReport(chromatogram, strFileName);
		xrAnysReport.CreateDocument();
		xrAnysReport.ExportToXls(directoryName + "\\" + text);
		xrAnysReport.Dispose();
		xrAnysReport = null;
	}

	private void InitializeComponent2()
	{
		xrtcH3.Text = Lang.PS("校正因子:", "calibration Factor :");
		xrtcH4.Text = Lang.PS(" 浓度:", " Amount:");
		xrtcH5.Text = Lang.PS(" 峰面积:", " PeakArea:");
		xrtcH6.Text = Lang.PS("峰高:", " PeakHight:");
		xrtcH7.Text = Lang.PS("半峰宽");
		xrtcH8.Text = Lang.PS("浓度百分比:", "PeakAreaPer:");
		xrtcH9.Text = Lang.PS("峰面积百分比:", "PeakAmontPer:");
		xrtcH10.Text = Lang.PS("峰高百分比:", " PeakHightPer:");
		xrtcH11.Text = Lang.PS("峰分离度");
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
		Detail = new DetailBand();
		xrtList = new XRTable();
		xrTableRow4 = new XRTableRow();
		xrtcL0 = new XRTableCell();
		xrtcL1 = new XRTableCell();
		xrtcL2 = new XRTableCell();
		xrtcL3 = new XRTableCell();
		xrtcL4 = new XRTableCell();
		xrtcL5 = new XRTableCell();
		xrtcL6 = new XRTableCell();
		xrtcL7 = new XRTableCell();
		xrtcL8 = new XRTableCell();
		xrtcL9 = new XRTableCell();
		xrtcL10 = new XRTableCell();
		xrtcL11 = new XRTableCell();
		fRulePeakAmont = new FormattingRule();
		instockList1 = new instockList();
		groupFooterBand1 = new GroupFooterBand();
		pageHeaderBand1 = new PageHeaderBand();
		xrtHead = new XRTable();
		xrTableRow3 = new XRTableRow();
		xrtcH0 = new XRTableCell();
		xrtcH1 = new XRTableCell();
		xrtcH2 = new XRTableCell();
		xrtcH3 = new XRTableCell();
		xrtcH4 = new XRTableCell();
		xrtcH5 = new XRTableCell();
		xrtcH6 = new XRTableCell();
		xrtcH7 = new XRTableCell();
		xrtcH8 = new XRTableCell();
		xrtcH9 = new XRTableCell();
		xrtcH10 = new XRTableCell();
		xrtcH11 = new XRTableCell();
		xrTableRow1 = new XRTableRow();
		xrTableCell1 = new XRTableCell();
		xrTableCell2 = new XRTableCell();
		xrTableCell3 = new XRTableCell();
		xrTableRow2 = new XRTableRow();
		xrTableCell4 = new XRTableCell();
		xrTableCell5 = new XRTableCell();
		xrTableCell6 = new XRTableCell();
		pageFooterBand1 = new PageFooterBand();
		xrPageInfo1 = new XRPageInfo();
		xrPageInfo2 = new XRPageInfo();
		xrLabel1 = new XRLabel();
		reportHeaderBand1 = new ReportHeaderBand();
		xrlCaption = new XRLabel();
		Title = new XRControlStyle();
		FieldCaption = new XRControlStyle();
		PageInfo = new XRControlStyle();
		DataField = new XRControlStyle();
		topMarginBand1 = new TopMarginBand();
		bottomMarginBand1 = new BottomMarginBand();
		formattingRule1 = new FormattingRule();
		xrControlStyle1 = new XRControlStyle();
		poOrderListReportBomTableAdapter1 = new storgeOrderAdapter();
		ReportFooter = new ReportFooterBand();
		xrTable1 = new XRTable();
		xrTableRow6 = new XRTableRow();
		xrTableCell7 = new XRTableCell();
		xrTableCell12 = new XRTableCell();
		xrTableCell8 = new XRTableCell();
		xrTableCell11 = new XRTableCell();
		xrTableCell9 = new XRTableCell();
		xrTableCell13 = new XRTableCell();
		xrTableCell10 = new XRTableCell();
		xrTableCell14 = new XRTableCell();
		xrlFoot = new XRLabel();
		instockList2 = new instockList();
		instockList3 = new instockList();
		instockList4 = new instockList();
		storgeOrderAdapter = new storgeOrderAdapter();
		((ISupportInitialize)xrtList).BeginInit();
		((ISupportInitialize)instockList1).BeginInit();
		((ISupportInitialize)xrtHead).BeginInit();
		((ISupportInitialize)xrTable1).BeginInit();
		((ISupportInitialize)instockList2).BeginInit();
		((ISupportInitialize)instockList3).BeginInit();
		((ISupportInitialize)instockList4).BeginInit();
		((ISupportInitialize)this).BeginInit();
		Detail.Borders = BorderSide.Left | BorderSide.Right;
		Detail.Controls.AddRange(new XRControl[1] { xrtList });
		Detail.Font = new Font("SimSun", 10f);
		Detail.HeightF = 23f;
		Detail.KeepTogether = true;
		Detail.Name = "Detail";
		Detail.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		Detail.TextAlignment = TextAlignment.MiddleCenter;
		xrtList.AnchorVertical = VerticalAnchorStyles.Both;
		xrtList.Borders = BorderSide.All;
		xrtList.LocationFloat = new PointFloat(0.9999809f, 0f);
		xrtList.Name = "xrtList";
		xrtList.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrtList.Rows.AddRange(new XRTableRow[1] { xrTableRow4 });
		xrtList.SizeF = new SizeF(770f, 23f);
		xrtList.StylePriority.UseBorderColor = false;
		xrtList.StylePriority.UseBorders = false;
		xrtList.StylePriority.UseFont = false;
		xrtList.StylePriority.UsePadding = false;
		xrtList.StylePriority.UseTextAlignment = false;
		xrTableRow4.Cells.AddRange(new XRTableCell[12]
		{
			xrtcL0, xrtcL1, xrtcL2, xrtcL3, xrtcL4, xrtcL5, xrtcL6, xrtcL7, xrtcL8, xrtcL9,
			xrtcL10, xrtcL11
		});
		xrTableRow4.Name = "xrTableRow4";
		xrTableRow4.Weight = 1.0;
		xrtcL0.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL0.CanGrow = false;
		xrtcL0.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "spreTable.ID")
		});
		xrtcL0.Name = "xrtcL0";
		xrtcL0.StyleName = "DataField";
		xrtcL0.StylePriority.UseBorders = false;
		xrtcL0.Weight = 14.435955231278;
		xrtcL1.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL1.CanGrow = false;
		xrtcL1.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "spreTable.location")
		});
		xrtcL1.Name = "xrtcL1";
		xrtcL1.StyleName = "DataField";
		xrtcL1.StylePriority.UseBorders = false;
		xrtcL1.Weight = 18.6199251445723;
		xrtcL2.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL2.CanGrow = false;
		xrtcL2.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "spreTable.indate", "{0:F4}")
		});
		xrtcL2.Name = "xrtcL2";
		xrtcL2.StylePriority.UseBorders = false;
		xrtcL2.Weight = 18.8085943080761;
		xrtcL3.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL3.CanGrow = false;
		xrtcL3.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "spreTable.O2", "{0:F4}")
		});
		xrtcL3.Name = "xrtcL3";
		xrtcL3.StylePriority.UseBorders = false;
		xrtcL3.Weight = 17.2461398901763;
		xrtcL4.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL4.CanGrow = false;
		xrtcL4.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakAmont", "{0:F4}")
		});
		xrtcL4.Name = "xrtcL4";
		xrtcL4.StyleName = "DataField";
		xrtcL4.StylePriority.UseBorders = false;
		xrtcL4.Weight = 16.3096613613176;
		xrtcL5.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL5.CanGrow = false;
		xrtcL5.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakArea", "{0:F4}")
		});
		xrtcL5.Name = "xrtcL5";
		xrtcL5.StyleName = "DataField";
		xrtcL5.StylePriority.UseBorders = false;
		xrtcL5.Weight = 16.9002484639485;
		xrtcL6.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL6.CanGrow = false;
		xrtcL6.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.Peakheight", "{0:F4}")
		});
		xrtcL6.Name = "xrtcL6";
		xrtcL6.StyleName = "DataField";
		xrtcL6.StylePriority.UseBorders = false;
		xrtcL6.Weight = 11.7618165475351;
		xrtcL7.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL7.CanGrow = false;
		xrtcL7.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakHalfheight", "{0:F4}")
		});
		xrtcL7.Name = "xrtcL7";
		xrtcL7.StyleName = "DataField";
		xrtcL7.StylePriority.UseBorders = false;
		xrtcL7.Weight = 18.4250767931525;
		xrtcL8.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL8.CanGrow = false;
		xrtcL8.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakAmontPer", "{0:F4}")
		});
		xrtcL8.Name = "xrtcL8";
		xrtcL8.StyleName = "DataField";
		xrtcL8.StylePriority.UseBorders = false;
		xrtcL8.Weight = 23.8251584111908;
		xrtcL9.CanGrow = false;
		xrtcL9.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakAreaPer", "{0:F4}")
		});
		xrtcL9.Name = "xrtcL9";
		xrtcL9.Weight = 18.5614512796755;
		xrtcL10.CanGrow = false;
		xrtcL10.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakheightPer", "{0:F4}")
		});
		xrtcL10.Name = "xrtcL10";
		xrtcL10.Weight = 15.5564553396201;
		xrtcL11.CanGrow = false;
		xrtcL11.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakLV", "{0:F4}")
		});
		xrtcL11.Name = "xrtcL11";
		xrtcL11.Weight = 75.22848903573575;
		fRulePeakAmont.Condition = "[PeakAmontPer]<0";
		fRulePeakAmont.Formatting.ForeColor = Color.White;
		fRulePeakAmont.Name = "fRulePeakAmont";
		instockList1.DataSetName = "instockList";
		instockList1.EnforceConstraints = false;
		instockList1.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		groupFooterBand1.HeightF = 1f;
		groupFooterBand1.Name = "groupFooterBand1";
		pageHeaderBand1.Controls.AddRange(new XRControl[1] { xrtHead });
		pageHeaderBand1.HeightF = 21.50258f;
		pageHeaderBand1.Name = "pageHeaderBand1";
		xrtHead.AnchorVertical = VerticalAnchorStyles.Bottom;
		xrtHead.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrtHead.LocationFloat = new PointFloat(0.9999809f, 0f);
		xrtHead.Name = "xrtHead";
		xrtHead.Rows.AddRange(new XRTableRow[1] { xrTableRow3 });
		xrtHead.SizeF = new SizeF(770f, 21.50258f);
		xrtHead.StylePriority.UseBorders = false;
		xrtHead.StylePriority.UseTextAlignment = false;
		xrtHead.TextAlignment = TextAlignment.MiddleCenter;
		xrTableRow3.Cells.AddRange(new XRTableCell[12]
		{
			xrtcH0, xrtcH1, xrtcH2, xrtcH3, xrtcH4, xrtcH5, xrtcH6, xrtcH7, xrtcH8, xrtcH9,
			xrtcH10, xrtcH11
		});
		xrTableRow3.Name = "xrTableRow3";
		xrTableRow3.Weight = 1.0;
		xrtcH0.Borders = BorderSide.All;
		xrtcH0.CanGrow = false;
		xrtcH0.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH0.ForeColor = SystemColors.Desktop;
		xrtcH0.Name = "xrtcH0";
		xrtcH0.StyleName = "FieldCaption";
		xrtcH0.StylePriority.UseBorders = false;
		xrtcH0.StylePriority.UseFont = false;
		xrtcH0.StylePriority.UseForeColor = false;
		xrtcH0.StylePriority.UseTextAlignment = false;
		xrtcH0.Text = "束管号";
		xrtcH0.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH0.Weight = 14.4359526975655;
		xrtcH1.Borders = BorderSide.All;
		xrtcH1.CanGrow = false;
		xrtcH1.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH1.ForeColor = SystemColors.Desktop;
		xrtcH1.Name = "xrtcH1";
		xrtcH1.StyleName = "FieldCaption";
		xrtcH1.StylePriority.UseBorders = false;
		xrtcH1.StylePriority.UseFont = false;
		xrtcH1.StylePriority.UseForeColor = false;
		xrtcH1.StylePriority.UseTextAlignment = false;
		xrtcH1.Text = "采样点";
		xrtcH1.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH1.Weight = 18.6199291323438;
		xrtcH2.Borders = BorderSide.All;
		xrtcH2.CanGrow = false;
		xrtcH2.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH2.ForeColor = SystemColors.Desktop;
		xrtcH2.Name = "xrtcH2";
		xrtcH2.StyleName = "FieldCaption";
		xrtcH2.StylePriority.UseBorders = false;
		xrtcH2.StylePriority.UseFont = false;
		xrtcH2.StylePriority.UseForeColor = false;
		xrtcH2.StylePriority.UseTextAlignment = false;
		xrtcH2.Text = "检测时间";
		xrtcH2.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH2.Weight = 18.8085872603051;
		xrtcH3.Borders = BorderSide.All;
		xrtcH3.CanGrow = false;
		xrtcH3.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH3.ForeColor = SystemColors.Desktop;
		xrtcH3.Name = "xrtcH3";
		xrtcH3.StyleName = "FieldCaption";
		xrtcH3.StylePriority.UseBorders = false;
		xrtcH3.StylePriority.UseFont = false;
		xrtcH3.StylePriority.UseForeColor = false;
		xrtcH3.StylePriority.UseTextAlignment = false;
		xrtcH3.Text = "O2";
		xrtcH3.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH3.Weight = 17.2461496282507;
		xrtcH4.Borders = BorderSide.All;
		xrtcH4.CanGrow = false;
		xrtcH4.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH4.ForeColor = SystemColors.Desktop;
		xrtcH4.Name = "xrtcH4";
		xrtcH4.StyleName = "FieldCaption";
		xrtcH4.StylePriority.UseBorders = false;
		xrtcH4.StylePriority.UseFont = false;
		xrtcH4.StylePriority.UseForeColor = false;
		xrtcH4.StylePriority.UseTextAlignment = false;
		xrtcH4.Text = "N2";
		xrtcH4.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH4.Weight = 16.30965186696;
		xrtcH5.Borders = BorderSide.All;
		xrtcH5.CanGrow = false;
		xrtcH5.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH5.ForeColor = SystemColors.Desktop;
		xrtcH5.Name = "xrtcH5";
		xrtcH5.StyleName = "FieldCaption";
		xrtcH5.StylePriority.UseBorders = false;
		xrtcH5.StylePriority.UseFont = false;
		xrtcH5.StylePriority.UseForeColor = false;
		xrtcH5.StylePriority.UseTextAlignment = false;
		xrtcH5.Text = "CO";
		xrtcH5.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH5.Weight = 16.9002379335003;
		xrtcH6.Borders = BorderSide.All;
		xrtcH6.CanGrow = false;
		xrtcH6.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH6.ForeColor = SystemColors.Desktop;
		xrtcH6.Name = "xrtcH6";
		xrtcH6.StyleName = "FieldCaption";
		xrtcH6.StylePriority.UseBorders = false;
		xrtcH6.StylePriority.UseFont = false;
		xrtcH6.StylePriority.UseForeColor = false;
		xrtcH6.StylePriority.UseTextAlignment = false;
		xrtcH6.Text = "CH4";
		xrtcH6.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH6.Weight = 11.761817155061;
		xrtcH7.Borders = BorderSide.All;
		xrtcH7.CanGrow = false;
		xrtcH7.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH7.ForeColor = SystemColors.Desktop;
		xrtcH7.Name = "xrtcH7";
		xrtcH7.StyleName = "FieldCaption";
		xrtcH7.StylePriority.UseBorders = false;
		xrtcH7.StylePriority.UseFont = false;
		xrtcH7.StylePriority.UseForeColor = false;
		xrtcH7.StylePriority.UseTextAlignment = false;
		xrtcH7.Text = "C2H2";
		xrtcH7.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH7.Weight = 18.4250764399399;
		xrtcH8.Borders = BorderSide.All;
		xrtcH8.CanGrow = false;
		xrtcH8.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH8.ForeColor = SystemColors.Desktop;
		xrtcH8.Name = "xrtcH8";
		xrtcH8.StyleName = "FieldCaption";
		xrtcH8.StylePriority.UseBorders = false;
		xrtcH8.StylePriority.UseFont = false;
		xrtcH8.StylePriority.UseForeColor = false;
		xrtcH8.StylePriority.UseTextAlignment = false;
		xrtcH8.Text = "C2H4";
		xrtcH8.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH8.Weight = 23.8251677360064;
		xrtcH9.Borders = BorderSide.All;
		xrtcH9.CanGrow = false;
		xrtcH9.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH9.ForeColor = SystemColors.Desktop;
		xrtcH9.Multiline = true;
		xrtcH9.Name = "xrtcH9";
		xrtcH9.StylePriority.UseBorders = false;
		xrtcH9.StylePriority.UseFont = false;
		xrtcH9.StylePriority.UseForeColor = false;
		xrtcH9.Text = "C2H6\r\n";
		xrtcH9.Weight = 18.5614508746583;
		xrtcH10.Borders = BorderSide.All;
		xrtcH10.CanGrow = false;
		xrtcH10.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH10.ForeColor = SystemColors.Desktop;
		xrtcH10.Name = "xrtcH10";
		xrtcH10.StylePriority.UseBorders = false;
		xrtcH10.StylePriority.UseFont = false;
		xrtcH10.StylePriority.UseForeColor = false;
		xrtcH10.Text = "CO2";
		xrtcH10.Weight = 15.5564500696865;
		xrtcH11.Borders = BorderSide.All;
		xrtcH11.CanGrow = false;
		xrtcH11.Font = new Font("SimHei", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH11.ForeColor = SystemColors.Desktop;
		xrtcH11.Name = "xrtcH11";
		xrtcH11.StylePriority.UseBorders = false;
		xrtcH11.StylePriority.UseFont = false;
		xrtcH11.StylePriority.UseForeColor = false;
		xrtcH11.Text = "烷烯比";
		xrtcH11.Weight = 75.5565000178955;
		xrTableRow1.Cells.AddRange(new XRTableCell[3] { xrTableCell1, xrTableCell2, xrTableCell3 });
		xrTableRow1.Name = "xrTableRow1";
		xrTableRow1.Weight = 1.0;
		xrTableCell1.Name = "xrTableCell1";
		xrTableCell1.Text = "xrTableCell1";
		xrTableCell1.Weight = 1.0;
		xrTableCell2.Name = "xrTableCell2";
		xrTableCell2.Text = "xrTableCell2";
		xrTableCell2.Weight = 1.0;
		xrTableCell3.Name = "xrTableCell3";
		xrTableCell3.Text = "xrTableCell3";
		xrTableCell3.Weight = 1.0;
		xrTableRow2.Cells.AddRange(new XRTableCell[3] { xrTableCell4, xrTableCell5, xrTableCell6 });
		xrTableRow2.Name = "xrTableRow2";
		xrTableRow2.Weight = 1.0;
		xrTableCell4.Name = "xrTableCell4";
		xrTableCell4.Text = "xrTableCell4";
		xrTableCell4.Weight = 1.0;
		xrTableCell5.Name = "xrTableCell5";
		xrTableCell5.Text = "xrTableCell5";
		xrTableCell5.Weight = 1.0;
		xrTableCell6.Name = "xrTableCell6";
		xrTableCell6.Text = "xrTableCell6";
		xrTableCell6.Weight = 1.0;
		pageFooterBand1.Controls.AddRange(new XRControl[3] { xrPageInfo1, xrPageInfo2, xrLabel1 });
		pageFooterBand1.HeightF = 31.5f;
		pageFooterBand1.Name = "pageFooterBand1";
		xrPageInfo1.LocationFloat = new PointFloat(66.47723f, 6.00001f);
		xrPageInfo1.Name = "xrPageInfo1";
		xrPageInfo1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
		xrPageInfo1.SizeF = new SizeF(303.3144f, 23f);
		xrPageInfo1.StyleName = "PageInfo";
		xrPageInfo1.StylePriority.UseTextAlignment = false;
		xrPageInfo1.TextAlignment = TextAlignment.MiddleLeft;
		xrPageInfo2.Format = "页 {0} / {1}";
		xrPageInfo2.LocationFloat = new PointFloat(412f, 6.00001f);
		xrPageInfo2.Name = "xrPageInfo2";
		xrPageInfo2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrPageInfo2.SizeF = new SizeF(343.5206f, 23f);
		xrPageInfo2.StyleName = "PageInfo";
		xrPageInfo2.StylePriority.UseTextAlignment = false;
		xrPageInfo2.TextAlignment = TextAlignment.MiddleRight;
		xrLabel1.AnchorVertical = VerticalAnchorStyles.Top;
		xrLabel1.Borders = BorderSide.All;
		xrLabel1.CanShrink = true;
		xrLabel1.Font = new Font("SimSun", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrLabel1.LocationFloat = new PointFloat(0f, 6.00001f);
		xrLabel1.Multiline = true;
		xrLabel1.Name = "xrLabel1";
		xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrLabel1.SizeF = new SizeF(66.47723f, 25.49999f);
		xrLabel1.StyleName = "Title";
		xrLabel1.StylePriority.UseBorders = false;
		xrLabel1.StylePriority.UseFont = false;
		xrLabel1.StylePriority.UseTextAlignment = false;
		xrLabel1.Text = "打印时间";
		xrLabel1.TextAlignment = TextAlignment.MiddleLeft;
		reportHeaderBand1.HeightF = 14.38011f;
		reportHeaderBand1.Name = "reportHeaderBand1";
		xrlCaption.LocationFloat = new PointFloat(0f, 10.00001f);
		xrlCaption.Name = "xrlCaption";
		xrlCaption.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrlCaption.SizeF = new SizeF(770f, 33f);
		xrlCaption.StyleName = "Title";
		xrlCaption.StylePriority.UseTextAlignment = false;
		xrlCaption.Text = "XXXX自定义表格演示";
		xrlCaption.TextAlignment = TextAlignment.MiddleCenter;
		Title.BackColor = Color.Transparent;
		Title.BorderColor = Color.Black;
		Title.Borders = BorderSide.None;
		Title.BorderWidth = 1f;
		Title.Font = new Font("Times New Roman", 20f, FontStyle.Bold);
		Title.ForeColor = Color.Maroon;
		Title.Name = "Title";
		FieldCaption.BackColor = Color.Transparent;
		FieldCaption.BorderColor = Color.Black;
		FieldCaption.Borders = BorderSide.Bottom;
		FieldCaption.BorderWidth = 1f;
		FieldCaption.Font = new Font("Arial", 10f, FontStyle.Bold);
		FieldCaption.ForeColor = Color.Maroon;
		FieldCaption.Name = "FieldCaption";
		PageInfo.BackColor = Color.Transparent;
		PageInfo.BorderColor = Color.Black;
		PageInfo.Borders = BorderSide.None;
		PageInfo.BorderWidth = 1f;
		PageInfo.Font = new Font("Times New Roman", 10f, FontStyle.Bold);
		PageInfo.ForeColor = Color.Black;
		PageInfo.Name = "PageInfo";
		DataField.BackColor = Color.Transparent;
		DataField.BorderColor = Color.Black;
		DataField.Borders = BorderSide.None;
		DataField.BorderWidth = 1f;
		DataField.Font = new Font("Times New Roman", 10f);
		DataField.ForeColor = Color.Black;
		DataField.Name = "DataField";
		DataField.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		topMarginBand1.Controls.AddRange(new XRControl[1] { xrlCaption });
		topMarginBand1.HeightF = 43.00001f;
		topMarginBand1.Name = "topMarginBand1";
		bottomMarginBand1.HeightF = 33f;
		bottomMarginBand1.Name = "bottomMarginBand1";
		formattingRule1.Condition = "[maIndex] >= 3";
		formattingRule1.DataMember = "poOrderListReportBom";
		formattingRule1.Formatting.BackColor = Color.Yellow;
		formattingRule1.Formatting.BorderColor = Color.Firebrick;
		formattingRule1.Formatting.Font = new Font("SimSun", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 134);
		formattingRule1.Formatting.Visible = DefaultBoolean.True;
		formattingRule1.Name = "formattingRule1";
		xrControlStyle1.ForeColor = Color.Red;
		xrControlStyle1.Name = "xrControlStyle1";
		poOrderListReportBomTableAdapter1.ClearBeforeFill = true;
		ReportFooter.Controls.AddRange(new XRControl[2] { xrTable1, xrlFoot });
		ReportFooter.HeightF = 48.41668f;
		ReportFooter.Name = "ReportFooter";
		xrTable1.AnchorVertical = VerticalAnchorStyles.Top;
		xrTable1.LocationFloat = new PointFloat(1f, 25.49999f);
		xrTable1.Name = "xrTable1";
		xrTable1.Rows.AddRange(new XRTableRow[1] { xrTableRow6 });
		xrTable1.SizeF = new SizeF(770f, 21.50258f);
		xrTable1.StylePriority.UseTextAlignment = false;
		xrTable1.TextAlignment = TextAlignment.MiddleCenter;
		xrTableRow6.Cells.AddRange(new XRTableCell[8] { xrTableCell7, xrTableCell12, xrTableCell8, xrTableCell11, xrTableCell9, xrTableCell13, xrTableCell10, xrTableCell14 });
		xrTableRow6.Name = "xrTableRow6";
		xrTableRow6.Weight = 1.0;
		xrTableCell7.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell7.CanGrow = false;
		xrTableCell7.Name = "xrTableCell7";
		xrTableCell7.StyleName = "FieldCaption";
		xrTableCell7.StylePriority.UseBorders = false;
		xrTableCell7.StylePriority.UseTextAlignment = false;
		xrTableCell7.Text = "制表人";
		xrTableCell7.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell7.Weight = 37.8989881539051;
		xrTableCell12.Name = "xrTableCell12";
		xrTableCell12.Text = "xrTableCell12";
		xrTableCell12.Weight = 23.44031424936619;
		xrTableCell8.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell8.CanGrow = false;
		xrTableCell8.Name = "xrTableCell8";
		xrTableCell8.StyleName = "FieldCaption";
		xrTableCell8.StylePriority.UseBorders = false;
		xrTableCell8.StylePriority.UseTextAlignment = false;
		xrTableCell8.Text = "技术员";
		xrTableCell8.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell8.Weight = 23.44031424936619;
		xrTableCell11.Name = "xrTableCell11";
		xrTableCell11.Text = "xrTableCell11";
		xrTableCell11.Weight = 30.01464904618186;
		xrTableCell9.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell9.CanGrow = false;
		xrTableCell9.Name = "xrTableCell9";
		xrTableCell9.StyleName = "FieldCaption";
		xrTableCell9.StylePriority.UseBorders = false;
		xrTableCell9.StylePriority.UseTextAlignment = false;
		xrTableCell9.Text = "科长";
		xrTableCell9.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell9.Weight = 30.01464904618186;
		xrTableCell13.Name = "xrTableCell13";
		xrTableCell13.Text = "xrTableCell13";
		xrTableCell13.Weight = 40.532676700208604;
		xrTableCell10.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell10.CanGrow = false;
		xrTableCell10.Name = "xrTableCell10";
		xrTableCell10.StyleName = "FieldCaption";
		xrTableCell10.StylePriority.UseBorders = false;
		xrTableCell10.StylePriority.UseTextAlignment = false;
		xrTableCell10.Text = "总工";
		xrTableCell10.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell10.Weight = 39.70751040489723;
		xrTableCell14.Name = "xrTableCell14";
		xrTableCell14.Text = "xrTableCell14";
		xrTableCell14.Weight = 38.637317902979575;
		xrlFoot.AnchorVertical = VerticalAnchorStyles.Top;
		xrlFoot.Borders = BorderSide.All;
		xrlFoot.CanShrink = true;
		xrlFoot.Font = new Font("SimSun", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrlFoot.LocationFloat = new PointFloat(607.4882f, 0f);
		xrlFoot.Multiline = true;
		xrlFoot.Name = "xrlFoot";
		xrlFoot.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrlFoot.SizeF = new SizeF(167.5118f, 25.49999f);
		xrlFoot.StyleName = "Title";
		xrlFoot.StylePriority.UseBorders = false;
		xrlFoot.StylePriority.UseFont = false;
		xrlFoot.StylePriority.UseTextAlignment = false;
		xrlFoot.Text = "备注：按               ";
		xrlFoot.TextAlignment = TextAlignment.MiddleLeft;
		instockList2.DataSetName = "instockList";
		instockList2.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		instockList3.DataSetName = "instockList";
		instockList3.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		instockList4.DataSetName = "instockList";
		instockList4.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		storgeOrderAdapter.ClearBeforeFill = true;
		base.Bands.AddRange(new Band[7] { Detail, pageHeaderBand1, pageFooterBand1, reportHeaderBand1, topMarginBand1, bottomMarginBand1, ReportFooter });
		base.DataAdapter = poOrderListReportBomTableAdapter1;
		base.DataMember = "spreTable";
		base.DataSource = instockList4;
		base.FormattingRuleSheet.AddRange(new FormattingRule[2] { formattingRule1, fRulePeakAmont });
		base.Margins = new Margins(17, 35, 43, 33);
		base.PageHeight = 1169;
		base.PageWidth = 827;
		base.PaperKind = PaperKind.A4;
		base.Scripts.OnDataSourceDemanded = "XrInStockList_DataSourceDemanded";
		base.ScriptsSource = "\r\nprivate void XrInStockList_DataSourceDemanded(object sender, System.EventArgs e) \r\n{\r\n\r\n}\r\n";
		base.StyleSheet.AddRange(new XRControlStyle[5] { Title, FieldCaption, PageInfo, DataField, xrControlStyle1 });
		base.Version = "13.2";
		((ISupportInitialize)xrtList).EndInit();
		((ISupportInitialize)instockList1).EndInit();
		((ISupportInitialize)xrtHead).EndInit();
		((ISupportInitialize)xrTable1).EndInit();
		((ISupportInitialize)instockList2).EndInit();
		((ISupportInitialize)instockList3).EndInit();
		((ISupportInitialize)instockList4).EndInit();
		((ISupportInitialize)this).EndInit();
	}
}
