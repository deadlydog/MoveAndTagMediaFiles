using System.IO;
using System.Net;

namespace MoveAndTagMediaFiles.Tests;

public class FileRetrieverTests
{
	public record FilePaths(string DirectoryPath, int NumberOfMediaFiles);

	private FilePaths GetValidImageFilePaths()
	{
		var imagesDirectoryPath = TestHelpers.GetPathInTestProject("TestAssets/Images");
		return new FilePaths(imagesDirectoryPath, 6);
	}

	private FilePaths GetInvalidImageFilePaths()
	{
		var imagesDirectoryPath = TestHelpers.GetPathInTestProject("TestAssets/DirectoryThatDoesNotExist");
		return new FilePaths(imagesDirectoryPath, 0);
	}

	[Fact]
	public void GettingFilePathsForADirectoryThatExistsOnLocalDiskShouldSucceed()
	{
		// Arrange.
		var imagesDirectory = GetValidImageFilePaths();
		var fileSearchSettings = new FileSearchSettings()
		{
			SourceDirectory = imagesDirectory.DirectoryPath,
			SearchSubdirectories = true,
			FileSearchPattern = string.Empty,
			UseCredentials = false,
		};

		// Act.
		var filePaths = FileRetriever.GetFilePaths(fileSearchSettings);

		// Assert.
		filePaths.Count().Should().Be(imagesDirectory.NumberOfMediaFiles);
	}

	[Fact]
	public void GettingFilePathsForADirectoryThatDoesNotExistShouldReportAnError()
	{
		// Arrange.
		var imagesDirectory = GetInvalidImageFilePaths();
		var fileSearchSettings = new FileSearchSettings()
		{
			SourceDirectory = imagesDirectory.DirectoryPath,
			SearchSubdirectories = true,
			FileSearchPattern = string.Empty,
			UseCredentials = false,
		};

		// Act.
		var action = () => FileRetriever.GetFilePaths(fileSearchSettings);

		// Assert.
		action.Should().Throw<DirectoryNotFoundException>();
	}


	[Fact(Skip = "This is an integration test that requires a UNC path that requires credentials, which is hard to mock. So just unskip this test and run it manually when needed.")]
	public void GettingFilePathsForADirectoryThatExistsOnANetworkShareThatRequiresCredentialsShouldSucceed()
	{
		var uncPathPassword = "YOUR PASSWORD GOES HERE. DON'T COMMIT IT TO SOURCE CONTROL!!!";

		// Arrange.
		var fileSearchSettings = new FileSearchSettings()
		{
			SourceDirectory = @"\\DiskStation\photo\By Date\2010",
			SearchSubdirectories = false,
			FileSearchPattern = string.Empty,
			UseCredentials = true,
			CredentialsUsername = @"DiskStation\Dan",
			CredentialsPassword = new NetworkCredential(string.Empty, uncPathPassword).SecurePassword,
		};

		// Act.
		var filePaths = FileRetriever.GetFilePaths(fileSearchSettings);

		// Assert.
		filePaths.Count().Should().BeGreaterThan(10);
	}
}
