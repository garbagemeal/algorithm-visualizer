namespace AlgorithmVisualizer.GraphTheory.MazeGeneration
{
	public class Cell
	{
		/* neighboring cell array: 
		 * adj[0] - (N)orth - top
		 * adj[1] - (E)ast - right
		 * adj[2] - (S)outh - bottom
		 * adj[3] - (W)est - left */
		public Cell[] adj = new Cell[4];
		private int r, c;
		public int R { get { return r; } }
		public int C { get { return c; } }
		// The DFS predecessor's idx in the adj array that also indicates the direction to the DFS predecessor
		// Note: may be -1 indicating the absence of a predecessor. in other words this cell is the starting cell!
		private int prevIdx;
		public int PrevIdx { get { return prevIdx; } set { prevIdx = value; } }

		public Cell(int _r, int _c)
		{
			r = _r;
			c = _c;
			prevIdx = -1;
		}

		public int HasSide(int side)
		{
			return adj[side] != null ? 1 : 0;
		}
		public override string ToString()
		{
			return string.Format("N: {0}, E: {1}, S: {2}, W: {3}, Coords: ({4}, {5})", HasSide(0), HasSide(1), HasSide(2), HasSide(3), R, C);
		}
		public string GetCoordsAsString()
		{
			return string.Format("({0}, {1})", r, c);
		}
	}
}
