using System;
using System.Diagnostics;

namespace IBrainChrom2018.Unit;

public class PerfromMgr
{
	private static PerfromMgr myself = null;

	private static TimeSpan prevCpuTime = TimeSpan.Zero;

	public static int MemorySizePrivate => (int)((float)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576f);

	public static int MemorySizeVirtual => (int)((float)Process.GetCurrentProcess().VirtualMemorySize64 / 1048576f);

	public static int MemorySizeTotalGC => (int)((float)GC.GetTotalMemory(forceFullCollection: false) / 1048576f);

	public static double CpuPerformance
	{
		get
		{
			int num = 500;
			TimeSpan totalProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
			double result = (totalProcessorTime - prevCpuTime).TotalMilliseconds / (double)num / (double)Environment.ProcessorCount;
			prevCpuTime = totalProcessorTime;
			return result;
		}
	}

	public static PerfromMgr Instance => myself;

	public static PerfromMgr Create()
	{
		if (myself == null)
		{
			myself = new PerfromMgr();
		}
		return myself;
	}

	private PerfromMgr()
	{
	}
}
