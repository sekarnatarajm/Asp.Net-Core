using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentManagement.API.Filters
{
    public class CustomRoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public CustomRoleAuthorizeAttribute(string role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated && !user.IsInRole(_role))
            {
               // context.Result = new ForbidResult();
            }
        }
    }
}
