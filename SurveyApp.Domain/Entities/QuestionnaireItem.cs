using SurveyApp.Common.Enums;
using System.Collections.Generic;

namespace SurveyApp.Domain.Entities
{
	public class QuestionnaireItem : SABaseEntity
	{
		public int Id { get; set; }

		public string Title { get; set; }
		public SurveyItemType Type { get; set; }

		public int QuestionnaireId { get; set; }
		public Questionnaire Questionnaire { get; set; }

		public IEnumerable<QuestionnaireAnswer> Answers { get; set; }
	}
}
