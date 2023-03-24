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
			Equipment.Columns[0].Visible = false;
			Equipment.Columns[1].Visible = false;
			Equipment.Columns[3].Width = 50;
			Equipment.Columns[4].Width = 80;
			Equipment.Columns[4].HeaderText = "Equipped";
			Equipment.Columns[5].Width = 80;
			Equipment.Columns[5].HeaderText = "Unlocked";

		}

		private void button1_Click(object sender, EventArgs e)
		{

			Equipment eq = new Equipment(equipmentDS.Items.Select(x => x.Level).ToArray());
			Trimp t = new(eq);
			Enemy en = new(2, eq);
			Simulation s = new(t, en);
			s.ProcessFights(100000);
		}

		private void loadGame_Click(object sender, EventArgs e)
		{
			JToken? items;
			if (string.IsNullOrEmpty(saveText.Text))
				return;
			try
			{
				string decodedString = LZString.DecompressFromBase64(saveText.Text);
				JObject save = JObject.Parse(decodedString);
				JToken? abData = save?["global"]?["autoBattleData"];
				items = abData?["items"];
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
				EquipmentDSItem dSItem = equipmentDS.Items.FirstOrDefault(x => x.SystemName == item.Name);
				if (dSItem != null)
				{
					dSItem.Level = (int)item.Value["level"];
					dSItem.IsEquipped = (bool)item.Value["equipped"];
					dSItem.IsUnlocked = (bool)item.Value["owned"];
				}
			}
			Equipment.Invalidate();
		}

		private void ProcessWrongSave()
		{
			MessageBox.Show("Incorrect save file. Please check your input.", "Incorrect save file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}
}