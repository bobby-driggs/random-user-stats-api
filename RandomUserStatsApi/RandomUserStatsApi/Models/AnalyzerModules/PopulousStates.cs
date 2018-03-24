using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
    public class PopulousStates : AnalyzerModuleBase
    {
		public override string ModuleName { get { return "POPULOUS_STATES"; } }
		public override string DisplayName { get { return "Percentage of people in each state, up to the top 10 most populous states"; } }
		private Dictionary<string, decimal> StatePersonCount { get; set; }
		private decimal TotalSampleSize { get; set; }

		public PopulousStates()
		{
			StatePersonCount = new Dictionary<string, decimal>();
		}

		public override void ParsePerson(Result person)
		{
			if (string.IsNullOrWhiteSpace(person?.location?.state))
			{
				return;
			}

			decimal statePopulation = decimal.Zero;
			StatePersonCount.TryGetValue(person.location.state, out statePopulation);

			statePopulation++;
			TotalSampleSize++;

			StatePersonCount[person.location.state] = statePopulation;
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var results = new Dictionary<string, decimal>();

			// most populous states at the top of the dictionary
			var topTen = StatePersonCount
				.OrderByDescending(sm => sm.Value)
				.Take(10);

			foreach (var state in topTen)
			{
				var percent = CalculatePercent(state.Value, TotalSampleSize);
				results.Add(state.Key, percent);
			}

			return results;
		}
	}
}
