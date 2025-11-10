namespace StudentManagement.API.Middlewares
{
    public class CustomHeaderMiddleware(ILogger<CustomHeaderMiddleware> _logger, RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Required-Header"))
            {
                //throw new InvalidOperationException("Missing required header: X-Required-Header");
            }

            // Add a custom header to the request (not common, but possible)
            context.Request.Headers["ApiKey"] = "895664623";

            // Add a custom header to the response
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["ApiKeyOne"] = "MyValue";
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
