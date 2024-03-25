using System.Reflection.Emit;
using System.Reflection;
using Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{   
    public ApplicationDbContext(DbContextOptions options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}


