using System.Windows.Input;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM
{
	public class AlwaysCanExecuteCommand<T> : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private readonly Action<T> _execute;

		public AlwaysCanExecuteCommand(Action<T> execute)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} action provided to {nameof(AlwaysCanExecuteCommand)} is null.");
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_execute((T)parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public class AlwaysCanExecuteCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private readonly Action _execute;

		public AlwaysCanExecuteCommand(Action execute)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} action provided to {nameof(AlwaysCanExecuteCommand)} is null.");
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_execute();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
