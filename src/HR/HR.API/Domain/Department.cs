using Shared.Entities;

namespace HR.API.Domain;

public class Department : EntityBase
{
    [BsonElement("Name")]
    public required string Name { get; set; }
    public int NoOfEmployees { get; set; }
    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("Manager")]
    public string Manager { get; set; }
}
