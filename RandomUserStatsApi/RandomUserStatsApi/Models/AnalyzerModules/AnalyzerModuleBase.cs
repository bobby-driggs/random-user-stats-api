using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
	public abstract class AnalyzerModuleBase : IAnalyzerModule
	{
		public abstract string ModuleName { get; }
		public abstract string DisplayName { get; }
		public abstract void ParsePerson(Result person);
		protected abstract IDictionary<string, decimal> GetResults();

		protected decimal CalculatePercent(decimal value, decimal sampleSize)
		{
			if (sampleSize == 0)
			{
				return decimal.Zero;
			}

			return (value / sampleSize) * 100M;
		}


		public ParsedDataResult ExtractData()
		{
			var results = GetResults();
			return new ParsedDataResult(ModuleName, DisplayName, results);
		}
	}
}
