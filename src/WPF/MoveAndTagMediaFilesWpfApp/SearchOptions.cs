using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFilesWpfApp
{
	public record SearchOptions
	{
		public string SourceDirectory { get; init; } = string.Empty;
		public string DestinationDirectory { get; init; } = string.Empty;
		public List<string> IncludedFilePatterns { get; init; } = new List<string>();
		public bool PreserveDirectoryStructure { get; init; } = true;

		public static SearchOptions FromApplicationSettings(ApplicationSettings appSettings)
		{
			var searchOptions = new SearchOptions()
			{
				SourceDirectory = appSettings.SourceDirectory,
				DestinationDirectory = appSettings.DestinationDirectory,
				IncludedFilePatterns = appSettings.IncludedFilePatterns.Split(",").ToList(),
				PreserveDirectoryStructure = appSettings.PreserveDirectoryStructure,
			};
			return searchOptions;
		}
	}
}
