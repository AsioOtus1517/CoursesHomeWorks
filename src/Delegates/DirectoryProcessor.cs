namespace Delegates;

public class DirectoryProcessor
{
    public class FileArgs(FileInfo fileInfo) : EventArgs 
    {
        public FileInfo FileInfo { get; set; } = fileInfo;
    }

    public event EventHandler<FileArgs> OnFileFound;

    public void ProcessDirectory(string directory, CancellationToken token) 
    {
        if(token.IsCancellationRequested) return;

        if (!Directory.Exists(directory)) 
        {
            throw new ArgumentException("Directory doesn't exist");
        }

        Task.Delay(1000).Wait();

        foreach(var directoryInfo in new DirectoryInfo(directory).GetDirectories()) 
        {
            if(token.IsCancellationRequested) return;
            Task.Delay(1000).Wait();
            ProcessDirectory(directoryInfo.FullName, token);
        }

        foreach(var fileInfo in new DirectoryInfo(directory).GetFiles()) 
        {
            if(token.IsCancellationRequested) return;
            Task.Delay(1000).Wait();
            OnFileFound?.Invoke(this, new FileArgs(fileInfo));
        }
    }
}