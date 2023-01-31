using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ItemsEditorApi.Auth
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        readonly static string _moduleName = "Zarządzanie Items - administracja";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionsService = context.HttpContext.RequestServices.GetService<IPermissionsService>();
            var token = GetAuthToken(context);

            var result = permissionsService.UserHasRightsToModule(token, _moduleName).GetAwaiter().GetResult();
            if (!result)
                context.Result = new UnauthorizedObjectResult(null);
        }

        private string GetAuthToken(AuthorizationFilterContext context)
        {

            string token = context.HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return token;
            }
            if (token.Contains("Bearer ", StringComparison.InvariantCultureIgnoreCase))
                return token.Substring("Bearer ".Length);

            return token;
        }
    }
}
