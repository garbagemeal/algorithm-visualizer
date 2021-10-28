using System;
using System.Drawing;

using AlgorithmVisualizer.DataStructures.Heap;

namespace AlgorithmVisualizer.Tracers
{
	class HeapTracer<T> : AbstractArrayTracer<T> where T : IComparable
	{
		private BinaryMinHeap<T> heap;

		public HeapTracer(BinaryMinHeap<T> _heap, Graphics g, string title, PointF startPoint, SizeF size)
			: base(g, title, startPoint, size) => heap = _heap;

		public override void Trace() => Trace(heap.ToArray());
		public override void Mark(int i, Color color) => Mark(heap.ToArray(), i, color);
	}
}
