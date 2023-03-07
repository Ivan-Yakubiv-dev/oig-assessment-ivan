namespace SurveyApp.Domain.Entities
{
	public class QuestionnaireAnswer : SABaseEntity
	{
		public int QuestionnaireItemId { get; set; }
		public QuestionnaireItem QuestionnaireItem { get; set; }

		public string QuestionnaireSubmissionId { get; set; }
		public QuestionnaireSubmission QuestionnaireSubmission { get; set; }

		// NOTE: In a current app structure all answers should be saved in database as a plain string.
		//		 When answer is submitted by user, validations should be performed according to question type, and answer should be stringified.
		//		 In order to support selecting predefined answer options, this string property might be removed,
		//		 and connection to exact answer entity might be set up instead.
		public string AnswerValue { get; set; }
	}
}
