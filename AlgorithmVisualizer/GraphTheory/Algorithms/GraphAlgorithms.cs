
/*******************************************************
 * This file is deprecated but is kept as a reference. *
 * In future this file will be deleted.                *
 *******************************************************/






























//using static System.Math;
//using System;
//using System.Collections.Generic;
//using System.Drawing;

//using AlgorithmVisualizer.ArrayTracer;
//using AlgorithmVisualizer.DataStructures;
//using AlgorithmVisualizer.DataStructures.Heap;
//using AlgorithmVisualizer.GraphTheory.Utils;

//namespace AlgorithmVisualizer.GraphTheory.FDGV
//{
//	class GraphAlgorithms : GraphAdjList
//	{
//		//private static Panel panelLog;
//		private Graphics panelLogG;

//		public GraphAlgorithms(Graphics g, int panelHeight, int panelWidth, Graphics _panelLogG)
//			: base(g, panelHeight, panelWidth)
//		{
//			//panelLog = FDGVForm.panelLog;
//			panelLogG = _panelLogG;
//		}

//		#region Graph algorithms
//		// Note: given node ids (from/to) are assumed to be valid!
//		public void BFS(int from, int to)
//		{
//			// BFS to check connectivity - setup
//			Queue<int> q = new Queue<int>();
//			HashSet<int> visited = new HashSet<int>();
//			q.Enqueue(from);
//			visited.Add(from);
//			ArrayTracer<int> qTracer =
//				new ArrayTracer<int>(q, panelLogG, "q: ", 0, 5, 500, 25);
//			qTracer.Trace();
//			Sleep(1500);
//			bool reachedTo = BFS(to, q, visited, qTracer);
//			Console.WriteLine("There exists {0} path from {1} to {2}",
//				reachedTo ? "a" : "no", from, to);
//		}
//		private bool BFS(int to, Queue<int> q, HashSet<int> visited,
//			ArrayTracer<int> qTracer)
//		{
//			// BFS to check connectivity
//			while (q.Count > 0)
//			{
//				qTracer.HighlightAt(0);
//				int at = q.Dequeue();
//				// Draw visited node in red if the end node otherwise in orange
//				DrawParticle(at, at == to ? Colors.Red : Colors.Orange); 
//				Sleep(1000);
//				qTracer.Trace();
//				Sleep(1000);
//				// if end node reached
//				if (at == to) return true;
//				// end node was not reached
//				// if at has outgoing edges
//				if (adjList[at] != null)
//				{
//					foreach (Edge edge in adjList[at])
//					{
//						// Draw edge in orange before visiting. last argument(0) indicates
//						// a directed edge (edge.From --> edge.To)
//						RedrawSpring(edge, Colors.Orange, 0);
//						Sleep(1000);
//						// avoid edges to visited nodes
//						if (!visited.Contains(edge.To))
//						{
//							visited.Add(edge.To);
//							q.Enqueue(edge.To);
//							qTracer.Trace();
//						}
//						// Draw visited edge in dark gray
//						// Note: the last argument is ommited, meaning it defaults to -1
//						// the edge is drawn as it originally appeared in the graph
//						RedrawSpring(edge, Colors.Visited);
//						Sleep(1000);
//					}
//				}
//				// Draw particle in dark gray (visited)
//				DrawParticle(at, Colors.Visited, Colors.VisitedBorder);
//				Sleep(1000);
//			}
//			return false;
//		}

//		public void DFS(int from, int to)
//		{
//			// DFS to check connectivity - setup
//			HashSet<int> visited = new HashSet<int>();
//			bool reachedTo = DFS(from, to, visited);
//			Console.WriteLine("There exists {0} path from {1} to {2}",
//				reachedTo ? "a" : "no", from, to);
//		}
//		private bool DFS(int at, int to, HashSet<int> visited)
//		{
//			// DFS to check connectivity
//			visited.Add(at);
//			DrawParticle(at, at == to ? Colors.Red : Colors.Orange);
//			Sleep(1500);
//			if (at == to) return true;
//			if (adjList[at] != null)
//			{
//				foreach (Edge edge in adjList[at])
//				{
//					// Does not visualize edges to already visited nodes to better show
//					// backtracking
//					if (!visited.Contains(edge.To))
//					{
//						// draw edge in orange to mark as visiting
//						RedrawSpring(edge, Colors.Orange, 0);
//						Sleep(1000);
//						if (DFS(edge.To, to, visited))
//						{
//							// If the a path has been found to the end node(to) then
//							// each node in the path will be highlighted here
//							RedrawSpring(edge, Colors.Green, 0);
//							DrawParticle(at, Colors.Green);
//							Sleep(1500);
//							return true;
//						}
//						// draw edge in dark gray to to mark as visited
//						RedrawSpring(edge, Colors.Visited);
//						Sleep(1000);
//					}
//				}
//			}
//			// Draw particle in darkGreyBrush (visited)
//			DrawParticle(at, Colors.Visited, Colors.VisitedBorder);
//			Sleep(1000);
//			return false;
//		}

//		public void ConnectedComponentsDFS()
//		{
//			// List graph's components - setup
//			// If the graph is not undirected does nothing
//			if (IsUndirected())
//			{
//				List<GComponent> components = new List<GComponent>();

