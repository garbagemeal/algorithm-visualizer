using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.ArrayTracer;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class EagerDijkstrasSSSP : GraphAlgorithm
	{
		private readonly int from, to;
		// Tacers for visuals in panel log
		private ArrayTracer<int> idxTracer, distMapTracer, prevTracer, ipqTracer;

		public EagerDijkstrasSSSP(Graph graph, int _from, int _to) : base(graph)
		{
			from = _from;
			to = _to;
			Solve();
		}

		public override void Solve()
		{
			// Note: algoNum denotes the algorithm number where:
			// 0 is the lazy Dijkstra's SSSP algo variant
			// 1 is the eager Dijkstra's SSSP algo variant
			// If the graph is not positive edge weighted do nothing
			if (!GraphValidator.IsPositiveEdgeWeighted(graph)) return;
			int[] distMap = new int[graph.NodeCount];
			int[] prev = new int[graph.NodeCount];
			// distMap initially hols "infinities" and prev ids of -1 (instead of null)
			for (int i = 0; i < graph.NodeCount; i++)
			{
				distMap[i] = int.MaxValue;
				prev[i] = -1;
			}
			HashSet<int> visited = new HashSet<int>();
			distMap[from] = 0;

			// Running Dijkstra's algo
			bool endReached = Solve(visited, distMap, prev);

			List<int> path = ReconstructPath(to, prev, endReached);

			if (endReached)
			{
				Console.WriteLine($"Shortest path from {from} to {to}:");
				foreach (int nodeId in path) Console.Write(nodeId + " ");
				Console.WriteLine("Cost: " + distMap[to]);
				// Draw SP in green
				for (int i = 0; i < path.Count; i++)
				{
					graph.MarkParticle(path[i], Colors.Green);
					// If not the starting node 
					if (i != 0)
					{
						// Find and draw edge to it in SP
						int at = path[i], prevAt = prev[at], delta = distMap[at] - distMap[prevAt];
						graph.MarkSpring(new Edge(prevAt, at, delta), Colors.Green, Dir.Directed);
					}
					Sleep(700);
				}
			}
			else Console.WriteLine($"No path from {from} to {to}.");
		}
		private bool Solve(HashSet<int> visited, int[] distMap, int[] prev)
		{
			// Eager Dijkstra's algo - O(ElogE/V(V))
			int degree = graph.EdgeCount / graph.NodeCount;
			MinIndexedDHeap<int> ipq = new MinIndexedDHeap<int>(degree, graph.NodeCount);
			ipq.InsertAt(from, 0);

			SetupAndShowTracers(distMap, prev, ipq);

			while (ipq.Count > 0)
			{
				ipqTracer.HighlightAt(0);
				int curNodeId = ipq.PeekMinKeyIndex(), curNodeMinDist = ipq.DequeueMinValue();
				visited.Add(curNodeId);
				graph.MarkParticle(curNodeId, Colors.Orange);
				Sleep(1500);
				ipqTracer.Trace();
				Sleep(1000);
				// Optimization: in case destionation node has been reached (to id)
				if (curNodeId == to)
				{
					return true;
				}
				VisitOutgoingEdges(curNodeId, visited, distMap, prev, ipq);
				graph.MarkParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
				Sleep(1000);
			}
			return false;
		}
		private void VisitOutgoingEdges(int curNodeId, HashSet<int> visited, int[] distMap, int[] prev, MinIndexedDHeap<int> ipq)
		{
			// Assumption(need to verify): stale nodes may not exist in the ipq because
			// any update for distMap in this function is also reflected in ipq.
			if (graph.AdjList[curNodeId] != null)
			{
				foreach (Edge edge in graph.AdjList[curNodeId])
				{
					graph.MarkSpring(edge, Colors.Orange);
					Sleep(1000);
					// Edge relaxation
					int toId = edge.To;
					int newDist = distMap[curNodeId] + edge.Cost;
					// Can't imporove distance by revisiting a node
					if (!visited.Contains(toId) && newDist < distMap[toId])
					{
						graph.MarkSpring(edge, Colors.Red);
						prevTracer.HighlightAt(toId);
						distMapTracer.HighlightAt(toId);
						prev[toId] = curNodeId;
						distMap[toId] = newDist;
						Sleep(1500);
						prevTracer.Trace();
						distMapTracer.Trace();
						// Insert node into ipq with its distance if not present in it,
						// otherwise try improve the value (decrease it)
						if (!ipq.Contains(toId)) ipq.InsertAt(toId, newDist);
						else ipq.DecreaseKey(toId, newDist);
						ipqTracer.Trace();
						Sleep(1000);
					}
					// stale edge (to already visited or no imporvement in cost)
					else graph.MarkSpring(edge, Colors.Blue);
					Sleep(1000);
					graph.MarkSpring(edge, Colors.Visited);
					Sleep(1000);
				}
			}
		}

		private List<int> ReconstructPath(int to, int[] prev, bool endReached)
		{
			// Reconstruct the path using prev array by following predecessors starting
			// from to until -1 is reached, -1 is the starting node's predecessor.
			List<int> path = new List<int>();
			// if to is reachable
			if (endReached)
			{
				for (int at = to; at != -1; at = prev[at]) path.Add(at);
				path.Reverse();
			}
			return path;
		}

		private void SetupAndShowTracers(int[] distMap, int[] prev, MinIndexedDHeap<int> ipq)
		{
			// Visuals(Tracers) for distMap, prev
			int[] idxArr = new int[graph.NodeCount]; for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", 0, 57, 500, 25);
			distMapTracer = new ArrayTracer<int>(distMap, panelLogG, "distMap: ", 0, 84, 500, 25);
			prevTracer = new ArrayTracer<int>(prev, panelLogG, "prev: ", 0, 111, 500, 25);
			ipqTracer = new ArrayTracer<int>(ipq, panelLogG, "IPQ: ", 0, 10, 500, 45);
			// Set width of the name(title) for idxTracer and pervTracer to match distMapTracer's name width(expected to be widest)
			idxTracer.NameOffset = prevTracer.NameOffset = distMapTracer.NameOffset;

			// Trace arrays
			ArrayTracer<int>[] tracers = new ArrayTracer<int>[] { idxTracer, distMapTracer, prevTracer, ipqTracer };
			foreach (ArrayTracer<int> tracer in tracers) tracer.Trace();
			Sleep(2000);
		}
	}
}
