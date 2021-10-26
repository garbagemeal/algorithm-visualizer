using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.DataStructures;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class KruskalsMST : GraphAlgorithm
	{
		private HeapTracer<Edge> heapTracer;

		public KruskalsMST(Graph graph) : base(graph) { }

		public override bool Solve()
		{
			/*
			 * Finds the graph's MST(min spanning tree) or MSF (min spanning forest)
			 * Note: The algorithm will result in the MST if the graph is connected
			 * otherwise if the graph is disconnected then the result will be a MSF.
			 * Also all nodes must be indexed from 0 to V non inclusive
			 */
			// If the graph is not undirected or has 0 edges do nothing
			if (!GraphValidator.IsUndirected(graph)) return false;
			// Getting list of all undirected edges from adjList
			List<Edge> edgeList = graph.GetUndirectedEdgeList();
			if (edgeList.Count < 1)
			{
				Console.WriteLine("Graph has no edges (1 edge needed at least for heap creation)");
				return false;
			}
			// O(E)
			// Avoid sorting the edges by creating a heap from the list O(n) where n = E
			BinaryMinHeap<Edge> heap = new BinaryMinHeap<Edge>(edgeList);
			// O(V) = O(sqrt(E)) assuming graph is simple, i.e, not a multigraph,
			// because E = V(V-1)/2 = (V^2 - V) / 2 and can be futher simplified to O(V^2)
			// and thus E = roughly V^2, so O(V) is asymptotically the same as, O(sqrt(E)).
			// Creating a disjoint set (union find) of size V
			DisjointSet disjointSet = new DisjointSet(graph.NodeCount);
			heapTracer = new HeapTracer<Edge>(heap, panelLogG, "Heap: ", new PointF(0, 10), new SizeF(500, 45));
			(int Cost, List<Edge> Edges) MSTDetails = Solve(heap, disjointSet, heapTracer);

			// Note that it may be a MSF and not a MST
			Console.WriteLine("MST Cost: " + MSTDetails.Cost);
			Console.WriteLine("MST Edges:");
			foreach (Edge edge in MSTDetails.Edges) Console.WriteLine(edge);
			return true;
		}
		private (int, List<Edge>) Solve(BinaryMinHeap<Edge> heap,
			DisjointSet disjointSet, HeapTracer<Edge> heapTracer)
		{
			// Finds and returns the graph's MST/MSF edge list and the total edge cost
			List<Edge> mstEdges = new List<Edge>();
			int mstCost = 0;
			heapTracer.Trace();
			Sleep(1500);
			// O(E*log(E))
			// As long as the heap is not empty and the disjoint set has more than 1 component
			while (heap.Count > 0 && disjointSet.NumComponents > 1)
			{
				heapTracer.Mark(0);
				Edge edge = heap.Dequeue();
				Sleep(1000);
				heapTracer.Trace();
				Sleep(1000);
				// Avoid adding edges where both composing nodes already belong
				// to the same group (would introduce a cycle to the MST!)
				if (!disjointSet.Connected(edge.From, edge.To))
				{
					// Add edge into the MST's edge list and sum its cost
					mstEdges.Add(edge);
					mstCost += edge.Cost;
					// Unify both composing nodes via id in the disjoint set
					// This operation takes amortized time complexity(near constant)
					disjointSet.Unify(edge.From, edge.To);
					// Draw the edge
					graph.MarkSpring(edge, Colors.Green);
					Sleep(1000);
				}
			}
			return (mstCost, mstEdges);
		}
	}
}
