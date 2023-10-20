using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Shared.Interfaces;
using Shared.Settings;
using MongoDB.Driver;
using Shared.Entities;

namespace Shared.MongoDb;

public static class Extensions
{
    //private static ServiceSettings? _serviceSettings;

    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // guid type is stored as string
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String)); // date type is stored as string

        services.Configure<MongoDBSettings>(configuration.GetSection("MongoDBSettings"));

        //_serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();



        // Register the MongoDB client and database in DI
        // construct mongo client and database globally
        services.AddSingleton(sp =>
        {
            var mongoDbSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

            return mongoClient.GetDatabase(serviceSettings.ServiceName);
        });

        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
            where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(sp =>
        {
            var database = sp.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database, collectionName);
        });

        return services;
    }
}
