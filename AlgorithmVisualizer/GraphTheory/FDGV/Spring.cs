using System.Drawing;
using System.Drawing.Drawing2D;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Utils;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class Spring : Edge
	{
		public Color InnerColor { get; set; }
		public Color TextColor { get; set; }
		private int edgeCostTextSize = 11;
		private float edgeWidth = 2.5f, arrowCapWidth = 2.7f, arrowCapHeight = 2.7f;

		// This spring's composing particles
		private Particle p1, p2;

		/* Physics related params and their defaults, can be changed from "FDGVConfigForm.cs".
		 * RestLen - the length where the spring is in rest meaning it applies no forces.
		 * K - The spring proportionality constant, bigger K --> stronger springs. */
		public const float DefaultK = 0.0005f, DefaultRestLen = 125;
		public static float RestLen, K;

		public bool Reversed { get; set; } = false;

		public Spring(Particle _p1, Particle _p2, int cost) : base(_p1.Id, _p2.Id, cost)
		{
			p1 = _p1;
			p2 = _p2;
			SetDefaultColors();
		}

		public static void SetDefaultPhysicsParams()
		{
			RestLen = DefaultRestLen;
			K = DefaultK;
		}
		public void SetDefaultColors()
		{
			InnerColor = Colors.SpringInnerColor;
			TextColor = Colors.SpringTextColor;
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
			// Find line between particles p1, p2
			float radius = Particle.Size / 2;
			var p1Center = new PointF(p1.Pos.X, p1.Pos.Y);
			var p2Center = new PointF(p2.Pos.X, p2.Pos.Y);
			
			// Offset line's start/end points to connect the line between points on the
			// circumferences of the particles rather then thier center points.
			Vector vector = p2.Pos - p1.Pos;
			vector.Magnitude = radius;
			p1Center.X += vector.X;
			p1Center.Y += vector.Y;
			vector.Magnitude = radius;
			p2Center.X -= vector.X;
			p2Center.Y -= vector.Y;
			
			// Draw line between p1, p2
			using (var pen = new Pen(brush, edgeWidth))
			using (var bigArrow = new AdjustableArrowCap(arrowCapWidth, arrowCapHeight))
			{
				if (Reversed) pen.CustomStartCap = bigArrow;
				else pen.CustomEndCap = bigArrow;
				g.DrawLine(pen, p1Center, p2Center);
			}

			DrawSpringText(g, brush);
		}
		private void DrawSpringText(Graphics g, SolidBrush brush)
		{
			// Compute center point of the line between p1, p2
			Vector springCenter = (p2.Pos - p1.Pos) / 2 + p1.Pos;

			// Draw the cost of the edge on the line, centered. Use undraw color for txtBrush if the
			// given brush also uses the undraw color(Colors.Undraw) else the instance color (TextColor)
			using (var font = new Font("Arial", edgeCostTextSize, FontStyle.Bold))
			using (var sf = new StringFormat())
			using (var txtBrush = new SolidBrush(brush.Color == Colors.Undraw ? Colors.Undraw : TextColor))
			{
				sf.LineAlignment = sf.Alignment = StringAlignment.Center;
				g.DrawString(Cost.ToString(), font, txtBrush, springCenter.X, springCenter.Y, sf);
			}
		}

		public void ExertForcesOnParticles()
		{
			// Apply forces on the spring's composing particles
			Vector F = p2.Pos - p1.Pos;
			F.Magnitude = K * (F.Magnitude - RestLen);
			p1.Acc += F;
			p2.Acc += F * -1;
		}

		// True if the given id is p1's or p2's id
		public bool ContainsNodeId(int id) => p1.Id == id || p2.Id == id;
	}
}
