using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	internal class Simulation
	{
		public Trimp Trimp { get; set; }
		public Enemy Enemy { get; set; }
		public int Wins;
		public int Losses;
		public double Dust;
		public long RunTime;
		public Simulation(Trimp trimp, Enemy enemy) 
		{ 
			Wins = 0;
			Losses = 0;
			Dust = 0;
			Trimp = trimp;
			Enemy = enemy;
			RunTime = 0;
		}

		public void ProcessTime(int time)
		{

		}

		public void ProcessFights(int Fights)
		{
			for (int i = 0; i < Fights; i++)
			{
				Fight fight = new Fight(this);
				double dust;
				if (fight.Simulate(out dust) == FightResult.Win)
				{
					Wins++;
					Dust += dust;
				}
				else
					Losses++;
				RunTime += fight.FightTime;
			}

		}
	

	}
}
