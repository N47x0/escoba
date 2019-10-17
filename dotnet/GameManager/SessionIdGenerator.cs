public class SessionIdGenerator : ISessionIdGenerator
{
    private long _currentId = 1;

    public long Next()
    {
        return ++_currentId;
    }
}