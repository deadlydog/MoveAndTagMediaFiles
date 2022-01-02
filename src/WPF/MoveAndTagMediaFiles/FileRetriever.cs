using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles;

public class FileRetriever
{
	public static IEnumerable<string> GetFilePaths(FileSearchSettings searchOptions)
	{
		var directorySearchOptions = new EnumerationOptions()
		{
			RecurseSubdirectories = searchOptions.SearchSubdirectories
		};

		var files = Directory.EnumerateFiles(searchOptions.SourceDirectory, searchOptions.FileSearchPattern, directorySearchOptions);
		return files;
	}
}
