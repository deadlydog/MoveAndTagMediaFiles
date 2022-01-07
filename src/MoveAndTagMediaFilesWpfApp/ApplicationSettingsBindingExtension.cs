using System.Windows.Data;

namespace MoveAndTagMediaFilesWpfApp;

// Binding extension that allows XAML to easily access application settings stored in the Properties\Settings.settings file.
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
		this.Source = ApplicationSettings.Default;
		this.Mode = BindingMode.TwoWay;
	}
}
