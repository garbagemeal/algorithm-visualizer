using MySql.Data.MySqlClient;
using System;
using System.Data;

using AlgorithmVisualizer.Forms.Dialogs;

namespace AlgorithmVisualizer.DBHandler
{
	public class DBConnection
	{
		/* Handles connection and provides basic operations for to 'Graphs' DB containing
		 * the 'Presets' table storing: id, name, serial, img_dir.
		 * 
		 * In case you dont have the DB, in order to re-create it, use the file graphs.sql
		 * located at: AlgorithmVisualizer\SQL Files\
		 */

		public static string Server = "localhost";
		public static string DBName = "Graphs";
		public static string Username = "root";
		public static string Password = "";
		private static DBConnection instance;

		private MySqlConnection connection;

		private DBConnection() { }
		public static DBConnection GetInstance()
		{

			if (instance == null) instance = new DBConnection();
			return instance;
		}

		public bool Connect()
		{
			if (connection == null)
			{
				// Make sure DBName is set
				if (string.IsNullOrEmpty(DBName)) return false;
				string connString = $"Server={Server}; database={DBName}; UID={Username}; password={Password}; SSL Mode=None";
				connection = new MySqlConnection(connString);
				try
				{
					connection.Open();
				}
				catch (Exception e)
				{
					Console.WriteLine("Caught exeption while connecting to DB:\n" + e.Message);
					return false;
				}
			}
			return true;
		}
		public void Disconnect()
		{
			if (connection != null)
			{
				connection.Close();
				connection = null;
			}
		}

		private bool ExecuteNonQuerry(MySqlCommand cmd)
		{
			bool res = true;
			if (Connect())
			{
				cmd.Connection = connection;
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception raised while executing NonQuerry:\n" + e.Message);
					res = false;
				}
			}
			return res;
		}
		private string ExecuteScalarStringQuerry(MySqlCommand cmd)
		{
			string res = "";
			if (Connect())
			{
				cmd.Connection = connection;
				try
				{
					res = (string)cmd.ExecuteScalar();
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception raised while executing ScalarQuerry:\n" + e.Message);
					res = "ERROR";
				}
			}
			return res;
		}

		public bool AddPreset(Preset preset)
		{
			string cmdStr = "INSERT INTO Presets(name, serial, img_dir) VALUES(@name, @serial, @img_dir)";
			using (var cmd = new MySqlCommand(cmdStr))
			{
				cmd.Parameters.AddWithValue("@name", preset.Name);
				cmd.Parameters.AddWithValue("@serial", preset.Serial);
				cmd.Parameters.AddWithValue("@img_dir", preset.ImgDir);
				return ExecuteNonQuerry(cmd);
			}
		}
		public bool RemovePreset(int id)
		{
			string cmdStr = "DELETE FROM Presets WHERE id = @id";
			using (var cmd = new MySqlCommand(cmdStr))
			{
				cmd.Parameters.AddWithValue("@id", id);
				return ExecuteNonQuerry(cmd);
			}
		}

		public bool UpdatePresetName(int id, string newName)
		{
			string cmdStr = "UPDATE Presets SET name = @newName WHERE id = @id";
			using (var cmd = new MySqlCommand(cmdStr))
			{
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@newName", newName);
				return ExecuteNonQuerry(cmd);
			}
		}
		public bool UpdatePresetSerial(int id, string newSerial)
		{
			string cmdStr = "UPDATE Presets SET serial = @newSerial WHERE id = @id";
			using (var cmd = new MySqlCommand(cmdStr))
			{
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@newSerial", newSerial);
				return ExecuteNonQuerry(cmd);
			}
		}
		public bool UpdatePresetImg(int id, string newImgDir)
		{
			string cmdStr = "UPDATE Presets SET img_dir = @newImgDir WHERE id = @id";
			using (var cmd = new MySqlCommand(cmdStr))
			{
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@newImgDir", newImgDir);
				return ExecuteNonQuerry(cmd);
			}
		}

		private DataSet GetMultiplyQuerry(MySqlCommand cmd)
		{
			DataSet dataSet = new DataSet();
			if (Connect())
			{
				cmd.Connection = connection;
				try
				{
					MySqlDataAdapter adapter = new MySqlDataAdapter();
					adapter.SelectCommand = cmd;
					adapter.Fill(dataSet);
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception raised while executing GetAllPresets:\n" + e.Message);
					dataSet = null;
				}
			}
			return dataSet;
		}
		public Preset[] GetAllPresets()
		{
			DataSet dataSet = new DataSet();
			Preset[] presets = null;
			string cmdStr = "SELECT * FROM Presets";
			using (var cmd = new MySqlCommand(cmdStr)) dataSet = GetMultiplyQuerry(cmd);
			if (dataSet == null)
			{
				Console.WriteLine("dataSet is null, returning null.");
				return null;
			}
			DataTable dataTable = null;
			try
			{
				dataTable = dataSet.Tables[0];
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception raised while executing GetAllPresets:\n" + e.Message);
				return null;
			}
			int dataTableRowCount = dataTable.Rows.Count;
			if (dataTableRowCount > 0)
			{
				presets = new Preset[dataTableRowCount];
				for (int i = 0; i < dataTableRowCount; i++)
				{
					var row = dataTable.Rows[i];
					presets[i] = new Preset((int)row[0], (string)row[1], (string)row[2],
						row[3] is DBNull ? "" : (string)row[3]);
				}
			}
			return presets;
		}
	}
}
