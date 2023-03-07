using SurveyApp.Common.Enums;
using System;

namespace SurveyApp.Domain.Entities
{
	public class Questionnaire : SABaseEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public DateTime StartTimeUtc { get; set; }
		public DateTime EndTimeUtc { get; set; }
		public SurveyStatus Status { get; set; }
	}
}
