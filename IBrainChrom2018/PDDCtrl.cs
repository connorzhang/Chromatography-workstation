using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HZH_Controls.Controls;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class PDDCtrl : UserControl
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private SystemParam sysParam;

	private OnLineCtrlParam onLineCtrlParam = OnLineCtrlParam.Create();

	private static MisMgrAssist myself = null;

	public bool m_bLoading = true;

	public bool bSaveHe = false;

	public static PDDCtrl self;

	public SerialPortBase serialPoartBase = new SerialPortBase();

	private IContainer components = null;

	private Button btnDownload;

	private Label label76;

	private Button MethodReSave;

	private Button MethodSave;

	private Button MethodOpen;

	public TextBox tbMethName;

	private Label label1;

	private Label label2;

	private UCSwitch uSAutoMode;

	private DateTimePicker dateTimePicker2;

	private DateTimePicker dateTimePicker1;

	private System.Windows.Forms.Timer timer1;

	public UCSwitch uSHe;

	public PDDCtrl()
	{
		self = this;
		InitializeComponent();
		sysParam = SystemParam.Create();
		initForm();
		try
		{
			serialPoartBase.openPort();
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError($"流路串口 COM{ex.Message}");
		}
		m_bLoading = false;
	}

	public void initForm()
	{
		dateTimePicker1.Value = onLineCtrlParam.dataTimeStart;
		dateTimePicker2.Value = onLineCtrlParam.dataTimeEnd;
		uSAutoMode.Checked = onLineCtrlParam.bAutoModeHe;
	}

	public MisMgr MakeMisData()
	{
		ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
		if (chromDeviceListMgr.CurrentChromDevice == null)
		{
			return null;
		}
		chromDeviceListMgr.formMain.UpdateMisMgr();
		return chromDeviceListMgr.CurrentChromDevice.misMgr;
	}

	private void MethodReSave_Click(object sender, EventArgs e)
	{
		MisMgr misMgr = MakeMisData();
		MisMgr misMgr2 = new MisMgr();
		misMgr2.ChannelChartParaS = misMgr.ChannelChartParaS;
		misMgr2.devManager = misMgr.devManager;
		misMgr2.nchannel = misMgr.nchannel;
		misMgr2.m_strExt = "mis";
		if (misMgr2 == null)
		{
			MessageBox.Show("请先选择设备!");
			return;
		}
		misMgr2.m_strExt = "mis";
		IBaseFileMgr.SaveFile(misMgr2);
		if (IBaseFileMgr.m_strFilePath != "")
		{
			sysParam.strMisDataFilePath = Path.GetFullPath(IBaseFileMgr.m_strFilePath);
			sysParam.SaveParam();
		}
	}

	private void MethodOpen_Click(object sender, EventArgs e)
	{
		MisMgr baseFile = new MisMgr();
		baseFile = (MisMgr)IBaseFileMgr.OpenFile(baseFile);
		if (baseFile != null)
		{
			baseFile.m_strExt = "mis";
			MisMgrAssist misMgrAssist = MisMgrAssist.Create();
			misMgrAssist.SetFormFromMisData(baseFile);
			if (IBaseFileMgr.m_strFilePath != "")
			{
				tbMethName.Text = IBaseFileMgr.m_strFilePath;
				sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
				sysParam.SaveParam();
			}
		}
	}

	private void MethodSave_Click(object sender, EventArgs e)
	{
		MisMgrAssist misMgrAssist = MisMgrAssist.Create();
		MisMgr misMgr = misMgrAssist.MakeMisData();
		MisMgr misMgr2 = new MisMgr();
		misMgr2.ChannelChartParaS = misMgr.ChannelChartParaS;
		misMgr2.devManager = misMgr.devManager;
		misMgr2.nchannel = misMgr.nchannel;
		misMgr2.m_strExt = "mis";
		if (tbMethName.Text != "")
		{
			IBaseFileMgr.SaveFile(tbMethName.Text, misMgr2);
		}
		else
		{
			IBaseFileMgr.SaveFile(misMgr2);
			tbMethName.Text = IBaseFileMgr.m_strFilePath;
		}
		if (IBaseFileMgr.m_strFilePath != "")
		{
			sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
			sysParam.SaveParam();
		}
	}

	private void btnDownload_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			LogMgr.Instance.Write2RunLog("PDDCtrl.btnDownload_Click   start");
			MisMgr misMgr = new MisMgr();
			IBaseFileMgr.m_strFilePath = tbMethName.Text;
			misMgr = (MisMgr)IBaseFileMgr.OpenFile(tbMethName.Text);
			if (misMgr == null)
			{
				return;
			}
			List<EpcDeviceSetting> epcDev = misMgr.devManager.epcDev1;
			misMgr.m_strExt = "mis";
			MisMgrAssist misMgrAssist = MisMgrAssist.Create();
			misMgrAssist.SetFormFromMisData(misMgr);
			byte[] byte_ = new byte[0];
			byte[] array = Encoding.ASCII.GetBytes("GCHH");
			int num = 4;
			Array.Resize(ref array, 2000);
			array[num++] = 7;
			array[num++] = 208;
			array[num++] = 2;
			num++;
			float num2 = 0f;
			num2 = misMgr.devManager.tempSetedList[1];
			int num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempHoldTime;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			for (int i = 0; i < misMgr.devManager.tempSettingList.Count; i++)
			{
				float.TryParse(misMgr.devManager.tempSettingList[i].tempStart.ToString(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				float.TryParse(misMgr.devManager.tempSettingList[i].tempEnd.ToString(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				float.TryParse(misMgr.devManager.tempSettingList[i].tempKeep.ToString(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
			}
			bool flag = true;
			array[num++] |= 128;
			num2 = 0f;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = 20f;
			num3 = (int)num2;
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempSetedList[2];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[9].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[10].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[11].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag2 = true;
			array[num++] |= 128;
			num2 = 0f;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = 20f;
			num3 = (int)num2;
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempSetedList[3];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[12].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[13].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[14].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag3 = true;
			array[num++] |= 128;
			num2 = 0f;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = 20f;
			num3 = (int)num2;
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = misMgr.devManager.tempSetedList[5];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag4 = true;
			array[num++] |= 128;
			num2 = misMgr.devManager.tempSetedList[0];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[1].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[2].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag5 = true;
			array[num++] |= 128;
			num2 = misMgr.devManager.tempSetedList[4];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[4].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[5].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			bool flag6 = true;
			array[num++] |= 128;
			num2 = misMgr.devManager.tempSetedList[5];
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[7].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num2 = epcDev[8].pressureData;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			array[num] = epcDev[0].gasType;
			if (epcDev[0].ctrlModel == 1)
			{
				array[num++] &= 127;
			}
			else
			{
				array[num++] |= 128;
			}
			IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.ToBcd_2B(epcDev[0].pressureData, 1));
			num3 = Convert.ToInt32(epcDev[0].pressureData * 10f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[0].pressureData, 1);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[0].chromColLenth * 100f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[0].chromColLenth, 2);
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[0].chromColDiameter * 100f);
			byte_ = IBrainConvert.ToBcd_3B_new(epcDev[0].chromColDiameter, 3);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			array[num++] = byte_[2];
			array[num++] = 0;
			array[num] = epcDev[3].gasType;
			epcDev[3].ctrlModel = 0;
			if (epcDev[3].ctrlModel == 1)
			{
				array[num++] &= 127;
			}
			else
			{
				array[num++] |= 128;
			}
			num3 = Convert.ToInt32(epcDev[3].pressureData * 10f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[3].pressureData, 1);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[3].chromColLenth * 100f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[3].chromColLenth, 2);
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[3].chromColDiameter * 100f);
			byte_ = IBrainConvert.ToBcd_3B_new(epcDev[3].chromColDiameter, 3);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			array[num++] = byte_[2];
			array[num] = epcDev[6].gasType;
			if (epcDev[6].ctrlModel == 1)
			{
				array[num++] &= 127;
			}
			else
			{
				array[num++] |= 128;
			}
			num3 = Convert.ToInt32(epcDev[6].pressureData * 10f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[6].pressureData, 1);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[6].chromColLenth * 100f);
			byte_ = IBrainConvert.ToBcd_2B(epcDev[6].chromColLenth, 2);
			array[num++] = byte_[1];
			num3 = Convert.ToInt32(epcDev[6].chromColDiameter * 100f);
			byte_ = IBrainConvert.ToBcd_3B_new(epcDev[6].chromColDiameter, 3);
			array[num++] = byte_[0];
			array[num++] = byte_[1];
			array[num++] = byte_[2];
			num = 204;
			for (int j = 0; j < 8; j++)
			{
				if (j < 4)
				{
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[0];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[1];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[2];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[3];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[4];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[5];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[6];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl0[j].fRowList[7];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
				}
				else
				{
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[0];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[1];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[2];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[3];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[4];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[5];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[6];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
					num2 = misMgr.devManager.eventCtrl1[j - 4].fRowList[7];
					num3 = Convert.ToInt32(num2 * 100f);
					array[num++] = (byte)(num3 >> 16);
					array[num++] = (byte)(num3 >> 8);
					array[num++] = (byte)num3;
				}
			}
			if (cdlMgr.CurrentTcpServerSocket != null)
			{
				cdlMgr.CurrentTcpServerSocket.SendData(array);
			}
			for (int k = 0; k < 100; k++)
			{
				Thread.Sleep(50);
				Application.DoEvents();
			}
			downLoadParameter3(misMgr.devManager.tempSetedList[2]);
			Class49.InsertIntoTable(Class49.string_9[1], Class49.user_0.u_name, "", Lang.PS("下载仪器参数方法"), Lang.PS("下载仪器参数方法:") + tbMethName.Text);
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void downLoadParameter3(float ftemp)
	{
		List<EpcDeviceSetting> epcDevR = InsDeviceCtrl.self.epcDevR;
		byte[] array = Encoding.ASCII.GetBytes("GCHH");
		int num = 4;
		Array.Resize(ref array, 100);
		array[num++] = 7;
		array[num++] = 208;
		array[num++] = 6;
		array[num++] = 0;
		float num2 = 0f;
		bool flag = true;
		array[num++] = 1;
		num2 = ftemp;
		int num3 = (int)(num2 * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		num3 = (int)(epcDevR[9].pressureData * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		num3 = (int)(epcDevR[10].pressureData * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		num3 = (int)(epcDevR[11].pressureData * 10f);
		array[num++] = (byte)(num3 >> 8);
		array[num++] = (byte)num3;
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			cdlMgr.CurrentTcpServerSocket.SendData(array);
		}
	}

	private void MethodNew_Click(object sender, EventArgs e)
	{
	}

	private void uSAutoMode_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			onLineCtrlParam.bAutoModeHe = uSAutoMode.Checked;
			onLineCtrlParam.SaveParam();
		}
	}

	private void uSHe_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			InsDeviceCtrl.self.checkBox_6.Checked = uSHe.Checked;
			InsDeviceCtrl.self.button33_Click(null, null);
		}
	}

	private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			onLineCtrlParam.dataTimeStart = dateTimePicker1.Value;
			onLineCtrlParam.SaveParam();
		}
	}

	private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			onLineCtrlParam.dataTimeEnd = dateTimePicker2.Value;
			onLineCtrlParam.SaveParam();
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (DateTime.Now.Hour == dateTimePicker1.Value.Hour && DateTime.Now.Minute == dateTimePicker1.Value.Minute && !uSHe.Checked && uSAutoMode.Checked)
		{
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			uSHe.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
		}
		if (DateTime.Now.Hour == dateTimePicker2.Value.Hour && DateTime.Now.Minute == dateTimePicker2.Value.Minute && uSHe.Checked && uSAutoMode.Checked)
		{
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			uSHe.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
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
		this.btnDownload = new System.Windows.Forms.Button();
		this.label76 = new System.Windows.Forms.Label();
		this.MethodReSave = new System.Windows.Forms.Button();
		this.MethodSave = new System.Windows.Forms.Button();
		this.MethodOpen = new System.Windows.Forms.Button();
		this.tbMethName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.uSAutoMode = new HZH_Controls.Controls.UCSwitch();
		this.uSHe = new HZH_Controls.Controls.UCSwitch();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.btnDownload.Location = new System.Drawing.Point(253, 3);
		this.btnDownload.Name = "btnDownload";
		this.btnDownload.Size = new System.Drawing.Size(103, 54);
		this.btnDownload.TabIndex = 46;
		this.btnDownload.Text = "下载到仪器";
		this.btnDownload.UseVisualStyleBackColor = true;
		this.btnDownload.Click += new System.EventHandler(btnDownload_Click);
		this.label76.AutoSize = true;
		this.label76.Location = new System.Drawing.Point(8, 9);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(65, 12);
		this.label76.TabIndex = 45;
		this.label76.Text = "参数方法：";
		this.MethodReSave.Location = new System.Drawing.Point(168, 34);
		this.MethodReSave.Name = "MethodReSave";
		this.MethodReSave.Size = new System.Drawing.Size(70, 23);
		this.MethodReSave.TabIndex = 42;
		this.MethodReSave.Text = "另存";
		this.MethodReSave.UseVisualStyleBackColor = true;
		this.MethodReSave.Click += new System.EventHandler(MethodReSave_Click);
		this.MethodSave.Location = new System.Drawing.Point(79, 34);
		this.MethodSave.Name = "MethodSave";
		this.MethodSave.Size = new System.Drawing.Size(75, 23);
		this.MethodSave.TabIndex = 43;
		this.MethodSave.Text = "保存";
		this.MethodSave.UseVisualStyleBackColor = true;
		this.MethodSave.Click += new System.EventHandler(MethodSave_Click);
		this.MethodOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen.Location = new System.Drawing.Point(207, 3);
		this.MethodOpen.Name = "MethodOpen";
		this.MethodOpen.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen.TabIndex = 41;
		this.MethodOpen.UseVisualStyleBackColor = true;
		this.MethodOpen.Click += new System.EventHandler(MethodOpen_Click);
		this.tbMethName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName.Location = new System.Drawing.Point(79, 7);
		this.tbMethName.Name = "tbMethName";
		this.tbMethName.ReadOnly = true;
		this.tbMethName.Size = new System.Drawing.Size(117, 21);
		this.tbMethName.TabIndex = 40;
		this.tbMethName.Text = "默认";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.Location = new System.Drawing.Point(365, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 16);
		this.label1.TabIndex = 49;
		this.label1.Text = "省气开始";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.Location = new System.Drawing.Point(365, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(72, 16);
		this.label2.TabIndex = 50;
		this.label2.Text = "省气结束";
		this.uSAutoMode.BackColor = System.Drawing.Color.Transparent;
		this.uSAutoMode.Checked = false;
		this.uSAutoMode.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSAutoMode.FalseTextColr = System.Drawing.Color.White;
		this.uSAutoMode.Location = new System.Drawing.Point(368, 83);
		this.uSAutoMode.Name = "uSAutoMode";
		this.uSAutoMode.Size = new System.Drawing.Size(81, 31);
		this.uSAutoMode.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSAutoMode.TabIndex = 52;
		this.uSAutoMode.Texts = new string[2] { "自动", "手动" };
		this.uSAutoMode.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSAutoMode.TrueTextColr = System.Drawing.Color.White;
		this.uSAutoMode.CheckedChanged += new System.EventHandler(uSAutoMode_CheckedChanged);
		this.uSHe.BackColor = System.Drawing.Color.Transparent;
		this.uSHe.Checked = false;
		this.uSHe.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSHe.FalseTextColr = System.Drawing.Color.White;
		this.uSHe.Location = new System.Drawing.Point(541, 83);
		this.uSHe.Name = "uSHe";
		this.uSHe.Size = new System.Drawing.Size(81, 31);
		this.uSHe.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSHe.TabIndex = 51;
		this.uSHe.Texts = new string[2] { "断", "通" };
		this.uSHe.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSHe.TrueTextColr = System.Drawing.Color.White;
		this.uSHe.CheckedChanged += new System.EventHandler(uSHe_CheckedChanged);
		this.dateTimePicker2.CustomFormat = "HH:mm";
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker2.Location = new System.Drawing.Point(443, 37);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.ShowUpDown = true;
		this.dateTimePicker2.Size = new System.Drawing.Size(151, 21);
		this.dateTimePicker2.TabIndex = 54;
		this.dateTimePicker2.ValueChanged += new System.EventHandler(dateTimePicker2_ValueChanged_1);
		this.dateTimePicker1.CustomFormat = "HH:mm";
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker1.Location = new System.Drawing.Point(443, 7);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.ShowUpDown = true;
		this.dateTimePicker1.Size = new System.Drawing.Size(151, 21);
		this.dateTimePicker1.TabIndex = 55;
		this.dateTimePicker1.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
		this.dateTimePicker1.ValueChanged += new System.EventHandler(dateTimePicker1_ValueChanged);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.Controls.Add(this.dateTimePicker1);
		base.Controls.Add(this.dateTimePicker2);
		base.Controls.Add(this.uSAutoMode);
		base.Controls.Add(this.uSHe);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnDownload);
		base.Controls.Add(this.label76);
		base.Controls.Add(this.MethodReSave);
		base.Controls.Add(this.MethodSave);
		base.Controls.Add(this.MethodOpen);
		base.Controls.Add(this.tbMethName);
		base.Name = "PDDCtrl";
		base.Size = new System.Drawing.Size(820, 144);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
