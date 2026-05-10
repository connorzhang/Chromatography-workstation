using System;
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
using IBrainChrom2018.Unit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IBrainChrom2018.Report;

public class XrAnysReport : XtraReport
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

	private XRTableCell xrtcL12;

	private XRTableCell xrtcL14;

	private XRTableCell xrtcL15;

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

	private XRTableCell xrtcH15;

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

	private XRTable xrTable3;

	private XRTableRow xrTableRow5;

	private XRTableCell xrTableCell27;

	private XRTableCell xrTableCell29;

	private XRTableCell xrtInjectTime;

	private XRTable xrTable4;

	private XRTableRow xrTableRow6;

	private XRTableCell xrTableCell30;

	private XRTableCell xrtFileName;

	private XRTableCell xrtcL2;

	private XRTableCell xrtcL3;

	private storgeOrderAdapter poOrderListReportBomTableAdapter1;

	private XRTableCell xrtcH9;

	private XRTableCell xrtcH10;

	private XRTableCell xrtcH11;

	private XRTableCell xrtcH12;

	private XRTableCell xrtcH13;

	private XRTableCell xrtcH14;

	private XRTableCell xrtcL9;

	private XRTableCell xrtcL10;

	private XRTableCell xrtcL11;

	private XRTableCell xrtcL13;

	private ReportFooterBand ReportFooter;

	public XRLabel xrlCaption;

	public XRTableCell xrtPrintTime;

	public XRLabel xrlTitle;

	public XRLabel xrlFoot;

	private XRTableCell xrtcL16;

	private XRTableCell xrtcH16;

	private XRTable xrtGroup;

	private XRTableRow xrTableRow7;

	private XRTableCell xrtcG0;

	private XRTableCell xrtcG1;

	private XRTableCell xrtcG2;

	private XRTableCell xrtcG3;

	private XRTableCell xrtcG4;

	private XRTableCell xrtcG5;

	private XRTableCell xrtcG6;

	private XRTableCell xrtcG7;

	private XRTableCell xrtcG8;

	private XRTableCell xrtcG9;

	private XRTableCell xrtcG10;

	private XRTableCell xrtcG11;

	private XRTableCell xrtcG12;

	private XRTableCell xrtcG13;

	private XRTableCell xrtcG14;

	private XRTableCell xrtcG15;

	private XRTableCell xrtcG16;

	private FormattingRule fRulePeakAmont;

	public XRPictureBox xrPictureBox;

	public XrAnysReport()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	public void SetOrderType(Chromatogram chromatogram, Image imgChrom, string strFileName)
	{
		PrintPara pPara = chromatogram.PPara;
		xrlCaption.Text = pPara.Title;
		xrtPrintTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
		if (pPara.BJtime)
		{
			xrTableCell29.Text = Lang.PS("进样时间", "INJ Time");
			xrtInjectTime.Text = chromatogram.userArchives[0].saveTime.ToString("yyyy/MM/dd HH:mm");
		}
		else
		{
			xrTableCell29.Text = "";
			xrtInjectTime.Text = "";
		}
		if (pPara.Bfname)
		{
			xrtFileName.Text = strFileName;
		}
		else
		{
			xrtFileName.Text = "";
		}
		xrlTitle.Text = pPara.PrintTitleTop;
		xrlFoot.Text = pPara.PrintTitleBotom;
		xrPictureBox.Image = imgChrom;
		int num = iPicWidth - 30;
		float num2 = 48.6f;
		int num3 = 17;
		xrtcH4.Text = Lang.PS("浓度") + "[" + chromatogram.AmountUnit + "]";
		if (!pPara.Bfname)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH1);
			xrtList.DeleteColumn(xrtcL1);
			xrtGroup.DeleteColumn(xrtcG1);
		}
		if (!pPara.BPeakMaxTime)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH2);
			xrtList.DeleteColumn(xrtcL2);
			xrtGroup.DeleteColumn(xrtcG2);
		}
		if (!pPara.BPeakPara)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH3);
			xrtList.DeleteColumn(xrtcL3);
			xrtGroup.DeleteColumn(xrtcG3);
		}
		if (!pPara.BPeakAmont)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH4);
			xrtList.DeleteColumn(xrtcL4);
			xrtGroup.DeleteColumn(xrtcG4);
		}
		if (!pPara.BPeakArea)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH5);
			xrtList.DeleteColumn(xrtcL5);
			xrtGroup.DeleteColumn(xrtcG5);
		}
		if (!pPara.BPeakheight)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH6);
			xrtList.DeleteColumn(xrtcL6);
			xrtGroup.DeleteColumn(xrtcG6);
		}
		if (!pPara.BPeakHalfheight)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH7);
			xrtList.DeleteColumn(xrtcL7);
			xrtGroup.DeleteColumn(xrtcG7);
		}
		if (!pPara.BPeakAmontPer)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH8);
			xrtList.DeleteColumn(xrtcL8);
			xrtGroup.DeleteColumn(xrtcG8);
		}
		if (!pPara.BPeakAreaPer)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH9);
			xrtList.DeleteColumn(xrtcL9);
			xrtGroup.DeleteColumn(xrtcG9);
		}
		if (!pPara.BPeakheightPer)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH10);
			xrtList.DeleteColumn(xrtcL10);
			xrtGroup.DeleteColumn(xrtcG10);
		}
		if (!pPara.BPeakLV)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH11);
			xrtList.DeleteColumn(xrtcL11);
			xrtGroup.DeleteColumn(xrtcG11);
		}
		if (!pPara.BPeakTBPara)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH12);
			xrtList.DeleteColumn(xrtcL12);
			xrtGroup.DeleteColumn(xrtcG12);
		}
		if (!pPara.BPeakUTBPara)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH13);
			xrtList.DeleteColumn(xrtcL13);
			xrtGroup.DeleteColumn(xrtcG13);
		}
		if (!pPara.BPeakLPara)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH14);
			xrtList.DeleteColumn(xrtcL14);
			xrtGroup.DeleteColumn(xrtcG14);
		}
		if (!pPara.BPeaktailPara)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH15);
			xrtList.DeleteColumn(xrtcL15);
			xrtGroup.DeleteColumn(xrtcG15);
		}
		if (!pPara.BPeakFx)
		{
			num3--;
			xrtHead.DeleteColumn(xrtcH16);
			xrtList.DeleteColumn(xrtcL16);
			xrtGroup.DeleteColumn(xrtcG16);
		}
		num2 = (float)num / (float)num3;
		XRTableCell xRTableCell = xrtcH0;
		XRTableCell xRTableCell2 = xrtcL0;
		float num4 = (xrtcG0.WidthF = num2);
		float widthF = (xRTableCell2.WidthF = num4);
		xRTableCell.WidthF = widthF;
		if (pPara.Bfname)
		{
			XRTableCell xRTableCell3 = xrtcH1;
			XRTableCell xRTableCell4 = xrtcL1;
			num4 = (xrtcG1.WidthF = num2);
			widthF = (xRTableCell4.WidthF = num4);
			xRTableCell3.WidthF = widthF;
		}
		if (pPara.BPeakMaxTime)
		{
			XRTableCell xRTableCell5 = xrtcH2;
			XRTableCell xRTableCell6 = xrtcL2;
			num4 = (xrtcG2.WidthF = num2);
			widthF = (xRTableCell6.WidthF = num4);
			xRTableCell5.WidthF = widthF;
		}
		if (pPara.BPeakPara)
		{
			XRTableCell xRTableCell7 = xrtcH3;
			XRTableCell xRTableCell8 = xrtcL3;
			num4 = (xrtcG3.WidthF = num2);
			widthF = (xRTableCell8.WidthF = num4);
			xRTableCell7.WidthF = widthF;
		}
		if (pPara.BPeakAmont)
		{
			XRTableCell xRTableCell9 = xrtcH4;
			XRTableCell xRTableCell10 = xrtcL4;
			num4 = (xrtcG4.WidthF = num2);
			widthF = (xRTableCell10.WidthF = num4);
			xRTableCell9.WidthF = widthF;
		}
		if (pPara.BPeakArea)
		{
			XRTableCell xRTableCell11 = xrtcH5;
			XRTableCell xRTableCell12 = xrtcL5;
			num4 = (xrtcG5.WidthF = num2);
			widthF = (xRTableCell12.WidthF = num4);
			xRTableCell11.WidthF = widthF;
		}
		if (pPara.BPeakheight)
		{
			XRTableCell xRTableCell13 = xrtcH6;
			XRTableCell xRTableCell14 = xrtcL6;
			num4 = (xrtcG6.WidthF = num2);
			widthF = (xRTableCell14.WidthF = num4);
			xRTableCell13.WidthF = widthF;
		}
		if (pPara.BPeakHalfheight)
		{
			XRTableCell xRTableCell15 = xrtcH7;
			XRTableCell xRTableCell16 = xrtcL7;
			num4 = (xrtcG7.WidthF = num2);
			widthF = (xRTableCell16.WidthF = num4);
			xRTableCell15.WidthF = widthF;
		}
		if (pPara.BPeakAmontPer)
		{
			XRTableCell xRTableCell17 = xrtcH8;
			XRTableCell xRTableCell18 = xrtcL8;
			num4 = (xrtcG8.WidthF = num2);
			widthF = (xRTableCell18.WidthF = num4);
			xRTableCell17.WidthF = widthF;
		}
		if (pPara.BPeakAreaPer)
		{
			XRTableCell xRTableCell19 = xrtcH9;
			XRTableCell xRTableCell20 = xrtcL9;
			num4 = (xrtcG9.WidthF = num2);
			widthF = (xRTableCell20.WidthF = num4);
			xRTableCell19.WidthF = widthF;
		}
		if (pPara.BPeakheightPer)
		{
			XRTableCell xRTableCell21 = xrtcH10;
			XRTableCell xRTableCell22 = xrtcL10;
			num4 = (xrtcG10.WidthF = num2);
			widthF = (xRTableCell22.WidthF = num4);
			xRTableCell21.WidthF = widthF;
		}
		if (pPara.BPeakLV)
		{
			XRTableCell xRTableCell23 = xrtcH11;
			XRTableCell xRTableCell24 = xrtcL11;
			num4 = (xrtcG11.WidthF = num2);
			widthF = (xRTableCell24.WidthF = num4);
			xRTableCell23.WidthF = widthF;
		}
		if (pPara.BPeakTBPara)
		{
			XRTableCell xRTableCell25 = xrtcH12;
			XRTableCell xRTableCell26 = xrtcL12;
			num4 = (xrtcG12.WidthF = num2);
			widthF = (xRTableCell26.WidthF = num4);
			xRTableCell25.WidthF = widthF;
		}
		if (pPara.BPeakUTBPara)
		{
			XRTableCell xRTableCell27 = xrtcH13;
			XRTableCell xRTableCell28 = xrtcL13;
			num4 = (xrtcG13.WidthF = num2);
			widthF = (xRTableCell28.WidthF = num4);
			xRTableCell27.WidthF = widthF;
		}
		if (pPara.BPeakLPara)
		{
			XRTableCell xRTableCell29 = xrtcH14;
			XRTableCell xRTableCell30 = xrtcL14;
			num4 = (xrtcG14.WidthF = num2);
			widthF = (xRTableCell30.WidthF = num4);
			xRTableCell29.WidthF = widthF;
		}
		if (pPara.BPeaktailPara)
		{
			XRTableCell xRTableCell31 = xrtcH15;
			XRTableCell xRTableCell32 = xrtcL15;
			num4 = (xrtcG15.WidthF = num2);
			widthF = (xRTableCell32.WidthF = num4);
			xRTableCell31.WidthF = widthF;
		}
		if (pPara.BPeakFx)
		{
			XRTableCell xRTableCell33 = xrtcH16;
			XRTableCell xRTableCell34 = xrtcL16;
			num4 = (xrtcG16.WidthF = num2);
			widthF = (xRTableCell34.WidthF = num4);
			xRTableCell33.WidthF = widthF;
		}
		SystemParam systemParam = SystemParam.Create();
		string formatString = "{0:F" + Class49.int_8 + "}";
		xrtcL2.DataBindings["Text"].FormatString = formatString;
		xrtcL3.DataBindings["Text"].FormatString = formatString;
		xrtcL4.DataBindings["Text"].FormatString = formatString;
		xrtcL5.DataBindings["Text"].FormatString = formatString;
		xrtcL6.DataBindings["Text"].FormatString = formatString;
		xrtcL7.DataBindings["Text"].FormatString = formatString;
		xrtcL8.DataBindings["Text"].FormatString = formatString;
		xrtcL9.DataBindings["Text"].FormatString = formatString;
		xrtcL10.DataBindings["Text"].FormatString = formatString;
		xrtcL11.DataBindings["Text"].FormatString = formatString;
		xrtcL12.DataBindings["Text"].FormatString = formatString;
		xrtcL13.DataBindings["Text"].FormatString = formatString;
		xrtcL14.DataBindings["Text"].FormatString = formatString;
		xrtcL15.DataBindings["Text"].FormatString = formatString;
		xrtcL16.DataBindings["Text"].FormatString = formatString;
		xrtcG3.Summary.FormatString = formatString;
		xrtcG4.Summary.FormatString = formatString;
		xrtcG5.Summary.FormatString = formatString;
		xrtcG6.Summary.FormatString = formatString;
		xrtcG7.Summary.FormatString = formatString;
		xrtcG8.Summary.FormatString = formatString;
		xrtcG9.Summary.FormatString = formatString;
		xrtcG10.Summary.FormatString = formatString;
		xrtcG11.Summary.FormatString = formatString;
		xrtcG12.Summary.FormatString = formatString;
		xrtcG13.Summary.FormatString = formatString;
		xrtcG14.Summary.FormatString = formatString;
		xrtcG15.Summary.FormatString = formatString;
		xrtcG16.Summary.FormatString = formatString;
	}

	protected void CreateMateFile(Chromatogram chromatogram)
	{
	}

	protected void CreateSeries(Chromatogram chromatogram)
	{
	}

	public static XrAnysReport CreateReport(Chromatogram chromatogram, Image imgChrom, string strFileName)
	{
		PrintPara pPara = chromatogram.PPara;
		XrAnysReport xrAnysReport = new XrAnysReport();
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
			if (float.IsNaN(num2))
			{
				num2 = 0f;
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
		SystemParam systemParam = SystemParam.Create();
		Size size = new Size(iPicWidth * 2, iPicHeight * 2);
		RectangleF rectangleF = new RectangleF(0f, 0f, size.Width, size.Height);
		Bitmap bitmap = new Bitmap(size.Width, size.Height);
		Graphics graphics = Graphics.FromImage(bitmap);
		chromatogram.signal.disColor = Color.Black;
		Font font = FrameDis.font;
		FrameDis.font = new Font(font.FontFamily, font.Size * 2f, font.Style);
		ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
		chromDisplay.LinkDisChroms(new Chromatogram[1] { chromatogram }, ref setChromNo);
		chromDisplay.showMouseLgValue = false;
		chromDisplay.showProgTemp = false;
		chromDisplay.ShowBgChrom = true;
		chromDisplay.setShowGrid = true;
		chromDisplay.rcPage = rectangleF;
		chromDisplay.dskRC = rectangleF;
		chromDisplay.FrmPen.Width = 2.2f;
		chromDisplay.DisPen.Width = 2.2f;
		Font peakFont = chromDisplay.options.peakFont;
		chromDisplay.options.peakFont = new Font(peakFont.FontFamily, peakFont.Size * 2f, peakFont.Style);
		chromDisplay.Draw(graphics, erase: true);
		graphics.Dispose();
		FrameDis.font = font;
		return CreateReport(chromatogram, bitmap, strFileName);
	}

	public static bool DataTableToExcel(DataTable dt, string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		ISheet sheet = null;
		ICell cell = null;
		bool flag = false;
		double result2 = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			if (dt != null && dt.Rows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dt.Rows.Count;
				int count2 = dt.Columns.Count;
				row = sheet.GetRow(7);
				for (int i = 0; i < count2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
					cell.SetCellValue(dt.Columns[i].ColumnName);
				}
				for (int j = 8; j < count + 8; j++)
				{
					row = sheet.GetRow(j);
					if (row == null)
					{
						row = sheet.CreateRow(j);
					}
					for (int k = 0; k < count2; k++)
					{
						cell = row.GetCell(k);
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						double.TryParse(dt.Rows[j - 8][k].ToString(), out result2);
						cell.SetCellValue(result2.ToString("0.0000"));
					}
				}
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			fileStream?.Close();
			return false;
		}
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

	public static void ExportAndPrintReport(Chromatogram chromatogram, string strFileName)
	{
		string text = Path.GetFileNameWithoutExtension(strFileName) + ".xls";
		string directoryName = Path.GetDirectoryName(strFileName);
		XrAnysReport xrAnysReport = CreateReport(chromatogram, strFileName);
		xrAnysReport.CreateDocument();
		xrAnysReport.ExportToXls(directoryName + "\\" + text);
		xrAnysReport.Print();
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
		xrtcH12.Text = Lang.PS("柱效");
		xrtcH13.Text = Lang.PS("塔板数");
		xrtcH14.Text = Lang.PS("容量因子");
		xrtcH15.Text = Lang.PS("拖尾因子");
		xrtcH16.Text = Lang.PS("工作曲线:", " PeakFx:");
		xrTableCell27.Text = Lang.PS("打印时间", "Print Time");
		xrTableCell29.Text = Lang.PS("进样时间", "INJ Time");
		xrtInjectTime.Text = Lang.PS("名称");
		xrTableCell30.Text = Lang.PS("谱图文件名", "Chrom File Name");
		xrtFileName.Text = Lang.PS("备注");
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
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XrAnysReport));
		XRSummary xRSummary = new XRSummary();
		XRSummary xRSummary2 = new XRSummary();
		XRSummary xRSummary3 = new XRSummary();
		XRSummary xRSummary4 = new XRSummary();
		XRSummary xRSummary5 = new XRSummary();
		XRSummary xRSummary6 = new XRSummary();
		XRSummary xRSummary7 = new XRSummary();
		XRSummary xRSummary8 = new XRSummary();
		XRSummary xRSummary9 = new XRSummary();
		XRSummary xRSummary10 = new XRSummary();
		XRSummary xRSummary11 = new XRSummary();
		XRSummary xRSummary12 = new XRSummary();
		XRSummary xRSummary13 = new XRSummary();
		XRSummary xRSummary14 = new XRSummary();
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
		xrtcL12 = new XRTableCell();
		xrtcL13 = new XRTableCell();
		xrtcL14 = new XRTableCell();
		xrtcL15 = new XRTableCell();
		xrtcL16 = new XRTableCell();
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
		xrtcH12 = new XRTableCell();
		xrtcH13 = new XRTableCell();
		xrtcH14 = new XRTableCell();
		xrtcH15 = new XRTableCell();
		xrtcH16 = new XRTableCell();
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
		reportHeaderBand1 = new ReportHeaderBand();
		xrPictureBox = new XRPictureBox();
		xrTable3 = new XRTable();
		xrTableRow5 = new XRTableRow();
		xrTableCell27 = new XRTableCell();
		xrtPrintTime = new XRTableCell();
		xrTableCell29 = new XRTableCell();
		xrtInjectTime = new XRTableCell();
		xrTable4 = new XRTable();
		xrTableRow6 = new XRTableRow();
		xrTableCell30 = new XRTableCell();
		xrtFileName = new XRTableCell();
		xrlTitle = new XRLabel();
		xrlCaption = new XRLabel();
		Title = new XRControlStyle();
		FieldCaption = new XRControlStyle();
		PageInfo = new XRControlStyle();
		DataField = new XRControlStyle();
		topMarginBand1 = new TopMarginBand();
		bottomMarginBand1 = new BottomMarginBand();
		formattingRule1 = new FormattingRule();
		xrControlStyle1 = new XRControlStyle();
		ReportFooter = new ReportFooterBand();
		xrtGroup = new XRTable();
		xrTableRow7 = new XRTableRow();
		xrtcG0 = new XRTableCell();
		xrtcG1 = new XRTableCell();
		xrtcG2 = new XRTableCell();
		xrtcG3 = new XRTableCell();
		xrtcG4 = new XRTableCell();
		xrtcG5 = new XRTableCell();
		xrtcG6 = new XRTableCell();
		xrtcG7 = new XRTableCell();
		xrtcG8 = new XRTableCell();
		xrtcG9 = new XRTableCell();
		xrtcG10 = new XRTableCell();
		xrtcG11 = new XRTableCell();
		xrtcG12 = new XRTableCell();
		xrtcG13 = new XRTableCell();
		xrtcG14 = new XRTableCell();
		xrtcG15 = new XRTableCell();
		xrtcG16 = new XRTableCell();
		xrlFoot = new XRLabel();
		((ISupportInitialize)xrtList).BeginInit();
		((ISupportInitialize)instockList1).BeginInit();
		((ISupportInitialize)xrtHead).BeginInit();
		((ISupportInitialize)xrTable3).BeginInit();
		((ISupportInitialize)xrTable4).BeginInit();
		((ISupportInitialize)xrtGroup).BeginInit();
		((ISupportInitialize)this).BeginInit();
		Detail.Borders = BorderSide.Left | BorderSide.Right;
		Detail.Controls.AddRange(new XRControl[1] { xrtList });
		Detail.Font = new Font("宋体", 10f);
		Detail.HeightF = 23f;
		Detail.KeepTogether = true;
		Detail.Name = "Detail";
		Detail.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		Detail.TextAlignment = TextAlignment.MiddleCenter;
		xrtList.AnchorVertical = VerticalAnchorStyles.Both;
		xrtList.Borders = BorderSide.All;
		xrtList.LocationFloat = new PointFloat(0.9999911f, 0f);
		xrtList.Name = "xrtList";
		xrtList.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrtList.Rows.AddRange(new XRTableRow[1] { xrTableRow4 });
		xrtList.SizeF = new SizeF(736f, 23f);
		xrtList.StylePriority.UseBorderColor = false;
		xrtList.StylePriority.UseBorders = false;
		xrtList.StylePriority.UseFont = false;
		xrtList.StylePriority.UsePadding = false;
		xrtList.StylePriority.UseTextAlignment = false;
		xrTableRow4.Cells.AddRange(new XRTableCell[17]
		{
			xrtcL0, xrtcL1, xrtcL2, xrtcL3, xrtcL4, xrtcL5, xrtcL6, xrtcL7, xrtcL8, xrtcL9,
			xrtcL10, xrtcL11, xrtcL12, xrtcL13, xrtcL14, xrtcL15, xrtcL16
		});
		xrTableRow4.Name = "xrTableRow4";
		xrTableRow4.Weight = 1.0;
		xrtcL0.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL0.CanGrow = false;
		xrtcL0.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.index")
		});
		xrtcL0.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrtcL0.Name = "xrtcL0";
		xrtcL0.StyleName = "DataField";
		xrtcL0.StylePriority.UseBorders = false;
		xrtcL0.StylePriority.UseFont = false;
		xrtcL0.Weight = 14.435955231278;
		xrtcL1.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL1.CanGrow = false;
		xrtcL1.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakName")
		});
		xrtcL1.Name = "xrtcL1";
		xrtcL1.StyleName = "DataField";
		xrtcL1.StylePriority.UseBorders = false;
		xrtcL1.Weight = 18.6199251445723;
		xrtcL2.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL2.CanGrow = false;
		xrtcL2.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakMaxTime", "{0:F4}")
		});
		xrtcL2.Name = "xrtcL2";
		xrtcL2.StylePriority.UseBorders = false;
		xrtcL2.Weight = 18.8085943080761;
		xrtcL3.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL3.CanGrow = false;
		xrtcL3.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakPara", "{0:F4}")
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
		xrtcL11.Weight = 13.1402386847837;
		xrtcL12.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL12.CanGrow = false;
		xrtcL12.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakTBPara", "{0:F4}")
		});
		xrtcL12.Name = "xrtcL12";
		xrtcL12.StyleName = "DataField";
		xrtcL12.StylePriority.UseBorders = false;
		xrtcL12.Weight = 13.8347392682676;
		xrtcL13.CanGrow = false;
		xrtcL13.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakUTBPara", "{0:F4}")
		});
		xrtcL13.Name = "xrtcL13";
		xrtcL13.Weight = 9.32479181878362;
		xrtcL14.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL14.CanGrow = false;
		xrtcL14.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakLPara", "{0:F4}")
		});
		xrtcL14.Name = "xrtcL14";
		xrtcL14.StyleName = "DataField";
		xrtcL14.StylePriority.UseBorders = false;
		xrtcL14.Weight = 14.9766088108957;
		xrtcL15.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcL15.CanGrow = false;
		xrtcL15.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeaktailPara", "{0:F4}")
		});
		xrtcL15.Name = "xrtcL15";
		xrtcL15.StyleName = "DataField";
		xrtcL15.StylePriority.UseBorders = false;
		xrtcL15.Weight = 13.6365693233631;
		xrtcL16.CanGrow = false;
		xrtcL16.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakFx", "{0:F4}")
		});
		xrtcL16.Name = "xrtcL16";
		xrtcL16.Weight = 1.9883235314477297;
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
		xrtHead.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrtHead.LocationFloat = new PointFloat(0.9999911f, 0f);
		xrtHead.Name = "xrtHead";
		xrtHead.Rows.AddRange(new XRTableRow[1] { xrTableRow3 });
		xrtHead.SizeF = new SizeF(735.9999f, 21.50258f);
		xrtHead.StylePriority.UseBorders = false;
		xrtHead.StylePriority.UseFont = false;
		xrtHead.StylePriority.UseTextAlignment = false;
		xrtHead.TextAlignment = TextAlignment.MiddleCenter;
		xrTableRow3.Cells.AddRange(new XRTableCell[17]
		{
			xrtcH0, xrtcH1, xrtcH2, xrtcH3, xrtcH4, xrtcH5, xrtcH6, xrtcH7, xrtcH8, xrtcH9,
			xrtcH10, xrtcH11, xrtcH12, xrtcH13, xrtcH14, xrtcH15, xrtcH16
		});
		xrTableRow3.Name = "xrTableRow3";
		xrTableRow3.Weight = 1.0;
		xrtcH0.Borders = BorderSide.All;
		xrtcH0.CanGrow = false;
		xrtcH0.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH0.ForeColor = SystemColors.Desktop;
		xrtcH0.Name = "xrtcH0";
		xrtcH0.StyleName = "FieldCaption";
		xrtcH0.StylePriority.UseBorders = false;
		xrtcH0.StylePriority.UseFont = false;
		xrtcH0.StylePriority.UseForeColor = false;
		xrtcH0.StylePriority.UseTextAlignment = false;
		xrtcH0.Text = "序号";
		xrtcH0.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH0.Weight = 14.4359526975655;
		xrtcH1.Borders = BorderSide.All;
		xrtcH1.CanGrow = false;
		xrtcH1.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH1.ForeColor = SystemColors.Desktop;
		xrtcH1.Name = "xrtcH1";
		xrtcH1.StyleName = "FieldCaption";
		xrtcH1.StylePriority.UseBorders = false;
		xrtcH1.StylePriority.UseFont = false;
		xrtcH1.StylePriority.UseForeColor = false;
		xrtcH1.StylePriority.UseTextAlignment = false;
		xrtcH1.Text = "组份名称:";
		xrtcH1.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH1.Weight = 18.6199291323438;
		xrtcH2.Borders = BorderSide.All;
		xrtcH2.CanGrow = false;
		xrtcH2.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH2.ForeColor = SystemColors.Desktop;
		xrtcH2.Name = "xrtcH2";
		xrtcH2.StyleName = "FieldCaption";
		xrtcH2.StylePriority.UseBorders = false;
		xrtcH2.StylePriority.UseFont = false;
		xrtcH2.StylePriority.UseForeColor = false;
		xrtcH2.StylePriority.UseTextAlignment = false;
		xrtcH2.Text = " 保留时间:";
		xrtcH2.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH2.Weight = 18.8085872603051;
		xrtcH3.Borders = BorderSide.All;
		xrtcH3.CanGrow = false;
		xrtcH3.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH3.ForeColor = SystemColors.Desktop;
		xrtcH3.Name = "xrtcH3";
		xrtcH3.StyleName = "FieldCaption";
		xrtcH3.StylePriority.UseBorders = false;
		xrtcH3.StylePriority.UseFont = false;
		xrtcH3.StylePriority.UseForeColor = false;
		xrtcH3.StylePriority.UseTextAlignment = false;
		xrtcH3.Text = "校正因子:";
		xrtcH3.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH3.Weight = 17.2461496282507;
		xrtcH4.Borders = BorderSide.All;
		xrtcH4.CanGrow = false;
		xrtcH4.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH4.ForeColor = SystemColors.Desktop;
		xrtcH4.Name = "xrtcH4";
		xrtcH4.StyleName = "FieldCaption";
		xrtcH4.StylePriority.UseBorders = false;
		xrtcH4.StylePriority.UseFont = false;
		xrtcH4.StylePriority.UseForeColor = false;
		xrtcH4.StylePriority.UseTextAlignment = false;
		xrtcH4.Text = " 浓度:";
		xrtcH4.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH4.Weight = 16.30965186696;
		xrtcH5.Borders = BorderSide.All;
		xrtcH5.CanGrow = false;
		xrtcH5.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH5.ForeColor = SystemColors.Desktop;
		xrtcH5.Name = "xrtcH5";
		xrtcH5.StyleName = "FieldCaption";
		xrtcH5.StylePriority.UseBorders = false;
		xrtcH5.StylePriority.UseFont = false;
		xrtcH5.StylePriority.UseForeColor = false;
		xrtcH5.StylePriority.UseTextAlignment = false;
		xrtcH5.Text = "峰面积:";
		xrtcH5.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH5.Weight = 16.9002379335003;
		xrtcH6.Borders = BorderSide.All;
		xrtcH6.CanGrow = false;
		xrtcH6.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH6.ForeColor = SystemColors.Desktop;
		xrtcH6.Name = "xrtcH6";
		xrtcH6.StyleName = "FieldCaption";
		xrtcH6.StylePriority.UseBorders = false;
		xrtcH6.StylePriority.UseFont = false;
		xrtcH6.StylePriority.UseForeColor = false;
		xrtcH6.StylePriority.UseTextAlignment = false;
		xrtcH6.Text = "峰高:";
		xrtcH6.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH6.Weight = 11.761817155061;
		xrtcH7.Borders = BorderSide.All;
		xrtcH7.CanGrow = false;
		xrtcH7.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH7.ForeColor = SystemColors.Desktop;
		xrtcH7.Name = "xrtcH7";
		xrtcH7.StyleName = "FieldCaption";
		xrtcH7.StylePriority.UseBorders = false;
		xrtcH7.StylePriority.UseFont = false;
		xrtcH7.StylePriority.UseForeColor = false;
		xrtcH7.StylePriority.UseTextAlignment = false;
		xrtcH7.Text = "半峰宽";
		xrtcH7.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH7.Weight = 18.4250764399399;
		xrtcH8.Borders = BorderSide.All;
		xrtcH8.CanGrow = false;
		xrtcH8.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH8.ForeColor = SystemColors.Desktop;
		xrtcH8.Name = "xrtcH8";
		xrtcH8.StyleName = "FieldCaption";
		xrtcH8.StylePriority.UseBorders = false;
		xrtcH8.StylePriority.UseFont = false;
		xrtcH8.StylePriority.UseForeColor = false;
		xrtcH8.StylePriority.UseTextAlignment = false;
		xrtcH8.Text = "浓度百分比:";
		xrtcH8.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH8.Weight = 23.8251677360064;
		xrtcH9.Borders = BorderSide.All;
		xrtcH9.CanGrow = false;
		xrtcH9.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH9.ForeColor = SystemColors.Desktop;
		xrtcH9.Name = "xrtcH9";
		xrtcH9.StylePriority.UseBorders = false;
		xrtcH9.StylePriority.UseFont = false;
		xrtcH9.StylePriority.UseForeColor = false;
		xrtcH9.Text = "峰面积百分比:";
		xrtcH9.Weight = 18.5614508746583;
		xrtcH10.Borders = BorderSide.All;
		xrtcH10.CanGrow = false;
		xrtcH10.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH10.ForeColor = SystemColors.Desktop;
		xrtcH10.Name = "xrtcH10";
		xrtcH10.StylePriority.UseBorders = false;
		xrtcH10.StylePriority.UseFont = false;
		xrtcH10.StylePriority.UseForeColor = false;
		xrtcH10.Text = "峰高百分比:";
		xrtcH10.Weight = 15.5564500696865;
		xrtcH11.Borders = BorderSide.All;
		xrtcH11.CanGrow = false;
		xrtcH11.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH11.ForeColor = SystemColors.Desktop;
		xrtcH11.Name = "xrtcH11";
		xrtcH11.StylePriority.UseBorders = false;
		xrtcH11.StylePriority.UseFont = false;
		xrtcH11.StylePriority.UseForeColor = false;
		xrtcH11.Text = "峰分离度";
		xrtcH11.Weight = 13.1402384631428;
		xrtcH12.Borders = BorderSide.All;
		xrtcH12.CanGrow = false;
		xrtcH12.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH12.ForeColor = SystemColors.Desktop;
		xrtcH12.Name = "xrtcH12";
		xrtcH12.StylePriority.UseBorders = false;
		xrtcH12.StylePriority.UseFont = false;
		xrtcH12.StylePriority.UseForeColor = false;
		xrtcH12.Text = "柱效";
		xrtcH12.Weight = 13.8347594556985;
		xrtcH13.Borders = BorderSide.All;
		xrtcH13.CanGrow = false;
		xrtcH13.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH13.ForeColor = SystemColors.Desktop;
		xrtcH13.Name = "xrtcH13";
		xrtcH13.StylePriority.UseBorders = false;
		xrtcH13.StylePriority.UseFont = false;
		xrtcH13.StylePriority.UseForeColor = false;
		xrtcH13.Text = "塔板数";
		xrtcH13.Weight = 9.60468111132414;
		xrtcH14.Borders = BorderSide.All;
		xrtcH14.CanGrow = false;
		xrtcH14.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH14.ForeColor = SystemColors.Desktop;
		xrtcH14.Name = "xrtcH14";
		xrtcH14.StylePriority.UseBorders = false;
		xrtcH14.StylePriority.UseFont = false;
		xrtcH14.StylePriority.UseForeColor = false;
		xrtcH14.Text = "容量因子";
		xrtcH14.Weight = 14.6967129463545;
		xrtcH15.Borders = BorderSide.All;
		xrtcH15.CanGrow = false;
		xrtcH15.Font = new Font("黑体", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
		xrtcH15.ForeColor = SystemColors.Desktop;
		xrtcH15.Name = "xrtcH15";
		xrtcH15.StyleName = "FieldCaption";
		xrtcH15.StylePriority.UseBorders = false;
		xrtcH15.StylePriority.UseFont = false;
		xrtcH15.StylePriority.UseForeColor = false;
		xrtcH15.StylePriority.UseTextAlignment = false;
		xrtcH15.Text = "拖尾因子";
		xrtcH15.TextAlignment = TextAlignment.MiddleCenter;
		xrtcH15.Weight = 13.951789296326478;
		xrtcH16.Borders = BorderSide.All;
		xrtcH16.CanGrow = false;
		xrtcH16.Font = new Font("黑体", 8.25f, FontStyle.Bold);
		xrtcH16.Name = "xrtcH16";
		xrtcH16.StylePriority.UseBorders = false;
		xrtcH16.StylePriority.UseFont = false;
		xrtcH16.Text = "工作曲线:";
		xrtcH16.Weight = 1.9908622206422204;
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
		pageFooterBand1.Controls.AddRange(new XRControl[2] { xrPageInfo1, xrPageInfo2 });
		pageFooterBand1.HeightF = 29.00001f;
		pageFooterBand1.Name = "pageFooterBand1";
		xrPageInfo1.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrPageInfo1.LocationFloat = new PointFloat(0.9999809f, 6f);
		xrPageInfo1.Name = "xrPageInfo1";
		xrPageInfo1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
		xrPageInfo1.SizeF = new SizeF(399f, 23f);
		xrPageInfo1.StyleName = "PageInfo";
		xrPageInfo1.StylePriority.UseFont = false;
		xrPageInfo1.StylePriority.UseTextAlignment = false;
		xrPageInfo1.TextAlignment = TextAlignment.MiddleLeft;
		xrPageInfo2.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrPageInfo2.Format = "页 {0} / {1}";
		xrPageInfo2.LocationFloat = new PointFloat(412f, 6.00001f);
		xrPageInfo2.Name = "xrPageInfo2";
		xrPageInfo2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrPageInfo2.SizeF = new SizeF(335.0003f, 23f);
		xrPageInfo2.StyleName = "PageInfo";
		xrPageInfo2.StylePriority.UseFont = false;
		xrPageInfo2.StylePriority.UseTextAlignment = false;
		xrPageInfo2.TextAlignment = TextAlignment.MiddleRight;
		reportHeaderBand1.Controls.AddRange(new XRControl[4] { xrPictureBox, xrTable3, xrTable4, xrlTitle });
		reportHeaderBand1.HeightF = 378.9635f;
		reportHeaderBand1.Name = "reportHeaderBand1";
		xrPictureBox.Borders = BorderSide.All;
		xrPictureBox.Image = (Image)componentResourceManager.GetObject("xrPictureBox.Image");
		xrPictureBox.LocationFloat = new PointFloat(0.9999911f, 109.7551f);
		xrPictureBox.Name = "xrPictureBox";
		xrPictureBox.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrPictureBox.SizeF = new SizeF(746f, 264f);
		xrPictureBox.Sizing = ImageSizeMode.StretchImage;
		xrTable3.AnchorVertical = VerticalAnchorStyles.Top;
		xrTable3.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrTable3.LocationFloat = new PointFloat(0.9999809f, 0f);
		xrTable3.Name = "xrTable3";
		xrTable3.Rows.AddRange(new XRTableRow[1] { xrTableRow5 });
		xrTable3.SizeF = new SizeF(746.0002f, 21.50258f);
		xrTable3.StylePriority.UseFont = false;
		xrTable3.StylePriority.UseTextAlignment = false;
		xrTable3.TextAlignment = TextAlignment.MiddleCenter;
		xrTableRow5.Cells.AddRange(new XRTableCell[4] { xrTableCell27, xrtPrintTime, xrTableCell29, xrtInjectTime });
		xrTableRow5.Name = "xrTableRow5";
		xrTableRow5.Weight = 1.0;
		xrTableCell27.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell27.CanGrow = false;
		xrTableCell27.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrTableCell27.Name = "xrTableCell27";
		xrTableCell27.StyleName = "FieldCaption";
		xrTableCell27.StylePriority.UseBorders = false;
		xrTableCell27.StylePriority.UseFont = false;
		xrTableCell27.StylePriority.UseTextAlignment = false;
		xrTableCell27.Text = "打印时间";
		xrTableCell27.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell27.Weight = 37.8989881539051;
		xrtPrintTime.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrtPrintTime.CanGrow = false;
		xrtPrintTime.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrtPrintTime.Name = "xrtPrintTime";
		xrtPrintTime.StyleName = "FieldCaption";
		xrtPrintTime.StylePriority.UseBorders = false;
		xrtPrintTime.StylePriority.UseFont = false;
		xrtPrintTime.StylePriority.UseTextAlignment = false;
		xrtPrintTime.Text = "2018/09/07";
		xrtPrintTime.TextAlignment = TextAlignment.MiddleCenter;
		xrtPrintTime.Weight = 76.1833422625506;
		xrTableCell29.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell29.CanGrow = false;
		xrTableCell29.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrTableCell29.Name = "xrTableCell29";
		xrTableCell29.StyleName = "FieldCaption";
		xrTableCell29.StylePriority.UseBorders = false;
		xrTableCell29.StylePriority.UseFont = false;
		xrTableCell29.StylePriority.UseTextAlignment = false;
		xrTableCell29.Text = "进样时间";
		xrTableCell29.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell29.Weight = 30.7265843285455;
		xrtInjectTime.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrtInjectTime.CanGrow = false;
		xrtInjectTime.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrtInjectTime.Name = "xrtInjectTime";
		xrtInjectTime.StyleName = "FieldCaption";
		xrtInjectTime.StylePriority.UseBorders = false;
		xrtInjectTime.StylePriority.UseFont = false;
		xrtInjectTime.StylePriority.UseTextAlignment = false;
		xrtInjectTime.Text = "名称";
		xrtInjectTime.TextAlignment = TextAlignment.MiddleCenter;
		xrtInjectTime.Weight = 110.65874960401861;
		xrTable4.AnchorVertical = VerticalAnchorStyles.Top;
		xrTable4.LocationFloat = new PointFloat(0.9999809f, 21.50258f);
		xrTable4.Name = "xrTable4";
		xrTable4.Rows.AddRange(new XRTableRow[1] { xrTableRow6 });
		xrTable4.SizeF = new SizeF(746.0004f, 21.50258f);
		xrTable4.StylePriority.UseTextAlignment = false;
		xrTable4.TextAlignment = TextAlignment.MiddleCenter;
		xrTableRow6.Cells.AddRange(new XRTableCell[2] { xrTableCell30, xrtFileName });
		xrTableRow6.Name = "xrTableRow6";
		xrTableRow6.Weight = 1.0;
		xrTableCell30.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
		xrTableCell30.CanGrow = false;
		xrTableCell30.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrTableCell30.Name = "xrTableCell30";
		xrTableCell30.StyleName = "FieldCaption";
		xrTableCell30.StylePriority.UseBorders = false;
		xrTableCell30.StylePriority.UseFont = false;
		xrTableCell30.StylePriority.UseTextAlignment = false;
		xrTableCell30.Text = "谱图文件名";
		xrTableCell30.TextAlignment = TextAlignment.MiddleCenter;
		xrTableCell30.Weight = 37.8989881539051;
		xrtFileName.Borders = BorderSide.All;
		xrtFileName.CanGrow = false;
		xrtFileName.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrtFileName.Name = "xrtFileName";
		xrtFileName.StyleName = "FieldCaption";
		xrtFileName.StylePriority.UseBorders = false;
		xrtFileName.StylePriority.UseFont = false;
		xrtFileName.StylePriority.UseTextAlignment = false;
		xrtFileName.Text = "备注";
		xrtFileName.TextAlignment = TextAlignment.MiddleCenter;
		xrtFileName.Weight = 219.49926721456677;
		xrlTitle.AnchorVertical = VerticalAnchorStyles.Top;
		xrlTitle.Borders = BorderSide.All;
		xrlTitle.CanShrink = true;
		xrlTitle.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrlTitle.LocationFloat = new PointFloat(0.9999911f, 43.00515f);
		xrlTitle.Multiline = true;
		xrlTitle.Name = "xrlTitle";
		xrlTitle.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrlTitle.SizeF = new SizeF(746.0004f, 66.75f);
		xrlTitle.StyleName = "Title";
		xrlTitle.StylePriority.UseBorders = false;
		xrlTitle.StylePriority.UseFont = false;
		xrlTitle.StylePriority.UseTextAlignment = false;
		xrlTitle.Text = "质检（E）字第（ \u3000）号\r送样单位：         \u3000\u3000        仪器型号:      \r取样日期：   年  月  日       收样日期：     年  月  日\r样品批号：                       样品名称：固液\r样品罐号：                       仪器控制参数文件：";
		xrlTitle.TextAlignment = TextAlignment.MiddleLeft;
		xrlCaption.Font = new Font("宋体", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
		xrlCaption.LocationFloat = new PointFloat(0f, 22.50001f);
		xrlCaption.Name = "xrlCaption";
		xrlCaption.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrlCaption.SizeF = new SizeF(747f, 33f);
		xrlCaption.StyleName = "Title";
		xrlCaption.StylePriority.UseFont = false;
		xrlCaption.StylePriority.UseTextAlignment = false;
		xrlCaption.Text = "XXXX分析报告";
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
		topMarginBand1.HeightF = 56f;
		topMarginBand1.Name = "topMarginBand1";
		bottomMarginBand1.HeightF = 33f;
		bottomMarginBand1.Name = "bottomMarginBand1";
		formattingRule1.Condition = "[maIndex] >= 3";
		formattingRule1.DataMember = "poOrderListReportBom";
		formattingRule1.Formatting.BackColor = Color.Yellow;
		formattingRule1.Formatting.BorderColor = Color.Firebrick;
		formattingRule1.Formatting.Font = new Font("宋体", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 134);
		formattingRule1.Formatting.Visible = DefaultBoolean.True;
		formattingRule1.Name = "formattingRule1";
		xrControlStyle1.ForeColor = Color.Red;
		xrControlStyle1.Name = "xrControlStyle1";
		ReportFooter.Controls.AddRange(new XRControl[2] { xrtGroup, xrlFoot });
		ReportFooter.HeightF = 83.83334f;
		ReportFooter.Name = "ReportFooter";
		xrtGroup.AnchorVertical = VerticalAnchorStyles.Both;
		xrtGroup.Borders = BorderSide.All;
		xrtGroup.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrtGroup.LocationFloat = new PointFloat(0.9999911f, 0f);
		xrtGroup.Name = "xrtGroup";
		xrtGroup.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrtGroup.Rows.AddRange(new XRTableRow[1] { xrTableRow7 });
		xrtGroup.SizeF = new SizeF(735.9999f, 23.00002f);
		xrtGroup.StylePriority.UseBorderColor = false;
		xrtGroup.StylePriority.UseBorders = false;
		xrtGroup.StylePriority.UseFont = false;
		xrtGroup.StylePriority.UsePadding = false;
		xrtGroup.StylePriority.UseTextAlignment = false;
		xrtGroup.TextAlignment = TextAlignment.MiddleCenter;
		xrTableRow7.Cells.AddRange(new XRTableCell[17]
		{
			xrtcG0, xrtcG1, xrtcG2, xrtcG3, xrtcG4, xrtcG5, xrtcG6, xrtcG7, xrtcG8, xrtcG9,
			xrtcG10, xrtcG11, xrtcG12, xrtcG13, xrtcG14, xrtcG15, xrtcG16
		});
		xrTableRow7.Name = "xrTableRow7";
		xrTableRow7.Weight = 1.0;
		xrtcG0.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG0.CanGrow = false;
		xrtcG0.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG0.Name = "xrtcG0";
		xrtcG0.StyleName = "DataField";
		xrtcG0.StylePriority.UseBorders = false;
		xrtcG0.StylePriority.UseFont = false;
		xrtcG0.Text = "合计:";
		xrtcG0.Weight = 14.435955231278;
		xrtcG1.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG1.CanGrow = false;
		xrtcG1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG1.Name = "xrtcG1";
		xrtcG1.StyleName = "DataField";
		xrtcG1.StylePriority.UseBorders = false;
		xrtcG1.StylePriority.UseFont = false;
		xrtcG1.Weight = 18.6199251445723;
		xrtcG2.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG2.CanGrow = false;
		xrtcG2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG2.Name = "xrtcG2";
		xrtcG2.StylePriority.UseBorders = false;
		xrtcG2.StylePriority.UseFont = false;
		xrtcG2.Weight = 18.8085943080761;
		xrtcG3.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG3.CanGrow = false;
		xrtcG3.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakPara")
		});
		xrtcG3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG3.Name = "xrtcG3";
		xrtcG3.StylePriority.UseBorders = false;
		xrtcG3.StylePriority.UseFont = false;
		xRSummary.FormatString = "{0:F4}";
		xRSummary.Running = SummaryRunning.Report;
		xrtcG3.Summary = xRSummary;
		xrtcG3.Weight = 17.2461398901763;
		xrtcG4.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG4.CanGrow = false;
		xrtcG4.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakAmont")
		});
		xrtcG4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG4.Name = "xrtcG4";
		xrtcG4.StyleName = "DataField";
		xrtcG4.StylePriority.UseBorders = false;
		xrtcG4.StylePriority.UseFont = false;
		xRSummary2.FormatString = "{0:F4}";
		xRSummary2.Running = SummaryRunning.Report;
		xrtcG4.Summary = xRSummary2;
		xrtcG4.Weight = 16.3096613613176;
		xrtcG5.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG5.CanGrow = false;
		xrtcG5.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakArea")
		});
		xrtcG5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG5.Name = "xrtcG5";
		xrtcG5.StyleName = "DataField";
		xrtcG5.StylePriority.UseBorders = false;
		xrtcG5.StylePriority.UseFont = false;
		xRSummary3.FormatString = "{0:F4}";
		xRSummary3.Running = SummaryRunning.Report;
		xrtcG5.Summary = xRSummary3;
		xrtcG5.Weight = 16.9002484639485;
		xrtcG6.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG6.CanGrow = false;
		xrtcG6.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.Peakheight")
		});
		xrtcG6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG6.Name = "xrtcG6";
		xrtcG6.StyleName = "DataField";
		xrtcG6.StylePriority.UseBorders = false;
		xrtcG6.StylePriority.UseFont = false;
		xRSummary4.FormatString = "{0:F4}";
		xRSummary4.Running = SummaryRunning.Report;
		xrtcG6.Summary = xRSummary4;
		xrtcG6.Weight = 11.7618165475351;
		xrtcG7.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG7.CanGrow = false;
		xrtcG7.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakHalfheight")
		});
		xrtcG7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG7.Name = "xrtcG7";
		xrtcG7.StyleName = "DataField";
		xrtcG7.StylePriority.UseBorders = false;
		xrtcG7.StylePriority.UseFont = false;
		xRSummary5.FormatString = "{0:F4}";
		xRSummary5.Running = SummaryRunning.Report;
		xrtcG7.Summary = xRSummary5;
		xrtcG7.Weight = 18.4250767931525;
		xrtcG8.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG8.CanGrow = false;
		xrtcG8.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakAmontPer")
		});
		xrtcG8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG8.Name = "xrtcG8";
		xrtcG8.StyleName = "DataField";
		xrtcG8.StylePriority.UseBorders = false;
		xrtcG8.StylePriority.UseFont = false;
		xRSummary6.FormatString = "{0:F4}";
		xRSummary6.Running = SummaryRunning.Report;
		xrtcG8.Summary = xRSummary6;
		xrtcG8.Weight = 23.8251584111908;
		xrtcG9.CanGrow = false;
		xrtcG9.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakAreaPer")
		});
		xrtcG9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG9.Name = "xrtcG9";
		xrtcG9.StylePriority.UseFont = false;
		xRSummary7.FormatString = "{0:F4}";
		xRSummary7.Running = SummaryRunning.Report;
		xrtcG9.Summary = xRSummary7;
		xrtcG9.Weight = 18.5614512796755;
		xrtcG10.CanGrow = false;
		xrtcG10.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakheightPer")
		});
		xrtcG10.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG10.Name = "xrtcG10";
		xrtcG10.StylePriority.UseFont = false;
		xRSummary8.FormatString = "{0:F4}";
		xRSummary8.Running = SummaryRunning.Report;
		xrtcG10.Summary = xRSummary8;
		xrtcG10.Weight = 15.5564553396201;
		xrtcG11.CanGrow = false;
		xrtcG11.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakLV")
		});
		xrtcG11.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG11.Name = "xrtcG11";
		xrtcG11.StylePriority.UseFont = false;
		xRSummary9.Running = SummaryRunning.Report;
		xrtcG11.Summary = xRSummary9;
		xrtcG11.Weight = 13.1402386847837;
		xrtcG12.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG12.CanGrow = false;
		xrtcG12.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakTBPara")
		});
		xrtcG12.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG12.Name = "xrtcG12";
		xrtcG12.StyleName = "DataField";
		xrtcG12.StylePriority.UseBorders = false;
		xrtcG12.StylePriority.UseFont = false;
		xRSummary10.Running = SummaryRunning.Report;
		xrtcG12.Summary = xRSummary10;
		xrtcG12.Weight = 13.8347392682676;
		xrtcG13.CanGrow = false;
		xrtcG13.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakUTBPara")
		});
		xrtcG13.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG13.Name = "xrtcG13";
		xrtcG13.StylePriority.UseFont = false;
		xRSummary11.Running = SummaryRunning.Report;
		xrtcG13.Summary = xRSummary11;
		xrtcG13.Weight = 9.32479181878362;
		xrtcG14.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG14.CanGrow = false;
		xrtcG14.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakLPara")
		});
		xrtcG14.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG14.Name = "xrtcG14";
		xrtcG14.StyleName = "DataField";
		xrtcG14.StylePriority.UseBorders = false;
		xrtcG14.StylePriority.UseFont = false;
		xRSummary12.Running = SummaryRunning.Report;
		xrtcG14.Summary = xRSummary12;
		xrtcG14.Weight = 14.9766088108957;
		xrtcG15.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
		xrtcG15.CanGrow = false;
		xrtcG15.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeaktailPara")
		});
		xrtcG15.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG15.Name = "xrtcG15";
		xrtcG15.StyleName = "DataField";
		xrtcG15.StylePriority.UseBorders = false;
		xrtcG15.StylePriority.UseFont = false;
		xRSummary13.Running = SummaryRunning.Report;
		xrtcG15.Summary = xRSummary13;
		xrtcG15.Weight = 13.6365693233631;
		xrtcG16.CanGrow = false;
		xrtcG16.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "zufenTable.PeakFx")
		});
		xrtcG16.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
		xrtcG16.Name = "xrtcG16";
		xrtcG16.StylePriority.UseFont = false;
		xRSummary14.FormatString = "{0:F4}";
		xRSummary14.Running = SummaryRunning.Report;
		xrtcG16.Summary = xRSummary14;
		xrtcG16.Weight = 1.48468552114434;
		xrlFoot.AnchorVertical = VerticalAnchorStyles.Top;
		xrlFoot.Borders = BorderSide.All;
		xrlFoot.CanShrink = true;
		xrlFoot.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
		xrlFoot.LocationFloat = new PointFloat(0f, 27.08333f);
		xrlFoot.Multiline = true;
		xrlFoot.Name = "xrlFoot";
		xrlFoot.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrlFoot.SizeF = new SizeF(737f, 56.74999f);
		xrlFoot.StyleName = "Title";
		xrlFoot.StylePriority.UseBorders = false;
		xrlFoot.StylePriority.UseFont = false;
		xrlFoot.StylePriority.UseTextAlignment = false;
		xrlFoot.Text = "备注：按                 检验，浓度含量单位：g/l\r检测结果:                检验部门：\r检验员：                 审核员：";
		xrlFoot.TextAlignment = TextAlignment.MiddleLeft;
		base.Bands.AddRange(new Band[7] { Detail, pageHeaderBand1, pageFooterBand1, reportHeaderBand1, topMarginBand1, bottomMarginBand1, ReportFooter });
		base.DataMember = "zufenTable";
		base.DataSource = instockList1;
		base.FormattingRuleSheet.AddRange(new FormattingRule[2] { formattingRule1, fRulePeakAmont });
		base.Margins = new Margins(40, 40, 56, 33);
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
		((ISupportInitialize)xrTable3).EndInit();
		((ISupportInitialize)xrTable4).EndInit();
		((ISupportInitialize)xrtGroup).EndInit();
		((ISupportInitialize)this).EndInit();
	}
}
