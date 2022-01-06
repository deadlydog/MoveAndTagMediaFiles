using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace MoveAndTagMediaFiles.Tests
{
	public class FileRetrieverTests
	{
		public record FilePaths(string DirectoryPath, int NumberOfMediaFiles);

		private FilePaths GetValidImageFilePaths()
		{
			var imagesDirectoryPath = TestHelpers.GetPathInTestProject("TestAssets/Images");
			return new FilePaths(imagesDirectoryPath, 3);
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
	}
}
