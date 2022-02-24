using MoveAndTagMediaFilesWpfApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoveAndTagMediaFilesWpfApp.Infrastructure
{
	public class FilePersistor : IPersistData
	{
		public async Task<T> GetObjectAsync<T>(string filePath, T defaultValue)
		{
			if (!File.Exists(filePath))
			{
				return defaultValue;
			}

			var jsonString = await File.ReadAllTextAsync(filePath);
			var settings = JsonSerializer.Deserialize<T>(jsonString) ?? defaultValue;
			return settings;
		}

		public async Task SaveObjectAsync<T>(string filePath, T value)
		{
			var directory = Directory.GetParent(filePath);
			if (!Directory.Exists(directory.FullName))
			{
				Directory.CreateDirectory(directory.FullName);
			}

			string jsonString = JsonSerializer.Serialize(value);
			await File.WriteAllTextAsync(filePath, jsonString);
		}
	}
}
