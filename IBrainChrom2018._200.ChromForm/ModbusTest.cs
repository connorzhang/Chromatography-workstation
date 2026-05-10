using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018._200.ChromForm;

public class ModbusTest : Form
{
	private IContainer components = null;

	private CheckBox checkBox1;

	public ModbusTest()
	{
		InitializeComponent();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018._200.ChromForm.ModbusTest));
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(12, 12);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(101, 19);
		this.checkBox1.TabIndex = 0;
		this.checkBox1.Text = "checkBox1";
		this.checkBox1.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.checkBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "ModbusTest";
		this.Text = "ModbusTest";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
