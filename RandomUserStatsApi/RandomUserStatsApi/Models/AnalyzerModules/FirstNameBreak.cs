using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
    public class FirstNameBreak : AnalyzerModuleBase
    {
		public override string ModuleName { get { return "FIRST_NAME_BREAK"; } }
		public override string DisplayName { get { return "Percentage of first names that start with A‐M versus N‐Z"; } }
		private decimal AtoM { get; set; }
		private decimal TotalSampleSize { get; set; }

		public FirstNameBreak()
		{
			AtoM = decimal.Zero;
		}

		public override void ParsePerson(Result person)
		{
			if (person == null || person?.name == null || string.IsNullOrWhiteSpace(person.name.first))
			{
				return;
			}

			var firstNameCharacter = person.name.first.ToUpper().FirstOrDefault();

			// Names like  "علی رضا" do not fall into the A-Z sample size, so we wont count it in our sample size
			if ('A' < firstNameCharacter && 'Z' > firstNameCharacter)
			{
				if ('A' < firstNameCharacter && 'M' > firstNameCharacter)
				{
					AtoM++;
				}

				TotalSampleSize++;
			}
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var percent = CalculatePercent(AtoM, TotalSampleSize);
			return new Dictionary<string, decimal>() {
				{  "A to M", percent }
			};
		}
	}
}
