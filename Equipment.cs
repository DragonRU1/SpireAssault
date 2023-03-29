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
		ComfyBoots = 11,
		Labcoat = 12,
		LifegivingGem = 13,
		MoodBracelet = 14,
		HungeringMold = 15,
		Recycler = 16,
		ShiningArmor = 17,
		ShockAndAwl = 18,
		SpikedGloves = 19,
		TameSnimp = 20,
		LichWraps = 21,
		WiredWristguards = 22,
		Aegis = 23,
		SwordAndBoard = 24,
		BiliousBoots = 25,
		BloodstainedGloves = 26
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
		public int Labcoat => Items[(int)EqNames.Labcoat];
		public int LifegivingGem => Items[(int)EqNames.LifegivingGem];
		public int MoodBracelet => Items[(int)EqNames.MoodBracelet];
		public int HungeringMold => Items[(int)EqNames.HungeringMold];
		public int Recycler => Items[(int)EqNames.Recycler];
		public int ShiningArmor => Items[(int)EqNames.ShiningArmor];
		public int ShockAndAwl => Items[(int)EqNames.ShockAndAwl];
		public int SpikedGloves => Items[(int)EqNames.SpikedGloves];
		public int TameSnimp => Items[(int)EqNames.TameSnimp];
		public int LichWraps => Items[(int)EqNames.LichWraps];
		public int WiredWristguards => Items[(int)EqNames.WiredWristguards];
		public int Aegis => Items[(int)EqNames.Aegis];
		public int SwordAndBoard => Items[(int)EqNames.SwordAndBoard];
		public int BiliousBoots => Items[(int)EqNames.BiliousBoots];
		public int BloodstainedGloves => Items[(int)EqNames.BloodstainedGloves];

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
