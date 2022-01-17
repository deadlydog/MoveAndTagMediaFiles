using System.IO;

namespace MoveAndTagMediaFiles.Tests;

public class FileTaggerTests
{
	public record TaggedFile(string filePath, IEnumerable<string> tags);

	private IEnumerable<TaggedFile> GetTaggedTestFiles()
	{
		var imagesDirectoryPath = TestHelpers.GetPathInTestProject("TestAssets/Images");
		var taggedImageFiles = new HashSet<TaggedFile>()
			{
				// These tags should correspond to the actual tags on the test image files.
				new TaggedFile(Path.Combine(imagesDirectoryPath, "ImageWithNoTags.jpg"), new HashSet<string>()),
				new TaggedFile(Path.Combine(imagesDirectoryPath, "ImageWithOneTag.jpg"), new HashSet<string>(){ "tag1" }),
				new TaggedFile(Path.Combine(imagesDirectoryPath, "ImageWithTwoTags.jpg"), new HashSet<string>(){ "tag1", "tag2" }),

				new TaggedFile(Path.Combine(imagesDirectoryPath, "PhotoWithNoTags.jpg"), new HashSet<string>()),
				new TaggedFile(Path.Combine(imagesDirectoryPath, "PhotoWithOneTag.jpg"), new HashSet<string>(){ "tag1" }),
				new TaggedFile(Path.Combine(imagesDirectoryPath, "PhotoWithTwoTags.jpg"), new HashSet<string>(){ "tag1", "tag2" }),
			};
		return taggedImageFiles;
	}



	[Fact]
	public void GettingsTagsFromValidFilesShouldReturnTheCorrectTags()
	{
		// Arrange.
		var taggedFiles = GetTaggedTestFiles();

		foreach (var taggedFile in taggedFiles)
		{
			// Act.
			var tags = FileTagger.GetTags(taggedFile.filePath);

			// Assert.
			tags.Should().Equal(taggedFile.tags);
		}
	}

	[Fact]
	public void GettingTagsFromAFileThatDoesNotExistShouldThrowAnException()
	{
		var action = () => FileTagger.GetTags("A file path that does not exist");

		action.Should().Throw<IOException>();
	}
}
