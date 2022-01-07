using System.IO;
using System.Reflection;

namespace MoveAndTagMediaFiles.Tests
{
	public class TestHelpers
	{
		public static string GetPathInTestProject(string testProjectRelativePath)
		{
			return Path.Combine(GetTestProjectDirectoryPath(), testProjectRelativePath);
		}

		public static string GetTestProjectDirectoryPath()
		{
			var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
			var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
			var directoryPath = Path.GetDirectoryName(codeBasePath) ?? string.Empty;
			return directoryPath;
		}
	}
}
