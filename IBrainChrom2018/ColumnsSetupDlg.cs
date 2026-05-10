using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class ColumnsSetupDlg : LclDialog
{
	private string[] string_8;

	private string[] string_9;

	private string[] string_10;

	private string string_11;

	private string string_12;

	private string[] string_13;

	private string[] string_14;

	private LclButton btnBottom;

	private LclButton btnDown;

	private LclButton btnHide;

	private LclButton btnHideAll;

	private LclButton btnShow;

	private LclButton btnShowAll;

	private LclButton btnTop;

	private LclButton btnUp;

	private DataGridViewColumn[] dataGridViewColumn_0;

	private DataGridViewColumn dataGridViewColumn_1;

	private DataGridViewColumn[] dataGridViewColumn_2;

	private IContainer icontainer_1;

	private LclGroupBox gbSetColumnProperties;

	private LclGridView lclGridView_0;

	private LclLabel lbDecimalPlaces;

	private LclLabel lbHideColumns;

	private LclListBox lbHideColumnsV;

	private LclLabel lbPreview;

	private LclLabel lbShowColumns;

	private LclListBox lbShowColumnsV;

	private LclPanel lclPanel_0;

	private LclTextBox tbDecimalPlaces;

	private LclTextBox tbPreview;

	private LclTabControl tcColumnsSetup;

	private TabPage tpColumns1;

	private TabPage tpColumns2;

	private string sTitle => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => string_11, 
		SysLanguage.EN => string_12, 
		_ => "", 
	};

	public ColumnsSetupDlg()
	{
		icontainer_1 = null;
		string_11 = "";
		string_12 = "";
		dataGridViewColumn_0 = new DataGridViewColumn[0];
		string_8 = new string[0];
		InitializeComponent();
	}

	public ColumnsSetupDlg(string scnTitle, string senTitle)
	{
		icontainer_1 = null;
		string_11 = "";
		string_12 = "";
		dataGridViewColumn_0 = new DataGridViewColumn[0];
		string_8 = new string[0];
		InitializeComponent();
		lclPanel_0.BringToFront();
		string_11 = scnTitle;
		string_12 = senTitle;
		lclPanel_0.Parent = this;
		tcColumnsSetup.Parent = this;
		ResourceImageLoad.SetCtrlBitmap(btnTop, SystemIconResource.smethod_43());
		ResourceImageLoad.SetCtrlBitmap(btnUp, SystemIconResource.smethod_44());
		ResourceImageLoad.SetCtrlBitmap(btnDown, SystemIconResource.smethod_42());
		ResourceImageLoad.SetCtrlBitmap(btnBottom, SystemIconResource.smethod_41());
	}

	private void method_0(int int_0, string string_15)
	{
		int index = lbShowColumnsV.Items.IndexOf(string_15);
		string value = lbShowColumnsV.Items[int_0].ToString();
		lbShowColumnsV.Items[int_0] = string_15;
		lbShowColumnsV.Items[index] = value;
		lbShowColumnsV.SetSelected(index, value: false);
		lbShowColumnsV.SetSelected(int_0, value: true);
	}

	private void btnShowAll_Click(object sender, EventArgs e)
	{
		if (sender == btnShowAll)
		{
			for (int i = 0; i < lbHideColumnsV.Items.Count; i++)
			{
				lbShowColumnsV.Items.Add(lbHideColumnsV.Items[i]);
			}
			lbHideColumnsV.Items.Clear();
		}
		else if (sender == btnShow)
		{
			if (lbHideColumnsV.SelectedItems != null)
			{
				string[] array = new string[lbHideColumnsV.SelectedItems.Count];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = lbHideColumnsV.SelectedItems[j].ToString();
				}
				for (int k = 0; k < array.Length; k++)
				{
					lbShowColumnsV.Items.Add(array[k]);
					lbHideColumnsV.Items.Remove(array[k]);
				}
				lbHideColumnsV.SelectedItem = null;
			}
		}
		else if (sender == btnHide)
		{
			if (lbShowColumnsV.SelectedItems != null)
			{
				string[] array2 = new string[lbShowColumnsV.SelectedItems.Count];
				for (int l = 0; l < array2.Length; l++)
				{
					array2[l] = lbShowColumnsV.SelectedItems[l].ToString();
				}
				for (int m = 0; m < array2.Length; m++)
				{
					lbHideColumnsV.Items.Add(array2[m]);
					lbShowColumnsV.Items.Remove(array2[m]);
				}
				lbShowColumnsV.SelectedItem = null;
			}
		}
		else if (sender == btnHideAll)
		{
			for (int n = 0; n < lbShowColumnsV.Items.Count; n++)
			{
				lbHideColumnsV.Items.Add(lbShowColumnsV.Items[n]);
			}
			lbShowColumnsV.Items.Clear();
		}
		if (tcColumnsSetup.TabCount == 2)
		{
			method_4();
		}
		method_2();
	}

	private void btnTop_Click(object sender, EventArgs e)
	{
		if (lbShowColumnsV.SelectedItems == null)
		{
			return;
		}
		string[] array = new string[lbShowColumnsV.SelectedItems.Count];
		string[] array2 = new string[lbShowColumnsV.Items.Count - lbShowColumnsV.SelectedItems.Count];
		int i;
		for (i = 0; i < array.Length; i++)
		{
			array[i] = lbShowColumnsV.SelectedItems[i].ToString();
		}
		int num = 0;
		i = 0;
		for (i = 0; i < lbShowColumnsV.Items.Count; i++)
		{
			string text = lbShowColumnsV.Items[i].ToString();
			bool flag = false;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Equals(text))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				array2[num++] = text;
			}
		}
		if (sender == btnTop)
		{
			for (i = 0; i < array.Length; i++)
			{
				lbShowColumnsV.Items[i] = array[i];
				lbShowColumnsV.SetSelected(i, value: true);
			}
			for (i = 0; i < array2.Length; i++)
			{
				lbShowColumnsV.Items[array.Length + i] = array2[i];
				lbShowColumnsV.SetSelected(array.Length + i, value: false);
			}
		}
		else if (sender == btnUp)
		{
			for (i = 0; i < array.Length; i++)
			{
				int num2 = lbShowColumnsV.Items.IndexOf(array[i]);
				if (num2 > i)
				{
					method_0(num2 - 1, array[i]);
				}
			}
		}
		else if (sender == btnDown)
		{
			int num3 = 0;
			for (i = array.Length - 1; i >= 0; i--)
			{
				int num4 = lbShowColumnsV.Items.IndexOf(array[i]);
				int num5 = lbShowColumnsV.Items.Count - 1 - num3;
				num3++;
				if (num4 < num5)
				{
					method_0(num4 + 1, array[i]);
				}
			}
		}
		else if (sender == btnBottom)
		{
			for (i = 0; i < array2.Length; i++)
			{
				lbShowColumnsV.Items[i] = array2[i];
				lbShowColumnsV.SetSelected(i, value: false);
			}
			for (i = 0; i < array.Length; i++)
			{
				lbShowColumnsV.Items[array2.Length + i] = array[i];
				lbShowColumnsV.SetSelected(array2.Length + i, value: true);
			}
		}
		if (tcColumnsSetup.TabCount == 2)
		{
			method_4();
		}
	}

	private void ColumnsSetupDlg_Load(object sender, EventArgs e)
	{
		Array.Resize(ref dataGridViewColumn_0, 0);
		Array.Resize(ref string_8, 0);
		tbDecimalPlaces.Text = "";
	}

	private void lbHideColumnsV_MouseDown(object sender, MouseEventArgs e)
	{
		lbShowColumnsV.SelectedItem = null;
	}

	private void lbHideColumnsV_SelectedValueChanged(object sender, EventArgs e)
	{
		method_2();
	}

	private void lbShowColumnsV_MouseDown(object sender, MouseEventArgs e)
	{
		lbHideColumnsV.SelectedItem = null;
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = string_11;
			lbHideColumns.Text = Lang.PS("隐藏列", "Hide Columns");
			lbShowColumns.Text = Lang.PS("显示列", "Show Columns");
			gbSetColumnProperties.Text = Lang.PS("设置列属性", "Set Column Properties");
			lbDecimalPlaces.Text = Lang.PS("小数位数", "Dec.Places");
			lbPreview.Text = Lang.PS("预览", "Preview");
			break;
		case SysLanguage.EN:
			Text = string_12;
			lbHideColumns.Text = "Hide Columns";
			lbShowColumns.Text = "Show Columns";
			gbSetColumnProperties.Text = "Set Column Properties";
			lbDecimalPlaces.Text = "Dec.Places";
			lbPreview.Text = "Preview";
			break;
		}
	}

	private void method_1(AccStyle accStyle_0)
	{
		if (dataGridViewColumn_1 == null)
		{
			accStyle_0 = AccStyle.Clear;
		}
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			for (int i = 0; i < dataGridViewColumn_0.Length; i++)
			{
				if (dataGridViewColumn_0[i] == dataGridViewColumn_1)
				{
					tbDecimalPlaces.Text = string_8[i];
					return;
				}
			}
			string text = "";
			string format = dataGridViewColumn_1.DefaultCellStyle.Format;
			if (format != null && format.StartsWith("F"))
			{
				text = format.Remove(0, 1);
			}
			tbDecimalPlaces.Text = text;
			break;
		}
		case AccStyle.Write:
			tbDecimalPlaces.Text = Class49.Object2Int(tbDecimalPlaces.Text, 2).ToString();
			dataGridViewColumn_1.DefaultCellStyle.Format = "F" + tbDecimalPlaces.Text;
			break;
		case AccStyle.Clear:
			tbDecimalPlaces.Text = "";
			break;
		}
	}

	private void method_2()
	{
		btnShowAll.Enabled = lbHideColumnsV.Items.Count != 0;
		btnHideAll.Enabled = lbShowColumnsV.Items.Count != 0;
		btnShow.Enabled = lbHideColumnsV.SelectedItem != null;
		btnHide.Enabled = lbShowColumnsV.SelectedItem != null;
		LclButton lclButton = btnTop;
		LclButton lclButton2 = btnUp;
		LclButton lclButton3 = btnDown;
		bool flag = (btnBottom.Enabled = btnHide.Enabled);
		bool flag2 = (lclButton3.Enabled = flag);
		bool enabled2 = (lclButton2.Enabled = flag2);
		lclButton.Enabled = enabled2;
		dataGridViewColumn_1 = null;
		gbSetColumnProperties.Enabled = lbShowColumnsV.SelectedItems.Count <= 1;
		if (gbSetColumnProperties.Enabled && lbShowColumnsV.SelectedItem != null)
		{
			if (tcColumnsSetup.SelectedTab == tpColumns1)
			{
				dataGridViewColumn_1 = lclGridView_0.Columns[lbShowColumnsV.SelectedItem.ToString()];
			}
			if (tcColumnsSetup.TabCount == 2)
			{
				for (int i = 0; i < dataGridViewColumn_2.Length; i++)
				{
					if (dataGridViewColumn_2[i].Name == lbShowColumnsV.SelectedItem.ToString())
					{
						dataGridViewColumn_1 = dataGridViewColumn_2[i];
						break;
					}
				}
			}
			method_1(AccStyle.Read);
		}
		else
		{
			method_1(AccStyle.Clear);
		}
	}

	private void method_3(AccStyle accStyle_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			lbShowColumnsV.Items.Clear();
			for (int k = 0; k < lclGridView_0.showColumns.Length; k++)
			{
				lbShowColumnsV.Items.Add(lclGridView_0.showColumns[k].HeaderText + "_" + lclGridView_0.showColumns[k].Name);
			}
			lbHideColumnsV.Items.Clear();
			for (int l = 0; l < lclGridView_0.hideColumns.Length; l++)
			{
				lbHideColumnsV.Items.Add(lclGridView_0.hideColumns[l].HeaderText + "_" + lclGridView_0.hideColumns[l].Name);
			}
			break;
		}
		case AccStyle.Write:
		{
			lclGridView_0.ArrayShowHideColumns(show: true, lbShowColumnsV.Items.Count);
			for (int i = 0; i < lclGridView_0.showColumns.Length; i++)
			{
				lclGridView_0.showColumns[i] = lclGridView_0.Columns[lbShowColumnsV.Items[i].ToString().Split('_')[1]];
			}
			lclGridView_0.ArrayShowHideColumns(show: false, lbHideColumnsV.Items.Count);
			for (int j = 0; j < lclGridView_0.hideColumns.Length; j++)
			{
				lclGridView_0.hideColumns[j] = lclGridView_0.Columns[lbHideColumnsV.Items[j].ToString().Split('_')[1]];
			}
			break;
		}
		}
	}

	private void method_4()
	{
		if (tcColumnsSetup.SelectedTab == tpColumns1)
		{
			Array.Resize(ref string_13, lbShowColumnsV.Items.Count);
			for (int i = 0; i < string_13.Length; i++)
			{
				string_13[i] = lbShowColumnsV.Items[i].ToString();
			}
			Array.Resize(ref string_9, lbHideColumnsV.Items.Count);
			for (int j = 0; j < string_9.Length; j++)
			{
				string_9[j] = lbHideColumnsV.Items[j].ToString();
			}
		}
		else if (tcColumnsSetup.SelectedTab == tpColumns2)
		{
			Array.Resize(ref string_14, lbShowColumnsV.Items.Count);
			for (int k = 0; k < string_14.Length; k++)
			{
				string_14[k] = lbShowColumnsV.Items[k].ToString();
			}
			Array.Resize(ref string_10, lbHideColumnsV.Items.Count);
			for (int l = 0; l < string_10.Length; l++)
			{
				string_10[l] = lbHideColumnsV.Items[l].ToString();
			}
		}
	}

	public DialogResult ShowDialog(LclGridView lclGridView_1)
	{
		tcColumnsSetup.TabPages.Clear();
		tcColumnsSetup.TabPages.Add(tpColumns1);
		tpColumns1.Text = sTitle;
		lclGridView_0 = lclGridView_1;
		lclGridView_1.Refresh_ShowHideColumns(AccStyle.Read);
		method_3(AccStyle.Read);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_3(AccStyle.Write);
			if (lclGridView_1 is LclCombineCGridView)
			{
				(lclGridView_1 as LclCombineCGridView).AdjustCombineDisInfo(read_refresh: false);
			}
			lclGridView_1.Refresh_ShowHideColumns(AccStyle.Write);
			for (int i = 0; i < dataGridViewColumn_0.Length; i++)
			{
				dataGridViewColumn_0[i].DefaultCellStyle.Format = "F" + string_8[i];
			}
		}
		return dialogResult;
	}

	public DialogResult ShowDialog(LclSummaryGridView lclSummaryGridView_0, InstruStyle instruStyle_0, string column1Text, string column2Text, DataGridViewColumn[] columns1, DataGridViewColumn[] shows1, DataGridViewColumn[] hides1, DataGridViewColumn[] columns2, DataGridViewColumn[] shows2, DataGridViewColumn[] hides2)
	{
		tcColumnsSetup.TabPages.Clear();
		tcColumnsSetup.TabPages.Add(tpColumns1);
		tcColumnsSetup.TabPages.Add(tpColumns2);
		tpColumns1.Text = column1Text;
		tpColumns2.Text = column2Text;
		lclGridView_0 = lclSummaryGridView_0;
		dataGridViewColumn_2 = columns2;
		Array.Resize(ref string_13, shows1.Length);
		for (int i = 0; i < string_13.Length; i++)
		{
			string_13[i] = shows1[i].Name;
		}
		Array.Resize(ref string_9, hides1.Length);
		for (int j = 0; j < string_9.Length; j++)
		{
			string_9[j] = hides1[j].Name;
		}
		Array.Resize(ref string_14, shows2.Length);
		for (int k = 0; k < string_14.Length; k++)
		{
			string_14[k] = shows2[k].Name;
		}
		Array.Resize(ref string_10, hides2.Length);
		for (int l = 0; l < string_10.Length; l++)
		{
			string_10[l] = hides2[l].Name;
		}
		tcColumnsSetup_SelectedIndexChanged(null, null);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			lclSummaryGridView_0.ArrayComSHColumns(show: true, string_13.Length);
			for (int m = 0; m < string_13.Length; m++)
			{
				lclSummaryGridView_0.AddComShowLink(m, string_13[m]);
			}
			lclSummaryGridView_0.FinishComHideLinks();
			lclSummaryGridView_0.ArraySmySHColumns(instruStyle_0, show: true, string_14.Length);
			for (int n = 0; n < string_14.Length; n++)
			{
				lclSummaryGridView_0.AddSmyShowLink(instruStyle_0, n, string_14[n]);
			}
			lclSummaryGridView_0.FinishSmyHideLinks(instruStyle_0);
			for (int num = 0; num < dataGridViewColumn_0.Length; num++)
			{
				dataGridViewColumn_0[num].DefaultCellStyle.Format = "F" + string_8[num];
			}
		}
		return dialogResult;
	}

	private void tbDecimalPlaces_KeyUp(object sender, KeyEventArgs e)
	{
		if (dataGridViewColumn_1 == null || !(tbDecimalPlaces.Text.Trim() != ""))
		{
			return;
		}
		string text = tbDecimalPlaces.Text.Trim();
		if (!int.TryParse(text, out var result) || result < 0)
		{
			return;
		}
		result = -1;
		for (int i = 0; i < dataGridViewColumn_0.Length; i++)
		{
			if (dataGridViewColumn_0[i] == dataGridViewColumn_1)
			{
				result = i;
				if (result == -1)
				{
					int num = dataGridViewColumn_0.Length;
					Array.Resize(ref dataGridViewColumn_0, num + 1);
					dataGridViewColumn_0[num] = dataGridViewColumn_1;
					Array.Resize(ref string_8, num + 1);
					string_8[num] = text;
				}
				else
				{
					string_8[result] = text;
				}
				break;
			}
		}
	}

	private void tcColumnsSetup_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tcColumnsSetup.SelectedTab == tpColumns1)
		{
			lbShowColumnsV.Items.Clear();
			for (int i = 0; i < string_13.Length; i++)
			{
				lbShowColumnsV.Items.Add(string_13[i]);
			}
			lbHideColumnsV.Items.Clear();
			for (int j = 0; j < string_9.Length; j++)
			{
				lbHideColumnsV.Items.Add(string_9[j]);
			}
		}
		else if (tcColumnsSetup.SelectedTab == tpColumns2)
		{
			lbShowColumnsV.Items.Clear();
			for (int k = 0; k < string_14.Length; k++)
			{
				lbShowColumnsV.Items.Add(string_14[k]);
			}
			lbHideColumnsV.Items.Clear();
			for (int l = 0; l < string_10.Length; l++)
			{
				lbHideColumnsV.Items.Add(string_10[l]);
			}
		}
	}

	private void method_5(object sender, EventArgs e)
	{
	}

	private void method_6(object sender, EventArgs e)
	{
	}

	private void method_7(object sender, EventArgs e)
	{
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ColumnsSetupDlg));
		this.lclPanel_0 = new IBrainChrom2018.LclPanel();
		this.gbSetColumnProperties = new IBrainChrom2018.LclGroupBox();
		this.tbPreview = new IBrainChrom2018.LclTextBox();
		this.tbDecimalPlaces = new IBrainChrom2018.LclTextBox();
		this.lbDecimalPlaces = new IBrainChrom2018.LclLabel();
		this.lbPreview = new IBrainChrom2018.LclLabel();
		this.btnHideAll = new IBrainChrom2018.LclButton();
		this.btnHide = new IBrainChrom2018.LclButton();
		this.btnShow = new IBrainChrom2018.LclButton();
		this.btnBottom = new IBrainChrom2018.LclButton();
		this.btnDown = new IBrainChrom2018.LclButton();
		this.btnUp = new IBrainChrom2018.LclButton();
		this.btnTop = new IBrainChrom2018.LclButton();
		this.btnShowAll = new IBrainChrom2018.LclButton();
		this.lbShowColumnsV = new IBrainChrom2018.LclListBox();
		this.lbHideColumnsV = new IBrainChrom2018.LclListBox();
		this.lbShowColumns = new IBrainChrom2018.LclLabel();
		this.lbHideColumns = new IBrainChrom2018.LclLabel();
		this.tcColumnsSetup = new IBrainChrom2018.LclTabControl();
		this.tpColumns1 = new System.Windows.Forms.TabPage();
		this.tpColumns2 = new System.Windows.Forms.TabPage();
		this.lclPanel_0.SuspendLayout();
		this.gbSetColumnProperties.SuspendLayout();
		this.tcColumnsSetup.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(211, 386);
		base.btnCancel.Text = "取消";
		base.btnCancel.Click += new System.EventHandler(method_6);
		base.btnHelp.Location = new System.Drawing.Point(307, 386);
		base.btnHelp.Text = "帮助";
		base.btnHelp.Click += new System.EventHandler(method_7);
		base.btnOK.Location = new System.Drawing.Point(125, 386);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_5);
		this.lclPanel_0.Controls.Add(this.gbSetColumnProperties);
		this.lclPanel_0.Controls.Add(this.btnHideAll);
		this.lclPanel_0.Controls.Add(this.btnHide);
		this.lclPanel_0.Controls.Add(this.btnShow);
		this.lclPanel_0.Controls.Add(this.btnBottom);
		this.lclPanel_0.Controls.Add(this.btnDown);
		this.lclPanel_0.Controls.Add(this.btnUp);
		this.lclPanel_0.Controls.Add(this.btnTop);
		this.lclPanel_0.Controls.Add(this.btnShowAll);
		this.lclPanel_0.Controls.Add(this.lbShowColumnsV);
		this.lclPanel_0.Controls.Add(this.lbHideColumnsV);
		this.lclPanel_0.Controls.Add(this.lbShowColumns);
		this.lclPanel_0.Controls.Add(this.lbHideColumns);
		this.lclPanel_0.Location = new System.Drawing.Point(9, 31);
		this.lclPanel_0.Name = "pnl";
		this.lclPanel_0.Size = new System.Drawing.Size(431, 341);
		this.lclPanel_0.TabIndex = 1;
		this.gbSetColumnProperties.Controls.Add(this.tbPreview);
		this.gbSetColumnProperties.Controls.Add(this.tbDecimalPlaces);
		this.gbSetColumnProperties.Controls.Add(this.lbDecimalPlaces);
		this.gbSetColumnProperties.Controls.Add(this.lbPreview);
		this.gbSetColumnProperties.Location = new System.Drawing.Point(3, 263);
		this.gbSetColumnProperties.Name = "gbSetColumnProperties";
		this.gbSetColumnProperties.Size = new System.Drawing.Size(425, 75);
		this.gbSetColumnProperties.TabIndex = 3;
		this.gbSetColumnProperties.TabStop = false;
		this.gbSetColumnProperties.Text = "设置列属性";
		this.tbPreview.Location = new System.Drawing.Point(287, 46);
		this.tbPreview.Name = "tbPreview";
		this.tbPreview.Size = new System.Drawing.Size(132, 21);
		this.tbPreview.TabIndex = 2;
		this.tbPreview.Visible = false;
		this.tbDecimalPlaces.Location = new System.Drawing.Point(71, 46);
		this.tbDecimalPlaces.Name = "tbDecimalPlaces";
		this.tbDecimalPlaces.Size = new System.Drawing.Size(56, 21);
		this.tbDecimalPlaces.TabIndex = 2;
		this.tbDecimalPlaces.Visible = false;
		this.tbDecimalPlaces.KeyUp += new System.Windows.Forms.KeyEventHandler(tbDecimalPlaces_KeyUp);
		this.lbDecimalPlaces.Location = new System.Drawing.Point(6, 49);
		this.lbDecimalPlaces.Name = "lbDecimalPlaces";
		this.lbDecimalPlaces.Size = new System.Drawing.Size(59, 12);
		this.lbDecimalPlaces.TabIndex = 0;
		this.lbDecimalPlaces.Text = "小数位数";
		this.lbDecimalPlaces.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbDecimalPlaces.Visible = false;
		this.lbPreview.Location = new System.Drawing.Point(222, 49);
		this.lbPreview.Name = "lbPreview";
		this.lbPreview.Size = new System.Drawing.Size(59, 12);
		this.lbPreview.TabIndex = 0;
		this.lbPreview.Text = "预览";
		this.lbPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbPreview.Visible = false;
		this.btnHideAll.Location = new System.Drawing.Point(179, 146);
		this.btnHideAll.Name = "btnHideAll";
		this.btnHideAll.Size = new System.Drawing.Size(42, 23);
		this.btnHideAll.TabIndex = 2;
		this.btnHideAll.Text = "<<<";
		this.btnHideAll.UseVisualStyleBackColor = true;
		this.btnHideAll.Click += new System.EventHandler(btnShowAll_Click);
		this.btnHide.Location = new System.Drawing.Point(179, 117);
		this.btnHide.Name = "btnHide";
		this.btnHide.Size = new System.Drawing.Size(42, 23);
		this.btnHide.TabIndex = 2;
		this.btnHide.Text = "<";
		this.btnHide.UseVisualStyleBackColor = true;
		this.btnHide.Click += new System.EventHandler(btnShowAll_Click);
		this.btnShow.Location = new System.Drawing.Point(179, 88);
		this.btnShow.Name = "btnShow";
		this.btnShow.Size = new System.Drawing.Size(42, 23);
		this.btnShow.TabIndex = 2;
		this.btnShow.Text = ">";
		this.btnShow.UseVisualStyleBackColor = true;
		this.btnShow.Click += new System.EventHandler(btnShowAll_Click);
		this.btnBottom.Image = (System.Drawing.Image)resources.GetObject("btnBottom.Image");
		this.btnBottom.Location = new System.Drawing.Point(401, 234);
		this.btnBottom.Name = "btnBottom";
		this.btnBottom.Size = new System.Drawing.Size(26, 23);
		this.btnBottom.TabIndex = 2;
		this.btnBottom.UseVisualStyleBackColor = true;
		this.btnBottom.Click += new System.EventHandler(btnTop_Click);
		this.btnDown.Image = (System.Drawing.Image)resources.GetObject("btnDown.Image");
		this.btnDown.Location = new System.Drawing.Point(401, 156);
		this.btnDown.Name = "btnDown";
		this.btnDown.Size = new System.Drawing.Size(26, 23);
		this.btnDown.TabIndex = 2;
		this.btnDown.UseVisualStyleBackColor = true;
		this.btnDown.Click += new System.EventHandler(btnTop_Click);
		this.btnUp.Image = (System.Drawing.Image)resources.GetObject("btnUp.Image");
		this.btnUp.Location = new System.Drawing.Point(401, 88);
		this.btnUp.Name = "btnUp";
		this.btnUp.Size = new System.Drawing.Size(26, 23);
		this.btnUp.TabIndex = 2;
		this.btnUp.UseVisualStyleBackColor = true;
		this.btnUp.Click += new System.EventHandler(btnTop_Click);
		this.btnTop.Image = (System.Drawing.Image)resources.GetObject("btnTop.Image");
		this.btnTop.Location = new System.Drawing.Point(401, 25);
		this.btnTop.Name = "btnTop";
		this.btnTop.Size = new System.Drawing.Size(26, 23);
		this.btnTop.TabIndex = 2;
		this.btnTop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnTop.UseVisualStyleBackColor = true;
		this.btnTop.Click += new System.EventHandler(btnTop_Click);
		this.btnShowAll.Location = new System.Drawing.Point(179, 59);
		this.btnShowAll.Name = "btnShowAll";
		this.btnShowAll.Size = new System.Drawing.Size(42, 23);
		this.btnShowAll.TabIndex = 2;
		this.btnShowAll.Text = ">>>";
		this.btnShowAll.UseVisualStyleBackColor = true;
		this.btnShowAll.Click += new System.EventHandler(btnShowAll_Click);
		this.lbShowColumnsV.FormattingEnabled = true;
		this.lbShowColumnsV.ItemHeight = 12;
		this.lbShowColumnsV.Location = new System.Drawing.Point(226, 25);
		this.lbShowColumnsV.Name = "lbShowColumnsV";
		this.lbShowColumnsV.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.lbShowColumnsV.Size = new System.Drawing.Size(170, 232);
		this.lbShowColumnsV.TabIndex = 1;
		this.lbShowColumnsV.MouseDown += new System.Windows.Forms.MouseEventHandler(lbShowColumnsV_MouseDown);
		this.lbShowColumnsV.SelectedValueChanged += new System.EventHandler(lbHideColumnsV_SelectedValueChanged);
		this.lbHideColumnsV.FormattingEnabled = true;
		this.lbHideColumnsV.ItemHeight = 12;
		this.lbHideColumnsV.Location = new System.Drawing.Point(4, 25);
		this.lbHideColumnsV.Name = "lbHideColumnsV";
		this.lbHideColumnsV.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		this.lbHideColumnsV.Size = new System.Drawing.Size(170, 232);
		this.lbHideColumnsV.TabIndex = 1;
		this.lbHideColumnsV.MouseDown += new System.Windows.Forms.MouseEventHandler(lbHideColumnsV_MouseDown);
		this.lbHideColumnsV.SelectedValueChanged += new System.EventHandler(lbHideColumnsV_SelectedValueChanged);
		this.lbShowColumns.AutoSize = true;
		this.lbShowColumns.Location = new System.Drawing.Point(267, 10);
		this.lbShowColumns.Name = "lbShowColumns";
		this.lbShowColumns.Size = new System.Drawing.Size(59, 12);
		this.lbShowColumns.TabIndex = 0;
		this.lbShowColumns.Text = "显示列";
		this.lbHideColumns.AutoSize = true;
		this.lbHideColumns.Location = new System.Drawing.Point(58, 10);
		this.lbHideColumns.Name = "lbHideColumns";
		this.lbHideColumns.Size = new System.Drawing.Size(59, 12);
		this.lbHideColumns.TabIndex = 0;
		this.lbHideColumns.Text = "隐藏列";
		this.tcColumnsSetup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tcColumnsSetup.Controls.Add(this.tpColumns1);
		this.tcColumnsSetup.Controls.Add(this.tpColumns2);
		this.tcColumnsSetup.ItemSize = new System.Drawing.Size(90, 19);
		this.tcColumnsSetup.Location = new System.Drawing.Point(5, 8);
		this.tcColumnsSetup.Name = "tcColumnsSetup";
		this.tcColumnsSetup.SelectedIndex = 0;
		this.tcColumnsSetup.Size = new System.Drawing.Size(438, 368);
		this.tcColumnsSetup.TabIndex = 2;
		this.tcColumnsSetup.SelectedIndexChanged += new System.EventHandler(tcColumnsSetup_SelectedIndexChanged);
		this.tpColumns1.Location = new System.Drawing.Point(4, 23);
		this.tpColumns1.Name = "tpColumns1";
		this.tpColumns1.Size = new System.Drawing.Size(430, 341);
		this.tpColumns1.TabIndex = 0;
		this.tpColumns1.Text = "tabPage1";
		this.tpColumns1.UseVisualStyleBackColor = true;
		this.tpColumns2.Location = new System.Drawing.Point(4, 23);
		this.tpColumns2.Name = "tpColumns2";
		this.tpColumns2.Size = new System.Drawing.Size(430, 341);
		this.tpColumns2.TabIndex = 1;
		this.tpColumns2.Text = "tabPage2";
		this.tpColumns2.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(448, 418);
		base.Controls.Add(this.lclPanel_0);
		base.Controls.Add(this.tcColumnsSetup);
		base.Name = "ColumnsSetupDlg";
		base.Load += new System.EventHandler(ColumnsSetupDlg_Load);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.tcColumnsSetup, 0);
		base.Controls.SetChildIndex(this.lclPanel_0, 0);
		this.lclPanel_0.ResumeLayout(false);
		this.lclPanel_0.PerformLayout();
		this.gbSetColumnProperties.ResumeLayout(false);
		this.gbSetColumnProperties.PerformLayout();
		this.tcColumnsSetup.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
