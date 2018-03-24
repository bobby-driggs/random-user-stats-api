using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.ResultSerializers
{
    public class SerializedRoot
    {
		public SerializedDataResult[] Results;
	}

	public class SerializedDataResult
	{
		public string ModuleName { get; set; }
		public string DisplayName { get; set; }

		// XML Serializer cant handle dictionaries, so we need to rebuild as a key value list
		public AnalysisResult[] Results { get; set; }
	}

	public class AnalysisResult
	{
		public string Key { get; set; }
		public decimal Percent { get; set; }
	}
}
