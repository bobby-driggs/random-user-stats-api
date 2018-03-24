using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Models
{

	public class AnalysisResult
	{
		public Analysis[] analysis { get; set; }
	}

	public class Analysis
	{
		public string name { get; set; }
 		public string display { get; set; }
		public IDictionary<string, decimal> results { get; set; }
	}
}
