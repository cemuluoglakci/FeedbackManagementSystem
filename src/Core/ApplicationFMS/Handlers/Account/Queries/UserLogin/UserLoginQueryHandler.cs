using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Queries.UserLogin
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, BaseResponse<string>>
    {
        private readonly IFMSDataContext _context;
        private readonly JwtSetting _jwtSettings;

        public UserLoginQueryHandler(IFMSDataContext context, IOptions<JwtSetting> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<BaseResponse<string>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            if (!_context.User.Any(x => x.Email == request.Email && x.IsActive == 1))
            {
                return new BaseResponse<string>(null, "Can not find email within Active Users!");
            }
            var currentUser = await _context.User.FirstOrDefaultAsync(x => x.Email == request.Email && x.IsActive == 1);

            if (!Security.CheckPassword(request.Password, currentUser.Salt, currentUser.Hash))
            {
                return new BaseResponse<string>(null, "Incorrect password!");
            }
            else
            {
                TokenHelper tokenHelper = new TokenHelper(_jwtSettings);
                string jwt = tokenHelper.GenerateJwtToken(currentUser);
                return new BaseResponse<string>(jwt);
            }

        }
    }
}
