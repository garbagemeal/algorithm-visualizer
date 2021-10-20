using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class NewPresetDialog : Form
	{
		public string PresetName { get; set; }
		private Graph graph;

		public NewPresetDialog(Graph _graph)
		{
			InitializeComponent();
			graph = _graph;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			// Get name for preset
			PresetName = textBoxName.Text;
			// Serialize if name non empty/null
			if (string.IsNullOrEmpty(PresetName))
			{
				SimpleDialog.ShowMessage("Name is empty", "Empty graph preset names is not supported!");
				btnOK.DialogResult = DialogResult.No; // default result is "DialogResult.OK"
			}
			else GraphSerializer.Serialize(graph, PresetName);
		}
	}
}
