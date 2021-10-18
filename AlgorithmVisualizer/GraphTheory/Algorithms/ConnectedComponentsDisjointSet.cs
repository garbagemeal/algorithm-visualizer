using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.DataStructures;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class ConnectedComponentsDisjointSet : GraphAlgorithm
	{
		// vizMode indicates weather to visualize the algo or not
		private bool vizMode;
		private int componentCount;
		public int ComponentCount { get { return componentCount; } }

		public ConnectedComponentsDisjointSet(Graph graph, bool vizMode = true) : base(graph)
		{
			this.vizMode = vizMode;
			Solve();
		}

		public override void Solve()
		{
			// Finds the graph's connected componenets
			// Assumption: the graph is undirected and also all nodes are indexed
			// from 0 to n non inclusive (otherwise will crash!)
			if (!GraphValidator.IsUndirected(graph))
			{
				componentCount = -1;
				return;
			}
			DisjointSet disjointSet = new DisjointSet(graph.NodeCount);

			for (int nodeId = 0; nodeId < graph.NodeCount; nodeId++)
			{
				List<Edge> edgeList = graph.AdjList[nodeId];
				if (edgeList != null)
				{
					foreach (Edge edge in edgeList)
					{
						// If nodes share an edge and both nodes are not part
						// of the same component in the disjoint set
						if (!disjointSet.Connected(edge.From, edge.To))
							disjointSet.Unify(edge.From, edge.To);
					}
				}
			}
			// Color all nodes to show all connected components
			int numComponents = disjointSet.NumComponents;
			// If in visualization mode
			if (vizMode)
			{
				Color[] colors = new Color[graph.NodeCount];
				for (int i = 0; i < graph.NodeCount; i++)
					colors[i] = Colors.GetRandom();
				for (int i = 0; i < graph.NodeCount; i++)
					graph.SetParticleColor(i, colors[disjointSet.Find(i)]);
			}
			// Update component count class attribute (can be accessed by user)
			componentCount = numComponents;
		}
	}
}
