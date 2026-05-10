using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Instrument
{
	public delegate void CloseInstrument(int instruNo, Instrument instrument);

	private const string string_0 = "别的用户已锁定仪器！";

	private const string string_1 = "仪器正在运行，请先停止采集！";

	private const string string_2 = "请先将仪器解锁！";

	private const string string_3 = "Instrument was locked by another user!";

	private const string string_4 = "Please stop sampling first!";

	private const string string_5 = "Please Unlock instrument!";

	public ASC_Sampler[] asc_Samplers = new ASC_Sampler[0];

	public int beginIdleTC;

	private BackgroundWorker backgroundWorker_0 = new BackgroundWorker();

	public Bitmap closedImage;

	public DtC_Channel[] dtc_Channels = new DtC_Channel[0];

	public InstrumentForm form;

	public GCC_GCs[] gcc_GCss = new GCC_GCs[0];

	private GradientRow gradientRow_0;

	public float idle_time;

	public string imgClosedFile = "";

	public string imgOpenedFile = "";

	public InjectStyle injectStyle;

	public InstruStyle instruStyle;

	private int int_0;

	public LCC_Pump[] lcc_Pumps = new LCC_Pump[0];

	public bool locked;

	public bool logged;

	public MtdSetup methodSetup = new MtdSetup();

	public string name = "";

	private static object object_0 = new object();

	private static OpenFileDialog openFileDialog_0 = new OpenFileDialog();

	public Bitmap openedImage;

	public int pageNo = -1;

	public PjtDir pjtDir;

	public Injection runningInjInfo = new Injection();

	public float sample_time;

	public bool sampling;

	public bool setuped;

	public Signal[] sglsSampling = new Signal[0];

	public string tmrFileName = "";

	public User user;

	private CloseInstrument closeInstrument_0;

	public bool AutoSample => asc_Samplers.Length != 0;

	public Point FormLocation
	{
		get
		{
			return form.Location;
		}
		set
		{
			form.Location = value;
		}
	}

	public Size FormSize => form.Size;

	public bool HasDADDetector
	{
		get
		{
			for (int i = 0; i < dtc_Channels.Length; i++)
			{
				if (dtc_Channels[i].detectorStyle == DetectorStyle.DAD)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool HasGeneralDetector
	{
		get
		{
			for (int i = 0; i < dtc_Channels.Length; i++)
			{
				if (dtc_Channels[i].detectorStyle == DetectorStyle.General)
				{
					return true;
				}
			}
			return false;
		}
	}

	public string InstruDir => Application.StartupPath + "\\";

	public string PrjPath => Application.StartupPath + "\\";

	private string sOtherUserLocked => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "别的用户已锁定仪器！", 
		SysLanguage.EN => "Instrument was locked by another user!", 
		_ => "", 
	};

	private string sRunning => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "仪器正在运行，请先停止采集！", 
		SysLanguage.EN => "Please stop sampling first!", 
		_ => "", 
	};

	private string sUnlockFirst => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "请先将仪器解锁！", 
		SysLanguage.EN => "Please Unlock instrument!", 
		_ => "", 
	};

	public event CloseInstrument OnCloseInstrument
	{
		add
		{
			CloseInstrument closeInstrument = closeInstrument_0;
			CloseInstrument closeInstrument2;
			do
			{
				closeInstrument2 = closeInstrument;
				CloseInstrument value2 = (CloseInstrument)Delegate.Combine(closeInstrument2, value);
				closeInstrument = Interlocked.CompareExchange(ref closeInstrument_0, value2, closeInstrument2);
			}
			while (closeInstrument != closeInstrument2);
		}
		remove
		{
			CloseInstrument closeInstrument = closeInstrument_0;
			CloseInstrument closeInstrument2;
			do
			{
				closeInstrument2 = closeInstrument;
				CloseInstrument value2 = (CloseInstrument)Delegate.Remove(closeInstrument2, value);
				closeInstrument = Interlocked.CompareExchange(ref closeInstrument_0, value2, closeInstrument2);
			}
			while (closeInstrument != closeInstrument2);
		}
	}

	public Instrument()
	{
		openFileDialog_0.Filter = "(*.jpg)|*.jpg";
		backgroundWorker_0.WorkerSupportsCancellation = true;
		backgroundWorker_0.DoWork += backgroundWorker_0_DoWork;
	}

	public void ApplyMethod()
	{
		form.dataAcqForm.ApplyMethod();
		if (sampling && methodSetup.chromInfoR.AcqAutoStop && methodSetup.chromInfoR.AcqRunTime > 0f && sample_time >= methodSetup.chromInfoR.AcqRunTime)
		{
			form.dataAcqForm.miAlyStopAcquisition_Click(null, null);
		}
	}

	private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
	{
		if (instruStyle != InstruStyle.LC)
		{
			return;
		}
		bool lcUse = methodSetup.chromInfoR.LcGradient.lcUse;
		bool uvUseProgWaves = methodSetup.chromInfoR.UvUseProgWaves;
		if (!(lcUse || uvUseProgWaves))
		{
			return;
		}
		bool flag = methodSetup.chromInfoR.LcGradient.gradientRows.Length != 0;
		bool flag2 = methodSetup.chromInfoR.UvProgWaves.Length != 0;
		while (!backgroundWorker_0.CancellationPending)
		{
			if (sampling)
			{
				if (lcUse && lcc_Pumps.Length != 0 && flag)
				{
					method_6(sample_time);
				}
				if (uvUseProgWaves && flag2)
				{
					int num = 0;
					for (int i = 0; i < methodSetup.chromInfoR.UvProgWaves.Length; i++)
					{
						if (sample_time >= methodSetup.chromInfoR.UvProgWaves[i].Time)
						{
							num = methodSetup.chromInfoR.UvProgWaves[i].Wave;
						}
					}
					if (190 <= num && num <= 720)
					{
						for (int j = 0; j < dtc_Channels.Length; j++)
						{
							if (dtc_Channels[j] is DtC_Detector)
							{
								(dtc_Channels[j] as DtC_Detector).Wave(write: true, (ushort)num);
							}
						}
					}
				}
				Thread.Sleep(300);
				continue;
			}
			switch (methodSetup.chromInfoR.LcGradient.idleStateProc)
			{
			case IdleStateProc.PumpOff:
				form.devMonitorForm.StopPumps();
				break;
			case IdleStateProc.Initial:
				if (flag)
				{
					method_6(0f);
				}
				break;
			case IdleStateProc.MonitorSet:
				form.devMonitorForm.SubmitFlows();
				break;
			}
			break;
		}
	}

	public bool CloseInstru()
	{
		if (logged)
		{
			if (locked)
			{
				MessageBox.Show(sUnlockFirst, Class49.smethod_13(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			if (sampling)
			{
				MessageBox.Show(sRunning, Class49.smethod_13(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			user.SaveWinInfo(form);
			form.Visible = false;
			form.HideChildWindows();
			logged = false;
			form.chromForm.miFiCloseAll_Click(null, null);
			if (method_7())
			{
				user.SaveUserOptions();
			}
			form.devMonitorForm.OnLogoutInstrument();
			if (closeInstrument_0 != null)
			{
				closeInstrument_0(pageNo, this);
			}
			Detector_stop(onlyVirtual: false);
			method_0(bool_0: false);
		}
		return true;
	}

	public void CreateForm()
	{
		try
		{
			form = new InstrumentForm(this);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void daf_BeginGather(bool sample, InjectStyle injectStyle)
	{
		if (runningInjInfo.methodFileName != "")
		{
			form.LoadMethodFile(runningInjInfo.methodFileName);
		}
		if (dtc_Channels[0] is DtC_Detector dtC_Detector)
		{
			methodSetup.chromInfoR.UvWave = dtC_Detector.wave;
			methodSetup.chromInfoR.UvRange = dtC_Detector.range.ToString("0.00");
			methodSetup.chromInfoR.UvRistTime = dtC_Detector.ristTime.ToString("0.0");
		}
		form.LoadReportStyleFile(runningInjInfo.reportStyleFileName);
		this.injectStyle = injectStyle;
		if (sample)
		{
			ResetSglsSamplingOriDots(sample);
		}
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			dtc_Channels[i].mark = 0;
			if (dtc_Channels[i] is DtC_Detector)
			{
				(dtc_Channels[i] as DtC_Detector).hwPara.acquisition_0 = methodSetup.dtcAcquisitions[i];
			}
		}
		sampling = sample;
		for (int j = 0; j < dtc_Channels.Length; j++)
		{
			if (dtc_Channels[j] is DtC_Detector)
			{
				(dtc_Channels[j] as DtC_Detector).BeginGather(sample);
			}
		}
		if (sampling)
		{
			InstrumentForm.PostMessage(form.Handle, 1028, (IntPtr)1, IntPtr.Zero);
			method_0(bool_0: true);
		}
	}

	private void method_0(bool bool_0)
	{
		if (bool_0 && !backgroundWorker_0.IsBusy)
		{
			backgroundWorker_0.RunWorkerAsync();
		}
	}

	public void daf_StopGather()
	{
		beginIdleTC = Environment.TickCount;
		sampling = false;
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			if (dtc_Channels[i] is DtC_Detector)
			{
				(dtc_Channels[i] as DtC_Detector).BeginGather(sample: false);
			}
		}
		form.RefreshInfo(injectStyle);
		InstrumentForm.PostMessage(form.Handle, 1028, IntPtr.Zero, IntPtr.Zero);
		method_0(bool_0: false);
	}

	private void method_1()
	{
		openFileDialog_0.InitialDirectory = ResourceImageLoad.PathInstruPic();
		switch (instruStyle)
		{
		case InstruStyle.GC:
			imgClosedFile = ResourceImageLoad.PathInstruPic() + "gcClosed";
			imgOpenedFile = ResourceImageLoad.PathInstruPic() + "gcOpened";
			break;
		case InstruStyle.LC:
			imgClosedFile = ResourceImageLoad.PathInstruPic() + "lcClosed";
			imgOpenedFile = ResourceImageLoad.PathInstruPic() + "lcOpened";
			break;
		}
	}

	public void DefaultSettings()
	{
		method_1();
		LoadClosedOpenedImage();
		switch (instruStyle)
		{
		case InstruStyle.GC:
			name = Lang.PS("气相", "Gas");
			break;
		case InstruStyle.LC:
			name = Lang.PS("液相", "Liquid");
			break;
		}
	}

	public void Detector_Set(bool zero)
	{
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			if (dtc_Channels[i] is DtC_Detector)
			{
				(dtc_Channels[i] as DtC_Detector).Detector_Set(zero);
			}
		}
	}

	public void Detector_stop(bool onlyVirtual)
	{
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			if (onlyVirtual)
			{
				if (!dtc_Channels[i].HasHardWare && dtc_Channels[i] is DtC_Detector)
				{
					(dtc_Channels[i] as DtC_Detector).Stop();
				}
			}
			else if (dtc_Channels[i] is DtC_Detector)
			{
				(dtc_Channels[i] as DtC_Detector).Stop();
			}
		}
	}

	private void method_2(int int_1)
	{
		bool needGather;
		if (form.dataAcqForm.dlgFg.fgUse && form.dataAcqForm.dlgFg.fgRows.Length != 0 && sampling && ((needGather = form.dataAcqForm.dlgFg.NeedGather(sample_time)) || sglsSampling[int_1].FgState != 0))
		{
			sglsSampling[int_1].JudgeFG(needGather);
		}
	}

	public void gc_StartAly()
	{
		form.dataAcqForm.miAlyRunSingle_Click(null, null);
	}

	public void gc_StopAly()
	{
		form.dataAcqForm.miAlyStopAcquisition_Click(null, null);
	}

	public void Initialize()
	{
		Array.Resize(ref sglsSampling, dtc_Channels.Length);
		float num = 0f;
		float num2 = 0.1f;
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			if (dtc_Channels[i] is DtC_Detector)
			{
				(dtc_Channels[i] as DtC_Detector).LoadVirtualSignal(i, num, num2, method_3);
				num += 0.23f;
				num2 += 0.1f;
			}
			dtc_Channels[i].Foo(i);
			dtc_Channels[i].OnGetNewSignals += method_4;
			if (sglsSampling[i] == null)
			{
				sglsSampling[i] = new Signal();
			}
			sglsSampling[i].detectorStyle = dtc_Channels[i].detectorStyle;
			sglsSampling[i].detector_name = dtc_Channels[i].name;
		}
		for (int j = 0; j < sglsSampling.Length; j++)
		{
			sglsSampling[j].instruMark = pageNo;
			sglsSampling[j].detectorMark = j;
		}
		SetSignalColor();
		beginIdleTC = Environment.TickCount;
	}

	private void method_3(int int_1, float float_0, bool bool_0)
	{
		lock (object_0)
		{
			if (bool_0 && methodSetup.chromInfoR.EcExternalControl)
			{
				int tickCount = Environment.TickCount;
				if (int_0 == 0 || tickCount - int_0 > 1000)
				{
					int_0 = tickCount;
					switch (methodSetup.chromInfoR.ExtCtrlStart)
					{
					case ExtCtrlStart.StartOnly:
						if (!sampling)
						{
							form.dataAcqForm.miAlyRunSingle_Click(null, null);
						}
						break;
					case ExtCtrlStart.StartRestart:
						if (sampling)
						{
							form.dataAcqForm.miAlyStopAcquisition_Click(null, null);
							form.dataAcqForm.miAlyRunSingle_Click(null, null);
						}
						else
						{
							form.dataAcqForm.miAlyRunSingle_Click(null, null);
						}
						break;
					case ExtCtrlStart.StartStop:
						if (sampling)
						{
							form.dataAcqForm.miAlyStopAcquisition_Click(null, null);
						}
						else
						{
							form.dataAcqForm.miAlyRunSingle_Click(null, null);
						}
						break;
					}
					return;
				}
			}
			if (sglsSampling[int_1].AddDot(float_0, out var newDot))
			{
				sample_time = newDot.X;
				if (!sampling)
				{
					idle_time = (float)(Environment.TickCount - beginIdleTC) / 60000f;
				}
				if (sample_time >= form.dataAcqForm.DisRt)
				{
					form.dataAcqForm.ChangeDisLg();
				}
				form.dataAcqForm.slbSignals[int_1].Tag = float_0;
				method_2(int_1);
				if (sampling && methodSetup.chromInfoR.AcqAutoStop && methodSetup.chromInfoR.AcqRunTime > 0f && newDot.X >= methodSetup.chromInfoR.AcqRunTime)
				{
					form.dataAcqForm.miAlyStopAcquisition_Click(null, null);
				}
			}
		}
	}

	private void method_4(int int_1, float[] float_0)
	{
	}

	public void LoadClosedOpenedImage()
	{
		if (openedImage != null)
		{
			openedImage.Dispose();
		}
		if (closedImage != null)
		{
			closedImage.Dispose();
		}
		closedImage = ResourceImageLoad.LoadBitmap(imgClosedFile);
		openedImage = ResourceImageLoad.LoadBitmap(imgOpenedFile);
		if (closedImage == null || openedImage == null)
		{
			method_1();
			closedImage = ResourceImageLoad.LoadBitmap(imgClosedFile);
			openedImage = ResourceImageLoad.LoadBitmap(imgOpenedFile);
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		name = binaryReader_0.ReadString();
		pageNo = binaryReader_0.ReadInt32();
		setuped = binaryReader_0.ReadBoolean();
		instruStyle = (InstruStyle)binaryReader_0.ReadByte();
		imgClosedFile = binaryReader_0.ReadString();
		imgOpenedFile = binaryReader_0.ReadString();
	}

	public void LoadFromObject(Instrument instrument)
	{
		setuped = instrument.setuped;
		instruStyle = instrument.instruStyle;
		name = instrument.name;
		if (instrument.closedImage != null)
		{
			closedImage = (Bitmap)instrument.closedImage.Clone();
		}
		if (instrument.openedImage != null)
		{
			openedImage = (Bitmap)instrument.openedImage.Clone();
		}
		imgClosedFile = instrument.imgClosedFile;
		imgOpenedFile = instrument.imgOpenedFile;
	}

	public void LockUnlockInstru()
	{
		if (locked)
		{
			if (Class49.loginDlg_0.ShowDialog(AccessType.Unlock))
			{
				if (Class49.loginDlg_0.user.Equals(user))
				{
					form.Visible = true;
					locked = false;
				}
				else
				{
					MessageBox.Show(sOtherUserLocked, Class49.smethod_13(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		else
		{
			form.Visible = false;
			locked = true;
		}
	}

	public void OpenInstru(User user)
	{
		if (user == null)
		{
			form.WindowState = FormWindowState.Normal;
			form.BringToFront();
			return;
		}
		this.user = user;
		user.ui_lastLogin = DateTime.Now;
		Initialize();
		logged = true;
		methodSetup.Init(this);
		form.dlgMethodSetup.OpenInstruInit();
		form.seqAlyForm.OpenInstrumentResetCounter();
		bool isGC = false;
		for (int i = 0; i < gcc_GCss.Length; i++)
		{
			if (gcc_GCss[i] is GC08_GCs)
			{
				isGC = true;
			}
		}
		for (int j = 0; j < dtc_Channels.Length; j++)
		{
			dtc_Channels[j].IsGC08 = isGC;
		}
		for (int k = 0; k < gcc_GCss.Length; k++)
		{
			if (gcc_GCss[k] is GC08_GCs)
			{
				(gcc_GCss[k] as GC08_GCs).LogIn(form.devMonitorForm, form.dlgMethodSetup, dtc_Channels);
			}
		}
		form.devMonitorForm.OnLoginInstrument();
		pjtDir = null;
		if (user.instrusWinsInfo[pageNo].valid)
		{
			user.instrusWinsInfo[pageNo].instruForm = form;
			user.instrusWinsInfo[pageNo].WriteToForm();
		}
		form.refreshTitle();
	}

	private void method_5(ref string string_6, string string_7)
	{
		if (!form.OverWrite && File.Exists(string_6))
		{
			string text = string_6.Remove(string_6.Length - string_7.Length);
			int num = 2;
			while (File.Exists(string_6 = text + num + string_7))
			{
				num++;
			}
		}
	}

	private void method_6(float float_0)
	{
		if (lcc_Pumps.Length == 0)
		{
			return;
		}
		GrdtOpt gradientOption = methodSetup.chromInfoR.LcGradient.gradientOption;
		if (!retGrdtRow(float_0, ref gradientRow_0))
		{
			return;
		}
		if (lcc_Pumps.Length == 1)
		{
			lcc_Pumps[0].Flow(write: true, gradientRow_0.flow);
		}
		else if (lcc_Pumps.Length == 2)
		{
			if (gradientOption.SolventNum != 2)
			{
				return;
			}
			float num = 0f;
			float num2 = 0f;
			if (gradientOption.hasSolvent1)
			{
				num = gradientRow_0.float_0;
				if (gradientOption.hasSolvent2)
				{
					num2 = gradientRow_0.float_1;
				}
				else if (gradientOption.hasSolvent3)
				{
					num2 = gradientRow_0.float_2;
				}
				else if (gradientOption.hasSolvent4)
				{
					num2 = gradientRow_0.float_3;
				}
			}
			else if (gradientOption.hasSolvent2)
			{
				num = gradientRow_0.float_1;
				if (gradientOption.hasSolvent3)
				{
					num2 = gradientRow_0.float_2;
				}
				else if (gradientOption.hasSolvent4)
				{
					num2 = gradientRow_0.float_3;
				}
			}
			else if (gradientOption.hasSolvent3)
			{
				num = gradientRow_0.float_2;
				num2 = gradientRow_0.float_3;
			}
			lcc_Pumps[0].Flow(write: true, gradientRow_0.flow * num);
			lcc_Pumps[1].Flow(write: true, gradientRow_0.flow * num2);
		}
		else if (lcc_Pumps.Length == 4 && gradientOption.SolventNum == 4)
		{
			lcc_Pumps[0].Flow(write: true, gradientRow_0.flow * gradientRow_0.float_0);
			lcc_Pumps[1].Flow(write: true, gradientRow_0.flow * gradientRow_0.float_1);
			lcc_Pumps[2].Flow(write: true, gradientRow_0.flow * gradientRow_0.float_2);
			lcc_Pumps[3].Flow(write: true, gradientRow_0.flow * gradientRow_0.float_3);
		}
	}

	public void RefreshName()
	{
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			sglsSampling[i].detector_name = dtc_Channels[i].name;
		}
	}

	public void ResetSglsSamplingOriDots(bool createDiskFile)
	{
		for (int i = 0; i < sglsSampling.Length; i++)
		{
			sglsSampling[i].ResetOriDots(createDiskFile);
		}
	}

	public bool retGrdtRow(float float_0, ref GradientRow gradientRow_1)
	{
		GradientRow[] gradientRows = methodSetup.chromInfoR.LcGradient.gradientRows;
		int num = gradientRows.Length;
		if (num == 0)
		{
			return false;
		}
		GrdtOpt gradientOption = methodSetup.chromInfoR.LcGradient.gradientOption;
		gradientRow_1 = gradientRows[0];
		if (float_0 != 0f)
		{
			if (float_0 >= gradientRows[num - 1].time)
			{
				gradientRow_1 = gradientRows[num - 1];
			}
			else
			{
				for (int i = 0; i < num - 1; i++)
				{
					if (gradientRows[i].time <= float_0 && float_0 <= gradientRows[i + 1].time)
					{
						double num2 = (float_0 - gradientRows[i].time) / (gradientRows[i + 1].time - gradientRows[i].time);
						if (gradientOption.hasSolvent1)
						{
							gradientRow_1.float_0 = Convert.ToSingle((double)gradientRows[i].float_0 + (double)(gradientRows[i + 1].float_0 - gradientRows[i].float_0) * num2);
						}
						if (gradientOption.hasSolvent2)
						{
							gradientRow_1.float_1 = Convert.ToSingle((double)gradientRows[i].float_1 + (double)(gradientRows[i + 1].float_1 - gradientRows[i].float_1) * num2);
						}
						if (gradientOption.hasSolvent3)
						{
							gradientRow_1.float_2 = Convert.ToSingle((double)gradientRows[i].float_2 + (double)(gradientRows[i + 1].float_2 - gradientRows[i].float_2) * num2);
						}
						if (gradientOption.hasSolvent4)
						{
							gradientRow_1.float_3 = Convert.ToSingle((double)gradientRows[i].float_3 + (double)(gradientRows[i + 1].float_3 - gradientRows[i].float_3) * num2);
						}
						gradientRow_1.flow = Convert.ToSingle((double)gradientRows[i].flow + (double)(gradientRows[i + 1].flow - gradientRows[i].flow) * num2);
					}
				}
			}
		}
		return true;
	}

	public void Save()
	{
		int num = sglsSampling.Length;
		if (num == 0)
		{
			return;
		}
		string prjPath = PrjPath;
		prjPath = ((!runningInjInfo.cali_stand) ? (prjPath + (prjPath.EndsWith("\\") ? "" : "\\") + "Data") : (prjPath + (prjPath.EndsWith("\\") ? "" : "\\") + "Calib"));
		Chromatogram chromatogram = new Chromatogram();
		chromatogram.injAnalysis.LoadFromObject(runningInjInfo);
		chromatogram.injAnalysis.tsAcquire = DateTime.Now.Subtract(runningInjInfo.dtAcquire);
		int index = form.devMonitorForm.clmCT6SetT.Index;
		for (int i = 0; i < methodSetup.chromInfoR.GcProgTemp.SetT6.Length; i++)
		{
			object value = form.devMonitorForm.dgvCT6.Rows[i].Cells[index].Value;
			if (value != null)
			{
				methodSetup.chromInfoR.GcProgTemp.SetT6[i] = Class49.String2Float(value, 0f);
			}
		}
		chromatogram.chromInfoR.LoadFromObject(methodSetup.chromInfoR);
		Array.Resize(ref chromatogram.userArchives, 1);
		UserArchive userArchive = (chromatogram.userArchives[0] = new UserArchive());
		userArchive.userName = user.u_name;
		userArchive.openTime = DateTime.Now;
		userArchive.saveTime = DateTime.Now;
		userArchive.chromInfo.LoadFromObject(methodSetup.chromInfo);
		userArchive.chromInfo.LoadFromInjAnalysis(chromatogram.injAnalysis);
		chromatogram.canSetRs = true;
		if (runningInjInfo.openChromWin)
		{
			form.btnChromWindow_Click(null, null);
			form.chromForm.ChkOverlayMode();
		}
		if (runningInjInfo.openCaliWin)
		{
			form.btnCaliWindow_Click(null, null);
			form.caliGnlForm.CloseAllChroms();
		}
		string[] array = new string[num];
		if (num == 1)
		{
			string string_ = prjPath + "\\" + tmrFileName + ".sda";
			method_5(ref string_, ".sda");
			chromatogram.signal = sglsSampling[0];
			if (chromatogram.signal.DotsNum < 10)
			{
				return;
			}
			userArchive.integ.LoadFromObject(methodSetup.sigIntegrations[0]);
			chromatogram.SaveToFile(string_);
			array[0] = string_;
		}
		else
		{
			bool flag = true;
			int num2 = 0;
			while (num2 < sglsSampling.Length)
			{
				for (int j = num2 + 1; j < sglsSampling.Length; j++)
				{
					if (sglsSampling[j].detector_name == sglsSampling[num2].detector_name)
					{
						flag = false;
						num2++;
						break;
					}
				}
			}
			int newSize = 0;
			for (num2 = 0; num2 < sglsSampling.Length; num2++)
			{
				string string_2 = prjPath + "\\" + tmrFileName + "." + (flag ? sglsSampling[num2].detector_name : num2.ToString()) + ".sda";
				method_5(ref string_2, ".sda");
				chromatogram.signal = sglsSampling[num2];
				if (chromatogram.signal.DotsNum >= 10)
				{
					userArchive.integ.LoadFromObject(methodSetup.sigIntegrations[num2]);
					chromatogram.SaveToFile(string_2);
					array[newSize++] = string_2;
				}
			}
			Array.Resize(ref array, newSize);
		}
		if (runningInjInfo.openChromWin)
		{
			form.chromForm.ChkOverlayMode();
			for (int k = 0; k < array.Length; k++)
			{
				form.chromForm.OpenChrom(array[k], sampling: true, useCurrent: false);
			}
		}
		if (runningInjInfo.openCaliWin)
		{
			for (int l = 0; l < array.Length; l++)
			{
				form.caliGnlForm.OpenChrom(array[l]);
			}
		}
		if (runningInjInfo.openCaliWin && num == 1)
		{
			form.caliGnlForm.AutoAddLevel();
		}
		if (runningInjInfo.openPrintWin)
		{
			form.dlgReportSetup.Print(array);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(name);
		binaryWriter_0.Write(pageNo);
		binaryWriter_0.Write(setuped);
		binaryWriter_0.Write((byte)instruStyle);
		binaryWriter_0.Write(imgClosedFile);
		binaryWriter_0.Write(imgOpenedFile);
	}

	public bool SetImageDialog(bool opened)
	{
		if (openFileDialog_0.ShowDialog() != DialogResult.OK)
		{
			return false;
		}
		if (opened)
		{
			imgOpenedFile = openFileDialog_0.FileName;
			if (openedImage != null)
			{
				openedImage.Dispose();
			}
			openedImage = ResourceImageLoad.LoadBitmap(imgOpenedFile);
		}
		else
		{
			imgClosedFile = openFileDialog_0.FileName;
			if (closedImage != null)
			{
				closedImage.Dispose();
			}
			closedImage = ResourceImageLoad.LoadBitmap(imgClosedFile);
		}
		return true;
	}

	public void SetInstrumentStyle(InstruStyle instruStyle, int pageNo)
	{
		this.instruStyle = instruStyle;
		this.pageNo = pageNo;
	}

	public void SetSignalColor()
	{
		for (int i = 0; i < sglsSampling.Length; i++)
		{
			if (i == 0 && user.options.dt1cAsInstru)
			{
				sglsSampling[i].disColor = Class49.GetColor(pageNo);
			}
			else
			{
				sglsSampling[i].disColor = user.options.dtColors[i];
			}
		}
		if (form.chromForm != null)
		{
			form.chromForm.SetSignalsColor();
		}
	}

	private bool method_7()
	{
		for (int i = 0; i < SysCfgDlg.sysConfig.pageInstrus.Length; i++)
		{
			if (SysCfgDlg.sysConfig.pageInstrus[i].logged && SysCfgDlg.sysConfig.pageInstrus[i].user == user)
			{
				return false;
			}
		}
		return true;
	}
}
