using System.Windows.Input;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM;

// Based on: https://johnthiriet.com/removing-async-void/

public class RelayCommandAsync<T> : PropertyChangedNotifier, IAsyncCommand<T>
{
	public event EventHandler? CanExecuteChanged;

	private readonly Func<T, Task> _execute;
	private readonly Func<T, bool> _canExecute;
	private readonly IErrorHandler _errorHandler;

	public bool IsExecuting
	{
		get => _isExecuting;
		set
		{
			SetProperty(ref _isExecuting, value);
			RaiseCanExecuteChanged();
		}
	}
	private bool _isExecuting;

	public RelayCommandAsync(Func<T, Task> execute, Func<T, bool> canExecute = null, IErrorHandler errorHandler = null)
	{
		_execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} action provided to {nameof(RelayCommandAsync)} is null.");
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

	public void RaiseCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
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
}

public class RelayCommandAsync : PropertyChangedNotifier, IAsyncCommand
{
	public event EventHandler? CanExecuteChanged;

	private readonly Func<Task> _execute;
	private readonly Func<bool> _canExecute;
	private readonly IErrorHandler _errorHandler;

	public bool IsExecuting
	{
		get => _isExecuting;
		set
		{
			SetProperty(ref _isExecuting, value);
			RaiseCanExecuteChanged();
		}
	}
	private bool _isExecuting;

	public RelayCommandAsync(Func<Task> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
	{
		_execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} action provided to {nameof(RelayCommandAsync)} is null.");
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

	public void RaiseCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
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
}
