using Microsoft.AspNetCore.Identity;
using Shared.Common;
namespace Auth.Models;

public class ApplicationUser : IdentityUser, IAuditable
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Guid? CompanyId { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}
