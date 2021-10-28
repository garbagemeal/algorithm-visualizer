using System.Drawing;

namespace AlgorithmVisualizer.Tracers
{
	class ArrayTracer<T> : AbstractArrayTracer<T>
	{
		private T[] arrToTrace;

		public ArrayTracer(T[] arr, Graphics g, string title, PointF startPoint, SizeF size)
			: base(g, title, startPoint, size) => arrToTrace = arr;

		public override void Trace() => Trace(arrToTrace);
		public override void Mark(int i, Color color) => Mark(arrToTrace, i, color);
	}
}
