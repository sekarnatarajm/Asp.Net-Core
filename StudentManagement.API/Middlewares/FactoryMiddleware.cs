
namespace StudentManagement.API.Middlewares
{
    public class FactoryMiddleware(ILogger<FactoryMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("FactoryMiddleware Before  request");
            await next(context);
            _logger.LogInformation("FactoryMiddleware After request");
        }
    }
}
