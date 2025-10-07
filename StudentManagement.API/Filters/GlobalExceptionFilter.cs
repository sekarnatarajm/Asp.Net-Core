using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentManagement.API.Exceptions;

namespace StudentManagement.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                Exceptions.UnauthorizedAccessException => StatusCodes.Status500InternalServerError,                
                _ => StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(new
            {
                error = context.Exception.Message,
                stackTrace = context.Exception.StackTrace
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
