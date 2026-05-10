using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018._200.ChromForm;

public class FormPoweroff : Form
{
	private IContainer components = null;

	private Button btnPowerOff;

	private Button btnCancel;

	public FormPoweroff()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018._200.ChromForm.FormPoweroff));
		this.btnPowerOff = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnPowerOff.Location = new System.Drawing.Point(45, 35);
		this.btnPowerOff.Name = "btnPowerOff";
		this.btnPowerOff.Size = new System.Drawing.Size(183, 97);
		this.btnPowerOff.TabIndex = 0;
		this.btnPowerOff.Text = "button1";
		this.btnPowerOff.UseVisualStyleBackColor = true;
		this.btnCancel.Location = new System.Drawing.Point(258, 35);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(183, 97);
		this.btnCancel.TabIndex = 1;
		this.btnCancel.Text = "button2";
		this.btnCancel.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(490, 175);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnPowerOff);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormPoweroff";
		this.Text = "FormPoweroff";
		base.ResumeLayout(false);
	}
}
