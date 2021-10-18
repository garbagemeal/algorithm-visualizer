using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.ArrayTracer;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class TarjansSCCs : GraphAlgorithm
	{
		public TarjansSCCs(Graph graph) : base(graph) => Solve();

		public override void Solve()
		{
			/*
			 * O(V + E)
			 * Tarjan's Strongly Connected Components(Tarjan's SCC) algorithm
			 * Expected input is a directed graph
			 */
			const int UNVISITED = -1;
			int id = 0, SCCCount = 0;
			int[] ids = new int[graph.NodeCount], low = new int[graph.NodeCount];
			Stack<int> stk = new Stack<int>();
			bool[] onStk = new bool[graph.NodeCount];
			for (int i = 0; i < graph.NodeCount; i++) ids[i] = UNVISITED;
			for (int i = 0; i < graph.NodeCount; i++) if (ids[i] == UNVISITED) DFS(i);
			void DFS(int at)
			{
				stk.Push(at);
				onStk[at] = true;
				ids[at] = low[at] = id++;
				// Visit at's neighbours and min low-link on callback
				foreach (Edge edge in graph.AdjList[at])
				{
					int to = edge.To;
					if (ids[to] == UNVISITED) DFS(to);
					if (onStk[to]) low[at] = Math.Min(low[at], low[to]);
				}
				// If we're at the start of a SCC empty the seen stack
				// until we're back to the start of the SCC.
				if (ids[at] == low[at])
				{
					for (int nodeId = stk.Pop(); ; nodeId = stk.Pop())
					{
						onStk[nodeId] = false;
						low[nodeId] = ids[at];
						if (nodeId == at) break;
					}
					SCCCount++;
				}
			}
			// May have colors brushes, can be bounded via SCCCount with adjustment
			Color[] colors = new Color[graph.NodeCount];
			for (int i = 0; i < graph.NodeCount; i++) colors[i] = Colors.GetRandom();
			for (int i = 0; i < graph.NodeCount; i++) graph.SetParticleColor(i, colors[low[i]]);
		}
	}
}
