using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LiquidPump2SetupForm : CtrlSetupDlg
{
	private const string string_2 = "泵 1";

	private const string string_3 = "泵 2";

	private const string string_4 = "泵 3";

	private const string string_5 = "泵 4";

	private const string string_6 = "串行口";

	private const string string_7 = "固件版本";

	private const string string_8 = "Pump 1";

	private const string string_9 = "Pump 2";

	private const string string_10 = "Pump 3";

	private const string string_11 = "Pump 4";

	private const string string_12 = "Serial";

	private const string string_13 = "Firmware Version";

	private LclComboBox cbCOM_Port;

	private IContainer icontainer_2;

	private LclLabel lbPump0;

	private LclLabel lbPump1;

	private LclLabel lbPump2;

	private LclLabel lbPump3;

	private LclLabel lbSerial;

	private LclLabel lbVersion;

	private LclTextBox tbPump0Name;

	private LclTextBox tbPump1Name;

	private LclTextBox tbPump2Name;

	private LclTextBox tbPump3Name;

	public LiquidPump2SetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent_2();
		cbCOM_Port.Items.Add(COM_Port.COM1);
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
		lbSerial = new LclLabel();
		lbVersion = new LclLabel();
		lbPump0 = new LclLabel();
		lbPump1 = new LclLabel();
		lbPump2 = new LclLabel();
		lbPump3 = new LclLabel();
		cbCOM_Port = new LclComboBox();
		tbPump0Name = new LclTextBox();
		tbPump1Name = new LclTextBox();
		tbPump2Name = new LclTextBox();
		tbPump3Name = new LclTextBox();
		SuspendLayout();
		btnOK.Location = new Point(13, 207);
		btnOK.Text = "确认";
		btnCancel.Location = new Point(103, 207);
		btnCancel.Text = "取消";
		btnHelp.Location = new Point(195, 207);
		btnHelp.Text = "帮助";
		lbSerial.AutoSize = true;
		lbSerial.Location = new Point(29, 21);
		lbSerial.Name = "lbSerial";
		lbSerial.Size = new Size(49, 13);
		lbSerial.TabIndex = 1;
		lbSerial.Text = "lclLabel1";
		lbVersion.AutoSize = true;
		lbVersion.Location = new Point(101, 53);
		lbVersion.Name = "lbVersion";
		lbVersion.Size = new Size(49, 13);
		lbVersion.TabIndex = 1;
		lbVersion.Text = "lclLabel1";
		lbPump0.AutoSize = true;
		lbPump0.Location = new Point(29, 79);
		lbPump0.Name = "lbPump0";
		lbPump0.Size = new Size(49, 13);
		lbPump0.TabIndex = 1;
		lbPump0.Text = "lclLabel1";
		lbPump1.AutoSize = true;
		lbPump1.Location = new Point(29, 106);
		lbPump1.Name = "lbPump1";
		lbPump1.Size = new Size(49, 13);
		lbPump1.TabIndex = 1;
		lbPump1.Text = "lclLabel1";
		lbPump2.AutoSize = true;
		lbPump2.Location = new Point(29, 138);
		lbPump2.Name = "lbPump2";
		lbPump2.Size = new Size(49, 13);
		lbPump2.TabIndex = 1;
		lbPump2.Text = "lclLabel1";
		lbPump3.AutoSize = true;
		lbPump3.Location = new Point(29, 168);
		lbPump3.Name = "lbPump3";
		lbPump3.Size = new Size(49, 13);
		lbPump3.TabIndex = 1;
		lbPump3.Text = "lclLabel1";
		cbCOM_Port.DropDownStyle = ComboBoxStyle.DropDownList;
		cbCOM_Port.FormattingEnabled = true;
		cbCOM_Port.ItemExtString = "";
		cbCOM_Port.Location = new Point(103, 17);
		cbCOM_Port.Name = "cbCOM_Port";
		cbCOM_Port.Size = new Size(75, 21);
		cbCOM_Port.TabIndex = 2;
		tbPump0Name.Location = new Point(103, 76);
		tbPump0Name.Name = "tbPump0Name";
		tbPump0Name.Size = new Size(139, 20);
		tbPump0Name.TabIndex = 3;
		tbPump0Name.Text = "LC 1";
		tbPump1Name.Location = new Point(103, 103);
		tbPump1Name.Name = "tbPump1Name";
		tbPump1Name.Size = new Size(139, 20);
		tbPump1Name.TabIndex = 3;
		tbPump1Name.Text = "LC 2";
		tbPump2Name.Location = new Point(103, 132);
		tbPump2Name.Name = "tbPump2Name";
		tbPump2Name.Size = new Size(139, 20);
		tbPump2Name.TabIndex = 3;
		tbPump2Name.Text = "LC 3";
		tbPump3Name.Location = new Point(103, 165);
		tbPump3Name.Name = "tbPump3Name";
		tbPump3Name.Size = new Size(139, 20);
		tbPump3Name.TabIndex = 3;
		tbPump3Name.Text = "LC 4";
		base.AutoScaleDimensions = new SizeF(6f, 13f);
		base.ClientSize = new Size(290, 242);
		base.Controls.Add(lbSerial);
		base.Controls.Add(lbVersion);
		base.Controls.Add(lbPump0);
		base.Controls.Add(tbPump0Name);
		base.Controls.Add(cbCOM_Port);
		base.Controls.Add(lbPump1);
		base.Controls.Add(tbPump1Name);
		base.Controls.Add(lbPump2);
		base.Controls.Add(lbPump3);
		base.Controls.Add(tbPump2Name);
		base.Controls.Add(tbPump3Name);
		base.Name = "LiquidChromControl2SetupForm";
		Text = "";
		base.Controls.SetChildIndex(tbPump3Name, 0);
		base.Controls.SetChildIndex(tbPump2Name, 0);
		base.Controls.SetChildIndex(lbPump3, 0);
		base.Controls.SetChildIndex(lbPump2, 0);
		base.Controls.SetChildIndex(tbPump1Name, 0);
		base.Controls.SetChildIndex(lbPump1, 0);
		base.Controls.SetChildIndex(cbCOM_Port, 0);
		base.Controls.SetChildIndex(tbPump0Name, 0);
		base.Controls.SetChildIndex(lbPump0, 0);
		base.Controls.SetChildIndex(lbVersion, 0);
		base.Controls.SetChildIndex(lbSerial, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		LiquidPump2 liquidPump = sysCfgControl as LiquidPump2;
		cbCOM_Port.SelectedIndex = (int)liquidPump.com_port;
		tbPump0Name.Text = liquidPump.bsCtrls[0].name;
		tbPump1Name.Text = liquidPump.bsCtrls[1].name;
		tbPump2Name.Text = liquidPump.bsCtrls[2].name;
		tbPump3Name.Text = liquidPump.bsCtrls[3].name;
	}

	public override void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			lbSerial.Text = "串行口";
			lbVersion.Text = "固件版本";
			lbPump0.Text = "泵 1";
			lbPump1.Text = "泵 2";
			lbPump2.Text = "泵 3";
			lbPump3.Text = "泵 4";
			break;
		case SysLanguage.EN:
			lbSerial.Text = "Serial";
			lbVersion.Text = "Firmware Version";
			lbPump0.Text = "Pump 1";
			lbPump1.Text = "Pump 2";
			lbPump2.Text = "Pump 3";
			lbPump3.Text = "Pump 4";
			break;
		}
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		LiquidPump2 liquidPump = sysCfgControl as LiquidPump2;
		liquidPump.com_port = (COM_Port)cbCOM_Port.SelectedIndex;
		liquidPump.bsCtrls[0].name = tbPump0Name.Text;
		liquidPump.bsCtrls[1].name = tbPump1Name.Text;
		liquidPump.bsCtrls[2].name = tbPump2Name.Text;
		liquidPump.bsCtrls[3].name = tbPump3Name.Text;
	}
}
