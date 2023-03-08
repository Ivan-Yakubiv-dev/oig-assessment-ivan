using SurveyApp.Common.Constants;
using SurveyApp.Common.Exceptions;
using System;

namespace SurveyApp.DtoModels
{
	public class UpdateQuestionnaireTimeRangeDto
	{
		public int Id { get; set; }
		public DateTime StartTimeUtc { get; set; }
		public DateTime EndTimeUtc { get; set; }

		// NOTE: ValidateModel() method only checks user's input as a first-step validation.
		//		 According to this rule, "Id" property is not validated on this level as long as its value is managed by client-side code.
		public void ValidateModel()
		{
			// A questionnaire can only be rescheduled for a future date/time
			if (DateTime.UtcNow.CompareTo(StartTimeUtc) >= 0)
			{
				throw new SABadRequestException(
					$"Invalid '{nameof(StartTimeUtc)}' property value",
					ErrorConsts.WrongQuestionnaireDatePublicError);
			}

			// The end date/time of questionnaire should be at least one hour after the beginning date/time
			if (StartTimeUtc.AddHours(1).CompareTo(EndTimeUtc) > 0)
			{
				throw new SABadRequestException(
					$"Invalid '{nameof(EndTimeUtc)}' property value",
					ErrorConsts.WrongQuestionnaireDatePublicError);
			}
		}
	}
}
