using MoveAndTagMediaFiles.Infrastructure.MVVM;
using MoveAndTagMediaFiles.Services;

namespace MoveAndTagMediaFiles.ViewModels;

public class ViewModelBase : PropertyChangedNotifier
{
	protected IDialogService DialogService { get; init; }

	protected IViewModelChangerService ViewModelChanger { get; init; }

	public ViewModelBase(ICommonServices commonServices)
	{
		DialogService = commonServices.DialogService;
		ViewModelChanger = commonServices.ViewModelChanger;
	}
}
