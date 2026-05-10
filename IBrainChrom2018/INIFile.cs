using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

public class INIFile
{
	private string string_0;

	public INISegments Segments;

	public string Path => string_0;

	[DllImport("kernel32.dll")]
	public static extern int GetPrivateProfileIntA(string segName, string keyName, int iDefault, string fileName);

	[DllImport("kernel32.dll")]
	public static extern int GetPrivateProfileStringA(string segName, string keyName, string sDefault, StringBuilder retValue, int nSize, string fileName);

	[DllImport("kernel32.dll")]
	public static extern int GetPrivateProfileSectionA(string segName, byte[] sData, int nSize, string fileName);

	[DllImport("kernel32.dll")]
	public static extern int WritePrivateProfileSectionA(string segName, byte[] sData, string fileName);

	[DllImport("kernel32.dll")]
	public static extern int WritePrivateProfileStringA(string segName, string keyName, string sValue, string fileName);

	[DllImport("kernel32.dll")]
	public static extern int GetPrivateProfileSectionNamesA(byte[] vData, int iLen, string fileName);

	public INIFile(string vPath)
	{
		string_0 = vPath;
		Segments = new INISegments(this);
		byte[] array = new byte[32767];
		int privateProfileSectionNamesA = GetPrivateProfileSectionNamesA(array, 32767, string_0);
		if (privateProfileSectionNamesA <= 0)
		{
			return;
		}
		int index = 0;
		for (int i = 0; i < privateProfileSectionNamesA; i++)
		{
			if (array[i] == 0)
			{
				string text = Encoding.Default.GetString(array, index, i).Trim();
				index = i + 1;
				if (text != "")
				{
					Segments.Add(text);
				}
			}
		}
	}

	public int GetInt(string segName, string keyName, int iDefault)
	{
		return GetPrivateProfileIntA(segName, keyName, iDefault, string_0);
	}

	public string GetString(string segName, string keyName, string sDefault)
	{
		StringBuilder stringBuilder = new StringBuilder(1024);
		GetPrivateProfileStringA(segName, keyName, "", stringBuilder, 1024, string_0);
		return stringBuilder.ToString();
	}

	public void SetString(string segName, string keyName, string vValue)
	{
		WritePrivateProfileStringA(segName, keyName, vValue, string_0);
	}

	public void WriteSegment(string segName, string vData)
	{
		WritePrivateProfileSectionA(segName, Encoding.Default.GetBytes(vData), string_0);
	}

	public void GetSegment(INISegment inisegment_0)
	{
		byte[] array = new byte[32767];
		int privateProfileSectionA = GetPrivateProfileSectionA(inisegment_0.Name, array, 32767, string_0);
		inisegment_0.Items.Clear();
		if (privateProfileSectionA < 1)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < privateProfileSectionA; i++)
		{
			if (array[i] != 0)
			{
				continue;
			}
			string text = Encoding.Default.GetString(array, num, i - num).Trim();
			if (text != "")
			{
				string[] array2 = text.Split('=');
				if (array2.Length <= 1)
				{
					inisegment_0.Items.Add(array2[0], "");
				}
				else
				{
					inisegment_0.Items.Add(array2[0], array2[1]);
				}
			}
			num = i + 1;
		}
	}
}
