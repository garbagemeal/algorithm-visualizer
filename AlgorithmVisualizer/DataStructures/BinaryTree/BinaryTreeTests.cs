using System;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	static class BinaryTreeTests
	{

		// A class to test BinarySearchTree, TreeUtils

		public static void RunTests()
		{
			BinarySearchTree<int> BST = new BinarySearchTree<int>();
			int[] vals = new int[] { 3, 1, 5, 0, 2, 4, 6 };
			//int[] vals = new int[] { 5, 1, 3, 0, 2, 4, 6 };
			foreach (int val in vals) if (!BST.Add(val)) Console.WriteLine($"Failed to add {val}");
			BST.Print();
			Console.WriteLine("==================================================");


			Console.WriteLine("Size of tree: " + TreeUtils<int>.Size(BST.Root));
			Console.WriteLine("Height of tree: " + TreeUtils<int>.Height(BST.Root));
			Console.WriteLine("Is BST ? " + BST.IsBST());
			bool containsAllInsertedValues = true;
			foreach (int val in vals) if (!BST.Contains(val)) containsAllInsertedValues = false;
			Console.WriteLine($"Contains all inserted values ? {containsAllInsertedValues}");
			Console.WriteLine("==================================================");


			Console.Write("PreOrder: \n");
			Console.Write("Rec: "); TreeUtils<int>.PrintPreOrderRec(BST.Root); Console.WriteLine();
			Console.Write("Itr: "); TreeUtils<int>.PrintPreOrderItr(BST.Root); Console.WriteLine();
			Console.Write("Itr: "); TreeUtils<int>.PrintPreOrderItrMorris(BST.Root); Console.WriteLine();
			
			Console.Write("InOrder: \n");
			Console.Write("Rec: "); TreeUtils<int>.PrintInOrderRec(BST.Root); Console.WriteLine();
			Console.Write("Itr: "); TreeUtils<int>.PrintInOrderItr(BST.Root); Console.WriteLine();
			Console.Write("Itr: "); TreeUtils<int>.PrintInOrderItrMorris(BST.Root); Console.WriteLine();

			Console.Write("PostOrder: \n");
			Console.Write("Rec: "); TreeUtils<int>.PrintPostOrderRec(BST.Root); Console.WriteLine();
			Console.Write("Itr: "); TreeUtils<int>.PrintPostOrderItr(BST.Root); Console.WriteLine();

			Console.Write("LevelOrder: \n");
			TreeUtils<int>.PrintLevelOrder(BST.Root); Console.WriteLine();

			Console.Write("ReverseLevelOrder: \n");
			TreeUtils<int>.PrintReverseLevelOrder(BST.Root); Console.WriteLine();

			Console.Write("LevelByLevel: \n");
			TreeUtils<int>.PrintLevelByLevelOrder(BST.Root); Console.WriteLine();
			Console.WriteLine("==================================================");


			foreach (int val in vals) if (!BST.Remove(val)) Console.WriteLine($"Failed to remove {val}");
			BST.Print();
			Console.WriteLine($"Is empty ? {BST.NodeCount == 0}");
		}
	}
}
