using Auth.Context;
using Auth.Models;
using Auth.Ultilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;
using Shared.Settings;

namespace Auth.Helper.ServiceExtensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureOpenIdDict(this IServiceCollection services, IConfiguration configuration)
    {
        var authSettings = new AuthSettings();
        configuration.Bind(nameof(AuthSettings), authSettings);

        var encryptionKey = Convert.FromBase64String(authSettings.SecretKey);

        services.AddOpenIddict()
        .AddCore(options =>
        {
            // Configure OpenIddict to use the Entity Framework Core stores and models.
            options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
        })
        // Register the OpenIddict server components.
        .AddServer(options =>
        {
            //key that is shared between the server and the API project.
            options.AddEncryptionKey(new SymmetricSecurityKey(encryptionKey));
            options.AddDevelopmentEncryptionCertificate() // Register the signing and encryption credentials.
           .AddDevelopmentSigningCertificate();

            //// Enable the token endpoint.
            options.SetTokenEndpointUris("connect/token");

            options.AllowPasswordFlow();//// Enable the password flow.
            options.AcceptAnonymousClients(); // Accept anonymous clients (i.e clients that don't send a client_id)


            options.SetAccessTokenLifetime(TimeSpan.FromMinutes(60))
            .SetIdentityTokenLifetime(TimeSpan.FromMinutes(60))
            .SetRefreshTokenLifetime(TimeSpan.FromMinutes(120));

            options.SetIssuer(authSettings.Issuer);

            //ToDo: Generate and add certificate.pfx file
            //if (!string.IsNullOrEmpty(configuration["Identity:Certificates:EncryptionCertificatePath"]))
            //{
            //    var encryptionKeyBytes = File.ReadAllBytes(configuration["Identity:Certificates:EncryptionCertificatePath"]);
            //    X509Certificate2 encryptionKey = new X509Certificate2(encryptionKeyBytes, configuration["Identity:EncryptionCertificateKey"],
            //         X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.EphemeralKeySet);
            //    options.AddEncryptionCertificate(encryptionKey);
            //}
            //else
            //{
            //    options.AddDevelopmentEncryptionCertificate();
            //}

            //// Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
            options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough();
        })

        // Register the OpenIddict validation components.
        .AddValidation(options =>
        {
            // Note: the validation handler uses OpenID Connect discovery
            // to retrieve the issuer signing keys used to validate tokens.
            options.SetIssuer("https://localhost:7123/");

            options.UseSystemNetHttp();

            //// Import the configuration from the local OpenIddict server instance.
            options.UseLocalServer();

            options.UseAspNetCore();

        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
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

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(24);
        });
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
