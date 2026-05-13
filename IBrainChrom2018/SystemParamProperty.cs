using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[StructLayout(LayoutKind.Sequential)]
public class SystemParamProperty
{
	public class ChrgOptionShowMethodComboBoxItem : PropertyComboBox
	{
		public override void GetConvertHash()
		{
			_hash.Add(Lang.PS("平移"));
			_hash.Add(Lang.PS("缩进"));
		}
	}

	public class ComComboBoxItem : PropertyComboBox
	{
		public override void GetConvertHash()
		{
			_hash.Add(Lang.PS("Com0"));
			_hash.Add(Lang.PS("Com1"));
			_hash.Add(Lang.PS("Com2"));
			_hash.Add(Lang.PS("Com3"));
			_hash.Add(Lang.PS("Com4"));
			_hash.Add(Lang.PS("Com5"));
			_hash.Add(Lang.PS("Com6"));
			_hash.Add(Lang.PS("Com7"));
			_hash.Add(Lang.PS("Com8"));
			_hash.Add(Lang.PS("Com9"));
			_hash.Add(Lang.PS("Com10"));
		}
	}

	public class ComModbusComboBoxItem : PropertyComboBox
	{
		public override void GetConvertHash()
		{
			_hash.Add(Lang.PS("V1.0"));
			_hash.Add(Lang.PS("V2.0"));
		}
	}

	public class PropertyButtonEditor : MyEditor
	{
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			switch (context.PropertyDescriptor.Name)
			{
			case "strPasswordNewCfg":
			{
				SystemParam systemParam = SystemParam.Create();
				string strPasswordOld = systemParam.strPasswordOld;
				string strPasswordNew = systemParam.strPasswordNew;
				string text = (string)value;
				if (strPasswordNew != text)
				{
					MessageBox.Show(Lang.PS("新密码不一致", "The new password does not match "));
					return value;
				}
				if (strPasswordNew.Length < 6)
				{
					MessageBox.Show(Lang.PS("密码长度需要大于6位！", "Password length should be greater than 6!"));
					return value;
				}
				string u_name = Class49.user_0.u_name;
				Logon logon = new Logon();
				if (logon.ChangePwd(u_name, strPasswordOld, strPasswordNew))
				{
					MessageBox.Show("密码修改成功");
					return value;
				}
				MessageBox.Show(Lang.PS("原密码错误", "The old password is wrong "));
				return value;
			}
			case "strDirOptionInitDir":
			{
				string strInitFolder = (string)value;
				strInitFolder = SelectFolder(strInitFolder);
				if (strInitFolder != "")
				{
					return strInitFolder;
				}
				break;
			}
			case "DetectorOption":
			{
				DetectorParamOptionDlg detectorParamOptionDlg = new DetectorParamOptionDlg();
				detectorParamOptionDlg.ShowDialog();
				break;
			}
			}
			return value;
		}

