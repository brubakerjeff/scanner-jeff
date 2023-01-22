
using System.Collections.Concurrent;
var counter = 0;

ConcurrentQueue<string> cq = new ConcurrentQueue<string>();


string[] files = Directory.GetFiles("toscan");
foreach (string file in files)
{
    Console.WriteLine(file);
    cq.Enqueue(file);
}

int outerSum = 0;
// An action to consume the ConcurrentQueue.
Action action = () =>
{
    int localSum = 0;
    string localValue;
    while (cq.TryDequeue(out localValue)) {
      //  localSum += localValue;
        Console.WriteLine(localValue);
    }
    Interlocked.Add(ref outerSum, localSum);
    
};

// Start 4 concurrent consuming actions.
int taskCount=5;
Parallel.Invoke(() =>
{
    for (int i = 0; i < taskCount; i++)
    {
        // do some work
        Console.WriteLine("Task " + i + " is running");
    }
});
Console.WriteLine("outerSum = {0}, should be 49995000", outerSum);

/*
var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;
while (max == -1 || counter < max)
{
    Console.WriteLine($"Counter: {++counter}");
    await Task.Delay(TimeSpan.FromMilliseconds(1_000));
}*/