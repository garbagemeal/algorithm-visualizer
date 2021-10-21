using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

using AlgorithmVisualizer.Forms.Dialogs;
using AlgorithmVisualizer.Forms.Dialogs.AlgoDetails;
using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.Algorithms;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.MathUtils;
using static AlgorithmVisualizer.Forms.GraphAlgoSettings;

namespace AlgorithmVisualizer.Forms
{
	public partial class GraphAlgoForm : Form
	{
		private Graph graph;

		// Selected algo name index in settings.AlgoNames
		private int selectedAlgoIdx;
		// Stores the algo names and the what node ids are required per algo
		private GraphAlgoSettings settings;

		private Form parentForm;
		private Panel panelLog;
		private Graphics panelLogG;

		// Keeping track of active particles (mouse events)
		private Vector activeRClickPos;
		private Particle activeParticle;
		private int activeParticleId = -1;

		private bool inVizMode = false;
		private bool forcesEnabled = true;

		public GraphAlgoForm(Form _parentForm)
		{
			InitializeComponent();

			settings = new GraphAlgoSettings();
			// adding algo names to the combo box
			foreach (string algoName in settings.AlgoNames) algoComboBox.Items.Add(algoName);
			algoComboBox.SelectedIndex = selectedAlgoIdx = 0;

			parentForm = _parentForm;
			panelLog = ((MainUIForm)parentForm).PanelLog;
			panelLogG = panelLog.CreateGraphics();
			graph = new Graph(canvas, panelLogG);

			StartGraphViz();
		}

		#region Visualizing a graph (force directed graph drawing)
		private BackgroundWorker bgwGraphLayoutViz;

		void StartGraphViz()
		{
			if (bgwGraphLayoutViz != null) bgwGraphAlgoViz.Dispose();
			bgwGraphLayoutViz = new BackgroundWorker();
			// Assign work to bgw (a function) and run async (supports cancelation)
			bgwGraphLayoutViz.WorkerSupportsCancellation = true;
			bgwGraphLayoutViz.DoWork += new DoWorkEventHandler(bgw_GraphViz);
			bgwGraphLayoutViz.RunWorkerAsync();
		}
		// Refers to the state of the parent form (minimized or not)
		private bool formIsMinimized = false;
		private void bgw_GraphViz(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				if (!formIsMinimized && !graph.IsEmpty())
				{
					if (forcesEnabled) graph.ApplyForcesAndUpdatePositions();
					TriggerCanvasPaintEvent();
				}
			}
		}

