using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlgorithmVisualizer.DataStructures.AVLTree;
using AlgorithmVisualizer.DataStructures.BinaryTree;
using AlgorithmVisualizer.Forms.Dialogs;

namespace AlgorithmVisualizer.Forms
{
	public partial class TreeAlgoForm : Form
	{
		private MainUIForm parentForm;
		private BinarySearchTree<int> BST;

		public TreeAlgoForm(MainUIForm _parentForm)
		{
			InitializeComponent();

			parentForm = _parentForm;
			algoComboBox.SelectedIndex = 1;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{

		}
		private void btnReset_Click(object sender, EventArgs e)
		{
			InitNewBST();
			canvas.Refresh();
		}
		private void btnPauseResume_Click(object sender, EventArgs e)
		{

		}
		private void btnAddNode_Click(object sender, EventArgs e)
		{
			string errTitle = "Failed to add a node!";
			int? val = ParseUserInput();
			if (val != null)
			{
				if (!BST.Add((int)val)) SimpleDialog.ShowMessage(errTitle, $"A node with the value '{val}' is already present!");
				else canvas.Refresh();
			}
			else SimpleDialog.ShowMessage(errTitle, "Failed to parse the given input");
			ClearAndFocusTxtBox(txtBoxNodeValue);
		}
		private void btnRemoveNode_Click(object sender, EventArgs e)
		{
			string errTitle = "Failed to remove the node!";
			int? val = ParseUserInput();
			if (val != null)
			{
				if (!BST.Remove((int)val)) SimpleDialog.ShowMessage(errTitle, $"A node with the value '{val}' is not present!");
				else canvas.Refresh();
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
		private void resetDisplacementToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeVisualizer<int>.ResetDisplacement();
			canvas.Refresh();
		}
		private void speedBar_Scroll(object sender, ScrollEventArgs e)
		{

		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			TreeVisualizer<int>.DrawTree(BST.Root, e.Graphics, canvas);
		}
		private void canvas_Resize(object sender, EventArgs e)
		{
			canvas.Refresh();
		}

		private bool inDisplacementMode = false;
		private Point? prevPt;
		private void canvas_MouseDown(object sender, MouseEventArgs e)
		{
			inDisplacementMode = true;
			prevPt = e.Location;
		}
		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (inDisplacementMode)
			{
				TreeVisualizer<int>.Displace(new Point(e.X - ((Point)prevPt).X, e.Y - ((Point)prevPt).Y));
				prevPt = e.Location;
				canvas.Refresh();
			}
		}
		private void canvas_MouseUp(object sender, MouseEventArgs e)
		{
			inDisplacementMode = false;
			if (e.Button == MouseButtons.Right) canvasContextStrip.Show(Cursor.Position);
		}

		private void algoComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitNewBST();
			canvas.Refresh();
		}

		private void InitNewBST()
		{
			if (algoComboBox.SelectedIndex == 0) BST = new BinarySearchTree<int>();
			else BST = new AVLTree<int>();
		}
	}
}
