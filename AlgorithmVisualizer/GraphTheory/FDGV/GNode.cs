using System;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class GNode : IComparable
	{
		public int Id { get; set; }
		public int Data { get; set; }

		public GNode(int id, int data)
		{
			Id = id;
			Data = data;
		}
		// Returns true if the given object is a GNode and has the same id
		public override bool Equals(object obj) => obj is GNode && Id == ((GNode)obj).Id;
		// Comapre only data
		public int CompareTo(object obj) => Data - ((GNode)obj).Data;
		public override string ToString() => string.Format($"id: {Id}, val: {Data}");
	}
}