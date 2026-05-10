using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CaliGnlIstdDlg : LclDialog
{
	private const string string_0 = "istd";

	private const string string_1 = "<清空>";

	private const string string_2 = "可选择内标";

	private const string string_3 = "选择内标";

	private const string string_4 = "<Clear>";

	private const string string_5 = "Can be Selected";

	private const string string_6 = "Select Istd.";

	private IContainer icontainer_1;

	private LclGridView gvCmpds;

	private string sClear => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "<清空>", 
		SysLanguage.EN => "<Clear>", 
		_ => "", 
	};

	private string sIstd => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "可选择内标", 
		SysLanguage.EN => "Can be Selected", 
		_ => "", 
	};

	public CaliGnlIstdDlg()
	{
		InitializeComponent();
		int num = base.Width - 25 - 30;
		gvCmpds.AddLclTextBoxColumn("istd", num, 0, StringAlignment.Near, readOnly: true).HeaderText = sIstd;
		gvCmpds.RowCount = 1;
		gvCmpds.Rows[0].Cells["istd"].Value = sClear;
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
		this.gvCmpds = new IBrainChrom2018.LclGridView();
		((System.ComponentModel.ISupportInitialize)this.gvCmpds).BeginInit();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(93, 144);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(174, 144);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(12, 144);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_0);
		this.gvCmpds.AllowUserToAddRows = false;
		this.gvCmpds.AllowUserToDeleteRows = false;
		this.gvCmpds.AllowUserToResizeRows = false;
		this.gvCmpds.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvCmpds.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvCmpds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvCmpds.Dock = System.Windows.Forms.DockStyle.Top;
		this.gvCmpds.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvCmpds.Location = new System.Drawing.Point(0, 0);
		this.gvCmpds.MultiSelect = false;
		this.gvCmpds.Name = "gvCmpds";
		this.gvCmpds.RowHeadersWidth = 25;
		this.gvCmpds.RowTemplate.Height = 16;
		this.gvCmpds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvCmpds.ShowCellToolTips = false;
		this.gvCmpds.Size = new System.Drawing.Size(267, 133);
		this.gvCmpds.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(267, 176);
		base.Controls.Add(this.gvCmpds);
		base.Name = "CaliGnlIstdDlg";
		this.Text = "选择内标";
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.gvCmpds, 0);
		((System.ComponentModel.ISupportInitialize)this.gvCmpds).EndInit();
		base.ResumeLayout(false);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		Text = Lang.PS("选择内标", "Select Istd.");
	}

	public DialogResult ShowDialog(LclCombineCGridView lclCombineCGridView_0, int curRowIndex, ref CaliDisMode caliDisMode)
	{
		if (lclCombineCGridView_0.RowCount == 0)
		{
			return DialogResult.Cancel;
		}
		gvCmpds.RowCount = lclCombineCGridView_0.RowCount;
		Compound compound_ = (lclCombineCGridView_0.Rows[curRowIndex].Tag as Class74).compound_0;
		int num = 1;
		int index = 0;
		for (int i = 0; i < lclCombineCGridView_0.RowCount; i++)
		{
			if (i != curRowIndex)
			{
				Compound compound_2 = (lclCombineCGridView_0.Rows[i].Tag as Class74).compound_0;
				if (compound_2 == compound_)
				{
					index = num;
				}
				gvCmpds.Rows[num].Tag = compound_2;
				gvCmpds.Rows[num++].Cells["istd"].Value = compound_2.cmpdInfo.name;
			}
		}
		gvCmpds.CurrentCell = gvCmpds.Rows[index].Cells[0];
		gvCmpds.Rows[index].Selected = true;
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			Compound compound = null;
			index = -1;
			for (int j = 0; j < gvCmpds.RowCount; j++)
			{
				if (gvCmpds.Rows[j].Selected)
				{
					index = j;
				}
			}
			if (index > 0)
			{
				compound = gvCmpds.Rows[index].Tag as Compound;
			}
			if (compound == null)
			{
				(lclCombineCGridView_0.Rows[curRowIndex].Tag as Class74).compound_0.cmpdInfo.isIstd = false;
				(lclCombineCGridView_0.Rows[curRowIndex].Tag as Class74).compound_0.cmpdInfo.istdCmpd = "";
				lclCombineCGridView_0.Rows[curRowIndex].Cells["IstdCmpd"].Value = "";
			}
			else
			{
				(lclCombineCGridView_0.Rows[curRowIndex].Tag as Class74).compound_0.cmpdInfo.istdCmpd = compound.cmpdInfo.name;
				lclCombineCGridView_0.Rows[curRowIndex].Cells["IstdCmpd"].Value = compound.cmpdInfo.name;
			}
			for (int k = curRowIndex; k < lclCombineCGridView_0.RowCount; k++)
			{
				if (compound != null)
				{
					if ((lclCombineCGridView_0.Rows[k].Tag as Class74).compound_0.cmpdInfo.name != compound.cmpdInfo.name)
					{
						(lclCombineCGridView_0.Rows[k].Tag as Class74).compound_0.cmpdInfo.istdCmpd = compound.cmpdInfo.name;
					}
				}
				else
				{
					(lclCombineCGridView_0.Rows[k].Tag as Class74).compound_0.cmpdInfo.istdCmpd = null;
				}
			}
			if (compound != null)
			{
				caliDisMode = CaliDisMode.Istd;
			}
			else
			{
				caliDisMode = CaliDisMode.Estd;
			}
		}
		return dialogResult;
	}

	private void method_0(object sender, EventArgs e)
	{
	}
}
