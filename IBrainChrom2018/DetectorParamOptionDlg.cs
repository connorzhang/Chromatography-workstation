using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class DetectorParamOptionDlg : Form
{
	private bool m_bFomularCheck = false;

	private DetectorParam m_approxParam = DetectorParam.Create();

	private IContainer components = null;

	private SimpleButton sbOK;

	private GridControl gcSmooth;

	private GridView gvApprox;

	private GridColumn gclnum;

	private GridColumn gclname;

	private GridColumn gclweight;

	private GridColumn gclhariheight;

	private RepositoryItemSpinEdit riseCount;

	private GridColumn gclCurveType;

	private GridColumn gclSection;

	private Panel panel1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn3;

	private GridColumn gridColumn4;

	private GridColumn gridColumn5;

	private SimpleButton sbCheckFomular;

	private RepositoryItemCheckEdit riceEnable;

	private GridColumn gridColumn6;

	public DetectorParamOptionDlg()
	{
		InitializeComponent();
	}

	private void sbReplace_Click(object sender, EventArgs e)
	{
		if (!m_bFomularCheck)
		{
			MessageBox.Show("请先验证公式后重试!");
			return;
		}
		DataTable dtApprox = (DataTable)gcSmooth.DataSource;
		m_approxParam.SaveParam(dtApprox);
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void ceReAlign_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void ApproxOptionDlg_Load(object sender, EventArgs e)
	{
		gcSmooth.DataSource = m_approxParam.GetDataTable();
	}

	private int GetIDCount(int myid)
	{
		int num = 0;
		DataTable dataTable = (DataTable)gcSmooth.DataSource;
		DataView defaultView = dataTable.DefaultView;
		foreach (DataRowView item in defaultView)
		{
			int num2 = (int)item[0];
			if (myid == num2)
			{
				num++;
			}
		}
		return num;
	}

	private void sbCheckFomular_Click(object sender, EventArgs e)
	{
		int num = 0;
		m_bFomularCheck = false;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		DataTable dataTable = (DataTable)gcSmooth.DataSource;
		DataView defaultView = dataTable.DefaultView;
		foreach (DataRowView item2 in defaultView)
		{
			int myid = (int)item2[0];
			string item = (string)item2[2];
			float num2 = (float)item2[7];
			if (GetIDCount(myid) > 1)
			{
				MessageBox.Show(this, "特征码不能重复！");
				return;
			}
			list.Add(myid.ToString());
			list2.Add(item);
		}
		CalculateExpression calculateExpression = CalculateExpression.Create();
		calculateExpression.AddCalculate(list.ToArray(), list2.ToArray(), 1);
		foreach (DataRowView item3 in defaultView)
		{
			int num3 = (int)item3[0];
			float num4 = (float)item3[7];
			double num5 = (double)calculateExpression.RunExpression(num3.ToString(), num4);
			gvApprox.SetRowCellValue(num++, "col11", num5);
		}
		m_bFomularCheck = true;
		MessageBox.Show(this, "公式验证成功！");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.DetectorParamOptionDlg));
		this.sbOK = new DevExpress.XtraEditors.SimpleButton();
		this.gcSmooth = new DevExpress.XtraGrid.GridControl();
		this.gvApprox = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gclnum = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gclname = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gclweight = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gclhariheight = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gclCurveType = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.riseCount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gclSection = new DevExpress.XtraGrid.Columns.GridColumn();
		this.riceEnable = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.panel1 = new System.Windows.Forms.Panel();
		this.sbCheckFomular = new DevExpress.XtraEditors.SimpleButton();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		((System.ComponentModel.ISupportInitialize)this.gcSmooth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvApprox).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riseCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riceEnable).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.sbOK, "sbOK");
		this.sbOK.Name = "sbOK";
		this.sbOK.Click += new System.EventHandler(sbReplace_Click);
		resources.ApplyResources(this.gcSmooth, "gcSmooth");
		this.gcSmooth.EmbeddedNavigator.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("gcSmooth.EmbeddedNavigator.Anchor");
		this.gcSmooth.EmbeddedNavigator.BackgroundImageLayout = (System.Windows.Forms.ImageLayout)resources.GetObject("gcSmooth.EmbeddedNavigator.BackgroundImageLayout");
		this.gcSmooth.EmbeddedNavigator.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("gcSmooth.EmbeddedNavigator.ImeMode");
		this.gcSmooth.MainView = this.gvApprox;
		this.gcSmooth.Name = "gcSmooth";
		this.gcSmooth.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2] { this.riseCount, this.riceEnable });
		this.gcSmooth.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gvApprox });
		this.gvApprox.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[12]
		{
			this.gclnum, this.gclname, this.gclweight, this.gclhariheight, this.gclCurveType, this.gridColumn1, this.gridColumn2, this.gridColumn3, this.gclSection, this.gridColumn4,
			this.gridColumn5, this.gridColumn6
		});
		this.gvApprox.GridControl = this.gcSmooth;
		resources.ApplyResources(this.gvApprox, "gvApprox");
		this.gvApprox.Name = "gvApprox";
		this.gvApprox.OptionsCustomization.AllowFilter = false;
		this.gvApprox.OptionsCustomization.AllowGroup = false;
		this.gvApprox.OptionsDetail.AllowZoomDetail = false;
		this.gvApprox.OptionsDetail.EnableMasterViewMode = false;
		this.gvApprox.OptionsDetail.SmartDetailExpand = false;
		this.gvApprox.OptionsView.ShowDetailButtons = false;
		this.gvApprox.OptionsView.ShowGroupPanel = false;
		resources.ApplyResources(this.gclnum, "gclnum");
		this.gclnum.FieldName = "col0";
		this.gclnum.Name = "gclnum";
		resources.ApplyResources(this.gclname, "gclname");
		this.gclname.FieldName = "col1";
		this.gclname.Name = "gclname";
		resources.ApplyResources(this.gclweight, "gclweight");
		this.gclweight.DisplayFormat.FormatString = "f1";
		this.gclweight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gclweight.FieldName = "col2";
		this.gclweight.Name = "gclweight";
		resources.ApplyResources(this.gclhariheight, "gclhariheight");
		this.gclhariheight.FieldName = "col3";
		this.gclhariheight.Name = "gclhariheight";
		resources.ApplyResources(this.gclCurveType, "gclCurveType");
		this.gclCurveType.FieldName = "col4";
		this.gclCurveType.Name = "gclCurveType";
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.FieldName = "col5";
		this.gridColumn1.Name = "gridColumn1";
		resources.ApplyResources(this.gridColumn2, "gridColumn2");
		this.gridColumn2.ColumnEdit = this.riseCount;
		this.gridColumn2.FieldName = "col6";
		this.gridColumn2.Name = "gridColumn2";
		resources.ApplyResources(this.riseCount, "riseCount");
		this.riseCount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.riseCount.Name = "riseCount";
		this.gridColumn3.AppearanceCell.ForeColor = (System.Drawing.Color)resources.GetObject("gridColumn3.AppearanceCell.ForeColor");
		this.gridColumn3.AppearanceCell.Options.UseForeColor = true;
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.ColumnEdit = this.riseCount;
		this.gridColumn3.FieldName = "col7";
		this.gridColumn3.Name = "gridColumn3";
		resources.ApplyResources(this.gclSection, "gclSection");
		this.gclSection.ColumnEdit = this.riceEnable;
		this.gclSection.FieldName = "col8";
		this.gclSection.Name = "gclSection";
		resources.ApplyResources(this.riceEnable, "riceEnable");
		this.riceEnable.Name = "riceEnable";
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.FieldName = "col9";
		this.gridColumn4.Name = "gridColumn4";
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.FieldName = "col10";
		this.gridColumn5.Name = "gridColumn5";
		this.panel1.Controls.Add(this.sbCheckFomular);
		this.panel1.Controls.Add(this.sbOK);
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.Name = "panel1";
		resources.ApplyResources(this.sbCheckFomular, "sbCheckFomular");
		this.sbCheckFomular.Name = "sbCheckFomular";
		this.sbCheckFomular.Click += new System.EventHandler(sbCheckFomular_Click);
		this.gridColumn6.AppearanceCell.ForeColor = (System.Drawing.Color)resources.GetObject("gridColumn6.AppearanceCell.ForeColor");
		this.gridColumn6.AppearanceCell.Options.UseForeColor = true;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.FieldName = "col11";
		this.gridColumn6.Name = "gridColumn6";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.gcSmooth);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "DetectorParamOptionDlg";
		base.ShowInTaskbar = false;
		base.TopMost = true;
		base.Load += new System.EventHandler(ApproxOptionDlg_Load);
		((System.ComponentModel.ISupportInitialize)this.gcSmooth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvApprox).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riseCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riceEnable).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
