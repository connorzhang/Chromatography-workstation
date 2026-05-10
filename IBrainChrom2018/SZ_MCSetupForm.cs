using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SZ_MCSetupForm : CtrlSetupDlg
{
	private const string string_2 = "硬件";

	private const string string_3 = "HardWare";

	private Button btnDisConn;

	private DataGridViewCheckBoxColumn colBool;

	private DataGridViewTextBoxColumn colHardWare;

	private DataGridViewTextBoxColumn colStatus;

	private IContainer icontainer_2;

	private LclGridView gvHardWares;

	private LclLabel lbDt;

	private LclLabel lbHardWare;

	private LclLabel lbPump0;

	private LclLabel lbPump1;

	private LclLabel lbSerial;

	private LclTextBox tbDt;

	private LclTextBox tbDthwc;

	private LclTextBox tbPump0;

	private LclTextBox tbPump0hwc;

	private LclTextBox tbPump1;

	private LclTextBox tbPump1hwc;

	public SZ_MCSetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent();
		btnDisConn.Text = Lang.PS("断开", "Disconn");
		gvHardWares.Columns[0].HeaderText = Lang.PS("硬件", "Hard Ware");
		gvHardWares.Columns[2].HeaderText = Lang.PS("状态", "State");
		lbPump0.Text = Lang.PS("泵一", "Pump 1");
		lbPump1.Text = Lang.PS("泵二", "Pump 2");
		lbDt.Text = Lang.PS("检测器", "Detector");
		lbHardWare.Text = "";
	}

	private void btnDisConn_Click(object sender, EventArgs e)
	{
		lbHardWare.Text = "";
		lbHardWare.Tag = null;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void gvHardWares_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex >= 0 && columnIndex >= 0 && !(bool)gvHardWares.Rows[rowIndex].Cells[colBool.Index].Value)
		{
			lbHardWare.Text = gvHardWares.Rows[rowIndex].Cells[colHardWare.Index].Value.ToString();
			lbHardWare.Tag = gvHardWares.Rows[rowIndex].Tag;
		}
	}

	private void InitializeComponent()
	{
		this.lbSerial = new IBrainChrom2018.LclLabel();
		this.gvHardWares = new IBrainChrom2018.LclGridView();
		this.colHardWare = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colBool = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lbHardWare = new IBrainChrom2018.LclLabel();
		this.btnDisConn = new System.Windows.Forms.Button();
		this.lbPump0 = new IBrainChrom2018.LclLabel();
		this.lbPump1 = new IBrainChrom2018.LclLabel();
		this.tbPump0 = new IBrainChrom2018.LclTextBox();
		this.tbPump1 = new IBrainChrom2018.LclTextBox();
		this.tbPump0hwc = new IBrainChrom2018.LclTextBox();
		this.tbPump1hwc = new IBrainChrom2018.LclTextBox();
		this.lbDt = new IBrainChrom2018.LclLabel();
		this.tbDthwc = new IBrainChrom2018.LclTextBox();
		this.tbDt = new IBrainChrom2018.LclTextBox();
		((System.ComponentModel.ISupportInitialize)this.gvHardWares).BeginInit();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(25, 224);
		base.btnOK.Text = "确认";
		base.btnCancel.Location = new System.Drawing.Point(115, 224);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(207, 224);
		base.btnHelp.Text = "帮助";
		this.lbSerial.AutoSize = true;
		this.lbSerial.Location = new System.Drawing.Point(12, 17);
		this.lbSerial.Name = "lbSerial";
		this.lbSerial.Size = new System.Drawing.Size(49, 13);
		this.lbSerial.TabIndex = 1;
		this.lbSerial.Text = "lclLabel1";
		this.gvHardWares.AllowUserToAddRows = false;
		this.gvHardWares.AllowUserToDeleteRows = false;
		this.gvHardWares.AllowUserToResizeRows = false;
		this.gvHardWares.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvHardWares.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvHardWares.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvHardWares.Columns.AddRange(this.colHardWare, this.colBool, this.colStatus);
		this.gvHardWares.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvHardWares.Location = new System.Drawing.Point(12, 41);
		this.gvHardWares.MultiSelect = false;
		this.gvHardWares.Name = "gvHardWares";
		this.gvHardWares.ReadOnly = true;
		this.gvHardWares.RowHeadersWidth = 25;
		this.gvHardWares.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvHardWares.RowTemplate.Height = 16;
		this.gvHardWares.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvHardWares.ShowCellToolTips = false;
		this.gvHardWares.Size = new System.Drawing.Size(289, 94);
		this.gvHardWares.TabIndex = 2;
		this.gvHardWares.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvHardWares_CellDoubleClick);
		this.colHardWare.HeaderText = "硬件";
		this.colHardWare.Name = "colHardWare";
		this.colHardWare.ReadOnly = true;
		this.colHardWare.Width = 130;
		this.colBool.HeaderText = "Column1";
		this.colBool.Name = "colBool";
		this.colBool.ReadOnly = true;
		this.colBool.Visible = false;
		this.colStatus.HeaderText = "状态";
		this.colStatus.Name = "colStatus";
		this.colStatus.ReadOnly = true;
		this.lbHardWare.AutoSize = true;
		this.lbHardWare.Location = new System.Drawing.Point(73, 17);
		this.lbHardWare.Name = "lbHardWare";
		this.lbHardWare.Size = new System.Drawing.Size(49, 13);
		this.lbHardWare.TabIndex = 1;
		this.lbHardWare.Text = "lclLabel1";
		this.btnDisConn.Location = new System.Drawing.Point(239, 12);
		this.btnDisConn.Name = "btnDisConn";
		this.btnDisConn.Size = new System.Drawing.Size(63, 23);
		this.btnDisConn.TabIndex = 3;
		this.btnDisConn.Text = "断开";
		this.btnDisConn.UseVisualStyleBackColor = true;
		this.btnDisConn.Click += new System.EventHandler(btnDisConn_Click);
		this.lbPump0.AutoSize = true;
		this.lbPump0.Location = new System.Drawing.Point(9, 144);
		this.lbPump0.Name = "lbPump0";
		this.lbPump0.Size = new System.Drawing.Size(31, 13);
		this.lbPump0.TabIndex = 1;
		this.lbPump0.Text = "泵一";
		this.lbPump1.AutoSize = true;
		this.lbPump1.Location = new System.Drawing.Point(9, 169);
		this.lbPump1.Name = "lbPump1";
		this.lbPump1.Size = new System.Drawing.Size(31, 13);
		this.lbPump1.TabIndex = 1;
		this.lbPump1.Text = "泵二";
		this.tbPump0.Location = new System.Drawing.Point(55, 141);
		this.tbPump0.Name = "tbPump0";
		this.tbPump0.Size = new System.Drawing.Size(219, 20);
		this.tbPump0.TabIndex = 4;
		this.tbPump1.Location = new System.Drawing.Point(55, 166);
		this.tbPump1.Name = "tbPump1";
		this.tbPump1.Size = new System.Drawing.Size(219, 20);
		this.tbPump1.TabIndex = 4;
		this.tbPump0hwc.Location = new System.Drawing.Point(280, 141);
		this.tbPump0hwc.Name = "tbPump0hwc";
		this.tbPump0hwc.Size = new System.Drawing.Size(19, 20);
		this.tbPump0hwc.TabIndex = 4;
		this.tbPump1hwc.Location = new System.Drawing.Point(280, 166);
		this.tbPump1hwc.Name = "tbPump1hwc";
		this.tbPump1hwc.Size = new System.Drawing.Size(19, 20);
		this.tbPump1hwc.TabIndex = 4;
		this.lbDt.AutoSize = true;
		this.lbDt.Location = new System.Drawing.Point(9, 194);
		this.lbDt.Name = "lbDt";
		this.lbDt.Size = new System.Drawing.Size(43, 13);
		this.lbDt.TabIndex = 1;
		this.lbDt.Text = "检测器";
		this.tbDthwc.Location = new System.Drawing.Point(280, 191);
		this.tbDthwc.Name = "tbDthwc";
		this.tbDthwc.Size = new System.Drawing.Size(19, 20);
		this.tbDthwc.TabIndex = 4;
		this.tbDt.Location = new System.Drawing.Point(55, 191);
		this.tbDt.Name = "tbDt";
		this.tbDt.Size = new System.Drawing.Size(219, 20);
		this.tbDt.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.ClientSize = new System.Drawing.Size(314, 259);
		base.Controls.Add(this.tbDt);
		base.Controls.Add(this.tbPump1);
		base.Controls.Add(this.tbDthwc);
		base.Controls.Add(this.tbPump1hwc);
		base.Controls.Add(this.tbPump0hwc);
		base.Controls.Add(this.tbPump0);
		base.Controls.Add(this.btnDisConn);
		base.Controls.Add(this.lbDt);
		base.Controls.Add(this.gvHardWares);
		base.Controls.Add(this.lbPump1);
		base.Controls.Add(this.lbHardWare);
		base.Controls.Add(this.lbPump0);
		base.Controls.Add(this.lbSerial);
		base.Name = "SZ_MCSetupForm";
		this.Text = "";
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbSerial, 0);
		base.Controls.SetChildIndex(this.lbPump0, 0);
		base.Controls.SetChildIndex(this.lbHardWare, 0);
		base.Controls.SetChildIndex(this.lbPump1, 0);
		base.Controls.SetChildIndex(this.gvHardWares, 0);
		base.Controls.SetChildIndex(this.lbDt, 0);
		base.Controls.SetChildIndex(this.btnDisConn, 0);
		base.Controls.SetChildIndex(this.tbPump0, 0);
		base.Controls.SetChildIndex(this.tbPump0hwc, 0);
		base.Controls.SetChildIndex(this.tbPump1hwc, 0);
		base.Controls.SetChildIndex(this.tbDthwc, 0);
		base.Controls.SetChildIndex(this.tbPump1, 0);
		base.Controls.SetChildIndex(this.tbDt, 0);
		((System.ComponentModel.ISupportInitialize)this.gvHardWares).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		Control1 control = sysCfgControl as Control1;
		lbHardWare.Text = control.HardStr;
		lbHardWare.Tag = control.HardWare;
		gvHardWares.RowCount = 0;
		for (int i = 0; i < SysCfgDlg.hardWares.Length; i++)
		{
			if (control.hwStyle == HwStyle.SZ && SysCfgDlg.hardWares[i] is UsbSZ)
			{
				UsbSZ usbSZ = SysCfgDlg.hardWares[i] as UsbSZ;
				int rowCount;
				gvHardWares.RowCount = (rowCount = gvHardWares.RowCount) + 1;
				int index = rowCount;
				gvHardWares.Rows[index].Tag = usbSZ;
				gvHardWares.Rows[index].Cells[colHardWare.Index].Value = usbSZ.productString;
				gvHardWares.Rows[index].Cells[colStatus.Index].Value = (usbSZ.installed ? "已配置" : "");
				gvHardWares.Rows[index].Cells[colBool.Index].Value = usbSZ.installed;
			}
		}
		tbPump0.Text = control.bsCtrls[0].name;
		tbPump1.Text = control.bsCtrls[1].name;
		tbDt.Text = control.bsCtrls[2].name;
		tbPump0hwc.Text = control.bsCtrls[0].byte_0.ToString();
		tbPump1hwc.Text = control.bsCtrls[1].byte_0.ToString();
		tbDthwc.Text = control.bsCtrls[2].byte_0.ToString();
	}

	public override void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			lbSerial.Text = "硬件";
			break;
		case SysLanguage.EN:
			lbSerial.Text = "HardWare";
			break;
		}
		base.LoadLanguage();
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		Control1 control = sysCfgControl as Control1;
		control.HardWare = lbHardWare.Tag;
		control.bsCtrls[0].name = tbPump0.Text;
		control.bsCtrls[1].name = tbPump1.Text;
		control.bsCtrls[2].name = tbDt.Text;
	}
}
