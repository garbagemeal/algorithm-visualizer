using System;
using System.Windows.Forms;

using AlgorithmVisualizer.Forms;
using AlgorithmVisualizer.DataStructures.BinaryTree;

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
			Application.Run(new MainUIForm()); // Run the main UI form

			// RunTests();
		}
		static void RunTests()
		{
			// Testing & Debugging
			BinaryTreeTests.RunTests(); // Test BST and TreeUtils
		}
	}
}
