namespace MoveAndTagMediaFiles;

public class InvalidCredentialsToAccessPathException : Exception
{
	public string PathThatCouldNotBeAccessed { get; init; } = string.Empty;

	public InvalidCredentialsToAccessPathException(string pathThatCouldNotBeAccessed)
	{
		PathThatCouldNotBeAccessed = pathThatCouldNotBeAccessed;
	}
}
