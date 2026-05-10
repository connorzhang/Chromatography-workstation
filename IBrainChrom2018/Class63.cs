using System;
using System.Threading;

namespace IBrainChrom2018;

internal class Class63
{
	public delegate void Delegate3();

	private int int_0;

	private Thread thread_0;

	private Delegate3 delegate3_0;

	public void method_0(Delegate3 delegate3_1)
	{
		Delegate3 @delegate = delegate3_0;
		Delegate3 delegate2;
		do
		{
			delegate2 = @delegate;
			Delegate3 value = (Delegate3)Delegate.Combine(delegate2, delegate3_1);
			@delegate = Interlocked.CompareExchange(ref delegate3_0, value, delegate2);
		}
		while (@delegate != delegate2);
	}

	public void method_1(Delegate3 delegate3_1)
	{
		Delegate3 @delegate = delegate3_0;
		Delegate3 delegate2;
		do
		{
			delegate2 = @delegate;
			Delegate3 value = (Delegate3)Delegate.Remove(delegate2, delegate3_1);
			@delegate = Interlocked.CompareExchange(ref delegate3_0, value, delegate2);
		}
		while (@delegate != delegate2);
	}

	public void method_2()
	{
		if (thread_0 != null && thread_0.IsAlive)
		{
			thread_0.Abort();
			thread_0.Join();
		}
	}

	private void method_3()
	{
		Thread.Sleep(int_0);
		if (delegate3_0 != null)
		{
			delegate3_0();
		}
	}

	public void method_4(float float_0)
	{
		method_2();
		float_0 = Math.Max(0f, float_0);
		int_0 = Convert.ToInt32(float_0 * 60000f);
		thread_0 = new Thread(method_3);
		thread_0.IsBackground = true;
		thread_0.Priority = ThreadPriority.AboveNormal;
		thread_0.Start();
	}

	public bool method_5()
	{
		return thread_0.IsAlive;
	}
}
