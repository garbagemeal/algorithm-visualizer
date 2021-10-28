using System.Collections.Generic;

using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class DFS : GraphAlgorithm
	{
		private readonly int from, to;
		private readonly HashSet<int> visited;

		public DFS(Graph graph, int _from, int _to) : base(graph)
		{
			from = _from;
			to = _to;
			visited = new HashSet<int>();
		}

		public override bool Solve() => Solve(from);
		private bool Solve(int at)
		{
			visited.Add(at);
			// Mark node on visit
			graph.MarkParticle(at, at == to ? Colors.Red : Colors.Orange);
			Sleep(1500);
			if (at == to) return true;
			foreach (Edge edge in graph.AdjList[at])
			{
				// Does not mark edges to already visited nodes to better show backtracking
				if (!visited.Contains(edge.To))
				{
					// Mark edge on visit
					graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
					Sleep(1000);
					if (Solve(edge.To)) // Current edge is in a path to 'to'
					{
						// Marking SP
						graph.MarkSpring(edge, Colors.Green, Dir.Directed);
						graph.MarkParticle(at, Colors.Green);
						Sleep(1500);
						return true;
					}
					// Unmark edge after visit
					graph.MarkSpring(edge, Colors.Visited);
					Sleep(1000);
				}
			}
			// Unmark node after vist
			graph.MarkParticle(at, Colors.Visited, Colors.VisitedBorder);
			Sleep(1000);
			return false;
		}
	}
}
