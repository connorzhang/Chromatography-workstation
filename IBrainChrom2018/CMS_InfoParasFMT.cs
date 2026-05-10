using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CMS_InfoParasFMT : ContextMenuStrip
{
	public const char fmtDateFmt_ymd = 'D';

	public const char fmtDayOfMonth = 'o';

	public const char fmtDayOfWeek = 'w';

	public const char fmtDayOfYear = 'j';

	public const char fmtHour = 'H';

	public const char fmtInjNumber = 'i';

	public const char fmtInstruName = 'N';

	public const char fmtInstruNumber = 'c';

	public const char fmtMinute = 'M';

	public const char fmtMonth = 'm';

	public const char fmtPercentSign = '%';

	public const char fmtSample = 'S';

	public const char fmtSampleID = 's';

	public const char fmtSampleSerialNumber = 'n';

	public const char fmtTimeFmt_hms = 'T';

	public const char fmtUser = 'u';

	public const char fmtVialNumber = 'v';

	public const char fmtYear = 'Y';

	public const char fmtYearLast2digits = 'y';

	private const string string_0 = "日期 dd_mm_yyyy";

	private const string string_1 = "细分日期时间格式";

	private const string string_2 = "日(月)";

	private const string string_3 = "日(周)";

	private const string string_4 = "日(年)";

	private const string string_5 = "小时";

	private const string string_6 = "分钟";

	private const string string_7 = "月";

	private const string string_8 = "年";

	private const string string_9 = "年(后2位数字)";

	private const string string_10 = "针号";

	private const string string_11 = "仪器名";

	private const string string_12 = "仪器号";

	private const string string_13 = "百分号%";

	private const string string_14 = "样品";

	private const string string_15 = "样品ID";

	private const string string_16 = "样品序号[位数]";

	private const string string_17 = "时间 hh_mm_ss";

	private const string string_18 = "用户";

	private const string string_19 = "瓶号";

	private const string string_20 = "Date dd_mm_yyy";

	private const string string_21 = "Advanced date and time formatting";

	private const string string_22 = "Day of month";

	private const string string_23 = "Day of week";

	private const string string_24 = "Day of year";

	private const string string_25 = "Hour";

	private const string string_26 = "Minute";

	private const string string_27 = "Month";

	private const string string_28 = "Year";

	private const string string_29 = "Year(last 2 digits)";

	private const string string_30 = "Injection Number";

	private const string string_31 = "Instrument Name";

	private const string string_32 = "Instrument Number";

	private const string string_33 = "Percent Sign %";

	private const string string_34 = "Sample";

	private const string string_35 = "Sample ID";

	private const string string_36 = "Sample serial number[fixed number]";

	private const string string_37 = "Time hh_mm_ss";

	private const string string_38 = "User";

	private const string string_39 = "Vial Number";

	private int int_0;

	private string string_40;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_1 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_2 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_3 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_4 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_5 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_6 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_7 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_8 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_9 = new ToolStripMenuItem();

	public ToolStripMenuItem miInjNumber = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_10 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_11 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_12 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_13 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_14 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_15 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_16 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_17 = new ToolStripMenuItem();

	public ToolStripMenuItem miVialNumber = new ToolStripMenuItem();

	private object object_0;

	public CMS_InfoParasFMT()
	{
		Items.Add(toolStripMenuItem_15);
		toolStripMenuItem_15.ShortcutKeyDisplayString = "%[x]n";
		toolStripMenuItem_15.Click += toolStripMenuItem_9_Click;
		Items.Add(miVialNumber);
		miVialNumber.ShortcutKeyDisplayString = "%v";
		miVialNumber.Click += toolStripMenuItem_9_Click;
		Items.Add(miInjNumber);
		miInjNumber.ShortcutKeyDisplayString = "%i";
		miInjNumber.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_11);
		toolStripMenuItem_11.ShortcutKeyDisplayString = "%c";
		toolStripMenuItem_11.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_10);
		toolStripMenuItem_10.ShortcutKeyDisplayString = "%N";
		toolStripMenuItem_10.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_17);
		toolStripMenuItem_17.ShortcutKeyDisplayString = "%u";
		toolStripMenuItem_17.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_13);
		toolStripMenuItem_13.ShortcutKeyDisplayString = "%S";
		toolStripMenuItem_13.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_14);
		toolStripMenuItem_14.ShortcutKeyDisplayString = "%s";
		toolStripMenuItem_14.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_12);
		toolStripMenuItem_12.ShortcutKeyDisplayString = "%%";
		toolStripMenuItem_12.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_16);
		toolStripMenuItem_16.ShortcutKeyDisplayString = "%T";
		toolStripMenuItem_16.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_0);
		toolStripMenuItem_0.ShortcutKeyDisplayString = "%D";
		toolStripMenuItem_0.Click += toolStripMenuItem_9_Click;
		Items.Add(toolStripMenuItem_1);
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_6);
		toolStripMenuItem_6.ShortcutKeyDisplayString = "%M";
		toolStripMenuItem_6.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_5);
		toolStripMenuItem_5.ShortcutKeyDisplayString = "%H";
		toolStripMenuItem_5.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_2);
		toolStripMenuItem_2.ShortcutKeyDisplayString = "%o";
		toolStripMenuItem_2.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_3);
		toolStripMenuItem_3.ShortcutKeyDisplayString = "%w";
		toolStripMenuItem_3.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_4);
		toolStripMenuItem_4.ShortcutKeyDisplayString = "%j";
		toolStripMenuItem_4.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_7);
		toolStripMenuItem_7.ShortcutKeyDisplayString = "%m";
		toolStripMenuItem_7.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_8);
		toolStripMenuItem_8.ShortcutKeyDisplayString = "%Y";
		toolStripMenuItem_8.Click += toolStripMenuItem_9_Click;
		toolStripMenuItem_1.DropDownItems.Add(toolStripMenuItem_9);
		toolStripMenuItem_9.ShortcutKeyDisplayString = "%y";
		toolStripMenuItem_9.Click += toolStripMenuItem_9_Click;
	}

	public static string FmtStr(byte byte_0, Injection injAnalysis, Instrument instrument)
	{
		string text = "";
		string text2 = byte_0 switch
		{
			0 => injAnalysis.sampleID, 
			1 => injAnalysis.sample, 
			_ => injAnalysis.fileNameFMT, 
		};
		bool flag = false;
		for (int i = 0; i < text2.Length; i++)
		{
			char c = text2[i];
			if (c == '%')
			{
				if (flag)
				{
					text += "%";
					flag = false;
				}
				else
				{
					flag = true;
				}
				continue;
			}
			if (flag)
			{
				if (smethod_0(text2[i]))
				{
					string s = new string(new char[1] { text2[i] });
					int num = int.Parse(s);
					if (text2.Length > i + 1 && text2[i + 1] == 'n')
					{
						string text3 = "0";
						for (int j = 1; j < num; j++)
						{
							text3 += "0";
						}
						text += injAnalysis.counter.ToString(text3);
						i++;
					}
					else
					{
						text = text + "%" + text2[i];
					}
				}
				else if (text2[i] == 'n')
				{
					text += injAnalysis.counter;
				}
				else if (text2[i] == 'v')
				{
					text += injAnalysis.vialNo;
				}
				else if (text2[i] == 'i')
				{
					text += injAnalysis.injNo;
				}
				else if (text2[i] == 'c')
				{
					text += instrument.pageNo + 1;
				}
				else if (text2[i] == 'N')
				{
					text += instrument.name;
				}
				else if (text2[i] == 'u')
				{
					text += instrument.user.u_name;
				}
				else if (text2[i] == 'S')
				{
					text += injAnalysis.sample;
				}
				else if (text2[i] == 's')
				{
					text += injAnalysis.sampleID;
				}
				else if (text2[i] == '%')
				{
					text += "%";
				}
				else if (text2[i] == 'T')
				{
					string text4 = text;
					text = text4 + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
				}
				else if (text2[i] == 'D')
				{
					text += DateTime.Today.ToShortDateString();
				}
				else if (text2[i] == 'M')
				{
					text += DateTime.Now.Minute;
				}
				else if (text2[i] == 'H')
				{
					text += DateTime.Now.Hour;
				}
				else if (text2[i] == 'o')
				{
					text += DateTime.Today.Day;
				}
				else if (text2[i] == 'w')
				{
					text += DateTime.Today.DayOfWeek;
				}
				else if (text2[i] == 'j')
				{
					text += DateTime.Today.DayOfYear;
				}
				else if (text2[i] == 'm')
				{
					text += DateTime.Today.Month;
				}
				else if (text2[i] == 'Y')
				{
					text += DateTime.Today.Year;
				}
				else if (text2[i] == 'y')
				{
					string text5 = DateTime.Today.Year.ToString();
					text += text5.Substring(2, 2);
				}
				else
				{
					text = text + "%" + text2[i];
				}
			}
			else
			{
				text += text2[i];
			}
			flag = false;
		}
		return text;
	}

	public static string FmtStr(byte byte_0, Injection injAnalysis, int instrumentNum, string InstrmentName, string username)
	{
		string text = "";
		string text2 = byte_0 switch
		{
			0 => injAnalysis.sampleID, 
			1 => injAnalysis.sample, 
			_ => injAnalysis.fileNameFMT, 
		};
		bool flag = false;
		for (int i = 0; i < text2.Length; i++)
		{
			char c = text2[i];
			if (c == '%')
			{
				if (flag)
				{
					text += "%";
					flag = false;
				}
				else
				{
					flag = true;
				}
				continue;
			}
			if (flag)
			{
				if (smethod_0(text2[i]))
				{
					string s = new string(new char[1] { text2[i] });
					int num = int.Parse(s);
					if (text2.Length > i + 1 && text2[i + 1] == 'n')
					{
						string text3 = "0";
						for (int j = 1; j < num; j++)
						{
							text3 += "0";
						}
						text += injAnalysis.counter.ToString(text3);
						i++;
					}
					else
					{
						text = text + "%" + text2[i];
					}
				}
				else if (text2[i] == 'n')
				{
					text += injAnalysis.counter;
				}
				else if (text2[i] == 'v')
				{
					text += injAnalysis.vialNo;
				}
				else if (text2[i] == 'i')
				{
					text += injAnalysis.injNo;
				}
				else if (text2[i] == 'c')
				{
					text += instrumentNum + 1;
				}
				else if (text2[i] == 'N')
				{
					text += InstrmentName;
				}
				else if (text2[i] == 'u')
				{
					text += username;
				}
				else if (text2[i] == 'S')
				{
					text += injAnalysis.sample;
				}
				else if (text2[i] == 's')
				{
					text += injAnalysis.sampleID;
				}
				else if (text2[i] == '%')
				{
					text += "%";
				}
				else if (text2[i] == 'T')
				{
					string text4 = text;
					text = text4 + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
				}
				else if (text2[i] == 'D')
				{
					text += DateTime.Today.ToShortDateString();
				}
				else if (text2[i] == 'M')
				{
					text += DateTime.Now.Minute;
				}
				else if (text2[i] == 'H')
				{
					text += DateTime.Now.Hour;
				}
				else if (text2[i] == 'o')
				{
					text += DateTime.Today.Day;
				}
				else if (text2[i] == 'w')
				{
					text += DateTime.Today.DayOfWeek;
				}
				else if (text2[i] == 'j')
				{
					text += DateTime.Today.DayOfYear;
				}
				else if (text2[i] == 'm')
				{
					text += DateTime.Today.Month;
				}
				else if (text2[i] == 'Y')
				{
					text += DateTime.Today.Year;
				}
				else if (text2[i] == 'y')
				{
					string text5 = DateTime.Today.Year.ToString();
					text += text5.Substring(2, 2);
				}
				else
				{
					text = text + "%" + text2[i];
				}
			}
			else
			{
				text += text2[i];
			}
			flag = false;
		}
		return text;
	}

	private static bool smethod_0(char char_0)
	{
		return '0' <= char_0 && char_0 <= '9';
	}

	public void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			toolStripMenuItem_15.Text = "样品序号[位数]";
			miVialNumber.Text = "瓶号";
			miInjNumber.Text = "针号";
			toolStripMenuItem_11.Text = "仪器号";
			toolStripMenuItem_10.Text = "仪器名";
			toolStripMenuItem_17.Text = "用户";
			toolStripMenuItem_13.Text = "样品";
			toolStripMenuItem_14.Text = "样品ID";
			toolStripMenuItem_12.Text = "百分号%";
			toolStripMenuItem_16.Text = "时间 hh_mm_ss";
			toolStripMenuItem_0.Text = "日期 dd_mm_yyyy";
			toolStripMenuItem_1.Text = "细分日期时间格式";
			toolStripMenuItem_6.Text = "分钟";
			toolStripMenuItem_5.Text = "小时";
			toolStripMenuItem_2.Text = "日(月)";
			toolStripMenuItem_3.Text = "日(周)";
			toolStripMenuItem_4.Text = "日(年)";
			toolStripMenuItem_7.Text = "月";
			toolStripMenuItem_8.Text = "年";
			toolStripMenuItem_9.Text = "年(后2位数字)";
			break;
		case SysLanguage.EN:
			toolStripMenuItem_15.Text = "Sample serial number[fixed number]";
			miVialNumber.Text = "Vial Number";
			miInjNumber.Text = "Injection Number";
			toolStripMenuItem_11.Text = "Instrument Number";
			toolStripMenuItem_10.Text = "Instrument Name";
			toolStripMenuItem_17.Text = "User";
			toolStripMenuItem_13.Text = "Sample";
			toolStripMenuItem_14.Text = "Sample ID";
			toolStripMenuItem_12.Text = "Percent Sign %";
			toolStripMenuItem_16.Text = "Time hh_mm_ss";
			toolStripMenuItem_0.Text = "Date dd_mm_yyy";
			toolStripMenuItem_1.Text = "Advanced date and time formatting";
			toolStripMenuItem_6.Text = "Minute";
			toolStripMenuItem_5.Text = "Hour";
			toolStripMenuItem_2.Text = "Day of month";
			toolStripMenuItem_3.Text = "Day of week";
			toolStripMenuItem_4.Text = "Day of year";
			toolStripMenuItem_7.Text = "Month";
			toolStripMenuItem_8.Text = "Year";
			toolStripMenuItem_9.Text = "Year(last 2 digits)";
			break;
		}
	}

	private void toolStripMenuItem_9_Click(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		if (!(object_0 is TextBox))
		{
			throw new Exception("不要急！");
		}
		string_40 = (object_0 as TextBox).Text;
		int_0 = (object_0 as TextBox).SelectionStart;
		string text = ((toolStripMenuItem != toolStripMenuItem_15) ? toolStripMenuItem.ShortcutKeyDisplayString : "%3n");
		if (string_40 == null)
		{
			string_40 = "";
		}
		Class49.SafeValueCheck(ref int_0, 0, string_40.Length);
		if (object_0 is TextBox)
		{
			TextBox textBox = object_0 as TextBox;
			string selectedText = textBox.SelectedText;
			if (selectedText != null && selectedText != string.Empty)
			{
				string text2 = string_40.Substring(0, textBox.SelectionStart);
				string text3 = string_40.Remove(0, textBox.SelectionStart).Remove(0, selectedText.Length);
				textBox.Text = text2 + text + text3;
			}
			else
			{
				textBox.Text = string_40.Insert(int_0, text);
			}
			textBox.Focus();
			textBox.SelectionLength = 0;
			textBox.SelectionStart = int_0 + text.Length;
		}
	}

	public void Show(Control control, int int_1, int int_2, object textControl)
	{
		object_0 = textControl;
		Show(control, int_1, int_2);
	}
}
