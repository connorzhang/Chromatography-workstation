using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormKR : Form
{
	public static FormKR selfCtrl;

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

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	private Button btnSetPara;

	private Label labComp1;

	private Label labC2H4;

	private Label labC2H6;

	private Label labC2H2;

	private Label labC3H8;

	private Label labTHC;

	private Timer timer1;

	private Label labCompName1;

	private Label labCompName2;

	private Label labCompName3;

	private Label labCompName4;

	private Label labCompName5;

	private Label labCompTotalName;

	public PictureBox picBoxFire;

	public Label labSignal;

	private Label labTitle;

	private Label labCompName10;

	private Label labCompName9;

	private Label labCompName8;

	private Label labCompName7;

	private Label labCompName6;

	private Label labCompName12;

	private Label labCompName11;

	private Label labComp10;

	private Label labComp9;

	private Label labComp8;

	private Label labComp7;

	private Label labComp6;

	private Label labComp12;

	private Label labComp11;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Label lab2Comp1;

	private Label lab2Comp12;

	private Label lab2Comp2;

	private Label lab2Comp11;

	private Label lab2Comp3;

	private Label lab2Comp10;

	private Label lab2Comp4;

	private Label lab2Comp9;

	private Label lab2Comp5;

	private Label lab2Comp8;

	private Label labTHC2;

	private Label lab2Comp7;

	private Label lab2CompName1;

	private Label lab2Comp6;

	private Label lab2CompName2;

	private Label lab2CompName12;

	private Label lab2CompName3;

	private Label lab2CompName11;

	private Label lab2CompName4;

	private Label lab2CompName10;

	private Label lab2CompName5;

	private Label lab2CompName9;

	private Label label23;

	private Label lab2CompName8;

	private Label lab2CompName6;

	private Label lab2CompName7;

	private GroupBox groupBox3;

	private Label lab4Comp1;

	private Label lab4Comp12;

	private Label lab4Comp2;

	private Label lab4Comp11;

	private Label lab4Comp3;

	private Label lab4Comp10;

	private Label lab4Comp4;

	private Label lab4Comp9;

	private Label lab4Comp5;

	private Label lab4Comp8;

	private Label labTHC4;

	private Label lab4Comp7;

	private Label lab4CompName1;

	private Label lab4Comp6;

	private Label lab4CompName2;

	private Label lab4CompName12;

	private Label lab4CompName3;

	private Label lab4CompName11;

	private Label lab4CompName4;

	private Label lab4CompName10;

	private Label lab4CompName5;

	private Label lab4CompName9;

	private Label label49;

	private Label lab4CompName8;

	private Label lab4CompName6;

	private Label lab4CompName7;

	private GroupBox groupBox4;

	private Label lab3Comp1;

	private Label lab3Comp12;

	private Label lab3Comp2;

	private Label lab3Comp11;

	private Label lab3Comp3;

	private Label lab3Comp10;

	private Label lab3Comp4;

	private Label lab3Comp9;

	private Label lab3Comp5;

	private Label lab3Comp8;

	private Label labTHC3;

	private Label lab3Comp7;

	private Label lab3CompName1;

	private Label lab3Comp6;

	private Label lab3CompName2;

	private Label lab3CompName12;

	private Label lab3CompName3;

	private Label lab3CompName11;

	private Label lab3CompName4;

	private Label lab3CompName10;

	private Label lab3CompName5;

	private Label lab3CompName9;

	private Label label75;

	private Label lab3CompName8;

	private Label lab3CompName6;

	private Label lab3CompName7;

	private Label 浓度;

	public Label labAmount;

	public FormKR()
	{
		selfCtrl = this;
		InitializeComponent();
		labTitle.Text = frmParam.strName;
		strCompName = new string[15];
		strCompName2 = new string[15];
		strCompName3 = new string[15];
		strCompName4 = new string[15];
		fComponet = new float[15];
		fComponet2 = new float[15];
		fComponet3 = new float[15];
		fComponet4 = new float[15];
		timer1.Enabled = true;
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
		fTotal = 0f;
		fTotal2 = 0f;
		fTotal3 = 0f;
		fTotal4 = 0f;
		for (int i = 0; i < 15; i++)
		{
			if (frmParam.bSum[i])
			{
				fTotal += fComponet[i];
				fTotal2 += fComponet2[i];
				fTotal3 += fComponet3[i];
				fTotal4 += fComponet4[i];
			}
		}
		labTHC.Text = fTotal.ToString("F" + Class49.int_8) + "   ppm";
		labTHC2.Text = fTotal2.ToString("F" + Class49.int_8) + "   ppm";
		labTHC3.Text = fTotal3.ToString("F" + Class49.int_8) + "   ppm";
		labTHC4.Text = fTotal4.ToString("F" + Class49.int_8) + "   ppm";
		Label label = lab4CompName1;
		Label label2 = lab3CompName1;
		Label label3 = lab2CompName1;
		string text = (labCompName1.Text = strCompName[0]);
		string text3 = (label3.Text = text);
		string text5 = (label2.Text = text3);
		label.Text = text5;
		if (labCompName1.Text == "")
		{
			labComp1.Text = "";
			lab2Comp1.Text = "";
			lab3Comp1.Text = "";
			lab4Comp1.Text = "";
		}
		else
		{
			labComp1.Text = fComponet[0].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp1.Text = fComponet2[0].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp1.Text = fComponet3[0].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp1.Text = fComponet4[0].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label4 = lab4CompName2;
		Label label5 = lab3CompName2;
		Label label6 = lab2CompName2;
		text = (labCompName2.Text = strCompName[1]);
		text3 = (label6.Text = text);
		text5 = (label5.Text = text3);
		label4.Text = text5;
		if (labCompName2.Text == "")
		{
			labC2H4.Text = "";
			lab2Comp2.Text = "";
			lab3Comp2.Text = "";
			lab4Comp2.Text = "";
		}
		else
		{
			labC2H4.Text = fComponet[1].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp2.Text = fComponet2[1].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp2.Text = fComponet3[1].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp2.Text = fComponet4[1].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label7 = lab4CompName3;
		Label label8 = lab3CompName3;
		Label label9 = lab2CompName3;
		text = (labCompName3.Text = strCompName[2]);
		text3 = (label9.Text = text);
		text5 = (label8.Text = text3);
		label7.Text = text5;
		if (labCompName3.Text == "")
		{
			labC2H6.Text = "";
			lab2Comp3.Text = "";
			lab3Comp3.Text = "";
			lab4Comp3.Text = "";
		}
		else
		{
			labC2H6.Text = fComponet[2].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp3.Text = fComponet2[2].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp3.Text = fComponet3[2].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp3.Text = fComponet4[2].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label10 = lab4CompName4;
		Label label11 = lab3CompName4;
		Label label12 = lab2CompName4;
		text = (labCompName4.Text = strCompName[3]);
		text3 = (label12.Text = text);
		text5 = (label11.Text = text3);
		label10.Text = text5;
		if (labCompName4.Text == "")
		{
			labC2H2.Text = "";
			lab2Comp4.Text = "";
			lab3Comp4.Text = "";
			lab4Comp4.Text = "";
		}
		else
		{
			labC2H2.Text = fComponet[3].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp4.Text = fComponet2[3].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp4.Text = fComponet3[3].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp4.Text = fComponet4[3].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label13 = lab4CompName5;
		Label label14 = lab3CompName5;
		Label label15 = lab2CompName5;
		text = (labCompName5.Text = strCompName[4]);
		text3 = (label15.Text = text);
		text5 = (label14.Text = text3);
		label13.Text = text5;
		if (labCompName5.Text == "")
		{
			labC3H8.Text = "";
			lab2Comp5.Text = "";
			lab3Comp5.Text = "";
			lab4Comp5.Text = "";
		}
		else
		{
			labC3H8.Text = fComponet[4].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp5.Text = fComponet2[4].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp5.Text = fComponet3[4].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp5.Text = fComponet4[4].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label16 = lab4CompName6;
		Label label17 = lab3CompName6;
		Label label18 = lab2CompName6;
		text = (labCompName6.Text = strCompName[5]);
		text3 = (label18.Text = text);
		text5 = (label17.Text = text3);
		label16.Text = text5;
		if (labCompName6.Text == "")
		{
			labComp6.Text = "";
			lab2Comp6.Text = "";
			lab3Comp6.Text = "";
			lab4Comp6.Text = "";
		}
		else
		{
			labComp6.Text = fComponet[5].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp6.Text = fComponet2[5].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp6.Text = fComponet3[5].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp6.Text = fComponet4[5].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label19 = lab4CompName7;
		Label label20 = lab3CompName7;
		Label label21 = lab2CompName7;
		text = (labCompName7.Text = strCompName[6]);
		text3 = (label21.Text = text);
		text5 = (label20.Text = text3);
		label19.Text = text5;
		if (labCompName7.Text == "")
		{
			labComp7.Text = "";
			lab2Comp7.Text = "";
			lab3Comp7.Text = "";
			lab4Comp7.Text = "";
		}
		else
		{
			labComp7.Text = fComponet[6].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp7.Text = fComponet2[6].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp7.Text = fComponet3[6].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp7.Text = fComponet4[6].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label22 = lab4CompName8;
		Label label23 = lab3CompName8;
		Label label24 = lab2CompName8;
		text = (labCompName8.Text = strCompName[7]);
		text3 = (label24.Text = text);
		text5 = (label23.Text = text3);
		label22.Text = text5;
		if (labCompName8.Text == "")
		{
			labComp8.Text = "";
			lab2Comp8.Text = "";
			lab3Comp8.Text = "";
			lab4Comp8.Text = "";
		}
		else
		{
			labComp8.Text = fComponet[7].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp8.Text = fComponet2[7].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp8.Text = fComponet3[7].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp8.Text = fComponet4[7].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label25 = lab4CompName9;
		Label label26 = lab3CompName9;
		Label label27 = lab2CompName9;
		text = (labCompName9.Text = strCompName[8]);
		text3 = (label27.Text = text);
		text5 = (label26.Text = text3);
		label25.Text = text5;
		if (labCompName9.Text == "")
		{
			labComp9.Text = "";
			lab2Comp9.Text = "";
			lab3Comp9.Text = "";
			lab4Comp9.Text = "";
		}
		else
		{
			labComp9.Text = fComponet[8].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp9.Text = fComponet2[8].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp9.Text = fComponet3[8].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp9.Text = fComponet4[8].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label28 = lab4CompName10;
		Label label29 = lab3CompName10;
		Label label30 = lab2CompName10;
		text = (labCompName10.Text = strCompName[9]);
		text3 = (label30.Text = text);
		text5 = (label29.Text = text3);
		label28.Text = text5;
		if (labCompName10.Text == "")
		{
			labComp10.Text = "";
			lab2Comp10.Text = "";
			lab3Comp10.Text = "";
			lab4Comp10.Text = "";
		}
		else
		{
			labComp10.Text = fComponet[9].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp10.Text = fComponet2[9].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp10.Text = fComponet3[9].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp10.Text = fComponet4[9].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label31 = lab4CompName11;
		Label label32 = lab3CompName11;
		Label label33 = lab2CompName11;
		text = (labCompName11.Text = strCompName[10]);
		text3 = (label33.Text = text);
		text5 = (label32.Text = text3);
		label31.Text = text5;
		if (labCompName11.Text == "")
		{
			labComp11.Text = "";
			lab2Comp11.Text = "";
			lab3Comp11.Text = "";
			lab4Comp11.Text = "";
		}
		else
		{
			labComp11.Text = fComponet[10].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp11.Text = fComponet2[10].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp11.Text = fComponet3[10].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp11.Text = fComponet4[10].ToString("F" + Class49.int_8) + "   ppm";
		}
		Label label34 = lab4CompName12;
		Label label35 = lab3CompName12;
		Label label36 = lab2CompName12;
		text = (labCompName12.Text = strCompName[11]);
		text3 = (label36.Text = text);
		text5 = (label35.Text = text3);
		label34.Text = text5;
		if (labCompName12.Text == "")
		{
			labComp12.Text = "";
			lab2Comp12.Text = "";
			lab3Comp12.Text = "";
			lab4Comp12.Text = "";
		}
		else
		{
			labComp12.Text = fComponet[11].ToString("F" + Class49.int_8) + "   ppm";
			lab2Comp12.Text = fComponet2[11].ToString("F" + Class49.int_8) + "   ppm";
			lab3Comp12.Text = fComponet3[11].ToString("F" + Class49.int_8) + "   ppm";
			lab4Comp12.Text = fComponet4[11].ToString("F" + Class49.int_8) + "   ppm";
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormKR));
		this.btnSetPara = new System.Windows.Forms.Button();
		this.labComp1 = new System.Windows.Forms.Label();
		this.labC2H4 = new System.Windows.Forms.Label();
		this.labC2H6 = new System.Windows.Forms.Label();
		this.labC2H2 = new System.Windows.Forms.Label();
		this.labC3H8 = new System.Windows.Forms.Label();
		this.labTHC = new System.Windows.Forms.Label();
		this.labSignal = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.picBoxFire = new System.Windows.Forms.PictureBox();
		this.labCompName1 = new System.Windows.Forms.Label();
		this.labCompName2 = new System.Windows.Forms.Label();
		this.labCompName3 = new System.Windows.Forms.Label();
		this.labCompName4 = new System.Windows.Forms.Label();
		this.labCompName5 = new System.Windows.Forms.Label();
		this.labCompTotalName = new System.Windows.Forms.Label();
		this.labTitle = new System.Windows.Forms.Label();
		this.labCompName10 = new System.Windows.Forms.Label();
		this.labCompName9 = new System.Windows.Forms.Label();
		this.labCompName8 = new System.Windows.Forms.Label();
		this.labCompName7 = new System.Windows.Forms.Label();
		this.labCompName6 = new System.Windows.Forms.Label();
		this.labCompName12 = new System.Windows.Forms.Label();
		this.labCompName11 = new System.Windows.Forms.Label();
		this.labComp10 = new System.Windows.Forms.Label();
		this.labComp9 = new System.Windows.Forms.Label();
		this.labComp8 = new System.Windows.Forms.Label();
		this.labComp7 = new System.Windows.Forms.Label();
		this.labComp6 = new System.Windows.Forms.Label();
		this.labComp12 = new System.Windows.Forms.Label();
		this.labComp11 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lab2Comp1 = new System.Windows.Forms.Label();
		this.lab2Comp12 = new System.Windows.Forms.Label();
		this.lab2Comp2 = new System.Windows.Forms.Label();
		this.lab2Comp11 = new System.Windows.Forms.Label();
		this.lab2Comp3 = new System.Windows.Forms.Label();
		this.lab2Comp10 = new System.Windows.Forms.Label();
		this.lab2Comp4 = new System.Windows.Forms.Label();
		this.lab2Comp9 = new System.Windows.Forms.Label();
		this.lab2Comp5 = new System.Windows.Forms.Label();
		this.lab2Comp8 = new System.Windows.Forms.Label();
		this.labTHC2 = new System.Windows.Forms.Label();
		this.lab2Comp7 = new System.Windows.Forms.Label();
		this.lab2CompName1 = new System.Windows.Forms.Label();
		this.lab2Comp6 = new System.Windows.Forms.Label();
		this.lab2CompName2 = new System.Windows.Forms.Label();
		this.lab2CompName12 = new System.Windows.Forms.Label();
		this.lab2CompName3 = new System.Windows.Forms.Label();
		this.lab2CompName11 = new System.Windows.Forms.Label();
		this.lab2CompName4 = new System.Windows.Forms.Label();
		this.lab2CompName10 = new System.Windows.Forms.Label();
		this.lab2CompName5 = new System.Windows.Forms.Label();
		this.lab2CompName9 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.lab2CompName8 = new System.Windows.Forms.Label();
		this.lab2CompName6 = new System.Windows.Forms.Label();
		this.lab2CompName7 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.lab4Comp1 = new System.Windows.Forms.Label();
		this.lab4Comp12 = new System.Windows.Forms.Label();
		this.lab4Comp2 = new System.Windows.Forms.Label();
		this.lab4Comp11 = new System.Windows.Forms.Label();
		this.lab4Comp3 = new System.Windows.Forms.Label();
		this.lab4Comp10 = new System.Windows.Forms.Label();
		this.lab4Comp4 = new System.Windows.Forms.Label();
		this.lab4Comp9 = new System.Windows.Forms.Label();
		this.lab4Comp5 = new System.Windows.Forms.Label();
		this.lab4Comp8 = new System.Windows.Forms.Label();
		this.labTHC4 = new System.Windows.Forms.Label();
		this.lab4Comp7 = new System.Windows.Forms.Label();
		this.lab4CompName1 = new System.Windows.Forms.Label();
		this.lab4Comp6 = new System.Windows.Forms.Label();
		this.lab4CompName2 = new System.Windows.Forms.Label();
		this.lab4CompName12 = new System.Windows.Forms.Label();
		this.lab4CompName3 = new System.Windows.Forms.Label();
		this.lab4CompName11 = new System.Windows.Forms.Label();
		this.lab4CompName4 = new System.Windows.Forms.Label();
		this.lab4CompName10 = new System.Windows.Forms.Label();
		this.lab4CompName5 = new System.Windows.Forms.Label();
		this.lab4CompName9 = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.lab4CompName8 = new System.Windows.Forms.Label();
		this.lab4CompName6 = new System.Windows.Forms.Label();
		this.lab4CompName7 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.lab3Comp1 = new System.Windows.Forms.Label();
		this.lab3Comp12 = new System.Windows.Forms.Label();
		this.lab3Comp2 = new System.Windows.Forms.Label();
		this.lab3Comp11 = new System.Windows.Forms.Label();
		this.lab3Comp3 = new System.Windows.Forms.Label();
		this.lab3Comp10 = new System.Windows.Forms.Label();
		this.lab3Comp4 = new System.Windows.Forms.Label();
		this.lab3Comp9 = new System.Windows.Forms.Label();
		this.lab3Comp5 = new System.Windows.Forms.Label();
		this.lab3Comp8 = new System.Windows.Forms.Label();
		this.labTHC3 = new System.Windows.Forms.Label();
		this.lab3Comp7 = new System.Windows.Forms.Label();
		this.lab3CompName1 = new System.Windows.Forms.Label();
		this.lab3Comp6 = new System.Windows.Forms.Label();
		this.lab3CompName2 = new System.Windows.Forms.Label();
		this.lab3CompName12 = new System.Windows.Forms.Label();
		this.lab3CompName3 = new System.Windows.Forms.Label();
		this.lab3CompName11 = new System.Windows.Forms.Label();
		this.lab3CompName4 = new System.Windows.Forms.Label();
		this.lab3CompName10 = new System.Windows.Forms.Label();
		this.lab3CompName5 = new System.Windows.Forms.Label();
		this.lab3CompName9 = new System.Windows.Forms.Label();
		this.label75 = new System.Windows.Forms.Label();
		this.lab3CompName8 = new System.Windows.Forms.Label();
		this.lab3CompName6 = new System.Windows.Forms.Label();
		this.lab3CompName7 = new System.Windows.Forms.Label();
		this.浓度 = new System.Windows.Forms.Label();
		this.labAmount = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.picBoxFire).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.btnSetPara.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSetPara.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnSetPara.Location = new System.Drawing.Point(1081, 671);
		this.btnSetPara.Name = "btnSetPara";
		this.btnSetPara.Size = new System.Drawing.Size(187, 80);
		this.btnSetPara.TabIndex = 1;
		this.btnSetPara.Text = "参数设置";
		this.btnSetPara.UseVisualStyleBackColor = true;
		this.btnSetPara.Click += new System.EventHandler(btnSetPara_Click);
		this.labComp1.AutoSize = true;
		this.labComp1.BackColor = System.Drawing.Color.Transparent;
		this.labComp1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp1.Location = new System.Drawing.Point(145, 47);
		this.labComp1.Name = "labComp1";
		this.labComp1.Size = new System.Drawing.Size(59, 20);
		this.labComp1.TabIndex = 2;
		this.labComp1.Text = "0    ";
		this.labComp1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp1.Click += new System.EventHandler(labComp1_Click);
		this.labC2H4.AutoSize = true;
		this.labC2H4.BackColor = System.Drawing.Color.Transparent;
		this.labC2H4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labC2H4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labC2H4.Location = new System.Drawing.Point(145, 76);
		this.labC2H4.Name = "labC2H4";
		this.labC2H4.Size = new System.Drawing.Size(59, 20);
		this.labC2H4.TabIndex = 3;
		this.labC2H4.Text = "0    ";
		this.labC2H4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labC2H4.Click += new System.EventHandler(labComp2_Click);
		this.labC2H6.AutoSize = true;
		this.labC2H6.BackColor = System.Drawing.Color.Transparent;
		this.labC2H6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labC2H6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labC2H6.Location = new System.Drawing.Point(145, 105);
		this.labC2H6.Name = "labC2H6";
		this.labC2H6.Size = new System.Drawing.Size(59, 20);
		this.labC2H6.TabIndex = 4;
		this.labC2H6.Text = "0    ";
		this.labC2H6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labC2H6.Click += new System.EventHandler(labComp3_Click);
		this.labC2H2.AutoSize = true;
		this.labC2H2.BackColor = System.Drawing.Color.Transparent;
		this.labC2H2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labC2H2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labC2H2.Location = new System.Drawing.Point(145, 134);
		this.labC2H2.Name = "labC2H2";
		this.labC2H2.Size = new System.Drawing.Size(59, 20);
		this.labC2H2.TabIndex = 5;
		this.labC2H2.Text = "0    ";
		this.labC2H2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labC2H2.Click += new System.EventHandler(labComp4_Click);
		this.labC3H8.AutoSize = true;
		this.labC3H8.BackColor = System.Drawing.Color.Transparent;
		this.labC3H8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labC3H8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labC3H8.Location = new System.Drawing.Point(145, 163);
		this.labC3H8.Name = "labC3H8";
		this.labC3H8.Size = new System.Drawing.Size(59, 20);
		this.labC3H8.TabIndex = 6;
		this.labC3H8.Text = "0    ";
		this.labC3H8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labC3H8.Click += new System.EventHandler(labComp5_Click);
		this.labTHC.AutoSize = true;
		this.labTHC.BackColor = System.Drawing.Color.Transparent;
		this.labTHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labTHC.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labTHC.Location = new System.Drawing.Point(145, 418);
		this.labTHC.Name = "labTHC";
		this.labTHC.Size = new System.Drawing.Size(49, 20);
		this.labTHC.TabIndex = 7;
		this.labTHC.Text = "0.00";
		this.labTHC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labTHC.Click += new System.EventHandler(labCompSum_Click);
		this.labSignal.BackColor = System.Drawing.Color.Transparent;
		this.labSignal.Cursor = System.Windows.Forms.Cursors.Default;
		this.labSignal.Font = new System.Drawing.Font("宋体", 21.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labSignal.Location = new System.Drawing.Point(824, 136);
		this.labSignal.Name = "labSignal";
		this.labSignal.Size = new System.Drawing.Size(233, 58);
		this.labSignal.TabIndex = 8;
		this.labSignal.Text = "0.00";
		this.labSignal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.picBoxFire.BackColor = System.Drawing.Color.Transparent;
		this.picBoxFire.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.picBoxFire.Cursor = System.Windows.Forms.Cursors.Hand;
		this.picBoxFire.Image = IBrainChrom2018.Properties.Resources.着火;
		this.picBoxFire.Location = new System.Drawing.Point(1131, 136);
		this.picBoxFire.Name = "picBoxFire";
		this.picBoxFire.Size = new System.Drawing.Size(109, 125);
		this.picBoxFire.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picBoxFire.TabIndex = 9;
		this.picBoxFire.TabStop = false;
		this.picBoxFire.Click += new System.EventHandler(picBoxFire_Click);
		this.labCompName1.AutoSize = true;
		this.labCompName1.BackColor = System.Drawing.Color.Transparent;
		this.labCompName1.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName1.Location = new System.Drawing.Point(19, 47);
		this.labCompName1.Name = "labCompName1";
		this.labCompName1.Size = new System.Drawing.Size(69, 20);
		this.labCompName1.TabIndex = 10;
		this.labCompName1.Text = "     0";
		this.labCompName1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName2.AutoSize = true;
		this.labCompName2.BackColor = System.Drawing.Color.Transparent;
		this.labCompName2.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName2.Location = new System.Drawing.Point(20, 76);
		this.labCompName2.Name = "labCompName2";
		this.labCompName2.Size = new System.Drawing.Size(69, 20);
		this.labCompName2.TabIndex = 11;
		this.labCompName2.Text = "     0";
		this.labCompName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName3.AutoSize = true;
		this.labCompName3.BackColor = System.Drawing.Color.Transparent;
		this.labCompName3.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName3.Location = new System.Drawing.Point(20, 105);
		this.labCompName3.Name = "labCompName3";
		this.labCompName3.Size = new System.Drawing.Size(69, 20);
		this.labCompName3.TabIndex = 12;
		this.labCompName3.Text = "     0";
		this.labCompName3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName4.AutoSize = true;
		this.labCompName4.BackColor = System.Drawing.Color.Transparent;
		this.labCompName4.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName4.Location = new System.Drawing.Point(20, 134);
		this.labCompName4.Name = "labCompName4";
		this.labCompName4.Size = new System.Drawing.Size(69, 20);
		this.labCompName4.TabIndex = 13;
		this.labCompName4.Text = "     0";
		this.labCompName4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName5.AutoSize = true;
		this.labCompName5.BackColor = System.Drawing.Color.Transparent;
		this.labCompName5.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName5.Location = new System.Drawing.Point(20, 163);
		this.labCompName5.Name = "labCompName5";
		this.labCompName5.Size = new System.Drawing.Size(69, 20);
		this.labCompName5.TabIndex = 14;
		this.labCompName5.Text = "     0";
		this.labCompName5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompTotalName.AutoSize = true;
		this.labCompTotalName.BackColor = System.Drawing.Color.Transparent;
		this.labCompTotalName.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompTotalName.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompTotalName.Location = new System.Drawing.Point(17, 418);
		this.labCompTotalName.Name = "labCompTotalName";
		this.labCompTotalName.Size = new System.Drawing.Size(39, 20);
		this.labCompTotalName.TabIndex = 15;
		this.labCompTotalName.Text = "THC";
		this.labCompTotalName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labTitle.BackColor = System.Drawing.Color.Transparent;
		this.labTitle.Font = new System.Drawing.Font("宋体", 40f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labTitle.Location = new System.Drawing.Point(200, 53);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(881, 63);
		this.labTitle.TabIndex = 16;
		this.labTitle.Text = "空气液氧总烃在线色谱分析系统";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName10.AutoSize = true;
		this.labCompName10.BackColor = System.Drawing.Color.Transparent;
		this.labCompName10.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName10.Location = new System.Drawing.Point(21, 308);
		this.labCompName10.Name = "labCompName10";
		this.labCompName10.Size = new System.Drawing.Size(69, 20);
		this.labCompName10.TabIndex = 21;
		this.labCompName10.Text = "     0";
		this.labCompName10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName9.AutoSize = true;
		this.labCompName9.BackColor = System.Drawing.Color.Transparent;
		this.labCompName9.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName9.Location = new System.Drawing.Point(21, 279);
		this.labCompName9.Name = "labCompName9";
		this.labCompName9.Size = new System.Drawing.Size(69, 20);
		this.labCompName9.TabIndex = 20;
		this.labCompName9.Text = "     0";
		this.labCompName9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName8.AutoSize = true;
		this.labCompName8.BackColor = System.Drawing.Color.Transparent;
		this.labCompName8.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName8.Location = new System.Drawing.Point(21, 250);
		this.labCompName8.Name = "labCompName8";
		this.labCompName8.Size = new System.Drawing.Size(69, 20);
		this.labCompName8.TabIndex = 19;
		this.labCompName8.Text = "     0";
		this.labCompName8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName7.AutoSize = true;
		this.labCompName7.BackColor = System.Drawing.Color.Transparent;
		this.labCompName7.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName7.Location = new System.Drawing.Point(21, 221);
		this.labCompName7.Name = "labCompName7";
		this.labCompName7.Size = new System.Drawing.Size(69, 20);
		this.labCompName7.TabIndex = 18;
		this.labCompName7.Text = "     0";
		this.labCompName7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName6.AutoSize = true;
		this.labCompName6.BackColor = System.Drawing.Color.Transparent;
		this.labCompName6.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName6.Location = new System.Drawing.Point(20, 192);
		this.labCompName6.Name = "labCompName6";
		this.labCompName6.Size = new System.Drawing.Size(69, 20);
		this.labCompName6.TabIndex = 17;
		this.labCompName6.Text = "     0";
		this.labCompName6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName12.AutoSize = true;
		this.labCompName12.BackColor = System.Drawing.Color.Transparent;
		this.labCompName12.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName12.Location = new System.Drawing.Point(21, 366);
		this.labCompName12.Name = "labCompName12";
		this.labCompName12.Size = new System.Drawing.Size(69, 20);
		this.labCompName12.TabIndex = 23;
		this.labCompName12.Text = "     0";
		this.labCompName12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labCompName11.AutoSize = true;
		this.labCompName11.BackColor = System.Drawing.Color.Transparent;
		this.labCompName11.Cursor = System.Windows.Forms.Cursors.Default;
		this.labCompName11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCompName11.Location = new System.Drawing.Point(21, 337);
		this.labCompName11.Name = "labCompName11";
		this.labCompName11.Size = new System.Drawing.Size(69, 20);
		this.labCompName11.TabIndex = 22;
		this.labCompName11.Text = "     0";
		this.labCompName11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp10.AutoSize = true;
		this.labComp10.BackColor = System.Drawing.Color.Transparent;
		this.labComp10.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp10.Location = new System.Drawing.Point(145, 308);
		this.labComp10.Name = "labComp10";
		this.labComp10.Size = new System.Drawing.Size(59, 20);
		this.labComp10.TabIndex = 28;
		this.labComp10.Text = "0    ";
		this.labComp10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp10.Click += new System.EventHandler(labComp10_Click);
		this.labComp9.AutoSize = true;
		this.labComp9.BackColor = System.Drawing.Color.Transparent;
		this.labComp9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp9.Location = new System.Drawing.Point(145, 279);
		this.labComp9.Name = "labComp9";
		this.labComp9.Size = new System.Drawing.Size(59, 20);
		this.labComp9.TabIndex = 27;
		this.labComp9.Text = "0    ";
		this.labComp9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp9.Click += new System.EventHandler(labComp9_Click);
		this.labComp8.AutoSize = true;
		this.labComp8.BackColor = System.Drawing.Color.Transparent;
		this.labComp8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp8.Location = new System.Drawing.Point(145, 250);
		this.labComp8.Name = "labComp8";
		this.labComp8.Size = new System.Drawing.Size(59, 20);
		this.labComp8.TabIndex = 26;
		this.labComp8.Text = "0    ";
		this.labComp8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp8.Click += new System.EventHandler(labComp8_Click);
		this.labComp7.AutoSize = true;
		this.labComp7.BackColor = System.Drawing.Color.Transparent;
		this.labComp7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp7.Location = new System.Drawing.Point(145, 221);
		this.labComp7.Name = "labComp7";
		this.labComp7.Size = new System.Drawing.Size(59, 20);
		this.labComp7.TabIndex = 25;
		this.labComp7.Text = "0    ";
		this.labComp7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp7.Click += new System.EventHandler(labComp7_Click);
		this.labComp6.AutoSize = true;
		this.labComp6.BackColor = System.Drawing.Color.Transparent;
		this.labComp6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp6.Location = new System.Drawing.Point(145, 192);
		this.labComp6.Name = "labComp6";
		this.labComp6.Size = new System.Drawing.Size(59, 20);
		this.labComp6.TabIndex = 24;
		this.labComp6.Text = "0    ";
		this.labComp6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp6.Click += new System.EventHandler(labComp6_Click);
		this.labComp12.AutoSize = true;
		this.labComp12.BackColor = System.Drawing.Color.Transparent;
		this.labComp12.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp12.Location = new System.Drawing.Point(145, 366);
		this.labComp12.Name = "labComp12";
		this.labComp12.Size = new System.Drawing.Size(59, 20);
		this.labComp12.TabIndex = 30;
		this.labComp12.Text = "0    ";
		this.labComp12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp12.Click += new System.EventHandler(labComp12_Click);
		this.labComp11.AutoSize = true;
		this.labComp11.BackColor = System.Drawing.Color.Transparent;
		this.labComp11.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labComp11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labComp11.Location = new System.Drawing.Point(145, 337);
		this.labComp11.Name = "labComp11";
		this.labComp11.Size = new System.Drawing.Size(59, 20);
		this.labComp11.TabIndex = 29;
		this.labComp11.Text = "0    ";
		this.labComp11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labComp11.Click += new System.EventHandler(labComp11_Click);
		this.groupBox1.Controls.Add(this.labComp1);
		this.groupBox1.Controls.Add(this.labComp12);
		this.groupBox1.Controls.Add(this.labC2H4);
		this.groupBox1.Controls.Add(this.labComp11);
		this.groupBox1.Controls.Add(this.labC2H6);
		this.groupBox1.Controls.Add(this.labComp10);
		this.groupBox1.Controls.Add(this.labC2H2);
		this.groupBox1.Controls.Add(this.labComp9);
		this.groupBox1.Controls.Add(this.labC3H8);
		this.groupBox1.Controls.Add(this.labComp8);
		this.groupBox1.Controls.Add(this.labTHC);
		this.groupBox1.Controls.Add(this.labComp7);
		this.groupBox1.Controls.Add(this.labCompName1);
		this.groupBox1.Controls.Add(this.labComp6);
		this.groupBox1.Controls.Add(this.labCompName2);
		this.groupBox1.Controls.Add(this.labCompName12);
		this.groupBox1.Controls.Add(this.labCompName3);
		this.groupBox1.Controls.Add(this.labCompName11);
		this.groupBox1.Controls.Add(this.labCompName4);
		this.groupBox1.Controls.Add(this.labCompName10);
		this.groupBox1.Controls.Add(this.labCompName5);
		this.groupBox1.Controls.Add(this.labCompName9);
		this.groupBox1.Controls.Add(this.labCompTotalName);
		this.groupBox1.Controls.Add(this.labCompName8);
		this.groupBox1.Controls.Add(this.labCompName6);
		this.groupBox1.Controls.Add(this.labCompName7);
		this.groupBox1.Location = new System.Drawing.Point(34, 197);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(259, 458);
		this.groupBox1.TabIndex = 31;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "流路1";
		this.groupBox2.Controls.Add(this.lab2Comp1);
		this.groupBox2.Controls.Add(this.lab2Comp12);
		this.groupBox2.Controls.Add(this.lab2Comp2);
		this.groupBox2.Controls.Add(this.lab2Comp11);
		this.groupBox2.Controls.Add(this.lab2Comp3);
		this.groupBox2.Controls.Add(this.lab2Comp10);
		this.groupBox2.Controls.Add(this.lab2Comp4);
		this.groupBox2.Controls.Add(this.lab2Comp9);
		this.groupBox2.Controls.Add(this.lab2Comp5);
		this.groupBox2.Controls.Add(this.lab2Comp8);
		this.groupBox2.Controls.Add(this.labTHC2);
		this.groupBox2.Controls.Add(this.lab2Comp7);
		this.groupBox2.Controls.Add(this.lab2CompName1);
		this.groupBox2.Controls.Add(this.lab2Comp6);
		this.groupBox2.Controls.Add(this.lab2CompName2);
		this.groupBox2.Controls.Add(this.lab2CompName12);
		this.groupBox2.Controls.Add(this.lab2CompName3);
		this.groupBox2.Controls.Add(this.lab2CompName11);
		this.groupBox2.Controls.Add(this.lab2CompName4);
		this.groupBox2.Controls.Add(this.lab2CompName10);
		this.groupBox2.Controls.Add(this.lab2CompName5);
		this.groupBox2.Controls.Add(this.lab2CompName9);
		this.groupBox2.Controls.Add(this.label23);
		this.groupBox2.Controls.Add(this.lab2CompName8);
		this.groupBox2.Controls.Add(this.lab2CompName6);
		this.groupBox2.Controls.Add(this.lab2CompName7);
		this.groupBox2.Location = new System.Drawing.Point(299, 197);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(259, 458);
		this.groupBox2.TabIndex = 32;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "流路2";
		this.lab2Comp1.AutoSize = true;
		this.lab2Comp1.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp1.Location = new System.Drawing.Point(145, 47);
		this.lab2Comp1.Name = "lab2Comp1";
		this.lab2Comp1.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp1.TabIndex = 2;
		this.lab2Comp1.Text = "0    ";
		this.lab2Comp1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp1.Click += new System.EventHandler(lab2Comp1_Click);
		this.lab2Comp12.AutoSize = true;
		this.lab2Comp12.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp12.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp12.Location = new System.Drawing.Point(145, 366);
		this.lab2Comp12.Name = "lab2Comp12";
		this.lab2Comp12.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp12.TabIndex = 30;
		this.lab2Comp12.Text = "0    ";
		this.lab2Comp12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp12.Click += new System.EventHandler(lab2Comp12_Click);
		this.lab2Comp2.AutoSize = true;
		this.lab2Comp2.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp2.Location = new System.Drawing.Point(145, 76);
		this.lab2Comp2.Name = "lab2Comp2";
		this.lab2Comp2.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp2.TabIndex = 3;
		this.lab2Comp2.Text = "0    ";
		this.lab2Comp2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp2.Click += new System.EventHandler(lab2Comp2_Click);
		this.lab2Comp11.AutoSize = true;
		this.lab2Comp11.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp11.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp11.Location = new System.Drawing.Point(145, 337);
		this.lab2Comp11.Name = "lab2Comp11";
		this.lab2Comp11.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp11.TabIndex = 29;
		this.lab2Comp11.Text = "0    ";
		this.lab2Comp11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp11.Click += new System.EventHandler(lab2Comp11_Click);
		this.lab2Comp3.AutoSize = true;
		this.lab2Comp3.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp3.Location = new System.Drawing.Point(145, 105);
		this.lab2Comp3.Name = "lab2Comp3";
		this.lab2Comp3.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp3.TabIndex = 4;
		this.lab2Comp3.Text = "0    ";
		this.lab2Comp3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp3.Click += new System.EventHandler(lab2Comp3_Click);
		this.lab2Comp10.AutoSize = true;
		this.lab2Comp10.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp10.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp10.Location = new System.Drawing.Point(145, 308);
		this.lab2Comp10.Name = "lab2Comp10";
		this.lab2Comp10.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp10.TabIndex = 28;
		this.lab2Comp10.Text = "0    ";
		this.lab2Comp10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp10.Click += new System.EventHandler(lab2Comp10_Click);
		this.lab2Comp4.AutoSize = true;
		this.lab2Comp4.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp4.Location = new System.Drawing.Point(145, 134);
		this.lab2Comp4.Name = "lab2Comp4";
		this.lab2Comp4.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp4.TabIndex = 5;
		this.lab2Comp4.Text = "0    ";
		this.lab2Comp4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp4.Click += new System.EventHandler(lab2Comp4_Click);
		this.lab2Comp9.AutoSize = true;
		this.lab2Comp9.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp9.Location = new System.Drawing.Point(145, 279);
		this.lab2Comp9.Name = "lab2Comp9";
		this.lab2Comp9.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp9.TabIndex = 27;
		this.lab2Comp9.Text = "0    ";
		this.lab2Comp9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp9.Click += new System.EventHandler(lab2Comp9_Click);
		this.lab2Comp5.AutoSize = true;
		this.lab2Comp5.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp5.Location = new System.Drawing.Point(145, 163);
		this.lab2Comp5.Name = "lab2Comp5";
		this.lab2Comp5.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp5.TabIndex = 6;
		this.lab2Comp5.Text = "0    ";
		this.lab2Comp5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp5.Click += new System.EventHandler(lab2Comp5_Click);
		this.lab2Comp8.AutoSize = true;
		this.lab2Comp8.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp8.Location = new System.Drawing.Point(145, 250);
		this.lab2Comp8.Name = "lab2Comp8";
		this.lab2Comp8.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp8.TabIndex = 26;
		this.lab2Comp8.Text = "0    ";
		this.lab2Comp8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp8.Click += new System.EventHandler(lab2Comp8_Click);
		this.labTHC2.AutoSize = true;
		this.labTHC2.BackColor = System.Drawing.Color.Transparent;
		this.labTHC2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labTHC2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labTHC2.Location = new System.Drawing.Point(145, 418);
		this.labTHC2.Name = "labTHC2";
		this.labTHC2.Size = new System.Drawing.Size(49, 20);
		this.labTHC2.TabIndex = 7;
		this.labTHC2.Text = "0.00";
		this.labTHC2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp7.AutoSize = true;
		this.lab2Comp7.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp7.Location = new System.Drawing.Point(145, 221);
		this.lab2Comp7.Name = "lab2Comp7";
		this.lab2Comp7.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp7.TabIndex = 25;
		this.lab2Comp7.Text = "0    ";
		this.lab2Comp7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp7.Click += new System.EventHandler(lab2Comp7_Click);
		this.lab2CompName1.AutoSize = true;
		this.lab2CompName1.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName1.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName1.Location = new System.Drawing.Point(19, 47);
		this.lab2CompName1.Name = "lab2CompName1";
		this.lab2CompName1.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName1.TabIndex = 10;
		this.lab2CompName1.Text = "     0";
		this.lab2CompName1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp6.AutoSize = true;
		this.lab2Comp6.BackColor = System.Drawing.Color.Transparent;
		this.lab2Comp6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab2Comp6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2Comp6.Location = new System.Drawing.Point(145, 192);
		this.lab2Comp6.Name = "lab2Comp6";
		this.lab2Comp6.Size = new System.Drawing.Size(59, 20);
		this.lab2Comp6.TabIndex = 24;
		this.lab2Comp6.Text = "0    ";
		this.lab2Comp6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2Comp6.Click += new System.EventHandler(lab2Comp6_Click);
		this.lab2CompName2.AutoSize = true;
		this.lab2CompName2.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName2.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName2.Location = new System.Drawing.Point(20, 76);
		this.lab2CompName2.Name = "lab2CompName2";
		this.lab2CompName2.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName2.TabIndex = 11;
		this.lab2CompName2.Text = "     0";
		this.lab2CompName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName12.AutoSize = true;
		this.lab2CompName12.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName12.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName12.Location = new System.Drawing.Point(21, 366);
		this.lab2CompName12.Name = "lab2CompName12";
		this.lab2CompName12.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName12.TabIndex = 23;
		this.lab2CompName12.Text = "     0";
		this.lab2CompName12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName3.AutoSize = true;
		this.lab2CompName3.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName3.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName3.Location = new System.Drawing.Point(20, 105);
		this.lab2CompName3.Name = "lab2CompName3";
		this.lab2CompName3.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName3.TabIndex = 12;
		this.lab2CompName3.Text = "     0";
		this.lab2CompName3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName11.AutoSize = true;
		this.lab2CompName11.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName11.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName11.Location = new System.Drawing.Point(21, 337);
		this.lab2CompName11.Name = "lab2CompName11";
		this.lab2CompName11.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName11.TabIndex = 22;
		this.lab2CompName11.Text = "     0";
		this.lab2CompName11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName4.AutoSize = true;
		this.lab2CompName4.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName4.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName4.Location = new System.Drawing.Point(20, 134);
		this.lab2CompName4.Name = "lab2CompName4";
		this.lab2CompName4.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName4.TabIndex = 13;
		this.lab2CompName4.Text = "     0";
		this.lab2CompName4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName10.AutoSize = true;
		this.lab2CompName10.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName10.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName10.Location = new System.Drawing.Point(21, 308);
		this.lab2CompName10.Name = "lab2CompName10";
		this.lab2CompName10.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName10.TabIndex = 21;
		this.lab2CompName10.Text = "     0";
		this.lab2CompName10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName5.AutoSize = true;
		this.lab2CompName5.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName5.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName5.Location = new System.Drawing.Point(20, 163);
		this.lab2CompName5.Name = "lab2CompName5";
		this.lab2CompName5.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName5.TabIndex = 14;
		this.lab2CompName5.Text = "     0";
		this.lab2CompName5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName9.AutoSize = true;
		this.lab2CompName9.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName9.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName9.Location = new System.Drawing.Point(21, 279);
		this.lab2CompName9.Name = "lab2CompName9";
		this.lab2CompName9.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName9.TabIndex = 20;
		this.lab2CompName9.Text = "     0";
		this.lab2CompName9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label23.AutoSize = true;
		this.label23.BackColor = System.Drawing.Color.Transparent;
		this.label23.Cursor = System.Windows.Forms.Cursors.Default;
		this.label23.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label23.Location = new System.Drawing.Point(17, 418);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(39, 20);
		this.label23.TabIndex = 15;
		this.label23.Text = "THC";
		this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName8.AutoSize = true;
		this.lab2CompName8.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName8.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName8.Location = new System.Drawing.Point(21, 250);
		this.lab2CompName8.Name = "lab2CompName8";
		this.lab2CompName8.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName8.TabIndex = 19;
		this.lab2CompName8.Text = "     0";
		this.lab2CompName8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName6.AutoSize = true;
		this.lab2CompName6.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName6.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName6.Location = new System.Drawing.Point(20, 192);
		this.lab2CompName6.Name = "lab2CompName6";
		this.lab2CompName6.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName6.TabIndex = 17;
		this.lab2CompName6.Text = "     0";
		this.lab2CompName6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab2CompName7.AutoSize = true;
		this.lab2CompName7.BackColor = System.Drawing.Color.Transparent;
		this.lab2CompName7.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab2CompName7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab2CompName7.Location = new System.Drawing.Point(21, 221);
		this.lab2CompName7.Name = "lab2CompName7";
		this.lab2CompName7.Size = new System.Drawing.Size(69, 20);
		this.lab2CompName7.TabIndex = 18;
		this.lab2CompName7.Text = "     0";
		this.lab2CompName7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox3.Controls.Add(this.lab4Comp1);
		this.groupBox3.Controls.Add(this.lab4Comp12);
		this.groupBox3.Controls.Add(this.lab4Comp2);
		this.groupBox3.Controls.Add(this.lab4Comp11);
		this.groupBox3.Controls.Add(this.lab4Comp3);
		this.groupBox3.Controls.Add(this.lab4Comp10);
		this.groupBox3.Controls.Add(this.lab4Comp4);
		this.groupBox3.Controls.Add(this.lab4Comp9);
		this.groupBox3.Controls.Add(this.lab4Comp5);
		this.groupBox3.Controls.Add(this.lab4Comp8);
		this.groupBox3.Controls.Add(this.labTHC4);
		this.groupBox3.Controls.Add(this.lab4Comp7);
		this.groupBox3.Controls.Add(this.lab4CompName1);
		this.groupBox3.Controls.Add(this.lab4Comp6);
		this.groupBox3.Controls.Add(this.lab4CompName2);
		this.groupBox3.Controls.Add(this.lab4CompName12);
		this.groupBox3.Controls.Add(this.lab4CompName3);
		this.groupBox3.Controls.Add(this.lab4CompName11);
		this.groupBox3.Controls.Add(this.lab4CompName4);
		this.groupBox3.Controls.Add(this.lab4CompName10);
		this.groupBox3.Controls.Add(this.lab4CompName5);
		this.groupBox3.Controls.Add(this.lab4CompName9);
		this.groupBox3.Controls.Add(this.label49);
		this.groupBox3.Controls.Add(this.lab4CompName8);
		this.groupBox3.Controls.Add(this.lab4CompName6);
		this.groupBox3.Controls.Add(this.lab4CompName7);
		this.groupBox3.Location = new System.Drawing.Point(829, 197);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(259, 458);
		this.groupBox3.TabIndex = 34;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "流路4";
		this.lab4Comp1.AutoSize = true;
		this.lab4Comp1.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp1.Location = new System.Drawing.Point(145, 47);
		this.lab4Comp1.Name = "lab4Comp1";
		this.lab4Comp1.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp1.TabIndex = 2;
		this.lab4Comp1.Text = "0    ";
		this.lab4Comp1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp1.Click += new System.EventHandler(lab4Comp1_Click);
		this.lab4Comp12.AutoSize = true;
		this.lab4Comp12.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp12.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp12.Location = new System.Drawing.Point(145, 366);
		this.lab4Comp12.Name = "lab4Comp12";
		this.lab4Comp12.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp12.TabIndex = 30;
		this.lab4Comp12.Text = "0    ";
		this.lab4Comp12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp12.Click += new System.EventHandler(lab4Comp12_Click);
		this.lab4Comp2.AutoSize = true;
		this.lab4Comp2.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp2.Location = new System.Drawing.Point(145, 76);
		this.lab4Comp2.Name = "lab4Comp2";
		this.lab4Comp2.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp2.TabIndex = 3;
		this.lab4Comp2.Text = "0    ";
		this.lab4Comp2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp2.Click += new System.EventHandler(lab4Comp2_Click);
		this.lab4Comp11.AutoSize = true;
		this.lab4Comp11.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp11.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp11.Location = new System.Drawing.Point(145, 337);
		this.lab4Comp11.Name = "lab4Comp11";
		this.lab4Comp11.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp11.TabIndex = 29;
		this.lab4Comp11.Text = "0    ";
		this.lab4Comp11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp11.Click += new System.EventHandler(lab4Comp11_Click);
		this.lab4Comp3.AutoSize = true;
		this.lab4Comp3.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp3.Location = new System.Drawing.Point(145, 105);
		this.lab4Comp3.Name = "lab4Comp3";
		this.lab4Comp3.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp3.TabIndex = 4;
		this.lab4Comp3.Text = "0    ";
		this.lab4Comp3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp3.Click += new System.EventHandler(lab4Comp3_Click);
		this.lab4Comp10.AutoSize = true;
		this.lab4Comp10.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp10.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp10.Location = new System.Drawing.Point(145, 308);
		this.lab4Comp10.Name = "lab4Comp10";
		this.lab4Comp10.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp10.TabIndex = 28;
		this.lab4Comp10.Text = "0    ";
		this.lab4Comp10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp10.Click += new System.EventHandler(lab4Comp10_Click);
		this.lab4Comp4.AutoSize = true;
		this.lab4Comp4.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp4.Location = new System.Drawing.Point(145, 134);
		this.lab4Comp4.Name = "lab4Comp4";
		this.lab4Comp4.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp4.TabIndex = 5;
		this.lab4Comp4.Text = "0    ";
		this.lab4Comp4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp4.Click += new System.EventHandler(lab4Comp4_Click);
		this.lab4Comp9.AutoSize = true;
		this.lab4Comp9.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp9.Location = new System.Drawing.Point(145, 279);
		this.lab4Comp9.Name = "lab4Comp9";
		this.lab4Comp9.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp9.TabIndex = 27;
		this.lab4Comp9.Text = "0    ";
		this.lab4Comp9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp9.Click += new System.EventHandler(lab4Comp9_Click);
		this.lab4Comp5.AutoSize = true;
		this.lab4Comp5.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp5.Location = new System.Drawing.Point(145, 163);
		this.lab4Comp5.Name = "lab4Comp5";
		this.lab4Comp5.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp5.TabIndex = 6;
		this.lab4Comp5.Text = "0    ";
		this.lab4Comp5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp5.Click += new System.EventHandler(lab4Comp5_Click);
		this.lab4Comp8.AutoSize = true;
		this.lab4Comp8.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp8.Location = new System.Drawing.Point(145, 250);
		this.lab4Comp8.Name = "lab4Comp8";
		this.lab4Comp8.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp8.TabIndex = 26;
		this.lab4Comp8.Text = "0    ";
		this.lab4Comp8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp8.Click += new System.EventHandler(lab4Comp8_Click);
		this.labTHC4.AutoSize = true;
		this.labTHC4.BackColor = System.Drawing.Color.Transparent;
		this.labTHC4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labTHC4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labTHC4.Location = new System.Drawing.Point(145, 418);
		this.labTHC4.Name = "labTHC4";
		this.labTHC4.Size = new System.Drawing.Size(49, 20);
		this.labTHC4.TabIndex = 7;
		this.labTHC4.Text = "0.00";
		this.labTHC4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp7.AutoSize = true;
		this.lab4Comp7.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp7.Location = new System.Drawing.Point(145, 221);
		this.lab4Comp7.Name = "lab4Comp7";
		this.lab4Comp7.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp7.TabIndex = 25;
		this.lab4Comp7.Text = "0    ";
		this.lab4Comp7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp7.Click += new System.EventHandler(lab4Comp7_Click);
		this.lab4CompName1.AutoSize = true;
		this.lab4CompName1.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName1.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName1.Location = new System.Drawing.Point(19, 47);
		this.lab4CompName1.Name = "lab4CompName1";
		this.lab4CompName1.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName1.TabIndex = 10;
		this.lab4CompName1.Text = "     0";
		this.lab4CompName1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp6.AutoSize = true;
		this.lab4Comp6.BackColor = System.Drawing.Color.Transparent;
		this.lab4Comp6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab4Comp6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4Comp6.Location = new System.Drawing.Point(145, 192);
		this.lab4Comp6.Name = "lab4Comp6";
		this.lab4Comp6.Size = new System.Drawing.Size(59, 20);
		this.lab4Comp6.TabIndex = 24;
		this.lab4Comp6.Text = "0    ";
		this.lab4Comp6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4Comp6.Click += new System.EventHandler(lab4Comp6_Click);
		this.lab4CompName2.AutoSize = true;
		this.lab4CompName2.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName2.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName2.Location = new System.Drawing.Point(20, 76);
		this.lab4CompName2.Name = "lab4CompName2";
		this.lab4CompName2.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName2.TabIndex = 11;
		this.lab4CompName2.Text = "     0";
		this.lab4CompName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName12.AutoSize = true;
		this.lab4CompName12.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName12.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName12.Location = new System.Drawing.Point(21, 366);
		this.lab4CompName12.Name = "lab4CompName12";
		this.lab4CompName12.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName12.TabIndex = 23;
		this.lab4CompName12.Text = "     0";
		this.lab4CompName12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName3.AutoSize = true;
		this.lab4CompName3.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName3.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName3.Location = new System.Drawing.Point(20, 105);
		this.lab4CompName3.Name = "lab4CompName3";
		this.lab4CompName3.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName3.TabIndex = 12;
		this.lab4CompName3.Text = "     0";
		this.lab4CompName3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName11.AutoSize = true;
		this.lab4CompName11.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName11.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName11.Location = new System.Drawing.Point(21, 337);
		this.lab4CompName11.Name = "lab4CompName11";
		this.lab4CompName11.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName11.TabIndex = 22;
		this.lab4CompName11.Text = "     0";
		this.lab4CompName11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName4.AutoSize = true;
		this.lab4CompName4.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName4.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName4.Location = new System.Drawing.Point(20, 134);
		this.lab4CompName4.Name = "lab4CompName4";
		this.lab4CompName4.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName4.TabIndex = 13;
		this.lab4CompName4.Text = "     0";
		this.lab4CompName4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName10.AutoSize = true;
		this.lab4CompName10.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName10.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName10.Location = new System.Drawing.Point(21, 308);
		this.lab4CompName10.Name = "lab4CompName10";
		this.lab4CompName10.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName10.TabIndex = 21;
		this.lab4CompName10.Text = "     0";
		this.lab4CompName10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName5.AutoSize = true;
		this.lab4CompName5.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName5.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName5.Location = new System.Drawing.Point(20, 163);
		this.lab4CompName5.Name = "lab4CompName5";
		this.lab4CompName5.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName5.TabIndex = 14;
		this.lab4CompName5.Text = "     0";
		this.lab4CompName5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName9.AutoSize = true;
		this.lab4CompName9.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName9.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName9.Location = new System.Drawing.Point(21, 279);
		this.lab4CompName9.Name = "lab4CompName9";
		this.lab4CompName9.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName9.TabIndex = 20;
		this.lab4CompName9.Text = "     0";
		this.lab4CompName9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label49.AutoSize = true;
		this.label49.BackColor = System.Drawing.Color.Transparent;
		this.label49.Cursor = System.Windows.Forms.Cursors.Default;
		this.label49.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label49.Location = new System.Drawing.Point(17, 418);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(39, 20);
		this.label49.TabIndex = 15;
		this.label49.Text = "THC";
		this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName8.AutoSize = true;
		this.lab4CompName8.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName8.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName8.Location = new System.Drawing.Point(21, 250);
		this.lab4CompName8.Name = "lab4CompName8";
		this.lab4CompName8.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName8.TabIndex = 19;
		this.lab4CompName8.Text = "     0";
		this.lab4CompName8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName6.AutoSize = true;
		this.lab4CompName6.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName6.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName6.Location = new System.Drawing.Point(20, 192);
		this.lab4CompName6.Name = "lab4CompName6";
		this.lab4CompName6.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName6.TabIndex = 17;
		this.lab4CompName6.Text = "     0";
		this.lab4CompName6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab4CompName7.AutoSize = true;
		this.lab4CompName7.BackColor = System.Drawing.Color.Transparent;
		this.lab4CompName7.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab4CompName7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab4CompName7.Location = new System.Drawing.Point(21, 221);
		this.lab4CompName7.Name = "lab4CompName7";
		this.lab4CompName7.Size = new System.Drawing.Size(69, 20);
		this.lab4CompName7.TabIndex = 18;
		this.lab4CompName7.Text = "     0";
		this.lab4CompName7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox4.Controls.Add(this.lab3Comp1);
		this.groupBox4.Controls.Add(this.lab3Comp12);
		this.groupBox4.Controls.Add(this.lab3Comp2);
		this.groupBox4.Controls.Add(this.lab3Comp11);
		this.groupBox4.Controls.Add(this.lab3Comp3);
		this.groupBox4.Controls.Add(this.lab3Comp10);
		this.groupBox4.Controls.Add(this.lab3Comp4);
		this.groupBox4.Controls.Add(this.lab3Comp9);
		this.groupBox4.Controls.Add(this.lab3Comp5);
		this.groupBox4.Controls.Add(this.lab3Comp8);
		this.groupBox4.Controls.Add(this.labTHC3);
		this.groupBox4.Controls.Add(this.lab3Comp7);
		this.groupBox4.Controls.Add(this.lab3CompName1);
		this.groupBox4.Controls.Add(this.lab3Comp6);
		this.groupBox4.Controls.Add(this.lab3CompName2);
		this.groupBox4.Controls.Add(this.lab3CompName12);
		this.groupBox4.Controls.Add(this.lab3CompName3);
		this.groupBox4.Controls.Add(this.lab3CompName11);
		this.groupBox4.Controls.Add(this.lab3CompName4);
		this.groupBox4.Controls.Add(this.lab3CompName10);
		this.groupBox4.Controls.Add(this.lab3CompName5);
		this.groupBox4.Controls.Add(this.lab3CompName9);
		this.groupBox4.Controls.Add(this.label75);
		this.groupBox4.Controls.Add(this.lab3CompName8);
		this.groupBox4.Controls.Add(this.lab3CompName6);
		this.groupBox4.Controls.Add(this.lab3CompName7);
		this.groupBox4.Location = new System.Drawing.Point(564, 197);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(259, 458);
		this.groupBox4.TabIndex = 33;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "流路3";
		this.lab3Comp1.AutoSize = true;
		this.lab3Comp1.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp1.Location = new System.Drawing.Point(145, 47);
		this.lab3Comp1.Name = "lab3Comp1";
		this.lab3Comp1.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp1.TabIndex = 2;
		this.lab3Comp1.Text = "0    ";
		this.lab3Comp1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp1.Click += new System.EventHandler(lab3Comp1_Click);
		this.lab3Comp12.AutoSize = true;
		this.lab3Comp12.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp12.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp12.Location = new System.Drawing.Point(145, 366);
		this.lab3Comp12.Name = "lab3Comp12";
		this.lab3Comp12.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp12.TabIndex = 30;
		this.lab3Comp12.Text = "0    ";
		this.lab3Comp12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp12.Click += new System.EventHandler(lab3Comp12_Click);
		this.lab3Comp2.AutoSize = true;
		this.lab3Comp2.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp2.Location = new System.Drawing.Point(145, 76);
		this.lab3Comp2.Name = "lab3Comp2";
		this.lab3Comp2.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp2.TabIndex = 3;
		this.lab3Comp2.Text = "0    ";
		this.lab3Comp2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp2.Click += new System.EventHandler(lab3Comp2_Click);
		this.lab3Comp11.AutoSize = true;
		this.lab3Comp11.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp11.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp11.Location = new System.Drawing.Point(145, 337);
		this.lab3Comp11.Name = "lab3Comp11";
		this.lab3Comp11.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp11.TabIndex = 29;
		this.lab3Comp11.Text = "0    ";
		this.lab3Comp11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp11.Click += new System.EventHandler(lab3Comp11_Click);
		this.lab3Comp3.AutoSize = true;
		this.lab3Comp3.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp3.Location = new System.Drawing.Point(145, 105);
		this.lab3Comp3.Name = "lab3Comp3";
		this.lab3Comp3.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp3.TabIndex = 4;
		this.lab3Comp3.Text = "0    ";
		this.lab3Comp3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp3.Click += new System.EventHandler(lab3Comp3_Click);
		this.lab3Comp10.AutoSize = true;
		this.lab3Comp10.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp10.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp10.Location = new System.Drawing.Point(145, 308);
		this.lab3Comp10.Name = "lab3Comp10";
		this.lab3Comp10.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp10.TabIndex = 28;
		this.lab3Comp10.Text = "0    ";
		this.lab3Comp10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp10.Click += new System.EventHandler(lab3Comp10_Click);
		this.lab3Comp4.AutoSize = true;
		this.lab3Comp4.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp4.Location = new System.Drawing.Point(145, 134);
		this.lab3Comp4.Name = "lab3Comp4";
		this.lab3Comp4.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp4.TabIndex = 5;
		this.lab3Comp4.Text = "0    ";
		this.lab3Comp4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp4.Click += new System.EventHandler(lab3Comp4_Click);
		this.lab3Comp9.AutoSize = true;
		this.lab3Comp9.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp9.Location = new System.Drawing.Point(145, 279);
		this.lab3Comp9.Name = "lab3Comp9";
		this.lab3Comp9.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp9.TabIndex = 27;
		this.lab3Comp9.Text = "0    ";
		this.lab3Comp9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp9.Click += new System.EventHandler(lab3Comp9_Click);
		this.lab3Comp5.AutoSize = true;
		this.lab3Comp5.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp5.Location = new System.Drawing.Point(145, 163);
		this.lab3Comp5.Name = "lab3Comp5";
		this.lab3Comp5.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp5.TabIndex = 6;
		this.lab3Comp5.Text = "0    ";
		this.lab3Comp5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp5.Click += new System.EventHandler(lab3Comp5_Click);
		this.lab3Comp8.AutoSize = true;
		this.lab3Comp8.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp8.Location = new System.Drawing.Point(145, 250);
		this.lab3Comp8.Name = "lab3Comp8";
		this.lab3Comp8.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp8.TabIndex = 26;
		this.lab3Comp8.Text = "0    ";
		this.lab3Comp8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp8.Click += new System.EventHandler(lab3Comp8_Click);
		this.labTHC3.AutoSize = true;
		this.labTHC3.BackColor = System.Drawing.Color.Transparent;
		this.labTHC3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labTHC3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labTHC3.Location = new System.Drawing.Point(145, 418);
		this.labTHC3.Name = "labTHC3";
		this.labTHC3.Size = new System.Drawing.Size(49, 20);
		this.labTHC3.TabIndex = 7;
		this.labTHC3.Text = "0.00";
		this.labTHC3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp7.AutoSize = true;
		this.lab3Comp7.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp7.Location = new System.Drawing.Point(145, 221);
		this.lab3Comp7.Name = "lab3Comp7";
		this.lab3Comp7.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp7.TabIndex = 25;
		this.lab3Comp7.Text = "0    ";
		this.lab3Comp7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp7.Click += new System.EventHandler(lab3Comp7_Click);
		this.lab3CompName1.AutoSize = true;
		this.lab3CompName1.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName1.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName1.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName1.Location = new System.Drawing.Point(19, 47);
		this.lab3CompName1.Name = "lab3CompName1";
		this.lab3CompName1.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName1.TabIndex = 10;
		this.lab3CompName1.Text = "     0";
		this.lab3CompName1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp6.AutoSize = true;
		this.lab3Comp6.BackColor = System.Drawing.Color.Transparent;
		this.lab3Comp6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lab3Comp6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3Comp6.Location = new System.Drawing.Point(145, 192);
		this.lab3Comp6.Name = "lab3Comp6";
		this.lab3Comp6.Size = new System.Drawing.Size(59, 20);
		this.lab3Comp6.TabIndex = 24;
		this.lab3Comp6.Text = "0    ";
		this.lab3Comp6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3Comp6.Click += new System.EventHandler(lab3Comp6_Click);
		this.lab3CompName2.AutoSize = true;
		this.lab3CompName2.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName2.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName2.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName2.Location = new System.Drawing.Point(20, 76);
		this.lab3CompName2.Name = "lab3CompName2";
		this.lab3CompName2.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName2.TabIndex = 11;
		this.lab3CompName2.Text = "     0";
		this.lab3CompName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName12.AutoSize = true;
		this.lab3CompName12.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName12.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName12.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName12.Location = new System.Drawing.Point(21, 366);
		this.lab3CompName12.Name = "lab3CompName12";
		this.lab3CompName12.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName12.TabIndex = 23;
		this.lab3CompName12.Text = "     0";
		this.lab3CompName12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName3.AutoSize = true;
		this.lab3CompName3.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName3.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName3.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName3.Location = new System.Drawing.Point(20, 105);
		this.lab3CompName3.Name = "lab3CompName3";
		this.lab3CompName3.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName3.TabIndex = 12;
		this.lab3CompName3.Text = "     0";
		this.lab3CompName3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName11.AutoSize = true;
		this.lab3CompName11.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName11.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName11.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName11.Location = new System.Drawing.Point(21, 337);
		this.lab3CompName11.Name = "lab3CompName11";
		this.lab3CompName11.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName11.TabIndex = 22;
		this.lab3CompName11.Text = "     0";
		this.lab3CompName11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName4.AutoSize = true;
		this.lab3CompName4.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName4.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName4.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName4.Location = new System.Drawing.Point(20, 134);
		this.lab3CompName4.Name = "lab3CompName4";
		this.lab3CompName4.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName4.TabIndex = 13;
		this.lab3CompName4.Text = "     0";
		this.lab3CompName4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName10.AutoSize = true;
		this.lab3CompName10.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName10.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName10.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName10.Location = new System.Drawing.Point(21, 308);
		this.lab3CompName10.Name = "lab3CompName10";
		this.lab3CompName10.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName10.TabIndex = 21;
		this.lab3CompName10.Text = "     0";
		this.lab3CompName10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName5.AutoSize = true;
		this.lab3CompName5.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName5.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName5.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName5.Location = new System.Drawing.Point(20, 163);
		this.lab3CompName5.Name = "lab3CompName5";
		this.lab3CompName5.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName5.TabIndex = 14;
		this.lab3CompName5.Text = "     0";
		this.lab3CompName5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName9.AutoSize = true;
		this.lab3CompName9.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName9.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName9.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName9.Location = new System.Drawing.Point(21, 279);
		this.lab3CompName9.Name = "lab3CompName9";
		this.lab3CompName9.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName9.TabIndex = 20;
		this.lab3CompName9.Text = "     0";
		this.lab3CompName9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label75.AutoSize = true;
		this.label75.BackColor = System.Drawing.Color.Transparent;
		this.label75.Cursor = System.Windows.Forms.Cursors.Default;
		this.label75.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label75.Location = new System.Drawing.Point(17, 418);
		this.label75.Name = "label75";
		this.label75.Size = new System.Drawing.Size(39, 20);
		this.label75.TabIndex = 15;
		this.label75.Text = "THC";
		this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName8.AutoSize = true;
		this.lab3CompName8.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName8.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName8.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName8.Location = new System.Drawing.Point(21, 250);
		this.lab3CompName8.Name = "lab3CompName8";
		this.lab3CompName8.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName8.TabIndex = 19;
		this.lab3CompName8.Text = "     0";
		this.lab3CompName8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName6.AutoSize = true;
		this.lab3CompName6.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName6.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName6.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName6.Location = new System.Drawing.Point(20, 192);
		this.lab3CompName6.Name = "lab3CompName6";
		this.lab3CompName6.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName6.TabIndex = 17;
		this.lab3CompName6.Text = "     0";
		this.lab3CompName6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lab3CompName7.AutoSize = true;
		this.lab3CompName7.BackColor = System.Drawing.Color.Transparent;
		this.lab3CompName7.Cursor = System.Windows.Forms.Cursors.Default;
		this.lab3CompName7.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lab3CompName7.Location = new System.Drawing.Point(21, 221);
		this.lab3CompName7.Name = "lab3CompName7";
		this.lab3CompName7.Size = new System.Drawing.Size(69, 20);
		this.lab3CompName7.TabIndex = 18;
		this.lab3CompName7.Text = "     0";
		this.lab3CompName7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.浓度.AutoSize = true;
		this.浓度.Font = new System.Drawing.Font("宋体", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.浓度.Location = new System.Drawing.Point(57, 696);
		this.浓度.Name = "浓度";
		this.浓度.Size = new System.Drawing.Size(62, 18);
		this.浓度.TabIndex = 35;
		this.浓度.Text = "浓度：";
		this.labAmount.AutoSize = true;
		this.labAmount.Font = new System.Drawing.Font("宋体", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labAmount.Location = new System.Drawing.Point(107, 697);
		this.labAmount.Name = "labAmount";
		this.labAmount.Size = new System.Drawing.Size(17, 18);
		this.labAmount.TabIndex = 36;
		this.labAmount.Text = "0";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new System.Drawing.Size(1280, 800);
		base.Controls.Add(this.labAmount);
		base.Controls.Add(this.浓度);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.labTitle);
		base.Controls.Add(this.picBoxFire);
		base.Controls.Add(this.labSignal);
		base.Controls.Add(this.btnSetPara);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormKR";
		this.Text = "FormKR";
		((System.ComponentModel.ISupportInitialize)this.picBoxFire).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
