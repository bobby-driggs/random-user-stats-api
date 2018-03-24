using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.ResultSerializers
{
    public interface IResultSerializer
    {
		SerializedResult Serialize(IEnumerable<ParsedDataResult> parsedResults);
    }
}
