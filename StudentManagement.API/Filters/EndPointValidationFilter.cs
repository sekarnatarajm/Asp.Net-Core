
namespace StudentManagement.API.Filters
{
    public class EndPointValidationFilter : IEndpointFilter
    {
        public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var name = context.GetArgument<string>(0);
            if (string.IsNullOrEmpty(name))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.HttpContext.Response.WriteAsync("Name is required");
            }
            return next(context);
        }
    }
}
