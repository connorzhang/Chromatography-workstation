using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class QCtrl : UserControl
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CallbackFun(int i);

	public delegate int RunningInstrument_CallBack(int mode, int SamplingTimes, float SamplingInterVal);

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public bool bLoading = true;

	public static QCtrl selfCtrl;

	public ulong cntTime1 = 0uL;

	public bool bStateAnalyze = false;

	public int iMode;

	private FormMainParam frmParam = FormMainParam.Create();

	public float fCarrierGasFlow;

	public float fH2GasFlow;

	public float fAirFlow;

	public double dLongitude;

	public double dLatitude;

	public float fTemprature;

	public float fHumidity;

	public float fPressEnvir;

	public ulong cntCycle = 0uL;

	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

	private byte[] CCITT_CRC8_DATA1 = new byte[320]
	{
		0, 94, 188, 226, 97, 63, 221, 131, 194, 156,
		126, 32, 163, 253, 31, 65, 157, 195, 33, 127,
		252, 162, 64, 30, 95, 1, 227, 189, 62, 96,
		130, 220, 35, 125, 159, 193, 66, 28, 254, 160,
		225, 191, 93, 3, 128, 222, 60, 98, 190, 224,
		2, 92, 223, 129, 99, 61, 124, 34, 192, 158,
		29, 67, 161, 255, 70, 24, 250, 164, 39, 121,
		155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
		219, 133, 103, 57, 186, 228, 6, 88, 25, 71,
		165, 251, 120, 38, 196, 154, 101, 59, 217, 135,
		4, 90, 184, 230, 167, 249, 27, 69, 198, 152,
		122, 36, 248, 166, 68, 26, 153, 199, 37, 123,
		58, 100, 134, 216, 91, 5, 231, 185, 140, 210,
		48, 110, 237, 179, 81, 15, 78, 16, 242, 172,
		47, 113, 147, 205, 17, 79, 173, 243, 112, 46,
		204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
		175, 241, 19, 77, 206, 144, 114, 44, 109, 51,
		209, 143, 12, 82, 176, 238, 50, 108, 142, 208,
		83, 13, 239, 177, 240, 174, 76, 18, 145, 207,
		45, 115, 202, 148, 118, 40, 171, 245, 23, 73,
		8, 86, 180, 234, 105, 55, 213, 139, 87, 9,
		235, 181, 54, 104, 138, 212, 149, 203, 41, 119,
		244, 170, 72, 22, 233, 183, 85, 11, 136, 214,
		52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
		116, 42, 200, 150, 21, 75, 169, 247, 182, 232,
		10, 84, 215, 137, 107, 53, 175, 241, 19, 77,
		206, 144, 114, 44, 109, 51, 209, 143, 12, 82,
		176, 238, 50, 108, 142, 208, 83, 13, 239, 177,
		240, 174, 76, 18, 145, 207, 45, 115, 202, 148,
		118, 40, 171, 245, 23, 73, 8, 86, 180, 234,
		105, 55, 213, 139, 87, 9, 235, 181, 54, 104,
		138, 212, 149, 203, 41, 119, 244, 170, 72, 22
	};

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button button1;

	private GroupBox groupBox2;

	private Timer timer1;

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void UpdateEnvTempHumiAndGPS(float temprature, float humidity, double longitude, double latitude);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void UpdateSingleSampleResult(int SampleNumber, float TotHydcarbonsVal, float TotHydcarbonsPearArea, float TotHydcarbonsKeepTime, float CH4Val, float CH4PearArea, float CH4KeepTime);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void UpdateRealTimeCurrent(double time, double current);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void UpdateTempratureFlow(float CataConvertTemp, float DecetorTemp, float H2GasFlow, float CarrierGasFlow, float AirFlow);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void reSignal(float fData);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern bool setStr(string strQt);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern bool showDialog(IntPtr parent);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern bool showQctrl(IntPtr parent);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void InitialDll();

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SetFunCallBack([MarshalAs(UnmanagedType.FunctionPtr)] CallbackFun pCallbackFun);

	[DllImport("interactionLib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void Set_RunningInstrumentSample_CallBACK(RunningInstrument_CallBack pCallbackFun);

	public QCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
		bLoading = false;
	}

	public void initForm()
	{
	}

	private void QCtrl_Load(object sender, EventArgs e)
	{
		InitialDll();
		CallbackFun funCallBack = HandleEvent;
		SetFunCallBack(funCallBack);
		showDialog(base.Handle);
		RunningInstrument_CallBack runningInstrument_CallBack = StaticRunningInstrumentCallBackH;
		try
		{
			Set_RunningInstrumentSample_CallBACK(StaticRunningInstrumentCallBackH);
		}
		catch
		{
			MessageBox.Show("注册RunningInstrument_CallBack错误");
		}
	}

	public static void HandleEvent(int iCmd)
	{
		LogMgr.Instance.LogWarning(iCmd.ToString());
		if (iCmd == 0)
		{
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	public void sendSignal(float fData)
	{
		UpdateRealTimeCurrent(1.0, fData);
	}

	public void setQtStr(string strQt)
	{
		setStr(strQt);
	}

	private void QCtrl_Resize(object sender, EventArgs e)
	{
	}

	public void disposeDates(byte[] byte1)
	{
		byte[] array = new byte[320];
		for (int i = 25; i < 320; i++)
		{
			array[i] = (byte)(byte1[i] ^ CCITT_CRC8_DATA1[i]);
		}
		string text = Encoding.ASCII.GetString(array, 25, 200);
		string[] array2 = text.Split(',');
		if (array2.Length > 4 && array2[0] == "$GNGGA")
		{
			double.TryParse(array2[2], out dLatitude);
			double.TryParse(array2[4], out dLongitude);
		}
		fTemprature = (float)(array[305] * 256 + array[306]) / 100f;
		fHumidity = (float)(array[307] * 256 + array[308]) / 100f;
		fPressEnvir = (float)((array[302] << 16) | (array[303] << 8) | array[304]) / 1000f;
		UpdateEnvTempHumiAndGPS(fTemprature, fHumidity, dLongitude, dLatitude);
		float num = (float)(array[309] * 256 + array[310]) / 100f - 12.9f;
		if (num < 0f)
		{
			num = 0f;
		}
		num = (float)(array[311] * 256 + array[312]) / 100f - 12.9f;
		if (num < 0f)
		{
			num = 0f;
		}
		num = (float)(array[313] * 256 + array[314]) / 100f - 12.9f;
		if (num < 0f)
		{
			num = 0f;
		}
	}

	public void ReadTempratureLY(Class44 class44_0, float fuzhu3, float zhulu2)
	{
		UpdateTempratureFlow(class44_0.float_0[2], class44_0.float_0[1], fH2GasFlow, fCarrierGasFlow, fAirFlow);
	}

	public static int StaticRunningInstrumentCallBackH(int mode, int SamplingTimes, float SamplingInterVal)
	{
		selfCtrl.RunningInstrumentCallBackH(mode, SamplingTimes, SamplingInterVal);
		return 0;
	}

	public int RunningInstrumentCallBackH(int mode, int SamplingTimes, float SamplingInterVal)
	{
		iMode = mode;
		switch (mode)
		{
		case 0:
			cdlMgr.CurrentTcpServerSocket?.SendCmd(18);
			break;
		case 1:
			bStateAnalyze = true;
			cntCycle = 0uL;
			timer1.Enabled = true;
			break;
		case 2:
			bStateAnalyze = true;
			cntCycle = 0uL;
			timer1.Enabled = true;
			break;
		}
		return 0;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		cntTime1++;
		if (bStateAnalyze)
		{
			if (iMode == 0)
			{
				stateSwitch();
			}
			else if (iMode == 1)
			{
				chuiSaoMode();
			}
			else if (iMode == 2)
			{
				cuiHua();
			}
		}
		else
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 0);
		}
	}

	public void stateSwitch()
	{
	}

	public void chuiSaoMode()
	{
		cntCycle++;
		if (cntCycle == 2)
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 29);
		}
		else if (cntCycle == 600)
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 21);
		}
		else if (cntCycle == 1200)
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 29);
		}
		else if (cntCycle == 1800)
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 21);
		}
		else if (cntCycle == 2400)
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 29);
		}
		else if (cntCycle == 3000)
		{
			cdlMgr.currentTcpServerMgrSendEPCCmd(84, 0);
			timer1.Enabled = false;
			bStateAnalyze = false;
			cntCycle = 0uL;
		}
	}

	public void cuiHua()
	{
		cntCycle++;
		if (cntCycle == 1)
		{
			MethodInvoker method = delegate
			{
				InsDeviceCtrl.self.dgtempControl.Rows[2].Cells[2].Value = lythcParamMgr.fCatalytic.ToString("0.0");
				cdlMgr.currentTcpServerMgrSendCmd(8);
			};
			Invoke(method);
		}
		else if (cntCycle == 12000)
		{
			MethodInvoker method2 = delegate
			{
				InsDeviceCtrl.self.dgtempControl.Rows[1].Cells[2].Value = lythcParamMgr.fSample.ToString("0.0");
				cdlMgr.currentTcpServerMgrSendCmd(8);
			};
			Invoke(method2);
			timer1.Enabled = false;
			bStateAnalyze = false;
			cntCycle = 0uL;
		}
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
		this.components = new System.ComponentModel.Container();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(467, 313);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "groupBox1";
		this.button1.Location = new System.Drawing.Point(584, 329);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.groupBox2.Location = new System.Drawing.Point(515, 3);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(291, 152);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "groupBox2";
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.groupBox1);
		base.Name = "QCtrl";
		base.Size = new System.Drawing.Size(662, 355);
		base.Load += new System.EventHandler(QCtrl_Load);
		base.Resize += new System.EventHandler(QCtrl_Resize);
		base.ResumeLayout(false);
	}
}
