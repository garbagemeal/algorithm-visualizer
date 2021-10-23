using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using AlgorithmVisualizer.Arrays;
using AlgorithmVisualizer.Forms.Dialogs.AlgoDetails;

namespace AlgorithmVisualizer.Forms
{
	public partial class ArrayAlgoForm : Form
	{
		// Number of entries in the array to, may change
		private int numEntries = 100;
		// Graphics object for the panel
		private Graphics g;
		ArrayVisualizer arrayVisualizer;
		// Stuff for algoComboBox
		private int agoNameIdx;
		private readonly string[] algoNames = { "InsertionSort", "SelectionSort", "BubbleSort",
			"MergeSort", "QuickSortLomuto", "QuickSortHoare", "HeapSort", "CountingSort",
			"RadixSort", "IntroSort", "BinarySearch", "TernarySearch", "ExponentialSearch" };
		// flag indicating if created array should be sorted
		private bool sortedArrayFlag = false;
		// Background worker to run the sorting algo on
		private BackgroundWorker bgw;
		// Refs to panelLog and its graphics object
		public static Panel panelLog;
		public static Graphics panelLogG;

		public ArrayAlgoForm(Panel _panelLog)
		{
			InitializeComponent();
			// Adding all algo names from algorithmNames into the algoComboBox
			foreach (string algoName in algoNames)
				algoComboBox.Items.Add(algoName);
			algoComboBox.SelectedIndex = agoNameIdx = 0;

			panelLog = _panelLog;
			panelLogG = panelLog.CreateGraphics();
		}

		private void btnViz_Click(object sender, EventArgs e)
		{
			// If arrayVisualizer is null then the panel has not yet been reset
			if (arrayVisualizer == null)
			{
				ResetPanel();
				Thread.Sleep(700);
			}
			// Create bgw, assign work and runn in async mode
			bgw = new BackgroundWorker { WorkerSupportsCancellation = true };
			bgw.DoWork += new DoWorkEventHandler(bgw_Visualize);
			bgw.RunWorkerAsync();
		}
		private void bgw_Visualize(object sender, DoWorkEventArgs e)
		{
			// The code to be executed by the background worker

			// Selecting the method(algorithm) by name & argument count (0).
			MethodInfo mInfo = arrayVisualizer.GetType().GetMethods().FirstOrDefault
				(method => method.Name == algoNames[agoNameIdx]
				&& method.GetParameters().Count() == 0);

			// Enable Pause/Resume and disable btnSort, btnReset, arrSizeBar, algoComboBox controls while visualizing
			Control[] controls = new Control[] { btnPauseResume, btnViz, btnReset, arrSizeBar, algoComboBox };
			foreach (Control control in controls) SetControlEnabled(control, control == btnPauseResume);

			// Invoking the selected method
			mInfo.Invoke(arrayVisualizer, null);
			Console.WriteLine("Invoked arrayVisualizer with: \n" + mInfo.ToString());

			// Disable Pause/Resume and enable btnSort, btnReset, arrSizeBar, algoComboBox controls after visualizing
			foreach (Control control in controls) SetControlEnabled(control, control != btnPauseResume);
		}
		// Helper method & callback to update the Enabled prop of a given control
		private delegate void SetControlEnabledCallback(Control control, bool enabled);
		private void SetControlEnabled(Control control, bool enabled)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (control.InvokeRequired)
			{
				SetControlEnabledCallback d = new SetControlEnabledCallback(SetControlEnabled);
				Invoke(d, new object[] { control, enabled });
			}
			else control.Enabled = enabled;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			// Reset button even handler
			ResetPanel();
		}
		private void ResetPanel()
		{
			// Resetting the panel and the array and creating a new
			// isntance of a SortEngine with the new panel and array
			g = panelMain.CreateGraphics();
			double entryWidth = (double)panelMain.Width / numEntries;
			int maxVal = panelMain.Height;
			int[] arr = new int[numEntries];

			// init panel background to black (clear all values)
			g.FillRectangle(new SolidBrush(Color.Black), 0, 0, panelMain.Width, maxVal);
			// Creating the arrayVisualizer, upon creation will draw the array's values.
			arrayVisualizer = new ArrayAlgorithms(arr, g, maxVal, entryWidth, sortedArrayFlag);
			UpdateDelayFactor();
		}
		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			agoNameIdx = algoComboBox.SelectedIndex;
			// swapped from an algo requiring a sorted array
			// to one that doesn't or vice versa
			bool newSortedFlagStatus = agoNameIdx >= 10;
			if (sortedArrayFlag != newSortedFlagStatus)
			{
				// update sortedArrayFlag
				sortedArrayFlag = newSortedFlagStatus;
				// Recreate visualizer (with a new array)
				ResetPanel();
			}
		}
		private void UpdateDelayFactor() => arrayVisualizer.DelayFactor = speedBar.Value;
		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{
			if (arrayVisualizer != null) UpdateDelayFactor();
		}
		private void arrSizeBar_Scroll(object sender, ScrollEventArgs e)
		{
			// Update size of array to be created
			// Limit array size via panelMain.Width
			numEntries = Math.Min(arrSizeBar.Value, panelMain.Width);
			Console.WriteLine("numEntries: " + numEntries);
			ResetPanel();
		}
		private void btnPauseResume_Click(object sender, EventArgs e)
		{
			// Note that this button is only enabled during a visualization!

			// Event handler for the Pause/Resume button, will pause or resume
			// the visualization depending on the current state of the visualization 
			if (arrayVisualizer.Paused)
			{
				btnPauseResume.Text = "Pause";
				arrayVisualizer.Resume();
			}
			else
			{
				btnPauseResume.Text = "Resume";
				arrayVisualizer.Pause();
			}
		}
		private void btnDetails_Click(object sender, EventArgs e)
		{
			// Show the details for the selected algo
			string algoName = algoNames[agoNameIdx];
			// in case the selected algo is quick sort then adjust the name
			if (algoName.StartsWith("QuickSort")) algoName = "QuickSort";
			string fileName = algoName + ".xml",
				fileDir = @"Forms\Dialogs\AlgoDetails\xml\Arrays\" + fileName,
				filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
				.Parent.FullName, fileDir);
			using (var detailsDialog = new DetailsDialog(filePath))
			{
				detailsDialog.StartPosition = FormStartPosition.CenterParent;
				detailsDialog.ShowDialog();
			}
		}
	}
}
