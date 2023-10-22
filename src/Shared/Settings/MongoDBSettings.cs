namespace Shared.Settings;

public class MongoDBSettings
{
    //public string EmployeeCollectionName { get; set; }

    public required string Host { get; init; }
    public int Port { get; init; }
    public string ConnectionString => $"mongodb://{Host}:{Port}";
}
