using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleChecked
{
    public class ToggleCheckedReplyCommandHandler : IRequestHandler<ToggleCheckedReplyCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleCheckedReplyCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(ToggleCheckedReplyCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse(0, "Current User Identity was not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.AdminRole)
            {
                return new BaseResponse(0, "Only administrators are allowed to activate / deactivate replies.");
            }

            Reply? reply = _context.Reply.FirstOrDefault(x => x.Id == request.Id);
            if (reply == null)
            {
                return new BaseResponse(0, "Reply was not found.");
            }

            reply.IsChecked = !reply.IsChecked;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse(reply.Id);
        }


    }
}
