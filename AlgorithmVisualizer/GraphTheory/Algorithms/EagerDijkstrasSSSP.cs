using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using AlgorithmVisualizer.Utils;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class EagerDijkstrasSSSP : GraphAlgorithm
	{
		/* Runtime is O(VlogE/V(V) + ElogE/V(V)) = O((V + E)(logE/V(V))), however because
		 * only simple graphs are supported (not multi graphs), E = V(V - 1) * 2 and thus:
		 * E = O(V^2). 
		 * Because E = O(V^2), the expression O((V + E)(logE/V(V))) can be simplified to:
		 * O(ElogE/V(V)) */

		private readonly int from, to;
		private readonly int[] distMap, prev;
		private readonly HashSet<int> visited;
		private readonly MinIndexedDHeap<int> ipq;

		// Tacers for visuals in panel log
		private ArrayTracer<int> idxTracer, distMapTracer, prevTracer;
		private IPQTracer<int> ipqTracer;

		public EagerDijkstrasSSSP(Graph graph, int _from, int _to) : base(graph)
		{
			from = _from;
			to = _to;

			visited = new HashSet<int>();
			distMap = new int[graph.NodeCount];
			prev = new int[graph.NodeCount];
			distMap.Fill(int.MaxValue);
			prev.Fill(-1);
			distMap[from] = 0;
			int degree = graph.EdgeCount / graph.NodeCount;
			ipq = new MinIndexedDHeap<int>(degree, graph.NodeCount);
		}

		public override bool Solve()
		{
			if (!GraphValidator.IsPositiveEdgeWeighted(graph)) return false;

			bool endReached = false;
			ipq.InsertAt(from, 0);
			SetupAndShowTracers();
			while (ipq.Count > 0 && !endReached)
			{
				ipqTracer.Mark(0, Colors.Red);
				int curNodeId = ipq.DequeueMinKeyIndex();
				visited.Add(curNodeId);
				graph.MarkParticle(curNodeId, Colors.Orange);
				Sleep(Delay.Long);
				ipqTracer.Trace();
				Sleep(Delay.Medium);
				if (curNodeId == to) endReached = true;
				else
				{
					VisitNeighbors(curNodeId);
					graph.MarkParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
					Sleep(Delay.Medium);
				}
			}
		
			if (endReached) MarkSP();
			else Console.WriteLine($"No path from {from} to {to}.");
			return endReached;
		}
		private void VisitNeighbors(int curNodeId)
		{
			foreach (Edge edge in graph.AdjList[curNodeId])
			{
				graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
				Sleep(Delay.Medium);
				RelaxEdge(edge, curNodeId);
				Sleep(Delay.Medium);
				graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
				Sleep(Delay.Medium);
			}
		}
		private void RelaxEdge(Edge edge, int curNodeId)
		{
			int toId = edge.To;
			int newDist = distMap[curNodeId] + edge.Cost;
			// Can't imporove distance by revisiting a node
			if (!visited.Contains(toId) && newDist < distMap[toId])
			{
				graph.MarkSpring(edge, Colors.Red, Dir.Directed);
				prevTracer.Mark(toId, Colors.Red);
				distMapTracer.Mark(toId, Colors.Red);
				prev[toId] = curNodeId;
				distMap[toId] = newDist;
				Sleep(Delay.Long);
				prevTracer.Trace();
				distMapTracer.Trace();
				// Insert toId into ipq if not present else decrease key
				if (!ipq.Contains(toId)) ipq.InsertAt(toId, newDist);
				else ipq.DecreaseKey(toId, newDist);
				ipqTracer.Trace();
				Sleep(Delay.Medium);
			}
			else graph.MarkSpring(edge, Colors.Blue, Dir.Directed);
		}

		private List<int> ReconstructPath()
		{
			// 'to' assumed to be reachable from the start node 'from'.
			// Reconstruct the path using prev array by following predecessors starting
			// from 'to' until -1 is reached, -1 is the starting node's predecessor.
			List<int> path = new List<int>();
			for (int at = to; at != -1; at = prev[at]) path.Add(at);
			path.Reverse();
			return path;
		}
		private void MarkSP()
		{
			List<int> path = ReconstructPath();
			Console.WriteLine($"Shortest path from {from} to {to}:");
			foreach (int nodeId in path) Console.Write(nodeId + " ");
			Console.WriteLine("Cost: " + distMap[to]);
			// Marking the SP
			for (int i = 0; i < path.Count; i++)
			{
				graph.MarkParticle(path[i], Colors.Green);
				// If not the starting node 
				if (i != 0)
				{
					// Find and draw the edge to it in the SP
					int at = path[i], prevAt = prev[at], delta = distMap[at] - distMap[prevAt];
					graph.MarkSpring(new Edge(prevAt, at, delta), Colors.Green, Dir.Directed);
				}
				Sleep(Delay.Short);
			}
		}

		private void SetupAndShowTracers()
		{
			// Setup tracers
			int[] idxArr = new int[graph.NodeCount]; for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", new PointF(0, 57), new SizeF(500, 25));
			distMapTracer = new ArrayTracer<int>(distMap, panelLogG, "distMap: ", new PointF(0, 84), new SizeF(500, 25));
			prevTracer = new ArrayTracer<int>(prev, panelLogG, "prev: ", new PointF(0, 111), new SizeF(500, 25));
			ipqTracer = new IPQTracer<int>(ipq, panelLogG, "IPQ: ", new PointF(0, 10), new SizeF(500, 45));

			// Setting the title width of all tracers to math the longest name's width
			idxTracer.TitleSize = prevTracer.TitleSize = distMapTracer.TitleSize;

			// Trace arrays
			AbstractArrayTracer<int>[] tracers = new AbstractArrayTracer<int>[] { idxTracer, distMapTracer, prevTracer };
			ipqTracer.Trace();
			foreach (AbstractArrayTracer<int> tracer in tracers) tracer.Trace();
			Sleep(Delay.VeryLong);
		}
	}
}