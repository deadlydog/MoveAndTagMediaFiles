namespace MoveAndTagMediaFilesWpfApp;

public record SearchOptions
{
	public string SourceDirectory { get; init; } = string.Empty;
	public string DestinationDirectory { get; init; } = string.Empty;
	public List<string> IncludedFilePatterns { get; init; } = new List<string>();
	public bool PreserveDirectoryStructure { get; init; } = true;
}
