using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public static class TreeVisualizer<T> where T : IComparable
	{
		// Work in progress...

		private static Graphics g;
		private const int nodeRad = 30, topOffset = 5, fontSize = 10;
		private static int panelHeight, panelWidth;

		private static readonly Color nodeColor = Color.Green, txtColor = Color.Black, edgeColor = Color.White;

		private const int delayTime = 50;

		public static void DrawTree(BinNode<T> root, Graphics _g, Panel panel)
		{
			g = _g;
			panelHeight = panel.Height;
			panelWidth = panel.Width;

			int treeHeight = TreeUtils<T>.Height(root);
			int sideOffset = (int)Math.Pow(2, treeHeight - 1);
			Debug.WriteLine("treeHeight: {0}, Initial offset: {1}, Panel width: {2}", treeHeight, sideOffset, panelWidth);

			DrawTree(root, panelWidth / 2 - nodeRad / 2, topOffset, sideOffset);
		}
		private static void DrawTree(BinNode<T> root, int x, int y, int sideOffset)
		{
			// Nodes are printed in post-order, edges are printed in pre-order
			// Note: edges printed before nodes for the nodes to be ontop
			if (root != null)
			{
				if (root.Left != null)
				{
					if (delayTime > 0) Thread.Sleep(delayTime);
					DrawEdge(x, y, -sideOffset);
					DrawTree(root.Left, x - sideOffset * nodeRad, y + nodeRad, sideOffset / 2);
				}
				if (root.Right != null)
				{
					if (delayTime > 0) Thread.Sleep(delayTime);
					DrawEdge(x, y, sideOffset);
					DrawTree(root.Right, x + sideOffset * nodeRad, y + nodeRad, sideOffset / 2);
				}
				if (delayTime > 0) Thread.Sleep(delayTime);
				DrawNode(root.Data, x, y);
			}
		}

		private static void DrawNode(T data, int x, int y)
		{
			var rect = new Rectangle(x, y, nodeRad, nodeRad);
			using (var nodeBrush = new SolidBrush(nodeColor)) g.FillEllipse(Brushes.Green, rect);

			using (var txtBrush = new SolidBrush(txtColor))
			using (var font = new Font("Arial", fontSize))
			using (var sf = new StringFormat())
			{
				sf.LineAlignment = StringAlignment.Center;
				sf.Alignment = StringAlignment.Center;
				g.DrawString(data.ToString(), font, txtBrush, rect, sf);
			}
		}
		private static void DrawEdge(int x, int y, int sideOffset)
		{
			var pt1 = new Point(x + nodeRad / 2, y + nodeRad / 2);
			var pt2 = new Point(x + nodeRad / 2 + sideOffset * nodeRad, y + nodeRad / 2 + nodeRad);
			using (var edgePen = new Pen(edgeColor)) g.DrawLine(edgePen, pt1, pt2);
		}
	}
}

