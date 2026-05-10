using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

internal class NoQYAboutBox : Form
{
	private IContainer icontainer_0;

	private TableLayoutPanel tableLayoutPanel;

	private PictureBox logoPictureBox;

	private Label labelProductName;

	private Label labelVersion;

	private Label labelCopyright;

	private TextBox textBoxDescription;

	private TextBox tbUpdateList;

	private Button okButton;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.NoQYAboutBox));
		this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
		this.logoPictureBox = new System.Windows.Forms.PictureBox();
		this.labelProductName = new System.Windows.Forms.Label();
		this.labelVersion = new System.Windows.Forms.Label();
		this.labelCopyright = new System.Windows.Forms.Label();
		this.textBoxDescription = new System.Windows.Forms.TextBox();
		this.okButton = new System.Windows.Forms.Button();
		this.tbUpdateList = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.logoPictureBox).BeginInit();
		base.SuspendLayout();
		this.tableLayoutPanel.ColumnCount = 2;
		this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33f));
		this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67f));
		this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
		this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
		this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
		this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
		this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 3);
		this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
		this.tableLayoutPanel.Controls.Add(this.tbUpdateList, 1, 4);
		this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel.Location = new System.Drawing.Point(9, 8);
		this.tableLayoutPanel.Name = "tableLayoutPanel";
		this.tableLayoutPanel.RowCount = 6;
		this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.142857f));
		this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.142857f));
		this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.142857f));
		this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.61225f));
		this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.45485f));
		this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.04013f));
		this.tableLayoutPanel.Size = new System.Drawing.Size(419, 299);
		this.tableLayoutPanel.TabIndex = 0;
		this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
		this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
		this.logoPictureBox.Name = "logoPictureBox";
		this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
		this.logoPictureBox.Size = new System.Drawing.Size(132, 293);
		this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.logoPictureBox.TabIndex = 12;
		this.logoPictureBox.TabStop = false;
		this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
		this.labelProductName.Location = new System.Drawing.Point(144, 0);
		this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelProductName.MaximumSize = new System.Drawing.Size(0, 16);
		this.labelProductName.Name = "labelProductName";
		this.labelProductName.Size = new System.Drawing.Size(272, 16);
		this.labelProductName.TabIndex = 19;
		this.labelProductName.Text = "产品名称";
		this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.labelVersion.Location = new System.Drawing.Point(144, 21);
		this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelVersion.MaximumSize = new System.Drawing.Size(0, 16);
		this.labelVersion.Name = "labelVersion";
		this.labelVersion.Size = new System.Drawing.Size(272, 16);
		this.labelVersion.TabIndex = 0;
		this.labelVersion.Text = "版本";
		this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
		this.labelCopyright.Location = new System.Drawing.Point(144, 42);
		this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 16);
		this.labelCopyright.Name = "labelCopyright";
		this.labelCopyright.Size = new System.Drawing.Size(272, 16);
		this.labelCopyright.TabIndex = 21;
		this.labelCopyright.Text = "版权";
		this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textBoxDescription.Location = new System.Drawing.Point(144, 66);
		this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
		this.textBoxDescription.Multiline = true;
		this.textBoxDescription.Name = "textBoxDescription";
		this.textBoxDescription.ReadOnly = true;
		this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.textBoxDescription.Size = new System.Drawing.Size(272, 85);
		this.textBoxDescription.TabIndex = 23;
		this.textBoxDescription.TabStop = false;
		this.textBoxDescription.Text = "说明";
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.okButton.Location = new System.Drawing.Point(341, 265);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 31);
		this.okButton.TabIndex = 24;
		this.okButton.Text = "确定(&O)";
		this.tbUpdateList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tbUpdateList.Location = new System.Drawing.Point(144, 157);
		this.tbUpdateList.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
		this.tbUpdateList.Multiline = true;
		this.tbUpdateList.Name = "tbUpdateList";
		this.tbUpdateList.ReadOnly = true;
		this.tbUpdateList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.tbUpdateList.Size = new System.Drawing.Size(272, 102);
		this.tbUpdateList.TabIndex = 26;
		this.tbUpdateList.TabStop = false;
		this.tbUpdateList.Text = "升级说明";
		base.AcceptButton = this.okButton;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(437, 315);
		base.Controls.Add(this.tableLayoutPanel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "NoQYAboutBox";
		base.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "NoQYAboutBox";
		this.tableLayoutPanel.ResumeLayout(false);
		this.tableLayoutPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.logoPictureBox).EndInit();
		base.ResumeLayout(false);
	}

	public NoQYAboutBox()
	{
		InitializeComponent();
		Text = Lang.PS("关于") + " " + AssemblyInfoCfg.Title + " V" + AssemblyInfoCfg.SoftVersion();
		labelProductName.Text = Lang.PS("产品名称:") + " " + AssemblyInfoCfg.Title;
		labelVersion.Text = Lang.PS("版本:") + " V" + AssemblyInfoCfg.SoftVersion() + Lang.PS(" 更新时间:", "Update time:") + AssemblyInfoCfg.ExeFileVersion();
		labelCopyright.Text = Lang.PS("版权:") + "  " + AssemblyInfoCfg.Corp;
		textBoxDescription.Text = Lang.PS("警告") + "\r\n" + Lang.PS("此计算机程序受版权法和国际条约的保护。未经授权对本程序或其任意部分进行任何复制或分发，都将导致严厉的民事和刑事处罚，将按法律所允许的最大限度予以起诉。");
		okButton.Text = Lang.PS("确定", "OK");
		string path = Lang.PS("Doc\\1.UpgradeList.txt", "Doc\\1.UpgradeListEn.txt");
		if (File.Exists(path))
		{
			tbUpdateList.Text = File.ReadAllText(path);
		}
	}

	public string method_0()
	{
		object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), inherit: false);
		if (customAttributes.Length != 0)
		{
			AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
			if (assemblyTitleAttribute.Title != "")
			{
				return assemblyTitleAttribute.Title;
			}
		}
		return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
	}

	public string method_1()
	{
		return Assembly.GetExecutingAssembly().GetName().Version.ToString();
	}

	public string method_2()
	{
		object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			return "";
		}
		return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
	}

	public string method_3()
	{
		object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			return "";
		}
		return ((AssemblyProductAttribute)customAttributes[0]).Product;
	}

	public string method_4()
	{
		object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			return "";
		}
		return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
	}

	public string method_5()
	{
		object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			return "";
		}
		return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
	}
}
