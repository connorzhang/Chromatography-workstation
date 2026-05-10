using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SZ_DtSetupForm : CtrlSetupDlg
{
	private Button btnDisConn;

	private DataGridViewCheckBoxColumn colBool;

	private DataGridViewTextBoxColumn colHardWare;

	private DataGridViewTextBoxColumn colStatus;

	private IContainer icontainer_2;

	private LclGridView gvHardWares;

	private LclLabel lbChl0;

	private LclLabel lbChl1;

	private LclLabel lbHardWare;

	private LclLabel lbHardWareV;

	private LclTextBox tbChl0;

	private LclTextBox tbChl1;

	public SZ_DtSetupForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent();
		lbHardWare.Text = Lang.PS("硬件", "Hard Ware");
		btnDisConn.Text = Lang.PS("断开", "Disconn");
		gvHardWares.Columns[0].HeaderText = lbHardWare.Text;
		gvHardWares.Columns[2].HeaderText = Lang.PS("状态", "State");
		lbChl0.Text = Lang.PS("通道一", "Chl. 1");
		lbChl1.Text = Lang.PS("通道二", "Chl. 2");
	}

	private void btnDisConn_Click(object sender, EventArgs e)
	{
		lbHardWareV.Text = "";
		lbHardWareV.Tag = null;
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
			lbHardWareV.Text = gvHardWares.Rows[rowIndex].Cells[colHardWare.Index].Value.ToString();
			lbHardWareV.Tag = gvHardWares.Rows[rowIndex].Tag;
		}
	}

	private void InitializeComponent()
	{
		this.tbChl1 = new IBrainChrom2018.LclTextBox();
		this.tbChl0 = new IBrainChrom2018.LclTextBox();
		this.btnDisConn = new System.Windows.Forms.Button();
		this.gvHardWares = new IBrainChrom2018.LclGridView();
		this.colHardWare = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colBool = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lbChl1 = new IBrainChrom2018.LclLabel();
		this.lbHardWareV = new IBrainChrom2018.LclLabel();
		this.lbChl0 = new IBrainChrom2018.LclLabel();
		this.lbHardWare = new IBrainChrom2018.LclLabel();
		((System.ComponentModel.ISupportInitialize)this.gvHardWares).BeginInit();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(26, 202);
		base.btnOK.Text = "确认";
		base.btnCancel.Location = new System.Drawing.Point(116, 202);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(208, 202);
		base.btnHelp.Text = "帮助";
		this.tbChl1.Location = new System.Drawing.Point(55, 164);
		this.tbChl1.Name = "tbChl1";
		this.tbChl1.Size = new System.Drawing.Size(245, 20);
		this.tbChl1.TabIndex = 12;
		this.tbChl0.Location = new System.Drawing.Point(55, 139);
		this.tbChl0.Name = "tbChl0";
		this.tbChl0.Size = new System.Drawing.Size(245, 20);
		this.tbChl0.TabIndex = 13;
		this.btnDisConn.Location = new System.Drawing.Point(244, 10);
		this.btnDisConn.Name = "btnDisConn";
		this.btnDisConn.Size = new System.Drawing.Size(58, 23);
		this.btnDisConn.TabIndex = 10;
		this.btnDisConn.Text = "断开";
		this.btnDisConn.UseVisualStyleBackColor = true;
		this.btnDisConn.Click += new System.EventHandler(btnDisConn_Click);
		this.gvHardWares.AllowUserToAddRows = false;
		this.gvHardWares.AllowUserToDeleteRows = false;
		this.gvHardWares.AllowUserToResizeRows = false;
		this.gvHardWares.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvHardWares.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvHardWares.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvHardWares.Columns.AddRange(this.colHardWare, this.colBool, this.colStatus);
		this.gvHardWares.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvHardWares.Location = new System.Drawing.Point(12, 39);
		this.gvHardWares.MultiSelect = false;
		this.gvHardWares.Name = "gvHardWares";
		this.gvHardWares.ReadOnly = true;
		this.gvHardWares.RowHeadersWidth = 25;
		this.gvHardWares.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvHardWares.RowTemplate.Height = 16;
		this.gvHardWares.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvHardWares.ShowCellToolTips = false;
		this.gvHardWares.Size = new System.Drawing.Size(289, 94);
		this.gvHardWares.TabIndex = 9;
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
		this.lbChl1.AutoSize = true;
		this.lbChl1.Location = new System.Drawing.Point(9, 167);
		this.lbChl1.Name = "lbChl1";
		this.lbChl1.Size = new System.Drawing.Size(43, 13);
		this.lbChl1.TabIndex = 6;
		this.lbChl1.Text = "通道二";
		this.lbHardWareV.AutoSize = true;
		this.lbHardWareV.Location = new System.Drawing.Point(73, 15);
		this.lbHardWareV.Name = "lbHardWareV";
		this.lbHardWareV.Size = new System.Drawing.Size(49, 13);
		this.lbHardWareV.TabIndex = 5;
		this.lbHardWareV.Text = "lclLabel1";
		this.lbChl0.AutoSize = true;
		this.lbChl0.Location = new System.Drawing.Point(9, 142);
		this.lbChl0.Name = "lbChl0";
		this.lbChl0.Size = new System.Drawing.Size(43, 13);
		this.lbChl0.TabIndex = 8;
		this.lbChl0.Text = "通道一";
		this.lbHardWare.AutoSize = true;
		this.lbHardWare.Location = new System.Drawing.Point(12, 15);
		this.lbHardWare.Name = "lbHardWare";
		this.lbHardWare.Size = new System.Drawing.Size(31, 13);
		this.lbHardWare.TabIndex = 7;
		this.lbHardWare.Text = "硬件";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.ClientSize = new System.Drawing.Size(312, 235);
		base.Controls.Add(this.tbChl1);
		base.Controls.Add(this.tbChl0);
		base.Controls.Add(this.btnDisConn);
		base.Controls.Add(this.gvHardWares);
		base.Controls.Add(this.lbChl1);
		base.Controls.Add(this.lbHardWareV);
		base.Controls.Add(this.lbChl0);
		base.Controls.Add(this.lbHardWare);
		base.Name = "SZ_DtSetupForm";
		this.Text = "";
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbHardWare, 0);
		base.Controls.SetChildIndex(this.lbChl0, 0);
		base.Controls.SetChildIndex(this.lbHardWareV, 0);
		base.Controls.SetChildIndex(this.lbChl1, 0);
		base.Controls.SetChildIndex(this.gvHardWares, 0);
		base.Controls.SetChildIndex(this.btnDisConn, 0);
		base.Controls.SetChildIndex(this.tbChl0, 0);
		base.Controls.SetChildIndex(this.tbChl1, 0);
		((System.ComponentModel.ISupportInitialize)this.gvHardWares).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadControl(SysCfgControl sysCfgControl)
	{
		base.LoadControl(sysCfgControl);
		SZ_Dt sZ_Dt = sysCfgControl as SZ_Dt;
		lbHardWareV.Text = sZ_Dt.HardStr;
		lbHardWareV.Tag = sZ_Dt.HardWare;
		gvHardWares.RowCount = 0;
		for (int i = 0; i < SysCfgDlg.hardWares.Length; i++)
		{
			if (sZ_Dt.hwStyle == HwStyle.SZ && SysCfgDlg.hardWares[i] is UsbSZ)
			{
				UsbSZ usbSZ = SysCfgDlg.hardWares[i] as UsbSZ;
				int rowCount;
				gvHardWares.RowCount = (rowCount = gvHardWares.RowCount) + 1;
				int index = rowCount;
				gvHardWares.Rows[index].Tag = usbSZ;
				gvHardWares.Rows[index].Cells[colHardWare.Index].Value = usbSZ.productString;
				gvHardWares.Rows[index].Cells[colStatus.Index].Value = (usbSZ.installed ? Lang.PS("已配置", "Equiped") : "");
				gvHardWares.Rows[index].Cells[colBool.Index].Value = usbSZ.installed;
			}
		}
		tbChl0.Text = sZ_Dt.bsCtrls[0].name;
		tbChl1.Text = sZ_Dt.bsCtrls[1].name;
	}

	public override void WriteControl(SysCfgControl sysCfgControl)
	{
		base.WriteControl(sysCfgControl);
		SZ_Dt sZ_Dt = sysCfgControl as SZ_Dt;
		sZ_Dt.HardWare = lbHardWareV.Tag;
		sZ_Dt.bsCtrls[0].name = tbChl0.Text;
		sZ_Dt.bsCtrls[1].name = tbChl1.Text;
	}
}
