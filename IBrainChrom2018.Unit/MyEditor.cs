using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace IBrainChrom2018.Unit;

public class MyEditor : UITypeEditor
{
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.Modal;
	}

	public override bool GetPaintValueSupported(ITypeDescriptorContext context)
	{
		return false;
	}

	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		return value;
	}
}
