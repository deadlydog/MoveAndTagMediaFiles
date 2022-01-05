namespace MoveAndTagMediaFiles;

public class FileRetriever
{
	public static IEnumerable<string> GetFilePaths(FileSearchSettings searchOptions)
	{
		var directorySearchOptions = new EnumerationOptions()
		{
			RecurseSubdirectories = searchOptions.SearchSubdirectories
		};

		// Leaving the File Search Pattern blank won't find any files; it has to use a wildcard to match against everything.
		var fileSearchPattern = searchOptions.FileSearchPattern;
		if (string.IsNullOrWhiteSpace(fileSearchPattern))
		{
			fileSearchPattern = "*";
		}

		var files = Directory.EnumerateFiles(searchOptions.SourceDirectory, fileSearchPattern, directorySearchOptions);
		return files;
	}
}
