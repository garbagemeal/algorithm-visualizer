using System.Threading;

namespace AlgorithmVisualizer.Threading
{
	public abstract class PauseResume
	{
		// Threading functions to allow pasuing/resuming functionality
		// Idea soruce: https://stackoverflow.com/questions/142826/is-there-a-way-to-indefinitely-pause-a-thread

		private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
		private bool paused = false;
		public bool Paused { get { return paused; } set { paused = value; } }

		// Methods to pause or resume the sorting
		// The pause occours after drawing a value(bar) if event is triggered
		public void Pause()
		{
			// Causes CheckForPause to pause 
			_pauseEvent.Reset();
			paused = true;
		}
		public void Resume()
		{
			// Causes CheckForPause to resume
			_pauseEvent.Set();
			paused = false;
		}
		protected void CheckForPause()
		{
			// If the pause event has been triggered then pause, otherwise return.
			// If paused then will wait indefinitely untill Resume function called.
			// Use this method in places where pausing is allowed
			_pauseEvent.WaitOne(Timeout.Infinite);
		}

	}
}
