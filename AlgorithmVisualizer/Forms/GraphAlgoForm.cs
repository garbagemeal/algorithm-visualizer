using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using AlgorithmVisualizer.Forms.Dialogs;
using AlgorithmVisualizer.Forms.Dialogs.AlgoDetails;
using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.Algorithms;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.MathUtils;
using static AlgorithmVisualizer.GraphTheory.FDGV.GraphVisualizer;

namespace AlgorithmVisualizer.Forms
{
	public partial class GraphAlgoForm : Form
	{
		// Bug list: Flickering, Graph dissapears after loading preset and clicking start ...
		private Graph graph;
		// Stuff for algoComboBox
		private int selectedAlgoNameIdx;
		private string[] algorithmNames = { "BFS", "DFS", "Connected components - DFS",
			"Connected components - disjoint set", "Lazy Prim's MST", "Kruskal's MST",
			"Top sort DFS", "Kahn's top sort", "SSSP for DAGs", "Lazy Dijkstra's SSSP",
			"Eager Dijkstra's SSSP", "Bellman Ford's", "Tarjan's SCCs", "Kosaraju's SCCs" };

		private Form parentForm;
		private Panel panelLog;
		private Graphics panelLogG;
		private BackgroundWorker bgw;
		private bool forceDirectedDrawMode = true;

		public GraphAlgoForm(Form _parentForm)
		{
			InitializeComponent();
			// adding algo names to the combo box
			foreach (string algoName in algorithmNames)
				algoComboBox.Items.Add(algoName);
			algoComboBox.SelectedIndex = selectedAlgoNameIdx = 0;

			// consider adding this to canvas: SmoothingMode.AntiAlias

			parentForm = _parentForm;
			panelLog = ((MainUIForm)parentForm).PanelLog;
			panelLogG = panelLog.CreateGraphics();
			graph = new Graph(canvas, panelLogG);
		}

		private void DrawGraph()
		{
			// Note that the boolean field "forceDirectedDrawMode" is modified in the method:
			// togglePhysicsToolStripMenuItem_Click()
			graph.DrawGraph(forceDirectedDrawMode ? DrawingMode.Default : DrawingMode.Forceless);
		}
		public void RunAlgo()
		{
			// pick random starting/eding nodes
			int nodeCount = graph.NodeCount;
			if (nodeCount > 0)
			{
				// Making sure the node ids are sequential
				graph.FixNodeIdNumbering();
				graph.PrintAdjListAndNodeLookup();
				int from = -1, to = -1;
				// Not all algo's need a starting/edging node specified
				if (selectedAlgoNameIdx == 8 || selectedAlgoNameIdx == 11)
				{
					using (var startEndNodeDialog = new StartEndNodeDialog(includeTo: false))
					{
						if (startEndNodeDialog.ShowDialog() == DialogResult.OK)
							from = startEndNodeDialog.From;
					}
					if (!graph.ContainsNode(from))
					{
						SimpleDialog.ShowMessage("Error", "Invalid start node id");
						return;
					}
					graph.DrawParticle(from, Colors.Green); // start node in green
				}
				else if (selectedAlgoNameIdx < 2 || selectedAlgoNameIdx == 9 || selectedAlgoNameIdx == 10)
				{
					using (var startEndNodeDialog = new StartEndNodeDialog())
					{
						if (startEndNodeDialog.ShowDialog() == DialogResult.OK)
						{
							from = startEndNodeDialog.From;
							to = startEndNodeDialog.To;
						}
					}
					if (!graph.ContainsNode(from) || !graph.ContainsNode(to))
					{
						SimpleDialog.ShowMessage("Error", "Invalid start/end node id(s)");
						return;
					}
					graph.DrawParticle(from, Colors.Green); // start node in green
					// If start is end sleep
					if (from == to) graph.Sleep(500);
					graph.DrawParticle(to, Colors.Red); // end node in red
				}
				// Unhighlight start/end node in case highlighted
				graph.Sleep(2500);
				graph.DrawGraph(DrawingMode.Forceless);
				RunAlgo(from, to);
			}
			else SimpleDialog.ShowMessage("Error, graph is empty!",
				"Algorithm did not run \nHint: Click \"Presets\" to load a preset or rightclick in canvas create a vertex");
		}
		private void RunAlgo(int from, int to)
		{
			switch (selectedAlgoNameIdx)
			{
				case 0:
					new BFS(graph, from, to);
					break;
				case 1:
					new DFS(graph, from, to);
					break;
				case 2:
					new ConnectedComponentsDFS(graph);
					break;
				case 3:
					Console.WriteLine("Number of components: " +
						new ConnectedComponentsDisjointSet(graph));
					break;
				case 4:
					new LazyPrimsMST(graph);
					break;
				case 5:
					new KruskalsMST(graph);
					break;
				case 6:
					int[] topSortDFS = new TopSortDFS(graph).TopOrder;
					if (topSortDFS != null)
					{
						foreach (int val in topSortDFS) Console.Write(val + " ");
						Console.WriteLine();
					}
					break;
				case 7:
					int[] topSortKahn = new KahnsTopSort(graph).TopOrder;
					if (topSortKahn != null)
					{
						foreach (int val in topSortKahn) Console.Write(val + " ");
						Console.WriteLine();
					}
					else Console.WriteLine("Cycle found in the graph");
					break;
				case 8:
					new DAGSSSP(graph, from);
					break;
				case 9:
					// Lazy version
					new LazyDijkstrasSSSP(graph, from, to);
					break;
				case 10:
					// Eager version
					new EagerDijkstrasSSSP(graph, from, to);
					break;
				case 11:
					new BellmanFords(graph, from);
					break;
				case 12:
					new TarjansSCCs(graph);
					break;
				case 13:
					new KosarajusSCCs(graph);
					break;
				default:
					Console.WriteLine($"{selectedAlgoNameIdx} is not a valid selection");
					break;
			}
		}

