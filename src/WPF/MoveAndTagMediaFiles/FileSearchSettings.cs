using System.Security;

namespace MoveAndTagMediaFiles;

public record FileSearchSettings
{
	public string SourceDirectory { get; init; } = string.Empty;
	public string FileSearchPattern { get; init; } = String.Empty;
	public bool SearchSubdirectories { get; init; } = true;
	public bool UseCredentials { get; init; } = false;
	public string CredentialsUsername { get; init; } = string.Empty;
	public SecureString CredentialsPassword { get; init; } = new SecureString();
}
