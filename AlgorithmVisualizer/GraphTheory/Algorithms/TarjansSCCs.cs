using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class TarjansSCCs : GraphAlgorithm
	{
		// Tarjan's Strongly Connected Components(Tarjan's SCC) - O(V + E)

		private const int UNVISITED = -1;
		private int id = 0, SCCCount = 0;
		private int[] ids, low;
		private Stack<int> stk;
		private bool[] onStk;

		public TarjansSCCs(Graph graph) : base(graph)
		{
			ids = new int[graph.NodeCount];
			low = new int[graph.NodeCount];
			stk = new Stack<int>();
			onStk = new bool[graph.NodeCount];
			ids.Fill(UNVISITED);
		}


		public override bool Solve()
		{
			// DFS foreach unvisted node to find the graph's SCCs
			for (int i = 0; i < graph.NodeCount; i++) if (ids[i] == UNVISITED) DFS(i);
			
			// Color the SCCs. Note that the SCC ids may not be sequntial
			Dictionary<int, Color> colors = new Dictionary<int, Color>(SCCCount);
			for (int i = 0; i < graph.NodeCount; i++)
			{
				int k = low[i];
				if (!colors.ContainsKey(k)) colors[k] = Colors.GetRandom();
				graph.MarkParticle(i, colors[k]);
			}
			
			return true;
		}
		private void DFS(int at)
		{
			stk.Push(at);
			onStk[at] = true;
			ids[at] = low[at] = id++;
			// Visit neighbors of 'at'
			foreach (Edge edge in graph.AdjList[at])
			{
				int to = edge.To;
				if (ids[to] == UNVISITED) DFS(to);
				// min the low link for 'at', 'to' on the recursive callback
				if (onStk[to]) low[at] = Math.Min(low[at], low[to]);
			}
			// If 'at' started the SCC
			if (ids[at] == low[at])
			{
				// Remove nodes in the SCC from stk, removing 'at' is the stop condition
				while (true)
				{
					int node = stk.Pop();
					onStk[node] = false;
					low[node] = ids[at];
					if (node == at) break;
				}
				SCCCount++;
			}
		}
	}
}
