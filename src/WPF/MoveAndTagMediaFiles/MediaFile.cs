namespace MoveAndTagMediaFiles;

public record MediaFile
{
	public string FilePath { get; init; } = string.Empty;

	public MediaTypes MediaFileType { get; init; }

	public bool FileExists { get; init; }
}
