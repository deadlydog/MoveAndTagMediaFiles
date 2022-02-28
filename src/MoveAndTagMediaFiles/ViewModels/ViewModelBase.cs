using MoveAndTagMediaFiles.Infrastructure.MVVM;
using MoveAndTagMediaFiles.Services;

namespace MoveAndTagMediaFiles.ViewModels;

public class ViewModelBase : PropertyChangedNotifier
{
	protected IDialogService DialogService { get; init; }

	public ViewModelBase(ICommonServices commonServices)
	{
		DialogService = commonServices.DialogService;
	}
}
