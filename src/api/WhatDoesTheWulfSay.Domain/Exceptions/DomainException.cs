﻿namespace WhatDoesTheWulfSay.Domain.Exceptions
{
    class DomainException : Exception
	{
		internal DomainException(string message)
			: base(message)
		{
		}

		internal DomainException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
