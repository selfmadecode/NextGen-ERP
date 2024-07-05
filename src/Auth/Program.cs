using auth.Constants;
using Duende.IdentityServer.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    options.EmitStaticAudienceClaim = true; // makes sure the audience claim is always on the token, https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/api_scopes/
}).AddTestUsers(Config.Users)
.AddInMemoryClients(Config.Clients)
.AddInMemoryApiResources(Config.ApiResources)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddInMemoryIdentityResources(Config.IdentityResources);

var app = builder.Build();
app.UseIdentityServer(); // add identity server to the middleware pipeline
app.MapGet("/", () => "Hello World!");

app.Run();
