using System;

using AlgorithmVisualizer.DataStructures.BinaryTree;

namespace AlgorithmVisualizer.DataStructures.AVLTree
{
	public class AVLNode<T> : BinNode<T> where T : IComparable
	{
		// Ref to parent node (null for root node)
		//private AVLNode<T> parent;
		private int height, balanceFactor;
		//public AVLNode<T> Parent { get { return parent; } set { parent = value; } }
		public int Height { get { return height; } set { height = value; } }
		public int BalanceFactor { get { return balanceFactor; } set { balanceFactor = value; } }

		public AVLNode(T data) : base(data)
		{
		}

		//public AVLNode(T _data, AVLNode<T> _parent) : base(_data)
		//{
		//	parent = _parent;
		//}


		public override string ToString()
		{
			return $"Data: {data}, Height: {height}, BF: {balanceFactor}";
		}
	}
}
