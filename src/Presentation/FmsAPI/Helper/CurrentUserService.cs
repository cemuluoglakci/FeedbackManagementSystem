using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using Microsoft.AspNetCore.Http;

namespace FmsAPI.Helper
{
    public class CurrentUserService : ICurrentUser
    {
        private HttpContext _httpContext;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public ContextUser UserDetail => (ContextUser)_httpContext.Items["User"] ?? new ContextUser();

        public string RequestScheme => _httpContext.Request.Scheme;
        public string RequestHost => _httpContext.Request.Host.Value;
        public string RequestPath => _httpContext.Request.Path;
        public string RequestQueryString => _httpContext.Request.QueryString.Value;
    }
}
