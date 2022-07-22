using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.AttributeRoleVerification
{
    public class RequireUserRoleAttribute : Attribute, IAuthorizationFilter
    {
        public string[] UserRoles { get; set; }

        public RequireUserRoleAttribute(params string[] userRoles)
        {
            UserRoles = userRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAdmin = context.HttpContext.User.Claims
                .Any(x => x.Type == "RoleName" && x.Value == RoleNames.ADMIN.ToString());

            if (isAdmin)
                return;

            foreach (var role in UserRoles)
            {
                var hasClaim = context.HttpContext.User.Claims.Any(x => x.Type == "RoleName" && x.Value == role);

                if (hasClaim)
                    return;
            }

            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
        }
    }
}