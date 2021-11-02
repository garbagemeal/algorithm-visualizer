using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class TopSortDFS : GraphAlgorithm
	{
		private HashSet<int> visited = new HashSet<int>();
		private Stack<int> topOrderStk = new Stack<int>();
		public int[] TopOrder { get; private set; }

		// Used to trace the stack containing the topological order
		private StackTracer<int> topOrderStkTracer;
		
		public TopSortDFS(Graph graph) : base(graph)
		{
			visited = new HashSet<int>();
			topOrderStk = new Stack<int>();
			TopOrder = new int[topOrderStk.Count];

			topOrderStkTracer = new StackTracer<int>(topOrderStk, panelLogG, "Top order: ", new PointF(0, 10), new SizeF(500, 45));
		}

		public override bool Solve()
		{
			// Returns the graph's topologial ordering using DFS
			// If the graph is not a DAG there is no top order.
			if (!GraphValidator.IsDAG(graph)) return false;
			
			topOrderStkTracer.Trace();

			for (int nodeId = 0; nodeId < graph.NodeCount; nodeId++)
				if (!visited.Contains(nodeId)) Solve(nodeId, visited, topOrderStk);

			// Popping the stack into the array - topOrder
			for (int i = 0; i < TopOrder.Length; i++) TopOrder[i] = topOrderStk.Pop();

			return true;
		}
		private void Solve(int at, HashSet<int> visited, Stack<int> topOrderStk)
		{
			// DFS to find top sort by pushing nodes into topOrderStk after visiting all
			// of the nodes neighbors
			visited.Add(at);
			graph.MarkParticle(at, Colors.Orange);
			Sleep(Delay.Long);
			foreach (Edge edge in graph.AdjList[at])
			{
				if (!visited.Contains(edge.To))
				{
					graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
					Sleep(Delay.Medium);
					Solve(edge.To, visited, topOrderStk);
					graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
					Sleep(Delay.Medium);
				}
			}
			topOrderStk.Push(at);
			graph.MarkParticle(at, Colors.Red);
			topOrderStkTracer.Mark(0, Colors.Red);
			Sleep(Delay.Medium);
			graph.MarkParticle(at, Colors.Visited, Colors.VisitedBorder);
			topOrderStkTracer.Trace();
			Sleep(Delay.Medium);
		}
	}
}
