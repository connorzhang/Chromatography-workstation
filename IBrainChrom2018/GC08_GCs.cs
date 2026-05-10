using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class GC08_GCs : GCC_GCs
{
	public class CmdItem
	{
		public byte byte_0;

		public string express = "";

		public CmdItem(string express, byte byte_1)
		{
			this.express = express;
			byte_0 = byte_1;
		}

		public override string ToString()
		{
			return express;
		}
	}

	public const string strConn = "已建立连接";

	public const string strStart = "开始侦听";

	public const string strStop = "停止侦听";

	private byte[] byte_1;

	private TcpClient tcpClient_0;

	private DevMonitorForm devMonitorForm_0;

	private DtC_Channel[] dtC_Channel_0;

	private DetectorParse[] class78_0;

	private InsDeviceManager class56_0;

	private InsDeviceManager class56_1;

	private Instrument instrument_0;

	private static TcpListener tcpListener_0;

	private static int int_0 = 0;

	public static List<CmdItem> lsItems = new List<CmdItem>();

	private MtdSetupDlg mtdSetupDlg_0;

	private NetworkStream networkStream_0;

	private short short_0;

	private Class44 class44_0;

	public Timer tmrListen;

	private byte[] byte_2;

	public GC08_GCs(SysCfgControl from)
		: base(from)
	{
		byte_1 = new byte[1024];
		short_0 = 0;
		class56_1 = new InsDeviceManager();
		class56_0 = new InsDeviceManager();
		class44_0 = new Class44();
		if (lsItems.Count == 0)
		{
			lsItems.Add(new CmdItem("温度数据", 143));
			lsItems.Add(new CmdItem("EPC数据", 159));
			lsItems.Add(new CmdItem("控温参数查询", 0));
			lsItems.Add(new CmdItem("程序升温参数查询", 1));
			lsItems.Add(new CmdItem("外部事件参数查询", 2));
			lsItems.Add(new CmdItem("自动进样时间查询", 4));
			lsItems.Add(new CmdItem("硬件版本号查询", 5));
			lsItems.Add(new CmdItem("执行文件号查询", 6));
			lsItems.Add(new CmdItem("检测器参数查询", 13));
			lsItems.Add(new CmdItem("EPC设定参数查询", 33));
			lsItems.Add(new CmdItem("查询气路配置", 35));
			lsItems.Add(new CmdItem("查询EPC模块工作参数", 36));
			lsItems.Add(new CmdItem("查询EPC名称", 38));
			lsItems.Add(new CmdItem("EPC控制状态查询", 40));
			lsItems.Add(new CmdItem("查询网络参数", 48));
			lsItems.Add(new CmdItem("查询控区名称", 64));
			lsItems.Add(new CmdItem("控温使能查询", 66));
			lsItems.Add(new CmdItem("控温参数设置", 8));
			lsItems.Add(new CmdItem("程序升温参数设置", 9));
			lsItems.Add(new CmdItem("外部事件参数设置", 10));
			lsItems.Add(new CmdItem("设置执行文件号", 3));
			lsItems.Add(new CmdItem("设置自动进样时间", 12));
			lsItems.Add(new CmdItem("设置仪器序列号", 7));
			lsItems.Add(new CmdItem("设置时钟", 11));
			lsItems.Add(new CmdItem("检测器参数设置", 14));
			lsItems.Add(new CmdItem("开始控温", 16));
			lsItems.Add(new CmdItem("关闭控温", 17));
			lsItems.Add(new CmdItem("启动全部样品分析", 18));
			lsItems.Add(new CmdItem("样品全部分析停止", 19));
			lsItems.Add(new CmdItem("启动FID1点火", 20));
			lsItems.Add(new CmdItem("启动FID2点火", 21));
			lsItems.Add(new CmdItem("启动指定通道开始分析", 22));
			lsItems.Add(new CmdItem("指定通道分析停止", 23));
			lsItems.Add(new CmdItem("EPC参数设定", 34));
			lsItems.Add(new CmdItem("设置查询EPC模块工作参数", 37));
			lsItems.Add(new CmdItem("设置EPC名称", 39));
			lsItems.Add(new CmdItem("EPC控制", 41));
			lsItems.Add(new CmdItem("设置网络参数", 49));
			lsItems.Add(new CmdItem("设置控区名称", 65));
			lsItems.Add(new CmdItem("控温使能设置", 67));
			lsItems.Add(new CmdItem("控温参数查询应答", 128));
			lsItems.Add(new CmdItem("控温参数设置应答", 136));
			lsItems.Add(new CmdItem("程序升温参数查询应答", 129));
			lsItems.Add(new CmdItem("程序升温参数设置应答", 137));
			lsItems.Add(new CmdItem("外部事件参数查询应答", 130));
			lsItems.Add(new CmdItem("外部事件参数设置应答", 138));
			lsItems.Add(new CmdItem("执行文件号查询应答", 134));
			lsItems.Add(new CmdItem("设置执行文件号应答", 131));
			lsItems.Add(new CmdItem("自动进样时间查询应答", 132));
			lsItems.Add(new CmdItem("设置自动进样时间应答", 140));
			lsItems.Add(new CmdItem("硬件版本号查询应答", 133));
			lsItems.Add(new CmdItem("设置仪器序列号应答", 135));
			lsItems.Add(new CmdItem("设置时钟应答", 139));
			lsItems.Add(new CmdItem("检测器参数查询应答", 141));
			lsItems.Add(new CmdItem("检测器参数设置应答", 142));
			lsItems.Add(new CmdItem("开始控温应答", 144));
			lsItems.Add(new CmdItem("关闭控温应答", 145));
			lsItems.Add(new CmdItem("启动全部样品分析应答", 146));
			lsItems.Add(new CmdItem("样品全部分析停止应答", 147));
			lsItems.Add(new CmdItem("启动指定通道分析应答", 150));
			lsItems.Add(new CmdItem("指定通道分析停止应答", 151));
			lsItems.Add(new CmdItem("EPC设定参数查询返回", 161));
			lsItems.Add(new CmdItem("EPC参数设定返回", 162));
			lsItems.Add(new CmdItem("查询气路配置应答", 163));
			lsItems.Add(new CmdItem("查询EPC模块工作参数应答", 164));
			lsItems.Add(new CmdItem("设置查询EPC模块工作参数应答", 165));
			lsItems.Add(new CmdItem("查询EPC名称应答", 166));
			lsItems.Add(new CmdItem("设置EPC名称应答", 167));
			lsItems.Add(new CmdItem("EPC控制状态查询返回", 168));
			lsItems.Add(new CmdItem("EPC控制返回", 169));
			lsItems.Add(new CmdItem("查询网络参数应答", 176));
			lsItems.Add(new CmdItem("设置网络参数应答", 177));
			lsItems.Add(new CmdItem("查询控区名称应答", 192));
			lsItems.Add(new CmdItem("设置控区名称应答", 193));
			lsItems.Add(new CmdItem("控温使能查询应答", 194));
			lsItems.Add(new CmdItem("控温使能设置应答", 195));
		}
		if (tcpListener_0 == null)
		{
			tcpListener_0 = new TcpListener(IPAddress.Any, 25001);
		}
		tmrListen = new Timer();
		tmrListen.Interval = 100;
		tmrListen.Tick += tmrListen_Tick;
		tmrListen.Enabled = false;
	}

	private void method_0(IAsyncResult iasyncResult_0)
	{
		if (!networkStream_0.CanRead)
		{
			return;
		}
		NetworkStream networkStream = (NetworkStream)iasyncResult_0.AsyncState;
		try
		{
			if (networkStream.CanRead)
			{
				int num = networkStream.EndRead(iasyncResult_0);
				if (num >= byte_1.Length)
				{
					throw new Exception("信息超长！");
				}
				byte[] array = new byte[num];
				Array.Copy(byte_1, 0, array, 0, num);
				byte b = IBrainConvert.BitByBitNo(array, 6, array.Length - 6 - 1);
				string text = "0x";
				if (array.Length >= 24)
				{
					text += BitConverter.ToString(new byte[1] { array[24] });
				}
				if (b != array[array.Length - 1])
				{
					throw new Exception(text + " 校验失败！");
				}
				devMonitorForm_0.tsslbGcStatus.Text = "";
				method_2(array);
			}
		}
		catch (Exception ex)
		{
			devMonitorForm_0.gcShow(null, ex.Message, true);
		}
		finally
		{
			if (devMonitorForm_0.tsslbGcListen.Text == "开始侦听")
			{
				method_9();
			}
			else if (networkStream.CanRead)
			{
				try
				{
					networkStream.BeginRead(byte_1, 0, byte_1.Length, method_0, networkStream_0);
				}
				catch
				{
				}
			}
		}
	}

	private string method_1(byte[] byte_3)
	{
		return "";
	}

	private byte[] method_2(byte[] byte_3)
	{
		devMonitorForm_0.lbinsSerial.Text = Encoding.ASCII.GetString(byte_3, 6, 16);
		instrument_0.form.slbExplain.Text = devMonitorForm_0.lbinsSerial.Text;
		byte b = byte_3[24];
		AccStyle accStyle_ = AccStyle.Read;
		byte[] array = IBrainConvert.ArrayCopy(byte_3, 25, byte_3.Length - 26);
		byte b2 = b;
		if (b2 == 128 || b2 == 136)
		{
			class56_1.SetTempSetedList(array);
			method_3(accStyle_, class56_1);
		}
		if (b == 129 || b == 137)
		{
			class56_1.SetTempSettingList(array);
			method_6(accStyle_, class56_1);
		}
		if (b == 130 || b == 138)
		{
			class56_1.SetEventTable0(array);
			method_5(accStyle_, class56_1);
		}
		if (b == 134 || b == 131)
		{
			class56_1.SetExeFileNumber(array);
		}
		if (b == 132 || b == 140)
		{
			class56_1.SetInjectInterval(array);
		}
		if (b == 133)
		{
			class56_1.SetNetDevList(array);
			devMonitorForm_0.gvHardVersion.RowCount = class56_1.netDevList.Count;
			for (int i = 0; i < devMonitorForm_0.gvHardVersion.RowCount; i++)
			{
				devMonitorForm_0.gvHardVersion.Rows[i].HeaderCell.Value = (i + 1).ToString();
				devMonitorForm_0.gvHardVersion.Rows[i].Cells[devMonitorForm_0.clmHV.Index].Value = class56_1.netDevList[i].ToString();
			}
		}
		if (b == 141 || b == 142)
		{
			class56_1.SetDetectorSettingList(array);
			method_4(accStyle_, class56_1);
		}
		switch (b)
		{
		case 144:
			return null;
		case 145:
			return null;
		case 146:
			instrument_0.gc_StartAly();
			return null;
		case 147:
			instrument_0.gc_StopAly();
			return null;
		case 150:
			class56_1.SetCurSglNumberStart(array);
			instrument_0.gc_StartAly();
			break;
		}
		if (b == 151)
		{
			class56_0.SetCurSglNumberEnd(array);
			instrument_0.gc_StopAly();
		}
		if (b != 161 && b != 162)
		{
			if (b == 163)
			{
				class56_1.SetInjectNumList(array);
			}
			if (b == 164 || b == 165)
			{
				class56_1.GetEPCDevParam(0, array);
			}
			if (b == 166 || b == 167)
			{
				class56_1.SetEpcNameList(array);
			}
			if (b == 168 || b == 169)
			{
				if (array.Length != 1)
				{
					throw new Exception("EPC控制");
				}
				class56_1.epcGasType = array[0];
			}
			if (b == 176 || b == 177)
			{
				class56_1.SetIpAddressList(array);
				devMonitorForm_0.gvNet.RowCount = class56_1.ipAddressList.Length;
				for (int j = 0; j < class56_1.ipAddressList.Length; j++)
				{
					devMonitorForm_0.gvNet.Rows[j].Cells[0].Value = class56_1.ipAddressList[j].ToString();
				}
			}
			if (b == 192 || b == 193)
			{
				class56_1.tempCtrlAreaTable.Byte2Name(array);
				int index = devMonitorForm_0.clmCT6CN.Index;
				int index2 = devMonitorForm_0.clmCT6EN.Index;
				devMonitorForm_0.dgvCT6.Rows[0].Cells[index].Value = class56_1.tempCtrlAreaTable.tempList[0].strNameCn;
				devMonitorForm_0.dgvCT6.Rows[0].Cells[index2].Value = class56_1.tempCtrlAreaTable.tempList[0].strNameEn;
				devMonitorForm_0.dgvCT6.Rows[1].Cells[index].Value = class56_1.tempCtrlAreaTable.tempList[1].strNameCn;
				devMonitorForm_0.dgvCT6.Rows[1].Cells[index2].Value = class56_1.tempCtrlAreaTable.tempList[1].strNameEn;
				devMonitorForm_0.dgvCT6.Rows[2].Cells[index].Value = class56_1.tempCtrlAreaTable.tempList[2].strNameCn;
				devMonitorForm_0.dgvCT6.Rows[2].Cells[index2].Value = class56_1.tempCtrlAreaTable.tempList[2].strNameEn;
				devMonitorForm_0.dgvCT6.Rows[3].Cells[index].Value = class56_1.tempCtrlAreaTable.tempList[3].strNameCn;
				devMonitorForm_0.dgvCT6.Rows[3].Cells[index2].Value = class56_1.tempCtrlAreaTable.tempList[3].strNameEn;
				devMonitorForm_0.dgvCT6.Rows[4].Cells[index].Value = class56_1.tempCtrlAreaTable.tempList[4].strNameCn;
				devMonitorForm_0.dgvCT6.Rows[4].Cells[index2].Value = class56_1.tempCtrlAreaTable.tempList[4].strNameEn;
				devMonitorForm_0.dgvCT6.Rows[5].Cells[index].Value = class56_1.tempCtrlAreaTable.tempList[5].strNameCn;
				devMonitorForm_0.dgvCT6.Rows[5].Cells[index2].Value = class56_1.tempCtrlAreaTable.tempList[5].strNameEn;
			}
			if (b == 194 || b == 195)
			{
				class56_1.SetInsDevEnable(array);
				int index3 = devMonitorForm_0.clmCT6CtrlT.Index;
				devMonitorForm_0.dgvCT6.Rows[5].Cells[index3].Value = class56_1.insDevEnable5;
				devMonitorForm_0.dgvCT6.Rows[4].Cells[index3].Value = class56_1.insDevEnable4;
				devMonitorForm_0.dgvCT6.Rows[3].Cells[index3].Value = class56_1.insDevEnable3;
				devMonitorForm_0.dgvCT6.Rows[2].Cells[index3].Value = class56_1.insDevEnable2;
				devMonitorForm_0.dgvCT6.Rows[1].Cells[index3].Value = class56_1.insDevEnable1;
				devMonitorForm_0.dgvCT6.Rows[0].Cells[index3].Value = class56_1.insDevEnable0;
			}
			if (b == 143)
			{
				method_10(array);
			}
			return array;
		}
		return null;
	}

	private void method_3(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		if (accStyle_0 == AccStyle.Read)
		{
			for (int i = 0; i < devMonitorForm_0.dgvCT6.RowCount; i++)
			{
				devMonitorForm_0.dgvCT6.Rows[i].Cells[devMonitorForm_0.clmCT6SetT.Index].Value = class56_2.tempSetedList[i];
				devMonitorForm_0.dgvCT6.Rows[i].Cells[devMonitorForm_0.clmCT6PtcT.Index].Value = class56_2.tempProtectList[i];
			}
		}
		if (accStyle_0 == AccStyle.Write)
		{
			for (int j = 0; j < devMonitorForm_0.dgvCT6.RowCount; j++)
			{
				class56_2.tempSetedList[j] = method_7(devMonitorForm_0.dgvCT6.Rows[j].Cells[devMonitorForm_0.clmCT6SetT.Index].Value);
				class56_2.tempProtectList[j] = method_7(devMonitorForm_0.dgvCT6.Rows[j].Cells[devMonitorForm_0.clmCT6PtcT.Index].Value);
			}
		}
	}

	private void method_4(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		DataGridView gvDtcrs = devMonitorForm_0.gvDtcrs;
		if (accStyle_0 == AccStyle.Read)
		{
			bool enabled = false;
			bool enabled2 = false;
			devMonitorForm_0.nudDtcrNum.Value = class56_2.detectorSettingList.Count;
			for (int i = 0; i < gvDtcrs.RowCount; i++)
			{
				gvDtcrs.Rows[i].Cells[devMonitorForm_0.clmDtMark.Index].Value = class56_2.detectorSettingList[i].GetDeviceTypeName();
				gvDtcrs.Rows[i].Cells[devMonitorForm_0.clmDtPosi.Index].Value = class56_2.detectorSettingList[i].GetPolarity();
				gvDtcrs.Rows[i].Cells[devMonitorForm_0.clmDtRange.Index].Value = class56_2.detectorSettingList[i].range;
				gvDtcrs.Rows[i].Cells[devMonitorForm_0.clmDtBsdct.Index].Value = class56_2.detectorSettingList[i].GetBaselineDeduction();
				gvDtcrs.Rows[i].Cells[devMonitorForm_0.clmDtFreq.Index].Value = class56_2.detectorSettingList[i].GetFreq().ToString();
				if (class56_2.detectorSettingList[i].detectorType == 64)
				{
					enabled = true;
				}
				if (class56_2.detectorSettingList[i].detectorType == 65)
				{
					enabled2 = true;
				}
			}
			Button btnAlyStart = devMonitorForm_0.btnAlyStart;
			bool enabled3 = (devMonitorForm_0.btnAlyStop.Enabled = class56_2.detectorSettingList.Count != 0);
			btnAlyStart.Enabled = enabled3;
			devMonitorForm_0.btnStartFID1.Enabled = enabled;
			devMonitorForm_0.btnStartFID2.Enabled = enabled2;
		}
		if (accStyle_0 != AccStyle.Write)
		{
			return;
		}
		DetectorSettingRow[] array = new DetectorSettingRow[0];
		DetectorSettingRow detectorSettingRow = new DetectorSettingRow();
		for (int j = 0; j < gvDtcrs.RowCount; j++)
		{
			detectorSettingRow.SetDeviceTypeByName(gvDtcrs.Rows[j].Cells[devMonitorForm_0.clmDtMark.Index].Value.ToString());
			object value = gvDtcrs.Rows[j].Cells[devMonitorForm_0.clmDtPosi.Index].Value;
			detectorSettingRow.SetPolarity(value != null && (bool)value);
			detectorSettingRow.range = byte.Parse(gvDtcrs.Rows[j].Cells[devMonitorForm_0.clmDtRange.Index].Value.ToString());
			value = gvDtcrs.Rows[j].Cells[devMonitorForm_0.clmDtBsdct.Index].Value;
			detectorSettingRow.SetBaselineDeduction(value != null && (bool)value);
			detectorSettingRow.SetFreq(byte.Parse(gvDtcrs.Rows[j].Cells[devMonitorForm_0.clmDtFreq.Index].Value.ToString()));
			if (!detectorSettingRow.IsVaildDevice())
			{
				gvDtcrs.Rows[j].HeaderCell.Value = "错误";
				throw new Exception("错误");
			}
			int num = array.Length;
			Array.Resize(ref array, num + 1);
			array[num] = detectorSettingRow;
		}
		IArrayBase.NewArray(ref class56_2.detectorSettingList, array.Length);
		for (int k = 0; k < class56_2.detectorSettingList.Count; k++)
		{
			class56_2.detectorSettingList[k] = array[k];
		}
	}

	private void method_5(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		DataGridView gvExtEvTP = mtdSetupDlg_0.gvExtEvTP;
		if (accStyle_0 == AccStyle.Read)
		{
			for (int i = 0; i < gvExtEvTP.RowCount; i++)
			{
				for (int j = 0; j < gvExtEvTP.ColumnCount; j++)
				{
					gvExtEvTP.Rows[i].Cells[j].Value = class56_2.eventCtrl0[i, j];
				}
			}
		}
		if (accStyle_0 != AccStyle.Write)
		{
			return;
		}
		for (int k = 0; k < gvExtEvTP.RowCount; k++)
		{
			for (int l = 0; l < gvExtEvTP.ColumnCount; l++)
			{
				class56_2.eventCtrl0[k, l] = method_7(gvExtEvTP.Rows[k].Cells[l].Value);
			}
		}
	}

	private void method_6(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		DataGridView dgvPT = mtdSetupDlg_0.dgvPT;
		if (accStyle_0 == AccStyle.Read)
		{
			mtdSetupDlg_0.tbptIniTempHoldT.Text = class56_2.tempHoldTime.ToString("0.0");
			for (int i = 0; i < dgvPT.RowCount; i++)
			{
				dgvPT.Rows[i].Cells[0].Value = class56_2.tempSettingList[i].tempStart;
				dgvPT.Rows[i].Cells[1].Value = class56_2.tempSettingList[i].tempEnd;
				dgvPT.Rows[i].Cells[2].Value = class56_2.tempSettingList[i].tempKeep;
			}
			if (mtdSetupDlg_0.Visible)
			{
				mtdSetupDlg_0.lbptInitT.Text = class56_2.tempSetedList[1].ToString("0.0");
				mtdSetupDlg_0.dgvPT_CellEndEdit(null, null);
				mtdSetupDlg_0.refresh_dpgcProgTemp();
			}
		}
		if (accStyle_0 == AccStyle.Write)
		{
			class56_2.tempHoldTime = method_7(mtdSetupDlg_0.tbptIniTempHoldT.Text);
			for (int j = 0; j < dgvPT.RowCount; j++)
			{
				class56_2.tempSettingList[j].tempStart = method_7(dgvPT.Rows[j].Cells[0].Value);
				class56_2.tempSettingList[j].tempEnd = method_7(dgvPT.Rows[j].Cells[1].Value);
				class56_2.tempSettingList[j].tempKeep = method_7(dgvPT.Rows[j].Cells[2].Value);
			}
		}
	}

	public void LogIn(DevMonitorForm devForm, MtdSetupDlg mtdDlg, DtC_Channel[] dtc_Channels)
	{
		devMonitorForm_0 = devForm;
		mtdSetupDlg_0 = mtdDlg;
		instrument_0 = devForm.instrument;
		dtC_Channel_0 = dtc_Channels;
		int_0++;
		if (!tmrListen.Enabled)
		{
			tmrListen.Enabled = true;
		}
	}

	public void LogOut()
	{
		int_0--;
		int_0 = Math.Max(0, int_0);
		if (tmrListen.Enabled)
		{
			tmrListen.Enabled = false;
		}
		method_9();
	}

	private float method_7(object object_0)
	{
		if (object_0 == null)
		{
			throw new Exception("相关字段没有赋值");
		}
		return float.Parse(object_0.ToString());
	}

	public void Send(byte byte_3)
	{
		if (networkStream_0 != null && networkStream_0.CanWrite)
		{
			devMonitorForm_0.gcShow(null, "准备发送", null);
			try
			{
				byte[] array = IBrainConvert.String2ByteArray("编号16字节");
				IBrainConvert.ArrayCopy(ref array, IBrainConvert.Short2ByteArray2(short_0++));
				IBrainConvert.ArrayAdd(ref array, byte_3);
				byte[] array2 = method_8(byte_3);
				IBrainConvert.ArrayCopy(ref array, array2);
				short num = (short)array.Length;
				IBrainConvert.ArrayAdd(ref array, IBrainConvert.BitByBitNo(array, 0, array.Length));
				byte[] bytes = Encoding.ASCII.GetBytes("GCKC");
				IBrainConvert.ArrayCopy(ref bytes, IBrainConvert.Short2ByteArray(num));
				IBrainConvert.ArrayCopy(ref bytes, array);
				networkStream_0.Write(bytes, 0, bytes.Length);
				devMonitorForm_0.gcShow(null, "已发送", null);
			}
			catch (Exception ex)
			{
				devMonitorForm_0.gcShow(null, ex.Message, null);
			}
		}
	}

	private byte[] method_8(byte byte_3)
	{
		AccStyle accStyle_ = AccStyle.Write;
		switch (byte_3)
		{
		case 8:
			method_3(accStyle_, class56_0);
			return class56_0.GetTempSetedList();
		case 1:
			return null;
		case 9:
			method_6(accStyle_, class56_0);
			return class56_0.GetTempSettingList();
		case 2:
			return null;
		case 10:
			method_5(accStyle_, class56_0);
			return class56_0.GetEventTable0();
		case 6:
			return null;
		case 3:
			return class56_0.GetExeFileNumber();
		case 4:
			return null;
		case 12:
			return class56_0.GetInjectInterval();
		case 5:
			return null;
		case 7:
			class56_0.SetInsSerial(devMonitorForm_0.tbinsSerial.Text);
			return class56_0.insSerial;
		case 11:
			return null;
		case 13:
			return null;
		case 14:
			method_4(accStyle_, class56_0);
			return class56_0.GetDetectorSettingList();
		case 22:
			class56_0.sglNumberStart = class56_1.detectorSettingList[devMonitorForm_0.ChannelNo].detectorType;
			return class56_0.GetCurSglNumberStart();
		case 23:
			class56_0.sglNumberEnd = class56_1.detectorSettingList[devMonitorForm_0.ChannelNo].detectorType;
			return class56_0.GetCurSglNumberEnd();
		case 35:
			return null;
		case 36:
			return null;
		case 37:
			return class56_0.GetEPCDevParam(0, 0);
		case 38:
			return null;
		case 39:
			return class56_0.GetEpcNameList();
		case 40:
			return null;
		case 41:
			class56_0.epcGasType = 0;
			return new byte[1] { class56_0.epcGasType };
		case 48:
			return null;
		case 49:
		{
			IArrayBase.NewArray2(ref class56_0.ipAddressList, 6);
			for (int j = 0; j < class56_0.ipAddressList.Length; j++)
			{
				object value3 = devMonitorForm_0.gvNet.Rows[j].Cells[0].Value;
				if (value3 == null)
				{
					throw new Exception("网络参数 Null");
				}
				class56_0.ipAddressList[j] = new MyIPAddress(value3.ToString());
			}
			return class56_0.GetIpAddressList();
		}
		case 64:
			return null;
		case 65:
		{
			int index2 = devMonitorForm_0.clmCT6CN.Index;
			int index3 = devMonitorForm_0.clmCT6EN.Index;
			for (int i = 0; i < 6; i++)
			{
				object value2 = devMonitorForm_0.dgvCT6.Rows[i].Cells[index2].Value;
				class56_0.tempCtrlAreaTable.tempList[i].strNameCn = ((value2 != null) ? value2.ToString() : "");
				value2 = devMonitorForm_0.dgvCT6.Rows[i].Cells[index3].Value;
				class56_0.tempCtrlAreaTable.tempList[i].strNameEn = ((value2 != null) ? value2.ToString() : "");
			}
			return class56_0.tempCtrlAreaTable.Name2Byte(6);
		}
		case 66:
			return null;
		case 67:
		{
			int index = devMonitorForm_0.clmCT6CtrlT.Index;
			object value = devMonitorForm_0.dgvCT6.Rows[5].Cells[index].Value;
			class56_0.insDevEnable5 = value != null && (bool)value;
			value = devMonitorForm_0.dgvCT6.Rows[4].Cells[index].Value;
			class56_0.insDevEnable4 = value != null && (bool)value;
			value = devMonitorForm_0.dgvCT6.Rows[3].Cells[index].Value;
			class56_0.insDevEnable3 = value != null && (bool)value;
			value = devMonitorForm_0.dgvCT6.Rows[2].Cells[index].Value;
			class56_0.insDevEnable2 = value != null && (bool)value;
			value = devMonitorForm_0.dgvCT6.Rows[1].Cells[index].Value;
			class56_0.insDevEnable1 = value != null && (bool)value;
			value = devMonitorForm_0.dgvCT6.Rows[0].Cells[index].Value;
			class56_0.insDevEnable0 = value != null && (bool)value;
			return class56_0.GetInsDevEnable();
		}
		case 33:
		case 34:
			return null;
		case 20:
		case 21:
			return null;
		case 18:
		case 19:
			return null;
		case 16:
		case 17:
			return null;
		default:
			return null;
		}
	}

	private void method_9()
	{
		if (tcpClient_0 != null)
		{
			if (tcpClient_0 != null && tcpClient_0.Connected)
			{
				networkStream_0.Close();
				tcpClient_0.Close();
				tcpClient_0 = null;
			}
			if (int_0 == 0 && tcpListener_0.Server.IsBound)
			{
				tcpListener_0.Stop();
			}
		}
	}

	private string method_10(byte[] byte_3)
	{
		for (int i = 0; i < class44_0.float_0.Length; i++)
		{
			class44_0.float_0[i] = IBrainConvert.ByteArray2Float(byte_3, i * 2, 1);
		}
		int num = 12;
		int num2 = 12;
		num = num2 + 1;
		byte b = byte_3[num2];
		int num3 = 13;
		num = num3 + 1;
		byte b2 = byte_3[num3];
		class44_0.bool_1 = IBrainConvert.Byte2Bool(b, 7);
		class44_0.bool_12 = IBrainConvert.Byte2Bool(b, 6);
		class44_0.bool_14 = IBrainConvert.Byte2Bool(b, 5);
		class44_0.bool_21 = IBrainConvert.Byte2Bool(b, 4);
		class44_0.bool_2 = IBrainConvert.Byte2Bool(b, 3);
		class44_0.bool_7 = IBrainConvert.Byte2Bool(b2, 7);
		class44_0.bool_5 = IBrainConvert.Byte2Bool(b2, 5);
		class44_0.bool_6 = IBrainConvert.Byte2Bool(b2, 4);
		class44_0.bool_13 = IBrainConvert.Byte2Bool(b2, 3);
		class44_0.bool_3 = IBrainConvert.Byte2Bool(b2, 2);
		class44_0.bool_4 = IBrainConvert.Byte2Bool(b2, 1);
		class44_0.bool_0 = IBrainConvert.Byte2Bool(b2, 0);
		int num4 = 14;
		num = num4 + 1;
		byte b3 = byte_3[num4];
		class44_0.bool_10 = IBrainConvert.Byte2Bool(b3, 7);
		class44_0.bool_11 = IBrainConvert.Byte2Bool(b3, 6);
		class44_0.bool_9 = IBrainConvert.Byte2Bool(b3, 5);
		class44_0.bool_8 = IBrainConvert.Byte2Bool(b3, 4);
		class44_0.byte_0 = (byte)(b3 & 0xE);
		int num5 = 15;
		num = num5 + 1;
		byte b4 = byte_3[num5];
		class44_0.bool_16 = IBrainConvert.Byte2Bool(b4, 7);
		class44_0.bool_15 = IBrainConvert.Byte2Bool(b4, 6);
		class44_0.byte_1 = (byte)(b4 & 0xE);
		int num6 = 16;
		num = num6 + 1;
		byte b5 = byte_3[num6];
		class44_0.bool_18 = IBrainConvert.Byte2Bool(b5, 7);
		class44_0.bool_17 = IBrainConvert.Byte2Bool(b5, 6);
		class44_0.byte_2 = (byte)(b5 & 0xE);
		int num7 = 17;
		num = num7 + 1;
		byte b6 = byte_3[num7];
		class44_0.bool_20 = IBrainConvert.Byte2Bool(b6, 7);
		class44_0.bool_19 = IBrainConvert.Byte2Bool(b6, 6);
		class44_0.byte_3 = (byte)(b6 & 0xE);
		if (devMonitorForm_0.tsslbGcListen.Text == "停止侦听")
		{
			for (int j = 0; j < devMonitorForm_0.dgvCT6.RowCount; j++)
			{
				devMonitorForm_0.dgvCT6.Rows[j].Cells[devMonitorForm_0.clmCT6T.Index].Value = class44_0.float_0[j];
			}
		}
		Array.Resize(ref class44_0.class78_0, 0);
		byte b7 = byte_3[num++];
		if (b7 != 0)
		{
			Array.Resize(ref class78_0, b7);
			int k;
			for (k = 0; k < b7; k++)
			{
				if (class78_0[k] == null)
				{
					class78_0[k] = new DetectorParse(k);
				}
				class78_0[k].method_0(byte_3, ref num);
			}
			class44_0.class78_0 = class78_0;
			k = 0;
			while (k < class78_0.Length)
			{
				for (int l = 0; l < dtC_Channel_0.Length; l++)
				{
					if (!dtC_Channel_0[l].IsGC08)
					{
						continue;
					}
					if (dtC_Channel_0[l].mark != 0)
					{
						if (class78_0[k].byte_2 != dtC_Channel_0[l].mark)
						{
							continue;
						}
						dtC_Channel_0[l].Gc08Values(class78_0[k].float_0);
					}
					else
					{
						dtC_Channel_0[l].mark = class78_0[k].byte_2;
						dtC_Channel_0[l].name = DetectorSettingRow.GetDeviceTypeNameByIdx(class78_0[k].byte_2);
						instrument_0.RefreshName();
						dtC_Channel_0[l].Gc08Values(class78_0[k].float_0);
					}
					k++;
					break;
				}
			}
		}
		return "";
	}

	private void tmrListen_Tick(object sender, EventArgs e)
	{
		try
		{
			if (!tcpListener_0.Server.IsBound)
			{
				tcpListener_0.Start();
			}
			else if (tcpListener_0.Pending())
			{
				if (tcpClient_0 != null && tcpClient_0.Connected)
				{
					networkStream_0.Close();
					tcpClient_0.Close();
				}
				tcpClient_0 = tcpListener_0.AcceptTcpClient();
				networkStream_0 = tcpClient_0.GetStream();
				networkStream_0.BeginRead(byte_1, 0, byte_1.Length, method_0, networkStream_0);
				devMonitorForm_0.gcShow(null, "已建立连接", true);
				tmrListen.Enabled = false;
				devMonitorForm_0.gcShow("停止侦听", null, null);
			}
		}
		catch (Exception ex)
		{
			devMonitorForm_0.gcShow(null, ex.Message, false);
		}
	}
}
