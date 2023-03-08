using Microsoft.AspNetCore.Http;
using System;

namespace SurveyApp.Common.Exceptions
{
	// NOTE: For large systems exceptions with BadRequest status code might be split into several classes to improve errors handling.
	//		 For example, such classes might be created:
	//		- WrongRequestHeadersException (client-side code error, custom API header is missing);
	//		- InvalidUserInputException (user error or client-side validation error, value does not meet requirements);
	//		- EntityDuplicationException (user error or client-side code error, entity with passed unique name already exists).
	//		- etc.
	public class SABadRequestException : SABaseException
	{
		public SABadRequestException(string internalMessage, string publicMessage)
			: base(StatusCodes.Status400BadRequest, internalMessage, publicMessage)
		{
		}

		public SABadRequestException(string internalMessage, Exception innerException, string publicMessage)
			: base(StatusCodes.Status400BadRequest, internalMessage, innerException, publicMessage)
		{
		}
	}
}
