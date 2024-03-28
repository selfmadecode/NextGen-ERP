namespace Shared.Caching
{
    public static class RegisterRedis
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = configuration.GetSection("RedisSetting").Get<RedisSetting>();
            services.AddSingleton<IConnectionMultiplexer>(opt =>
              ConnectionMultiplexer.Connect(redisSettings.RedisCacheUrl));

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}
