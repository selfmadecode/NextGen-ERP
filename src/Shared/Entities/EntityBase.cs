namespace Shared.Entities;

public class EntityBase// inherit from IEntity
{
    public EntityBase()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}

public interface IEntity
{
    public Guid Id { get; set; }
}