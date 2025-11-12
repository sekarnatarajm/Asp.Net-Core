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
            context.Request.Headers["ApiKey"] = "8956-6462-3896";

            // Add a custom header to the response
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["ApiKey"] = "8956-6462-3896";
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
