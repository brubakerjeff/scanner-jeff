public class MD5Filter : IFilter
{
    private string _meta {get;set;}

     public string meta
    {
        get { return _meta; }
        set { _meta = value; }
    }
    public bool IsSuspect(Scanee obj) {
        _meta= "suspected by MD5 match";
        return true;
    }
}