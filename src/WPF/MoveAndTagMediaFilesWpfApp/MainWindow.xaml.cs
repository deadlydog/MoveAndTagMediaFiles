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

		// If the user selected a folder, put it in the Source Directory text box.
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

		// If the user selected a folder, put it in the Destination Directory text box.
		if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			txtDestinationDirectory.Text = folderDialog.SelectedPath;
	}

	private void btnFindFiles_Click(object sender, RoutedEventArgs e)
	{
		btnFindFiles.IsEnabled = false;
		txtStatus.Text = "Searching for files...";

		// Get all settings from the UI before doing any potentially long-running operations where the user might change the UI values.
		var fileSearchSettings = GetFileSearchSettingsFromUi();
		var previewSettings = GetPreviewSettingsFromUi();

		var filePaths = Enumerable.Empty<string>();
		try
		{
			filePaths = FileRetriever.GetFilePaths(fileSearchSettings);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "An error occurred retrieving file paths");
		}

		if (filePaths.Any())
		{
			var previewWindow = new MediaPreviewWindow(filePaths.ToList(), previewSettings);
			previewWindow.Show();
		}
		else
		{
			MessageBox.Show("No files were found that matched the search criteria.", "No files found");
		}

		btnFindFiles.IsEnabled = true;
		txtStatus.Text = string.Empty;
	}

	private FileSearchSettings GetFileSearchSettingsFromUi()
	{
		var searchSettings = new FileSearchSettings()
		{
			SourceDirectory = txtSourceDirectory.Text,
			FileSearchPattern = txtFileSearchPattern.Text,
			SearchSubdirectories = chkSearchSubdirectories.IsChecked ?? true
		};
		return searchSettings;
	}

	private PreviewSettings GetPreviewSettingsFromUi()
	{
		var previewSettings = new PreviewSettings()
		{
			SourceDirectory = txtSourceDirectory.Text,
			DestinationDirectory = txtDestinationDirectory.Text,
			PreserveDirectoryStructure = chkPreserveDirectoryStructure.IsChecked ?? true
		};
		return previewSettings;
	}
}
