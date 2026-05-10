using DevExpress.XtraGrid.Localization;

namespace IBrainChrom2018.Report;

public class XtraGrid_CN : GridLocalizer
{
	public override string Language => "简体中文";

	public override string GetLocalizedString(GridStringId id)
	{
		return id switch
		{
			GridStringId.CardViewNewCard => "新卡片", 
			GridStringId.CardViewQuickCustomizationButton => "自定义格式", 
			GridStringId.CardViewQuickCustomizationButtonFilter => "筛选", 
			GridStringId.CardViewQuickCustomizationButtonSort => "排序", 
			GridStringId.ColumnViewExceptionMessage => "是否确定修改?", 
			GridStringId.CustomFilterDialog2FieldCheck => "字段", 
			GridStringId.CustomFilterDialogCancelButton => "取消", 
			GridStringId.CustomFilterDialogCaption => "条件为:", 
			GridStringId.CustomFilterDialogFormCaption => "清除筛选条件(&L)", 
			GridStringId.CustomFilterDialogOkButton => "确定(&O)", 
			GridStringId.CustomFilterDialogRadioAnd => "和(&A)", 
			GridStringId.CustomFilterDialogRadioOr => "或者(&O)", 
			GridStringId.CustomizationBands => "分区", 
			GridStringId.CustomizationCaption => "自定义显示字段", 
			GridStringId.CustomizationColumns => "列", 
			GridStringId.FileIsNotFoundError => "文件{0}没找到!", 
			GridStringId.GridGroupPanelText => "拖曳一列页眉在此进行排序", 
			GridStringId.GridNewRowText => "单击这里新增一行", 
			GridStringId.GridOutlookIntervals => "一个月以上;上个月;三周前;两周前;上周;;;;;;;;昨天;今天;明天; ;;;;;;;下周;两周后;三周后;下个月;一个月之后;", 
			GridStringId.MenuColumnBestFit => "自动调整字段宽度", 
			GridStringId.MenuColumnBestFitAllColumns => "自动调整所有字段宽度", 
			GridStringId.MenuColumnClearFilter => "清除筛选条件", 
			GridStringId.MenuColumnColumnCustomization => "显示/隐藏字段", 
			GridStringId.MenuColumnFilter => "筛选", 
			GridStringId.MenuColumnGroup => "按此列分组", 
			GridStringId.MenuColumnGroupBox => "分组区", 
			GridStringId.MenuColumnSortAscending => "升序排序", 
			GridStringId.MenuColumnSortDescending => "降序排序", 
			GridStringId.MenuColumnUnGroup => "取消分组", 
			GridStringId.MenuFooterAverage => "平均", 
			GridStringId.MenuFooterAverageFormat => "平均={0:#.##}", 
			GridStringId.MenuFooterCount => "计数", 
			GridStringId.MenuFooterCountFormat => "{0}", 
			GridStringId.MenuFooterMax => "最大值", 
			GridStringId.MenuFooterMaxFormat => "最大值={0}", 
			GridStringId.MenuFooterMin => "最小", 
			GridStringId.MenuFooterMinFormat => "最小值={0}", 
			GridStringId.MenuFooterNone => "没有", 
			GridStringId.MenuFooterSum => "合计", 
			GridStringId.MenuFooterSumFormat => "求和={0:#.##}", 
			GridStringId.MenuGroupPanelClearGrouping => "取消所有分组", 
			GridStringId.MenuGroupPanelFullCollapse => "收缩全部分组", 
			GridStringId.MenuGroupPanelFullExpand => "展开全部分组", 
			GridStringId.PopupFilterAll => "(所有)", 
			GridStringId.PopupFilterBlanks => "(空值)", 
			GridStringId.PopupFilterCustom => "(自定义)", 
			GridStringId.PopupFilterNonBlanks => "(非空值)", 
			GridStringId.PrintDesignerBandedView => "打印设置(区域模式)", 
			GridStringId.PrintDesignerBandHeader => "区标题", 
			GridStringId.PrintDesignerCardView => "打印设置(卡片模式)", 
			GridStringId.PrintDesignerDescription => "为当前视图设置不同的打印选项.", 
			GridStringId.PrintDesignerGridView => "打印设置(表格模式)", 
			GridStringId.WindowErrorCaption => "错误", 
			_ => base.GetLocalizedString(id), 
		};
	}
}
