using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChromFormFileSearchCtrl : UserControl
{
	public delegate void OpenFileEventHandler(string strFilePath);

	private SystemParam sysParam = SystemParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private bool m_bLoadFolderTree = false;

	private string strDirOptionInitDir = "";

	private TreeNode treeNode_0;

	private List<string> strFilenames = new List<string>(0);

	private FileSystemWatcher _watcher;

	public string strNewFileName = " ";

	private IContainer components = null;

	private SplitContainer splitContainer3;

	private Button MethodOpen;

	private TextBox tbpath;

	private RadioButton rdate;

	private RadioButton rchannel;

	private ComboBox cbbepuip;

	private Label label3;

	private TreeView tvFolder;

	private ContextMenuStrip treeMenu;

	private ToolStripMenuItem 删除ToolStripMenuItem;

	private ImageList imageList_2;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private DataGridView dataGridView1;

	private Button btnAdd;

	private Button btnClean;

	private Button btnDele;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn 序号;

	private DataGridViewTextBoxColumn 谱图;

	private Label label1;

	private TextBox tbRename;

	private Button btnRename;

	private Button btnOpensda;

	private Button btnBulkPrintf;

	public event OpenFileEventHandler OnOpenFile;

	public ChromFormFileSearchCtrl()
	{
		InitializeComponent();
		initForm();
	}

	public void initForm()
	{
	}

	public void FileListenerServer(string path)
	{
		try
		{
			_watcher = new FileSystemWatcher();
			_watcher.Path = path;
			_watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;
			_watcher.IncludeSubdirectories = true;
			_watcher.Created += FileWatcher_Created;
			_watcher.Changed += FileWatcher_Changed;
			_watcher.Deleted += FileWatcher_Deleted;
			_watcher.Renamed += FileWatcher_Renamed;
			watcherStart();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error:" + ex.Message);
		}
	}

	public void watcherStart()
	{
		_watcher.EnableRaisingEvents = true;
		Console.WriteLine("文件监控已经启动...");
	}

	public void watcherStop()
	{
		if (_watcher != null)
		{
			_watcher.EnableRaisingEvents = false;
			_watcher.Dispose();
			_watcher = null;
		}
	}

	protected void FileWatcher_Created(object sender, FileSystemEventArgs e)
	{
		try
		{
			if (strNewFileName != e.FullPath)
			{
				strNewFileName = e.FullPath;
				Console.WriteLine(string.Concat("新增:", e.ChangeType, ";", e.FullPath, ";", e.Name));
				Invoke((MethodInvoker)delegate
				{
					FileInfo fileInfo = new FileInfo(strNewFileName);
					TreeNode treeNode = tvFolder.Nodes.Add(fileInfo.Name.Replace(".sda", ""));
					treeNode.Tag = fileInfo.FullName;
					treeNode.ImageIndex = 0;
				});
			}
		}
		catch
		{
			watcherStop();
		}
	}

	protected void FileWatcher_Changed(object sender, FileSystemEventArgs e)
	{
		Console.WriteLine(string.Concat("变更:", e.ChangeType, ";", e.FullPath, ";", e.Name));
	}

	protected void FileWatcher_Deleted(object sender, FileSystemEventArgs e)
	{
		Console.WriteLine(string.Concat("删除:", e.ChangeType, ";", e.FullPath, ";", e.Name));
	}

	protected void FileWatcher_Renamed(object sender, RenamedEventArgs e)
	{
		Console.WriteLine("重命名: OldPath:{0} NewPath:{1} OldFileName{2} NewFileName:{3}", e.OldFullPath, e.FullPath, e.OldName, e.Name);
	}

	public static bool IsDesignMode()
	{
		return false;
	}

	private void ChromFormFileSearchCtrl_Load(object sender, EventArgs e)
	{
		if (!IsDesignMode())
		{
			Class49.strSdaDataFileDir = (strDirOptionInitDir = sysParam.strDirOptionInitDir);
			if (strDirOptionInitDir == "")
			{
				strDirOptionInitDir = Application.StartupPath;
			}
			tbpath.Text = strDirOptionInitDir;
			rchannel.Text = Lang.PS("按通道", "By channel");
			rdate.Text = Lang.PS("按日期", "By date");
			tabPage1.Text = Lang.PS("谱图检索", "Spectra retrieval");
			tabPage2.Text = Lang.PS("批处理", "Processing batch");
			btnOpensda.Text = Lang.PS("打开谱图", "Open");
			btnRename.Text = Lang.PS("重命名", "Rename");
			label1.Text = Lang.PS("批量重命名", "Batch rename");
			btnClean.Text = Lang.PS("清空", "Empty");
			btnAdd.Text = Lang.PS("添加", "Add");
		}
	}

	private void MethodOpen_Click(object sender, EventArgs e)
	{
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		DialogResult dialogResult = folderBrowserDialog.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			tbpath.Text = folderBrowserDialog.SelectedPath;
			strDirOptionInitDir = tbpath.Text;
			Class49.strSdaDataFileDir = tbpath.Text;
			sysParam.strDirOptionInitDir = strDirOptionInitDir;
			sysParam.SaveParam();
			InitSdaFolderTree(bool_10: true);
		}
	}

	private void cbbepuip_SelectedIndexChanged(object sender, EventArgs e)
	{
		InitSdaFolderTree(bool_10: false);
	}

	private void tvFolder_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (tvFolder.SelectedNode != null)
		{
			string text = (string)tvFolder.SelectedNode.Tag;
			text = text.ToLower();
			if (text.LastIndexOf(".sda") != -1 && this.OnOpenFile != null)
			{
				this.OnOpenFile(text);
			}
		}
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
	}

	private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void toolStripButton7_Click(object sender, EventArgs e)
	{
	}

	private void ChromFormFileSearchCtrl_VisibleChanged(object sender, EventArgs e)
	{
		if (!IsDesignMode() && !m_bLoadFolderTree)
		{
			InitSdaFolderTree(bool_10: true);
		}
	}

	private void GetAllDir(string strDirPath, ref string[] strDirList)
	{
		if (!strDirPath.Contains("System Volume Information") && !strDirPath.Contains("~") && !strDirPath.Contains("$"))
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(strDirPath);
			string[] files = Directory.GetFiles(strDirPath, "*.sda");
			Array.Resize(ref strDirList, strDirList.Length + files.Length);
			files.CopyTo(strDirList, strDirList.Length - files.Length);
			DirectoryInfo[] array = new DirectoryInfo[0];
			array = directoryInfo.GetDirectories("*");
			DirectoryInfo[] array2 = array;
			foreach (DirectoryInfo directoryInfo2 in array2)
			{
				GetAllDir(directoryInfo2.FullName, ref strDirList);
			}
			FileListenerServer(strDirPath);
		}
	}

	private FileInfoSortable[] FilterDeviceSdaFile(FileInfoSortable[] fileSortedList, string strDeviceName)
	{
		List<FileInfoSortable> list = new List<FileInfoSortable>();
		for (int i = 0; i < fileSortedList.Length; i++)
		{
			string name = fileSortedList[i].FileInfo.Name;
			if (name.IndexOf(strDeviceName) != -1)
			{
				list.Add(fileSortedList[i]);
			}
		}
		return list.ToArray();
	}

	private FileInfoSortable[] FilterDateSdaFile(FileInfoSortable[] fileSortedList, string strFilterDate)
	{
		if (strFilterDate == "")
		{
			return fileSortedList;
		}
		List<FileInfoSortable> list = new List<FileInfoSortable>();
		for (int i = 0; i < fileSortedList.Length; i++)
		{
			string text = fileSortedList[i].FileInfo.CreationTime.Date.ToString("yyyy-MM-dd");
			if (text == strFilterDate)
			{
				list.Add(fileSortedList[i]);
			}
		}
		return list.ToArray();
	}

	private FileInfoSortable[] FilterChannelSdaFile(FileInfoSortable[] fileSortedList, string strFilterChannel)
	{
		List<FileInfoSortable> list = new List<FileInfoSortable>();
		for (int i = 0; i < fileSortedList.Length; i++)
		{
			string fullName = fileSortedList[i].FileInfo.FullName;
			string directoryName = Path.GetDirectoryName(fullName);
			string text = "";
			int num = directoryName.LastIndexOf("\\");
			if (num != -1)
			{
				text = directoryName.Substring(num + 1, directoryName.Length - num - 1);
			}
			if (text == strFilterChannel)
			{
				list.Add(fileSortedList[i]);
			}
		}
		return list.ToArray();
	}

	private void SearchSdaFile(out FileInfoSortable[] fileSortedList, out string[] strDateList, out string[] strChannelList)
	{
		string[] strDirList = new string[0];
		GetAllDir(strDirOptionInitDir, ref strDirList);
		fileSortedList = new FileInfoSortable[strDirList.Length];
		int num = 0;
		string[] array = strDirList;
		int num2 = 0;
		while (num2 < array.Length)
		{
			string fileName = array[num2];
			fileSortedList[num2] = new FileInfoSortable(new FileInfo(fileName));
			num2++;
			num++;
		}
		Array.Sort(fileSortedList);
		Hashtable hashtable = new Hashtable();
		Hashtable hashtable2 = new Hashtable();
		for (int i = 0; i < fileSortedList.Length; i++)
		{
			string fullName = fileSortedList[i].FileInfo.FullName;
			string directoryName = Path.GetDirectoryName(fullName);
			string text = "";
			int num3 = directoryName.LastIndexOf("\\");
			if (num3 != -1)
			{
				text = directoryName.Substring(num3 + 1, directoryName.Length - num3 - 1);
			}
			hashtable2[text] = text;
			string text2 = fileSortedList[i].FileInfo.CreationTime.Date.ToString("yyyy-MM-dd");
			hashtable[text2] = text2;
		}
		strDateList = new string[hashtable.Count];
		strChannelList = new string[hashtable2.Count];
		IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
		for (int j = 0; j < strDateList.Length; j++)
		{
			enumerator.MoveNext();
			strDateList[j] = (string)enumerator.Value;
		}
		Array.Sort(strDateList);
		Array.Reverse(strDateList);
		IDictionaryEnumerator enumerator2 = hashtable2.GetEnumerator();
		for (int k = 0; k < strChannelList.Length; k++)
		{
			enumerator2.MoveNext();
			strChannelList[k] = (string)enumerator2.Value;
		}
		Array.Sort(strChannelList);
		Array.Reverse(strChannelList);
	}

	private void InitSdaFolderTree(bool bool_10)
	{
		FrmTip frmTip = new FrmTip();
		frmTip.Show();
		try
		{
			Application.DoEvents();
			tvFolder.Nodes.Clear();
			if (bool_10)
			{
				cbbepuip.Items.Clear();
				cbbepuip.Items.Add("");
				for (int i = 0; i < cdlMgr.Count; i++)
				{
					cbbepuip.Items.Add(cdlMgr[i].info.Name);
				}
			}
			FileInfoSortable[] fileSortedList = new FileInfoSortable[0];
			string[] strDateList = new string[0];
			string[] strChannelList = new string[0];
			SearchSdaFile(out fileSortedList, out strDateList, out strChannelList);
			fileSortedList = FilterDeviceSdaFile(fileSortedList, cbbepuip.Text.Trim());
			TreeNode treeNode = tvFolder.Nodes.Add(Lang.PS("所有文件", "All files"));
			treeNode.Tag = Lang.PS("所有文件", "All files");
			treeNode.ImageIndex = 2;
			TreeNode[] array = new TreeNode[0];
			TreeNode[] array2 = new TreeNode[0];
			if (rchannel.Checked)
			{
				Array.Resize(ref array, strChannelList.Length);
				for (int j = 0; j < strChannelList.Length; j++)
				{
					TreeNode treeNode2 = treeNode.Nodes.Add(strChannelList[j]);
					treeNode2.Tag = strChannelList[j];
					treeNode2.ImageIndex = 1;
					array[j] = treeNode2;
					FileInfoSortable[] array3 = FilterChannelSdaFile(fileSortedList, strChannelList[j]);
					for (int k = 0; k < array3.Length; k++)
					{
						TreeNode treeNode3 = treeNode2.Nodes.Add(array3[k].FileInfo.Name.Replace(".sda", ""));
						treeNode3.Tag = array3[k].FileInfo.FullName;
						treeNode3.ImageIndex = 0;
					}
				}
			}
			if (rdate.Checked)
			{
				Array.Resize(ref array, strDateList.Length);
				for (int l = 0; l < strDateList.Length; l++)
				{
					TreeNode treeNode4 = treeNode.Nodes.Add(strDateList[l]);
					treeNode4.Tag = strDateList[l];
					treeNode4.ImageIndex = 1;
					array[l] = treeNode4;
					FileInfoSortable[] array4 = FilterDateSdaFile(fileSortedList, strDateList[l]);
					for (int m = 0; m < array4.Length; m++)
					{
						TreeNode treeNode5 = treeNode4.Nodes.Add(array4[m].FileInfo.Name.Replace(".sda", ""));
						treeNode5.Tag = array4[m].FileInfo.FullName;
						treeNode5.ImageIndex = 0;
					}
				}
			}
			tvFolder.ExpandAll();
			frmTip.Close();
		}
		catch
		{
			frmTip.Close();
		}
		m_bLoadFolderTree = true;
	}

	private void method_88(TreeView treeView_0)
	{
		TreeNode treeNode = new TreeNode("我的电脑", 1, 1);
		treeView_0.Nodes.Add(treeNode);
		string[] logicalDrives = Directory.GetLogicalDrives();
		foreach (string tag in logicalDrives)
		{
			TreeNode treeNode2 = new TreeNode(tag);
			treeNode2.Tag = tag;
			treeNode2.ImageIndex = 4;
			treeNode2.SelectedImageIndex = 4;
			treeNode.Nodes.Add(treeNode2);
		}
		treeNode.ExpandAll();
	}

	private void method_89(object sender, TreeViewEventArgs e)
	{
		method_90(e.Node);
		treeNode_0 = e.Node;
		timer_0_Tick(null, null);
	}

	private void method_90(TreeNode treeNode_1)
	{
		if (treeNode_1.Nodes.Count != 0)
		{
			return;
		}
		if (treeNode_1.Parent == null)
		{
			string[] logicalDrives = Directory.GetLogicalDrives();
			foreach (string tag in logicalDrives)
			{
				TreeNode treeNode = new TreeNode(tag);
				treeNode.Tag = tag;
				treeNode.ImageIndex = 4;
				treeNode.SelectedImageIndex = 4;
				treeNode_1.Nodes.Add(treeNode);
				tvFolder.ExpandAll();
			}
			return;
		}
		string[] array = ((string)treeNode_1.Tag).Split('.');
		if (array.Length != 0 && !(array[array.Length - 1] == "sda"))
		{
			string[] directories = Directory.GetDirectories((string)treeNode_1.Tag);
			foreach (string tag2 in directories)
			{
				TreeNode treeNode2 = new TreeNode(tag2);
				treeNode2.Tag = tag2;
				treeNode2.ImageIndex = 5;
				treeNode2.SelectedImageIndex = 5;
				treeNode_1.Nodes.Add(treeNode2);
			}
		}
	}

	private void method_91(object sender, TreeViewEventArgs e)
	{
		treeNode_0 = e.Node;
	}

	private void tvFolder_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (tvFolder.SelectedNode == null)
		{
			return;
		}
		string text = (string)tvFolder.SelectedNode.Tag;
		text = text.ToLower();
		string[] array = text.Split('.');
		if (array.Length != 0 && !(array[array.Length - 1] == "sda"))
		{
			return;
		}
		try
		{
			if (ChromFormCtrl.form != null)
			{
				ChromFormCtrl.form.Show();
				ChromFormCtrl.form.OpenChrom(text, sampling: false, useCurrent: true);
			}
		}
		catch
		{
		}
	}

	private void rdate_CheckedChanged(object sender, EventArgs e)
	{
		InitSdaFolderTree(bool_10: true);
	}

	private void rchannel_CheckedChanged(object sender, EventArgs e)
	{
		InitSdaFolderTree(bool_10: true);
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		MyOfdChrom myOfdChrom = new MyOfdChrom();
		myOfdChrom.Multiselect = true;
		if (myOfdChrom.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}
		for (int i = 0; i < myOfdChrom.FileNames.Length; i++)
		{
			int num = 0;
			for (num = 0; num < strFilenames.Count; num++)
			{
				if (strFilenames[num] == myOfdChrom.FileNames[i].Trim())
				{
					MessageBox.Show(Lang.PS("已添加谱图!", "Chrom. added!") + "\n" + Path.GetFileName(myOfdChrom.FileNames[i]));
					break;
				}
			}
			if (num == strFilenames.Count)
			{
				strFilenames.Add(myOfdChrom.FileNames[i].Trim());
				dataGridView1.Rows.Add(strFilenames.Count.ToString(), Path.GetFileName(myOfdChrom.FileNames[i].Trim()));
			}
		}
	}

	private void btnDele_Click(object sender, EventArgs e)
	{
	}

	private void btnClean_Click(object sender, EventArgs e)
	{
		strFilenames.Clear();
		dataGridView1.Rows.Clear();
	}

	private void btnRename_Click(object sender, EventArgs e)
	{
		dataGridView1.Rows.Clear();
		for (int i = 0; i < strFilenames.Count; i++)
		{
			FileInfo fileInfo = new FileInfo(strFilenames[i]);
			fileInfo.MoveTo(tbRename.Text.Trim() + (i + 1) + ".sda");
			strFilenames[i] = tbRename.Text.Trim() + (i + 1) + ".sda";
			dataGridView1.Rows.Add((i + 1).ToString(), Path.GetFileName(strFilenames[i]));
		}
	}

	private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (dataGridView1.SelectedRows != null)
		{
			string chromName = strFilenames[dataGridView1.SelectedRows[0].Index];
			cdlMgr.formMain.chromFormCtrl.OpenChrom(chromName, sampling: true, useCurrent: true);
		}
	}

	private void btnOpensda_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows != null)
		{
			for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
			{
				string chromName = strFilenames[dataGridView1.SelectedRows[i].Index];
				cdlMgr.formMain.chromFormCtrl.OpenChrom(chromName, sampling: true, useCurrent: true);
			}
		}
	}

	private void btnBulkPrintf_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows != null)
		{
			for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
			{
				string chromName = strFilenames[dataGridView1.SelectedRows[i].Index];
				cdlMgr.formMain.chromFormCtrl.OpenChrom(chromName, sampling: true, useCurrent: true);
				cdlMgr.formMain.chromFormCtrl.printDirect();
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ChromFormFileSearchCtrl));
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.MethodOpen = new System.Windows.Forms.Button();
		this.tbpath = new System.Windows.Forms.TextBox();
		this.rdate = new System.Windows.Forms.RadioButton();
		this.rchannel = new System.Windows.Forms.RadioButton();
		this.cbbepuip = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.tvFolder = new System.Windows.Forms.TreeView();
		this.imageList_2 = new System.Windows.Forms.ImageList(this.components);
		this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.btnOpensda = new System.Windows.Forms.Button();
		this.btnRename = new System.Windows.Forms.Button();
		this.tbRename = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnClean = new System.Windows.Forms.Button();
		this.btnDele = new System.Windows.Forms.Button();
		this.btnAdd = new System.Windows.Forms.Button();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.谱图 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnBulkPrintf = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
		this.splitContainer3.Panel1.SuspendLayout();
		this.splitContainer3.Panel2.SuspendLayout();
		this.splitContainer3.SuspendLayout();
		this.treeMenu.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer3.IsSplitterFixed = true;
		this.splitContainer3.Location = new System.Drawing.Point(3, 3);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer3.Panel1.Controls.Add(this.MethodOpen);
		this.splitContainer3.Panel1.Controls.Add(this.tbpath);
		this.splitContainer3.Panel1.Controls.Add(this.rdate);
		this.splitContainer3.Panel1.Controls.Add(this.rchannel);
		this.splitContainer3.Panel1.Controls.Add(this.cbbepuip);
		this.splitContainer3.Panel1.Controls.Add(this.label3);
		this.splitContainer3.Panel2.Controls.Add(this.tvFolder);
		this.splitContainer3.Size = new System.Drawing.Size(346, 478);
		this.splitContainer3.SplitterDistance = 93;
		this.splitContainer3.TabIndex = 2;
		this.MethodOpen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.MethodOpen.Image = (System.Drawing.Image)resources.GetObject("MethodOpen.Image");
		this.MethodOpen.Location = new System.Drawing.Point(309, 6);
		this.MethodOpen.Name = "MethodOpen";
		this.MethodOpen.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen.TabIndex = 30;
		this.MethodOpen.UseVisualStyleBackColor = true;
		this.MethodOpen.Click += new System.EventHandler(MethodOpen_Click);
		this.tbpath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbpath.Location = new System.Drawing.Point(11, 6);
		this.tbpath.Multiline = true;
		this.tbpath.Name = "tbpath";
		this.tbpath.Size = new System.Drawing.Size(292, 32);
		this.tbpath.TabIndex = 3;
		this.rdate.AutoSize = true;
		this.rdate.Location = new System.Drawing.Point(79, 72);
		this.rdate.Name = "rdate";
		this.rdate.Size = new System.Drawing.Size(59, 16);
		this.rdate.TabIndex = 2;
		this.rdate.Text = "按日期";
		this.rdate.UseVisualStyleBackColor = true;
		this.rdate.CheckedChanged += new System.EventHandler(rdate_CheckedChanged);
		this.rchannel.AutoSize = true;
		this.rchannel.Checked = true;
		this.rchannel.Location = new System.Drawing.Point(14, 72);
		this.rchannel.Name = "rchannel";
		this.rchannel.Size = new System.Drawing.Size(59, 16);
		this.rchannel.TabIndex = 2;
		this.rchannel.TabStop = true;
		this.rchannel.Text = "按通道";
		this.rchannel.UseVisualStyleBackColor = true;
		this.rchannel.CheckedChanged += new System.EventHandler(rchannel_CheckedChanged);
		this.cbbepuip.FormattingEnabled = true;
		this.cbbepuip.Location = new System.Drawing.Point(61, 44);
		this.cbbepuip.Name = "cbbepuip";
		this.cbbepuip.Size = new System.Drawing.Size(89, 20);
		this.cbbepuip.TabIndex = 1;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(47, 12);
		this.label3.TabIndex = 0;
		this.label3.Text = "色谱仪:";
		this.tvFolder.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tvFolder.ImageIndex = 0;
		this.tvFolder.ImageList = this.imageList_2;
		this.tvFolder.Location = new System.Drawing.Point(0, 0);
		this.tvFolder.Name = "tvFolder";
		this.tvFolder.SelectedImageIndex = 0;
		this.tvFolder.Size = new System.Drawing.Size(346, 381);
		this.tvFolder.TabIndex = 0;
		this.tvFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(tvFolder_MouseDoubleClick);
		this.imageList_2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_2.ImageStream");
		this.imageList_2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_2.Images.SetKeyName(0, "cc.ico");
		this.imageList_2.Images.SetKeyName(1, "colors.ico");
		this.imageList_2.Images.SetKeyName(2, "package.ico");
		this.imageList_2.Images.SetKeyName(3, "clock.ico");
		this.imageList_2.Images.SetKeyName(4, "4.ico");
		this.imageList_2.Images.SetKeyName(5, "5.ico");
		this.imageList_2.Images.SetKeyName(6, "6.ico");
		this.imageList_2.Images.SetKeyName(7, "7.ico");
		this.imageList_2.Images.SetKeyName(8, "8.ico");
		this.imageList_2.Images.SetKeyName(9, "9.ico");
		this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.删除ToolStripMenuItem });
		this.treeMenu.Name = "treeMenu";
		this.treeMenu.Size = new System.Drawing.Size(153, 26);
		this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
		this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.删除ToolStripMenuItem.Text = "——删除——";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(360, 510);
		this.tabControl1.TabIndex = 3;
		this.tabPage1.Controls.Add(this.splitContainer3);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(352, 484);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "谱图检索";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.btnBulkPrintf);
		this.tabPage2.Controls.Add(this.btnOpensda);
		this.tabPage2.Controls.Add(this.btnRename);
		this.tabPage2.Controls.Add(this.tbRename);
		this.tabPage2.Controls.Add(this.label1);
		this.tabPage2.Controls.Add(this.btnClean);
		this.tabPage2.Controls.Add(this.btnDele);
		this.tabPage2.Controls.Add(this.btnAdd);
		this.tabPage2.Controls.Add(this.dataGridView1);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(352, 484);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "批处理";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.btnOpensda.Location = new System.Drawing.Point(8, 321);
		this.btnOpensda.Name = "btnOpensda";
		this.btnOpensda.Size = new System.Drawing.Size(154, 23);
		this.btnOpensda.TabIndex = 7;
		this.btnOpensda.Text = "打开谱图";
		this.btnOpensda.UseVisualStyleBackColor = true;
		this.btnOpensda.Click += new System.EventHandler(btnOpensda_Click);
		this.btnRename.Location = new System.Drawing.Point(249, 348);
		this.btnRename.Name = "btnRename";
		this.btnRename.Size = new System.Drawing.Size(75, 23);
		this.btnRename.TabIndex = 6;
		this.btnRename.Text = "重命名";
		this.btnRename.UseVisualStyleBackColor = true;
		this.btnRename.Click += new System.EventHandler(btnRename_Click);
		this.tbRename.Location = new System.Drawing.Point(83, 350);
		this.tbRename.Name = "tbRename";
		this.tbRename.Size = new System.Drawing.Size(160, 21);
		this.tbRename.TabIndex = 5;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 354);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "批量重命名:";
		this.btnClean.Location = new System.Drawing.Point(168, 292);
		this.btnClean.Name = "btnClean";
		this.btnClean.Size = new System.Drawing.Size(75, 23);
		this.btnClean.TabIndex = 3;
		this.btnClean.Text = "清空";
		this.btnClean.UseVisualStyleBackColor = true;
		this.btnClean.Click += new System.EventHandler(btnClean_Click);
		this.btnDele.Location = new System.Drawing.Point(87, 292);
		this.btnDele.Name = "btnDele";
		this.btnDele.Size = new System.Drawing.Size(75, 23);
		this.btnDele.TabIndex = 2;
		this.btnDele.Text = "删除";
		this.btnDele.UseVisualStyleBackColor = true;
		this.btnDele.Visible = false;
		this.btnDele.Click += new System.EventHandler(btnDele_Click);
		this.btnAdd.Location = new System.Drawing.Point(6, 292);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(75, 23);
		this.btnAdd.TabIndex = 1;
		this.btnAdd.Text = "添加";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.序号, this.谱图);
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(346, 258);
		this.dataGridView1.TabIndex = 0;
		this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseDoubleClick);
		this.序号.HeaderText = "序号";
		this.序号.Name = "序号";
		this.序号.ReadOnly = true;
		this.序号.Width = 40;
		this.谱图.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.谱图.HeaderText = "谱图";
		this.谱图.Name = "谱图";
		this.谱图.ReadOnly = true;
		this.谱图.Width = 54;
		this.dataGridViewTextBoxColumn1.HeaderText = "序号";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Width = 40;
		this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.dataGridViewTextBoxColumn2.HeaderText = "谱图";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.btnBulkPrintf.Location = new System.Drawing.Point(170, 321);
		this.btnBulkPrintf.Name = "btnBulkPrintf";
		this.btnBulkPrintf.Size = new System.Drawing.Size(154, 23);
		this.btnBulkPrintf.TabIndex = 8;
		this.btnBulkPrintf.Text = "打印选中谱图";
		this.btnBulkPrintf.UseVisualStyleBackColor = true;
		this.btnBulkPrintf.Click += new System.EventHandler(btnBulkPrintf_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tabControl1);
		base.Name = "ChromFormFileSearchCtrl";
		base.Size = new System.Drawing.Size(360, 510);
		base.Load += new System.EventHandler(ChromFormFileSearchCtrl_Load);
		base.VisibleChanged += new System.EventHandler(ChromFormFileSearchCtrl_VisibleChanged);
		this.splitContainer3.Panel1.ResumeLayout(false);
		this.splitContainer3.Panel1.PerformLayout();
		this.splitContainer3.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
		this.splitContainer3.ResumeLayout(false);
		this.treeMenu.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
	}
}
