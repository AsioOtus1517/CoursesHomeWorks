namespace ParallelFileRead;

public interface IFileProcessor<TResult>
{
    TResult Process(string filepath);

    Task<TResult> ProcessAsync(string filepath);
}

public class TextFileProcessor : IFileProcessor<int>
{
    public int Process(string filepath) 
    {
        if(!File.Exists(filepath)) throw new FileNotFoundException(filepath);

        var text = File.ReadAllText(filepath);
        if(string.IsNullOrEmpty(text)) return 0;

        return text.Count(c => c == ' ');
    }

    public Task<int> ProcessAsync(string filepath) 
    {
        return Task.Run(() => Process(filepath));
    }
}
