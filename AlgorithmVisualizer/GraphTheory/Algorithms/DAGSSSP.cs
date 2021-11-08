using System;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class DAGSSSP : GraphAlgorithm
	{
		/* This algo can be adjusted to compute longest paths by applying the following steps:
		 * 1. Before running this algo multiply all edge costs by -1
		 * 2. After running this algo the results in distMap by -1
		 * *. At this point distMap[i] is the longest path distance to node i.
		 * 3. Optional: multiply edge costs by -1 to restore graph to initial state.
		 * For generic graphs the above problem is considered NP hard.
		 * 
		 * Remarks: 
		 * Works only for directed acyclic graphs
		 * Works with negative edge weights because the graph is acyclic */

		private readonly int from;
		private readonly int[] topSort, distMap;

		private ArrayTracer<int> idxTracer;
		private ArrayTracer<int> topSortTracer;
		private ArrayTracer<int> distMapTracer;
		private AbstractArrayTracer<int>[] tracers;

		public DAGSSSP(Graph graph, int _from = 0) : base(graph)
		{
			from = _from;
			topSort = GetTopologicalOrdering();
			distMap = new int[graph.NodeCount];
			// Fill distMap array with "inifinities" and set distance to starting node as 0
			distMap.Fill(int.MaxValue);
			distMap[from] = 0;
			SetupTracers();
		}

		private int[] GetTopologicalOrdering()
		{
			var kahnsTopSortSolver = new KahnsTopSort(graph, vizMode: false);
			kahnsTopSortSolver.Solve();
			return kahnsTopSortSolver.TopOrder;
		}

		public override bool Solve()
		{
			// topSort == null --> graph is not a DAG --> this algo not applicable
			if (topSort == null) return false;

			ShowTracers();
			Sleep(Delay.Medium);
			for (int i = 0; i < graph.NodeCount; i++)
			{
				// Note: i is the index of curNodeId in topSort
				int curNodeId = topSort[i];
				graph.MarkParticle(curNodeId, Colors.Orange);
				topSortTracer.Mark(i, Colors.Orange);
				Sleep(Delay.Long);
				topSortTracer.Trace();
				Sleep(Delay.Medium);
				VisitNeighbors(curNodeId);
				graph.MarkParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
				Sleep(Delay.Medium);
			}

			for (int i = 0; i < distMap.Length; i++)
				Console.WriteLine("Distance to {0}: {1}", i, distMap[i] != int.MaxValue ? distMap[i] + "" : "INF");

			return true;
		}
		private void VisitNeighbors(int at)
		{
			// Relax each outgoing edge of 'at if 'at' is reachable
			if (distMap[at] != int.MaxValue)
				foreach (Edge edge in graph.AdjList[at]) RelaxEdge(edge, at);
		}
		private void RelaxEdge(Edge edge, int at)
		{
			graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
			Sleep(Delay.Medium);
			// Compute new distance for edge.To
			int newDist = distMap[at] + edge.Cost;
			bool distanceImproved = newDist < distMap[edge.To];
			graph.MarkSpring(edge, distanceImproved ? Colors.Red : Colors.Blue, Dir.Directed);
			Sleep(Delay.Medium);
			if (distanceImproved)
			{
				distMap[edge.To] = newDist;
				distMapTracer.Mark(edge.To, Colors.Red);
				Sleep(Delay.Long);
				distMapTracer.Trace();
				Sleep(Delay.Medium);
			}
			graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
			Sleep(Delay.Medium);
		}


		private void SetupTracers()
		{
			// Setup tracers
			int[] idxArr = new int[graph.NodeCount]; for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", new PointF(0, 5), new SizeF(500, 30));
			topSortTracer = new ArrayTracer<int>(topSort, panelLogG, "Top sort: ", new PointF(0, 37), new SizeF(500, 30));
			distMapTracer = new ArrayTracer<int>(distMap, panelLogG, "DistMap: ", new PointF(0, 69), new SizeF(500, 30));
			tracers = new AbstractArrayTracer<int>[] { idxTracer, distMapTracer, topSortTracer };

			idxTracer.TitleSize = distMapTracer.TitleSize = topSortTracer.TitleSize;
		}
		private void ShowTracers()
		{
			foreach (var tracer in tracers) tracer.Trace();
		}
	}
}