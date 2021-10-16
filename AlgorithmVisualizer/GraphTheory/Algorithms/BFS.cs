using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.ArrayTracer;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class BFS : GraphAlgorithm
	{
		private readonly int from, to;
		// Used to trace the queue
		private ArrayTracer<int> qTracer;

		public BFS(Graph graph, int _from, int _to) : base(graph)
		{
			from = _from;
			to = _to;

			Solve();
		}

		public override void Solve()
		{
			// BFS to check connectivity - setup
			Queue<int> q = new Queue<int>();
			HashSet<int> visited = new HashSet<int>();
			q.Enqueue(from);
			visited.Add(from);
			qTracer = new ArrayTracer<int>(q, panelLogG, "q: ", 0, 5, 500, 25);
			qTracer.Trace();
			Sleep(1500);
			bool reachedTo = Solve(q, visited);
			Console.WriteLine("There exists {0} path from {1} to {2}",
				reachedTo ? "a" : "no", from, to);
		}
		private bool Solve(Queue<int> q, HashSet<int> visited)
		{
			// BFS to check connectivity
			while (q.Count > 0)
			{
				qTracer.HighlightAt(0);
				int at = q.Dequeue();
				// Draw visited node in red if the end node otherwise in orange
				graph.DrawParticle(at, at == to ? Colors.Red : Colors.Orange);
				Sleep(1000);
				qTracer.Trace();
				Sleep(1000);
				// if end node reached
				if (at == to) return true;
				// end node was not reached
				// if at has outgoing edges
				if (graph.AdjList[at] != null)
				{
					foreach (Edge edge in graph.AdjList[at])
					{
						// Draw edge in orange before visiting. last argument(0) indicates
						// a directed edge (edge.From --> edge.To)
						graph.RedrawSpring(edge, Colors.Orange, 0);
						Sleep(1000);
						// avoid edges to visited nodes
						if (!visited.Contains(edge.To))
						{
							visited.Add(edge.To);
							q.Enqueue(edge.To);
							qTracer.Trace();
						}
						// Draw visited edge in dark gray
						// Note: the last argument is ommited, meaning it defaults to -1
						// the edge is drawn as it originally appeared in the graph
						graph.RedrawSpring(edge, Colors.Visited);
						Sleep(1000);
					}
				}
				// Draw particle in dark gray (visited)
				graph.DrawParticle(at, Colors.Visited, Colors.VisitedBorder);
				Sleep(1000);
			}
			return false;
		}
	}
}
