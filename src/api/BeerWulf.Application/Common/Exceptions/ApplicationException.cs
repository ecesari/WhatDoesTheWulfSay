namespace WhatDoesTheWulfSay.Application.Exceptions
{
    class ApplicationException : Exception
	{
		internal ApplicationException(string message)
			: base(message)
		{
		}

		internal ApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
