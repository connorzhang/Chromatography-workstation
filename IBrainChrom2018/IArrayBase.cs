using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class IArrayBase
{
	public static List<T> NewArray<T>(int count) where T : new()
	{
		count = Math.Max(0, count);
		List<T> list = new List<T>(count);
		for (int i = 0; i < count; i++)
		{
			list.Add(new T());
		}
		return list;
	}

	public static List<T> NewArray3<T>(int count) where T : new()
	{
		count = Math.Max(0, count);
		List<T> list = new List<T>(0);
		list.Clear();
		for (int i = 0; i < count; i++)
		{
			list.Add(new T());
		}
		return list;
	}

	public static void NewArray<T>(ref List<T> list, int count) where T : new()
	{
		count = Math.Max(0, count);
		count = Math.Min(300, count);
		list = new List<T>(count);
		for (int i = 0; i < count; i++)
		{
			list.Add(new T());
		}
	}

	public static T[] NewArray2<T>(int count) where T : new()
	{
		count = Math.Max(0, count);
		T[] array = new T[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = new T();
		}
		return array;
	}

	public static void NewArray2<T>(ref T[] list, int count) where T : new()
	{
		count = Math.Max(0, count);
		list = new T[count];
		for (int i = 0; i < count; i++)
		{
			list[i] = new T();
		}
	}
}
