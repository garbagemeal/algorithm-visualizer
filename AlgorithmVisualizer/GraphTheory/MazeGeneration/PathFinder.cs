﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace AlgorithmVisualizer.GraphTheory.MazeGeneration
{
	public class PathFinder : RecursiveBacktracker
	{
		private int endRow = -1, endCol = -1;
		private Random rnd = new Random();
		private readonly Brush greenBrush = new SolidBrush(Color.Green);
		private readonly Brush yellowBrush = new SolidBrush(Color.Yellow);

		public PathFinder(Graphics g, int mazeHeight, int mazeWidth, int cellWidth, int pathWidth, bool rndFlag, int delayTime) :
			base(g, mazeHeight, mazeWidth, cellWidth, pathWidth, rndFlag, delayTime)
		{
			//g = _g;
			//recursiveBacktracker = _recursiveBacktacker;
			//mazeVisualizer = recursiveBacktracker.MazeVisualizer;
		}

		private void PickRndEndPos()
		{
			// Pick random ending point for the BFS, maze sure its not -1 or the starting point
			//while(endRow == -1 || endRow == startRow)
			//	endRow = rnd.Next(MAZE_HEIGHT);
			do endRow = rnd.Next(MAZE_HEIGHT); while (endRow == startRow);
			//while (endCol == -1 || endCol == startCol)
			//	endCol = rnd.Next(MAZE_WIDTH);
			do endCol = rnd.Next(MAZE_WIDTH); while (endCol == startCol);
		}
		public void RunPathFinder()
		{
			PickRndEndPos();
			// BFS to find the shotest path in the maze (grid)
			Cell startingCell = maze[startRow, startCol];
			Cell endingCell = maze[endRow, endCol];
			// Mark starting & ending pos
			DrawCellIgnoreConnection(startingCell, greenBrush);
			DrawCellIgnoreConnection(endingCell, redBrush);
			Debug.WriteLine("Starting BFS at ({1}, {0}), ending at ({3}, {2}),",
				startingCell.R, startingCell.C, endingCell.R, endingCell.C);
			Thread.Sleep(1000);

			// q for BFS, visited to avoid revisiting cells
			Queue<Cell> q = new Queue<Cell>();
			HashSet<Cell> visited = new HashSet<Cell>();
			// q initially holds the starting cell, coming for the direction -1 (nowhere)
			q.Enqueue(startingCell);
			// Visited initially contains the starting cell
			visited.Add(startingCell);

			// prevDict is a Dictionary that maps for each Cell(key) its previous cell(value)
			// distDict is a Dictionary that maps for each Cell(key) the current ditance required to reach it
			Dictionary<Cell, Cell> prevDict = new Dictionary<Cell, Cell>();
			Dictionary<Cell, int> distDict = new Dictionary<Cell, int>();
			prevDict[startingCell] = null; // starting cell reached by null(nothing)
			distDict[startingCell] = 0; // starting cell's ditance is 0

			BFS(startingCell, endingCell, q, visited, prevDict, distDict);

			Thread.Sleep(5000);
			FindPath(startingCell, endingCell, visited, prevDict);
		}
		private void BFS(Cell startingCell, Cell endingCell, Queue<Cell> q,
			HashSet<Cell> visited, Dictionary<Cell, Cell> prevDict, Dictionary<Cell, int> distDict)
		{
			// Starting cell may never be the ending cell! assume end has no been reached
			bool endingCellReached = false;
			// As long as the queue is not empty and the end has not been reached
			while (q.Count > 0 && !endingCellReached)
			{
				Cell curCell = q.Dequeue();
				// If not the starting cell, visualize in blue
				if (curCell != startingCell) DrawCellWithConnection(curCell, blueBrush);
				// If curCell is the endingCell, visualize the cell in red and finish BFS
				if (curCell == endingCell)
				{
					// Need to draw in red again becuase ending cell is currentley visualized in blue
					DrawCellIgnoreConnection(curCell, redBrush);
					endingCellReached = true;
				}
				else
				{
					// Visit 4 adjacent cells (if non-null)
					for (int i = 0; i < 4; i++)
					{
						Cell adjCell = maze[curCell.R, curCell.C].adj[i];
						// The adjacent cell exists and was not visited
						if (adjCell != null && !visited.Contains(adjCell))
						{
							// Enqueue the adjacent cell and mark as visited
							q.Enqueue(adjCell);
							visited.Add(adjCell);
							// If the key adjCell in not present in the prevDict, map it to curCell
							if (!prevDict.ContainsKey(adjCell)) prevDict[adjCell] = curCell;
							// If the key adjCell in not present in the distDict OR it is present but
							// a shorter distance was found, create/update the mapping
							if (!distDict.ContainsKey(adjCell) || distDict[adjCell] < distDict[curCell] + 1)
								distDict[adjCell] = distDict[curCell] + 1;
						}
					}
				}
				Thread.Sleep(50);
			}
		}
		private void FindPath(Cell startingCell, Cell endingCell, HashSet<Cell> visited,
			Dictionary<Cell, Cell> prevDict)
		{
			// Highlight the shortest path, no need to check if a path exists because we know it does.
			// Also record shotest path into shortestPath list (in case it will be necessary later)
			List<Cell> shortestPath = new List<Cell>();
			int dirToPrev = -1;
			for (Cell at = endingCell; at != null; at = prevDict[at])
			{
				shortestPath.Add(at);
				if (at == startingCell)
				{
					DrawCellWithConnection(at, dirToPrev, yellowBrush);
					DrawCellIgnoreConnection(at, greenBrush);
				}
				else if (at != endingCell) DrawCellWithConnection(at, dirToPrev, yellowBrush);
				// determite next cell's direction with respect to current cell(at)
				if (prevDict[at] != null)
					for (int i = 0; i < 4; i++)
						if (prevDict[at].adj[i] == at) dirToPrev = i;
				Thread.Sleep(50);
			}

			shortestPath.Reverse();

			Thread.Sleep(1000);
			// Removing all blue colored cells that are not in the shotest path in other words:
			// Removing each cell that is in the visited HashSet but not in shortestPath List.
			// Removing means coloring a cell in white
			foreach (Cell cell in visited)
			{
				if (!shortestPath.Contains(cell))
					DrawCellWithConnection(cell, Cell1DirToCell2(cell, prevDict[cell]), whiteBrush);
			}
		}

		private int Cell1DirToCell2(Cell c1, Cell c2)
		{
			// get an int representing the direction from c1 to c2, assumes c1, c2
			// are not null. if c1 and c2 are not adjacent will return -1
			for(int adjIdx = 0; adjIdx < 4; adjIdx++) if (c1.adj[adjIdx] == c2) return adjIdx;
			return -1; // c1 and c2 and not adjacent
		}
	}
}
