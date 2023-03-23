using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAssault
{
	internal class Fighter
	{
		public Stats BaseStats { get; set; }
		public Stats CurrentStats { get; set; }
		public bool isTrimp { get; set; }
		public int Enraging { get; set; }

		public Fighter()
		{

		}
		public Fighter Copy()
		{
			Fighter f = (Fighter)MemberwiseClone();
			f.BaseStats = BaseStats.Copy();
			f.CurrentStats = CurrentStats.Copy();
			return f;
		}


	}
}
