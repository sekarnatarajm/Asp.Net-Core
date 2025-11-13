using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StudentManagement.API.BackgroundTask;
using StudentManagement.API.Exceptions;
using StudentManagement.API.Filters;
using StudentManagement.API.HealthCheck;
using StudentManagement.API.Middlewares;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(option =>
{
    option.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(120);
    option.Limits.MaxConcurrentConnections = 10;
    option.Limits.MaxConcurrentUpgradedConnections = 10;
});

//Add Memory Cache
builder.Services.AddMemoryCache();

//Add Background Services
//builder.Services.AddHostedService<TimedHostedService>();
//builder.Services.AddHostedService<TimedBackgroundService>();

//Add Exception Handler Services
builder.Services.AddExceptionHandler<GlobalExceptionHandelr>();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<FactoryMiddleware>();

//Cache
builder.Services.AddScoped<CustomCacheResourceFilter>();

builder.Services.AddControllers();
//builder.Services.AddTransient<FactoryMiddleware>();

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new ApiKeyValidationFilter());
    config.Filters.Add(new GlobalExceptionFilter());
    config.Filters.Add(new CustomRoleAuthorizeAttribute(""));
});
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<SqlCustomHealthCheck>("Custom Sql Server", HealthStatus.Unhealthy)
    .AddSqlServer("Server=localhost,1433;Database=CustomerSettings;User Id=sa;Password=Swiftdezire@123;TrustServerCertificate=True;")
    .AddRedis("127.0.0.1:6379");

builder.Services.AddHealthChecks()
    .AddCheck<SqlCustomHealthCheck>("Custom Sql Server health check", HealthStatus.Unhealthy)
    .AddSqlServer("connction string")
    .AddRedis("connction string");
//.AddSqlServer("Server=localhost,1433;Database=CustomerSettings;User Id=sa;Password=Swiftdezire@123", healthQuery : "Select 1", name: "SQL Server", 
//failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" });

var app = builder.Build();

app.Logger.LogInformation("Application Started");

app.Use(async (context, next) =>
{
    await next(context);
});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello Middleware 2..");
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = errorFeature?.Error;

        var errorResponse = new
        {
            Message = "An unexpected error occurred.",
            Detail = exception?.Message
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});


//app.UseHttpsRedirection();

//app.UseMiddleware<ExceptionHandler>();
//app.UseExceptionHandler();

app.UseAuthorization();

app.UseMiddleware<CustomHeaderMiddleware>();
app.MapControllers();

//app.UseMiddleware<ConventionMiddleware>();
//app.UseMiddleware<ConventionMiddleware1>();
//app.UseMiddleware<ConventionMiddleware2>();
//app.UseMiddleware<FactoryMiddleware>();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Map("/map1", MapMethod1);
//app.Map("/map2", MapMethod2);

app.MapPost("/students", async (string student = "") =>
{
    return Results.Ok(student);
})
.AddEndpointFilter<EndPointValidationFilter>();
//.AddEndpointFilter(async (context, next) =>
//{
//    var name = context.GetArgument<string>(0);
//    if (string.IsNullOrWhiteSpace(name))
//    {
//        return Results.BadRequest("Name is required.");
//    }
//    return await next(context);
//});

app.Run();

static void MapMethod1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        throw new Exception("Middle ware exception");
    });
}
static void MapMethod2(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map Test 2");
    });
}
