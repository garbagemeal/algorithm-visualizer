using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class EdgeDialog : Form
	{
		private Graph graph;
		private int from, to, cost;

		// The mode in which this dialog is opened in; addition or removal of an edge
		public enum DialogMode { AddEdge = 0, RemoveEdge = 1 }
		// Possible types of edges to add/remove
		private enum EdgeType { Directed = 0, Undirected = 1 }
		private DialogMode dialogMode;
		private EdgeType edgeType;

		public EdgeDialog(int _from, Graph _graph, DialogMode _dialogMode)
		{
			InitializeComponent();

			from = _from;
			graph = _graph;
			dialogMode = _dialogMode;

			Text = dialogMode == DialogMode.AddEdge ? "Add an edge" : "Remove an edge";
			textBoxFrom.Text = from.ToString();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			ParseUserInput();
			if (dialogMode == DialogMode.AddEdge) AddEdge();
			else RemoveEdge();
		}

		private void AddEdge()
		{
			// Add directed or undirected edge and get op status
			bool opStatus = edgeType == EdgeType.Directed ?
				graph.AddDirectedEdge(from, to, cost) :
				graph.AddUndirectedEdge(from, to, cost);
			// Custumize and print msg
			string msg = (edgeType == EdgeType.Directed ? "AddDirectedEdge" : "AddUndirectedEdge") +
					  $": ({from}, {to}, {cost}). " + (opStatus ? "Success" : "Failed");
			Console.WriteLine(msg);
		}
		private void RemoveEdge()
		{
			// Remove directed or undirected edge and get op status
			bool opStatus = edgeType == EdgeType.Directed ?
				graph.RemoveDirectedEdge(from, to, cost) :
				graph.RemoveUndirectedEdge(from, to, cost);
			// Custumize and print msg
			string msg = (edgeType == EdgeType.Directed ? "RemoveDirectedEdge" : "RemoveUndirectedEdge") +
					  $": ({from}, {to}, {cost}). " + (opStatus ? "Success" : "Failed");
			Console.WriteLine(msg);
		}

		private void ParseUserInput()
		{
			try
			{
				to = Int32.Parse(textBoxTo.Text);
				cost = Int32.Parse(textBoxCost.Text);
				edgeType = radioBtnDirected.Checked ? EdgeType.Directed : EdgeType.Undirected;
			}
			catch (FormatException)
			{
				// -1 denotes invalid input
				from = to = cost = -1;
			}
		}
	}
}
