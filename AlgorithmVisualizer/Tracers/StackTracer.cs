using System.Collections.Generic;
using System.Drawing;

namespace AlgorithmVisualizer.Tracers
{
	class StackTracer<T> : AbstractArrayTracer<T>
	{
		private Stack<T> stk;

		public StackTracer(Stack<T> _stk, Graphics g, string title, PointF startPoint, SizeF size)
			: base(g, title, startPoint, size) => stk = _stk;

		public override void Trace() => Trace(stk.ToArray());
		public override void Mark(int i, Color color) => Mark(stk.ToArray(), i, color);
	}
}
