using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class KahnsTopSort : GraphAlgorithm
	{
		// Flag indicating whether to visualize the algo or not
		private bool vizMode;
		// Tracers used by the algo (visuals)
		private QueueTracer<int> qTracer;
		private ArrayTracer<int> idxTracer, inDegTracer, topOrderTracer;
		private AbstractArrayTracer<int>[] tracers;

		private Queue<int> q;
		// Array to contain the result of the algo - the topological ordering (if exists)
		private int[] inDeg;
		public int[] TopOrder { get; private set; }

		public KahnsTopSort(Graph graph, bool vizMode = true) : base(graph)
		{
			// vizMode used to disable visuals
			this.vizMode = vizMode;

			q = new Queue<int>();
			inDeg = new int[graph.NodeCount];
			TopOrder = new int[graph.NodeCount];
			
			SetupTracers();
		}

		public override bool Solve()
		{
			// Returns the graph's top order if is a DAG otherwise null
			// O(E) - count in degree per vertex (numbre of incoming edges)
			foreach (List<Edge> edgeList in graph.AdjList.Values)
				foreach (Edge edge in edgeList) inDeg[edge.To]++;
			// O(V) - Create a queue to hold nodes of in degree 0
			for (int i = 0; i < graph.NodeCount; i++) if (inDeg[i] == 0) q.Enqueue(i);

			if (vizMode) ShowTracers();

			int idx = 0;
			// As long as the queue is not enmpty run the algo - O(V)
			while (q.Count > 0)
			{
				if (vizMode) qTracer.Mark(0);
				// Remove curNode from the q and add it to the topOrder (its inDeg is 0)
				int curNode = q.Dequeue();
				TopOrder[idx++] = curNode;
				if (vizMode)
				{
					graph.MarkParticle(curNode, Colors.Orange);
					topOrderTracer.Mark(idx - 1);
					Sleep(2000);
					qTracer.Trace(); topOrderTracer.Trace();
					Sleep(1000);
				}
				VisitNeighbors(curNode, q, inDeg);
				if (vizMode)
				{
					graph.MarkParticle(curNode, Colors.Visited);
					Sleep(1000);
				}
			}
			// If TopOrder contains all nodes then the graph is a DAG,
			// otherwise contains a directed cycle.
			return idx == graph.NodeCount;
		}
		private void VisitNeighbors(int curNode, Queue<int> q, int[] inDeg)
		{
			if (graph.AdjList[curNode] != null)
			{
				// for each outgoint edge from curNode
				foreach (Edge edge in graph.AdjList[curNode])
				{
					int to = edge.To;
					if (vizMode)
					{
						graph.MarkSpring(edge, Colors.Orange);
						inDegTracer.Mark(to);
						Sleep(1000);
					}
					inDeg[to]--;
					if (vizMode)
					{
						inDegTracer.Mark(to);
						Sleep(2000);
					}
					// If after decreasing To's inDeg by 1 it becomes 0, add to q
					//O(Eadj) - Eadj is the out degree of curNode
					if (inDeg[to] == 0)
					{
						q.Enqueue(to);
						if (vizMode)
						{
							qTracer.Mark(-1);
							Sleep(1500);
							qTracer.Trace();
							Sleep(1000);
						}
					}
					if (vizMode)
					{
						inDegTracer.Trace();
						graph.MarkSpring(edge, Colors.Visited);
						Sleep(1000);
					}
				}
			}
		}
		private void SetupTracers()
		{
			int[] idxArr = new int[graph.NodeCount];
			for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			
			// Creating tracers
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", new PointF(0, 57), new SizeF(500, 25));
			qTracer = new QueueTracer<int>(q, panelLogG, "q: ", new PointF(0, 10), new SizeF(500, 45));
			inDegTracer = new ArrayTracer<int>(inDeg, panelLogG, "inDeg: ", new PointF(0, 84), new SizeF(500, 25));
			topOrderTracer = new ArrayTracer<int>(TopOrder, panelLogG, "topOrder: ", new PointF(0, 111), new SizeF(500, 25));
			
			// Setting nameOffset to math the longest
			SizeF topOrderTracerNameOffset = topOrderTracer.TitleSize;
			tracers = new AbstractArrayTracer<int>[] { idxTracer, qTracer, inDegTracer, topOrderTracer };
			foreach (var tracer in tracers) tracer.TitleSize = topOrderTracerNameOffset;
		}
		private void ShowTracers()
		{
			foreach (var tracer in tracers) tracer.Trace();
		}
	}
}
