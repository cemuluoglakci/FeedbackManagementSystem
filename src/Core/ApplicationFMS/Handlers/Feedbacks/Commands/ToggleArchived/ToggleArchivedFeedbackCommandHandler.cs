using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleArchived
{
    public class ToggleArchivedFeedbackCommandHandler : IRequestHandler<ToggleArchivedFeedbackCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleArchivedFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleArchivedFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "Current User Identity was not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.CompanyEmployeeRole)
            {
                return new BaseResponse<int>(0, "Only Company Employees are allowed to label feedbacks as archived.");
            }

            Feedback? feedback = _context.Feedback.FirstOrDefault(x => x.Id == request.Id);
            if (feedback == null)
            {
                return new BaseResponse<int>(0, "Feedback was not found.");
            }
            if (_currentUser.UserDetail.CompanyId != feedback.CompanyId)
            {
                return new BaseResponse<int>(0, "Users are aloowed to olny manipulate feedbacks related to their company.");
            }
            if (_currentUser.UserDetail.Id != feedback.DirectedToEmployeeId)
            {
                return new BaseResponse<int>(0, "Users are aloowed to olny manipulate feedbacks which directed to them.");
            }

            feedback.IsArchived = !feedback.IsArchived;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(feedback.Id);
        }




    }
}
