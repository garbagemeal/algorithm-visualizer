using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using AlgorithmVisualizer.DataStructures.BinaryTree;
using AlgorithmVisualizer.Forms.Dialogs;

namespace AlgorithmVisualizer.Forms
{
	public partial class TreeAlgoForm : Form
	{
		// Work in progress...

		private MainUIForm parentForm;
		private BinarySearchTree<int> BST;

		public TreeAlgoForm(MainUIForm _parentForm)
		{
			InitializeComponent();

			parentForm = _parentForm;
			BST = new BinarySearchTree<int>();
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{

		}
		private void btnReset_Click(object sender, System.EventArgs e)
		{
			BST = new BinarySearchTree<int>();
			Refresh();
		}
		private void btnPauseResume_Click(object sender, System.EventArgs e)
		{

		}
		private void btnAddNode_Click(object sender, System.EventArgs e)
		{
			string errTitle = "Failed to add a node!";
			int? val = ParseUserInput();
			if (val != null)
			{
				if (!BST.Add((int)val)) SimpleDialog.ShowMessage(errTitle, $"A node with the value '{val}' is already present!");
				else panelMain.Refresh();
			}
			else SimpleDialog.ShowMessage(errTitle, "Failed to parse the given input");
			ClearAndFocusTxtBox(txtBoxNodeValue);
		}
		private void btnRemoveNode_Click(object sender, System.EventArgs e)
		{
			string errTitle = "Failed to remove the node!";
			int? val = ParseUserInput();
			if (val != null)
			{
				if (!BST.Remove((int)val)) SimpleDialog.ShowMessage(errTitle, $"A node with the value '{val}' is not present!");
				else panelMain.Refresh();
			}
			else SimpleDialog.ShowMessage(errTitle, "Failed to parse the given input");
			ClearAndFocusTxtBox(txtBoxNodeValue);
		}
		private int? ParseUserInput()
		{
			// Try parse and return user input, if failed return null
			try
			{
				return Int32.Parse(txtBoxNodeValue.Text);
			}
			catch (FormatException)
			{
				return null;
			}
		}
		private void ClearAndFocusTxtBox(TextBox txtBox)
		{
			txtBox.Clear();
			txtBox.Focus();
		}
		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{

		}

		private void panelMain_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			TreeVisualizer<int>.DrawTree(BST.Root, e.Graphics, panelMain);
		}
	}
}
