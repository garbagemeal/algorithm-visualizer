using System;

namespace AlgorithmVisualizer.Utils
{
	public class Vector
	{
		// Class to represent a 2d vector
		public float X { get; set; }
		public float Y { get; set; }
		public void Set(float x, float y)
		{
			X = x;
			Y = y;
		}
		private static Random rnd = new Random();

		public Vector(float _x, float _y) => Set(_x, _y);
		public Vector(Vector v) => Set(v.X, v.Y);

		public static Vector operator +(Vector v1, Vector v2) =>
			new Vector(v1.X + v2.X, v1.Y + v2.Y);
		public static Vector operator -(Vector v1, Vector v2) =>
			new Vector(v1.X - v2.X, v1.Y - v2.Y);
		public static Vector operator *(Vector v, float scalar) =>
			new Vector(v.X * scalar, v.Y * scalar);
		public static Vector operator /(Vector v, float scalar)
		{
			if (scalar == 0) throw new DivideByZeroException("Can't divide by 0!");
			return new Vector(v.X / scalar, v.Y / scalar);
		}

		public void Normalize()
		{
			// Scale vector such that its magnitude becomes 1
			// do nothing if current magnitude is 0 or 1
			float m = Magnitude;
			if (m != 0 && m != 1)
			{
				X /= m;
				Y /= m;
			}
		}

		public float Magnitude
		{
			// Return distance using the pythagorean formula
			get { return (float)Math.Sqrt(X * X + Y * Y); }
			// Normalize the vector and then set x and y to match the new mag
			set { Normalize(); X *= value; Y *= value; }
		}
		
		public override string ToString() => $"({X}, {Y})";

		public static bool IsValid(Vector v)
		{
			// false if v.X or v.Y is NaN or positive/negative infinity, true otherwise
			float[] values = new float[] { v.X, v.Y };
			foreach (float val in values)
			{
				if (float.IsNaN(val)) return false;
				if (float.IsInfinity(val)) return false;
			}
			return true;
		}
		public static Vector GetRandom()
		{
			// Returns a new randomized vector
			return new Vector(NextFloat(), NextFloat());
			float NextFloat()
			{
				// range of mantissa: -1 to 1
				double mantissa = (rnd.NextDouble() * 2.0) - 1.0;
				// the exponent, can be though of as a "scalar (power of 2)"
				double exponent = Math.Pow(2.0, rnd.Next(-126, 127));
				// gives a float
				return (float)(mantissa * exponent);
			}
		}


	}
}