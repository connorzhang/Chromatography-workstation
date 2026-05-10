using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.PropertyGridInternal;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class SystemParamDlg : Form
{
	private List<PropertyFilterItem> m_filterList = new List<PropertyFilterItem>();

	private SystemParam sysParam = SystemParam.Create();

	private SystemParamProperty sysPropertyParam = SystemParamProperty.Create();

	private bool bLoadFinished = false;

	private bool bUserPaintChange = false;

	private IContainer components = null;

	public PropertyGrid propGrid;

	private Button btnApply;

	private Button btnReset;

	private Panel panel1;

	public SystemParamDlg()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	private void SystemParamDlgNew_Load(object sender, EventArgs e)
	{
		InitFilter();
		propGrid.Visible = false;
		propGrid.SelectedObject = sysPropertyParam;
		propGrid.PropertyTabs.RemoveTabType(typeof(PropertiesTab));
		propGrid.PropertyTabs.AddTabType(typeof(PropertyTab1Always), PropertyTabScope.Component);
		propGrid.PropertyTabs.AddTabType(typeof(PropertyTab2Adv), PropertyTabScope.Component);
		propGrid.PropertyTabs.AddTabType(typeof(PropertyTab3Adv2), PropertyTabScope.Component);
		propGrid.PropertyTabs.AddTabType(typeof(PropertiesTab));
		propGrid.Visible = true;
		PropertyTab selectedTab = propGrid.SelectedTab;
		Control childAtPoint = propGrid.GetChildAtPoint(new Point(40, 15));
		if (childAtPoint != null)
		{
			ToolStrip toolStrip = (ToolStrip)childAtPoint;
			toolStrip.Items[4].PerformClick();
		}
	}

	private void InitFilter()
	{
		List<PropertyFilterSubItem> list = new List<PropertyFilterSubItem>();
		list.Add(new PropertyFilterSubItem(1, "ShowBackImageInHighSpeed"));
		m_filterList.Add(new PropertyFilterItem("HighSpeedDrawBackImage", list));
		PropertyFilter.AddFilter(typeof(SystemParamProperty).ToString(), SetDetectFilter);
	}

	private void SetDetectFilter(object component, List<PropertyDescriptor> psList)
	{
		if (m_filterList.Count == 0)
		{
			return;
		}
		PropertyFilter.SetDetectFilter(component, psList, m_filterList);
		if (DogFeturlMgr.LicencedDetector())
		{
			return;
		}
		string[] strList = new string[1] { "DetectorOption" };
		int i;
		for (i = 0; i < strList.Length; i++)
		{
			psList.RemoveAll((PropertyDescriptor x) => x.Name == strList[i]);
		}
	}

	private void propGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
	{
		string strName = e.ChangedItem.PropertyDescriptor.Name;
		if (m_filterList.Where((PropertyFilterItem x) => x.strName == strName).ToList().Count > 0)
		{
			propGrid.SelectedObject = sysPropertyParam;
		}
		if (strName == "strIpLocal" || strName == "strIpMask" || strName == "strIpGateway")
		{
			string ipString = (string)e.ChangedItem.Value;
			try
			{
				IPAddress iPAddress = IPAddress.Parse(ipString);
				return;
			}
			catch
			{
				MessageBox.Show("无效的IP地址！");
				return;
			}
		}
		if (strName.StartsWith("corChr"))
		{
			string text = ((Color)e.ChangedItem.Value).ToArgb().ToString();
			string text2 = text;
		}
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		sysParam.SaveParam();
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		sysParam.ResetParam();
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
		this.propGrid = new System.Windows.Forms.PropertyGrid();
		this.btnApply = new System.Windows.Forms.Button();
		this.btnReset = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.propGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propGrid.Location = new System.Drawing.Point(0, 0);
		this.propGrid.Name = "propGrid";
		this.propGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
		this.propGrid.Size = new System.Drawing.Size(334, 469);
		this.propGrid.TabIndex = 0;
		this.propGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(propGrid_PropertyValueChanged);
		this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnApply.Location = new System.Drawing.Point(258, 7);
		this.btnApply.Name = "btnApply";
		this.btnApply.Size = new System.Drawing.Size(64, 20);
		this.btnApply.TabIndex = 2;
		this.btnApply.Text = "应用";
		this.btnApply.UseVisualStyleBackColor = true;
		this.btnApply.Click += new System.EventHandler(btnApply_Click);
		this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReset.Location = new System.Drawing.Point(179, 7);
		this.btnReset.Name = "btnReset";
		this.btnReset.Size = new System.Drawing.Size(67, 20);
		this.btnReset.TabIndex = 3;
		this.btnReset.Text = "默认参数";
		this.btnReset.UseVisualStyleBackColor = true;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.panel1.Controls.Add(this.btnReset);
		this.panel1.Controls.Add(this.btnApply);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 469);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(334, 35);
		this.panel1.TabIndex = 4;
		base.AcceptButton = this.btnApply;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(334, 504);
		base.Controls.Add(this.propGrid);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.Name = "SystemParamDlg";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "系统参数配置";
		base.Load += new System.EventHandler(SystemParamDlgNew_Load);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
