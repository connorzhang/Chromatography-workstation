using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018.Unit;

public sealed class UIProxy
{
	private delegate void ThreadSleepDelegate(int milliseconds);

	private static readonly UIProxy m_instance;

	public static UIProxy Instance => m_instance;

	public StatusStrip StatusStrip => FormMain.fromMain.statusStrip1;

	public ToolStripStatusLabel ErrorMsgStaticLabel => FormMain.fromMain.tsslMsg;

	public ChromFormInterface MainForm => FormMain.fromMain;

	static UIProxy()
	{
		m_instance = new UIProxy();
	}

	private UIProxy()
	{
	}

	public void Error(string message, Exception ex)
	{
		if (MainForm.IsDisposed2)
		{
			return;
		}
		MainForm.Invoke((MethodInvoker)delegate
		{
			ToolStripStatusLabel errorMsgStaticLabel = ErrorMsgStaticLabel;
			if (message.Length > 100)
			{
				message = message.Substring(0, 100);
			}
			errorMsgStaticLabel.ForeColor = Color.Red;
			errorMsgStaticLabel.Text = message;
			Interaction.Beep();
			ThreadSleepDelegate threadSleepDelegate = ThreadSleepMethod;
			threadSleepDelegate.BeginInvoke(10000, ClearMessage, null);
		});
	}

	private void ThreadSleepMethod(int milliseconds)
	{
		Thread.Sleep(milliseconds);
	}

	private void ClearMessage(IAsyncResult asResult)
	{
		if (!MainForm.IsDisposed2)
		{
			MainForm.Invoke((MethodInvoker)delegate
			{
				ToolStripStatusLabel errorMsgStaticLabel = ErrorMsgStaticLabel;
				errorMsgStaticLabel.ForeColor = Color.Black;
				errorMsgStaticLabel.Text = Lang.PS("消息：");
			});
		}
	}

	public void SetErrorMsgStaticLabelMenu()
	{
		ToolStripStatusLabel errorMsgStaticLabel = ErrorMsgStaticLabel;
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem7 = new ToolStripMenuItem();
		ToolStripMenuItem toolStripMenuItem8 = new ToolStripMenuItem();
		contextMenuStrip.ImageScalingSize = new Size(20, 20);
		contextMenuStrip.Items.AddRange(new ToolStripItem[8] { toolStripMenuItem, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7, toolStripMenuItem8 });
		contextMenuStrip.Name = "logEdit";
		contextMenuStrip.Size = new Size(105, 30);
		toolStripMenuItem.Name = "openErrorFileMenuItem";
		toolStripMenuItem.Size = new Size(104, 26);
		toolStripMenuItem.Text = Lang.PS("打开错误Log文件");
		toolStripMenuItem.Click += openErrorFileMenuItem_Click;
		toolStripMenuItem2.Name = "openRunFileMenuItem";
		toolStripMenuItem2.Size = new Size(104, 26);
		toolStripMenuItem2.Text = Lang.PS("打开运行Log文件");
		toolStripMenuItem2.Click += openRunFileMenuItem_Click;
		toolStripMenuItem3.Name = "openDirMenuItem";
		toolStripMenuItem3.Size = new Size(104, 26);
		toolStripMenuItem3.Text = Lang.PS("打开Log路径");
		toolStripMenuItem3.Click += openDirMenuItem_Click;
		toolStripMenuItem4.Name = "clearDirMenuItem";
		toolStripMenuItem4.Size = new Size(104, 26);
		toolStripMenuItem4.Text = Lang.PS("清空日志文件");
		toolStripMenuItem4.Click += clearDirMenuItem_Click;
		toolStripMenuItem5.Name = "testErrorMenuItem";
		toolStripMenuItem5.Size = new Size(104, 26);
		toolStripMenuItem5.Text = Lang.PS("异常测试");
		toolStripMenuItem5.Click += testErrorMenuItem_Click;
		toolStripMenuItem6.Name = "testError2MenuItem";
		toolStripMenuItem6.Size = new Size(104, 26);
		toolStripMenuItem6.Text = Lang.PS("无响应测试");
		toolStripMenuItem6.Click += testError2MenuItem_Click;
		toolStripMenuItem7.Name = "memWatchMenuItem";
		toolStripMenuItem7.Size = new Size(104, 26);
		toolStripMenuItem7.Text = Lang.PS("内存查看");
		toolStripMenuItem7.Click += memWatchMenuItem_Click;
		toolStripMenuItem8.Name = "memSetTestSocketMenuItem";
		toolStripMenuItem8.Size = new Size(104, 26);
		toolStripMenuItem8.Text = Lang.PS("测试socket异常");
		toolStripMenuItem8.Click += memSetTestSocketMenuItem_Click;
		StatusStrip statusStrip = StatusStrip;
		statusStrip.ContextMenuStrip = contextMenuStrip;
	}

	private void memSetTestSocketMenuItem_Click(object sender, EventArgs e)
	{
		SystemParam systemParam = SystemParam.Create();
		systemParam.bTestSocketError = true;
	}

	private void memWatchMenuItem_Click(object sender, EventArgs e)
	{
		ClassStateWatchDlg classStateWatchDlg = new ClassStateWatchDlg();
		ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
		classStateWatchDlg.LoadWatchData(chromDeviceListMgr.tcpServerMgr.mainTcpServer);
		classStateWatchDlg.ShowDialog();
	}

	private void TsslMsg_Click(object sender, EventArgs e)
	{
	}

	private void TsslMsg_DoubleClick(object sender, EventArgs e)
	{
		openErrorFileMenuItem_Click(null, null);
	}

	private void openErrorFileMenuItem_Click(object sender, EventArgs e)
	{
		string text = Application.StartupPath + "\\ErrLog\\ErrLog.txt";
		if (File.Exists(text))
		{
			Process.Start(text);
			return;
		}
		LogMgr.Instance.LogWarning("Test");
		Process.Start(text);
	}

	private void openRunFileMenuItem_Click(object sender, EventArgs e)
	{
		string text = Application.StartupPath + "\\ErrLog\\RunLog.txt";
		if (File.Exists(text))
		{
			Process.Start(text);
			return;
		}
		LogMgr.Instance.Write2RunLog2("Test");
		Process.Start(text);
	}

	private void openDirMenuItem_Click(object sender, EventArgs e)
	{
		string text = Application.StartupPath + "\\ErrLog";
		if (Directory.Exists(text))
		{
			Process.Start(text);
		}
	}

	private void clearDirMenuItem_Click(object sender, EventArgs e)
	{
		LogMgr.Instance.ClearLogFiles();
	}

	private void testErrorMenuItem_Click(object sender, EventArgs e)
	{
		string[] array = new string[50];
		for (int i = 0; i < 60; i++)
		{
			array[i] = "Test Error";
		}
	}

	private void testError2MenuItem_Click(object sender, EventArgs e)
	{
		string[] array = new string[50];
		for (int i = 0; i < int.MaxValue; i++)
		{
			for (int j = 0; j < int.MaxValue; j++)
			{
				for (int k = 0; k < int.MaxValue; k++)
				{
					for (int l = 0; l < int.MaxValue; l++)
					{
						for (int m = 0; m < int.MaxValue; m++)
						{
							array[0] = "Test Error";
						}
					}
				}
			}
		}
	}
}
