using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018.ChromFile;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(MisMgr))]
[XmlInclude(typeof(ChromDevice))]
[XmlInclude(typeof(ChromDeviceList))]
[XmlInclude(typeof(MtdSetup))]
[XmlInclude(typeof(Chromatogram))]
public class IBaseFileMgr
{
	[NonSerialized]
	public static string m_strFilePath = "";

	[NonSerialized]
	public string m_strExt = "mis";

	[NonSerialized]
	public string m_strFileTypeName = Lang.PS("仪器设置文件");

	public static byte[] Compress(byte[] buffer)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true);
		gZipStream.Write(buffer, 0, buffer.Length);
		gZipStream.Close();
		return memoryStream.ToArray();
	}

	public static byte[] Decompress(byte[] buffer)
	{
		try
		{
			using MemoryStream stream = new MemoryStream(buffer);
			using GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress, leaveOpen: true);
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[40960];
			int count;
			while ((count = gZipStream.Read(array, 0, array.Length)) > 0)
			{
				memoryStream.Write(array, 0, count);
			}
			gZipStream.Close();
			array = null;
			return memoryStream.ToArray();
		}
		catch
		{
		}
		return null;
	}

	public static byte[] WriteByte(IBaseFileMgr subtoy)
	{
		using MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.Serialize(memoryStream, subtoy);
		return Compress(memoryStream.ToArray());
	}

	public static IBaseFileMgr ReadByte(byte[] buffer)
	{
		IBaseFileMgr baseFileMgr = null;
		byte[] array = Decompress(buffer);
		if (array == null)
		{
			array = buffer;
		}
		else
		{
			buffer = null;
		}
		MemoryStream memoryStream = new MemoryStream(array);
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			memoryStream.Position = 0L;
			baseFileMgr = binaryFormatter.Deserialize(memoryStream) as IBaseFileMgr;
			binaryFormatter = null;
		}
		catch
		{
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(MisMgr));
				memoryStream.Position = 0L;
				baseFileMgr = (IBaseFileMgr)xmlSerializer.Deserialize(memoryStream);
				xmlSerializer = null;
			}
			catch
			{
				baseFileMgr = null;
			}
		}
		memoryStream.Close();
		array = null;
		return baseFileMgr;
	}

	public static string WriteString(IBaseFileMgr subtoy)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(IBaseFileMgr));
		Encoding unicode = Encoding.Unicode;
		MemoryStream memoryStream = new MemoryStream();
		XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream, unicode);
		XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
		xmlSerializerNamespaces.Add("", "");
		xmlSerializer.Serialize(xmlWriter, subtoy, xmlSerializerNamespaces);
		return unicode.GetString(memoryStream.ToArray()).Trim();
	}

	public static IBaseFileMgr ReadString(string buffer)
	{
		Encoding unicode = Encoding.Unicode;
		MemoryStream memoryStream = new MemoryStream(unicode.GetBytes(buffer));
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(IBaseFileMgr));
		IBaseFileMgr result = (IBaseFileMgr)xmlSerializer.Deserialize(memoryStream);
		memoryStream.Close();
		return result;
	}

	public static IBaseFileMgr OpenFile(IBaseFileMgr baseFile)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = Lang.PS("仪器设置文件") + "(*.mis)|*.mis";
		openFileDialog.Title = Lang.PS("打开") + baseFile.m_strFileTypeName;
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			m_strFilePath = openFileDialog.FileName;
			return OpenFile(openFileDialog.FileName);
		}
		return null;
	}

	public static bool SaveFile(IBaseFileMgr mismgr)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = mismgr.m_strFileTypeName + "(*." + mismgr.m_strExt + ")|*." + mismgr.m_strExt;
		saveFileDialog.Title = Lang.PS("保存") + mismgr.m_strFileTypeName;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			m_strFilePath = saveFileDialog.FileName;
			SaveFile(saveFileDialog.FileName, mismgr);
			return true;
		}
		return false;
	}

	public static IBaseFileMgr OpenFile(string strFilePath)
	{
		SystemParam systemParam = SystemParam.Create();
		systemParam.iFileSerializeType = 0;
		if (systemParam.iFileSerializeType == 0)
		{
			try
			{
				return OpenFileBinary(strFilePath);
			}
			catch
			{
				try
				{
					return OpenFileXml(strFilePath);
				}
				catch
				{
					return null;
				}
			}
		}
		try
		{
			return OpenFileXml(strFilePath);
		}
		catch (Exception)
		{
			try
			{
				return OpenFileBinary(strFilePath);
			}
			catch
			{
				return null;
			}
		}
	}

	public static void SaveFile(string strFilePath, IBaseFileMgr mismgr)
	{
		SystemParam systemParam = SystemParam.Create();
		systemParam.iFileSerializeType = 0;
		if (systemParam.iFileSerializeType == 0)
		{
			SaveFileBinary(strFilePath, mismgr);
		}
		else
		{
			SaveFileXml(strFilePath, mismgr);
		}
	}

	private static IBaseFileMgr OpenFileXml(string strFilePath)
	{
		IBaseFileMgr baseFileMgr = null;
		Stream stream = new FileStream(strFilePath, FileMode.Open);
		try
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(IBaseFileMgr));
			baseFileMgr = (IBaseFileMgr)xmlSerializer.Deserialize(stream);
			stream.Close();
		}
		catch (Exception ex)
		{
			stream.Close();
			throw ex;
		}
		return baseFileMgr;
	}

	private static void SaveFileXml(string strFilePath, IBaseFileMgr subtoy)
	{
		string directoryName = Path.GetDirectoryName(strFilePath);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(IBaseFileMgr));
		Stream stream = new FileStream(strFilePath, FileMode.Create);
		xmlSerializer.Serialize(stream, subtoy);
		stream.Close();
	}

	private static IBaseFileMgr OpenFileBinary(string strFilePath)
	{
		IBaseFileMgr baseFileMgr = null;
		Stream stream = new FileStream(strFilePath, FileMode.Open);
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			stream.Position = 0L;
			baseFileMgr = binaryFormatter.Deserialize(stream) as IBaseFileMgr;
			binaryFormatter = null;
			stream.Close();
		}
		catch (Exception ex)
		{
			stream.Close();
			throw ex;
		}
		return baseFileMgr;
	}

	private static void SaveFileBinary(string strFilePath, IBaseFileMgr subtoy)
	{
		try
		{
			string directoryName = Path.GetDirectoryName(strFilePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			Stream stream = new FileStream(strFilePath, FileMode.Create);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, subtoy);
			stream.Close();
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError(string.Format("SaveFileBinary{0},{0}", strFilePath, ex.Message));
			LogMgr.Instance.LogError($"SaveFileBinary{ex.Message}");
		}
	}
}
