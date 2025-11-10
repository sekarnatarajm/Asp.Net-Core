using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StudentManagement.API.Exceptions
{
    public class ExceptionHandler(ILogger<ExceptionHandler> _logger, RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception occured while processing the request");

                var problemDetail = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Exception occured while processing the request"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetail);
            }
        }
    }
}
