using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FrmSingialAdd : Form
{
	private IContainer icontainer_0;

	private Label label1;

	private Label label2;

	private TextBox textBox1;

	private TextBox textBox2;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button button4;

	private OpenFileDialog openFileDialog_0;

	private SaveFileDialog saveFileDialog_0;

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
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.openFileDialog_0 = new System.Windows.Forms.OpenFileDialog();
		this.saveFileDialog_0 = new System.Windows.Forms.SaveFileDialog();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(24, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "谱图1:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(24, 73);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "谱图2:";
		this.textBox1.Enabled = false;
		this.textBox1.Location = new System.Drawing.Point(70, 26);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(350, 21);
		this.textBox1.TabIndex = 1;
		this.textBox2.Enabled = false;
		this.textBox2.Location = new System.Drawing.Point(70, 70);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(350, 21);
		this.textBox2.TabIndex = 1;
		this.button1.Location = new System.Drawing.Point(430, 24);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(38, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "...";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(430, 68);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(38, 23);
		this.button2.TabIndex = 2;
		this.button2.Text = "...";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.Location = new System.Drawing.Point(70, 141);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 23);
		this.button3.TabIndex = 3;
		this.button3.Text = "另存为";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.button4.Location = new System.Drawing.Point(345, 141);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 3;
		this.button4.Text = "关闭";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.openFileDialog_0.FileName = "openFileDialog1";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(491, 176);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmSingialAdd";
		base.ShowIcon = false;
		this.Text = "谱图嫁接";
		base.Load += new System.EventHandler(FrmSingialAdd_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FrmSingialAdd()
	{
		InitializeComponent();
	}

	private void FrmSingialAdd_Load(object sender, EventArgs e)
	{
		openFileDialog_0.Title = Lang.PS("打开谱图", "Open Chromatogram");
		openFileDialog_0.Filter = "(*.sda)|*.sda|(所有文件)|*.*";
		openFileDialog_0.FilterIndex = 1;
		openFileDialog_0.Multiselect = false;
		saveFileDialog_0.Title = Lang.PS("保存谱图", "Save Chromatogram");
		saveFileDialog_0.Filter = "(*.sda)|*.sda|(所有文件)|*.*";
		saveFileDialog_0.FilterIndex = 1;
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "谱图嫁接";
			label1.Text = "谱图1:";
			label2.Text = "谱图2:";
			button3.Text = "另存为";
			button4.Text = "关闭";
			break;
		case SysLanguage.EN:
			Text = "Chromatogram File Grafting";
			label1.Text = "File1:";
			label2.Text = "File2:";
			button3.Text = "Save as";
			button4.Text = "Close";
			break;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0.ShowDialog(this) == DialogResult.OK)
		{
			string[] fileNames = openFileDialog_0.FileNames;
			textBox1.Text = fileNames[0];
			FrmSingialAdd_Load(null, null);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0.ShowDialog(this) == DialogResult.OK)
		{
			string[] fileNames = openFileDialog_0.FileNames;
			textBox2.Text = fileNames[0];
			FrmSingialAdd_Load(null, null);
		}
	}

	private void button4_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(textBox1.Text.Trim(), DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(textBox2.Text.Trim(), DetectorStyle.General);
		if (chromatogram != null && chromatogram2 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			if (saveFileDialog_0.ShowDialog() != DialogResult.Cancel)
			{
				chromatogram.SaveToFile(saveFileDialog_0.FileName);
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					Text = "谱图嫁接成功";
					break;
				case SysLanguage.EN:
					Text = "Chromatogram File Grafting Success";
					break;
				}
			}
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}
}
