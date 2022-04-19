using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleActive
{
    public class ToggleActiveReplyCommandHandler : IRequestHandler<ToggleActiveReplyCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleActiveReplyCommandHandler (IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleActiveReplyCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "Current User Identity was not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.AdminRole)
            {
                return new BaseResponse<int>(0, "Only administrators are allowed to activate / deactivate replies.");
            }

            Reply? reply = _context.Reply.FirstOrDefault(x => x.Id == request.Id);
            if (reply == null)
            {
                return new BaseResponse<int>(0, "Reply was not found.");
            }

            reply.IsActive = !reply.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(reply.Id);
        }


    }
}
