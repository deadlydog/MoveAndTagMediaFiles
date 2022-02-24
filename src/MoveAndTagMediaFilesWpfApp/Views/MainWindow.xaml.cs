using MoveAndTagMediaFiles;
using System.Windows.Controls;

namespace MoveAndTagMediaFilesWpfApp;

public partial class MainWindow : Window
{
	public MainWindowViewModel ViewModel { get; init; }

	public MainWindow(MainWindowViewModel viewModel)
	{
		ViewModel = viewModel;

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

	private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
	{
		SaveViewModelSettings();
	}

	private void SaveViewModelSettings()
	{
		var settings = ApplicationSettings.Current;
		settings.MainWindow_SourceDirectory = ViewModel.SourceDirectory;
		settings.MainWindow_DestinationDirectory = ViewModel.DestinationDirectory;
		settings.MainWindow_FileSearchPattern = ViewModel.FileSearchPattern;
		settings.MainWindow_SearchSubdirectories = ViewModel.SearchSubdirectories;
		settings.MainWindow_PreserveDirectoryStructure = ViewModel.PreserveDirectoryStructure;
		settings.MainWindow_UseSourceDirectoryCredentials = ViewModel.UseSourceDirectoryCredentials;
		settings.MainWindow_SourceDirectoryCredentialsUsername = ViewModel.SourceDirectoryCredentialsUsername;
		settings.MainWindow_UseDestinationDirectoryCredentials = ViewModel.UseDestinationDirectoryCredentials;
		settings.MainWindow_DestinationDirectoryCredentialsUsername = ViewModel.DestinationDirectoryCredentialsUsername;
	}

	private void LoadViewModelSettings()
	{
		var settings = ApplicationSettings.Current;
		ViewModel.SourceDirectory = settings.MainWindow_SourceDirectory;
		ViewModel.DestinationDirectory = settings.MainWindow_DestinationDirectory;
		ViewModel.FileSearchPattern = settings.MainWindow_FileSearchPattern;
		ViewModel.SearchSubdirectories = settings.MainWindow_SearchSubdirectories;
		ViewModel.PreserveDirectoryStructure = settings.MainWindow_PreserveDirectoryStructure;
		ViewModel.UseSourceDirectoryCredentials = settings.MainWindow_UseSourceDirectoryCredentials;
		ViewModel.SourceDirectoryCredentialsUsername = settings.MainWindow_SourceDirectoryCredentialsUsername;
		ViewModel.UseDestinationDirectoryCredentials = settings.MainWindow_UseDestinationDirectoryCredentials;
		ViewModel.DestinationDirectoryCredentialsUsername = settings.MainWindow_DestinationDirectoryCredentialsUsername;
	}
}
