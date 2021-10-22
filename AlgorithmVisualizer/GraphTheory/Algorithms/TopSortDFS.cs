using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class TopSortDFS : GraphAlgorithm
	{
		// Used to trace the stack containing the topological order
		private StackTracer<int> topOrderStkTracer;
		// topOrder 
		private int[] topOrder;
		public int[] TopOrder { get { return topOrder; } }

		public TopSortDFS(Graph graph) : base(graph) => Solve();

		public override void Solve()
		{
			// Returns the graph's topologial ordering using DFS
			// If the graph is not a DAG there is no top order.
			if (!GraphValidator.IsDAG(graph)) topOrder = null;

			HashSet<int> visited = new HashSet<int>();
			Stack<int> topOrderStk = new Stack<int>();
			topOrderStkTracer = new StackTracer<int>(topOrderStk, panelLogG, "Top order: ", new PointF(0, 10), new SizeF(500, 45), 45);
			for (int nodeId = 0; nodeId < graph.NodeCount; nodeId++)
				if (!visited.Contains(nodeId)) Solve(nodeId, visited, topOrderStk);
			// Popping the stack into the array - topOrder
			topOrder = new int[topOrderStk.Count];
			for (int i = 0; i < topOrder.Length; i++) topOrder[i] = topOrderStk.Pop();
		}
		private void Solve(int at, HashSet<int> visited, Stack<int> topOrderStk)
		{
			// top sort into topSortStk using DFS
			visited.Add(at);
			graph.MarkParticle(at, Colors.Orange);
			Sleep(1500);
			if (graph.AdjList[at] != null)
				foreach (Edge edge in graph.AdjList[at])
				{
					int to = edge.To;
					if (!visited.Contains(to))
					{
						graph.MarkSpring(edge, Colors.Orange);
						Sleep(1000);
						Solve(to, visited, topOrderStk);
						graph.MarkSpring(edge, Colors.Visited);
						Sleep(1000);
					}
				}
			// Push values into the stack upon backtracking
			topOrderStk.Push(at);
			graph.MarkParticle(at, Colors.Blue);
			topOrderStkTracer.Mark(0);
			Sleep(1000);
			graph.MarkParticle(at, Colors.Visited, Colors.VisitedBorder);
			topOrderStkTracer.Trace();
			Sleep(1000);
		}
	}
}
