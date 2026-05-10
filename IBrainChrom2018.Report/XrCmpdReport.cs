using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using IBrainChrom2018._205.Report.instockListTableAdapters;

namespace IBrainChrom2018.Report;

public class XrCmpdReport : XtraReport
{
	public static int iPicWidth = 353;

	public static int iPicHeight = 360;

	private IContainer components = null;

	private DetailBand Detail;

	private instockList instockList1;

	private GroupFooterBand groupFooterBand1;

	private XRTableRow xrTableRow1;

	private XRTableCell xrTableCell1;

	private XRTableCell xrTableCell2;

	private XRTableCell xrTableCell3;

	private XRTableRow xrTableRow2;

	private XRTableCell xrTableCell4;

	private XRTableCell xrTableCell5;

	private XRTableCell xrTableCell6;

	private PageFooterBand pageFooterBand1;

	private XRControlStyle Title;

	private XRControlStyle FieldCaption;

	private XRControlStyle PageInfo;

	private XRControlStyle DataField;

	private TopMarginBand topMarginBand1;

	private BottomMarginBand bottomMarginBand1;

	private FormattingRule formattingRule1;

	private XRControlStyle xrControlStyle1;

	private storgeOrderAdapter poOrderListReportBomTableAdapter1;

	private ReportFooterBand ReportFooter;

	private XRTable xrTable2;

	private XRTableRow xrTableRow4;

	private XRTableCell xrTableCell13;

	private XRTableCell xrTableCell18;

	private XRTableCell xrTableCell14;

	private XRTableRow xrTableRow5;

	private XRTableCell xrTableCell20;

	private XRTableCell xrTableCell21;

	private XRTableCell xrTableCell22;

	public XRPictureBox xrPictureBox1;

	private PageHeaderBand PageHeader;

	public XRLabel xrlCaption;

	private XRTableCell xrTableCell10;

	private XRTableCell xrTableCell11;

	private XRTableRow xrTableRow3;

	private XRTableCell xrTableCell7;

	private XRTableCell xrTableCell12;

	private instockList instockList2;

	public XrCmpdReport()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	public void SetOrderType(Chromatogram chromatogram, string strFileName, instockList stockDataSet)
	{
		PrintPara pPara = chromatogram.PPara;
		if (chromatogram.caliGnl == null)
		{
			return;
		}
		chromatogram.caliGnl.CalculateFunc(appendLink: false);
		CaliGnl caliGnl = chromatogram.caliGnl;
		Compound[] cmpds = caliGnl.cmpds;
		CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
		Size size = new Size(iPicWidth, iPicHeight);
		RectangleF rectangleF = new RectangleF(0f, 0f, size.Width, size.Height);
		foreach (Compound compound in cmpds)
		{
			if (compound.levels == null || compound.levels.Length == 0)
			{
				break;
			}
			Level level = compound.levels[0];
			Image image = new Bitmap(iPicWidth, iPicHeight);
			Graphics graphics = Graphics.FromImage(image);
			string string_ = Class49.MesureUnit() + ".s";
			if (compound.cmpdInfo.respStyle == RespStyle.Height)
			{
				string_ = Class49.MesureUnit();
			}
			cmpdDisplay.rcPage = rectangleF;
			cmpdDisplay.dskRC = rectangleF;
			cmpdDisplay.SetCompound(compound, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
			cmpdDisplay.Draw(graphics, erase: true);
			graphics.Dispose();
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Jpeg);
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			memoryStream.Close();
			level.response = (float.IsNaN(level.response) ? 0f : level.response);
			level.amount = (float.IsNaN(level.amount) ? 0f : level.amount);
			level.respFactor = (float.IsNaN(level.respFactor) ? 0f : level.respFactor);
			compound.eFunc.corrFactor = (double.IsNaN(compound.eFunc.corrFactor) ? 0.0 : compound.eFunc.corrFactor);
			stockDataSet.cmpdTable.AddcmpdTableRow((decimal)level.response, (decimal)level.amount, (decimal)level.respFactor, array, compound.eFunc.GetEquationStr(), (decimal)compound.eFunc.corrFactor);
		}
		cmpdDisplay = null;
		base.DataAdapter = null;
		base.DataSource = stockDataSet;
		base.DataMember = "cmpdTable";
	}

