using System;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class StationConfigDlg : Form
{
	private TextBox tbStationId;

	private CheckBox cbMqttEnable;

	private TextBox tbMqttHost;

	private NumericUpDown nudMqttPort;

	private CheckBox cbMqttTls;

	private CheckBox cbMqttTlsAllowUntrusted;

	private TextBox tbMqttUser;

	private TextBox tbMqttPassword;

	private TextBox tbMqttClientId;

	private TextBox tbMqttTopicPrefix;

	private NumericUpDown nudMqttHeartbeat;

	private TextBox tbMqttTopicPreview;

	private Label lbMqttStatus;

	private Button btnMqttTest;

	private Timer mqttStatusTimer;

	private Button btnApply;

	private Button btnCancel;

	private GroupBox gbReserved;

	private Label lbHint;

	private Label lbMqttHint;

	private SystemParam sysParam = SystemParam.Create();

	public StationConfigDlg()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	private void StationConfigDlg_Load(object sender, EventArgs e)
	{
		tbStationId.Text = sysParam.strStationId;
		cbMqttEnable.Checked = sysParam.bMqttEnable;
		tbMqttHost.Text = sysParam.strMqttHost;
		nudMqttPort.Value = Math.Max(nudMqttPort.Minimum, Math.Min(nudMqttPort.Maximum, sysParam.iMqttPort));
		cbMqttTls.Checked = sysParam.bMqttTls;
		cbMqttTlsAllowUntrusted.Checked = sysParam.bMqttTlsAllowUntrusted;
		tbMqttUser.Text = sysParam.strMqttUser;
		tbMqttPassword.Text = sysParam.strMqttPassword;
		tbMqttClientId.Text = sysParam.strMqttClientId;
		tbMqttTopicPrefix.Text = sysParam.strMqttTopicPrefix;
		nudMqttHeartbeat.Value = Math.Max(nudMqttHeartbeat.Minimum, Math.Min(nudMqttHeartbeat.Maximum, sysParam.iMqttHeartbeatSec));
		UpdateTopicPreview();
		UpdateMqttEnableState();
		UpdateMqttStatusLabel();
		mqttStatusTimer.Start();
	}

	private void UpdateTopicPreview()
	{
		try
		{
			string prefix = (tbMqttTopicPrefix.Text ?? "").Trim();
			if (prefix == "")
			{
				prefix = "chrom/v1/default/default/{stationId}";
			}
			string sid = (tbStationId.Text ?? "").Trim();
			if (sid == "")
			{
				sid = (sysParam.strStationId ?? "").Trim();
			}
			string root = prefix.Replace("{stationId}", sid).TrimEnd('/');
			tbMqttTopicPreview.Text = root + "/status";
		}
		catch
		{
			if (tbMqttTopicPreview != null)
			{
				tbMqttTopicPreview.Text = "";
			}
		}
	}

	private void UpdateMqttEnableState()
	{
		bool enabled = cbMqttEnable.Checked;
		tbMqttHost.Enabled = enabled;
		nudMqttPort.Enabled = enabled;
		cbMqttTls.Enabled = enabled;
		cbMqttTlsAllowUntrusted.Enabled = enabled;
		tbMqttUser.Enabled = enabled;
		tbMqttPassword.Enabled = enabled;
		tbMqttClientId.Enabled = enabled;
		tbMqttTopicPrefix.Enabled = enabled;
		nudMqttHeartbeat.Enabled = enabled;
		lbMqttHint.Enabled = enabled;
		lbMqttStatus.Enabled = enabled;
		btnMqttTest.Enabled = enabled;
		tbMqttTopicPreview.Enabled = enabled;
	}

	private void UpdateMqttStatusLabel()
	{
		string text = MqttTelemetryService.Instance.GetStatusText();
		lbMqttStatus.Text = "连接状态: " + (text ?? "");
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		sysParam.strStationId = SystemParam.NormalizeStationId24Ascii(tbStationId.Text);
		sysParam.bMqttEnable = cbMqttEnable.Checked;
		sysParam.strMqttHost = (tbMqttHost.Text ?? "").Trim();
		sysParam.iMqttPort = (int)nudMqttPort.Value;
		sysParam.bMqttTls = cbMqttTls.Checked;
		sysParam.bMqttTlsAllowUntrusted = cbMqttTlsAllowUntrusted.Checked;
		sysParam.strMqttUser = tbMqttUser.Text ?? "";
		sysParam.strMqttPassword = tbMqttPassword.Text ?? "";
		sysParam.strMqttClientId = (tbMqttClientId.Text ?? "").Trim();
		sysParam.strMqttTopicPrefix = (tbMqttTopicPrefix.Text ?? "").Trim();
		sysParam.iMqttHeartbeatSec = (int)nudMqttHeartbeat.Value;
		sysParam.SaveParam();
		MqttTelemetryService.Instance.StartOrReload();
		MqttTelemetryService.Instance.PublishTest();
		UpdateMqttStatusLabel();
		DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private void tbStationId_Leave(object sender, EventArgs e)
	{
		tbStationId.Text = SystemParam.NormalizeStationId24Ascii(tbStationId.Text);
		UpdateTopicPreview();
	}

	private void InitializeComponent()
	{
		tbStationId = new TextBox();
		cbMqttEnable = new CheckBox();
		tbMqttHost = new TextBox();
		nudMqttPort = new NumericUpDown();
		cbMqttTls = new CheckBox();
		cbMqttTlsAllowUntrusted = new CheckBox();
		tbMqttUser = new TextBox();
		tbMqttPassword = new TextBox();
		tbMqttClientId = new TextBox();
		tbMqttTopicPrefix = new TextBox();
		nudMqttHeartbeat = new NumericUpDown();
		tbMqttTopicPreview = new TextBox();
		lbMqttStatus = new Label();
		btnMqttTest = new Button();
		mqttStatusTimer = new Timer();
		btnApply = new Button();
		btnCancel = new Button();
		gbReserved = new GroupBox();
		lbHint = new Label();
		lbMqttHint = new Label();
		Panel panelTop = new Panel();
		Panel panelBottom = new Panel();
		TableLayoutPanel table = new TableLayoutPanel();
		FlowLayoutPanel header = new FlowLayoutPanel();
		FlowLayoutPanel portRow = new FlowLayoutPanel();
		Panel statusPanel = new Panel();
		((System.ComponentModel.ISupportInitialize)nudMqttPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)nudMqttHeartbeat).BeginInit();
		SuspendLayout();
		panelTop.Dock = DockStyle.Top;
		panelTop.Height = 70;
		panelTop.Padding = new Padding(12, 10, 12, 8);
		lbHint.AutoSize = true;
		lbHint.Location = new Point(12, 12);
		lbHint.Name = "lbHint";
		lbHint.Text = "设备标识(24位ASCII)，Modbus寄存器801-812读取，少补空格多截断";
		Label labelStation = new Label();
		labelStation.AutoSize = true;
		labelStation.Location = new Point(12, 38);
		labelStation.Name = "labelStation";
		labelStation.Text = "设备标识";
		tbStationId.Location = new Point(80, 34);
		tbStationId.Name = "tbStationId";
		tbStationId.Size = new Size(420, 21);
		tbStationId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		tbStationId.TabIndex = 0;
		tbStationId.Leave += tbStationId_Leave;
		tbStationId.TextChanged += (sender, e) => UpdateTopicPreview();
		panelTop.Controls.Add(lbHint);
		panelTop.Controls.Add(labelStation);
		panelTop.Controls.Add(tbStationId);
		panelBottom.Dock = DockStyle.Bottom;
		panelBottom.Height = 48;
		panelBottom.Padding = new Padding(12, 10, 12, 10);
		btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnCancel.Size = new Size(75, 23);
		btnCancel.Text = "取消";
		btnCancel.UseVisualStyleBackColor = true;
		btnCancel.Click += btnCancel_Click;
		btnApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnApply.Size = new Size(75, 23);
		btnApply.Text = "应用";
		btnApply.UseVisualStyleBackColor = true;
		btnApply.Click += btnApply_Click;
		btnCancel.Location = new Point(panelBottom.Width - 87, 12);
		btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnApply.Location = new Point(panelBottom.Width - 170, 12);
		btnApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		panelBottom.Controls.Add(btnApply);
		panelBottom.Controls.Add(btnCancel);
		gbReserved.Dock = DockStyle.Fill;
		gbReserved.Padding = new Padding(12, 12, 12, 12);
		gbReserved.Text = "MQTT配置";
		table.Dock = DockStyle.Fill;
		table.ColumnCount = 2;
		table.RowCount = 10;
		table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90f));
		table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		for (int i = 0; i < table.RowCount; i++)
		{
			table.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
		}
		header.Dock = DockStyle.Fill;
		header.FlowDirection = FlowDirection.LeftToRight;
		header.WrapContents = false;
		header.AutoSize = true;
		cbMqttEnable.AutoSize = true;
		cbMqttEnable.Text = "启用MQTT";
		cbMqttEnable.CheckedChanged += (sender, e) => UpdateMqttEnableState();
		lbMqttHint.AutoSize = true;
		lbMqttHint.Text = "topic前缀支持{stationId}占位符";
		header.Controls.Add(cbMqttEnable);
		header.Controls.Add(lbMqttHint);
		table.Controls.Add(header, 0, 0);
		table.SetColumnSpan(header, 2);
		Label labelHost = new Label();
		labelHost.Text = "服务器";
		labelHost.TextAlign = ContentAlignment.MiddleLeft;
		labelHost.Dock = DockStyle.Fill;
		table.Controls.Add(labelHost, 0, 1);
		tbMqttHost.Dock = DockStyle.Fill;
		table.Controls.Add(tbMqttHost, 1, 1);
		Label labelPort = new Label();
		labelPort.Text = "端口";
		labelPort.TextAlign = ContentAlignment.MiddleLeft;
		labelPort.Dock = DockStyle.Fill;
		table.Controls.Add(labelPort, 0, 2);
		portRow.Dock = DockStyle.Fill;
		portRow.WrapContents = false;
		portRow.AutoSize = true;
		nudMqttPort.Minimum = 1;
		nudMqttPort.Maximum = 65535;
		nudMqttPort.Value = 1883;
		nudMqttPort.Width = 90;
		cbMqttTls.AutoSize = true;
		cbMqttTls.Text = "TLS";
		cbMqttTlsAllowUntrusted.AutoSize = true;
		cbMqttTlsAllowUntrusted.Text = "忽略证书错误";
		portRow.Controls.Add(nudMqttPort);
		portRow.Controls.Add(cbMqttTls);
		portRow.Controls.Add(cbMqttTlsAllowUntrusted);
		table.Controls.Add(portRow, 1, 2);
		Label labelUser = new Label();
		labelUser.Text = "用户名";
		labelUser.TextAlign = ContentAlignment.MiddleLeft;
		labelUser.Dock = DockStyle.Fill;
		table.Controls.Add(labelUser, 0, 3);
		tbMqttUser.Dock = DockStyle.Fill;
		table.Controls.Add(tbMqttUser, 1, 3);
		Label labelPwd = new Label();
		labelPwd.Text = "密码";
		labelPwd.TextAlign = ContentAlignment.MiddleLeft;
		labelPwd.Dock = DockStyle.Fill;
		table.Controls.Add(labelPwd, 0, 4);
		tbMqttPassword.Dock = DockStyle.Fill;
		tbMqttPassword.PasswordChar = '*';
		table.Controls.Add(tbMqttPassword, 1, 4);
		Label labelClientId = new Label();
		labelClientId.Text = "ClientId";
		labelClientId.TextAlign = ContentAlignment.MiddleLeft;
		labelClientId.Dock = DockStyle.Fill;
		table.Controls.Add(labelClientId, 0, 5);
		tbMqttClientId.Dock = DockStyle.Fill;
		table.Controls.Add(tbMqttClientId, 1, 5);
		Label labelTopic = new Label();
		labelTopic.Text = "Topic前缀";
		labelTopic.TextAlign = ContentAlignment.MiddleLeft;
		labelTopic.Dock = DockStyle.Fill;
		table.Controls.Add(labelTopic, 0, 6);
		tbMqttTopicPrefix.Dock = DockStyle.Fill;
		tbMqttTopicPrefix.TextChanged += (sender, e) => UpdateTopicPreview();
		table.Controls.Add(tbMqttTopicPrefix, 1, 6);
		Label labelHb = new Label();
		labelHb.Text = "心跳(秒)";
		labelHb.TextAlign = ContentAlignment.MiddleLeft;
		labelHb.Dock = DockStyle.Fill;
		table.Controls.Add(labelHb, 0, 7);
		nudMqttHeartbeat.Minimum = 5;
		nudMqttHeartbeat.Maximum = 3600;
		nudMqttHeartbeat.Value = 60;
		nudMqttHeartbeat.Width = 90;
		Panel hbPanel = new Panel();
		hbPanel.Dock = DockStyle.Fill;
		hbPanel.Controls.Add(nudMqttHeartbeat);
		nudMqttHeartbeat.Location = new Point(0, 2);
		table.Controls.Add(hbPanel, 1, 7);
		Label labelPreview = new Label();
		labelPreview.Text = "预览Topic";
		labelPreview.TextAlign = ContentAlignment.MiddleLeft;
		labelPreview.Dock = DockStyle.Fill;
		table.Controls.Add(labelPreview, 0, 8);
		tbMqttTopicPreview.ReadOnly = true;
		tbMqttTopicPreview.TabStop = false;
		tbMqttTopicPreview.Dock = DockStyle.Fill;
		table.Controls.Add(tbMqttTopicPreview, 1, 8);
		statusPanel.Dock = DockStyle.Fill;
		lbMqttStatus.Dock = DockStyle.Fill;
		lbMqttStatus.Text = "连接状态: ";
		btnMqttTest.Text = "发送测试";
		btnMqttTest.Width = 90;
		btnMqttTest.Dock = DockStyle.Right;
		btnMqttTest.Click += (sender, e) =>
		{
			string err = MqttTelemetryService.Instance.PublishTest();
			if (!string.IsNullOrEmpty(err))
			{
				MessageBox.Show(err, "MQTT测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			UpdateMqttStatusLabel();
		};
		statusPanel.Controls.Add(lbMqttStatus);
		statusPanel.Controls.Add(btnMqttTest);
		table.Controls.Add(statusPanel, 0, 9);
		table.SetColumnSpan(statusPanel, 2);
		gbReserved.Controls.Add(table);
		mqttStatusTimer.Interval = 2000;
		mqttStatusTimer.Tick += (sender, e) => UpdateMqttStatusLabel();
		AutoScaleDimensions = new SizeF(6f, 12f);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(640, 520);
		Controls.Add(gbReserved);
		Controls.Add(panelBottom);
		Controls.Add(panelTop);
		FormBorderStyle = FormBorderStyle.Sizable;
		MinimumSize = new Size(640, 520);
		Name = "StationConfigDlg";
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "系统配置";
		Load += StationConfigDlg_Load;
		ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)nudMqttPort).EndInit();
		((System.ComponentModel.ISupportInitialize)nudMqttHeartbeat).EndInit();
	}
}

