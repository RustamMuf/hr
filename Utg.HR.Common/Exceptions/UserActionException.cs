using System;

namespace Utg.HR.Common.Exceptions
{
	public class UserActionException : Exception
	{
		public UserActionException() { }

		public UserActionException(string message) : base(message)
		{ }

		public UserActionException(string message, Exception ex) : base(message, ex)
		{ }
	}
}
