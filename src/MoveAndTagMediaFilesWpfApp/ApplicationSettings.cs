namespace MoveAndTagMediaFilesWpfApp;

public record ApplicationSettings
{
	public static ApplicationSettings Current { get; set; } = new ApplicationSettings();

	public string MainWindow_SourceDirectory { get; set; } = String.Empty;
	public string MainWindow_DestinationDirectory { get; set; } = String.Empty;
	public string MainWindow_FileSearchPattern { get; set; } = String.Empty;
	public bool MainWindow_SearchSubdirectories { get; set; } = true;
	public bool MainWindow_PreserveDirectoryStructure { get; set; } = true;
	public bool MainWindow_UseSourceDirectoryCredentials { get; set; } = false;
	public string MainWindow_SourceDirectoryCredentialsUsername { get; set; } = String.Empty;
	public bool MainWindow_UseDestinationDirectoryCredentials { get; set; } = false;
	public string MainWindow_DestinationDirectoryCredentialsUsername { get; set; } = String.Empty;

	public double MainWindow_Width { get; set; } = 700;
	public double MainWindow_Height { get; set; } = 400;
	public double MainWindow_LeftPositiion { get; set; } = 0;
	public double MainWindow_TopPositiion { get; set; } = 0;
	public string MainWindow_WindowState { get; set; } = "Normal";

	public double PreviewWindow_Width { get; set; } = 800;
	public double PreviewWindow_Height { get; set; } = 600;
	public double PreviewWindow_LeftPositiion { get; set; } = 0;
	public double PreviewWindow_TopPositiion { get; set; } = 0;
	public string PreviewWindow_WindowState { get; set; } = "Normal";
}
