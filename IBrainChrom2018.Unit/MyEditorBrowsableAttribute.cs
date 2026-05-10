using System;

namespace IBrainChrom2018.Unit;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
public sealed class MyEditorBrowsableAttribute : Attribute
{
	private MyEditorBrowsableState mystate = MyEditorBrowsableState.Always;

	public MyEditorBrowsableState State => mystate;

	public MyEditorBrowsableAttribute()
	{
	}

	public MyEditorBrowsableAttribute(MyEditorBrowsableState state)
	{
		mystate = state;
	}
}
