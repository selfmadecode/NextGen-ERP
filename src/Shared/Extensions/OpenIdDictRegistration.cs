using Microsoft.IdentityModel.Tokens;
using Shared.Settings;

namespace Shared
{
    public static class OpenIdDictRegistration
    {
        public static void ConfigureOpenIdDictValidation(this IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = new AuthSettings();
            configuration.Bind(nameof(AuthSettings), authSettings);

            var encryptionKey = Convert.FromBase64String(authSettings.SecretKey);

            services.AddOpenIddict()
            .AddValidation(options =>
            {
                // Note: the validation handler uses OpenID Connect discovery
                // to retrieve the issuer signing keys used to validate tokens.
                options.SetIssuer(authSettings.Issuer);

                //key that is shared between the server and the API project.
                options.AddEncryptionKey(new SymmetricSecurityKey(encryptionKey));

                // Register the System.Net.Http integration.
                options.UseSystemNetHttp();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });
        }
    }
}
