using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
    public class ParsedDataResult
	{
		public string ModuleName { get; private set; }
		public string DisplayName { get; private set; }
		public IDictionary<string, decimal> Results { get; private set; }

		public ParsedDataResult(string moduleName, string displayName, IDictionary<string, decimal> results)
		{
			ModuleName = moduleName;
			DisplayName = displayName;
			Results = results;
		}

		public ParsedDataResult(string moduleName, string displayName, decimal result)
		{
			ModuleName = moduleName;
			Results = new Dictionary<string, decimal>() {
				{ moduleName, result }
			};
		}
	}
}
