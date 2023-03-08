namespace SurveyApp.Common.Enums
{
	// NOTE: This enum should be used to differentiate type of value saved for each item per each questionnaire participant.
	//		 Items should be validated during survey submition, and SurveyItemType would point to specific validator to be used.
	//		 Also, SurveyItemType would be handy on client-side to generate specific UI component for user depending on question type.
	public enum SurveyItemType : byte
	{
		Unknown = 0,

		/// <summary>
		/// Boolean-only value ("Yes"/"No", "True"/"False") should be saved for such questionnaire items (questions).
		/// Radio buttons or pair of regular buttons (with straightforward "Yes"/"No" labels) should be used on client-side,
		/// as representation for questions of such type.
		/// </summary>
		LogicalOperator = 1,
		// NOTE: Selecting a single (or multiple) answers from predefined list would require additional entities and dependencies in DB.
		//		 Also, ideally database structure should be rethinked and refactored,
		//		 in order to have solid support for this type of questionnaire items.
		/// <summary>
		/// Number value should be saved for such questionnaire items (questions).
		/// Dropdown (where each option has string as visible value and number as inner value) should be used on client-side,
		/// as representation for questions of such type.
		/// </summary>
		AnswerSelection = 2,
		/// <summary>
		/// String value should be saved for such questionnaire items (questions).
		/// Text input, textarea input or number input should be used on client-side,
		/// as representation for questions of such type.
		/// </summary>
		OpenAnswer = 3
	}
}
