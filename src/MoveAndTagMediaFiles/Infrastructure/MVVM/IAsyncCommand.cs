using System.ComponentModel;
using System.Windows.Input;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM;

public interface IAsyncCommand<T> : ICommand, INotifyPropertyChanged
{
	Task ExecuteAsync(T parameter);
	bool CanExecute(T parameter);
	bool IsExecuting { get; }
}

public interface IAsyncCommand : ICommand, INotifyPropertyChanged
{
	Task ExecuteAsync();
	bool CanExecute();
	bool IsExecuting { get; }
}
