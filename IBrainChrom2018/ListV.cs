using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

public class ListV
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct LVITEM
	{
		public int mask;

		public int iItem;

		public int iSubItem;

		public int state;

		public int stateMask;

		public IntPtr pszText;

		public int cchTextMax;
	}

	public const uint LVM_FIRST = 4096u;

	public const uint HDM_FIRST = 4608u;

	public const uint LVM_GETITEMCOUNT = 4100u;

	public const uint LVM_GETITEMW = 4171u;

	public const uint LVM_GETHEADER = 4127u;

	public const uint HDM_GETITEMCOUNT = 4608u;

	public const uint LVM_GETITEMSTATE = 4140u;

	public const uint LVM_GETITEMTEXTA = 4141u;

	public const uint LVM_GETITEMTEXTW = 4211u;

	public const uint PROCESS_VM_OPERATION = 8u;

	public const uint PROCESS_VM_READ = 16u;

	public const uint PROCESS_VM_WRITE = 32u;

	private const uint MEM_COMMIT = 4096u;

	private const uint MEM_RELEASE = 32768u;

	public const uint MEM_RESERVE = 8192u;

	private const uint PAGE_READWRITE = 4u;

	public static int LVIF_TEXT = 1;

	[DllImport("user32.DLL")]
	public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

	[DllImport("user32.DLL")]
	public static extern IntPtr FindWindow(string lpszClass, string lpszWindow);

	[DllImport("user32.DLL")]
	public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

	[DllImport("user32.dll")]
	public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);

	[DllImport("kernel32.dll")]
	public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

	[DllImport("kernel32.dll")]
	public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

	[DllImport("kernel32.dll")]
	public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

	[DllImport("kernel32.dll")]
	public static extern bool CloseHandle(IntPtr handle);

	[DllImport("kernel32.dll")]
	public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

	[DllImport("kernel32.dll")]
	public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

	public static int ListView_GetItemCount(IntPtr hwnd)
	{
		return SendMessage(hwnd, 4100u, 0, 0);
	}

	public static IntPtr ListView_GetHeader(IntPtr hwnd)
	{
		return (IntPtr)SendMessage(hwnd, 4127u, 0, 0);
	}

	public static int Header_GetItemCount(IntPtr header)
	{
		return SendMessage(header, 4608u, 0, 0);
	}

	public static int ListViewColumnCount(IntPtr listViewHandle)
	{
		return Header_GetItemCount(ListView_GetHeader(listViewHandle));
	}

	public static List<string> ListViewGetItem(IntPtr listViewHandle)
	{
		List<string> list = new List<string>();
		int num = ListView_GetItemCount(listViewHandle);
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				int num2 = Win32.SendMessage(listViewHandle, 4140, i, 2);
				if (num2 == 2)
				{
					byte[] array = new byte[256];
					LVITEM lVITEM = new LVITEM
					{
						mask = LVIF_TEXT,
						iItem = i,
						iSubItem = 0,
						cchTextMax = array.Length,
						pszText = Marshal.AllocHGlobal(4096)
					};
					int cb = Marshal.SizeOf((object)lVITEM);
					IntPtr intPtr = Marshal.AllocHGlobal(cb);
					Marshal.StructureToPtr((object)lVITEM, intPtr, fDeleteOld: true);
					int num3 = Win32.SendMessage(listViewHandle, 4211u, i, intPtr);
					object obj = Marshal.PtrToStructure(intPtr, typeof(LVITEM));
					string text = Marshal.PtrToStringAuto(((LVITEM)obj).pszText);
					Console.WriteLine(text);
					list.Add(text);
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}
		return list;
	}
}
