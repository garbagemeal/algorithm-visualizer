using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class LazyDijkstrasSSSP : GraphAlgorithm
	{
		private readonly int from, to;
		// Tracers
		private ArrayTracer<int> idxTracer, distMapTracer, prevTracer;
		private HeapTracer<GNode> heapTracer;

		public LazyDijkstrasSSSP(Graph graph, int _from, int _to) : base(graph)
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
			// Lazy Dijkstra's algo - O(Elog(V))
			// Binary min proiority queue to hold most promising nodes
			// Note that duplicate nodes are created with the same id's of existing nodes,
			// and the Data is set to the current shortest distance to the node as a comperator
			// for the MinHeap
			BinaryMinHeap<GNode> heap = new BinaryMinHeap<GNode>();
			heap.Enqueue(new GNode(from, 0));

			while (heap.Count > 0)
			{
				heapTracer.Mark(0);
				GNode curNode = heap.Dequeue();
				int curNodeId = curNode.Id, curNodeMinDist = curNode.Data;
				visited.Add(curNodeId);
				graph.MarkParticle(curNodeId, Colors.Orange);
				Sleep(1500);
				heapTracer.Trace();
				Sleep(1000);
				// Optimization: in case destionation node has been reached (to id)
				if (curNodeId == to)
				{
					// This can be done because Dijksta's algo will never update the distance
					// to a node already removed from the queue (an already visited node), in fact
					// it is assumed that the distance to already visited nodes is optimal and can't be improved!
					return true;
				}
				VisitOutgoingEdges(curNodeId, curNodeMinDist, visited, distMap, prev, heap);
				graph.MarkParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
				Sleep(1000);
			}
			return false;
		}
		private void VisitOutgoingEdges(int curNodeId, int curNodeMinDist, HashSet<int> visited, int[] distMap, int[] prev, BinaryMinHeap<GNode> heap)
		{
			/* Ignore stale nodes.
			 * Required for the lazy implementation because when updating distMap a new 
			 * (id, dist) mapping is inserted into the heap, without possibly removing the 
			 * old mapping of the same node id to a worse(greater) distance. */
			if (curNodeMinDist <= distMap[curNodeId] && graph.AdjList[curNodeId] != null)
			{
				foreach (Edge edge in graph.AdjList[curNodeId])
				{
					// Edge relaxation
					graph.MarkSpring(edge, Colors.Orange);
					Sleep(1000);
					int toId = edge.To;
					int newDist = distMap[curNodeId] + edge.Cost;
					// Can't imporove distance by revisiting a node
					if (!visited.Contains(toId) && newDist < distMap[toId])
					{
						graph.MarkSpring(edge, Colors.Red);
						prevTracer.Mark(toId);
						distMapTracer.Mark(toId);
						prev[toId] = curNodeId;
						distMap[toId] = newDist;
						Sleep(1500);
						prevTracer.Trace();
						distMapTracer.Trace();
						heap.Enqueue(new GNode(toId, newDist));
						heapTracer.Trace();
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

		private void SetupAndShowTracers(int[] distMap, int[] prev, BinaryMinHeap<GNode> heap)
		{
			// Visuals(Tracers) for distMap, prev
			int[] idxArr = new int[graph.NodeCount]; for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", new PointF(0, 57), new SizeF(500, 25), 25);
			distMapTracer = new ArrayTracer<int>(distMap, panelLogG, "distMap: ", new PointF(0, 84), new SizeF(500, 25), 25);
			prevTracer = new ArrayTracer<int>(prev, panelLogG, "prev: ", new PointF(0, 111), new SizeF(500, 25), 25);
			heapTracer = new HeapTracer<GNode>(heap, panelLogG, "Heap: ", new PointF(0, 10), new SizeF(500, 45), 45);

			// Set width of the name(title) for idxTracer and pervTracer to match distMapTracer's name width(expected to be widest)
			idxTracer.TitleSize = prevTracer.TitleSize = distMapTracer.TitleSize;
			// Trace arrays
			idxTracer.Trace(); prevTracer.Trace();
			distMapTracer.Trace(); heapTracer.Trace();
			Sleep(2000);
		}
	}
}
