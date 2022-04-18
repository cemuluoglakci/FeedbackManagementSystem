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
    public class ToggleCheckedReplyCommandHandler : IRequestHandler<ToggleCheckedReplyCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleCheckedReplyCommandHandler (IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleCheckedReplyCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "Current User Identity was not defined.");
            }
            if (_currentUser.UserDetail.RoleName != PreDefinedTypes._adminRole)
            {
                return new BaseResponse<int>(0, "Only administrators are allowed to activate / deactivate replies.");
            }

            Reply? reply = _context.Reply.FirstOrDefault(x => x.Id == request.Id);
            if (reply == null)
            {
                return new BaseResponse<int>(0, "Feedback was not found.");
            }

            reply.IsChecked = !reply.IsChecked;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(reply.Id);
        }


    }
}
