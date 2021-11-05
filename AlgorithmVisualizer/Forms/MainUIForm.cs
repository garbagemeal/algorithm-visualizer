using AlgorithmVisualizer.GraphTheory.Utils;
using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms
{
	public partial class MainUIForm : Form
	{
		private Form activeChildForm = null;
		public Panel PanelLog { get { return panelLog; } }
		private readonly int logMinHeight, logMaxHeight;
		private bool inResizeMode = false;
		// Used to avoid panel log resize when visualizing
		public bool inGraphAlgoViz { get; set; } = false;

		public MainUIForm()
		{
			InitializeComponent();
			// Open graph algo form as child
			OpenChildForm(new GraphAlgoForm(this));
			// Bound panelLog size between 2/10 and 2 of its initial size
			logMinHeight = (int)(panelLog.Height * 0.2f);
			logMaxHeight = (int)(panelLog.Height * 2f);
	}

		#region Child form
		private void OpenChildForm(Form childForm)
		{
			// Setup & open child from in this form
			if (activeChildForm != null)
				activeChildForm.Close();
			activeChildForm = childForm;
			childForm.TopLevel = false; // behave like a control
			childForm.FormBorderStyle = FormBorderStyle.None;
			childForm.Dock = DockStyle.Fill;
			panelChildForm.Controls.Add(childForm); // Add the form to list of controls in the container panel
			panelChildForm.Tag = childForm; // Asociate form with the container panel
			childForm.BringToFront();
			childForm.Show();
			panelLog.CreateGraphics().Clear(Colors.UndrawLog); // if case there are left overs in logging panel
		}
		// Open child form
		private void btnArrayAlgos_Click(object sender, EventArgs e) => OpenChildForm(new ArrayAlgoForm(panelLog));
		private void btnMazeGenerator_Click(object sender, EventArgs e) => OpenChildForm(new MazeGenForm(this));
		private void btnGraphAlgos_Click(object sender, EventArgs e) => OpenChildForm(new GraphAlgoForm(this));
		private void btnTreeAlgos_Click(object sender, EventArgs e) => OpenChildForm(new TreeAlgoForm(this));
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
			if (!inGraphAlgoViz && e.Button == MouseButtons.Left) inResizeMode = true;
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
		#endregion

		#region Threading
		// Thread safe main menu bttons toggle func
		public void ToggleMainMenuBtns() => ToggleMainMenuBtns(this);
		private delegate void ToggleMainMenuBtnsCallback(Control control);


		private void ToggleMainMenuBtns(Control control)
		{
			if (control.InvokeRequired)
				Invoke(new ToggleMainMenuBtnsCallback( ToggleMainMenuBtns), new object[] { control });
			else
			{
				// Toggle enabled state for main menu buttons
				var buttons = new Button[] { btnArrayAlgos, btnMazeGenerator, btnGraphAlgos };
				foreach (var button in buttons) button.Enabled = !button.Enabled;
			}
		}
		//// Thread safe window resize toggle func
		//public void ToggleWindowResize() => ToggleWindowResize(this);
		//private delegate void ToggleWindowResizeCallback(Control control);
		//private void ToggleWindowResize(Control control)
		//{
		//	if (control.InvokeRequired)
		//		Invoke(new ToggleWindowResizeCallback( ToggleMainMenuBtns), new object[] { control });
		//	else
		//	{
		//		// Toggle Window resize
		//		MaximizeBox = !MaximizeBox;
		//		FormBorderStyle sizeable = FormBorderStyle.Sizable, fixedSingle = FormBorderStyle.FixedSingle;
		//		FormBorderStyle = FormBorderStyle == sizeable ? fixedSingle : sizeable;
		//	}
		//}
		#endregion
	}


}
