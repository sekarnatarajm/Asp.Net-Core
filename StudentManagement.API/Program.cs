using StudentManagement.API.Filters;
using StudentManagement.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<FactoryMiddleware>();

builder.Services.AddControllers();
//builder.Services.AddTransient<FactoryMiddleware>();

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new GlobalExceptionFilter());
});
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

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
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ConventionMiddleware>();
//app.UseMiddleware<ConventionMiddleware1>();
//app.UseMiddleware<ConventionMiddleware2>();
//app.UseMiddleware<FactoryMiddleware>();


//app.Map("/map1", MapMethod1);
//app.Map("/map2", MapMethod2);

app.Run();

static void MapMethod1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map Test 1");
    });
}
static void MapMethod2(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map Test 2");
    });
}
