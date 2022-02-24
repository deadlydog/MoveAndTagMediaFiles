using Microsoft.Extensions.DependencyInjection;
using MoveAndTagMediaFiles;
using MoveAndTagMediaFilesWpfApp.Infrastructure;
using MoveAndTagMediaFilesWpfApp.Services;

namespace MoveAndTagMediaFilesWpfApp;

public static class CompositionRoot
{
	public static IServiceProvider ConfigureServices()
	{
		var services = new ServiceCollection();

		services.AddSingleton<IPersistData, FilePersistor>();

		services.AddTransient<MainWindowViewModel>();
		services.AddTransient<MainWindow>();

		return new NoNullsServiceProvider(services.BuildServiceProvider());
	}
}

public class NoNullsServiceProvider : IServiceProvider
{
	private ServiceProvider _serviceProvider;
	public NoNullsServiceProvider(ServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public object? GetService(Type serviceType) => _serviceProvider.GetService(serviceType) ?? throw new NotImplementedException($"IoC container could not resolve type '{serviceType.FullName}'.");
}
