using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.ResultSerializers
{
    public class SerializedRootAdapter
    {
		public SerializedRoot GetFromParsedDataResult(IEnumerable<ParsedDataResult> parsedDataResults)
		{
			var results = parsedDataResults.Select(pdr => new SerializedDataResult()
			{
				DisplayName = pdr.DisplayName,
				ModuleName = pdr.ModuleName,
				Results = pdr.Results.Select(r => new AnalysisResult() {
					Key = r.Key,
					Percent = r.Value
				}).ToArray()
			}).ToArray();

			return new SerializedRoot()
			{
				Results = results
			};
		}
    }
}
