using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormSGSet : Form
{
	private MineParam mineParam = MineParam.Create();

	private IContainer components = null;

	private Panel panel1;

	private TextBox tbAnalyzeTime;

	private Label label4;

	private TextBox tbInjQTime;

	private Label label3;

	private TextBox tbCycleQtime;

	private Label label2;

	private TextBox tbCycles;

	private Label label1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TextBox tbChannelName15;

	private TextBox tbChannelName14;

	private TextBox tbChannelName13;

	private TextBox tbChannelName12;

	private TextBox tbChannelName11;

	private TextBox tbChannelName10;

	private TextBox tbChannelName9;

	private TextBox tbChannelName8;

	private TextBox tbChannelName7;

	private TextBox tbChannelName6;

	private TextBox tbChannelName5;

	private TextBox tbChannelName4;

	private TextBox tbChannelName3;

	private TextBox tbChannelName2;

	private TextBox tbChannelName1;

	private CheckBox cbENSG15;

	private CheckBox cbENSG14;

	private CheckBox cbENSG13;

	private CheckBox cbENSG12;

	private CheckBox cbENSG11;

	private CheckBox cbENSG10;

	private CheckBox cbENSG9;

	private CheckBox cbENSG8;

	private CheckBox cbENSG7;

	private CheckBox cbENSG6;

	private CheckBox cbENSG5;

	private CheckBox cbENSG4;

	private CheckBox cbENSG3;

	private CheckBox cbENSG2;

	private CheckBox cbENSG1;

	private TextBox tbChannelName30;

	private TextBox tbChannelName29;

	private TextBox tbChannelName28;

	private TextBox tbChannelName27;

	private TextBox tbChannelName26;

	private TextBox tbChannelName25;

	private TextBox tbChannelName24;

	private TextBox tbChannelName23;

	private TextBox tbChannelName22;

	private TextBox tbChannelName21;

	private TextBox tbChannelName20;

	private TextBox tbChannelName19;

	private TextBox tbChannelName18;

	private TextBox tbChannelName17;

	private TextBox tbChannelName16;

	private CheckBox cbENSG30;

	private CheckBox cbENSG29;

	private CheckBox cbENSG28;

	private CheckBox cbENSG27;

	private CheckBox cbENSG26;

	private CheckBox cbENSG25;

	private CheckBox cbENSG24;

	private CheckBox cbENSG23;

	private CheckBox cbENSG22;

	private CheckBox cbENSG21;

	private CheckBox cbENSG20;

	private CheckBox cbENSG19;

	private CheckBox cbENSG18;

	private CheckBox cbENSG17;

	private CheckBox cbENSG16;

	private TextBox tbChannelName60;

	private TextBox tbChannelName59;

	private TextBox tbChannelName58;

	private TextBox tbChannelName57;

	private TextBox tbChannelName56;

	private TextBox tbChannelName55;

	private TextBox tbChannelName54;

	private TextBox tbChannelName53;

	private TextBox tbChannelName52;

	private TextBox tbChannelName51;

	private TextBox tbChannelName50;

	private TextBox tbChannelName49;

	private TextBox tbChannelName48;

	private TextBox tbChannelName47;

	private TextBox tbChannelName46;

	private CheckBox cbENSG60;

	private CheckBox cbENSG59;

	private CheckBox cbENSG58;

	private CheckBox cbENSG57;

	private CheckBox cbENSG56;

	private CheckBox cbENSG55;

	private CheckBox cbENSG54;

	private CheckBox cbENSG53;

	private CheckBox cbENSG52;

	private CheckBox cbENSG51;

	private CheckBox cbENSG50;

	private CheckBox cbENSG49;

	private CheckBox cbENSG48;

	private CheckBox cbENSG47;

	private CheckBox cbENSG46;

	private TextBox tbChannelName45;

	private TextBox tbChannelName44;

	private TextBox tbChannelName43;

	private TextBox tbChannelName42;

	private TextBox tbChannelName41;

	private TextBox tbChannelName40;

	private TextBox tbChannelName39;

	private TextBox tbChannelName38;

	private TextBox tbChannelName37;

	private TextBox tbChannelName36;

	private TextBox tbChannelName35;

	private TextBox tbChannelName34;

	private TextBox tbChannelName33;

	private TextBox tbChannelName32;

	private TextBox tbChannelName31;

	private CheckBox cbENSG45;

	private CheckBox cbENSG44;

	private CheckBox cbENSG43;

	private CheckBox cbENSG42;

	private CheckBox cbENSG41;

	private CheckBox cbENSG40;

	private CheckBox cbENSG39;

	private CheckBox cbENSG38;

	private CheckBox cbENSG37;

	private CheckBox cbENSG36;

	private CheckBox cbENSG35;

	private CheckBox cbENSG34;

	private CheckBox cbENSG33;

	private CheckBox cbENSG32;

	private CheckBox cbENSG31;

	private Button btnSGset;

	public FormSGSet()
	{
		InitializeComponent();
		InitForm();
	}

	public void InitForm()
	{
		mineParam.LoadParam();
		tbCycles.Text = mineParam.tbCycles.ToString();
		tbInjQTime.Text = mineParam.tbInjQTime.ToString("0.0");
		tbCycleQtime.Text = mineParam.tbCycleQtime.ToString("0.0");
		tbAnalyzeTime.Text = mineParam.tbAnalyzeTime.ToString("0.0");
		if ((mineParam.enSG & 1) == 1)
		{
			cbENSG1.Checked = true;
		}
		else
		{
			cbENSG1.Checked = false;
		}
		if ((mineParam.enSG & 2) == 2)
		{
			cbENSG2.Checked = true;
		}
		else
		{
			cbENSG2.Checked = false;
		}
		if ((mineParam.enSG & 4) == 4)
		{
			cbENSG3.Checked = true;
		}
		else
		{
			cbENSG3.Checked = false;
		}
		if ((mineParam.enSG & 8) == 8)
		{
			cbENSG4.Checked = true;
		}
		else
		{
			cbENSG4.Checked = false;
		}
		if ((mineParam.enSG & 0x10) == 16)
		{
			cbENSG5.Checked = true;
		}
		else
		{
			cbENSG5.Checked = false;
		}
		if ((mineParam.enSG & 0x20) == 32)
		{
			cbENSG6.Checked = true;
		}
		else
		{
			cbENSG6.Checked = false;
		}
		if ((mineParam.enSG & 0x40) == 64)
		{
			cbENSG7.Checked = true;
		}
		else
		{
			cbENSG7.Checked = false;
		}
		if ((mineParam.enSG & 0x80) == 128)
		{
			cbENSG8.Checked = true;
		}
		else
		{
			cbENSG8.Checked = false;
		}
		if ((mineParam.enSG & 0x100) == 256)
		{
			cbENSG9.Checked = true;
		}
		else
		{
			cbENSG9.Checked = false;
		}
		if ((mineParam.enSG & 0x200) == 512)
		{
			cbENSG10.Checked = true;
		}
		else
		{
			cbENSG10.Checked = false;
		}
		if ((mineParam.enSG & 0x400) == 1024)
		{
			cbENSG11.Checked = true;
		}
		else
		{
			cbENSG11.Checked = false;
		}
		if ((mineParam.enSG & 0x800) == 2048)
		{
			cbENSG12.Checked = true;
		}
		else
		{
			cbENSG12.Checked = false;
		}
		if ((mineParam.enSG & 0x1000) == 4096)
		{
			cbENSG13.Checked = true;
		}
		else
		{
			cbENSG13.Checked = false;
		}
		if ((mineParam.enSG & 0x2000) == 8192)
		{
			cbENSG14.Checked = true;
		}
		else
		{
			cbENSG14.Checked = false;
		}
		if ((mineParam.enSG & 0x4000) == 16384)
		{
			cbENSG15.Checked = true;
		}
		else
		{
			cbENSG15.Checked = false;
		}
		if ((mineParam.enSG & 0x8000) == 32768)
		{
			cbENSG16.Checked = true;
		}
		else
		{
			cbENSG16.Checked = false;
		}
		if ((mineParam.enSG & 0x10000) == 65536)
		{
			cbENSG17.Checked = true;
		}
		else
		{
			cbENSG17.Checked = false;
		}
		if ((mineParam.enSG & 0x20000) == 131072)
		{
			cbENSG18.Checked = true;
		}
		else
		{
			cbENSG18.Checked = false;
		}
		if ((mineParam.enSG & 0x40000) == 262144)
		{
			cbENSG19.Checked = true;
		}
		else
		{
			cbENSG19.Checked = false;
		}
		if ((mineParam.enSG & 0x80000) == 524288)
		{
			cbENSG20.Checked = true;
		}
		else
		{
			cbENSG20.Checked = false;
		}
		if ((mineParam.enSG & 0x100000) == 1048576)
		{
			cbENSG21.Checked = true;
		}
		else
		{
			cbENSG21.Checked = false;
		}
		if ((mineParam.enSG & 0x200000) == 2097152)
		{
			cbENSG22.Checked = true;
		}
		else
		{
			cbENSG22.Checked = false;
		}
		if ((mineParam.enSG & 0x400000) == 4194304)
		{
			cbENSG23.Checked = true;
		}
		else
		{
			cbENSG23.Checked = false;
		}
		if ((mineParam.enSG & 0x800000) == 8388608)
		{
			cbENSG24.Checked = true;
		}
		else
		{
			cbENSG24.Checked = false;
		}
		if ((mineParam.enSG & 0x1000000) == 16777216)
		{
			cbENSG25.Checked = true;
		}
		else
		{
			cbENSG25.Checked = false;
		}
		if ((mineParam.enSG & 0x2000000) == 33554432)
		{
			cbENSG26.Checked = true;
		}
		else
		{
			cbENSG26.Checked = false;
		}
		if ((mineParam.enSG & 0x4000000) == 67108864)
		{
			cbENSG27.Checked = true;
		}
		else
		{
			cbENSG27.Checked = false;
		}
		if ((mineParam.enSG & 0x8000000) == 134217728)
		{
			cbENSG28.Checked = true;
		}
		else
		{
			cbENSG28.Checked = false;
		}
		if ((mineParam.enSG & 0x10000000) == 268435456)
		{
			cbENSG29.Checked = true;
		}
		else
		{
			cbENSG29.Checked = false;
		}
		if ((mineParam.enSG & 0x20000000) == 536870912)
		{
			cbENSG30.Checked = true;
		}
		else
		{
			cbENSG30.Checked = false;
		}
		if ((mineParam.enSG & 0x40000000) == 1073741824)
		{
			cbENSG31.Checked = true;
		}
		else
		{
			cbENSG31.Checked = false;
		}
		if ((mineParam.enSG & 0x80000000u) == 2147483648u)
		{
			cbENSG32.Checked = true;
		}
		else
		{
			cbENSG32.Checked = false;
		}
		if ((mineParam.enSG & 0x100000000L) == 4294967296L)
		{
			cbENSG33.Checked = true;
		}
		else
		{
			cbENSG33.Checked = false;
		}
		if ((mineParam.enSG & 0x200000000L) == 8589934592L)
		{
			cbENSG34.Checked = true;
		}
		else
		{
			cbENSG34.Checked = false;
		}
		if ((mineParam.enSG & 0x400000000L) == 17179869184L)
		{
			cbENSG35.Checked = true;
		}
		else
		{
			cbENSG35.Checked = false;
		}
		if ((mineParam.enSG & 0x800000000L) == 34359738368L)
		{
			cbENSG36.Checked = true;
		}
		else
		{
			cbENSG36.Checked = false;
		}
		if ((mineParam.enSG & 0x1000000000L) == 68719476736L)
		{
			cbENSG37.Checked = true;
		}
		else
		{
			cbENSG37.Checked = false;
		}
		if ((mineParam.enSG & 0x2000000000L) == 137438953472L)
		{
			cbENSG38.Checked = true;
		}
		else
		{
			cbENSG38.Checked = false;
		}
		if ((mineParam.enSG & 0x4000000000L) == 274877906944L)
		{
			cbENSG39.Checked = true;
		}
		else
		{
			cbENSG39.Checked = false;
		}
		if ((mineParam.enSG & 0x8000000000L) == 549755813888L)
		{
			cbENSG40.Checked = true;
		}
		else
		{
			cbENSG40.Checked = false;
		}
		if ((mineParam.enSG & 0x10000000000L) == 1099511627776L)
		{
			cbENSG41.Checked = true;
		}
		else
		{
			cbENSG41.Checked = false;
		}
		if ((mineParam.enSG & 0x20000000000L) == 2199023255552L)
		{
			cbENSG42.Checked = true;
		}
		else
		{
			cbENSG42.Checked = false;
		}
		if ((mineParam.enSG & 0x40000000000L) == 4398046511104L)
		{
			cbENSG43.Checked = true;
		}
		else
		{
			cbENSG43.Checked = false;
		}
		if ((mineParam.enSG & 0x80000000000L) == 8796093022208L)
		{
			cbENSG44.Checked = true;
		}
		else
		{
			cbENSG44.Checked = false;
		}
		if ((mineParam.enSG & 0x100000000000L) == 17592186044416L)
		{
			cbENSG45.Checked = true;
		}
		else
		{
			cbENSG45.Checked = false;
		}
		if ((mineParam.enSG & 0x200000000000L) == 35184372088832L)
		{
			cbENSG46.Checked = true;
		}
		else
		{
			cbENSG46.Checked = false;
		}
		if ((mineParam.enSG & 0x400000000000L) == 70368744177664L)
		{
			cbENSG47.Checked = true;
		}
		else
		{
			cbENSG47.Checked = false;
		}
		if ((mineParam.enSG & 0x800000000000L) == 140737488355328L)
		{
			cbENSG48.Checked = true;
		}
		else
		{
			cbENSG48.Checked = false;
		}
		if ((mineParam.enSG & 0x1000000000000L) == 281474976710656L)
		{
			cbENSG49.Checked = true;
		}
		else
		{
			cbENSG49.Checked = false;
		}
		if ((mineParam.enSG & 0x2000000000000L) == 562949953421312L)
		{
			cbENSG50.Checked = true;
		}
		else
		{
			cbENSG50.Checked = false;
		}
		if ((mineParam.enSG & 0x4000000000000L) == 1125899906842624L)
		{
			cbENSG51.Checked = true;
		}
		else
		{
			cbENSG51.Checked = false;
		}
		if ((mineParam.enSG & 0x8000000000000L) == 2251799813685248L)
		{
			cbENSG52.Checked = true;
		}
		else
		{
			cbENSG52.Checked = false;
		}
		if ((mineParam.enSG & 0x10000000000000L) == 4503599627370496L)
		{
			cbENSG53.Checked = true;
		}
		else
		{
			cbENSG53.Checked = false;
		}
		if ((mineParam.enSG & 0x20000000000000L) == 9007199254740992L)
		{
			cbENSG54.Checked = true;
		}
		else
		{
			cbENSG54.Checked = false;
		}
		if ((mineParam.enSG & 0x40000000000000L) == 18014398509481984L)
		{
			cbENSG55.Checked = true;
		}
		else
		{
			cbENSG55.Checked = false;
		}
		if ((mineParam.enSG & 0x80000000000000L) == 36028797018963968L)
		{
			cbENSG56.Checked = true;
		}
		else
		{
			cbENSG56.Checked = false;
		}
		if ((mineParam.enSG & 0x100000000000000L) == 72057594037927936L)
		{
			cbENSG57.Checked = true;
		}
		else
		{
			cbENSG57.Checked = false;
		}
		if ((mineParam.enSG & 0x200000000000000L) == 144115188075855872L)
		{
			cbENSG58.Checked = true;
		}
		else
		{
			cbENSG58.Checked = false;
		}
		if ((mineParam.enSG & 0x400000000000000L) == 288230376151711744L)
		{
			cbENSG59.Checked = true;
		}
		else
		{
			cbENSG59.Checked = false;
		}
		if ((mineParam.enSG & 0x800000000000000L) == 576460752303423488L)
		{
			cbENSG60.Checked = true;
		}
		else
		{
			cbENSG60.Checked = false;
		}
		tbChannelName1.Text = mineParam.tbChannelName1;
		tbChannelName2.Text = mineParam.tbChannelName2;
		tbChannelName3.Text = mineParam.tbChannelName3;
		tbChannelName4.Text = mineParam.tbChannelName4;
		tbChannelName5.Text = mineParam.tbChannelName5;
		tbChannelName6.Text = mineParam.tbChannelName6;
		tbChannelName7.Text = mineParam.tbChannelName7;
		tbChannelName8.Text = mineParam.tbChannelName8;
		tbChannelName9.Text = mineParam.tbChannelName9;
		tbChannelName10.Text = mineParam.tbChannelName10;
		tbChannelName11.Text = mineParam.tbChannelName11;
		tbChannelName12.Text = mineParam.tbChannelName12;
		tbChannelName13.Text = mineParam.tbChannelName13;
		tbChannelName14.Text = mineParam.tbChannelName14;
		tbChannelName15.Text = mineParam.tbChannelName15;
		tbChannelName16.Text = mineParam.tbChannelName16;
		tbChannelName17.Text = mineParam.tbChannelName17;
		tbChannelName18.Text = mineParam.tbChannelName18;
		tbChannelName19.Text = mineParam.tbChannelName19;
		tbChannelName20.Text = mineParam.tbChannelName20;
		tbChannelName21.Text = mineParam.tbChannelName21;
		tbChannelName22.Text = mineParam.tbChannelName22;
		tbChannelName23.Text = mineParam.tbChannelName23;
		tbChannelName24.Text = mineParam.tbChannelName24;
		tbChannelName25.Text = mineParam.tbChannelName25;
		tbChannelName26.Text = mineParam.tbChannelName26;
		tbChannelName27.Text = mineParam.tbChannelName27;
		tbChannelName28.Text = mineParam.tbChannelName28;
		tbChannelName29.Text = mineParam.tbChannelName29;
		tbChannelName30.Text = mineParam.tbChannelName30;
		tbChannelName31.Text = mineParam.tbChannelName31;
		tbChannelName32.Text = mineParam.tbChannelName32;
		tbChannelName33.Text = mineParam.tbChannelName33;
		tbChannelName34.Text = mineParam.tbChannelName34;
		tbChannelName35.Text = mineParam.tbChannelName35;
		tbChannelName36.Text = mineParam.tbChannelName36;
		tbChannelName37.Text = mineParam.tbChannelName37;
		tbChannelName38.Text = mineParam.tbChannelName38;
		tbChannelName39.Text = mineParam.tbChannelName39;
		tbChannelName40.Text = mineParam.tbChannelName40;
		tbChannelName41.Text = mineParam.tbChannelName41;
		tbChannelName42.Text = mineParam.tbChannelName42;
		tbChannelName43.Text = mineParam.tbChannelName43;
		tbChannelName44.Text = mineParam.tbChannelName44;
		tbChannelName45.Text = mineParam.tbChannelName45;
		tbChannelName46.Text = mineParam.tbChannelName46;
		tbChannelName47.Text = mineParam.tbChannelName47;
		tbChannelName48.Text = mineParam.tbChannelName48;
		tbChannelName49.Text = mineParam.tbChannelName49;
		tbChannelName50.Text = mineParam.tbChannelName50;
		tbChannelName51.Text = mineParam.tbChannelName51;
		tbChannelName52.Text = mineParam.tbChannelName52;
		tbChannelName53.Text = mineParam.tbChannelName53;
		tbChannelName54.Text = mineParam.tbChannelName54;
		tbChannelName55.Text = mineParam.tbChannelName55;
		tbChannelName56.Text = mineParam.tbChannelName56;
		tbChannelName57.Text = mineParam.tbChannelName57;
		tbChannelName58.Text = mineParam.tbChannelName58;
		tbChannelName59.Text = mineParam.tbChannelName59;
		tbChannelName60.Text = mineParam.tbChannelName60;
	}

	private void btnSGset_Click(object sender, EventArgs e)
	{
		mineParam.tbCycles = int.Parse(tbCycles.Text);
		mineParam.tbInjQTime = float.Parse(tbInjQTime.Text);
		mineParam.tbCycleQtime = float.Parse(tbCycleQtime.Text);
		mineParam.tbAnalyzeTime = float.Parse(tbAnalyzeTime.Text);
		if (cbENSG1.Checked)
		{
			mineParam.enSG |= 1uL;
			mineParam.benSG[0] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551614uL;
			mineParam.benSG[0] = false;
		}
		if (cbENSG2.Checked)
		{
			mineParam.enSG |= 2uL;
			mineParam.benSG[1] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551613uL;
			mineParam.benSG[1] = false;
		}
		if (cbENSG3.Checked)
		{
			mineParam.enSG |= 4uL;
			mineParam.benSG[2] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551611uL;
			mineParam.benSG[2] = false;
		}
		if (cbENSG4.Checked)
		{
			mineParam.enSG |= 8uL;
			mineParam.benSG[3] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551607uL;
			mineParam.benSG[3] = false;
		}
		if (cbENSG5.Checked)
		{
			mineParam.enSG |= 16uL;
			mineParam.benSG[4] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551599uL;
			mineParam.benSG[4] = false;
		}
		if (cbENSG6.Checked)
		{
			mineParam.enSG |= 32uL;
			mineParam.benSG[5] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551583uL;
			mineParam.benSG[5] = false;
		}
		if (cbENSG7.Checked)
		{
			mineParam.enSG |= 64uL;
			mineParam.benSG[6] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551551uL;
			mineParam.benSG[6] = false;
		}
		if (cbENSG8.Checked)
		{
			mineParam.enSG |= 128uL;
			mineParam.benSG[7] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551487uL;
			mineParam.benSG[7] = false;
		}
		if (cbENSG9.Checked)
		{
			mineParam.enSG |= 256uL;
			mineParam.benSG[8] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551359uL;
			mineParam.benSG[8] = false;
		}
		if (cbENSG10.Checked)
		{
			mineParam.enSG |= 512uL;
			mineParam.benSG[9] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709551103uL;
			mineParam.benSG[9] = false;
		}
		if (cbENSG11.Checked)
		{
			mineParam.enSG |= 1024uL;
			mineParam.benSG[10] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709550591uL;
			mineParam.benSG[10] = false;
		}
		if (cbENSG12.Checked)
		{
			mineParam.enSG |= 2048uL;
			mineParam.benSG[11] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709549567uL;
			mineParam.benSG[11] = false;
		}
		if (cbENSG13.Checked)
		{
			mineParam.enSG |= 4096uL;
			mineParam.benSG[12] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709547519uL;
			mineParam.benSG[12] = false;
		}
		if (cbENSG14.Checked)
		{
			mineParam.enSG |= 8192uL;
			mineParam.benSG[13] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709543423uL;
			mineParam.benSG[13] = false;
		}
		if (cbENSG15.Checked)
		{
			mineParam.enSG |= 16384uL;
			mineParam.benSG[14] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709535231uL;
			mineParam.benSG[14] = false;
		}
		if (cbENSG16.Checked)
		{
			mineParam.enSG |= 32768uL;
			mineParam.benSG[15] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709518847uL;
			mineParam.benSG[15] = false;
		}
		if (cbENSG17.Checked)
		{
			mineParam.enSG |= 65536uL;
			mineParam.benSG[16] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709486079uL;
			mineParam.benSG[16] = false;
		}
		if (cbENSG18.Checked)
		{
			mineParam.enSG |= 131072uL;
			mineParam.benSG[17] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709420543uL;
			mineParam.benSG[17] = false;
		}
		if (cbENSG19.Checked)
		{
			mineParam.enSG |= 262144uL;
			mineParam.benSG[18] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709289471uL;
			mineParam.benSG[18] = false;
		}
		if (cbENSG20.Checked)
		{
			mineParam.enSG |= 524288uL;
			mineParam.benSG[19] = true;
		}
		else
		{
			mineParam.enSG &= 18446744073709027327uL;
			mineParam.benSG[19] = false;
		}
		if (cbENSG21.Checked)
		{
			mineParam.enSG |= 1048576uL;
		}
		else
		{
			mineParam.enSG &= 18446744073708503039uL;
		}
		if (cbENSG22.Checked)
		{
			mineParam.enSG |= 2097152uL;
		}
		else
		{
			mineParam.enSG &= 18446744073707454463uL;
		}
		if (cbENSG23.Checked)
		{
			mineParam.enSG |= 4194304uL;
		}
		else
		{
			mineParam.enSG &= 18446744073705357311uL;
		}
		if (cbENSG24.Checked)
		{
			mineParam.enSG |= 8388608uL;
		}
		else
		{
			mineParam.enSG &= 18446744073701163007uL;
		}
		if (cbENSG25.Checked)
		{
			mineParam.enSG |= 16777216uL;
		}
		else
		{
			mineParam.enSG &= 18446744073692774399uL;
		}
		if (cbENSG26.Checked)
		{
			mineParam.enSG |= 33554432uL;
		}
		else
		{
			mineParam.enSG &= 18446744073675997183uL;
		}
		if (cbENSG27.Checked)
		{
			mineParam.enSG |= 67108864uL;
		}
		else
		{
			mineParam.enSG &= 18446744073642442751uL;
		}
		if (cbENSG28.Checked)
		{
			mineParam.enSG |= 134217728uL;
		}
		else
		{
			mineParam.enSG &= 18446744073575333887uL;
		}
		if (cbENSG29.Checked)
		{
			mineParam.enSG |= 268435456uL;
		}
		else
		{
			mineParam.enSG &= 18446744073441116159uL;
		}
		if (cbENSG30.Checked)
		{
			mineParam.enSG |= 536870912uL;
		}
		else
		{
			mineParam.enSG &= 18446744073172680703uL;
		}
		if (cbENSG31.Checked)
		{
			mineParam.enSG |= 1073741824uL;
		}
		else
		{
			mineParam.enSG &= 18446744072635809791uL;
		}
		if (cbENSG32.Checked)
		{
			mineParam.enSG |= 2147483648uL;
		}
		else
		{
			mineParam.enSG &= 18446744071562067967uL;
		}
		if (cbENSG33.Checked)
		{
			mineParam.enSG |= 4294967296uL;
		}
		else
		{
			mineParam.enSG &= 18446744069414584319uL;
		}
		if (cbENSG34.Checked)
		{
			mineParam.enSG |= 8589934592uL;
		}
		else
		{
			mineParam.enSG &= 18446744065119617023uL;
		}
		if (cbENSG35.Checked)
		{
			mineParam.enSG |= 17179869184uL;
		}
		else
		{
			mineParam.enSG &= 18446744056529682431uL;
		}
		if (cbENSG36.Checked)
		{
			mineParam.enSG |= 34359738368uL;
		}
		else
		{
			mineParam.enSG &= 18446744039349813247uL;
		}
		if (cbENSG37.Checked)
		{
			mineParam.enSG |= 68719476736uL;
		}
		else
		{
			mineParam.enSG &= 18446744004990074879uL;
		}
		if (cbENSG38.Checked)
		{
			mineParam.enSG |= 137438953472uL;
		}
		else
		{
			mineParam.enSG &= 18446743936270598143uL;
		}
		if (cbENSG39.Checked)
		{
			mineParam.enSG |= 274877906944uL;
		}
		else
		{
			mineParam.enSG &= 18446743798831644671uL;
		}
		if (cbENSG40.Checked)
		{
			mineParam.enSG |= 549755813888uL;
		}
		else
		{
			mineParam.enSG &= 18446743523953737727uL;
		}
		if (cbENSG41.Checked)
		{
			mineParam.enSG |= 1099511627776uL;
		}
		else
		{
			mineParam.enSG &= 18446742974197923839uL;
		}
		if (cbENSG42.Checked)
		{
			mineParam.enSG |= 2199023255552uL;
		}
		else
		{
			mineParam.enSG &= 18446741874686296063uL;
		}
		if (cbENSG43.Checked)
		{
			mineParam.enSG |= 4398046511104uL;
		}
		else
		{
			mineParam.enSG &= 18446739675663040511uL;
		}
		if (cbENSG44.Checked)
		{
			mineParam.enSG |= 8796093022208uL;
		}
		else
		{
			mineParam.enSG &= 18446735277616529407uL;
		}
		if (cbENSG45.Checked)
		{
			mineParam.enSG |= 17592186044416uL;
		}
		else
		{
			mineParam.enSG &= 18446726481523507199uL;
		}
		if (cbENSG46.Checked)
		{
			mineParam.enSG |= 35184372088832uL;
		}
		else
		{
			mineParam.enSG &= 18446708889337462783uL;
		}
		if (cbENSG47.Checked)
		{
			mineParam.enSG |= 70368744177664uL;
		}
		else
		{
			mineParam.enSG &= 18446673704965373951uL;
		}
		if (cbENSG48.Checked)
		{
			mineParam.enSG |= 140737488355328uL;
		}
		else
		{
			mineParam.enSG &= 18446603336221196287uL;
		}
		if (cbENSG49.Checked)
		{
			mineParam.enSG |= 281474976710656uL;
		}
		else
		{
			mineParam.enSG &= 18446462598732840959uL;
		}
		if (cbENSG50.Checked)
		{
			mineParam.enSG |= 562949953421312uL;
		}
		else
		{
			mineParam.enSG &= 18446181123756130303uL;
		}
		if (cbENSG51.Checked)
		{
			mineParam.enSG |= 1125899906842624uL;
		}
		else
		{
			mineParam.enSG &= 18445618173802708991uL;
		}
		if (cbENSG52.Checked)
		{
			mineParam.enSG |= 2251799813685248uL;
		}
		else
		{
			mineParam.enSG &= 18444492273895866367uL;
		}
		if (cbENSG53.Checked)
		{
			mineParam.enSG |= 4503599627370496uL;
		}
		else
		{
			mineParam.enSG &= 18442240474082181119uL;
		}
		if (cbENSG54.Checked)
		{
			mineParam.enSG |= 9007199254740992uL;
		}
		else
		{
			mineParam.enSG &= 18437736874454810623uL;
		}
		if (cbENSG55.Checked)
		{
			mineParam.enSG |= 18014398509481984uL;
		}
		else
		{
			mineParam.enSG &= 18428729675200069631uL;
		}
		if (cbENSG56.Checked)
		{
			mineParam.enSG |= 36028797018963968uL;
		}
		else
		{
			mineParam.enSG &= 18410715276690587647uL;
		}
		if (cbENSG57.Checked)
		{
			mineParam.enSG |= 72057594037927936uL;
		}
		else
		{
			mineParam.enSG &= 18374686479671623679uL;
		}
		if (cbENSG58.Checked)
		{
			mineParam.enSG |= 144115188075855872uL;
		}
		else
		{
			mineParam.enSG &= 18302628885633695743uL;
		}
		if (cbENSG59.Checked)
		{
			mineParam.enSG |= 288230376151711744uL;
		}
		else
		{
			mineParam.enSG &= 18158513697557839871uL;
		}
		if (cbENSG60.Checked)
		{
			mineParam.enSG |= 576460752303423488uL;
		}
		else
		{
			mineParam.enSG &= 17870283321406128127uL;
		}
		mineParam.benSG[20] = cbENSG21.Checked;
		mineParam.benSG[21] = cbENSG22.Checked;
		mineParam.benSG[22] = cbENSG23.Checked;
		mineParam.benSG[23] = cbENSG24.Checked;
		mineParam.benSG[24] = cbENSG25.Checked;
		mineParam.benSG[25] = cbENSG26.Checked;
		mineParam.benSG[26] = cbENSG27.Checked;
		mineParam.benSG[27] = cbENSG28.Checked;
		mineParam.benSG[28] = cbENSG29.Checked;
		mineParam.benSG[29] = cbENSG30.Checked;
		mineParam.benSG[30] = cbENSG31.Checked;
		mineParam.benSG[31] = cbENSG32.Checked;
		mineParam.benSG[32] = cbENSG33.Checked;
		mineParam.benSG[33] = cbENSG34.Checked;
		mineParam.benSG[34] = cbENSG35.Checked;
		mineParam.benSG[35] = cbENSG36.Checked;
		mineParam.benSG[36] = cbENSG37.Checked;
		mineParam.benSG[37] = cbENSG38.Checked;
		mineParam.benSG[38] = cbENSG39.Checked;
		mineParam.benSG[39] = cbENSG40.Checked;
		mineParam.benSG[40] = cbENSG41.Checked;
		mineParam.benSG[41] = cbENSG42.Checked;
		mineParam.benSG[42] = cbENSG43.Checked;
		mineParam.benSG[43] = cbENSG44.Checked;
		mineParam.benSG[44] = cbENSG45.Checked;
		mineParam.benSG[45] = cbENSG46.Checked;
		mineParam.benSG[46] = cbENSG47.Checked;
		mineParam.benSG[47] = cbENSG48.Checked;
		mineParam.benSG[48] = cbENSG49.Checked;
		mineParam.benSG[49] = cbENSG50.Checked;
		mineParam.benSG[50] = cbENSG51.Checked;
		mineParam.benSG[51] = cbENSG52.Checked;
		mineParam.benSG[52] = cbENSG53.Checked;
		mineParam.benSG[53] = cbENSG54.Checked;
		mineParam.benSG[54] = cbENSG55.Checked;
		mineParam.benSG[55] = cbENSG56.Checked;
		mineParam.benSG[56] = cbENSG57.Checked;
		mineParam.benSG[57] = cbENSG58.Checked;
		mineParam.benSG[58] = cbENSG59.Checked;
		mineParam.benSG[59] = cbENSG60.Checked;
		mineParam.tbChannelName1 = tbChannelName1.Text;
		mineParam.tbChannelName2 = tbChannelName2.Text;
		mineParam.tbChannelName3 = tbChannelName3.Text;
		mineParam.tbChannelName4 = tbChannelName4.Text;
		mineParam.tbChannelName5 = tbChannelName5.Text;
		mineParam.tbChannelName6 = tbChannelName6.Text;
		mineParam.tbChannelName7 = tbChannelName7.Text;
		mineParam.tbChannelName8 = tbChannelName8.Text;
		mineParam.tbChannelName9 = tbChannelName9.Text;
		mineParam.tbChannelName10 = tbChannelName10.Text;
		mineParam.tbChannelName11 = tbChannelName11.Text;
		mineParam.tbChannelName12 = tbChannelName12.Text;
		mineParam.tbChannelName13 = tbChannelName13.Text;
		mineParam.tbChannelName14 = tbChannelName14.Text;
		mineParam.tbChannelName15 = tbChannelName15.Text;
		mineParam.tbChannelName16 = tbChannelName16.Text;
		mineParam.tbChannelName17 = tbChannelName17.Text;
		mineParam.tbChannelName18 = tbChannelName18.Text;
		mineParam.tbChannelName19 = tbChannelName19.Text;
		mineParam.tbChannelName20 = tbChannelName20.Text;
		mineParam.tbChannelName21 = tbChannelName21.Text;
		mineParam.tbChannelName22 = tbChannelName22.Text;
		mineParam.tbChannelName23 = tbChannelName23.Text;
		mineParam.tbChannelName24 = tbChannelName24.Text;
		mineParam.tbChannelName25 = tbChannelName25.Text;
		mineParam.tbChannelName26 = tbChannelName26.Text;
		mineParam.tbChannelName27 = tbChannelName27.Text;
		mineParam.tbChannelName28 = tbChannelName28.Text;
		mineParam.tbChannelName29 = tbChannelName29.Text;
		mineParam.tbChannelName30 = tbChannelName30.Text;
		mineParam.tbChannelName31 = tbChannelName31.Text;
		mineParam.tbChannelName32 = tbChannelName32.Text;
		mineParam.tbChannelName33 = tbChannelName33.Text;
		mineParam.tbChannelName34 = tbChannelName34.Text;
		mineParam.tbChannelName35 = tbChannelName35.Text;
		mineParam.tbChannelName36 = tbChannelName36.Text;
		mineParam.tbChannelName37 = tbChannelName37.Text;
		mineParam.tbChannelName38 = tbChannelName38.Text;
		mineParam.tbChannelName39 = tbChannelName39.Text;
		mineParam.tbChannelName40 = tbChannelName40.Text;
		mineParam.tbChannelName41 = tbChannelName41.Text;
		mineParam.tbChannelName42 = tbChannelName42.Text;
		mineParam.tbChannelName43 = tbChannelName43.Text;
		mineParam.tbChannelName44 = tbChannelName44.Text;
		mineParam.tbChannelName45 = tbChannelName45.Text;
		mineParam.tbChannelName46 = tbChannelName46.Text;
		mineParam.tbChannelName47 = tbChannelName47.Text;
		mineParam.tbChannelName48 = tbChannelName48.Text;
		mineParam.tbChannelName49 = tbChannelName49.Text;
		mineParam.tbChannelName50 = tbChannelName50.Text;
		mineParam.tbChannelName51 = tbChannelName51.Text;
		mineParam.tbChannelName52 = tbChannelName52.Text;
		mineParam.tbChannelName53 = tbChannelName53.Text;
		mineParam.tbChannelName54 = tbChannelName54.Text;
		mineParam.tbChannelName55 = tbChannelName55.Text;
		mineParam.tbChannelName56 = tbChannelName56.Text;
		mineParam.tbChannelName57 = tbChannelName57.Text;
		mineParam.tbChannelName58 = tbChannelName58.Text;
		mineParam.tbChannelName59 = tbChannelName59.Text;
		mineParam.tbChannelName60 = tbChannelName60.Text;
		mineParam.SaveParam();
		Close();
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.tbAnalyzeTime = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tbInjQTime = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.tbCycleQtime = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.tbCycles = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tbChannelName30 = new System.Windows.Forms.TextBox();
		this.tbChannelName29 = new System.Windows.Forms.TextBox();
		this.tbChannelName28 = new System.Windows.Forms.TextBox();
		this.tbChannelName27 = new System.Windows.Forms.TextBox();
		this.tbChannelName26 = new System.Windows.Forms.TextBox();
		this.tbChannelName25 = new System.Windows.Forms.TextBox();
		this.tbChannelName24 = new System.Windows.Forms.TextBox();
		this.tbChannelName23 = new System.Windows.Forms.TextBox();
		this.tbChannelName22 = new System.Windows.Forms.TextBox();
		this.tbChannelName21 = new System.Windows.Forms.TextBox();
		this.tbChannelName20 = new System.Windows.Forms.TextBox();
		this.tbChannelName19 = new System.Windows.Forms.TextBox();
		this.tbChannelName18 = new System.Windows.Forms.TextBox();
		this.tbChannelName17 = new System.Windows.Forms.TextBox();
		this.tbChannelName16 = new System.Windows.Forms.TextBox();
		this.cbENSG30 = new System.Windows.Forms.CheckBox();
		this.cbENSG29 = new System.Windows.Forms.CheckBox();
		this.cbENSG28 = new System.Windows.Forms.CheckBox();
		this.cbENSG27 = new System.Windows.Forms.CheckBox();
		this.cbENSG26 = new System.Windows.Forms.CheckBox();
		this.cbENSG25 = new System.Windows.Forms.CheckBox();
		this.cbENSG24 = new System.Windows.Forms.CheckBox();
		this.cbENSG23 = new System.Windows.Forms.CheckBox();
		this.cbENSG22 = new System.Windows.Forms.CheckBox();
		this.cbENSG21 = new System.Windows.Forms.CheckBox();
		this.cbENSG20 = new System.Windows.Forms.CheckBox();
		this.cbENSG19 = new System.Windows.Forms.CheckBox();
		this.cbENSG18 = new System.Windows.Forms.CheckBox();
		this.cbENSG17 = new System.Windows.Forms.CheckBox();
		this.cbENSG16 = new System.Windows.Forms.CheckBox();
		this.tbChannelName15 = new System.Windows.Forms.TextBox();
		this.tbChannelName14 = new System.Windows.Forms.TextBox();
		this.tbChannelName13 = new System.Windows.Forms.TextBox();
		this.tbChannelName12 = new System.Windows.Forms.TextBox();
		this.tbChannelName11 = new System.Windows.Forms.TextBox();
		this.tbChannelName10 = new System.Windows.Forms.TextBox();
		this.tbChannelName9 = new System.Windows.Forms.TextBox();
		this.tbChannelName8 = new System.Windows.Forms.TextBox();
		this.tbChannelName7 = new System.Windows.Forms.TextBox();
		this.tbChannelName6 = new System.Windows.Forms.TextBox();
		this.tbChannelName5 = new System.Windows.Forms.TextBox();
		this.tbChannelName4 = new System.Windows.Forms.TextBox();
		this.tbChannelName3 = new System.Windows.Forms.TextBox();
		this.tbChannelName2 = new System.Windows.Forms.TextBox();
		this.tbChannelName1 = new System.Windows.Forms.TextBox();
		this.cbENSG15 = new System.Windows.Forms.CheckBox();
		this.cbENSG14 = new System.Windows.Forms.CheckBox();
		this.cbENSG13 = new System.Windows.Forms.CheckBox();
		this.cbENSG12 = new System.Windows.Forms.CheckBox();
		this.cbENSG11 = new System.Windows.Forms.CheckBox();
		this.cbENSG10 = new System.Windows.Forms.CheckBox();
		this.cbENSG9 = new System.Windows.Forms.CheckBox();
		this.cbENSG8 = new System.Windows.Forms.CheckBox();
		this.cbENSG7 = new System.Windows.Forms.CheckBox();
		this.cbENSG6 = new System.Windows.Forms.CheckBox();
		this.cbENSG5 = new System.Windows.Forms.CheckBox();
		this.cbENSG4 = new System.Windows.Forms.CheckBox();
		this.cbENSG3 = new System.Windows.Forms.CheckBox();
		this.cbENSG2 = new System.Windows.Forms.CheckBox();
		this.cbENSG1 = new System.Windows.Forms.CheckBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tbChannelName60 = new System.Windows.Forms.TextBox();
		this.tbChannelName59 = new System.Windows.Forms.TextBox();
		this.tbChannelName58 = new System.Windows.Forms.TextBox();
		this.tbChannelName57 = new System.Windows.Forms.TextBox();
		this.tbChannelName56 = new System.Windows.Forms.TextBox();
		this.tbChannelName55 = new System.Windows.Forms.TextBox();
		this.tbChannelName54 = new System.Windows.Forms.TextBox();
		this.tbChannelName53 = new System.Windows.Forms.TextBox();
		this.tbChannelName52 = new System.Windows.Forms.TextBox();
		this.tbChannelName51 = new System.Windows.Forms.TextBox();
		this.tbChannelName50 = new System.Windows.Forms.TextBox();
		this.tbChannelName49 = new System.Windows.Forms.TextBox();
		this.tbChannelName48 = new System.Windows.Forms.TextBox();
		this.tbChannelName47 = new System.Windows.Forms.TextBox();
		this.tbChannelName46 = new System.Windows.Forms.TextBox();
		this.cbENSG60 = new System.Windows.Forms.CheckBox();
		this.cbENSG59 = new System.Windows.Forms.CheckBox();
		this.cbENSG58 = new System.Windows.Forms.CheckBox();
		this.cbENSG57 = new System.Windows.Forms.CheckBox();
		this.cbENSG56 = new System.Windows.Forms.CheckBox();
		this.cbENSG55 = new System.Windows.Forms.CheckBox();
		this.cbENSG54 = new System.Windows.Forms.CheckBox();
		this.cbENSG53 = new System.Windows.Forms.CheckBox();
		this.cbENSG52 = new System.Windows.Forms.CheckBox();
		this.cbENSG51 = new System.Windows.Forms.CheckBox();
		this.cbENSG50 = new System.Windows.Forms.CheckBox();
		this.cbENSG49 = new System.Windows.Forms.CheckBox();
		this.cbENSG48 = new System.Windows.Forms.CheckBox();
		this.cbENSG47 = new System.Windows.Forms.CheckBox();
		this.cbENSG46 = new System.Windows.Forms.CheckBox();
		this.tbChannelName45 = new System.Windows.Forms.TextBox();
		this.tbChannelName44 = new System.Windows.Forms.TextBox();
		this.tbChannelName43 = new System.Windows.Forms.TextBox();
		this.tbChannelName42 = new System.Windows.Forms.TextBox();
		this.tbChannelName41 = new System.Windows.Forms.TextBox();
		this.tbChannelName40 = new System.Windows.Forms.TextBox();
		this.tbChannelName39 = new System.Windows.Forms.TextBox();
		this.tbChannelName38 = new System.Windows.Forms.TextBox();
		this.tbChannelName37 = new System.Windows.Forms.TextBox();
		this.tbChannelName36 = new System.Windows.Forms.TextBox();
		this.tbChannelName35 = new System.Windows.Forms.TextBox();
		this.tbChannelName34 = new System.Windows.Forms.TextBox();
		this.tbChannelName33 = new System.Windows.Forms.TextBox();
		this.tbChannelName32 = new System.Windows.Forms.TextBox();
		this.tbChannelName31 = new System.Windows.Forms.TextBox();
		this.cbENSG45 = new System.Windows.Forms.CheckBox();
		this.cbENSG44 = new System.Windows.Forms.CheckBox();
		this.cbENSG43 = new System.Windows.Forms.CheckBox();
		this.cbENSG42 = new System.Windows.Forms.CheckBox();
		this.cbENSG41 = new System.Windows.Forms.CheckBox();
		this.cbENSG40 = new System.Windows.Forms.CheckBox();
		this.cbENSG39 = new System.Windows.Forms.CheckBox();
		this.cbENSG38 = new System.Windows.Forms.CheckBox();
		this.cbENSG37 = new System.Windows.Forms.CheckBox();
		this.cbENSG36 = new System.Windows.Forms.CheckBox();
		this.cbENSG35 = new System.Windows.Forms.CheckBox();
		this.cbENSG34 = new System.Windows.Forms.CheckBox();
		this.cbENSG33 = new System.Windows.Forms.CheckBox();
		this.cbENSG32 = new System.Windows.Forms.CheckBox();
		this.cbENSG31 = new System.Windows.Forms.CheckBox();
		this.btnSGset = new System.Windows.Forms.Button();
		this.panel1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel1.Controls.Add(this.tbAnalyzeTime);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.tbInjQTime);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.tbCycleQtime);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.tbCycles);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Location = new System.Drawing.Point(12, 12);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(427, 103);
		this.panel1.TabIndex = 0;
		this.tbAnalyzeTime.Location = new System.Drawing.Point(317, 63);
		this.tbAnalyzeTime.Name = "tbAnalyzeTime";
		this.tbAnalyzeTime.Size = new System.Drawing.Size(100, 21);
		this.tbAnalyzeTime.TabIndex = 7;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(210, 66);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(89, 12);
		this.label4.TabIndex = 6;
		this.label4.Text = "分析时间(min):";
		this.tbInjQTime.Location = new System.Drawing.Point(317, 22);
		this.tbInjQTime.Name = "tbInjQTime";
		this.tbInjQTime.Size = new System.Drawing.Size(100, 21);
		this.tbInjQTime.TabIndex = 5;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(210, 28);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(101, 12);
		this.label3.TabIndex = 4;
		this.label3.Text = "进样前等待(min):";
		this.tbCycleQtime.Location = new System.Drawing.Point(104, 63);
		this.tbCycleQtime.Name = "tbCycleQtime";
		this.tbCycleQtime.Size = new System.Drawing.Size(100, 21);
		this.tbCycleQtime.TabIndex = 3;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(3, 66);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(101, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "循环前等待(min):";
		this.tbCycles.Location = new System.Drawing.Point(104, 22);
		this.tbCycles.Name = "tbCycles";
		this.tbCycles.Size = new System.Drawing.Size(100, 21);
		this.tbCycles.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "循环次数:";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 121);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(430, 399);
		this.tabControl1.TabIndex = 1;
		this.tabPage1.Controls.Add(this.tbChannelName30);
		this.tabPage1.Controls.Add(this.tbChannelName29);
		this.tabPage1.Controls.Add(this.tbChannelName28);
		this.tabPage1.Controls.Add(this.tbChannelName27);
		this.tabPage1.Controls.Add(this.tbChannelName26);
		this.tabPage1.Controls.Add(this.tbChannelName25);
		this.tabPage1.Controls.Add(this.tbChannelName24);
		this.tabPage1.Controls.Add(this.tbChannelName23);
		this.tabPage1.Controls.Add(this.tbChannelName22);
		this.tabPage1.Controls.Add(this.tbChannelName21);
		this.tabPage1.Controls.Add(this.tbChannelName20);
		this.tabPage1.Controls.Add(this.tbChannelName19);
		this.tabPage1.Controls.Add(this.tbChannelName18);
		this.tabPage1.Controls.Add(this.tbChannelName17);
		this.tabPage1.Controls.Add(this.tbChannelName16);
		this.tabPage1.Controls.Add(this.cbENSG30);
		this.tabPage1.Controls.Add(this.cbENSG29);
		this.tabPage1.Controls.Add(this.cbENSG28);
		this.tabPage1.Controls.Add(this.cbENSG27);
		this.tabPage1.Controls.Add(this.cbENSG26);
		this.tabPage1.Controls.Add(this.cbENSG25);
		this.tabPage1.Controls.Add(this.cbENSG24);
		this.tabPage1.Controls.Add(this.cbENSG23);
		this.tabPage1.Controls.Add(this.cbENSG22);
		this.tabPage1.Controls.Add(this.cbENSG21);
		this.tabPage1.Controls.Add(this.cbENSG20);
		this.tabPage1.Controls.Add(this.cbENSG19);
		this.tabPage1.Controls.Add(this.cbENSG18);
		this.tabPage1.Controls.Add(this.cbENSG17);
		this.tabPage1.Controls.Add(this.cbENSG16);
		this.tabPage1.Controls.Add(this.tbChannelName15);
		this.tabPage1.Controls.Add(this.tbChannelName14);
		this.tabPage1.Controls.Add(this.tbChannelName13);
		this.tabPage1.Controls.Add(this.tbChannelName12);
		this.tabPage1.Controls.Add(this.tbChannelName11);
		this.tabPage1.Controls.Add(this.tbChannelName10);
		this.tabPage1.Controls.Add(this.tbChannelName9);
		this.tabPage1.Controls.Add(this.tbChannelName8);
		this.tabPage1.Controls.Add(this.tbChannelName7);
		this.tabPage1.Controls.Add(this.tbChannelName6);
		this.tabPage1.Controls.Add(this.tbChannelName5);
		this.tabPage1.Controls.Add(this.tbChannelName4);
		this.tabPage1.Controls.Add(this.tbChannelName3);
		this.tabPage1.Controls.Add(this.tbChannelName2);
		this.tabPage1.Controls.Add(this.tbChannelName1);
		this.tabPage1.Controls.Add(this.cbENSG15);
		this.tabPage1.Controls.Add(this.cbENSG14);
		this.tabPage1.Controls.Add(this.cbENSG13);
		this.tabPage1.Controls.Add(this.cbENSG12);
		this.tabPage1.Controls.Add(this.cbENSG11);
		this.tabPage1.Controls.Add(this.cbENSG10);
		this.tabPage1.Controls.Add(this.cbENSG9);
		this.tabPage1.Controls.Add(this.cbENSG8);
		this.tabPage1.Controls.Add(this.cbENSG7);
		this.tabPage1.Controls.Add(this.cbENSG6);
		this.tabPage1.Controls.Add(this.cbENSG5);
		this.tabPage1.Controls.Add(this.cbENSG4);
		this.tabPage1.Controls.Add(this.cbENSG3);
		this.tabPage1.Controls.Add(this.cbENSG2);
		this.tabPage1.Controls.Add(this.cbENSG1);
		this.tabPage1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(422, 373);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "1~30#束管";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tbChannelName30.Location = new System.Drawing.Point(303, 323);
		this.tbChannelName30.Name = "tbChannelName30";
		this.tbChannelName30.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName30.TabIndex = 59;
		this.tbChannelName29.Location = new System.Drawing.Point(303, 301);
		this.tbChannelName29.Name = "tbChannelName29";
		this.tbChannelName29.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName29.TabIndex = 58;
		this.tbChannelName28.Location = new System.Drawing.Point(303, 279);
		this.tbChannelName28.Name = "tbChannelName28";
		this.tbChannelName28.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName28.TabIndex = 57;
		this.tbChannelName27.Location = new System.Drawing.Point(303, 257);
		this.tbChannelName27.Name = "tbChannelName27";
		this.tbChannelName27.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName27.TabIndex = 56;
		this.tbChannelName26.Location = new System.Drawing.Point(303, 235);
		this.tbChannelName26.Name = "tbChannelName26";
		this.tbChannelName26.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName26.TabIndex = 55;
		this.tbChannelName25.Location = new System.Drawing.Point(303, 213);
		this.tbChannelName25.Name = "tbChannelName25";
		this.tbChannelName25.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName25.TabIndex = 54;
		this.tbChannelName24.Location = new System.Drawing.Point(303, 191);
		this.tbChannelName24.Name = "tbChannelName24";
		this.tbChannelName24.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName24.TabIndex = 53;
		this.tbChannelName23.Location = new System.Drawing.Point(303, 169);
		this.tbChannelName23.Name = "tbChannelName23";
		this.tbChannelName23.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName23.TabIndex = 52;
		this.tbChannelName22.Location = new System.Drawing.Point(303, 147);
		this.tbChannelName22.Name = "tbChannelName22";
		this.tbChannelName22.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName22.TabIndex = 51;
		this.tbChannelName21.Location = new System.Drawing.Point(303, 125);
		this.tbChannelName21.Name = "tbChannelName21";
		this.tbChannelName21.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName21.TabIndex = 50;
		this.tbChannelName20.Location = new System.Drawing.Point(303, 103);
		this.tbChannelName20.Name = "tbChannelName20";
		this.tbChannelName20.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName20.TabIndex = 49;
		this.tbChannelName19.Location = new System.Drawing.Point(303, 81);
		this.tbChannelName19.Name = "tbChannelName19";
		this.tbChannelName19.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName19.TabIndex = 48;
		this.tbChannelName18.Location = new System.Drawing.Point(303, 59);
		this.tbChannelName18.Name = "tbChannelName18";
		this.tbChannelName18.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName18.TabIndex = 47;
		this.tbChannelName17.Location = new System.Drawing.Point(303, 37);
		this.tbChannelName17.Name = "tbChannelName17";
		this.tbChannelName17.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName17.TabIndex = 46;
		this.tbChannelName16.Location = new System.Drawing.Point(303, 15);
		this.tbChannelName16.Name = "tbChannelName16";
		this.tbChannelName16.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName16.TabIndex = 45;
		this.cbENSG30.AutoSize = true;
		this.cbENSG30.Location = new System.Drawing.Point(248, 325);
		this.cbENSG30.Name = "cbENSG30";
		this.cbENSG30.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG30.Size = new System.Drawing.Size(45, 20);
		this.cbENSG30.TabIndex = 44;
		this.cbENSG30.Text = "30";
		this.cbENSG30.UseVisualStyleBackColor = true;
		this.cbENSG29.AutoSize = true;
		this.cbENSG29.Location = new System.Drawing.Point(248, 303);
		this.cbENSG29.Name = "cbENSG29";
		this.cbENSG29.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG29.Size = new System.Drawing.Size(45, 20);
		this.cbENSG29.TabIndex = 43;
		this.cbENSG29.Text = "29";
		this.cbENSG29.UseVisualStyleBackColor = true;
		this.cbENSG28.AutoSize = true;
		this.cbENSG28.Location = new System.Drawing.Point(248, 281);
		this.cbENSG28.Name = "cbENSG28";
		this.cbENSG28.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG28.Size = new System.Drawing.Size(45, 20);
		this.cbENSG28.TabIndex = 42;
		this.cbENSG28.Text = "28";
		this.cbENSG28.UseVisualStyleBackColor = true;
		this.cbENSG27.AutoSize = true;
		this.cbENSG27.Location = new System.Drawing.Point(248, 259);
		this.cbENSG27.Name = "cbENSG27";
		this.cbENSG27.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG27.Size = new System.Drawing.Size(45, 20);
		this.cbENSG27.TabIndex = 41;
		this.cbENSG27.Text = "27";
		this.cbENSG27.UseVisualStyleBackColor = true;
		this.cbENSG26.AutoSize = true;
		this.cbENSG26.Location = new System.Drawing.Point(248, 237);
		this.cbENSG26.Name = "cbENSG26";
		this.cbENSG26.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG26.Size = new System.Drawing.Size(45, 20);
		this.cbENSG26.TabIndex = 40;
		this.cbENSG26.Text = "26";
		this.cbENSG26.UseVisualStyleBackColor = true;
		this.cbENSG25.AutoSize = true;
		this.cbENSG25.Location = new System.Drawing.Point(248, 215);
		this.cbENSG25.Name = "cbENSG25";
		this.cbENSG25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG25.Size = new System.Drawing.Size(45, 20);
		this.cbENSG25.TabIndex = 39;
		this.cbENSG25.Text = "25";
		this.cbENSG25.UseVisualStyleBackColor = true;
		this.cbENSG24.AutoSize = true;
		this.cbENSG24.Location = new System.Drawing.Point(248, 193);
		this.cbENSG24.Name = "cbENSG24";
		this.cbENSG24.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG24.Size = new System.Drawing.Size(45, 20);
		this.cbENSG24.TabIndex = 38;
		this.cbENSG24.Text = "24";
		this.cbENSG24.UseVisualStyleBackColor = true;
		this.cbENSG23.AutoSize = true;
		this.cbENSG23.Location = new System.Drawing.Point(248, 171);
		this.cbENSG23.Name = "cbENSG23";
		this.cbENSG23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG23.Size = new System.Drawing.Size(45, 20);
		this.cbENSG23.TabIndex = 37;
		this.cbENSG23.Text = "23";
		this.cbENSG23.UseVisualStyleBackColor = true;
		this.cbENSG22.AutoSize = true;
		this.cbENSG22.Location = new System.Drawing.Point(248, 149);
		this.cbENSG22.Name = "cbENSG22";
		this.cbENSG22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG22.Size = new System.Drawing.Size(45, 20);
		this.cbENSG22.TabIndex = 36;
		this.cbENSG22.Text = "22";
		this.cbENSG22.UseVisualStyleBackColor = true;
		this.cbENSG21.AutoSize = true;
		this.cbENSG21.Location = new System.Drawing.Point(248, 127);
		this.cbENSG21.Name = "cbENSG21";
		this.cbENSG21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG21.Size = new System.Drawing.Size(45, 20);
		this.cbENSG21.TabIndex = 35;
		this.cbENSG21.Text = "21";
		this.cbENSG21.UseVisualStyleBackColor = true;
		this.cbENSG20.AutoSize = true;
		this.cbENSG20.Location = new System.Drawing.Point(248, 105);
		this.cbENSG20.Name = "cbENSG20";
		this.cbENSG20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG20.Size = new System.Drawing.Size(45, 20);
		this.cbENSG20.TabIndex = 34;
		this.cbENSG20.Text = "20";
		this.cbENSG20.UseVisualStyleBackColor = true;
		this.cbENSG19.AutoSize = true;
		this.cbENSG19.Location = new System.Drawing.Point(248, 83);
		this.cbENSG19.Name = "cbENSG19";
		this.cbENSG19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG19.Size = new System.Drawing.Size(45, 20);
		this.cbENSG19.TabIndex = 33;
		this.cbENSG19.Text = "19";
		this.cbENSG19.UseVisualStyleBackColor = true;
		this.cbENSG18.AutoSize = true;
		this.cbENSG18.Location = new System.Drawing.Point(248, 61);
		this.cbENSG18.Name = "cbENSG18";
		this.cbENSG18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG18.Size = new System.Drawing.Size(45, 20);
		this.cbENSG18.TabIndex = 32;
		this.cbENSG18.Text = "18";
		this.cbENSG18.UseVisualStyleBackColor = true;
		this.cbENSG17.AutoSize = true;
		this.cbENSG17.Location = new System.Drawing.Point(248, 39);
		this.cbENSG17.Name = "cbENSG17";
		this.cbENSG17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG17.Size = new System.Drawing.Size(45, 20);
		this.cbENSG17.TabIndex = 31;
		this.cbENSG17.Text = "17";
		this.cbENSG17.UseVisualStyleBackColor = true;
		this.cbENSG16.AutoSize = true;
		this.cbENSG16.Location = new System.Drawing.Point(248, 17);
		this.cbENSG16.Name = "cbENSG16";
		this.cbENSG16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG16.Size = new System.Drawing.Size(45, 20);
		this.cbENSG16.TabIndex = 30;
		this.cbENSG16.Text = "16";
		this.cbENSG16.UseVisualStyleBackColor = true;
		this.tbChannelName15.Location = new System.Drawing.Point(59, 323);
		this.tbChannelName15.Name = "tbChannelName15";
		this.tbChannelName15.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName15.TabIndex = 29;
		this.tbChannelName14.Location = new System.Drawing.Point(59, 301);
		this.tbChannelName14.Name = "tbChannelName14";
		this.tbChannelName14.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName14.TabIndex = 28;
		this.tbChannelName13.Location = new System.Drawing.Point(59, 279);
		this.tbChannelName13.Name = "tbChannelName13";
		this.tbChannelName13.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName13.TabIndex = 27;
		this.tbChannelName12.Location = new System.Drawing.Point(59, 257);
		this.tbChannelName12.Name = "tbChannelName12";
		this.tbChannelName12.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName12.TabIndex = 26;
		this.tbChannelName11.Location = new System.Drawing.Point(59, 235);
		this.tbChannelName11.Name = "tbChannelName11";
		this.tbChannelName11.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName11.TabIndex = 25;
		this.tbChannelName10.Location = new System.Drawing.Point(59, 213);
		this.tbChannelName10.Name = "tbChannelName10";
		this.tbChannelName10.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName10.TabIndex = 24;
		this.tbChannelName9.Location = new System.Drawing.Point(59, 191);
		this.tbChannelName9.Name = "tbChannelName9";
		this.tbChannelName9.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName9.TabIndex = 23;
		this.tbChannelName8.Location = new System.Drawing.Point(59, 169);
		this.tbChannelName8.Name = "tbChannelName8";
		this.tbChannelName8.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName8.TabIndex = 22;
		this.tbChannelName7.Location = new System.Drawing.Point(59, 147);
		this.tbChannelName7.Name = "tbChannelName7";
		this.tbChannelName7.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName7.TabIndex = 21;
		this.tbChannelName6.Location = new System.Drawing.Point(59, 125);
		this.tbChannelName6.Name = "tbChannelName6";
		this.tbChannelName6.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName6.TabIndex = 20;
		this.tbChannelName5.Location = new System.Drawing.Point(59, 103);
		this.tbChannelName5.Name = "tbChannelName5";
		this.tbChannelName5.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName5.TabIndex = 19;
		this.tbChannelName4.Location = new System.Drawing.Point(59, 81);
		this.tbChannelName4.Name = "tbChannelName4";
		this.tbChannelName4.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName4.TabIndex = 18;
		this.tbChannelName3.Location = new System.Drawing.Point(59, 59);
		this.tbChannelName3.Name = "tbChannelName3";
		this.tbChannelName3.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName3.TabIndex = 17;
		this.tbChannelName2.Location = new System.Drawing.Point(59, 37);
		this.tbChannelName2.Name = "tbChannelName2";
		this.tbChannelName2.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName2.TabIndex = 16;
		this.tbChannelName1.Location = new System.Drawing.Point(59, 15);
		this.tbChannelName1.Name = "tbChannelName1";
		this.tbChannelName1.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName1.TabIndex = 15;
		this.cbENSG15.AutoSize = true;
		this.cbENSG15.Location = new System.Drawing.Point(6, 325);
		this.cbENSG15.Name = "cbENSG15";
		this.cbENSG15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG15.Size = new System.Drawing.Size(45, 20);
		this.cbENSG15.TabIndex = 14;
		this.cbENSG15.Text = "15";
		this.cbENSG15.UseVisualStyleBackColor = true;
		this.cbENSG14.AutoSize = true;
		this.cbENSG14.Location = new System.Drawing.Point(6, 303);
		this.cbENSG14.Name = "cbENSG14";
		this.cbENSG14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG14.Size = new System.Drawing.Size(45, 20);
		this.cbENSG14.TabIndex = 13;
		this.cbENSG14.Text = "14";
		this.cbENSG14.UseVisualStyleBackColor = true;
		this.cbENSG13.AutoSize = true;
		this.cbENSG13.Location = new System.Drawing.Point(6, 281);
		this.cbENSG13.Name = "cbENSG13";
		this.cbENSG13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG13.Size = new System.Drawing.Size(45, 20);
		this.cbENSG13.TabIndex = 12;
		this.cbENSG13.Text = "13";
		this.cbENSG13.UseVisualStyleBackColor = true;
		this.cbENSG12.AutoSize = true;
		this.cbENSG12.Location = new System.Drawing.Point(6, 259);
		this.cbENSG12.Name = "cbENSG12";
		this.cbENSG12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG12.Size = new System.Drawing.Size(45, 20);
		this.cbENSG12.TabIndex = 11;
		this.cbENSG12.Text = "12";
		this.cbENSG12.UseVisualStyleBackColor = true;
		this.cbENSG11.AutoSize = true;
		this.cbENSG11.Location = new System.Drawing.Point(6, 237);
		this.cbENSG11.Name = "cbENSG11";
		this.cbENSG11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG11.Size = new System.Drawing.Size(45, 20);
		this.cbENSG11.TabIndex = 10;
		this.cbENSG11.Text = "11";
		this.cbENSG11.UseVisualStyleBackColor = true;
		this.cbENSG10.AutoSize = true;
		this.cbENSG10.Location = new System.Drawing.Point(6, 215);
		this.cbENSG10.Name = "cbENSG10";
		this.cbENSG10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG10.Size = new System.Drawing.Size(45, 20);
		this.cbENSG10.TabIndex = 9;
		this.cbENSG10.Text = "10";
		this.cbENSG10.UseVisualStyleBackColor = true;
		this.cbENSG9.AutoSize = true;
		this.cbENSG9.Location = new System.Drawing.Point(15, 193);
		this.cbENSG9.Name = "cbENSG9";
		this.cbENSG9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG9.Size = new System.Drawing.Size(36, 20);
		this.cbENSG9.TabIndex = 8;
		this.cbENSG9.Text = "9";
		this.cbENSG9.UseVisualStyleBackColor = true;
		this.cbENSG8.AutoSize = true;
		this.cbENSG8.Location = new System.Drawing.Point(15, 171);
		this.cbENSG8.Name = "cbENSG8";
		this.cbENSG8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG8.Size = new System.Drawing.Size(36, 20);
		this.cbENSG8.TabIndex = 7;
		this.cbENSG8.Text = "8";
		this.cbENSG8.UseVisualStyleBackColor = true;
		this.cbENSG7.AutoSize = true;
		this.cbENSG7.Location = new System.Drawing.Point(15, 149);
		this.cbENSG7.Name = "cbENSG7";
		this.cbENSG7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG7.Size = new System.Drawing.Size(36, 20);
		this.cbENSG7.TabIndex = 6;
		this.cbENSG7.Text = "7";
		this.cbENSG7.UseVisualStyleBackColor = true;
		this.cbENSG6.AutoSize = true;
		this.cbENSG6.Location = new System.Drawing.Point(15, 127);
		this.cbENSG6.Name = "cbENSG6";
		this.cbENSG6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG6.Size = new System.Drawing.Size(36, 20);
		this.cbENSG6.TabIndex = 5;
		this.cbENSG6.Text = "6";
		this.cbENSG6.UseVisualStyleBackColor = true;
		this.cbENSG5.AutoSize = true;
		this.cbENSG5.Location = new System.Drawing.Point(15, 105);
		this.cbENSG5.Name = "cbENSG5";
		this.cbENSG5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG5.Size = new System.Drawing.Size(36, 20);
		this.cbENSG5.TabIndex = 4;
		this.cbENSG5.Text = "5";
		this.cbENSG5.UseVisualStyleBackColor = true;
		this.cbENSG4.AutoSize = true;
		this.cbENSG4.Location = new System.Drawing.Point(15, 83);
		this.cbENSG4.Name = "cbENSG4";
		this.cbENSG4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG4.Size = new System.Drawing.Size(36, 20);
		this.cbENSG4.TabIndex = 3;
		this.cbENSG4.Text = "4";
		this.cbENSG4.UseVisualStyleBackColor = true;
		this.cbENSG3.AutoSize = true;
		this.cbENSG3.Location = new System.Drawing.Point(15, 61);
		this.cbENSG3.Name = "cbENSG3";
		this.cbENSG3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG3.Size = new System.Drawing.Size(36, 20);
		this.cbENSG3.TabIndex = 2;
		this.cbENSG3.Text = "3";
		this.cbENSG3.UseVisualStyleBackColor = true;
		this.cbENSG2.AutoSize = true;
		this.cbENSG2.Location = new System.Drawing.Point(15, 39);
		this.cbENSG2.Name = "cbENSG2";
		this.cbENSG2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG2.Size = new System.Drawing.Size(36, 20);
		this.cbENSG2.TabIndex = 1;
		this.cbENSG2.Text = "2";
		this.cbENSG2.UseVisualStyleBackColor = true;
		this.cbENSG1.AutoSize = true;
		this.cbENSG1.Location = new System.Drawing.Point(15, 17);
		this.cbENSG1.Name = "cbENSG1";
		this.cbENSG1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG1.Size = new System.Drawing.Size(36, 20);
		this.cbENSG1.TabIndex = 0;
		this.cbENSG1.Text = "1";
		this.cbENSG1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.tbChannelName60);
		this.tabPage2.Controls.Add(this.tbChannelName59);
		this.tabPage2.Controls.Add(this.tbChannelName58);
		this.tabPage2.Controls.Add(this.tbChannelName57);
		this.tabPage2.Controls.Add(this.tbChannelName56);
		this.tabPage2.Controls.Add(this.tbChannelName55);
		this.tabPage2.Controls.Add(this.tbChannelName54);
		this.tabPage2.Controls.Add(this.tbChannelName53);
		this.tabPage2.Controls.Add(this.tbChannelName52);
		this.tabPage2.Controls.Add(this.tbChannelName51);
		this.tabPage2.Controls.Add(this.tbChannelName50);
		this.tabPage2.Controls.Add(this.tbChannelName49);
		this.tabPage2.Controls.Add(this.tbChannelName48);
		this.tabPage2.Controls.Add(this.tbChannelName47);
		this.tabPage2.Controls.Add(this.tbChannelName46);
		this.tabPage2.Controls.Add(this.cbENSG60);
		this.tabPage2.Controls.Add(this.cbENSG59);
		this.tabPage2.Controls.Add(this.cbENSG58);
		this.tabPage2.Controls.Add(this.cbENSG57);
		this.tabPage2.Controls.Add(this.cbENSG56);
		this.tabPage2.Controls.Add(this.cbENSG55);
		this.tabPage2.Controls.Add(this.cbENSG54);
		this.tabPage2.Controls.Add(this.cbENSG53);
		this.tabPage2.Controls.Add(this.cbENSG52);
		this.tabPage2.Controls.Add(this.cbENSG51);
		this.tabPage2.Controls.Add(this.cbENSG50);
		this.tabPage2.Controls.Add(this.cbENSG49);
		this.tabPage2.Controls.Add(this.cbENSG48);
		this.tabPage2.Controls.Add(this.cbENSG47);
		this.tabPage2.Controls.Add(this.cbENSG46);
		this.tabPage2.Controls.Add(this.tbChannelName45);
		this.tabPage2.Controls.Add(this.tbChannelName44);
		this.tabPage2.Controls.Add(this.tbChannelName43);
		this.tabPage2.Controls.Add(this.tbChannelName42);
		this.tabPage2.Controls.Add(this.tbChannelName41);
		this.tabPage2.Controls.Add(this.tbChannelName40);
		this.tabPage2.Controls.Add(this.tbChannelName39);
		this.tabPage2.Controls.Add(this.tbChannelName38);
		this.tabPage2.Controls.Add(this.tbChannelName37);
		this.tabPage2.Controls.Add(this.tbChannelName36);
		this.tabPage2.Controls.Add(this.tbChannelName35);
		this.tabPage2.Controls.Add(this.tbChannelName34);
		this.tabPage2.Controls.Add(this.tbChannelName33);
		this.tabPage2.Controls.Add(this.tbChannelName32);
		this.tabPage2.Controls.Add(this.tbChannelName31);
		this.tabPage2.Controls.Add(this.cbENSG45);
		this.tabPage2.Controls.Add(this.cbENSG44);
		this.tabPage2.Controls.Add(this.cbENSG43);
		this.tabPage2.Controls.Add(this.cbENSG42);
		this.tabPage2.Controls.Add(this.cbENSG41);
		this.tabPage2.Controls.Add(this.cbENSG40);
		this.tabPage2.Controls.Add(this.cbENSG39);
		this.tabPage2.Controls.Add(this.cbENSG38);
		this.tabPage2.Controls.Add(this.cbENSG37);
		this.tabPage2.Controls.Add(this.cbENSG36);
		this.tabPage2.Controls.Add(this.cbENSG35);
		this.tabPage2.Controls.Add(this.cbENSG34);
		this.tabPage2.Controls.Add(this.cbENSG33);
		this.tabPage2.Controls.Add(this.cbENSG32);
		this.tabPage2.Controls.Add(this.cbENSG31);
		this.tabPage2.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(422, 373);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "31~60#束管";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.tbChannelName60.Location = new System.Drawing.Point(303, 325);
		this.tbChannelName60.Name = "tbChannelName60";
		this.tbChannelName60.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName60.TabIndex = 119;
		this.tbChannelName59.Location = new System.Drawing.Point(303, 303);
		this.tbChannelName59.Name = "tbChannelName59";
		this.tbChannelName59.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName59.TabIndex = 118;
		this.tbChannelName58.Location = new System.Drawing.Point(303, 281);
		this.tbChannelName58.Name = "tbChannelName58";
		this.tbChannelName58.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName58.TabIndex = 117;
		this.tbChannelName57.Location = new System.Drawing.Point(303, 259);
		this.tbChannelName57.Name = "tbChannelName57";
		this.tbChannelName57.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName57.TabIndex = 116;
		this.tbChannelName56.Location = new System.Drawing.Point(303, 237);
		this.tbChannelName56.Name = "tbChannelName56";
		this.tbChannelName56.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName56.TabIndex = 115;
		this.tbChannelName55.Location = new System.Drawing.Point(303, 215);
		this.tbChannelName55.Name = "tbChannelName55";
		this.tbChannelName55.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName55.TabIndex = 114;
		this.tbChannelName54.Location = new System.Drawing.Point(303, 193);
		this.tbChannelName54.Name = "tbChannelName54";
		this.tbChannelName54.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName54.TabIndex = 113;
		this.tbChannelName53.Location = new System.Drawing.Point(303, 171);
		this.tbChannelName53.Name = "tbChannelName53";
		this.tbChannelName53.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName53.TabIndex = 112;
		this.tbChannelName52.Location = new System.Drawing.Point(303, 149);
		this.tbChannelName52.Name = "tbChannelName52";
		this.tbChannelName52.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName52.TabIndex = 111;
		this.tbChannelName51.Location = new System.Drawing.Point(303, 127);
		this.tbChannelName51.Name = "tbChannelName51";
		this.tbChannelName51.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName51.TabIndex = 110;
		this.tbChannelName50.Location = new System.Drawing.Point(303, 105);
		this.tbChannelName50.Name = "tbChannelName50";
		this.tbChannelName50.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName50.TabIndex = 109;
		this.tbChannelName49.Location = new System.Drawing.Point(303, 83);
		this.tbChannelName49.Name = "tbChannelName49";
		this.tbChannelName49.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName49.TabIndex = 108;
		this.tbChannelName48.Location = new System.Drawing.Point(303, 61);
		this.tbChannelName48.Name = "tbChannelName48";
		this.tbChannelName48.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName48.TabIndex = 107;
		this.tbChannelName47.Location = new System.Drawing.Point(303, 39);
		this.tbChannelName47.Name = "tbChannelName47";
		this.tbChannelName47.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName47.TabIndex = 106;
		this.tbChannelName46.Location = new System.Drawing.Point(303, 17);
		this.tbChannelName46.Name = "tbChannelName46";
		this.tbChannelName46.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName46.TabIndex = 105;
		this.cbENSG60.AutoSize = true;
		this.cbENSG60.Location = new System.Drawing.Point(248, 327);
		this.cbENSG60.Name = "cbENSG60";
		this.cbENSG60.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG60.Size = new System.Drawing.Size(45, 20);
		this.cbENSG60.TabIndex = 104;
		this.cbENSG60.Text = "60";
		this.cbENSG60.UseVisualStyleBackColor = true;
		this.cbENSG59.AutoSize = true;
		this.cbENSG59.Location = new System.Drawing.Point(248, 305);
		this.cbENSG59.Name = "cbENSG59";
		this.cbENSG59.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG59.Size = new System.Drawing.Size(45, 20);
		this.cbENSG59.TabIndex = 103;
		this.cbENSG59.Text = "59";
		this.cbENSG59.UseVisualStyleBackColor = true;
		this.cbENSG58.AutoSize = true;
		this.cbENSG58.Location = new System.Drawing.Point(248, 283);
		this.cbENSG58.Name = "cbENSG58";
		this.cbENSG58.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG58.Size = new System.Drawing.Size(45, 20);
		this.cbENSG58.TabIndex = 102;
		this.cbENSG58.Text = "58";
		this.cbENSG58.UseVisualStyleBackColor = true;
		this.cbENSG57.AutoSize = true;
		this.cbENSG57.Location = new System.Drawing.Point(248, 261);
		this.cbENSG57.Name = "cbENSG57";
		this.cbENSG57.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG57.Size = new System.Drawing.Size(45, 20);
		this.cbENSG57.TabIndex = 101;
		this.cbENSG57.Text = "57";
		this.cbENSG57.UseVisualStyleBackColor = true;
		this.cbENSG56.AutoSize = true;
		this.cbENSG56.Location = new System.Drawing.Point(248, 239);
		this.cbENSG56.Name = "cbENSG56";
		this.cbENSG56.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG56.Size = new System.Drawing.Size(45, 20);
		this.cbENSG56.TabIndex = 100;
		this.cbENSG56.Text = "56";
		this.cbENSG56.UseVisualStyleBackColor = true;
		this.cbENSG55.AutoSize = true;
		this.cbENSG55.Location = new System.Drawing.Point(248, 217);
		this.cbENSG55.Name = "cbENSG55";
		this.cbENSG55.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG55.Size = new System.Drawing.Size(45, 20);
		this.cbENSG55.TabIndex = 99;
		this.cbENSG55.Text = "55";
		this.cbENSG55.UseVisualStyleBackColor = true;
		this.cbENSG54.AutoSize = true;
		this.cbENSG54.Location = new System.Drawing.Point(248, 195);
		this.cbENSG54.Name = "cbENSG54";
		this.cbENSG54.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG54.Size = new System.Drawing.Size(45, 20);
		this.cbENSG54.TabIndex = 98;
		this.cbENSG54.Text = "54";
		this.cbENSG54.UseVisualStyleBackColor = true;
		this.cbENSG53.AutoSize = true;
		this.cbENSG53.Location = new System.Drawing.Point(248, 173);
		this.cbENSG53.Name = "cbENSG53";
		this.cbENSG53.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG53.Size = new System.Drawing.Size(45, 20);
		this.cbENSG53.TabIndex = 97;
		this.cbENSG53.Text = "53";
		this.cbENSG53.UseVisualStyleBackColor = true;
		this.cbENSG52.AutoSize = true;
		this.cbENSG52.Location = new System.Drawing.Point(248, 151);
		this.cbENSG52.Name = "cbENSG52";
		this.cbENSG52.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG52.Size = new System.Drawing.Size(45, 20);
		this.cbENSG52.TabIndex = 96;
		this.cbENSG52.Text = "52";
		this.cbENSG52.UseVisualStyleBackColor = true;
		this.cbENSG51.AutoSize = true;
		this.cbENSG51.Location = new System.Drawing.Point(248, 129);
		this.cbENSG51.Name = "cbENSG51";
		this.cbENSG51.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG51.Size = new System.Drawing.Size(45, 20);
		this.cbENSG51.TabIndex = 95;
		this.cbENSG51.Text = "51";
		this.cbENSG51.UseVisualStyleBackColor = true;
		this.cbENSG50.AutoSize = true;
		this.cbENSG50.Location = new System.Drawing.Point(248, 107);
		this.cbENSG50.Name = "cbENSG50";
		this.cbENSG50.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG50.Size = new System.Drawing.Size(45, 20);
		this.cbENSG50.TabIndex = 94;
		this.cbENSG50.Text = "50";
		this.cbENSG50.UseVisualStyleBackColor = true;
		this.cbENSG49.AutoSize = true;
		this.cbENSG49.Location = new System.Drawing.Point(248, 85);
		this.cbENSG49.Name = "cbENSG49";
		this.cbENSG49.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG49.Size = new System.Drawing.Size(45, 20);
		this.cbENSG49.TabIndex = 93;
		this.cbENSG49.Text = "49";
		this.cbENSG49.UseVisualStyleBackColor = true;
		this.cbENSG48.AutoSize = true;
		this.cbENSG48.Location = new System.Drawing.Point(248, 63);
		this.cbENSG48.Name = "cbENSG48";
		this.cbENSG48.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG48.Size = new System.Drawing.Size(45, 20);
		this.cbENSG48.TabIndex = 92;
		this.cbENSG48.Text = "48";
		this.cbENSG48.UseVisualStyleBackColor = true;
		this.cbENSG47.AutoSize = true;
		this.cbENSG47.Location = new System.Drawing.Point(248, 41);
		this.cbENSG47.Name = "cbENSG47";
		this.cbENSG47.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG47.Size = new System.Drawing.Size(45, 20);
		this.cbENSG47.TabIndex = 91;
		this.cbENSG47.Text = "47";
		this.cbENSG47.UseVisualStyleBackColor = true;
		this.cbENSG46.AutoSize = true;
		this.cbENSG46.Location = new System.Drawing.Point(248, 19);
		this.cbENSG46.Name = "cbENSG46";
		this.cbENSG46.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG46.Size = new System.Drawing.Size(45, 20);
		this.cbENSG46.TabIndex = 90;
		this.cbENSG46.Text = "46";
		this.cbENSG46.UseVisualStyleBackColor = true;
		this.tbChannelName45.Location = new System.Drawing.Point(59, 325);
		this.tbChannelName45.Name = "tbChannelName45";
		this.tbChannelName45.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName45.TabIndex = 89;
		this.tbChannelName44.Location = new System.Drawing.Point(59, 303);
		this.tbChannelName44.Name = "tbChannelName44";
		this.tbChannelName44.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName44.TabIndex = 88;
		this.tbChannelName43.Location = new System.Drawing.Point(59, 281);
		this.tbChannelName43.Name = "tbChannelName43";
		this.tbChannelName43.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName43.TabIndex = 87;
		this.tbChannelName42.Location = new System.Drawing.Point(59, 259);
		this.tbChannelName42.Name = "tbChannelName42";
		this.tbChannelName42.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName42.TabIndex = 86;
		this.tbChannelName41.Location = new System.Drawing.Point(59, 237);
		this.tbChannelName41.Name = "tbChannelName41";
		this.tbChannelName41.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName41.TabIndex = 85;
		this.tbChannelName40.Location = new System.Drawing.Point(59, 215);
		this.tbChannelName40.Name = "tbChannelName40";
		this.tbChannelName40.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName40.TabIndex = 84;
		this.tbChannelName39.Location = new System.Drawing.Point(59, 193);
		this.tbChannelName39.Name = "tbChannelName39";
		this.tbChannelName39.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName39.TabIndex = 83;
		this.tbChannelName38.Location = new System.Drawing.Point(59, 171);
		this.tbChannelName38.Name = "tbChannelName38";
		this.tbChannelName38.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName38.TabIndex = 82;
		this.tbChannelName37.Location = new System.Drawing.Point(59, 149);
		this.tbChannelName37.Name = "tbChannelName37";
		this.tbChannelName37.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName37.TabIndex = 81;
		this.tbChannelName36.Location = new System.Drawing.Point(59, 127);
		this.tbChannelName36.Name = "tbChannelName36";
		this.tbChannelName36.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName36.TabIndex = 80;
		this.tbChannelName35.Location = new System.Drawing.Point(59, 105);
		this.tbChannelName35.Name = "tbChannelName35";
		this.tbChannelName35.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName35.TabIndex = 79;
		this.tbChannelName34.Location = new System.Drawing.Point(59, 83);
		this.tbChannelName34.Name = "tbChannelName34";
		this.tbChannelName34.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName34.TabIndex = 78;
		this.tbChannelName33.Location = new System.Drawing.Point(59, 61);
		this.tbChannelName33.Name = "tbChannelName33";
		this.tbChannelName33.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName33.TabIndex = 77;
		this.tbChannelName32.Location = new System.Drawing.Point(59, 39);
		this.tbChannelName32.Name = "tbChannelName32";
		this.tbChannelName32.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName32.TabIndex = 76;
		this.tbChannelName31.Location = new System.Drawing.Point(59, 17);
		this.tbChannelName31.Name = "tbChannelName31";
		this.tbChannelName31.Size = new System.Drawing.Size(100, 26);
		this.tbChannelName31.TabIndex = 75;
		this.cbENSG45.AutoSize = true;
		this.cbENSG45.Location = new System.Drawing.Point(6, 327);
		this.cbENSG45.Name = "cbENSG45";
		this.cbENSG45.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG45.Size = new System.Drawing.Size(45, 20);
		this.cbENSG45.TabIndex = 74;
		this.cbENSG45.Text = "45";
		this.cbENSG45.UseVisualStyleBackColor = true;
		this.cbENSG44.AutoSize = true;
		this.cbENSG44.Location = new System.Drawing.Point(6, 305);
		this.cbENSG44.Name = "cbENSG44";
		this.cbENSG44.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG44.Size = new System.Drawing.Size(45, 20);
		this.cbENSG44.TabIndex = 73;
		this.cbENSG44.Text = "44";
		this.cbENSG44.UseVisualStyleBackColor = true;
		this.cbENSG43.AutoSize = true;
		this.cbENSG43.Location = new System.Drawing.Point(6, 283);
		this.cbENSG43.Name = "cbENSG43";
		this.cbENSG43.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG43.Size = new System.Drawing.Size(45, 20);
		this.cbENSG43.TabIndex = 72;
		this.cbENSG43.Text = "43";
		this.cbENSG43.UseVisualStyleBackColor = true;
		this.cbENSG42.AutoSize = true;
		this.cbENSG42.Location = new System.Drawing.Point(6, 261);
		this.cbENSG42.Name = "cbENSG42";
		this.cbENSG42.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG42.Size = new System.Drawing.Size(45, 20);
		this.cbENSG42.TabIndex = 71;
		this.cbENSG42.Text = "42";
		this.cbENSG42.UseVisualStyleBackColor = true;
		this.cbENSG41.AutoSize = true;
		this.cbENSG41.Location = new System.Drawing.Point(6, 239);
		this.cbENSG41.Name = "cbENSG41";
		this.cbENSG41.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG41.Size = new System.Drawing.Size(45, 20);
		this.cbENSG41.TabIndex = 70;
		this.cbENSG41.Text = "41";
		this.cbENSG41.UseVisualStyleBackColor = true;
		this.cbENSG40.AutoSize = true;
		this.cbENSG40.Location = new System.Drawing.Point(6, 217);
		this.cbENSG40.Name = "cbENSG40";
		this.cbENSG40.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG40.Size = new System.Drawing.Size(45, 20);
		this.cbENSG40.TabIndex = 69;
		this.cbENSG40.Text = "40";
		this.cbENSG40.UseVisualStyleBackColor = true;
		this.cbENSG39.AutoSize = true;
		this.cbENSG39.Location = new System.Drawing.Point(6, 195);
		this.cbENSG39.Name = "cbENSG39";
		this.cbENSG39.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG39.Size = new System.Drawing.Size(45, 20);
		this.cbENSG39.TabIndex = 68;
		this.cbENSG39.Text = "39";
		this.cbENSG39.UseVisualStyleBackColor = true;
		this.cbENSG38.AutoSize = true;
		this.cbENSG38.Location = new System.Drawing.Point(6, 173);
		this.cbENSG38.Name = "cbENSG38";
		this.cbENSG38.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG38.Size = new System.Drawing.Size(45, 20);
		this.cbENSG38.TabIndex = 67;
		this.cbENSG38.Text = "38";
		this.cbENSG38.UseVisualStyleBackColor = true;
		this.cbENSG37.AutoSize = true;
		this.cbENSG37.Location = new System.Drawing.Point(6, 151);
		this.cbENSG37.Name = "cbENSG37";
		this.cbENSG37.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG37.Size = new System.Drawing.Size(45, 20);
		this.cbENSG37.TabIndex = 66;
		this.cbENSG37.Text = "37";
		this.cbENSG37.UseVisualStyleBackColor = true;
		this.cbENSG36.AutoSize = true;
		this.cbENSG36.Location = new System.Drawing.Point(6, 129);
		this.cbENSG36.Name = "cbENSG36";
		this.cbENSG36.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG36.Size = new System.Drawing.Size(45, 20);
		this.cbENSG36.TabIndex = 65;
		this.cbENSG36.Text = "36";
		this.cbENSG36.UseVisualStyleBackColor = true;
		this.cbENSG35.AutoSize = true;
		this.cbENSG35.Location = new System.Drawing.Point(6, 107);
		this.cbENSG35.Name = "cbENSG35";
		this.cbENSG35.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG35.Size = new System.Drawing.Size(45, 20);
		this.cbENSG35.TabIndex = 64;
		this.cbENSG35.Text = "35";
		this.cbENSG35.UseVisualStyleBackColor = true;
		this.cbENSG34.AutoSize = true;
		this.cbENSG34.Location = new System.Drawing.Point(6, 85);
		this.cbENSG34.Name = "cbENSG34";
		this.cbENSG34.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG34.Size = new System.Drawing.Size(45, 20);
		this.cbENSG34.TabIndex = 63;
		this.cbENSG34.Text = "34";
		this.cbENSG34.UseVisualStyleBackColor = true;
		this.cbENSG33.AutoSize = true;
		this.cbENSG33.Location = new System.Drawing.Point(6, 63);
		this.cbENSG33.Name = "cbENSG33";
		this.cbENSG33.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG33.Size = new System.Drawing.Size(45, 20);
		this.cbENSG33.TabIndex = 62;
		this.cbENSG33.Text = "33";
		this.cbENSG33.UseVisualStyleBackColor = true;
		this.cbENSG32.AutoSize = true;
		this.cbENSG32.Location = new System.Drawing.Point(6, 41);
		this.cbENSG32.Name = "cbENSG32";
		this.cbENSG32.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG32.Size = new System.Drawing.Size(45, 20);
		this.cbENSG32.TabIndex = 61;
		this.cbENSG32.Text = "32";
		this.cbENSG32.UseVisualStyleBackColor = true;
		this.cbENSG31.AutoSize = true;
		this.cbENSG31.Location = new System.Drawing.Point(6, 19);
		this.cbENSG31.Name = "cbENSG31";
		this.cbENSG31.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.cbENSG31.Size = new System.Drawing.Size(45, 20);
		this.cbENSG31.TabIndex = 60;
		this.cbENSG31.Text = "31";
		this.cbENSG31.UseVisualStyleBackColor = true;
		this.btnSGset.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.btnSGset.Location = new System.Drawing.Point(275, 548);
		this.btnSGset.Name = "btnSGset";
		this.btnSGset.Size = new System.Drawing.Size(164, 92);
		this.btnSGset.TabIndex = 2;
		this.btnSGset.Text = "设定";
		this.btnSGset.UseVisualStyleBackColor = true;
		this.btnSGset.Click += new System.EventHandler(btnSGset_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(454, 669);
		base.Controls.Add(this.btnSGset);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.panel1);
		base.Name = "FormSGSet";
		this.Text = "束管编号与检测选择";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		base.ResumeLayout(false);
	}
}
