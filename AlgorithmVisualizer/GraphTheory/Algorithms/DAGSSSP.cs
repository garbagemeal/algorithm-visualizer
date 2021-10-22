using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class DAGSSSP : GraphAlgorithm
	{
		private readonly int from;
		public DAGSSSP(Graph graph, int _from = 0) : base(graph)
		{
			from = _from;
			Solve();
		}

		public override void Solve()
		{
			/* Note:
			 * The longest path can be computed by multiplying all edge cost's by -1, 
			 * running this very same algo and afterwards, again, multiply all edge cost's by -1.
			 * In doing so the longest path will be computed! a problem for which general grahps 
			 * are considered NP Hard! */

			int[] topSort = new KahnsTopSort(graph, vizMode: false).TopOrder;
			// if graph is not a DAG do nothing
			if (topSort == null)
			{
				Console.WriteLine("The given graph is not a DAG!");
				return;
			}
			// a distance array where each index denotes the node id and the value
			// denotes the current shortest distance to it
			int[] distMap = new int[graph.NodeCount];
			// Fill distMap array with "inifinities" and set distance to starting node as 0
			for (int i = 0; i < graph.NodeCount; i++) distMap[i] = int.MaxValue;
			distMap[from] = 0;

			Solve(topSort, distMap);

			for (int i = 0; i < distMap.Length; i++)
				Console.WriteLine("Distance to {0}: {1}",
					i, distMap[i] != int.MaxValue ? distMap[i] + "" : "INF");
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
