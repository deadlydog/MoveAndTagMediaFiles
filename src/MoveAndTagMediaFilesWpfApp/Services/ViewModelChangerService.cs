using Microsoft.Extensions.DependencyInjection;
using MoveAndTagMediaFiles.Services;
using MoveAndTagMediaFiles.ViewModels;

namespace MoveAndTagMediaFilesWpfApp.Services;

public class ViewModelChangerService : IViewModelChangerService
{
	public void ShowViewModel<T>(Type viewModelType, T viewModelArgs)
	{
		switch (viewModelType.Name)
		{
			case nameof(PreviewWindowViewModel):
				var viewModel = ActivatorUtilities.CreateInstance<PreviewWindowViewModel>(App.Current.Services, viewModelArgs);
				var view = ActivatorUtilities.CreateInstance<PreviewWindow>(App.Current.Services, viewModel);
				view.Show();
				break;
		}
	}

	public void CloseViewModel()
	{

	}
}
