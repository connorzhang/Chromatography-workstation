using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class HistoryCtrl : UserControl
{
	public static HistoryCtrl selfCtrl;

	private IContainer components = null;

	private GroupBox groupBox1;

	public DataGridView dataGridView1;

	private SplitContainer splitContainer1;

	public DataGridView dataGridView2;

	public HistoryCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
	}

	public void initForm()
	{
		try
		{
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		}
		catch (Exception)
		{
		}
	}

	public void updataGridView()
	{
		try
		{
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		}
		catch (Exception)
		{
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.splitContainer1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(369, 494);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 17);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
		this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
		this.splitContainer1.Size = new System.Drawing.Size(363, 474);
		this.splitContainer1.SplitterDistance = 236;
		this.splitContainer1.TabIndex = 62;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(0, 0);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(363, 236);
		this.dataGridView1.TabIndex = 61;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView2.Location = new System.Drawing.Point(0, 0);
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.RowTemplate.Height = 23;
		this.dataGridView2.Size = new System.Drawing.Size(363, 234);
		this.dataGridView2.TabIndex = 62;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "HistoryCtrl";
		base.Size = new System.Drawing.Size(369, 494);
		this.groupBox1.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		base.ResumeLayout(false);
	}
}
