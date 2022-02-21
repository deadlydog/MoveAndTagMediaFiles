using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoveAndTagMediaFiles;

public class MainWindowViewModel : INotifyPropertyChanged
{
	#region Notify Property Changed
	public event PropertyChangedEventHandler? PropertyChanged;
	protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	#endregion

	public string WindowTitle { get => "Move and Tag Media Files v" + System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3); }

	public string SourceDirectory
	{
		get => _sourceDirectory;
		set
		{
			_sourceDirectory = value;
			NotifyPropertyChanged();
		}
	}
	private string _sourceDirectory = string.Empty;

	public string DestinationDirectory
	{
		get => _destinationDirectory;
		set
		{
			_destinationDirectory = value;
			NotifyPropertyChanged();
		}
	}
	private string _destinationDirectory = string.Empty;
}