//				HashSet<int> visited = new HashSet<int>();
//				foreach (int nodeId in nodeLookup.Keys)
//				{
//					if (!visited.Contains(nodeId))
//					{
//						GComponent gComponent = new GComponent();
//						components.Add(gComponent);
//						Color color = gComponent.Color;
//						ConnectedComponentsDFS(nodeId, gComponent, visited, color);
//						Sleep(1500);
//					}
//				}
//				foreach (GComponent gComponent in components)
//					Console.WriteLine(gComponent);
//			}
//		}
//		private void ConnectedComponentsDFS(int at, GComponent gComponent,
//			HashSet<int> visited, Color color)
//		{
//			// DFS to find connected componenets
//			visited.Add(at);
//			gComponent.NodeIds.Add(at);
//			DrawParticle(at, color);
//			Sleep(1500);
//			if (adjList[at] != null)
//			{
//				foreach (Edge edge in adjList[at])
//				{
//					if (!visited.Contains(edge.To))
//					{
//						// draw edge in orange to mark as visiting
//						RedrawSpring(edge, color, 0);
//						Sleep(1000);
//						ConnectedComponentsDFS(edge.To, gComponent, visited, color);
//						// draw edge in dark gret to to mark as visited
//						RedrawSpring(edge, Colors.Visited);
//						Sleep(1000);
//					}
//				}
//			}
//			Sleep(1000);
//		}

//		public int ConnectedComponentsDisjointSet(bool vizMode = true)
//		{
//			// Finds the graph's connected componenets
//			// Assumption: the graph is undirected and also all nodes are indexed
//			// from 0 to n non inclusive (otherwise will crash!)
//			if (!IsUndirected()) return -1;
//			DisjointSet disjointSet = new DisjointSet(NodeCount);

//			foreach (int nodeId in adjList.Keys)
//			{
//				List<Edge> edgeList = adjList[nodeId];
//				if(edgeList != null)
//				{
//					foreach(Edge edge in edgeList)
//					{
//						// If nodes share an edge and both nodes are not part
//						// of the same component in the disjoint set
//						if (!disjointSet.Connected(edge.From, edge.To))
//							disjointSet.Unify(edge.From, edge.To);
//					}
//				}
//			}
//			// Color all nodes to show all connected components
//			int numComponents = disjointSet.NumComponents;
//			// If in visualization mode
//			if (vizMode)
//			{
//				Color[] colors = new Color[NodeCount];
//				for (int i = 0; i < NodeCount; i++) colors[i] = Colors.GetRandomColor();
//				for (int i = 0; i < NodeCount; i++) DrawParticle(i, colors[disjointSet.Find(i)]);
//			}
//			return numComponents;
//		}

//		public void LazyPrimsMST(int from = 0)
//		{
//			// Find the graph's MST - setup
//			// If the graph is not connected does nothing
//			if (!IsConnectedUndirected()) return;
//			BinaryMinHeap<Edge> heap = new BinaryMinHeap<Edge>();
//			HashSet<int> visited = new HashSet<int>();
//			ArrayTracer<Edge> heapTracer =
//				new ArrayTracer<Edge>(heap, panelLogG, "Heap: ", 0, 10, 500, 45);
//			(int Cost, List<Edge> Edges) MSTDetails =
//				LazyPrimsMST(from, heap, visited, heapTracer);
//			// If the edge list is null then there is no min spanning tree
//			// This case is not to be confused with an empty (non-null) list
//			// represents an MST of a single node with no edes.
//			if (MSTDetails.Edges == null)
//				Console.WriteLine("There is no MST for the given graph");
//			else
//			{
//				Console.WriteLine("MST Details: \nCost: {0}, Edge list:", MSTDetails.Cost);
//				foreach (Edge edge in MSTDetails.Edges) Console.WriteLine(edge);
//			}
//		}
//		private (int, List<Edge>) LazyPrimsMST(int from, BinaryMinHeap<Edge> heap,
//			HashSet<int> visited, ArrayTracer<Edge> heapTracer)
//		{
//			// Find the graph's MST
//			int expectedEdgeCount = NodeCount - 1, edgeCount = 0, mstCost = 0;
//			List<Edge> mstEdges = new List<Edge>();
//			EnqueueAdjEdges(from, visited, heap, heapTracer);

//			while (heap.Count > 0 && edgeCount != expectedEdgeCount)
//			{
//				heapTracer.HighlightAt(0);
//				Edge edge = heap.Dequeue();
//				RedrawSpring(edge, Colors.Red, 0);
//				Sleep(1500);
//				heapTracer.Trace();
//				// Avoid adding edges to already visited nodes
//				bool edgeVisited = visited.Contains(edge.To);
//				RedrawSpring(edge, edgeVisited ? Colors.Visited : Colors.Green, edgeVisited ? -1 : 0);
//				if (!edgeVisited)
//				{
//					//mstEdges[edgeCount++] = edge;
//					mstEdges.Add(edge); edgeCount++;
//					mstCost += edge.Cost;
//					EnqueueAdjEdges(edge.To, visited, heap, heapTracer);
//				}
//				Sleep(1000);
//			}
//			return edgeCount == expectedEdgeCount ? (mstCost, mstEdges) : (0, null);
//		}
//		private void EnqueueAdjEdges(int nodeId, HashSet<int> visited,
//			BinaryMinHeap<Edge> heap, ArrayTracer<Edge> heapTracer)
//		{
//			// Function to visit a node and enqueue all adj edges
//			// mark node as visited
//			visited.Add(nodeId);
//			Sleep(1000);
//			DrawParticle(nodeId, Colors.Orange);
//			// add all outgoing edges from nodeId while avoiding
//			// adding edges to already visited nodes
//			// also visualizes all edges to non visted nodes and the node itself
//			if (adjList[nodeId] != null)
//			{
//				foreach (Edge edge in adjList[nodeId])
//				{
//					if (!visited.Contains(edge.To))
//					{
//						RedrawSpring(edge, Colors.Orange, 0);
//						heap.Enqueue(edge);
//						heapTracer.Trace();
//						Sleep(1000);
//					}
//				}
//				Sleep(1000);
//				// draw edges in dark grey (visited)
//				foreach (Edge edge in adjList[nodeId])
//					if (!visited.Contains(edge.To)) RedrawSpring(edge, Colors.Visited);
//			}
//			// draw particle in darkGreyBrush (visited)
//			DrawParticle(nodeId, Colors.Visited, Colors.VisitedBorder);
//			Sleep(1000);
//		}

