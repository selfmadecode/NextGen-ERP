using Auth.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Security.Claims;
using OpenIddict.Abstractions;
using System.Collections.Immutable;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) 
        : ControllerBase
    {
        [HttpPost("~/connect/token"), IgnoreAntiforgeryToken, Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("OpenIddict server request is null.");

            if(string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidRequest,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password is invalid."
                });
            }

            if (request.IsPasswordGrantType())
            {
                var user = await userManager.FindByNameAsync(request.Username);

                if (user == null)
                {
                    return BadRequest(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The username/password is invalid."
                    });
                }

                if (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user))
                {
                    var lockoutMinutes = userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalMinutes;

                    return BadRequest(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.AccessDenied,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            $"Your account has been locked. " +
                        $"Please contact the platform Administrators immediately or wait for {lockoutMinutes} minute(s) to retry"
                    });
                }

                // Validate the username/password parameters and ensure the account is not locked out.
                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

                if (!result.Succeeded)
                {
                    return BadRequest(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The username/password is invalid."
                    });
                }

                if (userManager.SupportsUserLockout)
                {
                    await userManager.ResetAccessFailedCountAsync(user);
                }

                user.LastLoginDate = DateTime.UtcNow;
                await userManager.UpdateAsync(user);

                var identity = await CreateTicketAsync(user, request);

                return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new NotImplementedException("The specified grant type is not implemented.");
        }

        private async Task<ClaimsIdentity> CreateTicketAsync(ApplicationUser user, OpenIddictRequest request)
        {
            // Create the claims-based identity that will be used by OpenIddict to generate tokens.
            var identity = new ClaimsIdentity(
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: Claims.Name,
                roleType: Claims.Role);

            AddUserClaims(user, identity);

            // Add the claims that will be persisted in the tokens.
            identity.SetClaim(Claims.Subject, await userManager.GetUserIdAsync(user))
                    .SetClaim(Claims.Email, await userManager.GetEmailAsync(user))
                    .SetClaim(Claims.Name, await userManager.GetUserNameAsync(user))
                    .SetClaim(Claims.PreferredUsername, await userManager.GetUserNameAsync(user))
                    .SetClaims(Claims.Role, (await userManager.GetRolesAsync(user)).ToImmutableArray());

            // Set the list of scopes granted to the client application.
            identity.SetScopes(new[]
            {
                Scopes.OpenId,
                Scopes.Email,
                Scopes.Profile,
                Scopes.Roles
            }.Intersect(request.GetScopes()));

            identity.SetDestinations(GetDestinations);

            return identity;
        }

        private static void AddUserClaims(ApplicationUser user, ClaimsIdentity identity)
        {
            identity.AddClaim(Claims.GivenName, $"{user.FirstName} {user.LastName}");
        }

        private static IEnumerable<string> GetDestinations(Claim claim)
        {
            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            switch (claim.Type)
            {
                case Claims.Name or Claims.PreferredUsername:
                    yield return Destinations.AccessToken;

                    if (claim.Subject.HasScope(Scopes.Profile))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (claim.Subject.HasScope(Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (claim.Subject.HasScope(Scopes.Roles))
                        yield return Destinations.IdentityToken;

                    yield break;

                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }
    }
}
