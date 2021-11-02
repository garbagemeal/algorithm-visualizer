using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Tracers;
using AlgorithmVisualizer.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class KosarajusSCCs : GraphAlgorithm
	{
		// Kosaraju's Strongly connected componenets - O(V + E)

		private readonly HashSet<int> visited;
		private readonly Stack<int> stk;
		// SccCount - the number of SCCs in the given graph
		// SccIds - maps SCC ids foreach node id, i.e:
		// SccIds[i] = j implies node 'i' is in the SCC 'j'
		public int SccCount { get; private set; } = 0;
		public int[] SccIds { get; private set; }


		private Color nodeMarkColor = Colors.Orange;
		private StackTracer<int> stkTracer;
		private ArrayTracer<int> sccIdsTracer, idxTracer;
		private AbstractArrayTracer<int>[] tracers;

		public KosarajusSCCs(Graph graph) : base(graph)
		{
			visited = new HashSet<int>();
			stk = new Stack<int>();
			SccCount = 0;
			SccIds = new int[graph.NodeCount];
			SccIds.Fill(-1);
			SetupTracers();
		}

		public override bool Solve()
		{
			ShowTracers();
			Sleep(Delay.Medium);
			// Perform DFS on given graph, after visiting all neighbors of a node push it into into stk
			for (int i = 0; i < graph.NodeCount; i++)
				if (!visited.Contains(i)) DFS(graph.AdjList, i);
			// Compute Gt - Gt is identical to G where every edge is reversed, also clear visited.
			Dictionary<int, List<Edge>> Gt = graph.GetGTranspose();
			visited.Clear();
			// Clear visuals by the former DFS (finish times)
			Sleep(Delay.VeryLong);
			graph.ClearVizState();
			// For the visuals to match Gt need to reverse the springs
			graph.ReverseSprings();
			Sleep(Delay.VeryLong);
			// Perfrom DFS on Gt, after visiting all neighbors of a node assign it a SCC id
			while (stk.Count > 0)
			{
				stkTracer.Mark(0, Colors.Red);
				Sleep(Delay.Medium);
				int i = stk.Pop();
				stkTracer.Trace();
				if (!visited.Contains(i))
				{
					nodeMarkColor = Colors.GetRandom();
					// Each non recursive call to DFS(G, at) results in a new SCC
					DFS(Gt, i);
					SccCount++;
				}
				Sleep(Delay.Medium);
			}
			//HideTracers();
			return true;
		}
		private void DFS(Dictionary<int, List<Edge>> G, int at)
		{
			visited.Add(at);
			graph.MarkParticle(at, Colors.Orange);
			Sleep(Delay.Long);
			// Visit neighbors of 'at'
			foreach (Edge edge in G[at])
			{
				if (!visited.Contains(edge.To))
				{
					// If G == graph.AdjList then working with original graph, otherwise
					// working with a clone graph and edges are revered. the visualizer is
					// still using the original graph and sets spring.Reversed = true without
					// actually changing the edge.
					var drawingSpring = G == graph.AdjList ? edge : Edge.ReversedCopy(edge);
					graph.MarkSpring(drawingSpring, Colors.Orange, Dir.Directed);
					Sleep(Delay.Medium);
					DFS(G, edge.To);
					graph.MarkSpring(drawingSpring, Colors.Visited, Dir.Directed);
					Sleep(Delay.Medium);
				}
			}
			// After visiting all neighbors of 'at', depending on G:
			// Either push 'at' into stk or assign SCC id per node id.
			if (G == graph.AdjList)
			{
				stk.Push(at);
				TraceNode(0, stkTracer);
				graph.MarkParticle(at, Colors.Visited, Colors.VisitedBorder);
			}
			else
			{
				SccIds[at] = SccCount;
				TraceNode(at, sccIdsTracer);
				graph.MarkParticle(at, nodeMarkColor);
			}
			Sleep(Delay.Medium);
		}
		private void TraceNode(int id, AbstractArrayTracer<int> tracer)
		{
			tracer.Mark(id, nodeMarkColor);
			Sleep(Delay.Medium);
			tracer.Trace();
			Sleep(Delay.Medium);
		}

		private void SetupTracers()
		{
			// Setup tracers
			int[] idxArr = new int[graph.NodeCount]; for (int i = 0; i < graph.NodeCount; i++) idxArr[i] = i;
			idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", new PointF(0, 5), new SizeF(500, 30));
			stkTracer = new StackTracer<int>(stk, panelLogG, "stk: ", new PointF(0, 37), new SizeF(500, 30));
			sccIdsTracer = new ArrayTracer<int>(SccIds, panelLogG, "SCC IDs: ", new PointF(0, 69), new SizeF(500, 30));
			tracers = new AbstractArrayTracer<int>[] { idxTracer, stkTracer, sccIdsTracer };
			
			idxTracer.TitleSize = stkTracer.TitleSize = sccIdsTracer.TitleSize;
		}
		private void ShowTracers()
		{
			foreach (var tracer in tracers) tracer.Trace();
		}
	}
}
