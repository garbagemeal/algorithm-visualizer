using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms
{
	public partial class MainUIForm : Form
	{
		private Form activeForm = null;
		public Panel PanelLog { get { return panelLog; } }
		// Used for panelLog resize
		private int logMinHeight, logMaxHeight;
		private bool inResizeMode = false;
		// Avoid resize when visualizing
		public bool InVizMode { get; set; } = false;

		public MainUIForm()
		{
			InitializeComponent();
			// Open graph algo form as child
			OpenChildForm(new GraphAlgoForm(this));
			// Bound panelLog size between 2/10 and 1/2 of its initial size
			logMinHeight = (int)(panelLog.Height * 0.2f);
			logMaxHeight = (int)(panelLog.Height * 2f);
	}

	#region Child form
	private void OpenChildForm(Form childForm)
		{
			// Setup & open child from in this form
			if (activeForm != null)
				activeForm.Close();
			activeForm = childForm;
			childForm.TopLevel = false; // behave like a control
			childForm.FormBorderStyle = FormBorderStyle.None;
			childForm.Dock = DockStyle.Fill;
			panelChildForm.Controls.Add(childForm); // Add the form to list of controls in the container panel
			panelChildForm.Tag = childForm; // Asociate form with the container panel
			childForm.BringToFront();
			childForm.Show();
		}
		// Open child form
		private void btnArrayAlgos_Click(object sender, EventArgs e) => OpenChildForm(new ArrayAlgoForm(panelLog));
		private void btnMazeGenerator_Click(object sender, EventArgs e) => OpenChildForm(new MazeGenForm());
		private void btnGraphAlgos_Click(object sender, EventArgs e) => OpenChildForm(new GraphAlgoForm(this));
		#endregion

		#region panelLog resize
		private void panelLog_MouseUp(object sender, MouseEventArgs e)
		{
			// Quit reszie mode on left click release
			if (e.Button == MouseButtons.Left) inResizeMode = false;
		}
		private void panelLog_MouseDown(object sender, MouseEventArgs e)
		{
			// Enter resize mode on left click if not visualizing
			if (!InVizMode && e.Button == MouseButtons.Left) inResizeMode = true;
		}
		private void panelLog_MouseMove(object sender, MouseEventArgs e)
		{
			// If in resize mode
			if (inResizeMode)
			{
				// Resize panelLog
				int diff = 0 - e.Y;
				panelLog.Height = Math.Min(logMaxHeight, Math.Max(logMinHeight, panelLog.Height + diff));
			}
		}
		// Toggle method with callback to support cross thread operation
		public void ToggleWindowResizeAndMainMenuBtns() => ToggleWindowResizeAndMainMenuBtns(this);
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
				// Toggle Window resize
				FormBorderStyle sizeable = FormBorderStyle.Sizable, fixedSingle = FormBorderStyle.FixedSingle;
				FormBorderStyle = FormBorderStyle == sizeable ? fixedSingle : sizeable;
				// Get current 'Enabled' state of main menu buttons (assumed the state is
				// the same for all buttons in the main menu)
				bool currentState = btnArrayAlgos.Enabled;
				var buttons = new Button[] { btnArrayAlgos, btnMazeGenerator, btnGraphAlgos };
				// Toggle state foreach button
				foreach (var button in buttons) button.Enabled = !currentState;
			}
		}
	}
		#endregion


}
