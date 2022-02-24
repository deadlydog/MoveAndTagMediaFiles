using MoveAndTagMediaFiles.Services;

namespace MoveAndTagMediaFilesWpfApp.Services;

public class CommonServices : ICommonServices
{
	public IDialogService DialogService { get; }

	public CommonServices(IDialogService dialogService)
	{
		DialogService = dialogService;
	}
}
