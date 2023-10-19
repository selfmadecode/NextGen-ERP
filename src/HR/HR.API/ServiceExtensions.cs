namespace HR.API
{
    public static class ServiceExtensions
    {

        public static void ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDBSettings"));
            
        }
    }
}
