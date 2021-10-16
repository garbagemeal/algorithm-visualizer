using System;
using System.Drawing;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory.MazeGeneration;

namespace AlgorithmVisualizer.Forms
{
	public partial class MazeGenForm : Form
	{
		private PathFinder pathFinder;
		private Graphics g;
		private int delayTime = 100;
		private bool mazeFinished = false;
		private const int MAX_SPEED = 500;

		public MazeGenForm()
		{
			InitializeComponent();
		}

		private void drawMaze_Click(object sender, EventArgs e)
		{
			g = panelMain.CreateGraphics();
			// Fill panel in black
			g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, panelMain.Width, panelMain.Height);
			// Getting height/width from text boxes and bounding dimensions depending on panel's height and width
			const int CELL_WIDTH = 5, PATH_WIDTH = 3;
			// Computing maximal dimensios for maze depending on window size
			int maxHeight = panelMain.Height / (CELL_WIDTH * PATH_WIDTH) - 1;
			int maxWidth = panelMain.Width / (CELL_WIDTH * PATH_WIDTH) - 1;

			int height, width;
			try
			{
				// Getting user input & fixing dimensions to valid values if necessary
				height = Math.Max(1, Math.Min(maxHeight, Int32.Parse(heightTxtBox.Text)));
				width = Math.Max(1, Math.Min(maxWidth, Int32.Parse(widthTxtBox.Text)));
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to parse one or more of the inputs, defaulting dimensions to: " + maxHeight + " " + maxWidth);
				// Adjusting text boxes to match defaulted dimensions
				height = maxHeight;
				width = maxWidth;
			}
			heightTxtBox.Text = height + "";
			widthTxtBox.Text = width + "";
			// Maze instantiation
			pathFinder = new PathFinder(g, height, width, CELL_WIDTH, PATH_WIDTH, true, delayTime);
			
			// Start the visualization
			pathFinder.GenerateMaze();
			mazeFinished = true;
		}
		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{
			delayTime = Math.Abs(speedBar.Value - MAX_SPEED);
			Console.WriteLine("Setting delayTime to: " + delayTime);
		}
		private void btnFindPath_Click(object sender, EventArgs e)
		{
			if (mazeFinished)
			{
				pathFinder.RunPathFinder();
			}
		}
	}
}
