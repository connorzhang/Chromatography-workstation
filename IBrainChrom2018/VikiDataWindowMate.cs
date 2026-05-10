using System;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class VikiDataWindowMate
{
	private byte[] myDataBuff;

	private int myDataSize;

	public byte[] DataBuff => myDataBuff;

	public short DataSize => (short)myDataSize;

	public VikiDataWindowMate()
	{
		int num = 30720;
		myDataBuff = new byte[num];
		myDataSize = 0;
	}

	public VikiDataWindowMate(int iDataBuffSize)
	{
		myDataBuff = new byte[iDataBuffSize];
		myDataSize = 0;
	}

	~VikiDataWindowMate()
	{
		myDataBuff = null;
	}

	public int AppendBlock(byte[] srcDataBuff, int nSrcDataSize)
	{
		lock (myDataBuff)
		{
			if (myDataSize + nSrcDataSize <= myDataBuff.Length)
			{
				Array.Copy(srcDataBuff, 0, myDataBuff, myDataSize, nSrcDataSize);
				myDataSize += nSrcDataSize;
			}
			else
			{
				LogMgr.Instance.Write2RunLog($"尚余太多的数据({myDataSize}Bytes + {nSrcDataSize}Bytes)没有解析或无法解析，这可能是非法连接或网络故障导致。\n请尽快与软件提供商联系！");
				Array.Copy(srcDataBuff, 0, myDataBuff, 0, nSrcDataSize);
				myDataSize = nSrcDataSize;
			}
		}
		return myDataSize;
	}

	public int MoveData(int nFromOffset)
	{
		lock (myDataBuff)
		{
			if (nFromOffset > 0 && nFromOffset < myDataSize)
			{
				Array.Copy(myDataBuff, nFromOffset, myDataBuff, 0, myDataSize - nFromOffset);
				myDataSize -= nFromOffset;
				Array.Clear(myDataBuff, myDataSize, myDataBuff.Length - myDataSize);
			}
		}
		return myDataSize;
	}

	public void Clean()
	{
		myDataSize = 0;
		myDataBuff = new byte[myDataBuff.Length];
	}
}
