namespace MoveAndTagMediaFiles;

public record FileSearchSettings
{
	public string SourceDirectory { get; init; } = string.Empty;
	public string FileSearchPattern { get; init; } = String.Empty;
	public bool SearchSubdirectories { get; init; } = true;
}
