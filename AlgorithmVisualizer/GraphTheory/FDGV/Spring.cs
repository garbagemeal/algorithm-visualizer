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

		// Length where spring is in reest (exerts no forces) limited by MAX_FORCE
		private float RestLen { get; set; }
		// Max force spring can apply and spring proportionality constant (k)
		private const float MAX_FORCE = 3f, k = 0.0005f;

		// Revesed state for spring
		public bool Reversed { get; set; } = false;

		public Particle P1 { get; private set; }
		public Particle P2 { get; private set; }

		public Spring(Particle p1, Particle p2, int cost, float restLen) : base(p1.Id, p2.Id, cost)
		{
			// Set spring's attr
			RestLen = restLen;
			P1 = p1;
			P2 = p2;
			// Use default color scheme
			SetDefaultColors();
		}

		public void ExertForcesOnParticles()
		{
			// Method to apply forces on the spring's composing particles
			Vector F = P2.Pos - P1.Pos;
			// Magnitude limited via MAX_FORCE
			float mag = Math.Min(MAX_FORCE, k * (F.Magnitude() - RestLen));
			F.SetMagnitude(mag);
			P1.Accelerate(F);
			//F *= -1;
			P2.Accelerate(F * -1);
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
			int p1Rad = P1.Size / 2, p2Rad = P2.Size / 2;
			var pt1 = new PointF(P1.Pos.X, P1.Pos.Y);
			var pt2 = new PointF(P2.Pos.X, P2.Pos.Y);

			// Offsetting the line starting/ending pos on the particle borders
			Vector vector = P2.Pos - P1.Pos;
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
			vector = (P2.Pos - P1.Pos) / 2 + P1.Pos;

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

		// Check if the given id is P1's or P2's id
		public bool ContainsNodeId(int id) =>  P1.Id == id || P2.Id == id;
	}
}
