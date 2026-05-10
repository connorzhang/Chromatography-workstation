using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Lcl_SignalPanel : Panel
{
	public delegate void SignalButtonClick(bool hasSignal, int signalNo);

	private const int int_0 = 4;

	private int int_1;

	private Lcl_SignalButton lcl_SignalButton_0;

	public int curSignalNo = -1;

	private Rectangle rectangle_0;

	private SignalButtonClick signalButtonClick_0;

	public int ButtonsNum
	{
		get
		{
			return int_1;
		}
		set
		{
			if (value != int_1)
			{
				int_1 = value;
				base.Controls.Clear();
				Lcl_SignalButton lcl_SignalButton = null;
				for (int i = 0; i < int_1; i++)
				{
					lcl_SignalButton = new Lcl_SignalButton();
					lcl_SignalButton.Tag = i;
					base.Controls.Add(lcl_SignalButton);
					lcl_SignalButton.Click += method_2;
				}
				base.Width = int_1 * (lcl_SignalButton.Width + 4) + 4 + 4;
			}
		}
	}

	public event SignalButtonClick OnSignalButtonClick
	{
		add
		{
			SignalButtonClick signalButtonClick = signalButtonClick_0;
			SignalButtonClick signalButtonClick2;
			do
			{
				signalButtonClick2 = signalButtonClick;
				SignalButtonClick value2 = (SignalButtonClick)Delegate.Combine(signalButtonClick2, value);
				signalButtonClick = Interlocked.CompareExchange(ref signalButtonClick_0, value2, signalButtonClick2);
			}
			while (signalButtonClick != signalButtonClick2);
		}
		remove
		{
			SignalButtonClick signalButtonClick = signalButtonClick_0;
			SignalButtonClick signalButtonClick2;
			do
			{
				signalButtonClick2 = signalButtonClick;
				SignalButtonClick value2 = (SignalButtonClick)Delegate.Remove(signalButtonClick2, value);
				signalButtonClick = Interlocked.CompareExchange(ref signalButtonClick_0, value2, signalButtonClick2);
			}
			while (signalButtonClick != signalButtonClick2);
		}
	}

	public Lcl_SignalPanel()
	{
		DoubleBuffered = true;
	}

	private void method_0(Lcl_SignalButton lcl_SignalButton_1)
	{
		rectangle_0 = lcl_SignalButton_1.Bounds;
		rectangle_0.Offset(-2, -2);
		rectangle_0.Width += 3;
		rectangle_0.Height += 3;
	}

	private Point method_1(int int_2)
	{
		Lcl_SignalButton lcl_SignalButton = base.Controls[int_2] as Lcl_SignalButton;
		lcl_SignalButton.HasSignal = true;
		Point location = lcl_SignalButton.Location;
		location.X += lcl_SignalButton.Width / 2;
		location.Y += lcl_SignalButton.Height;
		return location;
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		base.OnPaint(pevent);
		SolidBrush brush = new SolidBrush(SystemColors.Control);
		pevent.Graphics.FillRectangle(brush, base.ClientRectangle);
		pevent.Graphics.DrawRectangle(Pens.Black, rectangle_0);
	}

	public void RefreshColors(ref Color[] colors)
	{
		for (int i = 0; i < ButtonsNum; i++)
		{
			if (i < colors.Length)
			{
				colors[i] = (base.Controls[i] as Lcl_SignalButton).Color;
			}
		}
	}

	public void SetColors(Color[] colors)
	{
		for (int i = 0; i < colors.Length; i++)
		{
			if (i < ButtonsNum)
			{
				(base.Controls[i] as Lcl_SignalButton).Color = colors[i];
			}
		}
	}

	public void SetSignals(int signalsNum, int curSignalNo)
	{
		signalsNum = Math.Max(0, signalsNum);
		signalsNum = Math.Min(signalsNum, 12);
		for (int i = 0; i < base.Controls.Count; i++)
		{
			(base.Controls[i] as Lcl_SignalButton).HasSignal = i < signalsNum;
		}
		if (curSignalNo < 0)
		{
			rectangle_0 = default(Rectangle);
		}
		else
		{
			lcl_SignalButton_0 = base.Controls[curSignalNo] as Lcl_SignalButton;
			method_0(lcl_SignalButton_0);
		}
		this.curSignalNo = curSignalNo;
		Refresh();
	}

	private void method_2(object sender, EventArgs e)
	{
		Lcl_SignalButton lcl_SignalButton = sender as Lcl_SignalButton;
		if (lcl_SignalButton.HasSignal)
		{
			lcl_SignalButton_0 = lcl_SignalButton;
			method_0(lcl_SignalButton_0);
			Refresh();
		}
		else if (lcl_SignalButton_0 != null)
		{
			Color color = lcl_SignalButton_0.Color;
			lcl_SignalButton_0.Color = lcl_SignalButton.Color;
			lcl_SignalButton.Color = color;
		}
		if (signalButtonClick_0 != null)
		{
			signalButtonClick_0(lcl_SignalButton.HasSignal, lcl_SignalButton.SignalNo);
		}
	}

	public void VirtualClick(int curSignalNo)
	{
		Lcl_SignalButton lcl_SignalButton = base.Controls[curSignalNo] as Lcl_SignalButton;
		if (!lcl_SignalButton.HasSignal)
		{
			throw new Exception("不可能！");
		}
		lcl_SignalButton_0 = lcl_SignalButton;
		method_0(lcl_SignalButton_0);
		this.curSignalNo = curSignalNo;
		Refresh();
	}
}
