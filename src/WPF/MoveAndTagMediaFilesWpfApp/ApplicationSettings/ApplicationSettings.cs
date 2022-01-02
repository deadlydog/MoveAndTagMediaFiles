using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoveAndTagMediaFilesWpfApp;

public class ApplicationSettings : INotifyPropertyChanged
{
	#region Notify Property Changed
	public event PropertyChangedEventHandler PropertyChanged;

	public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	#endregion

	public string SourceDirectory { get; set; } = string.Empty;
	public string DestinationDirectory { get; set; } = string.Empty;
	public string FileSearchPattern { get; set; } = string.Empty;
	public bool SearchSubdirectories { get; set; } = true;
	public bool PreserveDirectoryStructure { get; set; } = true;

	public static ApplicationSettings Instance { get; set; } = new ApplicationSettings();
}
