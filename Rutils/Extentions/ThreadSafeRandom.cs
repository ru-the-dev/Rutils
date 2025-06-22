namespace Rutils.Extentions;


public static class ThreadSafeRandom
{
    public static Random Random
    {
        get
        {
            if (_localRandom == null)
            {
                lock (_global) 
                {
                    _localRandom = new Random(_global.Next());
                }
            }
            
            return _localRandom;
        }
    }
    [ThreadStatic] private static Random? _localRandom;

    // Global random is used to generate the seeds for the local randoms
    private static Random _global = new();
}