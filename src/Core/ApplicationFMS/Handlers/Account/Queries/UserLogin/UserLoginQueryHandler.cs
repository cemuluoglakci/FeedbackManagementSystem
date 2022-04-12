using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using CoreFMS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Queries.UserLogin
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, BaseResponse<string>>
    {
        private readonly IFMSDataContext _context;


        public UserLoginQueryHandler(IFMSDataContext context)
        {
            _context = context;
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
                string jwt = TokenHelper.generateJwtToken(currentUser);
                return new BaseResponse<string>(jwt);
            }

        }
    }
}
