using System;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	static class TreeTester
	{

		// A class to test the BinarySearchTree as well as TreeUtils

		public static void RunTests()
		{
			// Need to fix issues with encoding! as it stands the 2D binary tree printer is displaying incorrect chars!
			// IO Exceptions thrown while trying to change the enconding for some reason..

			//Console.OutputEncoding = System.Text.Encoding.GetEncoding(28591);
			//Console.OutputEncoding = System.Text.Encoding.UTF8;
			//Console.OutputEncoding = System.Text.Encoding.Default;


			BinarySearchTree<int> BST = new BinarySearchTree<int>();

			//int[] arr = { 8, 4, 12, 2, 6, 10, 14, 1, 3, 5, 7, 9, 11, 13, 15 };
			//// int[] arr = { 4, 2, 5, 1, 3 };
			//int N = arr.Length;

			//for (int i = 0; i < N; i++)
			//	BST.Add(arr[i]);

			//Console.WriteLine("Input array:");
			//for (int i = 0; i < N; i++)
			//	Console.Write(arr[i] + " ");
			//Console.WriteLine("\n==================================================");

			//Console.Write("PreOrder: \n");
			//Console.Write("Rec: "); TreeUtils<int>.PrintPreOrderRec(BST.Root); Console.WriteLine();
			//Console.Write("Itr: "); TreeUtils<int>.PrintPreOrderItr(BST.Root); Console.WriteLine();
			//Console.Write("Itr: "); TreeUtils<int>.PrintPreOrderItrMorris(BST.Root); Console.WriteLine();

			//Console.Write("InOrder: \n");
			//Console.Write("Rec: "); TreeUtils<int>.PrintInOrderRec(BST.Root); Console.WriteLine();
			//Console.Write("Itr: "); TreeUtils<int>.PrintInOrderItr(BST.Root); Console.WriteLine();
			//Console.Write("Itr: "); TreeUtils<int>.PrintInOrderItrMorris(BST.Root); Console.WriteLine();

			//Console.Write("PostOrder: \n");
			//Console.Write("Rec: "); TreeUtils<int>.PrintPostOrderRec(BST.Root); Console.WriteLine();
			//Console.Write("Itr: "); TreeUtils<int>.PrintPostOrderItr(BST.Root); Console.WriteLine();

			//Console.Write("LevelOrder: \n");
			//TreeUtils<int>.PrintLevelOrder(BST.Root); Console.WriteLine();

			//Console.Write("ReverseLevelOrder: \n");
			//TreeUtils<int>.PrintReverseLevelOrder(BST.Root); Console.WriteLine();

			//Console.Write("LevelByLevel: \n");
			//TreeUtils<int>.PrintLevelByLevelOrder(BST.Root); Console.WriteLine();
			//Console.WriteLine("==================================================");

			//Console.WriteLine("Does 3 appear in the BST ? " + BST.Contains(3));
			//TreeUtils<int>.PrintLevelOrder(BST.Root); Console.WriteLine();
			//Console.WriteLine("Trying to remove 3.. Successful ? " + BST.Remove(3));
			//TreeUtils<int>.PrintLevelOrder(BST.Root); Console.WriteLine();
			//Console.WriteLine("==================================================");

			//Console.WriteLine("Size of tree: " + TreeUtils<int>.Size(BST.Root));
			//Console.WriteLine("Height of tree: " + TreeUtils<int>.Height(BST.Root));
			//Console.WriteLine("Is tree a BST? " + BST.CheckBST());
			//Console.WriteLine("==================================================");

			//TreePrinter<int>.PintTree2D(BST.Root);
			//BST.Remove(7);
			//TreePrinter<int>.PintTree2D(BST.Root);
			//BST.Remove(6);
			//TreePrinter<int>.PintTree2D(BST.Root);

			//Console.WriteLine("==================================================");
			//int[] preOrder1 = { 8, 7, 3, 6, 4, 5, 1, 10, 9, 2 };
			//int[] inOrder1 = { 7, 6, 3, 8, 1, 5, 10, 4, 2, 9 };
			//BinNode<int> test1 = TreeUtils<int>.TreeFromInAndPreOrderTrav(inOrder1, preOrder1);
			//Console.WriteLine(test1);
			//TreePrinter<int>.PintTree2D(test1);

			//Console.WriteLine("==================================================");
			//int[] postOrder2 = { 8, 5, 3, 1, 6, 9, 10, 7, 4, 2 };
			//int[] inOrder2 = { 3, 8, 5, 6, 1, 2, 9, 10, 7, 4 };
			//BinNode<int> test2 = TreeUtils<int>.TreeFromInAndPostOrderTrav(inOrder2, postOrder2);
			//Console.WriteLine(test2);
			//TreePrinter<int>.PintTree2D(test2);

			//Console.WriteLine("==================================================");
			//int[] levelOrder3 = { 2, 6, 4, 3, 1, 7, 5, 10, 8, 9 };
			//int[] inOrder3 = { 3, 8, 5, 6, 1, 2, 9, 10, 7, 4 };
			//BinNode<int> test3 = TreeUtils<int>.TreeFromInAndLevelOrderTrav(inOrder3, levelOrder3);
			//Console.WriteLine(test3);
			//TreePrinter<int>.PintTree2D(test3);

			//Console.WriteLine("==================================================");
			//Console.WriteLine("Testing new removal method (for trees with non-unique values): ");
			//// NOTE TO SELF: Create a seperate BST class for tree's with non unique values
			//BST.Add(8); BST.Add(4); BST.Add(12); BST.Add(2); BST.Add(10);

			//TreePrinter<int>.PintTree2D(BST.Root);
			//Console.WriteLine();
			//TreeUtils<int>.PrintInOrderRec(BST.Root);
			//Console.WriteLine();

			//Console.WriteLine("===========================");

			//int removalNum = 4;
			//Console.WriteLine("Removing {0}...", removalNum);
			//BST.Remove(removalNum);
			//TreePrinter<int>.PintTree2D(BST.Root);
			//Console.WriteLine();
			//TreeUtils<int>.PrintInOrderRec(BST.Root);
			//Console.WriteLine();

			Console.WriteLine("===========================");
			BinNode<int> root = new BinNode<int>(2);
			root.Left = new BinNode<int>(2);
			root.Right = new BinNode<int>(3);
			TreeConsolePrinter<int>.PintTree2D(root);
			Console.WriteLine("IsBST: " + BST.CheckBST(root));
		}
	}
}
