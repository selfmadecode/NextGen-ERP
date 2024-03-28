using Microsoft.OpenApi.Models;

namespace Shared.Extensions;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection services, string title)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = title,
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "NextGen Admin",
                    Email = "Info@nextgen.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://en.wikipedia.org/wiki/MIT_Lincense")
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using Bearer scheme. \r\n\r\n" +
                "Enter 'Bearer' [space] and then your token in the input below.\r\n\r\n" +
                "Example: \"Bearer 123456\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new string[] {}
                }
            });

        });
    }
}
