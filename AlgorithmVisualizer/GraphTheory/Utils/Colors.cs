using System;
using System.Drawing;

namespace AlgorithmVisualizer.GraphTheory.Utils
{
	static class Colors
	{
		// Colors used for the graph visualization

		public static readonly Color
			// Visiting node/edge
			Orange = Color.Orange,
			// Relaxed edge
			Red = Color.Red,
			// Part of solution, i.e, the SSSP, the MST, ...
			Green = Color.Green,
			// Relaxation failed (i.e, no improvment in cost when trying to relax edge)
			Blue = Color.Blue,
			// Removal inside main panel (undrawing) (dark grey)
			Undraw = ColorTranslator.FromHtml("#222222"),
			// Removal inside panelLog (undrawing) (light grey)
			UndrawLog = ColorTranslator.FromHtml("#393939"),
			// Visited edge/vertex (dark grey, lighter then previous color)
			Visited = ColorTranslator.FromHtml("#454545"),
			// Visited vertex border (light grey)
			VisitedBorder = ColorTranslator.FromHtml("#909090");

		private static readonly Random rnd = new Random();
		// Rturns a random rgb color
		public static Color GetRandom() =>
			Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
	}
}
