using System;
using System.Collections.Generic;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using System.Drawing;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class BFS : GraphAlgorithm
	{
		private readonly int from, to;
		// Used to trace the queue
		private QueueTracer<int> qTracer;

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
			qTracer = new QueueTracer<int>(q, panelLogG, "q: ", new PointF(0, 5), new SizeF(500, 25), 25);
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
				qTracer.Mark(0);
				int at = q.Dequeue();
				// Draw visited node in red if the end node otherwise in orange
				graph.MarkParticle(at, at == to ? Colors.Red : Colors.Orange);
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
						graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
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
						graph.MarkSpring(edge, Colors.Visited);
						Sleep(1000);
					}
				}
				// Draw particle in dark gray (visited)
				graph.MarkParticle(at, Colors.Visited, Colors.VisitedBorder);
				Sleep(1000);
			}
			return false;
		}
	}
}
