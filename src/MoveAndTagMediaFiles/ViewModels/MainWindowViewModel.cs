using MoveAndTagMediaFiles.Infrastructure.MVVM;
using MoveAndTagMediaFiles.Services;
using MoveAndTagMediaFiles.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace MoveAndTagMediaFiles;

public class MainWindowViewModel : ViewModelBase
{
	public string WindowTitle { get => "Move and Tag Media Files v" + System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3); }

	[Required]
	public string SourceDirectory
	{
		get => _sourceDirectory;
		set => SetProperty(ref _sourceDirectory, value);
	}
	private string _sourceDirectory = string.Empty;

	[Required]
	public string DestinationDirectory
	{
		get => _destinationDirectory;
		set => SetProperty(ref _destinationDirectory, value);
	}
	private string _destinationDirectory = string.Empty;

	public string FileSearchPattern
	{
		get => _fileSearchPattern;
		set => SetProperty(ref _fileSearchPattern, value);
	}
	private string _fileSearchPattern = string.Empty;

	public bool SearchSubdirectories
	{
		get => _searchSubdirectories;
		set => SetProperty(ref _searchSubdirectories, value);
	}
	private bool _searchSubdirectories = false;

	public bool PreserveDirectoryStructure
	{
		get => _preserveDirectoryStructure;
		set => SetProperty(ref _preserveDirectoryStructure, value);
	}
	private bool _preserveDirectoryStructure = false;

	public bool UseSourceDirectoryCredentials
	{
		get => _useSourceDirectoryCredentials;
		set => SetProperty(ref _useSourceDirectoryCredentials, value);
	}
	private bool _useSourceDirectoryCredentials = false;

	public string SourceDirectoryCredentialsUsername
	{
		get => _sourceDirectoryCredentialsUsername;
		set => SetProperty(ref _sourceDirectoryCredentialsUsername, value);
	}
	private string _sourceDirectoryCredentialsUsername = string.Empty;

	public SecureString SourceDirectoryCredentialsPassword
	{
		get => _sourceDirectoryCredentialsPassword;
		set => SetProperty(ref _sourceDirectoryCredentialsPassword, value);
	}
	private SecureString _sourceDirectoryCredentialsPassword = new SecureString();

	public bool UseDestinationDirectoryCredentials
	{
		get => _useDestinationDirectoryCredentials;
		set => SetProperty(ref _useDestinationDirectoryCredentials, value);
	}
	private bool _useDestinationDirectoryCredentials = false;

	public string DestinationDirectoryCredentialsUsername
	{
		get => _destinationDirectoryCredentialsUsername;
		set => SetProperty(ref _destinationDirectoryCredentialsUsername, value);
	}
	private string _destinationDirectoryCredentialsUsername = string.Empty;

	public SecureString DestinationDirectoryCredentialsPassword
	{
		get => _destinationDirectoryCredentialsPassword;
		set => SetProperty(ref _destinationDirectoryCredentialsPassword, value);
	}
	private SecureString _destinationDirectoryCredentialsPassword = new SecureString();

	public string Status
	{
		get => _status;
		set => SetProperty(ref _status, value);
	}
	private string _status = string.Empty;

	public MainWindowViewModel(ICommonServices commonServices) : base(commonServices)
	{
		
	}

	public IAsyncCommand GetFilesAndLaunchPreviewWindowCommand => new RelayCommandRefreshedAutomaticallyAsync(GetFilePathsAndLaunchPreviewWindowAsync);
	public bool GetFilesAndLaunchPreviewWindowCommandIsRunning
	{
		get => _getFilesAndLaunchPreviewWindowCommandIsRunning;
		set => SetProperty(ref _getFilesAndLaunchPreviewWindowCommandIsRunning, value);
	}
	private bool _getFilesAndLaunchPreviewWindowCommandIsRunning = false;

	public async Task GetFilePathsAndLaunchPreviewWindowAsync()
	{
		try
		{
			GetFilesAndLaunchPreviewWindowCommandIsRunning = true;
			Status = "Searching for files...";
			await RunGetFilePathsAndLaunchPreviewWindowAsync();
		}
		finally
		{
			GetFilesAndLaunchPreviewWindowCommandIsRunning = false;
			Status = string.Empty;
		}
	}

	private async Task RunGetFilePathsAndLaunchPreviewWindowAsync()
	{ 
		var fileSearchSettings = ConstructFileSearchSettings();

		var filePaths = Enumerable.Empty<string>();
		try
		{
			filePaths = await Task.Run(() => FileRetriever.GetFilePaths(fileSearchSettings));
		}
		catch (InvalidCredentialsToAccessPathException ex)
		{
			var message = $"Appropriate credentials are required to access the Source Directory or one of its subdirectories. Please provide a username and password with permissions to access the path '{ex.PathThatCouldNotBeAccessed}'.";
			var title = "Provide appropriate credentials";
			DialogService.ShowWarningMessage(title, message);
		}
		catch (Exception ex)
		{
			var message = $"An error occurred retrieving file paths from '{fileSearchSettings.SourceDirectory}'.";
			var title = "Error occurred retrieving files";
			DialogService.ShowErrorMessage(title, message, ex);
		}

		if (!filePaths.Any())
		{
			var message = "No files were found that matched the search criteria.";
			var title = "No files found";
			DialogService.ShowInformationMessage(title, message);
			return;
		}

		var previewSettings = ConstructPreviewSettings();
		var previewWindowArgs = new PreviewWindowViewModelArguments
		{
			MediaFilePaths = filePaths.ToList(),
			PreviewSettings = previewSettings
		};

		await Task.Run(() => Thread.Sleep(10000));
	}

	private FileSearchSettings ConstructFileSearchSettings()
	{
		var searchSettings = new FileSearchSettings()
		{
			SourceDirectory = SourceDirectory.Trim(),
			FileSearchPattern = FileSearchPattern.Trim(),
			SearchSubdirectories = SearchSubdirectories,
			UseCredentials = UseSourceDirectoryCredentials,
			CredentialsUsername = SourceDirectoryCredentialsUsername.Trim(),
			CredentialsPassword = SourceDirectoryCredentialsPassword,
		};
		return searchSettings;
	}

	private PreviewSettings ConstructPreviewSettings()
	{
		var previewSettings = new PreviewSettings
		{
			SourceDirectory = SourceDirectory.Trim(),
			DestinationDirectory = DestinationDirectory.Trim(),
			PreserveDirectoryStructure = PreserveDirectoryStructure
		};
		return previewSettings;
	}
}
