using System;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public class BinarySearchTree<T> where T : IComparable
	{
		public BinNode<T> Root { get; private set; }
		public int NodeCount { get; private set; }

		public BinarySearchTree() { }
		public BinarySearchTree(BinNode<T> root) => Root = root;

		public bool Contains(T val) => Contains(Root, val);
		private bool Contains(BinNode<T> node, T val)
		{
			// Returns true if given value is present in the BST, false otherwise
			if (node == null) return false;
			int cmpVal = node.Data.CompareTo(val);
			if (cmpVal == 0) return true;
			return Contains(cmpVal > 0 ? node.Left : node.Right, val);
		}

		public bool Add(T val)
		{
			// Adds the given value into the BST assuming it is absent.
			// Returns true if the value was absent and added to the tree, false otherwise
			if (Contains(val)) return false;
			Root = Add(Root, val);
			NodeCount++;
			return true;
		}
		protected virtual BinNode<T> Add(BinNode<T> node, T val)
		{
			if (node == null) return new BinNode<T>(val);
			if (node.Data.CompareTo(val) > 0) node.Left = Add(node.Left, val);
			else node.Right = Add(node.Right, val);
			return node;
		}

		public bool Remove(T val)
		{
			// Removes the given value from the BST assuming it is present.
			// Returns true if the value was present and removed from the tree, false otherwise
			if (!Contains(val)) return false;
			Root = Remove(Root, val);
			NodeCount--;
			return true;
		}
		protected virtual BinNode<T> Remove(BinNode<T> node, T val)
		{
			if (node == null) return null;
			// Lookup phase
			int cmpVal = node.Data.CompareTo(val);
			if (cmpVal > 0) node.Left = Remove(node.Left, val);
			else if (cmpVal < 0) node.Right = Remove(node.Right, val);
			// Node located
			else
			{
				// Has both left & right sub-trees
				if (node.Left != null && node.Right != null)
				{
					// Get left sub-tree's max value node as a replacement to the node for removal
					BinNode<T> leftSubTreeMax = GetMax(node.Left);
					node.Data = leftSubTreeMax.Data;
					// Recursively remove the node with leftSubTreeMax's value
					node.Left = Remove(node.Left, node.Data);
				}
				// Has 0 or 1 sub-tree(s), try return the non-null sub-tree.
				else return node.Left != null ? node.Left : node.Right;
			}
			return node;
		}

		protected BinNode<T> GetMin(BinNode<T> node)
		{
			// Returns the min value in the BST rooted at the given node assuming node != null
			while (node.Left != null)
				node = node.Left;
			return node;
		}
		protected BinNode<T> GetMax(BinNode<T> node)
		{
			// Returns the max value in the BST rooted at the given node assuming node != null
			while (node.Right != null)
				node = node.Right;
			return node;
		}

		public bool IsBST() => IsBST(Root);
		public static bool IsBST(BinNode<T> node) => IsBST(node, null, null);
		private static bool IsBST(BinNode<T> node, BinNode<T> min, BinNode<T> max)
		{
			// Returns true if the given tree conforms to the BST invariant, false otherwise.
			if (node == null) return true;
			if (min != null && node.Data.CompareTo(min.Data) <= 0) return false;
			if (max != null && node.Data.CompareTo(max.Data) >= 0) return false;
			return IsBST(node.Left, min, node) && IsBST(node.Right, node, max);
		}

		public void Print() => TreeConsolePrinter<T>.Print(Root);
	}
}
