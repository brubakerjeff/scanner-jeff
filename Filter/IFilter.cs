namespace Filter
{
    public interface IFilter
    {
        bool IsSuspect(Scanee obj);
        public string meta {get;set;}
    }
}