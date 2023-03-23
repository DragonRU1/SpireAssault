using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	internal class Stats
	{
		public double HP;
		public double Attack;
		public double AttackSpeed;
		public double Defense;
		public double BleedChance;
		public double ShockChance;
		public double PoisonChance;
		public double BleedMult;
		public double ShockMult;
		public double PoisonDamage;
		public int PoisonMaxStacks;
		public double Lifesteal;

		public int BleedTimeMax;
		public int ShockTimeMax;
		public int PoisonTimeMax;

		public double BleedResist;
		public double ShockResist;
		public double PoisonResist;
		public double LifestealResist;

		public int BleedingTime;
		public int ShockedTime;
		public int PoisonedTime;
		public double BleedingMult;
		public double ShockedMult;
		public int PoisonedStacks;

		public int Enraging;

		public Stats()
		{
			BleedChance = 0;
			PoisonChance = 0;
			ShockChance = 0;
			BleedMult = 0;
			ShockMult = 0;
			PoisonDamage = 0;
			PoisonMaxStacks = 1;
			Lifesteal = 0;
			BleedTimeMax = 10000;
			ShockTimeMax = 10000;
			PoisonTimeMax = 10000;
			BleedResist = 0;
			ShockResist = 0;
			PoisonResist = 0;
			LifestealResist = 0;
			BleedingTime = 0;
			ShockedTime = 0;
			PoisonedTime = 0;
			BleedingMult = 0;
			ShockedMult = 0;
			PoisonedStacks = 0;

		}



		public Stats Copy()
		{
			return (Stats)MemberwiseClone();
		}
	}
}
