using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory.MazeGeneration;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.Forms
{
	public partial class MazeGenForm : Form
	{
		private readonly MainUIForm parentForm;

		private MazeSolver mazeVisualizer;
		private Graphics g;
		const int CELL_WIDTH = 5, PATH_WIDTH = 3;

		private BackgroundWorker bgw;

		public MazeGenForm(MainUIForm _parentForm)
		{
			InitializeComponent();
			parentForm = _parentForm;
		}

		private void RunBGW()
		{
			bgw = new BackgroundWorker();
			bgw.DoWork += new DoWorkEventHandler(bgw_Visualize);
			bgw.RunWorkerAsync();
		}
		private void bgw_Visualize(object sender, DoWorkEventArgs e)
		{
			var worker = sender as BackgroundWorker; 

			// Enable Pause/Resume and disable btnDrawSolveClearMaze while visualizing
			Control[] controls = new Control[] { btnPauseResume, btnDrawSolveClearMaze };
			parentForm.inGraphAlgoViz = true;
			parentForm.ToggleMainMenuBtns();
			foreach (Control control in controls) SetControlEnabled(control, control == btnPauseResume);

			// generate/solve maze
			if (!mazeVisualizer.IsDrawn) mazeVisualizer.GenerateMaze();
			else mazeVisualizer.Solve();

			// Disable Pause/Resume and enable btnDrawSolveClearMaze after visualizing
			foreach (Control control in controls) SetControlEnabled(control, control != btnPauseResume);
			parentForm.ToggleMainMenuBtns();
			parentForm.inGraphAlgoViz = false;
			if (worker.CancellationPending) e.Cancel = true;
		}

		// Thread safe method to update a control's enabled state
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

		private void btnDrawSolveClearMaze_Click(object sender, EventArgs e)
		{
			if (mazeVisualizer == null)
			{
				// Init maze visualizer and generate maze
				g = panelMain.CreateGraphics();
				GetMazeSize(out int height, out int width);
				mazeVisualizer = new MazeSolver(g, height, width, CELL_WIDTH, PATH_WIDTH, true);
				UpdateDelayFactor();
				btnDrawSolveClearMaze.Text = "Find SP";
				RunBGW();
			}
			else if (mazeVisualizer.IsDrawn && !mazeVisualizer.IsSolved)
			{
				// Find SP in maze
				btnDrawSolveClearMaze.Text = "Clear";
				RunBGW();
			}
			else // mazeVisualizer.IsDrawn == mazeVisualizer.IsSolved == true
			{
				// Clear the maze (panel), cycles to inital state (drawing new maze)
				mazeVisualizer = null;
				g.Clear(Colors.Undraw);
				btnDrawSolveClearMaze.Text = "Create Maze";
			}
		}

		private void GetMazeSize(out int height, out int width)
		{
			// Get width/height for maze from text boxes
			int maxHeight = panelMain.Height / (CELL_WIDTH * PATH_WIDTH) - 1;
			int maxWidth = panelMain.Width / (CELL_WIDTH * PATH_WIDTH) - 1;
			try
			{
				height = Math.Max(2, Math.Min(maxHeight, Int32.Parse(heightTxtBox.Text)));
				width = Math.Max(2, Math.Min(maxWidth, Int32.Parse(widthTxtBox.Text)));
			}
			catch (FormatException)
			{
				height = maxHeight;
				width = maxWidth;
			}
			heightTxtBox.Text = height.ToString();
			widthTxtBox.Text = width.ToString();
		}

		private void UpdateDelayFactor() => mazeVisualizer.DelayFactor = speedBar.Value;

		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{
			if (mazeVisualizer != null) UpdateDelayFactor();
		}
		private void btnPauseResume_Click(object sender, EventArgs e)
		{
			// Note that this button is only enabled during a visualization!

			// Event handler for the Pause/Resume button, will pause or resume
			// the visualization depending on the current state of the visualization 
			if (mazeVisualizer.Paused)
			{
				btnPauseResume.Text = "Pause";
				mazeVisualizer.Resume();
			}
			else
			{
				btnPauseResume.Text = "Resume";
				mazeVisualizer.Pause();
			}
		}
	}
}
