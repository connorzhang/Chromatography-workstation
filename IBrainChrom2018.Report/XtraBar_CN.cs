using DevExpress.XtraBars.Localization;

namespace IBrainChrom2018.Report;

public class XtraBar_CN : BarLocalizer
{
	public override string Language => "简体中文";

	public override string GetLocalizedString(BarString id)
	{
		return id switch
		{
			BarString.AddOrRemove => "新增或删除按钮(&A)", 
			BarString.CustomizeButton => "自定义(&C)...", 
			BarString.CustomizeWindowCaption => "自定义", 
			BarString.MenuAnimationFade => "减弱", 
			BarString.MenuAnimationNone => "空", 
			BarString.MenuAnimationRandom => "任意", 
			BarString.MenuAnimationSlide => "滑动", 
			BarString.MenuAnimationSystem => "(系统默认值)", 
			BarString.MenuAnimationUnfold => "展开", 
			BarString.NewToolbarCaption => "新建工具栏", 
			BarString.None => "", 
			BarString.RenameToolbarCaption => "重新命名", 
			BarString.ResetBar => "是否确实要重置对 '{0}' 工具栏所作的修改？", 
			BarString.ResetBarCaption => "自定义", 
			BarString.ResetButton => "重设工具栏(&R)", 
			BarString.ToolBarMenu => "重设(&R)$删除(&D)$!命名(&N)$!标准(&L)$总使用文字(&T)$在菜单中只用文字(&O)$图像与文本(&A)$!开始一组(&G)$常用的(&M)", 
			BarString.ToolbarNameCaption => "工具栏名称(&T):", 
			_ => base.GetLocalizedString(id), 
		};
	}
}
