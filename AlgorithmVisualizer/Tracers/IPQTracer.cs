using System;
using System.Drawing;

using AlgorithmVisualizer.DataStructures.Heap;

namespace AlgorithmVisualizer.Tracers
{
	class IPQTracer<T> : AbstractArrayTracer<string>
	{
		private MinIndexedDHeap<int> ipq;

		public IPQTracer(MinIndexedDHeap<int> _ipq, Graphics g, string title, PointF startPoint, SizeF size)
			: base(g, title, startPoint, size) => ipq = _ipq;

		public override void Trace() => Trace(IpqToStrArr());
		public override void Mark(int i, Color color) => Mark(IpqToStrArr(), i, color);

		private string[] IpqToStrArr()
		{
			// Convert (int id, int val)[] into string[]
			(int id, int val)[] idValArr = ipq.ToArray();
			string[] strArr = new string[idValArr.Length];
			for (int i = 0; i < strArr.Length; i++)
				strArr[i] = $"id: {idValArr[i].id} \nval: {idValArr[i].val}";
			return strArr;
		}
	}
}
