using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles.Messages
{
	public class UserErrorMessage
	{
		public string Title { get; }
		public string Message { get; }
		public Exception Exception { get; }

		public UserErrorMessage(string title, string message, Exception exception)
		{
			Title = title;
			Message = message;
			Exception = exception;
		}
	}
}
