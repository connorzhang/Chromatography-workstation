using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormSeq : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private IContainer components = null;

	private Button btnSaveSeq;

	private Button btnInsertRow;

	private Button btnDelRow;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private DataGridView dataGridView1;

	private TabPage tabPage2;

	private DataGridView dataGridView2;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private TabPage tabPage3;

	private DataGridView dataGridView3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private TabPage tabPage4;

	private DataGridView dataGridView4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	public FormSeq()
	{
		InitializeComponent();
	}

	private void btnDelRow_Click(object sender, EventArgs e)
	{
		if (tabControl1.SelectedIndex == 0)
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				int index = dataGridView1.SelectedRows[0].Index;
				dataGridView1.Rows.RemoveAt(index);
			}
		}
		else if (tabControl1.SelectedIndex == 1)
		{
			if (dataGridView2.SelectedRows.Count > 0)
			{
				int index2 = dataGridView2.SelectedRows[0].Index;
				dataGridView2.Rows.RemoveAt(index2);
			}
		}
		else if (tabControl1.SelectedIndex == 2)
		{
			if (dataGridView3.SelectedRows.Count > 0)
			{
				int index3 = dataGridView3.SelectedRows[0].Index;
				dataGridView3.Rows.RemoveAt(index3);
			}
		}
		else if (tabControl1.SelectedIndex == 3 && dataGridView4.SelectedRows.Count > 0)
		{
			int index4 = dataGridView4.SelectedRows[0].Index;
			dataGridView4.Rows.RemoveAt(index4);
		}
	}

	private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
		Rectangle bounds = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
		TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, bounds, dataGridView1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
	}

	private void btnInsertRow_Click(object sender, EventArgs e)
	{
		if (tabControl1.SelectedIndex == 0)
		{
			int rowIndex = 0;
			if (dataGridView1.SelectedRows.Count > 0)
			{
				rowIndex = dataGridView1.SelectedRows[0].Index;
				dataGridView1.Rows.Insert(rowIndex + 1);
			}
			else
			{
				dataGridView1.Rows.Insert(rowIndex);
			}
		}
		else if (tabControl1.SelectedIndex == 1)
		{
			int rowIndex2 = 0;
			if (dataGridView2.SelectedRows.Count > 0)
			{
				rowIndex2 = dataGridView2.SelectedRows[0].Index;
				dataGridView2.Rows.Insert(rowIndex2 + 1);
			}
			else
			{
				dataGridView2.Rows.Insert(rowIndex2);
			}
		}
		else if (tabControl1.SelectedIndex == 2)
		{
			int rowIndex3 = 0;
			if (dataGridView3.SelectedRows.Count > 0)
			{
				rowIndex3 = dataGridView3.SelectedRows[0].Index;
				dataGridView3.Rows.Insert(rowIndex3 + 1);
			}
			else
			{
				dataGridView3.Rows.Insert(rowIndex3);
			}
		}
		else if (tabControl1.SelectedIndex == 3)
		{
			int rowIndex4 = 0;
			if (dataGridView4.SelectedRows.Count > 0)
			{
				rowIndex4 = dataGridView4.SelectedRows[0].Index;
				dataGridView4.Rows.Insert(rowIndex4 + 1);
			}
			else
			{
				dataGridView4.Rows.Insert(rowIndex4);
			}
		}
	}

	private void btnSaveSeq_Click(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket == null)
		{
			if (DialogResult.OK == MessageBox.Show(Lang.PS("请先连接色谱仪！", "请先连接色谱仪！"), Lang.PS("提示", "Tips"), MessageBoxButtons.OKCancel))
			{
			}
			return;
		}
		Array.Resize(ref cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName1, dataGridView1.Rows.Count);
		Array.Resize(ref cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName2, dataGridView2.Rows.Count);
		Array.Resize(ref cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName3, dataGridView1.Rows.Count);
		Array.Resize(ref cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName4, dataGridView2.Rows.Count);
		cdlMgr.CurrentTcpServerSocket.cH4Param.countSeq1 = cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName1.Length;
		cdlMgr.CurrentTcpServerSocket.cH4Param.countSeq2 = cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName2.Length;
		for (int i = 0; i < dataGridView1.Rows.Count; i++)
		{
			if (dataGridView1.Rows[i].Cells[0].Value == null)
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName1[i] = "未命名1_" + i;
			}
			else
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName1[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
			}
		}
		for (int j = 0; j < dataGridView2.Rows.Count; j++)
		{
			if (dataGridView2.Rows[j].Cells[0].Value == null)
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName2[j] = "未命名2_" + j;
			}
			else
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName2[j] = dataGridView2.Rows[j].Cells[0].Value.ToString();
			}
		}
		for (int k = 0; k < dataGridView3.Rows.Count; k++)
		{
			if (dataGridView3.Rows[k].Cells[0].Value == null)
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName3[k] = "未命名2_" + k;
			}
			else
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName3[k] = dataGridView3.Rows[k].Cells[0].Value.ToString();
			}
		}
		for (int l = 0; l < dataGridView4.Rows.Count; l++)
		{
			if (dataGridView4.Rows[l].Cells[0].Value == null)
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName4[l] = "未命名2_" + l;
			}
			else
			{
				cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName4[l] = dataGridView4.Rows[l].Cells[0].Value.ToString();
			}
		}
		cdlMgr.CurrentTcpServerSocket.cH4Param.SaveParam();
		Close();
	}

	private void FormSeq_Load(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket == null)
		{
			if (DialogResult.OK == MessageBox.Show(Lang.PS("请先连接色谱仪！", "请先连接色谱仪！"), Lang.PS("提示", "Tips"), MessageBoxButtons.OKCancel))
			{
			}
			return;
		}
		for (int i = 0; i < cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName1.Length; i++)
		{
			dataGridView1.Rows.Add(cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName1[i]);
		}
		for (int j = 0; j < cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName2.Length; j++)
		{
			dataGridView2.Rows.Add(cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName2[j]);
		}
		for (int k = 0; k < cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName3.Length; k++)
		{
			dataGridView3.Rows.Add(cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName3[k]);
		}
		for (int l = 0; l < cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName4.Length; l++)
		{
			dataGridView4.Rows.Add(cdlMgr.CurrentTcpServerSocket.cH4Param.strSeqName4[l]);
		}
	}

	private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
		Rectangle bounds = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView2.RowHeadersWidth - 4, e.RowBounds.Height);
		TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView2.RowHeadersDefaultCellStyle.Font, bounds, dataGridView2.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormSeq));
		this.btnSaveSeq = new System.Windows.Forms.Button();
		this.btnInsertRow = new System.Windows.Forms.Button();
		this.btnDelRow = new System.Windows.Forms.Button();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.dataGridView3 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridView4 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		this.tabPage3.SuspendLayout();
		this.tabPage4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView4).BeginInit();
		base.SuspendLayout();
		this.btnSaveSeq.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.btnSaveSeq.Location = new System.Drawing.Point(0, 492);
		this.btnSaveSeq.Name = "btnSaveSeq";
		this.btnSaveSeq.Size = new System.Drawing.Size(379, 23);
		this.btnSaveSeq.TabIndex = 0;
		this.btnSaveSeq.Text = "保存并应用";
		this.btnSaveSeq.UseVisualStyleBackColor = true;
		this.btnSaveSeq.Click += new System.EventHandler(btnSaveSeq_Click);
		this.btnInsertRow.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.btnInsertRow.Location = new System.Drawing.Point(0, 469);
		this.btnInsertRow.Name = "btnInsertRow";
		this.btnInsertRow.Size = new System.Drawing.Size(379, 23);
		this.btnInsertRow.TabIndex = 1;
		this.btnInsertRow.Text = "插入行";
		this.btnInsertRow.UseVisualStyleBackColor = true;
		this.btnInsertRow.Click += new System.EventHandler(btnInsertRow_Click);
		this.btnDelRow.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.btnDelRow.Location = new System.Drawing.Point(0, 446);
		this.btnDelRow.Name = "btnDelRow";
		this.btnDelRow.Size = new System.Drawing.Size(379, 23);
		this.btnDelRow.TabIndex = 2;
		this.btnDelRow.Text = "删除行";
		this.btnDelRow.UseVisualStyleBackColor = true;
		this.btnDelRow.Click += new System.EventHandler(btnDelRow_Click);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(379, 446);
		this.tabControl1.TabIndex = 21;
		this.tabPage1.Controls.Add(this.dataGridView1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(371, 420);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "通道1";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.Column2);
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(365, 414);
		this.dataGridView1.TabIndex = 2;
		this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
		this.Column2.HeaderText = "名称";
		this.Column2.Name = "Column2";
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.tabPage2.Controls.Add(this.dataGridView2);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(371, 420);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "通道2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dataGridView2.AllowUserToAddRows = false;
		this.dataGridView2.AllowUserToDeleteRows = false;
		this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView2.Columns.AddRange(this.dataGridViewTextBoxColumn1);
		this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView2.Location = new System.Drawing.Point(3, 3);
		this.dataGridView2.MultiSelect = false;
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.RowTemplate.Height = 23;
		this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView2.Size = new System.Drawing.Size(365, 414);
		this.dataGridView2.TabIndex = 3;
		this.dataGridView2.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dataGridView2_RowPostPaint);
		this.dataGridViewTextBoxColumn1.HeaderText = "名称";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.tabPage3.Controls.Add(this.dataGridView3);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(371, 420);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "通道3";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.tabPage4.Controls.Add(this.dataGridView4);
		this.tabPage4.Location = new System.Drawing.Point(4, 22);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Size = new System.Drawing.Size(371, 420);
		this.tabPage4.TabIndex = 3;
		this.tabPage4.Text = "通道4";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.dataGridView3.AllowUserToAddRows = false;
		this.dataGridView3.AllowUserToDeleteRows = false;
		this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView3.Columns.AddRange(this.dataGridViewTextBoxColumn2);
		this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView3.Location = new System.Drawing.Point(3, 3);
		this.dataGridView3.MultiSelect = false;
		this.dataGridView3.Name = "dataGridView3";
		this.dataGridView3.RowTemplate.Height = 23;
		this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView3.Size = new System.Drawing.Size(365, 414);
		this.dataGridView3.TabIndex = 4;
		this.dataGridViewTextBoxColumn2.HeaderText = "名称";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridView4.AllowUserToAddRows = false;
		this.dataGridView4.AllowUserToDeleteRows = false;
		this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView4.Columns.AddRange(this.dataGridViewTextBoxColumn3);
		this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView4.Location = new System.Drawing.Point(0, 0);
		this.dataGridView4.MultiSelect = false;
		this.dataGridView4.Name = "dataGridView4";
		this.dataGridView4.RowTemplate.Height = 23;
		this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView4.Size = new System.Drawing.Size(371, 420);
		this.dataGridView4.TabIndex = 4;
		this.dataGridViewTextBoxColumn3.HeaderText = "名称";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(379, 515);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.btnDelRow);
		base.Controls.Add(this.btnInsertRow);
		base.Controls.Add(this.btnSaveSeq);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormSeq";
		this.Text = "序列进样";
		base.Load += new System.EventHandler(FormSeq_Load);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		this.tabPage3.ResumeLayout(false);
		this.tabPage4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView4).EndInit();
		base.ResumeLayout(false);
	}
}
