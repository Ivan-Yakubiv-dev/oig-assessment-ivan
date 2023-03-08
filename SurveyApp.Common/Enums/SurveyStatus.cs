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
		// NOTE: Active status requires general technical decision to be made:
		//		 - SurveyApp server should have background proccess which runs continously (aka Linux demon),
		//			checks current system time with some time step, and updates all questionnaires statuses when scheduled time comes
		//		 - SurveyApp server should support scheduling process (aka Windows Task Scheduler), which would be planned automatically
		//			when questionnaire's Schedule() endpoint is called, and would update single questionnaire status
		//		 - "Active" status should be removed from enum and treated as abstract one without actual implementation in code
		//			(additional method could be added to Questionnaire entity which would dynamically calculate its "active/inactive" state
		//			by checking combination of Status/StartTime/EndTime properties to meet business requirements)
		/// <summary>
		/// The questionnaire's start date and time are in the past, while its end date and time are in the future.
		/// Only in this state can the questions be answered
		/// </summary>
		Active = 3,
		// NOTE: This validation faces the same ambiguity as "Active" status and requires general technical decision.
		/// <summary>
		/// The questionnaire's end date and time have passed. No more questions can be answered
		/// </summary>
		Completed = 4
	}
}