//		private List<Edge> GetUndirectedEdgeList()
//		{
//			// O(E)
//			// Assumes the graph is "undirected"; for each edge (u, v, x)
//			// there exists an edge (v, u, x).
//			// Returns a list of all edges(u, v, x) in the graph where u < v
//			List<Edge> edgeList = new List<Edge>();
//			foreach (List<Edge> curEdgeList in adjList.Values)
//			{
//				if (curEdgeList != null)
//					foreach (Edge edge in curEdgeList)
//						if (edge.From < edge.To) edgeList.Add(edge);

//			}
//			return edgeList;
//		}
//		public void KruskalsMST()
//		{
//			/*
//			 * Finds the graph's MST(min spanning tree) or MSF (min spanning forest)
//			 * Note: The algorithm will result in the MST if the graph is connected
//			 * otherwise if the graph is disconnected then the result will be a MSF.
//			 * Also all nodes must be indexed from 0 to n non inclusive
//			 */
//			// If the graph is not undirected or has 0 edges do nothing
//			if (!IsUndirected()) return;
//			// Getting list of all undirected edges from adjList
//			List<Edge> edgeList = GetUndirectedEdgeList();
//			if (edgeList.Count < 1)
//			{
//				Console.WriteLine("Graph has no edges (1 edge needed at least for heap creation)");
//				return;
//			}
//			// O(E)
//			// Avoid sorting the edges by creating a heap from the list O(n) where n = E
//			BinaryMinHeap<Edge> heap = new BinaryMinHeap<Edge>(edgeList);
//			// O(V) = O(sqrt(E)) assuming graph is simple
//			// because E = V(V-1)/2 = (V^2 - V) / 2 and can be futher simplified to O(V^2)
//			// and thus E = roughly V^2, so O(V) is asymptotically the same as, O(sqrt(E).
//			// Creating a disjoint set (union find) of size V
//			DisjointSet disjointSet = new DisjointSet(NodeCount);
//			ArrayTracer<Edge> heapTracer = new ArrayTracer<Edge>(heap, panelLogG, "Heap: ", 0, 10, 500, 45);
//			(int Cost, List<Edge> Edges) MSTDetails = KruskalsMST(heap, disjointSet, heapTracer);

//			// Note that it may be a MSF and not a MST
//			Console.WriteLine("MST Cost: " + MSTDetails.Cost);
//			Console.WriteLine("MST Edges:");
//			foreach (Edge edge in MSTDetails.Edges) Console.WriteLine(edge);
//		}
//		private (int, List<Edge>) KruskalsMST(BinaryMinHeap<Edge> heap,
//			DisjointSet disjointSet, ArrayTracer<Edge> heapTracer)
//		{
//			// Finds and returns the graph's MST/MSF edge list and the total edge cost
//			List<Edge> mstEdges = new List<Edge>();
//			int mstCost = 0;
//			heapTracer.Trace();
//			Sleep(1500);
//			// O(E*log(E))
//			// As long as the heap is not empty and the disjoint set has more than 1 component
//			while (heap.Count > 0 && disjointSet.NumComponents > 1)
//			{
//				heapTracer.HighlightAt(0);
//				Edge edge = heap.Dequeue();
//				Sleep(1000);
//				heapTracer.Trace();
//				Sleep(1000);
//				// Avoid adding edges where both composing nodes already belong
//				// to the same group (would introduce a cycle to the MST!)
//				if (!disjointSet.Connected(edge.From, edge.To))
//				{
//					// Add edge into the MST's edge list and sum its cost
//					mstEdges.Add(edge);
//					mstCost += edge.Cost;
//					// Unify both composing nodes via id in the disjoint set
//					// This operation takes amortized time complexity(near constant)
//					disjointSet.Unify(edge.From, edge.To);
//					// Draw the edge
//					RedrawSpring(edge, Colors.Green);
//					Sleep(1000);
//				}
//			}
//			return (mstCost, mstEdges);
//		}

//		public int[] TopSortDFS()
//		{
//			// Returns the graph's topologial ordering using DFS
//			// If the graph is directed and acyclic do nothing
//			if (!IsDAG()) return null;

//			HashSet<int> visited = new HashSet<int>();
//			Stack<int> topOrderStk = new Stack<int>();
//			ArrayTracer<int> topOrderStkTracer = new ArrayTracer<int>(topOrderStk, panelLogG, "Top order: ", 0, 10, 500, 45);
//			foreach (int nodeId in nodeLookup.Keys)
//				if (!visited.Contains(nodeId)) TopSortDFS(nodeId, visited, topOrderStk, topOrderStkTracer);
//			// Popping the stack into the array - topOrder
//			int[] topOrder = new int[topOrderStk.Count];
//			for (int i = 0; i < topOrder.Length; i++) topOrder[i] = topOrderStk.Pop();
//			return topOrder;
//		}
//		private void TopSortDFS(int at, HashSet<int> visited, Stack<int> topOrderStk,
//			ArrayTracer<int> topOrderStkTracer)
//		{
//			// top sort into topSortStk using DFS
//			visited.Add(at);
//			DrawParticle(at, Colors.Orange);
//			Sleep(1500);
//			if (adjList[at] != null)
//				foreach (Edge edge in adjList[at])
//				{
//					int to = edge.To;
//					if (!visited.Contains(to))
//					{
//						RedrawSpring(edge, Colors.Orange);
//						Sleep(1000);
//						TopSortDFS(to, visited, topOrderStk, topOrderStkTracer);
//						RedrawSpring(edge, Colors.Visited);
//						Sleep(1000);
//					}
//				}
//			// Push values into the stack upon backtracking
//			topOrderStk.Push(at);
//			DrawParticle(at, Colors.Blue);
//			topOrderStkTracer.HighlightAt(0);
//			Sleep(1000);
//			DrawParticle(at, Colors.Visited, Colors.VisitedBorder);
//			topOrderStkTracer.Trace();
//			Sleep(1000);
//		}

