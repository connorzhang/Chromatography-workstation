using System.IO;

namespace IBrainChrom2018;

public class LclFileRW
{
	private BinaryReader binaryReader_0;

	private BinaryWriter binaryWriter_0;

	private FileInfo fileInfo_0;

	private FileStream fileStream_0;

	protected virtual void loadFromFile(BinaryReader binaryReader_1)
	{
	}

	public void LoadFromFile(string fileName)
	{
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_0, out fileStream_0, out binaryReader_0);
			loadFromFile(binaryReader_0);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
		}
	}

	protected virtual void saveToFile(BinaryWriter binaryWriter_1)
	{
	}

	public void SaveToFile(string fileName)
	{
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_0, out fileStream_0, out binaryWriter_0);
			saveToFile(binaryWriter_0);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryWriter_0);
		}
	}
}
