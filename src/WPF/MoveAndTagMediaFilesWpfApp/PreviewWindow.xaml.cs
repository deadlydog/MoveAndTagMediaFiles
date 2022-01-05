using MoveAndTagMediaFiles;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoveAndTagMediaFilesWpfApp
{
	public partial class PreviewWindow : Window, INotifyPropertyChanged
	{
		#region Notify Property Changed
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		private List<string> _mediaFilePaths = new List<string>();
		private PreviewSettings _previewSettings = new PreviewSettings();
		private int _totalNumberOfMediaFiles = 0;
		private int _currentMediaFileIndex = 0;

		public PreviewWindow(List<string> mediaFilePaths, PreviewSettings previewSettings)
		{
			_mediaFilePaths = mediaFilePaths;
			_previewSettings = previewSettings;
			_totalNumberOfMediaFiles = mediaFilePaths.Count;

			InitializeComponent();
			this.DataContext = this;

			MoveToMediaFileIndex(0);
		}

		public MediaFilePair CurrentMediaFilePair
		{
			get => _currentMediaFilePair;
			set
			{
				_currentMediaFilePair = value;
				NotifyPropertyChanged();
			}
		}
		private MediaFilePair _currentMediaFilePair = new MediaFilePair();

		public string MediaFilesListLocationDisplay => $"{_currentMediaFileIndex + 1} of {_totalNumberOfMediaFiles}";

		public void MoveToNextMediaFile()
		{
			MoveToMediaFileIndex(_currentMediaFileIndex + 1);
		}

		public void MoveToPreviousMediaFile()
		{
			MoveToMediaFileIndex(_currentMediaFileIndex - 1);
		}

		public void MoveToMediaFileIndex(int indexToMoveTo)
		{
			if (indexToMoveTo < 0 || indexToMoveTo >= _totalNumberOfMediaFiles)
				return;

			_currentMediaFileIndex = indexToMoveTo;

			var sourceMediaFilePath = _mediaFilePaths[_currentMediaFileIndex];
			var destinationMediaFilePath = _previewSettings.PreserveDirectoryStructure ?
				sourceMediaFilePath.Replace(_previewSettings.SourceDirectory, _previewSettings.DestinationDirectory) :
				_previewSettings.DestinationDirectory;

			var sourceMediaFile = MediaFileFactory.Create(sourceMediaFilePath);
			var destinationMediaFile = MediaFileFactory.Create(destinationMediaFilePath);
			CurrentMediaFilePair = new MediaFilePair()
			{
				SourceMediaFile = sourceMediaFile,
				DestinationMediaFile = destinationMediaFile
			};

			NotifyPropertyChanged(nameof(MediaFilesListLocationDisplay));
		}

		private void btnPreviousMediaFile_Click(object sender, RoutedEventArgs e)
		{
			MoveToPreviousMediaFile();
		}

		private void btnNextMediaFile_Click(object sender, RoutedEventArgs e)
		{
			MoveToNextMediaFile();
		}
	}
}
