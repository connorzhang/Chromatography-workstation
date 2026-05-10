using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclToolStrip : ToolStrip
{
	private const int int_0 = 17;

	protected override void OnPaint(PaintEventArgs pevent)
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items[i] is ToolStripTextBox)
			{
				ToolStripTextBox toolStripTextBox = Items[i] as ToolStripTextBox;
				if (toolStripTextBox.Visible)
				{
					toolStripTextBox.Height = 17;
					toolStripTextBox.AutoSize = false;
					Rectangle bounds = toolStripTextBox.Bounds;
					bounds.Offset(-1, -1);
					bounds.Width += 3;
					pevent.Graphics.DrawRectangle(Pens.Gray, bounds);
				}
			}
		}
	}
}
