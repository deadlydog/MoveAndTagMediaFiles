namespace MoveAndTagMediaFiles;

public class UserMessageException : Exception
{
	public string UserMessageTitle { get; }
	public string UserMessage { get; }

	public Exception OriginalException { get; }

	public UserMessageException(string title, string message, Exception exception)
	{
		UserMessageTitle = title;
		UserMessage = message;
		OriginalException = exception;
	}
}
