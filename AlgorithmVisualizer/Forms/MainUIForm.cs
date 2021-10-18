using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms
{
	public partial class MainUIForm : Form
	{
		private Form activeForm = null;
		public Panel PanelLog { get { return panelLog; } }

		public MainUIForm()
		{
			InitializeComponent();
			// Start with FDGV form (graph algos)
			OpenChildForm(new GraphAlgoForm(this));
		}

		#region Child form
		private void OpenChildForm(Form childForm)
		{
			// Method to open a form as a child from for this form
			if (activeForm != null)
				activeForm.Close();
			activeForm = childForm;
			childForm.TopLevel = false; // behave like a control
			childForm.FormBorderStyle = FormBorderStyle.None;
			childForm.Dock = DockStyle.Fill;
			panelChildForm.Controls.Add(childForm); // Add the form to list of controls in the container panel
			panelChildForm.Tag = childForm; // Asociate form with the container panel
			childForm.BringToFront(); // For logo bring the form to the front
			childForm.Show();
		}

		// Opening a child form via 1 of the 3 following methods
		private void btnArrayAlgos_Click(object sender, EventArgs e)
		{
			OpenChildForm(new ArrayAlgoForm(panelLog));
		}
		private void btnMazeGenerator_Click(object sender, EventArgs e)
		{
			OpenChildForm(new MazeGenForm());
		}
		private void btnGraphAlgos_Click(object sender, EventArgs e)
		{
			OpenChildForm(new GraphAlgoForm(this));
		}
		#endregion

		#region Resizing panelLog via user
		private bool inResizeMode = false;
		private bool inVizMode = false;
		public bool InVizMode { set { inVizMode = value; } }

		private void panelLog_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) inResizeMode = false;
		}
		private void panelLog_MouseDown(object sender, MouseEventArgs e)
		{
			// Prohibit resizing panelLog if a visualization is running
			if (!inVizMode && e.Button == MouseButtons.Left) inResizeMode = true;
		}
		private void panelLog_MouseMove(object sender, MouseEventArgs e)
		{
			// Bounding panelLog size to 1/10 - 1/2 of the form size
			if (inResizeMode)
			{
				int panelLogMinHeight = (int)(Height * 0.2f),
					panelLogMaxHeight = (int)(Height * 0.5f);
				int diff = 0 - e.Y;
				//panelLog.Height += diff;
				panelLog.Height = Math.Min(panelLogMaxHeight, Math.Max(panelLogMinHeight, panelLog.Height + diff));
			}
		}
		#endregion


		public void ToggleWindowResizeAndMainMenuBtns()
		{
			ToggleWindowResizeAndMainMenuBtns(this);
		}
		private delegate void ToggleWindowResizeAndMainMenuBtnsCallback(Control control);
		private void ToggleWindowResizeAndMainMenuBtns(Control control)
		{
			if (control.InvokeRequired)
			{
				Invoke(new ToggleWindowResizeAndMainMenuBtnsCallback(
					ToggleWindowResizeAndMainMenuBtns), new object[] { control });
			}
			else
			{
				// Toggle the maximize button
				MaximizeBox = !MaximizeBox;
				// Toggle Window resize via mouse drag, note that using FixedSingle causes
				// visuals to disappear for some reason, thats why Fixed3D is used.
				FormBorderStyle sizeable = FormBorderStyle.Sizable, fixedSingle = FormBorderStyle.FixedSingle;
				FormBorderStyle = FormBorderStyle == sizeable ? fixedSingle : sizeable;

				// Get current 'Enabled' status of main menu buttons (assumed the state is
				// the same for all buttons in the main menu)
				bool currentState = btnArrayAlgos.Enabled;
				var buttons = new Button[] { btnArrayAlgos, btnMazeGenerator, btnGraphAlgos };
				// Toggle state foreach button
				foreach (var button in buttons) button.Enabled = !currentState;
			}
		}
	}
}
