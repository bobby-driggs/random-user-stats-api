using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
	public class FemaleVsMale : AnalyzerModuleBase
	{
		public override string ModuleName { get { return "FEMALE_VS_MALE"; } }
		public override string DisplayName { get { return "Percentage female versus male"; } }

		private decimal FemaleCount { get; set; }
		private decimal TotalSampleSize { get; set; }

		public FemaleVsMale()
		{
			FemaleCount = decimal.Zero;
		}

		public override void ParsePerson(Result person)
		{
			if (string.IsNullOrWhiteSpace(person?.gender))
			{
				return;
			}

			// in case there are non-binary or 'other' users in the system, we are only comparing female vs male
			if (person.gender.ToLower() == "female" || person.gender.ToLower() == "male")
			{
				if (person.gender.ToLower() == "female")
				{
					FemaleCount++;
				}

				TotalSampleSize++;
			}
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var percent = CalculatePercent(FemaleCount, TotalSampleSize);
			return new Dictionary<string, decimal>() {
				{  "Female", percent }
			};
		}
	}
}
