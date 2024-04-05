using Microsoft.AspNetCore.Identity;

namespace Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public Guid? CompanyId { get; set; }

        public int MyProperty { get; set; }
    }
}
