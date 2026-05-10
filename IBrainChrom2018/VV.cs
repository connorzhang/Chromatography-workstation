using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class VV
{
	public VS vs_0 = VS.None;

	public int index = -1;

	public float value = -1f;

	public float X;

	public float Y;

	public void LoadFromObject(VV vv_0)
	{
		vs_0 = vv_0.vs_0;
		index = vv_0.index;
		X = vv_0.X;
		Y = vv_0.Y;
		value = vv_0.value;
	}

	public void Clone(VV vv_0)
	{
		vs_0 = vv_0.vs_0;
		index = vv_0.index;
		X = vv_0.X;
		Y = vv_0.Y;
		value = vv_0.value;
	}

	public VV Copy()
	{
		VV vV = new VV();
		vV.vs_0 = vs_0;
		vV.index = index;
		vV.X = X;
		vV.Y = Y;
		vV.value = value;
		return vV;
	}
}
