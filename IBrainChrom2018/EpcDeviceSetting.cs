using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class EpcDeviceSetting : IArrayBase
{
	public byte gasType;

	public float initTime;

	public byte ctrlModel;

	public List<TemperSettingRow> tempSettingTable = IArrayBase.NewArray<TemperSettingRow>(4);

	public byte splitRatio;

	public float pressureData;

	public float chromColLenth;

	public float chromColDiameter;

	public byte[] setData = new byte[8];
}