//		public int[] KahnsTopSort(bool vizMode = true)
//		{
//			// Returns the graph's topologial ordering using Kahn's algo
//			// if the graph is not a DAG null will be returned
//			// Array storing the in degree per node
//			int[] inDeg = new int[NodeCount];
//			// O(E) - For each Edge uv in E the in degree of the dstination vertex(u) will increase by 1
//			foreach (List<Edge> edgeList in adjList.Values)
//				foreach (Edge edge in edgeList) inDeg[edge.To]++;
//			// O(V) - Create a queue that will initially hold all nodes with in degree of 0 
//			Queue<int> q = new Queue<int>();
//			for (int i = 0; i < NodeCount; i++) if (inDeg[i] == 0) q.Enqueue(i);
//			return KahnsTopSort(q, inDeg, vizMode);
//		}
//		public int[] KahnsTopSort(Queue<int> q, int[] inDeg, bool vizMode)
//		{
//			// Main method to run Kahn's top sort
//			// Note: vizMode used to disable visuals
//			// Create an array to store the topological ordering (of size NodeCount)
//			// idx is used to add node id's into the array from 0 to NodeCount
//			int idx = 0;
//			int[] topOrder = new int[NodeCount];

//			// Visuals(Tracers)
//			Dictionary<string, ArrayTracer<int>> tracers = new Dictionary<string, ArrayTracer<int>>();
//			CreateTracersKahnsAlgo(q, inDeg, vizMode, topOrder, tracers);

//			// As long as the queue is not enmpty run the algo
//			// O(V)
//			while (q.Count > 0)
//			{
//				if (vizMode) tracers["q"].HighlightAt(0);
//				// Remove curNode from the q and add it to the topOrder (its inDeg is 0)
//				int curNode = q.Dequeue();
//				topOrder[idx++] = curNode;
//				if (vizMode)
//				{
//					DrawParticle(curNode, Colors.Orange);
//					tracers["topOrder"].HighlightAt(idx - 1);
//					Sleep(2000);
//					tracers["q"].Trace(); tracers["topOrder"].Trace();
//					Sleep(1000);
//				}
//				VisitOutgoingEdgesKahnsAlgo(curNode, q, inDeg, tracers["q"], tracers["inDeg"], vizMode);
//				if (vizMode)
//				{
//					DrawParticle(curNode, Colors.Visited);
//					Sleep(1000);
//				}
//			}
//			// Note that if the number of processed nodes is not NodeCount then
//			// there exists a cycle in the graph! (topOrder will become null)
//			return idx == NodeCount ? topOrder: null;
//		}
//		private void VisitOutgoingEdgesKahnsAlgo(int curNode, Queue<int> q, int[] inDeg,
//			ArrayTracer<int> qTracer, ArrayTracer<int> inDegTracer, bool vizMode)
//		{
//			if(adjList[curNode] != null)
//			{
//				// for each outgoint edge from curNode
//				foreach (Edge edge in adjList[curNode])
//				{
//					int to = edge.To;
//					if (vizMode)
//					{
//						RedrawSpring(edge, Colors.Orange);
//						inDegTracer.HighlightAt(to);
//						Sleep(1000);
//					}
//					inDeg[to]--;
//					if (vizMode)
//					{
//						inDegTracer.HighlightAt(to);
//						Sleep(2000);
//					}
//					// If after decreasing To's inDeg by 1 it becomes 0, add to q
//					//O(Eadj) - Eadj is the out degree of curNode
//					if (inDeg[to] == 0)
//					{
//						q.Enqueue(to);
//						if (vizMode)
//						{
//							qTracer.HighlightAt(-1);
//							Sleep(1500);
//							qTracer.Trace();
//							Sleep(1000);
//						}
//					}
//					if (vizMode)
//					{
//						inDegTracer.Trace();
//						RedrawSpring(edge, Colors.Visited);
//						Sleep(1000);
//					}
//				}
//			}
//		}
//		private void CreateTracersKahnsAlgo(Queue<int> q, int[] inDeg, bool vizMode,
//			int[] topOrder, Dictionary<string, ArrayTracer<int>> tracers)
//		{
//			// Visuals(Tracers)
//			int[] idxArr = new int[NodeCount]; for (int i = 0; i < NodeCount; i++) idxArr[i] = i;
//			tracers["q"] = new ArrayTracer<int>(q, panelLogG, "q: ", 0, 10, 500, 45);
//			tracers["idx"] = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", 0, 57, 500, 25);
//			tracers["inDeg"] = new ArrayTracer<int>(inDeg, panelLogG, "inDeg: ", 0, 84, 500, 25);
//			tracers["topOrder"] = new ArrayTracer<int>(topOrder, panelLogG, "topOrder: ", 0, 111, 500, 25);
//			int topOrderTracerNameOffset = tracers["topOrder"].NameOffset;
//			foreach (ArrayTracer<int> tracer in tracers.Values) tracer.NameOffset = topOrderTracerNameOffset;
//			if (vizMode) DrawTracers(tracers);
//		}
//		private void DrawTracers(Dictionary<string, ArrayTracer<int>> tracers)
//		{
//			foreach (ArrayTracer<int> tracer in tracers.Values) tracer.Trace();
//		}