	public static XrCmpdReport CreateReport(Chromatogram chromatogram, string strFileName, instockList stockDataSet)
	{
		XrCmpdReport xrCmpdReport = new XrCmpdReport();
		xrCmpdReport.SetOrderType(chromatogram, strFileName, stockDataSet);
		return xrCmpdReport;
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
		xrTable2 = new XRTable();
		xrTableRow4 = new XRTableRow();
		xrTableCell13 = new XRTableCell();
		xrTableCell18 = new XRTableCell();
		xrTableCell14 = new XRTableCell();
		xrTableCell10 = new XRTableCell();
		xrTableRow5 = new XRTableRow();
		xrTableCell20 = new XRTableCell();
		xrTableCell21 = new XRTableCell();
		xrTableCell22 = new XRTableCell();
		xrTableCell11 = new XRTableCell();
		xrTableRow3 = new XRTableRow();
		xrTableCell7 = new XRTableCell();
		xrTableCell12 = new XRTableCell();
		xrPictureBox1 = new XRPictureBox();
		instockList1 = new instockList();
		groupFooterBand1 = new GroupFooterBand();
		xrTableRow1 = new XRTableRow();
		xrTableCell1 = new XRTableCell();
		xrTableCell2 = new XRTableCell();
		xrTableCell3 = new XRTableCell();
		xrTableRow2 = new XRTableRow();
		xrTableCell4 = new XRTableCell();
		xrTableCell5 = new XRTableCell();
		xrTableCell6 = new XRTableCell();
		pageFooterBand1 = new PageFooterBand();
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
		PageHeader = new PageHeaderBand();
		xrlCaption = new XRLabel();
		instockList2 = new instockList();
		((ISupportInitialize)xrTable2).BeginInit();
		((ISupportInitialize)instockList1).BeginInit();
		((ISupportInitialize)instockList2).BeginInit();
		((ISupportInitialize)this).BeginInit();
		Detail.Borders = BorderSide.None;
		Detail.Controls.AddRange(new XRControl[2] { xrTable2, xrPictureBox1 });
		Detail.Font = new Font("宋体", 10f);
		Detail.HeightF = 544.7499f;
		Detail.KeepTogether = true;
		Detail.KeepTogetherWithDetailReports = true;
		Detail.MultiColumn.ColumnCount = 2;
		Detail.MultiColumn.Layout = ColumnLayout.AcrossThenDown;
		Detail.MultiColumn.Mode = MultiColumnMode.UseColumnCount;
		Detail.Name = "Detail";
		Detail.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		Detail.SnapLinePadding = new PaddingInfo(3, 3, 3, 3, 100f);
		Detail.StylePriority.UseBorders = false;
		Detail.TextAlignment = TextAlignment.MiddleCenter;
		xrTable2.Borders = BorderSide.All;
		xrTable2.Font = new Font("宋体", 8f);
		xrTable2.LocationFloat = new PointFloat(2.999997f, 0f);
		xrTable2.Name = "xrTable2";
		xrTable2.Padding = new PaddingInfo(0, 0, 0, 0, 96f);
		xrTable2.Rows.AddRange(new XRTableRow[3] { xrTableRow4, xrTableRow5, xrTableRow3 });
		xrTable2.SizeF = new SizeF(365f, 69f);
		xrTableRow4.Cells.AddRange(new XRTableCell[4] { xrTableCell13, xrTableCell18, xrTableCell14, xrTableCell10 });
		xrTableRow4.Name = "xrTableRow4";
		xrTableRow4.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrTableRow4.Weight = 0.5;
		xrTableCell13.Font = new Font("宋体", 8f);
		xrTableCell13.Name = "xrTableCell13";
		xrTableCell13.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrTableCell13.Text = "响应";
		xrTableCell13.Weight = 0.335472361437499;
		xrTableCell18.Font = new Font("宋体", 8f);
		xrTableCell18.Name = "xrTableCell18";
		xrTableCell18.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrTableCell18.Text = "浓度";
		xrTableCell18.Weight = 0.255001298526105;
		xrTableCell14.Font = new Font("宋体", 8f);
		xrTableCell14.Name = "xrTableCell14";
		xrTableCell14.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrTableCell14.Text = "因子";
		xrTableCell14.Weight = 0.217576969856207;
		xrTableCell10.Name = "xrTableCell10";
		xrTableCell10.Text = "相关系数";
		xrTableCell10.Weight = 0.1400013171752496;
		xrTableRow5.Cells.AddRange(new XRTableCell[4] { xrTableCell20, xrTableCell21, xrTableCell22, xrTableCell11 });
		xrTableRow5.Name = "xrTableRow5";
		xrTableRow5.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrTableRow5.Weight = 0.5;
		xrTableCell20.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "cmpdTable.response")
		});
		xrTableCell20.Font = new Font("宋体", 8f);
		xrTableCell20.Name = "xrTableCell20";
		xrTableCell20.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrTableCell20.Weight = 0.335472361437499;
		xrTableCell20.WordWrap = false;
		xrTableCell21.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "cmpdTable.amount", "{0:f4}")
		});
		xrTableCell21.Font = new Font("宋体", 8f);
		xrTableCell21.Name = "xrTableCell21";
		xrTableCell21.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrTableCell21.Weight = 0.255001298526105;
		xrTableCell21.WordWrap = false;
		xrTableCell22.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "cmpdTable.respFactor", "{0:f4}")
		});
		xrTableCell22.Font = new Font("宋体", 8f);
		xrTableCell22.Name = "xrTableCell22";
		xrTableCell22.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrTableCell22.Weight = 0.217576969856207;
		xrTableCell22.WordWrap = false;
		xrTableCell11.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "cmpdTable.corrFactor", "{0:f7}")
		});
		xrTableCell11.Name = "xrTableCell11";
		xrTableCell11.NullValueText = "-";
		xrTableCell11.Weight = 0.1400013171752496;
		xrTableRow3.Cells.AddRange(new XRTableCell[2] { xrTableCell7, xrTableCell12 });
		xrTableRow3.Name = "xrTableRow3";
		xrTableRow3.Weight = 0.5;
		xrTableCell7.Name = "xrTableCell7";
		xrTableCell7.Text = "方程";
		xrTableCell7.Weight = 0.335472438050659;
		xrTableCell12.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Text", null, "cmpdTable.EquationV")
		});
		xrTableCell12.Name = "xrTableCell12";
		xrTableCell12.Weight = 0.6125795089444017;
		xrTableCell12.WordWrap = false;
		xrPictureBox1.Borders = BorderSide.All;
		xrPictureBox1.DataBindings.AddRange(new XRBinding[1]
		{
			new XRBinding("Image", null, "cmpdTable.picture")
		});
		xrPictureBox1.LocationFloat = new PointFloat(3.000005f, 68.99999f);
		xrPictureBox1.Name = "xrPictureBox1";
		xrPictureBox1.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
		xrPictureBox1.SizeF = new SizeF(365f, 416.5f);
		instockList1.DataSetName = "instockList";
		instockList1.EnforceConstraints = false;
		instockList1.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		groupFooterBand1.HeightF = 1f;
		groupFooterBand1.Name = "groupFooterBand1";
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
		pageFooterBand1.HeightF = 14.41663f;
		pageFooterBand1.Name = "pageFooterBand1";
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
		topMarginBand1.HeightF = 0f;
		topMarginBand1.Name = "topMarginBand1";
		bottomMarginBand1.HeightF = 20f;
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
		poOrderListReportBomTableAdapter1.ClearBeforeFill = true;
		ReportFooter.HeightF = 0f;
		ReportFooter.Name = "ReportFooter";
		PageHeader.Controls.AddRange(new XRControl[1] { xrlCaption });
		PageHeader.HeightF = 24.04167f;
		PageHeader.Name = "PageHeader";
		xrlCaption.LocationFloat = new PointFloat(0f, 0f);
		xrlCaption.Name = "xrlCaption";
		xrlCaption.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
		xrlCaption.SizeF = new SizeF(747f, 23f);
		xrlCaption.StyleName = "Title";
		xrlCaption.StylePriority.UseTextAlignment = false;
		xrlCaption.TextAlignment = TextAlignment.MiddleCenter;
		instockList2.DataSetName = "instockList";
		instockList2.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		base.Bands.AddRange(new Band[6] { Detail, pageFooterBand1, topMarginBand1, bottomMarginBand1, ReportFooter, PageHeader });
		base.DataMember = "cmpdTable";
		base.DataSource = instockList1;
		base.FormattingRuleSheet.AddRange(new FormattingRule[1] { formattingRule1 });
		base.Margins = new Margins(40, 40, 0, 20);
		base.PageHeight = 1169;
		base.PageWidth = 827;
		base.PaperKind = PaperKind.A4;
		base.Scripts.OnDataSourceDemanded = "XrInStockList_DataSourceDemanded";
		base.ScriptsSource = "\r\nprivate void XrInStockList_DataSourceDemanded(object sender, System.EventArgs e) \r\n{\r\n\r\n}\r\n";
		base.StyleSheet.AddRange(new XRControlStyle[5] { Title, FieldCaption, PageInfo, DataField, xrControlStyle1 });
		base.Version = "13.2";
		((ISupportInitialize)xrTable2).EndInit();
		((ISupportInitialize)instockList1).EndInit();
		((ISupportInitialize)instockList2).EndInit();
		((ISupportInitialize)this).EndInit();
	}
}
