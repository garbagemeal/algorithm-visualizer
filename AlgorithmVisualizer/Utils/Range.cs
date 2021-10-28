namespace AlgorithmVisualizer.Utils
{
	class Range
	{
		public float Min { get; set; }
		public float Max { get; set; }
		
		public Range(float min, float max)
		{
			Min = min;
			Max = max;
		}

		public static float Scale(float val, Range rangeIn, Range rangeOut)
		{
			// Convert the value 'val' in the range 'rangeIn' into a value in 'rangeOut'
			// while maintaining the ratio.
			float minIn = rangeIn.Min, maxIn = rangeIn.Max, minOut = rangeOut.Min, maxOut = rangeOut.Max;
			return (((val - minIn) * (maxOut - minOut)) / (maxIn - minIn)) + minOut;
		}

		public override string ToString() => $"({Min}, {Max})";
	}
}
