using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class AutoSamplerControl1SetupForm : CtrlSetupDlg
{
	private LclComboBox cbCOM_Port;

	private LclComboBox cbID;

	private IContainer icontainer_2;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	public AutoSamplerControl1SetupForm()
	{
		icontainer_2 = null;
		InitializeComponent_2();
	}

	public AutoSamplerControl1SetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent_2();
		cbCOM_Port.Items.Add(COM_Port.COM1);
		cbCOM_Port.Items.Add(COM_Port.COM2);
		cbID.Items.Add(10);
		cbID.Items.Add(11);
		cbID.Items.Add(12);
		cbID.Items.Add(13);
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
		cbID = new LclComboBox();
		lclLabel1 = new LclLabel();
		lclLabel2 = new LclLabel();
		SuspendLayout();
		btnOK.Location = new Point(36, 107);
		btnCancel.Location = new Point(126, 107);
		btnHelp.Location = new Point(218, 107);
		cbCOM_Port.DropDownStyle = ComboBoxStyle.DropDownList;
		cbCOM_Port.FormattingEnabled = true;
		cbCOM_Port.Location = new Point(127, 30);
		cbCOM_Port.Name = "cbCOM_Port";
		cbCOM_Port.Size = new Size(121, 20);
		cbCOM_Port.TabIndex = 1;
		cbID.DropDownStyle = ComboBoxStyle.DropDownList;
		cbID.FormattingEnabled = true;
		cbID.Location = new Point(127, 56);
		cbID.Name = "cbID";
		cbID.Size = new Size(75, 20);
		cbID.TabIndex = 1;
		lclLabel1.AutoSize = true;
		lclLabel1.Location = new Point(72, 33);
		lclLabel1.Name = "lclLabel1";
		lclLabel1.Size = new Size(23, 12);
		lclLabel1.TabIndex = 2;
		lclLabel1.Text = "COM";
		lclLabel2.AutoSize = true;
		lclLabel2.Location = new Point(72, 59);
		lclLabel2.Name = "lclLabel2";
		lclLabel2.Size = new Size(17, 12);
		lclLabel2.TabIndex = 2;
		lclLabel2.Text = "ID";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(339, 154);
		base.Controls.Add(cbCOM_Port);
		base.Controls.Add(cbID);
		base.Controls.Add(lclLabel1);
		base.Controls.Add(lclLabel2);
		base.Name = "AutoSamplerControl1SetupForm";
		base.Controls.SetChildIndex(lclLabel2, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(lclLabel1, 0);
		base.Controls.SetChildIndex(cbID, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(cbCOM_Port, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		AutoSamplerControl1 autoSamplerControl = sysCfgControl as AutoSamplerControl1;
		cbCOM_Port.SelectedIndex = (int)autoSamplerControl.com_port;
		cbID.SelectedIndex = (int)autoSamplerControl.autoSamplerControl1_ID;
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		AutoSamplerControl1 autoSamplerControl = sysCfgControl as AutoSamplerControl1;
		autoSamplerControl.com_port = (COM_Port)cbCOM_Port.SelectedIndex;
		autoSamplerControl.autoSamplerControl1_ID = (AutoSamplerControl1_ID)cbID.SelectedIndex;
	}
}
