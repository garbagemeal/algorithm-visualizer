using System.Drawing;

using static AlgorithmVisualizer.Threading.PauseResumeSleep;

namespace AlgorithmVisualizer.GraphTheory.Algorithms
{
	abstract class GraphAlgorithm
	{
		// Abstract class defining common attributes and functionality to graph algorithms

		/*
		 * graph - A class containing both the logical and the visual representations of
		 * the graph, contains methods for visual and logical manipulations of the graph.
		 * 
		 * panelLogG - The graphics object used to draw in the log panel, i.e, tracers.
		 */
		protected Graph graph;
		protected Graphics panelLogG;

		public GraphAlgorithm(Graph _graph)
		{
			graph = _graph;
			panelLogG = graph.GLog;
		}
		
		abstract public bool Solve();
		protected void Sleep(Delay delay) => graph.Sleep(delay);
	}
}
