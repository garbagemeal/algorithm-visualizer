using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class StartEndNodeDialog : Form
	{
		public int From { get; set; }
		public int To { get; set; }
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
			ParseTextBoxes();
			if (graph.ContainsNode(From) && (!includeTo || graph.ContainsNode(To)))
			{
				InputIsValid = true;
				Close();
			}
			else SimpleDialog.ShowMessage("Invalid input", "Invalid start/end node id(s).");
		}
		private void ParseTextBoxes()
		{
			try
			{
				From = Int32.Parse(textBoxFrom.Text);
				if (includeTo) To = Int32.Parse(textBoxTo.Text);
			}
			catch (FormatException ex)
			{
				From = To = -1;
			}
		}
	}
}
