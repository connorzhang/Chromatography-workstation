using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclTV : DataGridView
{
	private int int_0;

	private Pen pen_0 = new Pen(Color.Azure);

	private LclTreeView lclTreeView_0 = new LclTreeView();

	private DrawTreeNodeEventHandler drawTreeNodeEventHandler_0;

	private TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler_0;

	public TreeViewDrawMode DrawMode
	{
		get
		{
			return lclTreeView_0.DrawMode;
		}
		set
		{
			lclTreeView_0.DrawMode = value;
		}
	}

	public string HeaderText0
	{
		set
		{
			method_1(0, value);
		}
	}

	public string HeaderText1
	{
		set
		{
			method_1(1, value);
		}
	}

	public string HeaderText2
	{
		set
		{
			method_1(2, value);
		}
	}

	public int HeaderWidth0
	{
		get
		{
			return method_0(0);
		}
		set
		{
			method_2(0, value);
		}
	}

	public int HeaderWidth1
	{
		get
		{
			return method_0(1);
		}
		set
		{
			method_2(1, value);
		}
	}

	public int HeaderWidth2
	{
		get
		{
			return method_0(2);
		}
		set
		{
			method_2(2, value);
		}
	}

	public ImageList ImageList
	{
		get
		{
			return lclTreeView_0.ImageList;
		}
		set
		{
			lclTreeView_0.ImageList = value;
		}
	}

	public TreeNodeCollection Nodes => lclTreeView_0.Nodes;

	public TreeNode SelectedNode => lclTreeView_0.SelectedNode;

	public event DrawTreeNodeEventHandler DrawNode
	{
		add
		{
			DrawTreeNodeEventHandler drawTreeNodeEventHandler = drawTreeNodeEventHandler_0;
			DrawTreeNodeEventHandler drawTreeNodeEventHandler2;
			do
			{
				drawTreeNodeEventHandler2 = drawTreeNodeEventHandler;
				DrawTreeNodeEventHandler value2 = (DrawTreeNodeEventHandler)Delegate.Combine(drawTreeNodeEventHandler2, value);
				drawTreeNodeEventHandler = Interlocked.CompareExchange(ref drawTreeNodeEventHandler_0, value2, drawTreeNodeEventHandler2);
			}
			while (drawTreeNodeEventHandler != drawTreeNodeEventHandler2);
		}
		remove
		{
			DrawTreeNodeEventHandler drawTreeNodeEventHandler = drawTreeNodeEventHandler_0;
			DrawTreeNodeEventHandler drawTreeNodeEventHandler2;
			do
			{
				drawTreeNodeEventHandler2 = drawTreeNodeEventHandler;
				DrawTreeNodeEventHandler value2 = (DrawTreeNodeEventHandler)Delegate.Remove(drawTreeNodeEventHandler2, value);
				drawTreeNodeEventHandler = Interlocked.CompareExchange(ref drawTreeNodeEventHandler_0, value2, drawTreeNodeEventHandler2);
			}
			while (drawTreeNodeEventHandler != drawTreeNodeEventHandler2);
		}
	}

	public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick
	{
		add
		{
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler = treeNodeMouseClickEventHandler_0;
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler2;
			do
			{
				treeNodeMouseClickEventHandler2 = treeNodeMouseClickEventHandler;
				TreeNodeMouseClickEventHandler value2 = (TreeNodeMouseClickEventHandler)Delegate.Combine(treeNodeMouseClickEventHandler2, value);
				treeNodeMouseClickEventHandler = Interlocked.CompareExchange(ref treeNodeMouseClickEventHandler_0, value2, treeNodeMouseClickEventHandler2);
			}
			while (treeNodeMouseClickEventHandler != treeNodeMouseClickEventHandler2);
		}
		remove
		{
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler = treeNodeMouseClickEventHandler_0;
			TreeNodeMouseClickEventHandler treeNodeMouseClickEventHandler2;
			do
			{
				treeNodeMouseClickEventHandler2 = treeNodeMouseClickEventHandler;
				TreeNodeMouseClickEventHandler value2 = (TreeNodeMouseClickEventHandler)Delegate.Remove(treeNodeMouseClickEventHandler2, value);
				treeNodeMouseClickEventHandler = Interlocked.CompareExchange(ref treeNodeMouseClickEventHandler_0, value2, treeNodeMouseClickEventHandler2);
			}
			while (treeNodeMouseClickEventHandler != treeNodeMouseClickEventHandler2);
		}
	}

	public LclTV()
	{
		base.ScrollBars = ScrollBars.None;
		base.AllowUserToResizeColumns = false;
		base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		base.BorderStyle = BorderStyle.None;
		base.RowHeadersVisible = false;
		base.Controls.Add(lclTreeView_0);
		lclTreeView_0.HideSelection = false;
		lclTreeView_0.DrawMode = TreeViewDrawMode.OwnerDrawText;
		lclTreeView_0.Location = new Point(base.Left, base.ColumnHeadersHeight - 4);
		lclTreeView_0.Width = base.Width;
		lclTreeView_0.Height = base.Height - base.ColumnHeadersHeight + 4;
		lclTreeView_0.Anchor |= AnchorStyles.Bottom | AnchorStyles.Right;
		lclTreeView_0.MouseDown += lclTreeView_0_MouseDown;
		lclTreeView_0.DrawNode += lclTreeView_0_DrawNode;
		lclTreeView_0.NodeMouseDoubleClick += lclTreeView_0_NodeMouseDoubleClick;
		lclTreeView_0.BeforeExpand += lclTreeView_0_BeforeExpand;
		lclTreeView_0.BeforeCollapse += lclTreeView_0_BeforeCollapse;
	}

	private int method_0(int int_1)
	{
		if (int_1 < base.ColumnCount)
		{
			return base.Columns[int_1].Width;
		}
		return -1;
	}

	private void method_1(int int_1, string string_0)
	{
		while (base.ColumnCount < int_1 + 1)
		{
			base.Columns.Add("", "");
		}
		base.Columns[int_1].HeaderText = string_0;
		base.Columns[int_1].SortMode = DataGridViewColumnSortMode.NotSortable;
	}

	private void method_2(int int_1, int int_2)
	{
		while (base.ColumnCount < int_1 + 1)
		{
			base.Columns.Add("", "");
		}
		base.Columns[int_1].Width = int_2;
	}

	private void lclTreeView_0_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
	{
		if (int_0 > 1)
		{
			e.Cancel = true;
		}
	}

	private void lclTreeView_0_BeforeExpand(object sender, TreeViewCancelEventArgs e)
	{
		if (int_0 > 1)
		{
			e.Cancel = true;
		}
	}

	private void lclTreeView_0_DrawNode(object sender, DrawTreeNodeEventArgs e)
	{
		Rectangle bounds = e.Bounds;
		bounds.Offset(1, 2);
		bounds.Width += 10;
		bounds.Height--;
		if ((e.State & TreeNodeStates.Selected) != 0)
		{
			e.Graphics.FillRectangle(Brushes.DodgerBlue, e.Bounds);
			e.Graphics.DrawString(e.Node.Text, Font, Brushes.White, bounds);
		}
		else
		{
			new Rectangle(e.Bounds.Left, e.Bounds.Top, base.Width - e.Bounds.Left, e.Bounds.Height);
			e.Graphics.FillRectangle(Brushes.White, e.Bounds);
			e.Graphics.DrawString(e.Node.Text, Font, Brushes.Black, bounds);
		}
		if (drawTreeNodeEventHandler_0 != null)
		{
			drawTreeNodeEventHandler_0(sender, e);
		}
	}

	private void lclTreeView_0_MouseDown(object sender, MouseEventArgs e)
	{
		int_0 = e.Clicks;
		try
		{
			lclTreeView_0.SelectedNode = lclTreeView_0.GetNodeAt(e.X, e.Y);
		}
		catch
		{
		}
	}

	private void lclTreeView_0_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (treeNodeMouseClickEventHandler_0 != null)
		{
			treeNodeMouseClickEventHandler_0(sender, e);
		}
	}
}
