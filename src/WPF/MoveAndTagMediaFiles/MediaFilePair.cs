namespace MoveAndTagMediaFiles;

public record MediaFilePair
{
	public MediaFile SourceMediaFile { get; init; } = MediaFile.Empty;
	public MediaFile DestinationMediaFile { get; init; } = MediaFile.Empty;
}
