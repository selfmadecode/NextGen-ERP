namespace auth.Constants;

public static class Config
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "818727",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                        IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser
                {
                    SubjectId = "88421113",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "user"),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                        IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };
        }
    }

    public static IEnumerable<IdentityResource> IdentityResources => new[]
    {
        new IdentityResources.OpenId(), // standard openid connect scopes for identity server to support
        new IdentityResources.Profile(), // standard openid connect scopes for identity server to support
        new IdentityResource
        {
            Name = "role",
            UserClaims = new List<string> {"role"}
        }
    };

    // the sco
    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope("hrapi.read"),
        new ApiScope("hrapi.write"),
    };

    // the services we are tryinf to protect are the resources, with two scopes (hrapi.read and hrapi.write)
    public static IEnumerable<ApiResource> ApiResources => new[]
    {
        new ApiResource("hrapi")
        {
            Scopes = new List<string> {"hrapi.read", "hrapi.write"},
            ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
            UserClaims = new List<string> {"role"}
        }
    };

    public static IEnumerable<Client> Clients => new[]
    {
        // m2m client credentials flow client
        // protects an api, for hr api
        new Client
        {
            ClientId = "m2m.client",
            ClientName = "Client Credentials Client",

            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

            AllowedScopes = {"hrapi.read", "hrapi.write"}
        },

        // interactive client using code flow + pkce
        new Client
        {
            ClientId = "interactive",
            ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

            AllowedGrantTypes = GrantTypes.Code,

            //RedirectUris = {"https://localhost:5150/signin-oidc"},
            RedirectUris = {"https://oauth.pstmn.io/v1/callback"},
            FrontChannelLogoutUri = "https://localhost:5150/signout-oidc",
            PostLogoutRedirectUris = {"https://localhost:5150/signout-callback-oidc"},

            AllowOfflineAccess = true,
            AllowedScopes = {"openid", "profile", "hrapi.read"},
            RequirePkce = true,
            //RequireConsent = true,
            AllowPlainTextPkce = false
        },
    };
}
