using ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility;
using ApplicationFMS.Helpers;
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

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive
{
    public class ToggleActiveFeedbackCommandHandler : IRequestHandler<ToggleActiveFeedbackCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ToggleActiveFeedbackCommandHandler (IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(ToggleActiveFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "Current User Identity was not defined.");
            }
            if (_currentUser.UserDetail.RoleName != PreDefinedTypes._adminRole)
            {
                return new BaseResponse<int>(0, "Only administrators are allowed to activate / deactivate feedbacks.");
            }

            Feedback? feedback = _context.Feedback.FirstOrDefault(x => x.Id == request.Id);
            if (feedback == null)
            {
                return new BaseResponse<int>(0, "Feedback was not found.");
            }

            feedback.IsActive = !feedback.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<int>(feedback.Id);
        }




    }
}
