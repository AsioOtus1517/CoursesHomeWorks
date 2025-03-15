using System.Diagnostics;
using ParallelFileRead;

// Пример вызова 
// ParallelFileRead - 1 часть задания, + заполнить список files
// ParallelFileRead <directory> - для 2 части

var fileProcessor = new TextFileProcessor();

#region Files from list

var w = new Stopwatch();

if (args.Length == 0)
{
    var files = new List<string>()
    {
        // TODO: add file paths
    };

    w.Start();

    var tasks = files.Select(fileProcessor.ProcessAsync);
    var results = await Task.WhenAll(tasks);

    w.Stop();

    Console.WriteLine($"{w.ElapsedMilliseconds}");

    return;
}

#endregion

#region Files from directory

var directory = args[0];

w.Start();

var result = await new DirectoryProcessor().ProcessDirectoryAsync(directory, fileProcessor);

w.Stop();

Console.WriteLine($"{w.ElapsedMilliseconds}");

#endregion