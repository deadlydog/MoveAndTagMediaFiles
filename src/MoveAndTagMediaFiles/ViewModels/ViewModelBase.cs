using Microsoft.Toolkit.Mvvm.ComponentModel;
using MoveAndTagMediaFiles.Services;

namespace MoveAndTagMediaFiles.ViewModels;

public class ViewModelBase : ObservableValidator
{
	protected IDialogService DialogService { get; init; }

	public ViewModelBase(ICommonServices commonServices)
	{
		DialogService = commonServices.DialogService;
	}
}