//		public void DAGSSSP(int from = 0)
//		{
//			/* Note:
//			 * The longest path can be computed by multiplying all edge cost's by -1, 
//			 * running this very same algo and afterwards, again, multiply all edge cost's by -1.
//			 * In doing so the longest path will be computed! a problem for which general grahps 
//			 * are considered NP Hard! */

//			int[] topSort = KahnsTopSort(vizMode: false);
//			// if graph is not a DAG do nothing
//			if(topSort == null)
//			{
//				Console.WriteLine("The given graph is not a DAG!");
//				return;
//			}
//			// a distance array where each index denotes the node id and the value
//			// denotes the current shortest distance to it
//			int[] distMap = new int[NodeCount];
//			// Fill distMap array with "inifinities" and set distance to starting node as 0
//			for (int i = 0; i < NodeCount; i++) distMap[i] = int.MaxValue;
//			distMap[from] = 0;

//			DAGSSSP(topSort, distMap);

//			for (int i = 0; i < distMap.Length; i++)
//				Console.WriteLine("Distance to {0}: {1}",
//					i, distMap[i] != int.MaxValue ? distMap[i] + "" : "INF");
//		}
//		private void DAGSSSP(int[] topSort, int[] distMap)
//		{
//			// Method to run the DAGSSSP after topSort and distMap have been prepared
//			// Go over each node in the topSort
//			for (int i = 0; i < NodeCount; i++)
//			{
//				// Note: i is the index of curNodeId in topSort
//				int curNodeId = topSort[i];
//				DrawParticle(curNodeId, Colors.Orange);
//				Sleep(1500);
//				// If the current node has alrady been reached and has incident edges
//				if (distMap[curNodeId] != int.MaxValue && adjList[curNodeId] != null)
//				{
//					// foreach edge incident to curNodeId
//					foreach (Edge edge in adjList[curNodeId])
//					{
//						// Edge relaxation
//						RedrawSpring(edge, Colors.Orange);
//						Sleep(1000);
//						// Compute new distance to reach edge.To
//						int newDist = distMap[curNodeId] + edge.Cost;
//						RedrawSpring(edge, newDist < distMap[edge.To] ? Colors.Red : Colors.Blue);
//						// Comapre both new and old distances and set to the smaller one
//						distMap[edge.To] = Math.Min(distMap[edge.To], newDist);
//						Sleep(1000);
//						RedrawSpring(edge, Colors.Visited);
//						Sleep(1000);
//					}
//				}
//				DrawParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
//				Sleep(1000);
//			}
//		}

//		private List<int> ReconstructPath(int to, int[] prev, bool endReached)
//		{
//			// Reconstruct the path using prev array by following predecessors starting
//			// from to until -1 is reached, -1 is the starting node's predecessor.
//			List<int> path = new List<int>();
//			// if to is reachable
//			if (endReached)
//			{
//				for (int at = to; at != -1; at = prev[at]) path.Add(at);
//				path.Reverse();
//			}
//			return path;
//		}
//		public void DijkstrasSSSP(int from, int to, int algoNum = 1)
//		{
//			// Note: algoNum denotes the algorithm number where:
//			// 0 is the lazy Dijkstra's SSSP algo variant
//			// 1 is the eager Dijkstra's SSSP algo variant
//			// If the graph is not positive edge weighted do nothing
//			if (!IsPositiveEdgeWeighted()) return;
//			int[] distMap = new int[NodeCount];
//			int[] prev = new int[NodeCount];
//			// distMap initially hols "infinities" and prev ids of -1 (instead of null)
//			for (int i = 0; i < NodeCount; i++)
//			{
//				distMap[i] = int.MaxValue;
//				prev[i] = -1;
//			}
//			HashSet<int> visited = new HashSet<int>();
//			distMap[from] = 0;

//			// Visuals(Tracers) for distMap, prev
//			int[] idxArr = new int[NodeCount]; for (int i = 0; i < NodeCount; i++) idxArr[i] = i;
//			ArrayTracer<int> idxTracer = new ArrayTracer<int>(idxArr, panelLogG, "idx: ", 0, 57, 500, 25);
//			ArrayTracer<int> distMapTracer = new ArrayTracer<int>(distMap, panelLogG, "distMap: ", 0, 84, 500, 25);
//			ArrayTracer<int> prevTracer = new ArrayTracer<int>(prev, panelLogG, "prev: ", 0, 111, 500, 25);

//			// Running Dijkstra's algo (Lazy/Eager version depending on algoNum)
//			bool endReached = algoNum == 0 ?
//				LazyDijkstrasSSSP(from, to, visited, distMap, prev, idxTracer, distMapTracer, prevTracer) :
//				EagerDijkstrasSSSP(from, to, visited, distMap, prev, idxTracer, distMapTracer, prevTracer);

//			List<int> path = ReconstructPath(to, prev, endReached);

