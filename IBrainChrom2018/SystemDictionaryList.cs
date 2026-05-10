using System.Collections.Generic;

namespace IBrainChrom2018;

public class SystemDictionaryList
{
	public static SystemDictionaryList self;

	public static Dictionary<string, int> dictionary_0;

	public static Dictionary<string, int> dictionary_30;

	public static SystemDictionaryList Create()
	{
		dictionary_0 = new Dictionary<string, int>(10)
		{
			{ "StT", 0 },
			{ "EdT", 1 },
			{ "StV", 2 },
			{ "EdV", 3 },
			{ "RtT", 4 },
			{ "Ara", 5 },
			{ "Hht", 6 },
			{ "Wth", 7 },
			{ "Whf", 8 },
			{ "Amt", 9 }
		};
		dictionary_30 = new Dictionary<string, int>(18)
		{
			{ "SV", 0 },
			{ "EV", 1 },
			{ "VI", 2 },
			{ "SampleID", 3 },
			{ "Sample", 4 },
			{ "Amount", 5 },
			{ "ISTDAmount", 6 },
			{ "Dilution", 7 },
			{ "InjVol", 8 },
			{ "K", 9 },
			{ "Alpha", 10 },
			{ "FileNameFMT", 11 },
			{ "CaliStand", 12 },
			{ "MethodName", 13 },
			{ "ReportStyle", 14 },
			{ "OpenChrom", 15 },
			{ "OpenCali", 16 },
			{ "Print", 17 }
		};
		if (self == null)
		{
			self = new SystemDictionaryList();
		}
		return self;
	}
}
