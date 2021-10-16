using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class StartEndNodeDialog : Form
	{
		public int From { get; set; }
		public int To { get; set; }
		private bool _includeTo;
		public StartEndNodeDialog(bool includeTo = true)
		{
			InitializeComponent();
			_includeTo = includeTo;
			// To may not be needed
			if (!_includeTo)
			{
				lblTo.Visible = textBoxTo.Visible = false;
				To = -1;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				From = Int32.Parse(textBoxFrom.Text);
				// End may not be needed
				if (_includeTo) To = Int32.Parse(textBoxTo.Text);
			}
			catch (FormatException)
			{
				// -1 denotes an invalid start/end node ids
				From = To = -1;
			}
		}
	}
}
