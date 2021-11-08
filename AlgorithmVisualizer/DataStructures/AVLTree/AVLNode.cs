using System;

using AlgorithmVisualizer.DataStructures.BinaryTree;

namespace AlgorithmVisualizer.DataStructures.AVLTree
{
	public class AVLNode<T> : BinNode<T> where T : IComparable
	{
		public int Height { get; set; }
		public int BalanceFactor { get; set; }

		public AVLNode(T data) : base(data) { }

		public override string ToString() => $"Data: {Data}, Height: {Height}, BF: {BalanceFactor}";
	}
}
