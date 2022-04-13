using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using FmsAPI.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace FmsAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<JwtSetting> _jwtSetting;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSetting> jwtSetting)
        {
            _next = next;
            _jwtSetting = jwtSetting;
        }

        public async Task Invoke(HttpContext context, IContextUserService contextUserService, ITokenHelper tokenHelper)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenHelper.ValidateJwtToken(token);

            //var currentContextUser = tokenHelper.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                //will be refactored later

                //context.Items["User"] = userService.GetById(userId.Value);
                //var currentUser = _context.User.FirstOrDefault(x => x.Id == userId && x.IsActive == 1);
                //var currentContextUser = _mapper.Map<ContextUser>(currentUser);
                //context.Items["User"] = currentContextUser;
                context.Items["User"] = contextUserService.GetContextUser(userId.Value);
            }

            await _next(context);
        }
    }
}
