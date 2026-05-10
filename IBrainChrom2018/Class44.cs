namespace IBrainChrom2018;

public class Class44
{
	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	public bool bool_4;

	public DetectorParse[] class78_0;

	public bool bool_5;

	public bool bool_6;

	public bool bool_7;

	public bool bool_8;

	public bool bool_9;

	public bool bool_10;

	public byte byte_0;

	public bool bool_11;

	public bool bool_12;

	public bool bool_13;

	public bool bool_14;

	public float[] float_0 = new float[6];

	public bool bool_15;

	public byte byte_1;

	public bool bool_16;

	public bool bool_17;

	public byte byte_2;

	public bool bool_18;

	public bool bool_19;

	public byte byte_3;

	public bool bool_20;

	public bool bool_21;

	public string method_0(bool bool_22)
	{
		string text = "6路实测温度: ";
		for (int i = 0; i < float_0.Length; i++)
		{
			text += string.Format("{0:0.0}" + ((i >= float_0.Length - 1) ? "" : ", "), float_0[i]);
		}
		text = text + "\r\n仪器状态1:   控温:" + (bool_1 ? "√" : "×") + ", 准备:" + (bool_12 ? "√" : "×") + ", TCP连接:" + (bool_14 ? "√" : "×") + ", USB:" + (bool_21 ? "√" : "×") + ", 数据分析:" + (bool_2 ? "√" : "×") + "\r\n仪器状态2:   参数错误:" + (bool_7 ? "√" : "×") + ", 进样器1超温:" + (bool_5 ? "√" : "×") + ", 进样器2超温:" + (bool_6 ? "√" : "×") + ", 柱炉超温:" + (bool_13 ? "√" : "×") + "\r\n             检测器1超温:" + (bool_3 ? "√" : "×") + ", 检测器2超温:" + (bool_4 ? "√" : "×") + ", 辅助超温:" + (bool_0 ? "√" : "×") + "\r\n\r\n程序升温:    初始:" + (bool_10 ? "√" : "×") + ", 升温:" + (bool_11 ? "√" : "×") + ", 保持:" + (bool_9 ? "√" : "×") + ", 降温:" + (bool_8 ? "√" : "×") + ", 阶数:" + byte_0 + "[程序升温时有效]\r\n时间程序1:   执行时间程序:" + (bool_16 ? "√" : "×") + ", 外部事件:" + (bool_15 ? "连接" : "断开") + ", 阶数:" + byte_1 + "[执行时间程序时有效]\r\n时间程序2:   执行时间程序:" + (bool_18 ? "√" : "×") + ", 外部事件:" + (bool_17 ? "连接" : "断开") + ", 阶数:" + byte_2 + "[执行时间程序时有效]\r\n时间程序3:   执行时间程序:" + (bool_20 ? "√" : "×") + ", 外部事件:" + (bool_19 ? "连接" : "断开") + ", 阶数:" + byte_3 + "[执行时间程序时有效]\r\n\r\n";
		if (bool_22)
		{
			text += "****[测试]****\r\n";
		}
		text = text + "检测器数:    " + class78_0.Length + "\r\n";
		for (int j = 0; j < class78_0.Length; j++)
		{
			string text2 = text;
			string[] array = new string[6]
			{
				text2,
				"检测器",
				(j + 1).ToString(),
				":     ",
				class78_0[j].ToString(),
				"\r\n"
			};
			text = string.Concat(array);
		}
		return text;
	}
}
