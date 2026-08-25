using StackExchange.Redis;

namespace SLMS.Infrastructure.Locking;

// UC-C09: lock:alloc:{stationId}:{size}, TTL 5s, released only if the stored value matches
// requestId (Lua script — avoids releasing a lock some other request already re-acquired).
public class RedisDistributedLock
{
    private const string ReleaseScript = @"
        if redis.call('get', KEYS[1]) == ARGV[1] then
            return redis.call('del', KEYS[1])
        else
            return 0
        end";

    private readonly IConnectionMultiplexer _redis;

    public RedisDistributedLock(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<bool> TryAcquireAsync(string key, string requestId, TimeSpan ttl)
    {
        var db = _redis.GetDatabase();
        return await db.StringSetAsync(key, requestId, ttl, When.NotExists);
    }

    public async Task ReleaseAsync(string key, string requestId)
    {
        var db = _redis.GetDatabase();
        await db.ScriptEvaluateAsync(ReleaseScript, new RedisKey[] { key }, new RedisValue[] { requestId });
    }
}
