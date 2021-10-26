using System;
using System.Collections.Generic;
using AlgorithmVisualizer.GraphTheory.Algorithms;

namespace AlgorithmVisualizer.GraphTheory.Utils
{
	static class GraphValidator
	{
		// Util class with methods to check for some properties of the graph

		public static bool IsUndirected(Graph graph)
		{
			// for each edgelist in g
			foreach (List<Edge> edgeList in graph.AdjList.Values)
			{
				// foreach edge (u, v, x) in g[u]
				foreach (Edge edge in edgeList)
				{
					// if g[v] contains (v, u, x)
					// note that it may not contain (v, u, y) because y != x (see "GraphVisualizer.cs" for more details)
					if (!graph.AdjList[edge.To].Contains(Edge.ReversedCopy(edge)))
					{
						Console.WriteLine($"Found a directed edge: {edge}\nThe graph is not undirected!");
						return false;
					}
				}
			}
			return true;
		}
		public static bool IsConnectedUndirected(Graph graph)
		{
			// Run connected components algo on the graph without visuals and
			// check if there is only 1 component (meaning the graph is connected)
			// Note: ConnectedComponentsDisjointSet ensures G is undirected.
			var solver = new ConnectedComponentsDisjointSet(graph, vizMode: false);
			if (solver.Solve() && solver.ComponentCount == 1) return true;
			Console.WriteLine("The graph is not connected and undirected!");
			return false;
		}
		public static bool IsDAG(Graph graph)
		{
			// Checks if the graph is a DAG using Kahn's algo
			var solver = new KahnsTopSort(graph, vizMode: false);
			if (solver.Solve()) return true;
			Console.WriteLine("Graph is not a DAG!");
			return false;
		}
		public static bool IsPositiveEdgeWeighted(Graph graph)
		{
			// Checks if all the edges in the graph have a positive weight
			// Important for some greedy algorithms, i.e, Dijkstra's.
			foreach (List<Edge> edgeList in graph.AdjList.Values)
			{
				foreach (Edge edge in edgeList)
				{
					if (edge.Cost < 0)
					{
						Console.WriteLine("The graph is not positive edge weighted!");
						return false;
					}
				}
			}
			return true;
		}
	}
}
