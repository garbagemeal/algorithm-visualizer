using System;
using System.IO;
using System.Xml.Serialization;

using AlgorithmVisualizer.Forms.Dialogs;

namespace AlgorithmVisualizer.Presets
{
	public class Preset
	{
		// A class to represent a graph preset, save/load a graph preset, etc...
		// The serial is the adjacency list of the graph as a string

		[XmlAttribute]
		public string id;
		public string name;
		public string serial;
		public string imgDir;
		public string timestamp;


		private static readonly string
			// The Presets directory
			PresetsRootDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\Presets",
			PresetsDir = PresetsRootDir + @"\Serializations",
			ImgsDir = PresetsRootDir + @"\Imgs",
			DefaultImgDir = @"\default.png";
		private static readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(Preset));

		public void Save()
		{
			// Save this Preset instance as an XML doc
			try
            {
				timestamp = GetTimestamp();
				string fileOutPath = PresetsDir + $@"\preset_{id}.xml";
				if (!File.Exists(fileOutPath))
				{
					using (var outFileStream = new StreamWriter(fileOutPath))
						xmlSerializer.Serialize(outFileStream, this);
				}
				else SimpleDialog.ShowMessage("ERROR1", "Collision of preset id");
            }
			catch (Exception ex)
			{
				SimpleDialog.ShowMessage("ERROR1", ex.Message);
			}
		}

		public static Preset[] LoadAll()
		{
			try
			{
				// Get paths of all files eding in ".xml" in the current dir; does not explore subdirectories
				string[] xmlFilePaths = Directory.GetFiles(PresetsDir, "*.xml", SearchOption.TopDirectoryOnly);
				int xmlFileCount = xmlFilePaths.Length;
				// Load and return presets
				var presets = new Preset[xmlFileCount];
				for (int i = 0; i < xmlFileCount; i++)
                {
					using (var inFileStream = new StreamReader(xmlFilePaths[i]))
						presets[i] = (Preset)xmlSerializer.Deserialize(inFileStream);
                }
				return presets;
			}
			catch (Exception ex)
			{
				SimpleDialog.ShowMessage("ERROR3", ex.Message);
				return null;
			}
		}

		public static void Remove(string id)
		{
			// Remove the XML doc for the given preset id
			try
			{
				File.Delete(PresetsDir + $@"\preset_{id}.xml");
				// TODO: Check if preset has an image and remove it
			}
			catch (Exception ex)
			{
				SimpleDialog.ShowMessage("ERROR4", ex.Message);
			}
		}

        public string GetAbsImgDir()
        {
			// Returns the absolute path of the image if exists else the default img's dir
            return ImgsDir + (File.Exists(ImgsDir + imgDir) ? imgDir : DefaultImgDir);
        }

		private static string GetTimestamp() => DateTime.Now.ToString("yyyyMMddHHmmssffff");

        public override string ToString()
		{
			return $"id: {id}, Name: {name}\n" +
				   $"derial:\n{serial}\n" +
				   $"imgDir: {imgDir}\n" +
				   $"timestamp: {timestamp}";
		}
	}
}
