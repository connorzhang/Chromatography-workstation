using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HZH_Controls;
using HZH_Controls.Controls;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ZWSCtrl : UserControl
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private SystemParam sysParam;

	private OnLineCtrlParam onLineCtrlParam = OnLineCtrlParam.Create();

	public List<PortableGridModel> lstSource1 = new List<PortableGridModel>();

	private static MisMgrAssist myself = null;

	public bool m_bLoading = true;

	public bool bSaveHe = false;

	public static ZWSCtrl self;

	public ushort u16Step = 0;

	public float u32CntSecond = 0f;

	public bool bStart = false;

	public bool bSendCMDAn = false;

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

	private Timer timer1;

	public UCSwitch uSHe;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private UCTextBoxEx uTStep1;

	private UCTextBoxEx uTStep8;

	private UCTextBoxEx uTStep7;

	private UCTextBoxEx uTStep6;

	private UCTextBoxEx uTStep5;

	private UCTextBoxEx uTStep4;

	private UCTextBoxEx uTStep3;

	private UCTextBoxEx uTStep2;

	private Label labStep1;

	private Label labStep4;

	private Label labStep3;

	private Label labStep2;

	private Label labStep8;

	private Label labStep7;

	private Label labStep6;

	private Label labStep5;

	private UCBtnExt uBtnStart;

	private Label labTime;

	private TabPage tabPage3;

	private UCBtnExt ucBtnCheck;

	private UCBtnExt ucBtnSet;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label label11;

	private UCTextBoxEx ucTextBoxEx10;

	private UCTextBoxEx ucTextBoxEx9;

	private UCTextBoxEx ucTextBoxEx8;

	private UCTextBoxEx ucTextBoxEx7a;

	private UCTextBoxEx ucTextBoxEx7;

	private UCTextBoxEx ucTextBoxEx6;

	private UCTextBoxEx ucTextBoxEx5;

	private UCTextBoxEx ucTextBoxEx3;

	private Label label12;

	private UCTextBoxEx ucTextBoxEx1;

	private Label label13;

	private UCTextBoxEx ucTextBoxEx2;

	private Label label14;

	private UCTextBoxEx ucTextBoxEx4;

	private Label label15;

	private UCTextBoxEx ucTextBoxEx7times;

	private Label label16;

	private UCTextBoxEx ucTextBoxEx11;

	private Label label3;

	private UCTextBoxEx ucTextBoxEx12;

	private Label labStep;

	public ZWSCtrl()
	{
		self = this;
		InitializeComponent();
		sysParam = SystemParam.Create();
		initForm();
		m_bLoading = false;
	}

	public void initForm()
	{
		AutoScroll = true;
		tabPage2.Parent = null;
		dateTimePicker1.Value = onLineCtrlParam.dataTimeStart;
		dateTimePicker2.Value = onLineCtrlParam.dataTimeEnd;
		uSAutoMode.Checked = onLineCtrlParam.bAutoModeHe;
		uTStep1.txtInput.KeyDown += upSetStep1;
		uTStep2.txtInput.KeyDown += upSetStep2;
		uTStep3.txtInput.KeyDown += upSetStep3;
		uTStep4.txtInput.KeyDown += upSetStep4;
		uTStep5.txtInput.KeyDown += upSetStep5;
		uTStep6.txtInput.KeyDown += upSetStep6;
		uTStep7.txtInput.KeyDown += upSetStep7;
		uTStep8.txtInput.KeyDown += upSetStep8;
		uTStep1.InputText = onLineCtrlParam.fStepTime[0].ToString();
		uTStep2.InputText = onLineCtrlParam.fStepTime[1].ToString();
		uTStep3.InputText = onLineCtrlParam.fStepTime[2].ToString();
		uTStep4.InputText = onLineCtrlParam.fStepTime[3].ToString();
		uTStep5.InputText = onLineCtrlParam.fStepTime[4].ToString();
		uTStep6.InputText = onLineCtrlParam.fStepTime[5].ToString();
		uTStep7.InputText = onLineCtrlParam.fStepTime[6].ToString();
		uTStep8.InputText = onLineCtrlParam.fStepTime[7].ToString();
		uTStep1.IsShowKeyboard = true;
		uTStep2.IsShowKeyboard = true;
		uTStep3.IsShowKeyboard = true;
		uTStep4.IsShowKeyboard = true;
		uTStep5.IsShowKeyboard = true;
		uTStep6.IsShowKeyboard = true;
		uTStep7.IsShowKeyboard = true;
		uTStep8.IsShowKeyboard = true;
		ucTextBoxEx1.IsShowKeyboard = true;
		ucTextBoxEx2.IsShowKeyboard = true;
		ucTextBoxEx3.IsShowKeyboard = true;
		ucTextBoxEx4.IsShowKeyboard = true;
		ucTextBoxEx5.IsShowKeyboard = true;
		ucTextBoxEx6.IsShowKeyboard = true;
		ucTextBoxEx7.IsShowKeyboard = true;
		ucTextBoxEx7a.IsShowKeyboard = true;
		ucTextBoxEx7times.IsShowKeyboard = true;
		ucTextBoxEx8.IsShowKeyboard = true;
		ucTextBoxEx9.IsShowKeyboard = true;
		ucTextBoxEx10.IsShowKeyboard = true;
		ucTextBoxEx11.IsShowKeyboard = true;
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
		if (misMgr == null)
		{
			MessageBox.Show("请先选择设备!");
			return;
		}
		misMgr.m_strExt = "mis";
		IBaseFileMgr.SaveFile(misMgr);
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
		misMgr.m_strExt = "mis";
		if (IBaseFileMgr.m_strFilePath != "")
		{
			IBaseFileMgr.SaveFile(IBaseFileMgr.m_strFilePath, misMgr);
		}
		else
		{
			IBaseFileMgr.SaveFile(misMgr);
		}
		if (IBaseFileMgr.m_strFilePath != "")
		{
			tbMethName.Text = IBaseFileMgr.m_strFilePath;
			sysParam.strMisDataFilePath = IBaseFileMgr.m_strFilePath;
			sysParam.SaveParam();
		}
	}

	private void btnDownload_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			LogMgr.Instance.Write2RunLog("PDDCtrl.btnDownload_Click   start");
			byte[] array = Encoding.ASCII.GetBytes("GCHH");
			int num = 4;
			Array.Resize(ref array, 2000);
			array[num++] = 7;
			array[num++] = 208;
			array[num++] = 2;
			num++;
			float num2 = 0f;
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[1].Cells[2].Value.ToString());
			int num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			float.TryParse(cdlMgr.formMain.insDeviceCtrl.tbptIniTempHoldT.Text.Trim(), out num2);
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			for (int i = 0; i < cdlMgr.formMain.insDeviceCtrl.gdTempControl.RowCount; i++)
			{
				float.TryParse(cdlMgr.formMain.insDeviceCtrl.gdTempControl.Rows[i].Cells[1].Value.ToString().Trim(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				float.TryParse(cdlMgr.formMain.insDeviceCtrl.gdTempControl.Rows[i].Cells[2].Value.ToString().Trim(), out num2);
				num3 = (int)(num2 * 10f);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				float.TryParse(cdlMgr.formMain.insDeviceCtrl.gdTempControl.Rows[i].Cells[3].Value.ToString().Trim(), out num2);
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
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[2].Cells[2].Value.ToString());
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
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[4].Cells[2].Value.ToString());
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
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[5].Cells[2].Value.ToString());
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
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[0].Cells[2].Value.ToString());
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
			bool flag5 = true;
			array[num++] |= 128;
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[3].Cells[2].Value.ToString());
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
			bool flag6 = true;
			array[num++] |= 128;
			num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.dgtempControl.Rows[5].Cells[2].Value.ToString());
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
			array[num] = 0;
			bool flag7 = true;
			array[num++] &= 127;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			float.TryParse(cdlMgr.formMain.insDeviceCtrl.maskedTextBox9.Text.Trim(), out num2);
			num3 = (int)(num2 * 100f);
			array[num++] = (byte)(num3 >> 16);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			float.TryParse(cdlMgr.formMain.insDeviceCtrl.maskedTextBox8.Text.Trim(), out num2);
			num3 = (int)(num2 * 1000f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			array[num] = 0;
			bool flag8 = true;
			array[num++] &= 127;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			array[num] = 0;
			bool flag9 = true;
			array[num++] &= 127;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			num3 = (int)(num2 * 10f);
			array[num++] = (byte)(num3 >> 8);
			array[num++] = (byte)num3;
			for (int j = 1; j < 9; j++)
			{
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[0].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[1].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[2].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[3].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[4].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[5].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[6].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
				num2 = float.Parse(cdlMgr.formMain.insDeviceCtrl.gvExtEvTP.Rows[7].Cells[j].Value.ToString()) * 100f;
				num3 = (int)num2;
				array[num++] = (byte)(num3 >> 16);
				array[num++] = (byte)(num3 >> 8);
				array[num++] = (byte)num3;
			}
			cdlMgr.CurrentTcpServerSocket.SendData(array);
			LogMgr.Instance.Write2RunLog("PDDCtrl.btnDownload_Click   end");
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
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
		if (bSendCMDAn)
		{
			bSendCMDAn = false;
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				return;
			}
		}
		if (bStart)
		{
			u32CntSecond += 1f / 60f;
			labTime.Text = "时间: " + u32CntSecond.ToString("0.00") + " min";
			upColor(u16Step);
			switch (u16Step)
			{
			case 0:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[0])
				{
					u16Step = 1;
					u32CntSecond = 0f;
					setMtep(u16Step);
				}
				break;
			case 1:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[1])
				{
					u16Step = 2;
					u32CntSecond = 0f;
					setMtep(u16Step);
				}
				break;
			case 2:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[2])
				{
					u16Step = 3;
					u32CntSecond = 0f;
					setMtep(u16Step);
				}
				break;
			case 3:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[3])
				{
					u16Step = 4;
					u32CntSecond = 0f;
					setMtep(u16Step);
				}
				break;
			case 4:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[4])
				{
					u16Step = 5;
					u32CntSecond = 0f;
					setMtep(u16Step);
				}
				break;
			case 5:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[5])
				{
					u16Step = 6;
					u32CntSecond = 0f;
					bSendCMDAn = true;
					cdlMgr.CurrentTcpServerSocket.SendCmd(18);
					setMtep(u16Step);
				}
				break;
			case 6:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[6])
				{
					u16Step = 7;
					u32CntSecond = 0f;
					setMtep(u16Step);
				}
				break;
			case 7:
				if (u32CntSecond >= onLineCtrlParam.fStepTime[7])
				{
					u16Step = 8;
					bStart = false;
					u32CntSecond = 0f;
					uBtnStart.BtnText = "开始";
					setMtep(u16Step);
				}
				break;
			case 8:
				break;
			}
		}
		else
		{
			upColor(8);
			labTime.Text = "时间: 0 min";
		}
	}

	private void uBtnStart_BtnClick(object sender, EventArgs e)
	{
		if (uBtnStart.BtnText == "开始")
		{
			bStart = true;
			u32CntSecond = 0f;
			uBtnStart.BtnText = "结束";
			u16Step = 0;
			if (u16Step == 0)
			{
				setMtep(u16Step);
				return;
			}
			u16Step = 1;
			setMtep(u16Step);
		}
		else
		{
			bStart = false;
			u32CntSecond = 0f;
			uBtnStart.BtnText = "开始";
			u16Step = 0;
			setMtep(8);
		}
	}

	public void upColor(int iStep)
	{
		switch (iStep)
		{
		case 0:
			labStep1.BackColor = Color.OrangeRed;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		case 1:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.OrangeRed;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		case 2:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.OrangeRed;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		case 3:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.OrangeRed;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		case 4:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.OrangeRed;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		case 5:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.OrangeRed;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		case 6:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.OrangeRed;
			labStep8.BackColor = Color.Transparent;
			break;
		case 7:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.OrangeRed;
			break;
		case 8:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		default:
			labStep1.BackColor = Color.Transparent;
			labStep2.BackColor = Color.Transparent;
			labStep3.BackColor = Color.Transparent;
			labStep4.BackColor = Color.Transparent;
			labStep5.BackColor = Color.Transparent;
			labStep6.BackColor = Color.Transparent;
			labStep7.BackColor = Color.Transparent;
			labStep8.BackColor = Color.Transparent;
			break;
		}
	}

	public void setMtep(int iStep)
	{
		switch (iStep)
		{
		case 0:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = true;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = true;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 1:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = true;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = true;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 2:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 3:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 4:
			InsDeviceCtrl.self.checkBox_0.Checked = true;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = true;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = true;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = true;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 5:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 6:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = true;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		case 7:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = true;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = true;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = true;
			InsDeviceCtrl.self.checkBox_6.Checked = true;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		default:
			InsDeviceCtrl.self.checkBox_0.Checked = false;
			InsDeviceCtrl.self.checkBox_1.Checked = false;
			InsDeviceCtrl.self.checkBox_3.Checked = false;
			InsDeviceCtrl.self.checkBox_5.Checked = false;
			InsDeviceCtrl.self.checkBox_2.Checked = false;
			InsDeviceCtrl.self.checkBox_4.Checked = false;
			InsDeviceCtrl.self.checkBox_6.Checked = false;
			InsDeviceCtrl.self.checkBox_7.Checked = false;
			InsDeviceCtrl.self.button33_Click(null, null);
			break;
		}
	}

	public void upSetStep1(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep1.InputText, out onLineCtrlParam.fStepTime[0]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep2(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep2.InputText, out onLineCtrlParam.fStepTime[1]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep3(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep3.InputText, out onLineCtrlParam.fStepTime[2]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep4(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep4.InputText, out onLineCtrlParam.fStepTime[3]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep5(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep5.InputText, out onLineCtrlParam.fStepTime[4]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep6(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep6.InputText, out onLineCtrlParam.fStepTime[5]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep7(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep7.InputText, out onLineCtrlParam.fStepTime[6]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void upSetStep8(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float.TryParse(uTStep8.InputText, out onLineCtrlParam.fStepTime[7]);
			onLineCtrlParam.SaveParam();
		}
	}

	public void updateState(byte bStep, byte bTime)
	{
		switch (bStep)
		{
		case 1:
			labStep.Text = "步骤：等待1";
			break;
		case 2:
			labStep.Text = "步骤：吹扫1";
			break;
		case 3:
			labStep.Text = "步骤：吹扫2";
			break;
		case 4:
			labStep.Text = "步骤：等待2";
			break;
		case 5:
			labStep.Text = "步骤：制冷1";
			break;
		case 6:
			labStep.Text = "步骤：制冷2";
			break;
		case 7:
			labStep.Text = "步骤：富集";
			break;
		case 8:
			labStep.Text = "步骤：间隔";
			break;
		case 9:
			labStep.Text = "步骤：加热";
			break;
		case 10:
			labStep.Text = "步骤：进样";
			break;
		case 11:
			labStep.Text = "步骤：检测";
			break;
		case 12:
			labStep.Text = "步骤：吹扫";
			break;
		default:
			labStep.Text = "--";
			break;
		}
		labStep.Text = labStep.Text + " 次数：" + bTime + "(255次后计数归零)";
	}

	public void decodePar(byte[] arrByte)
	{
		float num = 0f;
		uint num2 = 0u;
		num2 = (uint)((arrByte[0] << 16) | (arrByte[1] << 8) | arrByte[2]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx1.InputText = num.ToString();
		num2 = (uint)((arrByte[3] << 16) | (arrByte[4] << 8) | arrByte[5]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx2.InputText = num.ToString();
		num2 = (uint)((arrByte[6] << 16) | (arrByte[7] << 8) | arrByte[8]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx3.InputText = num.ToString();
		num2 = (uint)((arrByte[9] << 16) | (arrByte[10] << 8) | arrByte[11]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx4.InputText = num.ToString();
		num2 = (uint)((arrByte[12] << 16) | (arrByte[13] << 8) | arrByte[14]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx5.InputText = num.ToString();
		num2 = (uint)((arrByte[15] << 16) | (arrByte[16] << 8) | arrByte[17]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx6.InputText = num.ToString();
		num2 = (uint)((arrByte[18] << 16) | (arrByte[19] << 8) | arrByte[20]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx7.InputText = num.ToString();
		num2 = (uint)((arrByte[21] << 16) | (arrByte[22] << 8) | arrByte[23]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx7a.InputText = num.ToString();
		num2 = (uint)((arrByte[24] << 16) | (arrByte[25] << 8) | arrByte[26]);
		num = Convert.ToSingle(num2);
		ucTextBoxEx7times.InputText = num.ToString();
		num2 = (uint)((arrByte[27] << 16) | (arrByte[28] << 8) | arrByte[29]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx8.InputText = num.ToString();
		num2 = (uint)((arrByte[30] << 16) | (arrByte[31] << 8) | arrByte[32]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx9.InputText = num.ToString();
		num2 = (uint)((arrByte[33] << 16) | (arrByte[34] << 8) | arrByte[35]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx10.InputText = num.ToString();
		num2 = (uint)((arrByte[36] << 16) | (arrByte[37] << 8) | arrByte[38]);
		num = Convert.ToSingle((double)num2 / 100.0);
		ucTextBoxEx11.InputText = num.ToString();
		num2 = (uint)((arrByte[39] << 16) | (arrByte[40] << 8) | arrByte[41]);
		num = Convert.ToSingle(num2);
		ucTextBoxEx12.InputText = num.ToString();
	}

	private void ucBtnSet_BtnClick(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			byte[] array = new byte[47];
			float result = 0f;
			uint num = 0u;
			array[0] = 175;
			array[1] = 174;
			array[2] = 173;
			array[3] = 172;
			array[4] = 2;
			float.TryParse(ucTextBoxEx1.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[5] = (byte)(num >> 16);
			array[6] = (byte)(num >> 8);
			array[7] = (byte)num;
			float.TryParse(ucTextBoxEx2.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[8] = (byte)(num >> 16);
			array[9] = (byte)(num >> 8);
			array[10] = (byte)num;
			float.TryParse(ucTextBoxEx3.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[11] = (byte)(num >> 16);
			array[12] = (byte)(num >> 8);
			array[13] = (byte)num;
			float.TryParse(ucTextBoxEx4.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[14] = (byte)(num >> 16);
			array[15] = (byte)(num >> 8);
			array[16] = (byte)num;
			float.TryParse(ucTextBoxEx5.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[17] = (byte)(num >> 16);
			array[18] = (byte)(num >> 8);
			array[19] = (byte)num;
			float.TryParse(ucTextBoxEx6.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[20] = (byte)(num >> 16);
			array[21] = (byte)(num >> 8);
			array[22] = (byte)num;
			float.TryParse(ucTextBoxEx7.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[23] = (byte)(num >> 16);
			array[24] = (byte)(num >> 8);
			array[25] = (byte)num;
			float.TryParse(ucTextBoxEx7a.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[26] = (byte)(num >> 16);
			array[27] = (byte)(num >> 8);
			array[28] = (byte)num;
			float.TryParse(ucTextBoxEx7times.InputText, out result);
			result *= 1f;
			num = Convert.ToUInt32(result);
			array[29] = (byte)(num >> 16);
			array[30] = (byte)(num >> 8);
			array[31] = (byte)num;
			float.TryParse(ucTextBoxEx8.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[32] = (byte)(num >> 16);
			array[33] = (byte)(num >> 8);
			array[34] = (byte)num;
			float.TryParse(ucTextBoxEx9.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[35] = (byte)(num >> 16);
			array[36] = (byte)(num >> 8);
			array[37] = (byte)num;
			float.TryParse(ucTextBoxEx10.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[38] = (byte)(num >> 16);
			array[39] = (byte)(num >> 8);
			array[40] = (byte)num;
			float.TryParse(ucTextBoxEx11.InputText, out result);
			result *= 100f;
			num = Convert.ToUInt32(result);
			array[41] = (byte)(num >> 16);
			array[42] = (byte)(num >> 8);
			array[43] = (byte)num;
			float.TryParse(ucTextBoxEx12.InputText, out result);
			result *= 1f;
			num = Convert.ToUInt32(result);
			array[44] = (byte)(num >> 16);
			array[45] = (byte)(num >> 8);
			array[46] = (byte)num;
			cdlMgr.CurrentTcpServerSocket.SendData(array);
		}
	}

	private void ucBtnCheck_BtnClick(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			byte[] dataBuff = new byte[5] { 175, 174, 173, 172, 1 };
			cdlMgr.CurrentTcpServerSocket.SendData(dataBuff);
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
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.labTime = new System.Windows.Forms.Label();
		this.labStep8 = new System.Windows.Forms.Label();
		this.labStep7 = new System.Windows.Forms.Label();
		this.labStep6 = new System.Windows.Forms.Label();
		this.labStep5 = new System.Windows.Forms.Label();
		this.labStep4 = new System.Windows.Forms.Label();
		this.labStep3 = new System.Windows.Forms.Label();
		this.labStep2 = new System.Windows.Forms.Label();
		this.labStep1 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.label3 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.uBtnStart = new HZH_Controls.Controls.UCBtnExt();
		this.uTStep8 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep7 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep6 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep5 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep4 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep3 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uTStep1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uSAutoMode = new HZH_Controls.Controls.UCSwitch();
		this.uSHe = new HZH_Controls.Controls.UCSwitch();
		this.ucTextBoxEx12 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx11 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx7times = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx4 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucBtnCheck = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnSet = new HZH_Controls.Controls.UCBtnExt();
		this.ucTextBoxEx10 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx9 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx8 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx7a = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx7 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx6 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx5 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTextBoxEx3 = new HZH_Controls.Controls.UCTextBoxEx();
		this.labStep = new System.Windows.Forms.Label();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.tabPage3.SuspendLayout();
		base.SuspendLayout();
		this.btnDownload.Location = new System.Drawing.Point(301, 55);
		this.btnDownload.Name = "btnDownload";
		this.btnDownload.Size = new System.Drawing.Size(103, 54);
		this.btnDownload.TabIndex = 46;
		this.btnDownload.Text = "下载到仪器";
		this.btnDownload.UseVisualStyleBackColor = true;
		this.btnDownload.Click += new System.EventHandler(btnDownload_Click);
		this.label76.AutoSize = true;
		this.label76.Location = new System.Drawing.Point(56, 61);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(65, 12);
		this.label76.TabIndex = 45;
		this.label76.Text = "参数方法：";
		this.MethodReSave.Location = new System.Drawing.Point(216, 86);
		this.MethodReSave.Name = "MethodReSave";
		this.MethodReSave.Size = new System.Drawing.Size(70, 23);
		this.MethodReSave.TabIndex = 42;
		this.MethodReSave.Text = "另存";
		this.MethodReSave.UseVisualStyleBackColor = true;
		this.MethodReSave.Click += new System.EventHandler(MethodReSave_Click);
		this.MethodSave.Location = new System.Drawing.Point(127, 86);
		this.MethodSave.Name = "MethodSave";
		this.MethodSave.Size = new System.Drawing.Size(75, 23);
		this.MethodSave.TabIndex = 43;
		this.MethodSave.Text = "保存";
		this.MethodSave.UseVisualStyleBackColor = true;
		this.MethodSave.Click += new System.EventHandler(MethodSave_Click);
		this.MethodOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen.Location = new System.Drawing.Point(255, 55);
		this.MethodOpen.Name = "MethodOpen";
		this.MethodOpen.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen.TabIndex = 41;
		this.MethodOpen.UseVisualStyleBackColor = true;
		this.MethodOpen.Click += new System.EventHandler(MethodOpen_Click);
		this.tbMethName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName.Location = new System.Drawing.Point(127, 59);
		this.tbMethName.Name = "tbMethName";
		this.tbMethName.ReadOnly = true;
		this.tbMethName.Size = new System.Drawing.Size(117, 21);
		this.tbMethName.TabIndex = 40;
		this.tbMethName.Text = "默认";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.Location = new System.Drawing.Point(413, 64);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 16);
		this.label1.TabIndex = 49;
		this.label1.Text = "省气开始";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.Location = new System.Drawing.Point(413, 93);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(72, 16);
		this.label2.TabIndex = 50;
		this.label2.Text = "省气结束";
		this.dateTimePicker2.CustomFormat = "HH:mm";
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker2.Location = new System.Drawing.Point(491, 89);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.ShowUpDown = true;
		this.dateTimePicker2.Size = new System.Drawing.Size(151, 21);
		this.dateTimePicker2.TabIndex = 54;
		this.dateTimePicker2.ValueChanged += new System.EventHandler(dateTimePicker2_ValueChanged_1);
		this.dateTimePicker1.CustomFormat = "HH:mm";
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker1.Location = new System.Drawing.Point(491, 59);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.ShowUpDown = true;
		this.dateTimePicker1.Size = new System.Drawing.Size(151, 21);
		this.dateTimePicker1.TabIndex = 55;
		this.dateTimePicker1.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
		this.dateTimePicker1.ValueChanged += new System.EventHandler(dateTimePicker1_ValueChanged);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Location = new System.Drawing.Point(0, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(342, 667);
		this.tabControl1.TabIndex = 56;
		this.tabPage1.AutoScroll = true;
		this.tabPage1.BackColor = System.Drawing.Color.White;
		this.tabPage1.Controls.Add(this.labTime);
		this.tabPage1.Controls.Add(this.uBtnStart);
		this.tabPage1.Controls.Add(this.labStep8);
		this.tabPage1.Controls.Add(this.labStep7);
		this.tabPage1.Controls.Add(this.labStep6);
		this.tabPage1.Controls.Add(this.labStep5);
		this.tabPage1.Controls.Add(this.labStep4);
		this.tabPage1.Controls.Add(this.labStep3);
		this.tabPage1.Controls.Add(this.labStep2);
		this.tabPage1.Controls.Add(this.labStep1);
		this.tabPage1.Controls.Add(this.uTStep8);
		this.tabPage1.Controls.Add(this.uTStep7);
		this.tabPage1.Controls.Add(this.uTStep6);
		this.tabPage1.Controls.Add(this.uTStep5);
		this.tabPage1.Controls.Add(this.uTStep4);
		this.tabPage1.Controls.Add(this.uTStep3);
		this.tabPage1.Controls.Add(this.uTStep2);
		this.tabPage1.Controls.Add(this.uTStep1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(334, 641);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "A型机参数";
		this.labTime.AutoSize = true;
		this.labTime.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labTime.Location = new System.Drawing.Point(198, 297);
		this.labTime.Name = "labTime";
		this.labTime.Size = new System.Drawing.Size(53, 21);
		this.labTime.TabIndex = 22;
		this.labTime.Text = "0 min";
		this.labStep8.AutoSize = true;
		this.labStep8.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep8.Location = new System.Drawing.Point(7, 245);
		this.labStep8.Name = "labStep8";
		this.labStep8.Size = new System.Drawing.Size(129, 21);
		this.labStep8.TabIndex = 20;
		this.labStep8.Text = "检测+吹扫(min):";
		this.labStep7.AutoSize = true;
		this.labStep7.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep7.Location = new System.Drawing.Point(51, 211);
		this.labStep7.Name = "labStep7";
		this.labStep7.Size = new System.Drawing.Size(85, 21);
		this.labStep7.TabIndex = 19;
		this.labStep7.Text = "进样(min):";
		this.labStep6.AutoSize = true;
		this.labStep6.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep6.Location = new System.Drawing.Point(51, 177);
		this.labStep6.Name = "labStep6";
		this.labStep6.Size = new System.Drawing.Size(85, 21);
		this.labStep6.TabIndex = 18;
		this.labStep6.Text = "加热(min):";
		this.labStep5.AutoSize = true;
		this.labStep5.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep5.Location = new System.Drawing.Point(51, 143);
		this.labStep5.Name = "labStep5";
		this.labStep5.Size = new System.Drawing.Size(85, 21);
		this.labStep5.TabIndex = 17;
		this.labStep5.Text = "富集(min):";
		this.labStep4.AutoSize = true;
		this.labStep4.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep4.Location = new System.Drawing.Point(51, 109);
		this.labStep4.Name = "labStep4";
		this.labStep4.Size = new System.Drawing.Size(85, 21);
		this.labStep4.TabIndex = 16;
		this.labStep4.Text = "准备(min):";
		this.labStep3.AutoSize = true;
		this.labStep3.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep3.Location = new System.Drawing.Point(42, 75);
		this.labStep3.Name = "labStep3";
		this.labStep3.Size = new System.Drawing.Size(94, 21);
		this.labStep3.TabIndex = 15;
		this.labStep3.Text = "致冷2(min):";
		this.labStep2.AutoSize = true;
		this.labStep2.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep2.Location = new System.Drawing.Point(42, 41);
		this.labStep2.Name = "labStep2";
		this.labStep2.Size = new System.Drawing.Size(94, 21);
		this.labStep2.TabIndex = 14;
		this.labStep2.Text = "致冷1(min):";
		this.labStep1.AutoSize = true;
		this.labStep1.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep1.Location = new System.Drawing.Point(51, 9);
		this.labStep1.Name = "labStep1";
		this.labStep1.Size = new System.Drawing.Size(85, 21);
		this.labStep1.TabIndex = 13;
		this.labStep1.Text = "吹扫(min):";
		this.tabPage2.Controls.Add(this.label76);
		this.tabPage2.Controls.Add(this.dateTimePicker1);
		this.tabPage2.Controls.Add(this.tbMethName);
		this.tabPage2.Controls.Add(this.dateTimePicker2);
		this.tabPage2.Controls.Add(this.MethodOpen);
		this.tabPage2.Controls.Add(this.MethodSave);
		this.tabPage2.Controls.Add(this.MethodReSave);
		this.tabPage2.Controls.Add(this.label2);
		this.tabPage2.Controls.Add(this.btnDownload);
		this.tabPage2.Controls.Add(this.label1);
		this.tabPage2.Controls.Add(this.uSAutoMode);
		this.tabPage2.Controls.Add(this.uSHe);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(334, 639);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "tabPage2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.tabPage3.AutoScroll = true;
		this.tabPage3.Controls.Add(this.labStep);
		this.tabPage3.Controls.Add(this.label3);
		this.tabPage3.Controls.Add(this.label16);
		this.tabPage3.Controls.Add(this.label15);
		this.tabPage3.Controls.Add(this.label14);
		this.tabPage3.Controls.Add(this.label13);
		this.tabPage3.Controls.Add(this.label12);
		this.tabPage3.Controls.Add(this.label4);
		this.tabPage3.Controls.Add(this.label5);
		this.tabPage3.Controls.Add(this.label6);
		this.tabPage3.Controls.Add(this.label7);
		this.tabPage3.Controls.Add(this.label8);
		this.tabPage3.Controls.Add(this.label9);
		this.tabPage3.Controls.Add(this.label10);
		this.tabPage3.Controls.Add(this.label11);
		this.tabPage3.Controls.Add(this.ucTextBoxEx12);
		this.tabPage3.Controls.Add(this.ucTextBoxEx11);
		this.tabPage3.Controls.Add(this.ucTextBoxEx7times);
		this.tabPage3.Controls.Add(this.ucTextBoxEx4);
		this.tabPage3.Controls.Add(this.ucTextBoxEx2);
		this.tabPage3.Controls.Add(this.ucTextBoxEx1);
		this.tabPage3.Controls.Add(this.ucBtnCheck);
		this.tabPage3.Controls.Add(this.ucBtnSet);
		this.tabPage3.Controls.Add(this.ucTextBoxEx10);
		this.tabPage3.Controls.Add(this.ucTextBoxEx9);
		this.tabPage3.Controls.Add(this.ucTextBoxEx8);
		this.tabPage3.Controls.Add(this.ucTextBoxEx7a);
		this.tabPage3.Controls.Add(this.ucTextBoxEx7);
		this.tabPage3.Controls.Add(this.ucTextBoxEx6);
		this.tabPage3.Controls.Add(this.ucTextBoxEx5);
		this.tabPage3.Controls.Add(this.ucTextBoxEx3);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(334, 641);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "B型机参数";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label3.Location = new System.Drawing.Point(45, 546);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(78, 21);
		this.label3.TabIndex = 53;
		this.label3.Text = "分析次数:";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label16.Location = new System.Drawing.Point(38, 504);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(85, 21);
		this.label16.TabIndex = 51;
		this.label16.Text = "吹扫(min):";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label15.Location = new System.Drawing.Point(6, 348);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(117, 21);
		this.label15.TabIndex = 49;
		this.label15.Text = "富集次数(min):";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label14.Location = new System.Drawing.Point(29, 153);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(94, 21);
		this.label14.TabIndex = 47;
		this.label14.Text = "等待2(min):";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label13.Location = new System.Drawing.Point(29, 75);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(94, 21);
		this.label13.TabIndex = 45;
		this.label13.Text = "吹扫1(min):";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label12.Location = new System.Drawing.Point(29, 36);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(94, 21);
		this.label12.TabIndex = 43;
		this.label12.Text = "等待1(min):";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label4.Location = new System.Drawing.Point(38, 465);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(85, 21);
		this.label4.TabIndex = 38;
		this.label4.Text = "检测(min):";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label5.Location = new System.Drawing.Point(38, 426);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(85, 21);
		this.label5.TabIndex = 37;
		this.label5.Text = "进样(min):";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label6.Location = new System.Drawing.Point(38, 387);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(85, 21);
		this.label6.TabIndex = 36;
		this.label6.Text = "加热(min):";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label7.Location = new System.Drawing.Point(38, 309);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(85, 21);
		this.label7.TabIndex = 35;
		this.label7.Text = "间隔(min):";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label8.Location = new System.Drawing.Point(38, 270);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(85, 21);
		this.label8.TabIndex = 34;
		this.label8.Text = "富集(min):";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label9.Location = new System.Drawing.Point(29, 231);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(94, 21);
		this.label9.TabIndex = 33;
		this.label9.Text = "致冷2(min):";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label10.Location = new System.Drawing.Point(29, 192);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(94, 21);
		this.label10.TabIndex = 32;
		this.label10.Text = "致冷1(min):";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label11.Location = new System.Drawing.Point(29, 114);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(94, 21);
		this.label11.TabIndex = 31;
		this.label11.Text = "吹扫2(min):";
		this.uBtnStart.BackColor = System.Drawing.Color.White;
		this.uBtnStart.BtnBackColor = System.Drawing.Color.White;
		this.uBtnStart.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.uBtnStart.BtnForeColor = System.Drawing.Color.White;
		this.uBtnStart.BtnText = "开始";
		this.uBtnStart.ConerRadius = 5;
		this.uBtnStart.Cursor = System.Windows.Forms.Cursors.Hand;
		this.uBtnStart.EnabledMouseEffect = true;
		this.uBtnStart.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uBtnStart.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uBtnStart.IsRadius = true;
		this.uBtnStart.IsShowRect = true;
		this.uBtnStart.IsShowTips = false;
		this.uBtnStart.Location = new System.Drawing.Point(11, 280);
		this.uBtnStart.Margin = new System.Windows.Forms.Padding(0);
		this.uBtnStart.Name = "uBtnStart";
		this.uBtnStart.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.uBtnStart.RectWidth = 1;
		this.uBtnStart.Size = new System.Drawing.Size(184, 52);
		this.uBtnStart.TabIndex = 21;
		this.uBtnStart.TabStop = false;
		this.uBtnStart.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.uBtnStart.TipsText = "";
		this.uBtnStart.BtnClick += new System.EventHandler(uBtnStart_BtnClick);
		this.uTStep8.BackColor = System.Drawing.Color.Transparent;
		this.uTStep8.ConerRadius = 5;
		this.uTStep8.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep8.DecLength = 2;
		this.uTStep8.FillColor = System.Drawing.Color.Empty;
		this.uTStep8.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep8.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep8.InputText = "";
		this.uTStep8.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep8.IsFocusColor = true;
		this.uTStep8.IsRadius = true;
		this.uTStep8.IsShowClearBtn = true;
		this.uTStep8.IsShowKeyboard = false;
		this.uTStep8.IsShowRect = true;
		this.uTStep8.IsShowSearchBtn = false;
		this.uTStep8.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep8.Location = new System.Drawing.Point(140, 243);
		this.uTStep8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep8.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep8.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep8.Name = "uTStep8";
		this.uTStep8.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep8.PasswordChar = '\0';
		this.uTStep8.PromptColor = System.Drawing.Color.Gray;
		this.uTStep8.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep8.PromptText = "";
		this.uTStep8.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep8.RectWidth = 1;
		this.uTStep8.RegexPattern = "";
		this.uTStep8.Size = new System.Drawing.Size(145, 32);
		this.uTStep8.TabIndex = 9;
		this.uTStep7.BackColor = System.Drawing.Color.Transparent;
		this.uTStep7.ConerRadius = 5;
		this.uTStep7.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep7.DecLength = 2;
		this.uTStep7.FillColor = System.Drawing.Color.Empty;
		this.uTStep7.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep7.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep7.InputText = "";
		this.uTStep7.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep7.IsFocusColor = true;
		this.uTStep7.IsRadius = true;
		this.uTStep7.IsShowClearBtn = true;
		this.uTStep7.IsShowKeyboard = false;
		this.uTStep7.IsShowRect = true;
		this.uTStep7.IsShowSearchBtn = false;
		this.uTStep7.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep7.Location = new System.Drawing.Point(140, 209);
		this.uTStep7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep7.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep7.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep7.Name = "uTStep7";
		this.uTStep7.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep7.PasswordChar = '\0';
		this.uTStep7.PromptColor = System.Drawing.Color.Gray;
		this.uTStep7.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep7.PromptText = "";
		this.uTStep7.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep7.RectWidth = 1;
		this.uTStep7.RegexPattern = "";
		this.uTStep7.Size = new System.Drawing.Size(145, 32);
		this.uTStep7.TabIndex = 10;
		this.uTStep6.BackColor = System.Drawing.Color.Transparent;
		this.uTStep6.ConerRadius = 5;
		this.uTStep6.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep6.DecLength = 2;
		this.uTStep6.FillColor = System.Drawing.Color.Empty;
		this.uTStep6.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep6.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep6.InputText = "";
		this.uTStep6.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep6.IsFocusColor = true;
		this.uTStep6.IsRadius = true;
		this.uTStep6.IsShowClearBtn = true;
		this.uTStep6.IsShowKeyboard = false;
		this.uTStep6.IsShowRect = true;
		this.uTStep6.IsShowSearchBtn = false;
		this.uTStep6.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep6.Location = new System.Drawing.Point(140, 175);
		this.uTStep6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep6.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep6.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep6.Name = "uTStep6";
		this.uTStep6.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep6.PasswordChar = '\0';
		this.uTStep6.PromptColor = System.Drawing.Color.Gray;
		this.uTStep6.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep6.PromptText = "";
		this.uTStep6.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep6.RectWidth = 1;
		this.uTStep6.RegexPattern = "";
		this.uTStep6.Size = new System.Drawing.Size(145, 32);
		this.uTStep6.TabIndex = 11;
		this.uTStep5.BackColor = System.Drawing.Color.Transparent;
		this.uTStep5.ConerRadius = 5;
		this.uTStep5.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep5.DecLength = 2;
		this.uTStep5.FillColor = System.Drawing.Color.Empty;
		this.uTStep5.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep5.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep5.InputText = "";
		this.uTStep5.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep5.IsFocusColor = true;
		this.uTStep5.IsRadius = true;
		this.uTStep5.IsShowClearBtn = true;
		this.uTStep5.IsShowKeyboard = false;
		this.uTStep5.IsShowRect = true;
		this.uTStep5.IsShowSearchBtn = false;
		this.uTStep5.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep5.Location = new System.Drawing.Point(140, 141);
		this.uTStep5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep5.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep5.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep5.Name = "uTStep5";
		this.uTStep5.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep5.PasswordChar = '\0';
		this.uTStep5.PromptColor = System.Drawing.Color.Gray;
		this.uTStep5.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep5.PromptText = "";
		this.uTStep5.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep5.RectWidth = 1;
		this.uTStep5.RegexPattern = "";
		this.uTStep5.Size = new System.Drawing.Size(145, 32);
		this.uTStep5.TabIndex = 8;
		this.uTStep4.BackColor = System.Drawing.Color.Transparent;
		this.uTStep4.ConerRadius = 5;
		this.uTStep4.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep4.DecLength = 2;
		this.uTStep4.FillColor = System.Drawing.Color.Empty;
		this.uTStep4.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep4.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep4.InputText = "";
		this.uTStep4.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep4.IsFocusColor = true;
		this.uTStep4.IsRadius = true;
		this.uTStep4.IsShowClearBtn = true;
		this.uTStep4.IsShowKeyboard = false;
		this.uTStep4.IsShowRect = true;
		this.uTStep4.IsShowSearchBtn = false;
		this.uTStep4.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep4.Location = new System.Drawing.Point(142, 107);
		this.uTStep4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep4.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep4.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep4.Name = "uTStep4";
		this.uTStep4.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep4.PasswordChar = '\0';
		this.uTStep4.PromptColor = System.Drawing.Color.Gray;
		this.uTStep4.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep4.PromptText = "";
		this.uTStep4.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep4.RectWidth = 1;
		this.uTStep4.RegexPattern = "";
		this.uTStep4.Size = new System.Drawing.Size(145, 32);
		this.uTStep4.TabIndex = 7;
		this.uTStep3.BackColor = System.Drawing.Color.Transparent;
		this.uTStep3.ConerRadius = 5;
		this.uTStep3.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep3.DecLength = 2;
		this.uTStep3.FillColor = System.Drawing.Color.Empty;
		this.uTStep3.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep3.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep3.InputText = "";
		this.uTStep3.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep3.IsFocusColor = true;
		this.uTStep3.IsRadius = true;
		this.uTStep3.IsShowClearBtn = true;
		this.uTStep3.IsShowKeyboard = false;
		this.uTStep3.IsShowRect = true;
		this.uTStep3.IsShowSearchBtn = false;
		this.uTStep3.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep3.Location = new System.Drawing.Point(142, 73);
		this.uTStep3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep3.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep3.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep3.Name = "uTStep3";
		this.uTStep3.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep3.PasswordChar = '\0';
		this.uTStep3.PromptColor = System.Drawing.Color.Gray;
		this.uTStep3.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep3.PromptText = "";
		this.uTStep3.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep3.RectWidth = 1;
		this.uTStep3.RegexPattern = "";
		this.uTStep3.Size = new System.Drawing.Size(145, 32);
		this.uTStep3.TabIndex = 7;
		this.uTStep2.BackColor = System.Drawing.Color.Transparent;
		this.uTStep2.ConerRadius = 5;
		this.uTStep2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep2.DecLength = 2;
		this.uTStep2.FillColor = System.Drawing.Color.Empty;
		this.uTStep2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep2.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep2.InputText = "";
		this.uTStep2.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep2.IsFocusColor = true;
		this.uTStep2.IsRadius = true;
		this.uTStep2.IsShowClearBtn = true;
		this.uTStep2.IsShowKeyboard = false;
		this.uTStep2.IsShowRect = true;
		this.uTStep2.IsShowSearchBtn = false;
		this.uTStep2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep2.Location = new System.Drawing.Point(142, 39);
		this.uTStep2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep2.Name = "uTStep2";
		this.uTStep2.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep2.PasswordChar = '\0';
		this.uTStep2.PromptColor = System.Drawing.Color.Gray;
		this.uTStep2.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep2.PromptText = "";
		this.uTStep2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep2.RectWidth = 1;
		this.uTStep2.RegexPattern = "";
		this.uTStep2.Size = new System.Drawing.Size(145, 32);
		this.uTStep2.TabIndex = 7;
		this.uTStep1.BackColor = System.Drawing.Color.Transparent;
		this.uTStep1.ConerRadius = 5;
		this.uTStep1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.uTStep1.DecLength = 2;
		this.uTStep1.FillColor = System.Drawing.Color.Empty;
		this.uTStep1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uTStep1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep1.InputText = "";
		this.uTStep1.InputType = HZH_Controls.TextInputType.NotControl;
		this.uTStep1.IsFocusColor = true;
		this.uTStep1.IsRadius = true;
		this.uTStep1.IsShowClearBtn = true;
		this.uTStep1.IsShowKeyboard = false;
		this.uTStep1.IsShowRect = true;
		this.uTStep1.IsShowSearchBtn = false;
		this.uTStep1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.uTStep1.Location = new System.Drawing.Point(142, 5);
		this.uTStep1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.uTStep1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.uTStep1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.uTStep1.Name = "uTStep1";
		this.uTStep1.Padding = new System.Windows.Forms.Padding(5);
		this.uTStep1.PasswordChar = '\0';
		this.uTStep1.PromptColor = System.Drawing.Color.Gray;
		this.uTStep1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.uTStep1.PromptText = "";
		this.uTStep1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.uTStep1.RectWidth = 1;
		this.uTStep1.RegexPattern = "";
		this.uTStep1.Size = new System.Drawing.Size(145, 32);
		this.uTStep1.TabIndex = 0;
		this.uSAutoMode.BackColor = System.Drawing.Color.Transparent;
		this.uSAutoMode.Checked = false;
		this.uSAutoMode.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSAutoMode.FalseTextColr = System.Drawing.Color.White;
		this.uSAutoMode.Location = new System.Drawing.Point(416, 135);
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
		this.uSHe.Location = new System.Drawing.Point(589, 135);
		this.uSHe.Name = "uSHe";
		this.uSHe.Size = new System.Drawing.Size(81, 31);
		this.uSHe.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSHe.TabIndex = 51;
		this.uSHe.Texts = new string[2] { "断", "通" };
		this.uSHe.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSHe.TrueTextColr = System.Drawing.Color.White;
		this.uSHe.CheckedChanged += new System.EventHandler(uSHe_CheckedChanged);
		this.ucTextBoxEx12.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx12.ConerRadius = 5;
		this.ucTextBoxEx12.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx12.DecLength = 2;
		this.ucTextBoxEx12.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx12.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx12.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx12.InputText = "";
		this.ucTextBoxEx12.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx12.IsFocusColor = true;
		this.ucTextBoxEx12.IsRadius = true;
		this.ucTextBoxEx12.IsShowClearBtn = true;
		this.ucTextBoxEx12.IsShowKeyboard = false;
		this.ucTextBoxEx12.IsShowRect = true;
		this.ucTextBoxEx12.IsShowSearchBtn = false;
		this.ucTextBoxEx12.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx12.Location = new System.Drawing.Point(148, 542);
		this.ucTextBoxEx12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx12.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx12.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx12.Name = "ucTextBoxEx12";
		this.ucTextBoxEx12.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx12.PasswordChar = '\0';
		this.ucTextBoxEx12.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx12.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx12.PromptText = "";
		this.ucTextBoxEx12.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx12.RectWidth = 1;
		this.ucTextBoxEx12.RegexPattern = "";
		this.ucTextBoxEx12.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx12.TabIndex = 52;
		this.ucTextBoxEx11.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx11.ConerRadius = 5;
		this.ucTextBoxEx11.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx11.DecLength = 2;
		this.ucTextBoxEx11.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx11.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx11.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx11.InputText = "";
		this.ucTextBoxEx11.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx11.IsFocusColor = true;
		this.ucTextBoxEx11.IsRadius = true;
		this.ucTextBoxEx11.IsShowClearBtn = true;
		this.ucTextBoxEx11.IsShowKeyboard = false;
		this.ucTextBoxEx11.IsShowRect = true;
		this.ucTextBoxEx11.IsShowSearchBtn = false;
		this.ucTextBoxEx11.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx11.Location = new System.Drawing.Point(148, 500);
		this.ucTextBoxEx11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx11.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx11.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx11.Name = "ucTextBoxEx11";
		this.ucTextBoxEx11.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx11.PasswordChar = '\0';
		this.ucTextBoxEx11.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx11.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx11.PromptText = "";
		this.ucTextBoxEx11.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx11.RectWidth = 1;
		this.ucTextBoxEx11.RegexPattern = "";
		this.ucTextBoxEx11.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx11.TabIndex = 50;
		this.ucTextBoxEx7times.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx7times.ConerRadius = 5;
		this.ucTextBoxEx7times.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx7times.DecLength = 2;
		this.ucTextBoxEx7times.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx7times.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx7times.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx7times.InputText = "";
		this.ucTextBoxEx7times.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx7times.IsFocusColor = true;
		this.ucTextBoxEx7times.IsRadius = true;
		this.ucTextBoxEx7times.IsShowClearBtn = true;
		this.ucTextBoxEx7times.IsShowKeyboard = false;
		this.ucTextBoxEx7times.IsShowRect = true;
		this.ucTextBoxEx7times.IsShowSearchBtn = false;
		this.ucTextBoxEx7times.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx7times.Location = new System.Drawing.Point(148, 344);
		this.ucTextBoxEx7times.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx7times.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx7times.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx7times.Name = "ucTextBoxEx7times";
		this.ucTextBoxEx7times.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx7times.PasswordChar = '\0';
		this.ucTextBoxEx7times.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx7times.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx7times.PromptText = "";
		this.ucTextBoxEx7times.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx7times.RectWidth = 1;
		this.ucTextBoxEx7times.RegexPattern = "";
		this.ucTextBoxEx7times.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx7times.TabIndex = 48;
		this.ucTextBoxEx4.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx4.ConerRadius = 5;
		this.ucTextBoxEx4.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx4.DecLength = 2;
		this.ucTextBoxEx4.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx4.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx4.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx4.InputText = "";
		this.ucTextBoxEx4.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx4.IsFocusColor = true;
		this.ucTextBoxEx4.IsRadius = true;
		this.ucTextBoxEx4.IsShowClearBtn = true;
		this.ucTextBoxEx4.IsShowKeyboard = false;
		this.ucTextBoxEx4.IsShowRect = true;
		this.ucTextBoxEx4.IsShowSearchBtn = false;
		this.ucTextBoxEx4.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx4.Location = new System.Drawing.Point(150, 149);
		this.ucTextBoxEx4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx4.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx4.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx4.Name = "ucTextBoxEx4";
		this.ucTextBoxEx4.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx4.PasswordChar = '\0';
		this.ucTextBoxEx4.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx4.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx4.PromptText = "";
		this.ucTextBoxEx4.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx4.RectWidth = 1;
		this.ucTextBoxEx4.RegexPattern = "";
		this.ucTextBoxEx4.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx4.TabIndex = 46;
		this.ucTextBoxEx2.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx2.ConerRadius = 5;
		this.ucTextBoxEx2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx2.DecLength = 2;
		this.ucTextBoxEx2.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx2.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx2.InputText = "";
		this.ucTextBoxEx2.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx2.IsFocusColor = true;
		this.ucTextBoxEx2.IsRadius = true;
		this.ucTextBoxEx2.IsShowClearBtn = true;
		this.ucTextBoxEx2.IsShowKeyboard = false;
		this.ucTextBoxEx2.IsShowRect = true;
		this.ucTextBoxEx2.IsShowSearchBtn = false;
		this.ucTextBoxEx2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx2.Location = new System.Drawing.Point(150, 71);
		this.ucTextBoxEx2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx2.Name = "ucTextBoxEx2";
		this.ucTextBoxEx2.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx2.PasswordChar = '\0';
		this.ucTextBoxEx2.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx2.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx2.PromptText = "";
		this.ucTextBoxEx2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx2.RectWidth = 1;
		this.ucTextBoxEx2.RegexPattern = "";
		this.ucTextBoxEx2.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx2.TabIndex = 44;
		this.ucTextBoxEx1.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx1.ConerRadius = 5;
		this.ucTextBoxEx1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx1.DecLength = 2;
		this.ucTextBoxEx1.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx1.InputText = "";
		this.ucTextBoxEx1.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx1.IsFocusColor = true;
		this.ucTextBoxEx1.IsRadius = true;
		this.ucTextBoxEx1.IsShowClearBtn = true;
		this.ucTextBoxEx1.IsShowKeyboard = false;
		this.ucTextBoxEx1.IsShowRect = true;
		this.ucTextBoxEx1.IsShowSearchBtn = false;
		this.ucTextBoxEx1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx1.Location = new System.Drawing.Point(148, 32);
		this.ucTextBoxEx1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx1.Name = "ucTextBoxEx1";
		this.ucTextBoxEx1.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx1.PasswordChar = '\0';
		this.ucTextBoxEx1.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx1.PromptText = "";
		this.ucTextBoxEx1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx1.RectWidth = 1;
		this.ucTextBoxEx1.RegexPattern = "";
		this.ucTextBoxEx1.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx1.TabIndex = 42;
		this.ucBtnCheck.BackColor = System.Drawing.Color.White;
		this.ucBtnCheck.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnCheck.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnCheck.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnCheck.BtnText = "查询";
		this.ucBtnCheck.ConerRadius = 5;
		this.ucBtnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnCheck.EnabledMouseEffect = true;
		this.ucBtnCheck.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnCheck.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnCheck.IsRadius = true;
		this.ucBtnCheck.IsShowRect = true;
		this.ucBtnCheck.IsShowTips = false;
		this.ucBtnCheck.Location = new System.Drawing.Point(13, 585);
		this.ucBtnCheck.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnCheck.Name = "ucBtnCheck";
		this.ucBtnCheck.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnCheck.RectWidth = 1;
		this.ucBtnCheck.Size = new System.Drawing.Size(143, 52);
		this.ucBtnCheck.TabIndex = 41;
		this.ucBtnCheck.TabStop = false;
		this.ucBtnCheck.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnCheck.TipsText = "";
		this.ucBtnCheck.BtnClick += new System.EventHandler(ucBtnCheck_BtnClick);
		this.ucBtnSet.BackColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnSet.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnText = "设定";
		this.ucBtnSet.ConerRadius = 5;
		this.ucBtnSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnSet.EnabledMouseEffect = true;
		this.ucBtnSet.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnSet.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnSet.IsRadius = true;
		this.ucBtnSet.IsShowRect = true;
		this.ucBtnSet.IsShowTips = false;
		this.ucBtnSet.Location = new System.Drawing.Point(160, 585);
		this.ucBtnSet.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnSet.Name = "ucBtnSet";
		this.ucBtnSet.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnSet.RectWidth = 1;
		this.ucBtnSet.Size = new System.Drawing.Size(148, 52);
		this.ucBtnSet.TabIndex = 39;
		this.ucBtnSet.TabStop = false;
		this.ucBtnSet.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnSet.TipsText = "";
		this.ucBtnSet.BtnClick += new System.EventHandler(ucBtnSet_BtnClick);
		this.ucTextBoxEx10.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx10.ConerRadius = 5;
		this.ucTextBoxEx10.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx10.DecLength = 2;
		this.ucTextBoxEx10.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx10.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx10.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx10.InputText = "";
		this.ucTextBoxEx10.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx10.IsFocusColor = true;
		this.ucTextBoxEx10.IsRadius = true;
		this.ucTextBoxEx10.IsShowClearBtn = true;
		this.ucTextBoxEx10.IsShowKeyboard = false;
		this.ucTextBoxEx10.IsShowRect = true;
		this.ucTextBoxEx10.IsShowSearchBtn = false;
		this.ucTextBoxEx10.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx10.Location = new System.Drawing.Point(148, 461);
		this.ucTextBoxEx10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx10.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx10.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx10.Name = "ucTextBoxEx10";
		this.ucTextBoxEx10.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx10.PasswordChar = '\0';
		this.ucTextBoxEx10.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx10.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx10.PromptText = "";
		this.ucTextBoxEx10.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx10.RectWidth = 1;
		this.ucTextBoxEx10.RegexPattern = "";
		this.ucTextBoxEx10.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx10.TabIndex = 28;
		this.ucTextBoxEx9.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx9.ConerRadius = 5;
		this.ucTextBoxEx9.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx9.DecLength = 2;
		this.ucTextBoxEx9.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx9.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx9.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx9.InputText = "";
		this.ucTextBoxEx9.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx9.IsFocusColor = true;
		this.ucTextBoxEx9.IsRadius = true;
		this.ucTextBoxEx9.IsShowClearBtn = true;
		this.ucTextBoxEx9.IsShowKeyboard = false;
		this.ucTextBoxEx9.IsShowRect = true;
		this.ucTextBoxEx9.IsShowSearchBtn = false;
		this.ucTextBoxEx9.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx9.Location = new System.Drawing.Point(148, 422);
		this.ucTextBoxEx9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx9.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx9.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx9.Name = "ucTextBoxEx9";
		this.ucTextBoxEx9.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx9.PasswordChar = '\0';
		this.ucTextBoxEx9.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx9.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx9.PromptText = "";
		this.ucTextBoxEx9.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx9.RectWidth = 1;
		this.ucTextBoxEx9.RegexPattern = "";
		this.ucTextBoxEx9.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx9.TabIndex = 29;
		this.ucTextBoxEx8.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx8.ConerRadius = 5;
		this.ucTextBoxEx8.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx8.DecLength = 2;
		this.ucTextBoxEx8.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx8.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx8.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx8.InputText = "";
		this.ucTextBoxEx8.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx8.IsFocusColor = true;
		this.ucTextBoxEx8.IsRadius = true;
		this.ucTextBoxEx8.IsShowClearBtn = true;
		this.ucTextBoxEx8.IsShowKeyboard = false;
		this.ucTextBoxEx8.IsShowRect = true;
		this.ucTextBoxEx8.IsShowSearchBtn = false;
		this.ucTextBoxEx8.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx8.Location = new System.Drawing.Point(148, 383);
		this.ucTextBoxEx8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx8.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx8.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx8.Name = "ucTextBoxEx8";
		this.ucTextBoxEx8.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx8.PasswordChar = '\0';
		this.ucTextBoxEx8.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx8.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx8.PromptText = "";
		this.ucTextBoxEx8.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx8.RectWidth = 1;
		this.ucTextBoxEx8.RegexPattern = "";
		this.ucTextBoxEx8.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx8.TabIndex = 30;
		this.ucTextBoxEx7a.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx7a.ConerRadius = 5;
		this.ucTextBoxEx7a.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx7a.DecLength = 2;
		this.ucTextBoxEx7a.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx7a.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx7a.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx7a.InputText = "";
		this.ucTextBoxEx7a.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx7a.IsFocusColor = true;
		this.ucTextBoxEx7a.IsRadius = true;
		this.ucTextBoxEx7a.IsShowClearBtn = true;
		this.ucTextBoxEx7a.IsShowKeyboard = false;
		this.ucTextBoxEx7a.IsShowRect = true;
		this.ucTextBoxEx7a.IsShowSearchBtn = false;
		this.ucTextBoxEx7a.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx7a.Location = new System.Drawing.Point(148, 305);
		this.ucTextBoxEx7a.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx7a.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx7a.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx7a.Name = "ucTextBoxEx7a";
		this.ucTextBoxEx7a.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx7a.PasswordChar = '\0';
		this.ucTextBoxEx7a.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx7a.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx7a.PromptText = "";
		this.ucTextBoxEx7a.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx7a.RectWidth = 1;
		this.ucTextBoxEx7a.RegexPattern = "";
		this.ucTextBoxEx7a.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx7a.TabIndex = 27;
		this.ucTextBoxEx7.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx7.ConerRadius = 5;
		this.ucTextBoxEx7.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx7.DecLength = 2;
		this.ucTextBoxEx7.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx7.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx7.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx7.InputText = "";
		this.ucTextBoxEx7.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx7.IsFocusColor = true;
		this.ucTextBoxEx7.IsRadius = true;
		this.ucTextBoxEx7.IsShowClearBtn = true;
		this.ucTextBoxEx7.IsShowKeyboard = false;
		this.ucTextBoxEx7.IsShowRect = true;
		this.ucTextBoxEx7.IsShowSearchBtn = false;
		this.ucTextBoxEx7.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx7.Location = new System.Drawing.Point(150, 266);
		this.ucTextBoxEx7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx7.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx7.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx7.Name = "ucTextBoxEx7";
		this.ucTextBoxEx7.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx7.PasswordChar = '\0';
		this.ucTextBoxEx7.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx7.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx7.PromptText = "";
		this.ucTextBoxEx7.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx7.RectWidth = 1;
		this.ucTextBoxEx7.RegexPattern = "";
		this.ucTextBoxEx7.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx7.TabIndex = 25;
		this.ucTextBoxEx6.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx6.ConerRadius = 5;
		this.ucTextBoxEx6.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx6.DecLength = 2;
		this.ucTextBoxEx6.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx6.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx6.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx6.InputText = "";
		this.ucTextBoxEx6.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx6.IsFocusColor = true;
		this.ucTextBoxEx6.IsRadius = true;
		this.ucTextBoxEx6.IsShowClearBtn = true;
		this.ucTextBoxEx6.IsShowKeyboard = false;
		this.ucTextBoxEx6.IsShowRect = true;
		this.ucTextBoxEx6.IsShowSearchBtn = false;
		this.ucTextBoxEx6.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx6.Location = new System.Drawing.Point(150, 227);
		this.ucTextBoxEx6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx6.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx6.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx6.Name = "ucTextBoxEx6";
		this.ucTextBoxEx6.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx6.PasswordChar = '\0';
		this.ucTextBoxEx6.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx6.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx6.PromptText = "";
		this.ucTextBoxEx6.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx6.RectWidth = 1;
		this.ucTextBoxEx6.RegexPattern = "";
		this.ucTextBoxEx6.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx6.TabIndex = 24;
		this.ucTextBoxEx5.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx5.ConerRadius = 5;
		this.ucTextBoxEx5.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx5.DecLength = 2;
		this.ucTextBoxEx5.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx5.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx5.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx5.InputText = "";
		this.ucTextBoxEx5.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx5.IsFocusColor = true;
		this.ucTextBoxEx5.IsRadius = true;
		this.ucTextBoxEx5.IsShowClearBtn = true;
		this.ucTextBoxEx5.IsShowKeyboard = false;
		this.ucTextBoxEx5.IsShowRect = true;
		this.ucTextBoxEx5.IsShowSearchBtn = false;
		this.ucTextBoxEx5.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx5.Location = new System.Drawing.Point(150, 188);
		this.ucTextBoxEx5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx5.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx5.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx5.Name = "ucTextBoxEx5";
		this.ucTextBoxEx5.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx5.PasswordChar = '\0';
		this.ucTextBoxEx5.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx5.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx5.PromptText = "";
		this.ucTextBoxEx5.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx5.RectWidth = 1;
		this.ucTextBoxEx5.RegexPattern = "";
		this.ucTextBoxEx5.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx5.TabIndex = 26;
		this.ucTextBoxEx3.BackColor = System.Drawing.Color.Transparent;
		this.ucTextBoxEx3.ConerRadius = 5;
		this.ucTextBoxEx3.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTextBoxEx3.DecLength = 2;
		this.ucTextBoxEx3.FillColor = System.Drawing.Color.Empty;
		this.ucTextBoxEx3.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTextBoxEx3.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx3.InputText = "";
		this.ucTextBoxEx3.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTextBoxEx3.IsFocusColor = true;
		this.ucTextBoxEx3.IsRadius = true;
		this.ucTextBoxEx3.IsShowClearBtn = true;
		this.ucTextBoxEx3.IsShowKeyboard = false;
		this.ucTextBoxEx3.IsShowRect = true;
		this.ucTextBoxEx3.IsShowSearchBtn = false;
		this.ucTextBoxEx3.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTextBoxEx3.Location = new System.Drawing.Point(150, 110);
		this.ucTextBoxEx3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTextBoxEx3.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTextBoxEx3.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTextBoxEx3.Name = "ucTextBoxEx3";
		this.ucTextBoxEx3.Padding = new System.Windows.Forms.Padding(5);
		this.ucTextBoxEx3.PasswordChar = '\0';
		this.ucTextBoxEx3.PromptColor = System.Drawing.Color.Gray;
		this.ucTextBoxEx3.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTextBoxEx3.PromptText = "";
		this.ucTextBoxEx3.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTextBoxEx3.RectWidth = 1;
		this.ucTextBoxEx3.RegexPattern = "";
		this.ucTextBoxEx3.Size = new System.Drawing.Size(145, 32);
		this.ucTextBoxEx3.TabIndex = 23;
		this.labStep.AutoSize = true;
		this.labStep.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labStep.ForeColor = System.Drawing.Color.Red;
		this.labStep.Location = new System.Drawing.Point(35, 14);
		this.labStep.Name = "labStep";
		this.labStep.Size = new System.Drawing.Size(35, 14);
		this.labStep.TabIndex = 54;
		this.labStep.Text = "等待";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.Controls.Add(this.tabControl1);
		base.Name = "ZWSCtrl";
		base.Size = new System.Drawing.Size(342, 686);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		base.ResumeLayout(false);
	}
}
