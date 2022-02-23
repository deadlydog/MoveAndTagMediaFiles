using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles.Messages
{
	public class UserWarningMessage
	{
		public string Title { get; }
		public string Message { get; }

		public UserWarningMessage(string title, string message)
		{
			Title = title;
			Message = message;
		}
	}
}
