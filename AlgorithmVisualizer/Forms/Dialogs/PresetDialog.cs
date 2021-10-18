using System;
using System.Drawing;
using System.Windows.Forms;

using AlgorithmVisualizer.DBHandler;
using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.Utils;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class PresetDialog : Form
	{
		public string[] Serialization { get; set; } = null;
		
		private Preset[] presets;
		// Ref to graph, used to access the graph when saving it as a preset.
		private Graph graph;

		public PresetDialog(Graph _graph)
		{
			InitializeComponent();
			graph = _graph;
		}
		private void PresetDialog_Load(object sender, EventArgs e)
		{
			listView.MultiSelect = false;
			listView.View = View.Details;
			listView.Columns.Add("Presets", 250);
			// auto resize col 0
			listView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
			PopulateListView();
		}
		private void RepopulateListView(object sender, EventArgs e)
		{
			// Repopulate ListView
			listView.Clear();
			PresetDialog_Load(sender, e);
		}
		private void PopulateListView()
		{
			DBConnection db = DBConnection.GetInstance();
			if (db.Connect())
			{
				// Connect to DB and get all stored presets
				presets = db.GetAllPresets();
				db.Disconnect();
				// If there are no presets, nothing to load
				if (presets != null)
				{
					// Load imgs for presets into 'imgs'
					ImageList imgs = new ImageList();
					imgs.ImageSize = new Size(200, 200);
					try
					{
						foreach (Preset preset in presets)
						{
							// Get abs path to image of the preset
							string imgDir = preset.GetAbsoluteDir();
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
						listView.Items.Add(presets[i].Name, i);
				}
			}
		}
		
		private void listView_MouseClick(object sender, MouseEventArgs e)
		{
			if (listView.SelectedItems.Count > 0)
			{
				int selectedItem = listView.SelectedItems[0].Index;
				// Split serial that is possibly multiline into a string array by '\n'
				// Empty lines included!
				Serialization = presets[selectedItem].Serial.Split('\n');
			}
		}
		private void btnSaveNewPreset_Click(object sender, EventArgs e)
		{
			// Avoid saving empty presets
			if (!graph.IsEmpty())
			{
				// Show dialog for new preset name
				using (var newPresetDialog = new NewPresetDialog())
				{
					newPresetDialog.StartPosition = FormStartPosition.CenterParent;
					if (newPresetDialog.ShowDialog() == DialogResult.OK)
					{
						// Save this graph's preset with given name (serialize it)
						string presetName = newPresetDialog.PresetName;
						if (presetName != "") GraphSerializer.Serialize(graph, presetName);
						else Console.WriteLine("Preset name cannot be empty, aborting save...");
						RepopulateListView(sender, e);
					}
				}
			}
			else Console.WriteLine("The graph is empty(0 nodes), aboring save...");
		}
		private void btnRemovePreset_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count > 0)
			{
				string title = "Remove selected preset", text = "Press OK to proceed.";
				if (SimpleDialog.OKCancel(title, text))
				{
					// Find id of preset for removal
					int selectedItem = listView.SelectedItems[0].Index,
						selectedPresetId = presets[selectedItem].Id;
					// Remove preset via id from DB
					DBConnection db = DBConnection.GetInstance();
					if (db.Connect())
					{
						// If removed, repopulate listView
						if (db.RemovePreset(selectedPresetId)) RepopulateListView(sender, e);
						// Otherwise show error msg
						else Console.WriteLine($"Failed to remove preset with ID: {selectedPresetId}");
						db.Disconnect();
					}
				}
			}
		}
		
	}
}
