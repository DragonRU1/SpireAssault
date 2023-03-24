using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	internal class Trimp : Fighter
	{
		public Equipment Equipment { get; set; }

		public Trimp(Equipment equipment) 
		{
			Equipment = equipment;
			CalcualteBaseStats();
		}

		public new Trimp Copy()
		{
			Trimp s = (Trimp)base.Copy();
			s.Equipment = Equipment.Copy();
			return s;
		}

		public void CalcualteBaseStats()
		{
			BaseStats = new Stats();

			isTrimp = true;
			BaseStats.HP = 50;
			BaseStats.Defense = 0;
			BaseStats.Attack = 5;
			BaseStats.AttackSpeed = 5000;
			if (Equipment.MenacingMask > 0)
			{
				BaseStats.AttackSpeed *= Math.Pow(0.98, Equipment.MenacingMask);
			}
			BaseStats.Attack += Equipment.Sword;
			BaseStats.HP += Equipment.Armor * 20;
			if (Equipment.RustyDagger > 0)
			{
				int lv5 = (int)Math.Floor(Equipment.RustyDagger / 5.0);
				BaseStats.Attack += 10 * lv5;
				BaseStats.BleedChance += 17 + 3*Equipment.RustyDagger;
				BaseStats.BleedMult += 10 + 5*Equipment.RustyDagger + 20*lv5;
			}
			if (Equipment.FistsOfGoo > 0)
			{
				BaseStats.PoisonChance += 25;
				BaseStats.PoisonDamage += Equipment.FistsOfGoo;
			}
			if (Equipment.BatteryStick > 0)
			{
				BaseStats.ShockChance += 35;
				BaseStats.ShockMult += 15 + Equipment.BatteryStick * 10;
			}	
			BaseStats.Defense += Equipment.Pants;
		}
	}

}
