using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace IBrainChrom2018.Unit;

public class AssemblyInfoCfg
{
	private static AssemblyInfoCfg myself = null;

	public static string Title;

	public static string VOCTitle;

	public static string Corp;

	public static string Website;

	public static string Tel;

	public static string QQ;

	public static string HelpOnLine;

	public static string BannerImage;

	public static string AboutLogoImage;

	public static string IconImage;

	public static string SplashImage;

	public static string FileExtName;

	public static string LogoIcon;

	private static XmlDocument AssemblyInfo = new XmlDocument();

	public static AssemblyInfoCfg Instance => myself;

	public static AssemblyInfoCfg Create()
	{
		if (myself == null)
		{
			myself = new AssemblyInfoCfg();
		}
		return myself;
	}

	private AssemblyInfoCfg()
	{
		SystemParam systemParam = SystemParam.Create();
		string text = "AssemblyInfo.xml";
		if (File.Exists(text))
		{
			AssemblyInfo.Load(text);
		}
		else
		{
			LogMgr.Instance.Write2RunLog("AssemblyInfoCfg Wrong:AssemblyInfo.xml文件不存在，无法加载配置的信息，将采用默认信息代替。");
		}
		string language = systemParam.Language;
		if (language.Equals("zh-cn"))
		{
			Title = GetNodeString("Title", "IBrainChrom 色谱工作站");
			VOCTitle = GetNodeString("VOCTitle", "VOC在线监测系统");
			Corp = GetNodeString("Corp", "合肥智能科技有限公司 版权所有");
			Tel = GetNodeString("Tel", "咨询热线:0551-62810307");
			QQ = GetNodeString("QQ", "客服QQ:446996068");
			Website = GetNodeString("Website", "www.ibrain-smart.com");
			HelpOnLine = GetNodeString("HelpOnLine", "http://www.ibrain-smart.com/help");
			BannerImage = GetNodeString("BannerImage", "Image\\Banner.png");
			AboutLogoImage = GetNodeString("AboutLogoImage", "Image\\Logo.png");
			IconImage = GetNodeString("IconImage", "Image\\ico.png");
			SplashImage = GetNodeString("SplashImage", "Image\\Splash.png");
			FileExtName = GetNodeString("FileExtName", "kx");
			LogoIcon = GetNodeString("LogoIcon", "Image\\Logo.ico");
		}
		else
		{
			Title = GetNodeString("TitleEn", "IBrainChrom Station ");
			VOCTitle = GetNodeString("VOCTitleEn", "VOC OnLine System");
			Corp = GetNodeString("CorpEn", "HeFei IBrain Intelligent Technology Co.,Ltd. All Rights Reserved.");
			Tel = GetNodeString("TelEn", "Customer HotLine:0551-62810307");
			QQ = GetNodeString("QQEn", "Customer Service QQ :446996068");
			Website = GetNodeString("WebsiteEn", "www.ibrain-smart.com");
			HelpOnLine = GetNodeString("HelpOnLineEn", "http://www.ibrain-smart.com/help");
			BannerImage = GetNodeString("BannerImageEn", "Image\\Banner.png");
			AboutLogoImage = GetNodeString("AboutLogoImageEn", "Image\\Logo.png");
			IconImage = GetNodeString("IconImageEn", "Image\\ico.png");
			SplashImage = GetNodeString("SplashImageEn", "Image\\Splash.png");
			FileExtName = GetNodeString("FileExtName", "kx");
			LogoIcon = GetNodeString("LogoIcon", "Image\\Logo.ico");
		}
	}

	private static string GetNodeString(string strNode, string strDefault)
	{
		string result = strDefault;
		if (AssemblyInfo.DocumentElement != null && AssemblyInfo.DocumentElement.SelectSingleNode(strNode) != null)
		{
			result = AssemblyInfo.DocumentElement.SelectSingleNode(strNode).InnerText;
		}
		return result;
	}

	public static string SoftVersion()
	{
		return Assembly.GetExecutingAssembly().GetName().Version.ToString();
	}

	public static string ExeFileVersion()
	{
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Program.ApplicationName + ".exe");
		return versionInfo.FileVersion;
	}
}
