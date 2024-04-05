namespace Shared.Common;

public class EntityBase : IAuditable
{
    public Guid Id { get; set; }

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
