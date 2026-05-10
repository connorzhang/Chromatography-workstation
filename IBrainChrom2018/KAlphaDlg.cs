using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class KAlphaDlg : LclDialog
{
	private const string string_0 = "Alpha";

	private const string string_1 = "K";

	private const string string_2 = "Polymer";

	private const string string_3 = "Remark";

	private const string string_4 = "Solvent";

	private const string string_5 = "Temperature";

	private const string string_6 = ".gka";

	private const string string_7 = "聚合物";

	private const string string_8 = "备注";

	private const string string_9 = "溶剂";

	private const string string_10 = "温度[℃]";

	private const string string_11 = "K, Alpha 系数列表";

	private const string string_12 = "Polymer";

	private const string string_13 = "Remark";

	private const string string_14 = "Solvent";

	private const string string_15 = "Temperature[℃]";

	private const string string_16 = "List of K, Alpha coefficients";

	private const string string_17 = "null";

	private const string string_18 = "K & Alpha";

	public static float alpha = 0f;

	private LclButton btnNew;

	private LclButton btnOpen;

	private LclButton btnSave;

	private LclButton btnSaveAs;

	private string string_19;

	private IContainer icontainer_1;

	public static string fileName = "";

	private LclGridView gvKAlpha;

	public static float float_0 = 0f;

	private LclLabel lbExpress;

	private OpenFileDialog openFileDialog_0 = new OpenFileDialog();

	private SaveFileDialog saveFileDialog_0 = new SaveFileDialog();

	public KAlphaDlg()
	{
		InitializeComponent();
		gvKAlpha.AddLclTextBoxColumn("Polymer", 90, StringAlignment.Near);
		gvKAlpha.AddLclTextBoxColumn("Solvent", 90, StringAlignment.Near);
		gvKAlpha.AddLclTextBoxColumn("Temperature", 100, StringAlignment.Center);
		gvKAlpha.AddLclTextBoxColumn("K", 80, StringAlignment.Center);
		gvKAlpha.AddLclTextBoxColumn("Alpha", 80, StringAlignment.Center);
		gvKAlpha.AddLclTextBoxColumn("Remark", 120, StringAlignment.Near);
		gvKAlpha.ColumnHeadersHeight = 18;
		gvKAlpha.Columns["K"].HeaderText = "K[dL/g*10^3]";
		gvKAlpha.Columns["Alpha"].HeaderText = "Alpha";
		gvKAlpha.MultiSelect = false;
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		gvKAlpha.Rows.Clear();
		fileName = "";
		Text = "K & Alpha";
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			method_1(openFileDialog_0.FileName);
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (fileName == "")
		{
			btnSaveAs_Click(null, null);
		}
		else
		{
			method_2(fileName);
		}
	}

	private void btnSaveAs_Click(object sender, EventArgs e)
	{
		if (saveFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			try
			{
				method_2(saveFileDialog_0.FileName);
				fileName = saveFileDialog_0.FileName;
				Text = "K & Alpha: " + fileName;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}

	public void DefaultFile()
	{
		fileName = string_19 + "KAlpha.gka";
		if (File.Exists(fileName))
		{
			method_1(fileName);
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
		this.lbExpress = new IBrainChrom2018.LclLabel();
		this.gvKAlpha = new IBrainChrom2018.LclGridView();
		this.btnNew = new IBrainChrom2018.LclButton();
		this.btnOpen = new IBrainChrom2018.LclButton();
		this.btnSave = new IBrainChrom2018.LclButton();
		this.btnSaveAs = new IBrainChrom2018.LclButton();
		((System.ComponentModel.ISupportInitialize)this.gvKAlpha).BeginInit();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(623, 25);
		base.btnCancel.Location = new System.Drawing.Point(623, 54);
		base.btnHelp.Location = new System.Drawing.Point(623, 83);
		this.lbExpress.AutoSize = true;
		this.lbExpress.Location = new System.Drawing.Point(12, 8);
		this.lbExpress.Name = "lbExpress";
		this.lbExpress.Size = new System.Drawing.Size(59, 12);
		this.lbExpress.TabIndex = 1;
		this.lbExpress.Text = "lclLabel1";
		this.gvKAlpha.AllowUserToResizeRows = false;
		this.gvKAlpha.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvKAlpha.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvKAlpha.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvKAlpha.Location = new System.Drawing.Point(5, 24);
		this.gvKAlpha.Name = "gvKAlpha";
		this.gvKAlpha.RowHeadersWidth = 25;
		this.gvKAlpha.RowTemplate.Height = 16;
		this.gvKAlpha.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvKAlpha.ShowCellToolTips = false;
		this.gvKAlpha.Size = new System.Drawing.Size(610, 228);
		this.gvKAlpha.TabIndex = 2;
		this.btnNew.Location = new System.Drawing.Point(623, 129);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(74, 23);
		this.btnNew.TabIndex = 3;
		this.btnNew.Text = "lclButton1";
		this.btnNew.UseVisualStyleBackColor = true;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.btnOpen.Location = new System.Drawing.Point(623, 158);
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(74, 23);
		this.btnOpen.TabIndex = 3;
		this.btnOpen.Text = "lclButton1";
		this.btnOpen.UseVisualStyleBackColor = true;
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.btnSave.Location = new System.Drawing.Point(623, 187);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(74, 23);
		this.btnSave.TabIndex = 3;
		this.btnSave.Text = "lclButton1";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnSaveAs.Location = new System.Drawing.Point(624, 216);
		this.btnSaveAs.Name = "btnSaveAs";
		this.btnSaveAs.Size = new System.Drawing.Size(74, 23);
		this.btnSaveAs.TabIndex = 3;
		this.btnSaveAs.Text = "lclButton1";
		this.btnSaveAs.UseVisualStyleBackColor = true;
		this.btnSaveAs.Click += new System.EventHandler(btnSaveAs_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(708, 264);
		base.Controls.Add(this.lbExpress);
		base.Controls.Add(this.gvKAlpha);
		base.Controls.Add(this.btnSaveAs);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.btnOpen);
		base.Name = "KAlphaDlg";
		this.Text = "K & Alpha";
		base.Load += new System.EventHandler(KAlphaDlg_Load);
		base.Controls.SetChildIndex(this.btnOpen, 0);
		base.Controls.SetChildIndex(this.btnSave, 0);
		base.Controls.SetChildIndex(this.btnNew, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.btnSaveAs, 0);
		base.Controls.SetChildIndex(this.gvKAlpha, 0);
		base.Controls.SetChildIndex(this.lbExpress, 0);
		((System.ComponentModel.ISupportInitialize)this.gvKAlpha).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void KAlphaDlg_Load(object sender, EventArgs e)
	{
		string_19 = ResourceImageLoad.ExePath() + "Common\\";
		DirectoryInfo directoryInfo = new DirectoryInfo(string_19);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		OpenFileDialog openFileDialog = openFileDialog_0;
		string initialDirectory = (saveFileDialog_0.InitialDirectory = string_19);
		openFileDialog.InitialDirectory = initialDirectory;
		OpenFileDialog openFileDialog2 = openFileDialog_0;
		initialDirectory = (saveFileDialog_0.Filter = Class49.MakeFileFilter(".gka"));
		openFileDialog2.Filter = initialDirectory;
	}

	private void method_0(string string_20)
	{
		FileStream fileStream = new FileInfo(string_20).Open(FileMode.Open);
		BinaryReader binaryReader = new BinaryReader(fileStream);
		try
		{
			gvKAlpha.RowCount = binaryReader.ReadInt32();
			for (int i = 0; i < gvKAlpha.RowCount - 1; i++)
			{
				for (int j = 0; j < gvKAlpha.ColumnCount; j++)
				{
					string text = binaryReader.ReadString();
					if (text.Equals("null"))
					{
						gvKAlpha.Rows[i].Cells[j].Value = null;
					}
					else
					{
						gvKAlpha.Rows[i].Cells[j].Value = text;
					}
				}
			}
		}
		finally
		{
			binaryReader.Close();
			fileStream.Close();
		}
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			lbExpress.Text = "K, Alpha 系数列表";
			btnNew.Text = "新建";
			btnOpen.Text = "打开...";
			btnSave.Text = "保存";
			btnSaveAs.Text = "另存...";
			gvKAlpha.Columns["Polymer"].HeaderText = "聚合物";
			gvKAlpha.Columns["Solvent"].HeaderText = "溶剂";
			gvKAlpha.Columns["Temperature"].HeaderText = "温度[℃]";
			gvKAlpha.Columns["Remark"].HeaderText = "备注";
			break;
		case SysLanguage.EN:
			lbExpress.Text = "List of K, Alpha coefficients";
			btnNew.Text = "New";
			btnOpen.Text = "Open...";
			btnSave.Text = "Save";
			btnSaveAs.Text = "Save as...";
			gvKAlpha.Columns["Polymer"].HeaderText = "Polymer";
			gvKAlpha.Columns["Solvent"].HeaderText = "Solvent";
			gvKAlpha.Columns["Temperature"].HeaderText = "Temperature[℃]";
			gvKAlpha.Columns["Remark"].HeaderText = "Remark";
			break;
		}
	}

	private void method_1(string string_20)
	{
		try
		{
			method_0(string_20);
			fileName = string_20;
			Text = "K & Alpha: " + string_20;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void method_2(string string_20)
	{
		FileStream fileStream = new FileInfo(string_20).Open(FileMode.OpenOrCreate);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		try
		{
			binaryWriter.Write(gvKAlpha.RowCount);
			for (int i = 0; i < gvKAlpha.RowCount - 1; i++)
			{
				for (int j = 0; j < gvKAlpha.ColumnCount; j++)
				{
					if (gvKAlpha.Rows[i].Cells[j].Value == null)
					{
						binaryWriter.Write("null");
					}
					else
					{
						binaryWriter.Write(gvKAlpha.Rows[i].Cells[j].Value.ToString());
					}
				}
			}
		}
		finally
		{
			binaryWriter.Close();
			fileStream.Close();
		}
	}

	public new DialogResult ShowDialog()
	{
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			if (gvKAlpha.SelectedRows != null && gvKAlpha.SelectedRows.Count != 0)
			{
				int index = gvKAlpha.Columns["K"].Index;
				float_0 = Class49.String2Float(gvKAlpha.SelectedRows[0].Cells[index].Value, 0f);
				int index2 = gvKAlpha.Columns["Alpha"].Index;
				alpha = Class49.String2Float(gvKAlpha.SelectedRows[0].Cells[index2].Value, 0f);
				return dialogResult;
			}
			dialogResult = DialogResult.Cancel;
			float_0 = 0f;
			alpha = 0f;
		}
		return dialogResult;
	}
}
