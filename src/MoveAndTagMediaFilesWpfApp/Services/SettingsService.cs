using MoveAndTagMediaFiles.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFilesWpfApp.Services
{
	public sealed class SettingsService : ISettingsService
	{
		private readonly IDictionary<string, object> _properties = Xamarin.Forms.Application.Current.Properties;

		public T GetValue<T>(string key)
		{
			if (_properties.TryGetValue(key, out var value))
			{
				return (T)value;
			}

			return default;
		}

		public void SetValue<T>(string key, T value)
		{
			if (!_properties.ContainsKey(key))
			{
				_properties.Add(key, value);
			}
			else
			{
				_properties[key] = value;
			}
		}
	}
}
