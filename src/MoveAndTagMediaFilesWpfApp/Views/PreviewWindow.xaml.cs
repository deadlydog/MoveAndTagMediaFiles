using MoveAndTagMediaFiles;
using MoveAndTagMediaFiles.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoveAndTagMediaFilesWpfApp
{
	public partial class PreviewWindow : Window
	{
		public PreviewWindowViewModel ViewModel { get; init; }

		public PreviewWindow(PreviewWindowViewModel viewModel)
		{
			ViewModel = viewModel;

			InitializeComponent();
			this.DataContext = this;
		}
	}
}
