namespace MoveAndTagMediaFiles;

public class InvalidCredentialsException : Exception
{
	public string Domain { get; init; } = string.Empty;
	public string Username { get; init; } = string.Empty;
	public int ErrorCode { get; init; } = 0;

	public InvalidCredentialsException(string domain, string username, int errorCode)
	{
		Domain = domain;
		Username = username;
		ErrorCode = errorCode;
	}
}
