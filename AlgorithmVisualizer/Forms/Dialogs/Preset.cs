using System;
using System.IO;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public class Preset
	{
		// A class to represent a graph preset
		// The serial is the adjacency list of the graph as a string
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Serial { get; private set; }
		public string ImgDir { get; private set; }
		public const string DEFAULT_IMG_DIR = @"\images\presets\default-image.png";

		public Preset(string name, string serial) :
			this(-1, name, serial, null)
		{ }
		public Preset(int id, string name, string serial, string imgDir)
		{
			Id = id;
			Name = name;
			Serial = serial;
			ImgDir = imgDir;
		}

		public string GetAbsoluteDir()
		{
			// Get absolute path to the preset's image, if found returns it otherwise deault image.
			var imgFolderDir = Directory.GetParent( Environment.CurrentDirectory).Parent.Parent.FullName;
			return imgFolderDir + (File.Exists(imgFolderDir + ImgDir) ? ImgDir : DEFAULT_IMG_DIR);
		}
		public override string ToString()
		{
			return $"Id: {Id}, Name: {Name}\n" +
				   $"Serial:\n{Serial}\n" +
				   $"ImgDir: {ImgDir}\n";
		}

	}
}
