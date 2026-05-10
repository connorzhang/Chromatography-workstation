using System;

namespace IBrainChrom2018;

[Serializable]
public class OpenFileDialogExPathEventArgs : EventArgs
{
	private string m_path = string.Empty;

	public string Path => m_path;

	public OpenFileDialogExPathEventArgs(string path)
	{
		m_path = path;
	}
}
