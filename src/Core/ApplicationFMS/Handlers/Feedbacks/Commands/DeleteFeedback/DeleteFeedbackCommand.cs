using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DeleteFeedback
{
    public class DeleteFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }

        public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, BaseResponse<int>>
        {
            private readonly IFMSDataContext _context;
            private readonly ICurrentUser? _currentUser;

            public DeleteFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<BaseResponse<int>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Feedback.FindAsync(request.Id);
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
