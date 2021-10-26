using System;
using System.Collections.Generic;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class KosarajusSCCs : GraphAlgorithm
	{
		public KosarajusSCCs(Graph graph) : base(graph) { }

		public override bool Solve()
		{
			// Kosaraju's Strongly connected componenets - O(V + E)
			HashSet<int> visited = new HashSet<int>();
			Stack<int> stk = new Stack<int>();
			// sccCount tracks number of SCCs, ids denotes the SCC id for each node.
			// i.e, ids[i] = j implies node with id 'i' belongs to SCC with id 'j';
			// ids[i] = ids[j] implies nodes i and j are part of the same SCC.
			int sccCount = 0;
			int[] ids = new int[graph.NodeCount];

			// Perform DFS on original graph, when backtracking push node id into stk.
			for (int i = 0; i < graph.NodeCount; i++)
				if (!visited.Contains(i)) DFS(graph.AdjList, i);

			// Compute Gt - Gt is identical to G where each edge is reversed.
			Dictionary<int, List<Edge>> Gt = graph.GetGTranspose();
			visited.Clear();
			// Reverse every directed edge in adjList to visualize Gt
			graph.ReverseSprings();
			Sleep(5000);
			// Perfrom DFS on Gt, when backtracking assign SCC id for node.
			while (stk.Count > 0)
			{
				int i = stk.Pop();
				if (!visited.Contains(i))
				{
					// Each non recursive call to DFS(G, at) results in a new SCC
					DFS(Gt, i);
					sccCount++;
				}
			}
			for (int i = 0; i < graph.NodeCount; i++)
				Console.WriteLine($"Node id: {i}, SCC: {ids[i]}");
			// Reverse every directed edge in adjList to unvisualize Gt
			graph.ReverseSprings();
			return true;


			void DFS(Dictionary<int, List<Edge>> G, int at)
			{
				visited.Add(at);
				// Visit neighbors of 'at'
				foreach (Edge edge in G[at])
					if (!visited.Contains(edge.To)) DFS(G, edge.To);
				// After visiting all neighbors of 'at', depending on mode:
				// Either push 'at' into stk or assign SCC id per node id.
				if (G == graph.AdjList) stk.Push(at);
				else ids[at] = sccCount;
			}
		}
	}
}
