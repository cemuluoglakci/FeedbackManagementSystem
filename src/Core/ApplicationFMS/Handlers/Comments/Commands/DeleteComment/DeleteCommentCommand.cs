using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, BaseResponse>
        {
            private readonly IFMSDataContext _context;
            private readonly ICurrentUser? _currentUser;

            public DeleteCommentCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<BaseResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Comment.FindAsync(request.Id);
                if (entity == null)
                {
                    return BaseResponse.Fail("Related entity was not found.");
                }
                if (!_currentUser.HasSameId(entity.UserId))
                {
                    return BaseResponse.Fail("Users can only delete their own posts");
                }

                entity.IsActive = false;

                await _context.SaveChangesAsync(cancellationToken);

                return new BaseResponse(entity.Id);
            }

        }
    }
}
