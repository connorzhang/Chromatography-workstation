using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HZH_Controls.Controls;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormOnline : Form
{
	public static FormOnline selfCtrl;

	public float fSignal;

	public float[] fComponet;

	public float[] fComponet2;

	public float[] fComponet3;

	public float[] fComponet4;

	public string[] strCompName;

	public string[] strCompName2;

	public string[] strCompName3;

	public string[] strCompName4;

	public float fTotal = 0f;

	public float fTotal2 = 0f;

	public float fTotal3 = 0f;

	public float fTotal4 = 0f;

	public List<PortableGridModel> lstSource1 = new List<PortableGridModel>();

	public List<PortableGridModel> lstSource2 = new List<PortableGridModel>();

	public List<PortableGridModel> lstSource3 = new List<PortableGridModel>();

	public List<PortableGridModel> lstSource4 = new List<PortableGridModel>();

	public List<PortableGridModel> lstBSource1 = new List<PortableGridModel>();

	public List<PortableGridModel> lstBSource2 = new List<PortableGridModel>();

	public List<PortableGridModel> lstBSource3 = new List<PortableGridModel>();

	public List<PortableGridModel> lstBSource4 = new List<PortableGridModel>();

	public bool bLoading = true;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	private Button btnSetPara;

	private Timer timer1;

	public PictureBox picBoxFire;

	public Label labSignal;

	private TabControlExt tabControlExt1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private UCDataGridView uDgChannel1;

	private UCDataGridView uDgChannel2;

	private UCDataGridView uDgChannel3;

	private SplitContainer splitContainer1;

	private SplitContainer splitContainer2;

	private UCDataGridView uDgChannel4;

	private Label labUser;

	private UCBtnExt ucBtnHis;

	public ImageList imageList1;

	private UCBtnExt ucBtnSet;

	private UCBtnExt ucBtnReLon;

	private TabPage tabPage4;

	private TabPage tabPage3;

	private SplitContainer splitContainer3;

	private UCDataGridView uDgBChannel1;

	private SplitContainer splitContainer4;

	private UCDataGridView uDgBChannel2;

	private UCDataGridView uDgBChannel3;

	private UCDataGridView uDgBChannel4;

	public FormOnline()
	{
		selfCtrl = this;
		InitializeComponent();
		strCompName = new string[15];
		strCompName2 = new string[15];
		strCompName3 = new string[15];
		strCompName4 = new string[15];
		fComponet = new float[15];
		fComponet2 = new float[15];
		fComponet3 = new float[15];
		fComponet4 = new float[15];
		for (int i = 0; i < 15; i++)
		{
			strCompName[i] = i.ToString();
			strCompName2[i] = i.ToString();
			strCompName3[i] = i.ToString();
			strCompName4[i] = i.ToString();
		}
		loading();
		base.TopMost = true;
		bLoading = false;
		timer1.Enabled = false;
	}

	private void btnSetPara_Click(object sender, EventArgs e)
	{
		FormMain.fromMain.Show();
		FormMain.fromMain.Activate();
		FormMain.fromMain.WindowState = FormWindowState.Maximized;
		Hide();
	}

	private void labComp1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(1);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[0];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(2);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[1];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(3);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[2];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(4);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[3];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(5);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[4];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labCompSum_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(13);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = "THC";
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		for (int i = 0; i < 15; i++)
		{
			lstSource1[i].name = strCompName[i];
			lstSource1[i].setV = fComponet[i].ToString("F" + Class49.int_8);
			lstSource2[i].name = strCompName2[i];
			lstSource2[i].setV = fComponet2[i].ToString("F" + Class49.int_8);
			lstSource3[i].name = strCompName3[i];
			lstSource3[i].setV = fComponet3[i].ToString("F" + Class49.int_8);
			lstSource4[i].name = strCompName4[i];
			lstSource4[i].setV = fComponet4[i].ToString("F" + Class49.int_8);
		}
		uDgChannel1.ReloadSource();
		uDgChannel2.ReloadSource();
		uDgChannel3.ReloadSource();
		uDgChannel4.ReloadSource();
	}

	private void picBoxFire_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(20);
	}

	private void labComp6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(6);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[5];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(7);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[6];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(8);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[7];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(9);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[8];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp10_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(10);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[9];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp11_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(11);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[10];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labComp12_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(12);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[11];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(51);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[0];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(52);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[1];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(53);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[2];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(54);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[3];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(55);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[4];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(56);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[5];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(57);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[6];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(58);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[7];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(59);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[8];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp10_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(60);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[9];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp11_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(61);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[10];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab2Comp12_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(62);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[11];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(102);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[0];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(103);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[1];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(104);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[2];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(105);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[3];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(106);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[4];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(107);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[5];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(108);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[6];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(109);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[7];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(110);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[8];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp10_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(111);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[9];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp11_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(112);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[10];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab3Comp12_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(113);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[11];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(151);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[0];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(152);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[1];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(153);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[2];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(154);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[3];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(155);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[4];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(156);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[5];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(157);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[6];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(158);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[7];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(159);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[8];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp10_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(160);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[9];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp11_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(161);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[10];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lab4Comp12_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(162);
		formAreaPlot.TopMost = true;
		formAreaPlot.strPeakName = strCompName[11];
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	public void reloadData()
	{
		uDgChannel1.ReloadSource();
		uDgChannel2.ReloadSource();
		uDgChannel3.ReloadSource();
		uDgChannel4.ReloadSource();
		uDgBChannel1.ReloadSource();
		uDgBChannel2.ReloadSource();
		uDgBChannel3.ReloadSource();
		uDgBChannel4.ReloadSource();
	}

	public void loading()
	{
		List<DataGridViewColumnEntity> list = new List<DataGridViewColumnEntity>();
		list.Add(new DataGridViewColumnEntity
		{
			DataField = "name",
			HeadText = "",
			Width = 100,
			WidthType = SizeType.Absolute
		});
		list.Add(new DataGridViewColumnEntity
		{
			DataField = "setV",
			HeadText = "A:流路1",
			Width = 200,
			WidthType = SizeType.Absolute
		});
		uDgChannel1.Columns = list;
		uDgChannel1.IsShowCheckBox = false;
		list[1].HeadText = "A:流路2";
		uDgChannel2.Columns = list;
		uDgChannel2.IsShowCheckBox = false;
		list[1].HeadText = "A:流路3";
		uDgChannel3.Columns = list;
		uDgChannel3.IsShowCheckBox = false;
		list[1].HeadText = "A:流路4";
		uDgChannel4.Columns = list;
		uDgChannel4.IsShowCheckBox = false;
		for (int i = 0; i < 15; i++)
		{
			PortableGridModel item = new PortableGridModel
			{
				name = i.ToString(),
				setV = "0000.0000"
			};
			PortableGridModel item2 = new PortableGridModel
			{
				name = i.ToString(),
				setV = "0000.0000"
			};
			PortableGridModel item3 = new PortableGridModel
			{
				name = i.ToString(),
				setV = "0000.0000"
			};
			PortableGridModel item4 = new PortableGridModel
			{
				name = i.ToString(),
				setV = "0000.0000"
			};
			lstSource1.Add(item);
			lstSource2.Add(item2);
			lstSource3.Add(item3);
			lstSource4.Add(item4);
		}
		uDgChannel1.DataSource = lstSource1;
		uDgChannel2.DataSource = lstSource2;
		uDgChannel3.DataSource = lstSource3;
		uDgChannel4.DataSource = lstSource4;
		List<DataGridViewColumnEntity> list2 = new List<DataGridViewColumnEntity>();
		list2.Add(new DataGridViewColumnEntity
		{
			DataField = "name",
			HeadText = "",
			Width = 50,
			WidthType = SizeType.Absolute
		});
		list2.Add(new DataGridViewColumnEntity
		{
			DataField = "setV",
			HeadText = "B:流路1",
			Width = 200,
			WidthType = SizeType.Absolute
		});
		uDgBChannel1.Columns = list2;
		uDgBChannel1.IsShowCheckBox = false;
		list2[1].HeadText = "B:流路2";
		uDgBChannel2.Columns = list2;
		uDgBChannel2.IsShowCheckBox = false;
		list2[1].HeadText = "B:流路3";
		uDgBChannel3.Columns = list2;
		uDgBChannel3.IsShowCheckBox = false;
		list2[1].HeadText = "B:流路4";
		uDgBChannel4.Columns = list2;
		uDgBChannel4.IsShowCheckBox = false;
		for (int j = 0; j < 15; j++)
		{
			PortableGridModel item5 = new PortableGridModel
			{
				name = j.ToString(),
				setV = "0000.0000"
			};
			PortableGridModel item6 = new PortableGridModel
			{
				name = j.ToString(),
				setV = "0000.0000"
			};
			PortableGridModel item7 = new PortableGridModel
			{
				name = j.ToString(),
				setV = "0000.0000"
			};
			PortableGridModel item8 = new PortableGridModel
			{
				name = j.ToString(),
				setV = "0000.0000"
			};
			lstBSource1.Add(item5);
			lstBSource2.Add(item6);
			lstBSource3.Add(item7);
			lstBSource4.Add(item8);
		}
		uDgBChannel1.DataSource = lstBSource1;
		uDgBChannel2.DataSource = lstBSource2;
		uDgBChannel3.DataSource = lstBSource3;
		uDgBChannel4.DataSource = lstBSource4;
		picBoxFire.Image = imageList1.Images[2];
		labUser.Text = "登录账号：" + Class49.user_0.u_name;
	}

	public void fireOn()
	{
		picBoxFire.Image = imageList1.Images[2];
	}

	public void fireOff()
	{
		picBoxFire.Image = imageList1.Images[3];
	}

	private void FormOnline_Load(object sender, EventArgs e)
	{
	}

	private void ucBtnHis_BtnClick(object sender, EventArgs e)
	{
		FormHistoryLX formHistoryLX = new FormHistoryLX();
		formHistoryLX.TopMost = true;
		formHistoryLX.Show();
	}

	private void ucBtnSet_BtnClick(object sender, EventArgs e)
	{
		bool flag = true;
		FormMain.fromMain.Show();
		FormMain.fromMain.Activate();
		FormMain.fromMain.WindowState = FormWindowState.Maximized;
		Hide();
	}

	private void ucBtnReLon_BtnClick(object sender, EventArgs e)
	{
		Logon logon = new Logon();
		logon.TopMost = true;
		if (logon.ShowDialog() == DialogResult.OK)
		{
		}
		labUser.Text = "登录账号：" + Class49.user_0.u_name;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormOnline));
		this.btnSetPara = new System.Windows.Forms.Button();
		this.labSignal = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.picBoxFire = new System.Windows.Forms.PictureBox();
		this.tabControlExt1 = new HZH_Controls.Controls.TabControlExt();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.uDgChannel1 = new HZH_Controls.Controls.UCDataGridView();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.uDgChannel2 = new HZH_Controls.Controls.UCDataGridView();
		this.uDgChannel3 = new HZH_Controls.Controls.UCDataGridView();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.uDgChannel4 = new HZH_Controls.Controls.UCDataGridView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.labUser = new System.Windows.Forms.Label();
		this.ucBtnHis = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnSet = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnReLon = new HZH_Controls.Controls.UCBtnExt();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.uDgBChannel1 = new HZH_Controls.Controls.UCDataGridView();
		this.splitContainer4 = new System.Windows.Forms.SplitContainer();
		this.uDgBChannel2 = new HZH_Controls.Controls.UCDataGridView();
		this.uDgBChannel3 = new HZH_Controls.Controls.UCDataGridView();
		this.uDgBChannel4 = new HZH_Controls.Controls.UCDataGridView();
		((System.ComponentModel.ISupportInitialize)this.picBoxFire).BeginInit();
		this.tabControlExt1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.tabPage4.SuspendLayout();
		this.tabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
		this.splitContainer3.Panel1.SuspendLayout();
		this.splitContainer3.Panel2.SuspendLayout();
		this.splitContainer3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer4).BeginInit();
		this.splitContainer4.Panel1.SuspendLayout();
		this.splitContainer4.Panel2.SuspendLayout();
		this.splitContainer4.SuspendLayout();
		base.SuspendLayout();
		this.btnSetPara.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnSetPara.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnSetPara.Location = new System.Drawing.Point(16, -81);
		this.btnSetPara.Name = "btnSetPara";
		this.btnSetPara.Size = new System.Drawing.Size(187, 80);
		this.btnSetPara.TabIndex = 1;
		this.btnSetPara.Text = "参数设置";
		this.btnSetPara.UseVisualStyleBackColor = true;
		this.btnSetPara.Click += new System.EventHandler(btnSetPara_Click);
		this.labSignal.BackColor = System.Drawing.Color.Transparent;
		this.labSignal.Cursor = System.Windows.Forms.Cursors.Default;
		this.labSignal.Font = new System.Drawing.Font("宋体", 21.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labSignal.Location = new System.Drawing.Point(199, 9);
		this.labSignal.Name = "labSignal";
		this.labSignal.Size = new System.Drawing.Size(145, 56);
		this.labSignal.TabIndex = 8;
		this.labSignal.Text = "0.00";
		this.labSignal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.picBoxFire.BackColor = System.Drawing.Color.Transparent;
		this.picBoxFire.Cursor = System.Windows.Forms.Cursors.Hand;
		this.picBoxFire.Image = IBrainChrom2018.Properties.Resources.gas_50px;
		this.picBoxFire.Location = new System.Drawing.Point(124, 9);
		this.picBoxFire.Name = "picBoxFire";
		this.picBoxFire.Size = new System.Drawing.Size(57, 51);
		this.picBoxFire.TabIndex = 9;
		this.picBoxFire.TabStop = false;
		this.picBoxFire.Click += new System.EventHandler(picBoxFire_Click);
		this.tabControlExt1.CloseBtnColor = System.Drawing.Color.FromArgb(255, 85, 51);
		this.tabControlExt1.Controls.Add(this.tabPage1);
		this.tabControlExt1.Controls.Add(this.tabPage2);
		this.tabControlExt1.Controls.Add(this.tabPage3);
		this.tabControlExt1.Controls.Add(this.tabPage4);
		this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.tabControlExt1.ImageList = this.imageList1;
		this.tabControlExt1.IsShowCloseBtn = false;
		this.tabControlExt1.ItemSize = new System.Drawing.Size(0, 50);
		this.tabControlExt1.Location = new System.Drawing.Point(0, 68);
		this.tabControlExt1.Name = "tabControlExt1";
		this.tabControlExt1.SelectedIndex = 0;
		this.tabControlExt1.Size = new System.Drawing.Size(800, 532);
		this.tabControlExt1.TabIndex = 35;
		this.tabControlExt1.UncloseTabIndexs = null;
		this.tabPage1.Controls.Add(this.splitContainer1);
		this.tabPage1.ImageIndex = 0;
		this.tabPage1.Location = new System.Drawing.Point(4, 54);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(792, 474);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.ToolTipText = "流路1-3";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 3);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.uDgChannel1);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
		this.splitContainer1.Size = new System.Drawing.Size(786, 468);
		this.splitContainer1.SplitterDistance = 260;
		this.splitContainer1.TabIndex = 36;
		this.uDgChannel1.BackColor = System.Drawing.Color.White;
		this.uDgChannel1.Columns = null;
		this.uDgChannel1.DataSource = null;
		this.uDgChannel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.uDgChannel1.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgChannel1.HeadHeight = 25;
		this.uDgChannel1.HeadPadingLeft = 0;
		this.uDgChannel1.HeadTextColor = System.Drawing.Color.Black;
		this.uDgChannel1.IsShowCheckBox = false;
		this.uDgChannel1.IsShowHead = true;
		this.uDgChannel1.Location = new System.Drawing.Point(0, 0);
		this.uDgChannel1.Name = "uDgChannel1";
		this.uDgChannel1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgChannel1.RowHeight = 20;
		this.uDgChannel1.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgChannel1.Size = new System.Drawing.Size(260, 468);
		this.uDgChannel1.TabIndex = 34;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Panel1.Controls.Add(this.uDgChannel2);
		this.splitContainer2.Panel2.Controls.Add(this.uDgChannel3);
		this.splitContainer2.Size = new System.Drawing.Size(522, 468);
		this.splitContainer2.SplitterDistance = 257;
		this.splitContainer2.TabIndex = 0;
		this.uDgChannel2.BackColor = System.Drawing.Color.White;
		this.uDgChannel2.Columns = null;
		this.uDgChannel2.DataSource = null;
		this.uDgChannel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.uDgChannel2.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgChannel2.HeadHeight = 25;
		this.uDgChannel2.HeadPadingLeft = 0;
		this.uDgChannel2.HeadTextColor = System.Drawing.Color.Black;
		this.uDgChannel2.IsShowCheckBox = false;
		this.uDgChannel2.IsShowHead = true;
		this.uDgChannel2.Location = new System.Drawing.Point(0, 0);
		this.uDgChannel2.Name = "uDgChannel2";
		this.uDgChannel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgChannel2.RowHeight = 20;
		this.uDgChannel2.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgChannel2.Size = new System.Drawing.Size(257, 468);
		this.uDgChannel2.TabIndex = 35;
		this.uDgChannel3.BackColor = System.Drawing.Color.White;
		this.uDgChannel3.Columns = null;
		this.uDgChannel3.DataSource = null;
		this.uDgChannel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.uDgChannel3.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgChannel3.HeadHeight = 25;
		this.uDgChannel3.HeadPadingLeft = 0;
		this.uDgChannel3.HeadTextColor = System.Drawing.Color.Black;
		this.uDgChannel3.IsShowCheckBox = false;
		this.uDgChannel3.IsShowHead = true;
		this.uDgChannel3.Location = new System.Drawing.Point(0, 0);
		this.uDgChannel3.Name = "uDgChannel3";
		this.uDgChannel3.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgChannel3.RowHeight = 20;
		this.uDgChannel3.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgChannel3.Size = new System.Drawing.Size(261, 468);
		this.uDgChannel3.TabIndex = 35;
		this.tabPage2.Controls.Add(this.uDgChannel4);
		this.tabPage2.ImageIndex = 0;
		this.tabPage2.Location = new System.Drawing.Point(4, 54);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(792, 474);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.ToolTipText = "流路4";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.uDgChannel4.BackColor = System.Drawing.Color.White;
		this.uDgChannel4.Columns = null;
		this.uDgChannel4.DataSource = null;
		this.uDgChannel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.uDgChannel4.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgChannel4.HeadHeight = 25;
		this.uDgChannel4.HeadPadingLeft = 0;
		this.uDgChannel4.HeadTextColor = System.Drawing.Color.Black;
		this.uDgChannel4.IsShowCheckBox = false;
		this.uDgChannel4.IsShowHead = true;
		this.uDgChannel4.Location = new System.Drawing.Point(3, 3);
		this.uDgChannel4.Name = "uDgChannel4";
		this.uDgChannel4.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgChannel4.RowHeight = 20;
		this.uDgChannel4.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgChannel4.Size = new System.Drawing.Size(265, 468);
		this.uDgChannel4.TabIndex = 36;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "folder_52px.png");
		this.imageList1.Images.SetKeyName(1, "opened_folder_52px.png");
		this.imageList1.Images.SetKeyName(2, "gas_50px.png");
		this.imageList1.Images.SetKeyName(3, "gas_50px灰.png");
		this.labUser.AutoSize = true;
		this.labUser.Font = new System.Drawing.Font("微软雅黑", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labUser.ImageIndex = 2;
		this.labUser.Location = new System.Drawing.Point(610, 28);
		this.labUser.Name = "labUser";
		this.labUser.Size = new System.Drawing.Size(69, 25);
		this.labUser.TabIndex = 36;
		this.labUser.Text = "管理员";
		this.ucBtnHis.BackColor = System.Drawing.Color.White;
		this.ucBtnHis.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnHis.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnHis.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnHis.BtnText = "历史数据";
		this.ucBtnHis.ConerRadius = 5;
		this.ucBtnHis.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnHis.EnabledMouseEffect = false;
		this.ucBtnHis.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnHis.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnHis.IsRadius = true;
		this.ucBtnHis.IsShowRect = true;
		this.ucBtnHis.IsShowTips = false;
		this.ucBtnHis.Location = new System.Drawing.Point(694, 68);
		this.ucBtnHis.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnHis.Name = "ucBtnHis";
		this.ucBtnHis.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnHis.RectWidth = 1;
		this.ucBtnHis.Size = new System.Drawing.Size(105, 51);
		this.ucBtnHis.TabIndex = 37;
		this.ucBtnHis.TabStop = false;
		this.ucBtnHis.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnHis.TipsText = "";
		this.ucBtnHis.BtnClick += new System.EventHandler(ucBtnHis_BtnClick);
		this.ucBtnSet.BackColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnSet.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnText = "仪器设置";
		this.ucBtnSet.ConerRadius = 5;
		this.ucBtnSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnSet.EnabledMouseEffect = false;
		this.ucBtnSet.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnSet.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnSet.IsRadius = true;
		this.ucBtnSet.IsShowRect = true;
		this.ucBtnSet.IsShowTips = false;
		this.ucBtnSet.Location = new System.Drawing.Point(510, 67);
		this.ucBtnSet.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnSet.Name = "ucBtnSet";
		this.ucBtnSet.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnSet.RectWidth = 1;
		this.ucBtnSet.Size = new System.Drawing.Size(105, 51);
		this.ucBtnSet.TabIndex = 38;
		this.ucBtnSet.TabStop = false;
		this.ucBtnSet.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnSet.TipsText = "";
		this.ucBtnSet.BtnClick += new System.EventHandler(ucBtnSet_BtnClick);
		this.ucBtnReLon.BackColor = System.Drawing.Color.White;
		this.ucBtnReLon.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnReLon.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnReLon.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnReLon.BtnText = "重新登录";
		this.ucBtnReLon.ConerRadius = 5;
		this.ucBtnReLon.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnReLon.EnabledMouseEffect = false;
		this.ucBtnReLon.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnReLon.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnReLon.IsRadius = true;
		this.ucBtnReLon.IsShowRect = true;
		this.ucBtnReLon.IsShowTips = false;
		this.ucBtnReLon.Location = new System.Drawing.Point(331, 66);
		this.ucBtnReLon.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnReLon.Name = "ucBtnReLon";
		this.ucBtnReLon.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnReLon.RectWidth = 1;
		this.ucBtnReLon.Size = new System.Drawing.Size(105, 51);
		this.ucBtnReLon.TabIndex = 39;
		this.ucBtnReLon.TabStop = false;
		this.ucBtnReLon.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnReLon.TipsText = "";
		this.ucBtnReLon.BtnClick += new System.EventHandler(ucBtnReLon_BtnClick);
		this.tabPage4.Controls.Add(this.uDgBChannel4);
		this.tabPage4.ImageIndex = 0;
		this.tabPage4.Location = new System.Drawing.Point(4, 54);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Size = new System.Drawing.Size(792, 474);
		this.tabPage4.TabIndex = 3;
		this.tabPage4.UseVisualStyleBackColor = true;
		this.tabPage3.Controls.Add(this.splitContainer3);
		this.tabPage3.ImageIndex = 0;
		this.tabPage3.Location = new System.Drawing.Point(4, 54);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(792, 474);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.UseVisualStyleBackColor = true;
		this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer3.Location = new System.Drawing.Point(0, 0);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Panel1.Controls.Add(this.uDgBChannel1);
		this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
		this.splitContainer3.Size = new System.Drawing.Size(792, 474);
		this.splitContainer3.SplitterDistance = 261;
		this.splitContainer3.TabIndex = 37;
		this.uDgBChannel1.BackColor = System.Drawing.Color.White;
		this.uDgBChannel1.Columns = null;
		this.uDgBChannel1.DataSource = null;
		this.uDgBChannel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.uDgBChannel1.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgBChannel1.HeadHeight = 25;
		this.uDgBChannel1.HeadPadingLeft = 0;
		this.uDgBChannel1.HeadTextColor = System.Drawing.Color.Black;
		this.uDgBChannel1.IsShowCheckBox = false;
		this.uDgBChannel1.IsShowHead = true;
		this.uDgBChannel1.Location = new System.Drawing.Point(0, 0);
		this.uDgBChannel1.Name = "uDgBChannel1";
		this.uDgBChannel1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgBChannel1.RowHeight = 20;
		this.uDgBChannel1.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgBChannel1.Size = new System.Drawing.Size(261, 474);
		this.uDgBChannel1.TabIndex = 34;
		this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer4.Location = new System.Drawing.Point(0, 0);
		this.splitContainer4.Name = "splitContainer4";
		this.splitContainer4.Panel1.Controls.Add(this.uDgBChannel2);
		this.splitContainer4.Panel2.Controls.Add(this.uDgBChannel3);
		this.splitContainer4.Size = new System.Drawing.Size(527, 474);
		this.splitContainer4.SplitterDistance = 258;
		this.splitContainer4.TabIndex = 0;
		this.uDgBChannel2.BackColor = System.Drawing.Color.White;
		this.uDgBChannel2.Columns = null;
		this.uDgBChannel2.DataSource = null;
		this.uDgBChannel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.uDgBChannel2.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgBChannel2.HeadHeight = 25;
		this.uDgBChannel2.HeadPadingLeft = 0;
		this.uDgBChannel2.HeadTextColor = System.Drawing.Color.Black;
		this.uDgBChannel2.IsShowCheckBox = false;
		this.uDgBChannel2.IsShowHead = true;
		this.uDgBChannel2.Location = new System.Drawing.Point(0, 0);
		this.uDgBChannel2.Name = "uDgBChannel2";
		this.uDgBChannel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgBChannel2.RowHeight = 20;
		this.uDgBChannel2.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgBChannel2.Size = new System.Drawing.Size(258, 474);
		this.uDgBChannel2.TabIndex = 35;
		this.uDgBChannel3.BackColor = System.Drawing.Color.White;
		this.uDgBChannel3.Columns = null;
		this.uDgBChannel3.DataSource = null;
		this.uDgBChannel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.uDgBChannel3.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgBChannel3.HeadHeight = 25;
		this.uDgBChannel3.HeadPadingLeft = 0;
		this.uDgBChannel3.HeadTextColor = System.Drawing.Color.Black;
		this.uDgBChannel3.IsShowCheckBox = false;
		this.uDgBChannel3.IsShowHead = true;
		this.uDgBChannel3.Location = new System.Drawing.Point(0, 0);
		this.uDgBChannel3.Name = "uDgBChannel3";
		this.uDgBChannel3.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgBChannel3.RowHeight = 20;
		this.uDgBChannel3.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgBChannel3.Size = new System.Drawing.Size(265, 474);
		this.uDgBChannel3.TabIndex = 35;
		this.uDgBChannel4.BackColor = System.Drawing.Color.White;
		this.uDgBChannel4.Columns = null;
		this.uDgBChannel4.DataSource = null;
		this.uDgBChannel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.uDgBChannel4.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.uDgBChannel4.HeadHeight = 25;
		this.uDgBChannel4.HeadPadingLeft = 0;
		this.uDgBChannel4.HeadTextColor = System.Drawing.Color.Black;
		this.uDgBChannel4.IsShowCheckBox = false;
		this.uDgBChannel4.IsShowHead = true;
		this.uDgBChannel4.Location = new System.Drawing.Point(0, 0);
		this.uDgBChannel4.Name = "uDgBChannel4";
		this.uDgBChannel4.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.uDgBChannel4.RowHeight = 20;
		this.uDgBChannel4.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.uDgBChannel4.Size = new System.Drawing.Size(265, 474);
		this.uDgBChannel4.TabIndex = 37;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new System.Drawing.Size(800, 600);
		base.Controls.Add(this.ucBtnReLon);
		base.Controls.Add(this.ucBtnSet);
		base.Controls.Add(this.ucBtnHis);
		base.Controls.Add(this.labUser);
		base.Controls.Add(this.tabControlExt1);
		base.Controls.Add(this.picBoxFire);
		base.Controls.Add(this.labSignal);
		base.Controls.Add(this.btnSetPara);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormOnline";
		this.Text = "FormKR";
		base.Load += new System.EventHandler(FormOnline_Load);
		((System.ComponentModel.ISupportInitialize)this.picBoxFire).EndInit();
		this.tabControlExt1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.tabPage4.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		this.splitContainer3.Panel1.ResumeLayout(false);
		this.splitContainer3.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
		this.splitContainer3.ResumeLayout(false);
		this.splitContainer4.Panel1.ResumeLayout(false);
		this.splitContainer4.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer4).EndInit();
		this.splitContainer4.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
