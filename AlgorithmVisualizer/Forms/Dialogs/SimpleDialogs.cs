using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	static class SimpleDialog
	{
		public static bool OKCancel(string title, string text)
		{
			// Confirmation dialog
			var dialogResult = MessageBox.Show(text, title, MessageBoxButtons.OKCancel);
			return dialogResult == DialogResult.OK;
		}
		public static void ShowMessage(string title, string text)
		{
			// Notice dialog
			MessageBox.Show(text, title, MessageBoxButtons.OK);
		}
	}
}
