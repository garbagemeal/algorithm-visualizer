using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class NewPresetDialog : Form
	{
		public string PresetName { get; set; }

		public NewPresetDialog() => InitializeComponent();

		// Assign name for preset
		private void btnOK_Click(object sender, EventArgs e) => PresetName = textBoxName.Text;
	}
}
