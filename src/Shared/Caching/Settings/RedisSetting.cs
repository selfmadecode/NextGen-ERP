namespace Shared.Caching.Settings
{
    public class RedisSetting
    {
        public string RedisCacheUrl { get; init; }

        public int CacheExpirationTime { get; init; }

    }
}
