using System;

namespace SurveyApp.Common.Exceptions
{
	public abstract class SABaseException : Exception
	{
		public int StatusCode { get; private set; }
		// NOTE: PublicMessage property could be used as response message (if needed) in client-server requests,
		//		 while internalMessage parameter should be used only for logging/debugging purposes and can store sensitive data.
		public string PublicMessage { get; private set; }

		public SABaseException(int statusCode, string internalMessage, string publicMessage)
			: base(internalMessage)
		{
			StatusCode = statusCode;
			PublicMessage = publicMessage;
			Data.Add("ResponseMessage", publicMessage);
		}

		public SABaseException(int statusCode, string internalMessage, Exception innerException, string publicMessage)
			: base(internalMessage, innerException)
		{
			StatusCode = statusCode;
			PublicMessage = publicMessage;
			Data.Add("ResponseMessage", publicMessage);
		}
	}
}
