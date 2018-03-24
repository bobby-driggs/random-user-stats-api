using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
	public class FemaleMrs : AnalyzerModuleBase
	{
		public override string ModuleName { get { return "FEMALE_MRS"; } }
		public override string DisplayName { get { return "Percentage of females whose title is 'mrs'"; } }

		private decimal MrsCount { get; set; }
		private decimal TotalSampleSize { get; set; }

		public FemaleMrs()
		{
			MrsCount = decimal.Zero;
			TotalSampleSize = decimal.Zero;
		}

		public override void ParsePerson(Result person)
		{
			if (string.IsNullOrWhiteSpace(person?.gender))
			{
				return;
			}

			// in case there are non-binary or 'other' users in the system, we are only comparing female vs male
			if (person.gender.ToLower() == "female")
			{
				if (person.name.title.ToLower() == "mrs")
				{
					MrsCount++;
				}

				TotalSampleSize++;
			}
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var percent = CalculatePercent(MrsCount, TotalSampleSize);
			return new Dictionary<string, decimal>() {
				{  "Mrs", percent }
			};
		}
	}
}
