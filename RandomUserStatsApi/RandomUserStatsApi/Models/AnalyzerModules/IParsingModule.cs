using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
	public interface IAnalyzerModule
	{
		string ModuleName { get; }
		string DisplayName { get; }
		void ParsePerson(Result person);
		ParsedDataResult ExtractData();
	}
}
