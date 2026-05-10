using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Frmmultivalve : Form
{
	private ChromFormInterface formMain_0;

	private IContainer icontainer_0;

	private Button button4;

	private Button button1;

	public CheckBox checkBox1;

	public CheckBox checkBox2;

	public CheckBox checkBox3;

	public CheckBox checkBox4;

	public CheckBox checkBox5;

	public CheckBox checkBox6;

	public CheckBox checkBox7;

	public CheckBox checkBox8;

	public CheckBox checkBox9;

	public CheckBox checkBox10;

	public CheckBox checkBox11;

	public CheckBox checkBox12;

	public CheckBox checkBox13;

	public CheckBox checkBox14;

	public CheckBox checkBox15;

	public CheckBox checkBox16;

	public CheckBox checkBox17;

	public CheckBox checkBox18;

	public CheckBox checkBox19;

	public CheckBox checkBox20;

	public CheckBox checkBox21;

	public CheckBox checkBox22;

	public CheckBox checkBox23;

	public CheckBox checkBox24;

	public CheckBox checkBox25;

	public CheckBox checkBox26;

	public CheckBox checkBox27;

	public CheckBox checkBox28;

	public CheckBox checkBox29;

	public CheckBox checkBox30;

	public CheckBox checkBox31;

	public CheckBox checkBox32;

	public Frmmultivalve()
	{
		InitializeComponent();
	}

	private void method_0()
	{
		Text = Lang.PS("多位阀控制", " multiposition valve Control");
		button1.Text = Lang.PS("查询", "query");
		button4.Text = Lang.PS("设定", "set");
	}

	private void Frmmultivalve_Load(object sender, EventArgs e)
	{
		method_0();
	}

	public void Init(ChromFormInterface Fm)
	{
		formMain_0 = Fm;
	}

	public void button1_Click(object sender, EventArgs e)
	{
		formMain_0.MultiValveselect();
	}

	public void button4_Click(object sender, EventArgs e)
	{
		formMain_0.MultiValveSet();
	}

	public void setCheckBox(byte[] EnableValue)
	{
		byte b = EnableValue[0];
		byte b2 = EnableValue[1];
		byte b3 = EnableValue[2];
		byte b4 = EnableValue[3];
		checkBox1.Checked = (b & 0x80) == 128;
		checkBox2.Checked = (b & 0x40) == 64;
		checkBox3.Checked = (b & 0x20) == 32;
		checkBox4.Checked = (b & 0x10) == 16;
		checkBox5.Checked = (b & 8) == 8;
		checkBox6.Checked = (b & 4) == 4;
		checkBox7.Checked = (b & 2) == 2;
		checkBox8.Checked = (b & 1) == 1;
		checkBox9.Checked = (b2 & 0x80) == 128;
		checkBox10.Checked = (b2 & 0x40) == 64;
		checkBox11.Checked = (b2 & 0x20) == 32;
		checkBox12.Checked = (b2 & 0x10) == 16;
		checkBox13.Checked = (b2 & 8) == 8;
		checkBox14.Checked = (b2 & 4) == 4;
		checkBox15.Checked = (b2 & 2) == 2;
		checkBox16.Checked = (b2 & 1) == 1;
		checkBox17.Checked = (b3 & 0x80) == 128;
		checkBox18.Checked = (b3 & 0x40) == 64;
		checkBox19.Checked = (b3 & 0x20) == 32;
		checkBox20.Checked = (b3 & 0x10) == 16;
		checkBox21.Checked = (b3 & 8) == 8;
		checkBox22.Checked = (b3 & 4) == 4;
		checkBox23.Checked = (b3 & 2) == 2;
		checkBox24.Checked = (b3 & 1) == 1;
		checkBox25.Checked = (b4 & 0x80) == 128;
		checkBox26.Checked = (b4 & 0x40) == 64;
		checkBox27.Checked = (b4 & 0x20) == 32;
		checkBox28.Checked = (b4 & 0x10) == 16;
		checkBox29.Checked = (b4 & 8) == 8;
		checkBox30.Checked = (b4 & 4) == 4;
		checkBox31.Checked = (b4 & 2) == 2;
		checkBox32.Checked = (b4 & 1) == 1;
	}

	public byte[] GetCheckBox2Byte()
	{
		byte[] array = new byte[4];
		string text = (checkBox1.Checked ? "1" : "0");
		text += (checkBox2.Checked ? "1" : "0");
		text += (checkBox3.Checked ? "1" : "0");
		text += (checkBox4.Checked ? "1" : "0");
		text += (checkBox5.Checked ? "1" : "0");
		text += (checkBox6.Checked ? "1" : "0");
		text += (checkBox7.Checked ? "1" : "0");
		text += (checkBox8.Checked ? "1" : "0");
		array[0] = (byte)Convert.ToInt32(text, 2);
		text = (checkBox9.Checked ? "1" : "0");
		text += (checkBox10.Checked ? "1" : "0");
		text += (checkBox11.Checked ? "1" : "0");
		text += (checkBox12.Checked ? "1" : "0");
		text += (checkBox13.Checked ? "1" : "0");
		text += (checkBox14.Checked ? "1" : "0");
		text += (checkBox15.Checked ? "1" : "0");
		text += (checkBox16.Checked ? "1" : "0");
		array[1] = (byte)Convert.ToInt32(text, 2);
		text = (checkBox17.Checked ? "1" : "0");
		text += (checkBox18.Checked ? "1" : "0");
		text += (checkBox19.Checked ? "1" : "0");
		text += (checkBox20.Checked ? "1" : "0");
		text += (checkBox21.Checked ? "1" : "0");
		text += (checkBox22.Checked ? "1" : "0");
		text += (checkBox23.Checked ? "1" : "0");
		text += (checkBox24.Checked ? "1" : "0");
		array[2] = (byte)Convert.ToInt32(text, 2);
		text = (checkBox25.Checked ? "1" : "0");
		text += (checkBox26.Checked ? "1" : "0");
		text += (checkBox27.Checked ? "1" : "0");
		text += (checkBox28.Checked ? "1" : "0");
		text += (checkBox29.Checked ? "1" : "0");
		text += (checkBox30.Checked ? "1" : "0");
		text += (checkBox31.Checked ? "1" : "0");
		text += (checkBox32.Checked ? "1" : "0");
		array[3] = (byte)Convert.ToInt32(text, 2);
		return array;
	}

	private void Frmmultivalve_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
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
		this.button4 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.checkBox9 = new System.Windows.Forms.CheckBox();
		this.checkBox10 = new System.Windows.Forms.CheckBox();
		this.checkBox11 = new System.Windows.Forms.CheckBox();
		this.checkBox12 = new System.Windows.Forms.CheckBox();
		this.checkBox13 = new System.Windows.Forms.CheckBox();
		this.checkBox14 = new System.Windows.Forms.CheckBox();
		this.checkBox15 = new System.Windows.Forms.CheckBox();
		this.checkBox16 = new System.Windows.Forms.CheckBox();
		this.checkBox17 = new System.Windows.Forms.CheckBox();
		this.checkBox18 = new System.Windows.Forms.CheckBox();
		this.checkBox19 = new System.Windows.Forms.CheckBox();
		this.checkBox20 = new System.Windows.Forms.CheckBox();
		this.checkBox21 = new System.Windows.Forms.CheckBox();
		this.checkBox22 = new System.Windows.Forms.CheckBox();
		this.checkBox23 = new System.Windows.Forms.CheckBox();
		this.checkBox24 = new System.Windows.Forms.CheckBox();
		this.checkBox25 = new System.Windows.Forms.CheckBox();
		this.checkBox26 = new System.Windows.Forms.CheckBox();
		this.checkBox27 = new System.Windows.Forms.CheckBox();
		this.checkBox28 = new System.Windows.Forms.CheckBox();
		this.checkBox29 = new System.Windows.Forms.CheckBox();
		this.checkBox30 = new System.Windows.Forms.CheckBox();
		this.checkBox31 = new System.Windows.Forms.CheckBox();
		this.checkBox32 = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.button4.ForeColor = System.Drawing.Color.Blue;
		this.button4.Location = new System.Drawing.Point(202, 111);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 6;
		this.button4.Text = "设定";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button1.ForeColor = System.Drawing.Color.Blue;
		this.button1.Location = new System.Drawing.Point(4, 111);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 7;
		this.button1.Text = "查询";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(13, 23);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(30, 16);
		this.checkBox1.TabIndex = 8;
		this.checkBox1.Text = "1";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Location = new System.Drawing.Point(46, 23);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(30, 16);
		this.checkBox2.TabIndex = 8;
		this.checkBox2.Text = "2";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(79, 23);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(30, 16);
		this.checkBox3.TabIndex = 8;
		this.checkBox3.Text = "3";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Location = new System.Drawing.Point(112, 23);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(30, 16);
		this.checkBox4.TabIndex = 8;
		this.checkBox4.Text = "4";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Location = new System.Drawing.Point(145, 23);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(30, 16);
		this.checkBox5.TabIndex = 8;
		this.checkBox5.Text = "5";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox6.AutoSize = true;
		this.checkBox6.Location = new System.Drawing.Point(178, 23);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(30, 16);
		this.checkBox6.TabIndex = 8;
		this.checkBox6.Text = "6";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox7.AutoSize = true;
		this.checkBox7.Location = new System.Drawing.Point(211, 23);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(30, 16);
		this.checkBox7.TabIndex = 8;
		this.checkBox7.Text = "7";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox8.AutoSize = true;
		this.checkBox8.Location = new System.Drawing.Point(244, 23);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(30, 16);
		this.checkBox8.TabIndex = 8;
		this.checkBox8.Text = "8";
		this.checkBox8.UseVisualStyleBackColor = true;
		this.checkBox9.AutoSize = true;
		this.checkBox9.Location = new System.Drawing.Point(13, 45);
		this.checkBox9.Name = "checkBox9";
		this.checkBox9.Size = new System.Drawing.Size(30, 16);
		this.checkBox9.TabIndex = 8;
		this.checkBox9.Text = "9";
		this.checkBox9.UseVisualStyleBackColor = true;
		this.checkBox10.AutoSize = true;
		this.checkBox10.Location = new System.Drawing.Point(46, 45);
		this.checkBox10.Name = "checkBox10";
		this.checkBox10.Size = new System.Drawing.Size(36, 16);
		this.checkBox10.TabIndex = 8;
		this.checkBox10.Text = "10";
		this.checkBox10.UseVisualStyleBackColor = true;
		this.checkBox11.AutoSize = true;
		this.checkBox11.Location = new System.Drawing.Point(79, 45);
		this.checkBox11.Name = "checkBox11";
		this.checkBox11.Size = new System.Drawing.Size(36, 16);
		this.checkBox11.TabIndex = 8;
		this.checkBox11.Text = "11";
		this.checkBox11.UseVisualStyleBackColor = true;
		this.checkBox12.AutoSize = true;
		this.checkBox12.Location = new System.Drawing.Point(112, 45);
		this.checkBox12.Name = "checkBox12";
		this.checkBox12.Size = new System.Drawing.Size(36, 16);
		this.checkBox12.TabIndex = 8;
		this.checkBox12.Text = "12";
		this.checkBox12.UseVisualStyleBackColor = true;
		this.checkBox13.AutoSize = true;
		this.checkBox13.Location = new System.Drawing.Point(145, 45);
		this.checkBox13.Name = "checkBox13";
		this.checkBox13.Size = new System.Drawing.Size(36, 16);
		this.checkBox13.TabIndex = 8;
		this.checkBox13.Text = "13";
		this.checkBox13.UseVisualStyleBackColor = true;
		this.checkBox14.AutoSize = true;
		this.checkBox14.Location = new System.Drawing.Point(178, 45);
		this.checkBox14.Name = "checkBox14";
		this.checkBox14.Size = new System.Drawing.Size(36, 16);
		this.checkBox14.TabIndex = 8;
		this.checkBox14.Text = "14";
		this.checkBox14.UseVisualStyleBackColor = true;
		this.checkBox15.AutoSize = true;
		this.checkBox15.Location = new System.Drawing.Point(211, 45);
		this.checkBox15.Name = "checkBox15";
		this.checkBox15.Size = new System.Drawing.Size(36, 16);
		this.checkBox15.TabIndex = 8;
		this.checkBox15.Text = "15";
		this.checkBox15.UseVisualStyleBackColor = true;
		this.checkBox16.AutoSize = true;
		this.checkBox16.Location = new System.Drawing.Point(244, 45);
		this.checkBox16.Name = "checkBox16";
		this.checkBox16.Size = new System.Drawing.Size(36, 16);
		this.checkBox16.TabIndex = 8;
		this.checkBox16.Text = "16";
		this.checkBox16.UseVisualStyleBackColor = true;
		this.checkBox17.AutoSize = true;
		this.checkBox17.Location = new System.Drawing.Point(13, 67);
		this.checkBox17.Name = "checkBox17";
		this.checkBox17.Size = new System.Drawing.Size(36, 16);
		this.checkBox17.TabIndex = 8;
		this.checkBox17.Text = "17";
		this.checkBox17.UseVisualStyleBackColor = true;
		this.checkBox18.AutoSize = true;
		this.checkBox18.Location = new System.Drawing.Point(46, 67);
		this.checkBox18.Name = "checkBox18";
		this.checkBox18.Size = new System.Drawing.Size(36, 16);
		this.checkBox18.TabIndex = 8;
		this.checkBox18.Text = "18";
		this.checkBox18.UseVisualStyleBackColor = true;
		this.checkBox19.AutoSize = true;
		this.checkBox19.Location = new System.Drawing.Point(79, 67);
		this.checkBox19.Name = "checkBox19";
		this.checkBox19.Size = new System.Drawing.Size(36, 16);
		this.checkBox19.TabIndex = 8;
		this.checkBox19.Text = "19";
		this.checkBox19.UseVisualStyleBackColor = true;
		this.checkBox20.AutoSize = true;
		this.checkBox20.Location = new System.Drawing.Point(112, 67);
		this.checkBox20.Name = "checkBox20";
		this.checkBox20.Size = new System.Drawing.Size(36, 16);
		this.checkBox20.TabIndex = 8;
		this.checkBox20.Text = "20";
		this.checkBox20.UseVisualStyleBackColor = true;
		this.checkBox21.AutoSize = true;
		this.checkBox21.Location = new System.Drawing.Point(145, 67);
		this.checkBox21.Name = "checkBox21";
		this.checkBox21.Size = new System.Drawing.Size(36, 16);
		this.checkBox21.TabIndex = 8;
		this.checkBox21.Text = "21";
		this.checkBox21.UseVisualStyleBackColor = true;
		this.checkBox22.AutoSize = true;
		this.checkBox22.Location = new System.Drawing.Point(178, 67);
		this.checkBox22.Name = "checkBox22";
		this.checkBox22.Size = new System.Drawing.Size(36, 16);
		this.checkBox22.TabIndex = 8;
		this.checkBox22.Text = "22";
		this.checkBox22.UseVisualStyleBackColor = true;
		this.checkBox23.AutoSize = true;
		this.checkBox23.Location = new System.Drawing.Point(211, 67);
		this.checkBox23.Name = "checkBox23";
		this.checkBox23.Size = new System.Drawing.Size(36, 16);
		this.checkBox23.TabIndex = 8;
		this.checkBox23.Text = "23";
		this.checkBox23.UseVisualStyleBackColor = true;
		this.checkBox24.AutoSize = true;
		this.checkBox24.Location = new System.Drawing.Point(244, 67);
		this.checkBox24.Name = "checkBox24";
		this.checkBox24.Size = new System.Drawing.Size(36, 16);
		this.checkBox24.TabIndex = 8;
		this.checkBox24.Text = "24";
		this.checkBox24.UseVisualStyleBackColor = true;
		this.checkBox25.AutoSize = true;
		this.checkBox25.Location = new System.Drawing.Point(13, 89);
		this.checkBox25.Name = "checkBox25";
		this.checkBox25.Size = new System.Drawing.Size(36, 16);
		this.checkBox25.TabIndex = 8;
		this.checkBox25.Text = "25";
		this.checkBox25.UseVisualStyleBackColor = true;
		this.checkBox26.AutoSize = true;
		this.checkBox26.Location = new System.Drawing.Point(46, 89);
		this.checkBox26.Name = "checkBox26";
		this.checkBox26.Size = new System.Drawing.Size(36, 16);
		this.checkBox26.TabIndex = 8;
		this.checkBox26.Text = "26";
		this.checkBox26.UseVisualStyleBackColor = true;
		this.checkBox27.AutoSize = true;
		this.checkBox27.Location = new System.Drawing.Point(79, 89);
		this.checkBox27.Name = "checkBox27";
		this.checkBox27.Size = new System.Drawing.Size(36, 16);
		this.checkBox27.TabIndex = 8;
		this.checkBox27.Text = "27";
		this.checkBox27.UseVisualStyleBackColor = true;
		this.checkBox28.AutoSize = true;
		this.checkBox28.Location = new System.Drawing.Point(112, 89);
		this.checkBox28.Name = "checkBox28";
		this.checkBox28.Size = new System.Drawing.Size(36, 16);
		this.checkBox28.TabIndex = 8;
		this.checkBox28.Text = "28";
		this.checkBox28.UseVisualStyleBackColor = true;
		this.checkBox29.AutoSize = true;
		this.checkBox29.Location = new System.Drawing.Point(145, 89);
		this.checkBox29.Name = "checkBox29";
		this.checkBox29.Size = new System.Drawing.Size(36, 16);
		this.checkBox29.TabIndex = 8;
		this.checkBox29.Text = "29";
		this.checkBox29.UseVisualStyleBackColor = true;
		this.checkBox30.AutoSize = true;
		this.checkBox30.Location = new System.Drawing.Point(178, 89);
		this.checkBox30.Name = "checkBox30";
		this.checkBox30.Size = new System.Drawing.Size(36, 16);
		this.checkBox30.TabIndex = 8;
		this.checkBox30.Text = "30";
		this.checkBox30.UseVisualStyleBackColor = true;
		this.checkBox31.AutoSize = true;
		this.checkBox31.Location = new System.Drawing.Point(211, 89);
		this.checkBox31.Name = "checkBox31";
		this.checkBox31.Size = new System.Drawing.Size(36, 16);
		this.checkBox31.TabIndex = 8;
		this.checkBox31.Text = "31";
		this.checkBox31.UseVisualStyleBackColor = true;
		this.checkBox32.AutoSize = true;
		this.checkBox32.Location = new System.Drawing.Point(244, 89);
		this.checkBox32.Name = "checkBox32";
		this.checkBox32.Size = new System.Drawing.Size(36, 16);
		this.checkBox32.TabIndex = 8;
		this.checkBox32.Text = "32";
		this.checkBox32.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(286, 143);
		base.Controls.Add(this.checkBox32);
		base.Controls.Add(this.checkBox24);
		base.Controls.Add(this.checkBox16);
		base.Controls.Add(this.checkBox8);
		base.Controls.Add(this.checkBox31);
		base.Controls.Add(this.checkBox23);
		base.Controls.Add(this.checkBox30);
		base.Controls.Add(this.checkBox22);
		base.Controls.Add(this.checkBox15);
		base.Controls.Add(this.checkBox29);
		base.Controls.Add(this.checkBox14);
		base.Controls.Add(this.checkBox21);
		base.Controls.Add(this.checkBox7);
		base.Controls.Add(this.checkBox28);
		base.Controls.Add(this.checkBox13);
		base.Controls.Add(this.checkBox20);
		base.Controls.Add(this.checkBox6);
		base.Controls.Add(this.checkBox27);
		base.Controls.Add(this.checkBox12);
		base.Controls.Add(this.checkBox19);
		base.Controls.Add(this.checkBox5);
		base.Controls.Add(this.checkBox26);
		base.Controls.Add(this.checkBox11);
		base.Controls.Add(this.checkBox18);
		base.Controls.Add(this.checkBox4);
		base.Controls.Add(this.checkBox25);
		base.Controls.Add(this.checkBox10);
		base.Controls.Add(this.checkBox17);
		base.Controls.Add(this.checkBox3);
		base.Controls.Add(this.checkBox9);
		base.Controls.Add(this.checkBox2);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Frmmultivalve";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "多位阀控制";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Frmmultivalve_FormClosing);
		base.Load += new System.EventHandler(Frmmultivalve_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
