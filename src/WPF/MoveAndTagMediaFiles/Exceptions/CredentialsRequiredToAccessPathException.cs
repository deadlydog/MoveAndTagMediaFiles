namespace MoveAndTagMediaFiles;

public class CredentialsRequiredToAccessPathException : Exception
{
	public string PathThatCouldNotBeAccessed { get; init; } = string.Empty;

	public CredentialsRequiredToAccessPathException(string pathThatCouldNotBeAccessed)
	{
		PathThatCouldNotBeAccessed = pathThatCouldNotBeAccessed;
	}
}
