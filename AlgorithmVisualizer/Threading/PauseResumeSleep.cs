using System;
using System.Threading;

namespace AlgorithmVisualizer.Threading
{
	public abstract class PauseResumeSleep
	{
		// Basic threading functionality. Idea soruce:
		// https://stackoverflow.com/questions/142826/is-there-a-way-to-indefinitely-pause-a-thread

		private ManualResetEvent pauseEvent = new ManualResetEvent(true);
		public bool Paused { get; set; } = false;
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

		// DelayFactor can range from 0-2 (0 is faster), and is given by user at runtime.
		public double DelayFactor { get; protected set; } = 1;
		public void Sleep(int millis)
		{
			// If not paused and computed sleep time > 0
			if (!Paused && DelayFactor * millis > 0)
				Thread.Sleep((int)Math.Round(DelayFactor * millis));
			// Check for pause event
			CheckForPause();
		}

		public void SetDelayFactor(int speed, int min, int max)
		{
			// min/max denote the min/max possible given speed, i.e, the min/max possible
			// values in a scroll bar

			// "Reverse" speed such that higher speed --> higher DelayFactor
			int delayTime = Math.Abs(speed - max);
			// Scaling delayTime in the domain [0 - 2] and storing in newDelayFactor
			const double MIN_FACTOR = 0, MAX_FACTOR = 2;
			double newDelayFactor = delayTime * ((MAX_FACTOR - MIN_FACTOR) / (max - min));
			// Update DealyFactor
			DelayFactor = newDelayFactor;
			Console.WriteLine("DelayFactor update: " + newDelayFactor);
		}
	}
}
