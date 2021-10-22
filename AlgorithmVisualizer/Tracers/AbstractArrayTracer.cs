using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.Tracers
{
	abstract class AbstractArrayTracer<T>
	{
		protected Graphics g;
		protected PointF startPoint;
		protected SizeF size; // size(area) of tracer including the title

		protected string title;
		private string fontName = "Arial";
		private int fontSize = 10;
		private float entryWidth;
		public SizeF TitleSize { get; set; }

		public AbstractArrayTracer(Graphics _g, string _title, PointF _startPoint, SizeF _size, float _entryWidth)
		{
			g = _g;
			title = _title;
			startPoint = _startPoint;
			size = _size;
			entryWidth = _entryWidth;
			// Store title measurements in TitleMeasure
			using (var f = new Font(fontName, fontSize)) TitleSize = g.MeasureString(title, f);
		}

		public abstract void Trace();
		public abstract void Mark(int i);

		protected void Trace(T[] arr)
		{
			Untrace();
			DrawTitle();
			int n = arr.Length;
			if (n == 0) return;

			float rectStartX = startPoint.X + TitleSize.Width;
			for (int i = 0; i < n; i++)
			{
				// Draw rect for entry
				var rect = new RectangleF(rectStartX, startPoint.Y, entryWidth, size.Height);
				DrawRectF(Pens.Black, rect);

				// Draw the value centered in the rect
				using (var font = new Font(fontName, fontSize))
				using (var sf = new StringFormat())
				{
					string val = GetVal(arr, i, font);
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Center;
					g.DrawString(val, font, Brushes.White, rect, sf);
				}
				rectStartX += entryWidth;
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
				// Support tracing last value
				if (i == -1 && N > 0) i = N - 1;
				// Draw rect for entry
				float rectStartX = i * entryWidth + startPoint.X + TitleSize.Width;
				var rect = new RectangleF(rectStartX, startPoint.Y, entryWidth, size.Height);
				DrawRectF(Pens.Red, rect);

				// Draw the value centered in the rect
				using (var font = new Font(fontName, fontSize))
				using (var sf = new StringFormat())
				{
					string val = GetVal(arr, i, font);
					sf.LineAlignment = StringAlignment.Center;
					sf.Alignment = StringAlignment.Center;
					g.DrawString(val, font, Brushes.Red, rect, sf);
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
				g.DrawString(title, font, Brushes.White, new RectangleF(startPoint, TitleSize), sf);
			}
		}
		private string GetVal(T[] arr, int i, Font font)
		{
			string str = arr[i].ToString();
			// If value is int.MaxValue then change val to "inf"
			if (str.ToString().Equals(int.MaxValue.ToString())) str = "inf";
			// Else if string width measurement > entryWidth
			else if (g.MeasureString(str, font).Width > entryWidth)
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
