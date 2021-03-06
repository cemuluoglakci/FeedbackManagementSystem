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
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly JwtSetting _jwtSettings;
        private readonly int accountLockPeriod = 30;

        public UserLoginQueryHandler(IFMSDataContext context, IOptions<JwtSetting> JwtSettingOptions)
        {
            _context = context;
            _jwtSettings = JwtSettingOptions.Value;
        }

        public async Task<BaseResponse> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            if (!_context.User.Any(x => x.Email == request.Email && x.IsActive))
            {
                return BaseResponse.Fail("This E-mail is not registered to the system!");
            }

            var currentUser = await _context.User.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == request.Email && x.IsActive);

            if (!currentUser.IsVerified)
            {
                return BaseResponse.Fail("Please verify your email.");
            }

            if (currentUser.LastFailedLoginAt > DateTime.Now.AddMinutes(accountLockPeriod) && currentUser.FailedLoginAttemptCount >= 3)
            {
                return BaseResponse.Fail("Your account is locked, please try again later.");
            }

            if (!Security.CheckPassword(request.Password, currentUser.Salt, currentUser.Hash))
            {
                currentUser.LastFailedLoginAt = DateTime.Now;
                currentUser.FailedLoginAttemptCount ++;

                await _context.SaveChangesAsync(cancellationToken);
                return BaseResponse.Fail("Incorrect password!");
            }
            else
            {
                TokenHelper tokenHelper = new TokenHelper(_context, _jwtSettings);
                string jwt = tokenHelper.GenerateJwtToken(currentUser);

                currentUser.LastLoginAt = DateTime.Now;
                currentUser.FailedLoginAttemptCount = 0;

                await _context.SaveChangesAsync(cancellationToken);
                return new BaseResponse(jwt);
                 
            }

        }
    }
}
