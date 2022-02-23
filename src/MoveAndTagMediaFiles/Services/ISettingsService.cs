using System.Diagnostics.Contracts;

namespace MoveAndTagMediaFiles.Services;

public interface ISettingsService
{
	void SetValue<T>(string key, T value);

	[Pure]
	T? GetValue<T>(string key);
}
