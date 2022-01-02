using MoveAndTagMediaFiles;

namespace MoveAndTagMediaFilesWpfApp
{
	public partial class MediaPreviewWindow : Window
	{
		private List<string> MediaFilePaths = new List<string>();
		private PreviewSettings PreviewSettings = new PreviewSettings();

		public MediaPreviewWindow(List<string> mediaFilePaths, PreviewSettings previewSettings)
		{
			MediaFilePaths = mediaFilePaths;
			PreviewSettings = previewSettings;

			InitializeComponent();
		}
	}
}
