namespace SurveyApp.Common.Constants
{
	public static class ErrorConsts
	{
		public const string UserClaimsInvalidInternalError = "User claims are not valid";

		public const string WrongQuestionnaireNamePublicError = "Error! Wrong questionnaire name passed. Please, recheck your input and try again.";
		public const string WrongQuestionnaireDatePublicError = "Error! Wrong time range selected for questionnaire. Please, recheck your input and try again.";
		public const string QuestionnaireHasStartedPublicError = "Error! Questionnaire has already started.";
		public const string QuestionnaireCanNotBeClosedPublicError = "Error! Questionnaire has not started or has not lasted long enough or has already ended.";
		public const string WrongFilterParamsPublicError = "Error! Wrong parameters were passed to filter data.";
		public const string NoAccessPublicError = "Error! You have no access to do that. Please, ask your administrator for more details.";
		public const string ViewModelInvalidPublicError = "Error! Passed data contains wrong information. Please, recheck your input and try again.";
	}
}
