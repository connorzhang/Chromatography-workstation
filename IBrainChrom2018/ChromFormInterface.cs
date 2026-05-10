using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public interface ChromFormInterface
{
	bool IsLoaded { get; }

	bool BAlarm { get; set; }

	bool bAlarmMy { get; set; }

	bool bStart1 { get; set; }

	bool bStart2 { get; set; }

	bool bAutoCycle1 { get; set; }

	bool bAutoCycle2 { get; set; }

	uint cntChannel1 { get; set; }

	uint cntChannel2 { get; set; }

	uint cntAnalyze1 { get; set; }

	uint cntAnalyze2 { get; set; }

	string[] StrAlarmArray { get; set; }

	string StrAlarmFile { get; set; }

	bool IsDisposed2 { get; }

	int IsAutoCalibra { get; set; }

	int AutoCalibraPoint { get; set; }

	string CurrentGCID { get; }

	FrmChromatManager FrmChromat { get; }

	int CurrentChannelIndex { get; }

	SampleDisplay sampleDisplay { get; }

	int SetFireLengthValue { get; set; }

	int FIDCount { get; set; }

	DisLg disLg { get; }

	TabControl tabChannel { get; }

	CheckBox cbEnNMHC { get; }

	FrmTempNameSet FTNS { get; }

	ToolStripMenuItem tsmiFileMain { get; }

	ToolStripStatusLabel ToolLabelPeakInfo2 { get; }

	MbSerialPort ModbusComClient { get; }

	ModbusSlave mComModbus { get; }

	ModbusSlave mComModbus2 { get; }

	int StateYiqi { get; set; }

	Label labFireState { get; }

	TextBox tbMachineState { get; }

	long CountAnalyse { get; }

	float shuaijian { get; }

	float shuaijian2 { get; }

	float shuaijian3 { get; }

	int iChannel { get; set; }

	MstSet MainmstSet { get; }

	InsDeviceCtrl insDeviceCtrl { get; }

	FormVOC formVoc { get; }

	Button button37 { get; }

	Button button38 { get; }

	FrmChromatManager FrmEquip { get; }

	FrmMsetup fset { get; }

	Frmmultivalve Fmultivalve { get; }

	ChromDeviceCtrl chrDeviceCtrl { get; }

	ChromAcqCtrl chrAcqCtrl { get; }

	ChromFormCtrl chromFormCtrl { get; set; }

	VocCtrl vocctrl { get; }

	TabControl tabControl { get; }

	bool StartBtEnable { get; }

	bool StopBtEnable { get; }

	void ModbusComSendData(byte[] data);

	void SetZero();

	TcpServerSocket GetCurrentTcpSocket();

	void SetCurrentChromDevice();

	void SetShowMainmstSet(bool bShow);

	void SetShowWindow(bool bShow);

	void SetShowFullScreen(bool bFull);

	void SelectChannelIdx();

	void ReloadMisMgr();

	void ReloadMisMgr2();

	void UpdateMisMgr();

	void StartGather();

	void StopGather();

	void ClearGather();

	void UpdateAutoSamplerState();

	void DtrTempNameSelect();

	void DtrTempNameEnableSelect();

	void MultiValveselect();

	void MultiValveSet();

	void DtrTempNameSet();

	void DtrTempNameEnableSt();

	object Invoke(Delegate method);

	void CompServer_OnReceiveData(string ID);

	void tabChannel_SelectedIndexChanged(object sender, EventArgs e);

	void ChangeDisLg();

	void FID1Fire();

	void FID2Fire();

	void DtrSet();

	void DtrSetFireLength();

	void DtrSelectFireLength();

	void UpdateControlTempText(bool bCtrl);
}
