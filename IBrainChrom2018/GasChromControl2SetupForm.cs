using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class GasChromControl2SetupForm : CtrlSetupDlg
{
	private LclComboBox cbCOM_Port;

	private IContainer icontainer_2;

	private LclLabel lclLabel1;

	public GasChromControl2SetupForm()
	{
		icontainer_2 = null;
		InitializeComponent_2();
	}

	public GasChromControl2SetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent_2();
		cbCOM_Port.Items.Add(COM_Port.COM1);
		cbCOM_Port.Items.Add(COM_Port.COM2);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent_2()
	{
		cbCOM_Port = new LclComboBox();
		lclLabel1 = new LclLabel();
		SuspendLayout();
		btnOK.Location = new Point(14, 77);
		btnCancel.Location = new Point(104, 77);
		btnHelp.Location = new Point(196, 77);
		cbCOM_Port.DropDownStyle = ComboBoxStyle.DropDownList;
		cbCOM_Port.FormattingEnabled = true;
		cbCOM_Port.Location = new Point(114, 23);
		cbCOM_Port.Name = "cbCOM_Port";
		cbCOM_Port.Size = new Size(121, 20);
		cbCOM_Port.TabIndex = 4;
		lclLabel1.AutoSize = true;
		lclLabel1.Location = new Point(44, 26);
		lclLabel1.Name = "lclLabel1";
		lclLabel1.Size = new Size(23, 12);
		lclLabel1.TabIndex = 3;
		lclLabel1.Text = "COM";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(289, 119);
		base.Controls.Add(cbCOM_Port);
		base.Controls.Add(lclLabel1);
		base.Name = "GasChromControl2SetupForm";
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(lclLabel1, 0);
		base.Controls.SetChildIndex(cbCOM_Port, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		GasChromControl2 gasChromControl = sysCfgControl as GasChromControl2;
		cbCOM_Port.SelectedIndex = (int)gasChromControl.com_port;
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		GasChromControl2 gasChromControl = sysCfgControl as GasChromControl2;
		gasChromControl.com_port = (COM_Port)cbCOM_Port.SelectedIndex;
	}
}
