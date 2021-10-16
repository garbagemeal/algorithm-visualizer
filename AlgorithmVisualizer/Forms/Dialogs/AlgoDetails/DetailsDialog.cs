﻿using System;
using System.Windows.Forms;
using System.Xml;

namespace AlgorithmVisualizer.Forms.Dialogs.AlgoDetails
{
	public partial class DetailsDialog : Form
	{
		private string xmlFilePath;
		public DetailsDialog(string _xmlFilePath)
		{
			Console.WriteLine("Given _xmlFilePath: " + _xmlFilePath);
			InitializeComponent();
			xmlFilePath = _xmlFilePath;
		}

		private string GetDetails()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFilePath);
			string str = "";
			foreach (XmlNode node in doc.DocumentElement.ChildNodes)
			{
				if (!node.Name.Equals("sourceCode"))
					str += $"{node.Name}: {node.InnerText}\n";
			}
			return str;
		}
		private string GetSourceCode()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFilePath);
			XmlNode node = doc.DocumentElement.SelectSingleNode("/algorithm/sourceCode");
			return $"Srouce code:\n{node.InnerText}";
		}

		private void btnExplanation_Click(object sender, EventArgs e)
		{
			richTextBox.Clear();
			richTextBox.AppendText(GetDetails());
		}
		private void btnImplementation_Click(object sender, EventArgs e)
		{
			richTextBox.Clear();
			richTextBox.AppendText(GetSourceCode());
		}
	}
}
