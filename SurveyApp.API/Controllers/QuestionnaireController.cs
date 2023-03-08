using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApp.API.Services.Interfaces;
using SurveyApp.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyApp.API.Controllers
{
	public class QuestionnaireController : SABaseController
	{
		private readonly IQuestionnaireService _questionnaireService;

		public QuestionnaireController(ILogger<QuestionnaireController> logger, IQuestionnaireService questionnaireService)
			: base(logger)
		{
			_questionnaireService = questionnaireService;
		}

		[HttpGet]
		public async Task<IEnumerable<PublicQuestionnaireDto>> Get([FromQuery] FilterParams filterParams)
		{
			return await _questionnaireService.Get(filterParams)
				.ConfigureAwait(false);
		}

		// NOTE: [Authorize] attribute commented as long as authorization is mocked in current state of the system
		//[Authorize(Roles = "Admin")]
		[HttpPost]
		public PublicQuestionnaireDto Create([FromBody] CreateQuestionnaireDto questionnaireInput)
		{
			questionnaireInput.ValidateModel();

			return _questionnaireService.Create(questionnaireInput, CurrentUserId);
		}

		// NOTE: [Authorize] attribute commented as long as authorization is mocked in current state of the system
		//[Authorize(Roles = "Admin")]
		[HttpPut("Schedule")]
		public PublicQuestionnaireDto Schedule([FromBody] UpdateQuestionnaireTimeRangeDto questionnaireInput)
		{
			questionnaireInput.ValidateModel();

			return _questionnaireService.Schedule(questionnaireInput, CurrentUserId);
		}

		// NOTE: [Authorize] attribute commented as long as authorization is mocked in current state of the system
		//[Authorize(Roles = "Admin")]
		[HttpPut("Reschedule")]
		public PublicQuestionnaireDto Reschedule([FromBody] UpdateQuestionnaireTimeRangeDto questionnaireInput)
		{
			questionnaireInput.ValidateModel();

			return _questionnaireService.Reschedule(questionnaireInput, CurrentUserId);
		}

		// NOTE: [Authorize] attribute commented as long as authorization is mocked in current state of the system
		//[Authorize(Roles = "Admin")]
		[HttpPut("{questionnaireId}/Close")]
		public PublicQuestionnaireDto Close([FromRoute] int questionnaireId)
		{
			return _questionnaireService.Close(questionnaireId, CurrentUserId);
		}
	}
}