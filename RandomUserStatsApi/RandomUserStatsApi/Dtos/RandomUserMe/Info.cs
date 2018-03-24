using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Dtos.RandomUserMe
{
    public class Info
    {
		public string seed { get; set; }
		public int results { get; set; }
		public int page { get; set; }
		public string version { get; set; }
	}
}
