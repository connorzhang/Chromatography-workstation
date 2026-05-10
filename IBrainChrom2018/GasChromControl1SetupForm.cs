using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class GasChromControl1SetupForm : CtrlSetupDlg
{
	private const string string_2 = "GC 序列";

	private const string string_3 = "GC Serial";

	private LclComboBox cbCOM_Port;

	private IContainer icontainer_2;

	private LclLabel lbGCSerial;

	private LclLabel lclLabel1;

	private LclTextBox tbGCSerial;

	public GasChromControl1SetupForm()
	{
		icontainer_2 = null;
		InitializeComponent_2();
	}

	public GasChromControl1SetupForm(string scnControlName, string senControlName)
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
		lclLabel1 = new LclLabel();
		lbGCSerial = new LclLabel();
		cbCOM_Port = new LclComboBox();
		tbGCSerial = new LclTextBox();
		SuspendLayout();
		btnOK.Location = new Point(14, 103);
		btnCancel.Location = new Point(104, 103);
		btnHelp.Location = new Point(196, 103);
		lclLabel1.AutoSize = true;
		lclLabel1.Location = new Point(35, 25);
		lclLabel1.Name = "lclLabel1";
		lclLabel1.Size = new Size(23, 12);
		lclLabel1.TabIndex = 1;
		lclLabel1.Text = "COM";
		lbGCSerial.AutoSize = true;
		lbGCSerial.Location = new Point(35, 53);
		lbGCSerial.Name = "lbGCSerial";
		lbGCSerial.Size = new Size(59, 12);
		lbGCSerial.TabIndex = 1;
		lbGCSerial.Text = "lclLabel1";
		cbCOM_Port.DropDownStyle = ComboBoxStyle.DropDownList;
		cbCOM_Port.FormattingEnabled = true;
		cbCOM_Port.Location = new Point(105, 22);
		cbCOM_Port.Name = "cbCOM_Port";
		cbCOM_Port.Size = new Size(121, 20);
		cbCOM_Port.TabIndex = 2;
		tbGCSerial.Location = new Point(105, 50);
		tbGCSerial.Name = "tbGCSerial";
		tbGCSerial.Size = new Size(100, 21);
		tbGCSerial.TabIndex = 3;
		tbGCSerial.Text = "0";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(294, 138);
		base.Controls.Add(cbCOM_Port);
		base.Controls.Add(tbGCSerial);
		base.Controls.Add(lbGCSerial);
		base.Controls.Add(lclLabel1);
		base.Name = "GasChromControl1SetupForm";
		base.Controls.SetChildIndex(lclLabel1, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(lbGCSerial, 0);
		base.Controls.SetChildIndex(tbGCSerial, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(cbCOM_Port, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		GasChromControl1 gasChromControl = sysCfgControl as GasChromControl1;
		cbCOM_Port.SelectedIndex = (int)gasChromControl.com_port;
		tbGCSerial.Text = gasChromControl.gcSerial.ToString();
	}

	public override void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			lbGCSerial.Text = "GC 序列";
			break;
		case SysLanguage.EN:
			lbGCSerial.Text = "GC Serial";
			break;
		}
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		GasChromControl1 gasChromControl = sysCfgControl as GasChromControl1;
		gasChromControl.com_port = (COM_Port)cbCOM_Port.SelectedIndex;
		try
		{
			gasChromControl.gcSerial = int.Parse(tbGCSerial.Text.Trim());
		}
		catch
		{
		}
	}
}
