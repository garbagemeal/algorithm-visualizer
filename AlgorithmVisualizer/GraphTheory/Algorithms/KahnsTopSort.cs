using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.ArrayTracer;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class KahnsTopSort : GraphAlgorithm
	{
		// Flag indicated weather to visualize the algo or not
		private bool vizMode;
		// Tracers used by the algo (visuals)
		private ArrayTracer<int> idxTracer, qTracer, inDegTracer, topOrderTracer;
		// Array to contain the result of the algo - the topological ordering (if exists)
		private int[] topOrder;
		public int[] TopOrder { get { return topOrder; } }

		public KahnsTopSort(Graph graph, bool vizMode = true) : base(graph)
		{
			this.vizMode = vizMode;
			Solve();
		}

		public override void Solve()
		{
			// Returns the graph's topologial ordering using Kahn's algo
			// if the graph is not a DAG null will be returned
			// Array storing the in degree per node
			int[] inDeg = new int[graph.NodeCount];
			// O(E) - For each Edge uv in E the in degree of the dstination vertex(u) will increase by 1
			foreach (List<Edge> edgeList in graph.AdjList.Values)
				foreach (Edge edge in edgeList) inDeg[edge.To]++;
			// O(V) - Create a queue that will initially hold all nodes with in degree of 0 
			Queue<int> q = new Queue<int>();
			for (int i = 0; i < graph.NodeCount; i++) if (inDeg[i] == 0) q.Enqueue(i);
			topOrder = Solve(q, inDeg);
		}
		public int[] Solve(Queue<int> q, int[] inDeg)
		{
			// Main method to run Kahn's top sort
			// Note: vizMode used to disable visuals
			// Create an array to store the topological ordering (of size NodeCount)
			// idx is used to add node id's into the array from 0 to NodeCount
			int idx = 0;
			int[] topOrder = new int[graph.NodeCount];

			SetupTracers(q, inDeg, topOrder);

			// As long as the queue is not enmpty run the algo - O(V)
			while (q.Count > 0)
			{
				if (vizMode) qTracer.HighlightAt(0);
				// Remove curNode from the q and add it to the topOrder (its inDeg is 0)
				int curNode = q.Dequeue();
				topOrder[idx++] = curNode;
				if (vizMode)
				{
					graph.SetParticleColor(curNode, Colors.Orange);
					topOrderTracer.HighlightAt(idx - 1);
					Sleep(2000);
					qTracer.Trace(); topOrderTracer.Trace();
					Sleep(1000);
				}
				VisitOutgoingEdges(curNode, q, inDeg);
				if (vizMode)
				{
					graph.SetParticleColor(curNode, Colors.Visited);
					Sleep(1000);
				}
			}
			// Note that if the number of processed nodes is not NodeCount then
			// there exists a cycle in the graph! (topOrder will become null)
			return idx == graph.NodeCount ? topOrder : null;
		}
		private void VisitOutgoingEdges(int curNode, Queue<int> q, int[] inDeg)
		{
			if (graph.AdjList[curNode] != null)
			{
				// for each outgoint edge from curNode
				foreach (Edge edge in graph.AdjList[curNode])
				{
					int to = edge.To;
					if (vizMode)
					{
						graph.SetSpringState(edge, Colors.Orange);
						inDegTracer.HighlightAt(to);
						Sleep(1000);
					}
					inDeg[to]--;
					if (vizMode)
					{
						inDegTracer.HighlightAt(to);
						Sleep(2000);
					}
					// If after decreasing To's inDeg by 1 it becomes 0, add to q
					//O(Eadj) - Eadj is the out degree of curNode
					if (inDeg[to] == 0)
					{
						q.Enqueue(to);
						if (vizMode)
						{
							qTracer.HighlightAt(-1);
							Sleep(1500);
							qTracer.Trace();
							Sleep(1000);
						}
					}
					if (vizMode)
					{
						inDegTracer.Trace();
						graph.SetSpringState(edge, Colors.Visited);
						Sleep(1000);
					}
				}
			}
		}
		private void SetupTracers(Queue<int> q, int[] inDeg, int[] topOrder)
		{
			int[] idxArr = new int[graph.NodeCount];
			for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			
			// Creating tracers
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", 0, 57, 500, 25);
			qTracer = new ArrayTracer<int>(q, panelLogG, "q: ", 0, 10, 500, 45);
			inDegTracer = new ArrayTracer<int>(inDeg, panelLogG, "inDeg: ", 0, 84, 500, 25);
			topOrderTracer = new ArrayTracer<int>(topOrder, panelLogG, "topOrder: ", 0, 111, 500, 25);
			
			// Setting nameOffset to math the longest
			int topOrderTracerNameOffset = topOrderTracer.NameOffset;
			ArrayTracer<int>[] tracers =
				new ArrayTracer<int>[] { idxTracer, qTracer, inDegTracer, topOrderTracer };
			foreach (var tracer in tracers)
				tracer.NameOffset = topOrderTracerNameOffset;

			if (vizMode) DrawTracers(tracers);
		}
		private void DrawTracers(ArrayTracer<int>[] tracers)
		{
			foreach (ArrayTracer<int> tracer in tracers) tracer.Trace();
		}
	}
}
