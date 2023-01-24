using System.Collections.Concurrent;
using Filter;
using SafeFilter;

const int taskCount = 5; // Specify the number of simultaneous threads to for  processing

// Create a thread safe queue to load in files to be processed
ConcurrentQueue<string> cq = new ConcurrentQueue<string>();
// Create a thread safe queue of files we wish to quarantine
ConcurrentQueue<ResultInfo> quarantine = new ConcurrentQueue<ResultInfo>();


//List our filters identifying suspicious files
List<IFilter> FilterList = new List<IFilter>();
FilterList.Add(new SHA256Filter());
FilterList.Add(new Filter.NameFilter());

//List out filters identifying safe files (overrides the filters above)
List<ISafeFilter> SafeFilterList = new List<ISafeFilter>();
SafeFilterList.Add(new SafeFilter.NameFilter());

Console.WriteLine("Enumerating files from the 'toscan' directory for processing");

var dir = "toscan";

// Traverse directory structure populating the queue with names of files
static void PopulateQueues(ConcurrentQueue<string> cq, string dir)
{
    string[] files = Directory.GetFiles(dir);
    foreach (string file in files)
    {
        Console.WriteLine(file);
        cq.Enqueue(file);
    }

    string[] dirs = Directory.GetDirectories(dir);
    foreach (string subdir in dirs)
    {
        PopulateQueues(cq,subdir);
    }
}

PopulateQueues(cq, dir);

// Build an action so we can process in parallel to increase throughput 
Action my_action = () =>
{
    string? localValue;

    while (cq.TryDequeue(out localValue))
    {
        Console.WriteLine("Scanning " + localValue);

        var ff = new Scanee(localValue);
        var metaDescription = "";
        var isSuspect = false;

        // Check the files against each of our filters
        foreach (var SuspFilter in FilterList)
        {
            if (SuspFilter.IsSuspect(ff))
            {
                metaDescription += "|" + SuspFilter.meta;
                isSuspect = true;
                // Check the suspected files against any tagged as safe
                foreach (var SafeFilter in SafeFilterList)
                {
                    if(SafeFilter.IsSafe(ff)) 
                    {
                        isSuspect=false;
                        break;
                    }
                }
  
            }
        }
        if (isSuspect == true)
        {
            Console.WriteLine("Unsafe file found and quarantined " + localValue);
            quarantine.Enqueue(new ResultInfo(localValue, metaDescription));
        }
    }
};

Action[] tasks = BuildTasks(taskCount, my_action);
// Invoke our actions
Parallel.Invoke(tasks);


// Upon completion move any files to the quarantine directory.
if (!Directory.Exists("quarantine"))
{
    // If the directory does not exist, create it
    Directory.CreateDirectory("quarantine");
}
Console.WriteLine("moving files");
foreach (var i in quarantine)
{
    Console.WriteLine("Placing " + Path.GetFileName(i.Filename) + " into quarantine " + i.Reason);
    File.Move(Directory.GetCurrentDirectory() + "/" + i.Filename, Directory.GetCurrentDirectory() + "/quarantine/" + Path.GetFileName(i.Filename),true);
}

static Action[] BuildTasks(int taskCount, Action my_action)
{
    Action[] tasks = new Action[taskCount];
    for (int i = 0; i < taskCount; i++)
    {
        tasks[i] = () => my_action();
    }

    return tasks;
}