		private string SelectFolder(string strInitFolder)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (strInitFolder != "" && Directory.Exists(strInitFolder))
			{
				folderBrowserDialog.SelectedPath = strInitFolder;
			}
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				return folderBrowserDialog.SelectedPath;
			}
			folderBrowserDialog.Dispose();
			folderBrowserDialog = null;
			return "";
		}

		private string SelectFilePath(string strInitFolder)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "*.mdb|*.mdb";
			if (strInitFolder != "" && Directory.Exists(strInitFolder))
			{
				openFileDialog.InitialDirectory = strInitFolder;
			}
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog.FileName;
			}
			openFileDialog.Dispose();
			openFileDialog = null;
			return "";
		}
	}

	private static SystemParamProperty myself = null;

	private const string strChromGraphColor = "1.谱图颜色";

	private const string strChromGraphOption = "2.谱图选项";

	private const string strDirOption = "3.目录选项";

	private const string strFileNameOption = "4.文件命名选项";

	private const string strReportOption = "1.打印选项";

	private const string strComOption = "2.串口设置";

	private const string strIpOption = "3.IP设置";

	private const string strDcsOption = "4.DCS设置";

	private const string strPasswordOption = "5.密码设置";

	private const string strDetectorOption = "5.检测器设置";

	private const string strCloudOption = "6.云端设置";

	private SystemParam sysParam = SystemParam.Create();

	private string strPsdNewCfg = "";

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图背景色")]
	[PropertyDescription("谱图背景色")]
	public Color corChrgColorBackGround
	{
		get
		{
			return sysParam.corChrgColorBackGround;
		}
		set
		{
			sysParam.corChrgColorBackGround = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图网格颜色")]
	[PropertyDescription("谱图网格颜色")]
	public Color corChrgColoGrid
	{
		get
		{
			return sysParam.corChrgColoGrid;
		}
		set
		{
			sysParam.corChrgColoGrid = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线颜色(非采样)")]
	[PropertyDescription("设置谱图在实时采集时，非采样时的颜色")]
	public Color corChrgColoAcq
	{
		get
		{
			return sysParam.corChrgColoAcq;
		}
		set
		{
			sysParam.corChrgColoAcq = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线1颜色")]
	[PropertyDescription("设置谱图基线1颜色(通道1)")]
	public Color corChrgColoCurve1
	{
		get
		{
			return sysParam.corChrgColoCurve1;
		}
		set
		{
			sysParam.corChrgColoCurve1 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线2颜色")]
	[PropertyDescription("谱图基线2颜色(通道2)")]
	public Color corChrgColoCurve2
	{
		get
		{
			return sysParam.corChrgColoCurve2;
		}
		set
		{
			sysParam.corChrgColoCurve2 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线3颜色")]
	[PropertyDescription("谱图基线3颜色(通道3)")]
	public Color corChrgColoCurve3
	{
		get
		{
			return sysParam.corChrgColoCurve3;
		}
		set
		{
			sysParam.corChrgColoCurve3 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线4颜色")]
	[PropertyDescription("谱图基线4颜色")]
	public Color corChrgColoCurve4
	{
		get
		{
			return sysParam.corChrgColoCurve4;
		}
		set
		{
			sysParam.corChrgColoCurve4 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线5颜色")]
	[PropertyDescription("谱图基线5颜色")]
	public Color corChrgColoCurve5
	{
		get
		{
			return sysParam.corChrgColoCurve5;
		}
		set
		{
			sysParam.corChrgColoCurve5 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线6颜色")]
	[PropertyDescription("谱图基线6颜色")]
	public Color corChrgColoCurve6
	{
		get
		{
			return sysParam.corChrgColoCurve6;
		}
		set
		{
			sysParam.corChrgColoCurve6 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线7颜色")]
	[PropertyDescription("谱图基线7颜色")]
	public Color corChrgColoCurve7
	{
		get
		{
			return sysParam.corChrgColoCurve7;
		}
		set
		{
			sysParam.corChrgColoCurve7 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("1.谱图颜色")]
	[PropertyDisplayName("谱图基线8颜色")]
	[PropertyDescription("谱图基线8颜色")]
	public Color corChrgColoCurve8
	{
		get
		{
			return sysParam.corChrgColoCurve8;
		}
		set
		{
			sysParam.corChrgColoCurve8 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("是否显示网络线")]
	[PropertyDescription("是否显示网络线")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bChrgOptionShowGrid
	{
		get
		{
			return sysParam.bChrgOptionShowGrid;
		}
		set
		{
			sysParam.bChrgOptionShowGrid = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("是否标出峰间分割线")]
	[PropertyDescription("是否标出峰间分割线")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bChrgOptionShowPeakSplitLine
	{
		get
		{
			return sysParam.bChrgOptionShowPeakSplitLine;
		}
		set
		{
			sysParam.bChrgOptionShowPeakSplitLine = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("是否显示保持时间")]
	[PropertyDescription("是否显示保持时间")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bChrgOptionShowKeepTime
	{
		get
		{
			return sysParam.bChrgOptionShowKeepTime;
		}
		set
		{
			sysParam.bChrgOptionShowKeepTime = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("是否显示程升曲线")]
	[PropertyDescription("是否显示程升曲线")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bChrgOptionShowTempUpgrateLine
	{
		get
		{
			return sysParam.bChrgOptionShowTempUpgrateLine;
		}
		set
		{
			sysParam.bChrgOptionShowTempUpgrateLine = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("超出范围后处理")]
	[PropertyDescription("超出范围后平移，还是缩进")]
	[TypeConverter(typeof(ChrgOptionShowMethodComboBoxItem))]
	public int iChrgOptionShowMethod
	{
		get
		{
			return sysParam.iChrgOptionShowMethod;
		}
		set
		{
			sysParam.iChrgOptionShowMethod = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("浓度计算结果显示小数点后位数")]
	[PropertyDescription("浓度计算结果显示小数点后位数")]
	public int iChrgOptionDotNumberDensity
	{
		get
		{
			return sysParam.iChrgOptionDotNumberDensity;
		}
		set
		{
			sysParam.iChrgOptionDotNumberDensity = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("2.谱图选项")]
	[PropertyDisplayName("最小显示刻度")]
	[PropertyDescription("自适应显示的最小显示刻度值")]
	public int iDispMinValue
	{
		get
		{
			return sysParam.iDispMinValue;
		}
		set
		{
			sysParam.iDispMinValue = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("3.目录选项")]
	[PropertyDisplayName("保存起始目录")]
	[PropertyDescription("保存起始目录")]
	[Editor(typeof(PropertyButtonEditor), typeof(UITypeEditor))]
	public string strDirOptionInitDir
	{
		get
		{
			return sysParam.strDirOptionInitDir;
		}
		set
		{
			sysParam.strDirOptionInitDir = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("3.目录选项")]
	[PropertyDisplayName("增加色谱机名称文件夹")]
	[PropertyDescription("增加色谱机名称文件夹")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bDirOptionAddChromDir
	{
		get
		{
			return sysParam.bDirOptionAddChromDir;
		}
		set
		{
			sysParam.bDirOptionAddChromDir = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("3.目录选项")]
	[PropertyDisplayName("增加通道名称文件夹")]
	[PropertyDescription("增加通道名称文件夹")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bDirOptionAddChannelDir
	{
		get
		{
			return sysParam.bDirOptionAddChannelDir;
		}
		set
		{
			sysParam.bDirOptionAddChannelDir = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("3.目录选项")]
	[PropertyDisplayName("增加日期名称文件夹")]
	[PropertyDescription("增加日期名称文件夹")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bDirOptionAddDateDir
	{
		get
		{
			return sysParam.bDirOptionAddDateDir;
		}
		set
		{
			sysParam.bDirOptionAddDateDir = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("4.文件命名选项")]
	[PropertyDisplayName("显示机器ID")]
	[PropertyDescription("显示机器ID")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bFileNameOptionChrom
	{
		get
		{
			return sysParam.bFileNameOptionChrom;
		}
		set
		{
			sysParam.bFileNameOptionChrom = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("4.文件命名选项")]
	[PropertyDisplayName("显示通道")]
	[PropertyDescription("显示通道")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bFileNameOptionChannel
	{
		get
		{
			return sysParam.bFileNameOptionChannel;
		}
		set
		{
			sysParam.bFileNameOptionChannel = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("4.文件命名选项")]
	[PropertyDisplayName("显示时间")]
	[PropertyDescription("显示时间")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bFileNameOptionDate
	{
		get
		{
			return sysParam.bFileNameOptionDate;
		}
		set
		{
			sysParam.bFileNameOptionDate = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("4.文件命名选项")]
	[PropertyDisplayName("通道1自定义")]
	[PropertyDescription("通道1自定义")]
	public string strFileNameOptionChannel0Custom
	{
		get
		{
			return sysParam.strFileNameOptionChannel0Custom;
		}
		set
		{
			sysParam.strFileNameOptionChannel0Custom = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("4.文件命名选项")]
	[PropertyDisplayName("通道2自定义")]
	[PropertyDescription("通道2自定义")]
	public string strFileNameOptionChannel1Custom
	{
		get
		{
			return sysParam.strFileNameOptionChannel1Custom;
		}
		set
		{
			sysParam.strFileNameOptionChannel1Custom = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("4.文件命名选项")]
	[PropertyDisplayName("通道3自定义")]
	[PropertyDescription("通道3自定义")]
	public string strFileName2ptionChannel0Custom
	{
		get
		{
			return sysParam.strFileNameOptionChannel2Custom;
		}
		set
		{
			sysParam.strFileNameOptionChannel2Custom = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("5.检测器设置")]
	[PropertyDisplayName("检测器设置")]
	[PropertyDescription("检测器设置")]
	[Editor(typeof(PropertyButtonEditor), typeof(UITypeEditor))]
	public string DetectorOption
	{
		get
		{
			return "";
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("报表标题")]
	[PropertyDescription("报表标题")]
	public string strReportOptionTitle
	{
		get
		{
			return sysParam.strReportOptionTitle;
		}
		set
		{
			sysParam.strReportOptionTitle = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("打印打印时间")]
	[PropertyDescription("打印打印时间")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionPrintTime
	{
		get
		{
			return sysParam.bReportOptionPrintTime;
		}
		set
		{
			sysParam.bReportOptionPrintTime = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("打印进样时间")]
	[PropertyDescription("打印进样时间")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionInjectTime
	{
		get
		{
			return sysParam.bReportOptionInjectTime;
		}
		set
		{
			sysParam.bReportOptionInjectTime = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("打印文件名称")]
	[PropertyDescription("打印文件名称")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionFileName
	{
		get
		{
			return sysParam.bReportOptionFileName;
		}
		set
		{
			sysParam.bReportOptionFileName = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("打印结果数据")]
	[PropertyDescription("打印结果数据")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionResultData
	{
		get
		{
			return sysParam.bReportOptionResultData;
		}
		set
		{
			sysParam.bReportOptionResultData = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("原始曲线")]
	[PropertyDescription("原始曲线")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionResultOrgCurve
	{
		get
		{
			return sysParam.bReportOptionResultOrgCurve;
		}
		set
		{
			sysParam.bReportOptionResultOrgCurve = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("工作谱图")]
	[PropertyDescription("工作谱图")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionResultChromGraphic
	{
		get
		{
			return sysParam.bReportOptionResultChromGraphic;
		}
		set
		{
			sysParam.bReportOptionResultChromGraphic = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("谱线加粗")]
	[PropertyDescription("谱线加粗")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bReportOptionChromLineBold
	{
		get
		{
			return sysParam.bReportOptionChromLineBold;
		}
		set
		{
			sysParam.bReportOptionChromLineBold = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("1.打印选项")]
	[PropertyDisplayName("字号大小")]
	[PropertyDescription("字号大小")]
	public int bReportOptionChromFontSize
	{
		get
		{
			return sysParam.bReportOptionChromFontSize;
		}
		set
		{
			sysParam.bReportOptionChromFontSize = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("4.DCS设置")]
	[PropertyDisplayName("启用串口")]
	[PropertyDescription("启用串口")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bDcsComEnable
	{
		get
		{
			return sysParam.bDcsComEnable;
		}
		set
		{
			sysParam.bDcsComEnable = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("4.DCS设置")]
	[PropertyDisplayName("DCS上传串口")]
	[PropertyDescription("DCS上传串口")]
	[TypeConverter(typeof(ComComboBoxItem))]
	public int iDcsComNumber
	{
		get
		{
			return sysParam.iDcsComNumber;
		}
		set
		{
			sysParam.iDcsComNumber = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("4.DCS设置")]
	[PropertyDisplayName("4mA最小值")]
	[PropertyDescription("4mA最小值")]
	public float fDcsMinValue
	{
		get
		{
			return sysParam.fDcsMinValue;
		}
		set
		{
			sysParam.fDcsMinValue = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("4.DCS设置")]
	[PropertyDisplayName("20mA最大值")]
	[PropertyDescription("20mA最大值")]
	public float fDcsMaxValue
	{
		get
		{
			return sysParam.fDcsMaxValue;
		}
		set
		{
			sysParam.fDcsMaxValue = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("3.IP设置")]
	[PropertyDisplayName("本机IP")]
	[PropertyDescription("本机IP")]
	public string strIpLocal
	{
		get
		{
			return sysParam.strIpLocal;
		}
		set
		{
			sysParam.strIpLocal = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("3.IP设置")]
	[PropertyDisplayName("子网掩码")]
	[PropertyDescription("子网掩码")]
	public string strIpMask
	{
		get
		{
			return sysParam.strIpMask;
		}
		set
		{
			sysParam.strIpMask = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("3.IP设置")]
	[PropertyDisplayName("默认网关")]
	[PropertyDescription("默认网关")]
	public string strIpGateway
	{
		get
		{
			return sysParam.strIpGateway;
		}
		set
		{
			sysParam.strIpGateway = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("2.串口设置")]
	[PropertyDisplayName("色谱仪上传串口")]
	[PropertyDescription("色谱仪上传串口")]
	[TypeConverter(typeof(ComComboBoxItem))]
	public int iComNumber
	{
		get
		{
			return sysParam.iComNumber;
		}
		set
		{
			sysParam.iComNumber = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("2.串口设置")]
	[PropertyDisplayName("启用串口")]
	[PropertyDescription("启用串口")]
	[TypeConverter(typeof(BoolComboBoxItem))]
	public bool bComEnable
	{
		get
		{
			return sysParam.bComEnable;
		}
		set
		{
			sysParam.bComEnable = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("2.串口设置")]
	[PropertyDisplayName("Modbus版本")]
	[PropertyDescription("Modbus版本")]
	[TypeConverter(typeof(ComModbusComboBoxItem))]
	public int iComModbusType
	{
		get
		{
			return sysParam.iComModbusType;
		}
		set
		{
			sysParam.iComModbusType = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[PropertyCategory("6.云端设置")]
	[PropertyDisplayName("设备标识(24位ASCII)")]
	[PropertyDescription("Modbus寄存器801-812输出的设备标识(24位ASCII)")]
	public string strStationId
	{
		get
		{
			return sysParam.strStationId;
		}
		set
		{
			sysParam.strStationId = SystemParam.NormalizeStationId24Ascii(value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("5.密码设置")]
	[PropertyDisplayName("原密码")]
	[PropertyDescription("原密码")]
	public string strPasswordOrg
	{
		get
		{
			return sysParam.strPasswordOld;
		}
		set
		{
			sysParam.strPasswordOld = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("5.密码设置")]
	[PropertyDisplayName("新密码")]
	[PropertyDescription("新密码")]
	public string strPasswordNew
	{
		get
		{
			return sysParam.strPasswordNew;
		}
		set
		{
			sysParam.strPasswordNew = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[PropertyCategory("5.密码设置")]
	[PropertyDisplayName("确认新密码")]
	[PropertyDescription("确认新密码")]
	[Editor(typeof(PropertyButtonEditor), typeof(UITypeEditor))]
	public string strPasswordNewCfg
	{
		get
		{
			return strPsdNewCfg;
		}
		set
		{
			strPsdNewCfg = value;
		}
	}

	public static SystemParamProperty Create()
	{
		if (myself == null)
		{
			myself = new SystemParamProperty();
		}
		return myself;
	}
}
