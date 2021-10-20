using System;
using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class VertexDialog : Form
	{
		public int Id { get; set; }
		public int Data { get; set; }

		public VertexDialog() => InitializeComponent();

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				Id = Int32.Parse(textBoxID.Text);
			}
			catch (FormatException)
			{
				// invalid id
				Id = -1;
			}
		}
	}
}
