using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FrmChromatManager : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public string PathSunAquip = Application.StartupPath + "\\saq.cfg";

	public ChromDeviceListMgr SunAquips = ChromDeviceListMgr.Create();

	private IContainer icontainer_0;

	internal SplitContainer SplitContainer1;

	internal TreeView AquipTree;

	internal Button Button6;

	internal TabControl TabControl1;

	internal TabPage TabPage1;

	internal GroupBox GroupBox1;

	internal TextBox AquipID1;

	internal TextBox TMore;

	internal Label LTip;

	internal Label Label30;

	internal TextBox TDepartment;

	internal Label Label28;

	internal TextBox TName;

	internal Label Label3;

	internal Label Label2;

	internal Label Label1;

	internal Button Button4;

	private ImageList imageList_0;

	private NumericUpDown numericUpDown1;

	internal Label label4;

	private IContainer components;

	internal Label label5;

	public FrmChromatManager()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	public void Button4_Click(object sender, EventArgs e)
	{
		string strGCID = AquipID1.Text.Trim();
		ChromDevice chromDevice = SunAquips.GetChromDevice(strGCID);
		if (chromDevice == null)
		{
			ChromDeviceInfo myinfo = new ChromDeviceInfo(AquipID1.Text, TName.Text, TDepartment.Text, TMore.Text, (int)numericUpDown1.Value);
			chromDevice = new ChromDevice(myinfo, 4);
			SunAquips.Add(chromDevice);
		}
		else
		{
			chromDevice.info.DepartMent = TDepartment.Text;
			chromDevice.info.Name = TName.Text;
			chromDevice.info.Other = TMore.Text;
			chromDevice.info.ModBusDeviceID = (int)numericUpDown1.Value;
		}
		cdlMgr.SaveWorkSunFile();
		RefreshTree();
		cdlMgr.formMain.chrDeviceCtrl.RefrushChromList();
	}

	public bool LoadFromFile()
	{
		if (SunAquips.Count > 0)
		{
			AquipID1.Text = SunAquips[0].info.ID;
			TDepartment.Text = SunAquips[0].info.DepartMent;
			TName.Text = SunAquips[0].info.Name;
			TMore.Text = SunAquips[0].info.Other;
			numericUpDown1.Value = SunAquips[0].info.ModBusDeviceID;
		}
		return true;
	}

	public void SaveToFile(string fileName)
	{
		SaveToFileB(fileName);
	}

	public void SaveToFileB(string fileName)
	{
		Program.WriteLine("重新存储");
		for (int i = 0; i < SunAquips.Count; i++)
		{
		}
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_, out fileStream_, out binaryWriter_);
			binaryWriter_.Write("IBrainChrom");
			binaryWriter_.Write(DateTime.Now.ToString());
			binaryWriter_.Write(SunAquips.Count);
			for (int j = 0; j < SunAquips.Count; j++)
			{
				try
				{
					binaryWriter_.Write(SunAquips[j].info.ID);
					binaryWriter_.Write(SunAquips[j].info.ModBusDeviceID);
					binaryWriter_.Write(SunAquips[j].info.DepartMent);
					binaryWriter_.Write(SunAquips[j].info.Name);
					binaryWriter_.Write(SunAquips[j].info.Other);
					binaryWriter_.Write(SunAquips[j].misMgr.devManager.Msg.AutoSendByStopTime);
					if (SunAquips[j].misMgr.devManager.Msg.Mess == null)
					{
						SunAquips[j].misMgr.devManager.Msg.Mess = "";
					}
					binaryWriter_.Write(SunAquips[j].misMgr.devManager.Msg.Mess);
					binaryWriter_.Write(SunAquips[j].misMgr.devManager.Msg.sound);
					if (SunAquips[j].misMgr.devManager.Msg.soundTimes <= 0)
					{
						SunAquips[j].misMgr.devManager.Msg.soundTimes = 0;
					}
					binaryWriter_.Write(SunAquips[j].misMgr.devManager.Msg.soundTimes);
					for (int k = 0; k < SunAquips[j].misMgr.ChannelChartParaS.Count; k++)
					{
						binaryWriter_.Write((int)SunAquips[j].misMgr.ChannelChartParaS[k].cnlBasisQuantity);
						binaryWriter_.Write((int)SunAquips[j].misMgr.ChannelChartParaS[k].cnlDetectMethod);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].fullScreenTime);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].bClearZero);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].printWhenStop);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].analysisWhenStop);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].bFullScreen);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].stopTime);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].showHighLimit);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].showLowLimit);
						binaryWriter_.Write(SunAquips[j].misMgr.ChannelChartParaS[k].bBaselineDeduction);
					}
					for (int l = 0; l < SunAquips[j].misMgr.ChartParaOperaS.Count; l++)
					{
						if (SunAquips[j].misMgr.ChartParaOperaS[l].mtdMgr.printPara == null)
						{
							SunAquips[j].misMgr.ChartParaOperaS[l].mtdMgr.printPara = new PrintPara();
						}
						SunAquips[j].misMgr.ChartParaOperaS[l].mtdMgr.printPara.WriteToFile(binaryWriter_);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].TemplatePath);
						binaryWriter_.Write((int)SunAquips[j].misMgr.ChartParaOperaS[l].cnlDetectMethod);
						binaryWriter_.Write((int)SunAquips[j].misMgr.ChartParaOperaS[l].cnlBasisQuantity);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].FileNameAquipName);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].FileNameAutoInject);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].FileNameChannelName);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].FileNameDateTime);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].InjectIndex);
						if (SunAquips[j].misMgr.ChartParaOperaS[l].FileUserSet == null)
						{
							SunAquips[j].misMgr.ChartParaOperaS[l].FileUserSet = "";
						}
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].FileUserSet);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].UseUserZeroTime);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].ZeroTime);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].ZeroTimeLeft);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].ZeroTimeRight);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList.Count);
						for (int m = 0; m < SunAquips[j].misMgr.ChartParaOperaS[l].componentList.Count; m++)
						{
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].JuseTimeCheck);
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].JStdandPeakTime);
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].JTimePara);
							if (SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].name == null)
							{
								SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].name = "";
							}
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].name);
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].JPeakAdjustPara);
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].componentList[m].JModBusAddr);
						}
						if (SunAquips[j].misMgr.ChartParaOperaS[l].Integ == null)
						{
							SunAquips[j].misMgr.ChartParaOperaS[l].Integ = new Integration();
						}
						SunAquips[j].misMgr.ChartParaOperaS[l].Integ.SaveToFile(binaryWriter_);
						if (SunAquips[j].misMgr.ChartParaOperaS[l].mtdMgr == null)
						{
							SunAquips[j].misMgr.ChartParaOperaS[l].mtdMgr = new MtdSetup();
						}
						SunAquips[j].misMgr.ChartParaOperaS[l].mtdMgr.SaveToFile(binaryWriter_);
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].tProgram.Count);
						for (int n = 0; n < SunAquips[j].misMgr.ChartParaOperaS[l].tProgram.Count; n++)
						{
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].tProgram[n].TimeValue);
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].tProgram[n].TestCard);
						}
						binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].evenPara.Count);
						for (int num = 0; num < SunAquips[j].misMgr.ChartParaOperaS[l].evenPara.Count; num++)
						{
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].evenPara[num].TimeStart);
							binaryWriter_.Write(SunAquips[j].misMgr.ChartParaOperaS[l].evenPara[num].TimeEnd);
						}
					}
				}
				catch (Exception)
				{
				}
			}
			binaryWriter_.Write("--End--");
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
		}
	}

	public void RefreshTree()
	{
		AquipTree.Nodes.Clear();
		TreeNode treeNode = new TreeNode(Lang.PS("色谱机管理", "Instrument Management"), 0, 0);
		AquipTree.Nodes.Add(treeNode);
		if (SunAquips == null)
		{
			return;
		}
		for (int i = 0; i < SunAquips.Count; i++)
		{
			TreeNode treeNode2 = new TreeNode(SunAquips[i].info.Name, 1, 1);
			treeNode2.Tag = SunAquips[i].info.ID;
			treeNode.Nodes.Add(treeNode2);
			if (i == 0)
			{
				AquipTree.SelectedNode = treeNode2;
			}
		}
		AquipTree.ExpandAll();
	}

	private void AquipTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (e.Node.Tag != null && e.Node.Tag.ToString() == SunAquips[i].info.ID)
			{
				AquipID1.Text = SunAquips[i].info.ID;
				TDepartment.Text = SunAquips[i].info.DepartMent;
				TName.Text = SunAquips[i].info.Name;
				TMore.Text = SunAquips[i].info.Other;
				numericUpDown1.Value = SunAquips[i].info.ModBusDeviceID;
			}
		}
	}

	private void AquipTree_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (e.Node.Tag == null)
		{
			return;
		}
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (!(e.Node.Tag.ToString() == SunAquips[i].info.ID))
			{
				continue;
			}
			try
			{
				AquipID1.Text = SunAquips[i].info.ID;
				TDepartment.Text = SunAquips[i].info.DepartMent;
				TName.Text = SunAquips[i].info.Name;
				TMore.Text = SunAquips[i].info.Other;
				if (SunAquips[i].info.ModBusDeviceID > 0)
				{
					numericUpDown1.Value = SunAquips[i].info.ModBusDeviceID;
				}
			}
			catch (Exception ex)
			{
				LogMgr.Instance.LogError("logerr 2  LoadWorkSunFile" + ex.Message);
			}
		}
	}

	public string GetNameByID(string ID)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID && SunAquips[i].info.Name.Trim() != "")
			{
				return SunAquips[i].info.Name;
			}
		}
		return ID;
	}

	public int GetModBusDeviceIDByEquipID(string ID)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				return SunAquips[i].info.ModBusDeviceID;
			}
		}
		return -1;
	}

	public Integration GetRunningInteg(string ID, int Channelj)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID && SunAquips[i].misMgr.ChartParaOperaS[Channelj].mtdMgr.sigIntegrations.Count != 0)
			{
				return SunAquips[i].misMgr.ChartParaOperaS[Channelj].mtdMgr.sigIntegrations[0];
			}
		}
		return null;
	}

	public MtdSetup Getmethod(string ID, int Channelj)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				return SunAquips[i].misMgr.ChartParaOperaS[Channelj].mtdMgr;
			}
		}
		return null;
	}

	public ChartParaOpera GetDEVICE(string ID, int Channelj)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID && Channelj < SunAquips[i].misMgr.ChartParaOperaS.Count)
			{
				return SunAquips[i].misMgr.ChartParaOperaS[Channelj];
			}
		}
		return new ChartParaOpera();
	}

	public ChannelChartPara GetOneEquipPara(string ID, int ChannelIndex)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				return SunAquips[i].misMgr.GetChannelChartPara(ChannelIndex);
			}
		}
		return new ChannelChartPara();
	}

	public ChromDevice GetOneEquip(string ID)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				return SunAquips[i];
			}
		}
		return new ChromDevice();
	}

	public ChartParaOpera GetOneEquipSinglePara(string ID, int ChannelIndex)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				return SunAquips[i].misMgr.GetChartParaOpera(ChannelIndex);
			}
		}
		return new ChartParaOpera();
	}

	private void Button6_Click(object sender, EventArgs e)
	{
		string text = Lang.PS("确认删除:", "Confirm Delete?");
		string caption = Lang.PS("删除色谱机", "Delete chromatography machine");
		if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			string text2 = AquipID1.Text.Trim();
			SunAquips.Remove(text2);
			Program.WriteLine("del:" + text2);
			cdlMgr.SaveWorkSunFile();
			RefreshTree();
		}
	}

	private void FrmChromatManager_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	public void AddAquip(string ID)
	{
		SunAquips.Add(ID);
		cdlMgr.SaveWorkSunFile();
	}

	public void SelectNodeByID(string ID)
	{
		if (AquipTree.Nodes.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < AquipTree.Nodes[0].Nodes.Count; i++)
		{
			if ((string)AquipTree.Nodes[0].Nodes[i].Tag == ID)
			{
				AquipTree.SelectedNode = AquipTree.Nodes[0].Nodes[i];
				break;
			}
		}
	}

	public void FrmChromatManager_Load(object sender, EventArgs e)
	{
		LoadFromFile();
		RefreshTree();
		method_2();
	}

	private void method_2()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "色谱机管理";
			TabControl1.TabPages[0].Text = "色谱机信息";
			GroupBox1.Text = "色谱机信息";
			Label1.Text = "设备标示符(ID号)：";
			Label2.Text = "标示符格式及长度规定：";
			label4.Text = "设备序号";
			Label3.Text = "设备名称（助记符）：";
			Label28.Text = "所属单位部门：";
			LTip.Text = "设定参数提示：";
			break;
		case SysLanguage.EN:
			Text = "Chromatography machine management";
			TabControl1.TabPages[0].Text = "Chromatography Info";
			GroupBox1.Text = "Chromatography Info";
			Label1.Text = "ID：";
			Label2.Text = "Identifier specified format and length:";
			label4.Text = "Index";
			Label3.Text = "Name:";
			Label28.Text = "The Department:";
			TName.Text = "TestMachine";
			TDepartment.Text = "";
			TMore.Text = "";
			Label30.Text = "ReMark";
			LTip.Text = "Set parameters Tips：";
			Button4.Text = "Save";
			Button6.Text = "Delete";
			break;
		}
	}

	public bool UpdateOneEquipPara(string ID, int ChannelIndex, ChannelChartPara TempCP)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				SunAquips[i].misMgr.ChannelChartParaS[ChannelIndex] = TempCP;
				return true;
			}
		}
		return false;
	}

	public bool UpdateOneChannelChartPara(string ID, int ChannelIndex, ChartParaOpera TempP)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				SunAquips[i].misMgr.ChartParaOperaS[ChannelIndex] = TempP;
				return true;
			}
		}
		return false;
	}

	public bool UpdateMess(string ID, bool Asend, string mess, bool sound, int stimes)
	{
		for (int i = 0; i < SunAquips.Count; i++)
		{
			if (ID == SunAquips[i].info.ID)
			{
				SunAquips[i].misMgr.devManager.Msg.AutoSendByStopTime = Asend;
				SunAquips[i].misMgr.devManager.Msg.Mess = mess;
				SunAquips[i].misMgr.devManager.Msg.sound = sound;
				SunAquips[i].misMgr.devManager.Msg.soundTimes = stimes;
				return true;
			}
		}
		return false;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FrmChromatManager));
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.AquipTree = new System.Windows.Forms.TreeView();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
		this.Button6 = new System.Windows.Forms.Button();
		this.TabControl1 = new System.Windows.Forms.TabControl();
		this.TabPage1 = new System.Windows.Forms.TabPage();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.AquipID1 = new System.Windows.Forms.TextBox();
		this.TMore = new System.Windows.Forms.TextBox();
		this.LTip = new System.Windows.Forms.Label();
		this.Label30 = new System.Windows.Forms.Label();
		this.TDepartment = new System.Windows.Forms.TextBox();
		this.Label28 = new System.Windows.Forms.Label();
		this.TName = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.Button4 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		this.TabControl1.SuspendLayout();
		this.TabPage1.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		base.SuspendLayout();
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.AquipTree);
		this.SplitContainer1.Panel2.Controls.Add(this.Button6);
		this.SplitContainer1.Panel2.Controls.Add(this.TabControl1);
		this.SplitContainer1.Panel2.Controls.Add(this.Button4);
		this.SplitContainer1.Size = new System.Drawing.Size(666, 397);
		this.SplitContainer1.SplitterDistance = 189;
		this.SplitContainer1.TabIndex = 4;
		this.AquipTree.Dock = System.Windows.Forms.DockStyle.Fill;
		this.AquipTree.ImageIndex = 0;
		this.AquipTree.ImageList = this.imageList_0;
		this.AquipTree.Location = new System.Drawing.Point(0, 0);
		this.AquipTree.Name = "AquipTree";
		this.AquipTree.SelectedImageIndex = 0;
		this.AquipTree.Size = new System.Drawing.Size(189, 397);
		this.AquipTree.TabIndex = 0;
		this.AquipTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(AquipTree_AfterSelect);
		this.AquipTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(AquipTree_NodeMouseDoubleClick);
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_0.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "YBICO.ico");
		this.imageList_0.Images.SetKeyName(1, "201071655882501.jpg");
		this.Button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Button6.Image = (System.Drawing.Image)resources.GetObject("Button6.Image");
		this.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.Button6.Location = new System.Drawing.Point(348, 343);
		this.Button6.Name = "Button6";
		this.Button6.Size = new System.Drawing.Size(70, 39);
		this.Button6.TabIndex = 20;
		this.Button6.Text = "删除";
		this.Button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Button6.UseVisualStyleBackColor = true;
		this.Button6.Click += new System.EventHandler(Button6_Click);
		this.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabControl1.Controls.Add(this.TabPage1);
		this.TabControl1.Location = new System.Drawing.Point(11, 12);
		this.TabControl1.Name = "TabControl1";
		this.TabControl1.SelectedIndex = 0;
		this.TabControl1.Size = new System.Drawing.Size(437, 325);
		this.TabControl1.TabIndex = 18;
		this.TabPage1.Controls.Add(this.GroupBox1);
		this.TabPage1.ImageIndex = 1;
		this.TabPage1.Location = new System.Drawing.Point(4, 22);
		this.TabPage1.Name = "TabPage1";
		this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.TabPage1.Size = new System.Drawing.Size(429, 299);
		this.TabPage1.TabIndex = 0;
		this.TabPage1.Text = "色谱机信息";
		this.TabPage1.UseVisualStyleBackColor = true;
		this.GroupBox1.Controls.Add(this.numericUpDown1);
		this.GroupBox1.Controls.Add(this.AquipID1);
		this.GroupBox1.Controls.Add(this.TMore);
		this.GroupBox1.Controls.Add(this.LTip);
		this.GroupBox1.Controls.Add(this.Label30);
		this.GroupBox1.Controls.Add(this.TDepartment);
		this.GroupBox1.Controls.Add(this.Label28);
		this.GroupBox1.Controls.Add(this.TName);
		this.GroupBox1.Controls.Add(this.label4);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.label5);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(3, 3);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(423, 293);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "色谱机信息";
		this.numericUpDown1.Location = new System.Drawing.Point(136, 75);
		this.numericUpDown1.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Size = new System.Drawing.Size(87, 21);
		this.numericUpDown1.TabIndex = 27;
		this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.AquipID1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.AquipID1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.AquipID1.Location = new System.Drawing.Point(139, 27);
		this.AquipID1.MaxLength = 24;
		this.AquipID1.Name = "AquipID1";
		this.AquipID1.ReadOnly = true;
		this.AquipID1.Size = new System.Drawing.Size(230, 21);
		this.AquipID1.TabIndex = 26;
		this.AquipID1.Text = "1234";
		this.TMore.Location = new System.Drawing.Point(137, 176);
		this.TMore.Name = "TMore";
		this.TMore.Size = new System.Drawing.Size(232, 21);
		this.TMore.TabIndex = 10;
		this.TMore.Text = "备注信息";
		this.LTip.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LTip.AutoSize = true;
		this.LTip.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.LTip.Location = new System.Drawing.Point(9, 239);
		this.LTip.Name = "LTip";
		this.LTip.Size = new System.Drawing.Size(89, 12);
		this.LTip.TabIndex = 19;
		this.LTip.Text = "设定参数提示：";
		this.Label30.AutoSize = true;
		this.Label30.Location = new System.Drawing.Point(9, 179);
		this.Label30.Name = "Label30";
		this.Label30.Size = new System.Drawing.Size(65, 12);
		this.Label30.TabIndex = 9;
		this.Label30.Text = "备注信息：";
		this.TDepartment.Location = new System.Drawing.Point(137, 140);
		this.TDepartment.Name = "TDepartment";
		this.TDepartment.Size = new System.Drawing.Size(232, 21);
		this.TDepartment.TabIndex = 7;
		this.TDepartment.Text = "****部";
		this.Label28.AutoSize = true;
		this.Label28.Location = new System.Drawing.Point(9, 143);
		this.Label28.Name = "Label28";
		this.Label28.Size = new System.Drawing.Size(89, 12);
		this.Label28.TabIndex = 5;
		this.Label28.Text = "所属单位部门：";
		this.TName.Location = new System.Drawing.Point(137, 103);
		this.TName.Name = "TName";
		this.TName.Size = new System.Drawing.Size(232, 21);
		this.TName.TabIndex = 4;
		this.TName.Text = "测试色谱机1";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 73);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 12);
		this.label4.TabIndex = 3;
		this.label4.Text = "设备序号：";
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(6, 106);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(125, 12);
		this.Label3.TabIndex = 3;
		this.Label3.Text = "设备名称（助记符）：";
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.label5.Location = new System.Drawing.Point(229, 79);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(101, 12);
		this.label5.TabIndex = 2;
		this.label5.Text = "ModBus Device ID";
		this.Label2.AutoSize = true;
		this.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.Label2.Location = new System.Drawing.Point(140, 56);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(137, 12);
		this.Label2.TabIndex = 2;
		this.Label2.Text = "标示符格式及长度规定：";
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(4, 29);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(113, 12);
		this.Label1.TabIndex = 0;
		this.Label1.Text = "设备标示符(ID号)：";
		this.Button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Button4.Image = (System.Drawing.Image)resources.GetObject("Button4.Image");
		this.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.Button4.Location = new System.Drawing.Point(258, 343);
		this.Button4.Name = "Button4";
		this.Button4.Size = new System.Drawing.Size(70, 39);
		this.Button4.TabIndex = 17;
		this.Button4.Text = "保存";
		this.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Button4.UseVisualStyleBackColor = true;
		this.Button4.Click += new System.EventHandler(Button4_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(666, 397);
		base.Controls.Add(this.SplitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FrmChromatManager";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "色谱机管理";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmChromatManager_FormClosing);
		base.Load += new System.EventHandler(FrmChromatManager_Load);
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.TabControl1.ResumeLayout(false);
		this.TabPage1.ResumeLayout(false);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		base.ResumeLayout(false);
	}
}
