using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018.Unit;

public class LogMgr
{
	private static LogMgr myself = null;

	private string ApplicationName = Process.GetCurrentProcess().ProcessName;

	private string strFileVersion = "";

	private string strSoftwareVersion = "";

	private Mutex docMutex = new Mutex();

	public static LogMgr Instance => myself;

	public static LogMgr Create()
	{
		if (myself == null)
		{
			myself = new LogMgr();
		}
		return myself;
	}

	private LogMgr()
	{
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		Application.ThreadException += ThreadExceptHandler;
		AppDomain.CurrentDomain.UnhandledException += UhExceptHandler;
		ClearRunLogFile();
		SystemParam systemParam = SystemParam.Create();
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
		strFileVersion = versionInfo.FileVersion;
		strSoftwareVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		if (systemParam.strProgramVersion != strSoftwareVersion)
		{
			systemParam.strProgramVersion = strSoftwareVersion;
			systemParam.SaveParam();
			ClearErrLogFile();
		}
	}

	private void ThreadExceptHandler(object sender, ThreadExceptionEventArgs args)
	{
		if (NeedMsgError())
		{
			Exception exception = args.Exception;
			MessageBox.Show(exception.Message, "程序启动错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		WriteHandler(sender, args.Exception, "ThreadExceptHandler", "");
	}

	private void UhExceptHandler(object sender, UnhandledExceptionEventArgs args)
	{
		if (NeedMsgError())
		{
			Exception ex = (Exception)args.ExceptionObject;
			MessageBox.Show(ex.Message, "程序启动错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		WriteHandler(sender, (Exception)args.ExceptionObject, "UnHandleExceptHandler", "\r\nRuntime terminating: " + args.IsTerminating);
	}

	private bool NeedMsgError()
	{
		return UIProxy.Instance.MainForm == null || (UIProxy.Instance.MainForm != null && !UIProxy.Instance.MainForm.IsLoaded);
	}

	public void LogError(string strMsg)
	{
		string strMsg2 = "Error: " + strMsg + " Time:" + DateTime.Now.ToString();
		Write2ErrorLog(strMsg2);
	}

	public void LogWarning(string strMsg)
	{
		string strMsg2 = "Warning: " + strMsg + " Time:" + DateTime.Now.ToString();
		Write2ErrorLog(strMsg2);
	}

	private void Write2ErrorLog(string strMsg)
	{
		try
		{
			docMutex.WaitOne();
			string text = Application.StartupPath + "\\";
			if (!Directory.Exists(text + "ErrLog"))
			{
				Directory.CreateDirectory(text + "ErrLog");
			}
			using (StreamWriter streamWriter = new StreamWriter(text + "ErrLog\\ErrLog.txt", append: true))
			{
				streamWriter.WriteLine(strMsg);
				streamWriter.WriteLine("---------------------------------------------------------");
				streamWriter.Close();
			}
			docMutex.ReleaseMutex();
		}
		catch
		{
		}
	}

	public void LogResult(string strMsg)
	{
		string strMsg2 = "Result: " + strMsg + " Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		Write2ResultLog(strMsg2);
	}

	private void Write2ResultLog(string strMsg)
	{
		try
		{
			docMutex.WaitOne();
			string text = Application.StartupPath + "\\";
			if (!Directory.Exists(text + "ResultLog"))
			{
				Directory.CreateDirectory(text + "ResultLog");
			}
			using (StreamWriter streamWriter = new StreamWriter(text + "ResultLog\\ResultLog.txt", append: true))
			{
				streamWriter.WriteLine(strMsg);
				streamWriter.Close();
			}
			docMutex.ReleaseMutex();
		}
		catch
		{
		}
	}

	public void Write2RunLog(string strMsg, params string[] strParam)
	{
		if (strParam.Length != 0)
		{
			for (int i = 0; i < strParam.Length; i++)
			{
				strMsg = strMsg.Replace("{" + i + "}", string.Concat(strParam[i]));
			}
		}
		strMsg = strMsg + "   " + DateTime.Now.ToString();
		Write2RunLog2(strMsg);
	}

	public void Write2RunLog2(string strMsg)
	{
		try
		{
			docMutex.WaitOne();
			string text = Application.StartupPath + "\\";
			if (!Directory.Exists(text + "ErrLog"))
			{
				Directory.CreateDirectory(text + "ErrLog");
			}
			using (StreamWriter streamWriter = new StreamWriter(text + "ErrLog\\RunLog.txt", append: true))
			{
				streamWriter.WriteLine(strMsg);
				streamWriter.WriteLine("---------------------------------------------------------");
				streamWriter.Close();
			}
			docMutex.ReleaseMutex();
		}
		catch
		{
		}
	}

	private void WriteHandler(object sender, Exception e, string beginMessage, string endMessage)
	{
		string text = "Software Version: " + strSoftwareVersion + ". \r\n";
		text = text + "File Version: " + strFileVersion + ". \r\n";
		text = text + "Time: " + DateTime.Now.ToString() + "  \r\n";
		StringBuilder stringBuilder = new StringBuilder(" MyHandler caught: " + e.Message + "\r\nTargetSite: " + e.TargetSite.ToString() + "\r\nSource: " + e.Source + "\r\nHeap and Stack: " + e.StackTrace);
		if (e.StackTrace.IndexOf("Drawing") != -1 || e.StackTrace.IndexOf("OnPaint") != -1)
		{
			stringBuilder.AppendLine("\r\nSampleDisplay Debug:" + UIProxy.Instance.MainForm.sampleDisplay.DebugLog);
		}
		if (e.Message.IndexOf("内存不足") != -1 || e.Message.IndexOf("Memory") != -1)
		{
			string text2 = ((float)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576f).ToString();
			stringBuilder.AppendLine("Crush Memroy:" + text2);
		}
		if (e.Message.IndexOf("AsyncTcpServer.method_3") != -1)
		{
			TcpServerSocket currentTcpSocket = UIProxy.Instance.MainForm.GetCurrentTcpSocket();
			stringBuilder.AppendLine("\r\nAsyncTcpServer Debug:" + currentTcpSocket.DebugLog);
		}
		while ((e = e.InnerException) != null)
		{
			stringBuilder.AppendLine(e.Message + "\r\nHeap and Stack: " + e.StackTrace);
		}
		Write2ErrorLog(text + beginMessage + stringBuilder.ToString() + endMessage);
		UIProxy.Instance.Error(beginMessage + stringBuilder.ToString(), null);
	}

	private void ClearRunLogFile()
	{
		try
		{
			docMutex.WaitOne();
			string path = Application.StartupPath + "\\ErrLog\\RunLog.txt";
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			docMutex.ReleaseMutex();
		}
		catch
		{
		}
	}

	private void ClearErrLogFile()
	{
		try
		{
			docMutex.WaitOne();
			string path = Application.StartupPath + "\\ErrLog\\ErrLog.txt";
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			docMutex.ReleaseMutex();
		}
		catch
		{
		}
	}

	public void ClearLogFiles()
	{
		string path = Application.StartupPath + "\\ErrLog";
		if (Directory.Exists(path))
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			FileSystemInfo[] array = fileSystemInfos;
			foreach (FileSystemInfo fileSystemInfo in array)
			{
				if (fileSystemInfo is DirectoryInfo)
				{
					DirectoryInfo directoryInfo2 = new DirectoryInfo(fileSystemInfo.FullName);
					directoryInfo2.Delete(recursive: true);
				}
				else
				{
					File.Delete(fileSystemInfo.FullName);
				}
			}
		}
		string path2 = Application.StartupPath + "\\Dump";
		if (!Directory.Exists(path2))
		{
			return;
		}
		DirectoryInfo directoryInfo3 = new DirectoryInfo(path2);
		FileSystemInfo[] fileSystemInfos2 = directoryInfo3.GetFileSystemInfos();
		FileSystemInfo[] array2 = fileSystemInfos2;
		foreach (FileSystemInfo fileSystemInfo2 in array2)
		{
			if (fileSystemInfo2 is DirectoryInfo)
			{
				DirectoryInfo directoryInfo4 = new DirectoryInfo(fileSystemInfo2.FullName);
				directoryInfo4.Delete(recursive: true);
			}
			else
			{
				File.Delete(fileSystemInfo2.FullName);
			}
		}
	}
}
