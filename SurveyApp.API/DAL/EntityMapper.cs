using SurveyApp.Common.Enums;
using SurveyApp.Domain.Entities;
using SurveyApp.DtoModels;
using System;

namespace SurveyApp.API.DAL
{
	public static class EntityMapper
	{
		/// <summary>
		/// Map Questionnaire entity value to DTO.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static PublicQuestionnaireDto MapQuestionnaireFromEntity(Questionnaire entity)
		{
			return new PublicQuestionnaireDto
			{
				Id = entity.Id,

				Name = entity.Name,
				Topic = entity.Topic,
				StartTimeUtc = entity.StartTimeUtc,
				EndTimeUtc = entity.EndTimeUtc,
				Status = entity.Status
			};
		}


		/// <summary>
		/// Map DTO values to Questionnaire entity.
		/// Important: Questionnaire status will automatically be set to Concept.
		/// </summary>
		/// <param name="questionnaire"></param>
		/// <param name="ownerUserId"></param>
		/// <returns></returns>
		public static Questionnaire MapQuestionnaireToEntity(CreateQuestionnaireDto questionnaire, string ownerUserId)
		{
			return new Questionnaire
			{
				Name = questionnaire.Name,
				Topic = questionnaire.Topic,
				StartTimeUtc = questionnaire.StartTimeUtc,
				EndTimeUtc = questionnaire.EndTimeUtc,
				Status = SurveyStatus.Concept,

				OwnerId = ownerUserId
			};
		}

		/// <summary>
		/// Update StartTime and EndTime values of Questionnaire entity.
		/// Important: Questionnaire status will automatically be set to Scheduled
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="timeRange"></param>
		public static void UpdateQuestionnaireEntityTimeRange(this Questionnaire entity, UpdateQuestionnaireTimeRangeDto timeRange)
		{
			entity.StartTimeUtc = timeRange.StartTimeUtc;
			entity.EndTimeUtc = timeRange.EndTimeUtc;
			entity.Status = SurveyStatus.Scheduled;
		}

		/// <summary>
		/// Set current Date/Time as EndTime value of Questionnaire entity.
		/// Important: Questionnaire status will automatically be set to Completed
		/// </summary>
		/// <param name="entity"></param>
		public static void UpdateQuestionnaireEntityToClosed(this Questionnaire entity)
		{
			entity.EndTimeUtc = DateTime.UtcNow;
			entity.Status = SurveyStatus.Completed;
		}
	}
}
