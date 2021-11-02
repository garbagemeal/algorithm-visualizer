using System;
using static System.Math;
using System.Drawing;

using AlgorithmVisualizer.Threading;

namespace AlgorithmVisualizer.Arrays
{
	
	public class ArrayVisualizer : PauseResumeSleep
	{
		protected int[] arr;
		protected Graphics g;
		// Min/Max possible values (max height of entry)
		private readonly int minVal = 5;
		protected int maxVal;
		protected double entryWidth;
		protected static Random rnd = new Random();

		public ArrayVisualizer(int[] _arr, Graphics _g, int _maxVal, double _entryWidth, bool sortedFlag)
		{
			arr = _arr;
			g = _g;
			maxVal = _maxVal;
			entryWidth = _entryWidth;
			// Note that delayFactor is tomporairly adjusted to 0
			// for the array the be randomized with no delays
			float tmp = delayFactor;
			delayFactor = 0;
			if (sortedFlag) SortedFillArray();
			else RndFillArray(); 
			delayFactor = tmp;
		}

		private void RndFillArray()
		{
			// Randomly fill arr
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = rnd.Next(minVal, maxVal);
				DrawValueAt(i);
			}
			
		}
		private void SortedFillArray()
		{
			// Randomly fill arr but keep it sorted
			int N = arr.Length, prev = minVal;
			for(int i = 0; i < N; i++)
			{
				// As i gets bigger so does maxPossibleVal but no less then 5 and no bigger
				// then maxVal (+1 becuase rnd.Next does not include the upper bound)
				int maxPossibleVal = Min((int)((double)i / N * maxVal + minVal + 1), maxVal);
				arr[i] = rnd.Next(prev, maxPossibleVal);
				prev = arr[i];
				DrawValueAt(i);
			}
		}

		protected void UndrawValueAt(int idx)
		{
			// Undraw value at given idx
			g.FillRectangle(Brushes.Black, (int)Math.Ceiling(idx * entryWidth), 0, (int)Math.Ceiling(entryWidth), maxVal);
		}
		protected void DrawValueAt(int idx)
		{
			// draw value at given idx
			DrawValueAt(idx, arr[idx]);
		}
		protected void DrawValueAt(int idx, int val)
		{
			// draw given value at given idx
			DrawValueAt(idx, val, Brushes.White);
		}
		protected void DrawValueAt(int idx, Brush brush)
		{
			// Draw value at given idx with given brush
			DrawValueAt(idx, arr[idx], brush);
		}
		protected void DrawValueAt(int idx, int val, Brush brush)
		{
			// Fill old value in black ("remove" old value)
			UndrawValueAt(idx);

			// Draw the new value in white ("create" new value)
			g.FillRectangle(brush, (int)Math.Ceiling(idx * entryWidth), maxVal - val, (int)Math.Ceiling(entryWidth), maxVal);
			// Draw border around the visualized value (if entry width is greater then 5px)
			if ((int)Math.Ceiling(entryWidth) >= 5)
				g.DrawRectangle(Pens.Black, (int)Math.Ceiling(idx * entryWidth), maxVal - val, (int)Math.Ceiling(entryWidth) - 1, maxVal - 1);
			Sleep(Delay.Short);
		}
		protected void DrawValues(int from, int to)
		{
			// Draw values starting from from to to with white brush
			DrawValues(from, to, Brushes.White);
		}
		protected void DrawValues(int from, int to, Brush brush)
		{
			// Draw values starting from from to to with given brush
			// Note that some is tomporairly adjusted to 0
			// for the array the be randomized with no delays
			if (from <= to)
			{
				float tmp = delayFactor;
				delayFactor = 0;
				for (int i = from; i <= to; i++)
					DrawValueAt(i, brush);
				delayFactor = tmp;
				Sleep(Delay.Short);
			}
		}
	}
}
