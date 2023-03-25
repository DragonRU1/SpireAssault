using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	public enum EqNames
	{
		MenacingMask = 0,
		Sword = 1,
		Armor = 2,
		RustyDagger = 3,
		FistsOfGoo = 4,
		BatteryStick = 5,
		Pants = 6,
		Raincoat = 7,
		PutridPouch = 8,
		ChemistrySet = 9,
		BadMedkit = 10,
		ComfyBoots = 11
	}


	internal class Equipment
	{
		int[] Items;

		public int MenacingMask => Items[(int)EqNames.MenacingMask];
		public int Sword => Items[(int)EqNames.Sword];
		public int Armor => Items[(int)EqNames.Armor];
		public int RustyDagger => Items[(int)EqNames.RustyDagger];
		public int FistsOfGoo => Items[(int)EqNames.FistsOfGoo];
		public int BatteryStick => Items[(int)EqNames.BatteryStick];
		public int Pants => Items[(int)EqNames.Pants];
		public int Raincoat => Items[(int)EqNames.Raincoat];
		public int PutridPouch => Items[(int)EqNames.PutridPouch];
		public int ChemistrySet => Items[(int)EqNames.ChemistrySet];
		public int BadMedkit => Items[(int)EqNames.BadMedkit];
		public int ComfyBoots => Items[(int)EqNames.ComfyBoots];

		public Equipment(int[] items)
		{
			Items = items;
		}

		public Equipment Copy()
		{
			return (Equipment)MemberwiseClone();
		}

		public Equipment(EquipmentDS equipmentDS)
		{
			Items = equipmentDS.Items.Select(x => x.IsEquipped ? x.Level : 0).ToArray();
		}

	}
}
