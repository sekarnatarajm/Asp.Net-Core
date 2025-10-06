namespace StudentManagement.API.Middlewares
{
    public class ConventionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConventionMiddleware> _logger;
        public ConventionMiddleware(RequestDelegate next, ILogger<ConventionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Before request");
            await _next(context);
            _logger.LogInformation("After request");
        }
    }
    public class ConventionMiddleware1(RequestDelegate _next, ILogger<ConventionMiddleware> _logger)
    {
        //private readonly RequestDelegate _next;
        //private readonly ILogger<ConventionMiddleware> _logger;
        //public ConventionMiddleware1(RequestDelegate next, ILogger<ConventionMiddleware> logger)
        //{
        //    _next = next;
        //    _logger = logger;
        //}

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Before request 1");
            await _next(context);
            _logger.LogInformation("After request 1");
        }
    }
    public class ConventionMiddleware2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConventionMiddleware> _logger;
        public ConventionMiddleware2(RequestDelegate next, ILogger<ConventionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Before request 2");
            await _next(context);
            _logger.LogInformation("After request 2");
        }
    }
}
