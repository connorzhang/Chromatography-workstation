using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class claPrintGY
{
	public SerialPort sp = new SerialPort();

	public string strThcMax = null;

	public string strThcMin = null;

	public string strThcAvr = null;

	public string strCH4Avr = null;

	public string strCH4Max = null;

	public string strCH4Min = null;

	public string strNMHCAvr = null;

	public string strNMHCMax = null;

	public string strNMHCMin = null;

	public string strLongitude = null;

	public string strLatitude = null;

	public string strSamplingPerson = null;

	public string strTemperature = null;

	public string strHumidity = null;

	public string strAtmosphericPressure = null;

	public string strSampleTimes = null;

	private AreaPlotParamMgr plotParamMgr = AreaPlotParamMgr.Create();

	private AreaPlotParam plotParam = null;

	private Thread tr;

	private Thread tcpr;

	public void SetPortProperty()
	{
		try
		{
			if (!sp.IsOpen)
			{
				sp.PortName = "COM1";
				sp.BaudRate = 9600;
				sp.StopBits = StopBits.One;
				sp.Parity = Parity.None;
				sp.ReadTimeout = -1;
				sp.Open();
			}
		}
		catch (Exception)
		{
		}
	}

	public void printPram()
	{
		LYTHCPara lYTHCPara = LYTHCPara.Create();
		plotParam = plotParamMgr.GetAreaPlotParam(1);
		string s = "..............................\r         崂应3035型           \r\u3000\u3000\u3000\u3000便携式总烃、甲烷\u3000\u3000\u3000\u3000\r      和非甲烷总烃监测仪        \r       工况数据监测报表         \r仪器编号: " + lYTHCPara.strCollectBH + "\r.............................\r                             \r样品名称: " + lYTHCPara.strCollectSJDW + "\r采样地点: " + lYTHCPara.strCollectSite + "\r检测项目: " + lYTHCPara.strCollectJCXM + "\r检测单位: " + lYTHCPara.strCollectJYDW + "\r采样人员: " + lYTHCPara.strCollectP + "\r环境温度: " + strTemperature + "\r环境湿度: " + strHumidity + "\r位置经度: " + strLongitude + "\r位置纬度: " + strLatitude + "\r大气压  : " + strAtmosphericPressure + "\r                 \r         分析结果(" + plotParam.UintName + ")\r                 \r总      烃:     " + strThcAvr + "\r甲      烷:     " + strCH4Avr + "\r非甲烷总烃:     " + strNMHCAvr + "\r                 \r采样次数:  " + strSampleTimes + "\r开始时间:" + LYTHCtrl2.selfCtrl.strStartTime + "\r结束时间:" + LYTHCtrl2.selfCtrl.strStopTime + "\r打印时间:" + DateTime.Now.ToString() + "\r\r\n\r\n";
		//string s = "..............................\r         崂应3035型           \r\u3000\u3000\u3000\u3000便携式总烃、甲烷\u3000\u3000\u3000\u3000\r      和非甲烷总烃监测仪        \r       工况数据监测报表         \r仪器编号: " + lYTHCPara.strCollectBH + "\r.............................\r                             \r样品名称: " + lYTHCPara.strCollectSJDW + "\r采样地点: " + lYTHCPara.strCollectSite + "\r检测项目: " + lYTHCPara.strCollectJCXM + "\r检测单位: " + lYTHCPara.strCollectJYDW + "\r采样人员: " + lYTHCPara.strCollectP + "\r环境温度: " + strTemperature + "\r环境湿度: " + strHumidity + "\r位置经度: " + strLongitude + "\r位置纬度: " + strLatitude + "\r大气压  : " + strAtmosphericPressure + "\r                 \r         分析结果(" + plotParam.UintName + ")\r                 \r总      烃:     " + strThcAvr + "\r甲      烷:     " + strCH4Avr + "\r非甲烷总烃:     " + strNMHCAvr + "\r                 \r采样次数:  " + strSampleTimes + "\r开始时间:" + "\r结束时间:" +  "\r打印时间:" + DateTime.Now.ToString() + "\r\r\n\r\n";
		byte[] bytes = Encoding.Default.GetBytes(s);
		if (bytes != null && bytes.Length != 0)
		{
			try
			{
				sp.Write(bytes, 0, bytes.Length);
			}
			catch
			{
			}
		}
	}
}
