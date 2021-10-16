using System;

using AlgorithmVisualizer.DBHandler;
using AlgorithmVisualizer.Forms.Dialogs;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	static class GraphSerializer
	{
		public static void Serialize(Graph graph, string name)
		{
			// Serialize this graph into a string (adjacency list)

			graph.FixNodeIdNumbering();
			string serialization = "";
			for (int i = 0; i < graph.NodeCount; i++)
			{
				string row = i + ": ";
				foreach (Edge edge in graph.AdjList[i]) row += $"({edge.To},{edge.Cost}), ";
				serialization += row + "\n";
			}
			// Save preset in DB
			DBConnection db = DBConnection.GetInstance();
			if (db.Connect())
			{
				// Connect to DB and store new preset with given name and disconnect
				var newPreset = new Preset(name, serialization);
				if (!db.AddPreset(newPreset))
					Console.WriteLine($"Failed to add preset '{name}'");
				db.Disconnect();
			}
		}
		public static void Deserialize(Graph graph, string[] serialization)
		{
			// Given the serialization of a graph as a string array will deserialize it, 
			// that is recreate the graph by parsing it.
			// Note that first all nodes must be added, and only after also the edges.(an
			// edge may link to a node that has not yet been created)

			if (serialization == null) throw new ArgumentException("Serial may not be null!");
			graph.ClearGraph();
			// Parsing vertices from serialization
			foreach (string line in serialization)
			{
				if (!String.IsNullOrEmpty(line))
				{
					// Split entire line via ": "
					string[] idAndEdgesSplit = Split(line, ": ");
					int from = Int32.Parse(idAndEdgesSplit[0]);
					// Add the node into the graph
					graph.AddNode(from, 0);
				}
			}
			// Parsing edges from serialization
			foreach (string line in serialization)
			{
				if (!String.IsNullOrEmpty(line))
				{
					// Split entire line via ": "
					string[] splitIdAndEdges = Split(line, ": ");
					int from = Int32.Parse(splitIdAndEdges[0]);
					// Split edges list via ", "
					string[] edgesAsStr = Split(splitIdAndEdges[1], ", ");
					if (edgesAsStr != null)
					{
						foreach (string edgeAsStr in edgesAsStr)
						{
							if (!String.IsNullOrEmpty(edgeAsStr))
							{
								// Remove () from str
								string edgeWithoutParenthesis = edgeAsStr.Substring(1, edgeAsStr.Length - 2);
								// Split edge via ',', first split is the dst node second split is the cost
								string[] splitToAndCost = edgeWithoutParenthesis.Split(',');
								int to = Int32.Parse(splitToAndCost[0]), cost = Int32.Parse(splitToAndCost[1]);
								// Add the edge into the graph
								graph.AddDirectedEdge(from, to, cost);
							}
						}
					}
				}
			}

			// Helper method to split a given string using a given splitter(string).
			string[] Split(string str, string splitter) =>
				str.Split(new string[] { splitter }, StringSplitOptions.None);
		}
	}
}
