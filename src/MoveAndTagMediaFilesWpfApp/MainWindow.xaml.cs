using MoveAndTagMediaFiles;
using System.Windows.Controls;

namespace MoveAndTagMediaFilesWpfApp;

public partial class MainWindow : Window
{
	public MainWindowViewModel ViewModel { get; } = new MainWindowViewModel();

	public MainWindow()
	{
		InitializeComponent();
		this.DataContext = this;

		LoadViewModelSettings();
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

	private void txtSourceDirectoryPassword_PasswordChanged(object sender, RoutedEventArgs e)
	{
		ViewModel.SourceDirectoryCredentialsPassword = ((PasswordBox)sender).SecurePassword;
	}

	private void txtDestinationDirectoryPassword_PasswordChanged(object sender, RoutedEventArgs e)
	{
		ViewModel.DestinationDirectoryCredentialsPassword = ((PasswordBox)sender).SecurePassword;
	}

	private async void btnFindFiles_Click(object sender, RoutedEventArgs e)
	{
		btnFindFiles.IsEnabled = false;
		txtStatus.Text = "Searching for files...";

		// Get all settings from the UI before doing any potentially long-running operations where the user might change the UI values.
		var fileSearchSettings = GetFileSearchSettingsFromUi();
		var previewSettings = GetPreviewSettingsFromUi();
		
		var filePaths = Enumerable.Empty<string>();
		try
		{
			filePaths = await Task.Run(() => FileRetriever.GetFilePaths(fileSearchSettings));
		}
		catch (InvalidCredentialsToAccessPathException ex)
		{
			MessageBox.Show($"Appropriate credentials are required to access the Source Directory or one of its subdirectories. Please provide a username and password with permissions to access the path '{ex.PathThatCouldNotBeAccessed}'.", "Provide appropriate credentials");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "An error occurred retrieving file paths");
		}

		if (filePaths.Any())
		{
			var previewWindow = new PreviewWindow(filePaths.ToList(), previewSettings);
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
			SourceDirectory = ViewModel.SourceDirectory.Trim(),
			FileSearchPattern = ViewModel.FileSearchPattern.Trim(),
			SearchSubdirectories = ViewModel.SearchSubdirectories,
			UseCredentials = ViewModel.UseSourceDirectoryCredentials,
			CredentialsUsername = ViewModel.SourceDirectoryCredentialsUsername.Trim(),
			CredentialsPassword = ViewModel.SourceDirectoryCredentialsPassword,
		};
		return searchSettings;
	}

	private PreviewSettings GetPreviewSettingsFromUi()
	{
		var previewSettings = new PreviewSettings()
		{
			SourceDirectory = ViewModel.SourceDirectory.Trim(),
			DestinationDirectory = ViewModel.DestinationDirectory.Trim(),
			PreserveDirectoryStructure = ViewModel.PreserveDirectoryStructure,
		};
		return previewSettings;
	}

	private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
	{
		SaveViewModelSettings();
	}

	private void SaveViewModelSettings()
	{
		var settings = ApplicationSettings.Default;
		settings.FileSearchSettings_SourceDirectory = ViewModel.SourceDirectory;
		settings.FileSearchSettings_DestinationDirectory = ViewModel.DestinationDirectory;
		settings.FileSearchSettings_FileSearchPattern = ViewModel.FileSearchPattern;
		settings.FileSearchSettings_SearchSubdirectories = ViewModel.SearchSubdirectories;
		settings.FileSearchSettings_PreserveDirectoryStructure = ViewModel.PreserveDirectoryStructure;
		settings.UseSourceDirectoryCredentials = ViewModel.UseSourceDirectoryCredentials;
		settings.SourceDirectoryCredentialsUsername = ViewModel.SourceDirectoryCredentialsUsername;
		settings.UseDestinationDirectoryCredentials = ViewModel.UseDestinationDirectoryCredentials;
		settings.DestinationDirectoryCredentialsUsername = ViewModel.DestinationDirectoryCredentialsUsername;
	}

	private void LoadViewModelSettings()
	{
		var settings = ApplicationSettings.Default;
		ViewModel.SourceDirectory = settings.FileSearchSettings_SourceDirectory;
		ViewModel.DestinationDirectory = settings.FileSearchSettings_DestinationDirectory;
		ViewModel.FileSearchPattern = settings.FileSearchSettings_FileSearchPattern;
		ViewModel.SearchSubdirectories = settings.FileSearchSettings_SearchSubdirectories;
		ViewModel.PreserveDirectoryStructure = settings.FileSearchSettings_PreserveDirectoryStructure;
		ViewModel.UseSourceDirectoryCredentials = settings.UseSourceDirectoryCredentials;
		ViewModel.SourceDirectoryCredentialsUsername = settings.SourceDirectoryCredentialsUsername;
		ViewModel.UseDestinationDirectoryCredentials = settings.UseDestinationDirectoryCredentials;
		ViewModel.DestinationDirectoryCredentialsUsername = settings.DestinationDirectoryCredentialsUsername;
	}
}
