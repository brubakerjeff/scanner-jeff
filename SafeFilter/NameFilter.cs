namespace SafeFilter
{
    public class NameFilter : ISafeFilter
    {
        private string _meta {get;set;}

        List<string> names = new List<string>() { "iamsafe" };

        public string meta
        {
            get { return _meta; }
            set { _meta = value; }
        }
        public bool IsSafe(Scanee obj) {
            if(names.Contains(Path.GetFileName(obj._filename))) {
                _meta= "safe by file name match";
                return true;
            }
            else {
                return false;
            }
            
        }
    }
}
