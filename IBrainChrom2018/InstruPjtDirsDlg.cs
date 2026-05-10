using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class InstruPjtDirsDlg : LclDialog
{
	private const string string_0 = "Description";

	private const string string_1 = "PjtName";

	private const string string_2 = "工程名";

	private const string string_3 = "请输入工程名！";

	private const string string_4 = " [不更改仪器目录]";

	private const string string_5 = "请选择工程！";

	private const string string_6 = "Project Name";

	private const string string_7 = "Please input project name!";

	private const string string_8 = " [Do not change ins' dir.]";

	private const string string_9 = "Please sure select project!";

	private const string string_10 = "空";

	private LclButton btnDir;

	private IContainer icontainer_1;

	private PjtDir pjtDir_0;

	private FolderBrowserDialog folderBrowserDialog_0 = new FolderBrowserDialog();

	private LclGridView gvPjtDirs;

	public InstruPjtDirs instruPjtDirs = new InstruPjtDirs();

	private LclLabel lbDir;

	private DlgOpenStyle dlgOpenStyle_0;

	private PjtDir pjtDir_1;

	private LclTextBox tbPjtName;

	public InstruPjtDirsDlg()
	{
		InitializeComponent();
		gvPjtDirs.AddLclTextBoxColumn("PjtName", 230, StringAlignment.Near);
	}

	private void btnDir_Click(object sender, EventArgs e)
	{
		if (folderBrowserDialog_0.ShowDialog() == DialogResult.OK)
		{
			string text = folderBrowserDialog_0.SelectedPath;
			if (!text.EndsWith("\\"))
			{
				text += "\\";
			}
			method_1(text);
			if (text.Equals(pjtDir_0.instruDir))
			{
				Text = Lang.PS("另存...", "Save as...");
				return;
			}
			Text = Lang.PS("另存...", "Save as...") + Lang.PS(" [不更改仪器目录]", " [Do not change ins' dir.]");
		}
	}

	private void method_0(object sender, EventArgs e)
	{
		string text = tbPjtName.Text.Trim();
		switch (dlgOpenStyle_0)
		{
		case DlgOpenStyle.Open:
			if (gvPjtDirs.SelectedRows != null && gvPjtDirs.SelectedRows.Count == 1)
			{
				PjtDir pjtDir = gvPjtDirs.SelectedRows[0].Tag as PjtDir;
				pjtDir_1 = pjtDir;
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				MessageBox.Show(Lang.PS("请选择工程！", "Please sure select project!"), Class49.smethod_13(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			break;
		case DlgOpenStyle.SaveAs:
			if (Text.EndsWith(Lang.PS(" [不更改仪器目录]", " [Do not change ins' dir.]")))
			{
				pjtDir_1 = pjtDir_0;
				try
				{
					Class49.smethod_14(pjtDir_0.PjtFullName, lbDir.Tag.ToString() + text);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					MessageBox.Show(ex2.Message);
				}
				base.DialogResult = DialogResult.OK;
				break;
			}
			pjtDir_1 = instruPjtDirs.NewPjtDir(instrument.InstruDir, text);
			if (pjtDir_1 != null)
			{
				try
				{
					Class49.smethod_14(pjtDir_0.PjtFullName, pjtDir_1.PjtFullName);
				}
				catch (Exception ex3)
				{
					Exception ex4 = ex3;
					MessageBox.Show(ex4.Message);
				}
				base.DialogResult = DialogResult.OK;
			}
			break;
		case DlgOpenStyle.New:
			if (!text.Equals("空"))
			{
				pjtDir_1 = instruPjtDirs.NewPjtDir(instrument.InstruDir, text);
				if (pjtDir_1 != null)
				{
					base.DialogResult = DialogResult.OK;
				}
			}
			else
			{
				MessageBox.Show(Lang.PS("请输入工程名！", "Please input project name!"), Class49.smethod_13(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			break;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.gvPjtDirs = new IBrainChrom2018.LclGridView();
		this.tbPjtName = new IBrainChrom2018.LclTextBox();
		this.lbDir = new IBrainChrom2018.LclLabel();
		this.btnDir = new IBrainChrom2018.LclButton();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(21, 252);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_0);
		base.btnCancel.Location = new System.Drawing.Point(111, 252);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(203, 252);
		base.btnHelp.Text = "帮助";
		this.gvPjtDirs.AllowUserToAddRows = false;
		this.gvPjtDirs.AllowUserToDeleteRows = false;
		this.gvPjtDirs.AllowUserToResizeRows = false;
		this.gvPjtDirs.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvPjtDirs.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvPjtDirs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvPjtDirs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvPjtDirs.Location = new System.Drawing.Point(6, 10);
		this.gvPjtDirs.Name = "gvPjtDirs";
		this.gvPjtDirs.ReadOnly = true;
		this.gvPjtDirs.RowHeadersWidth = 25;
		this.gvPjtDirs.RowTemplate.Height = 16;
		this.gvPjtDirs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvPjtDirs.ShowCellToolTips = false;
		this.gvPjtDirs.Size = new System.Drawing.Size(295, 182);
		this.gvPjtDirs.TabIndex = 1;
		this.tbPjtName.Location = new System.Drawing.Point(6, 222);
		this.tbPjtName.Name = "tbPjtName";
		this.tbPjtName.Size = new System.Drawing.Size(292, 21);
		this.tbPjtName.TabIndex = 2;
		this.lbDir.Location = new System.Drawing.Point(4, 202);
		this.lbDir.Name = "lbDir";
		this.lbDir.Size = new System.Drawing.Size(259, 14);
		this.lbDir.TabIndex = 3;
		this.lbDir.Text = "lclLabel1";
		this.btnDir.Location = new System.Drawing.Point(267, 197);
		this.btnDir.Name = "btnDir";
		this.btnDir.Size = new System.Drawing.Size(31, 23);
		this.btnDir.TabIndex = 4;
		this.btnDir.Text = "...";
		this.btnDir.UseVisualStyleBackColor = true;
		this.btnDir.Click += new System.EventHandler(btnDir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(307, 287);
		base.Controls.Add(this.btnDir);
		base.Controls.Add(this.lbDir);
		base.Controls.Add(this.gvPjtDirs);
		base.Controls.Add(this.tbPjtName);
		base.Name = "InstruPjtDirsDlg";
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(this.tbPjtName, 0);
		base.Controls.SetChildIndex(this.gvPjtDirs, 0);
		base.Controls.SetChildIndex(this.lbDir, 0);
		base.Controls.SetChildIndex(this.btnDir, 0);
		((System.ComponentModel.ISupportInitialize)this.gvPjtDirs).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		gvPjtDirs.Columns["PjtName"].HeaderText = Lang.PS("工程名", "Project Name");
	}

	public PjtDir ShowDialog(PjtDir curPjtDir, string title, bool enableBtnDir, bool showTextBox, DlgOpenStyle openStyle)
	{
		Text = title;
		method_1((curPjtDir != null) ? curPjtDir.instruDir : "");
		btnDir.Enabled = enableBtnDir;
		tbPjtName.Visible = showTextBox;
		if (showTextBox)
		{
			tbPjtName.Focus();
		}
		tbPjtName.Text = ((openStyle == DlgOpenStyle.New) ? "空" : ((curPjtDir != null) ? curPjtDir.projectName : ""));
		string text = ((curPjtDir != null) ? curPjtDir.projectName : "");
		DirectoryInfo[] directories = new DirectoryInfo(instrument.InstruDir).GetDirectories();
		int num = directories.Length;
		Array.Resize(ref instruPjtDirs.pjtDirs, num);
		for (int i = 0; i < num; i++)
		{
			if (instruPjtDirs.pjtDirs[i] == null)
			{
				instruPjtDirs.pjtDirs[i] = new PjtDir(instrument.InstruDir, directories[i].Name);
				continue;
			}
			instruPjtDirs.pjtDirs[i].instruDir = instrument.InstruDir;
			instruPjtDirs.pjtDirs[i].projectName = directories[i].Name;
		}
		if (text != "")
		{
			for (int j = 0; j < num; j++)
			{
				if (instruPjtDirs.pjtDirs[j].projectName == text)
				{
					curPjtDir = instruPjtDirs.pjtDirs[j];
					break;
				}
			}
		}
		gvPjtDirs.RowCount = num;
		for (int k = 0; k < num; k++)
		{
			gvPjtDirs.Rows[k].Cells[0].Value = instruPjtDirs.pjtDirs[k].projectName;
			gvPjtDirs.Rows[k].Tag = instruPjtDirs.pjtDirs[k];
			gvPjtDirs.Rows[k].Selected = false;
		}
		pjtDir_0 = curPjtDir;
		dlgOpenStyle_0 = openStyle;
		pjtDir_1 = null;
		ShowDialog();
		return pjtDir_1;
	}

	private void method_1(string string_11)
	{
		lbDir.Tag = string_11;
		Graphics graphics = Graphics.FromHwnd(lbDir.Handle);
		SizeF sizeF = graphics.MeasureString(string_11, lbDir.Font);
		if (sizeF.Width < (float)(lbDir.Width - 4))
		{
			lbDir.Text = string_11;
		}
		else
		{
			string text = string_11.Substring(0, 2);
			string text2 = string_11.Remove(0, 2);
			string text3 = "";
			while (sizeF.Width >= (float)(lbDir.Width - 4))
			{
				text2 = text2.Remove(0, 1);
				text3 = text + "..." + text2;
				sizeF = graphics.MeasureString(text3, lbDir.Font);
			}
			lbDir.Text = text3;
		}
		graphics.Dispose();
	}
}
