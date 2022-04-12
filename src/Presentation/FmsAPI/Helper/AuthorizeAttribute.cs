using CoreFMS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FmsAPI.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized", StatusCodes = 401 }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
