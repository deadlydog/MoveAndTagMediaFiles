namespace MoveAndTagMediaFiles.Services;

public interface IDialogService
{
	void ShowInformationMessage(string title, string message);
	void ShowWarningMessage(string title, string message);
	void ShowErrorMessage(string title, string message, Exception exception);
}
