using MoveAndTagMediaFiles.Services;
namespace MoveAndTagMediaFilesWpfApp.Services;

public class DialogService : IDialogService
{
	public void ShowInformationMessage(string title, string message)
	{
		MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
	}

	public void ShowWarningMessage(string title, string message)
	{
		MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
	}

	public void ShowErrorMessage(string title, string message, Exception exception)
	{
		var msg = message + Environment.NewLine + Environment.NewLine + "Exception:" + Environment.NewLine + exception.ToString();
		MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
	}
}
