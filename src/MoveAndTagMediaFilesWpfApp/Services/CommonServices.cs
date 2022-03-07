using MoveAndTagMediaFiles.Services;

namespace MoveAndTagMediaFilesWpfApp.Services;

public class CommonServices : ICommonServices
{
	public IDialogService DialogService { get; }

	public IViewModelChangerService ViewModelChanger { get; }

	public CommonServices(IDialogService dialogService, IViewModelChangerService viewModelChangerService)
	{
		DialogService = dialogService;
		ViewModelChanger = viewModelChangerService;
	}
}
