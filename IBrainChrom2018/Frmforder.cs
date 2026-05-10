using System;
using System.ComponentModel;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Frmforder : Form
{
	private TreeView treeView1;

	private TextBox textBox1;

	private ListView listView1;

	private ColumnHeader columnHeader_0;

	private ColumnHeader columnHeader_1;

	private ColumnHeader columnHeader_2;

	private ColumnHeader columnHeader_3;

	private Splitter splitter1;

	private DirectoryInfo directoryInfo_0;

	private static string string_0 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	private MainMenu mainMenu_0;

	private MenuItem menuItem_0;

	private MenuItem menuItem_1;

	private OdbcConnection odbcConnection_0;

	private IContainer icontainer_0;

	private void method_0(TreeView treeView_0)
	{
		treeView_0.Nodes.Clear();
		string text = string_0;
		for (int i = 0; i < text.Length; i++)
		{
			string path = text[i] + ":\\";
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				if (directoryInfo.Exists)
				{
					TreeNode treeNode = new TreeNode(directoryInfo.FullName);
					treeView_0.Nodes.Add(treeNode);
					method_1(treeNode);
				}
			}
			catch (Exception ex)
			{
				Program.WriteLine(ex.Message);
			}
		}
	}

	private void method_1(TreeNode treeNode_0)
	{
		try
		{
			if (treeNode_0.Nodes.Count == 0)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(treeNode_0.FullPath);
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					TreeNode node = new TreeNode(directoryInfo2.Name);
					treeNode_0.Nodes.Add(node);
				}
			}
			foreach (TreeNode node3 in treeNode_0.Nodes)
			{
				if (node3.Nodes.Count == 0)
				{
					DirectoryInfo directoryInfo3 = new DirectoryInfo(node3.FullPath);
					DirectoryInfo[] directories2 = directoryInfo3.GetDirectories();
					foreach (DirectoryInfo directoryInfo4 in directories2)
					{
						TreeNode node2 = new TreeNode(directoryInfo4.Name);
						node3.Nodes.Add(node2);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Program.WriteLine(ex.Message);
		}
	}

	private void method_2(ListView listView_0, string string_1)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(string_1);
		listView_0.Items.Clear();
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		foreach (DirectoryInfo directoryInfo2 in directories)
		{
			ListViewItem listViewItem = new ListViewItem(directoryInfo2.Name);
			listViewItem.SubItems.Add(string.Empty);
			listViewItem.SubItems.Add("文件夹");
			listViewItem.SubItems.Add(string.Empty);
			listView_0.Items.Add(listViewItem);
		}
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			ListViewItem listViewItem2 = new ListViewItem(fileInfo.Name);
			listViewItem2.SubItems.Add(fileInfo.Length / 1024 + " KB");
			listViewItem2.SubItems.Add(fileInfo.Extension + "文件");
			listViewItem2.SubItems.Add(fileInfo.LastWriteTime.ToString());
			listView_0.Items.Add(listViewItem2);
		}
	}

	private string method_3(TreeNode treeNode_0)
	{
		string text = "";
		try
		{
			text = treeNode_0.FullPath;
			int num = text.IndexOf("\\\\");
			if (num > 1)
			{
				text = treeNode_0.FullPath.Remove(num, 1);
			}
		}
		catch (Exception ex)
		{
			Program.WriteLine(ex.Message);
		}
		return text;
	}

	private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
	{
		method_1(e.Node);
		textBox1.Text = method_3(e.Node);
		directoryInfo_0 = new DirectoryInfo(e.Node.FullPath);
		method_2(listView1, method_3(e.Node));
	}

	private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
	{
		method_1(e.Node);
		textBox1.Text = method_3(e.Node);
		directoryInfo_0 = new DirectoryInfo(e.Node.FullPath);
	}

	private void Frmforder_Load(object sender, EventArgs e)
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "打开谱图文件";
			break;
		case SysLanguage.EN:
			Text = "Open ChromFile";
			break;
		}
		method_0(treeView1);
	}

	public Frmforder()
	{
		InitializeComponent();
	}

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
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.listView1 = new System.Windows.Forms.ListView();
		this.columnHeader_0 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_1 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_2 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader_3 = new System.Windows.Forms.ColumnHeader();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.mainMenu_0 = new System.Windows.Forms.MainMenu(this.icontainer_0);
		this.menuItem_0 = new System.Windows.Forms.MenuItem();
		this.menuItem_1 = new System.Windows.Forms.MenuItem();
		this.odbcConnection_0 = new System.Data.Odbc.OdbcConnection();
		base.SuspendLayout();
		this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
		this.treeView1.Location = new System.Drawing.Point(0, 0);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(200, 573);
		this.treeView1.TabIndex = 0;
		this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(treeView1_BeforeExpand);
		this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(treeView1_BeforeSelect);
		this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.textBox1.Location = new System.Drawing.Point(200, 0);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(592, 21);
		this.textBox1.TabIndex = 1;
		this.textBox1.Text = "textBox1";
		this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[4] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3 });
		this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listView1.Location = new System.Drawing.Point(200, 21);
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(592, 552);
		this.listView1.TabIndex = 2;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.View = System.Windows.Forms.View.Details;
		this.columnHeader_0.Text = "名称";
		this.columnHeader_0.Width = 120;
		this.columnHeader_1.Text = "大小";
		this.columnHeader_1.Width = 100;
		this.columnHeader_2.Text = "类型";
		this.columnHeader_2.Width = 120;
		this.columnHeader_3.Text = "时间";
		this.columnHeader_3.Width = 140;
		this.splitter1.Location = new System.Drawing.Point(200, 21);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(3, 552);
		this.splitter1.TabIndex = 3;
		this.splitter1.TabStop = false;
		this.mainMenu_0.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1] { this.menuItem_0 });
		this.menuItem_0.Index = 0;
		this.menuItem_0.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1] { this.menuItem_1 });
		this.menuItem_0.Text = "文件";
		this.menuItem_1.Index = 0;
		this.menuItem_1.Text = "新建";
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		base.ClientSize = new System.Drawing.Size(792, 573);
		base.Controls.Add(this.splitter1);
		base.Controls.Add(this.listView1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.treeView1);
		base.Menu = this.mainMenu_0;
		base.Name = "Frmforder";
		this.Text = "Frmforder";
		base.Load += new System.EventHandler(Frmforder_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
