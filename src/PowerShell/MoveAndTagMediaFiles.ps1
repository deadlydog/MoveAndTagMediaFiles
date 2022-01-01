[CmdletBinding()]
Param
(
	[string] $SourceDirectory = 'C:\dev\Git\PreviewAndCopyFilesIntoDirectory\tests\Images',

	[string] $DestinationDirectory = 'C:\dev\Git\PreviewAndCopyFilesIntoDirectory\tests\CopiedImages',

	[string[]] $FilePatternsToInclude = @('*.jpg', '*.jpeg', '*.png', '*.gif'),

	[switch] $PreserveDirectoryStructure = $true
)

Process
{
	Write-Information "Retrieving list of files to process..."
	[System.IO.FileInfo[]] $files = Get-MatchingFilesInSourceDirectory -sourceDirectory $SourceDirectory -filePatternsToInclude $FilePatternsToInclude

	[int] $totalNumberOfFilesToProcess = $files.Length
	[int] $numberOfFilesProcessed = 0
	$files | ForEach-Object {
		$numberOfFilesProcessed++
		[System.IO.FileInfo] $file = $_
		[string] $filePath = $file.FullName

		Write-Information "Processing file ($numberOfFilesProcess of $totalNumberOfFilesToProcess): '$filePath'..."
		try
		{
			$process = $null
			$process = Start-Process -FilePath $filePath -Wait -PassThru
		}
		catch
		{
			[string] $errorMessage = $_.Exception.ToString()

			# UWP apps don't support PassThru, so look for the ApplicationFrameHost process that runs them.
			if ($errorMessage.Contains('This command cannot be run completely because the system cannot find all the information required.'))
			{
				$process = Get-Process -Name 'ApplicationFrameHost' |
					# Where-Object { $_.MainWindowTitle.Contains($file.Name) } |
					Select-Object -First 1
			}

			if ($null -eq $process)
			{
				Write-Error "An error occurred trying to launch file '$filePath': $errorMessage"
				return
			}
		}

		Set-FocusToThisWindow
		$answer = Read-Host -Prompt "What would you like to do? Copy (C), Move (M), Skip (S), or Abort (A)?"

		if (!$process.HasExited)
		{
			$process.Kill()
		}
	}
}

Begin
{
	$InformationPreference = 'Continue'
	$VerbosePreference = 'Continue'

	# Display the time that this script started running.
	[DateTime] $startTime = Get-Date
	Write-Verbose "Starting script at '$startTime'."

	function Get-MatchingFilesInSourceDirectory([string] $sourceDirectory, [string[]] $filePatternsToInclude)
	{
		$files = Get-ChildItem -Path $sourceDirectory -Include $filePatternsToInclude -Recurse -Force -File
		return $files
	}

	function Set-FocusToThisWindow
	{
		$WindowState = '[DllImport("user32.dll")] public static extern bool ShowWindow(int handle, int state);'
		Add-Type -Name win -member $WindowState -Namespace native
		[native.win]::ShowWindow(([System.Diagnostics.Process]::GetCurrentProcess() | Get-Process).MainWindowHandle, 0)
		SetForegroundWindow(this.Handle)
	}
}

End
{
	# Display the time that this script finished running, and how long it took to run.
	[DateTime] $finishTime = Get-Date
	[TimeSpan] $elapsedTime = $finishTime - $startTime
	Write-Verbose "Finished script at '$finishTime'. Took '$elapsedTime' to run."
}
