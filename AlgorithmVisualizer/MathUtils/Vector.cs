using System;

namespace AlgorithmVisualizer.MathUtils
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

		// Overloading arithmetic operations (+, -, *, /)
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

		// return distance using the pythagorean formula
		public float Magnitude() => (float)Math.Sqrt(X*X + Y*Y);
		public void Normalize()
		{
			// Scale vector such that its magnitude becomes 1
			// do nothing if current magnitude is 0 or 1
			float m = Magnitude();
			if (m != 0 && m != 1)
			{
				X /= m;
				Y /= m;
			}
		}
		public void SetMagnitude(float mag)
		{
			// Normalize the vector and then set to new mag
			Normalize();
			X *= mag;
			Y *= mag;
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
		public override string ToString() => string.Format("({0}, {1})", X, Y);
	}
}