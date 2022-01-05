namespace MoveAndTagMediaFiles;

public record PreviewSettings
{
	public string SourceDirectory { get; init; } = string.Empty;
	public string DestinationDirectory { get; init; } = string.Empty;
	public bool PreserveDirectoryStructure { get; init; } = true;
}
