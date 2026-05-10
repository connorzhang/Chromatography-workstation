using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LiquidChromControl1SetupForm : CtrlSetupDlg
{
	private const string string_2 = "连接到";

	private const string string_3 = "连接";

	private const string string_4 = "Connected";

	private const string string_5 = "Connection";

	private LclComboBox cbCOM_Port;

	private IContainer icontainer_2;

	private LclLabel lbConnected;

	private LclTabControl lclTabControl1;

	private TabPage tpConnection;

	public LiquidChromControl1SetupForm()
	{
		icontainer_2 = null;
		InitializeComponent_2();
	}

	public LiquidChromControl1SetupForm(string scnControlName, string senControlName)
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
		lclTabControl1 = new LclTabControl();
		tpConnection = new TabPage();
		cbCOM_Port = new LclComboBox();
		lbConnected = new LclLabel();
		lclTabControl1.SuspendLayout();
		tpConnection.SuspendLayout();
		SuspendLayout();
		btnOK.Location = new Point(24, 145);
		btnOK.Text = "确认";
		btnCancel.Location = new Point(126, 145);
		btnCancel.Text = "取消";
		btnHelp.Location = new Point(231, 145);
		btnHelp.Text = "帮助";
		lclTabControl1.Controls.Add(tpConnection);
		lclTabControl1.ItemSize = new Size(90, 19);
		lclTabControl1.Location = new Point(12, 12);
		lclTabControl1.Name = "lclTabControl1";
		lclTabControl1.SelectedIndex = 0;
		lclTabControl1.Size = new Size(302, 124);
		lclTabControl1.TabIndex = 1;
		tpConnection.Controls.Add(cbCOM_Port);
		tpConnection.Controls.Add(lbConnected);
		tpConnection.Location = new Point(4, 23);
		tpConnection.Name = "tpConnection";
		tpConnection.Size = new Size(294, 97);
		tpConnection.TabIndex = 0;
		tpConnection.Text = "tabPage1";
		tpConnection.UseVisualStyleBackColor = true;
		cbCOM_Port.DropDownStyle = ComboBoxStyle.DropDownList;
		cbCOM_Port.FormattingEnabled = true;
		cbCOM_Port.ItemExtString = "";
		cbCOM_Port.Location = new Point(21, 47);
		cbCOM_Port.Name = "cbCOM_Port";
		cbCOM_Port.Size = new Size(121, 20);
		cbCOM_Port.TabIndex = 1;
		lbConnected.AutoSize = true;
		lbConnected.Location = new Point(19, 30);
		lbConnected.Name = "lbConnected";
		lbConnected.Size = new Size(59, 12);
		lbConnected.TabIndex = 0;
		lbConnected.Text = "lclLabel1";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(326, 176);
		base.Controls.Add(lclTabControl1);
		base.Name = "LiquidChromControl1SetupForm";
		Text = "";
		base.Controls.SetChildIndex(lclTabControl1, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		lclTabControl1.ResumeLayout(performLayout: false);
		tpConnection.ResumeLayout(performLayout: false);
		tpConnection.PerformLayout();
		ResumeLayout(performLayout: false);
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		LiquidPump1 liquidPump = sysCfgControl as LiquidPump1;
		cbCOM_Port.SelectedIndex = (int)liquidPump.com_port;
	}

	public override void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			tpConnection.Text = "连接";
			lbConnected.Text = "连接到";
			break;
		case SysLanguage.EN:
			tpConnection.Text = "Connection";
			lbConnected.Text = "Connected";
			break;
		}
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		LiquidPump1 liquidPump = sysCfgControl as LiquidPump1;
		liquidPump.com_port = (COM_Port)cbCOM_Port.SelectedIndex;
	}
}
