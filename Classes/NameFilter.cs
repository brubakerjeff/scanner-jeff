public class NameFilter : IFilter
{
    private string _meta {get;set;}

    List<string> names = new List<string>() { "hello123" };

     public string meta
    {
        get { return _meta; }
        set { _meta = value; }
    }
    public bool IsSuspect(Scanee obj) {
        if(names.Contains(Path.GetFileName(obj._filename))) {
            _meta= "suspected by file name match";
            return true;
        }
        else {
            return false;
        }
        
    }
}