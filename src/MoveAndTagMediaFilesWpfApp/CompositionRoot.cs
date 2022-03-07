using Microsoft.Extensions.DependencyInjection;
using MoveAndTagMediaFiles;
using MoveAndTagMediaFiles.Infrastructure.Services;
using MoveAndTagMediaFiles.Services;
using MoveAndTagMediaFiles.ViewModels;
using MoveAndTagMediaFilesWpfApp.Infrastructure;
using MoveAndTagMediaFilesWpfApp.Services;

namespace MoveAndTagMediaFilesWpfApp;

public static class CompositionRoot
{
	public static IServiceProvider ConfigureServices()
	{
		var services = new ServiceCollection();

		services.AddSingleton<IPersistData, FilePersistor>();

		services.AddSingleton<IDialogService, DialogService>();
		services.AddSingleton<IViewModelChangerService, ViewModelChangerService>();
		services.AddTransient<ICommonServices, CommonServices>();

		services.AddTransient<MainWindowViewModel>();
		services.AddTransient<MainWindow>();

		services.AddTransient<PreviewWindowViewModel>();
		services.AddTransient<PreviewWindow>();

		return new ThrowIfNullServiceProvider(services.BuildServiceProvider());
	}
}

public class ThrowIfNullServiceProvider : IServiceProvider
{
	private ServiceProvider _serviceProvider;
	public ThrowIfNullServiceProvider(ServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public object? GetService(Type serviceType) => _serviceProvider.GetService(serviceType) ?? throw new InvalidOperationException($"IoC container could not resolve type '{serviceType.FullName}'.");
}
