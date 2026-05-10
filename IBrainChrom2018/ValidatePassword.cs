using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class ValidatePassword : Form
{
	private static bool bAllowAccess = false;

	private IContainer components = null;

	private Button button1;

	private TextBox textBox1;

	public static bool AllowAccess => bAllowAccess;

	public ValidatePassword()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text == "IB8888")
		{
			base.DialogResult = DialogResult.OK;
			bAllowAccess = true;
		}
	}

	private void ValidatePassword_Load(object sender, EventArgs e)
	{
	}

	private void button1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && textBox1.Text == "IB8888")
		{
			base.DialogResult = DialogResult.OK;
			bAllowAccess = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ValidatePassword));
		this.button1 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		resources.ApplyResources(this.button1, "button1");
		this.button1.Name = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(button1_KeyDown);
		resources.ApplyResources(this.textBox1, "textBox1");
		this.textBox1.Name = "textBox1";
		this.textBox1.UseSystemPasswordChar = true;
		base.AcceptButton = this.button1;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.button1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ValidatePassword";
		base.Load += new System.EventHandler(ValidatePassword_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
