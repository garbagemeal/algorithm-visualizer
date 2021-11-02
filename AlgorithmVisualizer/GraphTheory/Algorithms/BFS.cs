using System.Collections.Generic;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using System.Drawing;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class BFS : GraphAlgorithm
	{
		private readonly int from, to;
		private readonly Queue<int> q;
		private readonly HashSet<int> visited;

		private readonly QueueTracer<int> qTracer;

		public BFS(Graph graph, int _from, int _to) : base(graph)
		{
			from = _from;
			to = _to;
			
			q = new Queue<int>();
			visited = new HashSet<int>();
			qTracer = new QueueTracer<int>(q, panelLogG, "q: ", new PointF(0, 5), new SizeF(500, 40));
			q.Enqueue(from);
			visited.Add(from);
		}

		public override bool Solve()
		{
			qTracer.Trace();
			Sleep(Delay.Long);
			while (q.Count > 0)
			{
				qTracer.Mark(0, Colors.Red);
				int at = q.Dequeue();
				// Mark node on visit
				graph.MarkParticle(at, at == to ? Colors.Red : Colors.Orange);
				Sleep(Delay.Medium);
				qTracer.Trace();
				Sleep(Delay.Medium);
				if (at == to) return true;
				VisitNeighbors(at);
				// Unmark node after visit
				graph.MarkParticle(at, Colors.Visited, Colors.VisitedBorder);
				Sleep(Delay.Medium);
			}
			return false;
		}
		private void VisitNeighbors(int at)
		{
			foreach (Edge edge in graph.AdjList[at])
			{
				// Mark edge on visit
				graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
				Sleep(Delay.Medium);
				// Avoid edges to visited nodes
				if (!visited.Contains(edge.To))
				{
					visited.Add(edge.To);
					q.Enqueue(edge.To);
					qTracer.Trace();
				}
				// Unmark edge after visit
				graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
				Sleep(Delay.Medium);
			}
		}
	}
}
