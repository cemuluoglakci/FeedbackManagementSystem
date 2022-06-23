using System.Linq;
using System.Threading.Tasks;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using FmsWebUI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FmsWebUI.Middleware
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
            var token = context.Request.Cookies["Authorization"];//.Split(" ").Last();
            var userId = tokenHelper.ValidateJwtToken(token);

            if (userId != null)
            {
                context.Items["User"] = contextUserService.GetContextUser(userId.Value);
            }

            await _next(context);
        }
    }
}
