namespace MoveAndTagMediaFiles.Services;

public interface ICommonServices
{
	IDialogService DialogService { get; }
	IViewModelChangerService ViewModelChanger { get; }
}
