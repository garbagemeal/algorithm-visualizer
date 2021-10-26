using System;
using System.Drawing;

namespace AlgorithmVisualizer.GraphTheory.Utils
{
	static class Colors
	{
		// Default particle/spring colors
		public static Color
			ParticleInnerColor = ColorTranslator.FromHtml("#646464"),
			ParticleBorderColor = ColorTranslator.FromHtml("#E8E8E8"),
			ParticleTextColor = ColorTranslator.FromHtml("#E8E8E8"),
			SpringInnerColor = ColorTranslator.FromHtml("#E8E8E8"),
			SpringTextColor = ColorTranslator.FromHtml("#FF0000");

		// Colors used for the graph algorithm visualization
		public static readonly Color
			// Visiting node/edge
			Orange = Color.Orange,
			// Relaxed edge / Start node
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
		public static Color GetRandom() => Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
	}
}
