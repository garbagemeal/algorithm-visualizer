using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class StartEndNodeDialog : Form
	{
		public int From { get; set; } = -1;
		public int To { get; set; } = -1;
		// true if user perssed 'OK' and input was valid
		public bool InputIsValid { get; private set; } = false;

		// bool flag indicating if id for destination node id is a required input
		private bool includeTo;
		private Graph graph;

		public StartEndNodeDialog(Graph _graph, bool _includeTo)
		{
			InitializeComponent();
			graph = _graph;
			includeTo = _includeTo;
			// If id of node 'to' not needed hide related stuff
			if (!includeTo) lblTo.Visible = textBoxTo.Visible = false;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (ParseTextBoxes())
			{
				InputIsValid = true;
				Close();
			}
		}
		private bool GivenInputIsValid() => graph.ContainsNode(From) && (!includeTo || graph.ContainsNode(To));
		private bool ParseTextBoxes()
		{
			// Gets user input and returns true if valid, false otherwise
			try
			{
				From = Int32.Parse(textBoxFrom.Text);
				if (includeTo) To = Int32.Parse(textBoxTo.Text);
				if (GivenInputIsValid()) return true;
			}
			catch (FormatException ex)
			{
				Console.WriteLine("Failed parsing start/end nodes");
				Console.WriteLine(ex);
			}
			SimpleDialog.ShowMessage("Invalid input", "Invalid start/end node id(s).");
			return false;
		}
	}
}
