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
		RustyDagger = 1,
		FistsOfGoo = 2,
		BatteryStick = 3,
		Pants = 4,
		Sword = 5,
		Armor = 6,
		RainCoat = 7
	}


	internal class Equipment
	{
		int[] Items;

		public int MenacingMask => Items[(int)EqNames.MenacingMask];
		public int RustyDagger => Items[(int)EqNames.RustyDagger];
		public int FistsOfGoo => Items[(int)EqNames.FistsOfGoo];
		public int BatteryStick => Items[(int)EqNames.BatteryStick];
		public int Pants => Items[(int)EqNames.Pants];
		public int Sword => Items[(int)EqNames.Sword];
		public int Armor => Items[(int)EqNames.Armor];
		public int RainCoat => Items[(int)EqNames.RainCoat];

		public Equipment(int[] items)
		{
			Items = items;
		}

		public Equipment Copy()
		{
			return (Equipment)MemberwiseClone();
		}

	}
}
