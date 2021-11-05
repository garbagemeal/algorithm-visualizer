using System;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public class BinNode<T> where T : IComparable
	{
		// A class to represent a generic binary node. T must be a comparable type.
		public T Data { get; set; }
		public BinNode<T> Left { get; set; }
		public BinNode<T> Right { get; set; }

		public BinNode(T data) => Data = data;

		public override string ToString() => Data.ToString();
	}
}
