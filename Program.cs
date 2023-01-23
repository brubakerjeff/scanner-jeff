using System.Collections.Concurrent;
const int taskCount=5; // Specify the number of simultaneous threads to for  processing

// Create a thread safe queue to load in files to be processed
ConcurrentQueue<string> cq = new ConcurrentQueue<string>();
// Create a thread safe queue of files we wish to quarantine
ConcurrentQueue<ResultInfo> quarantine = new ConcurrentQueue<ResultInfo>();

List<IFilter> FilterList = new List<IFilter>();
FilterList.Add(new MD5Filter());
FilterList.Add(new NameFilter());
Console.WriteLine("Enumerating files from the 'toscan' directory for processing");

string[] files = Directory.GetFiles("toscan");
foreach (string file in files)
{
    Console.WriteLine(file);
    cq.Enqueue(file);
}

// An action to consume the ConcurrentQueue.
Action my_action = () =>
{
    string? localValue;
    
    while (cq.TryDequeue(out localValue)) {
        var ff = new Scanee(localValue); 
        var metaDescription = "";
        var isSuspect=false;
        
        // Check the files against each of our filters
        foreach(var f in FilterList)
        {
            if(f.IsSuspect(ff)) {
                metaDescription+="|" + f.meta;
                isSuspect=true;
            }   
            Console.WriteLine("potential positive " + localValue);
        }
        if(isSuspect==true) {
            quarantine.Enqueue(new ResultInfo(localValue,metaDescription));
        }
    }
};


Action[] tasks = new Action[taskCount];
for (int i = 0; i < taskCount; i++)
{
    tasks[i] = () => my_action();
}

Parallel.Invoke(tasks);



if (!Directory.Exists("quarantine"))
{
    // If the directory does not exist, create it
    Directory.CreateDirectory("quarantine");
}
Console.WriteLine("moving files");
foreach(var i in quarantine)
{
    Console.WriteLine("Placing " + Path.GetFileName(i.Filename) + " into quarantine " + i.Reason);
    File.Move(Directory.GetCurrentDirectory() + "/" + i.Filename,Directory.GetCurrentDirectory() + "/quarantine/" + Path.GetFileName(i.Filename));
}