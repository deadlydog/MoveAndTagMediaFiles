namespace MoveAndTagMediaFiles.Tests;

public class MediaFileFactoryTests
{
	[Fact]
	public void CreatingMediaFileForAFilePathThatDoesNotExistShouldCreateAnInstanceWithTheCorrectFilePath()
	{
		var filePathThatDoesNotExist = @"C:\A file path\that does not exist";
		var mediaFile = MediaFileFactory.Create(filePathThatDoesNotExist);

		mediaFile.Should().NotBeNull();
		mediaFile.FilePath.Should().Be(filePathThatDoesNotExist);
	}

	[Fact]
	public void CreatingMediaFileForAFilePathThatDoesNotExistShouldCreateAnInstanceShowingTheFilePathDoesNotExist()
	{
		var filePathThatDoesNotExist = @"C:\A file path\that does not exist";
		var mediaFile = MediaFileFactory.Create(filePathThatDoesNotExist);

		mediaFile.Should().NotBeNull();
		mediaFile.FileExists.Should().BeFalse();
	}

	[Fact]
	public void CreatingMediaFileForAFilePathThatDoesNotExistShouldCreateAnInstanceWithoutAnyTags()
	{
		var filePathThatDoesNotExist = @"C:\A file path\that does not exist";
		var mediaFile = MediaFileFactory.Create(filePathThatDoesNotExist);

		mediaFile.Should().NotBeNull();
		mediaFile.Tags.Should().BeEmpty();
	}
}
