using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[DefaultEvent("FileSelecting")]
public class OpenFileDialogEx : UserControl
{
	private class OpenFileDialogHostForm : Form
	{
		private OpenFileDialogEx m_dialogEx;

		private OpenFileDialog m_dialog = null;

		private DialogNativeWindow m_nativeWindow;

		public OpenFileDialogHostForm(OpenFileDialogEx dialogEx, OpenFileDialog dialog)
		{
			m_dialogEx = dialogEx;
			m_dialog = dialog;
			base.StartPosition = FormStartPosition.Manual;
			base.Location = new Point(-1000, -1000);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (m_nativeWindow != null)
			{
				m_nativeWindow.Dispose();
			}
			base.OnClosing(e);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 6)
			{
				bool flag = true;
				if (Application.OpenForms != null && Application.OpenForms.Count > 0)
				{
					foreach (Form openForm in Application.OpenForms)
					{
						if (m.LParam == openForm.Handle && openForm.Handle != base.Handle)
						{
							flag = false;
						}
					}
				}
				if (m_nativeWindow == null && flag)
				{
					m_nativeWindow = new DialogNativeWindow(m_dialogEx, m.LParam, m_dialog);
				}
			}
			base.WndProc(ref m);
		}
	}

	private class DialogNativeWindow : NativeWindow, IDisposable
	{
		private OpenFileDialogEx m_dialogEx;

		private OpenFileDialog m_dialog;

		private ChildControlNativeWindow m_childNative;

		private ChildControlNativeWindow m_SelectButton;

		private ChildControlNativeWindow m_ListView;

		private ChildControlNativeWindow m_SelectText;

		private bool m_isDisposed;

		private bool mInitializated = false;

		private IntPtr mOpenDialogHandle;

		private RECT mOpenDialogWindowRect = default(RECT);

		private RECT mOpenDialogClientRect = default(RECT);

		private Size mOriginalSize;

		private SetWindowPosFlags UFLAGSSIZE = (SetWindowPosFlags)530;

		private SetWindowPosFlags UFLAGSHIDE = (SetWindowPosFlags)659;

		private SetWindowPosFlags UFLAGSZORDER = (SetWindowPosFlags)19;

		public bool IsDisposed => m_isDisposed;

		public DialogNativeWindow(OpenFileDialogEx dialogEx, IntPtr handle, OpenFileDialog dialog)
		{
			mOpenDialogHandle = handle;
			m_dialogEx = dialogEx;
			m_dialog = dialog;
			AssignHandle(handle);
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
			case 24:
				InitChildNative();
				mInitializated = true;
				break;
			case 70:
			{
				if (m_isDisposed)
				{
					break;
				}
				if (!mInitializated)
				{
					WINDOWPOS wINDOWPOS = (WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
					if (m_dialogEx.StartLocation == AddonWindowLocation.Right && wINDOWPOS.flags != 0 && (wINDOWPOS.flags & 1) != 1)
					{
						mOriginalSize = new Size(wINDOWPOS.cx, wINDOWPOS.cy);
						wINDOWPOS.cx += m_dialogEx.Width;
						Marshal.StructureToPtr((object)wINDOWPOS, m.LParam, fDeleteOld: true);
						mInitializated = true;
					}
					if (m_dialogEx.StartLocation == AddonWindowLocation.Bottom && wINDOWPOS.flags != 0 && (wINDOWPOS.flags & 1) != 1)
					{
						mOriginalSize = new Size(wINDOWPOS.cx, wINDOWPOS.cy);
						wINDOWPOS.cy += m_dialogEx.Height;
						Marshal.StructureToPtr((object)wINDOWPOS, m.LParam, fDeleteOld: true);
						mInitializated = true;
					}
				}
				RECT rect = default(RECT);
				Win32.GetClientRect(mOpenDialogHandle, ref rect);
				switch (m_dialogEx.StartLocation)
				{
				case AddonWindowLocation.Right:
					m_dialogEx.Height = (int)rect.Height;
					m_dialogEx.Location = new Point((int)(rect.Width - m_dialogEx.Width), 0);
					break;
				case AddonWindowLocation.Bottom:
					m_dialogEx.Width = (int)rect.Width;
					m_dialogEx.Location = new Point(0, (int)(rect.Height - m_dialogEx.Height));
					break;
				case AddonWindowLocation.None:
					m_dialogEx.Width = (int)rect.Width;
					m_dialogEx.Height = (int)rect.Height;
					break;
				}
				break;
			}
			}
			base.WndProc(ref m);
		}

		private void InitChildNative()
		{
			Win32.EnumChildWindows(base.Handle, delegate(IntPtr handle, int lparam)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				Win32.GetClassName(handle, stringBuilder, stringBuilder.Capacity);
				StringBuilder stringBuilder2 = new StringBuilder(256);
				Win32.GetWindowText(handle, stringBuilder2, stringBuilder2.Capacity);
				if (stringBuilder.ToString().StartsWith("#32770"))
				{
					m_childNative = new ChildControlNativeWindow(handle, m_dialogEx);
					m_childNative.SelectFileChanged += childNative_SelectFileChanged;
					m_childNative.SelectPathChanged += childNative_SelectPathChanged;
					m_childNative.SelectPathSelectedFiles += m_childNative_SelectPathSelectedFiles;
					m_childNative.m_ListView = m_ListView;
					return true;
				}
				if (stringBuilder.ToString().StartsWith("Button") && stringBuilder2.ToString().Contains("(&O)"))
				{
					int dlgCtrlID = Win32.GetDlgCtrlID(handle);
					Win32.SetDlgItemText(Win32.GetParent(handle), dlgCtrlID, "选择(&O)");
					m_SelectButton = new ChildControlNativeWindow(handle, m_dialogEx);
					m_SelectButton.m_ListView = m_ListView;
					m_SelectButton.m_SelectText = m_SelectText;
					return true;
				}
				if (stringBuilder.ToString().StartsWith("SysListView32"))
				{
					m_ListView = new ChildControlNativeWindow(handle, m_dialogEx);
					m_ListView.m_ListView = m_ListView;
					return true;
				}
				if (stringBuilder.ToString().StartsWith("Edit"))
				{
					m_SelectText = new ChildControlNativeWindow(handle, m_dialogEx);
					if (m_SelectButton != null)
					{
						m_SelectButton.m_SelectText = m_SelectText;
					}
					return true;
				}
				return true;
			}, 0);
			Win32.GetClientRect(mOpenDialogHandle, ref mOpenDialogClientRect);
			Win32.GetWindowRect(mOpenDialogHandle, ref mOpenDialogWindowRect);
			PopulateWindowsHandlers();
			switch (m_dialogEx.StartLocation)
			{
			case AddonWindowLocation.Right:
				m_dialogEx.Location = new Point((int)(mOpenDialogClientRect.Width - m_dialogEx.Width), 0);
				Win32.SetParent(m_dialogEx.Handle, mOpenDialogHandle);
				Win32.SetWindowPos(m_dialogEx.Handle, (IntPtr)(-1), 0, 0, 0, 0, UFLAGSZORDER);
				break;
			case AddonWindowLocation.Bottom:
				m_dialogEx.Location = new Point(0, (int)(mOpenDialogClientRect.Height - m_dialogEx.Height));
				Win32.SetParent(m_dialogEx.Handle, mOpenDialogHandle);
				Win32.SetWindowPos(m_dialogEx.Handle, (IntPtr)1, 0, 0, 0, 0, UFLAGSZORDER);
				break;
			case AddonWindowLocation.None:
				Win32.SetParent(m_dialogEx.Handle, mOpenDialogHandle);
				Win32.SetWindowPos(m_dialogEx.Handle, (IntPtr)1, 0, 0, 0, 0, UFLAGSZORDER);
				break;
			}
		}

		private void PopulateWindowsHandlers()
		{
			Win32.EnumChildWindows(mOpenDialogHandle, OpenFileDialogEnumWindowCallBack, 0);
		}

		private bool OpenFileDialogEnumWindowCallBack(IntPtr hwnd, int lParam)
		{
			return true;
		}

		public void Dispose()
		{
			ReleaseHandle();
			if (m_childNative != null)
			{
				m_childNative.SelectFileChanged -= childNative_SelectFileChanged;
				m_childNative.SelectPathChanged -= childNative_SelectPathChanged;
				m_childNative.SelectPathSelectedFiles -= m_childNative_SelectPathSelectedFiles;
				m_childNative.Dispose();
			}
			m_isDisposed = true;
		}

		private void UpdateWindowsSize()
		{
			Win32.GetWindowRect(mOpenDialogHandle, ref mOpenDialogWindowRect);
			switch (m_dialogEx.StartLocation)
			{
			case AddonWindowLocation.Right:
				lastSize = new Size((int)mOpenDialogWindowRect.Width - m_dialogEx.Width, (int)mOpenDialogWindowRect.Height);
				break;
			case AddonWindowLocation.Bottom:
				lastSize = new Size((int)mOpenDialogWindowRect.Width, (int)mOpenDialogWindowRect.Height - m_dialogEx.Height);
				break;
			case AddonWindowLocation.None:
				break;
			}
		}

		private void childNative_SelectPathChanged(string path)
		{
			m_dialogEx.OnPathOpened(path);
		}

		private void childNative_SelectFileChanged(string fileName)
		{
			LogMgr.Instance.Write2RunLog("childNative_SelectFileChanged 610" + fileName);
			m_dialogEx.OnFileSelecting(fileName);
		}

		private void m_childNative_SelectPathSelectedFiles(List<string> lstSelectedItems)
		{
			m_dialogEx.OnItemSelected(lstSelectedItems);
		}
	}

	private class ChildControlNativeWindow : NativeWindow, IDisposable
	{
		public delegate void SelectFileChangedEventHandler(string fileName);

		public delegate void SelectPathChangedEventHandler(string path);

		public delegate void SelectPathSelectedFilesEventHandler(List<string> lstSelectedItems);

		public OpenFileDialogEx m_dialogEx;

		public ChildControlNativeWindow m_ListView { get; set; }

		public ChildControlNativeWindow m_SelectText { get; set; }

		public event SelectFileChangedEventHandler SelectFileChanged;

		public event SelectPathChangedEventHandler SelectPathChanged;

		public event SelectPathSelectedFilesEventHandler SelectPathSelectedFiles;

		public ChildControlNativeWindow(IntPtr handle, OpenFileDialogEx dialogEx)
		{
			m_dialogEx = dialogEx;
			AssignHandle(handle);
		}

		protected override void WndProc(ref Message m)
		{
			try
			{
				switch (m.Msg)
				{
				case 78:
				{
					OFNOTIFY oFNOTIFY = (OFNOTIFY)Marshal.PtrToStructure(m.LParam, typeof(OFNOTIFY));
					if (oFNOTIFY.hdr.code == 4294966694u)
					{
						StringBuilder stringBuilder3 = new StringBuilder(2560);
						Win32.SendMessage(Win32.GetParent(base.Handle), 1125, 2560, stringBuilder3);
						string text = stringBuilder3.ToString();
						if (this.SelectFileChanged != null)
						{
							this.SelectFileChanged(stringBuilder3.ToString());
						}
						if (m_ListView == null)
						{
							break;
						}
						int num = ListV.ListView_GetItemCount(m_ListView.Handle);
						if (num > 0)
						{
							List<string> lstSelectedItems = ListV.ListViewGetItem(m_ListView.Handle);
							if (this.SelectPathSelectedFiles != null)
							{
								this.SelectPathSelectedFiles(lstSelectedItems);
							}
						}
					}
					else
					{
						if (oFNOTIFY.hdr.code != 4294966693u)
						{
							break;
						}
						Win32.EnumChildWindows(Win32.GetParent(base.Handle), delegate(IntPtr handle, int lparam)
						{
							StringBuilder stringBuilder5 = new StringBuilder(256);
							Win32.GetClassName(handle, stringBuilder5, stringBuilder5.Capacity);
							if (stringBuilder5.ToString().StartsWith("SysListView32"))
							{
								m_ListView = new ChildControlNativeWindow(handle, m_dialogEx);
								return true;
							}
							return true;
						}, 0);
						StringBuilder stringBuilder4 = new StringBuilder(256);
						Win32.SendMessage(Win32.GetParent(base.Handle), 1126, 256, stringBuilder4);
						if (this.SelectPathChanged != null)
						{
							this.SelectPathChanged(stringBuilder4.ToString());
						}
					}
					break;
				}
				case 513:
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					Win32.GetWindowText(base.Handle, stringBuilder, stringBuilder.Capacity);
					if (stringBuilder.ToString().Contains("(&O)") && m_SelectText != null)
					{
						StringBuilder stringBuilder2 = new StringBuilder(256);
						Win32.GetWindowText(m_SelectText.Handle, stringBuilder2, stringBuilder2.Capacity);
						if (m_dialogEx.AllowFolderSelect && stringBuilder2.ToString() != "")
						{
							Win32.EndDialog(Win32.GetParent(base.Handle), 1);
						}
						else if (File.Exists(stringBuilder2.ToString()))
						{
							Win32.EndDialog(Win32.GetParent(base.Handle), 1);
						}
					}
					break;
				}
				}
				base.WndProc(ref m);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				MessageBox.Show(message);
			}
		}

		public void Dispose()
		{
			ReleaseHandle();
		}
	}

	private string m_fileName = string.Empty;

	private string m_filter = string.Empty;

	private AddonWindowLocation mStartLocation = AddonWindowLocation.Right;

	public static Size lastSize = new Size(0, 0);

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string FileName
	{
		get
		{
			return m_fileName;
		}
		set
		{
			m_fileName = value ?? string.Empty;
		}
	}

	public string Title { get; set; }

	[Description("文件筛选条件。")]
	public string Filter
	{
		get
		{
			return m_filter;
		}
		set
		{
			m_filter = value ?? string.Empty;
		}
	}

	public int FilterIndex { get; set; }

	public bool Multiselect { get; set; }

	[DefaultValue(AddonWindowLocation.Right)]
	public AddonWindowLocation StartLocation
	{
		get
		{
			return mStartLocation;
		}
		set
		{
			mStartLocation = value;
		}
	}

	public string InitialDirectory { get; set; }

	public string[] FileNames { get; set; }

	public string SelectPath { get; set; }

	[DefaultValue(false)]
	public bool AllowFolderSelect { get; set; }

	public DialogResult ShowDialog()
	{
		return ShowDialog(null);
	}

	public DialogResult ShowDialog(IWin32Window owner)
	{
		FileNames = new List<string>().ToArray();
		using OpenFileDialog openFileDialog = new OpenFileDialog
		{
			Title = Title,
			Filter = m_filter,
			FilterIndex = FilterIndex,
			Multiselect = Multiselect,
			InitialDirectory = InitialDirectory
		};
		SelectPath = openFileDialog.InitialDirectory;
		openFileDialog.AutoUpgradeEnabled = false;
		OpenFileDialogHostForm openFileDialogHostForm = new OpenFileDialogHostForm(this, openFileDialog);
		if (owner != null)
		{
			openFileDialogHostForm.Show(owner);
		}
		else
		{
			openFileDialogHostForm.Show(Application.OpenForms[0]);
		}
		Win32.SetWindowPos(openFileDialogHostForm.Handle, IntPtr.Zero, 0, 0, 0, 0, (SetWindowPosFlags)659);
		DialogResult dialogResult = openFileDialog.ShowDialog(openFileDialogHostForm);
		if (dialogResult == DialogResult.OK && openFileDialog.FileName != "")
		{
			m_fileName = openFileDialog.FileName;
			FileNames = openFileDialog.FileNames;
		}
		openFileDialogHostForm.Close();
		openFileDialogHostForm.Dispose();
		return dialogResult;
	}

	public virtual void OnFileSelecting(string fileName)
	{
		SelectPath = Path.GetDirectoryName(fileName);
	}

	public virtual void OnItemSelected(List<string> selectedItems)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < selectedItems.Count; i++)
		{
			list.Add(SelectPath + "\\" + selectedItems[i]);
		}
		FileNames = list.ToArray();
	}

	public virtual void OnPathOpened(string path)
	{
		SelectPath = path;
	}

	public OpenFileDialogEx()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.Name = "OpenFileDialogEx";
		base.Size = new System.Drawing.Size(185, 159);
		base.ResumeLayout(false);
	}
}
