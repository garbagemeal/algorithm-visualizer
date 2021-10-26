using System;
using System.Collections.Generic;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class BellmanFords : GraphAlgorithm
	{
		private readonly int from;
		public BellmanFords(Graph graph, int _from) : base(graph)
		{
			from = _from;
		}

		public override bool Solve()
		{
			// SSSP algo, detects negative cycles. O(VE)
			// For dense graphs will be O(V^3) in which case an adjacency matrix is better
			// Proof of correctness(starts at 18:00):
			// https://www.youtube.com/watch?v=ozsuci5pIso&list=PLUl4u3cNGP61Oq3tWYp6V_F-5jb5L2iHb&index=17&ab_channel=MITOpenCourseWare

			int[] distMap = new int[graph.NodeCount];
			for (int i = 0; i < graph.NodeCount; i++) distMap[i] = i == from ? 0 : int.MaxValue;
			// Find the SSSP for each vertex by relaxing each edge V-1 times.
			for (int i = 1; i < graph.NodeCount; i++)
			{
				foreach (List<Edge> edgeList in graph.AdjList.Values)
				{
					foreach (Edge edge in edgeList)
					{
						// Edge relaxation
						int newCost = distMap[edge.From] + edge.Cost;
						if (newCost < distMap[edge.To]) distMap[edge.To] = newCost;
					}
				}
			}
			foreach (List<Edge> edgeList in graph.AdjList.Values)
			{
				foreach (Edge edge in edgeList)
				{
					// Edge relaxation, if the edge can be relaxed then the destination
					// node is part of negative a cycle, distMap[edge.To] becomes -INF
					int newCost = distMap[edge.From] + edge.Cost;
					if (newCost < distMap[edge.To]) distMap[edge.To] = int.MinValue;
				}
			}
			/* +INF  : unreachable node
			 * -INF  : node in a negative a cycle
			 * other : reachable node with no negative cycles on the SP nor any other path
			 * leading to that node */
			for (int i = 0; i < graph.NodeCount; i++)
			{
				int dist = distMap[i];
				string distAsStr =
					dist == int.MaxValue ? "+INF" :
					dist == int.MinValue ? "-INF" :
					dist.ToString();
				Console.WriteLine($"DistMap[{i}] = {distAsStr}");
			}
			return true;
		}
	}
}
