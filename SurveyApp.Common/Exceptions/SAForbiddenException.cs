using Microsoft.AspNetCore.Http;
using System;

namespace SurveyApp.Common.Exceptions
{
	// NOTE: Constructors for access-related exceptions might require to pass userId,
	//		 in order to bundle all valuable information within exception,
	//		 to locate and solve problem or notify user if exception reason is known.
	public class SAForbiddenException : SABaseException
	{
		public SAForbiddenException(string internalMessage, string publicMessage)
			: base(StatusCodes.Status403Forbidden, internalMessage, publicMessage)
		{
		}

		public SAForbiddenException(string internalMessage, Exception innerException, string publicMessage)
			: base(StatusCodes.Status403Forbidden, internalMessage, innerException, publicMessage)
		{
		}
	}
}
