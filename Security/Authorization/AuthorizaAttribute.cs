using Common;
using DataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.AccessControl;

namespace Security.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] {};
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //skip
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }
            //author
            var user =(User) context.HttpContext.Items["User"];       

            var list_roles = _roles.ToList();    

            if (user == null || (_roles.Any() && !list_roles.Contains(user.Role))) throw new UnauthorizedError(Constants.INVALID_ROLE);
        }
    }
}
