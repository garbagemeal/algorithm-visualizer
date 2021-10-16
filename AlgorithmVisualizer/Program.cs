using System;
using System.Windows.Forms;

using AlgorithmVisualizer.Forms;

namespace AlgorithmVisualizer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Run the main UI form
			Application.Run(new MainUIForm());

			//RunTests();
		}
		static void RunTests()
		{
			// Testing & Debugging
			// TreeTester.RunTests(); // Test BST and TreeUtils
		}
	}
}
