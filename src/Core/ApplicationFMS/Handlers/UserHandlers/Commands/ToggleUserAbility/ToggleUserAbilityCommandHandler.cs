using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility
{
    public class ToggleUserAbilityCommandHandler : IRequestHandler<ToggleUserAbilityCommand, BaseResponse>
    {

        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleUserAbilityCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(ToggleUserAbilityCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse(0, "Current User Identity was not defined.");
            }

            User? user = _context.User.FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
            {
                return new BaseResponse(0, "User was not found.");
            }

            //Company representatives will be allowed to display and manipulate only users related to their company.
            if (_currentUser.UserDetail.RoleName == "Company Representative")
            {
                if (user.CompanyId != _currentUser.UserDetail.CompanyId)
                {
                    return new BaseResponse(0, "Company Representatives are only allowed to manage users from their own company.");
                }
            }
            else if (_currentUser.UserDetail.RoleName != "System Administrator")
            {
                return new BaseResponse(0, "User role is not authorized.");
            }

            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse(user.Id);
        }

    }
}
