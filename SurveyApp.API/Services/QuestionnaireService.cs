using Microsoft.EntityFrameworkCore;
using SurveyApp.API.DAL;
using SurveyApp.API.Services.Interfaces;
using SurveyApp.Common.Constants;
using SurveyApp.Common.Enums;
using SurveyApp.Common.Exceptions;
using SurveyApp.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.API.Services
{
	public class QuestionnaireService : IQuestionnaireService
	{
		// NOTE: For more solid project structure DbContext should not be used from within Services layer,
		//		 in order to follow some sort of "single responsibility" principle.
		//		 In current state of project it is done for simplification and it worths only for proof-of-concept solutions.
		protected readonly SurveyDbContext _dbContext;

		public QuestionnaireService(SurveyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<PublicQuestionnaireDto>> Get(FilterParams filterParams)
		{
			var entities = await _dbContext.Questionnaires
				// NOTE: GenericFilter() method could be used here if it would be fully implemented and would meet requirements.
				//		 Otherwise - custom FilterQuestionnaires() method would be used here to build custom DB query.
				//.GenericFilter(filterParams)
				.FilterQuestionnaires(filterParams)
				.GenericSort(filterParams)
				.GenericPaginate(filterParams)
				.ToListAsync()
				.ConfigureAwait(false);

			return entities.Select(e => EntityMapper.MapQuestionnaireFromEntity(e));
		}

		public PublicQuestionnaireDto Create(CreateQuestionnaireDto questionnaireInput, string currentUserId)
		{
			var entity = EntityMapper.MapQuestionnaireToEntity(questionnaireInput, currentUserId);

			_dbContext.Questionnaires.Add(entity);
			_dbContext.SaveChanges();

			return EntityMapper.MapQuestionnaireFromEntity(entity);
		}

		public PublicQuestionnaireDto Schedule(UpdateQuestionnaireTimeRangeDto questionnaireInput, string currentUserId)
		{
			var entity = _dbContext.Questionnaires.FirstOrDefault(e => e.Id == questionnaireInput.Id);

			// Only the owner of the questionnaire can schedule it
			if (entity.OwnerId != currentUserId)
			{
				throw new SAForbiddenException(
					$"User '{currentUserId}' can not schedule questionnaire '{questionnaireInput.Id}'",
					ErrorConsts.NoAccessPublicError);
			}

			// Only the "concept" questionnaire can be scheduled
			if (entity.Status != SurveyStatus.Concept)
			{
				// NOTE: This exception faces ambiguity and should be refactored, and it requires general decision from stakeholders:
				//		 - all issues which are not caused by user input directly should not be described on UI in details
				//			(questionnaire status should be better validated on client-side, thats why user should not even bother about it,
				//			and in case if such problem is met user should see general "no access" message from SAForbiddenException,
				//			and in such way StartTime validation exception in Reschedule() mehtod should be changed to Forbidden as well)
				//		 - all issues should be explained on UI even if they are not caused by user input directly
				//			(in such case this exception should have more detailed public message pointing to the wrong questionnaire status,
				//			in a similar way as it is done for StartTime validation exception in Reschedule() mehtod)
				throw new SABadRequestException(
					$"Questionnaire '{questionnaireInput.Id}' with status '{entity.Status}' can not be scheduled",
					ErrorConsts.NoAccessPublicError);
			}

			entity.UpdateQuestionnaireEntityTimeRange(questionnaireInput);
			_dbContext.SaveChanges();

			return EntityMapper.MapQuestionnaireFromEntity(entity);
		}

		public PublicQuestionnaireDto Reschedule(UpdateQuestionnaireTimeRangeDto questionnaireInput, string currentUserId)
		{
			var entity = _dbContext.Questionnaires.FirstOrDefault(e => e.Id == questionnaireInput.Id);

			// Only the owner of the questionnaire can reschedule it
			if (entity.OwnerId != currentUserId)
			{
				throw new SAForbiddenException(
					$"User '{currentUserId}' can not rescheduled questionnaire '{questionnaireInput.Id}'",
					ErrorConsts.NoAccessPublicError);
			}
			
			// Only the "scheduled" questionnaire can be rescheduled
			if (entity.Status != SurveyStatus.Scheduled)
			{
				// NOTE: This exception faces ambiguity and requires general decision from stakeholders (explained in Schedule() method)
				throw new SABadRequestException(
					$"Questionnaire '{questionnaireInput.Id}' with status '{entity.Status}' can not be rescheduled",
					ErrorConsts.NoAccessPublicError);
			}

			// A questionnaire can only be rescheduled if it has not started
			// NOTE: This validation faces ambiguity and requires general technical decision (explained in SurveyStatus enum)
			if (DateTime.UtcNow.CompareTo(entity.StartTimeUtc) >= 0)
			{
				// NOTE: This exception faces ambiguity and requires general decision from stakeholders (explained in Schedule() method)
				throw new SABadRequestException(
					$"Questionnaire '{questionnaireInput.Id}' has already started and can not be rescheduled",
					ErrorConsts.QuestionnaireHasStartedPublicError);
			}

			entity.UpdateQuestionnaireEntityTimeRange(questionnaireInput);
			_dbContext.SaveChanges();

			return EntityMapper.MapQuestionnaireFromEntity(entity);
		}

		public PublicQuestionnaireDto Close(int questionnaireId, string currentUserId)
		{
			var entity = _dbContext.Questionnaires.FirstOrDefault(e => e.Id == questionnaireId);

			// Only the owner of the questionnaire can close it
			if (entity.OwnerId != currentUserId)
			{
				throw new SAForbiddenException(
					$"User '{currentUserId}' can not close questionnaire '{questionnaireId}'",
					ErrorConsts.NoAccessPublicError);
			}

			// Only the "active" questionnaire can be closed
			// NOTE: This validation faces ambiguity and requires general technical decision (explained in SurveyStatus enum)
			if (entity.Status != SurveyStatus.Active)
			{
				// NOTE: This exception faces ambiguity and requires general decision from stakeholders (explained in Schedule() method)
				throw new SABadRequestException(
					$"Questionnaire '{questionnaireId}' with status '{entity.Status}' can not be closed",
					ErrorConsts.NoAccessPublicError);
			}

			// A questionnaire can only be closed if it has started, lasted for an hour, and has not ended
			// NOTE: This validation faces ambiguity and requires general technical decision (explained in SurveyStatus enum)
			if (DateTime.UtcNow.CompareTo(entity.StartTimeUtc.Value.AddHours(1)) <= 0 ||
				DateTime.UtcNow.CompareTo(entity.EndTimeUtc) >= 0)
			{
				// NOTE: This exception faces ambiguity and requires general decision from stakeholders (explained in Schedule() method)
				throw new SABadRequestException(
					$"Questionnaire '{questionnaireId}' has not started or has not lasted long enough or has already ended, and can not be closed",
					ErrorConsts.QuestionnaireCanNotBeClosedPublicError);
			}

			entity.UpdateQuestionnaireEntityToClosed();
			_dbContext.SaveChanges();

			return EntityMapper.MapQuestionnaireFromEntity(entity);
		}
	}
}
