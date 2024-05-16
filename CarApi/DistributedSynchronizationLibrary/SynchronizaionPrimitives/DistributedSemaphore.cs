using DistributedSynchronizationLibrary.Interfaces;
using StackExchange.Redis;

namespace DistributedSynchronizationLibrary.SynchronizaionPrimitives;

public class DistributedSemaphore : IDistributedSemaphore
{
    private readonly IDatabase _database;

    private static string _waitScript = 
        "local permit = redis.call('get', KEYS[1]); "
        + "if (permit == false) then "
        + "permit = 0;"
        + "end;"
        + "if (tonumber(permit) < tonumber(ARGV[1])) then "
        + "redis.call('set', KEYS[1], permit + 1); "
        + "redis.call('publish', KEYS[2], permit + 1); "
        + "return 1; "
        + "else "
        + "return 0; "
        + "end;";
    
    public DistributedSemaphore(IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }

    /// <inheritdoc />
    public async Task<bool> AcquireAsync(string key, TimeSpan timeout)
    {
        var script = _waitScript;
        var semaphoreKey = $"{key}:semaphore";
        var result = await _database.ScriptEvaluateAsync(script, new RedisKey[] { semaphoreKey });

        return (bool)result;
    }

    /// <inheritdoc />
    public async Task Release(string key)
    {
        var semaphoreKey = $"{key}:semaphore";
        
        await _database.StringIncrementAsync(semaphoreKey);
    }
}