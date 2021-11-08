using System;

using AlgorithmVisualizer.DataStructures.BinaryTree;

namespace AlgorithmVisualizer.DataStructures.AVLTree
{
	public class AVLTree<T> : BinarySearchTree<T> where T : IComparable
	{
		/* A self balancing binary search tree, each node stores the height and the balance factor.
		 * 
		 * Let BF denote the balance factor,
		 * lh, rh denote the heights of left/right sub-trees:
		 * BF = rh - lh
		 * 
		 * AVL trees maintain the following invariant: for each node, -1 <= BF <= 1
		 * BF == -2 implies tree is left heavy, BF == 2 implies tree is right heavy */

		public AVLTree() : base() { }
		public AVLTree(AVLNode<T> root) : base(root) { }

		protected override BinNode<T> Add(BinNode<T> node, T val)
		{
			if (node == null) return new AVLNode<T>(val);
			if (node.Data.CompareTo(val) > 0) node.Left = Add(node.Left, val);
			else node.Right = Add(node.Right, val);
			// Update height and BF for node, balnace and return the tree rooted at 'node'
			UpdateHeightAndBF(node as AVLNode<T>);
			return Balance(node as AVLNode<T>);
		}
		protected override BinNode<T> Remove(BinNode<T> node, T val)
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
			// Update height and BF for 'node', balnace and return the tree rooted at 'node'
			UpdateHeightAndBF(node as AVLNode<T>);
			return Balance(node as AVLNode<T>);
		}

		#region Rotations & balancing
		private AVLNode<T> RotateLeft(AVLNode<T> node)
		{
			// Perform a left rotation on 'node'
			AVLNode<T> newParent = node.Right as AVLNode<T>;
			node.Right = newParent.Left;
			newParent.Left = node;
			// Update height and BF for the rotated nodes
			UpdateHeightAndBF(node);
			UpdateHeightAndBF(newParent);
			// Return the new parent replacing 'node'
			return newParent;
		}
		private AVLNode<T> RotateRight(AVLNode<T> node)
		{
			// Perform a right rotation on 'node'
			AVLNode<T> newParent = node.Left as AVLNode<T>;
			node.Left = newParent.Right;
			newParent.Right = node;
			// Update height and BF for the rotated nodes
			UpdateHeightAndBF(node);
			UpdateHeightAndBF(newParent);
			// Return the new parent replacing 'node'
			return newParent;
		}
		private AVLNode<T> LLCase(AVLNode<T> node)
		{
			Console.WriteLine($"LL on {node.Data}");
			return RotateRight(node);
		}
		private AVLNode<T> RRCase(AVLNode<T> node)
		{
			Console.WriteLine($"RR on {node.Data}");
			return RotateLeft(node);
		}
		private AVLNode<T> LRCase(AVLNode<T> node)
		{
			Console.WriteLine($"LR on {node.Data}");
			node.Left = RotateLeft(node.Left as AVLNode<T>);
			return LLCase(node);
		}
		private AVLNode<T> RLCase(AVLNode<T> node)
		{
			Console.WriteLine($"RL on {node.Data}");
			node.Right = RotateRight(node.Right as AVLNode<T>);
			return RRCase(node);
		}

		private AVLNode<T> Balance(AVLNode<T> node)
		{
			/* Ensures the tree rooted at 'node' maintains the AVL tree invariant, the new
			 * root node after the rotation is returned
			 * 
			 * Cases:
			 * LL: -2, -1
			 * LR: -2, +1
			 * RR: +2, +1
			 * RL: +2, -1 */
			if (node.BalanceFactor == -2)
			{
				if (((AVLNode<T>)node.Left).BalanceFactor <= 0) return LLCase(node);
				return LRCase(node);
			}
			if (node.BalanceFactor == 2)
			{
				if (((AVLNode<T>)node.Right).BalanceFactor >= 0) return RRCase(node);
				return RLCase(node);
			}
			return node;
		}

		private void UpdateHeightAndBF(AVLNode<T> node)
		{
			// Get height of left/right sub-trees of 'node'
			int lh = node.Left == null ? -1 : ((AVLNode<T>)node.Left).Height,
				rh = node.Right == null ? -1 : ((AVLNode<T>)node.Right).Height;
			// Update height and BF for 'node'
			node.Height = 1 + Math.Max(lh, rh);
			node.BalanceFactor = rh - lh;
		}
		#endregion
	}
}
