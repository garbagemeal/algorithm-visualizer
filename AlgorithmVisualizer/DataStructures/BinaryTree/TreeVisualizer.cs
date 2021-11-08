using AlgorithmVisualizer.DataStructures.AVLTree;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public static class TreeVisualizer<T> where T : IComparable
	{
		private static Graphics g;
		private static int canvasHeight, canvasWidth;

		private const int nodeSize = 30, fontSize = 10, topOffset = 15;
		private const string fontName = "Arial";
		private static readonly Color nodeColor = Color.Green, nodeBorderColor = Color.DimGray,
			nodeTxtColor = Color.Black, edgeColor = Color.White;

		public static void DrawTree(BinNode<T> root, Graphics _g, PictureBox canvas)
		{
			g = _g;
			canvasHeight = canvas.Height;
			canvasWidth = canvas.Width;

			int treeHeight = TreeUtils<T>.Height(root);
			int sideOffset = (int)Math.Pow(2, treeHeight - 1);

			DrawTree(root, canvasWidth / 2 - nodeSize / 2, topOffset, sideOffset);
		}
		private static void DrawTree(BinNode<T> root, int x, int y, int sideOffset)
		{
			// Nodes are printed in post-order, edges are printed in pre-order
			// Note: edges printed before nodes for the nodes to be ontop
			if (root != null)
			{
				if (root.Left != null)
				{
					DrawEdge(x, y, -sideOffset);
					DrawTree(root.Left, x - sideOffset * nodeSize, y + nodeSize, sideOffset / 2);
				}
				if (root.Right != null)
				{
					DrawEdge(x, y, sideOffset);
					DrawTree(root.Right, x + sideOffset * nodeSize, y + nodeSize, sideOffset / 2);
				}
				DrawNode(root, x, y);
			}
		}

		private static void DrawNode(BinNode<T> node, int x, int y)
		{
			ApplyDisplacement(ref x, ref y);

			var rect = new Rectangle(x, y, nodeSize, nodeSize);

			using (var brush = new SolidBrush(nodeColor)) g.FillEllipse(brush, rect);
			using (var pen = new Pen(nodeBorderColor)) g.DrawEllipse(pen, rect);

			using (var brush = new SolidBrush(nodeTxtColor))
			using (var font = new Font(fontName, fontSize))
			using (var sf = new StringFormat())
			{
				sf.LineAlignment = sf.Alignment = StringAlignment.Center;
				g.DrawString(node.Data.ToString(), font, brush, rect, sf);
				if (node is AVLNode<T>)
				{
					// Draw balance factor over node
					var BFRect = new Rectangle(x, y - 10, nodeSize, 10);
					g.DrawString(((AVLNode<T>)node).BalanceFactor.ToString(), font, Brushes.Red, BFRect, sf);
				}
			}
		}
		private static void DrawEdge(int x, int y, int sideOffset)
		{
			ApplyDisplacement(ref x, ref y);

			var pt1 = new Point(x + nodeSize / 2, y + nodeSize / 2);
			var pt2 = new Point(x + nodeSize / 2 + sideOffset * nodeSize, y + nodeSize / 2 + nodeSize);
			using (var pen = new Pen(edgeColor)) g.DrawLine(pen, pt1, pt2);
		}

		// Amount of displacement in x, y (supporing moving the drawing)
		private static Point displacement = Point.Empty;
		public static void ResetDisplacement()
		{
			// Reset the displacement point to (0, 0)
			displacement = Point.Empty;
		}
		public static void Displace(Point delta)
		{
			// Add displacement into the displacement point
			displacement.X += delta.X;
			displacement.Y += delta.Y;
		}
		private static void ApplyDisplacement(ref int x, ref int y)
		{
			// Apply the displacement for given x, y int vars
			x += displacement.X;
			y += displacement.Y;
		}
	}
}

