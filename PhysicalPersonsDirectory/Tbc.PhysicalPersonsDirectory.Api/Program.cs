using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.ErrorHandling;
using Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.LocalizationLanguage;
using Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Swagger;
using Tbc.PhysicalPersonsDirectory.Application;
using Tbc.PhysicalPersonsDirectory.Infrastructure;
using Tbc.PhysicalPersonsDirectory.Persistence;
using Tbc.PhysicalPersonsDirectory.Persistence.Contexts;
using Tbc.PhysicalPersonsDirectory.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

// Add health check with service and database
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString(nameof(PhysicalPersonsContext))!,
        name: "Database",
        timeout: TimeSpan.FromSeconds(5),
        failureStatus: HealthStatus.Unhealthy);
builder.Services.AddControllers()
    //  .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.CamelCase, allowIntegerValues: false)))
    .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = ModelStateValidatorExpression.ProcessValidationError);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Register the custom operation filter for adding the Accept-Language header
    options.OperationFilter<AddAcceptLanguageHeaderParameter>();
    // we can use this filters also for enums
    //magalitad ricxvebi ro chans shegvidzlia sityvebi gamovachinot da ufro martivi iqneba
    //vizualistvis(satesto davalebistvis vpiqrob esec sakmarisia)
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("RestrictedOrigins",
            builder =>
            {
                builder.WithOrigins("https://tbc-frontend-app.com")
                       .WithMethods("POST", "GET")
                       .AllowAnyHeader();
            });
    });
}

var app = builder.Build();

// Apply migrations
app.ApplyMigrations();

// Configure the HTTP request pipeline
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<LocalizationMiddleware>();

// Apply CORS policy here
app.UseCors(builder.Environment.IsDevelopment() ? "AllowAllOrigins" : "RestrictedOrigins");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();