//			if (endReached)
//			{
//				Console.WriteLine($"Shortest path from {from} to {to}:");
//				foreach (int nodeId in path) Console.Write(nodeId + " ");
//				Console.WriteLine("Cost: " + distMap[to]);
//				//Visualization of the shortest path (vertices only)
//				for (int i = 0; i < path.Count; i++)
//				{
//					DrawParticle(path[i], Colors.Green);
//					// If not the starting node, find edge to it in SP and draw it
//					if (i != 0) RedrawSpring(new Edge(prev[path[i]], path[i], -1), Colors.Green, 0);
//					Sleep(700);
//				}
//			}
//			else Console.WriteLine($"No path from {from} to {to}.");
//		}
//		private bool LazyDijkstrasSSSP(int from, int to, HashSet<int> visited, int[] distMap, int[] prev,
//			ArrayTracer<int> idxTracer, ArrayTracer<int> distMapTracer, ArrayTracer<int> prevTracer)
//		{
//			// Lazy Dijkstra's algo - O(Elog(V))
//			// Binary min proiority queue to hold most promising nodes
//			// Note that duplicate nodes are created with the same id's of existing nodes,
//			// and the Data is set to the current shortest distance to the node as a comperator
//			// for the MinHeap
//			BinaryMinHeap<GNode> heap = new BinaryMinHeap<GNode>();
//			heap.Enqueue(new GNode(from, 0));

//			// Visuals(Tracers) for distMap, prev, and heap
//			ArrayTracer<GNode> heapTracer = new ArrayTracer<GNode>(heap, panelLogG, "Heap: ", 0, 10, 500, 45);

//			// Set width of the name(title) for idxTracer and pervTracer to match distMapTracer's name width(expected to be widest)
//			idxTracer.NameOffset = prevTracer.NameOffset = distMapTracer.NameOffset;
//			// Trace arrays
//			idxTracer.Trace(); prevTracer.Trace();
//			distMapTracer.Trace(); heapTracer.Trace();
//			Sleep(2000);

//			while (heap.Count > 0)
//			{
//				heapTracer.HighlightAt(0);
//				GNode curNode = heap.Dequeue();
//				int curNodeId = curNode.Id, curNodeMinDist = curNode.Data;
//				visited.Add(curNodeId);
//				DrawParticle(curNodeId, Colors.Orange);
//				Sleep(1500);
//				heapTracer.Trace();
//				Sleep(1000);
//				// Optimization: in case destionation node has been reached (to id)
//				if (curNodeId == to)
//				{
//					// This can be done because Dijksta's algo will never update the distance
//					// to a node already removed from the queue (an already visited node), in fact
//					// it is assumed that the distance to already visited nodes is optimal and can't be improved!
//					return true;
//				}
//				VisitOutgoingEdgesLazyDijkstra(curNodeId, curNodeMinDist, visited,
//					distMap, prev, heap, heapTracer, distMapTracer, prevTracer);
//				DrawParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
//				Sleep(1000);
//			}
//			return false;
//		}
//		private void VisitOutgoingEdgesLazyDijkstra(int curNodeId, int curNodeMinDist,
//			HashSet<int> visited, int[] distMap, int[] prev, BinaryMinHeap<GNode> heap,
//			ArrayTracer<GNode> heapTracer, ArrayTracer<int> distMapTracer, ArrayTracer<int> prevTracer)
//		{
//			/* Ignore stale nodes.
//			 * Required for the lazy implementation because when updating distMap a new 
//			 * (id, dist) mapping is inserted into the heap, without possibly removing the 
//			 * old mapping of the same node id to a worse(greater) distance. */
//			if (curNodeMinDist <= distMap[curNodeId] && adjList[curNodeId] != null)
//			{
//				foreach (Edge edge in adjList[curNodeId])
//				{
//					// Edge relaxation
//					RedrawSpring(edge, Colors.Orange);
//					Sleep(1000);
//					int toId = edge.To;
//					int newDist = distMap[curNodeId] + edge.Cost;
//					// Can't imporove distance by revisiting a node
//					if (!visited.Contains(toId) && newDist < distMap[toId])
//					{
//						RedrawSpring(edge, Colors.Red);
//						prevTracer.HighlightAt(toId);
//						distMapTracer.HighlightAt(toId);
//						prev[toId] = curNodeId;
//						distMap[toId] = newDist;
//						Sleep(1500);
//						prevTracer.Trace();
//						distMapTracer.Trace();
//						heap.Enqueue(new GNode(toId, newDist));
//						heapTracer.Trace();
//						Sleep(1000);
//					}
//					// stale edge (to already visited or no imporvement in cost)
//					else RedrawSpring(edge, Colors.Blue);
//					Sleep(1000);

//					RedrawSpring(edge, Colors.Visited);
//					Sleep(1000);
//				}
//			}
//		}
//		private bool EagerDijkstrasSSSP(int from, int to, HashSet<int> visited, int[] distMap, int[] prev,
//			ArrayTracer<int> idxTracer, ArrayTracer<int> distMapTracer, ArrayTracer<int> prevTracer)
//		{
//			// Eager Dijkstra's algo - O(ElogE/V(V))
//			int degree = edgeCount / NodeCount;
//			MinIndexedDHeap<int> ipq = new MinIndexedDHeap<int>(degree, NodeCount);
//			ipq.InsertAt(from, 0);

//			// Visuals(Tracers) for distMap, prev, and ipq
//			ArrayTracer<int> ipqTracer = new ArrayTracer<int>(ipq, panelLogG, "IPQ: ", 0, 10, 500, 45);

//			// Set width of the name(title) for idxTracer and pervTracer to match distMapTracer's name width(expected to be widest)
//			idxTracer.NameOffset = prevTracer.NameOffset = distMapTracer.NameOffset;
//			// Trace arrays
//			ArrayTracer<int>[] tracers = new ArrayTracer<int>[] { idxTracer, distMapTracer, prevTracer, ipqTracer };
//			foreach (ArrayTracer<int> tracer in tracers) tracer.Trace();
//			Sleep(2000);

