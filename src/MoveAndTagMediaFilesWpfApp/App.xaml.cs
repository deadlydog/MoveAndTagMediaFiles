using Microsoft.Extensions.DependencyInjection;
using MoveAndTagMediaFilesWpfApp.Services;
using System.Diagnostics;
using System.IO;

namespace MoveAndTagMediaFilesWpfApp;

public partial class App : Application
{
	public new static App Current => (App)Application.Current;
	public IServiceProvider Services { get; }

	public App() : base()
	{
		SetupUnhandledExceptionHandling();
		Services = CompositionRoot.ConfigureServices();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		await LoadAppSettings();
		var mainWindow = Services.GetService<MainWindow>();
		mainWindow.Show();
	}

	private async void Application_Exit(object sender, ExitEventArgs e)
	{
		await SaveAppSettings();
	}

	public string ApplicationSettingsDirectoryPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name ?? "MoveAndTagMediaFiles");
	public string ApplicationSettingsFilePath => Path.Combine(ApplicationSettingsDirectoryPath, "ApplicationSettings.json");

	private async Task SaveAppSettings()
	{
		var settingsPersistor = App.Current.Services.GetService<IPersistData>();
		await settingsPersistor.SaveObjectAsync(ApplicationSettingsFilePath, ApplicationSettings.Current);
	}

	private async Task LoadAppSettings()
	{
		if (!Directory.Exists(ApplicationSettingsDirectoryPath))
		{
			Directory.CreateDirectory(ApplicationSettingsDirectoryPath);
		}

		var settingsPersistor = App.Current.Services.GetService<IPersistData>();
		ApplicationSettings.Current = await settingsPersistor.GetObjectAsync<ApplicationSettings>(ApplicationSettingsFilePath, new ApplicationSettings());
	}

	private void SetupUnhandledExceptionHandling()
	{
		// Catch exceptions from all threads in the AppDomain.
		AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
			ShowUnhandledException(args.ExceptionObject as Exception ?? new Exception("Unknown exception"), "AppDomain.CurrentDomain.UnhandledException", false);

		// Catch exceptions from each AppDomain that uses a task scheduler for async operations.
		TaskScheduler.UnobservedTaskException += (sender, args) =>
			ShowUnhandledException(args.Exception, "TaskScheduler.UnobservedTaskException", false);

		// Catch exceptions from a single specific UI dispatcher thread.
		Dispatcher.UnhandledException += (sender, args) =>
		{
			// If we are debugging, let Visual Studio handle the exception and take us to the code that threw it.
			if (!Debugger.IsAttached)
			{
				args.Handled = true;
				ShowUnhandledException(args.Exception, "Dispatcher.UnhandledException", true);
			}
		};

		// Catch exceptions from the main UI dispatcher thread.
		// Typically we only need to catch this OR the Dispatcher.UnhandledException.
		// Handling both can result in the exception getting handled twice.
		//Application.Current.DispatcherUnhandledException += (sender, args) =>
		//{
		//	// If we are debugging, let Visual Studio handle the exception and take us to the code that threw it.
		//	if (!Debugger.IsAttached)
		//	{
		//		args.Handled = true;
		//		ShowUnhandledException(args.Exception, "Application.Current.DispatcherUnhandledException", true);
		//	}
		//};
	}

	private void ShowUnhandledException(Exception e, string unhandledExceptionType, bool promptUserForShutdown)
	{
		var messageBoxTitle = $"Unexpected Error Occurred: {unhandledExceptionType}";
		var messageBoxMessage = $"The following exception occurred:\n\n{e}";
		var messageBoxButtons = MessageBoxButton.OK;

		if (promptUserForShutdown)
		{
			messageBoxMessage += "\n\nNormally the app would die now. Should we let it die?";
			messageBoxButtons = MessageBoxButton.YesNo;
		}

		// Let the user decide if the app should die or not (if applicable).
		if (MessageBox.Show(messageBoxMessage, messageBoxTitle, messageBoxButtons) == MessageBoxResult.Yes)
		{
			Application.Current.Shutdown();
		}
	}
}
