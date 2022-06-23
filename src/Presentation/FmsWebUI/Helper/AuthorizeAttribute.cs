using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationFMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

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

                string errorMessage =
                    "Lütfen sistemin bütün hizmetlerinden faydalanmak için giriş yapın ya da kayıt olun.";

                //context.ac.HttpContext.Items..Controller.TempData["AuthMsg "] = "Sorry, unauthorized";
                //context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Index", ErrorMessage=errorMessage}));

                // not logged in
                //context.Result = new JsonResult(new { message = "Unauthorized", StatusCodes = 401 }) { StatusCode = StatusCodes.Status401Unauthorized };
                context.Result = new RedirectToActionResult("Index", "Error", new { errorMessage = "Lütfen sistemin bütün hizmetlerinden faydalanmak için giriş yapın ya da kayıt olun." });
                
            }
        }
    }
}
