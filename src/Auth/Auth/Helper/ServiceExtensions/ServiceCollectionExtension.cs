using Auth.Context;
using Auth.Models;
using Auth.Ultilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Helper.ServiceExtensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureOpenIdDict(this IServiceCollection services)
    {
        services.AddOpenIddict()

        // Register the OpenIddict core components.
        .AddCore(options =>
        {
            // Configure OpenIddict to use the Entity Framework Core stores and models.
            // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
            options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
        })
        // Register the OpenIddict server components.
        .AddServer(options =>
        {

            // Register the encryption credentials. This sample uses a symmetric
            // encryption key that is shared between the server and the API project.
            //
            // Note: in a real world application, this encryption key should be
            // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
            options.AddEncryptionKey(new SymmetricSecurityKey(
                Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

            // Register the signing credentials.
            options.AddDevelopmentSigningCertificate();

            //////////////////////////////////////
            //// Enable the token endpoint.
            options.SetTokenEndpointUris("connect/token");

            //// Enable the password flow.
            options.AllowPasswordFlow();

            //// Accept anonymous clients (i.e clients that don't send a client_id).
            options.AcceptAnonymousClients();

            //// Register the signing and encryption credentials.
            //options.AddDevelopmentEncryptionCertificate()
            //       .AddDevelopmentSigningCertificate();

            //// Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
            options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough();
        })

        // Register the OpenIddict validation components.
        .AddValidation(options =>
        {
            // Note: the validation handler uses OpenID Connect discovery
            // to retrieve the issuer signing keys used to validate tokens.
            options.SetIssuer("https://localhost:7202/");

            // Register the encryption credentials. This sample uses a symmetric
            // encryption key that is shared between the server and the API project.
            //
            // Note: in a real world application, this encryption key should be
            // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
            options.AddEncryptionKey(new SymmetricSecurityKey(
                Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

            // Register the System.Net.Http integration.
            options.UseSystemNetHttp();
            /////////////////////////////////////////////////////

            //// Import the configuration from the local OpenIddict server instance.
            options.UseLocalServer();

            //// Register the ASP.NET Core host.
            options.UseAspNetCore();
        });
    }

    public static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var identitysettings = new IdentitySettings();
        configuration.Bind("IdentitySettings", identitysettings);

        // register the Identity services.
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
           options.Password.RequireNonAlphanumeric = true;
           options.User.RequireUniqueEmail = true;
           options.Lockout.AllowedForNewUsers = true;
           options.Password.RequireDigit = false;
           options.Password.RequireLowercase = true;
           options.Password.RequireUppercase = true;
           options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitysettings.LockoutMinutes);
           options.Lockout.MaxFailedAccessAttempts = identitysettings.MaxLockoutAttempt;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            options.UseOpenIddict();
        });
    }
}