		// Safely invoke "canvas.Refresh()"
		private delegate void TriggerCanvasPaintEventCallback();
		private void TriggerCanvasPaintEvent()
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (canvas.InvokeRequired)
			{
				var d = new TriggerCanvasPaintEventCallback(TriggerCanvasPaintEvent);
				canvas.Invoke(d, new object[] { });
			}
			else canvas.Refresh();
		}
		#endregion

		#region Visualizing a graph algorithm
		private BackgroundWorker bgwGraphAlgoViz;

		private void VisualizeGraphAlgo()
		{
			// TODO: bgw.WorkerSupportsCancellation = true;

			int nodeCount = graph.NodeCount;
			if (nodeCount > 0)
			{
				if (bgwGraphAlgoViz != null) bgwGraphAlgoViz.Dispose();
				// Create new backgroundworker
				bgwGraphAlgoViz = new BackgroundWorker();
				// Assign work to bgw (a function) and run async
				bgwGraphAlgoViz.DoWork += new DoWorkEventHandler(bgw_VisualizeGraphAlgo);
				bgwGraphAlgoViz.RunWorkerAsync();
			}
			else SimpleDialog.ShowMessage("Graph is empty!", "Algorithm did not run \n" +
				"Hint: Click \"Presets\" to load a preset or rightclick in canvas create a vertex");
		}
		public bool GetStartAndEndNodes(ref int from, ref int to)
		{
			// Ensure node ids are sequential
			graph.FixNodeIdNumbering();
			RequiredNodes reqNodes = settings.RequiredNodeIds[selectedAlgoIdx];
			if (reqNodes == RequiredNodes.None) return true; // input unneeded
			// Get start/end nodes
			bool includeTo = reqNodes == RequiredNodes.StartAndEnd;
			using (var startEndNodeDialog = new StartEndNodeDialog(graph, includeTo))
			{
				startEndNodeDialog.StartPosition = FormStartPosition.CenterParent;
				// if 'startEndNodeDialog' is closed and user input is valid
				if (startEndNodeDialog.ShowDialog() == DialogResult.OK && startEndNodeDialog.InputIsValid)
				{
					// set user input to given int refs (pointers)
					from = startEndNodeDialog.From;
					to = startEndNodeDialog.To;
					MarkNodes(from, to, includeTo);
					UnmarkNodes(from, to);
					return true; // valid input given
				}
			}
			return false; // invalid input given
		}
		void MarkNodes(int from, int to, bool includeTo)
		{
			graph.MarkParticle(from, Colors.Green); // start node
			if (includeTo)
			{
				if (from == to) graph.Sleep(500);
				graph.MarkParticle(to, Colors.Red); // end node
			}
			// BUG: unsure why canvas paint trigger is required, doesn't
			// 'bgwGraphLayoutViz' already trigger the same event?
			TriggerCanvasPaintEvent();
		}
		void UnmarkNodes(int from, int to)
		{
			graph.Sleep(1500);
			foreach (int id in new int[] { from, to })
				if (id != -1) graph.ResetParticleColors(id);
			// Again unsure why triggering is needed if bgwGraphLayoutViz should already do so?
			TriggerCanvasPaintEvent();
		}
		private void RunAlgo(int from, int to)
		{
			switch (selectedAlgoIdx)
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
					Console.WriteLine($"{selectedAlgoIdx} is not a valid selection");
					break;
			}
		}
		private void bgw_VisualizeGraphAlgo(object sender, DoWorkEventArgs e)
		{
			// Controls to disable while visualizing (not including parent form)
			Control[] controls = new Control[] { btnPauseResume, btnStart, btnReset, btnClearState, btnPresets, algoComboBox };
			// Sync parent to prevent log panel resize 
			((MainUIForm)parentForm).InVizMode = inVizMode = true;
			((MainUIForm)parentForm).ToggleWindowResizeAndMainMenuBtns();
			// Enable Pause/Resume and disable the rest of the controls before visualizing
			foreach (Control control in controls) SetControlEnabled(control, control == btnPauseResume);

			int from = -1, to = -1;
			// Visualize graph algo on valid input (input may not be required)
			if (GetStartAndEndNodes(ref from, ref to)) RunAlgo(from, to);

			// Disable Pause/Resume and re-enable the rest of the controls after visualizing
			foreach (Control control in controls) SetControlEnabled(control, control != btnPauseResume);
			((MainUIForm)parentForm).ToggleWindowResizeAndMainMenuBtns();
			((MainUIForm)parentForm).InVizMode = inVizMode = false;
		}
		// Safely access control.Enabled, i.e: btnStart.Enabled
		private delegate void SetControlEnabledCallback(Control control, bool enabled);
		private void SetControlEnabled(Control control, bool enabled)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (control.InvokeRequired)
			{
				var d = new SetControlEnabledCallback(SetControlEnabled);
				Invoke(d, new object[] { control, enabled });
			}
			else control.Enabled = enabled;
		}
		#endregion

		#region Event handlers
		private void btnStart_Click(object sender, EventArgs e)
		{
			VisualizeGraphAlgo();
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
		private void btnReset_Click(object sender, EventArgs e)
		{
			string title = "Clear the graph",
				text = "You are about to remove all vertices and edges\n press OK to proceed.";
			if (!graph.IsEmpty() && SimpleDialog.OKCancel(title, text)) graph.ClearGraph();
		}
		private void btnClearState_Click(object sender, EventArgs e)
		{
			string title = "Reset colors and edge directions",
				text = "You are about to reset vertex/edge colors to defaults and also unreverse all reversed edged.\n Press OK to proceed.";
			if (!graph.IsEmpty() && SimpleDialog.OKCancel(title, text)) graph.ClearVizState();
		}
		private void btnPresets_Click(object sender, EventArgs e)
		{
			using (var presetDialog = new PresetDialog(graph))
			{
				presetDialog.StartPosition = FormStartPosition.CenterParent;
				presetDialog.ShowDialog();
			}
		}
		private void btnDetails_Click(object sender, EventArgs e)
		{
			string fileName = settings.AlgoNames[selectedAlgoIdx] + ".xml",
				fileDir = @"Forms\Dialogs\AlgoDetails\xml\GraphTheory\" + fileName,
				filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
				.Parent.FullName, fileDir);
			using(Form dialog = new DetailsDialog(filePath))
			{
				dialog.StartPosition = FormStartPosition.CenterParent;
				dialog.ShowDialog();
			}
		}
		private FDGVConfigForm configForm = null;
		private void btnPreferences_Click(object sender, EventArgs e)
		{
			// If form was already opened make sure to close 
			if (configForm != null)
			{
				configForm.Close();
				configForm.Dispose(); // not sure if I should dispose the form
			}
			configForm = new FDGVConfigForm(graph);
			configForm.Show();
		}

		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{
			if (graph != null)
				graph.SetDelayFactor(speedBar.Value, speedBar.Minimum, speedBar.Maximum);
		}
		private void algoComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedAlgoIdx = algoComboBox.SelectedIndex;
		}
		
		private void togglePhysicsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Toggle physics for graph drawing
			forcesEnabled = !forcesEnabled;
			Console.WriteLine($"Physics toggled " + (forcesEnabled ? "on" : "off"));
		}
		private void toggleCenterPullToolStripMenuItem_Click(object sender, EventArgs e)
		{
			graph.CenterPull = !graph.CenterPull;
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
			if (inVizMode) return; // do nothing if currently visualizing somthing
			using (var vertexDialog = new VertexDialog())
			{
				vertexDialog.StartPosition = FormStartPosition.CenterParent;
				if (vertexDialog.ShowDialog() == DialogResult.OK)
				{
					int id = vertexDialog.Id;
					// if id is negative then input is invalid
					if (id >= 0)
					{
						bool opStatus = graph.AddNode(id, activeRClickPos);
						activeRClickPos = null;
						Console.WriteLine("Adding new node with id {0}, status: {1}", id, opStatus ? "Success" : "Failure");
					}
					else Console.WriteLine("Failed to add! negative node ids not prohibited!");
				}
			}
		}
		private void removeVertexToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (inVizMode) return; // do nothing if currently visualizing somthing
			if (activeParticleId != -1)
			{
				bool remStatus = graph.RemoveNode(activeParticleId);
				Console.WriteLine("Removing particle with id {0}, status: {1}",
					activeParticleId, remStatus ? "Success" : "Failure");
				activeParticleId = -1;
			}
		}
		private void addEdgeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (inVizMode) return; // do nothing if currently visualizing somthing
			using (var edgeDialog = new EdgeDialog(addingMode: true))
			{
				edgeDialog.StartPosition = FormStartPosition.CenterParent;
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
				}
			}
		}
		private void removeEdgeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (inVizMode) return; // do nothing if currently visualizing somthing
			using (var edgeDialog = new EdgeDialog(addingMode: false))
			{
				edgeDialog.StartPosition = FormStartPosition.CenterParent;
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
				}
			}
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graph.DrawGraph(e.Graphics);
		}
		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			// leftclick
			// Removing this flag allows moving nodes when visualizing, may cause issues.
			if (/*!inVizMode && */e.Button == MouseButtons.Left)
			{
				int x = e.X, y = e.Y;
				Particle clickedParticle = graph.GetClickedParticle(x, y);
				if (clickedParticle != null)
				{
					activeParticle = clickedParticle;
					activeParticle.Pinned = true;
				}
			}
		}
		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (activeParticle != null) activeParticle.Pos = new Vector(e.X, e.Y);
		}
		private void canvas_MouseUp(object sender, MouseEventArgs e)
		{
			// Unpin particle for leftclick
			if (e.Button == MouseButtons.Left) if (activeParticle != null) activeParticle.Pinned = false;
			// Removing this flag allows opening context menus when visualizing, may cause issues.
			if (/*!inVizMode && */e.Button == MouseButtons.Right)
			{
				// Find location of click release and get clicked particle
				float x = e.X, y = e.Y;
				Particle clickedParticle = graph.GetClickedParticle(x, y);
				
				// clicked on particle particle
				if (clickedParticle != null)
				{
					// show vertexContextStrip (remove vertex/add edge for current particle)
					activeParticleId = clickedParticle.Id;
					vertexContextStrip.Show(Cursor.Position);
				}
				else
				{
					// show canvasMainContextStrip (add vertex)
					canvasContextStrip.Show(Cursor.Position);
					activeRClickPos = new Vector(x, y);
				}
			}
			activeParticle = null;
		}
		private void FDGVForm_Resize(object sender, EventArgs e)
		{
			// Check if width is > 0 in case window was minimized
			// Check required, otherwise all nodes may be placed at the point(0, 0)
			if (Width > 0)
			{
				// Update canvas height/width for stoed in graph
				graph.CanvasHeight = canvas.Height;
				graph.CanvasWidth = canvas.Width;
			}
			// Detect when the form is minimized to stop visualizing the graph. Note that
			// the parent the one minimized and not this form.
			if (ParentForm != null) formIsMinimized = ParentForm.WindowState == FormWindowState.Minimized;
		}
		#endregion
	}
}
