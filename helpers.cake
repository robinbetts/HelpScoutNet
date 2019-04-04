private void GenerateReleaseNote(string directoryName, IEnumerable<string> lines){
	  string releaseNoteFile=$"{directoryName}/releasenotes.md";


	   var releaseNotesExitCode = StartProcess(
        @"tools\GitReleaseNotes.0.7.1\tools\gitreleasenotes.exe", 
        new ProcessSettings { Arguments = $". /o {releaseNoteFile}" });

		System.IO.File.AppendAllText(releaseNoteFile,Environment.NewLine + String.Join("\r\n",lines));

    }