using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;

namespace SpireAssault
{
	internal class Enemy : Fighter
	{
		public double Dust { get; set; }
		public int Level { get; set; }
		public int Enraging { get; set; }
		public int Explosive { get; set; }
		public int Berserking { get; set; }
		public int Slowing { get; set; }
		public int Ethereal { get; set; }


		public Enemy(int level, Equipment eq)
		{
			isTrimp = false;
			Level = level;
			CalcualteEnemyStats(eq);
		}

		public void CalcualteEnemyStats(Equipment eq)
		{
			BaseStats = new Stats();
			// Enemy does not need specific items to enable debuffs
			BaseStats.CanBleed = true;
			BaseStats.CanShock = true;
			BaseStats.CanPoison = true;

			Dust = (1 + (Level-1)*5) * Math.Pow(1.19, Level-1);
			BaseStats.HP = 50 * Math.Pow(1.205, Level);
			BaseStats.Attack = (3 + 2*Level) * Math.Pow(1.04, Level);

			if (Level >= 50)
			{
				double extraMult = Math.Pow(1.1, Level-49);
				Dust *= extraMult;
				BaseStats.HP *= extraMult;
				BaseStats.Attack *= extraMult;
			}

			BaseStats.Defense = Level * 0.5;
			BaseStats.BleedResist = Level;
			BaseStats.ShockResist = Level;
			BaseStats.PoisonResist = Level;
			BaseStats.LifestealResist = Math.Max(Level-14, 0) * 0.03;

			BaseStats.AttackSpeed = 5000 * Math.Pow(0.98, Math.Min(Level, 29));

			ApplyModifiers();

			BaseStats.MaxHP = BaseStats.HP;
			if (eq.MenacingMask > 0)
				BaseStats.AttackSpeed *= 1.05 * Math.Pow(1.02, eq.MenacingMask-1);

		}

		void ApplyModifiers()
		{
			EnemyModifiersItem? emi = EnemyModifiers.Instance.Items.FirstOrDefault(x => x.Level == Level);
			if (emi == null)
			{
				// For now doing nothing, maybe will add error handling later
				return;
			}
			if (emi.Healthy > 0)
				BaseStats.HP *= (1 + emi.Healthy * Math.Min(Level/30.0, 1));
			if (emi.Fast > 0)
				BaseStats.AttackSpeed *= Math.Pow(Math.Max(0.5, Math.Pow(0.98, Level)), emi.Fast);
			if (emi.Strong > 0)
				BaseStats.Attack *= (1 + emi.Strong * Math.Min(Level/30.0, 1));
			if (emi.Defensive > 0)
				BaseStats.Defense += Math.Ceiling(Level * 0.75 * Math.Pow(1.05, Level));
			if (emi.Bloodletting > 0)
			{
				for (int i = 0; i < emi.Bloodletting; i++)
				{
					BaseStats.BleedChance += Math.Ceiling(3 * Level * RepeatMod(i));
					BaseStats.BleedMult += Math.Ceiling(Math.Min(2, Level/20.0) * RepeatMod(i));
				}
				BaseStats.BleedTimeMax = 8000;
			}
			if (emi.Shocking > 0)
			{
				for (int i = 0; i < emi.Shocking; i++)
				{
					BaseStats.ShockChance += Math.Ceiling(3 * Level * RepeatMod(i));
					BaseStats.ShockMult += Math.Ceiling(Math.Min(2.5, Level/15.0) * RepeatMod(i));
				}
				BaseStats.ShockTimeMax = 8000;
			}
			if (emi.Poisoning > 0)
			{
				for (int i = 0; i < emi.Poisoning; i++)
				{
					BaseStats.PoisonChance += Math.Ceiling(3 * Level * RepeatMod(i));
					BaseStats.PoisonDamage += Math.Ceiling(Level/5.0 * RepeatMod(i));
				}
				if (Level >= 30)
					BaseStats.PoisonDamage += Level-29;
				BaseStats.PoisonMaxStacks = (int)Math.Floor(Level/10.0) + emi.Poisoning;

				BaseStats.PoisonTimeMax = 2500 * (1 + Math.Ceiling(Level / 5.0));
			}
			if (emi.BleedResistant > 0)
				BaseStats.BleedResist += 10*Level*emi.BleedResistant;
			if (emi.PoisonResistant > 0)
				BaseStats.PoisonResist += 10*Level*emi.PoisonResistant;
			if (emi.ShockResistant > 0)
				BaseStats.ShockResist += 10*Level*emi.ShockResistant;
			if (emi.Lifestealing > 0)
				BaseStats.Lifesteal += Math.Min(1, Level/50.0)*emi.Lifestealing;

		}

		double RepeatMod(int level) => Math.Pow(0.5, level);

	}

	public class EnemyModifiersItem
	{
		public int Level { get; set; }
		public int Healthy { get; set; }
		public int Fast { get; set; }
		public int Strong { get; set; }
		public int Defensive { get; set; }

		public int Bloodletting { get; set; }
		public int Shocking { get; set; }
		public int Poisoning { get; set; }
		public int Lifestealing { get; set; }

		public int BleedResistant { get; set; }
		public int ShockResistant { get; set; }
		public int PoisonResistant { get; set; }
		public int Enraging { get; set; }
		public int Explosive { get; set; }
		public int Berserking { get; set; }
		public int Slowing { get; set; }
		public int Ethereal { get; set; }

		public EnemyModifiersItem() { }

		public EnemyModifiersItem(int level, string bonuses)
		{
			int GetValue(string name)
			{
				int idx = bonuses.IndexOf(name);
				if (idx == -1) return 0;
				int lastPos = idx + name.Length;
				if ((bonuses.Length < lastPos + 2) || (bonuses.Substring(lastPos, 2) != " x"))
					return 1;
				else
					return int.Parse(bonuses.Substring(lastPos+2, 1));
			}

			Level = level;

			// Because we can control structure of this file, and it is not a time-crucial code, let's do it simpliest way
			Healthy = GetValue("Healthy");
			Strong = GetValue("Strong");
			Fast = GetValue("Fast");
			Defensive = GetValue("Defensive");
			Poisoning = GetValue("Poisoning");
			Bloodletting = GetValue("Bloodletting");
			Shocking = GetValue("Shocking");
			Lifestealing = GetValue("Lifestealing");
			PoisonResistant = GetValue("Poison Resistant");
			ShockResistant = GetValue("Shock Resistant");
			BleedResistant = GetValue("Bleed Resistant");
			Enraging = GetValue("Enraging");
			Explosive = GetValue("Explosive");
			Berserking = GetValue("Berserking");
			Slowing = GetValue("Slowing");
			Ethereal = GetValue("Ethereal");
		}
	}

	public class EnemyModifiers
	{
		static EnemyModifiers _instance = new();
		public static EnemyModifiers Instance => _instance ?? (_instance = new EnemyModifiers());

		public List<EnemyModifiersItem> Items { get; set; }

		EnemyModifiers()
		{
			string json = File.ReadAllText("enemy.json");
			JObject data = JObject.Parse(json);
			Items = new();
			foreach (var itm in data)
			{
#pragma warning disable CS8604 // Possible null reference argument.
				Items.Add(new EnemyModifiersItem(int.Parse(itm.Key), itm.Value?.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
			}
		}
	}
}
	
