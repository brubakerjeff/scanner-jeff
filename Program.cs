using System.Collections.Concurrent;
const int taskCount=5; // Specify the number of simultaneous threads to for  processing


// Create a thread safe queue 
ConcurrentQueue<string> cq = new ConcurrentQueue<string>();
ConcurrentQueue<string> quarantine = new ConcurrentQueue<string>();

Console.WriteLine("Adding these files for processing");

string[] files = Directory.GetFiles("toscan");
foreach (string file in files)
{
    Console.WriteLine(file);
    cq.Enqueue(file);
}

int outerSum = 0;
// An action to consume the ConcurrentQueue.
Action my_action = () =>
{
    string? localValue;
    
    while (cq.TryDequeue(out localValue)) {
        if(localValue.Contains("file")) {
            quarantine.Enqueue(localValue);
            Console.WriteLine("potential positive " + localValue);
        }
    }
    
};


Action[] tasks = new Action[taskCount];
for (int i = 0; i < taskCount; i++)
{
    tasks[i] = () => my_action();
}

Parallel.Invoke(tasks);

Console.WriteLine("outerSum = {0}, should be 49995000", outerSum);


if (!Directory.Exists("quarantine"))
{
    // If the directory does not exist, create it
    Directory.CreateDirectory("quarantine");
}
Console.WriteLine("moving files");
foreach(var i in quarantine)
{
    File.Move(Directory.GetCurrentDirectory() + "/" + i,Directory.GetCurrentDirectory() + "/quarantine/" + Path.GetFileName(i));
}