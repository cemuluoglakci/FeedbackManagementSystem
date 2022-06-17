using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationFMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FmsWebUI.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roleNames;

        public AuthorizeAttribute(params string[] roleNames)
        {
            _roleNames = roleNames ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Check mode of operation

            var user = (ContextUser)context.HttpContext.Items["User"];
            if (user == null || (_roleNames.Any() && !_roleNames.Contains(user.RoleName)))
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized", StatusCodes = 401 }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
