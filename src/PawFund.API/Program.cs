using PawFund.API.DependencyInjection.Extensions;
using PawFund.API.Middleware;
using PawFund.Application.DepedencyInjection.Extensions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
using PawFund.Persistence.DependencyInjection.Options;
using PawFund.Persistence.DependencyInjection.Extensions;
using PawFund.Infrastructure.DependencyInjection.Extensions;
using PawFund.Infrastructure.Dapper.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add configuration

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

builder.Services.AddConfigureMediatR();

builder
    .Services
    .AddControllers()
    .AddApplicationPart(PawFund.Presentation.AssemblyReference.Assembly);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

// Configure Options and SQL
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddConfigurationAutoMapper();

// Configure Dapper
builder.Services.AddInfrastructureDapper();

// Configure Options and Redis
builder.Services.AddConfigurationRedis(builder.Configuration);

builder.Services.AddConfigurationService();

builder.Services.AddConfigurationAppSetting(builder.Configuration);

builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Config CORS
builder.Services.AddCors(options =>
{
    var clientUrl = builder.Configuration["ClientConfiguration:Url"];
    options.AddPolicy("AllowSpecificOrigin",
        option =>
        {
            option.WithOrigins(clientUrl)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

public partial class Program { }
