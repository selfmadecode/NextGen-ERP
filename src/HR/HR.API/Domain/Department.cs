using System.ComponentModel.DataAnnotations.Schema;

namespace HR.API.Domain;

public class Department : EntityBase, IEntity
{
    [BsonId]
    public Guid Id { get; set; }    

    [BsonElement("Name")]
    public required string Name { get; set; }

    [BsonElement("NoOfEmployees")]
    public int NoOfEmployees { get; set; }

    [BsonElement("Description")]
    public string? Description { get; set; }

    [BsonElement("Manager")]
    public required string Manager { get; set; }

}
