using System.ComponentModel;
using System.Windows.Input;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM;

// Based on: https://johnthiriet.com/removing-async-void/

public class RelayCommandAsync<T> : PropertyChangedNotifier, IAsyncCommand<T>
{
	private readonly Func<T, Task> _execute;
	private readonly Func<T, bool> _canExecute;
	private readonly IErrorHandler _errorHandler;

	public bool IsExecuting
	{
		get => _isExecuting;
		set => SetProperty(ref _isExecuting, value);
	}
	private bool _isExecuting;

	public RelayCommandAsync(Func<T, Task> execute, Func<T, bool> canExecute = null, IErrorHandler errorHandler = null)
	{
		ArgumentNullException.ThrowIfNull(execute, nameof(execute));
		_execute = execute;
		_canExecute = canExecute;
		_errorHandler = errorHandler;
	}

	public bool CanExecute(T parameter)
	{
		return !IsExecuting && (_canExecute?.Invoke(parameter) ?? true);
	}

	public async Task ExecuteAsync(T parameter)
	{
		if (CanExecute(parameter))
		{
			try
			{
				IsExecuting = true;
				await _execute(parameter);
			}
			finally
			{
				IsExecuting = false;
			}
		}
	}

	bool ICommand.CanExecute(object parameter)
	{
		return CanExecute((T)parameter);
	}

	async void ICommand.Execute(object parameter)
	{
		try
		{
			await ExecuteAsync((T)parameter);
		}
		catch (Exception ex)
		{
			_errorHandler?.HandleError(ex);
		}
	}

	public event EventHandler CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}
}

public class RelayCommandAsync : PropertyChangedNotifier, IAsyncCommand
{
	private readonly Func<Task> _execute;
	private readonly Func<bool> _canExecute;
	private readonly IErrorHandler _errorHandler;

	public bool IsExecuting
	{
		get => _isExecuting;
		set => SetProperty(ref _isExecuting, value);
	}
	private bool _isExecuting;

	public RelayCommandAsync(Func<Task> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
	{
		ArgumentNullException.ThrowIfNull(execute, nameof(execute));
		_execute = execute;
		_canExecute = canExecute;
		_errorHandler = errorHandler;
	}

	public bool CanExecute()
	{
		return !IsExecuting && (_canExecute?.Invoke() ?? true);
	}

	public async Task ExecuteAsync()
	{
		if (CanExecute())
		{
			try
			{
				IsExecuting = true;
				await _execute();
			}
			finally
			{
				IsExecuting = false;
			}
		}
	}

	bool ICommand.CanExecute(object parameter)
	{
		return CanExecute();
	}

	async void ICommand.Execute(object parameter)
	{
		try
		{
			await ExecuteAsync();
		}
		catch (Exception ex)
		{
			_errorHandler?.HandleError(ex);
		}
	}

	public event EventHandler CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}
}
