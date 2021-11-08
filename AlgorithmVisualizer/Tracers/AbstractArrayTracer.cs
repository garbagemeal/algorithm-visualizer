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
		public float EntryWidth { get; set; }

		public AbstractArrayTracer(Graphics _g, string _title, PointF _startPoint, SizeF _size)
		{
			g = _g;
			title = _title;
			startPoint = _startPoint;
			size = _size;
			EntryWidth = size.Height;
			// Store title measurements in TitleMeasure
			using (var f = new Font(fontName, fontSize)) TitleSize = g.MeasureString(title, f);
		}

		public abstract void Trace();
		public abstract void Mark(int i, Color color);

		protected void Trace(T[] arr)
		{
			int n = arr.Length;
			if (arr == null || n == 0) return;
			Untrace();
			DrawTitle();
			float rectStartX = startPoint.X + TitleSize.Width;
			for (int i = 0; i < n; i++)
			{
				DrawEntry(arr[i], rectStartX, Color.Black, Color.White);
				rectStartX += EntryWidth;
			}
		}
		public void Untrace()
		{
			using (var undrawBrush = new SolidBrush(Colors.UndrawLog))
				g.FillRectangle(undrawBrush, new RectangleF(startPoint, size + new SizeF(1, 1)));
		}
		protected void Mark(T[] arr, int i, Color color)
		{
			Trace(arr);
			int n = arr.Length;
			if (i < -1 || i >= n) return; // index out of bounds
			if (i == -1) i = n - 1; // Support tracing last value

			float rectStartX = i * EntryWidth + startPoint.X + TitleSize.Width;
			UndrawEntry(rectStartX);
			DrawEntry(arr[i], rectStartX, color, color);
		}

		private void DrawEntry(T val, float rectStartX, Color rectColor, Color txtColor)
		{
			if (!EntryInBounds(rectStartX)) return; // Avoid trying to draw the entry off screen
			using (var pen = new Pen(rectColor))
			using (var brush = new SolidBrush(txtColor))
			using (var font = new Font(fontName, fontSize))
			using (var sf = new StringFormat())
			{
				sf.LineAlignment = sf.Alignment = StringAlignment.Center;
				sf.Trimming = StringTrimming.EllipsisCharacter;
				sf.FormatFlags = StringFormatFlags.NoWrap;
				// Draw rect for entry
				var rect = GetEntryRectForX(rectStartX);
				DrawRectF(pen, rect);
				// Draw the value centered in the rect
				g.DrawString(ToStr(val), font, brush, rect, sf);
			}
		}
		private void UndrawEntry(float rectStartX)
		{
			var rect = GetEntryRectForX(rectStartX);
			using (var brush = new SolidBrush(Colors.UndrawLog)) g.FillRectangle(brush, rect);
		}

		private RectangleF GetEntryRectForX(float x) => new RectangleF(x, startPoint.Y, EntryWidth, size.Height);
		private bool EntryInBounds(float x) => x + EntryWidth < size.Width;

		private void DrawTitle()
		{
			using (var font = new Font(fontName, fontSize))
			using (var sf = new StringFormat())
			{
				sf.LineAlignment = sf.Alignment = StringAlignment.Center;
				sf.Trimming = StringTrimming.EllipsisCharacter;
				sf.FormatFlags = StringFormatFlags.NoWrap;
				var rect = new RectangleF(startPoint, new SizeF(TitleSize.Width, size.Height));
				g.DrawString(title, font, Brushes.White, rect, sf);
			}
		}

		private string ToStr(T val)
		{
			string str = val.ToString();
			// replace Int.MinValue/MaxValue with "+inf"/"-inf"
			if (str.Equals(int.MaxValue.ToString())) str = "+inf";
			else if (str.Equals(int.MinValue.ToString())) str = "-inf";
			return str;
		}

		private void DrawRectF(Pen pen, RectangleF rectF)
		{
			// Somewhy "g.DrawRectangle()" wont accept the type "RectangleF"
			RectangleF[] rectFs = new RectangleF[] { rectF };
			g.DrawRectangles(pen, rectFs);
		}
	}
}
