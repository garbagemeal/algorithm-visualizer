using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class EdgeDialog : Form
	{
		public int To { get; set; }
		public int Cost { get; set; }
		// true for an undirected edge, false for a directed edge
		public bool AddingMode { get; set; }
		public bool DirectedMode { get; set; }

		public EdgeDialog(bool addingMode = true)
		{
			// If costInvalid is true then an edge is being removed and therefore the cost invalid
			InitializeComponent();
			AddingMode = addingMode;
			// Adjust window title according to op type
			Text = AddingMode ? "Add an edge" : "Remove an edge";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				To = Int32.Parse(textBoxTo.Text);
				Cost = Int32.Parse(textBoxCost.Text);
				DirectedMode = radioBtnDirected.Checked;
			}
			catch (FormatException)
			{
				// -1 denotes invalid input
				To = Cost = -1;
			}
		}
	}
}
