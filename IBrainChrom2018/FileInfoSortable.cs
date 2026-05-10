using System;
using System.IO;

namespace IBrainChrom2018;

public class FileInfoSortable : IComparable
{
	private FileInfo fileInfo_0;

	public FileInfo FileInfo => fileInfo_0;

	public FileInfoSortable(FileInfo file)
	{
		fileInfo_0 = file;
	}

	public int CompareTo(object target)
	{
		DateTime creationTime = ((FileInfoSortable)target).FileInfo.CreationTime;
		DateTime creationTime2 = fileInfo_0.CreationTime;
		return creationTime.CompareTo(creationTime2);
	}

	public int CompareToByName(object object_0)
	{
		string name = ((FileInfoSortable)object_0).FileInfo.Name;
		string name2 = fileInfo_0.Name;
		return name.CompareTo(name2);
	}

	public int CompareToByCreateTime(object object_0)
	{
		DateTime creationTime = ((FileInfoSortable)object_0).FileInfo.CreationTime;
		DateTime creationTime2 = fileInfo_0.CreationTime;
		return creationTime.CompareTo(creationTime2);
	}
}
