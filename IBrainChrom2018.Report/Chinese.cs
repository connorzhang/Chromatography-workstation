using DevExpress.XtraBars.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraBars.Customization;

namespace IBrainChrom2018.Report;

public class Chinese
{
	public Chinese()
	{
		Localizer.Active = new XtraEditors_CN();
		GridLocalizer.Active = new XtraGrid_CN();
		BarLocalizer.Active = new XtraBar_CN();
		//BarLocalizer.Active.Customization = new XtraBarsCustomizationLocalization_CN();
		//BarLocalizer.Active.Customization = new XtraBarsCustomizationLocalization_CN();
		PreviewLocalizer.Active = new XtraPrinting_CN();
		ReportLocalizer.Active = new XtraReports_CN();
	}
}
