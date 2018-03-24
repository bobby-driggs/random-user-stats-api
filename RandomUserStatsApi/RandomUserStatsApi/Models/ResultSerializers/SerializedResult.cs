using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Models.ResultSerializers
{
	public class SerializedResult
	{
		public string SerializedData { get; private set; }
		public string ContentType { get; private set; }

		public SerializedResult(string serializedData, string contentType)
		{
			SerializedData = serializedData;
			ContentType = contentType;
		}
	}
}
