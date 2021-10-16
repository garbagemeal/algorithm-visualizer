using System;

namespace AlgorithmVisualizer.DataStructures.BinaryTree
{
	public class BinNode<T> where T : IComparable
	{
		protected T data;
		protected BinNode<T> left, right;

		public BinNode(T _data)
		{
			data = _data;
		}

		// Getters/Setters:
		public T Data { get { return data; } set { data = value; } }
		public BinNode<T> Left { get { return left; } set { left = value; } }
		public BinNode<T> Right { get { return right; } set { right = value; } }

		public override string ToString()
		{
			return data.ToString();
		}
	}
}
