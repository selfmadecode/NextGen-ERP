using System;
namespace Shared.Common;

public interface IAuditable
{
    /// <summary>
    /// DateTime of creation. This value will never change.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Indicates whether the entity is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Represents the ID of the user who deleted the entity, if applicable.
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// DateTime of last value update. Should be updated when entity data is modified.
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// Author of the last value update. Represents the ID of the user who modified the entity data.
    /// Should be updated when entity data is modified.
    /// </summary>
    public Guid? ModifiedBy { get; set; }

    /// <summary>
    /// DateTime of deletion. Represents the date and time when the entity was deleted, if applicable.
    /// </summary>
    public DateTime? DeletedOn { get; set; }
}

public interface IHaveId
{
    public Guid Id { get; set; }
}
