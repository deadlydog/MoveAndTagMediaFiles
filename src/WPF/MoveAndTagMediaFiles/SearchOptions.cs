namespace MoveAndTagMediaFiles;

public record SearchOptions
{
	public string SourceDirectory { get; init; } = string.Empty;
	public string DestinationDirectory { get; init; } = string.Empty;
	public string FileSearchPattern { get; init; } = String.Empty;
	public bool SearchSubdirectories { get; init; } = true;
	public bool PreserveDirectoryStructure { get; init; } = true;
}
