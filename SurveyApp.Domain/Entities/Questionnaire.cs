using SurveyApp.Common.Enums;
using System;
using System.Collections.Generic;

namespace SurveyApp.Domain.Entities
{
	public class Questionnaire : SABaseEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
		// NOTE: At the moment system does not support "topic" functionality.
		//		 It might be implemented in combination with User.Topic property as additional access/filter modification.
		public string? Topic { get; set; }
		public DateTime? StartTimeUtc { get; set; }
		public DateTime? EndTimeUtc { get; set; }
		public SurveyStatus Status { get; set; }

		public string OwnerId { get; set; }
		public User Owner { get; set; }

		public IEnumerable<QuestionnaireItem> QuestionnaireItems { get; set; }
		public IEnumerable<QuestionnaireSubmission> Submissions { get; set; }
	}
}