//			while (ipq.Count > 0)
//			{
//				ipqTracer.HighlightAt(0);
//				int curNodeId = ipq.PeekMinKeyIndex(), curNodeMinDist = ipq.DequeueMinValue();
//				visited.Add(curNodeId);
//				DrawParticle(curNodeId, Colors.Orange);
//				Sleep(1500);
//				ipqTracer.Trace();
//				Sleep(1000);
//				// Optimization: in case destionation node has been reached (to id)
//				if (curNodeId == to)
//				{
//					return true;
//				}
//				VisitOutgoingEdgesEagerDijkstra(curNodeId, visited,
//					distMap, prev, ipq, ipqTracer, distMapTracer, prevTracer);
//				DrawParticle(curNodeId, Colors.Visited, Colors.VisitedBorder);
//				Sleep(1000);
//			}
//			return false;
//		}
//		private void VisitOutgoingEdgesEagerDijkstra(int curNodeId, HashSet<int> visited,
//			int[] distMap, int[] prev, MinIndexedDHeap<int> ipq, ArrayTracer<int> ipqTracer,
//			ArrayTracer<int> distMapTracer, ArrayTracer<int> prevTracer)
//		{
//			// Assumption(need to verify): stale nodes may not exist in the ipq because
//			// any update for distMap in this function is also reflected in ipq.
//			if (adjList[curNodeId] != null)
//			{
//				foreach (Edge edge in adjList[curNodeId])
//				{
//					RedrawSpring(edge, Colors.Orange);
//					Sleep(1000);
//					// Edge relaxation
//					int toId = edge.To;
//					int newDist = distMap[curNodeId] + edge.Cost;
//					// Can't imporove distance by revisiting a node
//					if (!visited.Contains(toId) && newDist < distMap[toId])
//					{
//						RedrawSpring(edge, Colors.Red);
//						prevTracer.HighlightAt(toId);
//						distMapTracer.HighlightAt(toId);
//						prev[toId] = curNodeId;
//						distMap[toId] = newDist;
//						Sleep(1500);
//						prevTracer.Trace();
//						distMapTracer.Trace();
//						// Insert node into ipq with its distance if not present in it,
//						// otherwise try improve the value (decrease it)
//						if (!ipq.Contains(toId)) ipq.InsertAt(toId, newDist);
//						else ipq.DecreaseKey(toId, newDist);
//						ipqTracer.Trace();
//						Sleep(1000);
//					}
//					// stale edge (to already visited or no imporvement in cost)
//					else RedrawSpring(edge, Colors.Blue);
//					Sleep(1000);
//					RedrawSpring(edge, Colors.Visited);
//					Sleep(1000);
//				}
//			}
//		}

//		public void BellmanFord(int from = 0)
//		{
//			// SSSP algo, detects negative cycles. O(VE)
//			// For dense graphs will be O(V^3) in which case an adjacency matrix is better
//			// Proof of correctness(starts at 18:00):
//			// https://www.youtube.com/watch?v=ozsuci5pIso&list=PLUl4u3cNGP61Oq3tWYp6V_F-5jb5L2iHb&index=17&ab_channel=MITOpenCourseWare

//			int[] distMap = new int[NodeCount];
//			for (int i = 0; i < NodeCount; i++) distMap[i] = i == from ? 0 : int.MaxValue;
//			// Find the SSSP for each vertex by relaxing each edge V-1 times.
//			for (int i = 1; i < NodeCount; i++)
//			{
//				foreach (List<Edge> edgeList in adjList.Values)
//				{
//					foreach (Edge edge in edgeList)
//					{
//						// Edge relaxation
//						int newCost = distMap[edge.From] + edge.Cost;
//						if (newCost < distMap[edge.To]) distMap[edge.To] = newCost;
//					}
//				}
//			}
//			foreach (List<Edge> edgeList in adjList.Values)
//			{
//				foreach (Edge edge in edgeList)
//				{
//					// Edge relaxation, if the edge can be relaxed then the destination
//					// node is part of negative a cycle, distMap[edge.To] becomes -INF
//					int newCost = distMap[edge.From] + edge.Cost;
//					if (newCost < distMap[edge.To]) distMap[edge.To] = int.MinValue;
//				}
//			}
//			/* +INF  : unreachable node
//			 * -INF  : node in a negative a cycle
//			 * other : reachable node with no negative cycles on the SP nor any other path
//			 * leading to that node */
//			for (int i = 0; i < NodeCount; i++)
//			{
//				int dist = distMap[i];
//				string distAsStr =
//					dist == int.MaxValue ? "+INF" :
//					dist == int.MinValue ? "-INF" :
//					dist.ToString();
//				Console.WriteLine($"DistMap[{i}] = {distAsStr}");
//			}
//		}

