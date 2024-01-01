using Microsoft.Extensions.Configuration;
using Shared.Caching.Settings;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Caching
{
    public class CacheService : ICacheService
    {
       private readonly IConnectionMultiplexer _redis;
        private readonly IConfiguration _configuration;
        private  IDatabase _cacheDb;

        public CacheService(IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _redis = redis;
            _cacheDb= redis.GetDatabase();
            _configuration = configuration;
        }

        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default(T);
        }

        public object RemoveData(string key)
        {
            var keyexists = _cacheDb.KeyExists(key);
            if (keyexists)
            {
                return _cacheDb.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now); 
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value));
        }

        
        public DateTimeOffset SetCacheExpirationTime()
        {
            var expiration = _configuration.GetSection("RedisSetting").Get<RedisSetting>().cacheExpirationTime;
            var expiryTime = DateTimeOffset.Now.AddSeconds(expiration);
            return expiryTime;
        }
    }
}
