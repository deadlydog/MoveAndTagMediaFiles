namespace MoveAndTagMediaFiles.Infrastructure.Services
{
	public interface IPersistData
	{
		Task<T> GetObjectAsync<T>(string key, T defaultValue);
		Task SaveObjectAsync<T>(string key, T value);
	}
}
