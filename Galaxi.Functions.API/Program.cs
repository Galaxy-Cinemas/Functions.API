using Microsoft.Extensions.Configuration;
using Galaxi.Functions.Persistence;
using MediatR;
using System.Reflection;
using Galaxi.Functions.Domain.Profiles;
using Galaxi.Functions.Persistence.Repositorys;
using Galaxi.Functions.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Galaxi.Functions.Domain.IntegrationEvents.Consumers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services.BuildServiceProvider();
var configuration = service.GetService<IConfiguration>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UpdateFunctionConsumer>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(configuration.GetConnectionString("AzureServiceBus"));

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddInfrastructure(configuration);
builder.Services.AddAutoMapper(typeof(FunctionProfile).Assembly);
builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();
builder.Services.AddMediatR(Assembly.Load("Galaxi.Functions.Domain"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

// Add Authentication
var secretKey = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("SecretKey")
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

ApplyMigration();

app.MapControllers();

app.Run();
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<FunctionContextDb>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
