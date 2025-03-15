namespace ParallelFileRead;

public class DirectoryProcessor
{
    public async Task<IEnumerable<int>> ProcessDirectoryAsync(string directory, IFileProcessor<int> fileProcessor)
    {
        if (!Directory.Exists(directory)) throw new DirectoryNotFoundException();

        return await Task.Run(() =>
        {
            var result = new List<int>();
            foreach (var fileName in Directory.GetFiles(directory))
            {
                result.Add(fileProcessor.Process(fileName));
            }

            return result;
        });
    }
}