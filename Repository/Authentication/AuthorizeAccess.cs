using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace castlers.Repository.Authentication
{
    public class AuthorizeAccess : Attribute, IAuthorizationFilter
    {
        private List<string> Roles { get; set; }
        public AuthorizeAccess(string role)
        {
            Roles = role.Split(',').ToList();
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (context != null && context.HttpContext.User.Identity != null)
                {
                    bool isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
                    if (!isAuthenticated)
                    {
                        context.Result = new UnauthorizedObjectResult(string.Empty);
                        return;
                    }

                    bool canAccess = CheckAccessRole(context.HttpContext.User);
                    if (!canAccess)
                    {
                        context.Result = new UnauthorizedObjectResult("Premission Denied!");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckAccessRole(ClaimsPrincipal user)
        {
            try
            {
                string? role = user.FindFirstValue("userRole");
                if (role.Trim().Length == 0)
                {
                    return false;
                }

                var canAccess = Roles.Where(r => r.Equals(role)).ToList();
                if (canAccess.Any())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
