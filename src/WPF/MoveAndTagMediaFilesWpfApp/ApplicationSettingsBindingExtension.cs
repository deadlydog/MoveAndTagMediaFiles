using System.Windows.Data;

namespace MoveAndTagMediaFilesWpfApp;

// Binding extension that allows XAML to easily access application settings stored in a file.
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
		this.Source = ApplicationSettings.Instance;
		this.Mode = BindingMode.TwoWay;
	}
}
