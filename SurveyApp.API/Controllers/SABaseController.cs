using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SurveyApp.Common.Constants;
using SurveyApp.Common.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;

namespace SurveyApp.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("Api/[Controller]")]
	public abstract class SABaseController : ControllerBase
	{
		// NOTE: In real scenario _currentUserId should have default null value here, "U_01" here is just a mock to avoid exceptions
		private string? _currentUserId = "U_01";
		public string CurrentUserId
		{
			get
			{
				if (string.IsNullOrEmpty(_currentUserId))
				{
					// NOTE: NameIdentifier claim type is used as example and has no meaning in a current state of system
					_currentUserId = User.Claims.FirstOrDefault(n => n.Type == ClaimTypes.NameIdentifier)?.Value
						?? throw new SAForbiddenException(ErrorConsts.UserClaimsInvalidInternalError, ErrorConsts.NoAccessPublicError);
				}

				return _currentUserId;
			}
		}

		// NOTE: Ideally, custom logger wrapper should be implemented in the system.
		//		 Custom generic logger would help to differentiate caller class (for example Controller) in requests.
		private readonly ILogger<SABaseController> _logger;

		public SABaseController(ILogger<SABaseController> logger)
		{
			_logger = logger;
		}

		[NonAction]
		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation($"API request from user '{CurrentUserId}'");

			if (!ModelState.IsValid)
			{
				var modelStateError = new SerializableError(context.ModelState);

				var exception = new SABadRequestException(
					string.Join(Environment.NewLine, modelStateError),
					ErrorConsts.ViewModelInvalidPublicError);

				throw exception;
			}
		}
	}
}