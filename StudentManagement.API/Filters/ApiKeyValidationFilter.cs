using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentManagement.API.Filters
{
    public class ApiKeyValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var keyAvailable))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
