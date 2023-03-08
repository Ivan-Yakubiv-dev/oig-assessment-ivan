using SurveyApp.Common.Constants;
using SurveyApp.Common.Exceptions;
using System;

namespace SurveyApp.DtoModels
{
	public class CreateQuestionnaireDto
	{
		public string Name { get; set; }
		public DateTime StartTimeUtc { get; set; }
		public DateTime EndTimeUtc { get; set; }

		public void ValidateModel()
		{
			// A questionnaire can only be scheduled for a future date/time
			if (DateTime.UtcNow.CompareTo(StartTimeUtc) >= 0)
			{
				throw new SABadRequestException($"Invalid '{nameof(StartTimeUtc)}' property value", ErrorConsts.WrongQuestionnaireDatePublicError);
			}

			// The end date/time of questionnaire should be at least one hour after the beginning date/time
			if (StartTimeUtc.AddHours(1).CompareTo(EndTimeUtc) > 0)
			{
				throw new SABadRequestException($"Invalid '{nameof(EndTimeUtc)}' property value", ErrorConsts.WrongQuestionnaireDatePublicError);
			}
		}
	}
}
