using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleChecked
{
    public class ToggleCheckedCommentCommandHandler : IRequestHandler<ToggleCheckedCommentCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleCheckedCommentCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleCheckedCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsInRole(Constants.AdminRole))
            {
                return BaseResponse<int>.Fail("Only administrators are allowed to check / uncheck comments.");
            }

            Comment? comment = _context.Comment.FirstOrDefault(x => x.Id == request.Id);
            if (comment == null)
            {
                return BaseResponse<int>.Fail("Comment was not found.");
            }

            comment.IsChecked = !comment.IsChecked;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(comment.Id);
        }


    }
}
