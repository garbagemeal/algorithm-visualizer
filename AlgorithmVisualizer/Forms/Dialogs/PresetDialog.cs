using System;
using System.Drawing;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Presets;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class PresetDialog : Form
	{
		private int selectedPresetIdx = -1;
		
		private Preset[] presets;
		// Ref to graph, used to access the graph when saving it as a preset.
		private Graph graph;

		public PresetDialog(Graph _graph)
		{
			InitializeComponent();
			graph = _graph;
			SetupListView();
		}

		private void SetupListView()
		{
			listView.MultiSelect = false;
			listView.View = View.Details;
			listView.Columns.Add("Presets", 250);
			// auto resize col 0
			listView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
			PopulateListView();
		}
		private void PopulateListView()
		{
			presets = Preset.LoadAll();
			if (presets != null)
			{
				// Sort presets by timestamp; new presets appear last.
				Array.Sort(presets, (a, b) => a.timestamp.CompareTo(b.timestamp));
				// Load imgs for presets into 'imgs'
				ImageList imgs = new ImageList();
				imgs.ImageSize = new Size(200, 200);
				try
				{
					foreach (Preset preset in presets)
					{
                        // Get the abs path to the preset image and add it to the image list
                        string imgDir = preset.GetAbsImgDir();
						imgs.Images.Add(Image.FromFile(imgDir));
					}
				}
				catch(Exception e)
				{
					Console.WriteLine(e.Message);
				}
				listView.SmallImageList = imgs;
				
				// Populate listview
				for (int i = 0; i < presets.Length; i++)
					listView.Items.Add(presets[i].name, i);
			}
		}
		private void RepopulateListView()
		{
			listView.Clear();
			SetupListView();
		}

		private void listView_MouseClick(object sender, MouseEventArgs e)
		{
			if (listView.SelectedItems.Count > 0)
				selectedPresetIdx = listView.SelectedItems[0].Index;
		}
		private void btnSaveNewPreset_Click(object sender, EventArgs e)
		{
			// Avoid saving empty graphs
			if (!graph.IsEmpty())
			{
				// Get name for new preset and save it
				using (var newPresetDialog = new NewPresetDialog(graph))
				{
					newPresetDialog.StartPosition = FormStartPosition.CenterParent;
					if (newPresetDialog.ShowDialog() == DialogResult.OK)
					{
						SimpleDialog.ShowMessage("Save sucessful!",
							$"Created new preset named: \"{newPresetDialog.PresetName}\".");
						RepopulateListView();
					}
				}
			}
			else SimpleDialog.ShowMessage("Graph is empty!", "Saving empty graphs is not supported.");
		}
		private void btnRemovePreset_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count > 0)
			{
				if (SimpleDialog.OKCancel("Remove selected preset", "Press OK to proceed."))
				{
					// Remove preset by id
					string selectedPresetId = presets[listView.SelectedItems[0].Index].id;
					Preset.Remove(selectedPresetId);
					RepopulateListView();
				}
			}
		}

		private void btnLoadPreset_Click(object sender, EventArgs e)
		{
			if (0 <= selectedPresetIdx && selectedPresetIdx < presets.Length)
			{
				// Split serial that is possibly multiline into a string array by '\n' (empty lines included)
				string[] serialization = presets[selectedPresetIdx].serial.Split('\n');
				if (serialization != null)
				{
					graph.ClearGraph();
					GraphSerializer.Deserialize(graph, serialization);
					Close();
				}
				else SimpleDialog.ShowMessage("Error!", "Could not load preset!");
			}
			else SimpleDialog.ShowMessage("Error!", "Select a preset and then press load!");
		}
	}
}
