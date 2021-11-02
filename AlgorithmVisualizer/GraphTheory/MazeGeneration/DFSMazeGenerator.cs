using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace AlgorithmVisualizer.GraphTheory.MazeGeneration
{
	public class DFSMazeGenerator : MazeVisualizer
	{
		// Algorithm dependant stuff
		public int MazeHeight { get; }
		public int MazeWidth { get; }
		protected readonly Cell[,] maze;
		private readonly Random rnd = new Random();
		private readonly bool RANDOMIZED;
		protected readonly int startRow, startCol;
		public bool IsDrawn { get; private set; } = false;

		// Construction and maze initialization
		public DFSMazeGenerator(Graphics g, int mazeHeight, int mazeWidth, int cellWidth, int pathWidth, bool rndFlag)
			: base(g, cellWidth, pathWidth)
		{
			// Init values for maze array, visualization parameters, ...
			if (mazeHeight <= 0 || mazeWidth <= 0)
				throw new ArgumentException("Maze dimensions must be > 0");
			MazeHeight = mazeHeight;
			MazeWidth = mazeWidth;
			RANDOMIZED = rndFlag;
			// Pick random starting point
			startRow = rnd.Next(MazeHeight);
			startCol = rnd.Next(MazeWidth);
			// Define maze
			maze = new Cell[MazeHeight, MazeWidth];
		}
		// Initialization functions
		private void InitializeMaze()
		{
			// initialize maze and draw each cell in blue
			for (int r = 0; r < MazeHeight; r++)
			{
				for (int c = 0; c < MazeWidth; c++)
				{
					maze[r, c] = new Cell(r, c);
					DrawCellIgnoreConnection(maze[r, c], Brushes.Blue);
				}
			}
		}

		public void GenerateMaze()
		{
			// Init maze
			InitializeMaze();
			// Path used for backtracking, visited to avoid loops
			Stack<Cell> path = new Stack<Cell>();
			HashSet<Cell> visited = new HashSet<Cell>();
			// Push starting cell into the path, mark the cell as visited, set countVisited to 1
			path.Push(maze[startRow, startCol]);
			visited.Add(maze[startRow, startCol]);
			int countVisited = 1;
			// Visualize starting position, wait 1 second
			DrawCellIgnoreConnection(maze[startRow, startCol], Brushes.White);
			Sleep(Delay.Medium);
			// Perform DFS
			DFS(path, visited, countVisited);
		}

		// Iterative DFS
		private void DFS(Stack<Cell> path, HashSet<Cell> visited, int countVisited)
		{
			// Maze DFS
			Console.WriteLine("Starting DFS at: ({0}, {1})", startRow, startCol);
			while (!(path.Count == 0) && countVisited < MazeHeight * MazeWidth)
			{
				// Peek at current cell's coords
				Cell curCell = path.Peek();
				// Visualize stack top
				DrawCellIgnoreConnection(curCell, Brushes.Red);
				// Try to pick an adjacent cell
				Cell adjUnvisitedCell = PickAdjCell(curCell, visited);
				Sleep(Delay.Short);
				// if adjUnvisitedCell is null, there is no adjacent cell to visit
				if (adjUnvisitedCell != null)
				{
					Console.WriteLine("Visiting: " + adjUnvisitedCell.GetCoordsAsString());
					// add adjUnvisitedCell's coordiantes to the path stack and mark the coords as visited
					path.Push(adjUnvisitedCell);
					visited.Add(adjUnvisitedCell);
					// link current node with w
					LinkCells(curCell, adjUnvisitedCell);
					countVisited++;
					// visualize the newly visited cell
					DrawCellWithConnection(adjUnvisitedCell, Brushes.White);
				}
				else
				{
					Console.WriteLine("Backtacking from: " + curCell);
					path.Pop(); // Backtrack
				}
				// "Unvisualize" stack top
				DrawCellIgnoreConnection(curCell, Brushes.White);
			}
			IsDrawn = true;
		}

		// Cell linking
		private void LinkCells(Cell cell, Cell adjUnvisitedCell)
		{
			// Note that adjUnvisitedCell.PrevIdx yeilds a value represention the direction
			// from adjUnvisitedCell to cell! the following formula can be used to reverse the direction:
			// (direction + 2) % 4

			// Link current cell (referenced by cell) to the next cell (referenced by adjUnvisitedCell)
			// Note that adjUnvisitedCell's PrevIdx is reversed!
			maze[cell.R, cell.C].adj[(adjUnvisitedCell.PrevIdx + 2) % 4] = adjUnvisitedCell;
			// Link adjUnvisitedCell cell to current cell depending on adjUnvisitedCell's PrevIdx value
			adjUnvisitedCell.adj[adjUnvisitedCell.PrevIdx] = maze[cell.R, cell.C];
		}

		#region DFS Decision making
		private Cell PickAdjCell(Cell cell, HashSet<Cell> visited)
		{
			return RANDOMIZED ? PickAdjCellRandomized(cell, visited) : PickAdjCellNonRandomized(cell, visited);
		}
		private Cell PickAdjCellNonRandomized(Cell cell, HashSet<Cell> visited)
		{
			// Each colunm shared by rr and cc denotes a position vector such that:
			// by adding it to a given cell's position vector(coordinates)
			// we get the position vector of an adjacent cell
			int[] rr = { -1, +0, +1, +0 },
				  cc = { +0, +1, +0, -1 };

			// col corresponds to a side(direction):
			// -1 - None
			//  0 - N - top
			//  1 - E - right
			//  2 - S - bot
			//  3 - W - left
			for (int col = 0; col < 4; col++)
			{
				// If current cell's coordinates are in bounds, the cell is not visited
				if (BoundCheck(cell.R + rr[col], cell.C + cc[col]) &&
				!visited.Contains(maze[cell.R + rr[col], cell.C + cc[col]]))
				{
					// Note that the direction is reversed using (direction + 2) % 4
					// col denotes the direction from curNode to the current adjacent unvisited cell,
					// the reversed direction is stored!
					maze[cell.R + rr[col], cell.C + cc[col]].PrevIdx = (col + 2) % 4;
					return maze[cell.R + rr[col], cell.C + cc[col]];
				}
			}
			return null;
		}
		private Cell PickAdjCellRandomized(Cell cell, HashSet<Cell> visited)
		{
			// A list to contain all adjacent unvisited cells
			List<Cell> adjUnvisitedCellList = new List<Cell>();
			// Each colunm shared by rr and cc denotes a position vector such that:
			// by adding it to a given cell's position vector(coordinates)
			// we get the position vector of an adjacent cell
			int[] rr = { -1, +0, +1, +0 },
				  cc = { +0, +1, +0, -1 };

			// col corresponds to a side(direction):
			// -1 - None
			//  0 - N - top
			//  1 - E - right
			//  2 - S - bot
			//  3 - W - left
			for (int col = 0; col < 4; col++)
			{
				// If current cell's coordinates are in bounds, the cell is not visited
				if (BoundCheck(cell.R + rr[col], cell.C + cc[col]) &&
				!visited.Contains(maze[cell.R + rr[col], cell.C + cc[col]]))
				{
					adjUnvisitedCellList.Add(maze[cell.R + rr[col], cell.C + cc[col]]);
					// Note that the direction is reversed using (direction + 2) % 4
					// col denotes the direction from curNode to the current adjacent unvisited cell,
					// the reversed direction is stored!
					adjUnvisitedCellList.Last().PrevIdx = (col + 2) % 4;
				}
			}
			return adjUnvisitedCellList.Count > 0 ? adjUnvisitedCellList[rnd.Next(adjUnvisitedCellList.Count)] : null;
		}
		#endregion

		// Bound check of given coordinates
		private bool BoundCheck(int r, int c)
		{
			return r >= 0 && c >= 0 && r < MazeHeight && c < MazeWidth;
		}

		// Debugging functions
		public void PrintMazeToDebugCon()
		{
			Console.WriteLine("");
			bool firstRowNotPrinted = true;
			for (int r = 0; r < MazeHeight; r++)
			{
				for (int c = 0; c < MazeWidth; c++)
				{
					// first row consists of a space followed by 2n - 1 underscores followed by another space
					// rest of the rows 
					if (firstRowNotPrinted)
					{
						if (c == 0) Console.Write(" ");
						// prints 2 underscores except last iteration
						Console.Write(c < MazeWidth - 1 ? "__" : "_");
						if (c == MazeWidth - 1) Console.Write(" ");
					}
					else
					{
						// first col
						if (c == 0) Console.Write("|");

						// if has bottom connection print " " else "_"
						Console.Write(maze[r, c].adj[2] != null ? " " : "_");

						// if has right connection print "_" else "|"
						Console.Write(maze[r, c].adj[1] != null ? "_" : "|");
					}
				}
				Console.WriteLine("");
				if (firstRowNotPrinted)
				{
					firstRowNotPrinted = false;
					r--;
				}
			}
		}
		public void DebugConPrint()
		{
			for (int r = 0; r < MazeHeight; r++)
			{
				Console.Write("{ ");
				for (int c = 0; c < MazeWidth; c++)
					Console.Write(maze[r, c] + ", ");
				Console.WriteLine(" }");
			}
		}
	}
}