		private bool inVizMode = false;
		private void bgw_Visualize(object sender, DoWorkEventArgs e)
		{
			// The code to be executed by the background worker,
			// namely the selected graph algortihm

			((MainUIForm)parentForm).InVizMode = inVizMode = true;
			// Enable Pause/Resume and disable rest of the controls while visualizing
			Control[] controls = new Control[] { btnPauseResume, btnStart, btnReset, btnClearState, btnPresets, algoComboBox };
			foreach (Control control in controls) SetControlEnabled(control, control == btnPauseResume);
			((MainUIForm)parentForm).ToggleWindowResizeAndMainMenuBtns();

			// Running the visualization of the graph layout or the visualization for the graph algorithm
			// depending on the given arguemnt (true for algo viz, false for graph viz)
			if ((bool)e.Argument) RunAlgo();
			else graph.Visualize();

			// Disable Pause/Resume and enable rest of the controls after visualizing
			foreach (Control control in controls) SetControlEnabled(control, control != btnPauseResume);
			((MainUIForm)parentForm).ToggleWindowResizeAndMainMenuBtns();
			((MainUIForm)parentForm).InVizMode = inVizMode = false;

			// BUG - Redraw caused for some reason, suspecting: ToggleWindowResizeAndMainMenuBtns()
			graph.DrawGraph(DrawingMode.Forceless);
		}

		// Helper method & callback to update the Enabled prop of a given control
		private delegate void SetControlEnabledCallback(Control control, bool enabled);
		private void SetControlEnabled(Control control, bool enabled)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (control.InvokeRequired)
			{
				SetControlEnabledCallback d = new SetControlEnabledCallback(SetControlEnabled);
				Invoke(d, new object[] { control, enabled });
			}
			else control.Enabled = enabled;
		}

