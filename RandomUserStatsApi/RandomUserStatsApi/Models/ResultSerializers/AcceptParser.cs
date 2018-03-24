using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace RandomUserStatsApi.Models.ResultSerializers
{
	public enum ResultType
	{
		Unknown,
		Json,
		Xml,
		PlainText
	}

    public class AcceptParser
    {
		public ResultType ParseHeader(IHeaderDictionary headers)
		{
			StringValues acceptHeader;

			if(headers.TryGetValue("Accept", out acceptHeader))
			{
				var val = acceptHeader.FirstOrDefault();
				switch (val)
				{
					case "text/plain":
						return ResultType.PlainText;
					case "application/json":
						return ResultType.Json;
					case "application/xml":
					case "text/xml":
						return ResultType.Xml;
					default:
						return ResultType.Unknown;
				}
			}

			return ResultType.Unknown;
		}
	}
}
