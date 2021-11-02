using AlgorithmVisualizer.Utils;
using System;
using System.Threading;

namespace AlgorithmVisualizer.Threading
{
	public abstract class PauseResumeSleep
	{
		// Basic threading functionality. Idea soruce:
		// https://stackoverflow.com/questions/142826/is-there-a-way-to-indefinitely-pause-a-thread

		private readonly ManualResetEvent pauseEvent = new ManualResetEvent(true);
		public bool Paused { get; private set; } = false;
		public void Pause()
		{
			// Causes CheckForPause to pause 
			pauseEvent.Reset();
			Paused = true;
		}
		public void Resume()
		{
			// Causes CheckForPause to resume
			pauseEvent.Set();
			Paused = false;
		}
		protected void CheckForPause()
		{
			// If the pause event has been triggered then pause, otherwise return.
			// If paused then will wait indefinitely until Resume function called.
			// Use this method in places where pausing is allowed
			pauseEvent.WaitOne(Timeout.Infinite);
		}

		// delayFactor is in the domain[0, 2] (0 is faster), user input assumed in the
		// domain[0, 100], and is scaled to match the delayFactor domain.
		private static readonly Range rangeIn = new Range(0, 100), rangeOut = new Range(0, 2);
		// To allow setting without scaling delayFactor is protected.
		protected float delayFactor = 1;
		public float DelayFactor
		{
			get { return delayFactor; }
			set
			{
				float val = Math.Abs(value - rangeIn.Max);
				delayFactor = Range.Scale(val, rangeIn, rangeOut);
				Console.WriteLine($"delay factor: {delayFactor}");
			}
		}

		public enum Delay {

			Tiny = 50,
			VeryShort = 250,
			Short = 500,
			Medium = 1000,
			Long = 1500,
			VeryLong = 2500,
			Huge = 5000
		}
		public void Sleep(Delay delay)
		{
			int millis = (int)delay;
			// Sleep some time if millies > 0
			if (!Paused && DelayFactor * millis > 0)
				Thread.Sleep((int)Math.Round(DelayFactor * millis));
			// Check for pause event
			CheckForPause();
		}
	}
}
