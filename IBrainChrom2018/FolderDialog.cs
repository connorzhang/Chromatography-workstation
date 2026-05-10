using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace IBrainChrom2018;

public class FolderDialog : FolderNameEditor
{
	private FolderBrowser folderBrowser_0 = new FolderBrowser();

	public string Path => folderBrowser_0.DirectoryPath;

	public DialogResult DisplayDialog()
	{
		return DisplayDialog("请选择一个文件夹");
	}

	public DialogResult DisplayDialog(string description)
	{
		folderBrowser_0.Description = description;
		return folderBrowser_0.ShowDialog();
	}

	~FolderDialog()
	{
		folderBrowser_0.Dispose();
	}
}
