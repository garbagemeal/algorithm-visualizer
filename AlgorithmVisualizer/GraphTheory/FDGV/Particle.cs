using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Utils;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class Particle : GNode
	{
		public Color InnerColor { get; set; }
		public Color BorderColor { get; set; }
		public Color TextColor { get; set; }

		private int textSize = 10;
		private float borderWidth = 1.7f;

		public bool Pinned { get; set; } = false;
		public void TogglePin() => Pinned = !Pinned;

		/* pos - a position vector; the center point of the particle
		 * vel - the velocity of the particle
		 * acc - the acceleration of the particle
		 * Note that getters/setters use a copy constructor, and
		 * setters avoid setting if given vector's X/Y is NaN or infinity. */
		private Vector pos, vel, acc;
		public Vector Pos
		{
			get { return new Vector(pos.X, pos.Y); }
			set { if (Vector.IsValid(value)) pos = new Vector(value.X, value.Y); }
		}
		public Vector Vel
		{
			get { return new Vector(vel.X, vel.Y); }
			set { if (Vector.IsValid(value)) vel = new Vector(value.X, value.Y); }
		}
		public Vector Acc
		{
			get { return new Vector(acc.X, acc.Y); }
			set { if (Vector.IsValid(value)) acc = new Vector(value.X, value.Y); }
		}

		/* Physics related params and their defaults, can be changed from "FDGVConfigForm.cs".
		 * G - The gravitational force used to pull particles to the center or repel others.
		 * MaxSpeed - the maximum allowed speed of particles
		 * MaxCenterPullMag - the maximum allowed magnitude(size) of the center pull force
		 * VelDey - the rate of velocity loss, i.e a velDecay of .99 will result in 
		 * 1% loss of velocity per invocation of UpdatePos()
		 * Size - the size of the particle */
		public const float DefaultSize = 30, DefaultG = 1000f, DefaultMaxSpeed = 10f,
			DefaultMaxCenterPullMag = 0.1f, DefaultVelDecay = 0.99f;
		public static float G, MaxSpeed, MaxCenterPullMag, VelDecay, Size;

		public Particle(int id, int data, Vector _pos) : base(id, data)
		{
			Pos = _pos;
			vel = new Vector(0, 0);
			acc = new Vector(0, 0);
			// Use default color scheme
			SetDefaultColors();
		}

		public static void SetDefaultPhysicsParams()
		{
			G = DefaultG;
			MaxSpeed = DefaultMaxSpeed;
			MaxCenterPullMag = DefaultMaxCenterPullMag;
			VelDecay = DefaultVelDecay;
			Size = DefaultSize;
		}
		public void SetDefaultColors()
		{
			InnerColor = Colors.ParticleInnerColor;
			BorderColor = Colors.ParticleBorderColor;
			TextColor = Colors.ParticleTextColor;
		}

		public void Draw(Graphics g, int canvasHeight, int canvasWidth)
		{
			// Required in case forces are disabled and window is resized
			BoundWithinCanvas(canvasHeight, canvasWidth);
			using (var innerBrush = new SolidBrush(InnerColor)) Draw(g, innerBrush);
		}
		public void Undraw(Graphics g)
		{
			using (var undrawBrush = new SolidBrush(Colors.Undraw)) Draw(g, undrawBrush);
		}
		private void Draw(Graphics g, SolidBrush brush)
		{
			float radius = Size / 2;
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
				using (var font = new Font("Arial", textSize))
				using (var textBrush = new SolidBrush(TextColor))
				using (var sf = new StringFormat())
				{
					sf.LineAlignment = sf.Alignment = StringAlignment.Center;
					g.DrawString(Id.ToString(), font, textBrush, rect, sf);
				}
			}
		}
 
		public void UpdatePos(int canvasHeight, int canvasWidth)
		{
			// Ignore pinned particles
			if (!Pinned)
			{
				// Update velocity using current acceleration and apply force decay
				Vel += acc;
				Vel *= VelDecay;
				if (vel.Magnitude > MaxSpeed) vel.Magnitude = MaxSpeed;
				// Update the position by adding the velocity to the current position
				Pos += vel;
				BoundWithinCanvas(canvasHeight, canvasWidth);
			}
			else vel.Set(0, 0); // avoid storing vel for pinned particles
			// Avoid propagation of acceleration between invocations to this method
			acc.Set(0, 0);
		}
		public void PullToCenter(Vector centerPos)
		{
			// Vector of full length from pos to centerPos
			Vector F = centerPos - pos;
			// Compute a magnitude and adjust F's mag accordingly
			float mag = Math.Min(F.Magnitude / G, MaxCenterPullMag);
			F.Magnitude = mag;
			// Add force into acc
			Acc += F;
		}
		public void ApplyRepulsiveForces(List<Particle> paricles)
		{
			// Gravitational force formula: F = G * (m1 + m2) / d^2

			// foreach particle in the particle list
			foreach (Particle particle in paricles)
			{
				// If not this particle
				if (this != particle)
				{
					// Get vector of full magnitude from this.pos to particle.pos
					Vector F = particle.pos - this.pos;
					// If the magnitude is 0 (particle overlap) - randomize F
					if (F.Magnitude == 0) F = Vector.GetRandom();
					// set F's mag and add into acc
					F.Magnitude = G / (F.Magnitude * F.Magnitude);
					particle.Acc += F;
				}
			}
		}

		public void BoundWithinCanvas(int canvasHeight, int canvasWidth)
		{
			// Ensure 'Pos' is offset by radius from all 4 directions of the canvas
			float radius = Size / 2;
			Pos = new Vector(Math.Max(radius, Math.Min(canvasWidth - radius, pos.X)),
							 Math.Max(radius, Math.Min(canvasHeight - radius, pos.Y)));
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

