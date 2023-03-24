using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	internal enum FightResult { None, Win, Lose}

	internal class Fight
	{
		const int Tick = 300;

		public int FightTime { get; set; } // Calculated in 1000th of second
		public Trimp Trimp { get; set; }
		public Fighter Enemy { get; set; }

		double attackT, attackE;
		int poisonTick;
		Random rand;

		public Fight(Simulation sim)
		{
			Trimp=sim.Trimp;
			Enemy=sim.Enemy;
			Trimp.CurrentStats=Trimp.BaseStats.Copy();
			Enemy.CurrentStats=Enemy.BaseStats.Copy();
			FightTime=0;
			attackT=Trimp.BaseStats.AttackSpeed;
			attackE=Enemy.BaseStats.AttackSpeed;
			poisonTick=1000;
			rand=new Random();
		}

		public FightResult Simulate()
		{
			while (true)
			{
				FightTime += Tick;

				RecalculateStats();

				if (attackT < Tick)
				{
					FightResult fr = ProcessAttack(Trimp, Enemy);
					if (fr != FightResult.None) return fr;
					attackT += Trimp.CurrentStats.AttackSpeed;
				}
				if (poisonTick < Tick)
				{
					FightResult fr = ProcessPoison(Trimp, Enemy);
					if (fr != FightResult.None) return fr;
				}
				if (attackE < Tick)
				{
					FightResult fr = ProcessAttack(Enemy, Trimp);
					if (fr != FightResult.None) 
						return fr;
					attackE += Enemy.CurrentStats.AttackSpeed;
				}
				if (poisonTick < Tick)
				{
					FightResult fr = ProcessPoison(Enemy, Trimp);
					if (fr != FightResult.None) return fr;
					poisonTick += 1000;
				}
				attackT -= Tick;
				attackE -= Tick;
				poisonTick -= Tick;
				Trimp.CurrentStats.BleedingTime -= Tick;
				Trimp.CurrentStats.PoisonedTime -= Tick;
				Trimp.CurrentStats.ShockedTime -= Tick;
				Enemy.CurrentStats.BleedingTime -= Tick;
				Enemy.CurrentStats.PoisonedTime -= Tick;
				Enemy.CurrentStats.ShockedTime -= Tick;
			}
		}

		FightResult ProcessAttack(Fighter attacker, Fighter defender)
		{
			{
				// Process direct damage
				double dmg = attacker.CurrentStats.Attack
					* (defender.CurrentStats.ShockedTime > 0 ? 1+defender.CurrentStats.ShockedMult : 1)
					* (1+0.002*(rand.Next(201)-100));
				if (!attacker.isTrimp)
				{
					double enrageTimer = (60 - ((Enemy)attacker).Enraging*10) * 1000;
					double enrageMult = 1 + (((Enemy)attacker).Level > 29 ? 0.5 : 0.25) + ((Enemy)attacker).Enraging*0.1;
					dmg *= Math.Pow(enrageMult, Math.Floor(FightTime / enrageTimer));
				}

				double FullDamage = Math.Max(dmg - defender.CurrentStats.Defense, 0);
				defender.CurrentStats.HP -= FullDamage;
				if (defender.CurrentStats.HP < 0)
					return EndFight(attacker, true);

				if (attacker.CurrentStats.Lifesteal > 0)
				{
					attacker.CurrentStats.HP += FullDamage * (attacker.CurrentStats.Lifesteal - defender.CurrentStats.LifestealResist);
					if (attacker.CurrentStats.HP > attacker.CurrentStats.MaxHP)
						attacker.CurrentStats.HP = attacker.CurrentStats.MaxHP;
				}
			}

			// Process bleed
			if (attacker.CurrentStats.BleedingTime > 0)
			{
				double dmg = Math.Max(defender.CurrentStats.Attack 
					* (1 + defender.CurrentStats.BleedMult) 
					* (attacker.CurrentStats.ShockedTime > 0 ? 1+attacker.CurrentStats.ShockedMult : 1)
					- attacker.CurrentStats.Defense, 0);
				attacker.CurrentStats.HP -= dmg;
				if (attacker.CurrentStats.HP < 0)
					return EndFight(attacker, false);

				if (defender.CurrentStats.Lifesteal > 0)
				{
					defender.CurrentStats.HP += dmg * (defender.CurrentStats.Lifesteal - attacker.CurrentStats.LifestealResist);
					if (defender.CurrentStats.HP > defender.CurrentStats.MaxHP)
						defender.CurrentStats.HP = defender.CurrentStats.MaxHP;
				}
			}
			ModifyConditions(attacker, defender);
			return FightResult.None;
		}

		FightResult ProcessPoison(Fighter attacker, Fighter defender)
		{
			if (attacker.CurrentStats.PoisonedTime > 0)
			{
				attacker.CurrentStats.HP -= defender.CurrentStats.PoisonDamage
					* attacker.CurrentStats.PoisonedStacks 
					* (attacker.CurrentStats.ShockedTime > 0 ? 1+attacker.CurrentStats.ShockedMult : 1);
				if (attacker.CurrentStats.HP <= 0)
				{
					if (attacker.isTrimp)
						return FightResult.Lose;
					else
						return FightResult.Win;
				}
			}
			return FightResult.None;
		}

		void RecalculateStats()
		{
			Trimp.CurrentStats.BleedChance = Trimp.BaseStats.BleedChance;
			Trimp.CurrentStats.BleedMult = Trimp.BaseStats.BleedMult;
			Trimp.CurrentStats.PoisonChance = Trimp.BaseStats.PoisonChance;
			Trimp.CurrentStats.ShockChance = Trimp.BaseStats.ShockChance;
			Trimp.CurrentStats.ShockMult = Trimp.BaseStats.ShockMult;
			Trimp.CurrentStats.Defense = Trimp.BaseStats.Defense;
			Trimp.CurrentStats.Lifesteal = Trimp.BaseStats.Lifesteal;
			Trimp.CurrentStats.MaxHP = Trimp.BaseStats.MaxHP;

			if (Trimp.Equipment.RustyDagger > 0)
				if (Enemy.CurrentStats.ShockedTime > 0 || Enemy.CurrentStats.PoisonedTime > 0)
					Trimp.CurrentStats.BleedChance += Trimp.Equipment.RustyDagger*3 + 17;
			if (Trimp.Equipment.FistsOfGoo > 0)
				if (Enemy.CurrentStats.ShockedTime > 0 || Enemy.CurrentStats.BleedingTime > 0)
					Trimp.CurrentStats.PoisonChance += 25;
			if (Trimp.Equipment.BatteryStick > 0)
				if (Enemy.CurrentStats.PoisonedTime > 0 || Enemy.CurrentStats.BleedingTime > 0)
					Trimp.CurrentStats.PoisonChance += 35;
			if ((Trimp.Equipment.RainCoat > 0) && (Trimp.CurrentStats.BleedChance > Enemy.CurrentStats.BleedResist))
			{
				Trimp.CurrentStats.Defense += 4 + Trimp.Equipment.RainCoat * 2;
				Trimp.CurrentStats.MaxHP += 20 + Trimp.Equipment.RainCoat * 20;

				// if you have bleed chance on start, your current hp is also increased
				if (FightTime == Tick)
					Trimp.CurrentStats.HP = Trimp.CurrentStats.MaxHP;

				Trimp.CurrentStats.Lifesteal += 0.125 + 0.025*Trimp.Equipment.RainCoat;
				Trimp.CurrentStats.BleedMult += 0.2 + 0.1*Trimp.Equipment.RainCoat;
			}
		}

		void ModifyConditions(Fighter attacker, Fighter defender)
		{
			if (defender.CurrentStats.BleedingTime <= 0)
			{
				if (rand.NextDouble() * 100 < attacker.CurrentStats.BleedChance - defender.CurrentStats.BleedResist)
				{
					defender.CurrentStats.BleedingTime = attacker.CurrentStats.BleedTimeMax;
					defender.CurrentStats.BleedingMult = attacker.CurrentStats.BleedMult;
				}
			}

			if (rand.NextDouble() * 100 < attacker.CurrentStats.ShockChance - defender.CurrentStats.ShockResist)
			{
				defender.CurrentStats.ShockedTime = attacker.CurrentStats.ShockTimeMax;
				defender.CurrentStats.ShockedMult = attacker.CurrentStats.ShockMult;
			}

			if (rand.NextDouble() * 100 < attacker.CurrentStats.PoisonChance - defender.CurrentStats.PoisonResist)
			{
				defender.CurrentStats.PoisonedTime = attacker.CurrentStats.PoisonTimeMax;
				defender.CurrentStats.PoisonedStacks = Math.Min(defender.CurrentStats.PoisonedStacks+1, attacker.CurrentStats.PoisonMaxStacks);
			}


		}


		FightResult EndFight (Fighter attacker, bool isWon)
		{
			if (attacker.isTrimp == isWon)
				return FightResult.Win;
			else
				return FightResult.Lose;
		}



	}
}
