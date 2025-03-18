using System.Diagnostics;

#region Без использования потоков

Console.WriteLine("Суммирование без потоков");

SimpleSuming(100000);
SimpleSuming(1000000);
SimpleSuming(10000000);

Console.WriteLine("----------------------------------------------------------");

#endregion

#region Использование потоков

Console.WriteLine("Суммирование с использованием потоков");

ThreadSumming(100000);
ThreadSumming(1000000);
ThreadSumming(10000000);

Console.WriteLine("----------------------------------------------------------");

#endregion

#region PLINQ

Console.WriteLine("Суммирование PLINQ");

PlinqSuming(100000);
PlinqSuming(1000000);
PlinqSuming(10000000);

Console.WriteLine("----------------------------------------------------------");

#endregion

return;

void SimpleSuming(int count) 
{
    var collection = GenerateCollection(count);
    var w = new Stopwatch();
    
    w.Start();

    var sum = 0;
    foreach(var item in collection) sum += item;
    // Console.WriteLine($"Сумма: {sum}");

    w.Stop();

    Console.Write($"{count} | ");
    Console.WriteLine($"Время выполнения {w.ElapsedMilliseconds}");
}

void ThreadSumming(int count) 
{
    var collection = GenerateCollection(count).ToList();
    var w = new Stopwatch();
    
    w.Start();

    int totalSum = 0;
    Lock lockObj = new();
        
    int threadCount = Environment.ProcessorCount; 
    int chunkSize = collection.Count() / threadCount;
        
    Thread[] threads = new Thread[threadCount];
    
    for (int i = 0; i < threadCount; i++)
    {
        int start = i * chunkSize;
        int end = (i == threadCount - 1) ? collection.Count() : start + chunkSize;
            
        threads[i] = new Thread(() =>
        {
            int localSum = 0;
            for (int j = start; j < end; j++) localSum += collection[j];
                
            lock (lockObj) totalSum += localSum;
        });
            
        threads[i].Start();
    }
    
    foreach (var thread in threads) thread.Join();

    w.Stop();

    Console.Write($"{count} | ");
    Console.WriteLine($"Время выполнения {w.ElapsedMilliseconds}");
}

void PlinqSuming(int count) 
{
    var collection = GenerateCollection(count);
    var w = new Stopwatch();
    
    w.Start();

    var result = collection.AsParallel().Sum();
    // Console.WriteLine($"Сумма: {result}");

    w.Stop();

    Console.Write($"{count} | ");
    Console.WriteLine($"Время выполнения {w.ElapsedMilliseconds}");
}

IEnumerable<int> GenerateCollection(int count, int min = 0, int max = 100)
{
    var random = new Random(DateTime.Now.Millisecond);
    for(var i = 0; i < count; i++) 
    {
        yield return random.Next(min, max);
    }
}