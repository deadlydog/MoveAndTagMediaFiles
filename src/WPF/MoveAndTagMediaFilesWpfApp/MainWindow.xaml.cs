namespace MoveAndTagMediaFilesWpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void btnBrowseForSourceDirectory_Click(object sender, RoutedEventArgs e)
	{

	}

	private void btnBrowseForDestinationDirectory_Click(object sender, RoutedEventArgs e)
	{

	}

	private void btnFindFiles_Click(object sender, RoutedEventArgs e)
	{
		var searchOptions = SearchOptions.FromApplicationSettings(ApplicationSettings.Instance);


	}
}
