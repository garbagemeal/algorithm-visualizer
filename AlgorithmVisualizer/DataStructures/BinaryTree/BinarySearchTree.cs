using System;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public class BinarySearchTree<T> where T : IComparable
	{
		// This implementation does not allow duplicate values!
		// May no longer hold true, needs to be tested!
		
		/* TODO: 
		 * optimize all functions, namely add/remove/etc.. to avoid traversing the tree more than once!
		 * consider a class variable or create inside the method and pass into the recrsive method */


		protected BinNode<T> root;
		public BinNode<T> Root { get { return root; } set { root = value; } }

		public BinarySearchTree() {}
		public BinarySearchTree(BinNode<T> _root)
		{
			root = _root;
		}

		// Returns a boolean result indicating the presence or absance of a value in the BST
		public bool Contains(T val)
		{
			return Contains(root, val);
		}
		private bool Contains(BinNode<T> node, T val)
		{
			if (node == null) return false; // an empty tree does not contain the value
			if (node.Data.CompareTo(val) == 0) return true; // current node is the searched value - return true
			return Contains(node.Data.CompareTo(val) > 0 ? node.Left : node.Right, val); // recur on left or right depending on node's data
		}

		// Adding a node to the tree, returns a boolean result indicating success or failure to add
		public virtual bool Add(T val)
		{
			if (Contains(val)) return false; // if tree contains given value return flase
			root = Add(root, val); // add the node
			return true; // return true (added successfully)
		}
		protected virtual BinNode<T> Add(BinNode<T> node, T val)
		{
			// If node is an empty tree, simply return the new node
			if (node == null) return new BinNode<T>(val);
			// node is not null (empty tree), pick as side for insertion and recur on it:
			if (node.Data.CompareTo(val) > 0) node.Left = Add(node.Left, val);
			else node.Right = Add(node.Right, val);
			// finished recurring, return resulting node
			return node;
		}
		private BinNode<T> AddItr(BinNode<T> node, T val)
		{
			// Create a new node to hold the new value (tmp)
			BinNode<T> tmp = new BinNode<T>(val);
			// if given tree is empty simply return the new node.
			if (node == null) return tmp;
			// Set parentNode as null and curNode as node
			BinNode<T> parentNode = null, curNode = node;
			// find the parent node for insertion by traversing the tree using curNode until hitting null
			while(curNode != null)
			{
				parentNode = curNode;
				curNode = node.Data.CompareTo(val) > 0 ? curNode.Left : curNode.Right;
			}
			// parentNode is now the node that should hold tmp, determine the side for the insertion
			if (node.Data.CompareTo(val) > 0) parentNode.Left = tmp;
			else parentNode.Right = tmp;
			// return the tree (required in case root node has been adjusted)
			return node;
		}

		// Note that this removal method does not assume the tree consists of unique values!
		private bool removedSuccessfuly;
		public virtual bool Remove(T val)
		{
			// assume removal failed
			removedSuccessfuly = false;
			root = Remove(root, val);
			// return removal status
			return removedSuccessfuly;
		}
		protected virtual BinNode<T> Remove(BinNode<T> node, T val)
		{
			// Base case - an empty tree does not contain the value.
			if (node == null) return null;

			// Lookup phase
			if (node.Data.CompareTo(val) > 0) node.Left = Remove(node.Left, val);
			else if (node.Data.CompareTo(val) < 0) node.Right = Remove(node.Right, val);
			// Lookup phase finished, the node from removal has been found
			else
			{
				removedSuccessfuly = true;
				// Has both left & right sub-trees
				if (node.Left != null && node.Right != null)
				{
					// The GetMax method on the left sub-tree is preferred by me because the right sub-tree
					// may contain the value for removal more than once, and the first occourance would be removed!
					// This behavior is undesired because it could potentially remove more than just one node!

					// Get left sub-tree's max value node (as a replacement to the current node for removal)
					BinNode<T> leftSubTreeMax = GetMax(node.Left);
					// Overwrite current nodes value
					node.Data = leftSubTreeMax.Data;
					// Recursively remove the node with leftSubTreeMax's value (referenced by leftSubTreeMax)
					node.Left = Remove(node.Left, node.Data);
				}
				// Has 0 or 1 sub-tree(s), return the non-null sub-tree (if there is 1)
				else return node.Left != null ? node.Left : node.Right;

			}
			return node;
		}
		
		// Min/Max functions, node is assumed to be non null!
		private BinNode<T> GetMin(BinNode<T> node)
		{
			while (node.Left != null)
				node = node.Left;
			return node;
		}
		private BinNode<T> GetMax(BinNode<T> node)
		{
			while (node.Right != null)
				node = node.Right;
			return node;
		}
		private bool IsLeafNode(BinNode<T> node)
		{
			// node is assumed to be non-null!
			return node.Left == null && node.Right == null;
		}

		////Returns true if tree(held by root) conforms to the BST invariant, false otherwise
		//public bool CheckBST()
		//{
		//	return CheckBST(root, int.MinValue, int.MaxValue);
		//}
		//// This implementation assumes values are unique, and so node.Data is exclusive in the BST checks
		//// The issue with this implementation is that it assumes the tree to be of type int
		//private bool CheckBST(BinNode<T> node, int min, int max)
		//{
		//	if (node == null) return true;
		//	if (node.Data < min || node.Data > max) return false;
		//	return CheckBST(node.Left, min, node.Data - 1) && CheckBST(node.Right, node.Data + 1, max);
		//}

		// Returns true if tree (held by root) conforms to the BST invariant, false otherwise
		private BinNode<T> prevNode;
		private bool flag;
		public bool CheckBST()
		{
			prevNode = null;
			flag = false;
			return CheckBST(root);
		}
		// Performs an in-order traversal of the tree comparing each value with its in-order predecessor
		// in other words making sure that the in-order traversal results in unique values of increasing order.
		//public bool CheckBST(BinNode<T> node)
		//{
		//	if (node != null)
		//	{
		//		// recursively check for left sub-tree
		//		if (!CheckBST(node.Left)) return false;
		//		// node's value < prevNode's value that is the in-order predecessor
		//		if (prevNode != null && node.Data.CompareTo(prevNode.Data) <= 0) return false;
		//		// advance prev in-order
		//		prevNode = node;
		//		// recursively check for right sub-tree
		//		// Note: unlike the left sub-tree recurrence while recurring on the right sub-tree
		//		// if the result it true the tree is a BST and we can return true!
		//		// doing so for the left sub-tree recurrence is a mistake!
		//		return CheckBST(node.Right);
		//	}
		//	return true;
		//}

		public bool CheckBST(BinNode<T> node)
		{
			if (node != null)
			{
				if (!CheckBST(node.Left)) return false;
				if (prevNode != null)
				{
					int cmpVal = node.Data.CompareTo(prevNode.Data);
					if (flag)
					{
						flag = false;
						if (cmpVal <= 0) return false;

					}
					if (cmpVal < 0) return false;
				}
				prevNode = node;
				flag = true;
				return CheckBST(node.Right);
			}
			return true;
		}
	}
}
