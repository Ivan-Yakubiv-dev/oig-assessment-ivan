using SurveyApp.Common.Enums;
using System;
using System.Collections.Generic;

namespace SurveyApp.Domain.Entities
{
	public class Questionnaire : SABaseEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public DateTime StartTimeUtc { get; set; }
		public DateTime EndTimeUtc { get; set; }
		public SurveyStatus Status { get; set; }

		public string OwnerId { get; set; }
		public User Owner { get; set; }

		public IEnumerable<QuestionnaireItem> QuestionnaireItems { get; set; }
		public IEnumerable<QuestionnaireSubmission> Submissions { get; set; }
	}
}
