using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.MathUtils;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class Spring : Edge
	{
		// Default spring color scheme
		private static readonly Color
			defaultInnerColor = ColorTranslator.FromHtml("#E8E8E8"),
			defaultTextColor = ColorTranslator.FromHtml("#FF0000");
		public Color InnerColor { get; set; }
		public Color TextColor { get; set; }
		public void SetDefaultColors()
		{
			// Set edge's colors to defaults
			InnerColor = defaultInnerColor;
			TextColor = defaultTextColor;
		}
		private int edgeCostTextSize = 11;
		private float edgeWidth = 2.5f, arrowCapWidth = 2.7f, arrowCapHeight = 2.7f;

		// Composing particles
		private Particle p1, p2;
		// Length where spring is in reest
		private float RestLen { get; set; }
		// Proportionality constant
		private const float k = 0.0005f;
		public bool Reversed { get; set; } = false;


		public Spring(Particle _p1, Particle _p2, int cost, float restLen) : base(_p1.Id, _p2.Id, cost)
		{
			// Set spring's attr
			RestLen = restLen;
			p1 = _p1;
			p2 = _p2;
			// Use default color scheme
			SetDefaultColors();
		}

		public void ExertForcesOnParticles()
		{
			// Apply forces on the spring's composing particles
			Vector F = p2.Pos - p1.Pos;
			F.SetMagnitude(k * (F.Magnitude() - RestLen));
			p1.Accelerate(F);
			p2.Accelerate(F * -1);
		}

		public void Draw(Graphics g)
		{
			using (var innerBrush = new SolidBrush(InnerColor)) Draw(g, innerBrush);
		}
		public void Undraw(Graphics g)
		{
			using (var undrawBrush = new SolidBrush(Colors.Undraw)) Draw(g, undrawBrush);
		}
		private void Draw(Graphics g, SolidBrush brush)
		{
			// Draw this spring using the given brush

			// center points of the particles
			int p1Rad = p1.Size / 2, p2Rad = p2.Size / 2;
			var pt1 = new PointF(p1.Pos.X, p1.Pos.Y);
			var pt2 = new PointF(p2.Pos.X, p2.Pos.Y);

			// Offsetting the line starting/ending pos on the particle borders
			Vector vector = p2.Pos - p1.Pos;
			vector.SetMagnitude(p1Rad);
			pt1.X += vector.X;
			pt1.Y += vector.Y;
			vector.SetMagnitude(p2Rad);
			pt2.X -= vector.X;
			pt2.Y -= vector.Y;

			// Drawing the line between pt1, pt2
			using (var pen = new Pen(brush, edgeWidth))
			{
				using (var bigArrow = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight))
				{
					if (Reversed) pen.CustomStartCap = bigArrow;
					else pen.CustomEndCap = bigArrow;
					g.DrawLine(pen, pt1, pt2);
				}
			}

			// Find center point of vector from p1 to p2
			vector = (p2.Pos - p1.Pos) / 2 + p1.Pos;

			// if selected brush is not the undrawing brush
			if (brush.Color != Colors.Undraw) brush = new SolidBrush(TextColor);

			// Draw the cost of the edge on the line, centered
			using (var font = new Font("Arial", edgeCostTextSize, FontStyle.Bold))
			{
				using (var sf = new StringFormat())
				{
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Center;
					g.DrawString(Cost.ToString(), font, brush, vector.X, vector.Y, sf);
				}
			}
			// Dispose of brush in case a new 1 was created in if statement
			brush.Dispose();
		}

		// True if the given id is p1's or p2's id
		public bool ContainsNodeId(int id) =>  p1.Id == id || p2.Id == id;
	}
}
