using System.Drawing;

namespace AlgorithmVisualizer.Tracers
{
	class ArrayTracer<T> : AbstractArrayTracer<T>
	{
		private T[] arrToTrace;

		public ArrayTracer(T[] arr, Graphics g, string title, PointF startPoint, SizeF size, float entryWidth)
			: base(g, title, startPoint, size, entryWidth) => arrToTrace = arr;

		public override void Trace() => Trace(arrToTrace);
		public override void Mark(int i) => Mark(arrToTrace, i);
	}
}
