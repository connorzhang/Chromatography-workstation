using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormExplosionDelta : Form
{
	private IContainer components = null;

	private DataGridView dataGridView1;

	private DataGridViewTextBoxColumn 采样时间;

	private DataGridViewTextBoxColumn 采样地点;

	private DataGridViewTextBoxColumn 氧气含量;

	private DataGridViewTextBoxColumn 瓦斯含量;

	private DataGridViewTextBoxColumn 分析结果;

	public FormExplosionDelta()
	{
		InitializeComponent();
	}

	private void FormExplosionDelta_Paint(object sender, PaintEventArgs e)
	{
		Pen pen = new Pen(Color.Black, 1f);
		e.Graphics.DrawLine(pen, 10, 10, 10, 250);
		e.Graphics.DrawLine(pen, 10, 250, 600, 250);
		e.Graphics.DrawLine(pen, 10, 40, 600, 250);
		Pen pen2 = new Pen(Color.Red, 2f);
		e.Graphics.DrawLine(pen2, 94, 70, 43, 51);
		e.Graphics.DrawLine(pen2, 43, 152, 43, 51);
		e.Graphics.DrawLine(pen2, 43, 152, 94, 70);
		e.Graphics.DrawLine(pen, 43, 152, 10, 150);
		e.Graphics.DrawLine(pen, 43, 152, 70, 250);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormExplosionDelta));
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.采样时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.采样地点 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.氧气含量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.瓦斯含量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.分析结果 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.采样时间, this.采样地点, this.氧气含量, this.瓦斯含量, this.分析结果);
		this.dataGridView1.Location = new System.Drawing.Point(12, 323);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(582, 150);
		this.dataGridView1.TabIndex = 0;
		this.采样时间.HeaderText = "采样时间";
		this.采样时间.Name = "采样时间";
		this.采样地点.HeaderText = "采样地点";
		this.采样地点.Name = "采样地点";
		this.氧气含量.HeaderText = "氧气含量(%)";
		this.氧气含量.Name = "氧气含量";
		this.瓦斯含量.HeaderText = "瓦斯含量(%)";
		this.瓦斯含量.Name = "瓦斯含量";
		this.分析结果.HeaderText = "分析结果";
		this.分析结果.Name = "分析结果";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(606, 587);
		base.Controls.Add(this.dataGridView1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormExplosionDelta";
		this.Text = "FormExplosionDelta";
		base.Paint += new System.Windows.Forms.PaintEventHandler(FormExplosionDelta_Paint);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
	}
}
