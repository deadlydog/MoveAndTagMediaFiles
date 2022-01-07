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

		var files = Enumerable.Empty<string>();
		if (searchOptions.UseCredentials)
		{
			var password = new System.Net.NetworkCredential("", searchOptions.CredentialsPassword).Password;
			PInvokeWindowsNetworking.connectToRemote(searchOptions.SourceDirectory, searchOptions.CredentialsUsername, password);
			files = EnumerateFilePaths(searchOptions.SourceDirectory, fileSearchPattern, directorySearchOptions);
			PInvokeWindowsNetworking.disconnectRemote(searchOptions.SourceDirectory);
		}
		else
		{
			files = EnumerateFilePaths(searchOptions.SourceDirectory, fileSearchPattern, directorySearchOptions);
		}
		return files;
	}

	private static IEnumerable<string> EnumerateFilePaths(string sourceDirectory, string fileSearchPattern, EnumerationOptions directorySearchOptions)
	{
		try
		{
			return Directory.EnumerateFiles(sourceDirectory, fileSearchPattern, directorySearchOptions);
		}
		catch (System.IO.IOException ex)
			when (ex.Message.Contains("The user name or password is incorrect", StringComparison.OrdinalIgnoreCase))
		{
			var pathThatCouldNotBeAccessed = ex.Message.Split(":").Last();
			var formattedPathTheCouldNotBeAccessed = pathThatCouldNotBeAccessed.Trim().Trim('\'');

			throw new InvalidCredentialsToAccessPathException(formattedPathTheCouldNotBeAccessed);
		}
	}
}
