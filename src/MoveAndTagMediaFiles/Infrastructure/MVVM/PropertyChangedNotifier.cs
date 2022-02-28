using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoveAndTagMediaFiles.Infrastructure.MVVM;

public class PropertyChangedNotifier : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
	protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
	{
		if (Equals(property, value) || string.IsNullOrWhiteSpace(propertyName))
		{
			return false;
		}

		property = value;
		NotifyPropertyChanged(propertyName);
		return true;
	}
}
