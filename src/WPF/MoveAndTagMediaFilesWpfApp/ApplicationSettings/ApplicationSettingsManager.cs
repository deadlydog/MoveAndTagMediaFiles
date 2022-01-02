using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoveAndTagMediaFilesWpfApp;

public class ApplicationSettingsManager
{
	public static async Task SaveSettings(ApplicationSettings appSettings)
	{
		var directoryPath = Path.GetDirectoryName(SettingsFilePath) ?? string.Empty;
		if (!Directory.Exists(directoryPath))
		{
			Directory.CreateDirectory(directoryPath);
		}

		string json = JsonSerializer.Serialize(appSettings);
		await File.WriteAllTextAsync(SettingsFilePath, json);
	}

	public static async Task<ApplicationSettings> LoadSettings()
	{
		if (File.Exists(SettingsFilePath))
		{
			var json = await File.ReadAllTextAsync(SettingsFilePath);
			var appSettings = JsonSerializer.Deserialize<ApplicationSettings>(json) ?? new ApplicationSettings();
			return appSettings;
		}

		return new ApplicationSettings();
	}

	public static readonly string SettingsFilePath = Path.Combine(AppContext.BaseDirectory, "settings.json");
}
