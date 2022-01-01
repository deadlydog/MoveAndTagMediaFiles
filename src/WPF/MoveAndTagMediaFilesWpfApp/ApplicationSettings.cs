namespace MoveAndTagMediaFilesWpfApp;

public class ApplicationSettings
{
	public string SourceDirectory { get; set; } = string.Empty;
	public string DestinationDirectory { get; set; } = string.Empty;
	public bool PreserveDirectoryStructure { get; set; } = true;
	public List<string> IncludedFilePatterns { get; set; } = new List<string>();

	public static ApplicationSettings Instance { get; } = new ApplicationSettings();
}
