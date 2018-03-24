using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
    public class AgeRange : AnalyzerModuleBase
    {
		public override string ModuleName { get { return "AGE_RANGE"; } }
		public override string DisplayName { get { return "Percentage of people in the following age ranges"; } }

		private const double DAYS_IN_YEAR = 365.0;
		private const int AGE_100 = 100;
		private const int AGE_81 = 81;
		private const int AGE_61 = 61;
		private const int AGE_41 = 41;
		private const int AGE_21 = 21;
		private const int AGE_0 = 0;

		private Dictionary<int, decimal> AgeRangeCounts { get; set; }
		private decimal TotalSampleSize { get; set; }


		public AgeRange()
		{
			AgeRangeCounts = new Dictionary<int, decimal>()
			{
				{ AGE_0, decimal.Zero },
				{ AGE_21, decimal.Zero },
				{ AGE_41, decimal.Zero },
				{ AGE_61, decimal.Zero },
				{ AGE_81, decimal.Zero },
				{ AGE_100, decimal.Zero }
			};
		}

		public override void ParsePerson(Result person)
		{
			var dob = DateTime.Parse(person.dob);
			var ageInYears = Math.Floor((DateTime.UtcNow - dob).TotalDays / DAYS_IN_YEAR);

			if (ageInYears >= AGE_100)
			{
				AgeRangeCounts[AGE_100]++;
			}
			else if (ageInYears >= AGE_81)
			{
				AgeRangeCounts[AGE_81]++;
			}
			else if (ageInYears >= AGE_61)
			{
				AgeRangeCounts[AGE_61]++;
			}
			else if (ageInYears >= AGE_41)
			{
				AgeRangeCounts[AGE_41]++;
			}
			else if (ageInYears >= AGE_21)
			{
				AgeRangeCounts[AGE_21]++;
			}
			else if (ageInYears >= AGE_0)
			{
				AgeRangeCounts[AGE_0]++;
			}

			TotalSampleSize++;
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var results = new Dictionary<string, decimal>();

			foreach (var age in AgeRangeCounts)
			{
				var percent = CalculatePercent(age.Value, TotalSampleSize);
				results.Add(age.Key.ToString(), percent);
			}

			return results;
		}
	}
}
