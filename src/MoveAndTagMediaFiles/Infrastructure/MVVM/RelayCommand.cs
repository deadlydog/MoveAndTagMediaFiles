using System.Windows.Input;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM;

// Relay Command can be very expensive because the UI may constantly call CanExecute(), so use it sparingly.
// More info: https://medium.com/@rochar/avoid-relay-commands-and-prevent-cpu-usage-peaks-in-wpf-c-ffe672de8559

public class RelayCommand<T> : ICommand
{
	private readonly Func<T, bool> _canExecute;
	private readonly Action<T> _execute;

	public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
	{
		ArgumentNullException.ThrowIfNull(execute, nameof(execute));
		_canExecute = canExecute;
		_execute = execute;
	}

	public bool CanExecute(object parameter)
	{
		return _canExecute((T)parameter);
	}

	public void Execute(object parameter)
	{
		_execute((T)parameter);
	}

	public event EventHandler CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}
}

public class RelayCommand : ICommand
{
	private readonly Func<object, bool> _canExecute;
	private readonly Action<object> _execute;

	public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
	{
		ArgumentNullException.ThrowIfNull(execute, nameof(execute));
		_canExecute = canExecute;
		_execute = execute;
	}

	public bool CanExecute(object parameter)
	{
		return _canExecute == null || _canExecute(parameter);
	}

	public void Execute(object parameter)
	{
		_execute(parameter);
	}

	public event EventHandler CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}
}
