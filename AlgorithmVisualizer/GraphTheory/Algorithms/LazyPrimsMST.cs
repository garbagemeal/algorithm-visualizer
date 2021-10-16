using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.ArrayTracer;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class LazyPrimsMST : GraphAlgorithm
	{
		private readonly int from;
		// Used to trace the heap
		private ArrayTracer<Edge> heapTracer;

		public LazyPrimsMST(Graph graph, int from = 0) : base(graph)
		{
			this.from = from;
			Solve();
		}

		public override void Solve()
		{
			// Find the graph's MST - setup
			// If the graph is not connected does nothing
			if (!GraphValidator.IsConnectedUndirected(graph)) return;
			BinaryMinHeap<Edge> heap = new BinaryMinHeap<Edge>();
			HashSet<int> visited = new HashSet<int>();
			heapTracer = new ArrayTracer<Edge>(heap, panelLogG, "Heap: ", 0, 10, 500, 45);
			(int Cost, List<Edge> Edges) MSTDetails = Solve(heap, visited);
			// If the edge list is null then there is no min spanning tree
			// This case is not to be confused with an empty (non-null) list
			// represents an MST of a single node with no edes.
			if (MSTDetails.Edges == null)
				Console.WriteLine("There is no MST for the given graph");
			else
			{
				Console.WriteLine("MST Details: \nCost: {0}, Edge list:", MSTDetails.Cost);
				foreach (Edge edge in MSTDetails.Edges) Console.WriteLine(edge);
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
				heapTracer.HighlightAt(0);
				Edge edge = heap.Dequeue();
				graph.RedrawSpring(edge, Colors.Red, 0);
				Sleep(1500);
				heapTracer.Trace();
				// Avoid adding edges to already visited nodes
				bool edgeVisited = visited.Contains(edge.To);
				graph.RedrawSpring(edge, edgeVisited ? Colors.Visited : Colors.Green, edgeVisited ? -1 : 0);
				if (!edgeVisited)
				{
					//mstEdges[edgeCount++] = edge;
					mstEdges.Add(edge); edgeCount++;
					mstCost += edge.Cost;
					EnqueueAdjEdges(edge.To, visited, heap);
				}
				Sleep(1000);
			}
			return edgeCount == expectedEdgeCount ? (mstCost, mstEdges) : (0, null);
		}
		private void EnqueueAdjEdges(int nodeId, HashSet<int> visited, BinaryMinHeap<Edge> heap)
		{
			// Function to visit a node and enqueue all adj edges
			// mark node as visited
			visited.Add(nodeId);
			Sleep(1000);
			graph.DrawParticle(nodeId, Colors.Orange);
			// add all outgoing edges from nodeId while avoiding
			// adding edges to already visited nodes
			// also visualizes all edges to non visted nodes and the node itself
			if (graph.AdjList[nodeId] != null)
			{
				foreach (Edge edge in graph.AdjList[nodeId])
				{
					if (!visited.Contains(edge.To))
					{
						graph.RedrawSpring(edge, Colors.Orange, 0);
						heap.Enqueue(edge);
						heapTracer.Trace();
						Sleep(1000);
					}
				}
				Sleep(1000);
				// draw edges in dark grey (visited)
				foreach (Edge edge in graph.AdjList[nodeId])
					if (!visited.Contains(edge.To)) graph.RedrawSpring(edge, Colors.Visited);
			}
			// draw particle in darkGreyBrush (visited)
			graph.DrawParticle(nodeId, Colors.Visited, Colors.VisitedBorder);
			Sleep(1000);
		}
	}
}
