using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018.Unit;

public static class MiniDump
{
	private struct MinidumpExceptionInfo
	{
		public int ThreadId;

		public IntPtr ExceptionPointers;

		public bool ClientPointers;
	}

	[Flags]
	public enum MiniDumpType
	{
		MiniDumpNormal = 0,
		MiniDumpWithDataSegs = 1,
		MiniDumpWithFullMemory = 2,
		MiniDumpWithHandleData = 4,
		MiniDumpFilterMemory = 8,
		MiniDumpScanMemory = 0x10,
		MiniDumpWithUnloadedModules = 0x20,
		MiniDumpWithIndirectlyReferencedMemory = 0x40,
		MiniDumpFilterModulePaths = 0x80,
		MiniDumpWithProcessThreadData = 0x100,
		MiniDumpWithPrivateReadWriteMemory = 0x200,
		MiniDumpWithoutOptionalData = 0x400,
		MiniDumpWithFullMemoryInfo = 0x800,
		MiniDumpWithThreadInfo = 0x1000,
		MiniDumpWithCodeSegs = 0x2000,
		MiniDumpWithoutAuxiliaryState = 0x4000,
		MiniDumpWithFullAuxiliaryState = 0x8000,
		MiniDumpWithPrivateWriteCopyMemory = 0x10000,
		MiniDumpIgnoreInaccessibleMemory = 0x20000,
		MiniDumpWithTokenInformation = 0x40000,
		MiniDumpWithModuleHeaders = 0x80000,
		MiniDumpFilterTriage = 0x100000,
		MiniDumpValidTypeFlags = 0x1FFFFF
	}

	[DllImport("kernel32.dll")]
	private static extern int GetCurrentThreadId();

	[DllImport("DbgHelp.dll")]
	private static extern bool MiniDumpWriteDump(IntPtr hProcess, int processId, IntPtr fileHandle, MiniDumpType dumpType, ref MinidumpExceptionInfo excepInfo, IntPtr userInfo, IntPtr extInfo);

	[DllImport("DbgHelp.dll")]
	private static extern bool MiniDumpWriteDump(IntPtr hProcess, int processId, IntPtr fileHandle, MiniDumpType dumpType, IntPtr excepParam, IntPtr userInfo, IntPtr extInfo);

	[DllImport("Kernel32.dll")]
	public static extern int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);

	public static string ChechAndGetDumpFolder()
	{
		string text = Application.StartupPath + "\\Dump";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "Dump.dmp";
	}

	public static bool TryDump()
	{
		string dmpFileName = ChechAndGetDumpFolder();
		return TryDump(dmpFileName, MiniDumpType.MiniDumpWithFullMemory);
	}

	public static bool TryDump2(string strProcessName)
	{
		string dmpFileName = ChechAndGetDumpFolder();
		return TryDump(strProcessName, dmpFileName, MiniDumpType.MiniDumpWithFullMemory);
	}

	public static bool TryDump(string dmpPath)
	{
		return TryDump(dmpPath, MiniDumpType.MiniDumpWithFullMemory);
	}

	public static bool TryDump(string strProcessName, string dmpPath)
	{
		return TryDump(strProcessName, dmpPath, MiniDumpType.MiniDumpWithFullMemory);
	}

	public static bool TryDump(string dmpFileName, MiniDumpType dmpType)
	{
		using FileStream fileStream = new FileStream(dmpFileName, FileMode.OpenOrCreate);
		Process currentProcess = Process.GetCurrentProcess();
		MinidumpExceptionInfo excepInfo = new MinidumpExceptionInfo
		{
			ThreadId = GetCurrentThreadId(),
			ExceptionPointers = Marshal.GetExceptionPointers(),
			ClientPointers = true
		};
		if (excepInfo.ExceptionPointers == IntPtr.Zero)
		{
			return MiniDumpWriteDump(currentProcess.Handle, currentProcess.Id, fileStream.SafeFileHandle.DangerousGetHandle(), dmpType, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}
		return MiniDumpWriteDump(currentProcess.Handle, currentProcess.Id, fileStream.SafeFileHandle.DangerousGetHandle(), dmpType, ref excepInfo, IntPtr.Zero, IntPtr.Zero);
	}

	public static bool TryDump(string strProcessName, string dmpFileName, MiniDumpType dmpType)
	{
		Process[] processesByName = Process.GetProcessesByName(strProcessName);
		if (processesByName.Length == 0)
		{
			return false;
		}
		Process process = processesByName[0];
		using FileStream fileStream = new FileStream(dmpFileName, FileMode.OpenOrCreate);
		MinidumpExceptionInfo excepInfo = new MinidumpExceptionInfo
		{
			ThreadId = GetCurrentThreadId(),
			ExceptionPointers = Marshal.GetExceptionPointers(),
			ClientPointers = true
		};
		if (excepInfo.ExceptionPointers == IntPtr.Zero)
		{
			return MiniDumpWriteDump(process.Handle, process.Id, fileStream.SafeFileHandle.DangerousGetHandle(), dmpType, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}
		return MiniDumpWriteDump(process.Handle, process.Id, fileStream.SafeFileHandle.DangerousGetHandle(), dmpType, ref excepInfo, IntPtr.Zero, IntPtr.Zero);
	}

	public static string GetSysErrMsg(int errCode)
	{
		IntPtr source = IntPtr.Zero;
		string buf = null;
		FormatMessage(4864, ref source, errCode, 0, ref buf, 255, ref source);
		return buf;
	}
}
