using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Queries.UserLogin
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, BaseResponse<string>>
    {
        private readonly IFMSDataContext _context;
        private readonly JwtSetting _jwtSettings;

        public UserLoginQueryHandler(IFMSDataContext context, IOptions<JwtSetting> JwtSettingOptions)
        {
            _context = context;
            _jwtSettings = JwtSettingOptions.Value;
        }

        public async Task<BaseResponse<string>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            if (!_context.User.Any(x => x.Email == request.Email && x.IsActive))
            {
                return new BaseResponse<string>(null, "This E-mail is not registered to the system!");
            }

            var currentUser = await _context.User.FirstOrDefaultAsync(x => x.Email == request.Email && x.IsActive);

            if (currentUser.LastFailedLoginAt < DateTime.Now.AddMinutes(30) && currentUser.FailedLoginAttemptCount >= 3)
            {
                return new BaseResponse<string>(null, "Your account is locked, please try again later.");
            }

            if (!Security.CheckPassword(request.Password, currentUser.Salt, currentUser.Hash))
            {
                currentUser.LastFailedLoginAt = DateTime.Now;
                currentUser.FailedLoginAttemptCount ++;

                await _context.SaveChangesAsync(cancellationToken);
                return new BaseResponse<string>(null, "Incorrect password!");
            }
            else
            {
                TokenHelper tokenHelper = new TokenHelper(_jwtSettings);
                string jwt = tokenHelper.GenerateJwtToken(currentUser);

                currentUser.LastLoginAt = DateTime.Now;
                currentUser.FailedLoginAttemptCount = 0;

                await _context.SaveChangesAsync(cancellationToken);
                return new BaseResponse<string>(jwt);
                 
            }

        }
    }
}
