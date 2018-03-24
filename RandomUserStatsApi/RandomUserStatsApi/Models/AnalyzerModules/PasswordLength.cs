using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserStatsApi.Dtos.RandomUserMe;

namespace RandomUserStatsApi.Models.AnalyzerModules
{
	public class PasswordLength : AnalyzerModuleBase
	{
		public override string ModuleName { get { return "PASSWORD_LENGTH"; } }
		public override string DisplayName { get { return "Percentage of people's passwords which are 8 characters or greater"; } }
		private decimal PasswordLengthCount { get; set; }
		private decimal TotalSampleSize { get; set; }

		public PasswordLength()
		{
			PasswordLengthCount = decimal.Zero;
		}

		public override void ParsePerson(Result person)
		{
			if (string.IsNullOrWhiteSpace(person?.login?.password))
			{
				return;
			}

			// in case there are non-binary or 'other' users in the system, we are only comparing female vs male
			if (person.login.password.Length >= 8)
			{
				PasswordLengthCount++;
			}

			TotalSampleSize++;
		}

		protected override IDictionary<string, decimal> GetResults()
		{
			var percent = CalculatePercent(PasswordLengthCount, TotalSampleSize);
			return new Dictionary<string, decimal>() {
				{  "8+ Characters", percent }
			};
		}
	}
}
