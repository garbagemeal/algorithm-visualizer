using System;

namespace AlgorithmVisualizer.GraphTheory
{
	public class Edge : IComparable
	{
		public int From { get; set; }
		public int To { get; set; }
		public int Cost { get; set; }

		public Edge(int from, int to, int cost)
		{
			From = from;
			To = to;
			Cost = cost;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Edge)) return false;
			Edge edge = obj as Edge;
			return From == edge.From && To == edge.To && Cost == edge.Cost;
		} 
		// Comapre only edge costs
		public int CompareTo(object obj) => Cost - (obj as Edge).Cost;
		public override string ToString() => $"({From}, {To}, {Cost})";
		// Returns a new edge where the from/to id's are swapped
		public static Edge ReversedCopy(Edge edge) => new Edge(edge.To, edge.From, edge.Cost);
	}
}