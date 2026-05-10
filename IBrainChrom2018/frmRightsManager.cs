using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class frmRightsManager : Form
{
	public struct User
	{
		public enum Level
		{
			管理员,
			分析员,
			检验员,
			访问员
		}

		public string UName;

		public string Pwd;

		public DateTime TooltipTime;

		public Level UserLevel;
	}

	private IContainer components;

	private ContextMenuStrip cmsRightsTreeView;

	private ToolStripMenuItem tsmiSelectAll;

	private ToolStripMenuItem tsmiCancelAll;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem tsmiExpandAll;

	private ToolStripMenuItem tsmiCollapseAll;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem tsmiReload;

	private ToolStrip tsRightsManager;

	private ToolStripButton tsbtnRefreshOperator;

	private ToolStripButton tsbtnCloseWindow;

	private ToolStripSeparator tsSpr1;

	private ToolStripSeparator tsSpr2;

	private ToolStripButton tsbtnAddOperator;

	private ToolStripButton tsbtnDeleteOperator;

	private GroupBox gbOperatorList;

	private DataGridView dgvOperatorList;

	private DataGridViewTextBoxColumn 用户名;

	private DataGridViewTextBoxColumn 密码;

	private DataGridViewTextBoxColumn 提示日期;

	private DataGridViewTextBoxColumn 用户级别;

	private ToolStripButton tSaveAll;

	private ToolStripSeparator toolStripSeparator3;

	public User[] NetChromUsers;

	private string passPath = "";

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.frmRightsManager));
		this.cmsRightsTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiCancelAll = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.tsmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
		this.tsRightsManager = new System.Windows.Forms.ToolStrip();
		this.tsbtnRefreshOperator = new System.Windows.Forms.ToolStripButton();
		this.tsSpr1 = new System.Windows.Forms.ToolStripSeparator();
		this.tsbtnAddOperator = new System.Windows.Forms.ToolStripButton();
		this.tsbtnDeleteOperator = new System.Windows.Forms.ToolStripButton();
		this.tsSpr2 = new System.Windows.Forms.ToolStripSeparator();
		this.tSaveAll = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.tsbtnCloseWindow = new System.Windows.Forms.ToolStripButton();
		this.gbOperatorList = new System.Windows.Forms.GroupBox();
		this.dgvOperatorList = new System.Windows.Forms.DataGridView();
		this.用户名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.密码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.提示日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.用户级别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cmsRightsTreeView.SuspendLayout();
		this.tsRightsManager.SuspendLayout();
		this.gbOperatorList.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOperatorList).BeginInit();
		base.SuspendLayout();
		this.cmsRightsTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.tsmiSelectAll, this.tsmiCancelAll, this.toolStripSeparator1, this.tsmiExpandAll, this.tsmiCollapseAll, this.toolStripSeparator2, this.tsmiReload });
		this.cmsRightsTreeView.Name = "cmsTreeView";
		this.cmsRightsTreeView.Size = new System.Drawing.Size(141, 126);
		this.cmsRightsTreeView.Text = "权限视图右键菜单";
		this.tsmiSelectAll.Name = "tsmiSelectAll";
		this.tsmiSelectAll.Size = new System.Drawing.Size(140, 22);
		this.tsmiSelectAll.Text = "全部勾选(&S)";
		this.tsmiCancelAll.Name = "tsmiCancelAll";
		this.tsmiCancelAll.Size = new System.Drawing.Size(140, 22);
		this.tsmiCancelAll.Text = "全部取消(&C)";
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
		this.tsmiExpandAll.Name = "tsmiExpandAll";
		this.tsmiExpandAll.Size = new System.Drawing.Size(140, 22);
		this.tsmiExpandAll.Text = "全部展开(&E)";
		this.tsmiCollapseAll.Name = "tsmiCollapseAll";
		this.tsmiCollapseAll.Size = new System.Drawing.Size(140, 22);
		this.tsmiCollapseAll.Text = "全部折叠(&X)";
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
		this.tsmiReload.Name = "tsmiReload";
		this.tsmiReload.Size = new System.Drawing.Size(140, 22);
		this.tsmiReload.Text = "重新加载(&R)";
		this.tsRightsManager.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.tsRightsManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.tsbtnRefreshOperator, this.tsSpr1, this.tsbtnAddOperator, this.tsbtnDeleteOperator, this.tsSpr2, this.tSaveAll, this.toolStripSeparator3, this.tsbtnCloseWindow });
		this.tsRightsManager.Location = new System.Drawing.Point(0, 0);
		this.tsRightsManager.Name = "tsRightsManager";
		this.tsRightsManager.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.tsRightsManager.Size = new System.Drawing.Size(624, 64);
		this.tsRightsManager.TabIndex = 1;
		this.tsRightsManager.Text = "权限管理工具栏";
		this.tsbtnRefreshOperator.Image = (System.Drawing.Image)resources.GetObject("tsbtnRefreshOperator.Image");
		this.tsbtnRefreshOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.tsbtnRefreshOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsbtnRefreshOperator.Name = "tsbtnRefreshOperator";
		this.tsbtnRefreshOperator.Size = new System.Drawing.Size(60, 61);
		this.tsbtnRefreshOperator.Text = "刷新用户";
		this.tsbtnRefreshOperator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.tsbtnRefreshOperator.Click += new System.EventHandler(tsbtnRefreshOperator_Click);
		this.tsSpr1.Name = "tsSpr1";
		this.tsSpr1.Size = new System.Drawing.Size(6, 64);
		this.tsbtnAddOperator.Image = (System.Drawing.Image)resources.GetObject("tsbtnAddOperator.Image");
		this.tsbtnAddOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.tsbtnAddOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsbtnAddOperator.Name = "tsbtnAddOperator";
		this.tsbtnAddOperator.Size = new System.Drawing.Size(60, 61);
		this.tsbtnAddOperator.Text = "添加用户";
		this.tsbtnAddOperator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.tsbtnAddOperator.Click += new System.EventHandler(tsbtnAddOperator_Click);
		this.tsbtnDeleteOperator.Image = (System.Drawing.Image)resources.GetObject("tsbtnDeleteOperator.Image");
		this.tsbtnDeleteOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.tsbtnDeleteOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsbtnDeleteOperator.Name = "tsbtnDeleteOperator";
		this.tsbtnDeleteOperator.Size = new System.Drawing.Size(60, 61);
		this.tsbtnDeleteOperator.Text = "删除用户";
		this.tsbtnDeleteOperator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.tsbtnDeleteOperator.Click += new System.EventHandler(tsbtnDeleteOperator_Click);
		this.tsSpr2.Name = "tsSpr2";
		this.tsSpr2.Size = new System.Drawing.Size(6, 64);
		this.tSaveAll.Image = (System.Drawing.Image)resources.GetObject("tSaveAll.Image");
		this.tSaveAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.tSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tSaveAll.Name = "tSaveAll";
		this.tSaveAll.Size = new System.Drawing.Size(60, 61);
		this.tSaveAll.Text = "全部保存";
		this.tSaveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.tSaveAll.Click += new System.EventHandler(tSaveAll_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 64);
		this.tsbtnCloseWindow.Image = (System.Drawing.Image)resources.GetObject("tsbtnCloseWindow.Image");
		this.tsbtnCloseWindow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.tsbtnCloseWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsbtnCloseWindow.Name = "tsbtnCloseWindow";
		this.tsbtnCloseWindow.Size = new System.Drawing.Size(60, 61);
		this.tsbtnCloseWindow.Text = "关闭窗口";
		this.tsbtnCloseWindow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.tsbtnCloseWindow.Click += new System.EventHandler(tsbtnCloseWindow_Click);
		this.gbOperatorList.Controls.Add(this.dgvOperatorList);
		this.gbOperatorList.Location = new System.Drawing.Point(0, 67);
		this.gbOperatorList.Name = "gbOperatorList";
		this.gbOperatorList.Size = new System.Drawing.Size(624, 370);
		this.gbOperatorList.TabIndex = 14;
		this.gbOperatorList.TabStop = false;
		this.gbOperatorList.Text = "操作员列表";
		this.dgvOperatorList.AllowUserToAddRows = false;
		this.dgvOperatorList.AllowUserToDeleteRows = false;
		this.dgvOperatorList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvOperatorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvOperatorList.Columns.AddRange(this.用户名, this.密码, this.提示日期, this.用户级别);
		this.dgvOperatorList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOperatorList.Location = new System.Drawing.Point(3, 17);
		this.dgvOperatorList.MultiSelect = false;
		this.dgvOperatorList.Name = "dgvOperatorList";
		this.dgvOperatorList.ReadOnly = true;
		this.dgvOperatorList.RowHeadersWidth = 25;
		this.dgvOperatorList.RowTemplate.Height = 23;
		this.dgvOperatorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvOperatorList.Size = new System.Drawing.Size(618, 350);
		this.dgvOperatorList.TabIndex = 5;
		this.dgvOperatorList.DoubleClick += new System.EventHandler(dgvOperatorList_DoubleClick);
		this.用户名.HeaderText = "用户名";
		this.用户名.Name = "用户名";
		this.用户名.ReadOnly = true;
		this.密码.HeaderText = "密码";
		this.密码.Name = "密码";
		this.密码.ReadOnly = true;
		this.提示日期.HeaderText = "提示日期";
		this.提示日期.Name = "提示日期";
		this.提示日期.ReadOnly = true;
		this.用户级别.HeaderText = "用户级别";
		this.用户级别.Name = "用户级别";
		this.用户级别.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(624, 442);
		base.Controls.Add(this.gbOperatorList);
		base.Controls.Add(this.tsRightsManager);
		this.DoubleBuffered = true;
		base.HelpButton = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(640, 480);
		base.Name = "frmRightsManager";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "权限管理";
		base.Load += new System.EventHandler(frmRightsManager_Load);
		this.cmsRightsTreeView.ResumeLayout(false);
		this.tsRightsManager.ResumeLayout(false);
		this.tsRightsManager.PerformLayout();
		this.gbOperatorList.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOperatorList).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmRightsManager()
	{
		InitializeComponent();
	}

	private void frmRightsManager_Load(object sender, EventArgs e)
	{
		passPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		NetChromUsers = new User[0];
		passPath += "\\IBrainChrom\\UInfo.upf";
		LoadFromFile(passPath);
		if (!IsHaveAdmin())
		{
			Array.Resize(ref NetChromUsers, NetChromUsers.Length + 1);
			NetChromUsers[NetChromUsers.Length - 1].UName = "admin";
			NetChromUsers[NetChromUsers.Length - 1].Pwd = "admin";
			NetChromUsers[NetChromUsers.Length - 1].TooltipTime = DateTime.Now.AddYears(100);
			NetChromUsers[NetChromUsers.Length - 1].UserLevel = User.Level.管理员;
		}
		refreshUser2Grid();
	}

	public bool LoadFromFile(string fileName)
	{
		if (!File.Exists(fileName))
		{
			return false;
		}
		fileName = fileName.ToLower();
		FileInfo fi = null;
		FileStream fs = null;
		BinaryReader br = null;
		try
		{
			Common.LoadFromFile(fileName, out fi, out fs, out br);
			int num = 0;
			num = br.ReadInt32();
			Array.Resize(ref NetChromUsers, num);
			for (int i = 0; i < num; i++)
			{
				NetChromUsers[i].UName = br.ReadString();
				NetChromUsers[i].Pwd = br.ReadString();
				NetChromUsers[i].TooltipTime = DateTime.Parse(br.ReadString());
				NetChromUsers[i].UserLevel = (User.Level)br.ReadInt32();
			}
		}
		catch
		{
			MessageBox.Show("载入失败");
			return false;
		}
		finally
		{
			Common.CloseFile(ref fs, ref br);
		}
		return true;
	}

	public bool IsHaveAdmin()
	{
		for (int i = 0; i < NetChromUsers.Length; i++)
		{
			if (NetChromUsers[i].UName == "admin")
			{
				return true;
			}
		}
		return false;
	}

	public void SaveToFile(string fileName)
	{
		FileInfo fi = null;
		FileStream fs = null;
		BinaryWriter bw = null;
		try
		{
			Common.SaveToFile(fileName, out fi, out fs, out bw);
			bool flag = IsHaveAdmin();
			int num = NetChromUsers.Length;
			if (!flag)
			{
				num++;
			}
			bw.Write(num);
			for (int i = 0; i < NetChromUsers.Length; i++)
			{
				bw.Write(NetChromUsers[i].UName);
				bw.Write(NetChromUsers[i].Pwd);
				bw.Write(NetChromUsers[i].TooltipTime.Date.ToString());
				bw.Write((int)NetChromUsers[i].UserLevel);
			}
			if (!flag)
			{
				Array.Resize(ref NetChromUsers, NetChromUsers.Length + 1);
				NetChromUsers[NetChromUsers.Length - 1].UName = "admin";
				NetChromUsers[NetChromUsers.Length - 1].Pwd = "admin";
				NetChromUsers[NetChromUsers.Length - 1].TooltipTime = DateTime.Now.AddYears(100);
				NetChromUsers[NetChromUsers.Length - 1].UserLevel = User.Level.管理员;
				bw.Write(NetChromUsers[NetChromUsers.Length - 1].UName);
				bw.Write(NetChromUsers[NetChromUsers.Length - 1].Pwd);
				bw.Write(NetChromUsers[NetChromUsers.Length - 1].TooltipTime.Date.ToString());
				bw.Write((int)NetChromUsers[NetChromUsers.Length - 1].UserLevel);
			}
			bw.Write("--End--");
		}
		finally
		{
			Common.CloseFile(ref fs, ref bw);
			Text = "保存成功";
		}
	}

	private void tsbtnCloseWindow_Click(object sender, EventArgs e)
	{
		Close();
	}

	private bool CheckIsExistUpdate(User U)
	{
		for (int i = 0; i < NetChromUsers.Length; i++)
		{
			if (U.UName.Trim() == NetChromUsers[i].UName)
			{
				if (MessageBox.Show("用户已存在，是否修改该用户？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					NetChromUsers[i] = U;
				}
				return true;
			}
		}
		return false;
	}

	private void refreshUser2Grid()
	{
		dgvOperatorList.Rows.Clear();
		for (int i = 0; i < NetChromUsers.Length; i++)
		{
			object[] values = new object[4]
			{
				NetChromUsers[i].UName,
				NetChromUsers[i].Pwd,
				NetChromUsers[i].TooltipTime.Date.ToString(),
				NetChromUsers[i].UserLevel.ToString()
			};
			dgvOperatorList.Rows.Add(values);
		}
	}

	private void tsbtnAddOperator_Click(object sender, EventArgs e)
	{
		frmOperatorManager frmOperatorManager2 = new frmOperatorManager();
		frmOperatorManager2.ShowDialog();
		if (frmOperatorManager2.AddUser.UName != "")
		{
			if (!CheckIsExistUpdate(frmOperatorManager2.AddUser))
			{
				Array.Resize(ref NetChromUsers, NetChromUsers.Length + 1);
				NetChromUsers[NetChromUsers.Length - 1] = frmOperatorManager2.AddUser;
			}
			refreshUser2Grid();
			SaveToFile(passPath);
		}
	}

	private void tsbtnRefreshOperator_Click(object sender, EventArgs e)
	{
		LoadFromFile(passPath);
		refreshUser2Grid();
	}

	private void tSaveAll_Click(object sender, EventArgs e)
	{
		SaveToFile(passPath);
	}

	private void tsbtnDeleteOperator_Click(object sender, EventArgs e)
	{
		if (dgvOperatorList.SelectedRows.Count > 0)
		{
			if (MessageBox.Show("确认删除选中用户？", "用户删除", MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			string text = dgvOperatorList.SelectedRows[0].Cells[0].Value.ToString();
			for (int i = 0; i < NetChromUsers.Length; i++)
			{
				if (!(text.Trim() == NetChromUsers[i].UName))
				{
					continue;
				}
				for (int j = i; j < NetChromUsers.Length; j++)
				{
					if (j != NetChromUsers.Length - 1)
					{
						NetChromUsers[j] = NetChromUsers[j + 1];
					}
				}
				Array.Resize(ref NetChromUsers, NetChromUsers.Length - 1);
				break;
			}
			Text = "删除成功！";
			SaveToFile(passPath);
			refreshUser2Grid();
			Common.AddLog2Db("系统", Common.LoginUserName, "", "系统用户及权限管理", "删除用户:用户名:" + text);
		}
		else
		{
			MessageBox.Show("请选择要删除的用户！");
		}
	}

	private void dgvOperatorList_DoubleClick(object sender, EventArgs e)
	{
		try
		{
			if (dgvOperatorList.SelectedRows.Count <= 0)
			{
				return;
			}
			string text = dgvOperatorList.SelectedRows[0].Cells[0].Value.ToString();
			frmOperatorManager frmOperatorManager2 = new frmOperatorManager();
			bool flag = false;
			for (int i = 0; i < NetChromUsers.Length; i++)
			{
				if (text.Trim() == NetChromUsers[i].UName)
				{
					frmOperatorManager2.txtOperatorName.Text = NetChromUsers[i].UName;
					frmOperatorManager2.txtValidatePwd.Text = NetChromUsers[i].Pwd;
					frmOperatorManager2.txtOperatorPwd.Text = NetChromUsers[i].Pwd;
					frmOperatorManager2.dateTimePicker1.Value = NetChromUsers[i].TooltipTime;
					frmOperatorManager2.cright.SelectedIndex = (int)NetChromUsers[i].UserLevel;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			frmOperatorManager2.ShowDialog();
			if (frmOperatorManager2.AddUser.UName != "")
			{
				if (!CheckIsExistUpdate(frmOperatorManager2.AddUser))
				{
					Array.Resize(ref NetChromUsers, NetChromUsers.Length + 1);
					NetChromUsers[NetChromUsers.Length - 1] = frmOperatorManager2.AddUser;
				}
				refreshUser2Grid();
				SaveToFile(passPath);
			}
		}
		catch
		{
		}
	}
}
