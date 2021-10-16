using System.Drawing;
using System.Windows.Forms;

using AlgorithmVisualizer.DataStructures.BinaryTree;

namespace AlgorithmVisualizer.Forms
{
	public partial class TreeAlgoForm : Form
	{
		private Graphics g;
		public TreeAlgoForm()
		{
			InitializeComponent();
			g = panelMain.CreateGraphics();
		}

		#region Tree visualizer testing
		private void TestTreePrinter()
		{
			new TreeVisualizer<int>(g, panelMain.Height, panelMain.Width);

			int[] inOrder = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
				18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
				36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53,
				54, 55, 56, 57, 58, 59, 60, 61, 62, 63 },
				preOrder = { 32, 16, 8, 4, 2, 1, 3, 6, 5, 7, 12, 10, 9, 11, 14, 13, 15,
				24, 20, 18, 17, 19, 22, 21, 23, 28, 26, 25, 27, 30, 29, 31, 48, 40, 36,
				34, 33, 35, 38, 37, 39, 44, 42, 41, 43, 46, 45, 47, 56, 52, 50, 49, 51,
				54, 53, 55, 60, 58, 57, 59, 62, 61, 63 };

			int[] inOrder2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
				18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 },
				preOrder2 = { 16, 8, 4, 2, 1, 3, 6, 5, 7, 12, 10, 9, 11, 14, 13, 15, 24,
				20, 18, 17, 19, 22, 21, 23, 28, 26, 25, 27, 30, 29, 31 };

			int[] inOrder3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17,
				18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 },
				preOrder3 = { 16, 8, 4, 2, 1, 3, 6, 5, 7, 10, 9, 11, 14, 13, 15, 24,
				20, 18, 17, 19, 22, 21, 23, 28, 26, 25, 27, 30, 29, 31 };

			BinNode<int> root = TreeFromInAndPre(inOrder2, preOrder2);

			TreeVisualizer<int>.DrawTree(root);
		}
		private int preStart;
		private BinNode<int> TreeFromInAndPre(int[] inOrder, int[] preOrder)
		{
			preStart = 0;
			return THelper(inOrder, preOrder, 0, inOrder.Length - 1);
		}
		private BinNode<int> THelper(int[] inOrder, int[] preOrder, int inStart, int inEnd)
		{
			if (inStart > inEnd) return null;
			BinNode<int> node = new BinNode<int>(preOrder[preStart++]);
			if (inStart == inEnd) return node;

			// value is assumed to appear in the array inOrder!
			int inIdx = GetValueIdx(inOrder, node.Data, inStart, inEnd);

			node.Left = THelper(inOrder, preOrder, inStart, inIdx - 1);
			node.Right = THelper(inOrder, preOrder, inIdx + 1, inEnd);
			return node;
		}
		private int GetValueIdx(int[] inOrder, int val, int inStart, int inEnd)
		{
			for (int i = inStart; i <= inEnd; i++)
				if (inOrder[i] == val) return i;
			return -1;
		}
		#endregion
	}
}
