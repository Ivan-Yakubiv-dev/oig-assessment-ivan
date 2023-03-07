using SurveyApp.Common.Enums;
using SurveyApp.Domain.Entities;
using System;

namespace SurveyApp.DtoModels
{
	public class PublicQuestionnaireDto
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public DateTime StartTimeUtc { get; set; }
		public DateTime EndTimeUtc { get; set; }
		public SurveyStatus Status { get; set; }

		public PublicQuestionnaireDto(Questionnaire entity)
		{
			Id = entity.Id;

			Name = entity.Name;
			StartTimeUtc = entity.StartTimeUtc;
			EndTimeUtc = entity.EndTimeUtc;
			Status = entity.Status;
		}
	}
}
