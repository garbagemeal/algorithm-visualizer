using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class LazyPrimsMST : GraphAlgorithm
	{
		private readonly int from;
		// Used to trace the heap
		private HeapTracer<Edge> heapTracer;

		public LazyPrimsMST(Graph graph, int from = 0) : base(graph)
		{
			this.from = from;
		}

		public override bool Solve()
		{
			// Find the graph's MST - setup
			// If the graph is not connected does nothing
			if (!GraphValidator.IsConnectedUndirected(graph)) return false;
			BinaryMinHeap<Edge> heap = new BinaryMinHeap<Edge>();
			HashSet<int> visited = new HashSet<int>();
			heapTracer = new HeapTracer<Edge>(heap, panelLogG, "Heap: ", new PointF(0, 10), new SizeF(500, 50));
			(int Cost, List<Edge> Edges) = Solve(heap, visited);
			// If the edge list is null then there is no min spanning tree
			// This case is not to be confused with an empty (non-null) list
			// represents an MST of a single node with no edes.
			if (Edges == null)
			{
				Console.WriteLine("There is no MST for the given graph");
				return false;
			}
			else
			{
				Console.WriteLine("MST Details: \nCost: {0}, Edge list:", Cost);
				foreach (Edge edge in Edges) Console.WriteLine(edge);
				return true;
			}
		}
		private (int, List<Edge>) Solve(BinaryMinHeap<Edge> heap, HashSet<int> visited)
		{
			// Find the graph's MST
			int expectedEdgeCount = graph.NodeCount - 1, edgeCount = 0, mstCost = 0;
			List<Edge> mstEdges = new List<Edge>();
			EnqueueAdjEdges(from, visited, heap);

			while (heap.Count > 0 && edgeCount != expectedEdgeCount)
			{
				heapTracer.Mark(0, Colors.Red);
				Edge edge = heap.Dequeue();
				graph.MarkSpring(edge, Colors.Red, Dir.Directed);
				Sleep(Delay.Long);
				heapTracer.Trace();
				// Avoid adding edges to already visited nodes
				bool edgeVisited = visited.Contains(edge.To);
				graph.MarkSpring(edge, edgeVisited ? Colors.Visited : Colors.Green, Dir.Directed);
				if (!edgeVisited)
				{
					mstEdges.Add(edge); edgeCount++;
					mstCost += edge.Cost;
					EnqueueAdjEdges(edge.To, visited, heap);
				}
				Sleep(Delay.Medium);
			}
			return edgeCount == expectedEdgeCount ? (mstCost, mstEdges) : (0, null);
		}
		private void EnqueueAdjEdges(int nodeId, HashSet<int> visited, BinaryMinHeap<Edge> heap)
		{
			visited.Add(nodeId);
			Sleep(Delay.Medium);
			graph.MarkParticle(nodeId, Colors.Orange);
			// Visit outgoing edges and enqueue edges not to visted nodes (also marks them)
			foreach (Edge edge in graph.AdjList[nodeId])
			{
				if (!visited.Contains(edge.To))
				{
					graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
					heap.Enqueue(edge);
					heapTracer.Trace();
					Sleep(Delay.Medium);
				}
			}
			Sleep(Delay.Medium);
			// Mark visted edges & particle
			foreach (Edge edge in graph.AdjList[nodeId])
				if (!visited.Contains(edge.To)) graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
			graph.MarkParticle(nodeId, Colors.Visited, Colors.VisitedBorder);
			Sleep(Delay.Medium);
		}
	}
}
