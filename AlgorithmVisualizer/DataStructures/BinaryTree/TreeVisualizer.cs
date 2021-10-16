using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;


namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public class TreeVisualizer<T> where T : IComparable
	{
		private static Graphics g;
		private const int NODE_RADIUS = 20, TOP_OFFSET = 5;
		private static int panelHeight, panelWidth;

		private const int delayTime = 200;

		public TreeVisualizer(Graphics _g, int _panelHeight, int _panelWidth)
		{
			g = _g;
			panelHeight = _panelHeight;
			panelWidth = _panelWidth;
		}

		public static void DrawTree(BinNode<T> root)
		{
			// log(n) time assumed for the Math.Pow(2, x) is assumed
			// O(2n + log(n)) = O(n) time, assuming all drawing methods take O(1) time.
			int treeHeight = TreeHeight(root);
			int sideOffset = (int)Math.Pow(2, treeHeight - 1);
			Debug.WriteLine("treeHeight: {0}, Initial offset: {1}, Panel width: {2}", treeHeight, sideOffset, panelWidth);
			TreePrinterHelper(root, panelWidth / 2 - NODE_RADIUS / 2, TOP_OFFSET, sideOffset);
		}
		private static void TreePrinterHelper(BinNode<T> root, int x, int y, int sideOffset)
		{
			// Nodes are printed in post-order, edges are printed in pre-order
			// Note: edges printed before nodes for the nodes to be ontop
			if (root != null)
			{
				if (root.Left != null)
				{
					if (delayTime > 0) Thread.Sleep(delayTime);
					PrintEdge(x, y, -sideOffset);
					TreePrinterHelper(root.Left, x - sideOffset * NODE_RADIUS, y + NODE_RADIUS, sideOffset / 2);
				}
				if (root.Right != null)
				{
					if (delayTime > 0) Thread.Sleep(delayTime);
					PrintEdge(x, y, sideOffset);
					TreePrinterHelper(root.Right, x + sideOffset * NODE_RADIUS, y + NODE_RADIUS, sideOffset / 2);
				}
				if (delayTime > 0) Thread.Sleep(delayTime);
				PrintNode(root.Data, x, y);
			}
		}

		private static void PrintNode(T data, int x, int y)
		{
			Rectangle rect = new Rectangle(x, y, NODE_RADIUS, NODE_RADIUS);
			g.FillEllipse(Brushes.Green, rect);

			// Print the value inside the ellipse centered
			StringFormat sf = new StringFormat();
			sf.LineAlignment = StringAlignment.Center;
			sf.Alignment = StringAlignment.Center;
			g.DrawString(data.ToString(), new Font("Arial", 10), Brushes.Black, rect, sf);
		}
		private static void PrintEdge(int x, int y, int sideOffset)
		{
			Point pt1 = new Point(x + NODE_RADIUS / 2, y + NODE_RADIUS / 2);
			Point pt2 = new Point(x + NODE_RADIUS / 2 + sideOffset * NODE_RADIUS, y + NODE_RADIUS / 2 + NODE_RADIUS);
			g.DrawLine(Pens.Black, pt1, pt2);
		}

		private static int TreeHeight(BinNode<T> root)
		{
			if (root == null) return -1;
			return 1 + Math.Max(TreeHeight(root.Left), TreeHeight(root.Right));
		}
	}
}

