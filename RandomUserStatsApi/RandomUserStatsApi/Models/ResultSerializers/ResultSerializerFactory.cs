using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Models.ResultSerializers
{
    public class ResultSerializerFactory
    {
		public IResultSerializer GetResultSerializer(ResultType resultType)
		{
			switch (resultType)
			{
				case ResultType.Json:
					return new JsonResultSerializer();
				case ResultType.Xml:
					return new XmlResultSerializer();
				// default to plain text, if unkown result type
				default:
				case ResultType.Unknown:
				case ResultType.PlainText:
					return new PlainTextResultSerializer();
			}
		}
    }
}
