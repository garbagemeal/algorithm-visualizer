using System;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.Tracers
{
	abstract class AbstractArrayTracer<T>
	{
		private Graphics g;
		private PointF startPoint;
		private SizeF size; // size(area) of tracer including the title

		private string title;
		private string fontName = "Arial";
		private int fontSize = 8;
		public SizeF TitleSize { get; set; }

		public AbstractArrayTracer(Graphics _g, string _title, PointF _startPoint, SizeF _size)
		{
			g = _g;
			title = _title;
			startPoint = _startPoint;
			size = _size;
			// Store title measurements in TitleMeasure
			using (var f = new Font(fontName, fontSize)) TitleSize = g.MeasureString(title, f);
		}

		public abstract void Trace();
		public abstract void Mark(int i);

		private bool EntryInBounds(float x, float entryWidth) => x + entryWidth < size.Width;

		protected void Trace(T[] arr)
		{
			Untrace();
			DrawTitle();
			int n = arr.Length;
			if (n == 0) return;
			using (var font = new Font(fontName, fontSize))
			using (var sf = new StringFormat())
			{
				float entryWidth = GetEntryWidth(arr, font), rectStartX = startPoint.X + TitleSize.Width;
				for (int i = 0; i < n; i++)
				{
					if (EntryInBounds(rectStartX, entryWidth))
					{
						// Draw rect for entry
						var rect = new RectangleF(rectStartX, startPoint.Y, entryWidth, size.Height);
						DrawRectF(Pens.Black, rect);

						// Draw the value centered in the rect
						string val = GetVal(arr[i], font, entryWidth);
						sf.LineAlignment = StringAlignment.Center;
						sf.Alignment = StringAlignment.Center;
						g.DrawString(val, font, Brushes.White, rect, sf);
						rectStartX += entryWidth;
					}
				}
			}
		}
		public void Untrace()
		{
			using (var undrawBrush = new SolidBrush(Colors.UndrawLog))
				g.FillRectangle(undrawBrush, new RectangleF(startPoint, size + new SizeF(1, 1)));
		}
		protected void Mark(T[] arr, int i)
		{
			Trace(arr);
			int N = arr.Length;
			if (N > i && i >= -1)
			{
				using (var font = new Font(fontName, fontSize))
				using (var sf = new StringFormat())
				{
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Center;

					// Support tracing last value
					if (i == -1 && N > 0) i = N - 1;

					float entryWidth = GetEntryWidth(arr, font), rectStartX = i * entryWidth + startPoint.X + TitleSize.Width;
					if (EntryInBounds(rectStartX, entryWidth))
					{
						// Draw rect for entry
						var rect = new RectangleF(rectStartX, startPoint.Y, entryWidth, size.Height);
						DrawRectF(Pens.Red, rect);

						// Draw the value centered in the rect
						string val = GetVal(arr[i], font, entryWidth);
						g.DrawString(val, font, Brushes.Red, rect, sf);
					}
				}
			}
		}

		private void DrawTitle()
		{
			using (var font = new Font(fontName, fontSize))
			using (var sf = new StringFormat())
			{
				sf.LineAlignment = StringAlignment.Center;
				sf.Alignment = StringAlignment.Center;
				var rect = new RectangleF(startPoint, new SizeF(TitleSize.Width, size.Height));
				g.DrawString(title, font, Brushes.White, rect, sf);
			}
		}

		private float GetEntryWidth(T[] arr, Font font)
		{
			float maxPossibleEntryWidth = (size.Width - TitleSize.Width) / arr.Length;
			//float longestEntryWidth = 0;
			//foreach (T val in arr)
			//{
			//	float curEntryWidth = g.MeasureString(val.ToString(), font).Width;
			//	if (curEntryWidth > longestEntryWidth) longestEntryWidth = curEntryWidth;
			//}
			// return Math.Min(maxPossibleEntryWidth, longestEntryWidth);
			return Math.Min(size.Height, maxPossibleEntryWidth);
		}

		private string GetVal(T val, Font font, float entryWidth)
		{
			string str = val.ToString();
			// replace Int.MinValue/MaxValue with "+inf"/"-inf"
			if (str.ToString().Equals(int.MaxValue.ToString())) str = "+inf";
			if (str.ToString().Equals(int.MinValue.ToString())) str = "-inf";
			// if string width measurement > entryWidth
			if (g.MeasureString(str, font).Width > entryWidth)
			{
				// Remove last char While value width measurement > entryWidth + ".."
				while (g.MeasureString(str + "..", font).Width > entryWidth)
					str = str.Substring(0, str.Length - 1);
				str += "..";
			}
			return str;
		}

		private void DrawRectF(Pen pen, RectangleF rectF)
		{
			// Somewhy "g.DrawRectangle()" wont accept the type "RectangleF", only "RectangleF[]"
			RectangleF[] rectFs = new RectangleF[] { rectF };
			g.DrawRectangles(pen, rectFs);
		}
	}
}
