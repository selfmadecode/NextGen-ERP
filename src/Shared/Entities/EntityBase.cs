namespace Shared.Entities;

public class EntityBase
{
    public EntityBase()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}
