namespace MoveAndTagMediaFilesWpfApp;

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
		var searchOptions = ApplicationSettings.Instance.ToSearchOptions();


	}
}
