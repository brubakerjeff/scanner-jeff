public class MD5Filter : IFilter
{
    public bool IsSuspect(Scanee obj) {
        return true;
    }
}