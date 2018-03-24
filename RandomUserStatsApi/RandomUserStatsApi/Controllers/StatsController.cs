using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomUserStatsApi.Dtos.RandomUserMe;
using RandomUserStatsApi.Models;
using RandomUserStatsApi.Models.ResultSerializers;
using RandomUserStatsApi.Models.AnalyzerModules;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace RandomUserStatsApi.Controllers
{
	[Route("api/v1/[controller]")]
	[EnableCors("AllowAllOrigins")]
	public class StatsController : Controller
	{
		// GET api/v1/stats/echo?
		[HttpGet("echo")]
		public string Echo(string s)
		{
			return s;
		}

		// POST api/v1/stats
		[HttpPost]
		public IActionResult Post([FromBody]RandomUserMeData body)
		{
			if (body == null || body.info == null || body.results == null)
			{
				return BadRequest("Malformed request data");
			}

			var registeredAnalyzers = new IAnalyzerModule[] {
				new FemaleVsMale(),
				new FirstNameBreak(),
				new LastNameBreak(),
				new PopulousStatesMale(),
				new PopulousStatesFemale(),
				new PopulousStates(),
				new AgeRange(),
				new PasswordLength(),
				new FemaleMrs()
			};

			var analyzer = new StatisticAnalyzer(registeredAnalyzers);
			analyzer.AnalyzeData(body.results);

			var data = analyzer.ExtractData();

			var acceptParser = new AcceptParser();
			var returnType = acceptParser.ParseHeader(HttpContext.Request.Headers);

			var resultFactory = new ResultSerializerFactory();
			var resultSerializer = resultFactory.GetResultSerializer(returnType);

			var serializedData = resultSerializer.Serialize(data);

			using (var memStream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(memStream))
				{
					streamWriter.Write(serializedData.SerializedData);
					streamWriter.Flush();
				}

				return File(memStream.ToArray(), serializedData.ContentType);
			}
		}

	}
}
