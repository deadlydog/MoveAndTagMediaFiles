using MoveAndTagMediaFiles.ViewModels;

namespace MoveAndTagMediaFiles.Services;

public interface IViewModelChangerService
{
	void ShowViewModel<T>(Type viewModelType, T viewModelArgs);
	void CloseViewModel();
}
