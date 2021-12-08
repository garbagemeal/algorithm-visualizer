using System;

using AlgorithmVisualizer.Presets;

namespace AlgorithmVisualizer.GraphTheory.Utils
{
	static class GraphSerializer
	{
		/******************************************
		 Example of a graph's serialization format:
		 ******************************************
		 0: (1,12), 
		 1: (0,12), (2,14), (3,13), (5,13), 
		 2: (1,14), (4,11), (5,6), 
		 3: (1,13), (4,5), (5,2), (6,7), 
		 4: (2,11), (3,5), (5,3), 
		 5: (1,13), (2,6), (3,2), (4,3), 
		 6: (3,7), 

		 ************
		 Explanation:
		 ************
		 vertex_id: (edge_destination, edge_cost), (edge_destination, edge_cost), ... \n
		 vertex_id: (edge_destination, edge_cost), (edge_destination, edge_cost), ... \n
		 .
		 .
		 .
		 ******************************************/

		public static void Serialize(Graph graph, string name)
		{
			// Serialize this graph into a string (adjacency list).

			graph.FixNodeIdNumbering();
			string serialization = "";
			for (int i = 0; i < graph.NodeCount; i++)
			{
				string row = i + ": ";
				foreach (Edge edge in graph.AdjList[i]) row += $"({edge.To},{edge.Cost}), ";
				serialization += row + "\n";
			}
			// Save the preset as an XML doc
			new Preset { id = Guid.NewGuid().ToString(), name = name, serial = serialization }.Save();
		}
		public static void Deserialize(Graph graph, string[] serialization)
		{
			// Parse given serialization and recreate the graph (if non null)
			if (serialization == null) throw new ArgumentException("Serial may not be null!");
			ParseVertices(graph, serialization);
			ParseEdges(graph, serialization);
		}
		private static void ParseVertices(Graph graph, string[] serialization)
		{
			// Parsing vertices from serialization
			foreach (string line in serialization)
			{
				if (!String.IsNullOrEmpty(line))
				{
					// Split entire line via ": "
					string[] idAndEdgesSplit = Split(line, ": ");
					int from = Int32.Parse(idAndEdgesSplit[0]);
					// Add the node into the graph
					graph.AddNode(from);
				}
			}
		}
		private static void ParseEdges(Graph graph, string[] serialization)
		{
			// Parsing edges from serialization
			foreach (string line in serialization)
			{
				if (!String.IsNullOrEmpty(line))
				{
					// Split entire line by ": "
					string[] splitIdAndEdges = Split(line, ": ");
					int from = Int32.Parse(splitIdAndEdges[0]);
					// Split edge list by ", "
					string[] edgesAsStr = Split(splitIdAndEdges[1], ", ");
					foreach (string edgeAsStr in edgesAsStr)
					{
						if (!String.IsNullOrEmpty(edgeAsStr))
						{
							// Remove () from str
							string edgeWithoutParenthesis = edgeAsStr.Substring(1, edgeAsStr.Length - 2);
							// Split edge by ','
							string[] splitToAndCost = edgeWithoutParenthesis.Split(',');
							int to = Int32.Parse(splitToAndCost[0]), cost = Int32.Parse(splitToAndCost[1]);
							// Add the edge into the graph
							graph.AddDirectedEdge(from, to, cost);
						}
					}
				}
			}
		}

		// Split a given string using the given splitter(string).
		private static string[] Split(string str, string splitter) =>
			str.Split(new string[] { splitter }, StringSplitOptions.None);
	}
}