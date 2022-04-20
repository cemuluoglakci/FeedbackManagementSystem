using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleActive
{
    public class ToggleActiveCommentCommandHandler : IRequestHandler<ToggleActiveCommentCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleActiveCommentCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleActiveCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsInRole(Constants.AdminRole))
            {
                return BaseResponse<int>.ReturnFailureResponse("Only administrators are allowed to activate / deactivate comments.");
            }

            Comment? comment = _context.Comment.FirstOrDefault(x => x.Id == request.Id);
            if (comment == null)
            {
                return BaseResponse<int>.ReturnFailureResponse("Comment was not found.");
            }

            comment.IsActive = !comment.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(comment.Id);
        }
    }
}
