using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.DataStructures;
using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class KruskalsMST : GraphAlgorithm
	{
		// O(ElogE)
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
			List<Edge> edgeList = graph.GetUndirectedEdgeList(); // O(E)
			if (edgeList.Count < 1)
			{
				Console.WriteLine("Graph has no edges (1 edge needed at least for heap creation)");
				return false;
			}
			// Avoid sorting the edges by creating a heap from the edge list in O(E)
			BinaryMinHeap<Edge> heap = new BinaryMinHeap<Edge>(edgeList);
			DisjointSet disjointSet = new DisjointSet(graph.NodeCount); // O(V)
			heapTracer = new HeapTracer<Edge>(heap, panelLogG, "Heap: ", new PointF(0, 10), new SizeF(500, 50));
			(int Cost, List<Edge> Edges) = Solve(heap, disjointSet, heapTracer);

			// Note that it may be a MSF and not a MST
			Console.WriteLine("MST Cost: " + Cost);
			Console.WriteLine("MST Edges:");
			foreach (Edge edge in Edges) Console.WriteLine(edge);
			return true;
		}
		private (int, List<Edge>) Solve(BinaryMinHeap<Edge> heap,
			DisjointSet disjointSet, HeapTracer<Edge> heapTracer)
		{
			// Finds and returns the graph's MST/MSF edge list and the total edge cost
			List<Edge> mstEdges = new List<Edge>();
			int mstCost = 0;
			heapTracer.Trace();
			Sleep(Delay.Long);
			// While heap not empty and the disjoint set has more than 1 component
			while (heap.Count > 0 && disjointSet.NumComponents > 1) // O(E)
			{
				heapTracer.Mark(0, Colors.Red);
				Edge edge = heap.Dequeue(); // O(logE)
				Sleep(Delay.Medium);
				heapTracer.Trace();
				Sleep(Delay.Medium);
				// Avoid adding edges where both composing nodes already belong
				// to the same group (would introduce a cycle to the MST!)
				if (!disjointSet.Connected(edge.From, edge.To)) // α(n) - amortized constant time
				{
					// Add edge into the MST's edge list and sum its cost
					mstEdges.Add(edge);
					mstCost += edge.Cost;
					// Unify both composing nodes via id in the disjoint set
					disjointSet.Unify(edge.From, edge.To); // α(n) - amortized constant time
					// Draw the edge
					graph.MarkSpring(edge, Colors.Green);
					Sleep(Delay.Medium);
				}
			}
			return (mstCost, mstEdges);
		}
	}
}
