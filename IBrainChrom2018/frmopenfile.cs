using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class frmopenfile : Form
{
	public enum Type
	{
		本地磁盘,
		Directory,
		File
	}

	public class Tab
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private Type type_0;

		public string Path { get; set; }

		public Type FType { get; set; }
	}

	private SystemParam sysParam = SystemParam.Create();

	private IContainer icontainer_0;

	private SplitContainer splitContainer1;

	private SplitContainer splitContainer2;

	private GroupBox groupBox1;

	private TreeView treeView1;

	private ListView listView1;

	private ComboBox comboBox2;

	private Label label2;

	private ComboBox cbbepuip;

	private Label label1;

	private Label label3;

	private Label label4;

	private DateTimePicker dateTimePicker2;

	private DateTimePicker dateTimePicker1;

	private Label label5;

	private Button button1;

	private ImageList imageList_0;

	private Label label6;

	private TextBox textBox2;

	private ColumnHeader columnHeader_0;

	private ColumnHeader columnHeader_1;

	private ColumnHeader columnHeader_2;

	private ColumnHeader columnHeader_3;

	private ColumnHeader columnHeader_4;

	private ColumnHeader columnHeader_5;

	private ColumnHeader columnHeader_6;

	private Button button2;

	private ColumnHeader columnHeader_7;

	private ToolTip toolTip_0;

	private ComboBox textBox1;

	private CheckBox checkBox1;

	public ChromFormCtrl ChromFrm;

	private int int_0;

	[CompilerGenerated]
	private static Comparison<FileInfo> comparison_0;

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.icontainer_0 = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.frmopenfile));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.textBox1 = new System.Windows.Forms.ComboBox();
		this.button2 = new System.Windows.Forms.Button();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.comboBox2 = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cbbepuip = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.icontainer_0);
		this.listView1 = new System.Windows.Forms.ListView();
		this.columnHeader_0 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_1 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_2 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_5 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_7 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_3 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_4 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_6 = new System.Windows.Forms.ColumnHeader();
		this.toolTip_0 = new System.Windows.Forms.ToolTip(this.icontainer_0);
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
		this.splitContainer1.Size = new System.Drawing.Size(843, 445);
		this.splitContainer1.SplitterDistance = 113;
		this.splitContainer1.TabIndex = 0;
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.button2);
		this.groupBox1.Controls.Add(this.textBox2);
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dateTimePicker2);
		this.groupBox1.Controls.Add(this.dateTimePicker1);
		this.groupBox1.Controls.Add(this.comboBox2);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cbbepuip);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(843, 113);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "查询条件";
		this.textBox1.FormattingEnabled = true;
		this.textBox1.Location = new System.Drawing.Point(471, 13);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(236, 20);
		this.textBox1.TabIndex = 8;
		this.button2.Location = new System.Drawing.Point(591, 41);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(118, 23);
		this.button2.TabIndex = 7;
		this.button2.Text = "查找当前目录";
		this.toolTip_0.SetToolTip(this.button2, "查找当前文件夹下谱图文件");
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.textBox2.Enabled = false;
		this.textBox2.Location = new System.Drawing.Point(112, 78);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(595, 21);
		this.textBox2.TabIndex = 6;
		this.button1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.button1.ForeColor = System.Drawing.Color.Red;
		this.button1.Location = new System.Drawing.Point(419, 41);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(166, 23);
		this.button1.TabIndex = 5;
		this.button1.Text = "查询所有目录(谨慎使用)";
		this.toolTip_0.SetToolTip(this.button1, "查找本文件夹下包含子文件夹内所有文件，谨慎使用。");
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(255, 48);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(11, 12);
		this.label5.TabIndex = 4;
		this.label5.Text = "~";
		this.dateTimePicker2.Location = new System.Drawing.Point(287, 42);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.Size = new System.Drawing.Size(121, 21);
		this.dateTimePicker2.TabIndex = 3;
		this.dateTimePicker1.Location = new System.Drawing.Point(112, 42);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(121, 21);
		this.dateTimePicker1.TabIndex = 3;
		this.comboBox2.FormattingEnabled = true;
		this.comboBox2.Items.AddRange(new object[11]
		{
			"FID1", "FID2", "TCD1", "TCD2", "FPD1", "FPD2", "ECD1", "ECD2", "NPD1", "NPD2",
			"AUX"
		});
		this.comboBox2.Location = new System.Drawing.Point(287, 14);
		this.comboBox2.Name = "comboBox2";
		this.comboBox2.Size = new System.Drawing.Size(121, 20);
		this.comboBox2.TabIndex = 1;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(417, 17);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(47, 12);
		this.label3.TabIndex = 0;
		this.label3.Text = "关键字:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(246, 17);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "通道:";
		this.cbbepuip.FormattingEnabled = true;
		this.cbbepuip.Location = new System.Drawing.Point(112, 14);
		this.cbbepuip.Name = "cbbepuip";
		this.cbbepuip.Size = new System.Drawing.Size(121, 20);
		this.cbbepuip.TabIndex = 1;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 45);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(83, 12);
		this.label4.TabIndex = 0;
		this.label4.Text = "创建起止日期:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 77);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(59, 12);
		this.label6.TabIndex = 0;
		this.label6.Text = "当前路径:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "色谱机名称:";
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Panel1.Controls.Add(this.treeView1);
		this.splitContainer2.Panel2.Controls.Add(this.listView1);
		this.splitContainer2.Size = new System.Drawing.Size(843, 328);
		this.splitContainer2.SplitterDistance = 228;
		this.splitContainer2.TabIndex = 0;
		this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeView1.ImageIndex = 0;
		this.treeView1.ImageList = this.imageList_0;
		this.treeView1.Location = new System.Drawing.Point(0, 0);
		this.treeView1.Name = "treeView1";
		this.treeView1.SelectedImageIndex = 0;
		this.treeView1.Size = new System.Drawing.Size(228, 328);
		this.treeView1.TabIndex = 0;
		this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "doc.bmp");
		this.imageList_0.Images.SetKeyName(1, "ppt.bmp");
		this.imageList_0.Images.SetKeyName(2, "txt.bmp");
		this.imageList_0.Images.SetKeyName(3, "磁盘.bmp");
		this.imageList_0.Images.SetKeyName(4, "打开后的文件.bmp");
		this.imageList_0.Images.SetKeyName(5, "大文件夹.bmp");
		this.imageList_0.Images.SetKeyName(6, "回收站.bmp");
		this.imageList_0.Images.SetKeyName(7, "记事本.bmp");
		this.imageList_0.Images.SetKeyName(8, "网上邻居.bmp");
		this.imageList_0.Images.SetKeyName(9, "文件夹.bmp");
		this.imageList_0.Images.SetKeyName(10, "我的电脑.bmp");
		this.imageList_0.Images.SetKeyName(11, "桌面.bmp");
		this.imageList_0.Images.SetKeyName(12, "cc.ico");
		this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[8] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_5, this.columnHeader_7, this.columnHeader_3, this.columnHeader_4, this.columnHeader_6 });
		this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listView1.FullRowSelect = true;
		this.listView1.LargeImageList = this.imageList_0;
		this.listView1.Location = new System.Drawing.Point(0, 0);
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(611, 328);
		this.listView1.TabIndex = 0;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.View = System.Windows.Forms.View.Details;
		this.listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
		this.columnHeader_0.Text = "序号";
		this.columnHeader_0.Width = 44;
		this.columnHeader_1.Text = "色谱仪名称";
		this.columnHeader_1.Width = 89;
		this.columnHeader_2.Text = "通道类型";
		this.columnHeader_2.Width = 69;
		this.columnHeader_5.Text = "进样序号";
		this.columnHeader_7.Text = "自定义名称";
		this.columnHeader_7.Width = 87;
		this.columnHeader_3.Text = "进样日期";
		this.columnHeader_3.Width = 110;
		this.columnHeader_4.Text = "进样时间";
		this.columnHeader_4.Width = 76;
		this.columnHeader_6.Text = "文件名称";
		this.columnHeader_6.Width = 125;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(714, 14);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(108, 16);
		this.checkBox1.TabIndex = 9;
		this.checkBox1.Text = "使用自定义历史";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(843, 445);
		base.Controls.Add(this.splitContainer1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmopenfile";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "打开文件";
		base.TopMost = true;
		base.Load += new System.EventHandler(frmopenfile_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmopenfile_FormClosing);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public frmopenfile()
	{
		InitializeComponent();
	}

	private void frmopenfile_Load(object sender, EventArgs e)
	{
		dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
		TreeNode treeNode = treeView1.Nodes.Add(Lang.PS("我的电脑", "My PC"));
		treeNode.ImageIndex = 10;
		treeNode.SelectedImageIndex = 10;
		DriveInfo[] drives = DriveInfo.GetDrives();
		DriveInfo[] array = drives;
		foreach (DriveInfo driveInfo in array)
		{
			try
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Name = driveInfo.VolumeLabel;
				treeNode2.Text = Lang.PS("本地磁盘(" + driveInfo.Name.Remove(driveInfo.Name.Length - 1) + ")", "Local disk(" + driveInfo.Name.Remove(driveInfo.Name.Length - 1) + ")");
				treeNode2.ImageIndex = 3;
				treeNode2.SelectedImageIndex = 3;
				treeNode2.Tag = new Tab
				{
					Path = driveInfo.Name,
					FType = Type.本地磁盘
				};
				treeNode.Nodes.Add(treeNode2);
			}
			catch
			{
			}
		}
		treeView1.ExpandAll();
		cbbepuip.Items.Clear();
		cbbepuip.Items.Add("");
		FrmChromatManager frmChromatManager = new FrmChromatManager();
		frmChromatManager.FrmChromatManager_Load(null, null);
		if (frmChromatManager.SunAquips.Count > 0)
		{
			for (int j = 0; j < frmChromatManager.SunAquips.Count; j++)
			{
				cbbepuip.Items.Add(frmChromatManager.SunAquips[j].info.Name);
			}
		}
		textBox2.Text = sysParam.strDirOptionInitDir;
		method_0();
		LoadLanguage();
	}

	private void LoadLanguage()
	{
		Text = Lang.PS("打开文件", "Open-file");
		groupBox1.Text = Lang.PS("查询条件", "Query condition");
		label1.Text = Lang.PS("色谱机名称:", "Chromatographic machine:");
		label4.Text = Lang.PS("创建起止日期:", "Beginning and ending dates:");
		label6.Text = Lang.PS("当前路径:", "Current path:");
		label2.Text = Lang.PS("通道:", "Channel:");
		label3.Text = Lang.PS("关键字:", "Keyword:");
		checkBox1.Text = Lang.PS("使用自定义历史", "Use custom history");
		button1.Text = Lang.PS("查询所有目录(谨慎使用)", "Query all directories");
		button2.Text = Lang.PS("查找当前目录", "Find the current directory");
		listView1.Columns[0].Text = Lang.PS("序号", "sequence:");
		listView1.Columns[1].Text = Lang.PS("色谱仪名称", "name:");
		listView1.Columns[2].Text = Lang.PS("通道类型", "channel type:");
		listView1.Columns[3].Text = Lang.PS("进样序号", "Sample number:");
		listView1.Columns[4].Text = Lang.PS("自定义名称", "Custom name:");
		listView1.Columns[5].Text = Lang.PS("进样日期", "Sample date:");
		listView1.Columns[6].Text = Lang.PS("进样时间", "Sample time:");
		listView1.Columns[7].Text = Lang.PS("文件名称", "file name:");
	}

	private void method_0()
	{
		textBox1.Items.Clear();
		if (checkBox1.Checked)
		{
			string text = Class49.ReadConfigSection("SampleName");
			if (text != null)
			{
				string[] array = text.Split('$');
				for (int i = 0; i < array.Length; i++)
				{
					textBox1.Items.Add(array[i]);
				}
			}
		}
		else
		{
			string text2 = textBox2.Text.Trim();
			if (text2 != "")
			{
				method_5(text2);
			}
		}
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (e.Node.Level >= 1)
		{
			e.Node.Nodes.Clear();
			listView1.Items.Clear();
			Tab tab = (Tab)e.Node.Tag;
			method_2(tab.Path.ToString());
			string[] directories = Directory.GetDirectories(tab.Path.ToString());
			string[] array = directories;
			foreach (string path in array)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				if (directoryInfo.Attributes == FileAttributes.Directory)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = directoryInfo.Name;
					treeNode.ImageIndex = 9;
					treeNode.SelectedImageIndex = 4;
					treeNode.Tag = new Tab
					{
						Path = path,
						FType = Type.Directory
					};
					e.Node.Nodes.Add(treeNode);
				}
			}
		}
		else
		{
			listView1.Items.Clear();
		}
		if (e.Node.Nodes.Count > 0)
		{
			e.Node.Expand();
		}
		method_0();
	}

	private void listView1_DoubleClick(object sender, EventArgs e)
	{
		DriveInfo.GetDrives();
		ListViewItem listViewItem = listView1.SelectedItems[0];
		Tab tab = (Tab)listViewItem.Tag;
		if (tab.FType == Type.File)
		{
			ChromFrm.Opensda(tab.Path);
		}
		else
		{
			method_2(tab.Path);
		}
	}

	private void method_1(string string_0)
	{
		string[] directories = Directory.GetDirectories(string_0);
		string[] files = Directory.GetFiles(string_0);
		listView1.Items.Clear();
		string[] array = directories;
		foreach (string path in array)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (directoryInfo.Attributes == FileAttributes.Directory)
			{
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = directoryInfo.Name;
				listViewItem.ImageIndex = 5;
				listViewItem.Tag = new Tab
				{
					Path = path,
					FType = Type.Directory
				};
				listView1.Items.Add(listViewItem);
			}
		}
		string[] array2 = files;
		foreach (string text in array2)
		{
			FileInfo fileInfo = new FileInfo(text);
			if (fileInfo.Attributes == FileAttributes.Archive)
			{
				ListViewItem listViewItem2 = new ListViewItem();
				listViewItem2.Text = fileInfo.Name;
				listViewItem2.ImageIndex = 2;
				listViewItem2.Tag = new Tab
				{
					Path = text,
					FType = Type.File
				};
				listView1.Items.Add(listViewItem2);
			}
		}
	}

	private void method_2(string string_0)
	{
		string[] directories = Directory.GetDirectories(string_0);
		DirectoryInfo directoryInfo = new DirectoryInfo(string_0);
		FileInfo[] fileInfo_ = new FileInfo[0];
		try
		{
			fileInfo_ = directoryInfo.GetFiles("*.sda");
			method_6(ref fileInfo_);
		}
		catch
		{
		}
		listView1.Items.Clear();
		textBox2.Text = string_0;
		string[] array = directories;
		foreach (string path in array)
		{
			DirectoryInfo directoryInfo2 = new DirectoryInfo(path);
			if (directoryInfo2.Attributes == FileAttributes.Directory)
			{
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.ImageIndex = 5;
				listViewItem.Text = directoryInfo2.Name;
				listViewItem.SubItems.Add("");
				listViewItem.SubItems.Add("文件夹");
				listViewItem.SubItems.Add(directoryInfo2.LastWriteTime.ToString("yyyy-MM-dd hh:mm"));
				listViewItem.Tag = new Tab
				{
					Path = path,
					FType = Type.Directory
				};
			}
		}
		int num = 0;
		FileInfo[] array2 = fileInfo_;
		foreach (FileInfo fileInfo in array2)
		{
			FileInfo fileInfo2 = fileInfo;
			if (fileInfo2.Name.EndsWith(".sda"))
			{
				string[] array3 = fileInfo2.Name.Replace(".sda", "").Split('_');
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				if (array3.Length >= 4)
				{
					text = array3[0];
					text2 = array3[1];
					text3 = array3[2];
					text4 = array3[3];
				}
				string[] items = new string[8]
				{
					num++.ToString(),
					text,
					text2,
					text4,
					text3,
					fileInfo2.CreationTime.ToString("yyyy-MM-dd"),
					fileInfo2.CreationTime.ToString("HH:mm:ss"),
					fileInfo2.Name.Replace(".sda", "")
				};
				ListViewItem listViewItem2 = new ListViewItem(items);
				string text5 = fileInfo2.Extension.Substring(1);
				string text6;
				if ((text6 = text5) != null && text6 == "sda")
				{
					listViewItem2.ImageIndex = 12;
				}
				listViewItem2.Tag = new Tab
				{
					Path = fileInfo.FullName,
					FType = Type.File
				};
				listView1.Items.Add(listViewItem2);
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		FrmTip frmTip = new FrmTip();
		frmTip.Show();
		Application.DoEvents();
		int_0 = 0;
		listView1.Items.Clear();
		string text = textBox2.Text.Trim();
		if (text != "")
		{
			method_4(text);
		}
		frmTip.Close();
	}

	private void method_3(string string_0, ref FileInfo[] fileInfo_0)
	{
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(string_0);
			if (fileInfo_0 == null)
			{
				fileInfo_0 = new FileInfo[0];
			}
			FileInfo[] files = directoryInfo.GetFiles("*.sda");
			Array.Resize(ref fileInfo_0, fileInfo_0.Length + files.Length);
			files.CopyTo(fileInfo_0, fileInfo_0.Length - files.Length);
			DirectoryInfo[] array = new DirectoryInfo[0];
			array = directoryInfo.GetDirectories("*");
			DirectoryInfo[] array2 = array;
			foreach (DirectoryInfo directoryInfo2 in array2)
			{
				method_3(directoryInfo2.FullName, ref fileInfo_0);
			}
		}
		catch
		{
		}
	}

	private void method_4(string string_0)
	{
		FileInfo[] fileInfo_ = null;
		method_3(string_0, ref fileInfo_);
		method_6(ref fileInfo_);
		FileInfo[] array = fileInfo_;
		foreach (FileInfo fileInfo in array)
		{
			FileInfo fileInfo2 = fileInfo;
			if (fileInfo2.Name.EndsWith(".sda"))
			{
				string[] array2 = fileInfo2.Name.Replace(".sda", "").Split('_');
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				if (array2.Length >= 4)
				{
					text = array2[0];
					text2 = array2[1];
					text3 = array2[2];
					text4 = array2[3];
				}
				string[] items = new string[8]
				{
					"0",
					text,
					text2,
					text4,
					text3,
					fileInfo2.CreationTime.ToString("yyyy-MM-dd"),
					fileInfo2.CreationTime.ToString("HH:mm:ss"),
					fileInfo2.Name.Replace(".sda", "")
				};
				ListViewItem listViewItem = new ListViewItem(items);
				string text5 = fileInfo2.Extension.Substring(1);
				string text6;
				if ((text6 = text5) != null && text6 == "sda")
				{
					listViewItem.ImageIndex = 12;
				}
				listViewItem.Tag = new Tab
				{
					Path = fileInfo.FullName,
					FType = Type.File
				};
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				if (cbbepuip.Text.Trim() == "" || text == cbbepuip.Text.Trim())
				{
					flag = true;
				}
				if (comboBox2.Text.Trim() == "" || text2 == comboBox2.Text.Trim())
				{
					flag2 = true;
				}
				if (textBox1.Text.Trim() == "" || text3.Contains(textBox1.Text.Trim()))
				{
					flag3 = true;
				}
				if (fileInfo2.CreationTime.Date >= dateTimePicker1.Value.Date && fileInfo2.CreationTime.Date <= dateTimePicker2.Value.Date)
				{
					flag4 = true;
				}
				if (flag && flag2 && flag3 && flag4)
				{
					listViewItem.SubItems[0].Text = int_0++.ToString();
					listView1.Items.Add(listViewItem);
				}
			}
		}
	}

	private void method_5(string string_0)
	{
		FrmTip frmTip = new FrmTip();
		frmTip.Show();
		frmTip.label1.Text = Lang.PS("正在准备关键字...", "Preparing keywords...");
		Application.DoEvents();
		FileInfo[] fileInfo_ = null;
		method_3(string_0, ref fileInfo_);
		method_6(ref fileInfo_);
		FileInfo[] array = fileInfo_;
		foreach (FileInfo fileInfo in array)
		{
			FileInfo fileInfo2 = fileInfo;
			if (fileInfo2.Name.EndsWith(".sda"))
			{
				string[] array2 = fileInfo2.Name.Replace(".sda", "").Split('_');
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				if (array2.Length >= 4)
				{
					text = array2[0];
					text2 = array2[1];
					text3 = array2[2];
					text4 = array2[3];
				}
				string[] items = new string[8]
				{
					"0",
					text,
					text2,
					text4,
					text3,
					fileInfo2.CreationTime.ToString("yyyy-MM-dd"),
					fileInfo2.CreationTime.ToString("HH:mm:ss"),
					fileInfo2.Name.Replace(".sda", "")
				};
				ListViewItem listViewItem = new ListViewItem(items);
				string text5 = fileInfo2.Extension.Substring(1);
				string text6;
				if ((text6 = text5) != null && text6 == "sda")
				{
					listViewItem.ImageIndex = 12;
				}
				if (!textBox1.Items.Contains(text3) && text3.Trim() != "")
				{
					textBox1.Items.Add(text3);
				}
			}
		}
		frmTip.Close();
	}

	private void method_6(ref FileInfo[] fileInfo_0)
	{
		FileInfo[] array = fileInfo_0;
		Array.Sort(array, (FileInfo fileInfo2, FileInfo fileInfo) => fileInfo.CreationTime.CompareTo(fileInfo2.CreationTime));
	}

	private void button2_Click(object sender, EventArgs e)
	{
		listView1.Items.Clear();
		string text = textBox2.Text.Trim();
		int num = 0;
		if (!(text != ""))
		{
			return;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		FileInfo[] fileInfo_ = new FileInfo[0];
		try
		{
			fileInfo_ = directoryInfo.GetFiles("*.sda");
			method_6(ref fileInfo_);
		}
		catch
		{
		}
		FileInfo[] array = fileInfo_;
		foreach (FileInfo fileInfo in array)
		{
			FileInfo fileInfo2 = fileInfo;
			if (fileInfo2.Name.EndsWith(".sda"))
			{
				string[] array2 = fileInfo2.Name.Replace(".sda", "").Split('_');
				string text2 = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				if (array2.Length >= 4)
				{
					text2 = array2[0];
					text3 = array2[1];
					text4 = array2[2];
					text5 = array2[3];
				}
				string[] items = new string[8]
				{
					num++.ToString(),
					text2,
					text3,
					text5,
					text4,
					fileInfo2.CreationTime.ToString("yyyy-MM-dd"),
					fileInfo2.CreationTime.ToString("HH:mm:ss"),
					fileInfo2.Name.Replace(".sda", "")
				};
				ListViewItem listViewItem = new ListViewItem(items);
				string text6 = fileInfo2.Extension.Substring(1);
				string text7;
				if ((text7 = text6) != null && text7 == "sda")
				{
					listViewItem.ImageIndex = 12;
				}
				listViewItem.Tag = new Tab
				{
					Path = fileInfo.FullName,
					FType = Type.File
				};
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				if (cbbepuip.Text.Trim() == "" || text2 == cbbepuip.Text.Trim())
				{
					flag = true;
				}
				if (comboBox2.Text.Trim() == "" || text3 == comboBox2.Text.Trim())
				{
					flag2 = true;
				}
				if (textBox1.Text.Trim() == "" || text4.Contains(textBox1.Text.Trim()))
				{
					flag3 = true;
				}
				if (fileInfo2.CreationTime.Date >= dateTimePicker1.Value.Date && fileInfo2.CreationTime.Date <= dateTimePicker2.Value.Date)
				{
					flag4 = true;
				}
				if (flag && flag2 && flag3 && flag4)
				{
					listView1.Items.Add(listViewItem);
				}
			}
		}
	}

	private void frmopenfile_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		method_0();
	}

	[CompilerGenerated]
	private static int smethod_0(FileInfo fileInfo_0, FileInfo fileInfo_1)
	{
		return fileInfo_1.CreationTime.CompareTo(fileInfo_0.CreationTime);
	}
}
