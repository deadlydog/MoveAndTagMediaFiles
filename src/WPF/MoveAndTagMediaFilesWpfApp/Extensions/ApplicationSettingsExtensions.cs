using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFilesWpfApp
{
	public static class ApplicationSettingsExtensions
	{
		public static SearchOptions ToSearchOptions(this ApplicationSettings appSettings)
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
