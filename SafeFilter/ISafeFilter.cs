using Filter;
namespace SafeFilter
{
    public interface ISafeFilter
    {
        bool IsSafe(Scanee obj);
        public string meta {get;set;}
    }
}