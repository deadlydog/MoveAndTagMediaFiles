namespace MoveAndTagMediaFiles;

public record MediaFile
{
	public string FilePath { get; init; } = string.Empty;

	public MediaTypes MediaFileType { get; init; }

	public bool FileExists { get; init; }

	public static MediaFile Empty => _empty;
	private static MediaFile _empty = new MediaFile();
}
