using System;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class WinInfo
{
	public BinaryReader binaryReader_0;

	public BinaryWriter binaryWriter_0;

	public Injection dftInj;

	private GvColumnsManager[] gvColumnsManager_0 = new GvColumnsManager[0];

	public bool hasS;

	public int height;

	public int left;

	public int para1;

	public string string_0;

	public string string_1;

	public string string_2;

	public int int_0;

	public bool visible;

	public int width;

	public FormWindowState windowState;

	public void gvCF_r(LclGridView lclGridView_0)
	{
		lclGridView_0.SaveToManager();
		if (lclGridView_0 is LclSummaryGridView)
		{
			LclSummaryGridView lclSummaryGridView = lclGridView_0 as LclSummaryGridView;
			int num = gvColumnsManager_0.Length;
			Array.Resize(ref gvColumnsManager_0, num + 4);
			gvColumnsManager_0[num] = new GvColumnsManager();
			gvColumnsManager_0[num++].LoadFromObject(lclSummaryGridView.gvComManager);
			gvColumnsManager_0[num] = new GvColumnsManager();
			gvColumnsManager_0[num++].LoadFromObject(lclSummaryGridView.gvGnlManager);
			gvColumnsManager_0[num] = new GvColumnsManager();
			gvColumnsManager_0[num++].LoadFromObject(lclSummaryGridView.gvGpcManager);
			gvColumnsManager_0[num] = new GvColumnsManager();
			gvColumnsManager_0[num++].LoadFromObject(lclSummaryGridView.gvDadManager);
		}
		else
		{
			int num = gvColumnsManager_0.Length;
			Array.Resize(ref gvColumnsManager_0, num + 1);
			gvColumnsManager_0[num] = new GvColumnsManager();
			gvColumnsManager_0[num].LoadFromObject(lclGridView_0.gvColumnsManager);
		}
	}

	public void gvCF_w(LclGridView lclGridView_0, ref int gvNo)
	{
		if (gvNo < gvColumnsManager_0.Length)
		{
			if (lclGridView_0 is LclSummaryGridView)
			{
				LclSummaryGridView lclSummaryGridView = lclGridView_0 as LclSummaryGridView;
				lclSummaryGridView.gvComManager.LoadFromObject(gvColumnsManager_0[gvNo++]);
				lclSummaryGridView.gvGnlManager.LoadFromObject(gvColumnsManager_0[gvNo++]);
				lclSummaryGridView.gvGpcManager.LoadFromObject(gvColumnsManager_0[gvNo++]);
				lclSummaryGridView.gvDadManager.LoadFromObject(gvColumnsManager_0[gvNo++]);
			}
			else
			{
				lclGridView_0.gvColumnsManager.LoadFromObject(gvColumnsManager_0[gvNo++]);
			}
			lclGridView_0.LoadFromManager();
		}
	}

	public void Load(BinaryReader binaryReader_1)
	{
		binaryReader_0 = binaryReader_1;
		visible = binaryReader_1.ReadBoolean();
		windowState = (FormWindowState)binaryReader_1.ReadByte();
		left = binaryReader_1.ReadInt32();
		int_0 = binaryReader_1.ReadInt32();
		width = binaryReader_1.ReadInt32();
		height = binaryReader_1.ReadInt32();
		para1 = binaryReader_1.ReadInt32();
		hasS = binaryReader_1.ReadBoolean();
		if (hasS)
		{
			string_0 = binaryReader_1.ReadString();
			string_1 = binaryReader_1.ReadString();
			string_2 = binaryReader_1.ReadString();
		}
		if (binaryReader_1.ReadBoolean())
		{
			dftInj = new Injection();
			dftInj.LoadFromFile(binaryReader_1);
		}
		Array.Resize(ref gvColumnsManager_0, binaryReader_1.ReadInt32());
		for (int i = 0; i < gvColumnsManager_0.Length; i++)
		{
			gvColumnsManager_0[i] = new GvColumnsManager();
			gvColumnsManager_0[i].Load(binaryReader_1);
		}
	}

	public void ReadFromForm(Form form)
	{
		Array.Resize(ref gvColumnsManager_0, 0);
		(form as LclGnlForm).WriteWinInfo(this);
	}

	public void Save(BinaryWriter binaryWriter_1)
	{
		binaryWriter_0 = binaryWriter_1;
		binaryWriter_1.Write(visible);
		binaryWriter_1.Write((byte)windowState);
		binaryWriter_1.Write(left);
		binaryWriter_1.Write(int_0);
		binaryWriter_1.Write(width);
		binaryWriter_1.Write(height);
		binaryWriter_1.Write(para1);
		binaryWriter_1.Write(hasS);
		if (hasS)
		{
			binaryWriter_1.Write(string_0);
			binaryWriter_1.Write(string_1);
			binaryWriter_1.Write(string_2);
		}
		bool flag = dftInj != null;
		binaryWriter_1.Write(flag);
		if (flag)
		{
			dftInj.SaveToFile(binaryWriter_1);
		}
		binaryWriter_1.Write(gvColumnsManager_0.Length);
		for (int i = 0; i < gvColumnsManager_0.Length; i++)
		{
			gvColumnsManager_0[i].Save(binaryWriter_1);
		}
	}
}
