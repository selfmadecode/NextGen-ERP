using HR.API.Enums;
namespace HR.API.Domain;

public class Company : EntityBase, IEntity
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("CompanyName")]  // change to name too
    public required string CompanyName { get; set; } // Name

    [BsonElement("About")]
    public required string About { get; set; }

    [BsonElement("Vision")]
    public required string Vision { get; set; }

    [BsonElement("Mission")]
    public required string Mission { get; set; }

    public List<CompanyDocument> CompanyDocumentsList { get; set; } = new(); // change to document


}
