using System.Collections.Generic;
using System.Drawing;

namespace AlgorithmVisualizer.Tracers
{
	class QueueTracer<T> : AbstractArrayTracer<T>
	{
		private Queue<T> q;

		public QueueTracer(Queue<T> _q, Graphics g, string title, PointF startPoint, SizeF size, float entryWidth)
			: base(g, title, startPoint, size, entryWidth) => q = _q;

		public override void Trace() => Trace(q.ToArray());
		public override void Mark(int i) => Mark(q.ToArray(), i);
	}
}
