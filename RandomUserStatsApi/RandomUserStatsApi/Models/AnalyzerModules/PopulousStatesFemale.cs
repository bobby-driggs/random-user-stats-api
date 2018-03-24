using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
    public class PopulousStatesFemale : AnalyzerModuleBase
    {
		public override string ModuleName { get { return "POPULOUS_STATES_FEMALE"; } }
		public override string DisplayName { get { return "Percentage of females in each state, up to the top 10 most populous states"; } }
		private Dictionary<string, PersonDataSample> StateToFemaleCount { get; set; }

		public PopulousStatesFemale()
		{
			StateToFemaleCount = new Dictionary<string, PersonDataSample>();
		}

		public override void ParsePerson(Result person)
		{
			var isFemale = false;
			if (string.IsNullOrWhiteSpace(person?.location?.state))
			{
				return;
			}

			if (!string.IsNullOrWhiteSpace(person?.gender) && person.gender.ToLower() == "female")
			{
				isFemale = true;
			}

			PersonDataSample femaleDataSample = null;
			if (!StateToFemaleCount.TryGetValue(person.location.state, out femaleDataSample))
			{
				femaleDataSample = new PersonDataSample();
			}

			femaleDataSample.TotalSampleSize++;
			if (isFemale)
			{
				femaleDataSample.SampleData++;
			}

			StateToFemaleCount[person.location.state] = femaleDataSample;
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var results = new Dictionary<string, decimal>();

			// most populous states at the top of the dictionary
			var topTen = StateToFemaleCount
				.OrderByDescending(sm => sm.Value.TotalSampleSize)
				.Take(10);

			foreach (var state in topTen)
			{
				var percent = CalculatePercent(state.Value.SampleData, state.Value.TotalSampleSize);
				results.Add(state.Key, percent);
			}

			return results;
		}
	}
}
