using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class HWSendData
{
	private static HWSendData myself = null;

	[DllImport("HWSendData.dll")]
	private static extern void SendDataToHW(int data1, int data2, int data3);

	private HWSendData()
	{
	}

	public static HWSendData Create()
	{
		if (myself == null)
		{
			myself = new HWSendData();
		}
		return myself;
	}

	public void Start(int channel = 0)
	{
		if ((channel & 1) > 0)
		{
			SendDataToHW(1, 0, 1);
		}
		if ((channel & 2) > 0)
		{
			SendDataToHW(0, 1, 1);
		}
		if ((channel & 4) > 0)
		{
			SendDataToHW(1, 0, 10);
		}
	}

	public void Stop(int channel = 0)
	{
		if ((channel & 1) > 0)
		{
			SendDataToHW(1, 0, 2);
		}
		if ((channel & 2) > 0)
		{
			SendDataToHW(0, 1, 2);
		}
		if ((channel & 4) > 0)
		{
			SendDataToHW(0, 1, 10);
		}
	}

	public void Clear(int channel = 0)
	{
		if ((channel & 1) > 0)
		{
			SendDataToHW(1, 0, 5);
		}
		if ((channel & 2) > 0)
		{
			SendDataToHW(0, 1, 5);
		}
		if ((channel & 4) > 0)
		{
			MessageBox.Show("尚不支持该指令!");
		}
	}

	public void SendDataAB(int a, int b)
	{
		if (a != 0 && b != 0)
		{
			SendDataToHW(a, b, 0);
		}
		else if (a != 0)
		{
			SendDataToHW(a, 0, 3);
			SendDataToHW(a, 0, 4);
		}
		else if (b != 0)
		{
			SendDataToHW(0, b, 4);
			SendDataToHW(0, b, 3);
		}
	}

	public void SendDataC(int c)
	{
		if (c != 0)
		{
			SendDataToHW(c, 0, 10);
		}
	}

	public void SendData(int a, int b, int c)
	{
		if (a != 0 || b != 0)
		{
			SendDataAB(a, b);
		}
		if (c != 0)
		{
			SendDataC(c);
		}
	}

	public int IndexToChannel(int index)
	{
		int result = 0;
		switch (index)
		{
		case 0:
			result = 1;
			break;
		case 1:
			result = 2;
			break;
		case 2:
			result = 4;
			break;
		case 3:
			result = 8;
			break;
		}
		return result;
	}

	public int MergeChannels(int channel0, params int[] param)
	{
		for (int i = 0; i < param.Length; i++)
		{
			channel0 |= param[i];
		}
		return channel0;
	}

	public static byte[] intToBytes(int value)
	{
		byte[] array = new byte[4];
		array[3] = (byte)((value >> 24) & 0xFF);
		array[2] = (byte)((value >> 16) & 0xFF);
		array[1] = (byte)((value >> 8) & 0xFF);
		array[0] = (byte)(value & 0xFF);
		return array;
	}

	public static byte[] intToBytes2(int value)
	{
		return new byte[4]
		{
			(byte)((value >> 24) & 0xFF),
			(byte)((value >> 16) & 0xFF),
			(byte)((value >> 8) & 0xFF),
			(byte)(value & 0xFF)
		};
	}

	public static int bytesToInt(byte[] src, int offset)
	{
		return (src[offset] & 0xFF) | ((src[offset + 1] & 0xFF) << 8) | ((src[offset + 2] & 0xFF) << 16) | ((src[offset + 3] & 0xFF) << 24);
	}

	public static int bytesToInt2(byte[] src, int offset)
	{
		return ((src[offset] & 0xFF) << 24) | ((src[offset + 1] & 0xFF) << 16) | ((src[offset + 2] & 0xFF) << 8) | (src[offset + 3] & 0xFF);
	}
}
