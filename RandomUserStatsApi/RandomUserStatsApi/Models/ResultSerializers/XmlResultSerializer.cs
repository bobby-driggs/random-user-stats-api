using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RandomUserStatsApi.Models.AnalyzerModules;

namespace RandomUserStatsApi.Models.ResultSerializers
{
	public class XmlResultSerializer : IResultSerializer
	{
		public SerializedResult Serialize(IEnumerable<ParsedDataResult> parsedResults)
		{
			var adapter = new SerializedRootAdapter();
			var root = adapter.GetFromParsedDataResult(parsedResults);

			var xmlSerializer = new XmlSerializer(root.GetType());

			using (var textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, root);
				var xmlData = textWriter.ToString();

				return new SerializedResult(xmlData, "application/xml");
			}
		}
	}
}
