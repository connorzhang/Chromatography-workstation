using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CaliGnlCurveCtrl : UserControl
{
	public delegate CaliGnl GetCaliGnlHandler();

	public delegate Chromatogram GetChromHandler();

	public delegate Compound GetCompoundHandler();

	public bool bool_0;

	public Instrument instrument;

	private CmpdDisplay cmpdDisplay;

	private Compound compound_0 = null;

	private string string_39;

	private int int_1;

	private int int_2;

	private DisLg disLg_0;

	private IContainer components = null;

	private LclPanel pnlCmpd;

	private LclSplitter lclSplitter1;

	private LclPanel pnlL;

	private LclGridView gvCmpd;

	private LclPanel pnlLD;

	private LclGroupBox gbFunc;

	private LclLabel lbCurveFit;

	private LclCusComboBox cbCurveFit;

	private LclLabel lbOriginal;

	private LclCusComboBox cbOriginal;

	private LclLabel lbEquationV;

	private LclLabel lbResiduum;

	private LclLabel lbCorrFactor;

	private LclLabel lbResiduumV;

	private LclLabel lbCorrFactorV;

	private LclLabel lbIstdCmpd;

	private LclLabel lbIstdCmpdV;

	private LclLabel lbRespStyleV;

	private LclLabel lbRespStyle;

	public LclDisplayPanel dpCmpd;

	private Chromatogram curChromatogram
	{
		get
		{
			if (GetCurrentChrom != null)
			{
				return GetCurrentChrom();
			}
			return null;
		}
	}

	public GetCaliGnlHandler GetCaliGnl { get; set; }

	public GetChromHandler GetCurrentChrom { get; set; }

	public GetCompoundHandler GetCurrentCompound { get; set; }

	public CaliGnl caliGnl_0
	{
		get
		{
			if (GetCaliGnl == null)
			{
				return null;
			}
			return GetCaliGnl();
		}
	}

	public Compound Compound
	{
		get
		{
			return compound_0;
		}
		set
		{
			compound_0 = value;
		}
	}

	public static bool IsDesignMode()
	{
		return false;
	}

	public CaliGnlCurveCtrl()
	{
		InitializeComponent();
		if (!IsDesignMode())
		{
			string_39 = "";
			disLg_0 = default(DisLg);
			cbOriginal.InitItems(new object[3]
			{
				Original.Ignore,
				Original.With,
				Original.Pass
			});
			cbOriginal.InitShowText(new string[3]
			{
				Lang.PS("忽略", "Ignore"),
				Lang.PS("考虑", "Compute With"),
				Lang.PS("经过", "Pass Through")
			});
			cbCurveFit.InitItems(new object[6]
			{
				CurveFit.Free,
				CurveFit.PtToPt,
				CurveFit.Linear,
				CurveFit.Quadratic,
				CurveFit.Cubic,
				CurveFit.Exponent
			});
			cbCurveFit.InitShowText(new string[6]
			{
				Lang.PS("校正归一(基于校正因子)", "Free"),
				Lang.PS("单点校正点到点(基于线性直线)", "Pt to Pt"),
				Lang.PS("单点校正线性(基于线性直线)", "Linear"),
				Lang.PS("多点校正二次(基于工作曲线)", "Quadratic2"),
				Lang.PS("多点校正三次(基于工作曲线)", "Quadratic3"),
				Lang.PS("指数", "Cubic")
			});
			cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, dpCmpd);
			pnlCmpd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gvCmpd.BorderStyle = BorderStyle.None;
			gvCmpd.Dock = DockStyle.Fill;
			dpCmpd.Dock = DockStyle.Fill;
			gvCmpd.CharacterHeaderColor = Color.Red;
			gvCmpd.AddLclTextBoxColumn("Resp", 80, 3, StringAlignment.Far, readOnly: true).DefaultCellStyle.ForeColor = Color.Gray;
			DataGridViewColumn dataGridViewColumn = gvCmpd.AddLclTextBoxColumn("Amount", 80, StringAlignment.Far);
			gvCmpd.AddColorHeader(dataGridViewColumn.Index);
			gvCmpd.AddLclTextBoxColumn("RespFactor", 80, 4, StringAlignment.Far, readOnly: true).DefaultCellStyle.ForeColor = Color.Gray;
			gvCmpd.AddLclTextBoxColumn("RecordNumber", 30, 0, StringAlignment.Center, readOnly: true).DefaultCellStyle.ForeColor = Color.Gray;
			dataGridViewColumn = gvCmpd.AddLclCheckBoxColumn("Used", 30);
			gvCmpd.AddColorHeader(dataGridViewColumn.Index);
			for (int i = 0; i < 20; i++)
			{
				int index = gvCmpd.Rows.Add();
				gvCmpd.Rows[index].Tag = new Level();
			}
			gvCmpd.Columns["Amount"].HeaderText = Lang.PS("浓度", "Amount");
			gvCmpd.Columns["RespFactor"].HeaderText = Lang.PS("因子", "Factor");
			gvCmpd.Columns["RecordNumber"].HeaderText = Lang.PS("记录", "Rec.Num");
			gvCmpd.Columns["Used"].HeaderText = Lang.PS("使用", "Used");
			lbRespStyle.Text = Lang.PS("响应类型", "Response type");
			lbCorrFactor.Text = Lang.PS("相关系数", "Correlation coefficient");
			lbResiduum.Text = Lang.PS("残余", "Remnant");
			lbOriginal.Text = Lang.PS("原点方案", "The origin scheme");
			lbCurveFit.Text = Lang.PS("曲线类型", "Curve types");
		}
	}

	private void cbCurveFit_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CurveFit curveFit = compound_0.eFunc.curveFit;
		CurveFit curveFit2 = compound_0.iFunc.curveFit;
		if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Estd)
		{
			compound_0.eFunc.curveFit = (CurveFit)cbCurveFit.SelectedItem;
		}
		else if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Istd)
		{
			compound_0.iFunc.curveFit = (CurveFit)cbCurveFit.SelectedItem;
		}
		try
		{
			caliGnl_0.CalculateFunc(appendLink: true);
			LoadCompound();
		}
		catch
		{
			if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Estd)
			{
				cbCurveFit.SelectedItem = curveFit;
				compound_0.eFunc.curveFit = curveFit;
			}
			else if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Istd)
			{
				cbCurveFit.SelectedItem = curveFit2;
				compound_0.iFunc.curveFit = curveFit2;
			}
			MessageBox.Show(Lang.PS("标样数不能满足当前阶数，请重新选择条件！"));
		}
	}

	private void cbOriginal_SelectionChangeCommitted(object sender, EventArgs e)
	{
		Original original = compound_0.eFunc.original;
		Original original2 = compound_0.iFunc.original;
		if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Estd)
		{
			compound_0.eFunc.original = (Original)cbOriginal.SelectedItem;
		}
		else if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Istd)
		{
			compound_0.iFunc.original = (Original)cbOriginal.SelectedItem;
		}
		compound_0.eFunc.original = (Original)cbOriginal.SelectedItem;
		compound_0.iFunc.original = (Original)cbOriginal.SelectedItem;
		try
		{
			caliGnl_0.CalculateFunc(appendLink: true);
			LoadCompound();
		}
		catch (Exception)
		{
			if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Estd)
			{
				cbOriginal.SelectedItem = original;
				compound_0.eFunc.original = original;
			}
			else if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Istd)
			{
				cbOriginal.SelectedItem = original2;
				compound_0.iFunc.original = original2;
			}
			MessageBox.Show("标样数不能满足当前阶数，请重新选择条件！");
		}
	}

	public object gvCmpdValue(bool gvUse, Level level, string columnName)
	{
		object obj = null;
		string text = gvCmpd.ConvertValFmt(columnName);
		if (columnName != null)
		{
			switch (columnName)
			{
			case "Amount":
				obj = ((!gvUse) ? level.amount.ToString(text) : ((object)level.amount));
				break;
			case "RespFactor":
				obj = ((!gvUse) ? level.respFactor.ToString(text) : ((object)level.respFactor));
				break;
			case "RecordNumber":
				obj = ((!gvUse) ? level.SecsNum.ToString(text) : ((object)level.SecsNum));
				break;
			case "Used":
				obj = ((!gvUse) ? (level.used ? "√" : "") : ((object)level.used));
				break;
			case "Resp":
				obj = ((!gvUse) ? level.response.ToString(text) : ((object)level.response));
				break;
			}
		}
		if (!gvUse && obj == null)
		{
			obj = "";
		}
		return obj;
	}

	private void gvCmpd_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		e.Cancel = method_11(bool_2: true);
		bool_0 = false;
	}

	private void gvCmpd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (bool_0)
		{
			compound_0.levels[e.RowIndex].amount = Class49.String2Float(gvCmpd.Rows[e.RowIndex].Cells["Amount"].Value, compound_0.levels[e.RowIndex].amount);
			compound_0.levels[e.RowIndex].used = (bool)gvCmpd.Rows[e.RowIndex].Cells["Used"].Value;
			if (int_1 == e.RowIndex)
			{
			}
			LoadCompound();
		}
	}

	private void gvCmpd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
		{
			bool_0 = true;
		}
	}

	public void GetCmpdDisColumns(ref GvInfos gvInfos)
	{
		Class49.SetGridViewInfo(gvCmpd, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && text != "Resp" && text != "Amount" && text != "RespFactor" && (text == "RecordNumber" || text == "Used"))
			{
				num = 45;
			}
			gvInfos.colWidths[i] = num;
		}
	}

	private void dpCmpd_MouseMove(object sender, MouseEventArgs e)
	{
		if (!IsDesignMode())
		{
			cmpdDisplay.mouseLocation = e.Location;
			cmpdDisplay.DrawMouseLgValue();
		}
	}

	private void dpCmpd_Paint(object sender, PaintEventArgs e)
	{
		if (!IsDesignMode())
		{
			cmpdDisplay.Draw(e.Graphics, erase: true);
		}
	}

	private void pnlLD_Paint(object sender, PaintEventArgs e)
	{
		if (!IsDesignMode())
		{
			e.Graphics.DrawLine(Pens.Gray, new Point(0, 2), new Point(pnlL.Width, 2));
		}
	}

	private bool method_11(bool bool_2)
	{
		return false;
	}

	public void ShowCompound()
	{
		if (caliGnl_0.caliOption.cmpdUnit == "")
		{
			gvCmpd.Columns["Amount"].HeaderText = Lang.PS("浓度", "Amount") + "\n[]";
		}
		else
		{
			gvCmpd.Columns["Amount"].HeaderText = Lang.PS("浓度", "Amount") + "\n[" + caliGnl_0.caliOption.cmpdUnit + "]";
		}
		if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Estd)
		{
			gbFunc.Text = Lang.PS("显示模式", "Display Mode") + ":[" + Lang.PS("外标模式", "ESTD Style") + "]";
		}
		else
		{
			gbFunc.Text = Lang.PS("显示模式", "Display Mode") + ":[" + Lang.PS("内标模式", "ISTD Style") + "]";
		}
	}

	public void LoadCompound()
	{
		if (compound_0 != null)
		{
			LoadCompound(curChromatogram, compound_0);
		}
	}

	public void LoadCompound(Chromatogram chromatogram, Compound compound)
	{
		compound_0 = compound;
		caliGnl_0.CalculateFunc(appendLink: true);
		string string_ = Class49.MesureUnit() + ".s";
		if (compound_0.cmpdInfo.respStyle == RespStyle.Height)
		{
			string_ = Class49.MesureUnit();
		}
		gvCmpd.Columns["Resp"].HeaderText = Lang.PS("响应", "Response") + "\n[" + string_ + "]";
		for (int i = 0; i < gvCmpd.RowCount; i++)
		{
			if (i < compound_0.levels.Length)
			{
				gvCmpd.Rows[i].Cells["Resp"].Value = gvCmpdValue(gvUse: true, compound_0.levels[i], "Resp");
				gvCmpd.Rows[i].Cells["Amount"].Value = gvCmpdValue(gvUse: true, compound_0.levels[i], "Amount");
				gvCmpd.Rows[i].Cells["RespFactor"].Value = gvCmpdValue(gvUse: true, compound_0.levels[i], "RespFactor");
				gvCmpd.Rows[i].Cells["RecordNumber"].Value = gvCmpdValue(gvUse: true, compound_0.levels[i], "RecordNumber");
				gvCmpd.Rows[i].Cells["Used"].Value = gvCmpdValue(gvUse: true, compound_0.levels[i], "Used");
			}
		}
		if (compound_0.cmpdInfo.respStyle == RespStyle.Area)
		{
			lbRespStyleV.Text = Lang.PS("面积", "Area");
		}
		else if (compound_0.cmpdInfo.respStyle == RespStyle.Height)
		{
			lbRespStyleV.Text = Lang.PS("高度", "Height");
		}
		else if (compound_0.cmpdInfo.respStyle == RespStyle.AreaSquare)
		{
			lbRespStyleV.Text = Lang.PS("面积平方根", "AreaSquare");
		}
		else if (compound_0.cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
		{
			lbRespStyleV.Text = Lang.PS("高度平方根", "PeakHeightSquare");
		}
		if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Estd)
		{
			lbIstdCmpdV.Visible = false;
			lbIstdCmpd.Visible = false;
			cbCurveFit.SelectedItem = compound_0.eFunc.curveFit;
			cbOriginal.SelectedItem = compound_0.eFunc.original;
			lbEquationV.Text = compound_0.eFunc.GetEquationStr();
			cmpdDisplay.SetCompound(compound_0, bool_0: false, caliGnl_0.caliOption.cmpdUnit, ref string_);
			lbCorrFactorV.Text = compound_0.eFunc.GetCorrFactorTxt();
			lbResiduumV.Text = compound_0.eFunc.GetResiduumTxt(string_);
		}
		else if (caliGnl_0.caliOption.caliDisMode == CaliDisMode.Istd)
		{
			lbIstdCmpdV.Visible = true;
			lbIstdCmpd.Visible = true;
			if (compound_0 == null)
			{
				lbIstdCmpdV.Text = "-";
			}
			else if (compound_0.cmpdInfo.istdCmpd != "")
			{
				lbIstdCmpdV.Text = compound_0.cmpdInfo.istdCmpd;
			}
			else
			{
				lbIstdCmpdV.Text = compound_0.cmpdInfo.name;
			}
			cbCurveFit.SelectedItem = compound_0.iFunc.curveFit;
			cbOriginal.SelectedItem = compound_0.iFunc.original;
			lbEquationV.Text = compound_0.iFunc.GetEquationStr();
			cmpdDisplay.SetCompound(compound_0, bool_0: true, caliGnl_0.caliOption.cmpdUnit, ref string_);
			lbCorrFactorV.Text = compound_0.iFunc.GetCorrFactorTxt();
			lbResiduumV.Text = compound_0.iFunc.GetResiduumTxt(string_);
		}
		dpCmpd.Refresh();
	}

	private void gvCmpd_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.pnlCmpd = new IBrainChrom2018.LclPanel();
		this.dpCmpd = new IBrainChrom2018.LclDisplayPanel();
		this.lclSplitter1 = new IBrainChrom2018.LclSplitter();
		this.pnlL = new IBrainChrom2018.LclPanel();
		this.gvCmpd = new IBrainChrom2018.LclGridView();
		this.pnlLD = new IBrainChrom2018.LclPanel();
		this.gbFunc = new IBrainChrom2018.LclGroupBox();
		this.lbCurveFit = new IBrainChrom2018.LclLabel();
		this.cbCurveFit = new IBrainChrom2018.LclCusComboBox();
		this.lbOriginal = new IBrainChrom2018.LclLabel();
		this.cbOriginal = new IBrainChrom2018.LclCusComboBox();
		this.lbEquationV = new IBrainChrom2018.LclLabel();
		this.lbResiduum = new IBrainChrom2018.LclLabel();
		this.lbCorrFactor = new IBrainChrom2018.LclLabel();
		this.lbResiduumV = new IBrainChrom2018.LclLabel();
		this.lbCorrFactorV = new IBrainChrom2018.LclLabel();
		this.lbIstdCmpd = new IBrainChrom2018.LclLabel();
		this.lbIstdCmpdV = new IBrainChrom2018.LclLabel();
		this.lbRespStyleV = new IBrainChrom2018.LclLabel();
		this.lbRespStyle = new IBrainChrom2018.LclLabel();
		this.pnlCmpd.SuspendLayout();
		this.pnlL.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvCmpd).BeginInit();
		this.pnlLD.SuspendLayout();
		this.gbFunc.SuspendLayout();
		base.SuspendLayout();
		this.pnlCmpd.Controls.Add(this.dpCmpd);
		this.pnlCmpd.Controls.Add(this.lclSplitter1);
		this.pnlCmpd.Controls.Add(this.pnlL);
		this.pnlCmpd.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlCmpd.Location = new System.Drawing.Point(0, 0);
		this.pnlCmpd.Name = "pnlCmpd";
		this.pnlCmpd.Size = new System.Drawing.Size(874, 397);
		this.pnlCmpd.TabIndex = 9;
		this.dpCmpd.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpCmpd.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dpCmpd.Location = new System.Drawing.Point(352, 0);
		this.dpCmpd.Name = "dpCmpd";
		this.dpCmpd.Size = new System.Drawing.Size(522, 397);
		this.dpCmpd.TabIndex = 3;
		this.dpCmpd.Paint += new System.Windows.Forms.PaintEventHandler(dpCmpd_Paint);
		this.dpCmpd.MouseMove += new System.Windows.Forms.MouseEventHandler(dpCmpd_MouseMove);
		this.lclSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lclSplitter1.Location = new System.Drawing.Point(347, 0);
		this.lclSplitter1.Name = "lclSplitter1";
		this.lclSplitter1.Size = new System.Drawing.Size(5, 397);
		this.lclSplitter1.TabIndex = 1;
		this.lclSplitter1.TabStop = false;
		this.pnlL.Controls.Add(this.gvCmpd);
		this.pnlL.Controls.Add(this.pnlLD);
		this.pnlL.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnlL.Location = new System.Drawing.Point(0, 0);
		this.pnlL.Name = "pnlL";
		this.pnlL.Size = new System.Drawing.Size(347, 397);
		this.pnlL.TabIndex = 0;
		this.gvCmpd.AllowUserToAddRows = false;
		this.gvCmpd.AllowUserToDeleteRows = false;
		this.gvCmpd.AllowUserToResizeRows = false;
		this.gvCmpd.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvCmpd.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvCmpd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.gvCmpd.ColumnHeadersHeight = 32;
		this.gvCmpd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvCmpd.DefaultCellStyle = dataGridViewCellStyle2;
		this.gvCmpd.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gvCmpd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvCmpd.Location = new System.Drawing.Point(0, 0);
		this.gvCmpd.Name = "gvCmpd";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvCmpd.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.gvCmpd.RowHeadersWidth = 25;
		this.gvCmpd.RowTemplate.Height = 16;
		this.gvCmpd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvCmpd.ShowCellToolTips = false;
		this.gvCmpd.Size = new System.Drawing.Size(347, 173);
		this.gvCmpd.TabIndex = 4;
		this.gvCmpd.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvCmpd_CellBeginEdit);
		this.gvCmpd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpd_CellEndEdit);
		this.gvCmpd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(gvCmpd_CellValueChanged);
		this.gvCmpd.Enter += new System.EventHandler(gvCmpd_Enter);
		this.pnlLD.Controls.Add(this.gbFunc);
		this.pnlLD.Controls.Add(this.lbIstdCmpd);
		this.pnlLD.Controls.Add(this.lbIstdCmpdV);
		this.pnlLD.Controls.Add(this.lbRespStyleV);
		this.pnlLD.Controls.Add(this.lbRespStyle);
		this.pnlLD.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pnlLD.Location = new System.Drawing.Point(0, 173);
		this.pnlLD.Name = "pnlLD";
		this.pnlLD.Size = new System.Drawing.Size(347, 224);
		this.pnlLD.TabIndex = 3;
		this.pnlLD.Paint += new System.Windows.Forms.PaintEventHandler(pnlLD_Paint);
		this.gbFunc.Controls.Add(this.lbCurveFit);
		this.gbFunc.Controls.Add(this.cbCurveFit);
		this.gbFunc.Controls.Add(this.lbOriginal);
		this.gbFunc.Controls.Add(this.cbOriginal);
		this.gbFunc.Controls.Add(this.lbEquationV);
		this.gbFunc.Controls.Add(this.lbResiduum);
		this.gbFunc.Controls.Add(this.lbCorrFactor);
		this.gbFunc.Controls.Add(this.lbResiduumV);
		this.gbFunc.Controls.Add(this.lbCorrFactorV);
		this.gbFunc.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.gbFunc.Location = new System.Drawing.Point(0, 59);
		this.gbFunc.Name = "gbFunc";
		this.gbFunc.Size = new System.Drawing.Size(347, 165);
		this.gbFunc.TabIndex = 4;
		this.gbFunc.TabStop = false;
		this.gbFunc.Text = "显示模式";
		this.lbCurveFit.AutoSize = true;
		this.lbCurveFit.Location = new System.Drawing.Point(8, 25);
		this.lbCurveFit.Name = "lbCurveFit";
		this.lbCurveFit.Size = new System.Drawing.Size(53, 12);
		this.lbCurveFit.TabIndex = 1;
		this.lbCurveFit.Text = "曲线类型";
		this.cbCurveFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbCurveFit.FormattingEnabled = true;
		this.cbCurveFit.ItemExtString = "";
		this.cbCurveFit.Location = new System.Drawing.Point(85, 22);
		this.cbCurveFit.Name = "cbCurveFit";
		this.cbCurveFit.Size = new System.Drawing.Size(209, 20);
		this.cbCurveFit.TabIndex = 3;
		this.cbCurveFit.SelectionChangeCommitted += new System.EventHandler(cbCurveFit_SelectionChangeCommitted);
		this.lbOriginal.AutoSize = true;
		this.lbOriginal.Location = new System.Drawing.Point(8, 50);
		this.lbOriginal.Name = "lbOriginal";
		this.lbOriginal.Size = new System.Drawing.Size(53, 12);
		this.lbOriginal.TabIndex = 1;
		this.lbOriginal.Text = "原点方案";
		this.cbOriginal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbOriginal.FormattingEnabled = true;
		this.cbOriginal.ItemExtString = "";
		this.cbOriginal.Location = new System.Drawing.Point(85, 47);
		this.cbOriginal.Name = "cbOriginal";
		this.cbOriginal.Size = new System.Drawing.Size(209, 20);
		this.cbOriginal.TabIndex = 3;
		this.cbOriginal.SelectionChangeCommitted += new System.EventHandler(cbOriginal_SelectionChangeCommitted);
		this.lbEquationV.AutoSize = true;
		this.lbEquationV.Location = new System.Drawing.Point(9, 74);
		this.lbEquationV.Name = "lbEquationV";
		this.lbEquationV.Size = new System.Drawing.Size(41, 12);
		this.lbEquationV.TabIndex = 1;
		this.lbEquationV.Text = "y=F(x)";
		this.lbResiduum.AutoSize = true;
		this.lbResiduum.Location = new System.Drawing.Point(9, 142);
		this.lbResiduum.Name = "lbResiduum";
		this.lbResiduum.Size = new System.Drawing.Size(29, 12);
		this.lbResiduum.TabIndex = 1;
		this.lbResiduum.Text = "残余";
		this.lbCorrFactor.AutoSize = true;
		this.lbCorrFactor.Location = new System.Drawing.Point(9, 118);
		this.lbCorrFactor.Name = "lbCorrFactor";
		this.lbCorrFactor.Size = new System.Drawing.Size(59, 12);
		this.lbCorrFactor.TabIndex = 1;
		this.lbCorrFactor.Text = "相关系数:";
		this.lbResiduumV.AutoSize = true;
		this.lbResiduumV.Location = new System.Drawing.Point(83, 142);
		this.lbResiduumV.Name = "lbResiduumV";
		this.lbResiduumV.Size = new System.Drawing.Size(71, 12);
		this.lbResiduumV.TabIndex = 1;
		this.lbResiduumV.Text = "lbResiduumV";
		this.lbCorrFactorV.AutoSize = true;
		this.lbCorrFactorV.Location = new System.Drawing.Point(83, 118);
		this.lbCorrFactorV.Name = "lbCorrFactorV";
		this.lbCorrFactorV.Size = new System.Drawing.Size(83, 12);
		this.lbCorrFactorV.TabIndex = 1;
		this.lbCorrFactorV.Text = "lbCorrFactorV";
		this.lbIstdCmpd.AutoSize = true;
		this.lbIstdCmpd.Location = new System.Drawing.Point(8, 10);
		this.lbIstdCmpd.Name = "lbIstdCmpd";
		this.lbIstdCmpd.Size = new System.Drawing.Size(29, 12);
		this.lbIstdCmpd.TabIndex = 1;
		this.lbIstdCmpd.Text = "内标";
		this.lbIstdCmpdV.AutoSize = true;
		this.lbIstdCmpdV.Location = new System.Drawing.Point(83, 10);
		this.lbIstdCmpdV.Name = "lbIstdCmpdV";
		this.lbIstdCmpdV.Size = new System.Drawing.Size(71, 12);
		this.lbIstdCmpdV.TabIndex = 1;
		this.lbIstdCmpdV.Text = "lbIstdCmpdV";
		this.lbRespStyleV.AutoSize = true;
		this.lbRespStyleV.Location = new System.Drawing.Point(83, 35);
		this.lbRespStyleV.Name = "lbRespStyleV";
		this.lbRespStyleV.Size = new System.Drawing.Size(29, 12);
		this.lbRespStyleV.TabIndex = 1;
		this.lbRespStyleV.Text = "面积";
		this.lbRespStyle.AutoSize = true;
		this.lbRespStyle.Location = new System.Drawing.Point(8, 35);
		this.lbRespStyle.Name = "lbRespStyle";
		this.lbRespStyle.Size = new System.Drawing.Size(53, 12);
		this.lbRespStyle.TabIndex = 1;
		this.lbRespStyle.Text = "响应类型";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.pnlCmpd);
		base.Name = "CaliGnlCurveCtrl";
		base.Size = new System.Drawing.Size(874, 397);
		this.pnlCmpd.ResumeLayout(false);
		this.pnlL.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvCmpd).EndInit();
		this.pnlLD.ResumeLayout(false);
		this.pnlLD.PerformLayout();
		this.gbFunc.ResumeLayout(false);
		this.gbFunc.PerformLayout();
		base.ResumeLayout(false);
	}
}
