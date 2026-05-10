using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class UsbSZ : SLUSBXpressDLL
{
	public delegate void GetHwData(float mV, bool bool_0);

	public const int READE_SIZE = 10;

	public const int WRITE_SIZE = 8;

	public ControlModule[] applyCMs = new ControlModule[1] { ControlModule.Set };

	private BackgroundWorker backgroundWorker_0 = new BackgroundWorker();

	private byte[] byte_0 = new byte[10];

	private long long_0;

	private double double_0;

	private double double_1;

	public bool hasShowMsg;

	public uint hUSBDevice;

	private HwPara hwPara_0;

	private HwPara hwPara_1;

	public readonly HwStyle hwStyle = HwStyle.SZ;

	public bool installed;

	private byte[] byte_1 = new byte[8];

	public int int_0;

	public string productString = Lang.PS("赛智卡", "hzSZ Card");

	private Random random_0 = new Random();

	private double double_2;

	private bool bool_0;

	private GetHwData getHwData_0;

	private GetHwData getHwData_1;

	public event GetHwData OnGetHwData0
	{
		add
		{
			GetHwData getHwData = getHwData_0;
			GetHwData getHwData2;
			do
			{
				getHwData2 = getHwData;
				GetHwData value2 = (GetHwData)Delegate.Combine(getHwData2, value);
				getHwData = Interlocked.CompareExchange(ref getHwData_0, value2, getHwData2);
			}
			while (getHwData != getHwData2);
		}
		remove
		{
			GetHwData getHwData = getHwData_0;
			GetHwData getHwData2;
			do
			{
				getHwData2 = getHwData;
				GetHwData value2 = (GetHwData)Delegate.Remove(getHwData2, value);
				getHwData = Interlocked.CompareExchange(ref getHwData_0, value2, getHwData2);
			}
			while (getHwData != getHwData2);
		}
	}

	public event GetHwData OnGetHwData1
	{
		add
		{
			GetHwData getHwData = getHwData_1;
			GetHwData getHwData2;
			do
			{
				getHwData2 = getHwData;
				GetHwData value2 = (GetHwData)Delegate.Combine(getHwData2, value);
				getHwData = Interlocked.CompareExchange(ref getHwData_1, value2, getHwData2);
			}
			while (getHwData != getHwData2);
		}
		remove
		{
			GetHwData getHwData = getHwData_1;
			GetHwData getHwData2;
			do
			{
				getHwData2 = getHwData;
				GetHwData value2 = (GetHwData)Delegate.Remove(getHwData2, value);
				getHwData = Interlocked.CompareExchange(ref getHwData_1, value2, getHwData2);
			}
			while (getHwData != getHwData2);
		}
	}

	public UsbSZ()
	{
		backgroundWorker_0.WorkerReportsProgress = true;
		backgroundWorker_0.WorkerSupportsCancellation = true;
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
		backgroundWorker_0.ProgressChanged += backgroundWorker_0_ProgressChanged;
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		long num = PerformanceCounter();
		while (!backgroundWorker_0.CancellationPending)
		{
			HwData hwData = new HwData();
			int lpdwBytesWritten = 0;
			int lpdwBytesReturned = 0;
			int num2 = 0;
			if (SLUSBXpressDLL.SI_Write(hUSBDevice, ref byte_1[0], 8, ref lpdwBytesWritten, 0) == 0 && lpdwBytesWritten == 8)
			{
				if (SLUSBXpressDLL.SI_Read(hUSBDevice, ref byte_0[0], 10, ref lpdwBytesReturned, 0) != 0 || lpdwBytesReturned != 10)
				{
					continue;
				}
				int num3 = byte_0[0] << 24 >> 8;
				num3 += byte_0[1] << 8;
				num3 += byte_0[2];
				hwData.mV0 = Convert.ToSingle((double)num3 * double_0);
				num3 = byte_0[3] << 24 >> 8;
				num3 += byte_0[4] << 8;
				num3 += byte_0[5];
				hwData.mV1 = Convert.ToSingle((double)num3 * double_1);
				hwData.key0 = GetBitFromChar(byte_0[6], 0) != '\0';
				hwData.key1 = GetBitFromChar(byte_0[6], 1) != '\0';
				backgroundWorker_0.ReportProgress(0, hwData);
			}
			else
			{
				num2++;
				if (num2 > 50)
				{
					backgroundWorker_0.ReportProgress(0, null);
					break;
				}
			}
			long num4 = PerformanceCounter();
			if (num4 <= num)
			{
				num = num4;
			}
			double num5 = num4 - num;
			for (double num6 = num5 / (double)long_0; num6 < double_2; num6 = num5 / (double)long_0)
			{
				Thread.Sleep(1);
				num4 = PerformanceCounter();
				if (num4 <= num)
				{
					num = num4;
				}
				num5 = num4 - num;
			}
			num = num4;
		}
	}

	private void backgroundWorker_0_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		if (e.UserState == null)
		{
			MessageBox.Show("数据采集失败！");
			return;
		}
		HwData hwData = e.UserState as HwData;
		if (hwPara_0.working && getHwData_0 != null)
		{
			getHwData_0(hwData.mV0, hwData.key0);
		}
		if (hwPara_1.working && getHwData_1 != null)
		{
			getHwData_1(hwData.mV1, hwData.key1);
		}
	}

	public void EndDevice()
	{
		SLUSBXpressDLL.SI_Close(hUSBDevice);
	}

	public void ExecutePara(byte channel, HwPara hwPara)
	{
		switch (channel)
		{
		case 0:
			hwPara_0 = hwPara;
			break;
		case 1:
			hwPara_1 = hwPara;
			break;
		}
		bool_0 = hwPara_0.working || hwPara_1.working;
		if (bool_0)
		{
			lock (byte_1)
			{
				float num = Math.Max(hwPara_0.acquisition_0.AcqRate, hwPara_1.acquisition_0.AcqRate);
				char myData = SetBitInChar(SetBitInChar(Convert.ToChar(0), 0, bitSet: true), 1, bitSet: true);
				if (num == 15f)
				{
					myData = SetBitInChar(SetBitInChar(SetBitInChar(myData, 2, bitSet: false), 3, bitSet: false), 4, bitSet: true);
				}
				else if (num == 30f)
				{
					myData = SetBitInChar(SetBitInChar(SetBitInChar(myData, 2, bitSet: false), 3, bitSet: true), 4, bitSet: false);
				}
				else if (num == 60f)
				{
					myData = SetBitInChar(SetBitInChar(SetBitInChar(myData, 2, bitSet: false), 3, bitSet: true), 4, bitSet: true);
				}
				else
				{
					if (num != 120f)
					{
						throw new Exception("不支持的采样频率");
					}
					myData = SetBitInChar(SetBitInChar(SetBitInChar(myData, 2, bitSet: true), 3, bitSet: false), 4, bitSet: false);
				}
				myData = SetBitInChar(SetBitInChar(SetBitInChar(myData, 5, bitSet: false), 6, bitSet: false), 7, bitSet: false);
				byte_1[0] = Convert.ToByte(myData);
				byte_1[1] = 0;
				byte_1[2] = 0;
				byte_1[3] = 0;
				byte_1[4] = 0;
				byte_1[5] = 0;
				byte_1[6] = 0;
				byte_1[7] = 0;
				double_2 = 1.0 / (double)num;
				long_0 = PerformanceFrequency();
				double_0 = hwPara_0.acquisition_0.AcqRange / 8388607f;
				double_1 = hwPara_1.acquisition_0.AcqRange / 8388607f;
			}
			if (!backgroundWorker_0.IsBusy)
			{
				backgroundWorker_0.RunWorkerAsync();
			}
		}
		if (!bool_0 && backgroundWorker_0.IsBusy)
		{
			backgroundWorker_0.CancelAsync();
		}
	}

	public static char GetBitFromChar(byte myData, int nBit)
	{
		return Convert.ToChar(myData & (1 << nBit));
	}

	public bool Install(ControlModule ctrlModule, bool installed)
	{
		bool flag = false;
		for (int i = 0; i < applyCMs.Length; i++)
		{
			if (applyCMs[i] == ctrlModule)
			{
				if (1 == 0)
				{
					return false;
				}
				this.installed = installed;
				return true;
			}
		}
		return false;
	}

	public static long PerformanceCounter()
	{
		long long_ = 0L;
		QueryPerformanceCounter(ref long_);
		return long_;
	}

	public static long PerformanceFrequency()
	{
		long long_ = 0L;
		QueryPerformanceFrequency(ref long_);
		return long_;
	}

	[DllImport("kernel32.dll")]
	private static extern short QueryPerformanceCounter(ref long long_1);

	[DllImport("kernel32.dll")]
	private static extern short QueryPerformanceFrequency(ref long long_1);

	public static char SetBitInChar(char myData, int nBit, bool bitSet)
	{
		char c = Convert.ToChar(0);
		if (bitSet)
		{
			c = Convert.ToChar(1 << nBit);
			return Convert.ToChar(myData | c);
		}
		c = Convert.ToChar(1 << nBit);
		return Convert.ToChar((int)(myData & ~(uint)c));
	}

	public void SetLink(byte channel, GetHwData getHwData)
	{
		switch (channel)
		{
		case 0:
			getHwData_0 = getHwData;
			break;
		case 1:
			getHwData_1 = getHwData;
			break;
		}
	}

	public bool WriteCmd(byte[] cmdBuf)
	{
		if (cmdBuf.Length != 8)
		{
			return false;
		}
		if (int_0 != 0)
		{
			string text = "";
			for (int i = 0; i < cmdBuf.Length; i++)
			{
				text = text + cmdBuf[i] + " ";
			}
			MessageBox.Show(int_0.ToString("0") + "\n" + hUSBDevice + "\n" + text);
		}
		int_0 = 0;
		int lpdwBytesWritten = 0;
		if (SLUSBXpressDLL.SI_Write(hUSBDevice, ref cmdBuf[0], 8, ref lpdwBytesWritten, 0) == 0 && lpdwBytesWritten == 8)
		{
			hasShowMsg = false;
			return true;
		}
		if (!hasShowMsg)
		{
			MessageBox.Show("写命令失败！");
			hasShowMsg = true;
		}
		return false;
	}
}
