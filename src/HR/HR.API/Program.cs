using OpenIddict.Validation.AspNetCore;
using Shared;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger("NextGen HR Platform API");
builder.Services.AddSwaggerGen();
builder.Services.AddMongo(builder.Configuration);
//builder.Services.AddMongoRepository<Department>("departments");
//builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransitWithRabbitMq(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.ConfigureOpenIdDictValidation(builder.Configuration);

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
