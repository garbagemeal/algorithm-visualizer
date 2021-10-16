using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.DataStructures.Heap;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.ArrayTracer
{
	// I should probably refactor this...
	public class ArrayTracer<T> where T : IComparable
	{
		private Graphics g;
		private string name;
		private int nameOffset, x, y, height, width;
		private Color undrawColor = Colors.UndrawLog;
		// Font defaults (name and size)
		private static readonly string defaultFontName = "Arial";
		private const int defaultFontSize = 10;
		// dsType denotes the data structure being traced:
		// 0 - Array
		// 1 - Queue
		// 2 - Stack
		// 3 - List
		// 2 - Heap (BinaryMinHeap)
		// 3 - IPQ (MinIndexedDHeap)
		private int dsType;
		private T[] arr;
		private Queue<T> q;
		private Stack<T> stk;
		private List<T> list;
		private BinaryMinHeap<T> heap;
		private MinIndexedDHeap<T> ipq;
		// the selected ds is "converted" into a string array for tracing.
		private string[] strArr;

		public int NameOffset { get { return nameOffset; } set { nameOffset = value; } }

		#region Constructors
		private ArrayTracer(Graphics _g, string _name, int _x, int _y, int _width, int _height)
		{
			g = _g;

			name = _name;
			// Measure names's width and store in nameOffset
			using (var font = new Font(defaultFontName, defaultFontSize))
				nameOffset = (int)Math.Ceiling(g.MeasureString(name, font).Width);

			x = _x;
			y = _y;
			width = _width;
			height = _height;
		}
		public ArrayTracer(T[] _arr, Graphics _g, string _name, int _x, int _y, int _width, int _height) : this(_g, _name, _x, _y, _width, _height)
		{
			if (_arr == null) throw new ArgumentException("_arr cannot be null");
			dsType = 0;
			arr = _arr;
		}
		public ArrayTracer(Queue<T> _q, Graphics _g, string _name, int _x, int _y, int _width, int _height) : this(_g, _name, _x, _y, _width, _height)
		{
			if (_q == null) throw new ArgumentException("_q cannot be null");
			dsType = 1;
			q = _q;
		}
		public ArrayTracer(Stack<T> _stk, Graphics _g, string _name, int _x, int _y, int _width, int _height) : this(_g, _name, _x, _y, _width, _height)
		{
			if (_stk == null) throw new ArgumentException("_stk cannot be null");
			dsType = 2;
			stk = _stk;
		}
		public ArrayTracer(List<T> _list, Graphics _g, string _name, int _x, int _y, int _width, int _height) : this(_g, _name, _x, _y, _width, _height)
		{
			if (_list == null) throw new ArgumentException("_list cannot be null");
			dsType = 3;
			list = _list;
		}
		public ArrayTracer(BinaryMinHeap<T> _heap, Graphics _g, string _name, int _x, int _y, int _width, int _height) : this(_g, _name, _x, _y, _width, _height)
		{
			if (_heap == null) throw new ArgumentException("_heap cannot be null");
			dsType = 4;
			heap = _heap;
		}
		public ArrayTracer(MinIndexedDHeap<T> _ipq, Graphics _g, string _name, int _x, int _y, int _width, int _height) : this(_g, _name, _x, _y, _width, _height)
		{
			if (_ipq == null) throw new ArgumentException("_ipq cannot be null");
			dsType = 5;
			ipq = _ipq;
		}
		#endregion

		#region Conversion of ds into an array
		private void ArrToStrArr()
		{
			// copy arr into strArr using arr[i].ToString()
			strArr = new string[arr.Length];
			for (int i = 0; i < arr.Length; i++)
				strArr[i] = arr[i].ToString();
		}
		private void QueueToStrArr()
		{
			// convert q into string array
			arr = q.ToArray();
			ArrToStrArr();
		}
		private void StkToStrArr()
		{
			// convert stack into string array
			arr = stk.ToArray();
			ArrToStrArr();
		}
		private void ListToStrArr()
		{
			// convert list into string array
			arr = list.ToArray();
			ArrToStrArr();
		}
		private void HeapToStrArr()
		{
			// convert heap into string array
			//arr = (T[])Convert.ChangeType(heap.GetSortedIdDataArray(), typeof(T[]));
			strArr = heap.GetSortedIdDataArray();
		}
		private void IPQToStrArr()
		{
			// convert ipq into string array
			//arr = (T[])Convert.ChangeType(ipq.GetSortedIdValueArr(), typeof(T[]));
			strArr = ipq.GetSortedIdValueArr();
		}
		private void ConvertToStrArr()
		{
			// depending on the dsType do the needed conversion
			// of the data structure into a string array
			switch (dsType)
			{
				case 0:
					ArrToStrArr();
					break;
				case 1:
					QueueToStrArr();
					break;
				case 2:
					StkToStrArr();
					break;
				case 3:
					ListToStrArr();
					break;
				case 4:
					HeapToStrArr();
					break;
				case 5:
					IPQToStrArr();
					break;
			}
		}
		#endregion

		#region Visuals
		public void Trace()
		{
			// Undraw in case traced before
			Undraw();

			// In case a queue is being traced convert it into an array and save reference in arr
			ConvertToStrArr();

			// Draw name of array
			Rectangle rect = new Rectangle(x, y, nameOffset, height);
			using (var font = new Font(defaultFontName, defaultFontSize))
			{
				using (var sf = new StringFormat())
				{
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Center;
					g.DrawString(name, font, Brushes.White, rect, sf);
				}
			}

			// Trace array values (if non empty)
			int N = strArr.Length;
			if(N > 0)
			{
			float entryWidth = Math.Min(height, (width - nameOffset) / N), rectStartX = x + nameOffset;
				for (int i = 0; i < N; i++)
				{
					rect = new Rectangle((int)rectStartX, y, (int)entryWidth, height);
					g.DrawRectangle(Pens.Black, rect);
					string val = strArr[i];
					// if value is int.MaxValue then it is assumed val is used to represent
					// positive infinity in dijkstra's algo (distMap) if this is the case
					// change val to "INF"
					if (val.ToString().Equals(int.MaxValue.ToString())) val = "inf";
					using (var font = new Font(defaultFontName, defaultFontSize))
					{
						using (var sf = new StringFormat())
						{
							sf.LineAlignment = StringAlignment.Center;
							sf.Alignment = StringAlignment.Center;
							g.DrawString(val, font, Brushes.White, rect, sf);
						}
					}
					//g.DrawLine(Pens.Black, rectStartX, y, rectStartX, y + height);
					rectStartX += entryWidth;
				}
			}
		}
		public void Undraw()
		{
			// Undraw rectangle containing tracer from prev use
			Rectangle rect = new Rectangle(x, y, width, height + 1);
			using (var undrawBrush = new SolidBrush(undrawColor))
				g.FillRectangle(undrawBrush, rect);
		}
		public void HighlightAt(int i)
		{
			// Highlight a value at the given index in strArr
			Trace();
			// Trace value at index i of strArr
			// if i is -1 then trace the last value
			int N = strArr.Length;
			if (N > i && i >= -1)
			{
				if(i == -1 && N > 0) i = N - 1;
				float entryWidth = Math.Min(height, (width - nameOffset) / N),
					rectStartX = i * entryWidth + x + nameOffset;
				Rectangle rect = new Rectangle((int)rectStartX, y, (int)entryWidth, height);
				g.DrawRectangle(Pens.Red, rect);
				string val = strArr[i];
				// if value is int.MaxValue then it is assumed val is used to represent
				// positive infinity in dijkstra's algo (distMap) if this is the case
				// change val to "INF"
				if (val.ToString().Equals(int.MaxValue.ToString())) val = "inf";
				using (var font = new Font(defaultFontName, defaultFontSize))
				{
					using (var sf = new StringFormat())
					{
						sf.LineAlignment = StringAlignment.Center;
						sf.Alignment = StringAlignment.Center;
						g.DrawString(val, font, Brushes.Red, rect, sf);
					}
				}
			}
		}
		#endregion
	}
}
