using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Logon : Form
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

	private IContainer icontainer_0;

	private Button bLogin;

	private Button bCancel;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	private TextBox lclTextBox2;

	private string string_0 = "";

	private ComboBox lclTextBox1;

	public User[] NetChromUsers;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.Logon));
		this.bLogin = new System.Windows.Forms.Button();
		this.bCancel = new System.Windows.Forms.Button();
		this.lclTextBox2 = new System.Windows.Forms.TextBox();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.lclTextBox1 = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.bLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.bLogin.Location = new System.Drawing.Point(41, 147);
		this.bLogin.Name = "bLogin";
		this.bLogin.Size = new System.Drawing.Size(75, 23);
		this.bLogin.TabIndex = 3;
		this.bLogin.Text = "登录";
		this.bLogin.UseVisualStyleBackColor = true;
		this.bLogin.Click += new System.EventHandler(bLogin_Click);
		this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.bCancel.Location = new System.Drawing.Point(169, 147);
		this.bCancel.Name = "bCancel";
		this.bCancel.Size = new System.Drawing.Size(75, 23);
		this.bCancel.TabIndex = 4;
		this.bCancel.Text = "取消";
		this.bCancel.UseVisualStyleBackColor = true;
		this.lclTextBox2.Location = new System.Drawing.Point(98, 87);
		this.lclTextBox2.Name = "lclTextBox2";
		this.lclTextBox2.PasswordChar = '*';
		this.lclTextBox2.Size = new System.Drawing.Size(146, 21);
		this.lclTextBox2.TabIndex = 2;
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(39, 90);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(53, 12);
		this.lclLabel2.TabIndex = 2;
		this.lclLabel2.Text = "密  码：";
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(39, 42);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(53, 12);
		this.lclLabel1.TabIndex = 2;
		this.lclLabel1.Text = "用户名：";
		this.lclTextBox1.FormattingEnabled = true;
		this.lclTextBox1.Location = new System.Drawing.Point(98, 42);
		this.lclTextBox1.Name = "lclTextBox1";
		this.lclTextBox1.Size = new System.Drawing.Size(146, 20);
		this.lclTextBox1.TabIndex = 6;
		base.AcceptButton = this.bLogin;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.bCancel;
		base.ClientSize = new System.Drawing.Size(296, 189);
		base.Controls.Add(this.lclTextBox1);
		base.Controls.Add(this.lclTextBox2);
		base.Controls.Add(this.lclLabel2);
		base.Controls.Add(this.lclLabel1);
		base.Controls.Add(this.bCancel);
		base.Controls.Add(this.bLogin);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Logon";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "色谱工作站登陆";
		base.Load += new System.EventHandler(Logon_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Logon()
	{
		InitializeComponent();
	}

	private void Logon_Load(object sender, EventArgs e)
	{
		string_0 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		NetChromUsers = new User[0];
		string_0 += "\\IBrainChrom\\UInfo.upf";
		if (File.Exists(string_0))
		{
			LoadFromFile(string_0);
			if (NetChromUsers.Length == 0)
			{
				Class49.user_0.u_name = "admin";
				Class49.user_0.TipTime = DateTime.MaxValue.AddYears(-1);
				Class49.user_0.ULevel = IBrainChrom2018.User.Level.管理员;
				MessageBox.Show("未设置密码,请先设置用户后使用!");
				string moduleName = Process.GetCurrentProcess().MainModule.ModuleName;
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(moduleName);
				method_0(fileNameWithoutExtension);
			}
			if (NetChromUsers.Length == 1 && NetChromUsers[0].UName == "admin" && !(NetChromUsers[0].Pwd == "admin"))
			{
			}
		}
	}

	private void method_0(string string_1)
	{
		try
		{
			Process[] processesByName = Process.GetProcessesByName(string_1);
			if (processesByName.Length != 0)
			{
				for (int i = 0; i < processesByName.Length; i++)
				{
					if (!processesByName[i].CloseMainWindow())
					{
						processesByName[i].Kill();
					}
					Program.WriteLine("进程 {0}关闭成功", string_1);
				}
			}
			else
			{
				Program.WriteLine("进程 {0} 关闭失败!", string_1);
			}
		}
		catch
		{
			Program.WriteLine("结束进程{0}出错！", string_1);
		}
	}

	public bool ChangePwd(string UName, string OldPwd, string Pwd)
	{
		string_0 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		NetChromUsers = new User[0];
		string_0 += "\\IBrainChrom\\UInfo.upf";
		LoadFromFile(string_0);
		for (int i = 0; i < NetChromUsers.Length; i++)
		{
			if (OldPwd == NetChromUsers[i].Pwd && UName == NetChromUsers[i].UName)
			{
				NetChromUsers[i].Pwd = Pwd;
				NetChromUsers[i].TooltipTime = NetChromUsers[i].TooltipTime.AddMonths(2);
				SaveToFile(string_0);
				return true;
			}
		}
		return false;
	}

	public bool LoadFromFile(string fileName)
	{
		if (!File.Exists(fileName))
		{
			return false;
		}
		fileName = fileName.ToLower();
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_, out fileStream_, out binaryReader_);
			int num = binaryReader_.ReadInt32();
			Array.Resize(ref NetChromUsers, num);
			for (int i = 0; i < num; i++)
			{
				NetChromUsers[i].UName = binaryReader_.ReadString();
				NetChromUsers[i].Pwd = binaryReader_.ReadString();
				NetChromUsers[i].TooltipTime = DateTime.Parse(binaryReader_.ReadString());
				NetChromUsers[i].UserLevel = (User.Level)binaryReader_.ReadInt32();
				lclTextBox1.Items.Add(NetChromUsers[i].UName);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("密码文件载入失败");
			return false;
		}
		finally
		{
			lclTextBox1.SelectedIndex = 0;
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
		return true;
	}

	private void bLogin_Click(object sender, EventArgs e)
	{
		string text = lclTextBox1.Text.Trim();
		string text2 = lclTextBox2.Text.Trim();
		if (NetChromUsers.Length != 0)
		{
			base.DialogResult = DialogResult.Cancel;
			bool flag = false;
			for (int i = 0; i < NetChromUsers.Length; i++)
			{
				if (NetChromUsers[i].UName == "admin")
				{
					flag = true;
				}
				if (text2 == NetChromUsers[i].Pwd && text == NetChromUsers[i].UName)
				{
					Class49.user_0.u_name = NetChromUsers[i].UName;
					Class49.user_0.TipTime = NetChromUsers[i].TooltipTime;
					Class49.user_0.ULevel = (IBrainChrom2018.User.Level)NetChromUsers[i].UserLevel;
					base.DialogResult = DialogResult.OK;
					Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "登录", "登录系统");
					if (base.DialogResult != DialogResult.OK)
					{
						base.DialogResult = DialogResult.Cancel;
						MessageBox.Show("你没有权限或用户名、密码错误！");
						Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "登录", "登录系统失败,没有权限或用户名、密码错误");
					}
					return;
				}
			}
		}
		else
		{
			if (!(text2 == "admin") || !(text == "admin"))
			{
				base.DialogResult = DialogResult.Cancel;
				MessageBox.Show("你没有权限或用户名、密码错误！");
				Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "登录", "登录系统失败,没有权限或用户名、密码错误");
				return;
			}
			Class49.user_0.u_name = "admin";
			Class49.user_0.TipTime = DateTime.MaxValue.AddYears(-1);
			Class49.user_0.ULevel = IBrainChrom2018.User.Level.管理员;
			base.DialogResult = DialogResult.OK;
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "登录", "启动系统");
		}
		if (base.DialogResult != DialogResult.OK)
		{
			base.DialogResult = DialogResult.Cancel;
			MessageBox.Show(Lang.PS("没有权限或用户名、密码错误！", "No permissions or user name, password error!"), Lang.PS("提示", "Tips"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "登录", "登录系统失败,没有权限或用户名、密码错误");
		}
	}

	public void SaveToFile(string fileName)
	{
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_, out fileStream_, out binaryWriter_);
			bool flag = IsHaveAdmin();
			int num = NetChromUsers.Length;
			if (!flag)
			{
				num++;
			}
			binaryWriter_.Write(num);
			for (int i = 0; i < NetChromUsers.Length; i++)
			{
				binaryWriter_.Write(NetChromUsers[i].UName);
				binaryWriter_.Write(NetChromUsers[i].Pwd);
				binaryWriter_.Write(NetChromUsers[i].TooltipTime.Date.ToString());
				binaryWriter_.Write((int)NetChromUsers[i].UserLevel);
			}
			if (!flag)
			{
				Array.Resize(ref NetChromUsers, NetChromUsers.Length + 1);
				NetChromUsers[NetChromUsers.Length - 1].UName = "admin";
				NetChromUsers[NetChromUsers.Length - 1].Pwd = "admin";
				NetChromUsers[NetChromUsers.Length - 1].TooltipTime = DateTime.MaxValue.AddYears(-1);
				NetChromUsers[NetChromUsers.Length - 1].UserLevel = User.Level.管理员;
				binaryWriter_.Write(NetChromUsers[NetChromUsers.Length - 1].UName);
				binaryWriter_.Write(NetChromUsers[NetChromUsers.Length - 1].Pwd);
				binaryWriter_.Write(NetChromUsers[NetChromUsers.Length - 1].TooltipTime.Date.ToString());
				binaryWriter_.Write((int)NetChromUsers[NetChromUsers.Length - 1].UserLevel);
			}
			binaryWriter_.Write("--End--");
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
			Text = "保存成功";
		}
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
}
