using System;

namespace IBrainChrom2018;

public class VirSglArg
{
	public bool bs_setZero;

	public int chg_itv = 4;

	private Random random_0;

	public bool sample;

	public int sample_interval = 100;

	private float float_0;

	private float float_1;

	private float float_2;

	public float signal_bs;

	public float signal_nr = 0.13f;

	public float signal_scale = 1f;

	public float[] signals = new float[0];

	public uint uint_0;

	public float GenerateSignal()
	{
		long num = uint_0 % (uint)chg_itv;
		if (num == 0)
		{
			float_1 = float_2;
			float_2 = Convert.ToSingle((double)signal_nr * random_0.NextDouble());
			float_0 = float_2 - float_1;
		}
		float num2 = (float)num / (float)chg_itv;
		float num3 = float_1 + signal_nr * num2;
		if (!bs_setZero)
		{
			num3 += signal_bs;
		}
		uint_0++;
		if (uint_0 >= uint.MaxValue)
		{
			uint_0 = 0u;
		}
		return num3;
	}

	public void Init_sf()
	{
		float_1 = Convert.ToSingle((double)signal_nr * random_0.NextDouble());
		float_2 = Convert.ToSingle((double)signal_nr * random_0.NextDouble());
		float_0 = float_2 - float_1;
	}

	public void SetRandomSeed(int seed)
	{
		random_0 = new Random(seed);
	}
}
