using System.Windows.Input;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM;

// Automatic refresh is achieved by subscribing to CommandManager.RequerySuggested.
// This can be very expensive because the UI may constantly call CanExecute(), so use it sparingly.
// More info: https://medium.com/@rochar/avoid-relay-commands-and-prevent-cpu-usage-peaks-in-wpf-c-ffe672de8559

public class RelayCommandRefreshedAutomatically<T> : ICommand
{
	private readonly Func<T, bool> _canExecute;
	private readonly Action<T> _execute;

	public RelayCommandRefreshedAutomatically(Action<T> execute, Func<T, bool> canExecute)
	{
		_execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} action provided to {nameof(RelayCommandRefreshedAutomatically)} is null.");
		_canExecute = canExecute;
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

public class RelayCommandRefreshedAutomatically : ICommand
{
	private readonly Func<object, bool> _canExecute;
	private readonly Action<object> _execute;

	public RelayCommandRefreshedAutomatically(Action<object> execute, Func<object, bool> canExecute = null)
	{
		_execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} action provided to {nameof(RelayCommandRefreshedAutomatically)} is null.");
		_canExecute = canExecute;
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
