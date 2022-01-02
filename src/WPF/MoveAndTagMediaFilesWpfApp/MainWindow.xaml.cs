using MoveAndTagMediaFiles;

namespace MoveAndTagMediaFilesWpfApp;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		this.DataContext = this;

		SetWindowTitle();
	}

	private void SetWindowTitle()
	{
		this.Title += " v" + System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3);
	}

	private void btnBrowseForSourceDirectory_Click(object sender, RoutedEventArgs e)
	{
		var folderDialog = new System.Windows.Forms.FolderBrowserDialog
		{
			Description = "Select the directory that contains the media files to preview...",
			ShowNewFolderButton = false
		};

		// If the user selected a folder, put it in the Root Directory text box.
		if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			txtSourceDirectory.Text = folderDialog.SelectedPath;
	}

	private void btnBrowseForDestinationDirectory_Click(object sender, RoutedEventArgs e)
	{
		var folderDialog = new System.Windows.Forms.FolderBrowserDialog
		{
			Description = "Select the directory to copy or move the media files to...",
			ShowNewFolderButton = false
		};

		// If the user selected a folder, put it in the Root Directory text box.
		if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			txtSourceDirectory.Text = folderDialog.SelectedPath;
	}

	private void btnFindFiles_Click(object sender, RoutedEventArgs e)
	{
		btnFindFiles.IsEnabled = false;
		txtStatus.Text = "Searching for files...";
		try
		{
			var searchOptions = GetSearchOptionsFromUi();
			var filePaths = FileRetriever.GetFilePaths(searchOptions);

		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "An error occurred retrieving file paths");
		}
		btnFindFiles.IsEnabled = true;
		txtStatus.Text = string.Empty;
	}

	private SearchOptions GetSearchOptionsFromUi()
	{
		var searchOptions = new SearchOptions()
		{
			SourceDirectory = txtSourceDirectory.Text,
			DestinationDirectory = txtDestinationDirectory.Text,
			FileSearchPattern = txtFileSearchPattern.Text,
			SearchSubdirectories = chkSearchSubdirectories.IsChecked ?? true,
			PreserveDirectoryStructure = chkPreserveDirectoryStructure.IsChecked ?? true
		};
		return searchOptions;
	}
}
