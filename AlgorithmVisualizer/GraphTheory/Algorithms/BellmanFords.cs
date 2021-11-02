using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class BellmanFords : GraphAlgorithm
	{
		// SSSP algo, detects negative cycles. O(VE)
		// For dense graphs will be O(V^3) in which case an adjacency matrix is better
		// Proof of correctness(starts at 18:00):
		// https://www.youtube.com/watch?v=ozsuci5pIso&list=PLUl4u3cNGP61Oq3tWYp6V_F-5jb5L2iHb&index=17&ab_channel=MITOpenCourseWare

		private readonly int from;
		private readonly double[] distMap;

		private ArrayTracer<int> idxTracer;
		private ArrayTracer<double> distMapTracer;

		public BellmanFords(Graph graph, int _from) : base(graph)
		{
			from = _from;
			distMap = new double[graph.NodeCount];
			distMap.Fill(double.PositiveInfinity);
			distMap[from] = 0;

			SetupTracers();
		}

		public override bool Solve()
		{
			ShowTracers();
			Sleep(Delay.Medium);
			// Find the SSSP for each vertex by relaxing each edge V-1 times.
			for (int i = 1; i < graph.NodeCount; i++)
			{
				ShowMessageInLogPanel("Iteration " + i, new PointF(10, 80));
				Sleep(Delay.VeryLong);
				RelaxAllEdges(RelaxMode.Default);
				// Clear visuals for graph after each iteration
				Sleep(Delay.Long);
				graph.ClearVizState();
				Sleep(Delay.Long);
			}

			ShowMessageInLogPanel("Detecting negative cycles", new PointF(10, 80));
			RelaxAllEdges(RelaxMode.CycleDetection);

			ShowMessageInLogPanel($"Done. \nNodes in a negative cycle and nodes reachable " +
				$"by a negative cycle have a dist of {float.NegativeInfinity}", new PointF(10, 80));

			/*
			 * +INF(8)  : Unreachable node
			 * -INF(-8) : Node in a negative a cycle
			 * Other    : Reachable node, value expected to match the SP distance from 'from'
			 */
			for (int i = 0; i < graph.NodeCount; i++)
				Console.WriteLine($"DistMap[{i}] = {distMap[i]}");

			return true;
		}
		private enum RelaxMode { Default = 0, CycleDetection = 1 };

		private void RelaxAllEdges(RelaxMode mode)
		{
			// relax every edge coming from a reachable node
			for (int at = 0; at < graph.NodeCount; at++)
				RelaxEdgeList(graph.AdjList[at], mode);
		}
		private void RelaxEdgeList(List<Edge> edgeList, RelaxMode mode)
		{
			foreach (Edge edge in edgeList)
			{
				graph.MarkSpring(edge, Colors.Orange, Dir.Directed);
				// for mode = RelaxMode.Default functions normally, however if
				// mode = RelaxMode.CycleDetection then a relaxation of an edge implies
				// that the source node (edge.To) is part of a negative cycle, and so
				// distMap[edge.To] is set to -INF.
				double newCost = distMap[edge.From] + edge.Cost;
				if (newCost < distMap[edge.To])
				{
					distMap[edge.To] = mode == RelaxMode.Default ? newCost : double.NegativeInfinity;
					graph.MarkSpring(edge, Colors.Red, Dir.Directed);
					distMapTracer.Mark(edge.To, Colors.Red);
					Sleep(Delay.Long);
					distMapTracer.Trace();
				}
				else // Relaxation failed
				{
					graph.MarkSpring(edge, Colors.Blue, Dir.Directed);
					Sleep(Delay.Medium);
				}
				graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
				Sleep(Delay.Medium);
			}
		}
		private void SetupTracers()
		{
			int[] idxArr = new int[graph.NodeCount]; for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", new PointF(0, 5), new SizeF(500, 30));
			distMapTracer = new ArrayTracer<double>(distMap, panelLogG, "distMap: ", new PointF(0, 37), new SizeF(500, 30));

			idxTracer.TitleSize = distMapTracer.TitleSize;
		}
		private void ShowTracers()
		{
			idxTracer.Trace();
			distMapTracer.Trace();
		}

		private SizeF? prevMsgSize = null;
		private void ShowMessageInLogPanel(string msg, PointF pt)
		{
			// Undraw previous message if exists
			if (prevMsgSize != null)
			{
				var rect = new RectangleF(pt, (SizeF)prevMsgSize);
				using (var brush = new SolidBrush(Colors.UndrawLog))
					panelLogG.FillRectangle(brush, rect);
			}
			Sleep(Delay.VeryShort);
			using (var font = new Font("Arial", 12, FontStyle.Bold))
			using (var brush = new SolidBrush(Colors.Red))
			{
				panelLogG.DrawString(msg, font, brush, pt);
				// Store size of this message for undraw on next call to "ShowMessageInLogPanel"
				prevMsgSize = panelLogG.MeasureString(msg, font);
			}
		}
	}

}
