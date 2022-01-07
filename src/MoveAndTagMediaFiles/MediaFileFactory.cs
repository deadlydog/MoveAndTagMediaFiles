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

		var mediaType = MediaFileTypeResolver.GetMediaTypeFromFilePath(filePath);

		var mediaFile = new MediaFile()
		{
			FilePath = filePath,
			FileExists = fileExists,
			MediaFileType = mediaType
		};

		return mediaFile;
	}
}
