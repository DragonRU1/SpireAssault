using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	public class EquipmentDSItem
	{
		public int Index { get; set; }
		public string SystemName { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public bool IsEquipped { get; set; }
		public bool IsUnlocked { get; set; }

		public EquipmentDSItem(int index, string systemName, int level, bool isEquipped, bool isUnlocked) 
		{
			Index = index;
			SystemName = systemName;
			Name = systemName.Replace("__", "-").Replace('_', ' ');
			Level = level;
			IsEquipped = isEquipped;
			IsUnlocked = isUnlocked;
		}
	}

	public class EquipmentDS
	{
		public BindingList<EquipmentDSItem> Items { get; set; }
		public EquipmentDS() 
		{
			string[] sItems = new string[] {
				"Menacing_Mask",
				"Sword",
				"Armor",
				"Rusty_Dagger",
				"Fists_of_Goo",
				"Battery_Stick",
				"Pants",
				"Raincoat",
				"Putrid_Pouch",
				"Chemistry_Set",
				"Bad_Medkit",
				"Comfy_Boots",
				"Labcoat",
				"Lifegiving_Gem",
				"Mood_Bracelet",
				"Hungering_Mold",
				"Recycler",
				"Shining_Armor",
				"Shock_and_Awl",
				"Spiked_Gloves",
				"Tame_Snimp",
				"Lich_Wraps",
				"Wired_Wristguards",
				"Aegis",
				"Sword_and_Board",
				"Bilious_Boots",
				"Bloodstained_Gloves",
				"Unlucky_Coin",
				"Eelimp_in_a_Bottle",
				"Big_Cleaver",
				"The_Globulator",
				"Metal_Suit",
				"Nozzled_Goggles",
				"Sundering_Scythe",
				"Sacrificial_Shank",
				"Plague_Bringer",
				"Very_Large_Slime",
				"Monkimp_Paw",
				"Grounded_Crown",
				"Fearsome_Piercer",
				"Bag_of_Nails",
				"Blessed_Protector",
				"The_Doomspring",
				"Snimp__Fanged_Blade",
				"Doppelganger_Signet",
				"Wrath_Crafted_Hatchet",
				"Basket_of_Souls",
				"Goo_Golem",
				"Omni_Enhancer",
				"Stormbringer",
				"Box_of_Spores",
				"Nullifium_Armor",
				"Handful_of_Mold",
				"Haunted_Harpoon"};

			Items = new BindingList<EquipmentDSItem> {};
			int idx = 0;
			foreach (string itm in sItems)
			{
				Items.Add(new EquipmentDSItem(idx, itm, 0, false, false));
				idx++;
			}
		}
	}
}
