using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.MathUtils;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class Particle : GNode
	{
		// Default particle color scheme
		private static readonly Color
			defaultInnerColor = ColorTranslator.FromHtml("#646464"),
			defaultBorderColor = ColorTranslator.FromHtml("#E8E8E8"),
			defaultTextColor = ColorTranslator.FromHtml("#E8E8E8");
		public Color InnerColor { get; set; }
		public Color BorderColor { get; set; }
		public Color TextColor { get; set; }
		public void SetDefaultColors()
		{
			// Set particle's colors to defaults
			InnerColor = defaultInnerColor;
			BorderColor = defaultBorderColor;
			TextColor = defaultTextColor;
		}


		// Position, velocity and acceleration (2D vectors)
		private Vector pos, vel, acc;
		// Note that vector getter/setters use a copy constructor!
		public Vector Pos { get { return new Vector(pos.X, pos.Y); } set { pos = new Vector(value.X, value.Y); } }
		public Vector Vel { get { return new Vector(vel.X, vel.Y); } set { vel = new Vector(value.X, value.Y); } }
		public Vector Acc { get { return new Vector(acc.X, acc.Y); } set { acc = new Vector(value.X, value.Y); } }


		// size is the diameter of the particle
		private int textSize = 10;
		private float borderWidth = 1.7f;


		// Physics related & particle stabilization
		private const float G = 1000f, MAX_PARTICLE_SPEED = 7f, MIN_REPEL_MAG = 0.003f,
			MAX_REPEL_MAG = 1, MAX_CENTER_PULL_MAG = 0.5f, VEL_DECAY = 0.99f;


		// Used to capture the maximal velocity magnitude per iteration in the force
		// directed method of drawin the graph (used as a stop condition when the maximal
		// velocity is small enough in some interation i)
		// Make sure to reset to 0 after each iteration, otherwise max speed will propegate
		// between iterations (no longer max speed per iteration but total runtime max speed).
		public static float MAX_VEL_MAG_PER_ITR { get; set; } = 0;


		// Diameter of particle & keeping track of pinned and visible particles
		// Note the default values for visible and pinned
		public int Size { get; set; }
		public bool Pinned { get; set; } = false;
		public void TogglePin() => Pinned = !Pinned;


		public Particle(int id, int data, Vector _pos, int size) : base(id, data)
		{
			pos = _pos;
			vel = new Vector(0, 0);
			acc = new Vector(0, 0);
			Size = size;
			// Use default color scheme
			SetDefaultColors();
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
			int radius = Size / 2;
			// Create rectagle centered around x, y
			RectangleF rect = new RectangleF(pos.X - radius, pos.Y - radius, Size, Size);
			// Draw/Undraw particle
			g.FillEllipse(brush, rect);
			// If not undrawing the particle
			if (brush.Color != Colors.Undraw)
			{
				// Draw border
				using (var pen = new Pen(BorderColor, borderWidth)) g.DrawEllipse(pen, rect);
				// Draw node id centered within particle
				using (var textBrush = new SolidBrush(TextColor))
				{
					using (StringFormat sf = new StringFormat())
					{
						// Used to center string with sf
						sf.LineAlignment = StringAlignment.Center;
						sf.Alignment = StringAlignment.Center;
						
						using (Font font = new Font("Arial", textSize))
							g.DrawString(Id.ToString(), font, textBrush, rect, sf);
					}
				}
			}
		}

		// Add given force to acc. Note: F = ma --> a = F/m --> F = a (because m = 1) 
		public void Accelerate(Vector F) => acc += F;
		public void UpdatePos(int canvasHeight, int canvasWidth)
		{
			// Ignore pinned particles
			if (!Pinned)
			{
				// Update the velocity via current acceleration
				vel += acc;
				// recude vel by some %
				vel *= VEL_DECAY;
				if (vel.Magnitude() > MAX_PARTICLE_SPEED) vel.SetMagnitude(MAX_PARTICLE_SPEED);
				// Update the position by adding the velocity to the current position
				pos += vel;
				BoundWithinCanvas(canvasHeight, canvasWidth);
				// Keep track of maximal verlocity per iteration of the FDGV; needs to be
				// manually reset from within the FDGV algo after each iteration.
				if (vel.Magnitude() > MAX_VEL_MAG_PER_ITR) MAX_VEL_MAG_PER_ITR = vel.Magnitude();
			}
			// Reset the acceleration to 0 for the next iteration, otherwise paricle
			// will accelerate with each iteration and eventually "fly" of the screen.
			acc.Set(0, 0);
		}
		public void BoundWithinCanvas(int canvasHeight, int canvasWidth)
		{
			// Bounds the position vector of this particle within the canvas.
			// The bounding is with respect to the middle point where x, y are
			// always offset by radius from all 4 directions

			int radius = Size / 2;
			// Bound X within canvasWidth
			pos.X = Math.Max(radius, Math.Min(canvasWidth - radius, pos.X));
			// Bound Y within canvasHeight
			pos.Y = Math.Max(radius, Math.Min(canvasHeight - radius, pos.Y));
		}
		public void PullToCenter(Vector centerPos)
		{
			// Vector of full length from pos to centerPos
			Vector F = centerPos - pos;
			// Compute a magnitude and adjust F's mag accordingly
			float mag = Math.Min(F.Magnitude() / G, MAX_CENTER_PULL_MAG);
			F.SetMagnitude(mag);
			// Add force into acc
			acc += F;
		}
		public void ApplyRepulsiveForces(List<Particle> paricleList)
		{
			// Gravitational force formula: F = G * (m1 + m2) / d^2

			// foreach particle in the particle list
			foreach (Particle particle in paricleList)
			{
				// If not this particle
				if (this != particle)
				{
					// Get vector of full magnitude from this.pos to particle.pos
					Vector F = particle.pos - this.pos;
					// If the magnitude is 0 (particle overlap) - randomize F
					if (F.Magnitude() == 0) F = Vector.GetRandom();
					// Bound and magnitude within [MIN_REPEL_MAG, MAX_REPEL_MAG]
					float mag = Math.Max(MIN_REPEL_MAG, Math.Min(G / (F.Magnitude() * F.Magnitude()), MAX_REPEL_MAG));
					F.SetMagnitude(mag);
					// Accelerate particle using computed force
					particle.acc += F;
				}
			}
		}

		public bool PointIsWithin(float x, float y)
		{
			// Check if the given point's coordinates are within the particle(circle)
			// Note: given the center point of a circle and its radius, a given point is
			// within the circle if: (x1-x2)^2 + (y1-y2)^2 <= r^2

			// Compute particle's radius
			float r = Size / 2;
			// Find particle's center point
			float xc = pos.X, yc = pos.Y;
			// Use formula to check if given point (x, y) is within the particle
			float dx = xc - x, dy = yc - y;
			return dx * dx + dy * dy <= r * r;
		}
	}
}

