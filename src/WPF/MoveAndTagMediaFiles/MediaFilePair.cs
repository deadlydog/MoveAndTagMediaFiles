namespace MoveAndTagMediaFiles;

public record MediaFilePair
{
	public MediaFile SourceMediaFile { get; set; }
	public MediaFile DestinationMediaFile { get; set; }
}
