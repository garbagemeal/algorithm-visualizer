using System;
using System.Collections.Generic;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public class TreeConsolePrinter<T> where T : IComparable
	{
		// Binary tree printer (old, supports values with up to 2 digits)
		public static void Print(BinNode<T> root)
		{
			Queue<BinNode<T>> q1 = new Queue<BinNode<T>>(), q2 = new Queue<BinNode<T>>();
			q1.Enqueue(root);

			int offset = 0, height = TreeUtils<T>.Height(root);
			for (int i = 0; i < height; i++) offset = offset * 2 + 2;

			for (int level = 0; level <= height; level++)
			{
				PrintHelper(q1, q2, offset);
				Swap(ref q1, ref q2);
				offset = (offset - 2) / 2;
			}
		}
		private static void PrintHelper(Queue<BinNode<T>> q1, Queue<BinNode<T>> q2, int offset)
		{
			// 'q1' holds the current level, 'q2' will hold the next level.
			// Print the nodes found in the 'q1' and the lines connecting from those nodes
			// to the nodes in the next level.
			string lines = "";
			int OFFSET = offset + 1;
			while (q1.Count > 0)
			{
				BinNode<T> curNode = q1.Dequeue();
				printSpaces(OFFSET);
				lines += GetChars(' ', OFFSET / 2 + 1);
				if (curNode != null)
				{
					Console.Write(Fill0(curNode.Data.ToString(), 2));
					q2.Enqueue(curNode.Left);
					q2.Enqueue(curNode.Right);
					if (curNode.Left != null) lines += "┌" + GetChars('─', OFFSET / 2);
					else lines += GetChars(' ', OFFSET / 2 + 1);

					if (curNode.Left != null && curNode.Right != null) lines += "┴";
					else
					{
						if (curNode.Left != null) lines += "┘";
						else if (curNode.Right != null) lines += "└";
						else lines += " ";
					}

					if (curNode.Right != null) lines += GetChars('─', OFFSET / 2) + "┐";
					else lines += GetChars(' ', OFFSET / 2 + 1);
				}
				else
				{
					printSpaces(2);
					// null enqueued to print spaces under missing nodes
					q2.Enqueue(null);
					q2.Enqueue(null);
					lines += GetChars(' ', OFFSET + 2);
				}
				printSpaces(OFFSET);
				lines += GetChars(' ', OFFSET / 2);
			}
			Console.Write("| offset: {0}, OFFSET: {1} \n", offset, OFFSET);
			if (offset > 0) Console.WriteLine(lines + "|");
		}

		private static void printSpaces(int n)
		{
			for (int i = 0; i < n; i++) Console.Write(" ");
		}
		private static string GetSpaces(int n)
		{
			string str = "";
			for (int i = 0; i < n; i++) str += " ";
			return str;
		}
		private static string GetChars(char c, int n)
		{
			if (n == 0) return "";
			return c + GetChars(c, n - 1);
		}
		private static void PrintChars(char c, int n)
		{
			if (n == 0) return;
			Console.Write(c);
			PrintChars(c, n - 1);
		}
		private static string Fill0(string str, int n)
		{
			while (str.Length < n) str = "0" + str;
			return str;
		}

		private static void Swap(ref Queue<BinNode<T>> q1, ref Queue<BinNode<T>> q2)
		{
			Queue<BinNode<T>> tmp = q1;
			q1 = q2;
			q2 = tmp;
		}
	}
}
