namespace SurveyApp.Common.Enums
{
	public enum SurveyStatus : byte
	{
		Unknown = 0,

		/// <summary>
		/// The questionnaire is intentionally inactive, and cannot be administered
		/// </summary>
		Concept = 1,
		/// <summary>
		/// The start date and time of the questionnaire are in the future. No questions can be answered yet
		/// </summary>
		Scheduled = 2,
		/// <summary>
		/// The questionnaire's start date and time are in the past, while its end date and time are in the future.
		/// Only in this state can the questions be answered
		/// </summary>
		Active = 3,
		/// <summary>
		/// The questionnaire's end date and time have passed. No more questions can be answered
		/// </summary>
		Completed = 4
	}
}
