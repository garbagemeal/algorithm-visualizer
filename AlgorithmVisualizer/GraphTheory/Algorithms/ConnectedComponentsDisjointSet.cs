using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.DataStructures;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	class ConnectedComponentsDisjointSet : GraphAlgorithm
	{
		// vizMode indicates whether to visualize the algo or not
		private readonly bool vizMode;

		private readonly DisjointSet disjointSet;
		public int ComponentCount { get; private set; } = -1;

		public ConnectedComponentsDisjointSet(Graph graph, bool vizMode = true) : base(graph)
		{
			this.vizMode = vizMode;

			disjointSet = new DisjointSet(graph.NodeCount);
		}

		public override bool Solve()
		{
			if (!GraphValidator.IsUndirected(graph)) return false;

			for (int nodeId = 0; nodeId < graph.NodeCount; nodeId++)
			{
				List<Edge> edgeList = graph.AdjList[nodeId];
				foreach (Edge edge in edgeList)
				{
					// Unify edge's start/end nodes if not in the same component
					if (!disjointSet.Connected(edge.From, edge.To))
						disjointSet.Unify(edge.From, edge.To);
				}
			}
			ComponentCount = disjointSet.NumComponents;
			// Color all nodes to show all connected components if in visualization mode
			if (vizMode)
			{
				// map component ids to colors, ids may not be sequntial
				var colors = new Dictionary<int, Color>(ComponentCount);
				for (int i = 0; i < graph.NodeCount; i++)
				{
					int compId = disjointSet.Find(i);
					System.Console.WriteLine($"COMP ID: {compId}");
					if (!colors.ContainsKey(compId)) colors[compId] = Colors.GetRandom();
					graph.MarkParticle(i, colors[compId]);
				}
			}
			return true;
		}
	}
}
