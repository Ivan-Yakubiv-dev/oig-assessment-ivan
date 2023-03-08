using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApp.API.Services.Interfaces;
using SurveyApp.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyApp.API.Controllers
{
	public abstract class QuestionnaireController : SABaseController
	{
		private readonly IQuestionnaireService _questionnaireService;

		public QuestionnaireController(ILogger<QuestionnaireController> logger, IQuestionnaireService questionnaireService)
			: base(logger)
		{
			_questionnaireService = questionnaireService;
		}

		[HttpGet]
		public async Task<IEnumerable<PublicQuestionnaireDto>> Get(FilterParams filterParams)
		{
			return await _questionnaireService.Get(filterParams)
				.ConfigureAwait(false);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public PublicQuestionnaireDto Create(CreateQuestionnaireDto questionnaireInput)
		{
			questionnaireInput.ValidateModel();

			return _questionnaireService.Create(questionnaireInput, CurrentUserId);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("Schedule")]
		public PublicQuestionnaireDto Schedule(UpdateQuestionnaireTimeRangeDto questionnaireInput)
		{
			questionnaireInput.ValidateModel();

			return _questionnaireService.Schedule(questionnaireInput, CurrentUserId);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("Reschedule")]
		public PublicQuestionnaireDto Reschedule(UpdateQuestionnaireTimeRangeDto questionnaireInput)
		{
			questionnaireInput.ValidateModel();

			return _questionnaireService.Reschedule(questionnaireInput, CurrentUserId);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{questionnaireId}/Close")]
		public PublicQuestionnaireDto Close(int questionnaireId)
		{
			return _questionnaireService.Close(questionnaireId, CurrentUserId);
		}
	}
}