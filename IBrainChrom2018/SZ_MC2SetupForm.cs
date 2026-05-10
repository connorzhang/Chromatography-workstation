using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SZ_MC2SetupForm : CtrlSetupDlg
{
	private const string string_2 = "硬件";

	private const string string_3 = "HardWare";

	private Button btnDisConn;

	private DataGridViewCheckBoxColumn colBool;

	private DataGridViewTextBoxColumn colHardWare;

	private DataGridViewTextBoxColumn colStatus;

	private IContainer icontainer_2;

	private LclGridView gvHardWares;

	private LclLabel lbAS;

	private LclLabel lbDt;

	private LclLabel lbG;

	private LclLabel lbHardWare;

	private LclLabel lbOven;

	private LclLabel lbPump0;

	private LclLabel lbPump1;

	private LclLabel lbSerial;

	private LclTextBox tbAS;

	private LclTextBox tbAShwc;

	private LclTextBox tbDt;

	private LclTextBox tbDthwc;

	private LclTextBox tbG;

	private LclTextBox tbGhwc;

	private LclTextBox tbOven;

	private LclTextBox tbOvenhwc;

	private LclTextBox tbPump0;

	private LclTextBox tbPump0hwc;

	private LclTextBox tbPump1;

	private LclTextBox tbPump1hwc;

	public SZ_MC2SetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent();
		btnDisConn.Text = Lang.PS("断开", "Disconn");
		gvHardWares.Columns[0].HeaderText = Lang.PS("硬件", "Hard Ware");
		gvHardWares.Columns[2].HeaderText = Lang.PS("状态", "State");
		lbAS.Text = Lang.PS("进样器", "Sampler");
		lbPump0.Text = Lang.PS("泵一", "Pump 1");
		lbPump1.Text = Lang.PS("泵二", "Pump 2");
		lbG.Text = Lang.PS("梯度", "Gradient");
		lbOven.Text = Lang.PS("柱温箱", "Oven");
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
		this.lbAS = new IBrainChrom2018.LclLabel();
		this.lbG = new IBrainChrom2018.LclLabel();
		this.lbOven = new IBrainChrom2018.LclLabel();
		this.tbAS = new IBrainChrom2018.LclTextBox();
		this.tbAShwc = new IBrainChrom2018.LclTextBox();
		this.tbGhwc = new IBrainChrom2018.LclTextBox();
		this.tbOvenhwc = new IBrainChrom2018.LclTextBox();
		this.tbG = new IBrainChrom2018.LclTextBox();
		this.tbOven = new IBrainChrom2018.LclTextBox();
		((System.ComponentModel.ISupportInitialize)this.gvHardWares).BeginInit();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(28, 278);
		base.btnOK.Text = "确认";
		base.btnCancel.Location = new System.Drawing.Point(118, 278);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(210, 278);
		base.btnHelp.Text = "帮助";
		this.lbSerial.AutoSize = true;
		this.lbSerial.Location = new System.Drawing.Point(12, 16);
		this.lbSerial.Name = "lbSerial";
		this.lbSerial.Size = new System.Drawing.Size(59, 12);
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
		this.gvHardWares.Location = new System.Drawing.Point(12, 38);
		this.gvHardWares.MultiSelect = false;
		this.gvHardWares.Name = "gvHardWares";
		this.gvHardWares.ReadOnly = true;
		this.gvHardWares.RowHeadersWidth = 25;
		this.gvHardWares.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvHardWares.RowTemplate.Height = 16;
		this.gvHardWares.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvHardWares.ShowCellToolTips = false;
		this.gvHardWares.Size = new System.Drawing.Size(289, 87);
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
		this.lbHardWare.Location = new System.Drawing.Point(73, 16);
		this.lbHardWare.Name = "lbHardWare";
		this.lbHardWare.Size = new System.Drawing.Size(59, 12);
		this.lbHardWare.TabIndex = 1;
		this.lbHardWare.Text = "lclLabel1";
		this.btnDisConn.Location = new System.Drawing.Point(239, 11);
		this.btnDisConn.Name = "btnDisConn";
		this.btnDisConn.Size = new System.Drawing.Size(63, 21);
		this.btnDisConn.TabIndex = 3;
		this.btnDisConn.Text = "断开";
		this.btnDisConn.UseVisualStyleBackColor = true;
		this.btnDisConn.Click += new System.EventHandler(btnDisConn_Click);
		this.lbPump0.AutoSize = true;
		this.lbPump0.Location = new System.Drawing.Point(9, 158);
		this.lbPump0.Name = "lbPump0";
		this.lbPump0.Size = new System.Drawing.Size(29, 12);
		this.lbPump0.TabIndex = 1;
		this.lbPump0.Text = "泵一";
		this.lbPump1.AutoSize = true;
		this.lbPump1.Location = new System.Drawing.Point(9, 181);
		this.lbPump1.Name = "lbPump1";
		this.lbPump1.Size = new System.Drawing.Size(29, 12);
		this.lbPump1.TabIndex = 1;
		this.lbPump1.Text = "泵二";
		this.tbPump0.Location = new System.Drawing.Point(55, 155);
		this.tbPump0.Name = "tbPump0";
		this.tbPump0.Size = new System.Drawing.Size(219, 21);
		this.tbPump0.TabIndex = 4;
		this.tbPump1.Location = new System.Drawing.Point(55, 178);
		this.tbPump1.Name = "tbPump1";
		this.tbPump1.Size = new System.Drawing.Size(219, 21);
		this.tbPump1.TabIndex = 4;
		this.tbPump0hwc.Location = new System.Drawing.Point(280, 155);
		this.tbPump0hwc.Name = "tbPump0hwc";
		this.tbPump0hwc.Size = new System.Drawing.Size(19, 21);
		this.tbPump0hwc.TabIndex = 4;
		this.tbPump1hwc.Location = new System.Drawing.Point(280, 178);
		this.tbPump1hwc.Name = "tbPump1hwc";
		this.tbPump1hwc.Size = new System.Drawing.Size(19, 21);
		this.tbPump1hwc.TabIndex = 4;
		this.lbDt.AutoSize = true;
		this.lbDt.Location = new System.Drawing.Point(9, 250);
		this.lbDt.Name = "lbDt";
		this.lbDt.Size = new System.Drawing.Size(41, 12);
		this.lbDt.TabIndex = 1;
		this.lbDt.Text = "检测器";
		this.tbDthwc.Location = new System.Drawing.Point(280, 247);
		this.tbDthwc.Name = "tbDthwc";
		this.tbDthwc.Size = new System.Drawing.Size(19, 21);
		this.tbDthwc.TabIndex = 4;
		this.tbDt.Location = new System.Drawing.Point(55, 247);
		this.tbDt.Name = "tbDt";
		this.tbDt.Size = new System.Drawing.Size(219, 21);
		this.tbDt.TabIndex = 4;
		this.lbAS.AutoSize = true;
		this.lbAS.Location = new System.Drawing.Point(9, 136);
		this.lbAS.Name = "lbAS";
		this.lbAS.Size = new System.Drawing.Size(41, 12);
		this.lbAS.TabIndex = 1;
		this.lbAS.Text = "进样器";
		this.lbG.AutoSize = true;
		this.lbG.Location = new System.Drawing.Point(9, 204);
		this.lbG.Name = "lbG";
		this.lbG.Size = new System.Drawing.Size(29, 12);
		this.lbG.TabIndex = 1;
		this.lbG.Text = "梯度";
		this.lbOven.AutoSize = true;
		this.lbOven.Location = new System.Drawing.Point(9, 227);
		this.lbOven.Name = "lbOven";
		this.lbOven.Size = new System.Drawing.Size(41, 12);
		this.lbOven.TabIndex = 1;
		this.lbOven.Text = "柱温箱";
		this.tbAS.Location = new System.Drawing.Point(55, 132);
		this.tbAS.Name = "tbAS";
		this.tbAS.Size = new System.Drawing.Size(219, 21);
		this.tbAS.TabIndex = 4;
		this.tbAShwc.Location = new System.Drawing.Point(280, 132);
		this.tbAShwc.Name = "tbAShwc";
		this.tbAShwc.Size = new System.Drawing.Size(19, 21);
		this.tbAShwc.TabIndex = 4;
		this.tbGhwc.Location = new System.Drawing.Point(280, 201);
		this.tbGhwc.Name = "tbGhwc";
		this.tbGhwc.Size = new System.Drawing.Size(19, 21);
		this.tbGhwc.TabIndex = 4;
		this.tbOvenhwc.Location = new System.Drawing.Point(280, 224);
		this.tbOvenhwc.Name = "tbOvenhwc";
		this.tbOvenhwc.Size = new System.Drawing.Size(19, 21);
		this.tbOvenhwc.TabIndex = 4;
		this.tbG.Location = new System.Drawing.Point(55, 201);
		this.tbG.Name = "tbG";
		this.tbG.Size = new System.Drawing.Size(219, 21);
		this.tbG.TabIndex = 4;
		this.tbOven.Location = new System.Drawing.Point(55, 224);
		this.tbOven.Name = "tbOven";
		this.tbOven.Size = new System.Drawing.Size(219, 21);
		this.tbOven.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(314, 311);
		base.Controls.Add(this.tbOven);
		base.Controls.Add(this.tbG);
		base.Controls.Add(this.tbDt);
		base.Controls.Add(this.tbOvenhwc);
		base.Controls.Add(this.tbPump1);
		base.Controls.Add(this.tbGhwc);
		base.Controls.Add(this.tbDthwc);
		base.Controls.Add(this.tbAShwc);
		base.Controls.Add(this.tbPump1hwc);
		base.Controls.Add(this.tbAS);
		base.Controls.Add(this.tbPump0hwc);
		base.Controls.Add(this.tbPump0);
		base.Controls.Add(this.lbOven);
		base.Controls.Add(this.btnDisConn);
		base.Controls.Add(this.lbDt);
		base.Controls.Add(this.lbG);
		base.Controls.Add(this.gvHardWares);
		base.Controls.Add(this.lbPump1);
		base.Controls.Add(this.lbAS);
		base.Controls.Add(this.lbHardWare);
		base.Controls.Add(this.lbPump0);
		base.Controls.Add(this.lbSerial);
		base.Name = "SZ_MC2SetupForm";
		this.Text = "";
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbSerial, 0);
		base.Controls.SetChildIndex(this.lbPump0, 0);
		base.Controls.SetChildIndex(this.lbHardWare, 0);
		base.Controls.SetChildIndex(this.lbAS, 0);
		base.Controls.SetChildIndex(this.lbPump1, 0);
		base.Controls.SetChildIndex(this.gvHardWares, 0);
		base.Controls.SetChildIndex(this.lbG, 0);
		base.Controls.SetChildIndex(this.lbDt, 0);
		base.Controls.SetChildIndex(this.btnDisConn, 0);
		base.Controls.SetChildIndex(this.lbOven, 0);
		base.Controls.SetChildIndex(this.tbPump0, 0);
		base.Controls.SetChildIndex(this.tbPump0hwc, 0);
		base.Controls.SetChildIndex(this.tbAS, 0);
		base.Controls.SetChildIndex(this.tbPump1hwc, 0);
		base.Controls.SetChildIndex(this.tbAShwc, 0);
		base.Controls.SetChildIndex(this.tbDthwc, 0);
		base.Controls.SetChildIndex(this.tbGhwc, 0);
		base.Controls.SetChildIndex(this.tbPump1, 0);
		base.Controls.SetChildIndex(this.tbOvenhwc, 0);
		base.Controls.SetChildIndex(this.tbDt, 0);
		base.Controls.SetChildIndex(this.tbG, 0);
		base.Controls.SetChildIndex(this.tbOven, 0);
		((System.ComponentModel.ISupportInitialize)this.gvHardWares).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		Control0 control = sysCfgControl as Control0;
		lbHardWare.Text = control.HardStr;
		lbHardWare.Tag = control.method_1();
		gvHardWares.RowCount = 0;
		for (int i = 0; i < SysCfgDlg.hardWares.Length; i++)
		{
			if (control.hwStyle == HwStyle.SZ && SysCfgDlg.hardWares[i] is Class10)
			{
				Class10 @class = SysCfgDlg.hardWares[i] as Class10;
				int rowCount;
				gvHardWares.RowCount = (rowCount = gvHardWares.RowCount) + 1;
				int index = rowCount;
				gvHardWares.Rows[index].Tag = @class;
				gvHardWares.Rows[index].Cells[colHardWare.Index].Value = @class.string_0;
				gvHardWares.Rows[index].Cells[colStatus.Index].Value = (@class.bool_0 ? "已配置" : "");
				gvHardWares.Rows[index].Cells[colBool.Index].Value = @class.bool_0;
			}
		}
		tbAS.Text = control.bsCtrls[0].name;
		tbPump0.Text = control.bsCtrls[1].name;
		tbPump1.Text = control.bsCtrls[2].name;
		tbG.Text = control.bsCtrls[3].name;
		tbOven.Text = control.bsCtrls[4].name;
		tbDt.Text = control.bsCtrls[5].name;
		tbAShwc.Text = control.bsCtrls[0].byte_0.ToString();
		tbPump0hwc.Text = control.bsCtrls[1].byte_0.ToString();
		tbPump1hwc.Text = control.bsCtrls[2].byte_0.ToString();
		tbGhwc.Text = control.bsCtrls[3].byte_0.ToString();
		tbOvenhwc.Text = control.bsCtrls[4].byte_0.ToString();
		tbDthwc.Text = control.bsCtrls[5].byte_0.ToString();
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
		Control0 control = sysCfgControl as Control0;
		control.method_2(lbHardWare.Tag);
		control.bsCtrls[0].name = tbAS.Text;
		control.bsCtrls[1].name = tbPump0.Text;
		control.bsCtrls[2].name = tbPump1.Text;
		control.bsCtrls[3].name = tbG.Text;
		control.bsCtrls[4].name = tbOven.Text;
		control.bsCtrls[5].name = tbDt.Text;
		control.bsCtrls[0].byte_0 = byte.Parse(tbAShwc.Text);
		control.bsCtrls[1].byte_0 = byte.Parse(tbPump0hwc.Text);
		control.bsCtrls[2].byte_0 = byte.Parse(tbPump1hwc.Text);
		control.bsCtrls[3].byte_0 = byte.Parse(tbGhwc.Text);
		control.bsCtrls[4].byte_0 = byte.Parse(tbOvenhwc.Text);
		control.bsCtrls[5].byte_0 = byte.Parse(tbDthwc.Text);
	}
}
