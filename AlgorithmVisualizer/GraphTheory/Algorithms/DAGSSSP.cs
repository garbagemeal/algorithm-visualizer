using System;

using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class DAGSSSP : GraphAlgorithm
	{
		/* This algo can be adjusted to compute longest paths by applying the following steps:
		 * 1. Before running this algo multiply all edge costs by -1
		 * 2. After running this algo the results in distMap by -1
		 * *. At this point distMap[i] is the longest path distance to node i.
		 * 3. Optional: multiply edge costs by -1 to restore graph to initial state.
		 * For generic graphs the above problem is considered NP hard.
		 * 
		 * Remarks: 
		 * Works only for directed acyclic graphs, 
		 * Works with negative edge weights because the graph is acyclic */

		private readonly int from;
		private readonly int[] topSort, distMap;

		public DAGSSSP(Graph graph, int _from = 0) : base(graph)
		{
			from = _from;
			topSort = GetTopologicalOrdering();
			distMap = new int[graph.NodeCount];
		}

		private int[] GetTopologicalOrdering()
		{
			var kahnsTopSortSolver = new KahnsTopSort(graph, vizMode: false);
			kahnsTopSortSolver.Solve();
			return kahnsTopSortSolver.TopOrder;
		}

		public override bool Solve()
		{
			// topSort == null --> graph is not a DAG --> this algo not applicable
			if (topSort == null) return false;
			// Fill distMap array with "inifinities" and set distance to starting node as 0
			for (int i = 0; i < graph.NodeCount; i++) distMap[i] = int.MaxValue;
			distMap[from] = 0;

			Solve(topSort, distMap);

			for (int i = 0; i < distMap.Length; i++)
				Console.WriteLine("Distance to {0}: {1}", i, distMap[i] != int.MaxValue ? distMap[i] + "" : "INF");

			return true;
		}
		private void Solve(int[] topSort, int[] distMap)
		{
			// Method to run the DAGSSSP after topSort and distMap have been prepared
			// Go over each node in the topSort
			for (int i = 0; i < graph.NodeCount; i++)
			{
				// Note: i is the index of curNodeId in topSort
				int curNodeId = topSort[i];
				graph.MarkParticle(curNodeId, Colors.Orange);
				Sleep(1500);
				// If the current node has alrady been reached and has incident edges
				if (distMap[curNodeId] != int.MaxValue && graph.AdjList[curNodeId] != null)
				{
					// foreach edge incident to curNodeId
					foreach (Edge edge in graph.AdjList[curNodeId])
					{
						// Edge relaxation
						graph.MarkSpring(edge, Colors.Orange);
						Sleep(1000);
						// Compute new distance to reach edge.To
						int newDist = distMap[curNodeId] + edge.Cost;
						graph.MarkSpring(edge, newDist < distMap[edge.To] ? Colors.Red : Colors.Blue);
						// Comapre both new and old distances and set to the smaller one
						distMap[edge.To] = Math.Min(distMap[edge.To], newDist);
						Sleep(1000);
						graph.MarkSpring(edge, Colors.Visited);
						Sleep(1000);
					}
				}
				graph.MarkParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
				Sleep(1000);
			}
		}
	}
}
