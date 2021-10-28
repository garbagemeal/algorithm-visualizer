using System.Collections.Generic;
using System.Drawing;

namespace AlgorithmVisualizer.Tracers
{
	class QueueTracer<T> : AbstractArrayTracer<T>
	{
		private Queue<T> q;

		public QueueTracer(Queue<T> _q, Graphics g, string title, PointF startPoint, SizeF size)
			: base(g, title, startPoint, size) => q = _q;

		public override void Trace() => Trace(q.ToArray());
		public override void Mark(int i, Color color) => Mark(q.ToArray(), i, color);
	}
}
