using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;
using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class ConnectedComponentsDFS : GraphAlgorithm
	{
		
		private readonly int[] components;
		private int componentCount = 0;
		private readonly HashSet<int> visited;
		public ConnectedComponentsDFS(Graph graph) : base(graph)
		{
			// components[i] = j implies node with id i is in component j
			components = new int[graph.NodeCount];
			visited = new HashSet<int>();
		}

		public override bool Solve()
		{
			if (!GraphValidator.IsUndirected(graph)) return false;
			Color randomColor = Colors.GetRandom();
			// for each node in the graph
			for (int nodeId = 0; nodeId < graph.NodeCount; nodeId++)
			{
				// if not visited
				if (!visited.Contains(nodeId))
				{
					// Create new component using DFS
					DFS(nodeId, randomColor);
					Sleep(Delay.Long);
					componentCount++;
					randomColor = Colors.GetRandom();
				}
			}
			return true;
		}
		private void DFS(int at, Color color)
		{
			visited.Add(at);
			components[at] = componentCount;
			graph.MarkParticle(at, color);
			Sleep(Delay.Long);
			foreach (Edge edge in graph.AdjList[at])
			{
				if (!visited.Contains(edge.To))
				{
					graph.MarkSpring(edge, color, Dir.Directed);
					Sleep(Delay.Medium);
					DFS(edge.To, color);
					graph.MarkSpring(edge, Colors.Visited, Dir.Directed);
					Sleep(Delay.Medium);
				}
			}
			Sleep(Delay.Medium);
		}
	}
}
