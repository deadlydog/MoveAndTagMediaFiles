using System.Windows.Data;

namespace MoveAndTagMediaFilesWpfApp;

// Binding extension that allows XAML to easily access application settings.
public class ApplicationSettingsBindingExtension : Binding
{
	public ApplicationSettingsBindingExtension()
	{
		Initialize();
	}

	public ApplicationSettingsBindingExtension(string path) : base(path)
	{
		Initialize();
	}

	private void Initialize()
	{
		this.Source = ApplicationSettings.Current;
		this.Mode = BindingMode.TwoWay;
	}
}
