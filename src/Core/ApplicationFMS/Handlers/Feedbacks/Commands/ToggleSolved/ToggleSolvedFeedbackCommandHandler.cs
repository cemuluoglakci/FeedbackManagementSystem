using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved
{
    public class ToggleSolvedFeedbackCommandHandler : IRequestHandler<ToggleSolvedFeedbackCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleSolvedFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleSolvedFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "Current User Identity was not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.CustomerRole)
            {
                return new BaseResponse<int>(0, "Only customers are allowed to label feedbacks as solved.");
            }


            Feedback? feedback = _context.Feedback.FirstOrDefault(x => x.Id == request.Id);
            if (feedback == null)
            {
                return new BaseResponse<int>(0, "Feedback was not found.");
            }
            if (_currentUser.UserDetail.Id != feedback.UserId)
            {
                return new BaseResponse<int>(0, "Users are only allowed to edit their own feedback.");
            }

            feedback.IsSolved = !feedback.IsSolved;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(feedback.Id);
        }




    }
}
