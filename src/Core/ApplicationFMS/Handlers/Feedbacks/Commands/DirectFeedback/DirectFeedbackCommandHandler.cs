using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DirectFeedback
{
    public class DirectFeedbackCommandHandler : IRequestHandler<DirectFeedbackCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public DirectFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(DirectFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "User Identity could not defined.");
            }

            Feedback feedback = _context.Feedback.Find(request.FeedbackId);
            if (feedback == null)
            {
                return new BaseResponse<int>(0, "Feedback was not found.");
            }

            if (_currentUser.UserDetail.CompanyId != feedback.CompanyId || _currentUser.UserDetail.RoleName != Constants.CompanyRepresentativeRole)
            {
                return new BaseResponse<int>(0, "Only related company representatives can direct feedbacks.");
            }

            User companyEmployee = await _context.User.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
            if (companyEmployee == null)
            {
                return new BaseResponse<int>(0, "Company employee was not found.");
            }
            if (companyEmployee.CompanyId != feedback.CompanyId || companyEmployee.Role.RoleName != Constants.CompanyEmployeeRole)
            {
                return new BaseResponse<int>(0, "User is not authorized to reply feedbacks in the name of the company");
            }

            feedback.DirectedToEmployeeId = request.EmployeeId;

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<int>(feedback.Id);
        }




    }
}
