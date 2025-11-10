using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.API.Exceptions
{
    public class GlobalExceptionHandelr(ILogger<GlobalExceptionHandelr> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError("Exception occured while processing the request");

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Exception occured while processing the request"
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetail);
            return true;
        }
    }
}
