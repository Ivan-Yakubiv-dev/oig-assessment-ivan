using System.Collections.Generic;

namespace SurveyApp.Domain.Entities
{
	public class QuestionnaireSubmission : SABaseEntity
	{
		public int QuestionnaireId { get; set; }
		public Questionnaire Questionnaire { get; set; }

		public string ParticipantId { get; set; }
		public User Participant { get; set; }

		public IEnumerable<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
	}
}
