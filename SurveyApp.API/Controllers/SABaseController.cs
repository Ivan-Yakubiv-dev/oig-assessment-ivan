using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
		// NOTE: In real scenario _userId value should be null here, "U_01" here is just a mock to avoid exceptions
		private string? _userId = "U_01";
		public string UserId
		{
			get
			{
				if (string.IsNullOrEmpty(_userId))
				{
					// NOTE: NameIdentifier claim type is used as example and has no meaning in a current state of system
					_userId = User.Claims.FirstOrDefault(n => n.Type == ClaimTypes.NameIdentifier)?.Value
						?? throw new SABaseException(
							StatusCodes.Status403Forbidden,
							ErrorConsts.UserClaimsInvalidInternalError,
							ErrorConsts.NoAccessPublicError);
				}

				return _userId;
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
			_logger.LogInformation($"API request from user '{UserId}'");

			if (!ModelState.IsValid)
			{
				var modelStateError = new SerializableError(context.ModelState);

				var exception = new SABaseException(
					StatusCodes.Status400BadRequest,
					string.Join(Environment.NewLine, modelStateError),
					ErrorConsts.ViewModelInvalidPublicError);

				throw exception;
			}
		}
	}
}