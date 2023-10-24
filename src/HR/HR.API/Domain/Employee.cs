namespace HR.API.Domain;

public class Employee : EntityBase, IEntity
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("FirstName")]
    public required string FirstName { get; set; }

    [BsonElement("LastName")]
    public required string LastName { get; set; }

    [BsonElement("DateOfBirth")]
    public DateTime DateOfBirth{ get; set; }

    [BsonElement("PhoneNumber")]
    public string Contact { get; set; }


    [BsonElement("Email")]
    public string Email { get; set; }

    [BsonElement("JobTitle")]
    public required string JobTitle { get; set; }

    [BsonElement("ResumptionDate")]
    public DateTime HiredDate { get; set; }

    public Guid DepartmentId { get; set; }

    public Address Address { get; set; }
    
}
