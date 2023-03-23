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
		public Fighter Enemy { get; set; }
		public int Wins;
		public int Losses;
		public long RunTime;
		public Simulation(Trimp trimp, Fighter enemy) 
		{ 
			Wins = 0;
			Losses = 0;
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
				if (fight.Simulate() == FightResult.Win)
					Wins++;
				else
					Losses++;
				RunTime += fight.FightTime;
			}

		}
	

	}
}