		#region Event handlers
		private void FDGVForm_Resize(object sender, EventArgs e)
		{
			// Check if width is > 0 in case window was minimized
			// Check required, otherwise all nodes may be placed at the point(0, 0)
			if (Width > 0)
			{
				// When resizing the canvas create new graphics object from it
				// and also make sure to update the canvas width/height for the graph
				// visualizer and to redraw the graph
				graph.CanvasHeight = canvas.Height;
				graph.CanvasWidth = canvas.Width;
				DrawGraph();
			}
		}
		private void btnStart_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Selected algo name: " + algoComboBox.Text);
			// Creating a new backgroundworker and running the graph algo on it in asynchronous mode
			bgw = new BackgroundWorker();
			//bgw.WorkerSupportsCancellation = true;
			bgw.DoWork += new DoWorkEventHandler(bgw_Visualize);
			bgw.RunWorkerAsync(argument: true);
		}
		private void btnReset_Click(object sender, EventArgs e)
		{
			string title = "Remove all vertices and edges",
				text = "You can save this graph as a new preset by clicking the button \"Presets\" and then \"New Preset\"\nPress OK to proceed.";
			if (!graph.IsEmpty() && SimpleDialog.OKCancel(title, text)) graph.ClearGraph();
		}
		private void btnClearState_Click(object sender, EventArgs e)
		{
			string title = "Reset colors and edge directions",
				text = "You are about to redraw graph at its initial configuration \nPress OK to proceed.";
			if (!graph.IsEmpty() && SimpleDialog.OKCancel(title, text))
			{
				// Reset graph color scheme and directions of edges to defaults
				graph.ClearGraphState();
				graph.DrawGraph(DrawingMode.Forceless);
			}
		}
		private void btnPresets_Click(object sender, EventArgs e)
		{
			using (var presetDialog = new PresetDialog(graph))
			{
				if (presetDialog.ShowDialog() == DialogResult.OK)
				{
					string[] serialization = presetDialog.Serialization;
					if (serialization != null)
					{
						graph.ClearGraph();
						GraphSerializer.Deserialize(graph, serialization);

						// Creating a new backgroundworker and running the graph viz on it in asynchronous mode
						bgw = new BackgroundWorker();
						bgw.DoWork += new DoWorkEventHandler(bgw_Visualize);
						bgw.RunWorkerAsync(argument: false);
					}
				}
			}
		}
		private void btnPauseResume_Click(object sender, EventArgs e)
		{
			// Note that this button is only enabled during a visualization!

			// Event handler for the Pause/Resume button, will pause or resume
			// the visualization depending on the current state of the visualization 
			if (graph.Paused)
			{
				btnPauseResume.Text = "Pause";
				graph.Resume();
			}
			else
			{
				btnPauseResume.Text = "Resume";
				graph.Pause();
			}
		}
		private void btnDetails_Click(object sender, EventArgs e)
		{
			string fileName = algorithmNames[selectedAlgoNameIdx] + ".xml",
				fileDir = @"Forms\Dialogs\AlgoDetails\xml\GraphTheory\" + fileName,
				filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
				.Parent.FullName, fileDir);
			new DetailsDialog(filePath).ShowDialog();
		}
		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{
			if (graph != null)
				graph.SetDelayFactor(speedBar.Value, speedBar.Minimum, speedBar.Maximum);
		}
		private void algoComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedAlgoNameIdx = algoComboBox.SelectedIndex;
		}
		private Vector activeRClickPos;
		private void togglePhysicsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Flag used when drawing draw to know if forces should be avoided
			// Does not include the method "graph.Visualize()" in the backgound worker
			forceDirectedDrawMode = !forceDirectedDrawMode;
		}
		private void toggleVertexPinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeParticleId != -1)
			{
				graph.ToggleParticlePin(activeParticleId);
				activeParticleId = -1;
			}
		}
		private void pinAllVerticesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			graph.PinAllParticles();
		}
		private void unpinAllVerticesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			graph.UnpinAllParticles();
		}
		private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var vertexDialog = new VertexDialog())
			{
				if (vertexDialog.ShowDialog() == DialogResult.OK)
				{
					int id = vertexDialog.Id, data = vertexDialog.Data;
					bool opStatus = false;
					// if id is negative then input is invalid or 
					if (id >= 0)
					{

						opStatus = graph.AddNode(id, data, activeRClickPos);
						activeRClickPos = null;
						Console.WriteLine("Adding new node (id: {0}, data: {1}), status: {2}", id, data, opStatus ? "Success" : "Failure");
					}
					else Console.WriteLine("Failed to add! negative node ids not prohibited!");
					if (opStatus) graph.Visualize();
				}
			}
		}
		private void removeVertexToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeParticleId != -1)
			{
				bool remStatus = graph.RemoveNode(activeParticleId);
				Console.WriteLine("Removing particle with id {0}, status: {1}",
					activeParticleId, remStatus ? "Success" : "Failure");
				if (remStatus) DrawGraph();
				activeParticleId = -1;
			}
		}
		private void addEdgeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var edgeDialog = new EdgeDialog(addingMode: true))
			{
				if (edgeDialog.ShowDialog() == DialogResult.OK)
				{
					int from = activeParticleId, to = edgeDialog.To, cost = edgeDialog.Cost;
					bool directedMode = edgeDialog.DirectedMode;
					// Add directed or undirected edge and get op status
					bool opStatus = directedMode ?
						graph.AddDirectedEdge(from, to, cost) :
						graph.AddUndirectedEdge(from, to, cost);
					// Custumize and print msg
					string msg = (directedMode ? "AddDirectedEdge" : "AddUndirectedEdge") +
							  $": ({from}, {to}, {cost}). " +
							  (opStatus ? "Success" : "Failed");
					Console.WriteLine(msg);
					activeParticleId = -1;
					if (opStatus) DrawGraph();
				}
			}
		}
		private void removeEdgeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var edgeDialog = new EdgeDialog(addingMode: false))
			{
				if (edgeDialog.ShowDialog() == DialogResult.OK)
				{
					int from = activeParticleId, to = edgeDialog.To, cost = edgeDialog.Cost;
					bool directedMode = edgeDialog.DirectedMode;
					// Remove directed or undirected edge and get op status
					bool opStatus = directedMode ?
						graph.RemoveDirectedEdge(from, to, cost) :
						graph.RemoveUndirectedEdge(from, to, cost);
					// Custumize and print msg
					string msg = (directedMode ? "RemoveDirectedEdge" : "RemoveUndirectedEdge") +
							  $": ({from}, {to}, {cost}). " +
							  (opStatus ? "Success" : "Failed");
					Console.WriteLine(msg);
					activeParticleId = -1;
					if (opStatus) DrawGraph();
				}
			}
		}
		private Particle activeParticle;
		private int activeParticleId = -1;
		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			// leftclick
			// Note that in case an algo is running then the click is ignored
			// thus prohibiting moving vertices during a vizsualization
			if (!inVizMode && e.Button == MouseButtons.Left)
			{
				int x = e.X, y = e.Y;
				Particle clickedParticle = graph.GetClickedParticle(x, y);
				if (clickedParticle != null) activeParticle = clickedParticle;
			}
		}
		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (activeParticle != null)
			{
				float x = e.X, y = e.Y;
				activeParticle.Pos = new Vector(x, y);
				DrawGraph();
			}
		}
		private void canvas_MouseUp(object sender, MouseEventArgs e)
		{
			// rightclick
			// Note that in case an algo is running then the click is ignored
			// thus prohibiting openning toolbars of the canvas
			if (!inVizMode && e.Button == MouseButtons.Right)
			{
				// Checking if clicked within a particle and if so diplaying vertexContextStrip
				float x = e.X, y = e.Y;
				Particle clickedParticle = graph.GetClickedParticle(x, y);
				// if within a particle
				if (clickedParticle != null)
				{
					activeParticleId = clickedParticle.Id;
					// show vertexContextStrip (remove vertex/add edge for current particle)
					vertexContextStrip.Show(Cursor.Position);
				}
				// otherwise if not within a particle
				else
				{
					// show canvasMainContextStrip (add vertex)
					canvasContextStrip.Show(Cursor.Position);
					activeRClickPos = new Vector(x, y);
				}
			}
			activeParticle = null;
		}
		#endregion

	}
}
