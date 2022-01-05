namespace MoveAndTagMediaFiles;

public class MediaFileTypeResolver
{
	public static MediaTypes GetMediaTypeFromFilePath(string filePath)
	{
		var fileExtension = Path.GetExtension(filePath).TrimStart('.');
		return GetMediaTypeFromFileExtension(fileExtension);
	}

	public static MediaTypes GetMediaTypeFromFileExtension(string extension) =>
		extension.ToLower() switch
		{
			"ai" or "andd" or "bmp" or "eps" or "gif" or "heic" or "heif" or "jpg" or "jpeg" or
			"pdf" or "png" or "psd" or "svg" or "svgz" or "raw" or "tif" or "tiff" or "webp" => MediaTypes.Image,

			"avi" or "avchd" or "flv" or "f4v" or "mkv" or "mov" or "m4p" or "m4v" or "mp4" or
			"mpg" or "mpeg" or "ogg" or "ogv" or "qt" or "swf" or "webm" or "wmv" => MediaTypes.Video,

			"aac" or "flac" or "m4a" or "mp3" or "wav" or "wma" => MediaTypes.Audio,

			_ => MediaTypes.Unknown
		};
}
