using System;

using AlgorithmVisualizer.DataStructures.BinaryTree;

namespace AlgorithmVisualizer.DataStructures.AVLTree
{
	public class AVLTree<T> : BinarySearchTree<T> where T : IComparable
	{
		// AVL Tree - a self balancing binary search tree

		public AVLTree() : base()
		{
		}
		public AVLTree(AVLNode<T> root) : base(root)
		{
		}
		/* Rotation examples:
		 * ==================
		 * Right rotation:
		 *    A
		 *  B   C
		 * D E
		 * transforms into:
		 *    B
		 *  D   A
		 *     E C
		 * ==================
		 * ==================
		 * Left rotation:
		 *    A
		 *  B   C
		 *     D E
		 * transforms into:
		 *    C
		 *  A   E
		 * B D
		 * ==================
		 */
		private AVLNode<T> RightRotation(AVLNode<T> a)
		{
			// assumes a is not null, does not take into accout parent node references!
			AVLNode<T> b = (AVLNode<T>)a.Left;
			a.Left = b.Right;
			b.Right = a;
			return b;
		}
		private AVLNode<T> LeftRotation(AVLNode<T> a)
		{
			// assumes a is not null, does not take into accout parent node references!
			AVLNode<T> c = (AVLNode<T>)a.Right;
			a.Right = c.Left;
			c.Left = a;
			return c;
		}

		public override bool Add(T val)
		{
			if (val == null) return false;
			root = Add(root, val);
			return true;
		}
		//protected override BinNode<T> Add(BinNode<T> node, T val)
		//{
		//	if (node == null) return new BinNode<T>(val);
		//	if (node.Data.CompareTo(val) > 0) Add(node.Left, val);
		//}
		public override bool Remove(T val)
		{
			return true;
		}
	}
}
