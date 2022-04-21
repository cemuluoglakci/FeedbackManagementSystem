using ApplicationFMS.Handlers.Comments.Commands.PostComment;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Commands
{
    public class DeleteReplyCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }

        public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand, BaseResponse<int>>
        {
            private readonly IFMSDataContext _context;
            private readonly ICurrentUser? _currentUser;

            public DeleteReplyCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<BaseResponse<int>> Handle(DeleteReplyCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Reply.FindAsync(request.Id);
                if (entity == null)
                {
                    return BaseResponse<int>.ReturnFailureResponse("Related entity was not found.");
                }
                if (!_currentUser.HasSameId(entity.UserId)) 
                {
                    return BaseResponse<int>.ReturnFailureResponse("Users can only delete their own posts");
                }

                entity.IsActive = false;

                await _context.SaveChangesAsync(cancellationToken);

                return new BaseResponse<int>(entity.Id);
            }
        }
    }
}
