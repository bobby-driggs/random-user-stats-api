using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models
{
    public class StatisticAnalyzer
    {
		private IEnumerable<IAnalyzerModule> ParsingModules { get; set; }

		public StatisticAnalyzer(params IAnalyzerModule[] parsingModules)
		{
			ParsingModules = parsingModules;
		}

		public StatisticAnalyzer(IEnumerable<IAnalyzerModule> parsingModules)
		{
			ParsingModules = parsingModules;
		}

		public bool AnalyzeData(IEnumerable<Result> people)
		{
			if (people == null || people.Count() == 0)
			{
				return false;
			}

			foreach (var person in people)
			{
				foreach (var module in ParsingModules)
				{
					module.ParsePerson(person);
				}
			}

			return true;
		}

		public IEnumerable<ParsedDataResult> ExtractData()
		{
			var data = new List<ParsedDataResult>();
			foreach (var module in ParsingModules)
			{
				data.Add(module.ExtractData());
			}

			return data;
		}
    }
}
