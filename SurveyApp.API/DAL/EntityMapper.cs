using SurveyApp.Common.Enums;
using SurveyApp.Domain.Entities;
using SurveyApp.DtoModels;

namespace SurveyApp.API.DAL
{
	public static class EntityMapper
	{
		public static PublicQuestionnaireDto MapQuestionnaireFromEntity(Questionnaire entity)
		{
			return new PublicQuestionnaireDto
			{
				Id = entity.Id,

				Name = entity.Name,
				StartTimeUtc = entity.StartTimeUtc,
				EndTimeUtc = entity.EndTimeUtc,
				Status = entity.Status
			};
		}

		public static Questionnaire MapQuestionnaireToEntity(CreateQuestionnaireDto questionnaire, string ownerUserId)
		{
			return new Questionnaire
			{
				Name = questionnaire.Name,
				StartTimeUtc = questionnaire.StartTimeUtc,
				EndTimeUtc = questionnaire.EndTimeUtc,
				Status = SurveyStatus.Concept,

				OwnerId = ownerUserId
			};
		}
	}
}
