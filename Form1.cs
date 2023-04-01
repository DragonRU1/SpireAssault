using LZStringCSharp;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
//using System.Text.Json;
//using System.Text.Json.Nodes;

namespace SpireAssault
{
	public partial class Form1 : Form
	{
		EquipmentDS equipmentDS;
		public Form1()
		{
			InitializeComponent();
			equipmentDS = new();
			Equipment.DataSource = equipmentDS.Items;
			SetupGrid();
		}

		void SetupGrid()
		{
			Equipment.Columns[0].Visible = false;
			Equipment.Columns[1].Visible = false;
			Equipment.Columns[3].Width = 52;
			Equipment.Columns[4].Width = 75;
			Equipment.Columns[4].HeaderText = "Equipped";
			Equipment.Columns[5].Width = 75;
			Equipment.Columns[5].HeaderText = "Unlocked";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Result.Clear();
			Equipment eq = new Equipment(equipmentDS);
			int enemyLevel = (int)eLevel.Value;
			Trimp t = new(eq);
			Enemy en = new(enemyLevel, eq);
			Simulation s = new(t, en);
#if LOGGING
			s.ProcessFights(10000);
#else
			s.ProcessFights(100000);
#endif
			Result.Text = $"Wins = {s.Wins}, {Environment.NewLine}" +
				$"Losses = {s.Losses}, {Environment.NewLine}" +
				$"Dust per second = {s.Dust/s.RunTime * 1000:0.00}";
		}

		private void loadGame_Click(object sender, EventArgs e)
		{
			JToken? items, level;
			
			if (string.IsNullOrEmpty(saveText.Text))
				return;
			try
			{
				string decodedString = LZString.DecompressFromBase64(saveText.Text);
				JObject save = JObject.Parse(decodedString);
				JToken? abData = save?["global"]?["autoBattleData"];
				items = abData?["items"];
				level = abData?["enemyLevel"];
				eLevel.Value = int.Parse(level?.Value<string>() ?? "");
			}
			catch
			{
				ProcessWrongSave();
				return;
			}
			if (items is null)
			{
				ProcessWrongSave();
				return;
			}
			foreach (JProperty item in items)
			{
				EquipmentDSItem? dSItem = equipmentDS.Items.FirstOrDefault(x => x.SystemName == item.Name);
				if (dSItem != null)
				{
#pragma warning disable CS8604 // Possible null reference argument.
					dSItem.Level = (int)item.Value["level"];
					dSItem.IsEquipped = (bool)item.Value["equipped"];
					dSItem.IsUnlocked = (bool)item.Value["owned"];
#pragma warning restore CS8604 // Possible null reference argument.
				}
			}
			Equipment.Invalidate();
		}

		private void ProcessWrongSave()
		{
			MessageBox.Show("Incorrect save file. Please check your input.", "Incorrect save file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void reset_Click(object sender, EventArgs e)
		{
			equipmentDS = new();
			Equipment.DataSource = equipmentDS.Items;
		}

		private void saveText_Enter(object sender, EventArgs e)
		{
			saveText.Text = string.Empty;
		}
	}
}