using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
    public class PopulousStatesMale : AnalyzerModuleBase
	{
		public override string ModuleName { get { return "POPULOUS_STATES_MALE"; } }
		public override string DisplayName { get { return "Percentage of males in each state, up to the top 10 most populous states"; } }
		private Dictionary<string, PersonDataSample> StateToMaleCount { get; set; }

		public PopulousStatesMale()
		{
			StateToMaleCount = new Dictionary<string, PersonDataSample>();
		}

		public override void ParsePerson(Result person)
		{
			var isMale = false;
			if(string.IsNullOrWhiteSpace(person?.location?.state))
			{
				return;
			}

			if (!string.IsNullOrWhiteSpace(person?.gender) && person.gender.ToLower() == "male")
			{
				isMale = true;
			}

			PersonDataSample maleDataSample = null;
			if (!StateToMaleCount.TryGetValue(person.location.state, out maleDataSample))
			{
				maleDataSample = new PersonDataSample();
			}

			maleDataSample.TotalSampleSize++;
			if (isMale)
			{
				maleDataSample.SampleData++;
			}

			StateToMaleCount[person.location.state] = maleDataSample;
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var results = new Dictionary<string, decimal>();

			// most populous states at the top of the dictionary
			var topTen = StateToMaleCount
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
