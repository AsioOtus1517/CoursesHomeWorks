using Delegates;

try 
{
    using var cts = new CancellationTokenSource();

    Console.CancelKeyPress += (sender, args) => {
        Console.WriteLine("Cancelling...");
        cts.Cancel();
    };

    var processor = new DirectoryProcessor();

    IList<FileInfo> files = null;
    processor.OnFileFound += (sender, args) => 
    {
        Console.WriteLine(args.FileInfo.Name);
        files.Add(args.FileInfo);
    };

    files = new List<FileInfo>();
    Console.Write("Input directory: ");
    processor.ProcessDirectory(Console.ReadLine(), cts.Token);

    var comparer = new Func<FileInfo, float>(file => file.Length);
    var maxItem = EnumerableExtensions.GetMax(files, comparer);
    Console.WriteLine("Biggest file: {0} {1}", maxItem.Name, maxItem.Length);
} 
catch(Exception e) 
{
    Console.WriteLine(e.Message);
}