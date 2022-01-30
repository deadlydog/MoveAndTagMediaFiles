namespace MoveAndTagMediaFiles;

public class MediaFileFactory
{
	public static MediaFile Create(string filePath)
	{
		bool fileExists = false;
		try
		{
			fileExists = File.Exists(filePath);
		}
		catch
		{
			// Eat any exceptions thrown and assume the file doesn't exist.
		}

		var tags = new HashSet<string>();
		if (fileExists)
		{
			tags = FileTagger.GetTags(filePath).ToHashSet(StringComparer.OrdinalIgnoreCase);
		}

		var mediaType = MediaFileTypeResolver.GetMediaTypeFromFilePath(filePath);

		var mediaFile = new MediaFile()
		{
			FilePath = filePath,
			FileExists = fileExists,
			MediaFileType = mediaType,
			Tags = tags,
		};

		return mediaFile;
	}
}
