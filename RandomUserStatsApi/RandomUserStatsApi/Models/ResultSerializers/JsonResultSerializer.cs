using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.ResultSerializers
{
	public class JsonResultSerializer : IResultSerializer
	{
		public SerializedResult Serialize(IEnumerable<ParsedDataResult> parsedResults)
		{
			var adapter = new SerializedRootAdapter();
			var root = adapter.GetFromParsedDataResult(parsedResults);

			var jsonData = JsonConvert.SerializeObject(root);

			return new SerializedResult(jsonData, "application/json");
		}
	}
}

