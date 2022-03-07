using MoveAndTagMediaFiles.Infrastructure.MVVM;
using MoveAndTagMediaFiles.Services;

namespace MoveAndTagMediaFiles.ViewModels;

public record PreviewWindowViewModelArguments
{
	public IList<string> MediaFilePaths { get; init; } = new List<string>();
	public PreviewSettings PreviewSettings { get; init; } = new PreviewSettings();
}

public class PreviewWindowViewModel : ViewModelBase
{
	public string WindowTitle => "Preview, move, and tag media files";

	public PreviewWindowViewModel(ICommonServices commonServices, PreviewWindowViewModelArguments args) : base(commonServices)
	{
		ArgumentNullException.ThrowIfNull(args, nameof(args));

		MediaFilePaths = args.MediaFilePaths ?? throw new ArgumentNullException(nameof(args.MediaFilePaths), "MediaFilePaths list is null");
		PreviewSettings = args.PreviewSettings ?? throw new ArgumentNullException(nameof(args.PreviewSettings), "PreviewSettings is null");
	}

	public RelayCommandAsync MoveToPreviousMediaFileCommand =>
		new RelayCommandAsync(MoveToPreviousMediaFile, () => CurrentMediaFileIndex > 0);
	public RelayCommandAsync MoveToNextMediaFileCommand =>
		new RelayCommandAsync(MoveToNextMediaFile, () => CurrentMediaFileIndex < (TotalNumberOfMediaFiles - 1));

	public IList<string> MediaFilePaths
	{
		get => _mediaFilePaths;
		set
		{
			SetProperty(ref _mediaFilePaths, value);
			TotalNumberOfMediaFiles = _mediaFilePaths.Count;
			CurrentMediaFileIndex = 0;
		}
	}
	private IList<string> _mediaFilePaths = new List<string>();

	public PreviewSettings PreviewSettings
	{
		get => _previewSettings;
		set => SetProperty(ref _previewSettings, value);
	}
	private PreviewSettings _previewSettings = new PreviewSettings();

	public int TotalNumberOfMediaFiles
	{
		get => _totalNumberOfMediaFiles;
		set
		{
			SetProperty(ref _totalNumberOfMediaFiles, value);
			NotifyPropertyChanged(nameof(MediaFilesListLocationDisplay));
		}
	}
	private int _totalNumberOfMediaFiles = 0;

	private int CurrentMediaFileIndex
	{
		get => _currentMediaFileIndex;
		set
		{
			SetProperty(ref _currentMediaFileIndex, value);

			MoveToPreviousMediaFileCommand.RaiseCanExecuteChanged();
			MoveToNextMediaFileCommand.RaiseCanExecuteChanged();
			NotifyPropertyChanged(nameof(MediaFilesListLocationDisplay));
		}
	}
	private int _currentMediaFileIndex = 0;

	public string MediaFilesListLocationDisplay => $"{CurrentMediaFileIndex + 1} of {TotalNumberOfMediaFiles}";

	public MediaFilePair CurrentMediaFilePair
	{
		get => _currentMediaFilePair;
		set => SetProperty(ref _currentMediaFilePair, value);
	}
	private MediaFilePair _currentMediaFilePair = new MediaFilePair();

	public bool AutomaticallyProgressToNextMediaFile
	{
		get => _automaticallyProgressToNextMediaFile;
		set => SetProperty(ref _automaticallyProgressToNextMediaFile, value);
	}
	private bool _automaticallyProgressToNextMediaFile;

	private async Task MoveToNextMediaFile()
	{
		MoveToMediaFileIndex(CurrentMediaFileIndex + 1);
	}

	private async Task MoveToPreviousMediaFile()
	{
		MoveToMediaFileIndex(CurrentMediaFileIndex - 1);
	}

	private void MoveToMediaFileIndex(int indexToMoveTo)
	{
		if (indexToMoveTo < 0 || indexToMoveTo >= TotalNumberOfMediaFiles)
			return;

		CurrentMediaFileIndex = indexToMoveTo;

		var sourceMediaFilePath = _mediaFilePaths[CurrentMediaFileIndex];
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
	}
}
