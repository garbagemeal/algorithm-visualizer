namespace AlgorithmVisualizer.Forms
{
	public class GraphAlgoSettings
	{
		public string[] AlgoNames = {
			"BFS",
			"DFS",
			"Connected components - DFS",
			"Connected components - disjoint set",
			"Lazy Prim's MST",
			"Kruskal's MST",
			"Top sort DFS",
			"Kahn's top sort",
			"SSSP for DAGs",
			"Lazy Dijkstra's SSSP",
			"Eager Dijkstra's SSSP",
			"Bellman Ford's",
			"Tarjan's SCCs",
			"Kosaraju's SCCs"
		};
		
		// enum defining what node ids are required for an algorithm
		public enum RequiredNodes { None = 0, Start = 1, StartAndEnd = 2 };

		// Array of required nodes to match the array "AlgoNames". i.e:
		// nodeIdsNeeded[i] = RequiredNodes.None implies the algo with name stored in
		// AlgoNames[i] requirees no node ids to run.
		public RequiredNodes[] RequiredNodeIds = new RequiredNodes[] {
			RequiredNodes.StartAndEnd,
			RequiredNodes.StartAndEnd,
			RequiredNodes.None,
			RequiredNodes.None,
			RequiredNodes.None,
			RequiredNodes.None,
			RequiredNodes.None,
			RequiredNodes.None,
			RequiredNodes.Start,
			RequiredNodes.StartAndEnd,
			RequiredNodes.StartAndEnd,
			RequiredNodes.Start,
			RequiredNodes.None,
			RequiredNodes.None
		};
	}
}
