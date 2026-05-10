using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

internal static class Program
{
	public static string ApplicationName = Process.GetCurrentProcess().ProcessName;

	public static Mutex Run;

	[DllImport("User32.dll")]
	private static extern int SendMessage(int int_1, int int_2, int int_3, ref COPYDATASTRUCT copydatastruct_0);

	[DllImport("User32.dll")]
	private static extern int FindWindow(string string_0, string string_1);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int add(int i);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void call(int i);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern double linearFit(double[] x, double[] y, int length, double[] factor);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern double linearFitPass(double[] x, double[] y, int length, double[] factor);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern double TestFoumular(int id, double value);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern byte TestFoumularEncrypt(int id, byte value);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern double RSDCalculate(float fAverage, float[] arrValue, int length);

	[DllImport("User32.dll")]
	private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

	[DllImport("User32.dll")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	[STAThread]
	private static void Main(string[] args)
	{
		string string_ = "";
		bool createdNew = false;
		Process runingInstance = GetRuningInstance();
		Run = new Mutex(initiallyOwned: true, Application.ProductName, out createdNew);
		if (runingInstance != null)
		{
			ShowWindowAsync(runingInstance.MainWindowHandle, 1);
			SetForegroundWindow(runingInstance.MainWindowHandle);
		}
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		LogMgr logMgr = LogMgr.Create();
		logMgr.Write2RunLog("Program Start" + AssemblyInfoCfg.SoftVersion());
		CtrlLangPS.Create();
		SystemParam systemParam = SystemParam.Create();
		logMgr.Write2RunLog("Program Loaded sysParam");
		AssemblyInfoCfg assemblyInfoCfg = AssemblyInfoCfg.Create();
		SystemDictionaryList systemDictionaryList = SystemDictionaryList.Create();
		DetectorParam detectorParam = DetectorParam.Create();
		logMgr.Write2RunLog("Program Loaded detector");
		if (DogFeturlMgr.LicencedGMP())
		{
			Logon logon = new Logon();
			if (logon.ShowDialog() != DialogResult.OK)
			{
				Application.Exit();
				return;
			}
		}
		FormMain.string_0 = string_;
		Application.Run(new FormMain());
	}

	private static string smethod_0()
	{
		long num = 1L;
		byte[] array = Guid.NewGuid().ToByteArray();
		foreach (byte b in array)
		{
			num *= b + 1;
		}
		return $"{num - DateTime.Now.Ticks:x}";
	}

	public static void WriteLine(string strValue, params string[] strParam)
	{
		LogMgr logMgr = LogMgr.Create();
		logMgr.Write2RunLog(strValue, strParam);
	}

	public static void StartWatchDog()
	{
		IpcThread ipcThread = IpcThread.CreateIPC();
		ipcThread.StartIPC();
		string text = Application.StartupPath.ToString() + "\\WatchDog.exe";
		if (File.Exists(text))
		{
			Process.Start(text);
		}
	}

	private static Process GetRuningInstance()
	{
		Process currentProcess = Process.GetCurrentProcess();
		Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
		Process[] array = processesByName;
		foreach (Process process in array)
		{
			if (process.Id != currentProcess.Id && Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
			{
				return process;
			}
		}
		return null;
	}

	public static float getCharacteristic(string gasName, float amount, int index)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		switch (gasName)
		{
		case "1，3-丁二稀":
			switch (index)
			{
			case 1:
				return 113.51f * amount;
			case 2:
				return 107.47f * amount;
			case 3:
				return 1.6932f * amount;
			case 4:
				num3 = 1.6932f * amount;
				return 1.29222f * num3;
			case 5:
				return 27112f * amount;
			case 6:
				return 25669f * amount;
			case 7:
				return 54.0916f * amount;
			case 8:
				return 0.1844f * amount;
			}
			break;
		case "异丁稀":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "二甲醚":
			switch (index)
			{
			case 1:
				return 63.16f * amount;
			case 2:
				return 58.8f * amount;
			case 3:
				return 1.592f * amount;
			case 4:
				num3 = 1.592f * amount;
				return 1.29222f * num3;
			case 5:
				return 15086f * amount;
			case 6:
				return 14044f * amount;
			case 7:
				return 46.069f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "CH3OCH3":
			switch (index)
			{
			case 1:
				return 63.16f * amount;
			case 2:
				return 58.8f * amount;
			case 3:
				return 1.592f * amount;
			case 4:
				num3 = 2.0572f * amount;
				return 1.29222f * num3;
			case 5:
				return 15086f * amount;
			case 6:
				return 14044f * amount;
			case 7:
				return 46.069f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "He":
			switch (index)
			{
			case 1:
				return 0f;
			case 2:
				return 0f;
			case 3:
				return 0.138f * amount;
			case 4:
				num3 = 0.138f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f;
			case 6:
				return 0f;
			case 7:
				return 4.003f * amount;
			case 8:
				return 0.0006f * amount;
			}
			break;
		case "Ar":
			switch (index)
			{
			case 1:
				return 0f;
			case 2:
				return 0f;
			case 3:
				return 1.379f * amount;
			case 4:
				num3 = 1.379f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f;
			case 6:
				return 0f;
			case 7:
				return 39.948f * amount;
			case 8:
				return 0.0316f * amount;
			}
			break;
		case "H2":
			switch (index)
			{
			case 1:
				return 12.789f * amount;
			case 2:
				return 10.779f * amount;
			case 3:
				return 0.07f * amount;
			case 4:
				num3 = 0.07f * amount;
				return 1.29222f * num3;
			case 5:
				return 3054.6f * amount;
			case 6:
				return 2574.52f * amount;
			case 7:
				return 2f * amount;
			case 8:
				return -0.004f * amount;
			}
			break;
		case "O2":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1.105f * amount;
			case 4:
				num3 = 1.105f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 32f * amount;
			case 8:
				return 0.0316f * amount;
			}
			break;
		case "N2":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 0.967f * amount;
			case 4:
				num3 = 0.967f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28f * amount;
			case 8:
				return 0.0224f * amount;
			}
			break;
		case "CO":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 0.967f * amount;
			case 4:
				num3 = 0.967f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.01f * amount;
			case 8:
				return 0.0265f * amount;
			}
			break;
		case "CO2":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1.52f * amount;
			case 4:
				num3 = 1.52f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 44.01f * amount;
			case 8:
				return 0.0819f * amount;
			}
			break;
		case "H2S":
			switch (index)
			{
			case 1:
				return 25.141f * amount;
			case 2:
				return 23.13f * amount;
			case 3:
				return 1.177f * amount;
			case 4:
				num3 = 1.177f * amount;
				return 1.29222f * num3;
			case 5:
				return 6004.825f * amount;
			case 6:
				return 5524.505f * amount;
			case 7:
				return 34.076f * amount;
			case 8:
				return 0.1f * amount;
			}
			break;
		case "H2O":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 0.622f * amount;
			case 4:
				num3 = 0.622f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 18.015f * amount;
			case 8:
				return 0.2646f * amount;
			}
			break;
		case "Air":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1f * amount;
			case 4:
				num3 = 1f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "Air+CH4":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1f * amount;
			case 4:
				num3 = 1f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "CH4":
			switch (index)
			{
			case 1:
				return 39.829f * amount;
			case 2:
				return 35.807f * amount;
			case 3:
				return 0.554f * amount;
			case 4:
				num3 = 0.554f * amount;
				return 1.29222f * num3;
			case 5:
				return 9512.993f * amount;
			case 6:
				return 8552.355f * amount;
			case 7:
				return 16.043f * amount;
			case 8:
				return 0.049f * amount;
			}
			break;
		case "C1":
			switch (index)
			{
			case 1:
				return 39.829f * amount;
			case 2:
				return 35.807f * amount;
			case 3:
				return 0.554f * amount;
			case 4:
				num3 = 0.554f * amount;
				return 1.29222f * num3;
			case 5:
				return 9512.993f * amount;
			case 6:
				return 8552.355f * amount;
			case 7:
				return 16.043f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "C2H6":
			switch (index)
			{
			case 1:
				return 69.759f * amount;
			case 2:
				return 63.727f * amount;
			case 3:
				return 1.038f * amount;
			case 4:
				num3 = 1.038f * amount;
				return 1.29222f * num3;
			case 5:
				return 16661.652f * amount;
			case 6:
				return 15220.933f * amount;
			case 7:
				return 30.07f * amount;
			case 8:
				return 0.1f * amount;
			}
			break;
		case "C2H6+C2H4":
			switch (index)
			{
			case 1:
				return 69.759f * amount;
			case 2:
				return 63.727f * amount;
			case 3:
				return 1.038f * amount;
			case 4:
				num3 = 1.038f * amount;
				return 1.29222f * num3;
			case 5:
				return 16661.652f * amount;
			case 6:
				return 15220.933f * amount;
			case 7:
				return 30.07f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "C2":
			switch (index)
			{
			case 1:
				return 69.759f * amount;
			case 2:
				return 63.727f * amount;
			case 3:
				return 1.038f * amount;
			case 4:
				num3 = 1.038f * amount;
				return 1.29222f * num3;
			case 5:
				return 16661.652f * amount;
			case 6:
				return 15220.933f * amount;
			case 7:
				return 30.07f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "C3H8":
			switch (index)
			{
			case 1:
				return 99.264f * amount;
			case 2:
				return 91.223f * amount;
			case 3:
				return 1.523f * amount;
			case 4:
				num3 = 1.523f * amount;
				return 1.29222f * num3;
			case 5:
				return 23708.799f * amount;
			case 6:
				return 21788.238f * amount;
			case 7:
				return 44.097f * amount;
			case 8:
				return 0.1453f * amount;
			}
			break;
		case "C3":
			switch (index)
			{
			case 1:
				return 99.264f * amount;
			case 2:
				return 91.223f * amount;
			case 3:
				return 1.523f * amount;
			case 4:
				num3 = 1.523f * amount;
				return 1.29222f * num3;
			case 5:
				return 23708.799f * amount;
			case 6:
				return 21788.238f * amount;
			case 7:
				return 44.097f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "n-C4H10":
			switch (index)
			{
			case 1:
				return 128.629f * amount;
			case 2:
				return 118.577f * amount;
			case 3:
				return 2.007f * amount;
			case 4:
				num3 = 2.007f * amount;
				return 1.29222f * num3;
			case 5:
				return 30722.508f * amount;
			case 6:
				return 28321.63f * amount;
			case 7:
				return 58.124f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "n-C4":
			switch (index)
			{
			case 1:
				return 128.629f * amount;
			case 2:
				return 118.577f * amount;
			case 3:
				return 2.007f * amount;
			case 4:
				num3 = 2.007f * amount;
				return 1.29222f * num3;
			case 5:
				return 30722.508f * amount;
			case 6:
				return 28321.63f * amount;
			case 7:
				return 58.124f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "i-C4H10":
			switch (index)
			{
			case 1:
				return 128.257f * amount;
			case 2:
				return 118.206f * amount;
			case 3:
				return 2.007f * amount;
			case 4:
				num3 = 2.007f * amount;
				return 1.29222f * num3;
			case 5:
				return 30633.658f * amount;
			case 6:
				return 28233.018f * amount;
			case 7:
				return 58.124f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "i-C4":
			switch (index)
			{
			case 1:
				return 128.257f * amount;
			case 2:
				return 118.206f * amount;
			case 3:
				return 2.007f * amount;
			case 4:
				num3 = 2.007f * amount;
				return 1.29222f * num3;
			case 5:
				return 30633.658f * amount;
			case 6:
				return 28233.018f * amount;
			case 7:
				return 58.124f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "n-C5H12":
			switch (index)
			{
			case 1:
				return 158.087f * amount;
			case 2:
				return 146.025f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37758.434f * amount;
			case 6:
				return 34877.473f * amount;
			case 7:
				return 72.151f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "n-C5":
			switch (index)
			{
			case 1:
				return 158.087f * amount;
			case 2:
				return 146.025f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37758.434f * amount;
			case 6:
				return 34877.473f * amount;
			case 7:
				return 72.151f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "i-C5H12":
			switch (index)
			{
			case 1:
				return 157.73f * amount;
			case 2:
				return 145.668f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37673.16f * amount;
			case 6:
				return 34792.203f * amount;
			case 7:
				return 72.151f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "i-C5":
			switch (index)
			{
			case 1:
				return 157.73f * amount;
			case 2:
				return 145.668f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37673.16f * amount;
			case 6:
				return 34792.203f * amount;
			case 7:
				return 72.151f * amount;
			case 8:
				return 72.151f * amount;
			}
			break;
		case "neo-C5H12":
			switch (index)
			{
			case 1:
				return 157.215f * amount;
			case 2:
				return 145.153f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37550.156f * amount;
			case 6:
				return 34669.2f * amount;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "neo-C5":
			switch (index)
			{
			case 1:
				return 157.215f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 2:
				return 145.153f * amount;
			case 5:
				return 37550.156f * amount;
			case 6:
				return 34669.2f * amount;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "C6H14":
			switch (index)
			{
			case 1:
				return 187.528f * amount;
			case 3:
				return 2.975f * amount;
			case 4:
				num3 = 2.975f * amount;
				return 1.29222f * num3;
			case 2:
				return 173.454f * amount;
			case 5:
				return 44790.293f * amount;
			case 6:
				return 41428.773f * amount;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C6":
			switch (index)
			{
			case 1:
				return 187.528f * amount;
			case 2:
				return 173.454f * amount;
			case 3:
				return 2.975f * amount;
			case 4:
				num3 = 2.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 44790.293f * amount;
			case 6:
				return 41428.773f * amount;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C6及更重组份":
			switch (index)
			{
			case 1:
				return 187.528f * amount;
			case 2:
				return 173.454f * amount;
			case 3:
				return 2.975f * amount;
			case 4:
				num3 = 2.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 44790.293f * amount;
			case 6:
				return 41428.773f * amount;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C6+":
			switch (index)
			{
			case 1:
				return 187.528f * amount;
			case 2:
				return 173.454f * amount;
			case 3:
				return 2.975f * amount;
			case 4:
				num3 = 2.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 44790.293f * amount;
			case 6:
				return 41428.773f * amount;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C7H16":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C7":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C7及更重组份":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 200.203f * amount;
			}
			break;
		case "C7+":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C8H18":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 3.944f * amount;
			case 4:
				num3 = 3.944f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C8":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 3.944f * amount;
			case 4:
				num3 = 3.944f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C9H20":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C9":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C10H22":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C10":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C11H24":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C11":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C12H26":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C6H12":
			switch (index)
			{
			case 1:
				return 176.706f * amount;
			case 2:
				return 164.644f * amount;
			case 3:
				return 2.906f * amount;
			case 4:
				num3 = 2.906f * amount;
				return 1.29222f * num3;
			case 5:
				return 42205.5f * amount;
			case 6:
				return 39324.543f * amount;
			case 7:
				return 84.162f * amount;
			}
			break;
		case "C7H14":
			switch (index)
			{
			case 1:
				return 205.649f * amount;
			case 2:
				return 191.577f * amount;
			case 3:
				return 3.39f * amount;
			case 4:
				num3 = 3.39f * amount;
				return 1.29222f * num3;
			case 5:
				return 49118.42f * amount;
			case 6:
				return 45757.38f * amount;
			case 7:
				return 98.189f * amount;
			}
			break;
		case "C6H6":
			switch (index)
			{
			case 1:
				return 147.464f * amount;
			case 2:
				return 141.432f * amount;
			case 3:
				return 2.697f * amount;
			case 4:
				num3 = 2.697f * amount;
				return 1.29222f * num3;
			case 5:
				return 35221.17f * amount;
			case 6:
				return 33780.453f * amount;
			case 7:
				return 78.114f * amount;
			}
			break;
		case "C7H8":
			switch (index)
			{
			case 1:
				return 176.358f * amount;
			case 2:
				return 168.316f * amount;
			case 3:
				return 3.181f * amount;
			case 4:
				num3 = 3.181f * amount;
				return 1.29222f * num3;
			case 5:
				return 42122.387f * amount;
			case 6:
				return 40201.586f * amount;
			case 7:
				return 92.141f * amount;
			}
			break;
		case "C2H4":
			switch (index)
			{
			case 1:
				return 63.438f * amount;
			case 2:
				return 59.477f * amount;
			case 3:
				return 0.975f * amount;
			case 4:
				num3 = 0.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 15151.906f * amount;
			case 6:
				return 14205.838f * amount;
			case 7:
				return 28.054f * amount;
			}
			break;
		case "C2=":
			switch (index)
			{
			case 1:
				return 63.438f * amount;
			case 2:
				return 59.477f * amount;
			case 3:
				return 0.975f * amount;
			case 4:
				num3 = 0.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 15151.906f * amount;
			case 6:
				return 14205.838f * amount;
			case 7:
				return 28.054f * amount;
			}
			break;
		case "C3H6":
			switch (index)
			{
			case 1:
				return 93.667f * amount;
			case 2:
				return 87.667f * amount;
			case 3:
				return 1.481f * amount;
			case 4:
				num3 = 1.481f * amount;
				return 1.29222f * num3;
			case 5:
				return 22371.979f * amount;
			case 6:
				return 20938.902f * amount;
			case 7:
				return 42.081f * amount;
			}
			break;
		case "C3=":
			switch (index)
			{
			case 1:
				return 93.667f * amount;
			case 2:
				return 87.667f * amount;
			case 3:
				return 1.481f * amount;
			case 4:
				num3 = 1.481f * amount;
				return 1.29222f * num3;
			case 5:
				return 22371.979f * amount;
			case 6:
				return 20938.902f * amount;
			case 7:
				return 42.081f * amount;
			}
			break;
		case "C4H8":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "C4=":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "n-i-C4H8":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "f-C4H8":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "f-C4H8-2":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "s-C4H8":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "s-C4H8-2":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "C5H10":
			switch (index)
			{
			case 1:
				return 159.107f * amount;
			case 2:
				return 148.736f * amount;
			case 3:
				return 2.558f * amount;
			case 4:
				num3 = 2.558f * amount;
				return 1.29222f * num3;
			case 5:
				return 38002.055f * amount;
			case 6:
				return 35524.98f * amount;
			case 7:
				return 70.135f * amount;
			}
			break;
		case "C5=":
			switch (index)
			{
			case 1:
				return 159.107f * amount;
			case 2:
				return 148.736f * amount;
			case 3:
				return 2.558f * amount;
			case 4:
				num3 = 2.558f * amount;
				return 1.29222f * num3;
			case 5:
				return 38002.055f * amount;
			case 6:
				return 35524.98f * amount;
			case 7:
				return 70.135f * amount;
			}
			break;
		case "C2H2":
			switch (index)
			{
			case 1:
				return 58.41f * amount;
			case 2:
				return 56.4f * amount;
			case 3:
				return 0.906f * amount;
			case 4:
				num3 = 0.906f * amount;
				return 1.29222f * num3;
			case 5:
				return 13950.989f * amount;
			case 6:
				return 13470.909f * amount;
			case 7:
				return 26.038f * amount;
			}
			break;
		case "氦":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 0.138f * amount;
			case 4:
				num3 = 0.138f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 4.003f * amount;
			}
			break;
		case "氩":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1.379f * amount;
			case 4:
				num3 = 1.379f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 39.948f * amount;
			}
			break;
		case "氢":
			switch (index)
			{
			case 1:
				return 12.789f * amount;
			case 2:
				return 10.779f * amount;
			case 3:
				return 0.07f * amount;
			case 4:
				num3 = 0.07f * amount;
				return 1.29222f * num3;
			case 5:
				return 3054.6f * amount;
			case 6:
				return 2574.52f * amount;
			case 7:
				return 2.016f * amount;
			}
			break;
		case "氧":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1.105f * amount;
			case 4:
				num3 = 1.105f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 31.999f * amount;
			}
			break;
		case "氮":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 0.967f * amount;
			case 4:
				num3 = 0.967f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.013f * amount;
			}
			break;
		case "一氧化碳":
			switch (index)
			{
			case 1:
				return 12.618f * amount;
			case 2:
				return 12.618f * amount;
			case 3:
				return 0.967f * amount;
			case 4:
				num3 = 0.967f * amount;
				return 1.29222f * num3;
			case 5:
				return 3013.758f * amount;
			case 6:
				return 3013.758f * amount;
			case 7:
				return 28.01f * amount;
			}
			break;
		case "二氧化碳":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1.52f * amount;
			case 4:
				num3 = 1.52f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 44.01f * amount;
			}
			break;
		case "硫化氢":
			switch (index)
			{
			case 1:
				return 25.141f * amount;
			case 2:
				return 23.13f * amount;
			case 3:
				return 1.177f * amount;
			case 4:
				num3 = 1.177f * amount;
				return 1.29222f * num3;
			case 5:
				return 6004.825f * amount;
			case 6:
				return 5524.505f * amount;
			case 7:
				return 34.076f * amount;
			}
			break;
		case "水蒸气":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 0.622f * amount;
			case 4:
				num3 = 0.622f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 18.015f * amount;
			}
			break;
		case "空气":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1f * amount;
			case 4:
				num3 = 1f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			}
			break;
		case "空气+甲烷":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				return 1f * amount;
			case 4:
				num3 = 1f * amount;
				return 1.29222f * num3;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			}
			break;
		case "甲烷":
			switch (index)
			{
			case 1:
				return 39.829f * amount;
			case 2:
				return 35.807f * amount;
			case 3:
				return 0.5541f * amount;
			case 4:
				num3 = 0.554f * amount;
				return 1.29222f * num3;
			case 5:
				return 9512.993f * amount;
			case 6:
				return 8552.355f * amount;
			case 7:
				return 16.043f * amount;
			}
			break;
		case "乙烷":
			switch (index)
			{
			case 1:
				return 69.759f * amount;
			case 2:
				return 63.727f * amount;
			case 3:
				return 1.038f * amount;
			case 4:
				num3 = 1.038f * amount;
				return 1.29222f * num3;
			case 5:
				return 16661.652f * amount;
			case 6:
				return 15220.933f * amount;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "乙烷+乙烯":
			switch (index)
			{
			case 1:
				return 69.759f * amount;
			case 2:
				return 63.727f * amount;
			case 3:
				return 1.038f * amount;
			case 4:
				num3 = 1.038f * amount;
				return 1.29222f * num3;
			case 5:
				return 16661.652f * amount;
			case 6:
				return 15220.933f * amount;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "丙烷":
			switch (index)
			{
			case 1:
				return 99.264f * amount;
			case 2:
				return 91.223f * amount;
			case 3:
				return 1.523f * amount;
			case 4:
				num3 = 1.523f * amount;
				return 1.29222f * num3;
			case 5:
				return 23708.799f * amount;
			case 6:
				return 21788.238f * amount;
			case 7:
				return 44.097f * amount;
			}
			break;
		case "正丁烷":
			switch (index)
			{
			case 1:
				return 128.629f * amount;
			case 2:
				return 118.577f * amount;
			case 3:
				return 2.007f * amount;
			case 4:
				num3 = 2.007f * amount;
				return 1.29222f * num3;
			case 5:
				return 30722.508f * amount;
			case 6:
				return 28321.63f * amount;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "异丁烷":
			switch (index)
			{
			case 1:
				return 128.257f * amount;
			case 2:
				return 118.206f * amount;
			case 3:
				return 2.007f * amount;
			case 4:
				num3 = 2.007f * amount;
				return 1.29222f * num3;
			case 5:
				return 30633.658f * amount;
			case 6:
				return 28233.018f * amount;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "正戊烷":
			switch (index)
			{
			case 1:
				return 158.087f * amount;
			case 2:
				return 146.025f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37758.434f * amount;
			case 6:
				return 34877.473f * amount;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "异戊烷":
			switch (index)
			{
			case 1:
				return 157.73f * amount;
			case 2:
				return 145.668f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37673.16f * amount;
			case 6:
				return 34792.203f * amount;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "新戊烷":
			switch (index)
			{
			case 1:
				return 157.215f * amount;
			case 2:
				return 145.153f * amount;
			case 3:
				return 2.491f * amount;
			case 4:
				num3 = 2.491f * amount;
				return 1.29222f * num3;
			case 5:
				return 37550.156f * amount;
			case 6:
				return 34669.2f * amount;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "己烷":
			switch (index)
			{
			case 1:
				return 187.528f * amount;
			case 2:
				return 173.454f * amount;
			case 3:
				return 2.975f * amount;
			case 4:
				num3 = 2.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 44790.293f * amount;
			case 6:
				return 41428.773f * amount;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "己烷及更重组份":
			switch (index)
			{
			case 1:
				return 187.528f * amount;
			case 2:
				return 173.454f * amount;
			case 3:
				return 2.975f * amount;
			case 4:
				num3 = 2.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 44790.293f * amount;
			case 6:
				return 41428.773f * amount;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "庚烷":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳七":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "庚烷及更重组份":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳七及更重组份":
			switch (index)
			{
			case 1:
				return 216.966f * amount;
			case 2:
				return 200.881f * amount;
			case 3:
				return 3.46f * amount;
			case 4:
				num3 = 3.46f * amount;
				return 1.29222f * num3;
			case 5:
				return 51821.44f * amount;
			case 6:
				return 47979.6f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "辛烷":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 3.944f * amount;
			case 4:
				num3 = 3.944f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳八":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 3.944f * amount;
			case 4:
				num3 = 3.944f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "壬烷":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳九":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "葵烷":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳十":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳十一":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳十二":
			switch (index)
			{
			case 1:
				return 246.381f * amount;
			case 2:
				return 228.286f * amount;
			case 3:
				return 4.241f * amount;
			case 4:
				num3 = 4.241f * amount;
				return 1.29222f * num3;
			case 5:
				return 58847.09f * amount;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "环己烷":
			switch (index)
			{
			case 1:
				return 176.706f * amount;
			case 2:
				return 164.644f * amount;
			case 3:
				return 2.906f * amount;
			case 4:
				num3 = 2.906f * amount;
				return 1.29222f * num3;
			case 5:
				return 42205.5f * amount;
			case 6:
				return 39324.543f * amount;
			case 7:
				return 84.162f * amount;
			}
			break;
		case "甲基环己烷":
			switch (index)
			{
			case 1:
				return 205.649f * amount;
			case 2:
				return 191.577f * amount;
			case 3:
				return 3.39f * amount;
			case 4:
				num3 = 3.39f * amount;
				return 1.29222f * num3;
			case 5:
				return 49118.42f * amount;
			case 6:
				return 45757.38f * amount;
			case 7:
				return 98.189f * amount;
			}
			break;
		case "苯":
			switch (index)
			{
			case 1:
				return 147.464f * amount;
			case 2:
				return 141.432f * amount;
			case 3:
				return 2.697f * amount;
			case 4:
				num3 = 2.697f * amount;
				return 1.29222f * num3;
			case 5:
				return 35221.17f * amount;
			case 6:
				return 33780.453f * amount;
			case 7:
				return 78.114f * amount;
			}
			break;
		case "甲苯":
			switch (index)
			{
			case 1:
				return 176.358f * amount;
			case 2:
				return 168.316f * amount;
			case 3:
				return 3.181f * amount;
			case 4:
				num3 = 3.181f * amount;
				return 1.29222f * num3;
			case 5:
				return 42122.387f * amount;
			case 6:
				return 40201.586f * amount;
			case 7:
				return 92.141f * amount;
			}
			break;
		case "乙烯":
			switch (index)
			{
			case 1:
				return 63.438f * amount;
			case 2:
				return 59.477f * amount;
			case 3:
				return 0.975f * amount;
			case 4:
				num3 = 0.975f * amount;
				return 1.29222f * num3;
			case 5:
				return 15151.906f * amount;
			case 6:
				return 14205.838f * amount;
			case 7:
				return 28.054f * amount;
			}
			break;
		case "丙烯":
			switch (index)
			{
			case 1:
				return 93.667f * amount;
			case 2:
				return 87.667f * amount;
			case 3:
				return 1.481f * amount;
			case 4:
				num3 = 1.481f * amount;
				return 1.29222f * num3;
			case 5:
				return 22371.979f * amount;
			case 6:
				return 20938.902f * amount;
			case 7:
				return 42.081f * amount;
			}
			break;
		case "丁烯":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "顺丁烯":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "顺丁烯-2":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "反丁烯":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "反丁烯-2":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "正丁烯":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "正异丁烯":
			switch (index)
			{
			case 1:
				return 125.847f * amount;
			case 2:
				return 117.695f * amount;
			case 3:
				return 2.01f * amount;
			case 4:
				num3 = 2.01f * amount;
				return 1.29222f * num3;
			case 5:
				return 30058.04f * amount;
			case 6:
				return 28110.969f * amount;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "戊烯":
			switch (index)
			{
			case 1:
				return 159.107f * amount;
			case 2:
				return 148.736f * amount;
			case 3:
				return 2.558f * amount;
			case 4:
				num3 = 2.558f * amount;
				return 1.29222f * num3;
			case 5:
				return 38002.055f * amount;
			case 6:
				return 35524.98f * amount;
			case 7:
				return 70.135f * amount;
			}
			break;
		case "乙炔":
			switch (index)
			{
			case 1:
				return 58.41f * amount;
			case 2:
				return 56.4f * amount;
			case 3:
				return 0.906f * amount;
			case 4:
				num3 = 0.906f * amount;
				return 1.29222f * num3;
			case 5:
				return 13950.989f * amount;
			case 6:
				return 13470.909f * amount;
			case 7:
				return 26.038f * amount;
			}
			break;
		}
		return 0f;
	}

	public static float getCharacteristic(string gasName, float amount, int index, float temp)
	{
		float result = 0f;
		float result2 = 0f;
		float num = 0f;
		float result3 = 0f;
		switch (gasName)
		{
		case "1，3-丁二稀":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 110.721f * amount;
				}
				else if (temp == 20f)
				{
					result = 110.721f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 104.912f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 104.912f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9226f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.9576f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.3571f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.3571f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 26445f * amount;
				}
				else if (temp == 20f)
				{
					result = 26445f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 25058f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 25058f * amount;
				}
				return result2;
			case 7:
				return 54.0916f * amount;
			case 8:
				return 0.1844f * amount;
			}
			break;
		case "异丁稀":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9963f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0326f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4475f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4475f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058.04f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28110.969f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "二甲醚":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 63.16f * amount;
				}
				else if (temp == 15f)
				{
					result = 59.87f * amount;
				}
				else if (temp == 20f)
				{
					result = 59.87f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 58.8f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 55.46f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 55.46f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.592f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5912f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.6202f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.0572f * amount;
				}
				if (temp == 15f)
				{
					num = 1.9508f * amount;
				}
				if (temp == 20f)
				{
					num = 1.9508f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 15086f * amount;
				}
				else if (temp == 15f)
				{
					result = 14300f * amount;
				}
				else if (temp == 20f)
				{
					result = 14300f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 14044f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 13246f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 13246f * amount;
				}
				return result2;
			case 7:
				return 46.069f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "CH3OCH3":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 63.16f * amount;
				}
				else if (temp == 15f)
				{
					result = 59.87f * amount;
				}
				else if (temp == 20f)
				{
					result = 59.87f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 58.8f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 55.46f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 55.46f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.592f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5912f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.6202f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.0572f * amount;
				}
				if (temp == 15f)
				{
					num = 1.9508f * amount;
				}
				if (temp == 20f)
				{
					num = 1.9508f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 15086f * amount;
				}
				else if (temp == 15f)
				{
					result = 14300f * amount;
				}
				else if (temp == 20f)
				{
					result = 14300f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 14044f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 13246f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 13246f * amount;
				}
				return result2;
			case 7:
				return 46.069f * amount;
			case 8:
				return 0f * amount;
			}
			break;
		case "He":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.1382f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.1357f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.1382f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.1786f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.1664f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.1664f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				if (temp == 0f)
				{
					num = 4.003f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.003f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.003f * amount;
				}
				return num;
			}
			break;
		case "Ar":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1.3793f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.3546f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.3792f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.7823f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.6607f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.6607f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 0f * amount;
				}
				else if (temp == 15f)
				{
					result = 0f * amount;
				}
				else if (temp == 20f)
				{
					result = 0f * amount;
				}
				return result;
			case 6:
				return 0f * amount;
			case 7:
				return 39.948f * amount;
			}
			break;
		case "H2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 12.789f * amount;
				}
				else if (temp == 15f)
				{
					result = 12.095f * amount;
				}
				else if (temp == 20f)
				{
					result = 11.889f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 10.779f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 10.217f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 10.051f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.0696f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.0695f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.0696f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.0899f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.0852f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.0838f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 3054.6f * amount;
				}
				else if (temp == 15f)
				{
					result = 2889f * amount;
				}
				else if (temp == 20f)
				{
					result = 2840f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 2574.52f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 2440f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 2401f * amount;
				}
				return result2;
			case 7:
				return 2.016f * amount;
			}
			break;
		case "O2":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1.1048f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1053f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1047f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.4276f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.3551f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.3302f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 31.999f * amount;
			}
			break;
		case "N2":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.9672f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9671f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9672f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2498f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1857f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1646f * amount;
				}
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.013f * amount;
			}
			break;
		case "CO":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 12.618f * amount;
				}
				else if (temp == 15f)
				{
					result = 11.966f * amount;
				}
				else if (temp == 20f)
				{
					result = 11.763f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 12.618f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 11.966f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 11.763f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.9671f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9672f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.967f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2497f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1858f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1644f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 3013.758f * amount;
				}
				else if (temp == 15f)
				{
					result = 2858f * amount;
				}
				else if (temp == 20f)
				{
					result = 2810f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 3013.758f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 2858f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 2810f * amount;
				}
				return result2;
			case 7:
				return 28.01f * amount;
			}
			break;
		case "CO2":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1.5195f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5275f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.5195f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9635f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8727f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8296f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 44.01f * amount;
			}
			break;
		case "H2S":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 25.141f * amount;
				}
				else if (temp == 15f)
				{
					result = 23.393f * amount;
				}
				else if (temp == 20f)
				{
					result = 23.393f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 23.13f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 21.555f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 21.555f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.1765f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1555f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1765f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.5203f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.4166f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.4166f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 6004.825f * amount;
				}
				else if (temp == 15f)
				{
					result = 5587f * amount;
				}
				else if (temp == 20f)
				{
					result = 5587f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 5524.505f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 5148f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 5148f * amount;
				}
				return result2;
			case 7:
				return 34.076f * amount;
			}
			break;
		case "H2O":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.622f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.6108f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.622f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.8038f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.7489f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.7489f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 0f * amount;
				}
				else if (temp == 15f)
				{
					result = 0f * amount;
				}
				else if (temp == 20f)
				{
					result = 0f * amount;
				}
				return result;
			case 6:
				return 0f * amount;
			case 7:
				return 18.015f * amount;
			}
			break;
		case "Air":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 0f * amount;
				}
				else if (temp == 15f)
				{
					result = 0f * amount;
				}
				else if (temp == 20f)
				{
					result = 0f * amount;
				}
				return result;
			case 2:
				return 0f * amount;
			case 3:
				return 1f * amount;
			case 4:
				if (temp == 0f)
				{
					num = 1.2922f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.226f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.2041f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			}
			break;
		case "Air+CH4":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1f * amount;
				}
				else if (temp == 15f)
				{
					num = 1f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0177f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2922f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.226f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.2254f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			}
			break;
		case "CH4":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 39.829f * amount;
				}
				else if (temp == 15f)
				{
					result = 37.782f * amount;
				}
				else if (temp == 20f)
				{
					result = 37.033f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 35.807f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 34.016f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 33.356f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.5539f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.5548f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.5539f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.7157f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.6802f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.6669f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 9512.993f * amount;
				}
				else if (temp == 15f)
				{
					result = 9024f * amount;
				}
				else if (temp == 20f)
				{
					result = 8845f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 8552.355f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 8125f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 7967f * amount;
				}
				return result2;
			case 7:
				return 16.043f * amount;
			}
			break;
		case "C1":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 39.829f * amount;
				}
				else if (temp == 15f)
				{
					result = 37.782f * amount;
				}
				else if (temp == 20f)
				{
					result = 37.033f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 35.807f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 34.016f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 33.356f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.5539f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.5548f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.5539f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.7157f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.6802f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.6669f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 9512.993f * amount;
				}
				else if (temp == 15f)
				{
					result = 9024f * amount;
				}
				else if (temp == 20f)
				{
					result = 8845f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 8552.355f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 8125f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 7967f * amount;
				}
				return result2;
			case 7:
				return 16.043f * amount;
			}
			break;
		case "C2H6":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 69.759f * amount;
				}
				else if (temp == 15f)
				{
					result = 66.636f * amount;
				}
				else if (temp == 20f)
				{
					result = 64.877f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 63.727f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 60.948f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 59.362f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.0382f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.0467f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0381f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.3416f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.2833f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.25f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 16661.652f * amount;
				}
				else if (temp == 15f)
				{
					result = 15916f * amount;
				}
				else if (temp == 20f)
				{
					result = 15496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 15220.933f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 14557f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 14178f * amount;
				}
				return result2;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "C2H6+C2H4":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 69.759f * amount;
				}
				else if (temp == 15f)
				{
					result = 66.636f * amount;
				}
				else if (temp == 20f)
				{
					result = 64.877f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 63.727f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 60.948f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 59.362f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.0382f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.0467f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0381f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.3416f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.2833f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.25f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 16661.652f * amount;
				}
				else if (temp == 15f)
				{
					result = 15916f * amount;
				}
				else if (temp == 20f)
				{
					result = 15496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 15220.933f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 14557f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 14178f * amount;
				}
				return result2;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "C2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 69.759f * amount;
				}
				else if (temp == 15f)
				{
					result = 66.636f * amount;
				}
				else if (temp == 20f)
				{
					result = 64.877f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 63.727f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 60.948f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 59.362f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.0382f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.0467f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0381f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.3416f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.2833f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.25f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 16661.652f * amount;
				}
				else if (temp == 15f)
				{
					result = 15916f * amount;
				}
				else if (temp == 20f)
				{
					result = 15496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 15220.933f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 14557f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 14178f * amount;
				}
				return result2;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "C3H8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 99.264f * amount;
				}
				else if (temp == 15f)
				{
					result = 95.652f * amount;
				}
				else if (temp == 20f)
				{
					result = 92.331f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 91.223f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 87.995f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 84.978f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.5225f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5496f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.5225f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9674f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8998f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8332f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 23709f * amount;
				}
				else if (temp == 15f)
				{
					result = 22846f * amount;
				}
				else if (temp == 20f)
				{
					result = 22053f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 21788f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 21017f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 20297f * amount;
				}
				return result2;
			case 7:
				return 44.097f * amount;
			}
			break;
		case "C3":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 99.264f * amount;
				}
				else if (temp == 15f)
				{
					result = 95.652f * amount;
				}
				else if (temp == 20f)
				{
					result = 92.331f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 91.223f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 87.995f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 84.978f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.5225f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5496f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.5225f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9674f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8998f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8332f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 23709f * amount;
				}
				else if (temp == 15f)
				{
					result = 22846f * amount;
				}
				else if (temp == 20f)
				{
					result = 22053f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 21788f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 21017f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 20297f * amount;
				}
				return result2;
			case 7:
				return 44.097f * amount;
			}
			break;
		case "n-C4H10":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 128.629f * amount;
				}
				else if (temp == 15f)
				{
					result = 126.774f * amount;
				}
				else if (temp == 20f)
				{
					result = 119.655f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 118.577f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 116.999f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.463f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0068f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.0852f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0067f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5932f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.5565f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4163f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30722.508f * amount;
				}
				else if (temp == 15f)
				{
					result = 30279f * amount;
				}
				else if (temp == 20f)
				{
					result = 28579f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28321.63f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 27945f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26384f * amount;
				}
				return result2;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "n-C4":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 128.629f * amount;
				}
				else if (temp == 15f)
				{
					result = 126.774f * amount;
				}
				else if (temp == 20f)
				{
					result = 119.655f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 118.577f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 116.999f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.463f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0068f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.0852f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0067f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5932f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.5565f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4163f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30722.508f * amount;
				}
				else if (temp == 15f)
				{
					result = 30279f * amount;
				}
				else if (temp == 20f)
				{
					result = 28579f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28321.63f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 27945f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26384f * amount;
				}
				return result2;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "i-C4H10":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 128.257f * amount;
				}
				else if (temp == 15f)
				{
					result = 125.641f * amount;
				}
				else if (temp == 20f)
				{
					result = 119.307f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 118.206f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 115.954f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.116f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0068f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.0722f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0067f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5932f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.5405f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4163f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30633.658f * amount;
				}
				else if (temp == 15f)
				{
					result = 30009f * amount;
				}
				else if (temp == 20f)
				{
					result = 28496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28233.018f * amount;
				}
				else if (temp == 0f)
				{
					result2 = 27695f * amount;
				}
				else if (temp == 0f)
				{
					result2 = 26301f * amount;
				}
				return result2;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "i-C4":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 128.257f * amount;
				}
				else if (temp == 15f)
				{
					result = 125.641f * amount;
				}
				else if (temp == 20f)
				{
					result = 119.307f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 118.206f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 115.954f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.116f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0068f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.0722f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0067f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5932f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.5405f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4163f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30633.658f * amount;
				}
				else if (temp == 15f)
				{
					result = 30009f * amount;
				}
				else if (temp == 20f)
				{
					result = 28496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28233.018f * amount;
				}
				else if (temp == 0f)
				{
					result2 = 27695f * amount;
				}
				else if (temp == 0f)
				{
					result2 = 26301f * amount;
				}
				return result2;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "n-C5H12":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 158.087f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.723f * amount;
				}
				else if (temp == 20f)
				{
					result = 147.063f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 146.025f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 147.684f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 136.034f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.4911f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6575f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.2581f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37758.434f * amount;
				}
				else if (temp == 15f)
				{
					result = 37758.434f * amount;
				}
				else if (temp == 20f)
				{
					result = 37758.434f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34877.473f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35274f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32491f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "n-C5":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 158.087f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.723f * amount;
				}
				else if (temp == 20f)
				{
					result = 147.063f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 146.025f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 147.684f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 136.034f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.4911f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6575f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.2581f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37758.434f * amount;
				}
				else if (temp == 15f)
				{
					result = 37758.434f * amount;
				}
				else if (temp == 20f)
				{
					result = 37758.434f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34877.473f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35274f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32491f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "i-C5H12":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 157.73f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.723f * amount;
				}
				else if (temp == 20f)
				{
					result = 146.729f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 145.668f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 147.684f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 135.7f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.4911f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6575f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.2581f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37673.16f * amount;
				}
				else if (temp == 15f)
				{
					result = 38149f * amount;
				}
				else if (temp == 20f)
				{
					result = 35046f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34792.203f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35274f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32411f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "i-C5":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 157.73f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.723f * amount;
				}
				else if (temp == 20f)
				{
					result = 146.729f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 145.668f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 147.684f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 135.7f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.4911f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6575f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.2581f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37673.16f * amount;
				}
				else if (temp == 15f)
				{
					result = 38149f * amount;
				}
				else if (temp == 20f)
				{
					result = 35046f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34792.203f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35274f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32411f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "neo-C5H12":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 157.215f * amount;
				}
				else if (temp == 15f)
				{
					result = 146.25f * amount;
				}
				else if (temp == 20f)
				{
					result = 146.25f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 145.153f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 135.221f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 135.221f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.491f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4465f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9994f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				num = 2.491f * amount;
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37550.156f * amount;
				}
				else if (temp == 15f)
				{
					result = 34931f * amount;
				}
				else if (temp == 20f)
				{
					result = 34931f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34669.2f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 32297f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32297f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "neo-C5":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 157.215f * amount;
				}
				else if (temp == 15f)
				{
					result = 146.25f * amount;
				}
				else if (temp == 20f)
				{
					result = 146.25f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 145.153f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 135.221f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 135.221f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.491f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4465f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9994f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				num = 2.491f * amount;
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37550.156f * amount;
				}
				else if (temp == 15f)
				{
					result = 34931f * amount;
				}
				else if (temp == 20f)
				{
					result = 34931f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34669.2f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 32297f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32297f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "C6H14":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 187.528f * amount;
				}
				else if (temp == 15f)
				{
					result = 174.459f * amount;
				}
				else if (temp == 20f)
				{
					result = 174.459f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 173.454f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 161.589f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 161.589f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9754f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9221f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9753f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.8448f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.5825f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.5825f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 44790.293f * amount;
				}
				else if (temp == 15f)
				{
					result = 41669f * amount;
				}
				else if (temp == 20f)
				{
					result = 41669f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 41429f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 38595f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 38595f * amount;
				}
				return result2;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C6":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 187.528f * amount;
				}
				else if (temp == 15f)
				{
					result = 174.459f * amount;
				}
				else if (temp == 20f)
				{
					result = 174.459f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 173.454f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 161.589f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 161.589f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9754f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9221f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9753f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.8448f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.5825f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.5825f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 44790.293f * amount;
				}
				else if (temp == 15f)
				{
					result = 41669f * amount;
				}
				else if (temp == 20f)
				{
					result = 41669f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 41429f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 38595f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 38595f * amount;
				}
				return result2;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C6及更重组份":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 187.528f * amount;
				}
				else if (temp == 15f)
				{
					result = 174.459f * amount;
				}
				else if (temp == 20f)
				{
					result = 174.459f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 173.454f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 161.589f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 161.589f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9754f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9221f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9753f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.8448f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.5825f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.5825f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 44790.293f * amount;
				}
				else if (temp == 15f)
				{
					result = 41669f * amount;
				}
				else if (temp == 20f)
				{
					result = 41669f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 41429f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 38595f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 38595f * amount;
				}
				return result2;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C6+":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 187.528f * amount;
				}
				else if (temp == 15f)
				{
					result = 174.459f * amount;
				}
				else if (temp == 20f)
				{
					result = 174.459f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 173.454f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 161.589f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 161.589f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9754f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9221f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9753f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.8448f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.5825f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.5825f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 44790.293f * amount;
				}
				else if (temp == 15f)
				{
					result = 41669f * amount;
				}
				else if (temp == 20f)
				{
					result = 41669f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 41429f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 38595f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 38595f * amount;
				}
				return result2;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "C7H16":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C7":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C7及更重组份":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C7+":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C8H18":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 229.219f * amount;
				}
				else if (temp == 20f)
				{
					result = 229.219f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 212.673f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 212.673f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.944f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.8734f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.9439f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.0965f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.7488f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.7488f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 54748f * amount;
				}
				else if (temp == 20f)
				{
					result = 54748f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 50796f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 50796f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 229.219f * amount;
				}
				else if (temp == 20f)
				{
					result = 229.219f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 212.673f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 212.673f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.944f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.8734f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.9439f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.0965f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.7488f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.7488f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 54748f * amount;
				}
				else if (temp == 20f)
				{
					result = 54748f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 50796f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 50796f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C9H20":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C9":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C10H22":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C10":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C11H24":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C11":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C12":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C12H26":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "C6H12":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 176.706f * amount;
				}
				else if (temp == 15f)
				{
					result = 164.393f * amount;
				}
				else if (temp == 20f)
				{
					result = 164.393f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 164.644f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 153.364f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 153.364f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9058f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.8538f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9057f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.7549f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.4987f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4987f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 42206f * amount;
				}
				else if (temp == 15f)
				{
					result = 39265f * amount;
				}
				else if (temp == 20f)
				{
					result = 39265f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 39325f * amount;
				}
				else if (temp == 0f)
				{
					result2 = 36630f * amount;
				}
				else if (temp == 0f)
				{
					result2 = 36630f * amount;
				}
				return result2;
			case 7:
				return 84.162f * amount;
			}
			break;
		case "C7H14":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 205.649f * amount;
				}
				else if (temp == 15f)
				{
					result = 191.329f * amount;
				}
				else if (temp == 20f)
				{
					result = 191.329f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 191.577f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 178.461f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 178.461f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.3901f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3294f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.3899f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.3807f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.0818f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.0818f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 49118.42f * amount;
				}
				else if (temp == 15f)
				{
					result = 45698f * amount;
				}
				else if (temp == 20f)
				{
					result = 45698f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 45757.38f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 42625f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 42625f * amount;
				}
				return result2;
			case 7:
				return 98.189f * amount;
			}
			break;
		case "C6H6":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 147.464f * amount;
				}
				else if (temp == 15f)
				{
					result = 137.28f * amount;
				}
				else if (temp == 20f)
				{
					result = 137.28f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 141.432f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 131.765f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 131.765f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.697f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6487f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.6969f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					result3 = 3.4851f;
				}
				else if (temp == 10f)
				{
					result3 = 3.2473f;
				}
				else if (temp == 15f)
				{
					result3 = 3.2473f;
				}
				return result3;
			case 5:
				if (temp == 0f)
				{
					result = 35221.17f * amount;
				}
				else if (temp == 15f)
				{
					result = 32789f * amount;
				}
				else if (temp == 20f)
				{
					result = 32789f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 33780.453f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 31472f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 31472f * amount;
				}
				return result2;
			case 7:
				return 78.114f * amount;
			}
			break;
		case "C7H8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 176.358f * amount;
				}
				else if (temp == 15f)
				{
					result = 164.163f * amount;
				}
				else if (temp == 20f)
				{
					result = 164.163f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 168.316f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 156.809f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 156.809f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.1813f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.1243f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.1811f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.1109f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.8304f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.8304f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 42122f * amount;
				}
				else if (temp == 15f)
				{
					result = 39210f * amount;
				}
				else if (temp == 20f)
				{
					result = 39210f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 40202f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 37453f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 37453f * amount;
				}
				return result2;
			case 7:
				return 92.141f * amount;
			}
			break;
		case "C2H4":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 63.438f * amount;
				}
				else if (temp == 15f)
				{
					result = 60.105f * amount;
				}
				else if (temp == 20f)
				{
					result = 60.105f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 59.477f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 56.32f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 56.32f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.975f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9745f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9918f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2605f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1947f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1942f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 15151.906f * amount;
				}
				else if (temp == 15f)
				{
					result = 14356f * amount;
				}
				else if (temp == 20f)
				{
					result = 14356f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 14205.838f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 13452f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 13452f * amount;
				}
				return result2;
			case 7:
				return 28.054f * amount;
			}
			break;
		case "C2=":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 63.438f * amount;
				}
				else if (temp == 15f)
				{
					result = 60.105f * amount;
				}
				else if (temp == 20f)
				{
					result = 60.105f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 59.477f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 56.32f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 56.32f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.975f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9745f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9918f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2605f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1947f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1942f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 15151.906f * amount;
				}
				else if (temp == 15f)
				{
					result = 14356f * amount;
				}
				else if (temp == 20f)
				{
					result = 14356f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 14205.838f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 13452f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 13452f * amount;
				}
				return result2;
			case 7:
				return 28.054f * amount;
			}
			break;
		case "C3H6":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 93.667f * amount;
				}
				else if (temp == 15f)
				{
					result = 88.516f * amount;
				}
				else if (temp == 20f)
				{
					result = 88.516f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 87.667f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 82.785f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 82.785f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.4809f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.4759f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.502f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9136f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8095f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8086f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 22372f * amount;
				}
				if (temp == 15f)
				{
					result = 21142f * amount;
				}
				if (temp == 20f)
				{
					result = 21142f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 20939f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 19773f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 19773f * amount;
				}
				return result2;
			case 7:
				return 42.081f * amount;
			}
			break;
		case "C3=":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 93.667f * amount;
				}
				else if (temp == 15f)
				{
					result = 88.516f * amount;
				}
				else if (temp == 20f)
				{
					result = 88.516f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 87.667f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 82.785f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 82.785f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.4809f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.4759f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.502f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9136f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8095f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8086f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 22372f * amount;
				}
				if (temp == 15f)
				{
					result = 21142f * amount;
				}
				if (temp == 20f)
				{
					result = 21142f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 20939f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 19773f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 19773f * amount;
				}
				return result2;
			case 7:
				return 42.081f * amount;
			}
			break;
		case "C4H8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "C4=":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "n-i-C4H8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "f-C4H8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "f-C4H8-2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "s-C4H8":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "s-C4H8-2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "C5H10":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 159.107f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.107f * amount;
				}
				else if (temp == 20f)
				{
					result = 159.107f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 148.736f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 148.736f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 148.736f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.558f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6962f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.7452f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.3055f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3055f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.3055f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 38002.055f * amount;
				}
				else if (temp == 15f)
				{
					result = 38002.055f * amount;
				}
				else if (temp == 20f)
				{
					result = 38002.055f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 35524.98f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35524.98f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 35524.98f * amount;
				}
				return result2;
			case 7:
				return 70.135f * amount;
			}
			break;
		case "C5=":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 159.107f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.107f * amount;
				}
				else if (temp == 20f)
				{
					result = 159.107f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 148.736f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 148.736f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 148.736f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.558f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6962f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.7452f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.3055f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3055f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.3055f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 38002.055f * amount;
				}
				else if (temp == 15f)
				{
					result = 38002.055f * amount;
				}
				else if (temp == 20f)
				{
					result = 38002.055f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 35524.98f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35524.98f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 35524.98f * amount;
				}
				return result2;
			case 7:
				return 70.135f * amount;
			}
			break;
		case "C2H2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 58.41f * amount;
				}
				else if (temp == 15f)
				{
					result = 58.41f * amount;
				}
				else if (temp == 20f)
				{
					result = 58.41f * amount;
				}
				return result;
			case 2:
				return 56.4f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.9061f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9551f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9724f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.1709f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1709f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1709f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 13950.989f * amount;
			case 6:
				return 13470.909f * amount;
			case 7:
				return 26.038f * amount;
			}
			break;
		case "氦":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.1382f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.1357f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.1382f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.1786f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.1664f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.1664f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				if (temp == 0f)
				{
					num = 4.003f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.003f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.003f * amount;
				}
				return num;
			}
			break;
		case "氩":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1.3793f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.3546f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.3792f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.7823f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.6607f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.6607f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 0f * amount;
				}
				else if (temp == 15f)
				{
					result = 0f * amount;
				}
				else if (temp == 20f)
				{
					result = 0f * amount;
				}
				return result;
			case 6:
				return 0f * amount;
			case 7:
				return 39.948f * amount;
			}
			break;
		case "氢":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 12.789f * amount;
				}
				else if (temp == 15f)
				{
					result = 12.095f * amount;
				}
				else if (temp == 20f)
				{
					result = 11.889f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 10.779f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 10.217f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 10.051f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.0696f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.0695f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.0696f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.0899f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.0852f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.0838f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 3054.6f * amount;
				}
				else if (temp == 15f)
				{
					result = 2889f * amount;
				}
				else if (temp == 20f)
				{
					result = 2840f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 2574.52f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 2440f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 2401f * amount;
				}
				return result2;
			case 7:
				return 2.016f * amount;
			}
			break;
		case "氧":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1.1048f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1053f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1047f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.4276f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.3551f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.3302f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 31.999f * amount;
			}
			break;
		case "氮":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.9672f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9671f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9672f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2498f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1857f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1646f * amount;
				}
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.013f * amount;
			}
			break;
		case "一氧化碳":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 12.618f * amount;
				}
				else if (temp == 15f)
				{
					result = 11.966f * amount;
				}
				else if (temp == 20f)
				{
					result = 11.763f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 12.618f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 11.966f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 11.763f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.9671f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9672f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.967f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2497f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1858f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1644f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 3013.758f * amount;
				}
				else if (temp == 15f)
				{
					result = 2858f * amount;
				}
				else if (temp == 20f)
				{
					result = 2810f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 3013.758f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 2858f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 2810f * amount;
				}
				return result2;
			case 7:
				return 28.01f * amount;
			}
			break;
		case "二氧化碳":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1.5195f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5275f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.5195f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9635f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8727f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8296f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 44.01f * amount;
			}
			break;
		case "硫化氢":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 25.141f * amount;
				}
				else if (temp == 15f)
				{
					result = 23.393f * amount;
				}
				else if (temp == 20f)
				{
					result = 23.393f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 23.13f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 21.555f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 21.555f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.1765f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1555f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1765f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.5203f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.4166f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.4166f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 6004.825f * amount;
				}
				else if (temp == 15f)
				{
					result = 5587f * amount;
				}
				else if (temp == 20f)
				{
					result = 5587f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 5524.505f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 5148f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 5148f * amount;
				}
				return result2;
			case 7:
				return 34.076f * amount;
			}
			break;
		case "水蒸气":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.622f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.6108f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.622f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.8038f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.7489f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.7489f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 0f * amount;
				}
				else if (temp == 15f)
				{
					result = 0f * amount;
				}
				else if (temp == 20f)
				{
					result = 0f * amount;
				}
				return result;
			case 6:
				return 0f * amount;
			case 7:
				return 18.015f * amount;
			}
			break;
		case "空气":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 0f * amount;
				}
				else if (temp == 15f)
				{
					result = 0f * amount;
				}
				else if (temp == 20f)
				{
					result = 0f * amount;
				}
				return result;
			case 2:
				return 0f * amount;
			case 3:
				return 1f * amount;
			case 4:
				if (temp == 0f)
				{
					num = 1.2922f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.226f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.2041f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			}
			break;
		case "空气+甲烷":
			switch (index)
			{
			case 1:
				return 0f * amount;
			case 2:
				return 0f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 1f * amount;
				}
				else if (temp == 15f)
				{
					num = 1f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0177f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2922f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.226f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.2254f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 0f * amount;
			case 6:
				return 0f * amount;
			case 7:
				return 28.966f * amount;
			}
			break;
		case "甲烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 39.829f * amount;
				}
				else if (temp == 15f)
				{
					result = 37.782f * amount;
				}
				else if (temp == 20f)
				{
					result = 37.033f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 35.807f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 34.016f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 33.356f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.5539f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.5548f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.5539f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 0.7157f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.6802f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.6669f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 9512.993f * amount;
				}
				else if (temp == 15f)
				{
					result = 9024f * amount;
				}
				else if (temp == 20f)
				{
					result = 8845f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 8552.355f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 8125f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 7967f * amount;
				}
				return result2;
			case 7:
				return 16.043f * amount;
			}
			break;
		case "乙烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 69.759f * amount;
				}
				else if (temp == 15f)
				{
					result = 66.636f * amount;
				}
				else if (temp == 20f)
				{
					result = 64.877f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 63.727f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 60.948f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 59.362f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.0382f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.0467f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0381f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.3416f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.2833f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.25f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 16661.652f * amount;
				}
				else if (temp == 15f)
				{
					result = 15916f * amount;
				}
				else if (temp == 20f)
				{
					result = 15496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 15220.933f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 14557f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 14178f * amount;
				}
				return result2;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "乙烷+乙烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 69.759f * amount;
				}
				else if (temp == 15f)
				{
					result = 66.636f * amount;
				}
				else if (temp == 20f)
				{
					result = 64.877f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 63.727f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 60.948f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 59.362f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.0382f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.0467f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.0381f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.3416f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.2833f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.25f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 16661.652f * amount;
				}
				else if (temp == 15f)
				{
					result = 15916f * amount;
				}
				else if (temp == 20f)
				{
					result = 15496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 15220.933f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 14557f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 14178f * amount;
				}
				return result2;
			case 7:
				return 30.07f * amount;
			}
			break;
		case "丙烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 99.264f * amount;
				}
				else if (temp == 15f)
				{
					result = 95.652f * amount;
				}
				else if (temp == 20f)
				{
					result = 92.331f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 91.223f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 87.995f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 84.978f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.5225f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.5496f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.5225f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9674f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8998f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8332f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 23709f * amount;
				}
				else if (temp == 15f)
				{
					result = 22846f * amount;
				}
				else if (temp == 20f)
				{
					result = 22053f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 21788f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 21017f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 20297f * amount;
				}
				return result2;
			case 7:
				return 44.097f * amount;
			}
			break;
		case "正丁烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 128.629f * amount;
				}
				else if (temp == 15f)
				{
					result = 126.774f * amount;
				}
				else if (temp == 20f)
				{
					result = 119.655f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 118.577f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 116.999f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.463f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0068f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.0852f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0067f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5932f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.5565f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4163f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30722.508f * amount;
				}
				else if (temp == 15f)
				{
					result = 30279f * amount;
				}
				else if (temp == 20f)
				{
					result = 28579f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28321.63f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 27945f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26384f * amount;
				}
				return result2;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "异丁烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 128.257f * amount;
				}
				else if (temp == 15f)
				{
					result = 125.641f * amount;
				}
				else if (temp == 20f)
				{
					result = 119.307f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 118.206f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 115.954f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.116f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0068f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.0722f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0067f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5932f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.5405f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4163f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30633.658f * amount;
				}
				else if (temp == 15f)
				{
					result = 30009f * amount;
				}
				else if (temp == 20f)
				{
					result = 28496f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28233.018f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 27695f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26301f * amount;
				}
				return result2;
			case 7:
				return 58.124f * amount;
			}
			break;
		case "正戊烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 158.087f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.723f * amount;
				}
				else if (temp == 20f)
				{
					result = 147.063f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 146.025f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 147.684f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 136.034f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.4911f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6575f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.2581f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37758.434f * amount;
				}
				else if (temp == 15f)
				{
					result = 37758.434f * amount;
				}
				else if (temp == 20f)
				{
					result = 37758.434f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34877.473f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35274f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32491f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "异戊烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 157.73f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.723f * amount;
				}
				else if (temp == 20f)
				{
					result = 146.729f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 145.668f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 147.684f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 135.7f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.4911f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6575f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.2581f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37673.16f * amount;
				}
				else if (temp == 15f)
				{
					result = 38149f * amount;
				}
				else if (temp == 20f)
				{
					result = 35046f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34792.203f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35274f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32411f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "新戊烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 157.215f * amount;
				}
				else if (temp == 15f)
				{
					result = 146.25f * amount;
				}
				else if (temp == 20f)
				{
					result = 146.25f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 145.153f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 135.221f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 135.221f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.491f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4465f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.491f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.219f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9994f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9994f * amount;
				}
				num = 2.491f * amount;
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 37550.156f * amount;
				}
				else if (temp == 15f)
				{
					result = 34931f * amount;
				}
				else if (temp == 20f)
				{
					result = 34931f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 34669.2f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 32297f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 32297f * amount;
				}
				return result2;
			case 7:
				return 72.151f * amount;
			}
			break;
		case "己烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 187.528f * amount;
				}
				else if (temp == 15f)
				{
					result = 174.459f * amount;
				}
				else if (temp == 20f)
				{
					result = 174.459f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 173.454f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 161.589f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 161.589f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9754f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9221f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9753f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.8448f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.5825f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.5825f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 44790.293f * amount;
				}
				else if (temp == 15f)
				{
					result = 41669f * amount;
				}
				else if (temp == 20f)
				{
					result = 41669f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 41429f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 38595f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 38595f * amount;
				}
				return result2;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "己烷及更重组份":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 187.528f * amount;
				}
				else if (temp == 15f)
				{
					result = 174.459f * amount;
				}
				else if (temp == 20f)
				{
					result = 174.459f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 173.454f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 161.589f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 161.589f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9754f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.9221f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9753f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.8448f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.5825f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.5825f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 44790.293f * amount;
				}
				else if (temp == 15f)
				{
					result = 41669f * amount;
				}
				else if (temp == 20f)
				{
					result = 41669f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 41429f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 38595f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 38595f * amount;
				}
				return result2;
			case 7:
				return 86.177f * amount;
			}
			break;
		case "庚烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳七":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "庚烷及更重组份":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳七及更重组份":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 216.966f * amount;
				}
				else if (temp == 15f)
				{
					result = 201.849f * amount;
				}
				else if (temp == 20f)
				{
					result = 201.849f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 200.881f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 187.141f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 187.141f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.4597f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3977f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4595f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.4706f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.1656f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.1656f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 51821f * amount;
				}
				else if (temp == 15f)
				{
					result = 48211f * amount;
				}
				else if (temp == 20f)
				{
					result = 48211f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 47980f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 44698f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 44698f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "辛烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 229.219f * amount;
				}
				else if (temp == 20f)
				{
					result = 229.219f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 212.673f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 212.673f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.944f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.8734f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.9439f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.0965f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.7488f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.7488f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 54748f * amount;
				}
				else if (temp == 20f)
				{
					result = 54748f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 50796f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 50796f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳八":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 229.219f * amount;
				}
				else if (temp == 20f)
				{
					result = 229.219f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 212.673f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 212.673f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.944f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.8734f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.9439f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.0965f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.7488f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.7488f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 54748f * amount;
				}
				else if (temp == 20f)
				{
					result = 54748f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 50796f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 50796f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "壬烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳九":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "葵烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳十":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳十一":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				return 54525.17f * amount;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "碳十二":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 15f)
				{
					result = 246.381f * amount;
				}
				else if (temp == 20f)
				{
					result = 246.381f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 228.286f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 228.286f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 4.2408f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.4698f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.5511f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 15f)
				{
					num = 5.48f * amount;
				}
				else if (temp == 20f)
				{
					num = 5.48f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 15f)
				{
					result = 58847.09f * amount;
				}
				else if (temp == 20f)
				{
					result = 58847.09f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 54525.17f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 54525.17f * amount;
				}
				return result2;
			case 7:
				return 100.203f * amount;
			}
			break;
		case "环己烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 176.706f * amount;
				}
				else if (temp == 15f)
				{
					result = 164.393f * amount;
				}
				else if (temp == 20f)
				{
					result = 164.393f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 164.644f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 153.364f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 153.364f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.9058f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.8538f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.9057f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.7549f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.4987f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.4987f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 42206f * amount;
				}
				else if (temp == 15f)
				{
					result = 39265f * amount;
				}
				else if (temp == 20f)
				{
					result = 39265f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 39325f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 36630f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 36630f * amount;
				}
				return result2;
			case 7:
				return 84.162f * amount;
			}
			break;
		case "甲基环己烷":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 205.649f * amount;
				}
				else if (temp == 15f)
				{
					result = 191.329f * amount;
				}
				else if (temp == 20f)
				{
					result = 191.329f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 191.577f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 178.461f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 178.461f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.3901f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3294f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.3899f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.3807f * amount;
				}
				else if (temp == 15f)
				{
					num = 4.0818f * amount;
				}
				else if (temp == 20f)
				{
					num = 4.0818f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 49118.42f * amount;
				}
				else if (temp == 15f)
				{
					result = 45698f * amount;
				}
				else if (temp == 20f)
				{
					result = 45698f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 45757.38f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 42625f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 42625f * amount;
				}
				return result2;
			case 7:
				return 98.189f * amount;
			}
			break;
		case "苯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 147.464f * amount;
				}
				else if (temp == 15f)
				{
					result = 137.28f * amount;
				}
				else if (temp == 20f)
				{
					result = 137.28f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 141.432f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 131.765f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 131.765f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.697f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6487f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.6969f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.4851f;
				}
				else if (temp == 10f)
				{
					num = 3.2473f;
				}
				else if (temp == 15f)
				{
					num = 3.2473f;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 35221.17f * amount;
				}
				else if (temp == 15f)
				{
					result = 32789f * amount;
				}
				else if (temp == 20f)
				{
					result = 32789f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 33780.453f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 31472f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 31472f * amount;
				}
				return result2;
			case 7:
				return 78.114f * amount;
			}
			break;
		case "甲苯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 176.358f * amount;
				}
				else if (temp == 15f)
				{
					result = 164.163f * amount;
				}
				else if (temp == 20f)
				{
					result = 164.163f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 168.316f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 156.809f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 156.809f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 3.1813f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.1243f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.1811f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 4.1109f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.8304f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.8304f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 42122f * amount;
				}
				else if (temp == 15f)
				{
					result = 39210f * amount;
				}
				else if (temp == 20f)
				{
					result = 39210f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 40202f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 37453f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 37453f * amount;
				}
				return result2;
			case 7:
				return 92.141f * amount;
			}
			break;
		case "乙烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 63.438f * amount;
				}
				else if (temp == 15f)
				{
					result = 60.105f * amount;
				}
				else if (temp == 20f)
				{
					result = 60.105f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 59.477f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 56.32f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 56.32f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 0.975f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9745f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9918f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.2605f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1947f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1942f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 15151.906f * amount;
				}
				else if (temp == 15f)
				{
					result = 14356f * amount;
				}
				else if (temp == 20f)
				{
					result = 14356f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 14205.838f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 13452f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 13452f * amount;
				}
				return result2;
			case 7:
				return 28.054f * amount;
			}
			break;
		case "丙烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 93.667f * amount;
				}
				else if (temp == 15f)
				{
					result = 88.516f * amount;
				}
				else if (temp == 20f)
				{
					result = 88.516f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 87.667f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 82.785f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 82.785f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 1.4809f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.4759f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.502f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.9136f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.8095f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.8086f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 22372f * amount;
				}
				if (temp == 15f)
				{
					result = 21142f * amount;
				}
				if (temp == 20f)
				{
					result = 21142f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 20939f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 19773f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 19773f * amount;
				}
				return result2;
			case 7:
				return 42.081f * amount;
			}
			break;
		case "丁烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "顺丁烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9963f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0326f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4475f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4475f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058.04f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28110.969f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "顺丁烯-2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "反丁烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9963f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0326f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4475f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4475f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058.04f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "反丁烯-2":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9963f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0326f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4475f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4475f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058.04f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "正丁烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9663f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0011f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4107f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4095f * amount;
				}
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28111f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "正异丁烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 125.847f * amount;
				}
				else if (temp == 15f)
				{
					result = 118.536f * amount;
				}
				else if (temp == 20f)
				{
					result = 118.536f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 117.695f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 110.784f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 110.784f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.0096f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.9963f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.0326f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 2.5968f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.4475f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.4475f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 30058.04f * amount;
				}
				else if (temp == 15f)
				{
					result = 28312f * amount;
				}
				else if (temp == 20f)
				{
					result = 28312f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 28110.969f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 26460f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 26460f * amount;
				}
				return result2;
			case 7:
				return 56.108f * amount;
			}
			break;
		case "戊烯":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 159.107f * amount;
				}
				else if (temp == 15f)
				{
					result = 159.107f * amount;
				}
				else if (temp == 20f)
				{
					result = 159.107f * amount;
				}
				return result;
			case 2:
				if (temp == 0f)
				{
					result2 = 148.736f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 148.736f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 148.736f * amount;
				}
				return result2;
			case 3:
				if (temp == 0f)
				{
					num = 2.558f * amount;
				}
				else if (temp == 15f)
				{
					num = 2.6962f * amount;
				}
				else if (temp == 20f)
				{
					num = 2.7452f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 3.3055f * amount;
				}
				else if (temp == 15f)
				{
					num = 3.3055f * amount;
				}
				else if (temp == 20f)
				{
					num = 3.3055f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				if (temp == 0f)
				{
					result = 38002.055f * amount;
				}
				else if (temp == 15f)
				{
					result = 38002.055f * amount;
				}
				else if (temp == 20f)
				{
					result = 38002.055f * amount;
				}
				return result;
			case 6:
				if (temp == 0f)
				{
					result2 = 35524.98f * amount;
				}
				else if (temp == 15f)
				{
					result2 = 35524.98f * amount;
				}
				else if (temp == 20f)
				{
					result2 = 35524.98f * amount;
				}
				return result2;
			case 7:
				return 70.135f * amount;
			}
			break;
		case "乙炔":
			switch (index)
			{
			case 1:
				if (temp == 0f)
				{
					result = 58.41f * amount;
				}
				else if (temp == 15f)
				{
					result = 58.41f * amount;
				}
				else if (temp == 20f)
				{
					result = 58.41f * amount;
				}
				return result;
			case 2:
				return 56.4f * amount;
			case 3:
				if (temp == 0f)
				{
					num = 0.9061f * amount;
				}
				else if (temp == 15f)
				{
					num = 0.9551f * amount;
				}
				else if (temp == 20f)
				{
					num = 0.9724f * amount;
				}
				return num;
			case 4:
				if (temp == 0f)
				{
					num = 1.1709f * amount;
				}
				else if (temp == 15f)
				{
					num = 1.1709f * amount;
				}
				else if (temp == 20f)
				{
					num = 1.1709f * amount;
				}
				result3 = 1.29222f * num;
				return num;
			case 5:
				return 13950.989f * amount;
			case 6:
				return 13470.909f * amount;
			case 7:
				return 26.038f * amount;
			}
			break;
		}
		return 0f;
	}
}
