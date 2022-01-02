namespace MoveAndTagMediaFiles;

public class PreviewSettings
{
	public string SourceDirectory { get; init; } = string.Empty;
	public string DestinationDirectory { get; init; } = string.Empty;
	public bool PreserveDirectoryStructure { get; init; } = true;
}
