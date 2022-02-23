using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles.Messages
{
	public class UserInformationMessage
	{
		public string Title { get; }
		public string Message { get; }

		public UserInformationMessage(string title, string message)
		{
			Title = title;
			Message = message;
		}
	}
}
