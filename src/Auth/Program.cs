using auth;
using auth.Constants;
using auth.Database;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
var migrationAssembly = typeof(Config).Assembly.GetName().Name;

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString, sqlliteOptions => sqlliteOptions.MigrationsAssembly(migrationAssembly));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents
    = true;
    options.Events.RaiseSuccessEvents = true;

    options.EmitStaticAudienceClaim = true; // this makes sure the audience claim is always on the token, https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/api_scopes/
}).AddConfigurationStore(options => options.ConfigureDbContext = b => b.UseSqlite(connectionString, opt => opt.MigrationsAssembly(migrationAssembly)))
.AddOperationalStore(options => options.ConfigureDbContext = b => b.UseSqlite(connectionString, opt => opt.MigrationsAssembly(migrationAssembly)))
  .AddAspNetIdentity<IdentityUser>();

//.AddTestUsers(Config.Users);
//.AddInMemoryClients(Config.Clients)
//.AddInMemoryApiResources(Config.ApiResources)
//.AddInMemoryApiScopes(Config.ApiScopes)
//.AddInMemoryIdentityResources(Config.IdentityResources);
builder.Services.AddAuthentication();
builder.Host.UseSerilog((ctx, lc) =>
{
    // todo: set from app settings
    lc.MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
    theme: AnsiConsoleTheme.Code)
    .Enrich.FromLogContext();
});

var app = builder.Build();
app.UseIdentityServer(); // add identity server to the middleware pipeline

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();


if (args.Contains("/seed"))
{
    Log.Information("Seeding database");
    SeedData.EnsureSeedData(app);
    Log.Information("Done seeding database");

    return;
}

app.Run();
