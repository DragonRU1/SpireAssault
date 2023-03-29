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
			BaseStats.PoisonMaxStacks = 2;
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
				BaseStats.BleedMult += 0.1 + 0.05*Equipment.RustyDagger + 0.2*lv5;
				BaseStats.CanBleed = true;
			}
			if (Equipment.FistsOfGoo > 0)
			{
				BaseStats.PoisonChance += 25;
				BaseStats.PoisonDamage += Equipment.FistsOfGoo;
				BaseStats.CanPoison = true;
			}
			if (Equipment.BatteryStick > 0)
			{
				BaseStats.ShockChance += 35;
				BaseStats.ShockMult += 0.15 + Equipment.BatteryStick * 0.1;
				BaseStats.CanShock = true;
			}	
			BaseStats.Defense += Equipment.Pants;
			if (Equipment.PutridPouch > 0)
			{
				BaseStats.PoisonTimeMax = Math.Max(BaseStats.PoisonTimeMax, 19000 + Equipment.PutridPouch * 1000);
				BaseStats.PoisonChance += 14 + Equipment.PutridPouch * 6;
			}
			if (Equipment.ChemistrySet > 0)
			{
				BaseStats.PoisonChance += 6 + Equipment.ChemistrySet*4;
				BaseStats.PoisonMaxStacks += 1 + (int)Math.Floor(Equipment.ChemistrySet/4.0);
			}
			if (Equipment.BadMedkit > 0)
			{
				BaseStats.BleedTimeMax = Math.Max(BaseStats.BleedTimeMax, 11000 + Equipment.BadMedkit * 1000);
				BaseStats.BleedChance += 21 + Equipment.BadMedkit*4;
				BaseStats.Lifesteal += 0.175 + 0.025*Equipment.BadMedkit;
			}
			if (Equipment.ComfyBoots > 0)
			{
				BaseStats.Defense += 2 + Equipment.ComfyBoots*2;
				BaseStats.BleedResist += Equipment.ComfyBoots*5;
				BaseStats.ShockResist += Equipment.ComfyBoots*5;
				BaseStats.PoisonResist += Equipment.ComfyBoots*5;
			}
			if (Equipment.HungeringMold > 0)
			{
				BaseStats.PoisonTick *= 0.909 * Math.Pow(0.99, Equipment.HungeringMold);
				BaseStats.PoisonHeal += 0.5 + Equipment.HungeringMold*0.5;
			}
			if (Equipment.Recycler > 0)
			{
				BaseStats.Lifesteal += 0.2 + 0.05*Equipment.Recycler;
			}
			if (Equipment.ShiningArmor > 0)
			{
				BaseStats.Defense += 14 + 6*Equipment.ShiningArmor;
				BaseStats.HP += 200 + 100*Equipment.ShiningArmor;
			}
			if (Equipment.ShockAndAwl > 0)
			{
				BaseStats.ShockTimeMax = Math.Max(20000, BaseStats.ShockTimeMax);
				BaseStats.ShockChance += 20 + 10 * Equipment.ShockAndAwl;
				BaseStats.ShockMult += 0.4 + 0.1 * Equipment.ShockAndAwl;
				BaseStats.Attack += 6 + 4 * Equipment.ShockAndAwl;
			}
			if (Equipment.TameSnimp > 0)
			{
				BaseStats.CanPoison = true;
				BaseStats.PoisonChance += 30 + 10*Equipment.TameSnimp;
				BaseStats.PoisonDamage += 5 + 2*Equipment.TameSnimp;
			}
			if (Equipment.WiredWristguards > 0)
			{
				BaseStats.Defense += 7 + 3*Equipment.WiredWristguards;
				BaseStats.ShockChance += 25 + 15*Equipment.WiredWristguards;
				BaseStats.ShockMult += 0.25 + 0.15*Equipment.WiredWristguards;
				BaseStats.BleedResist += 50;
				BaseStats.ShockResist += 50;
				BaseStats.PoisonResist += 50;
			}

			// After all equipment
			BaseStats.MaxHP = BaseStats.HP;
		}
	}

}
