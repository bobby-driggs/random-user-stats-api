using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.ResultSerializers
{
	public class PlainTextResultSerializer : IResultSerializer
	{
		public SerializedResult Serialize(IEnumerable<ParsedDataResult> parsedResults)
		{
			var lines = new List<string>();

			foreach (var parsedResult in parsedResults)
			{
				if (parsedResult.Results.Count == 1)
				{
					lines.Add($"{parsedResult.DisplayName}: %{Math.Round(parsedResult.Results.First().Value, 2)}");
				}
				else
				{
					lines.Add($"{parsedResult.DisplayName}:");

					foreach (var result in parsedResult.Results)
					{
						lines.Add($" * {result.Key}: %{Math.Round(result.Value, 2)}");
					}
				}
			}

			return new SerializedResult(string.Join('\n', lines), "text/plain");
		}
	}
}