//		public void TarjansSCC()
//		{
//			/*
//			 * O(V + E)
//			 * Tarjan's Strongly Connected Components(Tarjan's SCC) algorithm
//			 * Expected input is a directed graph
//			 */
//			const int UNVISITED = -1;
//			int id = 0, SCCCount = 0;
//			int[] ids = new int[NodeCount], low = new int[NodeCount];
//			Stack<int> stk = new Stack<int>();
//			bool[] onStk = new bool[NodeCount];
//			for (int i = 0; i < NodeCount; i++) ids[i] = UNVISITED;
//			for (int i = 0; i < NodeCount; i++) if (ids[i] == UNVISITED) DFS(i);
//			void DFS(int at)
//			{
//				stk.Push(at);
//				onStk[at] = true;
//				ids[at] = low[at] = id++;
//				// Visit at's neighbours and min low-link on callback
//				foreach (Edge edge in adjList[at])
//				{
//					int to = edge.To;
//					if (ids[to] == UNVISITED) DFS(to);
//					if (onStk[to]) low[at] = Min(low[at], low[to]);
//				}
//				// If we're at the start of a SCC empty the seen stack
//				// until we're back to the start of the SCC.
//				if (ids[at] == low[at])
//				{
//					for (int nodeId = stk.Pop(); ; nodeId = stk.Pop())
//					{
//						onStk[nodeId] = false;
//						low[nodeId] = ids[at];
//						if (nodeId == at) break;
//					}
//					SCCCount++;
//				}
//			}
//			// May have colors brushes, can be bounded via SCCCount with adjustment
//			Color[] colors = new Color[NodeCount];
//			for (int i = 0; i < NodeCount; i++) colors[i] = Colors.GetRandomColor();
//			for (int i = 0; i < NodeCount; i++) DrawParticle(i, colors[low[i]]);
//		}

//		public void KosarajusSCC()
//		{
//			// Kosaraju's Strongly connected componenets - O(V + E)
//			HashSet<int> visited = new HashSet<int>();
//			Stack<int> stk = new Stack<int>();
//			// sccCount tracks number of SCCs, ids denotes the SCC id for each node.
//			// i.e, ids[i] = j implies node with id 'i' belongs to SCC with id 'j';
//			// ids[i] = ids[j] implies nodes i and j are part of the same SCC.
//			int sccCount = 0;
//			int[] ids = new int[NodeCount];

//			// Perform DFS on original graph, when backtracking push node id into stk.
//			for (int i = 0; i < NodeCount; i++) if (!visited.Contains(i)) DFS(adjList, i);

//			// Compute Gt - Gt is identical to G where each edge is reversed.
//			Dictionary<int, List<Edge>> Gt = GetGTranspose();
//			visited.Clear();
//			// Reverse every directed edge in adjList to visualize Gt
//			ReverseSprings();
//			Sleep(5000);
//			// Perfrom DFS on Gt, when backtracking assign SCC id for node.
//			while (stk.Count > 0)
//			{
//				int i = stk.Pop();
//				if (!visited.Contains(i))
//				{
//					// Each non recursive call to DFS(G, at) results in a new SCC
//					DFS(Gt, i);
//					sccCount++;
//				}
//			}
//			for (int i = 0; i < NodeCount; i++)
//				Console.WriteLine($"Node id: {i}, SCC: {ids[i]}");
//			// Reverse every directed edge in adjList to unvisualize Gt
//			ReverseSprings();

//			// Remove flag by checking if graph == adjList
//			void DFS(Dictionary<int, List<Edge>> graph, int at)
//			{
//				visited.Add(at);
//				// Visit neighbors of 'at'
//				foreach (Edge edge in graph[at])
//					if (!visited.Contains(edge.To)) DFS(graph, edge.To);
//				// After visiting all neighbors of 'at', depending on mode:
//				// Either push 'at' into stk or assign SCC id per node id.
//				if (graph == adjList) stk.Push(at);
//				else ids[at] = sccCount;
//			}
//		}

//		/* Note to self(idea):
//		 * Pass tracers as an array and destruct within
//		 * functions to shorten the signatures
//		 * 
//		 * Another idea is to create some abstract visit outgoing edges for all
//		 * algos requiring it and reusing instead of duplicating
//		 * 
//		 * Last option thought of is creating larger functions to scope variables rather
//		 * then passing them around, i.e, using nested functions.
//		 * */
//		#endregion

//		#region Validators
//		// Methods to check for some properties of the graph
//		protected bool IsUndirected()
//		{
//			// Method to check if the graph is undirected
//			// Go over all edges in the graph
//			// if any edge does not appear reversed in adjList[edge.To]
//			// return false (found a directed edge, should be undirected)
//			foreach (List<Edge> edgeList in adjList.Values)
//			{
//				foreach (Edge edge in edgeList)
//				{
//					if (!adjList[edge.To].Contains(Edge.Reversed(edge)))
//					{
//						Console.WriteLine($"Found a directed edge: {edge}\nThe graph is not undirected!");
//						return false;
//					}
//				}
//			}
//			return true;
//		}
//		protected bool IsConnectedUndirected()
//		{
//			// Run connected components algo on the graph without visuals and
//			// check if there is only 1 component (meaning the graph is connected)
//			// Note: the call to ConnectedComponentsDisjointSet ensures G is undirected.
//			if (ConnectedComponentsDisjointSet(vizMode: false) == 1) return true;
//			else Console.WriteLine("The graph is not connected and undirected!");
//			return false;
//		}
//		protected bool IsDAG()
//		{
//			// Checks if the graph is a DAG using Kahn's algo
//			if (KahnsTopSort(vizMode: false) != null) return true;
//			else Console.WriteLine("Graph is not a DAG!");
//			return false;
//		}
//		protected bool IsPositiveEdgeWeighted()
//		{
//			// Checks if all the edges in the graph have a positive weight
//			// this check is important for Dijkstra's algorithm!
//			foreach (List<Edge> edgeList in adjList.Values)
//			{
//				foreach (Edge edge in edgeList)
//				{
//					if (edge.Cost < 0)
//					{
//						Console.WriteLine("The graph is not positive edge weighted!");
//						return false;
//					}
//				}
//			}
//			return true;
//		}
//		#endregion
//	}
//}
