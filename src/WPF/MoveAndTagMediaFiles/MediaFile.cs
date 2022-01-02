using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles;

public record MediaFile
{
	public string FilePath { get; init; } = string.Empty;
}
