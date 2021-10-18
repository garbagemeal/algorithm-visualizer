using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class ConnectedComponentsDFS : GraphAlgorithm
	{
		
		private int[] components;
		private int componentCount = 0;
		public ConnectedComponentsDFS(Graph graph) : base(graph) => Solve();

		public override void Solve()
		{
			// List graph's components
			// components[i] = j implies node with id i is in component j
			components = new int[graph.NodeCount];
			// If the graph is not undirected "does nothing"
			Color randomColor = Colors.GetRandom();
			if (GraphValidator.IsUndirected(graph))
			{
				HashSet<int> visited = new HashSet<int>();
				for (int nodeId = 0; nodeId < graph.NodeCount; nodeId++)
				{
					if (!visited.Contains(nodeId))
					{
						// count & get new rnd color only when creating a new comp.
						Solve(nodeId, visited, randomColor);
						Sleep(1500);
						componentCount++;
						randomColor = Colors.GetRandom();
					}
				}
				Color[] colors = new Color[componentCount];
				for (int i = 0; i < 3; i++) colors[i] = Colors.GetRandom();
				for (int i = 0; i < graph.NodeCount; i++)
				{
					graph.SetParticleColor(i, colors[components[i]]);
					Console.WriteLine($"Node id: {i}, comp id: {components[i]}");
				}
			}
		}
		private void Solve(int at, HashSet<int> visited, Color color)
		{
			// DFS to find the connected componenets
			visited.Add(at);
			components[at] = componentCount;
			graph.SetParticleColor(at, color);
			Sleep(1500);
			if (graph.AdjList[at] != null)
			{
				foreach (Edge edge in graph.AdjList[at])
				{
					if (!visited.Contains(edge.To))
					{
						// draw edge in orange to mark as visiting
						graph.SetSpringState(edge, color, 0);
						Sleep(1000);

						Solve(edge.To, visited, color);

						// draw edge in dark gret to to mark as visited
						graph.SetSpringState(edge, Colors.Visited);
						Sleep(1000);
					}
				}
			}
			Sleep(1000);
		}
	}
}
