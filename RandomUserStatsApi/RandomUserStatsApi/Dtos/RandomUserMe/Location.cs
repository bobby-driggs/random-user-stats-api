using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Dtos.RandomUserMe
{
    public class Location
	{
		public string street { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string postcode { get; set; }
	}
}
