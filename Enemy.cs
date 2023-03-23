using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	internal class Enemy : Fighter
	{
		public double Dust { get; set; }
		public int Level { get; set; }

		public Enemy(int level, Equipment eq)
		{
			isTrimp = false;
			Level = level;
			CalcualteEnemyStats(eq);
		}

		public void CalcualteEnemyStats(Equipment eq)
		{
			BaseStats = new Stats();
			BaseStats.BleedTimeMax = 8000;
			BaseStats.ShockTimeMax = 8000;

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
			BaseStats.LifestealResist = Math.Max(Level-14, 0) * 3;

			BaseStats.AttackSpeed = 5000 * Math.Pow(0.98, Math.Min(Level, 29));

			if (eq.MenacingMask > 0)
				BaseStats.AttackSpeed *= 1.05 * Math.Pow(1.02, eq.MenacingMask-1);

			if (Level == 2)
				BaseStats.Attack *= 16/15;

		}

}
}
