using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles
{
	public class FileTagger
	{
		public static IEnumerable<string> GetTags(string filePath)
		{
			var shellFile = ShellFile.FromFilePath(filePath);
			var tags = (string[])shellFile.Properties.System.Keywords.ValueAsObject ?? Enumerable.Empty<string>();
			return tags;
		}

		public static void SetTags(string filePath, IEnumerable<string> tagsToAdd)
		{
			var shellFile = ShellFile.FromFilePath(filePath);
			shellFile.Properties.System.Keywords.Value = tagsToAdd.ToArray();
		}

		public static void AddTags(string filePath, IEnumerable<string> tagsToAdd)
		{
			var tags = GetTags(filePath).ToHashSet(StringComparer.OrdinalIgnoreCase);

			foreach(var newTag in tagsToAdd)
			{
				tags.Add(newTag);
			}

			SetTags(filePath, tags);
		}
	}
}
