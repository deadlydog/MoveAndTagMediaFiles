using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFiles.ViewModels
{
	public record PreviewWindowViewModelArguments
	{
		public ICollection<string> MediaFilePaths { get; init; } = new List<string>();
		public PreviewSettings PreviewSettings { get; init; } = new PreviewSettings();
	}

	public class PreviewWindowViewModel
	{
		public PreviewWindowViewModel(PreviewWindowViewModelArguments args)
		{

		}
	}
}
