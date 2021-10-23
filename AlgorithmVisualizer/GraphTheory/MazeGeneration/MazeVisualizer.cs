using System;
using System.Drawing;

using AlgorithmVisualizer.Threading;

namespace AlgorithmVisualizer.GraphTheory.MazeGeneration
{
	public class MazeVisualizer : PauseResumeSleep
	{
		protected Graphics g;
		protected int cellWidth, pathWidth;
		public MazeVisualizer(Graphics _g, int _cellWidth, int _pathWith)
		{
			if (_cellWidth <= 0 || _pathWith <= 0)
				throw new ArgumentException("Both cell and path width must be > 0");
			g = _g;
			cellWidth = _cellWidth;
			pathWidth = _pathWith;
		}

		// Drawing functions (visualization)
		protected void DrawCellWithConnection(Cell cell, Brush brush)
		{
			// cell is a newly selected neightboring cell (obtained from the CellAndDir wrapper)
			// dirFrom - direction from prev node to current node
			// 0 - top, 1 - bot, 2 - left, 3 - right

			// Define cell rect for newly picked cell
			Rectangle rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth);

			// draw white cell
			g.FillRectangle(brush, rect);

			// pick rectangle such that by printing it we create a connection with respect to directionToPrev
			switch (cell.PrevIdx)
			{
				// top
				case 0:
					rect =  new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth, 2 * cellWidth, 2 * cellWidth);
					break;
				// right
				case 1:
					rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth + cellWidth, 2 * cellWidth);
					break;
				// bot
				case 2:
					rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth + cellWidth);
					break;
				// left
				case 3:
					rect = new Rectangle(cell.C * pathWidth * cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth);
					break;
				// Any other case including -1!
				default:
					rect =  new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth);
					break;
			}

			// Print the rectangle to connect current cell with previous cell
			g.FillRectangle(brush, rect);
		}
		protected void DrawCellWithConnection(Cell cell, int prevDir, Brush brush)
		{
			// cell is a newly selected neightboring cell (obtained from the CellAndDir wrapper)
			// dirFrom - direction from prev node to current node
			// 0 - top, 1 - bot, 2 - left, 3 - right

			// Define cell rect for newly picked cell
			Rectangle rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth);

			// draw white cell
			g.FillRectangle(brush, rect);

			// pick rectangle such that by printing it we create a connection with respect to directionToPrev
			switch (prevDir)
			{
				// top
				case 0:
					rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth, 2 * cellWidth, 2 * cellWidth);
					break;
				// right
				case 1:
					rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth + cellWidth, 2 * cellWidth);
					break;
				// bot
				case 2:
					rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth + cellWidth);
					break;
				// left
				case 3:
					rect = new Rectangle(cell.C * pathWidth * cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth);
					break;
				// Any other case including -1!
				default:
					rect = new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth);
					break;
			}

			// Print the rectangle to connect current cell with previous cell
			g.FillRectangle(brush, rect);
		}
		protected void DrawCellIgnoreConnection(Cell cell, Brush brush)
		{
			g.FillRectangle(brush, new Rectangle(cell.C * pathWidth * cellWidth + cellWidth, cell.R * pathWidth * cellWidth + cellWidth, 2 * cellWidth, 2 * cellWidth));
		}
	}
}
